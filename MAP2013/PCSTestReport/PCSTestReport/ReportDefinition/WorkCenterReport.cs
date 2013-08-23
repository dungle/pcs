using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSComUtils.Common;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace WorkCenterReport
{
	/// <summary>
	/// Objective: Manage stock, in/out, assess delivery progress of vendor, warning stock.
	/// Paper Size: A3.
	/// Orientaion: Lanscape.
	/// </summary>
	[Serializable]
	public class WorkCenterReport : MarshalByRefObject, IDynamicReport
	{
		#region Enum

		/// <summary>
		/// Type of row in data source
		/// </summary>
		private enum RowTypeEnum
		{
			/// <summary>
			/// Delivery Plan Row
			/// </summary>
			DeliveryPlan = 0,
			/// <summary>
			/// Delivery Actual Row
			/// </summary>
			DeliveryActual = 1,
			/// <summary>
			/// Total Delivery Plan Row
			/// </summary>
			TotalDeliveryPlan = 2,
			/// <summary>
			/// Total Delivery Actual Row
			/// </summary>
			TotalDeliveryActual = 3,
			/// <summary>
			/// Total Production Plan
			/// </summary>
			ProductionPlan = 4,
			/// <summary>
			/// Total Production Actual
			/// </summary>
			ProductionActual = 5,
			/// <summary>
			/// Out Abnormal Row
			/// </summary>
			OutAbnormal = 6,
			/// <summary>
			/// Production Progress Row
			/// </summary>
			Progress = 7,
			/// <summary>
			/// Stock Plan Row
			/// </summary>
			StockPlan = 8,
			/// <summary>
			/// Stock Actual Row
			/// </summary>
			StockActual = 9,
			/// <summary>
			/// Warning Stock Row
			/// </summary>
			WarningStock = 10,
			/// <summary>
			/// Not Goods Part Return Row
			/// </summary>
			NG_Part_Return = 11,
			/// <summary>
			/// Delivery Progress Row
			/// </summary>
			ProgressDelivery = 12,
			/// <summary>
			/// Stock Actual for NG Part
			/// </summary>
			StockActualNG = 13,
			/// <summary>
			/// Production actual but NG
			/// </summary>
			ProductionActualNG = 14,
			/// <summary>
			/// Repair NG Part
			/// </summary>
			RepairNG = 15
		}

		/// <summary>
		/// Type of product in master data
		/// </summary>
		private enum ProductType
		{
			/// <summary>
			/// Finished Goods
			/// </summary>
			FinishedGoods = 0,
			/// <summary>
			/// Component
			/// </summary>
			Component = 1
		}
		private enum ViewType
		{
			MakeItem = 1,
			NoneMake = 2,
			Both = 3
		}

		#endregion

		public WorkCenterReport()
		{
		}

		private string mConnectionString = string.Empty;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		private ReportBuilder mReportBuilder;

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		private C1PrintPreviewControl mReportViewer;

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mReportViewer; }
			set { mReportViewer = value; }
		}

		private object mResult;

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}

		private bool mUseViewer;

		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseViewer; }
			set { mUseViewer = value; }
		}

		private string mDefinitionFolder;

		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>		
		public string ReportDefinitionFolder
		{
			get { return mDefinitionFolder; }
			set { mDefinitionFolder = value; }
		}

		private string mLayoutFile;

		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get { return this.mLayoutFile; }
			set { mLayoutFile = value; }
		}


		#region constants

		const string PRODUCTION_PLAN_TABLE = "ProductionPlan";
		const string ROW_TYPE_FLD = "RowType";
		const string PRODUCT_TYPE_COL = "ProductType";
		const string CATEGORY_ID_FLD = "CategoryID";
		const string VENDOR_FLD = "Vendor";
		const string SOURCE_FLD = "Source";
		const string PARTNUMBER_FLD = "PartsNumber";
		const string PARTNAME_FLD = "PartsName";
		const string CATEGORY_FLD = "Category";
		const string UNIT_FLD = "Unit";
		const string BOM_FLD = "Bom";
		const string CONTENT_FLD = "Content";
		const string SUBCONTENT_FLD = "SubContent";
		const string MODEL_FLD = "Model";
		const string BEGIN_FLD = "Begin";
		const string SAFETYSTOCK_FLD = "SafetyStock";
		const string DELIVERY_CONTENT = "Delivery";
		const string TOTAL_FLD = "Total";
		const string PRODUCTION_CONTENT = "Production";
		const string STOCK_CONTENT = "Stock";
		const string PLAN_CONTENT = "Plan";
		const string ACTUAL_CONTENT = "Actual";
		const string PROGRESS_CONTENT = "Progress";
		const string NG_PART_RETURN_CONTENT = "NG Part Return";
		const string OUT_ABNORMAL_CONTENT = "Out Abnormal";
		const string WARNING_STOCK = "Warning";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrMethod">name of the method to call (which declare in the DynamicReport C# file)</param>
		/// <param name="pobjParameters">Array of parameters provide to call the Method with method name = pstrMethod</param>
		/// <returns></returns>
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		/// <summary>
		/// Execute report and return data
		/// </summary>
		/// <param name="pstrCCNID">CCN</param>
		/// <param name="pstrYear">Year</param>
		/// <param name="pstrMonth">Month</param>
		/// <param name="pstrProductionLineID">Production Line</param>
		/// <returns>DataTable</returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID,
			string pstrCategoryID, string pstrModel, string pstrProductID, string pstrViewType)
		{
			
			#region local variables

			int intViewType = (int)ViewType.Both;
			if (pstrViewType != null && pstrViewType != string.Empty)
			{
				try
				{
					bool blnMakeItem = Convert.ToBoolean(pstrViewType);
					if (blnMakeItem)
						intViewType = 1;
					else
						intViewType = 2;
				}
				catch{}
			}

			DateTime dtmFromDate = new DateTime(int.Parse(pstrYear), int.Parse(pstrMonth), 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddSeconds(-1);
			// list of all finished goods in work center
			ArrayList arrFGIDs = new ArrayList();
			StringBuilder strFGIDs = new StringBuilder();
			// list of all raw materials in work center
			ArrayList arrRMIDs = new ArrayList();
			StringBuilder strRMIDs = new StringBuilder();
			// list of detail item
			ArrayList arrDetailItemIDs = new ArrayList();
			StringBuilder strDetailItemIDs = new StringBuilder();

			#endregion

			#region create table schema

			DataTable dtbMasterData = new DataTable();
			// general row
			dtbMasterData.Columns.Add(new DataColumn("ProductID", typeof (int)));
			dtbMasterData.Columns["ProductID"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("CategoryID", typeof (int)));
			dtbMasterData.Columns["CategoryID"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("ProductType", typeof (int)));
			dtbMasterData.Columns["ProductType"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("RowType", typeof (int)));
			dtbMasterData.Columns["RowType"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Vendor", typeof (string)));
			dtbMasterData.Columns["Vendor"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Source", typeof (string)));
			dtbMasterData.Columns["Source"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("PartsNumber", typeof (string)));
			dtbMasterData.Columns["PartsNumber"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("PartsName", typeof (string)));
			dtbMasterData.Columns["PartsName"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Category", typeof (string)));
			dtbMasterData.Columns["Category"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Unit", typeof (string)));
			dtbMasterData.Columns["Unit"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Bom", typeof (decimal)));
			dtbMasterData.Columns["Bom"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Content", typeof (string)));
			dtbMasterData.Columns["Content"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("SubContent", typeof (string)));
			dtbMasterData.Columns["SubContent"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Model", typeof (string)));
			dtbMasterData.Columns["Model"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("Begin", typeof (decimal)));
			dtbMasterData.Columns["Begin"].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("SafetyStock", typeof (decimal)));
			dtbMasterData.Columns["SafetyStock"].AllowDBNull = true;
			// now add column for each day
			for (int i = dtmFromDate.Day; i <= dtmToDate.Day; i++)
			{
				dtbMasterData.Columns.Add(new DataColumn("D" + i.ToString("00"), typeof (decimal)));
				dtbMasterData.Columns["D" + i.ToString("00")].AllowDBNull = true;
			}
			// add total column
			dtbMasterData.Columns.Add(new DataColumn(TOTAL_FLD, typeof (decimal)));
			dtbMasterData.Columns[TOTAL_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn("ComponentID", typeof (int)));
			dtbMasterData.Columns["ComponentID"].AllowDBNull = true;

			#endregion

			// gets the list of items produce in main work center of production line
			DataTable dtbProduce = GetItemProduceInProLine(pstrProductionLineID, pstrCategoryID, pstrModel, pstrProductID);
			if (dtbProduce.Rows.Count == 0)
				return dtbMasterData;

			#region prepare data
			DataTable dtbFG = new DataTable();
			dtbFG.Columns.Add(new DataColumn("ProductID", typeof (int)));
			dtbFG.Columns["ProductID"].Unique = true;
			dtbFG.Columns.Add(new DataColumn("SafetyStock", typeof (decimal)));
			dtbFG.Columns["SafetyStock"].AllowDBNull = true;
			dtbFG.Columns["SafetyStock"].DefaultValue = 0;
			dtbFG.Columns.Add(new DataColumn("MakeItem", typeof (bool)));
			dtbFG.Columns["MakeItem"].AllowDBNull = true;
			dtbFG.Columns["MakeItem"].DefaultValue = true;
			DataTable dtbRM = dtbFG.Clone();
			dtbRM.Columns["MakeItem"].DefaultValue = false;
			DateTime dtmServerDate = GetDBDate();

			DataTable dtbAllProduct = GetAllProduct(pstrCCNID);
			// gets BOM structure
			DataTable dtbBOM = GetBOMStructure();
			
			DataTable dtbWorkingTime = GetWorkingTime();
			string strLocationID;
			GetMasterLocationOfProductionLine(pstrProductionLineID, out strLocationID);

			#region Master Items

			foreach (DataRow drowFinishedGoods in dtbProduce.Rows)
			{
				string strProductID = drowFinishedGoods["ProductID"].ToString().Trim();
				if (!arrFGIDs.Contains(strProductID))
				{
					arrFGIDs.Add(strProductID);
					strFGIDs.Append(strProductID).Append(",");
					DataRow[] drowInfo = GetItemInfo(strProductID, dtbAllProduct);
					try
					{
						DataRow drowFG = dtbFG.NewRow();
						drowFG["ProductID"] = strProductID;
						drowFG["SafetyStock"] = drowInfo[0]["SafetyStock"];
						drowFG["MakeItem"] = drowInfo[0]["MakeItem"];
						dtbFG.Rows.Add(drowFG);
					}
					catch
					{}
				}
				DataRow[] drowComponents = GetComponents(strProductID, dtbBOM);

				#region Components

				foreach (DataRow drowComponent in drowComponents)
				{
					strProductID = drowComponent["ComponentID"].ToString().Trim();
					if (!arrRMIDs.Contains(strProductID))
					{
						arrRMIDs.Add(strProductID);
						strRMIDs.Append(strProductID).Append(",");
						DataRow[] drowInfo = GetItemInfo(strProductID, dtbAllProduct);
						try
						{
							DataRow drowRM = dtbRM.NewRow();
							drowRM["ProductID"] = strProductID;
							drowRM["SafetyStock"] = drowInfo[0]["SafetyStock"];
							drowRM["MakeItem"] = drowInfo[0]["MakeItem"];
							dtbRM.Rows.Add(drowRM);
						}
						catch
						{
						}
					}
				}

				#endregion
			}
			// refine the list of item
			arrFGIDs.TrimToSize();
			arrFGIDs.Sort();
			arrRMIDs.TrimToSize();
			arrRMIDs.Sort();
			string strFGList = strFGIDs.ToString();
			if (strFGList.EndsWith(","))
				strFGList = strFGList.Remove(strFGList.Length - 1, 1);
			string strRMList = strRMIDs.ToString();
			if (strRMList.EndsWith(","))
				strRMList = strRMList.Remove(strRMList.Length - 1, 1);

			#endregion

			#region Detail Items

			// parents of all raw materials first
			foreach (string strProductID in arrRMIDs)
			{
				DataRow[] drowParents = GetParents(strProductID, dtbBOM);
				foreach (DataRow drowItem in drowParents)
				{
					string strItemID = drowItem["ProductID"].ToString().Trim();
					if (!arrDetailItemIDs.Contains(strItemID))
					{
						arrDetailItemIDs.Add(strItemID);
						strDetailItemIDs.Append(strItemID).Append(",");
					}
				}
			}
			// parents of all finished goods
			foreach (string strProductID in arrFGIDs)
			{
				DataRow[] drowParents = GetParents(strProductID, dtbBOM);
				foreach (DataRow drowItem in drowParents)
				{
					string strItemID = drowItem["ProductID"].ToString().Trim();
					if (!arrDetailItemIDs.Contains(strItemID))
					{
						arrDetailItemIDs.Add(strItemID);
						strDetailItemIDs.Append(strItemID).Append(",");
					}
				}
			}
			arrDetailItemIDs.TrimToSize();
			arrDetailItemIDs.Sort();
			string strDetailItemList = strDetailItemIDs.ToString();
			if (strDetailItemList.EndsWith(","))
				strDetailItemList = strDetailItemList.Remove(strDetailItemList.Length - 1, 1);

			#endregion

			#region combine the list of item

			ArrayList arrAllItems = new ArrayList();
			foreach (string strProductID in arrFGIDs)
			{
				if (!arrAllItems.Contains(strProductID))
					arrAllItems.Add(strProductID);
			}
			foreach (string strProductID in arrRMIDs)
			{
				if (!arrAllItems.Contains(strProductID))
					arrAllItems.Add(strProductID);
			}
			foreach (string strProductID in arrDetailItemIDs)
			{
				if (!arrAllItems.Contains(strProductID))
					arrAllItems.Add(strProductID);
			}
			string strAllItems = string.Empty;
			for (int i = 0; i < arrAllItems.Count; i++)
			{
				if (i == 0)
					strAllItems += arrAllItems[i].ToString();
				else
					strAllItems += "," + arrAllItems[i].ToString();
			}

			#endregion

			// planning offset
			DataTable dtbPlanningOffset = GetPlanningOffset(pstrCCNID);
			// cycles
			DataTable dtbCycles = GetCycles(pstrCCNID);
			// planning period
			ArrayList arrPlanningPeriod = GetPlanningPeriod(pstrCCNID);
			// refine cycles as of date based on production line
			dtbCycles = RefineCycle(pstrProductionLineID, dtbPlanningOffset, dtbCycles);
			DateTime dtmShiftFromDate = dtmFromDate;
			DateTime dtmShiftToDate = dtmToDate;
			DateTime dtmTempDate = DateTime.MinValue;
			GetStartAndEndTime(dtmFromDate, ref dtmShiftFromDate, ref dtmTempDate, dtbWorkingTime);
			GetStartAndEndTime(dtmToDate, ref dtmTempDate, ref dtmShiftToDate, dtbWorkingTime);
			// cycle of current month
			StringBuilder sbCycleIDs;
			DataTable dtbCyclesCurrentMonth = ArrangeCycles(dtmFromDate, DateTime.MinValue, dtbCycles, arrPlanningPeriod, out sbCycleIDs);
			DataTable dtbValidWorkDay = GetWorkingDateFromWCCapacity(pstrProductionLineID);
			// get first valid work day of current month
			DateTime dtmFirstValidDay = GetFirtValidWorkday(dtbValidWorkDay, dtbCyclesCurrentMonth);
			DataTable dtbDeliveryForNextWC = GetDeliveryForNextWC(strAllItems, dtmShiftFromDate, dtmShiftToDate);
			// refine the delivery date
			foreach (DataRow drowData in dtbDeliveryForNextWC.Rows)
			{
				// using StartTime of parent instead of WorkingDate
				DateTime dtmDate = (DateTime)drowData["StartTime"];
				// EndTime to check for over quantity from parent
				DateTime dtmEndTime = (DateTime)drowData["EndTime"];
				// do nothing with over quantity from parent
				if (dtmDate.Equals(dtmEndTime))
					continue;
				DateTime dtmTemp = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
				if (dtmTemp <= dtmFirstValidDay && dtmTemp >= dtmFromDate)
				{
					dtmDate = new DateTime(dtmFirstValidDay.Year, dtmFirstValidDay.Month, dtmFirstValidDay.Day,
						dtmDate.Hour, dtmDate.Minute, dtmDate.Second);
					drowData["StartTime"] = dtmDate;
					continue;
				}
				decimal decLeadTimeOffset = 0;
				try
				{
					decLeadTimeOffset = Convert.ToDecimal(drowData["LeadTimeOffSet"]);
				}
				catch{}
				decimal decNumOfDay = decLeadTimeOffset / 86400;
				// convert to valid work day
				dtmDate = ConvertWorkingDay(dtbWorkingTime, dtbValidWorkDay, dtmDate, decNumOfDay);
				drowData["StartTime"] = dtmDate;
			}

			DataTable dtbMiscIssue = GetDataFromMiscIssue(strAllItems, strLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbProductionPlan = GetProductionPlan(pstrProductionLineID, strAllItems, dtmFromDate, dtmToDate);
			DataTable dtbProductionActualRM = GetCompletionForChild(strAllItems, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbIssueMaterial = GetDataFromIssueMaterial(strLocationID, strAllItems, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbRecover = GetDataFromRecoverMaterial(strAllItems, strLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbProductionActual = GetCompletion(strAllItems, dtmShiftFromDate, dtmShiftToDate);
			DateTime dtmPreMonth = dtmFromDate.AddDays(-1);
			DataTable dtbBeginNetQuantity = GetBeginNetQuantity(pstrCCNID, strAllItems, strLocationID);
			DataTable dtbBeginData = new DataTable();
			if (dtmServerDate >= dtmFromDate)
				dtbBeginData = GetBeginData(strAllItems, strLocationID, dtmPreMonth);
			#endregion

			#region fill data to report table

			if (intViewType == (int)ViewType.Both || intViewType == (int)ViewType.MakeItem)
			{
				#region Finished Goods

				foreach (DataRow drowFG in dtbFG.Rows)
				{
					#region Variables

					string strProductID = drowFG["ProductID"].ToString();
					string strFilter = "ProductID = " + strProductID;
					decimal decBeginQuantity = 0, decBeginQuantityNG = 0;
					decimal decSafetyStock = 0, decRowTotalDeliveryPlan = 0, decRowTotalDeliveryActual = 0;
					decimal decRowProductionPlan = 0, decRowProductionActual = 0, decRowProductionActualNG = 0;
					decimal decRowNGPart = 0, decRowOutAbnormal = 0;
					decimal decRowDeliveryPlan = 0, decRowDeliveryActual = 0;
					bool blnHasTotalDeliveryPlan = false;
					bool blnHasTotalDeliveryActual = false;
					bool blnHasProductPlan = false;
					bool blnHasProductionActual = false;
					bool blnHasProductionActualNG = false;
					bool blnHasNGPart = false;
					bool blnHasOutAbnormal = false;
					bool blnHasDeliveryPlan = false;
					bool blnHasDeliveryActual = false;

					#endregion

					#region Define all rows of an item

					#region Total Delivery Plan row

					DataRow drowTotalDeliveryPlan = dtbMasterData.NewRow();
					drowTotalDeliveryPlan[ROW_TYPE_FLD] = (int) RowTypeEnum.TotalDeliveryPlan;
					drowTotalDeliveryPlan["ProductID"] = strProductID;
					drowTotalDeliveryPlan[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowTotalDeliveryPlan[CONTENT_FLD] = DELIVERY_CONTENT;
					drowTotalDeliveryPlan[SUBCONTENT_FLD] = TOTAL_FLD + " " + PLAN_CONTENT;

					#endregion

					#region Total Delivery Actual row

					DataRow drowTotalDeliveryActual = dtbMasterData.NewRow();
					drowTotalDeliveryActual[ROW_TYPE_FLD] = (int) RowTypeEnum.TotalDeliveryActual;
					drowTotalDeliveryActual["ProductID"] = strProductID;
					drowTotalDeliveryActual[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowTotalDeliveryActual[SUBCONTENT_FLD] = TOTAL_FLD + " " + ACTUAL_CONTENT;

					#endregion

					#region Production Plan row

					DataRow drowProductionPlan = dtbMasterData.NewRow();
					drowProductionPlan[ROW_TYPE_FLD] = (int) RowTypeEnum.ProductionPlan;
					drowProductionPlan["ProductID"] = strProductID;
					drowProductionPlan[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProductionPlan[CONTENT_FLD] = PRODUCTION_CONTENT;
					drowProductionPlan[SUBCONTENT_FLD] = PLAN_CONTENT;

					#endregion

					#region Production Actual row

					DataRow drowProductionActual = dtbMasterData.NewRow();
					drowProductionActual[ROW_TYPE_FLD] = (int) RowTypeEnum.ProductionActual;
					drowProductionActual["ProductID"] = strProductID;
					drowProductionActual[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProductionActual[SUBCONTENT_FLD] = ACTUAL_CONTENT + " OK";

					#endregion

					#region Production Actual NG row

					DataRow drowProductionActualNG = dtbMasterData.NewRow();
					drowProductionActualNG[ROW_TYPE_FLD] = (int) RowTypeEnum.ProductionActualNG;
					drowProductionActualNG["ProductID"] = strProductID;
					drowProductionActualNG[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProductionActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";

					#endregion

					#region NG Part Return row

					DataRow drowNGPart = dtbMasterData.NewRow();
					drowNGPart[ROW_TYPE_FLD] = (int) RowTypeEnum.NG_Part_Return;
					drowNGPart["ProductID"] = strProductID;
					drowNGPart[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowNGPart[CONTENT_FLD] = NG_PART_RETURN_CONTENT;

					#endregion

					#region Out Abnormal row

					DataRow drowAbnormal = dtbMasterData.NewRow();
					drowAbnormal[ROW_TYPE_FLD] = (int) RowTypeEnum.OutAbnormal;
					drowAbnormal["ProductID"] = strProductID;
					drowAbnormal[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowAbnormal[CONTENT_FLD] = OUT_ABNORMAL_CONTENT;

					#endregion

					#region Delivery Progress row

					DataRow drowProgressDelivery = dtbMasterData.NewRow();
					drowProgressDelivery[ROW_TYPE_FLD] = (int) RowTypeEnum.ProgressDelivery;
					drowProgressDelivery["ProductID"] = strProductID;
					drowProgressDelivery[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProgressDelivery[SUBCONTENT_FLD] = PROGRESS_CONTENT;

					#endregion

					#region Production Progress row

					DataRow drowProgress = dtbMasterData.NewRow();
					drowProgress[ROW_TYPE_FLD] = (int) RowTypeEnum.Progress;
					drowProgress["ProductID"] = strProductID;
					drowProgress[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProgress[SUBCONTENT_FLD] = PROGRESS_CONTENT;

					#endregion

					#region Stock Plan row

					DataRow drowStockPlan = dtbMasterData.NewRow();
					drowStockPlan[ROW_TYPE_FLD] = (int) RowTypeEnum.StockPlan;
					drowStockPlan["ProductID"] = strProductID;
					drowStockPlan[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowStockPlan[CONTENT_FLD] = STOCK_CONTENT;
					drowStockPlan[SUBCONTENT_FLD] = PLAN_CONTENT;

					#endregion

					#region Stock Actual row

					DataRow drowStockActual = dtbMasterData.NewRow();
					drowStockActual[ROW_TYPE_FLD] = (int) RowTypeEnum.StockActual;
					drowStockActual["ProductID"] = strProductID;
					drowStockActual[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowStockActual[SUBCONTENT_FLD] = ACTUAL_CONTENT + " OK";

					#endregion

					#region Stock Actual NG row

					DataRow drowStockActualNG = dtbMasterData.NewRow();
					drowStockActualNG[ROW_TYPE_FLD] = (int) RowTypeEnum.StockActualNG;
					drowStockActualNG["ProductID"] = strProductID;
					drowStockActualNG[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowStockActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";

					#endregion

					#region Warning Stock row

					DataRow drowWarningStock = dtbMasterData.NewRow();
					drowWarningStock[ROW_TYPE_FLD] = (int) RowTypeEnum.WarningStock;
					drowWarningStock["ProductID"] = strProductID;
					drowWarningStock[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowWarningStock[SUBCONTENT_FLD] = WARNING_STOCK;

					#endregion

					#endregion

					#region Calculate stock begin quantity

					#region inventory quantity

					#region OK

					try
					{
						decBeginQuantity += Convert.ToDecimal(dtbBeginNetQuantity.Compute("SUM(Quantity)",
							strFilter + " AND BinType IN (" + (int)BinTypeEnum.OK + "," + (int)BinTypeEnum.IN + ")"));
					}
					catch{}
				
					#endregion

					#region NG

					try
					{
						decBeginQuantityNG += Convert.ToDecimal(dtbBeginNetQuantity.Compute("SUM(Quantity)",
							strFilter + " AND BinType IN (" + (int)BinTypeEnum.NG + ")"));
					}
					catch{}

					#endregion

					#endregion

					if (dtmServerDate >= dtmFromDate)
					{
						#region OK

						try
						{
							decBeginQuantity = Convert.ToDecimal(dtbBeginData.Compute("SUM(Quantity)",
								strFilter + " AND BinTypeID IN (" + (int)BinTypeEnum.OK + "," + (int)BinTypeEnum.IN + ")"));
						}
						catch{}
				
						#endregion

						#region NG

						try
						{
							decBeginQuantityNG = Convert.ToDecimal(dtbBeginData.Compute("SUM(Quantity)",
								strFilter + " AND BinTypeID IN (" + (int)BinTypeEnum.NG + ")"));
						}
						catch{}

						#endregion
					}
					string strForParentFilter = "ComponentID =" + strProductID;

					// Mr.Nam (MAP) Request begin progress of month alway equal 0
					decimal decProductionProgress = 0;
					decimal decDeliveryProgress = 0;
					decimal decStockPlan = decBeginQuantity;
					decimal decStockActual = decBeginQuantity;
					decimal decStockActualNG = decBeginQuantityNG;
					// stock actual ok = begin stock + production plan of previous month - delivery plan of previous month + adjustment ok
					drowStockActual[BEGIN_FLD] = decBeginQuantity;
					// MAP request 2006-09-11 stock plan = stock actual OK
					drowStockPlan[BEGIN_FLD] = decBeginQuantity;
					// stock actual NG = begin stock + adjustment NG
					drowStockActualNG[BEGIN_FLD] = decBeginQuantityNG;
					drowWarningStock[BEGIN_FLD] = 0;

					#endregion

					try
					{
						decSafetyStock = decimal.Parse(drowFG["SafetyStock"].ToString());
					}
					catch{}

					#region Data for Delivery (Plan, Actual)

					DataTable dtbDelivery = dtbMasterData.Clone();
				
					// get all parents of current product
					DataRow[] drowParents = GetParents(strProductID, dtbBOM);

					#region each parent will be one row

					foreach (DataRow drowParent in drowParents)
					{
						// delivery plan
						DataRow drowDeliveryPlanParent = dtbDelivery.NewRow();
						drowDeliveryPlanParent[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryPlan;
						// delivery actual
						DataRow drowDeliveryActualParent = dtbDelivery.NewRow();
						drowDeliveryActualParent[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryActual;
						string strParentID = drowParent["ProductID"].ToString();
						for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
						{
							#region variable & condition

							string strColName = "D" + dtmDay.Day.ToString("00");
							decimal decDeliveryPlan = 0, decDeliveryActual = 0;
							// cycle of current day
							string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
							DateTime dtmStartTime = dtmDay;
							DateTime dtmEndTime = dtmDay;
							// get start and end time based on shift pattern
							GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
							string strPlanForParentFilter = strForParentFilter + " AND ProductID =" + strParentID
								+ " AND StartTime >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND StartTime < '" + dtmEndTime.ToString("G") + "'"
								+ " AND DCOptionMasterID = '" + strCycleID + "'";
							string strActualForParentFilter = strForParentFilter + " AND ProductID =" + strParentID
								+ " AND PostDate >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND PostDate < '" + dtmEndTime.ToString("G") + "'";

							#endregion

							#region Delivery Plan

							try
							{
								decDeliveryPlan += Convert.ToDecimal(dtbDeliveryForNextWC.Compute("SUM(Quantity)", strPlanForParentFilter));
							}
							catch{}
					
							if (decDeliveryPlan > 0)
							{
								decRowDeliveryPlan += decDeliveryPlan;
								blnHasDeliveryPlan = true;
								drowDeliveryPlanParent[strColName] = decDeliveryPlan;
							}
							decimal decCurrentTotal = 0;
							try
							{
								decCurrentTotal = Convert.ToDecimal(drowTotalDeliveryPlan[strColName]);
							}
							catch{}
							decCurrentTotal += decDeliveryPlan;
							// update total
							drowTotalDeliveryPlan[strColName] = decCurrentTotal;
							#endregion

							#region Delivery Actual

							// delivery actual from issue material
							try
							{
								decDeliveryActual += Convert.ToDecimal(dtbIssueMaterial.Compute("SUM(Quantity)",
									strActualForParentFilter + " AND LocationID = " + strLocationID));
							}
							catch{}
							
							if (decDeliveryActual > 0)
							{
								decRowDeliveryActual += decDeliveryActual;
								blnHasDeliveryActual = true;
								drowDeliveryActualParent[strColName] = decDeliveryActual;
							}

							decCurrentTotal = 0;
							try
							{
								decCurrentTotal = Convert.ToDecimal(drowTotalDeliveryActual[strColName]);
							}
							catch{}
							decCurrentTotal += decDeliveryActual;
							// update total
							drowTotalDeliveryActual[strColName] = decCurrentTotal;
							#endregion
						}

						#region Add result to report table

						if (blnHasDeliveryPlan || blnHasDeliveryActual)
						{
							blnHasTotalDeliveryPlan = true;
							drowDeliveryPlanParent["ProductID"] = strParentID;
							drowDeliveryPlanParent[CATEGORY_ID_FLD] = drowParent[CATEGORY_ID_FLD];
							drowDeliveryPlanParent[VENDOR_FLD] = drowParent[VENDOR_FLD];
							drowDeliveryPlanParent[SOURCE_FLD] = drowParent[SOURCE_FLD];
							drowDeliveryPlanParent[PARTNUMBER_FLD] = drowParent[PARTNUMBER_FLD];
							drowDeliveryPlanParent[PARTNAME_FLD] = drowParent[PARTNAME_FLD];
							drowDeliveryPlanParent[CATEGORY_FLD] = drowParent[CATEGORY_FLD];
							drowDeliveryPlanParent[UNIT_FLD] = drowParent[UNIT_FLD];
							drowDeliveryPlanParent[BOM_FLD] = drowParent["Quantity"];
							drowDeliveryPlanParent[MODEL_FLD] = drowParent[MODEL_FLD];
							drowDeliveryPlanParent[SAFETYSTOCK_FLD] = drowParent[SAFETYSTOCK_FLD];
							//drowDeliveryPlanParent[CONTENT_FLD] = DELIVERY_CONTENT;
							drowDeliveryPlanParent[SUBCONTENT_FLD] = PLAN_CONTENT;
							drowDeliveryPlanParent["ComponentID"] = strProductID;
							drowDeliveryPlanParent[TOTAL_FLD] = decRowDeliveryPlan;
							// add to table
							dtbDelivery.Rows.Add(drowDeliveryPlanParent);

							blnHasTotalDeliveryActual = true;
							drowDeliveryActualParent["ProductID"] = strParentID;
							drowDeliveryActualParent[CATEGORY_ID_FLD] = drowParent[CATEGORY_ID_FLD];
							drowDeliveryActualParent[VENDOR_FLD] = drowParent[VENDOR_FLD];
							drowDeliveryActualParent[SOURCE_FLD] = drowParent[SOURCE_FLD];
							drowDeliveryActualParent[PARTNUMBER_FLD] = drowParent[PARTNUMBER_FLD];
							drowDeliveryActualParent[PARTNAME_FLD] = drowParent[PARTNAME_FLD];
							drowDeliveryActualParent[CATEGORY_FLD] = drowParent[CATEGORY_FLD];
							drowDeliveryActualParent[UNIT_FLD] = drowParent[UNIT_FLD];
							drowDeliveryActualParent[BOM_FLD] = drowParent["Quantity"];
							drowDeliveryActualParent[MODEL_FLD] = drowParent[MODEL_FLD];
							drowDeliveryActualParent[SAFETYSTOCK_FLD] = drowParent[SAFETYSTOCK_FLD];
							//drowDeliveryActualParent[CONTENT_FLD] = DELIVERY_CONTENT;
							drowDeliveryActualParent[SUBCONTENT_FLD] = ACTUAL_CONTENT;
							drowDeliveryActualParent["ComponentID"] = strProductID;
							drowDeliveryActualParent[TOTAL_FLD] = decRowDeliveryActual;
							// add to table
							dtbDelivery.Rows.Add(drowDeliveryActualParent);
						}

						#endregion

						// reset variable for next item
						blnHasDeliveryPlan = false;
						blnHasDeliveryActual = false;
						decRowDeliveryActual = decRowDeliveryPlan = 0;
					}

					#endregion

					#region for the item which is the root item
					if (drowParents.Length <= 0)
					{
						// delivery plan
						DataRow drowDeliveryPlan = dtbDelivery.NewRow();
						drowDeliveryPlan[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryPlan;
						// delivery actual
						DataRow drowDeliveryActual = dtbDelivery.NewRow();
						drowDeliveryActual[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryActual;
						// delivery plan = production plan
						for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
						{
							#region variable & condition

							string strColName = "D" + dtmDay.Day.ToString("00");
							decimal decDeliveryPlan = 0, decDeliveryActual = 0;
							// cycle of current day
							string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
							DateTime dtmStartTime = dtmDay;
							DateTime dtmEndTime = dtmDay;
							// get start and end time based on shift pattern
							GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
							string strFilterPlan = "ProductID = " + strProductID
								+ " AND WorkingDate = '" + dtmDay.ToString("G") + "'"
								+ " AND DCOptionMasterID = " + strCycleID;
							string strFilterActual = "ProductID = " + strProductID
								+ " AND PostDate >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND PostDate < '" + dtmEndTime.ToString("G") + "'";

							#endregion

							#region Delivery Plan

							try
							{
								decDeliveryPlan = Convert.ToDecimal(dtbProductionPlan.Compute("SUM(Quantity)", strFilterPlan));
							}
							catch{}

							if (decDeliveryPlan > 0)
							{
								decRowDeliveryPlan += decDeliveryPlan;
								blnHasDeliveryPlan = true;
								drowDeliveryPlan[strColName] = decDeliveryPlan;
							}

							decimal decCurrentTotal = 0;
							try
							{
								decCurrentTotal = Convert.ToDecimal(drowTotalDeliveryPlan[strColName]);
							}
							catch{}
							decCurrentTotal += decDeliveryPlan;
							// update total
							drowTotalDeliveryPlan[strColName] = decCurrentTotal;

							#endregion

							#region Delivery Actual

							// delivery actual from misc. issue
							try
							{
								decDeliveryActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)",
									strFilterActual + " AND Purpose = " + (int)PurposeEnum.LocToLoc
									+ " AND SourceLocationID = " + strLocationID));
							}
							catch{}
							if (decDeliveryActual > 0)
							{
								decRowDeliveryActual += decDeliveryActual;
								blnHasDeliveryActual = true;
								drowDeliveryActual[strColName] = decDeliveryActual;
							}

							decCurrentTotal = 0;
							try
							{
								decCurrentTotal = Convert.ToDecimal(drowTotalDeliveryActual[strColName]);
							}
							catch{}
							decCurrentTotal += decDeliveryActual;
							// update total
							drowTotalDeliveryActual[strColName] = decCurrentTotal;

							#endregion
						}

						#region Add result to report table

						if (blnHasDeliveryPlan || blnHasDeliveryActual)
						{
							DataRow[] drowItemInfo = GetItemInfo(strProductID, dtbAllProduct);
							blnHasTotalDeliveryPlan = true;
							drowDeliveryPlan["ProductID"] = strProductID;
							drowDeliveryPlan[CATEGORY_ID_FLD] = drowItemInfo[0][CATEGORY_ID_FLD];
							drowDeliveryPlan[VENDOR_FLD] = drowItemInfo[0][VENDOR_FLD];
							drowDeliveryPlan[SOURCE_FLD] = drowItemInfo[0][SOURCE_FLD];
							drowDeliveryPlan[PARTNUMBER_FLD] = drowItemInfo[0][PARTNUMBER_FLD];
							drowDeliveryPlan[PARTNAME_FLD] = drowItemInfo[0][PARTNAME_FLD];
							drowDeliveryPlan[CATEGORY_FLD] = drowItemInfo[0][CATEGORY_FLD];
							drowDeliveryPlan[UNIT_FLD] = drowItemInfo[0][UNIT_FLD];
							drowDeliveryPlan[MODEL_FLD] = drowItemInfo[0][MODEL_FLD];
							drowDeliveryPlan[SAFETYSTOCK_FLD] = drowItemInfo[0][SAFETYSTOCK_FLD];
							drowDeliveryPlan[CONTENT_FLD] = DELIVERY_CONTENT;
							drowDeliveryPlan[SUBCONTENT_FLD] = PLAN_CONTENT;
							drowDeliveryPlan["ComponentID"] = strProductID;
							drowDeliveryPlan[TOTAL_FLD] = decRowDeliveryPlan;
							// add to table
							dtbDelivery.Rows.Add(drowDeliveryPlan);

							blnHasTotalDeliveryActual = true;
							drowDeliveryActual["ProductID"] = strProductID;
							drowDeliveryActual[CATEGORY_ID_FLD] = drowItemInfo[0][CATEGORY_ID_FLD];
							drowDeliveryActual[VENDOR_FLD] = drowItemInfo[0][VENDOR_FLD];
							drowDeliveryActual[SOURCE_FLD] = drowItemInfo[0][SOURCE_FLD];
							drowDeliveryActual[PARTNUMBER_FLD] = drowItemInfo[0][PARTNUMBER_FLD];
							drowDeliveryActual[PARTNAME_FLD] = drowItemInfo[0][PARTNAME_FLD];
							drowDeliveryActual[CATEGORY_FLD] = drowItemInfo[0][CATEGORY_FLD];
							drowDeliveryActual[UNIT_FLD] = drowItemInfo[0][UNIT_FLD];
							drowDeliveryActual[MODEL_FLD] = drowItemInfo[0][MODEL_FLD];
							drowDeliveryActual[SAFETYSTOCK_FLD] = drowItemInfo[0][SAFETYSTOCK_FLD];
							drowDeliveryActual[CONTENT_FLD] = DELIVERY_CONTENT;
							drowDeliveryActual[SUBCONTENT_FLD] = ACTUAL_CONTENT;
							drowDeliveryActual["ComponentID"] = strProductID;
							drowDeliveryActual[TOTAL_FLD] = decRowDeliveryActual;
							// add to table
							dtbDelivery.Rows.Add(drowDeliveryActual);
						}

						#endregion

						// reset variable for next item
						blnHasDeliveryPlan = false;
						blnHasDeliveryActual = false;
						decRowDeliveryActual = decRowDeliveryPlan = 0;
					}
					#endregion

					#endregion

					#region data for each day in month

					for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
					{
						#region Defind variable and condition

						string strColName = "D" + dtmDay.Day.ToString("00");
						decimal decTotalDeliveryPlan = 0, decTotalDeliveryActual = 0;
						decimal decProductionPlan = 0, decProductionActual = 0, decProductionActualNG = 0;
						decimal decNGReturn = 0, decOutAbnormal = 0, decOutAbnormalNG = 0;
						DateTime dtmStartTime = dtmDay;
						DateTime dtmEndTime = dtmDay;
						// cycle of current day
						string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
						// get start and end time based on shift pattern
						GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
						string strExpression = "ProductID =" + strProductID
							+ " AND PostDate >= '" + dtmStartTime.ToString("G") + "'"
							+ " AND PostDate < '" + dtmEndTime.ToString("G") + "'";
						string strProductionPlanFilter = "ProductID = " + strProductID
							+ " AND WorkingDate ='" + dtmDay.ToString("G") + "'"
							+ " AND DCOptionMasterID = " + strCycleID;
						
						#endregion

						#region Delivery Progress

						try
						{
							decTotalDeliveryPlan = Convert.ToDecimal(drowTotalDeliveryPlan[strColName]);
						}
						catch{}
						decRowTotalDeliveryPlan += decTotalDeliveryPlan;

						try
						{
							decTotalDeliveryActual = Convert.ToDecimal(drowTotalDeliveryActual[strColName]);
						}
						catch{}

						if (drowParents.Length > 0) // for item is not the root item
						{
							// delivery actual from misc. issue
							try
							{
								decTotalDeliveryActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)",
									strExpression + " AND Purpose = " + (int)PurposeEnum.LocToLoc
									+ " AND SourceLocationID = " + strLocationID));
							}
							catch{}
						}
						if (decTotalDeliveryActual != 0)
							drowTotalDeliveryActual[strColName] = decTotalDeliveryActual;

						decRowTotalDeliveryActual += decTotalDeliveryActual;
						decDeliveryProgress = decDeliveryProgress + decTotalDeliveryActual - decTotalDeliveryPlan;
						if (decDeliveryProgress != 0)
							drowProgressDelivery[strColName] = decDeliveryProgress;

						#endregion

						#region Production Plan

						try
						{
							decProductionPlan += Convert.ToDecimal(dtbProductionPlan.Compute("SUM(Quantity)", strProductionPlanFilter));
						}
						catch{}
						if (decProductionPlan != 0)
						{
							decRowProductionPlan += decProductionPlan;
							blnHasProductPlan = true;
							drowProductionPlan[strColName] = decProductionPlan;
						}
						
						#endregion

						#region Production Actual

						try
						{
							decProductionActual += Convert.ToDecimal(dtbProductionActual.Compute("SUM(" + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ")", 
								strExpression + " AND BinType = " + (int)BinTypeEnum.OK));
						}
						catch{}
						if (decProductionActual != 0)
						{
							decRowProductionActual += decProductionActual;
							blnHasProductionActual = true;
							drowProductionActual[strColName] = decProductionActual;
						}
						
						#endregion

						#region Production Actual NG

						try
						{
							decProductionActualNG += Convert.ToDecimal(dtbProductionActual.Compute("SUM(" + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ")", 
								strExpression + " AND BinType = " + (int)BinTypeEnum.NG));
						}
						catch{}
						if (decProductionActualNG != 0)
						{
							decRowProductionActualNG += decProductionActualNG;
							blnHasProductionActualNG = true;
							drowProductionActualNG[strColName] = decProductionActualNG;
						}
						
						#endregion

						#region Production Progress

						decProductionProgress = decProductionProgress + decProductionActual - decProductionPlan;
						if (decProductionProgress != 0)
							drowProgress[strColName] = decProductionProgress;

						#endregion

						#region NG Part Return

						// misc. issue: xuat tra cong doan truoc
						try
						{
							decNGReturn += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose = " + (int)PurposeEnum.ReturnPrevious
								+ " AND " + IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + "=" + strLocationID));
						}
						catch{}
						// misc. issue: xuat chuyen kho to bin NG
						try
						{
							decNGReturn += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose = " + (int)PurposeEnum.LocToLoc
								+ " AND " + IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + "=" + strLocationID
								+ " AND DesType = " + (int)BinTypeEnum.NG));
						}
						catch{}
						// recover material
						try
						{
							decNGReturn += Convert.ToDecimal(dtbRecover.Compute("SUM(Quantity)", 
								strExpression + " AND BinType = " + (int)BinTypeEnum.NG));
						}
						catch{}
						if (decNGReturn != 0)
						{
							decRowNGPart += decNGReturn;
							blnHasNGPart = true;
							drowNGPart[strColName] = decNGReturn;
						}

						#endregion

						#region Abnormal

						try
						{
							decOutAbnormal += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose IN (" + (int)PurposeEnum.QC + ","
								+ (int)PurposeEnum.Scrap + ","
								+ (int)PurposeEnum.Misc + ","
								+ (int)PurposeEnum.CompensationForCustomer + ")"
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND SourceType = " + (int)BinTypeEnum.OK));
						}
						catch{}
						try
						{
							decOutAbnormalNG += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose IN (" + (int)PurposeEnum.QC + ","
								+ (int)PurposeEnum.Scrap + ","
								+ (int)PurposeEnum.Misc + ","
								+ (int)PurposeEnum.CompensationForCustomer + ")"
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND SourceType = " + (int)BinTypeEnum.NG));
						}
						catch{}
						decRowOutAbnormal += decOutAbnormal + decOutAbnormalNG;
						if (decOutAbnormal != 0)
						{
							blnHasOutAbnormal = true;
							drowAbnormal[strColName] = decOutAbnormal;
						}

						#endregion

						#region Stock Plan

						decStockPlan = decStockPlan + decProductionPlan - decTotalDeliveryPlan;
						if (decStockPlan != 0)
							drowStockPlan[strColName] = decStockPlan;

						#endregion

						#region Stock Actual

						decimal decNGOKActual = 0;
						try
						{
							decNGOKActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose IN (" + (int)PurposeEnum.Transfer + ")"
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND SourceType = " + (int)BinTypeEnum.NG
								+ " AND DesType = " + (int)BinTypeEnum.OK));
						}
						catch{}
						decStockActual = decStockActual + decNGOKActual - decOutAbnormal;
						//decStockActual = decStockActual - decOutAbnormal;
						if (dtmDay > new DateTime(dtmServerDate.Year, dtmServerDate.Month, dtmServerDate.Day))
							decStockActual = decStockActual + decProductionPlan - decTotalDeliveryPlan;
						else
							decStockActual = decStockActual + decProductionActual - decTotalDeliveryActual;
						if (decStockActual != 0)
							drowStockActual[strColName] = decStockActual;

						#endregion

						#region Stock NG Actual

						//decStockActualNG = decStockActualNG + decProductionActualNG + decNGReturn - decOutAbnormalNG;
						decStockActualNG = decStockActualNG + decProductionActualNG + decNGReturn - decOutAbnormalNG - decNGOKActual;
						if (decStockActualNG != 0)
							drowStockActualNG[strColName] = decStockActualNG;
					
						#endregion

						#region Warning Stock

						if ((decStockActual - decSafetyStock) != 0)
							drowWarningStock[strColName] = decStockActual - decSafetyStock;

						#endregion
					}

					#endregion

					#region Add to report table with general information

					if (blnHasTotalDeliveryPlan || blnHasTotalDeliveryActual || blnHasProductionActual
						|| blnHasProductionActualNG || blnHasProductPlan || blnHasOutAbnormal || blnHasNGPart)
					{
						DataRow drowItem = GetItemInfo(strProductID, dtbAllProduct)[0];

						#region Total Delivery Plan Row
						drowTotalDeliveryPlan["ProductID"] = strProductID;
						drowTotalDeliveryPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowTotalDeliveryPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowTotalDeliveryPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowTotalDeliveryPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowTotalDeliveryPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowTotalDeliveryPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowTotalDeliveryPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowTotalDeliveryPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowTotalDeliveryPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowTotalDeliveryPlan[CONTENT_FLD] = DELIVERY_CONTENT;
						drowTotalDeliveryPlan[SUBCONTENT_FLD] = TOTAL_FLD + " " + PLAN_CONTENT;
						drowTotalDeliveryPlan[TOTAL_FLD] = decRowTotalDeliveryPlan;
						// add to table
						dtbMasterData.Rows.Add(drowTotalDeliveryPlan);
						#endregion

						#region Total Delivery Actual Row
						drowTotalDeliveryActual["ProductID"] = strProductID;
						drowTotalDeliveryActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowTotalDeliveryActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowTotalDeliveryActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowTotalDeliveryActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowTotalDeliveryActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowTotalDeliveryActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowTotalDeliveryActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowTotalDeliveryActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowTotalDeliveryActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowTotalDeliveryActual[CONTENT_FLD] = DELIVERY_CONTENT;
						drowTotalDeliveryActual[SUBCONTENT_FLD] = TOTAL_FLD + " " + ACTUAL_CONTENT;
						drowTotalDeliveryActual[TOTAL_FLD] = decRowTotalDeliveryActual;
						// add to table
						dtbMasterData.Rows.Add(drowTotalDeliveryActual);
						#endregion

						#region Delivery Plan and Actual for Parent
					
						foreach (DataRow drowForParent in dtbDelivery.Rows)
							dtbMasterData.ImportRow(drowForParent);
						#endregion

						#region Delivery Progress Row
						drowProgressDelivery["ProductID"] = strProductID;
						drowProgressDelivery[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProgressDelivery[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProgressDelivery[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProgressDelivery[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProgressDelivery[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProgressDelivery[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProgressDelivery[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProgressDelivery[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProgressDelivery[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProgressDelivery[CONTENT_FLD] = DELIVERY_CONTENT;
						drowProgressDelivery[SUBCONTENT_FLD] = PROGRESS_CONTENT;
						drowProgressDelivery[TOTAL_FLD] = decDeliveryProgress;
						// add to table
						dtbMasterData.Rows.Add(drowProgressDelivery);
						#endregion

						#region Out Abnormal Row
						drowAbnormal["ProductID"] = strProductID;
						drowAbnormal[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowAbnormal[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowAbnormal[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowAbnormal[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowAbnormal[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowAbnormal[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowAbnormal[UNIT_FLD] = drowItem[UNIT_FLD];
						drowAbnormal[MODEL_FLD] = drowItem[MODEL_FLD];
						drowAbnormal[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowAbnormal[CONTENT_FLD] = OUT_ABNORMAL_CONTENT;
						drowAbnormal[TOTAL_FLD] = decRowOutAbnormal;
						// add to table
						dtbMasterData.Rows.Add(drowAbnormal);
						#endregion

						#region Production Plan
						drowProductionPlan["ProductID"] = strProductID;
						drowProductionPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProductionPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProductionPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProductionPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProductionPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProductionPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProductionPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProductionPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProductionPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowProductionPlan[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProductionPlan[SUBCONTENT_FLD] = PLAN_CONTENT;
						drowProductionPlan[TOTAL_FLD] = decRowProductionPlan;
						// add to table
						dtbMasterData.Rows.Add(drowProductionPlan);
						#endregion

						#region Production Actual
						drowProductionActual["ProductID"] = strProductID;
						drowProductionActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProductionActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProductionActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProductionActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProductionActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProductionActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProductionActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProductionActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProductionActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProductionActual[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProductionActual[SUBCONTENT_FLD] = ACTUAL_CONTENT;
						drowProductionActual[TOTAL_FLD] = decRowProductionActual;
						// add to table
						dtbMasterData.Rows.Add(drowProductionActual);
						#endregion

						#region Production Actual NG
						drowProductionActualNG["ProductID"] = strProductID;
						drowProductionActualNG[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProductionActualNG[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProductionActualNG[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProductionActualNG[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProductionActualNG[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProductionActualNG[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProductionActualNG[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProductionActualNG[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProductionActualNG[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProductionActualNG[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProductionActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";
						drowProductionActualNG[TOTAL_FLD] = decRowProductionActualNG;
						// add to table
						dtbMasterData.Rows.Add(drowProductionActualNG);
						#endregion

						#region Production Progress
						drowProgress["ProductID"] = strProductID;
						drowProgress[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProgress[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProgress[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProgress[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProgress[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProgress[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProgress[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProgress[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProgress[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProgress[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProgress[SUBCONTENT_FLD] = PROGRESS_CONTENT;
						drowProgress[TOTAL_FLD] = decProductionProgress;
						// add to table
						dtbMasterData.Rows.Add(drowProgress);
						#endregion

						#region NG Part Return
						drowNGPart["ProductID"] = strProductID;
						drowNGPart[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowNGPart[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowNGPart[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowNGPart[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowNGPart[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowNGPart[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowNGPart[UNIT_FLD] = drowItem[UNIT_FLD];
						drowNGPart[MODEL_FLD] = drowItem[MODEL_FLD];
						drowNGPart[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowNGPart[CONTENT_FLD] = NG_PART_RETURN_CONTENT;
						drowNGPart[TOTAL_FLD] = decRowNGPart;
						// add to table
						dtbMasterData.Rows.Add(drowNGPart);
						#endregion

						#region Stock Plan
						drowStockPlan["ProductID"] = strProductID;
						drowStockPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowStockPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowStockPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowStockPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowStockPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowStockPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowStockPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowStockPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowStockPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowStockPlan[CONTENT_FLD] = STOCK_CONTENT;
						drowStockPlan[SUBCONTENT_FLD] = PLAN_CONTENT;
						drowStockPlan[TOTAL_FLD] = decStockPlan;
						// add to table
						dtbMasterData.Rows.Add(drowStockPlan);
						#endregion

						#region Stock Actual OK
						drowStockActual["ProductID"] = strProductID;
						drowStockActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowStockActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowStockActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowStockActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowStockActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowStockActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowStockActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowStockActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowStockActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowStockActual[SUBCONTENT_FLD] = ACTUAL_CONTENT + " OK";
						drowStockActual[TOTAL_FLD] = decStockActual;
						// add to table
						dtbMasterData.Rows.Add(drowStockActual);
						#endregion

						#region Stock Actual NG
						drowStockActualNG["ProductID"] = strProductID;
						drowStockActualNG[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowStockActualNG[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowStockActualNG[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowStockActualNG[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowStockActualNG[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowStockActualNG[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowStockActualNG[UNIT_FLD] = drowItem[UNIT_FLD];
						drowStockActualNG[MODEL_FLD] = drowItem[MODEL_FLD];
						drowStockActualNG[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowStockActualNG[CONTENT_FLD] = STOCK_CONTENT;
						drowStockActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";
						drowStockActualNG[TOTAL_FLD] = decStockActualNG;
						// add to table
						dtbMasterData.Rows.Add(drowStockActualNG);
						#endregion

						#region Warning Stock
						drowWarningStock["ProductID"] = strProductID;
						drowWarningStock[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowWarningStock[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowWarningStock[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowWarningStock[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowWarningStock[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowWarningStock[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowWarningStock[UNIT_FLD] = drowItem[UNIT_FLD];
						drowWarningStock[MODEL_FLD] = drowItem[MODEL_FLD];
						drowWarningStock[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowWarningStock[CONTENT_FLD] = STOCK_CONTENT;
						drowWarningStock[SUBCONTENT_FLD] = WARNING_STOCK;
						drowWarningStock[TOTAL_FLD] = decStockActual - decSafetyStock;
						// add to table
						dtbMasterData.Rows.Add(drowWarningStock);
						#endregion
					}

					#endregion
				}

				#endregion
			}

			if (intViewType == (int)ViewType.Both || intViewType == (int)ViewType.NoneMake)
			{
				#region Raw material

				foreach (DataRow drowFG in dtbRM.Rows)
				{
					#region Variables

					string strProductID = drowFG["ProductID"].ToString();
					string strFilter = "ProductID = " + strProductID;
					decimal decBeginQuantity = 0, decBeginQuantityNG = 0;
					decimal decSafetyStock = 0, decRowTotalDeliveryPlan = 0, decRowTotalDeliveryActual = 0;
					decimal decRowProductionPlan = 0, decRowProductionActual = 0;
					decimal decRowNGPart = 0, decRowOutAbnormal = 0;
					decimal decRowDeliveryPlan = 0, decRowDeliveryActual = 0;
					bool blnHasTotalDeliveryPlan = false;
					bool blnHasTotalDeliveryActual = false;
					bool blnHasProductPlan = false;
					bool blnHasProductionActual = false;
					bool blnHasProductionActualNG = false;
					bool blnHasNGPart = false;
					bool blnHasOutAbnormal = false;
					bool blnHasDeliveryPlan = false;
					bool blnHasDeliveryActual = false;

					#endregion

					#region Define all rows of an item

					#region Total Delivery Plan row

					DataRow drowTotalDeliveryPlan = dtbMasterData.NewRow();
					drowTotalDeliveryPlan[ROW_TYPE_FLD] = (int) RowTypeEnum.TotalDeliveryPlan;
					drowTotalDeliveryPlan["ProductID"] = strProductID;
					drowTotalDeliveryPlan[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowTotalDeliveryPlan[CONTENT_FLD] = DELIVERY_CONTENT;
					drowTotalDeliveryPlan[SUBCONTENT_FLD] = TOTAL_FLD + " " + PLAN_CONTENT;

					#endregion

					#region Total Delivery Actual row

					DataRow drowTotalDeliveryActual = dtbMasterData.NewRow();
					drowTotalDeliveryActual[ROW_TYPE_FLD] = (int) RowTypeEnum.TotalDeliveryActual;
					drowTotalDeliveryActual["ProductID"] = strProductID;
					drowTotalDeliveryActual[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowTotalDeliveryActual[SUBCONTENT_FLD] = TOTAL_FLD + " " + ACTUAL_CONTENT;

					#endregion

					#region Production Plan row

					DataRow drowProductionPlan = dtbMasterData.NewRow();
					drowProductionPlan[ROW_TYPE_FLD] = (int) RowTypeEnum.ProductionPlan;
					drowProductionPlan["ProductID"] = strProductID;
					drowProductionPlan[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProductionPlan[CONTENT_FLD] = PRODUCTION_CONTENT;
					drowProductionPlan[SUBCONTENT_FLD] = PLAN_CONTENT;

					#endregion

					#region Production Actual row

					DataRow drowProductionActual = dtbMasterData.NewRow();
					drowProductionActual[ROW_TYPE_FLD] = (int) RowTypeEnum.ProductionActual;
					drowProductionActual["ProductID"] = strProductID;
					drowProductionActual[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProductionActual[SUBCONTENT_FLD] = ACTUAL_CONTENT + " OK";

					#endregion

					#region Production Actual NG row

					DataRow drowProductionActualNG = dtbMasterData.NewRow();
					drowProductionActualNG[ROW_TYPE_FLD] = (int) RowTypeEnum.ProductionActualNG;
					drowProductionActualNG["ProductID"] = strProductID;
					drowProductionActualNG[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProductionActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";

					#endregion

					#region NG Part Return row

					DataRow drowNGPart = dtbMasterData.NewRow();
					drowNGPart[ROW_TYPE_FLD] = (int) RowTypeEnum.NG_Part_Return;
					drowNGPart["ProductID"] = strProductID;
					drowNGPart[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowNGPart[CONTENT_FLD] = NG_PART_RETURN_CONTENT;

					#endregion

					#region Out Abnormal row

					DataRow drowAbnormal = dtbMasterData.NewRow();
					drowAbnormal[ROW_TYPE_FLD] = (int) RowTypeEnum.OutAbnormal;
					drowAbnormal["ProductID"] = strProductID;
					drowAbnormal[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowAbnormal[CONTENT_FLD] = OUT_ABNORMAL_CONTENT;

					#endregion

					#region Delivery Progress row

					DataRow drowProgressDelivery = dtbMasterData.NewRow();
					drowProgressDelivery[ROW_TYPE_FLD] = (int) RowTypeEnum.ProgressDelivery;
					drowProgressDelivery["ProductID"] = strProductID;
					drowProgressDelivery[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProgressDelivery[SUBCONTENT_FLD] = PROGRESS_CONTENT;

					#endregion

					#region Production Progress row

					DataRow drowProgress = dtbMasterData.NewRow();
					drowProgress[ROW_TYPE_FLD] = (int) RowTypeEnum.Progress;
					drowProgress["ProductID"] = strProductID;
					drowProgress[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowProgress[SUBCONTENT_FLD] = PROGRESS_CONTENT;

					#endregion

					#region Stock Plan row

					DataRow drowStockPlan = dtbMasterData.NewRow();
					drowStockPlan[ROW_TYPE_FLD] = (int) RowTypeEnum.StockPlan;
					drowStockPlan["ProductID"] = strProductID;
					drowStockPlan[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowStockPlan[CONTENT_FLD] = STOCK_CONTENT;
					drowStockPlan[SUBCONTENT_FLD] = PLAN_CONTENT;

					#endregion

					#region Stock Actual row

					DataRow drowStockActual = dtbMasterData.NewRow();
					drowStockActual[ROW_TYPE_FLD] = (int) RowTypeEnum.StockActual;
					drowStockActual["ProductID"] = strProductID;
					drowStockActual[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowStockActual[SUBCONTENT_FLD] = ACTUAL_CONTENT + " OK";

					#endregion

					#region Stock Actual NG row

					DataRow drowStockActualNG = dtbMasterData.NewRow();
					drowStockActualNG[ROW_TYPE_FLD] = (int) RowTypeEnum.StockActualNG;
					drowStockActualNG["ProductID"] = strProductID;
					drowStockActualNG[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowStockActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";

					#endregion

					#region Warning Stock row

					DataRow drowWarningStock = dtbMasterData.NewRow();
					drowWarningStock[ROW_TYPE_FLD] = (int) RowTypeEnum.WarningStock;
					drowWarningStock["ProductID"] = strProductID;
					drowWarningStock[PRODUCT_TYPE_COL] = (int) ProductType.FinishedGoods;
					drowWarningStock[SUBCONTENT_FLD] = WARNING_STOCK;

					#endregion

					#endregion

					#region Calculate stock begin quantity

					#region inventory quantity

					#region OK

					try
					{
						decBeginQuantity += Convert.ToDecimal(dtbBeginNetQuantity.Compute("SUM(Quantity)",
							strFilter + " AND BinType IN (" + (int)BinTypeEnum.OK + "," + (int)BinTypeEnum.IN + ")"));
					}
					catch{}
				
					#endregion

					#region NG

					try
					{
						decBeginQuantityNG += Convert.ToDecimal(dtbBeginNetQuantity.Compute("SUM(Quantity)",
							strFilter + " AND BinType IN (" + (int)BinTypeEnum.NG + ")"));
					}
					catch{}

					#endregion

					#endregion

					if (dtmServerDate >= dtmFromDate)
					{
						#region OK

						try
						{
							decBeginQuantity = Convert.ToDecimal(dtbBeginData.Compute("SUM(Quantity)",
								strFilter + " AND BinTypeID IN (" + (int)BinTypeEnum.OK + "," + (int)BinTypeEnum.IN + ")"));
						}
						catch{}
				
						#endregion

						#region NG

						try
						{
							decBeginQuantityNG = Convert.ToDecimal(dtbBeginData.Compute("SUM(Quantity)",
								strFilter + " AND BinTypeID IN (" + (int)BinTypeEnum.NG + ")"));
						}
						catch{}

						#endregion
					}
					string strForParentFilter = "ComponentID =" + strProductID;

					// Mr.Nam (MAP) Request begin progress of month alway equal 0
					decimal decProductionProgress = 0;
					decimal decDeliveryProgress = 0;
					decimal decStockPlan = decBeginQuantity;
					decimal decStockActual = decBeginQuantity;
					// stock actual ok = begin stock + production plan of previous month - delivery plan of previous month + adjustment ok
					drowStockActual[BEGIN_FLD] = decBeginQuantity;
					// MAP request 2006-09-11 stock plan = stock actual OK
					drowStockPlan[BEGIN_FLD] = decBeginQuantity;
					// stock actual NG = begin stock + adjustment NG
					drowStockActualNG[BEGIN_FLD] = decBeginQuantityNG;
					drowWarningStock[BEGIN_FLD] = 0;

					#endregion

					try
					{
						decSafetyStock = decimal.Parse(drowFG["SafetyStock"].ToString());
					}
					catch{}

					#region Data for Delivery (Plan, Actual)

					DataTable dtbDelivery = dtbMasterData.Clone();
				
					// get all parents of current product
					DataRow[] drowParents = GetParents(strProductID, dtbBOM);

					#region each parent will be one row

					foreach (DataRow drowParent in drowParents)
					{
						// delivery plan
						DataRow drowDeliveryPlanParent = dtbDelivery.NewRow();
						drowDeliveryPlanParent[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryPlan;
						// delivery actual
						DataRow drowDeliveryActualParent = dtbDelivery.NewRow();
						drowDeliveryActualParent[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryActual;
						string strParentID = drowParent["ProductID"].ToString();
						for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
						{
							#region variable & condition

							string strColName = "D" + dtmDay.Day.ToString("00");
							decimal decDeliveryPlan = 0, decDeliveryActual = 0;
							// cycle of current day
							string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
							DateTime dtmStartTime = dtmDay;
							DateTime dtmEndTime = dtmDay;
							// get start and end time based on shift pattern
							GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
							string strPlanForParentFilter = strForParentFilter + " AND ProductID =" + strParentID
								+ " AND StartTime >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND StartTime < '" + dtmEndTime.ToString("G") + "'"
								+ " AND DCOptionMasterID = '" + strCycleID + "'";
							string strActualForParentFilter = strForParentFilter + " AND ProductID =" + strParentID
								+ " AND PostDate >= '" + dtmStartTime.ToString("G") + "'"
								+ " AND PostDate < '" + dtmEndTime.ToString("G") + "'";

							#endregion

							#region Delivery Plan

							try
							{
								decDeliveryPlan += Convert.ToDecimal(dtbDeliveryForNextWC.Compute("SUM(Quantity)", strPlanForParentFilter));
							}
							catch{}
					
							if (decDeliveryPlan > 0)
							{
								decRowDeliveryPlan += decDeliveryPlan;
								blnHasDeliveryPlan = true;
								drowDeliveryPlanParent[strColName] = decDeliveryPlan;
								decimal decCurrentTotal = 0;
								try
								{
									decCurrentTotal = Convert.ToDecimal(drowTotalDeliveryPlan[strColName]);
								}
								catch{}
								decCurrentTotal += decDeliveryPlan;
								// update total
								drowTotalDeliveryPlan[strColName] = decCurrentTotal;
							}
							#endregion

							#region Delivery Actual

							// delivery actual from work order completion of parent
							try
							{
								decDeliveryActual += Convert.ToDecimal(dtbProductionActualRM.Compute("SUM(Quantity)",
									strActualForParentFilter));
							}
							catch{}
							// delivery actual from misc. issue
							try
							{
								decDeliveryActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)",
									strActualForParentFilter + " AND Purpose = " + (int)PurposeEnum.LocToLoc
									+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + " = " + strLocationID));
							}
							catch{}
						
							if (decDeliveryActual > 0)
							{
								decRowDeliveryActual += decDeliveryActual;
								blnHasDeliveryActual = true;
								drowDeliveryActualParent[strColName] = decDeliveryActual;
								decimal decCurrentTotal = 0;
								try
								{
									decCurrentTotal = Convert.ToDecimal(drowTotalDeliveryActual[strColName]);
								}
								catch{}
								decCurrentTotal += decDeliveryActual;
								// update total
								drowTotalDeliveryActual[strColName] = decCurrentTotal;
							}
							#endregion
						}

						#region Add result to report table

						if (blnHasDeliveryPlan || blnHasDeliveryActual)
						{
							blnHasTotalDeliveryPlan = true;
							drowDeliveryPlanParent["ProductID"] = strParentID;
							drowDeliveryPlanParent[CATEGORY_ID_FLD] = drowParent[CATEGORY_ID_FLD];
							drowDeliveryPlanParent[VENDOR_FLD] = drowParent[VENDOR_FLD];
							drowDeliveryPlanParent[SOURCE_FLD] = drowParent[SOURCE_FLD];
							drowDeliveryPlanParent[PARTNUMBER_FLD] = drowParent[PARTNUMBER_FLD];
							drowDeliveryPlanParent[PARTNAME_FLD] = drowParent[PARTNAME_FLD];
							drowDeliveryPlanParent[CATEGORY_FLD] = drowParent[CATEGORY_FLD];
							drowDeliveryPlanParent[UNIT_FLD] = drowParent[UNIT_FLD];
							drowDeliveryPlanParent[BOM_FLD] = drowParent["Quantity"];
							drowDeliveryPlanParent[MODEL_FLD] = drowParent[MODEL_FLD];
							drowDeliveryPlanParent[SAFETYSTOCK_FLD] = drowParent[SAFETYSTOCK_FLD];
							//drowDeliveryPlanParent[CONTENT_FLD] = DELIVERY_CONTENT;
							drowDeliveryPlanParent[SUBCONTENT_FLD] = PLAN_CONTENT;
							drowDeliveryPlanParent["ComponentID"] = strProductID;
							drowDeliveryPlanParent[TOTAL_FLD] = decRowDeliveryPlan;
							// add to table
							dtbDelivery.Rows.Add(drowDeliveryPlanParent);

							blnHasTotalDeliveryActual = true;
							drowDeliveryActualParent["ProductID"] = strParentID;
							drowDeliveryActualParent[CATEGORY_ID_FLD] = drowParent[CATEGORY_ID_FLD];
							drowDeliveryActualParent[VENDOR_FLD] = drowParent[VENDOR_FLD];
							drowDeliveryActualParent[SOURCE_FLD] = drowParent[SOURCE_FLD];
							drowDeliveryActualParent[PARTNUMBER_FLD] = drowParent[PARTNUMBER_FLD];
							drowDeliveryActualParent[PARTNAME_FLD] = drowParent[PARTNAME_FLD];
							drowDeliveryActualParent[CATEGORY_FLD] = drowParent[CATEGORY_FLD];
							drowDeliveryActualParent[UNIT_FLD] = drowParent[UNIT_FLD];
							drowDeliveryActualParent[BOM_FLD] = drowParent["Quantity"];
							drowDeliveryActualParent[MODEL_FLD] = drowParent[MODEL_FLD];
							drowDeliveryActualParent[SAFETYSTOCK_FLD] = drowParent[SAFETYSTOCK_FLD];
							//drowDeliveryActualParent[CONTENT_FLD] = DELIVERY_CONTENT;
							drowDeliveryActualParent[SUBCONTENT_FLD] = ACTUAL_CONTENT;
							drowDeliveryActualParent["ComponentID"] = strProductID;
							drowDeliveryActualParent[TOTAL_FLD] = decRowDeliveryActual;
							// add to table
							dtbDelivery.Rows.Add(drowDeliveryActualParent);
						}

						#endregion

						// reset variable for next item
						blnHasDeliveryPlan = false;
						blnHasDeliveryActual = false;
						decRowDeliveryActual = decRowDeliveryPlan = 0;
					}

					#endregion

					#endregion

					#region data for each day in month

					for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
					{
						#region Defind variable and condition

						string strColName = "D" + dtmDay.Day.ToString("00");
						decimal decTotalDeliveryPlan = 0, decTotalDeliveryActual = 0;
						decimal decProductionPlan = 0, decProductionActual = 0;
						decimal decNGReturn = 0, decOutAbnormal = 0, decOutAbnormalNG = 0;
						DateTime dtmStartTime = dtmDay;
						DateTime dtmEndTime = dtmDay;
						// get start and end time based on shift pattern
						GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
						string strExpression = "ProductID =" + strProductID
							+ " AND PostDate >= '" + dtmStartTime.ToString("G") + "'"
							+ " AND PostDate < '" + dtmEndTime.ToString("G") + "'";
						
						#endregion

						#region Delivery Progress

						try
						{
							decTotalDeliveryPlan = Convert.ToDecimal(drowTotalDeliveryPlan[strColName]);
						}
						catch{}
						try
						{
							decTotalDeliveryActual = Convert.ToDecimal(drowTotalDeliveryActual[strColName]);
						}
						catch{}
						decRowTotalDeliveryActual += decTotalDeliveryActual;
						decDeliveryProgress = decDeliveryProgress + decTotalDeliveryActual - decTotalDeliveryPlan;
						if (decDeliveryProgress != 0)
							drowProgressDelivery[strColName] = decDeliveryProgress;

						#endregion

						#region Production Plan

						decProductionPlan = decTotalDeliveryPlan;
						if (decProductionPlan != 0)
						{
							decRowProductionPlan += decProductionPlan;
							blnHasProductPlan = true;
							drowProductionPlan[strColName] = decProductionPlan;
						}
						
						#endregion

						#region Production Actual

						// from issue material (to bin BF)
						try
						{
							decProductionActual += Convert.ToDecimal(dtbIssueMaterial.Compute("SUM(Quantity)", 
								strExpression + " AND " + PRO_IssueMaterialMasterTable.TOLOCATIONID_FLD + "=" + strLocationID
								+ " AND ToBinType = " + (int)BinTypeEnum.IN));
						}
						catch{}
						// from misc. issue (Loc to loc to bin BF)
						try
						{
							decProductionActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND " + IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD + "=" + strLocationID
								+ " AND DesType = " + (int)BinTypeEnum.IN
								+ " AND Purpose = " + (int)PurposeEnum.LocToLoc));
						}
						catch{}
						if (decProductionActual != 0)
						{
							decRowProductionActual += decProductionActual;
							blnHasProductionActual = true;
							drowProductionActual[strColName] = decProductionActual;
						}
						
						#endregion

						#region Production Progress

						decProductionProgress = decProductionProgress + decProductionActual - decProductionPlan;
						if (decProductionProgress != 0)
							drowProgress[strColName] = decProductionProgress;

						#endregion

						#region NG Part Return

						// misc. issue: xuat tra cong doan truoc
						try
						{
							decNGReturn += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose = " + (int)PurposeEnum.ReturnPrevious
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND SourceType = " + (int)BinTypeEnum.IN));
						}
						catch{}
						if (decNGReturn != 0)
						{
							decRowNGPart += decNGReturn;
							blnHasNGPart = true;
							drowNGPart[strColName] = decNGReturn;
						}

						#endregion

						#region Abnormal

						try
						{
							decOutAbnormal += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose IN (" + (int)PurposeEnum.QC + ","
								+ (int)PurposeEnum.Scrap + ","
								+ (int)PurposeEnum.Misc + ","
								+ (int)PurposeEnum.CompensationForCustomer + ")"
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND BinType = " + (int)BinTypeEnum.OK));
						}
						catch{}
						try
						{
							decOutAbnormalNG += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose IN (" + (int)PurposeEnum.QC + ","
								+ (int)PurposeEnum.Scrap + ","
								+ (int)PurposeEnum.Misc + ","
								+ (int)PurposeEnum.CompensationForCustomer + ")"
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND BinType = " + (int)BinTypeEnum.NG));
						}
						catch{}
						decRowOutAbnormal += decOutAbnormal + decOutAbnormalNG;
						if (decOutAbnormal != 0)
						{
							blnHasOutAbnormal = true;
							drowAbnormal[strColName] = decOutAbnormal;
						}

						#endregion

						#region Stock Plan

						decStockPlan = decStockPlan + decProductionPlan - decTotalDeliveryPlan;
						if (decStockPlan != 0)
							drowStockPlan[strColName] = decStockPlan;

						#endregion

						#region Stock Actual

						decimal decNGOKActual = 0;
						try
						{
							decNGOKActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", 
								strExpression + " AND Purpose IN (" + (int)PurposeEnum.Transfer + ")"
								+ " AND " + IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD + "=" + strLocationID
								+ " AND SourceType = " + (int)BinTypeEnum.NG
								+ " AND DesType = " + (int)BinTypeEnum.OK));
						}
						catch{}
						//decStockActual = decStockActual + decNGOKActual - decOutAbnormal;
						decStockActual = decStockActual + decNGOKActual - decOutAbnormal - decNGReturn;
						if (dtmDay > new DateTime(dtmServerDate.Year, dtmServerDate.Month, dtmServerDate.Day))
							decStockActual = decStockActual + decProductionPlan - decTotalDeliveryPlan;
						else
							decStockActual = decStockActual + decProductionActual - decTotalDeliveryActual;
						if (decStockActual != 0)
							drowStockActual[strColName] = decStockActual;

						#endregion

						#region Warning Stock

						if ((decStockActual - decSafetyStock) != 0)
							drowWarningStock[strColName] = decStockActual - decSafetyStock;

						#endregion
					}

					#endregion

					#region Add to report table with general information

					if (blnHasTotalDeliveryPlan || blnHasTotalDeliveryActual || blnHasProductionActual
						|| blnHasProductionActualNG || blnHasProductPlan || blnHasOutAbnormal || blnHasNGPart)
					{
						DataRow drowItem = GetItemInfo(strProductID, dtbAllProduct)[0];

						#region Total Delivery Plan Row
						drowTotalDeliveryPlan["ProductID"] = strProductID;
						drowTotalDeliveryPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowTotalDeliveryPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowTotalDeliveryPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowTotalDeliveryPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowTotalDeliveryPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowTotalDeliveryPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowTotalDeliveryPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowTotalDeliveryPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowTotalDeliveryPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowTotalDeliveryPlan[CONTENT_FLD] = DELIVERY_CONTENT;
						drowTotalDeliveryPlan[SUBCONTENT_FLD] = TOTAL_FLD + " " + PLAN_CONTENT;
						drowTotalDeliveryPlan[TOTAL_FLD] = decRowTotalDeliveryPlan;
						// add to table
						dtbMasterData.Rows.Add(drowTotalDeliveryPlan);
						#endregion

						#region Total Delivery Actual Row
						drowTotalDeliveryActual["ProductID"] = strProductID;
						drowTotalDeliveryActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowTotalDeliveryActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowTotalDeliveryActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowTotalDeliveryActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowTotalDeliveryActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowTotalDeliveryActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowTotalDeliveryActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowTotalDeliveryActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowTotalDeliveryActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowTotalDeliveryActual[CONTENT_FLD] = DELIVERY_CONTENT;
						drowTotalDeliveryActual[SUBCONTENT_FLD] = TOTAL_FLD + " " + ACTUAL_CONTENT;
						drowTotalDeliveryActual[TOTAL_FLD] = decRowTotalDeliveryActual;
						// add to table
						dtbMasterData.Rows.Add(drowTotalDeliveryActual);
						#endregion

						#region Delivery Plan and Actual for Parent
					
						foreach (DataRow drowForParent in dtbDelivery.Rows)
							dtbMasterData.ImportRow(drowForParent);
						#endregion

						#region Delivery Progress Row
						drowProgressDelivery["ProductID"] = strProductID;
						drowProgressDelivery[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProgressDelivery[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProgressDelivery[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProgressDelivery[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProgressDelivery[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProgressDelivery[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProgressDelivery[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProgressDelivery[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProgressDelivery[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProgressDelivery[CONTENT_FLD] = DELIVERY_CONTENT;
						drowProgressDelivery[SUBCONTENT_FLD] = PROGRESS_CONTENT;
						drowProgressDelivery[TOTAL_FLD] = decDeliveryProgress;
						// add to table
						dtbMasterData.Rows.Add(drowProgressDelivery);
						#endregion

						#region Out Abnormal Row
						drowAbnormal["ProductID"] = strProductID;
						drowAbnormal[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowAbnormal[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowAbnormal[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowAbnormal[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowAbnormal[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowAbnormal[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowAbnormal[UNIT_FLD] = drowItem[UNIT_FLD];
						drowAbnormal[MODEL_FLD] = drowItem[MODEL_FLD];
						drowAbnormal[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowAbnormal[CONTENT_FLD] = OUT_ABNORMAL_CONTENT;
						drowAbnormal[TOTAL_FLD] = decRowOutAbnormal;
						// add to table
						dtbMasterData.Rows.Add(drowAbnormal);
						#endregion

						#region Production Plan
						drowProductionPlan["ProductID"] = strProductID;
						drowProductionPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProductionPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProductionPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProductionPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProductionPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProductionPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProductionPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProductionPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProductionPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowProductionPlan[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProductionPlan[SUBCONTENT_FLD] = PLAN_CONTENT;
						drowProductionPlan[TOTAL_FLD] = decRowProductionPlan;
						// add to table
						dtbMasterData.Rows.Add(drowProductionPlan);
						#endregion

						#region Production Actual
						drowProductionActual["ProductID"] = strProductID;
						drowProductionActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProductionActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProductionActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProductionActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProductionActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProductionActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProductionActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProductionActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProductionActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProductionActual[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProductionActual[SUBCONTENT_FLD] = ACTUAL_CONTENT;
						drowProductionActual[TOTAL_FLD] = decRowProductionActual;
						// add to table
						dtbMasterData.Rows.Add(drowProductionActual);
						#endregion

						#region Production Actual NG
						drowProductionActualNG["ProductID"] = strProductID;
						drowProductionActualNG[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProductionActualNG[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProductionActualNG[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProductionActualNG[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProductionActualNG[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProductionActualNG[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProductionActualNG[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProductionActualNG[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProductionActualNG[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProductionActualNG[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProductionActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";
						// add to table
						dtbMasterData.Rows.Add(drowProductionActualNG);
						#endregion

						#region Production Progress
						drowProgress["ProductID"] = strProductID;
						drowProgress[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowProgress[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowProgress[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowProgress[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowProgress[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowProgress[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowProgress[UNIT_FLD] = drowItem[UNIT_FLD];
						drowProgress[MODEL_FLD] = drowItem[MODEL_FLD];
						drowProgress[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowProgress[CONTENT_FLD] = PRODUCTION_CONTENT;
						drowProgress[SUBCONTENT_FLD] = PROGRESS_CONTENT;
						drowProgress[TOTAL_FLD] = decProductionProgress;
						// add to table
						dtbMasterData.Rows.Add(drowProgress);
						#endregion

						#region NG Part Return
						drowNGPart["ProductID"] = strProductID;
						drowNGPart[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowNGPart[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowNGPart[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowNGPart[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowNGPart[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowNGPart[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowNGPart[UNIT_FLD] = drowItem[UNIT_FLD];
						drowNGPart[MODEL_FLD] = drowItem[MODEL_FLD];
						drowNGPart[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowNGPart[CONTENT_FLD] = NG_PART_RETURN_CONTENT;
						drowNGPart[TOTAL_FLD] = decRowNGPart;
						// add to table
						dtbMasterData.Rows.Add(drowNGPart);
						#endregion

						#region Stock Plan
						drowStockPlan["ProductID"] = strProductID;
						drowStockPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowStockPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowStockPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowStockPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowStockPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowStockPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowStockPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowStockPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowStockPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowStockPlan[CONTENT_FLD] = STOCK_CONTENT;
						drowStockPlan[SUBCONTENT_FLD] = PLAN_CONTENT;
						drowStockPlan[TOTAL_FLD] = decStockPlan;
						// add to table
						dtbMasterData.Rows.Add(drowStockPlan);
						#endregion

						#region Stock Actual OK
						drowStockActual["ProductID"] = strProductID;
						drowStockActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowStockActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowStockActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowStockActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowStockActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowStockActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowStockActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowStockActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowStockActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowStockActual[CONTENT_FLD] = STOCK_CONTENT;
						drowStockActual[SUBCONTENT_FLD] = ACTUAL_CONTENT + " OK";
						drowStockActual[TOTAL_FLD] = decStockActual;
						// add to table
						dtbMasterData.Rows.Add(drowStockActual);
						#endregion

						#region Stock Actual NG
						drowStockActualNG["ProductID"] = strProductID;
						drowStockActualNG[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowStockActualNG[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowStockActualNG[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowStockActualNG[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowStockActualNG[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowStockActualNG[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowStockActualNG[UNIT_FLD] = drowItem[UNIT_FLD];
						drowStockActualNG[MODEL_FLD] = drowItem[MODEL_FLD];
						drowStockActualNG[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowStockActualNG[CONTENT_FLD] = STOCK_CONTENT;
						drowStockActualNG[SUBCONTENT_FLD] = ACTUAL_CONTENT + " NG";
						// add to table
						dtbMasterData.Rows.Add(drowStockActualNG);
						#endregion

						#region Warning Stock
						drowWarningStock["ProductID"] = strProductID;
						drowWarningStock[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowWarningStock[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowWarningStock[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowWarningStock[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowWarningStock[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowWarningStock[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowWarningStock[UNIT_FLD] = drowItem[UNIT_FLD];
						drowWarningStock[MODEL_FLD] = drowItem[MODEL_FLD];
						drowWarningStock[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						//drowWarningStock[CONTENT_FLD] = STOCK_CONTENT;
						drowWarningStock[SUBCONTENT_FLD] = WARNING_STOCK;
						drowWarningStock[TOTAL_FLD] = decStockActual - decSafetyStock;
						// add to table
						dtbMasterData.Rows.Add(drowWarningStock);
						#endregion
					}

					#endregion
				}

				#endregion
			}

			#endregion

			#region Rendering Report

			C1Report rptReport = new C1Report();
			if (mLayoutFile == null || mLayoutFile == string.Empty)
				mLayoutFile = "WorkCenterReport.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mDefinitionFolder + "\\" + mLayoutFile);
			rptReport.Load(mDefinitionFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			arrstrReportInDefinitionFile = null;
			rptReport.Layout.PaperSize = PaperKind.A3;

			#region refine report layout based on the day of month

			ArrayList arrOffDay = GetWorkingDayByYear(dtmFromDate.Year);
			ArrayList arrHolidays = GetHolidaysInYear(dtmFromDate.Year);
			string strMonth = dtmFromDate.ToString("MMM");
			for (int i = dtmFromDate.Day; i <= dtmToDate.Day; i++)
			{
				DateTime dtmDay = new DateTime(dtmFromDate.Year, dtmFromDate.Month, i);
				string strDate = "lblD" + i.ToString("00");
				string strDayOfWeek = "lblDay" + i.ToString("00");
				try
				{
					rptReport.Fields[strDate].Text = i + "-" + strMonth;
				}
				catch
				{
				}
				try
				{
					rptReport.Fields[strDayOfWeek].Text = dtmDay.DayOfWeek.ToString().Substring(0, 3);
				}
				catch
				{
				}
				if (arrOffDay.Contains(dtmDay.DayOfWeek) || arrHolidays.Contains(dtmDay))
				{
					try
					{
						if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
						{
							rptReport.Fields[strDate].ForeColor = Color.Blue;
							rptReport.Fields[strDate].BackColor = Color.Yellow;
						}
						else
						{
							rptReport.Fields[strDate].ForeColor = Color.Red;
							rptReport.Fields[strDate].BackColor = Color.Yellow;
						}
					}
					catch
					{
					}
					try
					{
						if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
						{
							rptReport.Fields[strDayOfWeek].ForeColor = Color.Blue;
							rptReport.Fields[strDayOfWeek].BackColor = Color.Yellow;
						}
						else
						{
							rptReport.Fields[strDayOfWeek].ForeColor = Color.Red;
							rptReport.Fields[strDayOfWeek].BackColor = Color.Yellow;
						}
					}
					catch
					{
					}
				}
			}

			int intDaysInMonth = DateTime.DaysInMonth(dtmFromDate.Year, dtmFromDate.Month);
			if (intDaysInMonth < 31)
			{
				for (int i = intDaysInMonth + 1; i <= 31; i++)
				{
					string strDate = "lblD" + i.ToString("00");
					string strDayOfWeek = "lblDay" + i.ToString("00");
					string strDiv = "divD" + i.ToString("00");
					string strDivDetail = "divDetail" + i.ToString("00");
					string strDetail = "fldD" + i.ToString("00");
					try
					{
						rptReport.Fields[strDate].Visible = false;
					}
					catch
					{
					}
					try
					{
						rptReport.Fields[strDayOfWeek].Visible = false;
					}
					catch
					{
					}
					try
					{
						rptReport.Fields[strDiv].Visible = false;
					}
					catch
					{
					}
					try
					{
						rptReport.Fields[strDivDetail].Visible = false;
					}
					catch
					{
					}
					try
					{
						rptReport.Fields[strDetail].Visible = false;
					}
					catch
					{
					}
				}
				try
				{
					double dWidth = rptReport.Fields["lineTop"].Width;
					rptReport.Fields["lineMiddle"].Width =
						rptReport.Fields["lineMiddle"].Width - (31 - intDaysInMonth)*450;
					rptReport.Fields["lineTop"].Width =
						rptReport.Fields["lineBottom"].Width = dWidth - (31 - intDaysInMonth)*450;
					double dDetailLineWidth = rptReport.Fields["lineDetailBottom"].Width;
					rptReport.Fields["lineDetailBottom"].Width = dDetailLineWidth - (31 - intDaysInMonth)*450;
					dWidth = rptReport.Fields["lineTop"].Width;
					// move the total field
					rptReport.Fields["lblTotal"].Left = dWidth - rptReport.Fields["lblTotal"].Width;
					rptReport.Fields["divDetailTotal"].Left = rptReport.Fields["divTotal"].Left = dWidth;
					rptReport.Fields["lblDayTotal"].Left = dWidth - rptReport.Fields["lblTotal"].Width;
					rptReport.Fields["fldTotal"].Left = dWidth - rptReport.Fields["lblTotal"].Width;
				}
				catch
				{
				}
			}

			#endregion

			rptReport.ReportName = "Work Center Report";
			rptReport.DataSource.Recordset = dtbMasterData;
			
			// and show it in preview dialog				
			C1PrintPreviewDialog printPreview = new C1PrintPreviewDialog();
			printPreview.FormTitle = "Work Center Report";
			
			const string REPORTFLD_PARAMETER_CCN = "fldParameterCCN";
			const string REPORTFLD_PARAMETER_MONTH = "fldParamMonth";
			const string REPORTFLD_PARAMETER_YEAR = "fldParamYear";
			const string REPORTFLD_PARAM_MONTH = "fldParameterMonth";
			const string REPORTFLD_PARAMETER_PRODUCTIONLINE = "fldProLine";
			const string CAT_PARAM = "fldParamCat";
			const string MODEL_PARAM = "fldParamModel";
			const string PARTNO_FLD = "fldParamPartNo";
			const string PARTNAME_PARAM = "fldParamPartName";
			string strCCN = GetCCNCode(pstrCCNID);
			rptReport.Fields[REPORTFLD_PARAMETER_CCN].Text = strCCN;
			rptReport.Fields[REPORTFLD_PARAMETER_MONTH].Text = pstrMonth;
			rptReport.Fields[REPORTFLD_PARAMETER_YEAR].Text = pstrYear;
			rptReport.Fields[REPORTFLD_PARAM_MONTH].Text = pstrMonth + " - " + pstrYear;
			string strProductionLine = GetProCodeAndName(pstrProductionLineID);
			rptReport.Fields[REPORTFLD_PARAMETER_PRODUCTIONLINE].Text = strProductionLine;
			if (pstrCategoryID != null && pstrCategoryID.Length > 0)
				rptReport.Fields[CAT_PARAM].Text = GetCatInfo(pstrCategoryID);
			if (pstrModel != null && pstrModel.Length > 0)
			{
				// refine Model string
				pstrModel = pstrModel.Replace("'", string.Empty);
				rptReport.Fields[MODEL_PARAM].Text = pstrModel;
			}
			if (pstrProductID != null && pstrProductID.Length > 0)
			{
				string strPartNo = string.Empty, strPartName = string.Empty;
				DataRow[] drowsItem = GetItemsInfo(pstrProductID, dtbAllProduct);
				foreach (DataRow drowItem in drowsItem)
				{
					strPartNo += drowItem[PARTNUMBER_FLD].ToString() + ", ";
					strPartName += drowItem[PARTNAME_FLD].ToString() + ", ";
				}
				// remove the last ","
				if (strPartNo.IndexOf(",") >= 0)
					strPartNo = strPartNo.Substring(0, strPartNo.Length - 2);
				if (strPartName.IndexOf(",") >= 0)
					strPartName = strPartName.Substring(0, strPartName.Length - 2);
				try
				{
					rptReport.Fields[PARTNO_FLD].Text = strPartNo;
					rptReport.Fields[PARTNAME_PARAM].Text = strPartName;
				}
				catch{}
			}
			
			rptReport.Render();
			printPreview.ReportViewer.Document = rptReport.Document;
			
			printPreview.Show();

			#endregion

			return dtbMasterData;
		}

		/// <summary>
		/// Getss all Items In CCN
		/// </summary>
		/// <param name="pstrCCNID">CCN</param>
		/// <returns>DataTable</returns>
		private DataTable GetAllProduct(string pstrCCNID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSQL = "SELECT DISTINCT ITM_Product.ProductID, ITM_Product.Code AS " + PARTNUMBER_FLD 
					+ ", ITM_Product.Description AS " + PARTNAME_FLD + ", ISNULL(ProductionLineID, 0) ProductionLineID,"
					+ " ITM_Product.Revision AS Model, ITM_Category.CategoryID, ITM_Category.Code AS Category, ISNULL(MakeItem,0) AS MakeItem,"
					+ " ITM_Product.SafetyStock, MST_Party.Code AS Vendor, ITM_Source.Code AS Source, MST_UnitOfMeasure.Code AS Unit"
					+ " FROM ITM_Product LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " LEFT JOIN MST_Party ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " LEFT JOIN ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID"
					+ " JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " WHERE ITM_Product.CCNID = " + pstrCCNID
					+ " ORDER BY ProductID";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSQL, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Getss all Items produce in main work center of production line
		/// </summary>
		/// <param name="pstrProductionLineID">Production Line ID</param>
		/// <returns>DataTable</returns>
		private DataTable GetItemProduceInProLine(string pstrProductionLineID, string pstrCategoryID, string pstrModel, string pstrProductID)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT ITM_Product.ProductID, ITM_Product.Code AS PartNumber, ITM_Product.Description AS PartName,"
					+ " ITM_Product.Revision AS Model, ITM_Category.CategoryID, ITM_Category.Code AS Category"
					+ " FROM ITM_Product"
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " WHERE ITM_Product.ProductionLineID = " + pstrProductionLineID;
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					strSql += " AND ITM_Product.CategoryID IN (" + pstrCategoryID + ")";
				if (pstrModel != null && pstrModel.Length > 0)
					strSql += " AND ITM_Product.Revision IN (" + pstrModel + ")";
				if (pstrProductID != null && pstrProductID.Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrProductID + ")";
				strSql += " ORDER BY ITM_Product.ProductID";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get Item information
		/// </summary>
		/// <param name="pstrProductID">ProductID</param>
		/// <param name="pdtbAllItems">All Item</param>
		/// <returns>Item Info</returns>
		private DataRow[] GetItemInfo(string pstrProductID, DataTable pdtbAllItems)
		{
			return pdtbAllItems.Select("ProductID = '" + pstrProductID + "'");
		}

		private DataRow[] GetItemsInfo(string pstrProductID, DataTable pdtbAllItems)
		{
			return pdtbAllItems.Select("ProductID IN (" + pstrProductID + ")");
		}

		/// <summary>
		/// Getss all components of item
		/// </summary>
		/// <param name="pstrProductID">Product ID</param>
		/// <param name="pdtbBOM">BOM structure</param>
		/// <returns>DataRow[]</returns>
		private DataRow[] GetComponents(string pstrProductID, DataTable pdtbBOM)
		{
			return pdtbBOM.Select("ProductID = '" + pstrProductID + "'");
		}

		/// <summary>
		/// Getss all parents of item
		/// </summary>
		/// <param name="pstrProductID">Product ID</param>
		/// <param name="pdtbBOM">BOM structure</param>
		/// <returns>DataRow[]</returns>
		private DataRow[] GetParents(string pstrProductID, DataTable pdtbBOM)
		{
			return pdtbBOM.Select("ComponentID = '" + pstrProductID + "'");
		}

		/// <summary>
		/// Getss all BOM structure of system
		/// </summary>
		/// <returns>DataTable</returns>
		private DataTable GetBOMStructure()
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSQL = "SELECT ITM_BOM.ProductID, ComponentID, ITM_BOM.Quantity, Shrink, LeadTimeOffSet,"
					+ " ITM_Product.Code AS " + PARTNUMBER_FLD + ", ITM_Product.Description AS " + PARTNAME_FLD + ","
					+ " ITM_Product.Revision AS Model, ITM_Category.CategoryID, ITM_Category.Code AS Category, ITM_Product.SafetyStock,"
					+ " MST_Party.Code AS Vendor, ITM_Source.Code AS Source, MST_UnitOfMeasure.Code AS Unit,"
					+ " ISNULL(MakeItem,0) MakeItem, ISNULL(ProductionLineID,0) ProductionLineID"
					+ " FROM ITM_BOM JOIN ITM_Product"
					+ " ON ITM_BOM.ProductID = ITM_Product.ProductID"
					+ " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " LEFT JOIN MST_Party ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " LEFT JOIN ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID"
					+ " JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " ORDER BY ITM_BOM.ProductID";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSQL, oconPCS);
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Gets Begin Net Quantity of a list of Item
		/// </summary>
		/// <param name="pstrItems">List of Product</param>
		/// <returns>DataTable</returns>
		private DataTable GetBeginNetQuantity(string pstrCCNID, string pstrItems, string pstrLocationID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql=	"SELECT ISNULL(SUM(ISNULL(OHQuantity, 0)), 0) - ISNULL(SUM(ISNULL(CommitQuantity, 0)), 0) AS Quantity"
					+ " , ProductID, MST_BIN.BinTypeID AS BinType"
					+ " FROM IV_BinCache JOIN MST_BIN"
					+ " ON IV_BinCache.BinID = MST_BIN.BinID"
					+ " WHERE CCNID = " + pstrCCNID
					+ " AND MST_BIN.LocationID = " + pstrLocationID
					+ " AND ProductID IN ( " + pstrItems + ")"
					+ " GROUP BY BinTypeID, ProductID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbTRC = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbTRC);
				return dtbTRC;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Get CCN Code from ID
		/// </summary>
		/// <param name="pstrCCNID">CCN ID</param>
		/// <returns>CCN Code</returns>
		private string GetCCNCode(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT	Code + ' (' + Description + ')' FROM MST_CCN WHERE CCNID = " + pstrCCNID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				object objResult = cmdData.ExecuteScalar();
				try
				{
					return objResult.ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private string GetCatInfo(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM ITM_Category WHERE CategoryID IN (" + pstrID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
				string strCode = string.Empty;
				foreach (DataRow drowData in dtbData.Rows)
					strCode += drowData["Code"].ToString() + ",";
				if (strCode.IndexOf(",") >= 0)
					strCode = strCode.Substring(0, strCode.Length - 1);
				return strCode;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get Production Line Code and Name from ID
		/// </summary>
		/// <param name="pstrProID">Production Line ID</param>
		/// <returns>Pro Code (Pro Name)</returns>
		private string GetProCodeAndName(string pstrProID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' FROM PRO_ProductionLine WHERE ProductionLineID = " + pstrProID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				object objResult = cmdData.ExecuteScalar();
				try
				{
					return objResult.ToString();
				}
				catch
				{
					return string.Empty;
				}
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get working day in a year
		/// </summary>
		/// <param name="pintYear"></param>
		/// <returns></returns>
		private ArrayList GetWorkingDayByYear(int pintYear)
		{
			DataSet dstPCS = new DataSet();
			ArrayList arrDayOfWeek = new ArrayList();
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT [WorkingDayMasterID], [Sun], [CCNID], [Year], [Mon],"
					+ " [Tue], [Wed], [Thu], [Fri], [Sat]"
					+ " FROM [MST_WorkingDayMaster]"
					+ " WHERE [Year] = " + pintYear;

				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, "MST_WorkingDayMaster");

				if (dstPCS != null)
				{
					if (dstPCS.Tables[0].Rows.Count != 0)
					{
						DataRow drow = dstPCS.Tables[0].Rows[0];

						if (!bool.Parse(drow["Mon"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Monday);
						}

						if (!bool.Parse(drow["Tue"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Tuesday);
						}

						if (!bool.Parse(drow["Wed"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Wednesday);
						}

						if (!bool.Parse(drow["Thu"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Thursday);
						}

						if (!bool.Parse(drow["Fri"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Friday);
						}

						if (!bool.Parse(drow["Sat"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Saturday);
						}

						if (!bool.Parse(drow["Sun"].ToString()))
						{
							arrDayOfWeek.Add(DayOfWeek.Sunday);
						}
					}
				}

				return arrDayOfWeek;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Get all holidays in a year
		/// </summary>
		/// <param name="pintYear">Year</param>
		/// <returns>List of Holiday</returns>
		/// <author>DungLA</author>
		private ArrayList GetHolidaysInYear(int pintYear)
		{
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT OffDay FROM dbo.MST_WorkingDayDetail WHERE DATEPART(year, OffDay) = " + pintYear
					+ " ORDER BY OffDay";

				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, "MST_WorkingDayDetail");

				if (dstPCS != null)
				{
					if (dstPCS.Tables[0].Rows.Count != 0)
					{
						//have data--> create new array list to add items
						ArrayList arrHolidays = new ArrayList();
						for (int i = 0; i < dstPCS.Tables[0].Rows.Count; i++)
						{
							DateTime dtmTemp = DateTime.Parse(dstPCS.Tables[0].Rows[i]["OffDay"].ToString());
							//truncate hour, minute, second
							dtmTemp = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day);
							arrHolidays.Add(dtmTemp);
						}
						// return holidays in a year
						return arrHolidays;
					}
					else
					{
						// other else, return a blank list
						return new ArrayList();
					}
				}
				// return a bank list
				return new ArrayList();
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Gets working time of work center
		/// </summary>
		/// <returns></returns>
		private DataTable GetWorkingTime()
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT PRO_ShiftPattern.EffectDateFrom, "
					+ " PRO_ShiftPattern.WorkTimeFrom, PRO_ShiftPattern.WorkTimeTo"
					+ " FROM PRO_ShiftPattern JOIN PRO_Shift"
					+ " ON PRO_ShiftPattern.ShiftID = PRO_Shift.ShiftID"
					+ " WHERE ShiftDesc IN ('1S','2S','3S')";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Get working start time and end time of work center in a day
		/// </summary>
		/// <param name="pdtmCurrentDay">Current Day</param>
		/// <param name="pdtmStartTime">Start Time</param>
		/// <param name="pdtmEndTime">End Time</param>
		/// <param name="pdtmWorkingTime">Working Time</param>
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime,
			ref DateTime pdtmEndTime, DataTable pdtmWorkingTime)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(string.Empty, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
			{
				return;
			}
			//change shift configured day to working day
			pdtmStartTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Hour,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Minute,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Second);
			pdtmEndTime = new DateTime(pdtmCurrentDay.Year, pdtmCurrentDay.Month, pdtmCurrentDay.Day,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).
				Subtract((DateTime)drowShifts[0]["WorkTimeFrom"]).Days;
			pdtmEndTime = pdtmEndTime.AddDays(dblDiff);
		}
		/// <summary>
		/// Gets Master Location ID of Production Line
		/// </summary>
		/// <param name="pstrProductionLineID">Production Line</param>
		/// <param name="ostrLocationID">Out: Location ID</param>
		/// <returns>Master Location ID</returns>
		private string GetMasterLocationOfProductionLine(string pstrProductionLineID, out string ostrLocationID)
		{
			OleDbConnection oconPCS = null;
			ostrLocationID = string.Empty;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT ISNULL(MasterLocationID, 0) AS 'MasterLocationID', ISNULL(MST_Location.LocationID, 0) AS 'LocationID'"
					+ " FROM MST_Location JOIN PRO_ProductionLine"
					+ " ON MST_Location.LocationID = PRO_ProductionLine.LocationID"
					+ " WHERE ProductionLineID = " + pstrProductionLineID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadData = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadData.Fill(dtbData);
				if (dtbData.Rows.Count == 0)
					return string.Empty;
				else
				{
					try
					{
						ostrLocationID = dtbData.Rows[0]["LocationID"].ToString();
					}
					catch{}
					try
					{
						return dtbData.Rows[0]["MasterLocationID"].ToString();
					}
					catch
					{
						return string.Empty;
					}
				}
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DateTime GetDBDate()
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				strSql=	" SELECT  getdate() ";
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return (DateTime)ocmdPCS.ExecuteScalar();
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		/// <summary>
		/// Get Delivery plan for parent
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetDeliveryForNextWC(string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT ISNULL((PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity)"
					+ " /((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0) AS Quantity,"
					+ " ITM_BOM.ComponentID, PRO_DCPResultMaster.ProductID, WorkingDate,"
					+ " PRO_DCPResultDetail.StartTime, EndTime, LeadTimeOffset, DCOptionMasterID"
					+ " FROM PRO_DCPResultMaster JOIN PRO_DCPResultDetail"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
					+ " WHERE ITM_BOM.ComponentID IN (" + strItems + ")"
					+ " AND PRO_DCPResultDetail.StartTime >= ? AND PRO_DCPResultDetail.StartTime < ?"
					+ " ORDER BY PRO_DCPResultMaster.ProductID, ITM_BOM.ComponentID, StartTime";

				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetProductionPlan(string pstrProductionLineID, string pstrItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				DataTable dtbProduce = new DataTable(PRODUCTION_PLAN_TABLE);
				// produce
				string strSql = "SELECT ProductID, PRO_DCPResultDetail.Quantity, StartTime, EndTime,"
					+ " WorkingDate, DCOptionMasterID"
					+ " FROM PRO_DCPResultMaster JOIN PRO_DCPResultDetail"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE PRO_DCPResultMaster.ProductID IN (" + pstrItems + ")"
					+ " AND WorkingDate >= ?"
					+ " AND WorkingDate <= ?"
					+ " AND MST_WorkCenter.ProductionLineID = " + pstrProductionLineID
					+ " AND IsMain = 1";
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();
				odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbProduce);
				return dtbProduce;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DataTable GetCompletionForChild(string pstrItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				DataTable dtbProduce = new DataTable(PRODUCTION_PLAN_TABLE);
				// produce
				string strSql = "SELECT ISNULL((PRO_WorkOrderCompletion.CompletedQuantity * ITM_BOM.Quantity)"
					+ " /((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0) AS Quantity, ITM_BOM.ComponentID, PRO_WorkOrderCompletion.ProductID,"
					+ " PRO_WorkOrderCompletion.PostDate, LeadTimeOffset, ITM_BOM.Quantity " + BOM_FLD
					+ " FROM PRO_WorkOrderCompletion JOIN ITM_BOM ON PRO_WorkOrderCompletion.ProductID = ITM_BOM.ProductID"
					+ " WHERE ITM_BOM.ComponentID IN (" + pstrItems + ")"
					+ " AND PRO_WorkOrderCompletion.PostDate >= ?"
					+ " AND PRO_WorkOrderCompletion.PostDate <= ?";
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();
				odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbProduce);
				return dtbProduce;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Gets all data from misc. issue transaction
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>All misc. issue transaction</returns>
		private DataTable GetDataFromMiscIssue(string strItems, string pstrLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = " SELECT ProductID, SUM(ISNULL(Quantity, 0)) AS Quantity, PostDate,"
					+ " SourceLocationID, SourceBinID, SourceBin.BinTypeID AS SourceType,"
					+ " DesLocationID, DesBinID, DesBin.BinTypeID AS DesType,"
					+ " PRO_IssuePurpose.Code AS Purpose"
					+ " FROM IV_MiscellaneousIssueDetail JOIN IV_MiscellaneousIssueMaster"
					+ " ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID"
					+ " JOIN PRO_IssuePurpose ON IV_MiscellaneousIssueMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID"
					+ " JOIN MST_Bin AS DesBin ON IV_MiscellaneousIssueMaster.DesBinID = DesBin.BinID"
					+ " JOIN MST_Bin AS SourceBin ON IV_MiscellaneousIssueMaster.SourceBinID = SourceBin.BinID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate < ?"
					+ " AND ProductID IN (" + strItems + ")"
					+ " AND (SourceLocationID = " + pstrLocationID + " OR DesLocationID = " + pstrLocationID + ")"
					+ " GROUP BY ProductID, PostDate, SourceLocationID, SourceBinID, SourceBin.BinTypeID,"
					+ " DesLocationID, DesBinID, DesBin.BinTypeID, PRO_IssuePurpose.Code"
					+ " ORDER BY ProductID, PostDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Get delivery actual for parent (issue for work order)
		/// </summary>
		/// <param name="pstrLocationID">From Location</param>
		/// <param name="strItems">Items</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetDataFromIssueMaterial(string pstrLocationID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT PRO_WorkOrderDetail.ProductID, PRO_IssueMaterialDetail.ProductID As ComponentID,"
					+ " CommitQuantity AS Quantity, PostDate, PRO_IssuePurpose.Code AS Purpose,"
					+ " PRO_IssueMaterialMaster.ToLocationID, PRO_IssueMaterialDetail.LocationID,"
					+ " FromBin.BinTypeID AS FromBinType, ToBin.BinTypeID AS ToBinType"
					+ " FROM PRO_IssueMaterialDetail JOIN PRO_IssueMaterialMaster"
					+ " ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID"
					+ " JOIN PRO_WorkOrderDetail ON PRO_IssueMaterialDetail.WorkOrderDetailID = PRO_WorkOrderDetail.WorkOrderDetailID"
					+ " JOIN PRO_IssuePurpose ON PRO_IssueMaterialMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID"
					+ " JOIN MST_Bin FromBin ON PRO_IssueMaterialDetail.BinID = FromBin.BinID"
					+ " JOIN MST_Bin ToBin ON PRO_IssueMaterialMaster.ToBinID = ToBin.BinID"
					+ " WHERE (PRO_IssueMaterialDetail.LocationID = " + pstrLocationID
					+ " OR PRO_IssueMaterialMaster.ToLocationID = " + pstrLocationID + ")"
					+ " AND PostDate >= ? AND PostDate < ?"
					+ " AND PRO_IssueMaterialDetail.ProductID IN (" + strItems + ")"
					+ " ORDER BY PRO_WorkOrderDetail.ProductID, PRO_IssueMaterialDetail.ProductID, PostDate";

				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Get data from recover material transaction
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pstrLocationID">Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetDataFromRecoverMaterial(string strItems, string pstrLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = " SELECT SUM(ISNULL(RecoverQuantity,0)) AS Quantity, CST_RecoverMaterialDetail.ProductID, PostDate, MST_BIN.BinTypeID AS BinType"
					+ " FROM CST_RecoverMaterialMaster JOIN CST_RecoverMaterialDetail"
					+ " ON CST_RecoverMaterialMaster.RecoverMaterialMasterID = CST_RecoverMaterialDetail.RecoverMaterialMasterID"
					+ " JOIN MST_Bin ON CST_RecoverMaterialDetail.ToBinID = MST_Bin.BinID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate < ?"
					+ " AND CST_RecoverMaterialDetail.ToLocationID = " + pstrLocationID;
				if (strItems.Trim().Length > 0)
					strSql += " AND CST_RecoverMaterialDetail.ProductID IN (" + strItems + ")";
				strSql += " GROUP BY CST_RecoverMaterialDetail.ProductID, PostDate, MST_BIN.BinTypeID"
					+ " ORDER BY CST_RecoverMaterialDetail.ProductID, PostDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbDelivery);
				return dtbDelivery;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetCompletion(string pstrItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadData = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				DataTable dtbProduce = new DataTable(PRODUCTION_PLAN_TABLE);
				// produce
				string strSql = "SELECT CompletedQuantity, ProductID, PostDate, B.BinTypeID AS BinType"
					+ " FROM PRO_WorkOrderCompletion W JOIN MST_Bin B ON W.BinID = B.BinID"
					+ " WHERE ProductID IN (" + pstrItems + ")"
					+ " AND PostDate >= ?"
					+ " AND PostDate <= ?";
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();
				odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbProduce);
				return dtbProduce;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		private DateTime ConvertWorkingDay(DataTable pdtbWorkingTime, DataTable pdtbValidWorkDays, DateTime pdtmDate, decimal pdecNumberOfDay)
		{
			DateTime dtmConvert = pdtmDate;
			DataRow[] drowValidWorkDay = null;
			string strExpression = string.Empty;
			
			dtmConvert = dtmConvert.AddDays(-(double)pdecNumberOfDay);
			DateTime dtmWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			bool blnIsOK = false;
			while (!blnIsOK)
			{
				DateTime dtmOld = dtmConvert;				
				//if(arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
				DateTime dtmConverted = new DateTime(dtmWorkingDay.Year, dtmWorkingDay.Month, dtmWorkingDay.Day);
				strExpression = "BeginDate <= '" + dtmConverted.ToString("G") + "'"
					+ " AND EndDate >='" + dtmConverted.ToString("G") + "'";
				drowValidWorkDay = pdtbValidWorkDays.Select(strExpression);
				if(drowValidWorkDay.Length == 0)
				{
					if(pdtbValidWorkDays.Select("BeginDate <= '"+ dtmConvert.ToString("G") + "'").Length == 0) 
					{
						dtmConvert = (DateTime)pdtbValidWorkDays.Select("","BeginDate ASC")[0]["BeginDate"];
						break;
					}
					dtmConvert = dtmConvert.AddDays(-1);
					dtmWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				}
				if(dtmOld == dtmConvert) blnIsOK = true;
			}

			return dtmConvert;
		}
		private DateTime GetFirtValidWorkday(DataTable pdtbValidWorkdays, DataTable pdtbCycleDate)
		{
			DateTime dtmFirstValidDay = DateTime.MinValue;
			DataRow[] drowDate = pdtbCycleDate.Select("", "FromDate ASC");
			if (drowDate.Length > 0)
			{
				DateTime dtmFromDate = (DateTime)drowDate[0]["FromDate"];
				DateTime dtmToDate = (DateTime)drowDate[drowDate.Length - 1]["ToDate"];
				// loop thru all date to find the first valid work day
				string strExpression;
				for (DateTime dtmDate = dtmFromDate; dtmDate <= dtmToDate; dtmDate = dtmDate.AddDays(1))
				{
					strExpression = "BeginDate <= '" + dtmDate.ToString("G") + "'"
						+ " AND EndDate >='" + dtmDate.ToString("G") + "'";
					if (pdtbValidWorkdays.Select(strExpression).Length > 0)
					{
						dtmFirstValidDay = dtmDate;
						break;
					}
				}
			}
			return dtmFirstValidDay;
		}
		private DateTime GetRealWorkingDay(DateTime pdtmNeedToResolve, DataTable pdtbWorkingTime)
		{
			DataRow[] drowShifts = pdtbWorkingTime.Select(string.Empty, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
				return DateTime.MinValue;

			DateTime dtmResolvedDate = pdtmNeedToResolve;
			//change shift configured day to working day
			DateTime dtmStartTime = new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Hour,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Minute,
				((DateTime)drowShifts[0]["WorkTimeFrom"]).Second);
			DateTime dtmEndTime = new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Hour,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Minute,
				((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).Second);
			double dblDiff = ((DateTime)drowShifts[drowShifts.Length - 1]["WorkTimeTo"]).
				Subtract((DateTime)drowShifts[0]["WorkTimeFrom"]).Days;
			dtmEndTime = dtmEndTime.AddDays(dblDiff);

			while (dtmResolvedDate < dtmStartTime)
				dtmStartTime = dtmStartTime.AddDays(-1);

			return dtmStartTime;
		}
		private DataTable ArrangeCycles(DateTime pdtmFromMonth, DateTime pdtmToMonth, DataTable pdtbCycles,
			ArrayList parrPlanningPeriod, out StringBuilder sbCycleIDs)
		{
			DataTable dtbResult = pdtbCycles.Clone();
			DateTime dtmFromDate = new DateTime(pdtmFromMonth.Year,  pdtmFromMonth.Month, 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddDays(-1);
			if (pdtmToMonth > DateTime.MinValue)
				dtmToDate = pdtmToMonth;
			sbCycleIDs = new StringBuilder();
			DataTable dtbTemp = pdtbCycles.Clone();
			ArrayList arrMonths = GetAllMonthInRange(dtmFromDate, dtmToDate);

			#region find all cycle go thru the date range

			foreach (DateTime dtmPeriod in parrPlanningPeriod)
			{
				DataRow[] drowPeriod = pdtbCycles.Select("PlanningPeriod = '" + dtmPeriod.ToString("G") + "'"
					, "Version DESC");
				foreach (DataRow period in drowPeriod)
				{
					DateTime dtmFromDateCycle = (DateTime)period["FromDate"];
					DateTime dtmToDateCycle = (DateTime)period["ToDate"];
					ArrayList arrCycleMonths = GetAllMonthInRange(dtmFromDateCycle, dtmToDateCycle);
					foreach (DateTime dtmDate in arrMonths)
					{
						if (arrCycleMonths.Contains(dtmDate))
							dtbTemp.ImportRow(period);
					}
				}
			}

			#endregion

			#region sorting all cycle

			// order by planning period, from date and version
			DataRow[] drowCycles = dtbTemp.Select("", "PlanningPeriod ASC, FromDate ASC, Version DESC");
			DateTime dtmPreFromDate = DateTime.MinValue;
			int intPreVersion = -1;
			DateTime dtmPlanningPeriod = DateTime.MinValue;
			if (drowCycles.Length > 0)
				dtmPlanningPeriod = (DateTime)drowCycles[0]["PlanningPeriod"];
			for (int i = 0; i < drowCycles.Length; i++)
			{
				DataRow drowCycle = drowCycles[i];
				// from date of current cycle
				DateTime dtmCurFromDate = (DateTime)drowCycle["FromDate"];
				// version of current cycle
				int intVersion = Convert.ToInt32(drowCycle["Version"]);
				// this cycle is old version of period, from date is greater than new version then ignore it
				if (intVersion < intPreVersion && dtmCurFromDate > dtmPreFromDate
					&& dtmPlanningPeriod.Equals(drowCycle["PlanningPeriod"]))
					continue;
				// re-assign value
				intPreVersion = intVersion;
				dtmPreFromDate = dtmCurFromDate;
				dtmPlanningPeriod = (DateTime)drowCycle["PlanningPeriod"];
				// update ToDate of previous cycle
				if (i > 0)
				{
					// previous cycle
					DataRow drowPreCycle = drowCycles[i-1];
					// as of date of current cycle
					DateTime dtmAsOfDate = (DateTime)drowCycle["FromDate"];
					// update to date of previous cycle
					drowPreCycle["ToDate"] = dtmAsOfDate.AddDays(-1);
				}
			}
			if (drowCycles.Length > 0)
				drowCycles[drowCycles.Length - 1]["ToDate"] = dtmToDate;
			// import to result table
			foreach (DataRow drowCycle in drowCycles)
			{
				sbCycleIDs.Append(drowCycle["DCOptionMasterID"].ToString() + ",");
				dtbResult.ImportRow(drowCycle);
			}

			#endregion
			
			sbCycleIDs.Append("0");
			return dtbResult;
		}
		private ArrayList GetAllMonthInRange(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			pdtmFromDate = new DateTime(pdtmFromDate.Year, pdtmFromDate.Month, 1);
			pdtmToDate = new DateTime(pdtmToDate.Year, pdtmToDate.Month, 1);
			ArrayList arrMonths = new ArrayList();
			for (DateTime dtmDate = pdtmFromDate; dtmDate <= pdtmToDate; dtmDate = dtmDate.AddMonths(1))
			{
				arrMonths.Add(dtmDate);
			}
			return arrMonths;
		}
		private string GetCycleOfDate(DateTime pdtmDate, DataTable pdtbCycles)
		{
			string strCycleID = "0";
			foreach (DataRow drowCycle in pdtbCycles.Rows)
			{
				DateTime dtmFromDate = (DateTime)drowCycle["FromDate"];
				DateTime dtmToDate = (DateTime)drowCycle["ToDate"];
				if (pdtmDate >= dtmFromDate && pdtmDate <= dtmToDate)
				{
					strCycleID = drowCycle["DCOptionMasterID"].ToString();
					break;
				}
			}
			return strCycleID;
		}
		private DataTable GetCycles(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT DCOptionMasterID, PlanningPeriod, Version,"
					+ " AsOfDate AS FromDate, DATEADD(dd, PlanHorizon, AsOfDate) AS ToDate"
					+ " FROM PRO_DCOptionMaster"
					+ " WHERE CCNID = " + pstrCCNID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				odadPCS = new OleDbDataAdapter(cmdData);
				cmdData.Connection.Open();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable RefineCycle(string pstrProductionLineID, DataTable pdtbPlanningOffset, DataTable pdtbCycles)
		{
			foreach (DataRow drowData in pdtbCycles.Rows)
			{
				string strCycleID = drowData["DCOptionMasterID"].ToString();
				string strFilter = "DCOptionMasterID = '" + strCycleID 
					+ "' AND ProductionLineID = '" + pstrProductionLineID + "'";
				DataRow[] drowOffset = pdtbPlanningOffset.Select(strFilter);
				// refine as of date of the cycle based on planning offset of current production line
				if (drowOffset.Length > 0 && drowOffset[0]["PlanningStartDate"] != DBNull.Value)
				{
					DateTime dtmStartDate = (DateTime) drowOffset[0]["PlanningStartDate"];
					dtmStartDate = new DateTime(dtmStartDate.Year, dtmStartDate.Month, dtmStartDate.Day);
					drowData["FromDate"] = dtmStartDate;
				}
			}
			return pdtbCycles;
		}
		private ArrayList GetPlanningPeriod(string pstrCCNID)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = "SELECT DISTINCT PlanningPeriod FROM PRO_DCOptionMaster WHERE CCNID = " + pstrCCNID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataReader reader = ocmdPCS.ExecuteReader();
				ArrayList arrDate = new ArrayList();
				while(reader.Read())
					arrDate.Add((DateTime)reader["PlanningPeriod"]);
				return arrDate;
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		private DataTable GetPlanningOffset(string pstrCCNID)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = "SELECT PRO_PlanningOffset.PlanningStartDate, PRO_PlanningOffset.DCOptionMasterID,"
					+ " PRO_PlanningOffset.ProductionLineID"
					+ " FROM PRO_PlanningOffset JOIN PRO_DCOptionMaster"
					+ " ON PRO_PlanningOffset.DCOptionMasterID = PRO_DCOptionMaster.DCOptionMasterID"
					+ " WHERE PRO_DCOptionMaster.CCNID = " + pstrCCNID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		private DataTable GetWorkingDateFromWCCapacity(string pstrProductionLineID)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			DataTable dtbData = new DataTable();
			try
			{
				string strSql=	"SELECT BeginDate, EndDate"
					+ " FROM PRO_WCCapacity JOIN MST_WorkCenter"
					+ " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " JOIN PRO_ProductionLine ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND MST_WorkCenter.ProductionLineID = " + pstrProductionLineID;
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}
		private DataTable GetBeginData(string pstrItems, string pstrLocationID, DateTime pdtmMonth)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql=	"SELECT BB.LocationID, BB.ProductID, B.BinTypeID, SUM(ISNULL(OHQuantity,0)) AS Quantity"
					+ " FROM IV_BalanceBin BB JOIN MST_Bin B ON BB.BinID = B.BinID"
					+ " WHERE DATEPART(year, EffectDate) = " + pdtmMonth.Year
					+ " AND DATEPART(month, EffectDate) = " + pdtmMonth.Month;
				if (pstrItems != null && pstrItems != string.Empty)
					strSql += " AND BB.ProductID IN (" + pstrItems + ")";
				if (pstrLocationID != null && pstrLocationID != string.Empty)
					strSql += " AND BB.LocationID IN (" + pstrLocationID + ")";
				strSql += " GROUP BY BB.LocationID, BB.ProductID, B.BinTypeID"
					+ " ORDER BY BB.LocationID, BB.ProductID, B.BinTypeID";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.CommandTimeout = 1000;
				cmdPCS.Connection.Open();
				DataTable dtbTRC = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbTRC);
				return dtbTRC;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
	}
}
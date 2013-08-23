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
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace NIGURI
{
	/// <summary>
	/// Objective: Manage stock, in/out, assess delivery progress of vendor, warning stock.
	/// Paper Size: A3.
	/// Orientaion: Lanscape.
	/// </summary>
	[Serializable]
	public class NIGURI : MarshalByRefObject, IDynamicReport
	{
		#region Enum

		/// <summary>
		/// Purpose
		/// </summary>
		public enum PurposeEnum
		{
			/// <summary>
			/// Xuat Ke Hoach
			/// </summary>
			Plan = 1,
			/// <summary>
			/// Xuat Bat Thuong
			/// </summary>
			Misc = 2,
			/// <summary>
			/// Xuat Bu Hong
			/// </summary>
			Compensation = 3,
			/// <summary>
			/// Xuat tra lai nha cung cap
			/// </summary>
			ReturnToVendor = 4,
			/// <summary>
			/// Xuat tra lai cong doan truoc
			/// </summary>
			ReturnPrevious = 5,
			/// <summary>
			/// QC
			/// </summary>
			QC = 6,
			/// <summary>
			/// Xuat hang gia cong ngoai
			/// </summary>
			Outside = 7,
			/// <summary>
			/// Xuat ban hang
			/// </summary>
			Ship = 8,
			/// <summary>
			/// Nhap hang tu nha cung cap
			/// </summary>
			Receipt = 9,
			/// <summary>
			/// Nhap san pham hoan thanh
			/// </summary>
			Completion = 10,
			/// <summary>
			/// Nhap hang tra lai tu khach hang
			/// </summary>
			ReturnGoodReceipt = 11,
			/// <summary>
			/// Xuat Chuyen Kho
			/// </summary>
			LocToLoc = 12,
			/// <summary>
			/// Chuyen tu kho NG den kho OK
			/// </summary>
			Transfer = 13,
			/// <summary>
			/// Xuat Huy
			/// </summary>
			Scrap = 14,
			/// <summary>
			/// Adjustment
			/// </summary>
			Adjustment = 15,
			/// <summary>
			/// Commit
			/// </summary>
			Commit = 16,
			/// <summary>
			/// Cancel Commitment
			/// </summary>
			CancelCommitment = 17
		}

		/// <summary>
		/// Type of Operation: Inside = 0,Outside = 1
		/// </summary>
		public enum OperationType
		{
			/// <summary>
			/// 0
			/// </summary>
			Inside = 0,
			/// <summary>
			/// 1
			/// </summary>
			Outside = 1
		}

		public enum BinTypeEnum
		{
			/// <summary>
			/// OK 
			/// </summary>
			OK = 1,
			/// <summary>
			/// No Good
			/// </summary>
			NG = 2,
			/// <summary>
			/// Destroy
			/// </summary>
			DS = 3,
			/// <summary>
			/// Buffer
			/// </summary>
			BF = 4
		}
		public enum TransactionHistoryType
		{
			/// <summary>
			/// Out
			/// </summary>
			Out = 0,
			/// <summary>
			/// In
			/// </summary>
			In = 1,
			/// <summary>
			/// Both
			/// </summary>
			Both = 2,
			/// <summary>
			/// Booking
			/// </summary>
			Booking = 3
		}
		/// <summary>
		/// Type of row in data source
		/// </summary>
		private enum RowTypeEnum
		{
			/// <summary>
			/// Total Delivery Plan Row Type
			/// </summary>
			TotalDeliveryPlan = 0,
			/// <summary>
			/// Total Delivery Actual Row Type
			/// </summary>
			TotalDeliveryActual = 1,
			/// <summary>
			/// Delivery Plan Row Type
			/// </summary>
			DeliveryPlan = 2,
			/// <summary>
			/// Delivery Actual Row Type
			/// </summary>
			DeliveryActual = 3,
			/// <summary>
			/// Delivery Progress Row Type
			/// </summary>
			DeliveryProgress = 4,
			/// <summary>
			/// Out Abnormal Row Type
			/// </summary>
			OutAbnormal = 5,
			/// <summary>
			/// Arrival Plan Row Type
			/// </summary>
			ArrivalPlan = 6,
			/// <summary>
			/// Arrival Actual Row Type
			/// </summary>
			ArrivalActual = 7,
			/// <summary>
			/// Arrival Progress Row Type
			/// </summary>
			ArrivalProgress = 8,
			/// <summary>
			/// NG Part Return Row Type
			/// </summary>
			NGPartReturn = 9,
			/// <summary>
			/// Stock Plan Row Type
			/// </summary>
			StockPlan = 10,
			/// <summary>
			/// Stock Actual Row Type
			/// </summary>
			StockActualOK = 11,
			/// <summary>
			/// Stock Actual NG Row Type
			/// </summary>
			StockActualNG = 12,
			/// <summary>
			/// Warning Stock Row Type
			/// </summary>
			WarningStock = 13
		}

		/// <summary>
		/// WOLineStatus: Unreleased = 1, Released = 2, MfgClose = 3, FinClose = 4
		/// </summary>
		public enum WOLineStatus

		{
			/// <summary>
			/// 1
			/// </summary>
			Unreleased = 1,
			/// <summary>
			/// 2
			/// </summary>
			Released = 2,
			/// <summary>
			/// 3
			/// </summary>
			MfgClose = 3,
			/// <summary>
			/// 4
			/// </summary>
			FinClose = 4
		}

		#endregion

		#region Contants

		const string QUANTITY_COL = "Quantity";
		const string DELIVERY = "Delivery";
		const string OUT_ABNORMAL = "Other Issue";
		const string NG_PARTRETURN = "NG Part Return";
		const string PLAN = "Plan";
		const string ACTUAL = "Actual";
		const string PROGRESS = "Progress";
		const string ARRIVAL = "Arrival";
		const string STOCK = "Stock";
		const string WARNING = "Warning";
		const string POST_DATE = "PostDate";
		const string TOTAL_FLD = "Total";
		const string PRODUCT_ID_FLD = "ProductID";
		const string CATEGORY_ID_FLD = "CategoryID";
		const string ROW_TYPE_FLD = "RowType";
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
		const string COMPONENTID_FLD = "ComponentID";

		#endregion

		public NIGURI()
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

		private string mLayoutFile = string.Empty;

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
		/// <param name="pstrLocationID">Location</param>
		/// <returns>DataTable</returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrLocationID,
			string pstrCategoryID, string pstrPartyID, string pstrSourceID, string pstrModel, string pstrProductID)
		{
			#region local variables

			DateTime dtmFromDate = new DateTime(int.Parse(pstrYear), int.Parse(pstrMonth), 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddSeconds(-1);
			DateTime dtmServerDate = GetDBDate();
			C1Report rptReport = new C1Report();
			ArrayList arrOffDay = GetWorkingDayByYear(int.Parse(pstrYear));
			ArrayList arrHolidays = GetHolidaysInYear(int.Parse(pstrYear));
			string strMonth = dtmFromDate.ToString("MMM");
			DataTable dtbItems = new DataTable();
			int intYear = 0;
			int intMonth = 0;
			try
			{
				intYear = int.Parse(pstrYear);
			}
			catch{}
			try
			{
				intMonth = int.Parse(pstrMonth);
			}
			catch{}
			int intSourceID = 0;
			try
			{
				intSourceID = int.Parse(pstrSourceID);
			}
			catch{}

			#endregion

			#region Load report from definition file
			if (mLayoutFile == null || mLayoutFile.Trim() == string.Empty)
				mLayoutFile = "NIGURI.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mDefinitionFolder + "\\" + mLayoutFile);
			rptReport.Load(mDefinitionFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			arrstrReportInDefinitionFile = null;
			rptReport.Layout.PaperSize = PaperKind.A3;
			#endregion

			#region create table schema and layout report

			DataTable dtbMasterData = new DataTable();
			// general row
			dtbMasterData.Columns.Add(new DataColumn(PRODUCT_ID_FLD, typeof (int)));
			dtbMasterData.Columns[PRODUCT_ID_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(CATEGORY_ID_FLD, typeof (int)));
			dtbMasterData.Columns[CATEGORY_ID_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(ROW_TYPE_FLD, typeof (int)));
			dtbMasterData.Columns[ROW_TYPE_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(VENDOR_FLD, typeof (string)));
			dtbMasterData.Columns[VENDOR_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(SOURCE_FLD, typeof (string)));
			dtbMasterData.Columns[SOURCE_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(PARTNUMBER_FLD, typeof (string)));
			dtbMasterData.Columns[PARTNUMBER_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(PARTNAME_FLD, typeof (string)));
			dtbMasterData.Columns[PARTNAME_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(CATEGORY_FLD, typeof (string)));
			dtbMasterData.Columns[CATEGORY_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(UNIT_FLD, typeof (string)));
			dtbMasterData.Columns[UNIT_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(BOM_FLD, typeof (decimal)));
			dtbMasterData.Columns[BOM_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(CONTENT_FLD, typeof (string)));
			dtbMasterData.Columns[CONTENT_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(SUBCONTENT_FLD, typeof (string)));
			dtbMasterData.Columns[SUBCONTENT_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(MODEL_FLD, typeof (string)));
			dtbMasterData.Columns[MODEL_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(BEGIN_FLD, typeof (decimal)));
			dtbMasterData.Columns[BEGIN_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(SAFETYSTOCK_FLD, typeof (decimal)));
			dtbMasterData.Columns[SAFETYSTOCK_FLD].AllowDBNull = true;
			// now add column for each day
			for (int i = dtmFromDate.Day; i <= dtmToDate.Day; i++)
			{
				DateTime dtmDay = new DateTime(dtmFromDate.Year, dtmFromDate.Month, i);
				dtbMasterData.Columns.Add(new DataColumn("D" + i.ToString("00"), typeof (decimal)));
				dtbMasterData.Columns["D" + i.ToString("00")].AllowDBNull = true;
				
				#region modified the report field for each day
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
				#endregion
			}
			// add total column
			dtbMasterData.Columns.Add(new DataColumn(TOTAL_FLD, typeof (decimal)));
			dtbMasterData.Columns[TOTAL_FLD].AllowDBNull = true;
			dtbMasterData.Columns.Add(new DataColumn(COMPONENTID_FLD, typeof (int)));
			dtbMasterData.Columns[COMPONENTID_FLD].AllowDBNull = true;

			#region refine the report based on days in month
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

			#endregion

			#region Prepare report data

			// gets the list of item in location
			dtbItems = GetItemInLocation(pstrLocationID, pstrCategoryID, pstrProductID, pstrModel, pstrPartyID, intSourceID);
			if (dtbItems.Rows.Count == 0)
				return dtbMasterData;
			// planning offset
			DataTable dtbPlanningOffset = GetPlanningOffset(pstrCCNID);
			// cycles
			DataTable dtbCycles = GetCycles(pstrCCNID);
			// planning period
			ArrayList arrPlanningPeriod = GetPlanningPeriod(pstrCCNID);
			DataTable dtbBOM = GetBOM();
			DataTable dtbWorkingTime = GetWorkingTime();
			int intLocationID = 0;
			try
			{
				intLocationID = int.Parse(pstrLocationID);
			}
			catch{}
			int intMasterLocationID = GetMasterLocationID(pstrLocationID);
			// build the string item list
			StringBuilder sbItems = new StringBuilder();
			foreach (DataRow drowItem in dtbItems.Rows)
				sbItems.Append(drowItem["ProductID"].ToString()).Append(",");
			string strItems = sbItems.ToString(0, sbItems.Length - 1);
			
			#region Data of current month

			DateTime dtmShiftFromDate = dtmFromDate;
			DateTime dtmShiftToDate = dtmToDate;
			DateTime dtmTempDate = DateTime.MinValue;
			GetStartAndEndTime(dtmFromDate, ref dtmShiftFromDate, ref dtmTempDate, dtbWorkingTime);
			GetStartAndEndTime(dtmToDate, ref dtmTempDate, ref dtmShiftToDate, dtbWorkingTime);
			// inventory adjustment of current month
			DataTable dtbAdjustment = GetDataFromAdjustment(pstrCCNID, pstrLocationID, strItems, dtmFromDate, dtmToDate);
			DataTable dtbDeliveryPlanForParent = GetDeliveryPlanForParent(strItems, dtmShiftFromDate, dtmShiftToDate);
			// get first valid work day of current month
			DateTime dtmFirstValidDay = GetFirtValidWorkday(arrOffDay, arrHolidays, dtmFromDate);
			// refine the delivery date
			foreach (DataRow drowData in dtbDeliveryPlanForParent.Rows)
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
				dtmDate = ConvertWorkingDay(arrOffDay, arrHolidays, dtmDate, decNumOfDay, dtbWorkingTime);
				drowData["StartTime"] = dtmDate;
			}
			DataTable dtbIssueMaterial = GetDataFromIssueMaterial(pstrLocationID, strItems, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbDeliveryPlanForSO = GetDeliveryPlanForSO(strItems, intMasterLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbDeliveryActualForSO = GetDeliveryActualForSO(strItems, pstrCCNID, intLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbArrivalPlanFromCPO = GetArrivalPlanFromCPO(strItems, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbArrivalPlanFromPO = GetArrivalPlanFromPO(strItems, intMasterLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbArrivalActualFromPO = GetArrivalActualFromPO(strItems, pstrLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbMiscIssue = GetDataFromMiscIssue(strItems, pstrLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbReturnToVendor = GetReturnToVendor(strItems, pstrLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbRecover = GetDataFromRecoverMaterial(strItems, pstrLocationID, dtmShiftFromDate, dtmShiftToDate);
			DataTable dtbBeginNetQuantity = GetBeginNetQuantity(pstrCCNID, strItems, pstrLocationID);
			
			#endregion

			DateTime dtmFromDatePreMonth = dtmFromDate.AddMonths(-1);
			DataTable dtbBeginData = new DataTable();
			if (dtmServerDate >= dtmFromDate)
				dtbBeginData = GetBeginData(strItems, pstrLocationID, dtmFromDatePreMonth);
			
			#region data for stock actual in the future

			DataTable dtbActualPlanParent = null, dtbActualPlanSO = null, dtbActualPlanCPO = null, dtbActualPlanPO = null;
			if (dtmServerDate < dtmFromDate)
			{
				dtbActualPlanParent = GetDeliveryPlanForParent(strItems, dtmServerDate, dtmShiftFromDate);
				// get first valid work day of current month
				dtmFirstValidDay = GetFirtValidWorkday(arrOffDay, arrHolidays, dtmFromDate);
				// refine the delivery date
				foreach (DataRow drowData in dtbActualPlanParent.Rows)
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
					dtmDate = ConvertWorkingDay(arrOffDay, arrHolidays, dtmDate, decNumOfDay, dtbWorkingTime);
					drowData["StartTime"] = dtmDate;
				}

				dtbActualPlanSO = GetDeliveryPlanForSO(strItems, intMasterLocationID, dtmServerDate, dtmShiftFromDate);
				dtbActualPlanCPO = GetArrivalPlanFromCPO(strItems, dtmServerDate, dtmShiftFromDate);
				dtbActualPlanPO = GetArrivalPlanFromPO(strItems, intMasterLocationID, dtmServerDate, dtmShiftFromDate);
			}

			#endregion

			#endregion

			#region fill data to report table

			foreach (DataRow drowItem in dtbItems.Rows)
			{
				#region Define variable

				decimal decBeginQuantity = 0, decBeginQuantityNG = 0;
				bool blnHasDeliveryPlan = false, blnHasDeliveryActual = false;
				bool blnHasTotalDeliveryActual = false, blnHasTotalDeliveryPlan = false;
				bool blnHasArrivalPlan = false, blnHasArrivalActual = false;
				bool blnHasNG = false, blnHasAbnormal = false;
				decimal decRowDeliveryPlan = 0, decRowDeliveryActual = 0, decRowArrivalPlan = 0, decRowArrivalActual = 0;
				decimal decRowNG = 0, decRowOutAbnormal = 0, decRowTotalPlan = 0, decRowTotalActual = 0;
				decimal decActualPlanDelivery = 0, decActualPlanArrival = 0;
				string strProductID = drowItem["ProductID"].ToString();
				bool blnMakeItem = Convert.ToBoolean(drowItem["MakeItem"]);
				string strFilter = "ProductID =" + strProductID;

				#endregion

				#region Calculate Stock Begin Quantity

				#region inventory quantity

				#region OK

				try
				{
					decBeginQuantity += Convert.ToDecimal(dtbBeginNetQuantity.Compute("SUM(" + QUANTITY_COL + ")",
						strFilter + " AND BinType IN (" + (int)BinTypeEnum.OK + "," + (int)BinTypeEnum.BF + ")"));
				}
				catch{}
				
				#endregion

				#region NG

				try
				{
					decBeginQuantityNG += Convert.ToDecimal(dtbBeginNetQuantity.Compute("SUM(" + QUANTITY_COL + ")",
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
							strFilter + " AND BinTypeID IN (" + (int)BinTypeEnum.OK + "," + (int)BinTypeEnum.BF + ")"));
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
				else
				{
					#region delivery plan for calculate stock actual in the future

					try
					{
						decActualPlanDelivery += Convert.ToDecimal(dtbActualPlanParent.Compute("SUM(Quantity)",
							"ComponentID = " + strProductID
							+ " AND WorkingDate >= '" + dtmServerDate.ToString("G") + "'"
							+ " AND WorkingDate < '" + dtmFromDate.ToString("G") + "'"));
					}
					catch{}
					try
					{
						decActualPlanDelivery += Convert.ToDecimal(dtbActualPlanSO.Compute("SUM(Quantity)",
							"ProductID = " + strProductID
							+ " AND ScheduleDate >= '" + dtmServerDate.ToString("G") + "'"
							+ " AND ScheduleDate < '" + dtmShiftFromDate.ToString("G") + "'"));
					}
					catch{}
					
					#endregion

					#region arrival plan for calculate stock actual in the future

					try
					{
						decActualPlanArrival += Convert.ToDecimal(dtbActualPlanPO.Compute("SUM(Quantity)",
							"ProductID = " + strProductID
							+ " AND ScheduleDate >= '" + dtmServerDate.ToString("G") + "'"
							+ " AND ScheduleDate < '" + dtmShiftFromDate.ToString("G") + "'"));
					}
					catch{}
					try
					{
						decActualPlanArrival += Convert.ToDecimal(dtbActualPlanCPO.Compute("SUM(Quantity)",
							"ProductID = " + strProductID
							+ " AND EndTime >= '" + dtmServerDate.ToString("G") + "'"
							+ " AND EndTime < '" + dtmShiftFromDate.ToString("G") + "'"));
					}
					catch{}

					#endregion
				}

				string strForParentFilter = COMPONENTID_FLD + "=" + strProductID;
				
				// Mr.Nam (MAP) Request begin progress of month alway equal 0
				decimal decDeliveryProgressOfPreDay = 0;
				decimal decArrivalProgressOfPreDay = 0;
				// MAP Request: Begin Stock Plan = Stock Actual
				decimal decStockPlanOfPreDay = decBeginQuantity;
				decimal decStockActualOKOfPreDay = decBeginQuantity;
				decimal decStockActualNGOfPreDay = decBeginQuantityNG;
				decimal decSafetyStock = 0;
				try
				{
					decSafetyStock = Convert.ToDecimal(drowItem[SAFETYSTOCK_FLD]);
				}
				catch{}
				#endregion

				#region Define all rows of an item
				// delivery plan
				DataRow drowDeliveryPlan = dtbMasterData.NewRow();
				drowDeliveryPlan[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryPlan;
				// delivery actual
				DataRow drowDeliveryActual = dtbMasterData.NewRow();
				drowDeliveryActual[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryActual;
				// total delivery plan
				DataRow drowTotalDeliveryPlan = dtbMasterData.NewRow();
				drowTotalDeliveryPlan[ROW_TYPE_FLD] = (int)RowTypeEnum.TotalDeliveryPlan;
				// total delivery actual
				DataRow drowTotalDeliveryActual = dtbMasterData.NewRow();
				drowTotalDeliveryActual[ROW_TYPE_FLD] = (int)RowTypeEnum.TotalDeliveryActual;
				// delivery progress
				DataRow drowDeliveryProgress = dtbMasterData.NewRow();
				drowDeliveryProgress[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryProgress;
				drowDeliveryProgress[BEGIN_FLD] = 0;
				// out abnormal
				DataRow drowOutAbnormal = dtbMasterData.NewRow();
				drowOutAbnormal[ROW_TYPE_FLD] = (int)RowTypeEnum.OutAbnormal;
				// NG Part Return
				DataRow drowNGPartReturn = dtbMasterData.NewRow();
				drowNGPartReturn[ROW_TYPE_FLD] = (int)RowTypeEnum.NGPartReturn;
				// arrival plan
				DataRow drowArrivalPlan = dtbMasterData.NewRow();
				drowArrivalPlan[ROW_TYPE_FLD] = (int)RowTypeEnum.ArrivalPlan;
				// arrival actual
				DataRow drowArrivalActual = dtbMasterData.NewRow();
				drowArrivalActual[ROW_TYPE_FLD] = (int)RowTypeEnum.ArrivalActual;
				// arrival progress
				DataRow drowArrivalProgress = dtbMasterData.NewRow();
				drowArrivalProgress[ROW_TYPE_FLD] = (int)RowTypeEnum.ArrivalProgress;
				drowArrivalProgress[BEGIN_FLD] = 0;
				// stock plan
				DataRow drowStockPlan = dtbMasterData.NewRow();
				drowStockPlan[ROW_TYPE_FLD] = (int)RowTypeEnum.StockPlan;
				drowStockPlan[BEGIN_FLD] = decStockPlanOfPreDay;
				// stock actual ok
				DataRow drowStockActualOK = dtbMasterData.NewRow();
				drowStockActualOK[ROW_TYPE_FLD] = (int)RowTypeEnum.StockActualOK;
				drowStockActualOK[BEGIN_FLD] = decStockActualOKOfPreDay;
				// stock actual ng
				DataRow drowStockActualNG = dtbMasterData.NewRow();
				drowStockActualNG[ROW_TYPE_FLD] = (int)RowTypeEnum.StockActualNG;
				drowStockActualNG[BEGIN_FLD] = decStockActualNGOfPreDay;
				// warning stock
				DataRow drowWarningStock = dtbMasterData.NewRow();
				drowWarningStock[ROW_TYPE_FLD] = (int)RowTypeEnum.WarningStock;
				drowWarningStock[BEGIN_FLD] = 0;
				#endregion

				#region Data for Delivery (Plan, Actual)

				DataTable dtbDelivery = dtbMasterData.Clone();
				
				// get all parents of current product
				DataRow[] drowParents = GetParents(strProductID, dtbBOM);

				// each parent will be one row
				foreach (DataRow drowParent in drowParents)
				{
					// delivery plan
					DataRow drowDeliveryPlanParent = dtbDelivery.NewRow();
					drowDeliveryPlanParent[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryPlan;
					// delivery actual
					DataRow drowDeliveryActualParent = dtbDelivery.NewRow();
					drowDeliveryActualParent[ROW_TYPE_FLD] = (int)RowTypeEnum.DeliveryActual;
					string strParentID = drowParent[PRODUCT_ID_FLD].ToString();
					string strProductionLineID = drowParent["ProductionLineID"].ToString();
					DataTable dtbTempCycle = RefineCycle(strProductionLineID, dtbPlanningOffset, dtbCycles);
					StringBuilder sbCycleIDs;
					DataTable dtbCyclesCurrentMonth = ArrangeCycles(dtmFromDate, DateTime.MinValue, dtbTempCycle, arrPlanningPeriod, out sbCycleIDs);
					for (int i = 1; i <= intDaysInMonth; i++)
					{
						string strColName = "D" + i.ToString("00");
						decimal decDeliveryPlan = 0, decDeliveryActual = 0;
						DateTime dtmDay = new DateTime(intYear, intMonth, i);
						// cycle of current day
						string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
						DateTime dtmStartTime = dtmDay;
						DateTime dtmEndTime = dtmDay;
						// get start and end time based on shift pattern
						GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
						string strPlanForParentFilter = strForParentFilter + " AND " + PRODUCT_ID_FLD + "=" + strParentID
							+ " AND StartTime >= '" + dtmStartTime.ToString("G") + "'"
							+ " AND StartTime < '" + dtmEndTime.ToString("G") + "'"
							+ " AND DCOptionMasterID = '" + strCycleID + "'";
						string strActualForParentFilter = strForParentFilter + " AND " + PRODUCT_ID_FLD + "=" + strParentID
							+ " AND PostDate >= '" + dtmStartTime.ToString("G") + "'"
							+ " AND PostDate < '" + dtmEndTime.ToString("G") + "'"
							+ " AND Purpose = " + (int)PurposeEnum.Plan;
						
						#region Delivery Plan

						try
						{
							decDeliveryPlan += Convert.ToDecimal(dtbDeliveryPlanForParent.Compute("SUM(Quantity)", strPlanForParentFilter));
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
							decDeliveryActual += Convert.ToDecimal(dtbIssueMaterial.Compute("SUM(Quantity)", strActualForParentFilter));
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
						drowDeliveryPlanParent[PRODUCT_ID_FLD] = strParentID;
						drowDeliveryPlanParent[CATEGORY_ID_FLD] = drowParent[CATEGORY_ID_FLD];
						drowDeliveryPlanParent[VENDOR_FLD] = drowParent[VENDOR_FLD];
						drowDeliveryPlanParent[SOURCE_FLD] = drowParent[SOURCE_FLD];
						drowDeliveryPlanParent[PARTNUMBER_FLD] = drowParent[PARTNUMBER_FLD];
						drowDeliveryPlanParent[PARTNAME_FLD] = drowParent[PARTNAME_FLD];
						drowDeliveryPlanParent[CATEGORY_FLD] = drowParent[CATEGORY_FLD];
						drowDeliveryPlanParent[UNIT_FLD] = drowParent[UNIT_FLD];
						drowDeliveryPlanParent[BOM_FLD] = drowParent[QUANTITY_COL];
						drowDeliveryPlanParent[MODEL_FLD] = drowParent[MODEL_FLD];
						drowDeliveryPlanParent[SAFETYSTOCK_FLD] = drowParent[SAFETYSTOCK_FLD];
						drowDeliveryPlanParent[CONTENT_FLD] = DELIVERY;
						drowDeliveryPlanParent[SUBCONTENT_FLD] = PLAN;
						drowDeliveryPlanParent[COMPONENTID_FLD] = strProductID;
						drowDeliveryPlanParent[TOTAL_FLD] = decRowDeliveryPlan;
						// add to table
						dtbDelivery.Rows.Add(drowDeliveryPlanParent);

						blnHasTotalDeliveryActual = true;
						drowDeliveryActualParent[PRODUCT_ID_FLD] = strParentID;
						drowDeliveryActualParent[CATEGORY_ID_FLD] = drowParent[CATEGORY_ID_FLD];
						drowDeliveryActualParent[VENDOR_FLD] = drowParent[VENDOR_FLD];
						drowDeliveryActualParent[SOURCE_FLD] = drowParent[SOURCE_FLD];
						drowDeliveryActualParent[PARTNUMBER_FLD] = drowParent[PARTNUMBER_FLD];
						drowDeliveryActualParent[PARTNAME_FLD] = drowParent[PARTNAME_FLD];
						drowDeliveryActualParent[CATEGORY_FLD] = drowParent[CATEGORY_FLD];
						drowDeliveryActualParent[UNIT_FLD] = drowParent[UNIT_FLD];
						drowDeliveryActualParent[BOM_FLD] = drowParent[QUANTITY_COL];
						drowDeliveryActualParent[MODEL_FLD] = drowParent[MODEL_FLD];
						drowDeliveryActualParent[SAFETYSTOCK_FLD] = drowParent[SAFETYSTOCK_FLD];
						drowDeliveryActualParent[CONTENT_FLD] = DELIVERY;
						drowDeliveryActualParent[SUBCONTENT_FLD] = ACTUAL;
						drowDeliveryActualParent[COMPONENTID_FLD] = strProductID;
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

				for (int i = 1; i <= intDaysInMonth; i++)
				{
					#region Defind variable and condition

					string strColName = "D" + i.ToString("00");
					decimal decDeliveryPlan = 0, decDeliveryActual = 0, decTotalDeliveryPlan = 0, decTotalDeliveryActual = 0;
					decimal decArrivalPlan = 0, decArrivalActual = 0, decArrivalActualProgress = 0;
					decimal decNGReturn = 0, decOutAbnormal = 0;
					decimal decMiscOK = 0, decMiscNG = 0, decIssueOK = 0;
					decimal decReturnVendorOK = 0, decReturnVendorNG = 0, decAdjustDayOK = 0, decAdjustDayNG = 0;
					DateTime dtmDay = new DateTime(intYear, intMonth, i);
					string strProductionLineID = drowItem["ProductionLineID"].ToString();
					DataTable dtbTempCycle = RefineCycle(strProductionLineID, dtbPlanningOffset, dtbCycles);
					StringBuilder sbCycleIDs;
					DataTable dtbCyclesCurrentMonth = ArrangeCycles(dtmFromDate, DateTime.MinValue, dtbTempCycle, arrPlanningPeriod, out sbCycleIDs);
					DateTime dtmStartTime = dtmDay;
					DateTime dtmEndTime = dtmDay;
					// cycle of current day
					string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
					// get start and end time based on shift pattern
					GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
					string strSOFilter = PRODUCT_ID_FLD + "=" + strProductID
						+ " AND ScheduleDate >= '" + dtmStartTime.ToString("G") + "'"
						+ " AND ScheduleDate < '" + dtmEndTime.ToString("G") + "'";
					string strExpression = PRODUCT_ID_FLD + "=" + strProductID
						+ " AND " + POST_DATE + " >= '" + dtmStartTime.ToString("G") + "'"
						+ " AND " + POST_DATE + " < '" + dtmEndTime.ToString("G") + "'";
					string strIssueFilter = COMPONENTID_FLD + "=" + strProductID
						+ " AND " + POST_DATE + " >= '" + dtmStartTime.ToString("G") + "'"
						+ " AND " + POST_DATE + " < '" + dtmEndTime.ToString("G") + "'";
					string strCPOFilter = "ProductID = " + strProductID
						+ " AND EndTime >= '" + dtmStartTime.ToString("G") + "'"
						+ " AND EndTime < '" + dtmEndTime.ToString("G") + "'"
						+ " AND DCOptionMasterID = " + strCycleID;
					DateTime dtmEndOfDay = dtmDay.AddDays(1);
					string strAdjustFilter = "ProductID = " + strProductID + " AND"
						+ " PostDate >='" + dtmDay.ToString("G") + "' AND"
						+ " PostDate <'" + dtmEndOfDay.ToString("G") + "'";

					#endregion

					#region Delivery For SO

					#region plan for SO

					try
					{
						decDeliveryPlan += Convert.ToDecimal(dtbDeliveryPlanForSO.Compute("SUM(Quantity)", strSOFilter));
					}
					catch{}

					if (decDeliveryPlan != 0)
					{
						decRowDeliveryPlan += decDeliveryPlan;
						blnHasDeliveryPlan = true;
						blnHasTotalDeliveryPlan = true;
						drowDeliveryPlan[strColName] = decDeliveryPlan;
					}

					try
					{
						decTotalDeliveryPlan = Convert.ToDecimal(drowTotalDeliveryPlan[strColName]);
					}
					catch{}
					decTotalDeliveryPlan += decDeliveryPlan;
					decRowTotalPlan += decTotalDeliveryPlan;
					// update total
					drowTotalDeliveryPlan[strColName] = decTotalDeliveryPlan;
					
					#endregion

					#region actual for SO

					try
					{
						decDeliveryActual += Convert.ToDecimal(dtbDeliveryActualForSO.Compute("SUM(Quantity)", strExpression));
					}
					catch{}
					
					#endregion

					#endregion

					#region Delivery Actual From Misc. Issue

					try
					{
						decDeliveryActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND SourceLocationID = " + pstrLocationID
							+ " AND Purpose IN (" + (int)PurposeEnum.Outside + "," + (int)PurposeEnum.LocToLoc + ")"));
					}
					catch{}
					if ((decDeliveryActual != 0))
					{
						blnHasDeliveryActual = true;
						blnHasTotalDeliveryActual = true;
						decRowDeliveryActual += decDeliveryActual;
						drowDeliveryActual[strColName] = decDeliveryActual;
					}

					try
					{
						decTotalDeliveryActual = Convert.ToDecimal(drowTotalDeliveryActual[strColName]);
					}
					catch{}
					decTotalDeliveryActual += decDeliveryActual;
					decRowTotalActual += decTotalDeliveryActual;
					// update total
					drowTotalDeliveryActual[strColName] = decTotalDeliveryActual;
					
					#endregion

					#region Delivery Progress

					decDeliveryProgressOfPreDay = decDeliveryProgressOfPreDay + decTotalDeliveryActual - decTotalDeliveryPlan;
					if (decDeliveryProgressOfPreDay != 0)
						drowDeliveryProgress[strColName] = decDeliveryProgressOfPreDay;

					#endregion

					#region Arrival Plan

					// arrival from PO
					try
					{
						if (blnMakeItem)
							decArrivalPlan += Convert.ToDecimal(dtbArrivalPlanFromCPO.Compute("SUM(Quantity)", strCPOFilter));
						else
							decArrivalPlan += Convert.ToDecimal(dtbArrivalPlanFromPO.Compute("SUM(Quantity)", strSOFilter));
					}
					catch{}
					if (decArrivalPlan != 0)
					{
						decRowArrivalPlan += decArrivalPlan;
						blnHasArrivalPlan = true;
						drowArrivalPlan[strColName] = decArrivalPlan;
					}

					#endregion

					#region Arrival Actual

					// arrival from PO
					try
					{
						decArrivalActual += Convert.ToDecimal(dtbArrivalActualFromPO.Compute("SUM(Quantity)", strExpression));
						decArrivalActualProgress += decArrivalActual;
					}
					catch{}
					// arrival from Misc. Issue
					try
					{
						decArrivalActual += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND Purpose IN (" + (int)PurposeEnum.LocToLoc + "," + (int)PurposeEnum.Transfer + "," + (int)PurposeEnum.ReturnPrevious + ")"
							+ " AND DesBinType = " + (int)BinTypeEnum.OK
							+ " AND DesLocationID = " + pstrLocationID));
					}
					catch{}
					try
					{
						decArrivalActualProgress += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND Purpose IN (" + (int)PurposeEnum.LocToLoc + ")"
							+ " AND DesBinType = " + (int)BinTypeEnum.OK
							+ " AND DesLocationID = " + pstrLocationID));
					}
					catch{}
					// arrival from positive adjustment
					try
					{
						decArrivalActual += Convert.ToDecimal(dtbAdjustment.Compute("SUM(Quantity)", strAdjustFilter
							+ " AND BinType = " + (int)BinTypeEnum.OK)
							+ " AND Quantity > 0");
					}
					catch{}
					// arrival from recover material
					try
					{
						decArrivalActual += Convert.ToDecimal(dtbRecover.Compute("SUM(Quantity)", strExpression
							+ " AND BinType = " + (int)BinTypeEnum.OK));
					}
					catch{}
					if (decArrivalActual != 0)
					{
						decRowArrivalActual += decArrivalActual;
						blnHasArrivalActual = true;
						drowArrivalActual[strColName] = decArrivalActual;
					}

					#endregion

					#region Arrival Progress

					// decArrivalActualProgress = PO Receipt + Misc. Issue (xuat chuyen kho) 
					decArrivalProgressOfPreDay = decArrivalProgressOfPreDay + decArrivalActualProgress - decArrivalPlan;
					if (decArrivalProgressOfPreDay != 0)
						drowArrivalProgress[strColName] = decArrivalProgressOfPreDay;
                    
					#endregion

					#region NG Part Return

					try
					{
						decNGReturn += Convert.ToDecimal(dtbRecover.Compute("SUM(Quantity)", strExpression
							+ " AND BinType = " + (int)BinTypeEnum.NG));
					}
					catch{}
					try
					{
						decNGReturn += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND DesBinType = " + (int)BinTypeEnum.NG
							+ " AND DesLocationID = " + pstrLocationID
							+ " AND Purpose = " + (int)PurposeEnum.ReturnPrevious));
					}
					catch{}
					if (decNGReturn != 0)
					{
						decRowNG += decNGReturn;
						blnHasNG = true;
						drowNGPartReturn[strColName] = decNGReturn;
					}

					#endregion

					#region Out Abnormal - Other Issue

					try
					{
						decOutAbnormal += Convert.ToDecimal(dtbReturnToVendor.Compute("SUM(Quantity)", strExpression));
					}
					catch{}
					try
					{
						decOutAbnormal += Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND Purpose IN (" + (int)PurposeEnum.QC + "," + (int)PurposeEnum.Misc + "," + (int)PurposeEnum.Scrap + ")"
							+ " AND SourceLocationID = " + pstrLocationID));
					}
					catch{}
					try
					{
						decOutAbnormal += Convert.ToDecimal(dtbIssueMaterial.Compute("SUM(Quantity)", strIssueFilter
							+ " AND Purpose IN (" + (int)PurposeEnum.Compensation + ")"));
					}
					catch{}
					try
					{
						decOutAbnormal += Convert.ToDecimal(dtbAdjustment.Compute("SUM(Quantity)", strAdjustFilter
							+ " AND Quantity < 0"));
					}
					catch{}
					if (decOutAbnormal != 0)
					{
						blnHasAbnormal = true;
						decRowOutAbnormal += decOutAbnormal;
						drowOutAbnormal[strColName] = decOutAbnormal;
					}
					
					#endregion

					#region Stock Plan

					decStockPlanOfPreDay = decStockPlanOfPreDay + decArrivalPlan - decTotalDeliveryPlan;
					if (decStockPlanOfPreDay != 0)
						drowStockPlan[strColName] = decStockPlanOfPreDay;

					#endregion

					#region Stock Actual OK

					try
					{
						decMiscOK = Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND Purpose IN (" + (int)PurposeEnum.QC + "," + (int)PurposeEnum.Misc + "," + (int)PurposeEnum.Scrap + ")"
							+ " AND SourceLocationID = " + pstrLocationID
							+ " AND SouceBinType = " + (int)BinTypeEnum.OK));
					}
					catch{}
					try
					{
						decReturnVendorOK = Convert.ToDecimal(dtbReturnToVendor.Compute("SUM(Quantity)", strExpression
							+ " AND BinType = " + (int)BinTypeEnum.OK));
					}
					catch{}
					try
					{
						decIssueOK = Convert.ToDecimal(dtbIssueMaterial.Compute("SUM(Quantity)", strIssueFilter
							+ " AND Purpose IN (" + (int)PurposeEnum.Compensation + ")"
							+ " AND BinType = " + (int)BinTypeEnum.OK));
					}
					catch{}
					try
					{
						decAdjustDayOK = Convert.ToDecimal(dtbAdjustment.Compute("SUM(Quantity)", strAdjustFilter
							+ " AND BinType = " + (int)BinTypeEnum.OK
							+ " AND Quantity < 0"));
					}
					catch{}
					decStockActualOKOfPreDay = decStockActualOKOfPreDay + decArrivalActual - (decTotalDeliveryActual
						+ decMiscOK + decIssueOK + decReturnVendorOK + decAdjustDayOK);
					if (dtmDay > new DateTime(dtmServerDate.Year, dtmServerDate.Month, dtmServerDate.Day))
						decStockActualOKOfPreDay -= decTotalDeliveryPlan;
					if (decStockActualOKOfPreDay != 0)
						drowStockActualOK[strColName] = decStockActualOKOfPreDay;

					#endregion

					#region Stock Actual NG

					try
					{
						decMiscNG = Convert.ToDecimal(dtbMiscIssue.Compute("SUM(Quantity)", strExpression
							+ " AND Purpose IN (" + (int)PurposeEnum.QC + "," + (int)PurposeEnum.Misc + "," + (int)PurposeEnum.Scrap + ")"
							+ " AND SourceLocationID = " + pstrLocationID
							+ " AND SouceBinType = " + (int)BinTypeEnum.NG));
					}
					catch{}
					try
					{
						decReturnVendorNG = Convert.ToDecimal(dtbReturnToVendor.Compute("SUM(Quantity)", strExpression
							+ " AND BinType = " + (int)BinTypeEnum.NG));
					}
					catch{}
					try
					{
						decAdjustDayNG = Convert.ToDecimal(dtbAdjustment.Compute("SUM(Quantity)", strAdjustFilter
							+ " AND BinType = " + (int)BinTypeEnum.NG));
					}
					catch{}
					decStockActualNGOfPreDay = decStockActualNGOfPreDay + decNGReturn - decAdjustDayNG - (decMiscNG + decReturnVendorNG);
					if (decStockActualNGOfPreDay != 0)
						drowStockActualNG[strColName] = decStockActualNGOfPreDay;

					#endregion

					#region Warning Stock
					
					if ((decStockActualOKOfPreDay - decSafetyStock) != 0)
						drowWarningStock[strColName] = decStockActualOKOfPreDay - decSafetyStock;

					#endregion
				}

				#region Add to report table with general information

				if (blnHasTotalDeliveryActual || blnHasTotalDeliveryPlan || blnHasArrivalPlan
					|| blnHasArrivalActual || blnHasNG || blnHasAbnormal)
				{
					#region Total Delivery Plan Row
					drowTotalDeliveryPlan[PRODUCT_ID_FLD] = strProductID;
					drowTotalDeliveryPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowTotalDeliveryPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowTotalDeliveryPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowTotalDeliveryPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowTotalDeliveryPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowTotalDeliveryPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowTotalDeliveryPlan[UNIT_FLD] = drowItem[UNIT_FLD];
					drowTotalDeliveryPlan[MODEL_FLD] = drowItem[MODEL_FLD];
					drowTotalDeliveryPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowTotalDeliveryPlan[CONTENT_FLD] = DELIVERY;
					drowTotalDeliveryPlan[SUBCONTENT_FLD] = TOTAL_FLD + " " + PLAN;
					drowTotalDeliveryPlan[TOTAL_FLD] = decRowTotalPlan;
					// add to table
					dtbMasterData.Rows.Add(drowTotalDeliveryPlan);
					#endregion

					#region Total Delivery Actual Row
					drowTotalDeliveryActual[PRODUCT_ID_FLD] = strProductID;
					drowTotalDeliveryActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowTotalDeliveryActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowTotalDeliveryActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowTotalDeliveryActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowTotalDeliveryActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowTotalDeliveryActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowTotalDeliveryActual[UNIT_FLD] = drowItem[UNIT_FLD];
					drowTotalDeliveryActual[MODEL_FLD] = drowItem[MODEL_FLD];
					drowTotalDeliveryActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowTotalDeliveryActual[CONTENT_FLD] = DELIVERY;
					drowTotalDeliveryActual[SUBCONTENT_FLD] = TOTAL_FLD + " " + ACTUAL;
					drowTotalDeliveryActual[TOTAL_FLD] = decRowTotalActual;
					// add to table
					dtbMasterData.Rows.Add(drowTotalDeliveryActual);
					#endregion

					#region Delivery Plan and Actual for Parent
					
					foreach (DataRow drowForParent in dtbDelivery.Rows)
						dtbMasterData.ImportRow(drowForParent);
					#endregion

					#region Add Delivery for SO to report table

					if (blnHasDeliveryPlan || blnHasDeliveryActual)
					{
						blnHasTotalDeliveryPlan = true;
						drowDeliveryPlan[PRODUCT_ID_FLD] = strProductID;
						drowDeliveryPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowDeliveryPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowDeliveryPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowDeliveryPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowDeliveryPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowDeliveryPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowDeliveryPlan[UNIT_FLD] = drowItem[UNIT_FLD];
						drowDeliveryPlan[MODEL_FLD] = drowItem[MODEL_FLD];
						drowDeliveryPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowDeliveryPlan[CONTENT_FLD] = DELIVERY;
						drowDeliveryPlan[SUBCONTENT_FLD] = "SO " + PLAN;
						drowDeliveryPlan[COMPONENTID_FLD] = strProductID;
						drowDeliveryPlan[TOTAL_FLD] = decRowDeliveryPlan;
						// add to table
						dtbMasterData.Rows.Add(drowDeliveryPlan);

						blnHasTotalDeliveryActual = true;
						drowDeliveryActual[PRODUCT_ID_FLD] = strProductID;
						drowDeliveryActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
						drowDeliveryActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
						drowDeliveryActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
						drowDeliveryActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
						drowDeliveryActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
						drowDeliveryActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
						drowDeliveryActual[UNIT_FLD] = drowItem[UNIT_FLD];
						drowDeliveryActual[MODEL_FLD] = drowItem[MODEL_FLD];
						drowDeliveryActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
						drowDeliveryActual[CONTENT_FLD] = DELIVERY;
						drowDeliveryActual[SUBCONTENT_FLD] = "SO " + ACTUAL;
						drowDeliveryActual[COMPONENTID_FLD] = strProductID;
						drowDeliveryActual[TOTAL_FLD] = decRowDeliveryActual;
						// add to table
						dtbMasterData.Rows.Add(drowDeliveryActual);
					}

					#endregion

					#region Delivery Progress Row
					drowDeliveryProgress[PRODUCT_ID_FLD] = strProductID;
					drowDeliveryProgress[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowDeliveryProgress[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowDeliveryProgress[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowDeliveryProgress[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowDeliveryProgress[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowDeliveryProgress[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowDeliveryProgress[UNIT_FLD] = drowItem[UNIT_FLD];
					drowDeliveryProgress[MODEL_FLD] = drowItem[MODEL_FLD];
					drowDeliveryProgress[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowDeliveryProgress[CONTENT_FLD] = DELIVERY;
					drowDeliveryProgress[SUBCONTENT_FLD] = PROGRESS;
					drowDeliveryProgress[TOTAL_FLD] = decDeliveryProgressOfPreDay;
					// add to table
					dtbMasterData.Rows.Add(drowDeliveryProgress);
					#endregion

					#region Out Abnormal Row
					drowOutAbnormal[PRODUCT_ID_FLD] = strProductID;
					drowOutAbnormal[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowOutAbnormal[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowOutAbnormal[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowOutAbnormal[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowOutAbnormal[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowOutAbnormal[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowOutAbnormal[UNIT_FLD] = drowItem[UNIT_FLD];
					drowOutAbnormal[MODEL_FLD] = drowItem[MODEL_FLD];
					drowOutAbnormal[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowOutAbnormal[CONTENT_FLD] = DELIVERY;
					drowOutAbnormal[SUBCONTENT_FLD] = OUT_ABNORMAL;
					drowOutAbnormal[TOTAL_FLD] = decRowOutAbnormal;
					// add to table
					dtbMasterData.Rows.Add(drowOutAbnormal);
					#endregion

					#region Arrival Plan
					drowArrivalPlan[PRODUCT_ID_FLD] = strProductID;
					drowArrivalPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowArrivalPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowArrivalPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowArrivalPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowArrivalPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowArrivalPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowArrivalPlan[UNIT_FLD] = drowItem[UNIT_FLD];
					drowArrivalPlan[MODEL_FLD] = drowItem[MODEL_FLD];
					drowArrivalPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowArrivalPlan[CONTENT_FLD] = ARRIVAL;
					drowArrivalPlan[SUBCONTENT_FLD] = PLAN;
					drowArrivalPlan[TOTAL_FLD] = decRowArrivalPlan;
					// add to table
					dtbMasterData.Rows.Add(drowArrivalPlan);
					#endregion

					#region Arrival Actual
					drowArrivalActual[PRODUCT_ID_FLD] = strProductID;
					drowArrivalActual[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowArrivalActual[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowArrivalActual[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowArrivalActual[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowArrivalActual[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowArrivalActual[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowArrivalActual[UNIT_FLD] = drowItem[UNIT_FLD];
					drowArrivalActual[MODEL_FLD] = drowItem[MODEL_FLD];
					drowArrivalActual[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowArrivalActual[CONTENT_FLD] = ARRIVAL;
					drowArrivalActual[SUBCONTENT_FLD] = ACTUAL;
					drowArrivalActual[TOTAL_FLD] = decRowArrivalActual;
					// add to table
					dtbMasterData.Rows.Add(drowArrivalActual);
					#endregion

					#region Arrival Progress
					drowArrivalProgress[PRODUCT_ID_FLD] = strProductID;
					drowArrivalProgress[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowArrivalProgress[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowArrivalProgress[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowArrivalProgress[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowArrivalProgress[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowArrivalProgress[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowArrivalProgress[UNIT_FLD] = drowItem[UNIT_FLD];
					drowArrivalProgress[MODEL_FLD] = drowItem[MODEL_FLD];
					drowArrivalProgress[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowArrivalProgress[CONTENT_FLD] = ARRIVAL;
					drowArrivalProgress[SUBCONTENT_FLD] = PROGRESS;
					drowArrivalProgress[TOTAL_FLD] = decArrivalProgressOfPreDay;
					// add to table
					dtbMasterData.Rows.Add(drowArrivalProgress);
					#endregion

					#region NG Part Return
					drowNGPartReturn[PRODUCT_ID_FLD] = strProductID;
					drowNGPartReturn[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowNGPartReturn[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowNGPartReturn[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowNGPartReturn[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowNGPartReturn[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowNGPartReturn[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowNGPartReturn[UNIT_FLD] = drowItem[UNIT_FLD];
					drowNGPartReturn[MODEL_FLD] = drowItem[MODEL_FLD];
					drowNGPartReturn[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowNGPartReturn[CONTENT_FLD] = ARRIVAL;
					drowNGPartReturn[SUBCONTENT_FLD] = NG_PARTRETURN;
					drowNGPartReturn[TOTAL_FLD] = decRowNG;
					// add to table
					dtbMasterData.Rows.Add(drowNGPartReturn);
					#endregion

					#region Stock Plan
					drowStockPlan[PRODUCT_ID_FLD] = strProductID;
					drowStockPlan[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowStockPlan[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowStockPlan[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowStockPlan[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowStockPlan[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowStockPlan[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowStockPlan[UNIT_FLD] = drowItem[UNIT_FLD];
					drowStockPlan[MODEL_FLD] = drowItem[MODEL_FLD];
					drowStockPlan[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowStockPlan[CONTENT_FLD] = STOCK;
					drowStockPlan[SUBCONTENT_FLD] = PLAN;
					drowStockPlan[TOTAL_FLD] = decStockPlanOfPreDay;
					// add to table
					dtbMasterData.Rows.Add(drowStockPlan);
					#endregion

					#region Stock Actual OK
					drowStockActualOK[PRODUCT_ID_FLD] = strProductID;
					drowStockActualOK[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowStockActualOK[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowStockActualOK[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowStockActualOK[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowStockActualOK[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowStockActualOK[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowStockActualOK[UNIT_FLD] = drowItem[UNIT_FLD];
					drowStockActualOK[MODEL_FLD] = drowItem[MODEL_FLD];
					drowStockActualOK[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowStockActualOK[CONTENT_FLD] = STOCK;
					drowStockActualOK[SUBCONTENT_FLD] = ACTUAL + " OK";
					drowStockActualOK[TOTAL_FLD] = decStockActualOKOfPreDay;
					// add to table
					dtbMasterData.Rows.Add(drowStockActualOK);
					#endregion

					#region Stock Actual NG
					drowStockActualNG[PRODUCT_ID_FLD] = strProductID;
					drowStockActualNG[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowStockActualNG[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowStockActualNG[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowStockActualNG[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowStockActualNG[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowStockActualNG[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowStockActualNG[UNIT_FLD] = drowItem[UNIT_FLD];
					drowStockActualNG[MODEL_FLD] = drowItem[MODEL_FLD];
					drowStockActualNG[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowStockActualNG[CONTENT_FLD] = STOCK;
					drowStockActualNG[SUBCONTENT_FLD] = ACTUAL + " NG";
					drowStockActualNG[TOTAL_FLD] = decStockActualNGOfPreDay;
					// add to table
					dtbMasterData.Rows.Add(drowStockActualNG);
					#endregion

					#region Warning Stock
					drowWarningStock[PRODUCT_ID_FLD] = strProductID;
					drowWarningStock[CATEGORY_ID_FLD] = drowItem[CATEGORY_ID_FLD];
					drowWarningStock[VENDOR_FLD] = drowItem[VENDOR_FLD];
					drowWarningStock[SOURCE_FLD] = drowItem[SOURCE_FLD];
					drowWarningStock[PARTNUMBER_FLD] = drowItem[PARTNUMBER_FLD];
					drowWarningStock[PARTNAME_FLD] = drowItem[PARTNAME_FLD];
					drowWarningStock[CATEGORY_FLD] = drowItem[CATEGORY_FLD];
					drowWarningStock[UNIT_FLD] = drowItem[UNIT_FLD];
					drowWarningStock[MODEL_FLD] = drowItem[MODEL_FLD];
					drowWarningStock[SAFETYSTOCK_FLD] = drowItem[SAFETYSTOCK_FLD];
					drowWarningStock[CONTENT_FLD] = STOCK;
					drowWarningStock[SUBCONTENT_FLD] = WARNING;
					drowWarningStock[TOTAL_FLD] = decStockActualOKOfPreDay - decSafetyStock;
					// add to table
					dtbMasterData.Rows.Add(drowWarningStock);
					#endregion
				}
				#endregion
			}
			#endregion

			#region Rendering Report

			rptReport.ReportName = "NIGURI";
			rptReport.DataSource.Recordset = dtbMasterData;
			
			// and show it in preview dialog				
			C1PrintPreviewDialog printPreview = new C1PrintPreviewDialog();
			
			const string REPORTFLD_PARAMETER_CCN = "fldParameterCCN";
			const string REPORTFLD_PARAMETER_MONTH = "fldParamMonth";
			const string REPORTFLD_PARAMETER_YEAR = "fldParamYear";
			const string REPORTFLD_PARAMETER_PRODUCTIONLINE = "fldProLine";
			const string PARAMETER_CATEGORY = "fldCategoryParam";
			const string PARAMETER_MODEL = "fldModelParam";
			const string PARAMETER_VENDOR = "fldVendorParam";
			const string PARAMETER_SOURCE = "fldSourceParam";
			const string PARAMETER_PART = "fldPartParam";
			const string TITLE_FLD = "fldTitle";

			#region PARAMETER

			try
			{
				rptReport.Fields[REPORTFLD_PARAMETER_CCN].Text = GetCCNCode(pstrCCNID);;
			}
			catch{}
			try
			{
				rptReport.Fields[REPORTFLD_PARAMETER_MONTH].Text = pstrMonth;
			}
			catch{}
			try
			{
				rptReport.Fields[REPORTFLD_PARAMETER_YEAR].Text = pstrYear;
			}
			catch{}
			try
			{
				rptReport.Fields[TITLE_FLD].Text = rptReport.Fields[TITLE_FLD].Text + " " + int.Parse(pstrMonth).ToString("00") + " - " + pstrYear;
			}
			catch{}
			try
			{
				printPreview.FormTitle = rptReport.Fields[TITLE_FLD].Text;
			}
			catch{}
			try
			{
				rptReport.Fields[REPORTFLD_PARAMETER_PRODUCTIONLINE].Text = GetLocationInfo(pstrLocationID);
			}
			catch{}

			try
			{
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					rptReport.Fields[PARAMETER_CATEGORY].Text = GetCategoryInfo(pstrCategoryID);
				else
				{
					rptReport.Fields[PARAMETER_CATEGORY].Visible = false;
					rptReport.Fields["lblCategoryParam"].Visible = false;
					rptReport.Fields["divCategory"].Visible = false;
				}
			}
			catch {}
			try
			{
				if (pstrPartyID != null && pstrPartyID.Length > 0)
					rptReport.Fields[PARAMETER_VENDOR].Text = GetPartyInfo(pstrPartyID);
				else
				{
					rptReport.Fields[PARAMETER_VENDOR].Visible = false;
					rptReport.Fields["lblVendorParam"].Visible = false;
					rptReport.Fields["divVendor"].Visible = false;
				}
			}
			catch {}
			if (pstrProductID != null && pstrProductID.Length > 0)
			{
				string strPartNo = string.Empty, strPartName = string.Empty;
				DataTable dtbItem = GetItemsInfo(pstrProductID);
				foreach (DataRow drowItem in dtbItem.Rows)
				{
					strPartNo += drowItem["Code"].ToString() + ", ";
					strPartName += drowItem["Description"].ToString() + ", ";
				}
				// remove the last ","
				if (strPartNo.IndexOf(",") >= 0)
					strPartNo = strPartNo.Substring(0, strPartNo.Length - 2);
				if (strPartName.IndexOf(",") >= 0)
					strPartName = strPartName.Substring(0, strPartName.Length - 2);
				try
				{
					rptReport.Fields["fldPartParam"].Text = strPartNo;
					rptReport.Fields["fldPartNameParam"].Text = strPartName;
				}
				catch{}
			}
			else
			{
				try
				{
					rptReport.Fields[PARAMETER_PART].Visible = false;
					rptReport.Fields["lblPart"].Visible = false;
					rptReport.Fields["divPart"].Visible = false;
				}
				catch {}
			}
			
			try
			{
				if (intSourceID > 0)
					rptReport.Fields[PARAMETER_SOURCE].Text = GetSourceInfo(pstrSourceID);
				else
				{
					rptReport.Fields[PARAMETER_SOURCE].Visible = false;
					rptReport.Fields["lblSourceParam"].Visible = false;
					rptReport.Fields["divSource"].Visible = false;
				}
			}
			catch {}
			try
			{
				if (pstrModel != null && pstrModel != string.Empty)
				{
					// refine Model string
					pstrModel = pstrModel.Replace("'", string.Empty);
					rptReport.Fields[PARAMETER_MODEL].Text = pstrModel;
				}
				else
				{
					rptReport.Fields[PARAMETER_MODEL].Visible = false;
					rptReport.Fields["lblModelParam"].Visible = false;
					rptReport.Fields["divModel"].Visible = false;
				}
			}
			catch {}

			#endregion

			rptReport.Render();
			printPreview.ReportViewer.Document = rptReport.Document;
			
			printPreview.Show();

			#endregion

			return dtbMasterData;
		}

		private DataTable GetItemsInfo(string pstrProductID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code, Description FROM ITM_Product WHERE ProductID IN (" + pstrProductID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odad = new OleDbDataAdapter(cmdData);
				odad.Fill(dtbData);
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
		/// Get BOM structure
		/// </summary>
		/// <returns></returns>
		private DataTable GetBOM()
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
		/// Get all parents of product from bom structure
		/// </summary>
		/// <param name="pstrProductID">ProductID</param>
		/// <param name="pdtbBOM">BOM Structure</param>
		/// <returns>Parents</returns>
		private DataRow[] GetParents(string pstrProductID, DataTable pdtbBOM)
		{
			return pdtbBOM.Select("ComponentID = '" + pstrProductID + "'");
		}
		/// <summary>
		/// Gets all Items store in location
		/// </summary>
		/// <param name="pstrLocationID">Location ID</param>
		/// <returns>DataTable</returns>
		private DataTable GetItemInLocation(string pstrLocationID, string pstrCategoryID, string pstrProductID, string pstrModel,
			string pstrPartyID, int pintSourceID)
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
					+ " WHERE ITM_Product.LocationID = " + pstrLocationID;
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					strSQL += " AND ITM_Product.CategoryID IN (" + pstrCategoryID + ")";
				if (pstrProductID != null && pstrProductID.Length > 0)
					strSQL += " AND ITM_Product.ProductID IN (" + pstrProductID + ")";
				if (pstrModel != null && pstrModel != string.Empty)
					strSQL += " AND ITM_Product.Revision IN (" + pstrModel + ")";
				if (pstrPartyID != null && pstrPartyID.Length > 0)
					strSQL += " AND ITM_Product.PrimaryVendorID IN (" + pstrPartyID + ")";
				if (pintSourceID > 0)
					strSQL += " AND ITM_Product.SourceID IN (" + pintSourceID + ")";
				strSQL += " ORDER BY ITM_Product.ProductID";

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
		/// <param name="pstrLocationID">Location ID</param>
		/// <returns>DataTable</returns>
		private DataTable GetBeginNetQuantity(string pstrCCNID, string pstrItems, string pstrLocationID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql=	"SELECT ISNULL(SUM(ISNULL(OHQuantity, 0)), 0) - ISNULL(SUM(ISNULL(CommitQuantity, 0)), 0) AS " + QUANTITY_COL
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
		/// Gets Master Location ID of Location
		/// </summary>
		/// <param name="pstrLocationID">LocationID</param>
		/// <returns>Master Location ID</returns>
		private int GetMasterLocationID(string pstrLocationID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT MasterLocationID FROM MST_Location WHERE LocationID = " + pstrLocationID;
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				try
				{
					return Convert.ToInt32(cmdData.ExecuteScalar());
				}
				catch
				{
					return 0;
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
		/// Get Database server date
		/// </summary>
		/// <returns>Database server date</returns>
		private DateTime GetDBDate()
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql =	" SELECT  getdate() ";
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
				string strSql = "SELECT	Code FROM MST_CCN WHERE CCNID = " + pstrCCNID;
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
		/// Get Location Code and Name
		/// </summary>
		/// <param name="pstrLocationID">LocationID</param>
		/// <returns>Location Cod (Name)</returns>
		private string GetLocationInfo(string pstrLocationID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM MST_Location WHERE LocationID IN (" + pstrLocationID + ")";
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

		private string GetCategoryInfo(string pstrCategoryID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM ITM_Category WHERE CategoryID IN (" + pstrCategoryID + ")";
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

		private string GetPartyInfo(string pstrPartyID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' AS 'Code' FROM MST_Party WHERE PartyID IN (" + pstrPartyID + ")";
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

		private string GetSourceInfo(string pstrSourceID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM ITM_Source WHERE SourceID = " + pstrSourceID;
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
				cmdData.CommandTimeout = 1000;
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
		/// Get Delivery plan for parent
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetDeliveryPlanForParent(string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
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
					+ " SUM(ISNULL(CommitQuantity, 0)) AS Quantity, PostDate, PRO_IssuePurpose.Code AS Purpose"
					+ " FROM PRO_IssueMaterialDetail JOIN PRO_IssueMaterialMaster"
					+ " ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID"
					+ " JOIN PRO_WorkOrderDetail ON PRO_IssueMaterialDetail.WorkOrderDetailID = PRO_WorkOrderDetail.WorkOrderDetailID"
					+ " JOIN PRO_IssuePurpose ON PRO_IssueMaterialMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID"
					+ " WHERE PRO_IssueMaterialDetail.LocationID = " + pstrLocationID
					+ " AND PostDate >= ? AND PostDate < ?"
					+ " AND PRO_IssueMaterialDetail.ProductID IN (" + strItems + ")"
					+ " GROUP BY PRO_WorkOrderDetail.ProductID, PRO_IssueMaterialDetail.ProductID, PostDate, PRO_IssuePurpose.Code"
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
		/// Get delivery plan for all sale order
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetDeliveryPlanForSO(string strItems, int pintMasterLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT SUM(ISNULL(DeliveryQuantity, 0)) AS Quantity, ScheduleDate, ProductID"
					+ " FROM SO_DeliverySchedule JOIN SO_SaleOrderDetail"
					+ " ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID"
					+ " JOIN SO_SaleOrderMaster ON SO_SaleOrderDetail.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID"
					+ " WHERE ScheduleDate >= ? AND ScheduleDate < ?"
					+ " AND SO_SaleOrderMaster.ShipFromLocID = " + pintMasterLocationID;
				if (strItems.Trim().Length > 0)
					strSql += " AND ProductID IN (" + strItems + ")";
				strSql += " GROUP BY ProductID, ScheduleDate ORDER BY ProductID, ScheduleDate";

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
		/// Get all shipped quantity
		/// </summary>
		/// <param name="pstrItems">Items</param>
		/// <param name="pstrCCNID">CCN</param>
		/// <param name="pintLocationID">Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetDeliveryActualForSO(string pstrItems, string pstrCCNID, int pintLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT SUM(ISNULL(CommitQuantity, 0)) AS Quantity, ProductID, ShipDate AS PostDate"
					+ " FROM	SO_CommitInventoryDetail"
					+ " WHERE LocationID = " + pintLocationID
					+ " AND CCNID = " + pstrCCNID
					+ " AND ProductID IN (" + pstrItems + ")"
					+ " AND ISNULL(Shipped,0) = 1"
					+ " AND ShipDate >= ? AND ShipDate < ?"
					+ " GROUP BY ProductID, ShipDate"
					+ " ORDER BY ProductID, ShipDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.CommandTimeout = 1000;
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch (Exception ex)
			{
				throw ex;
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
		/// Get arrival plan from purchase orders
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetArrivalPlanFromPO(string strItems, int pintMasterLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT SUM(ISNULL(DeliveryQuantity, 0)) AS 'Quantity', PO_PurchaseOrderDetail.ProductID, ScheduleDate"
					+ " FROM PO_DeliverySchedule JOIN PO_PurchaseOrderDetail"
					+ " ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster ON"
					+ " PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " WHERE ScheduleDate >= ? AND ScheduleDate < ?"
					+ " AND PO_PurchaseOrderMaster.ShipToLocID = " + pintMasterLocationID
					+ " AND ISNULL(PO_PurchaseOrderDetail.ApproverID,0) > 0 "
					+ " AND ISNULL(PO_DeliverySchedule.CancelDelivery,0) = 0";
				if (strItems.Trim().Length > 0)
					strSql += " AND ProductID IN (" + strItems + ")";
				strSql += " GROUP BY PO_PurchaseOrderDetail.ProductID, ScheduleDate ORDER BY PO_PurchaseOrderDetail.ProductID, ScheduleDate";

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
		/// Get arrival actual from Purchase Orders
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pstrLocationID">Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetArrivalActualFromPO(string strItems, string pstrLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT SUM(ISNULL(ReceiveQuantity,0)) AS 'Quantity', PO_PurchaseOrderReceiptDetail.ProductID,"
					+ " PO_PurchaseOrderReceiptMaster.PostDate"
					+ " FROM PO_PurchaseOrderReceiptDetail JOIN PO_PurchaseOrderReceiptMaster"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID"
					+ " WHERE PostDate >= ? AND PostDate < ?"
					+ " AND PO_PurchaseOrderReceiptDetail.LocationID = " + pstrLocationID;
					if (strItems.Trim().Length > 0)
						strSql += " AND PO_PurchaseOrderReceiptDetail.ProductID IN (" + strItems + ")";
				strSql += " GROUP BY PO_PurchaseOrderReceiptDetail.ProductID, PO_PurchaseOrderReceiptMaster.PostDate"
					+ " ORDER BY PO_PurchaseOrderReceiptDetail.ProductID, PO_PurchaseOrderReceiptMaster.PostDate";

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
		/// Get arrival plan from CPO (DCP Result)
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetArrivalPlanFromCPO(string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT PRO_DCPResultDetail.Quantity, StartTime, EndTime, ProductID,"
					+ " DCOptionMasterID, WorkingDate"
					+ " FROM PRO_DCPResultMaster JOIN PRO_DCPResultDetail"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " WHERE EndTime >= ? AND EndTime < ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND ProductID IN (" + strItems + ")";

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
		/// <summary>
		/// Get all item return to vender (purchase orders)
		/// </summary>
		/// <param name="strItems">Items</param>
		/// <param name="pstrLocationID">Location</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
		private DataTable GetReturnToVendor(string strItems, string pstrLocationID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT ProductID, SUM(ISNULL(Quantity, 0)) AS Quantity, PostDate"
					+ " FROM PO_ReturnToVendorDetail JOIN PO_ReturnToVendorMaster"
					+ " ON PO_ReturnToVendorDetail.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID"
					+ " WHERE PostDate >= ? AND PostDate < ?"
					+ " AND LocationID = " + pstrLocationID;
				if (strItems.Trim().Length > 0)
					strSql += " AND ProductID IN (" + strItems + ")";
				strSql += " GROUP BY ProductID, PostDate ORDER BY ProductID, PostDate";

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
		private DataTable GetDataFromAdjustment(string pstrCCNID, string pstrLocationID, string pstrItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql=	"SELECT ISNULL(AdjustQuantity,0) AS Quantity, ProductID, PostDate, MST_BIN.BinTypeID AS BinType"
					+ " FROM IV_Adjustment"
					+ " JOIN MST_BIN ON IV_Adjustment.BinID = MST_BIN.BinID"
					+ " WHERE CCNID = " + pstrCCNID
					+ " AND ProductID IN (" + pstrItems + ")"
					+ " AND PostDate >= ? AND PostDate <= ?"
					+ " AND IV_Adjustment.LocationID = " + pstrLocationID
					+ " ORDER BY ProductID, PostDate ";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();
				cmdPCS.CommandTimeout = 1000;
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdPCS);
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
		private DateTime ConvertWorkingDay(ArrayList arrDayOfWeek, ArrayList arrHolidays, DateTime pdtmDate, decimal pdecNumberOfDay, DataTable pdtbWorkingTime)
		{
			int intNumberOfDay = (int)decimal.Floor(pdecNumberOfDay);
			double dblRemainder = (double)(pdecNumberOfDay - (decimal)intNumberOfDay);
			DateTime dtmConvert = pdtmDate;
			// get real working day of current day
			DateTime dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);

			for(int i =0; i < intNumberOfDay; i++)
			{							
				//Add day
				dtmConvert = dtmConvert.AddDays(-1);
				
				//goto next day if the day is holidayday
				while(arrHolidays.Contains(new DateTime(dtmConvertWorkingDay.Year, dtmConvertWorkingDay.Month, dtmConvertWorkingDay.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
					dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				}

				dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				//goto next day if the day is off day
				while(arrDayOfWeek.Contains(dtmConvertWorkingDay.DayOfWeek))
				{
					dtmConvert = dtmConvert.AddDays(-1);
					dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
					if(arrHolidays.Contains(new DateTime(dtmConvertWorkingDay.Year, dtmConvertWorkingDay.Month, dtmConvertWorkingDay.Day)))
						dtmConvert = dtmConvert.AddDays(-1);
					dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				}

				dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				//goto next day if the day is holidayday
				while(arrHolidays.Contains(new DateTime(dtmConvertWorkingDay.Year, dtmConvertWorkingDay.Month, dtmConvertWorkingDay.Day)))
				{
					dtmConvert = dtmConvert.AddDays(-1);
					dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				}
			}
						
			//Add remainder
			dtmConvert = dtmConvert.AddDays(-dblRemainder);
			dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);

			//goto next day if the day is holidayday
			while(arrHolidays.Contains(new DateTime(dtmConvertWorkingDay.Year, dtmConvertWorkingDay.Month, dtmConvertWorkingDay.Day)))
			{
				dtmConvert = dtmConvert.AddDays(-1);
				dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			}

			dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			//goto next day if the day is off day
			while(arrDayOfWeek.Contains(dtmConvertWorkingDay.DayOfWeek))
			{
				dtmConvert = dtmConvert.AddDays(-1);
				dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
				if(arrHolidays.Contains(new DateTime(dtmConvertWorkingDay.Year, dtmConvertWorkingDay.Month, dtmConvertWorkingDay.Day)))
					dtmConvert = dtmConvert.AddDays(-1);
				dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			}

			dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			//goto next day if the day is holidayday
			while(arrHolidays.Contains(new DateTime(dtmConvertWorkingDay.Year, dtmConvertWorkingDay.Month, dtmConvertWorkingDay.Day)))
			{
				dtmConvert = dtmConvert.AddDays(-1);
				dtmConvertWorkingDay = GetRealWorkingDay(dtmConvert, pdtbWorkingTime);
			}
			return dtmConvert;
		}
		private DateTime GetFirtValidWorkday(ArrayList arrOffDay, ArrayList arrHolidays, DateTime dtmMonth)
		{
			DateTime dtmFirstValidDay = DateTime.MinValue;
			for (int i = 1; i <= DateTime.DaysInMonth(dtmMonth.Year, dtmMonth.Month); i++)
			{
				dtmFirstValidDay = new DateTime(dtmMonth.Year, dtmMonth.Month, i);
				if (arrOffDay.Contains(dtmFirstValidDay.DayOfWeek) || arrHolidays.Contains(dtmFirstValidDay))
					continue;
				else
					break;
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
		private void GetStartAndEndTime(DateTime pdtmCurrentDay, ref DateTime pdtmStartTime, ref DateTime pdtmEndTime, DataTable pdtmWorkingTime)
		{
			DataRow[] drowShifts = pdtmWorkingTime.Select(string.Empty, "WorkTimeFrom ASC");

			if (drowShifts.Length <= 0)
				return;
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
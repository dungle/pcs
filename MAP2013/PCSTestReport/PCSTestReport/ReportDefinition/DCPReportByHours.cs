using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Text;
using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace DCPReportByHours
{
	/// <summary>
	/// Summary description for DCPReportByHours.
	/// </summary>
	public class DCPReportByHours : MarshalByRefObject, IDynamicReport
	{
		#region Enum

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
		private enum RowType
		{
			Delivery = 1,
			Production = 2
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
		/// <summary>
		/// DCPResultDetail.Type: 0 - Running, 1 - Change category, 2 - CheckPoint
		/// </summary>
		public enum DCPResultTypeEnum
		{
			/// <summary>
			/// 0: Running time
			/// </summary>
			Running = 0,
			/// <summary>
			/// 1: Change category time
			/// </summary>
			ChangeCategory = 1,
			/// <summary>
			/// 2: Check point time
			/// </summary>
			CheckPoint = 2
		}

		#endregion

		public DCPReportByHours()
		{
			
		}


		private string mConnectionString;
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

		private C1PrintPreviewControl mViewer;
		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mViewer; }
			set { mViewer = value; }
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

		private bool mUseEngine;
		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseEngine; }
			set { mUseEngine = value; }
		}

		private string mReportFolder;
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>		
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
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
			get { return mLayoutFile; }
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

		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrCategoryID, string pstrModel, string pstrProductID)
		{
			#region Variables
			const double FIELD_WIDTH = 645;
			const string CATEGORY_FLD = "Category";
			const string ITEM_FLD = "Item";
			const string PLAN_FLD = "Plan";
			const string PRODUCT_ID_FLD = "ProductID";
			const string CATEGORY_ID_FLD = "CategoryID";
			const string QTY_PRE = "Qty";
			const string START_PRE = "Start";
			const string DUE_PRE = "Due";
			const string MONTH_FORMAT = "MMM";
			const string DAY_FORMAT = "00";
			const string SCHEDULE_DATE_FLD = "ScheduleDate";
			int intCCNID = 0;
			try
			{
				intCCNID = int.Parse(pstrCCNID);
			}
			catch{}
			int intProductionLineID = 0;
			try
			{
				intProductionLineID = int.Parse(pstrProductionLineID);
			}
			catch{}
			int intMonth = int.Parse(pstrMonth);
			int intYear = int.Parse(pstrYear);
			DataTable dtbWorkingTime = GetWorkingTime();
			int intDaysInMonth = DateTime.DaysInMonth(intYear, intMonth);
			DateTime dtmFromDate = new DateTime(intYear, intMonth, 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddDays(-1);
			DateTime dtmTempDate = DateTime.MinValue;
			DataTable dtbItems = new DataTable();
			C1Report rptReport = new C1Report();
			DataTable dtbValidWorkDay = GetWorkingDateFromWCCapacity(intProductionLineID);
			string strMonth = dtmFromDate.ToString(MONTH_FORMAT);
			#endregion

			#region Report layout

			if (mLayoutFile == null || mLayoutFile.Trim() == string.Empty)
				mLayoutFile = "DCPReportByHours.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile);
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			arrstrReportInDefinitionFile = null;
			rptReport.Layout.PaperSize = PaperKind.A3;

			#endregion

			#region Make Table Schema and Report Layout

			DataTable dtbReportData = new DataTable();
			dtbReportData.Columns.Add(new DataColumn(CATEGORY_FLD, typeof(string)));
			dtbReportData.Columns.Add(new DataColumn(ITEM_FLD, typeof(string)));
			dtbReportData.Columns.Add(new DataColumn(PRODUCT_ID_FLD, typeof(int)));
			dtbReportData.Columns.Add(new DataColumn(CATEGORY_ID_FLD, typeof(int)));
			// Delivery, Produce
			dtbReportData.Columns.Add(new DataColumn(PLAN_FLD, typeof(string)));
			string strTotalField = string.Empty;
			for (int i = 1; i <= intDaysInMonth; i++)
			{
				strTotalField += QTY_PRE + i.ToString(DAY_FORMAT) + "+";
				// quantity col
				dtbReportData.Columns.Add(new DataColumn(QTY_PRE + i.ToString(DAY_FORMAT), typeof(decimal)));
				// start time col
				dtbReportData.Columns.Add(new DataColumn(START_PRE + i.ToString(DAY_FORMAT), typeof(DateTime)));
				// due time col
				dtbReportData.Columns.Add(new DataColumn(DUE_PRE + i.ToString(DAY_FORMAT), typeof(DateTime)));

				#region Report layout

				DateTime dtmDay = new DateTime(intYear, intMonth, i);
				string strDate = "lblD" + i.ToString(DAY_FORMAT);
				string strDay = "fldDayD" + i.ToString(DAY_FORMAT);
				string strDue = "lblDue" + i.ToString(DAY_FORMAT);
				string strStart = "lblS" + i.ToString(DAY_FORMAT);
				string strQty = "lblQ" + i.ToString(DAY_FORMAT);
				try
				{
					rptReport.Fields[strDate].Text = i + "-" + strMonth;
					rptReport.Fields[strDay].Text = dtmDay.DayOfWeek.ToString().Substring(0, 3);
				}
				catch{}
				DataRow[] drowValidWorkDay = dtbValidWorkDay.Select("BeginDate <= '" + dtmDay.ToString("G") + "'"
					+ " AND EndDate >='" + dtmDay.ToString("G") + "'");
				if (drowValidWorkDay.Length == 0)
				{
					try
					{
						if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
						{
							// date
							rptReport.Fields[strDate].ForeColor = Color.Blue;
							rptReport.Fields[strDate].BackColor = Color.Yellow;
							// day of week
							rptReport.Fields[strDay].ForeColor = Color.Blue;
							rptReport.Fields[strDay].BackColor = Color.Yellow;
							// due
							rptReport.Fields[strDue].ForeColor = Color.Blue;
							rptReport.Fields[strDue].BackColor = Color.Yellow;
							// start
							rptReport.Fields[strStart].ForeColor = Color.Blue;
							rptReport.Fields[strStart].BackColor = Color.Yellow;
							// quantity
							rptReport.Fields[strQty].ForeColor = Color.Blue;
							rptReport.Fields[strQty].BackColor = Color.Yellow;
						}
						else
						{
							// date
							rptReport.Fields[strDate].ForeColor = Color.Red;
							rptReport.Fields[strDate].BackColor = Color.Yellow;
							// day of week
							rptReport.Fields[strDay].ForeColor = Color.Red;
							rptReport.Fields[strDay].BackColor = Color.Yellow;
							// due
							rptReport.Fields[strDue].ForeColor = Color.Red;
							rptReport.Fields[strDue].BackColor = Color.Yellow;
							// start
							rptReport.Fields[strStart].ForeColor = Color.Red;
							rptReport.Fields[strStart].BackColor = Color.Yellow;
							// quantity
							rptReport.Fields[strQty].ForeColor = Color.Red;
							rptReport.Fields[strQty].BackColor = Color.Yellow;
						}
					}
					catch{}
				}
				
				#endregion
			}

			try
			{
				rptReport.Fields["fldTotal"].Text = "=" + strTotalField.Substring(0, strTotalField.Length - 1);
			}
			catch{}
			
			// make temp table
			DataTable dtbDeliveryData = dtbReportData.Clone();
			DataTable dtbProductionData = dtbReportData.Clone();
			#endregion

			#region Layout the format based on days in month
			if (intDaysInMonth < 31)
			{
				for (int i = intDaysInMonth + 1; i <= 31; i++)
				{
					#region field name

					string strDate = "lblD" + i.ToString(DAY_FORMAT);
					string strDue = "lblDue" + i.ToString(DAY_FORMAT);
					string strStart = "lblS" + i.ToString(DAY_FORMAT);
					string strQty = "lblQ" + i.ToString(DAY_FORMAT);
					string strDayOfWeek = "fldDayD" + i.ToString(DAY_FORMAT);
					string strDivDue = "divDue" + i.ToString(DAY_FORMAT);
					string strDivS = "divS" + i.ToString(DAY_FORMAT);
					string strDivQ = "divQ" + i.ToString(DAY_FORMAT);
					string strDivDetailDue = "divDetailDue" + i.ToString(DAY_FORMAT);
					string strDivDetailS = "divDetailS" + i.ToString(DAY_FORMAT);
					string strDivDetailQ = "divDetailQ" + i.ToString(DAY_FORMAT);
					string strDetailDue = "fldDue" + i.ToString(DAY_FORMAT);
					string strDetailStart = "fldStart" + i.ToString(DAY_FORMAT);
					string strDetailQ = "fldQ" + i.ToString(DAY_FORMAT);
					
					#endregion

					#region Page Header

					try
					{
						rptReport.Fields[strDate].Visible = false;
						rptReport.Fields[strDayOfWeek].Visible = false;
						rptReport.Fields[strDivDue].Visible = false;
						rptReport.Fields[strDivS].Visible = false;
						rptReport.Fields[strDivQ].Visible = false;
						rptReport.Fields[strDue].Visible = false;
						rptReport.Fields[strStart].Visible = false;
						rptReport.Fields[strQty].Visible = false;
					}
					catch
					{}

					#endregion

					#region Detail

					try
					{
						rptReport.Fields[strDivDetailDue].Visible = false;
						rptReport.Fields[strDivDetailS].Visible = false;
						rptReport.Fields[strDivDetailQ].Visible = false;
						rptReport.Fields[strDetailDue].Visible = false;
						rptReport.Fields[strDetailStart].Visible = false;
						rptReport.Fields[strDetailQ].Visible = false;
					}
					catch
					{}

					#endregion
				}
				try
				{
					double dWidthToChange = (31 - intDaysInMonth)*FIELD_WIDTH;
					// resize line's width
					for (int i = 1; i <= 6; i++)
						rptReport.Fields["line" + i.ToString()].Width = rptReport.Fields["line" + i.ToString()].Width - dWidthToChange;

					#region Total columns
					
					rptReport.Fields["lblTotal"].Left = 
						rptReport.Fields["fldTotal"].Left = rptReport.Fields["lblTotal"].Left - dWidthToChange;
					rptReport.Fields["divTotal"].Left = 
						rptReport.Fields["divDetailTotal"].Left = rptReport.Fields["lblTotal"].Left + rptReport.Fields["lblTotal"].Width;

					#endregion
				}
				catch (Exception ex)
				{throw ex;}
			}

			#endregion

			#region Prepare Report data

			// gets all items pass thru production line
			dtbItems = GetProducts(pstrCategoryID, intCCNID, intProductionLineID, pstrProductID, pstrModel);
			if (dtbItems.Rows.Count <= 0)
				return dtbReportData;

			// get all cycles in selected year
			DataTable dtbCycles = GetCycles(pstrCCNID);
			ArrayList arrPlanningPeriod = GetPlanningPeriod(pstrCCNID);
			StringBuilder sbCycleIDs;
			DataTable dtbCyclesCurrentMonth = ArrangeCycles(dtmFromDate, DateTime.MinValue, dtbCycles, arrPlanningPeriod, out sbCycleIDs);
			
			// build the string item list
			StringBuilder strItems = new StringBuilder();
			foreach (DataRow drowItem in dtbItems.Rows)
				strItems.Append(drowItem[PRODUCT_ID_FLD].ToString()).Append(",");
			DataTable dtbDeliveryForParent = GetDeliveryForParent(sbCycleIDs.ToString(), strItems.ToString(0, strItems.Length - 1), dtmFromDate, dtmToDate);
			// refine the working date
			foreach (DataRow drowData in dtbDeliveryForParent.Rows)
			{
				DateTime dtmDate = (DateTime)drowData["StartTime"];
				decimal decLeadTimeOffset = 0;
				try
				{
					decLeadTimeOffset = Convert.ToDecimal(drowData["LeadTimeOffSet"]);
				}
				catch{}
				decimal decNumOfDay = decLeadTimeOffset / 86400;
				// convert to valid work day
				dtmDate = ConvertWorkingDay(dtbValidWorkDay, dtmDate, decNumOfDay);
				// remove Millisecond from date if any
				if (dtmDate.Millisecond > 0)
					dtmDate = dtmDate.AddMilliseconds(-dtmDate.Millisecond);
				drowData["StartTime"] = dtmDate;
			}
			DateTime dtmShiftFromDate = dtmFromDate;
			DateTime dtmShiftToDate = dtmToDate;
			GetStartAndEndTime(dtmFromDate, ref dtmShiftFromDate, ref dtmTempDate, dtbWorkingTime);
			GetStartAndEndTime(dtmToDate, ref dtmTempDate, ref dtmShiftToDate, dtbWorkingTime);
			DataTable dtbSO = GetDeliveryForSO(strItems.ToString(0, strItems.Length - 1), dtmShiftFromDate, dtmShiftToDate);
			// remove millisecond
			foreach (DataRow drowSO in dtbSO.Rows)
			{
				DateTime dtmDate = Convert.ToDateTime(drowSO[SCHEDULE_DATE_FLD]);
				if (dtmDate.Millisecond > 0)
					dtmDate = dtmDate.AddMilliseconds(-dtmDate.Millisecond);
				drowSO[SCHEDULE_DATE_FLD] = dtmDate;
			}
			DataTable dtbProduce = GetProduce(sbCycleIDs.ToString(), intProductionLineID, strItems.ToString(0, strItems.Length - 1), dtmFromDate, dtmToDate);
			// remove millisecond
			foreach (DataRow drowProduce in dtbProduce.Rows)
			{
				DateTime dtmDate = Convert.ToDateTime(drowProduce["StartTime"]);
				if (dtmDate.Millisecond > 0)
					dtmDate = dtmDate.AddMilliseconds(-dtmDate.Millisecond);
				drowProduce["StartTime"] = dtmDate;
			}

			#region get all delivery/produce date and put it to an array in order to build data faster

			DataTable dtbDeliveryDate = new DataTable();
			dtbDeliveryDate.Columns.Add(new DataColumn(PRODUCT_ID_FLD, typeof(int)));
			dtbDeliveryDate.Columns.Add(new DataColumn(SCHEDULE_DATE_FLD, typeof(DateTime)));
			DataColumn[] dcolPK = new DataColumn[2];
			dcolPK[0] = dtbDeliveryDate.Columns[PRODUCT_ID_FLD];
			dcolPK[1] = dtbDeliveryDate.Columns[SCHEDULE_DATE_FLD];
			dtbDeliveryDate.PrimaryKey = dcolPK;
			DataTable dtbProduceDate = dtbDeliveryDate.Clone();
			
			// delivery date from delivery for parent
			foreach (DataRow drowData in dtbDeliveryForParent.Rows)
			{
				DataRow drowDate = dtbDeliveryDate.NewRow();
				drowDate[PRODUCT_ID_FLD] = drowData[PRODUCT_ID_FLD];
				DateTime dtmDate = Convert.ToDateTime(drowData["StartTime"]);
				if (dtmDate.Year == intYear && dtmDate.Month != intMonth)
					continue;
				drowDate[SCHEDULE_DATE_FLD] = drowData["StartTime"];
				try
				{
					dtbDeliveryDate.Rows.Add(drowDate);
				}
				catch{}
			}
			// delivery date from delivery for so
			foreach (DataRow drowData in dtbSO.Rows)
			{
				DataRow drowDate = dtbDeliveryDate.NewRow();
				string strProductID = drowData[PRODUCT_ID_FLD].ToString();
				drowDate[PRODUCT_ID_FLD] = strProductID;
				drowDate[SCHEDULE_DATE_FLD] = drowData[SCHEDULE_DATE_FLD];
				try
				{
					dtbDeliveryDate.Rows.Add(drowDate);
				}
				catch{}
			}
			// produce date
			foreach (DataRow drowData in dtbProduce.Rows)
			{
				DataRow drowDate = dtbProduceDate.NewRow();
				drowDate[PRODUCT_ID_FLD] = drowData[PRODUCT_ID_FLD];
				drowDate[SCHEDULE_DATE_FLD] = drowData["StartTime"];
				try
				{
					dtbProduceDate.Rows.Add(drowDate);
				}
				catch{}
			}
			
			#endregion

			#endregion

			#region Fill Data To Table

			#region Delivery rows first

			foreach (DataRow drowItem in dtbItems.Rows)
			{
				#region defined variable

				string strProductID = drowItem[PRODUCT_ID_FLD].ToString();
				DataRow drowProductInfo = GetProductInfo(strProductID, dtbItems);
				
				#endregion

				int intMaxRow = GetMaxRowOfItem(strProductID, dtbDeliveryDate, SCHEDULE_DATE_FLD);
				ArrayList arrRows = new ArrayList();
				for (int i = 0; i < intMaxRow; i++)
				{
					DataRow drowDelivery = dtbDeliveryData.NewRow();
					arrRows.Add(drowDelivery);
				}
				// gets all delivery date of current item
				DataRow[] drowDeliveryDates = dtbDeliveryDate.Select(PRODUCT_ID_FLD + "='" + strProductID + "'", SCHEDULE_DATE_FLD + " ASC");
				DateTime dtmSameDate = DateTime.MinValue;
				ArrayList arrUsedDate = new ArrayList();
				foreach (DataRow drowDelivery in arrRows)
				{
					foreach (DataRow drowDate in drowDeliveryDates)
					{
						DateTime dtmDate = Convert.ToDateTime(drowDate[SCHEDULE_DATE_FLD]);
						// same day, ignore it
						if ((new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day)).Equals
							(new DateTime(dtmSameDate.Year, dtmSameDate.Month, dtmSameDate.Day)))
							continue;
						if (arrUsedDate.Contains(dtmDate))
							continue;
						dtmSameDate = dtmDate;
						arrUsedDate.Add(dtmDate);
						drowDelivery[PRODUCT_ID_FLD] = strProductID;
						drowDelivery[CATEGORY_ID_FLD] = drowProductInfo[CATEGORY_ID_FLD];
						drowDelivery[CATEGORY_FLD] = drowProductInfo[CATEGORY_FLD];
						string strItemName = drowProductInfo["PartNumber"] + "\n"
							+ drowProductInfo["PartName"] + "\n"
							+ drowProductInfo["Model"] + "\n";
						drowDelivery[ITEM_FLD] = strItemName;
						drowDelivery[PLAN_FLD] = RowType.Delivery.ToString();
						// cycle of current day
						string strCycleID = GetCycleOfDate(dtmDate, dtbCyclesCurrentMonth);
						string strFilterSO = PRODUCT_ID_FLD + "='" + strProductID + "'"
							+ " AND " + SCHEDULE_DATE_FLD + "='" + dtmDate.ToString("G") + "'";
						string strFilterParent = PRODUCT_ID_FLD + " ='" + strProductID + "'"
							+ " AND StartTime ='" + dtmDate.ToString("G") + "'"
							+ " AND DCOptionMasterID = '" + strCycleID + "'";
						// delivery for SO
						DataRow[] drowSO = dtbSO.Select(strFilterSO);
						DataRow[] drowParent = dtbDeliveryForParent.Select(strFilterParent);
						decimal decQuantity = 0;
						foreach (DataRow drowData in drowSO)
						{
							try
							{
								decQuantity += Convert.ToDecimal(drowData["Quantity"]);
							}
							catch{}
						}
						foreach (DataRow drowData in drowParent)
						{
							try
							{
								decQuantity += Convert.ToDecimal(drowData["Quantity"]);
							}
							catch{}
						}
						drowDelivery[QTY_PRE + dtmDate.Day.ToString(DAY_FORMAT)] = decQuantity;
						drowDelivery[DUE_PRE + dtmDate.Day.ToString(DAY_FORMAT)] = dtmDate;
					}
					dtbDeliveryData.Rows.Add(drowDelivery);
				}
			}

			#endregion

			#region production rows

			foreach (DataRow drowItem in dtbItems.Rows)
			{
				#region defined variable

				string strProductID = drowItem[PRODUCT_ID_FLD].ToString();
				DataRow drowProductInfo = GetProductInfo(strProductID, dtbItems);
				
				#endregion

				int intMaxRow = GetMaxRowOfItem(strProductID, dtbProduceDate, SCHEDULE_DATE_FLD);
				ArrayList arrRows = new ArrayList();
				for (int i = 0; i < intMaxRow; i++)
				{
					DataRow drowProduction = dtbProductionData.NewRow();
					arrRows.Add(drowProduction);
				}

				// gets all produce date of current item
				DataRow[] drowProduceDates = dtbProduceDate.Select(PRODUCT_ID_FLD + "='" + strProductID + "'", SCHEDULE_DATE_FLD + " ASC");
				DateTime dtmSameDate = DateTime.MinValue;
				ArrayList arrUsedDate = new ArrayList();
				foreach (DataRow drowProduction in arrRows)
				{
					foreach (DataRow drowDate in drowProduceDates)
					{
						DateTime dtmDate = Convert.ToDateTime(drowDate[SCHEDULE_DATE_FLD]);
						// same day, ignore it
						if ((new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day)).Equals
							(new DateTime(dtmSameDate.Year, dtmSameDate.Month, dtmSameDate.Day)))
							continue;
						if (arrUsedDate.Contains(dtmDate))
							continue;
						drowProduction[PRODUCT_ID_FLD] = strProductID;
						drowProduction[PLAN_FLD] = RowType.Production.ToString();
						drowProduction[CATEGORY_ID_FLD] = drowProductInfo[CATEGORY_ID_FLD];
						drowProduction[CATEGORY_FLD] = drowProductInfo[CATEGORY_FLD];
						string strItemName = drowProductInfo["PartNumber"] + "\n"
							+ drowProductInfo["PartName"] + "\n"
							+ drowProductInfo["Model"] + "\n";
						drowProduction[ITEM_FLD] = strItemName;
						// cycle of current day
						string strCycleID = GetCycleOfDate(dtmDate, dtbCyclesCurrentMonth);
						string strFilterParent = PRODUCT_ID_FLD + " ='" + strProductID + "'"
							+ " AND StartTime ='" + dtmDate.ToString("G") + "'"
							+ " AND DCOptionMasterID = '" + strCycleID + "'";
						dtmSameDate = dtmDate;
						arrUsedDate.Add(dtmDate);
						DataRow[] drowProduces = dtbProduce.Select(strFilterParent);
						decimal decQuantity = 0;
						foreach (DataRow drowData in drowProduces)
						{
							try
							{
								decQuantity += Convert.ToDecimal(drowData["Quantity"]);
							}
							catch{}
						}
						drowProduction[QTY_PRE + dtmDate.Day.ToString(DAY_FORMAT)] = decQuantity;
						drowProduction[START_PRE + dtmDate.Day.ToString(DAY_FORMAT)] = dtmDate;
						if (drowProduces.Length  > 0)
							drowProduction[DUE_PRE + dtmDate.Day.ToString(DAY_FORMAT)] = (DateTime)drowProduces[0]["EndTime"];
					}
					dtbProductionData.Rows.Add(drowProduction);
				}
			}

			#endregion

			foreach (DataRow drowItem in dtbItems.Rows)
			{
				#region defined variable

				string strProductID = drowItem[PRODUCT_ID_FLD].ToString();
				
				#endregion

				DataRow[] drowsDelivery = dtbDeliveryData.Select(PRODUCT_ID_FLD + " = '" + strProductID + "'");
				DataRow[] drowsProduction = dtbProductionData.Select(PRODUCT_ID_FLD + " = '" + strProductID + "'");
				foreach (DataRow drowDelivery in drowsDelivery)
					dtbReportData.ImportRow(drowDelivery);
				foreach (DataRow drowProduction in drowsProduction)
					dtbReportData.ImportRow(drowProduction);
			}

			#endregion

			#region PUSH PARAMETER VALUE
			
			// header information get from system params
			try
			{
				rptReport.Fields["fldTitle"].Text = rptReport.Fields["fldTitle"].Text + " " + strMonth.ToUpper() + " - " + intYear.ToString("0000");
			}
			catch{}
			try
			{
				rptReport.Fields["fldCompany"].Text = SystemProperty.SytemParams.Get("CompanyFullName");
			}
			catch{}
			try
			{
				rptReport.Fields["fldParameterCCN"].Text		= GetCCNCode(pstrCCNID);;
			}
			catch{}
			try
			{
				rptReport.Fields["fldYear"].Text = intYear.ToString("0000");
			}
			catch{}
			try
			{
				rptReport.Fields["fldMonth"].Text = dtmFromDate.Month.ToString(DAY_FORMAT);
			}
			catch{}
			try
			{
				rptReport.Fields["fldProductionLine"].Text = GetProCodeAndName(pstrProductionLineID);
			}
			catch{}
			if (pstrCategoryID != null && pstrCategoryID.Length > 0)
			{
				try
				{
					rptReport.Fields["fldParamCategory"].Text = GetCategoryInfo(pstrCategoryID);
				}
				catch{}
			}
			try
			{
				rptReport.Fields["fldModel"].Text = pstrModel;
			}
			catch{}
			if (pstrProductID != null && pstrProductID.Length > 0)
			{
				string strPartNo = string.Empty, strPartName = string.Empty;
				DataTable dtbItem = GetProductInfo(pstrProductID);
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
					rptReport.Fields["fldPartNo"].Text = strPartNo;
				}
				catch{}
				try
				{
					rptReport.Fields["fldPartName"].Text = strPartName;
				}
				catch{}
			}
			
			#endregion

			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbReportData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Production plan and delivery by hours " + strMonth + "-" + intYear.ToString();
			
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();

			return dtbReportData;
		}
		public DataTable GetCycles(string pstrCCNID)
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
		/// <summary>
		/// Gets list of product has produce quantity in DCP result in Production Line
		/// </summary>
		/// <param name="pintCategoryID">Category</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns>List of Product</returns>
		private DataTable GetProducts(string pstrCategoryID, int pintCCNID, int pintProductionLineID, string pstrProductID, string pstrModel)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT DISTINCT ITM_Product.ProductID, ITM_Product.Code AS PartNumber, ITM_Product.Description AS PartName,"
					+ " ITM_Product.Revision AS Model, ITM_Category.CategoryID, ITM_Category.Code AS Category"
					+ " FROM ITM_Product "
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID
					+ " AND ITM_Product.MakeItem = 1"
					+ " AND ITM_Product.ProductionLineID = " + pintProductionLineID;
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					strSql += " AND ITM_Product.CategoryID IN (" + pstrCategoryID + ")";
				if (pstrProductID != null && pstrProductID.Length > 0)
					strSql += " AND ITM_Product.ProductID IN (" + pstrProductID + ")";
				if (pstrModel != null && pstrModel.Length > 0)
					strSql += " AND ITM_Product.Revision IN (" + pstrModel + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				DataTable dtbProduct = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdData);
				odadPCS.Fill(dtbProduct);
				return dtbProduct;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetDeliveryForParent(string pstrOptionID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				if (pstrOptionID.Length == 0)
					pstrOptionID = "0";
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT DISTINCT SUM(ISNULL((PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity)/((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0)) AS Quantity,"
					+ " ITM_BOM.ComponentID AS ProductID, ITM_BOM.LeadTimeOffset, PRO_DCPResultDetail.StartTime,"
					+ " PRO_DCPResultMaster.DCOptionMasterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID IN (" + pstrOptionID + ")"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND ITM_BOM.ComponentID IN (" + strItems + ")";
				strSql += " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.StartTime,"
					+ " ITM_BOM.ComponentID, LeadTimeOffset"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, ITM_BOM.ComponentID, PRO_DCPResultDetail.StartTime";

				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
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
		private DataTable GetDeliveryForSO(string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
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
					+ " WHERE ScheduleDate >= ? AND ScheduleDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND ProductID IN (" + strItems + ")";
				strSql += " GROUP BY ProductID, ScheduleDate ORDER BY ProductID, ScheduleDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
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
		private DataTable GetProduce(string pstrOptionID, int pintProductionLineID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				if (pstrOptionID == null || pstrOptionID.Length == 0)
					pstrOptionID = "0";
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT PRO_DCPResultDetail.Quantity, PRO_DCPResultMaster.ProductID, PRO_DCPResultMaster.WorkCenterID,"
					+ " StartTime, EndTime, PRO_DCPResultMaster.DCOptionMasterID, WorkingDate"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID IN (" + pstrOptionID + ")"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND MST_WorkCenter.IsMain = 1"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND PRO_DCPResultMaster.ProductID IN (" + strItems + ")";
				strSql += " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, WorkingDate, StartTime, EndTime,"
					+ " PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultMaster.ProductID, PRO_DCPResultDetail.Quantity"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultMaster.ProductID, WorkingDate,"
					+ " StartTime, EndTime";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				odadPCS = new OleDbDataAdapter(ocmdPCS);
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

		private ArrayList GetPlanningPeriod(string pstrCCNID)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				strSql=	"SELECT DISTINCT PlanningPeriod FROM PRO_DCOptionMaster WHERE CCNID = " + pstrCCNID;
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
		/// <summary>
		/// Gets working date of main work center from work center capactity
		/// </summary>
		/// <param name="pintProductionLineID">Production Line ID</param>
		/// <returns>DataTable</returns>
		private DataTable GetWorkingDateFromWCCapacity(int pintProductionLineID)
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
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
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
		private DataTable ArrangeCycles(DateTime pdtmFromMonth, DateTime pdtmToMonth, DataTable pdtbCycles,
			ArrayList parrPlanningPeriod, out StringBuilder sbCycleIDs)
		{
			DataTable dtbResult = pdtbCycles.Clone();
			DateTime dtmFromDate = new DateTime(pdtmFromMonth.Year,  pdtmFromMonth.Month, 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddDays(-1);
			if (pdtmToMonth > DateTime.MinValue)
				dtmToDate = pdtmToMonth;
			DataTable dtbTemp = pdtbCycles.Clone();
			sbCycleIDs = new StringBuilder();
			ArrayList arrMonths = GetAllMonthInRange(dtmFromDate, dtmToDate);
			// store the lastest version of each planning period which go thru selected date
			foreach (DateTime dtmPeriod in parrPlanningPeriod)
			{
				DataRow[] drowPeriod = pdtbCycles.Select("PlanningPeriod = '" + dtmPeriod.ToString("G") + "'"
					, "Version DESC");
				if (drowPeriod.Length > 0)
				{
					DateTime dtmFromDateCycle = (DateTime)drowPeriod[0]["FromDate"];
					DateTime dtmToDateCycle = (DateTime)drowPeriod[0]["ToDate"];
					ArrayList arrCycleMonths = GetAllMonthInRange(dtmFromDateCycle, dtmToDateCycle);
					foreach (DateTime dtmDate in arrMonths)
					{
						if (arrCycleMonths.Contains(dtmDate))
							dtbTemp.ImportRow(drowPeriod[0]);
					}
				}
			}
			
			DataRow[] drowCycles = dtbTemp.Select(string.Empty, "FromDate ASC");
			DateTime dtmPlanningPeriod = DateTime.MinValue;
			for (int i = 0; i < drowCycles.Length; i++)
			{
				DataRow drowCycle = drowCycles[i];
				// current cycle plan period
				DateTime dtmPlanDate = DateTime.MinValue;
				try
				{
					dtmPlanDate = (DateTime)drowCycle["PlanningPeriod"];
				}
				catch{}
				// update first cycle information
				if (i == 0)
				{
					drowCycle["FromDate"] = dtmFromDate;
					dtmPlanningPeriod = dtmPlanDate;
					continue;
				}
				// ignore cycle plan month ealier than previous cycle
				if (dtmPlanningPeriod < dtmPlanDate)
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
			sbCycleIDs.Append("0");
			return dtbResult;
		}
		private ArrayList GetAllMonthInRange(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			pdtmFromDate = new DateTime(pdtmFromDate.Year, pdtmFromDate.Month, 1);
			pdtmToDate = new DateTime(pdtmToDate.Year, pdtmToDate.Month, 1);
			ArrayList arrMonths = new ArrayList();
			for (DateTime dtmDate = pdtmFromDate; dtmDate <= pdtmToDate; dtmDate = dtmDate.AddMonths(1))
				arrMonths.Add(dtmDate);
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
		private DataRow GetProductInfo(string pstrProductID, DataTable pdtbAllItems)
		{
			return pdtbAllItems.Select("ProductID = '" + pstrProductID + "'")[0];
		}
		private string GetCategoryInfo(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code FROM ITM_Category WHERE CategoryID IN (" + pstrID + ")";
				OleDbCommand cmdData = new OleDbCommand(strSql, oconPCS);
				cmdData.Connection.Open();
				string strCode = string.Empty;
				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(cmdData);
				odadPCS.Fill(dtbData);
				foreach (DataRow drowData in dtbData.Rows)
					strCode += drowData["Code"].ToString() + ",";
				if (strCode.Length > 0)
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

		private DataTable GetProductInfo(string pstrID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code, Description FROM ITM_Product WHERE ProductID IN (" + pstrID + ")";
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

		private int GetMaxRowOfItem(string pstrProductID, DataTable pdtbData, string pstrDateField)
		{
			DataTable dtbTempData = pdtbData.Copy();
			int intMax = 0;
			dtbTempData.PrimaryKey = null;
			// refine date field first
			DataTable dtbTemp = dtbTempData.Clone();
			DataRow[] drowDates = dtbTempData.Select("ProductID = '" + pstrProductID + "'", pstrDateField + " ASC");
			foreach (DataRow drowData in drowDates)
			{
				DateTime dtmDate = DateTime.Parse(drowData[pstrDateField].ToString());
				dtmDate = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
				drowData[pstrDateField] = dtmDate;
				dtbTemp.ImportRow(drowData);
			}
			DateTime dtmPreDate = DateTime.MinValue;
			foreach (DataRow drowData in dtbTemp.Rows)
			{
				DateTime dtmDate = (DateTime)drowData[pstrDateField];
				if (dtmDate == dtmPreDate)
					continue;
				dtmPreDate = dtmDate;
				string strFilter = "ProductID = '" + pstrProductID + "'"
					+ " AND " + pstrDateField + "='" + dtmDate.ToString() + "'";
				DataRow[] drowsMax = dtbTemp.Select(strFilter);
				intMax = (intMax < drowsMax.Length) ? drowsMax.Length : intMax;
			}
			return intMax;
		}
		/// <summary>
		/// Convert working day with offline method
		/// </summary>
		/// <param name="pdtmDate">Date to convert</param>
		/// <param name="pdecNumberOfDay">Number of day to add/subtract</param>
		/// <returns>Converted Date</returns>
		private DateTime ConvertWorkingDay(DataTable pdtbValidWorkDays, DateTime pdtmDate, decimal pdecNumberOfDay)
		{
			int intNumberOfDay = (int) decimal.Floor(pdecNumberOfDay);
			double dblRemainder = (double) (pdecNumberOfDay - (decimal) intNumberOfDay);

			DateTime dtmConvert = pdtmDate;
			DateTime dtmConverted = DateTime.MinValue;
			DataRow[] drowValidWorkDay = null;
			DateTime dtmFromDate = DateTime.MinValue;
			if (pdtbValidWorkDays.Rows.Count > 0)
				dtmFromDate = (DateTime)pdtbValidWorkDays.Rows[0]["BeginDate"];
			for(int i =0; i < intNumberOfDay; i++)
			{							
				dtmConvert = dtmConvert.AddDays(-1);

				if (dtmConvert < dtmFromDate)
					break;

				dtmConverted = new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day);

				drowValidWorkDay = pdtbValidWorkDays.Select("BeginDate <= '" + dtmConverted.ToString("G") + "'"
					+ " AND EndDate >='" + dtmConverted.ToString("G") + "'");

				while (drowValidWorkDay.Length == 0)
				{
					dtmConvert = dtmConvert.AddDays(-1);
					dtmConverted = new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day);
					if (dtmConvert < dtmFromDate)
						break;
					drowValidWorkDay = pdtbValidWorkDays.Select("BeginDate <= '" + dtmConverted.ToString("G") + "'"
						+ " AND EndDate >='" + dtmConverted.ToString("G") + "'");
				}
			}
						
			// Add remainder
			dtmConvert = dtmConvert.AddDays(-dblRemainder);

			dtmConverted = new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day);
			if (dtmConvert < dtmFromDate)
				return dtmConvert;

			drowValidWorkDay = pdtbValidWorkDays.Select("BeginDate <= '" + dtmConverted.ToString("G") + "'"
				+ " AND EndDate >='" + dtmConverted.ToString("G") + "'");

			while (drowValidWorkDay.Length == 0)
			{
				dtmConvert = dtmConvert.AddDays(-1);
				dtmConverted = new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day);
				drowValidWorkDay = pdtbValidWorkDays.Select("BeginDate <= '" + dtmConverted.ToString("G") + "'"
					+ " AND EndDate >='" + dtmConverted.ToString("G") + "'");
				if (dtmConvert < dtmFromDate)
					break;
			}
			return dtmConvert;
		}

	}
}

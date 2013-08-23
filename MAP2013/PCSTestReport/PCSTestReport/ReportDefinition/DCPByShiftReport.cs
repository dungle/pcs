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

namespace DCPByShiftReport
{
	/// <summary>
	/// Summary description for DCPByShiftReport.
	/// </summary>
	public class DCPByShiftReport : MarshalByRefObject, IDynamicReport
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
			return GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public DataTable ExecuteReport(string pstrCCNID, string pstrProductionLineID, string pstrYear, string pstrMonth, string pstrCategoryID, string pstrProductID, string pstrVersion)
		{
			#region Variables
			const double FIELD_WIDTH = 576;
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
			int intProductID = 0;
			try
			{
				intProductID = int.Parse(pstrProductID);
			}
			catch{}
			int intCategoryID = 0;
			try
			{
				intCategoryID = int.Parse(pstrCategoryID);
			}
			catch{}
			DataTable dtbWorkingTime = GetWorkingTime();
			int intDaysInMonth = DateTime.DaysInMonth(intYear, intMonth);
			DateTime dtmFromDate = new DateTime(intYear, intMonth, 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddDays(-1);
			DateTime dtmFromDatePreMonth = dtmFromDate.AddMonths(-1);
			DateTime dtmToDatePreMonth = dtmFromDatePreMonth.AddMonths(1).AddDays(-1);
			DateTime dtmTempDate = DateTime.MinValue;
			DataTable dtbItems = new DataTable();
			C1Report rptReport = new C1Report();
			DataTable dtbValidWorkDay = GetWorkingDateFromWCCapacity(intProductionLineID);
			string strMonth = dtmFromDate.ToString("MMM");
			#endregion

			#region Report layout

			if (mLayoutFile == null || mLayoutFile == string.Empty)
				mLayoutFile = "DCPByShift.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile);
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;

			#endregion

			#region Make Table Schema and Report Layout

			DataTable dtbReportData = new DataTable();
			dtbReportData.Columns.Add(new DataColumn("Category", typeof(string)));
			dtbReportData.Columns.Add(new DataColumn("PartNumber", typeof(string)));
			dtbReportData.Columns.Add(new DataColumn("PartName", typeof(string)));
			dtbReportData.Columns.Add(new DataColumn("Model", typeof(string)));
			dtbReportData.Columns.Add(new DataColumn("ProductID", typeof(int)));
			dtbReportData.Columns.Add(new DataColumn("CategoryID", typeof(int)));
			// begin quantity column
			dtbReportData.Columns.Add(new DataColumn("D00", typeof(decimal)));
			StringBuilder sbSum1Total = new StringBuilder();
			StringBuilder sbSum2Total = new StringBuilder();
			StringBuilder sbSumCategoryTotal = new StringBuilder();
			StringBuilder sbS1DTotal = new StringBuilder();
			StringBuilder sbS1PTotal = new StringBuilder();
			StringBuilder sbS2DTotal = new StringBuilder();
			StringBuilder sbS2PTotal = new StringBuilder();
			StringBuilder sbS3DTotal = new StringBuilder();
			StringBuilder sbS3PTotal = new StringBuilder();
			int intNumOfWorkday = 0;
			int intNumOfOffdays = 0;
			for (int i = 1; i <= intDaysInMonth; i++)
			{
				// shift 1 delivery
				dtbReportData.Columns.Add(new DataColumn("S1D" + i.ToString("00"), typeof(decimal)));
				// shift 1 production
				dtbReportData.Columns.Add(new DataColumn("S1PD" + i.ToString("00"), typeof(decimal)));
				// shift 2 delivery
				dtbReportData.Columns.Add(new DataColumn("S2D" + i.ToString("00"), typeof(decimal)));
				// shift 2 production
				dtbReportData.Columns.Add(new DataColumn("S2PD" + i.ToString("00"), typeof(decimal)));
				// shift 3 delivery
				dtbReportData.Columns.Add(new DataColumn("S3D" + i.ToString("00"), typeof(decimal)));
				// shift 3 production
				dtbReportData.Columns.Add(new DataColumn("S3PD" + i.ToString("00"), typeof(decimal)));
				// total delivery
				dtbReportData.Columns.Add(new DataColumn("D" + i.ToString("00"), typeof(decimal)));
				// total production
				dtbReportData.Columns.Add(new DataColumn("P" + i.ToString("00"), typeof(decimal)));
				// total stock
				dtbReportData.Columns.Add(new DataColumn("S" + i.ToString("00"), typeof(decimal)));

				#region Report layout
				DateTime dtmDay = new DateTime(intYear, intMonth, i);
				string strDate = "D" + i.ToString("00") + "Lbl";
				string strDay = "fldDayD" + i.ToString("00");
				string strShift = "fldShiftD" + i.ToString("00");
				try
				{
					rptReport.Fields[strDate].Text = i + "-" + strMonth;
				}
				catch{}
				try
				{
					rptReport.Fields[strDay].Text = dtmDay.DayOfWeek.ToString().Substring(0, 3);
				}
				catch{}
				string strExpression = "BeginDate <= '" + dtmDay.ToString("G") + "'"
					+ " AND EndDate >='" + dtmDay.ToString("G") + "'";
				DataRow[] drowValidWorkDay = dtbValidWorkDay.Select(strExpression);
				if (drowValidWorkDay.Length == 0)
				{
					// increase num of off days
					intNumOfOffdays++;
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
					catch{}
					try
					{
						if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
						{
							rptReport.Fields[strDay].ForeColor = Color.Blue;
							rptReport.Fields[strDay].BackColor = Color.Yellow;
						}
						else
						{
							rptReport.Fields[strDay].ForeColor = Color.Red;
							rptReport.Fields[strDay].BackColor = Color.Yellow;
						}
					}
					catch{}
					try
					{
						if (dtmDay.DayOfWeek == DayOfWeek.Saturday)
						{
							rptReport.Fields[strShift].ForeColor = Color.Blue;
							rptReport.Fields[strShift].BackColor = Color.Yellow;
						}
						else
						{
							rptReport.Fields[strShift].ForeColor = Color.Red;
							rptReport.Fields[strShift].BackColor = Color.Yellow;
						}
					}
					catch{}
				}
				sbSum1Total.Append("fldSum1D" + i.ToString("00")).Append(" + ");
				sbSum2Total.Append("fldSum2D" + i.ToString("00")).Append(" + ");
				sbSumCategoryTotal.Append("fldCategoriesOnDayD" + i.ToString("00")).Append(" + ");
				// delivery
				sbS1DTotal.Append("S1D" + i.ToString("00")).Append(" + ");
				sbS2DTotal.Append("S2D" + i.ToString("00")).Append(" + ");
				sbS3DTotal.Append("S3D" + i.ToString("00")).Append(" + ");
				// production
				sbS1PTotal.Append("S1PD" + i.ToString("00")).Append(" + ");
				sbS2PTotal.Append("S2PD" + i.ToString("00")).Append(" + ");
				sbS3PTotal.Append("S3PD" + i.ToString("00")).Append(" + ");
				#endregion
			}
			intNumOfWorkday = intDaysInMonth - intNumOfOffdays;
			
			try
			{
				rptReport.Fields["fldSum1Total"].Text = sbSum1Total.ToString(0, sbSum1Total.Length - 2);
				rptReport.Fields["fldSum2Total"].Text = sbSum2Total.ToString(0, sbSum2Total.Length - 2);
				rptReport.Fields["fldSum3Total"].Value = "fldSum3D" + intDaysInMonth.ToString("00");
				rptReport.Fields["fldCategoriesOnDayTotal"].Text = sbSumCategoryTotal.ToString(0, sbSumCategoryTotal.Length - 2);
				rptReport.Fields["fldTotal"].Text = sbS1DTotal.ToString(0, sbS1DTotal.Length - 2);
				rptReport.Fields["fldTotal1"].Text = sbS1PTotal.ToString(0, sbS1PTotal.Length - 2);
				rptReport.Fields["fldTotal2"].Text = sbS2DTotal.ToString(0, sbS2DTotal.Length - 2);
				rptReport.Fields["fldTotal3"].Text = sbS2PTotal.ToString(0, sbS2PTotal.Length - 2);
				rptReport.Fields["fldTotal4"].Text = sbS3DTotal.ToString(0, sbS3DTotal.Length - 2);
				rptReport.Fields["fldTotal5"].Text = sbS3PTotal.ToString(0, sbS3PTotal.Length - 2);
			}
			catch{}

			#region total field

			dtbReportData.Columns.Add(new DataColumn("Total", typeof(decimal)));
			dtbReportData.Columns.Add(new DataColumn("PTotal", typeof(decimal)));
			dtbReportData.Columns.Add(new DataColumn("STotal", typeof(decimal)));

			#endregion

			#endregion

			#region Layout the format based on days in month
			if (intDaysInMonth < 31)
			{
				for (int i = intDaysInMonth + 1; i <= 31; i++)
				{
					#region field name

					string strDate = "D" + i.ToString("00") + "Lbl";
					string strDayOfWeek = "fldDayD" + i.ToString("00");
					string strShift = "fldShiftD" + i.ToString("00");
					string strDiv = "div" + i.ToString("00");
					string strDivPage = "divPage" + i.ToString("00");
					string strDivDetail = "divDetail" + i.ToString("00");
					string strDetail = "D" + i.ToString("00") + "Ctl";
					string strSum1 = "fldSum1D" + i.ToString("00");
					string strSum2 = "fldSum2D" + i.ToString("00");
					string strSum3 = "fldSum3D" + i.ToString("00");
					string strCategories = "fldCategoriesOnDayD" + i.ToString();
					string strSC = "fldStandardCapacityD" + i.ToString();
					string strTRC = "fldTotalRequiredCapacityD" + i.ToString();
					string strRC = "fldRemainCapacityD" + i.ToString();
					string strEffect = "fldEffectiveD" + i.ToString();

					#endregion

					#region Report Header

					try
					{
						rptReport.Fields[strShift].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strDivPage].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strSum1].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strSum2].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strSum3].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strCategories].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strSC].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strTRC].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strRC].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strEffect].Visible = false;
					}
					catch
					{}

					#endregion

					#region Page Header

					try
					{
						rptReport.Fields[strDate].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strDayOfWeek].Visible = false;
					}
					catch
					{}
					try
					{
						rptReport.Fields[strDiv].Visible = false;
					}
					catch
					{}

					#endregion

					#region Detail

					try
					{
						rptReport.Fields[strDivDetail].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strDetail].Visible = false;
					}
					catch{}
					for (int j = 1; j <= 8; j++)
					{
						try
						{
							rptReport.Fields[strDetail + j].Visible = false;
						}
						catch{}
					}

					#endregion
				}
				try
				{
					#region Resize all line

					//double dWidth = rptReport.Fields["line1"].Width;
					for (int i = 1; i <= 22; i++)
						rptReport.Fields["line" + i].Width = rptReport.Fields["line" + i].Width - (31 - intDaysInMonth)*FIELD_WIDTH;

					#endregion

					#region moving rest of field in report header

					double dWidthToChange = (31 - intDaysInMonth)*FIELD_WIDTH;
					
					#region Total columns
					rptReport.Fields["fldSum1Total"].Left =
						rptReport.Fields["fldSum2Total"].Left =
						rptReport.Fields["fldSum3Total"].Left =
						rptReport.Fields["fldCategoriesOnDayTotal"].Left =
						rptReport.Fields["fldStandardCapacityTotal"].Left =
						rptReport.Fields["fldTotalRequiredCapacityTotal"].Left =
						rptReport.Fields["fldRemainCapacityTotal"].Left =
						rptReport.Fields["fldEffectiveTotal"].Left =
						rptReport.Fields["lblTotal"].Left =
						rptReport.Fields["fldTotal"].Left =
						rptReport.Fields["fldTotal1"].Left = 
						rptReport.Fields["fldTotal2"].Left = 
						rptReport.Fields["fldTotal3"].Left = 
						rptReport.Fields["fldTotal4"].Left = 
						rptReport.Fields["fldTotal5"].Left = 
						rptReport.Fields["fldTotal6"].Left = 
						rptReport.Fields["fldTotal7"].Left = 
						rptReport.Fields["fldTotal8"].Left = rptReport.Fields["fldSum1Total"].Left - dWidthToChange;
					rptReport.Fields["divTotal"].Left = 
						rptReport.Fields["divPageTotal"].Left = 
						rptReport.Fields["divDetailTotal"].Left = rptReport.Fields["fldSum1Total"].Left + FIELD_WIDTH;
					#endregion

					#endregion
				}
				catch (Exception ex)
				{throw ex;}
			}

			#endregion

			#region Prepare Report data

			// gets all items pass thru production line
			dtbItems = GetProducts(intCategoryID, intCCNID, intProductionLineID, intProductID);
			if (dtbItems.Rows.Count <= 0)
				return dtbReportData;

			// planning offset
			DataTable dtbPlanningOffset = GetPlanningOffset(pstrCCNID);
			// get all cycles in selected year
			DataTable dtbCycles = GetCycles(pstrCCNID);
			// refine cycles as of date based on production line
			dtbCycles = RefineCycle(pstrProductionLineID, dtbPlanningOffset, dtbCycles);
			ArrayList arrPlanningPeriod = GetPlanningPeriod(pstrCCNID);
			StringBuilder sbCycleIDs;
			DataTable dtbCyclesCurrentMonth = ArrangeCycles(dtmFromDate, DateTime.MinValue, dtbCycles, arrPlanningPeriod, out sbCycleIDs);
			
			// build the string item list
			StringBuilder strItems = new StringBuilder();
			foreach (DataRow drowItem in dtbItems.Rows)
				strItems.Append(drowItem["ProductID"].ToString()).Append(",");
			strItems.Append("0");
			// begin stock data
			DataTable dtbBeginStockData = GetBeginStockData(strItems.ToString(), dtmFromDate);
			DataTable dtbDeliveryForParent = GetDeliveryForParent(sbCycleIDs.ToString(), strItems.ToString(), dtmFromDate, dtmToDate);
			// get first valid work day of current month
			DateTime dtmFirstValidDay = GetFirtValidWorkday(dtbValidWorkDay, dtbCyclesCurrentMonth);
			// refine the delivery date
			foreach (DataRow drowData in dtbDeliveryForParent.Rows)
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
			
			DateTime dtmShiftFromDate = dtmFromDate;
			DateTime dtmShiftToDate = dtmToDate;
			GetStartAndEndTime(dtmFromDate, ref dtmShiftFromDate, ref dtmTempDate, dtbWorkingTime);
			GetStartAndEndTime(dtmToDate, ref dtmTempDate, ref dtmShiftToDate, dtbWorkingTime);
			DataTable dtbSO = GetDeliveryForSO(strItems.ToString(), dtmShiftFromDate, dtmShiftToDate);
			GetStartAndEndTime(dtmFromDatePreMonth, ref dtmShiftFromDate, ref dtmTempDate, dtbWorkingTime);
			GetStartAndEndTime(dtmToDatePreMonth, ref dtmTempDate, ref dtmShiftToDate, dtbWorkingTime);
			DataTable dtbProduce = GetProduce(sbCycleIDs.ToString(), intProductionLineID, strItems.ToString(), dtmFromDate, dtmToDate);
			DataTable dtbShifts = GetShifts(intProductionLineID);
			DataTable dtbStandardCapacity = GetStandardCapacity(intProductionLineID, intCCNID);
			DataTable dtbTRC = GetTotalRequiredCapacity(intProductionLineID, sbCycleIDs.ToString(), intProductID, dtmFromDate, dtmToDate);
			
			#endregion

			#region Fill Data To Table

			foreach (DataRow drowItem in dtbItems.Rows)
			{
				#region defined variable

				bool blnHasDelivery = false;
				bool blnHasProduce = false;
				string strProductID = drowItem["ProductID"].ToString();
				decimal decBeginQuantity = 0, decTotalDelivery = 0, decTotalProduce = 0;
				decimal decStockLastDay = 0;
				
				#endregion

				#region general information

				DataRow drowData = dtbReportData.NewRow();
				drowData["ProductID"] = strProductID;
				drowData["Category"] = drowItem["Category"];
				drowData["CategoryID"] = drowItem["CategoryID"];
				drowData["PartNumber"] = drowItem["PartNumber"];
				drowData["PartName"] = drowItem["PartName"];
				drowData["Model"] = drowItem["Model"];

				#endregion

				#region Begin Quantity

				try
				{
					decBeginQuantity = Convert.ToDecimal(dtbBeginStockData.Compute(
						" SUM(Quantity)", "ProductID = " + strProductID
						+ " AND EffectDate = '" + dtmFromDate.Date.ToString() + "'"));
				}
				catch{}

				drowData["D00"] = decStockLastDay = decBeginQuantity;

				#endregion

				#region fill data for each day in month

				for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
				{
					#region variables

					string strExpression = "BeginDate <= '" + dtmDay.ToString("G") + "'"
						+ " AND EndDate >='" + dtmDay.ToString("G") + "'";
					DataRow[] drowValidWorkDay = dtbValidWorkDay.Select(strExpression);
					DateTime dtmNextDay = dtmDay.AddDays(1);
					DateTime dtmStartNextDay = dtmDay;
					DateTime dtmEndNextDay = dtmDay;
					GetStartAndEndTime(dtmNextDay, ref dtmStartNextDay, ref dtmEndNextDay, dtbWorkingTime);
					// cycle of current day
					string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
					decimal decDelivery = 0, decDeliverySO = 0;
					decimal decProduce = 0;

					string strPCol = "P" + dtmDay.Day.ToString("00");
					string strDCol = "D" + dtmDay.Day.ToString("00");
					string strSCol = "S" + dtmDay.Day.ToString("00");
					
					#endregion

					// ignore offday
					if (drowValidWorkDay.Length != 0)
					{
						// shift of day with pattern
						DataRow[] drowShifts = GetShiftsOfDay(dtmDay, dtbShifts);

						#region fill data for each shift

						for (int j = 0; j < drowShifts.Length; j++)
						{
							#region variables

							string strShiftID = drowShifts[j]["ShiftID"].ToString();
							decimal decProduceShift = 0, decDeliveryShift = 0;
							string strShiftProduceCol = "S" + (j+1).ToString() + "PD" + dtmDay.Day.ToString("00");
							string strShiftDeliveryCol = "S" + (j+1).ToString() + "D" + dtmDay.Day.ToString("00");
							DateTime dtmWorkTimeFrom = Convert.ToDateTime(drowShifts[j]["WorkTimeFrom"]);
							DateTime dtmWorkTimeTo = Convert.ToDateTime(drowShifts[j]["WorkTimeTo"]);

							DateTime dtmStartTime = new DateTime(dtmDay.Year, dtmDay.Month, dtmDay.Day, dtmWorkTimeFrom.Hour, dtmWorkTimeFrom.Minute, dtmWorkTimeFrom.Second);
							DateTime dtmEndTime = new DateTime(dtmDay.Year, dtmDay.Month, dtmDay.Day, dtmWorkTimeTo.Hour, dtmWorkTimeTo.Minute, dtmWorkTimeTo.Second);

							#endregion

							#region expression

							string strFilter = "ProductID = " + strProductID + " AND"
								+ " WorkingDate ='" + dtmDay.ToString() + "'"
								+ " AND DCOptionMasterID = " + strCycleID
								+ " AND ShiftID = " + strShiftID;
							string strFilterSO = "ProductID = '" + strProductID + "' AND"
								+ " ScheduleDate >='" + dtmStartTime.ToString("G") + "'";

							string strFilterForParent = "ProductID = " + strProductID + " AND"
								+ " StartTime >='" + dtmStartTime.ToString("G") + "' AND"
								+ " DCOptionMasterID = '" + strCycleID + "'";
							if (j == drowShifts.Length-1)
							{
								strFilterForParent += " AND StartTime <'" + dtmStartNextDay.ToString("G") + "'";
								strFilterSO += " AND ScheduleDate <'" + dtmStartNextDay.ToString("G") + "'";
							}
							else
							{
								strFilterForParent += " AND StartTime <'" + dtmEndTime.ToString("G") + "'";
								strFilterSO += " AND ScheduleDate <'" + dtmEndTime.ToString("G") + "'";
							}

							#endregion

							#region Produce

							try
							{
								decProduceShift += Convert.ToDecimal(dtbProduce.Compute("SUM(Quantity)", strFilter));
							}
							catch{}
							decProduce += decProduceShift;
							decTotalProduce += decProduceShift;

							if (decProduceShift > 0)
							{
								drowData[strShiftProduceCol] = decimal.Round(decProduceShift, 0);
								blnHasProduce = true;
							}
							
							#endregion

							#region Delivery For Parent
							try
							{
								decDeliveryShift += decimal.Floor(Convert.ToDecimal(dtbDeliveryForParent.Compute(("SUM(Quantity)"), strFilterForParent)));
							}
							catch{}
							#endregion

							#region Delivery For SO
							try
							{
								decDeliveryShift += Convert.ToDecimal(dtbSO.Compute("SUM(Quantity)", strFilterSO));
							}
							catch{}
							decTotalDelivery += decDeliveryShift;
							decDeliverySO += decDeliveryShift;
							if (decDeliveryShift > 0)
							{
								drowData[strShiftDeliveryCol] = decimal.Round(decDeliveryShift, 0);
								blnHasDelivery = true;
							}
					
							#endregion
						}

						#endregion
					}
					else
					{
						DateTime dtmStartTime = dtmDay, dtmEndTime = dtmDay;
						GetStartAndEndTime(dtmDay, ref dtmStartTime, ref dtmEndTime, dtbWorkingTime);
						string strFilterSO = "ProductID = " + strProductID
							+ " AND ScheduleDate >= '" + dtmStartTime.ToString("G") + "'"
							+ " AND ScheduleDate < '" + dtmEndTime.ToString("G") + "'";
						#region Delivery For SO
						try
						{
							decDeliverySO += Convert.ToDecimal(dtbSO.Compute("SUM(Quantity)", strFilterSO));
						}
						catch{}
						decTotalDelivery += decDeliverySO;
					
						#endregion
					}

					if (decProduce > 0)
						drowData[strPCol] = decimal.Round(decProduce, 0);

					if (decDelivery + decDeliverySO > 0)
						drowData[strDCol] = decimal.Round(decDelivery + decDeliverySO, 0);

					decStockLastDay = decStockLastDay + decProduce - (decDelivery + decDeliverySO);
					drowData[strSCol] = decStockLastDay;
				}

				#region total field
				drowData["Total"] = decTotalDelivery;
				drowData["PTotal"] = decTotalProduce;
				drowData["STotal"] = decStockLastDay;
				#endregion

				#endregion

				if (blnHasDelivery || blnHasProduce)
					dtbReportData.Rows.Add(drowData);
			}

			#endregion

			#region Number of Produce product for each day

			for (int i = 1; i <= intDaysInMonth; i++)
			{
				string strCategories = "fldCategoriesOnDayD" + i.ToString("00");
				string strDelivery = "fldSum1D" + i.ToString("00");
				string strProduce = "fldSum2D" + i.ToString("00");
				string strStock = "fldSum3D" + i.ToString("00");
				int intNumOfCategory = 0;
				decimal decDayDelivery = 0, decDayProduce = 0, decDayStock =0;
				foreach (DataRow drowData in dtbReportData.Rows)
				{
					decimal decDelivery = 0, decProduce = 0, decStock = 0;
					try
					{
						decDelivery = decimal.Parse(drowData["D" + i.ToString("00")].ToString());
					}
					catch{}
					decDayDelivery += decDelivery;
					try
					{
						decProduce = decimal.Parse(drowData["P" + i.ToString("00")].ToString());
					}
					catch{}
					decDayProduce += decProduce;
					// if current day has produce quantity, get the category
					if (decProduce > decimal.Zero)
					{
						// if current product has category, increase by one
						if (drowData["CategoryID"] != DBNull.Value)
							intNumOfCategory++;
					}
					try
					{
						decStock = decimal.Parse(drowData["S" + i.ToString("00")].ToString());
					}
					catch{}
					decDayStock += decStock;
				}
				// total delivery of day
				rptReport.Fields[strDelivery].Text = decDayDelivery.ToString();
				// total produce of day
				rptReport.Fields[strProduce].Text = decDayProduce.ToString();
				// total stock of day
				rptReport.Fields[strStock].Text = decDayStock.ToString();
				// produce item/day
				rptReport.Fields[strCategories].Text = intNumOfCategory.ToString("00");

			}
			try
			{
				rptReport.Fields["fldParameterCategory"].Text	= intNumOfWorkday.ToString("00");
			}
			catch{}
			try
			{
				rptReport.Fields["fldParameterPartNumber"].Text = intNumOfOffdays.ToString("00");
			}
			catch{}

			#endregion

			#region Shift, Standard Capacity, Total Required Capacity, Remain Capacity, Effective
			decimal decTotalSC = 0;
			decimal decTotalRC = 0;
			decimal decTotalRemain = 0;
			decimal decTotalEffect = 0;

			#region for each day in month

			for (int i = 1; i <= intDaysInMonth; i++)
			{
				string strShiflField = "fldShiftD" + i.ToString("00");
				string strSCField = "fldStandardCapacityD" + i.ToString("00");
				string strTRCField = "fldTotalRequiredCapacityD" + i.ToString("00");
				string strEffective = "fldEffectiveD" + i.ToString("00");
				string strRemain = "fldRemainCapacityD" + i.ToString("00");
				DateTime dtmDay = new DateTime(intYear, intMonth, i);
				string strCycleID = GetCycleOfDate(dtmDay, dtbCyclesCurrentMonth);
				string strExpression = "WorkingDate = '" + dtmDay.ToString() + "'"
					+ " AND DCOptionMasterID = '" + strCycleID + "'";
				string strFilter = "BeginDate <='" + dtmDay.ToString() + "' AND"
					+ " EndDate >='" + dtmDay.ToString() + "'";
				string strShift = "Off day";
				decimal decStandard = 0;
				decimal decTotalRequired = 0;
				decimal decRemain = 0;
				decimal decEffective = 0;
				DataRow[] drowValidWorkDay = dtbValidWorkDay.Select(strFilter);
				if (drowValidWorkDay.Length > 0)
				{
					if (dtbShifts != null)
					{
						DataRow[] drowShifts = dtbShifts.Select(strFilter);
						if (drowShifts.Length > 0)
						{
							strShift = string.Empty;
							foreach (DataRow drowShift in drowShifts)
								strShift += drowShift["ShiftDesc"].ToString().Trim() + ",";
							// remove the last comma
							if (strShift.Length > 0 && strShift.EndsWith(","))
								strShift = strShift.Substring(0, strShift.Length - 1);
						}
					}
					strShift = MappingShift(strShift);
					DataRow[] drowStandards = dtbStandardCapacity.Select(strFilter);
					foreach (DataRow drowData in drowStandards)
					{
						try
						{
							decStandard += (decimal)drowData["Capacity"];
						}
						catch{}
					}
					decTotalSC += decStandard;
					DataRow[] drowTotalRequired = dtbTRC.Select(strExpression);
					foreach (DataRow drowData in drowTotalRequired)
					{
						try
						{
							decTotalRequired += (decimal)drowData["TotalSecond"];
						}
						catch{}
					}
					decTotalRC += decTotalRequired;

					// remain capacity
					decRemain = decStandard - decTotalRequired;
					// calculate effective
					try
					{
						decEffective = decimal.Round((decTotalRequired / decStandard) * 100, 0);
					}
					catch{}
				}
				try
				{
					rptReport.Fields[strShiflField].Text = strShift;
				}
				catch{}
				try
				{
					rptReport.Fields[strSCField].Text = Convert.ToString(decStandard);
				}
				catch{}
				try
				{
					rptReport.Fields[strTRCField].Text = Convert.ToString(decTotalRequired);
				}
				catch{}
				try
				{
					rptReport.Fields[strRemain].Text = Convert.ToString(decRemain);
				}
				catch{}
				try
				{
					rptReport.Fields[strEffective].Text = Convert.ToString(decEffective) + "%";
				}
				catch{}
			}

			#endregion

			// total
			decTotalRemain = decTotalSC - decTotalRC;
			try
			{
				decTotalEffect = decimal.Round((decTotalRC / decTotalSC)*100, 0);
			}
			catch{}
			try
			{
				rptReport.Fields["fldStandardCapacityTotal"].Text = Convert.ToString(decTotalSC);
				rptReport.Fields["fldTotalRequiredCapacityTotal"].Text = Convert.ToString(decTotalRC);
				rptReport.Fields["fldRemainCapacityTotal"].Text = Convert.ToString(decTotalRemain);
				rptReport.Fields["fldEffectiveTotal"].Text = Convert.ToString(decTotalEffect) + "%";
			}
			catch{}

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
				rptReport.Fields["fldParameterWorkCenter"].Text = GetProCodeAndName(pstrProductionLineID);
			}
			catch{}
			try
			{
				rptReport.Fields["fldVersion"].Text = pstrVersion;
			}
			catch{}
			
			#endregion

			// set datasource object that provides data to report.
			rptReport.DataSource.Recordset = dtbReportData;
			// render report
			rptReport.Render();

			// render the report into the PrintPreviewControl
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Production plan and delivery " + strMonth + "-" + intYear.ToString();
			
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
		private DataTable GetProducts(int pintCategoryID, int pintCCNID, int pintProductionLineID, int pintProductID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT DISTINCT ITM_Product.ProductID, ITM_Product.Code AS PartNumber, ITM_Product.Description AS PartName,"
					+ " ITM_Product.Revision AS Model, ITM_Category.CategoryID, ITM_Category.Code AS Category"
					+ " FROM ITM_Product "
					+ " JOIN PRO_ProductionLine ON ITM_Product.ProductionLineID = PRO_ProductionLine.ProductionLineID "
					+ " LEFT JOIN ITM_Category"
					+ " ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " WHERE ITM_Product.CCNID = " + pintCCNID
					+ " AND ITM_Product.MakeItem = 1"
					+ " AND ITM_Product.ProductionLineID = " + pintProductionLineID;
				if (pintCategoryID > 0)
					strSql += " AND ITM_Product.CategoryID = " + pintCategoryID;
				if (pintProductID > 0)
					strSql += " AND ITM_Product.ProductID = " + pintProductID;
				strSql += " ORDER BY ITM_Category.Code ASC, ITM_Product.Code ASC, ITM_Product.Description ASC, ITM_Product.Model ASC";
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
					+ " ITM_BOM.ComponentID AS ProductID, PRO_DCPResultMaster.WorkCenterID, ITM_BOM.LeadTimeOffset, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultDetail.ShiftID, PRO_Shift.ShiftDesc"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
					+ " LEFT JOIN PRO_Shift ON PRO_DCPResultDetail.ShiftID = PRO_Shift.ShiftID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID IN (" + pstrOptionID + ")"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND ITM_BOM.ComponentID IN (" + strItems + ")";
				strSql += " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ITM_BOM.ComponentID, LeadTimeOffset,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultDetail.ShiftID, PRO_Shift.ShiftDesc"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, ITM_BOM.ComponentID, WorkingDate";
				
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
				if (pstrOptionID.Length == 0)
					pstrOptionID = "0";
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT DISTINCT SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)) AS Quantity,"
					+ " PRO_DCPResultMaster.ProductID, PRO_DCPResultMaster.WorkCenterID, WorkingDate, PRO_DCPResultMaster.DCOptionMasterID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultDetail.ShiftID, PRO_Shift.ShiftDesc"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN PRO_Shift ON PRO_DCPResultDetail.ShiftID = PRO_Shift.ShiftID"
					+ " WHERE PRO_DCPResultMaster.DCOptionMasterID IN (" + pstrOptionID + ")"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND MST_WorkCenter.IsMain = 1"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (strItems.Trim().Length > 0)
					strSql += " AND PRO_DCPResultMaster.ProductID IN (" + strItems + ")";
				strSql += " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " PRO_DCPResultDetail.ShiftID, PRO_Shift.ShiftDesc"
					+ " ORDER BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultMaster.ProductID, WorkingDate";

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

		private DataTable GetShifts(int pintProductionLineID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT PRO_Shift.ShiftID, ShiftDesc, PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate,"
					+ " WorkTimeFrom, WorkTimeTo"
					+ " FROM PRO_Shift JOIN PRO_ShiftCapacity"
					+ " ON PRO_Shift.ShiftID = PRO_ShiftCapacity.ShiftID"
					+ " JOIN PRO_ShiftPattern ON PRO_Shift.ShiftID = PRO_ShiftPattern.ShiftID"
					+ " JOIN PRO_WCCapacity"
					+ " ON PRO_ShiftCapacity.WCCapacityID = PRO_WCCapacity.WCCapacityID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND MST_WorkCenter.ProductionLineID = " + pintProductionLineID;
				DataTable dtbShift = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(strSql, oconPCS);
				odadPCS.Fill(dtbShift);
				return dtbShift;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetStandardCapacity(int pintProductionLineID, int pintCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT ISNULL(SUM(ISNULL(PRO_WCCapacity.Capacity, 0)), 0) AS 'Capacity',"
					+ " PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate"
					+ " FROM PRO_WCCapacity JOIN MST_WorkCenter"
					+ " ON PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " LEFT JOIN PRO_ProductionLine"
					+ " ON MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND PRO_ProductionLine.ProductionLineID = " + pintProductionLineID
					//					+ " AND PRO_WCCapacity.BeginDate >= ?"
					//					+ " AND PRO_WCCapacity.EndDate <= ?"
					+ " AND PRO_WCCapacity.CCNID = " + pintCCNID
					+ " GROUP BY PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate";
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				//				ocmdPCS.Parameters.Add(new OleDbParameter("BeginDate", OleDbType.Date)).Value = pdtmFromDate;
				//				ocmdPCS.Parameters.Add(new OleDbParameter("EndDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Connection.Open();
				DataTable dtbShift = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbShift);
				return dtbShift;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		private DataTable GetTotalRequiredCapacity(int pintProductionLineID, string pstrOptionIDs, int pintProductID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT SUM(ISNULL(TotalSecond, 0)) AS TotalSecond, WorkingDate, DCOptionMasterID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID"
					+ " JOIN MST_WorkCenter"
					+ " ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.ProductionLineID = " + pintProductionLineID
					+ " AND DCOptionMasterID IN (" + pstrOptionIDs + ")"
					+ " AND IsMain = 1"
					+ " AND WorkingDate >= ? AND WorkingDate <= ?";
				if (pintProductID > 0)
					strSql += " AND ProductID = " + pintProductID;
				strSql += " GROUP BY DCOptionMasterID, WorkingDate";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
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
		private DataRow[] GetShiftsOfDay(DateTime pdtmDate, DataTable pdtbShifts)
		{
			return pdtbShifts.Select("BeginDate <= '" + pdtmDate.ToString("G")
				+ "' AND EndDate >= '" + pdtmDate.ToString("G") + "'",
				" WorkTimeFrom ASC");
		}
		/// <summary>
		/// Mapping shift pattern to display
		/// </summary>
		private string MappingShift(string pstrShift)
		{
			if ((pstrShift.ToUpper().IndexOf("1S") >= 0) &&
				(pstrShift.ToUpper().IndexOf("2S") >= 0) &&
				(pstrShift.ToUpper().IndexOf("3S") >= 0))
			{
				return "3S-Full";
			}
			else if ((pstrShift.ToUpper().IndexOf("1S") >= 0) &&
				(pstrShift.ToUpper().IndexOf("C") >= 0) &&
				(pstrShift.ToUpper().IndexOf("2S") >= 0) &&
				(pstrShift.Length == "1S,A,2S,A".Length))
			{
				return "2S-C";
			}
			else if ((pstrShift.ToUpper().IndexOf("1S") >= 0) &&
				(pstrShift.ToUpper().IndexOf("B") >= 0)&&
				(pstrShift.ToUpper().IndexOf("2S") >= 0) &&
				(pstrShift.Length == "1S,B,2S".Length))
			{
				return "2S-B";
			}
			else if ((pstrShift.ToUpper().IndexOf("1S") >= 0) &&
				(pstrShift.ToUpper().IndexOf("2S") >= 0) &&
				(pstrShift.Length == "1S,2S".Length))
			{
				return "2S-A";
			}
			else if ((pstrShift.ToUpper().IndexOf("1S") >= 0) &&
				(pstrShift.ToUpper().IndexOf("A") >= 0) &&
				(pstrShift.Length == "1S,A".Length))
			{
				return "1S-A";
			}
			else
				return pstrShift;
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
		/// <summary>
		/// Put the DateTime need to Resolve in
		/// Reference the WorkingTime table
		/// if the ResolveTime is in the working time of any shift, of a configured period, determine the real WorkingDay, and return.
		/// </summary>
		/// <param name="pdtmNeedToResolve">Date to find</param>
		/// <param name="pdtbWorkingTime">Working Time</param>
		/// <returns>Working Day</returns>
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
		/// <summary>
		/// Convert working day with offline method
		/// </summary>
		/// <param name="pdtmDate">Date to convert</param>
		/// <param name="pdecNumberOfDay">Number of day to add/subtract</param>
		/// <returns>Converted Date</returns>
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
		private DataTable GetBeginStockData(string pstrItems, DateTime dtmEffectDate)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql=	"SELECT * FROM IV_BeginDCPReport WHERE ProductID IN (" + pstrItems + ")"
					+ " AND EffectDate = ?";
				OleDbCommand cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("EffectDate", OleDbType.Date)).Value = dtmEffectDate;
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
	}
}

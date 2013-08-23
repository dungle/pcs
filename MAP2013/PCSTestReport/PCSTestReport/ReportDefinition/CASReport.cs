using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Preview;
using C1.C1Report;
using Microsoft.Office.Interop.Excel;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;
using DataTable = System.Data.DataTable;

namespace CASReport
{
	[Serializable]
	public class CASReport : MarshalByRefObject, IDynamicReport
	{
		private ReportBuilder mReportBuilder;
		private string mConnectionString;
		private bool mUseReportViewerRenderEngine = false;
		private string mReportFolder = string.Empty;

		private object mResult;
		private C1PrintPreviewControl mPreview;

		#region IDynamicReport Members

		public ReportBuilder PCSReportBuilder
		{
			get { return this.mReportBuilder; }
			set { mReportBuilder = value; }
		}

		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}

		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseReportViewerRenderEngine; }
			set { mUseReportViewerRenderEngine = value; }
		}

		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mReportFolder; }
			set { mReportFolder = value; }
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
			get { return mLayoutFile; }
			set { mLayoutFile = value; }
		}


		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}

		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mPreview; }
			set { mPreview = value; }
		}

		#endregion

		/// <summary>
		/// Execute Report
		/// </summary>
		/// <param name="pstrCCNID">CCN</param>
		/// <param name="pstrYear">Year</param>
		/// <param name="pstrMonth">Month</param>
		/// <param name="pstrProLineID">Production Line</param>
		/// <param name="pstrWorkCenterID">Work Center</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProLineID, string pstrWorkCenterID)
		{
			const string WORKING_DATE = "WorkingDate";
			const string BEGIN_DATE = "BeginDate";
			const string END_DATE = "EndDate";
			const double FIELD_WIDTH = 585;
			const string FLD = "fld";
			int intCCNID = 0;
			int intYear = 0;
			int intMonth = 0;
			int intProductionLineID = 0;
			int intWorkCenterID = 0;
			try
			{
				intCCNID = int.Parse(pstrCCNID);
			}
			catch{}
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
			try
			{
				intProductionLineID = int.Parse(pstrProLineID);
			}
			catch{}
			try
			{
				intWorkCenterID = int.Parse(pstrWorkCenterID);
			}
			catch{}
			DateTime dtmStartDate = new DateTime(intYear, intMonth, 1);
			DateTime dtmEndDate = dtmStartDate.AddMonths(1).AddDays(-1);
			string strExpression = string.Empty;
			Hashtable arrStandardCapacity = new Hashtable();
			Hashtable arrActual = new Hashtable();
			Hashtable arrRemain = new Hashtable();
			Hashtable arrEffective = new Hashtable();
			C1Report rptReport = new C1Report();
			string strMonth = dtmStartDate.ToString("MMM");
			// planning offset
			DataTable dtbPlanningOffset = GetPlanningOffset(pstrCCNID);
			// get all cycles in selected year
			DataTable dtbCycles = GetCycles(pstrCCNID);
			// refine cycles as of date based on production line
			dtbCycles = RefineCycle(pstrProLineID, dtbPlanningOffset, dtbCycles);
			// all planning period
			ArrayList arrPlanningPeriod = GetPlanningPeriod(pstrCCNID);
			StringBuilder sbCycleIDs;
			DataTable dtbCyclesCurrentMonth = ArrangeCycles(dtmStartDate, dtmEndDate, dtbCycles, arrPlanningPeriod, out sbCycleIDs);

			DataTable dtbStandard = GetStandardCapacity(intWorkCenterID, intCCNID, intProductionLineID);
			DataTable dtbTRC = GetTotalRequiredCapacity(intProductionLineID, sbCycleIDs.ToString(), dtmStartDate, dtmEndDate);
			DataTable dtbValidWorkDay = GetWorkingDateFromWCCapacity(intProductionLineID);
			decimal decTotalStandard = 0;
			decimal decTotalActual = 0;
			decimal decTotalRemain = 0;
			decimal decTotalEffective = 0;
			DataRow[] drowStandard = null;
			for (int i = dtmStartDate.Day; i <= dtmEndDate.Day; i++)
			{
				DateTime dtmDate = new DateTime(dtmStartDate.Year, dtmStartDate.Month, i);
				string strColName = "D" + i.ToString("00");
				decimal decSC = 0;
				decimal decActual = 0;
				decimal decRemain = 0;
				decimal decEffective = 0;
				strExpression = BEGIN_DATE + "<='" + dtmDate.ToString("G")
					+ "' AND " + END_DATE + ">='" + dtmDate.ToString("G") + "'";
				DataRow[] drowValidWorkDay = dtbValidWorkDay.Select(strExpression);
				if (drowValidWorkDay.Length == 0)
				{
					arrStandardCapacity.Add(strColName, decSC);
					arrActual.Add(strColName, decActual);
					arrRemain.Add(strColName, decRemain);
					arrEffective.Add(strColName, decimal.Round(decEffective, 2));
					continue;
				}

				string strCycleID = GetCycleOfDate(dtmDate, dtbCyclesCurrentMonth);
				string strFilter = "WorkingDate = '" + dtmDate.ToString() + "'"
					+ " AND DCOptionMasterID = '" + strCycleID + "'";

				#region Standard Capacity

				drowStandard = dtbStandard.Select(strExpression);
				foreach (DataRow drowData in drowStandard)
				{
					try
					{
						decSC += (decimal) drowData["Capacity"];
					}
					catch
					{
					}
				}
				arrStandardCapacity.Add(strColName, decimal.Round(decSC, 0));
				decTotalStandard += decSC;

				#endregion

				#region Total Required Capacity

				DataRow[] drowTotalRequired = dtbTRC.Select(strFilter);
				foreach (DataRow drowData in drowTotalRequired)
				{
					try
					{
						decActual += (decimal)drowData["TotalSecond"];
					}
					catch{}
				}
				arrActual.Add(strColName, decimal.Round(decActual, 0));
				decTotalActual += decActual;

				#endregion

				#region Effective = Required Cap / Standard Cap

				try
				{
					decEffective = decActual / decSC;
				}
				catch{}
				arrEffective.Add(strColName, decimal.Round(decEffective, 2));
				
				#endregion

				#region Remain Capacity

				// remain capacity
				decRemain = decSC - decActual;
				arrRemain.Add(strColName, decimal.Round(decRemain, 0));

				#endregion

			}
			arrStandardCapacity.Add("Total", decimal.Round(decTotalStandard, 0));
			arrActual.Add("Total", decimal.Round(decTotalActual, 0));
			decTotalRemain = decTotalStandard - decTotalActual;
			arrRemain.Add("Total", decimal.Round(decTotalRemain, 0));
			try
			{
				decTotalEffective = decTotalActual / decTotalStandard;
			}
			catch{}
			arrEffective.Add("Total", decimal.Round(decTotalEffective, 2));


			/// column Name in the dtbResult
			const string STANDARD_CAPACITY = "StandardCapacity";
			const string TOTAL_REQUIRED_CAPACITY = "TotalRequiredCapacity";
			const string REMAIN_CAPACITY = "RemainCapacity";
			const string EFFECTIVE = "Effective";


			DataTable dtbResult = new DataTable();
			dtbResult.Columns.Add(new DataColumn("RowType", typeof (string)));

			#region Report layout

			mLayoutFile = "CASReport.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile);
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			arrstrReportInDefinitionFile = null;
			rptReport.Layout.PaperSize = PaperKind.A3;

			#endregion

			for (int i = dtmStartDate.Day; i <= dtmEndDate.Day; i++)
			{
				string strColumnName = "D" + i.ToString("00");
				dtbResult.Columns.Add(new DataColumn(strColumnName, typeof (decimal)));

				#region Report layout

				DateTime dtmDay = new DateTime(intYear, intMonth, i);
				string strDate = "fldD" + i.ToString("00");
				string strDay = "fldDay" + i.ToString("00");
				try
				{
					rptReport.Fields[strDate].Text = i + "-" + strMonth;
				}
				catch
				{
				}
				try
				{
					rptReport.Fields[strDay].Text = dtmDay.DayOfWeek.ToString().Substring(0, 3);
				}
				catch
				{
				}
				DataRow[] drowValidWorkDay = dtbValidWorkDay.Select("BeginDate <= '" + dtmDay.ToString("G") + "'" + " AND EndDate >='" + dtmDay.ToString("G") + "'");
				if (drowValidWorkDay.Length == 0)
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
							rptReport.Fields[strDay].ForeColor = Color.Blue;
							rptReport.Fields[strDay].BackColor = Color.Yellow;
						}
						else
						{
							rptReport.Fields[strDay].ForeColor = Color.Red;
							rptReport.Fields[strDay].BackColor = Color.Yellow;
						}
					}
					catch
					{
					}
				}

				#endregion
			}

			#region Layout the format based on days in month

			int intDaysInMonth = DateTime.DaysInMonth(dtmStartDate.Year, dtmStartDate.Month);
			if (intDaysInMonth < 31)
			{
				for (int i = intDaysInMonth + 1; i <= 31; i++)
				{
					#region field name

					string strDate = "fldD" + i.ToString("00");
					string strDayOfWeek = "fldDay" + i.ToString("00");
					string strDiv = "div" + i.ToString("00");

					#endregion

					#region Report Header

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

					#endregion
				}
				try
				{
					#region Resize all line

					//double dWidth = rptReport.Fields["line1"].Width;
					for (int i = 1; i <= 7; i++)
						rptReport.Fields["line" + i].Width = rptReport.Fields["line" + i].Width - (31 - intDaysInMonth)*FIELD_WIDTH;

					#endregion

					double dWidthToChange = (31 - intDaysInMonth)*FIELD_WIDTH;

					#region Total columns

					rptReport.Fields["fldDTotal"].Left = 
						rptReport.Fields["fldStandardCapacityD"].Left = 
						rptReport.Fields["fldTotalRequiredCapacityD"].Left =
						rptReport.Fields["fldEffectiveD"].Left =
						rptReport.Fields["fldRemainCapacityD"].Left = rptReport.Fields["fldDTotal"].Left - dWidthToChange;
					rptReport.Fields["divTotal"].Left = rptReport.Fields["fldDTotal"].Left + FIELD_WIDTH;

					#endregion
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			#endregion

			DataRow drowSC = dtbResult.NewRow();
			drowSC["RowType"] = STANDARD_CAPACITY;
			DataRow drowTR = dtbResult.NewRow();
			drowTR["RowType"] = TOTAL_REQUIRED_CAPACITY;
			DataRow drowRC = dtbResult.NewRow();
			drowRC["RowType"] = REMAIN_CAPACITY;
			DataRow drowEff = dtbResult.NewRow();
			drowEff["RowType"] = EFFECTIVE;
			for (int i = dtmStartDate.Day; i <= dtmEndDate.Day; i++)
			{
				string strColumnName = "D" + i.ToString("00");
				drowSC[strColumnName] = arrStandardCapacity[strColumnName];
				drowTR[strColumnName] = arrActual[strColumnName];
				drowRC[strColumnName] = arrRemain[strColumnName];
				drowEff[strColumnName] = arrEffective[strColumnName];
			}
			dtbResult.Rows.Add(drowSC);
			dtbResult.Rows.Add(drowTR);
			dtbResult.Rows.Add(drowRC);
			dtbResult.Rows.Add(drowEff);

			#region RENDER REPORT

			const string REPORTFLD_CHART = "fldChart";
			const string REPORTFLD_TOTALCHART = "fldTotalChart";

			if (dtbResult.Rows.Count > 0)
			{
				#region BUILD CHART, save to image in clipboard, and then put in the report field fldChart

				Field fldChart = rptReport.Fields[REPORTFLD_CHART];
				Field fldTotalChart = rptReport.Fields[REPORTFLD_TOTALCHART];

				#region	INIT

				string EXCEL_FILE = "CASReport.xls";

				string strTemplateFilePath = mReportFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

				string strDestinationFilePath = mReportFolder + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(EXCEL_FILE) + FormControlComponents.NowToUTCString() + ".XLS";

				/// Copy layout excel report file to ExcelReport folder with a UTC datetime name
				File.Copy(strTemplateFilePath, strDestinationFilePath, true);

				ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);

				#endregion

				try
				{
					#region BUILD THE REPORT ON EXCEL FILE

					string[] arrExcelColumnHeading = new string[DateTime.DaysInMonth(intYear, intMonth)];
					for (int i = 1; i <= intDaysInMonth; i++)
					{
						DateTime dtmDate = new DateTime(intYear, intMonth, i);
						string strColHeading = i + "-" + strMonth + "\n" + dtmDate.DayOfWeek.ToString().Substring(0, 3);
						arrExcelColumnHeading[i - 1] = strColHeading;
					}

					double[,] arrExcelStandard = new double[1,intDaysInMonth];
					double[,] arrExcelActual = new double[1,intDaysInMonth];
					for (int i = dtmStartDate.Day; i <= dtmEndDate.Day; i++)
					{
						string strSC = "fldStandardCapacityD" + i.ToString("00");
						string strTR = "fldTotalRequiredCapacityD" + i.ToString("00");
						string strRC = "fldRemainCapacityD" + i.ToString("00");
						string strEff = "fldEffectiveD" + i.ToString("00");
						string strColName = "D" + i.ToString("00");
						
						rptReport.Fields[strSC].Text = arrStandardCapacity[strColName].ToString();
						rptReport.Fields[strTR].Text = arrActual[strColName].ToString();
						rptReport.Fields[strRC].Text = arrRemain[strColName].ToString();
						rptReport.Fields[strEff].Text = arrEffective[strColName].ToString();

						try
						{
							arrExcelStandard[0, i - 1] = Decimal.ToDouble((decimal) arrStandardCapacity[strColName]);
						}
						catch{}
						try
						{
							arrExcelActual[0, i - 1] = Decimal.ToDouble((decimal) arrActual[strColName]);
						}
						catch{}
					}
					// total field
					rptReport.Fields["fldStandardCapacityD"].Text = arrStandardCapacity["Total"].ToString();
					rptReport.Fields["fldTotalRequiredCapacityD"].Text = arrActual["Total"].ToString();
					rptReport.Fields["fldRemainCapacityD"].Text = arrRemain["Total"].ToString();
					rptReport.Fields["fldEffectiveD"].Text = arrEffective["Total"].ToString();

					switch (intDaysInMonth)
					{
						case 28:
							objXLS.GetRange("A1", "AB1").Value2 = arrExcelColumnHeading;
							objXLS.GetRange("A2", "AB2").Value2 = arrExcelStandard;
							objXLS.GetRange("A3", "AB3").Value2 = arrExcelActual;
							break;
						case 29:
							objXLS.GetRange("A1", "AC1").Value2 = arrExcelColumnHeading;
							objXLS.GetRange("A2", "AC2").Value2 = arrExcelStandard;
							objXLS.GetRange("A3", "AC3").Value2 = arrExcelActual;
							break;
						case 30:
							objXLS.GetRange("A1", "AD1").Value2 = arrExcelColumnHeading;
							objXLS.GetRange("A2", "AD2").Value2 = arrExcelStandard;
							objXLS.GetRange("A3", "AD3").Value2 = arrExcelActual;
							break;
						default:
							objXLS.GetRange("A1", "AE1").Value2 = arrExcelColumnHeading;
							objXLS.GetRange("A2", "AE2").Value2 = arrExcelStandard;
							objXLS.GetRange("A3", "AE3").Value2 = arrExcelActual;
							break;
					}

					ChartObject chart = objXLS.GetChart("DetailChart");
//					SeriesCollection serieSC = (SeriesCollection) chart.Chart.SeriesCollection(0);
					chart.Chart.CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap, XlPictureAppearance.xlScreen);
					Image image = (Image) Clipboard.GetDataObject().GetData(typeof (Bitmap));
					fldChart.Visible = true;
					fldChart.Text = "";
					fldChart.Picture = image;

					chart = objXLS.GetChart("Chart 12");
					chart.Chart.CopyPicture(XlPictureAppearance.xlScreen, XlCopyPictureFormat.xlBitmap, XlPictureAppearance.xlScreen);
					image = (Image) Clipboard.GetDataObject().GetData(typeof (Bitmap));
					fldTotalChart.Visible = true;
					fldTotalChart.Text = "";
					fldTotalChart.Picture = image;

					#endregion
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
				}
				finally
				{
					#region SAVE, CLOSE EXCEL FILE CONTAIN REPORT

					objXLS.CloseWorkbook();
					objXLS.Dispose();
					objXLS = null;

					#endregion
				}

				#endregion BUILD CHART		
			}

			#region MODIFY THE REPORT LAYOUT

			#region PUSH PARAMETER VALUE

			const string REPORTFLD_PARAMETER_CCN = "fldParameterCCN";
			const string REPORTFLD_PARAMETER_MONTH = "fldParameterMonth";
			const string REPORTFLD_PARAMETER_YEAR = "fldParameterYear";
			const string REPORTFLD_PARAMETER_PRODUCTIONLINE = "fldParameterProductionLine";
			const string REPORTFLD_PARAMETER_WORKCENTER = "fldParameterWorkCenter";
			string strCCN = GetCCNCode(intCCNID);
			rptReport.Fields[REPORTFLD_PARAMETER_CCN].Text = strCCN;
			rptReport.Fields[REPORTFLD_PARAMETER_MONTH].Text = pstrMonth;
			rptReport.Fields[REPORTFLD_PARAMETER_YEAR].Text = pstrYear;
			string strProductionLine = GetProCodeAndName(intProductionLineID);
			rptReport.Fields[REPORTFLD_PARAMETER_PRODUCTIONLINE].Text = strProductionLine;
			string strWorkCenter = GetWCCodeAndName(intWorkCenterID);
			rptReport.Fields[REPORTFLD_PARAMETER_WORKCENTER].Text = strWorkCenter;

			#endregion		

			#endregion

			rptReport.DataSource.Recordset = dtbResult;
			rptReport.Render();
			C1PrintPreviewDialog ppvViewer = new C1PrintPreviewDialog();
			ppvViewer.FormTitle = "Compare Actual And Standard Capacity";
			ppvViewer.ReportViewer.Document = rptReport.Document;
			ppvViewer.Show();

			#endregion

			return dtbResult;
		}

		/// <summary>
		/// Gets standard capacity of work center in a day
		/// </summary>
		/// <param name="pintWorkCenterID">Selected Work Center</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns>Standard Capacity</returns>
		private DataTable GetStandardCapacity(int pintWorkCenterID, int pintCCNID, int pintProductionLineID)
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
					+ " WHERE PRO_WCCapacity.WorkCenterID = " + pintWorkCenterID
					+ " AND ISNULL(MST_WorkCenter.IsMain, 0) = 1"
					+ " AND PRO_ProductionLine.ProductionLineID = " + pintProductionLineID
					+ " AND PRO_WCCapacity.CCNID = " + pintCCNID
					+ " GROUP BY PRO_WCCapacity.BeginDate, PRO_WCCapacity.EndDate";
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
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
		/// Gets total required capacity
		/// </summary>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <param name="pstrOptionIDs">Cycle Option go thru selected month</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>Total Required Capacity</returns>
		private DataTable GetTotalRequiredCapacity(int pintProductionLineID, string pstrOptionIDs, DateTime pdtmFromDate, DateTime pdtmToDate)
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
					+ " AND WorkingDate >= ? AND WorkingDate <= ?"
					+ " GROUP BY DCOptionMasterID, WorkingDate";
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

		/// <summary>
		/// Get CCN Code from ID
		/// </summary>
		/// <param name="pintCCNID">CCN ID</param>
		/// <returns>CCN Code</returns>
		private string GetCCNCode(int pintCCNID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT	Code FROM MST_CCN WHERE CCNID = " + pintCCNID;
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
		/// <param name="pintProID">Production Line ID</param>
		/// <returns>Pro Code (Pro Name)</returns>
		private string GetProCodeAndName(int pintProID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' FROM PRO_ProductionLine WHERE ProductionLineID = " + pintProID;
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
		/// Get Workcenter Code and Name from ID
		/// </summary>
		/// <param name="pintWorkCenterID">Work Center ID</param>
		/// <returns>Code (Name)</returns>
		private string GetWCCodeAndName(int pintWorkCenterID)
		{
			OleDbConnection oconPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT Code + ' (' + Name + ')' FROM MST_WorkCenter WHERE WorkCenterID = " + pintWorkCenterID;
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
		/// Gets working date of main work center from work center capactity
		/// </summary>
		/// <param name="pintProductionLineID">Production Line ID</param>
		/// <returns>DataTable</returns>
		private DataTable GetWorkingDateFromWCCapacity(int pintProductionLineID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			DataTable dtbData = new DataTable();
			try
			{
				string strSql = "SELECT BeginDate, EndDate"
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
		/// Gets all cycles of CCN
		/// </summary>
		/// <param name="pstrCCNID">CCN</param>
		/// <returns>All cycles</returns>
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
		/// Arrange cycles by as of date and planning period
		/// </summary>
		/// <param name="pdtmFromMonth">From Month</param>
		/// <param name="pdtmToMonth">To Month</param>
		/// <param name="pdtbCycles">All cycles</param>
		/// <param name="parrPlanningPeriod">All Planning Period</param>
		/// <param name="sbCycleIDs">out: cycle ids in range</param>
		/// <returns>Arranged cycles</returns>
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
		/// <summary>
		/// Gets all months appears in range of date
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Month</returns>
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
		/// <summary>
		/// Gets cycle of given date
		/// </summary>
		/// <param name="pdtmDate">Date</param>
		/// <param name="pdtbCycles">All cycles</param>
		/// <returns>Cycle ID</returns>
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
		/// Gets all planning period of CCN
		/// </summary>
		/// <param name="pstrCCNID">CCNID</param>
		/// <returns>List of Planning Period</returns>
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
				if (drowOffset.Length > 0)
				{
					DateTime dtmStartDate = (DateTime) drowOffset[0]["PlanningStartDate"];
					dtmStartDate = new DateTime(dtmStartDate.Year, dtmStartDate.Month, dtmStartDate.Day);
					drowData["FromDate"] = dtmStartDate;
				}
			}
			return pdtbCycles;
		}
	}
}

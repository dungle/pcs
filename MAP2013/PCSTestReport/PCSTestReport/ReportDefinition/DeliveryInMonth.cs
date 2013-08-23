using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace DeliveryInMonth
{
	public class DeliveryInMonth : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Members

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

		#endregion

		
		public DataTable ExecuteReport(string pstrYear, string pstrMonth, string pstrPartyID, string pstrMakerID, string pstrCategoryID, string pstrProductID)
		{
			const string DATE_FLD = "lblD";
			const string DAY_FLD = "lblDay";
			const double FIELD_WIDTH = 500;
			DataTable dtbReportData = GetReportData(pstrYear, pstrMonth, pstrPartyID, pstrMakerID, pstrProductID, pstrCategoryID);
			DateTime dtmFromDate = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddDays(-1);

			ArrayList arrOffDay = GetWorkingDayByYear(int.Parse(pstrYear));
			ArrayList arrHolidays = GetHolidaysInYear(int.Parse(pstrYear));

			#region Report Layout

			C1Report rptReport = new C1Report();
			if (mLayoutFile == null || mLayoutFile.Trim() == string.Empty)
				mLayoutFile = "DeliveryInMonth.xml";
			string[] arrstrReportInDefinitionFile = rptReport.GetReportInfo(mReportFolder + "\\" + mLayoutFile);
			rptReport.Load(mReportFolder + "\\" + mLayoutFile, arrstrReportInDefinitionFile[0]);
			rptReport.Layout.PaperSize = PaperKind.A3;

			for (DateTime dtmDay = dtmFromDate; dtmDay <= dtmToDate; dtmDay = dtmDay.AddDays(1))
			{
				string strDate = DATE_FLD + dtmDay.Day.ToString("00");
				string strDay = DAY_FLD + dtmDay.Day.ToString("00");
				try
				{
					rptReport.Fields[strDate].Text = dtmDay.ToString("dd-MMM");
				}
				catch{}
				try
				{
					rptReport.Fields[strDay].Text = dtmDay.DayOfWeek.ToString().Substring(0, 3);
				}
				catch{}

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
				}
			}

			#endregion

			#region refine the report based on days in month
			int intDaysInMonth = DateTime.DaysInMonth(dtmFromDate.Year, dtmFromDate.Month);
			if (intDaysInMonth < 31)
			{
				for (int i = intDaysInMonth + 1; i <= 31; i++)
				{
					#region hide fields

					string strDate = "lblD" + i.ToString("00");
					string strDayOfWeek = "lblDay" + i.ToString("00");
					string strDiv = "div" + i.ToString("00");
					string strDivDetail = "divData" + i.ToString("00");
					string strDetail = "fldD" + i.ToString("00");
					string strSum = "sum" + i.ToString("00");
					string strDivMaker = "divMaker" + i.ToString("00");
					string strSumVendor = "sumVendor" + i.ToString("00");
					string strDivVendor = "divVendor" + i.ToString("00");
					string strDivFtr = "divFtr" + i.ToString("00");
					string strFtr = "fldFtr" + i.ToString("00");
					try
					{
						rptReport.Fields[strSum].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strDivMaker].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strDate].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strDayOfWeek].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strDiv].Visible = false;
					}
					catch{}
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
					try
					{
						rptReport.Fields[strDivFtr].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strFtr].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strSumVendor].Visible = false;
					}
					catch{}
					try
					{
						rptReport.Fields[strDivVendor].Visible = false;
					}
					catch{}

					#endregion
				}
				try
				{
					#region resize and moving

					for (int i = 1; i < 8; i++)
						rptReport.Fields["line" + i].Width = rptReport.Fields["line" + i].Width - (31 - intDaysInMonth) * FIELD_WIDTH;
					double dLeft = rptReport.Fields["div" + dtmToDate.Day.ToString("00")].Left;
					// move the total field
					rptReport.Fields["lblTotal"].Left = dLeft;
					rptReport.Fields["divTotal"].Left = dLeft + rptReport.Fields["lblTotal"].Width;
					rptReport.Fields["fldTotal"].Left = dLeft;
					rptReport.Fields["divDataTotal"].Left = dLeft + rptReport.Fields["fldTotal"].Width;
					rptReport.Fields["sumTotal"].Left = dLeft;
					rptReport.Fields["divMakerTotal"].Left = dLeft + rptReport.Fields["sumTotal"].Width;
					rptReport.Fields["sumVendorTotal"].Left = dLeft;
					rptReport.Fields["divVendorTotal"].Left = dLeft + rptReport.Fields["sumVendorTotal"].Width;
					rptReport.Fields["fldFtrTotal"].Left = dLeft;
					rptReport.Fields["divFtrTotal"].Left = dLeft + rptReport.Fields["fldFtrTotal"].Width;

					#endregion
				}
				catch{}
			}
			#endregion

			rptReport.DataSource.Recordset = dtbReportData;

			C1PrintPreviewDialog printPreview = new C1PrintPreviewDialog();
			const string REPORTFLD_PARAMETER_MONTH = "fldMonth";
			const string REPORTFLD_PARAMETER_YEAR = "fldYear";
			const string TITLE_FLD = "fldTitle";
			try
			{
				rptReport.Fields[REPORTFLD_PARAMETER_MONTH].Text = dtmFromDate.ToString("MMM");
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

			rptReport.Render();
			printPreview.ReportViewer.Document = rptReport.Document;
			
			printPreview.Show();

			return dtbReportData;
		}

		private DataTable GetReportData(string pstrYear, string pstrMonth, string pstrPartyID, string pstrMakerID, string pstrProductID, string pstrCategoryID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT MakerID, ProductID,"
					+ "     SUM(CASE DayinMonth WHEN 1 THEN OrderQty ELSE NULL END) AS d01,"
					+ "     SUM(CASE DayinMonth WHEN 2 THEN OrderQty ELSE NULL END) AS d02,"
					+ "     SUM(CASE DayinMonth WHEN 3 THEN OrderQty ELSE NULL END) AS d03,"
					+ "     SUM(CASE DayinMonth WHEN 4 THEN OrderQty ELSE NULL END) AS d04,"
					+ "     SUM(CASE DayinMonth WHEN 5 THEN OrderQty ELSE NULL END) AS d05,"
					+ "     SUM(CASE DayinMonth WHEN 6 THEN OrderQty ELSE NULL END) AS d06,"
					+ "     SUM(CASE DayinMonth WHEN 7 THEN OrderQty ELSE NULL END) AS d07,"
					+ "     SUM(CASE DayinMonth WHEN 8 THEN OrderQty ELSE NULL END) AS d08,"
					+ "     SUM(CASE DayinMonth WHEN 9 THEN OrderQty ELSE NULL END) AS d09,"
					+ "     SUM(CASE DayinMonth WHEN 10 THEN OrderQty ELSE NULL END) AS d10,"
					+ "     SUM(CASE DayinMonth WHEN 11 THEN OrderQty ELSE NULL END) AS d11,"
					+ "     SUM(CASE DayinMonth WHEN 12 THEN OrderQty ELSE NULL END) AS d12,"
					+ "     SUM(CASE DayinMonth WHEN 13 THEN OrderQty ELSE NULL END) AS d13,"
					+ "     SUM(CASE DayinMonth WHEN 14 THEN OrderQty ELSE NULL END) AS d14,"
					+ "     SUM(CASE DayinMonth WHEN 15 THEN OrderQty ELSE NULL END) AS d15,"
					+ "     SUM(CASE DayinMonth WHEN 16 THEN OrderQty ELSE NULL END) AS d16,"
					+ "     SUM(CASE DayinMonth WHEN 17 THEN OrderQty ELSE NULL END) AS d17,"
					+ "     SUM(CASE DayinMonth WHEN 18 THEN OrderQty ELSE NULL END) AS d18,"
					+ "     SUM(CASE DayinMonth WHEN 19 THEN OrderQty ELSE NULL END) AS d19,"
					+ "     SUM(CASE DayinMonth WHEN 20 THEN OrderQty ELSE NULL END) AS d20,"
					+ "     SUM(CASE DayinMonth WHEN 21 THEN OrderQty ELSE NULL END) AS d21,"
					+ "     SUM(CASE DayinMonth WHEN 22 THEN OrderQty ELSE NULL END) AS d22,"
					+ "     SUM(CASE DayinMonth WHEN 23 THEN OrderQty ELSE NULL END) AS d23,"
					+ "     SUM(CASE DayinMonth WHEN 24 THEN OrderQty ELSE NULL END) AS d24,"
					+ "     SUM(CASE DayinMonth WHEN 25 THEN OrderQty ELSE NULL END) AS d25,"
					+ "     SUM(CASE DayinMonth WHEN 26 THEN OrderQty ELSE NULL END) AS d26,"
					+ "     SUM(CASE DayinMonth WHEN 27 THEN OrderQty ELSE NULL END) AS d27,"
					+ "     SUM(CASE DayinMonth WHEN 28 THEN OrderQty ELSE NULL END) AS d28,"
					+ "     SUM(CASE DayinMonth WHEN 29 THEN OrderQty ELSE NULL END) AS d29,"
					+ "     SUM(CASE DayinMonth WHEN 30 THEN OrderQty ELSE NULL END) AS d30,"
					+ "     SUM(CASE DayinMonth WHEN 31 THEN OrderQty ELSE NULL END) AS d31,"
					+ " PartyID,MakerCode,PartyCode,PartNo,PartName,Model,UM,Category"
					+ " FROM v_PODeliver_Maker"
					+ " WHERE CountryID NOT IN (Select CountryID FROM MST_CCN)"
					+ " AND month= " + pstrMonth
					+ " AND year= " + pstrYear;
				if (pstrMakerID != null && pstrMakerID.Length > 0)
					strSql += " AND MakerID in (" + pstrMakerID + ")";
				if (pstrPartyID != null && pstrPartyID.Length > 0)
					strSql += " AND PartyID in (" + pstrPartyID + ")";
				if (pstrProductID != null && pstrProductID.Length > 0)
					strSql += " AND ProductID in (" + pstrProductID + ")";
				if (pstrCategoryID != null && pstrCategoryID.Length > 0)
					strSql += " AND CategoryID in (" + pstrCategoryID + ")";
				strSql += " GROUP BY PartyID, MakerID, ProductID,MakerCode,PartyCode,PartNo,PartName,Model,UM,Category";
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
	}
}
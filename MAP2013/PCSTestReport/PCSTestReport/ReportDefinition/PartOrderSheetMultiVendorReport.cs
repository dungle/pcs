using System;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Data.OleDb;
using System.Text;
using C1.C1Report;
using PCSComUtils.Common;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace PartOrderSheetMultiVendorReport
{
	[Serializable]	
	public class PartOrderSheetMultiVendorReport : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Implementation
		private string _mConnectionString;
		private ReportBuilder _mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl _mReportViewer;
		private bool _mUseReportViewerRenderEngine = true;	

		private string _mstrReportDefinitionFolder = string.Empty;

		private object _mResult;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return _mConnectionString; }
			set { _mConnectionString = value; }
		}

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return _mReportBuilder; }
			set { _mReportBuilder = value; }
		}

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return _mReportViewer; }
			set { _mReportViewer = value; }
		}

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return _mResult; }
			set { _mResult = value; }
		}

		/// <summary>
		/// Notify PCS whether the rendering report process is run by
		/// this IDynamicReport 
		/// or the ReportViewer Engine (in the ReportViewer form) 
		/// </summary> 		
		public bool UseReportViewerRenderEngine { get { return _mUseReportViewerRenderEngine; } set { _mUseReportViewerRenderEngine = value; } }

		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get 
			{
				return _mstrReportDefinitionFolder;
			}
			set
			{
				_mstrReportDefinitionFolder = value;
			}
		}

        private string _reportLayoutFile = string.Empty;		
		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get 
			{
				return _reportLayoutFile;
			}
			set
			{
				_reportLayoutFile = value;
			}
		}
        
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{			
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}
        
		#endregion
        
		#region My variables
		
		const string DateFormat = "MMM-yy";

		public static string FormatReportNumber = "#,##0";

		const string PARTYID = "PartyID";
		const string PARTNO = "Part No.";
		const string PARTNAME = "Part Name";
		const string MODEL = "Model";
		const string UNITPRICE = "UnitPrice";
		const string SCHEDULE_DATE = "ScheduleDate";
		const string SUMROWNEXT_O = "SumRowNextO";
		const string SUMROWNEXTNEXT_O = "SumRowNextNextO";
		const string SUMROW_A = "SumRowA";
		const string SUMROWNEXT_A = "SumRowNextA";
		const string SUMROWNEXTNEXT_A = "SumRowNextNextA";
		const string REPORT_LAYOUT_FILE = "PartOrderSheetMultiVendorReport.xml";
		const string REPORT_NAME = "PartOrderSheetReport";
		const string COL_ORDER_PREFIX			= "D";
		const string COL_ADJUSTMENT_PREFIX		= "A";

		/// Result Data Table Column name
		const string VENDOR = "Vendor";
			
		#endregion		

		// STATIC SHARE VARIABLES FOR OTHER CLASS ACCESS
		public static int pnCCNID;
		public static string pstrYear = string.Empty;
		public static string pstrMonth = string.Empty;
		public static string pstrPartyID_LIST = string.Empty;
		public static string pstrProductID_LIST = string.Empty;
		public static DateTime CurrentMonth = DateTime.MinValue;
		public static DateTime NextMonth = DateTime.MinValue;
		public static DateTime Next2Month = DateTime.MinValue;

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report		

		/// ------------------------------ MAIN FLOW OF THIS REPORT ----------------------------------
		/// BUILD THE PO LIST TABLE
		///
		/// GETTING RAW DATA USING DataHelper Class
		/// FILL TO THE DTB TABLE
		/// 
		/// RENDER TO REPORT
		/// 
		/// SHOW THE REPORT			
		/// END -------------------------- MAIN FLOW OF THIS REPORT ----------------------------------
		/// </summary>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPPMDefault"></param>
		/// <param name="pstrPPMDefault">Base Item</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, 
			string pstrYear, string pstrMonth, string pstrPartyID_LIST, string pstrProductID_LIST)
		{
            // custom object to access and modify the dtbPurchaseOrderList
            PLReportDataHelper objPLTable = new PLReportDataHelper(_mConnectionString);
			pnCCNID = int.Parse(pstrCCNID);
			DateTime dtmMonth = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
			DateTime dtmNextMonth = dtmMonth.AddMonths(1);
			DateTime dtmNext2Month = dtmMonth.AddMonths(2);
			CurrentMonth = dtmMonth;
			NextMonth = dtmNextMonth;
			Next2Month = dtmNext2Month;
			PartOrderSheetMultiVendorReport.pstrYear = pstrYear;
			PartOrderSheetMultiVendorReport.pstrMonth = pstrMonth;
			PartOrderSheetMultiVendorReport.pstrPartyID_LIST = pstrPartyID_LIST;		
			PartOrderSheetMultiVendorReport.pstrProductID_LIST = pstrProductID_LIST;
			
	
			// ------------------------------ MAIN FLOW OF THIS REPORT ----------------------------------			
			// ##1## BUILD THE PRODUCTION LINE LIST TABLE
			#region BUILD some RAW DATA TABLE and Array		
			
			objPLTable.GetDataAndCache();
			DataTable dtbPOInMonth = objPLTable.GetPOInMonth(dtmMonth);
			DataTable dtbPOInNextMonth = objPLTable.GetPOInMonth(dtmNextMonth);
			DataTable dtbPOInNext2Month = objPLTable.GetPOInMonth(dtmNext2Month);

			// get the range of each verion in selected month
			DataTable dtbScheduleOrigin = objPLTable.GetDateRange(dtmMonth, pstrPartyID_LIST);
			DataTable dtbScheduleNextOrigin = objPLTable.GetDateRange(dtmNextMonth, pstrPartyID_LIST);
			DataTable dtbScheduleNextNextOrigin = objPLTable.GetDateRange(dtmNext2Month, pstrPartyID_LIST);
			
			#region PREPARE				
			DataTable dtbResult;
			DataTable dtbResultNext;
			DataTable dtbResultNextNext;
				
			// contain array of string: 01 02 03 .. of day of month in the dtbResult, except the missing day			
			ArrayList arrDueDateHeading = new ArrayList();	

			#endregion
            
			#region BUILDING THE TABLE (getting from database by BO)			
			DataSet dstResult = objPLTable.mdst_MAIN_DATA_REPOSITORY;
			dtbResult = dstResult.Tables[0];
			dtbResultNext = dstResult.Tables[1];
			dtbResultNextNext = dstResult.Tables[2];
			#endregion

			#endregion BUILD some RAW DATA TABLE

			#region	GETTING THE PARAMETER			

			#endregion GETTING THE PARAMETER
			
			#region GETTING THE DATE HEADING
			ArrayList arrDueDate = GetColumnValuesFromTable(dtbResult,SCHEDULE_DATE);
			DataTable dtbPartyItemList = objPLTable.GetPartyItemList(pstrPartyID_LIST, pstrProductID_LIST);
			ArrayList arrItemList = new ArrayList();
			if (pstrProductID_LIST != null && pstrProductID_LIST.Trim().Length > 0)
			{
			    arrItemList.AddRange(pstrProductID_LIST.Split(','));
			}
			ArrayList arrItems = GetPartyID_PartNoGROUPFromTable(dtbResult, dtbPartyItemList, PARTYID, ITM_ProductTable.PRODUCTID_FLD, arrItemList);

			foreach(DateTime dtm in arrDueDate)
			{
				string strColumnName = COL_ORDER_PREFIX + dtm.Day.ToString("00");
				string strColumnNameA = COL_ADJUSTMENT_PREFIX + dtm.Day.ToString("00");
				arrDueDateHeading.Add(strColumnName);
				arrDueDateHeading.Add(strColumnNameA);
			}		

			#endregion
			
			#region TRANSFORM ORIGINAL TABLE FOR REPORT			

			DataTable dtbTransform = BuildPartOrderSheetTable(arrDueDateHeading);
		    dtbTransform.Columns.Add(new DataColumn("OrderMonth", typeof (string)));
            dtbTransform.Columns.Add(new DataColumn("NextMonth", typeof(string)));
            dtbTransform.Columns.Add(new DataColumn("Next2Month", typeof(string)));

            DateTime dtmOrderMonth = new DateTime(
				int.Parse(PartOrderSheetMultiVendorReport.pstrYear)	,
				int.Parse(PartOrderSheetMultiVendorReport.pstrMonth) ,
				1);

            dtbTransform.Columns["OrderMonth"].DefaultValue = dtmOrderMonth.ToString(DateFormat).ToUpperInvariant();
            dtbTransform.Columns["NextMonth"].DefaultValue = dtmOrderMonth.AddMonths(1).ToString(DateFormat).ToUpperInvariant();
            dtbTransform.Columns["Next2Month"].DefaultValue = dtmOrderMonth.AddMonths(2).ToString(DateFormat).ToUpperInvariant();
			// fill data to the dtbTransform
			foreach(string strItem in arrItems)
			{
				// Create DUMMYROW FIRST
				DataRow dtrNew = dtbTransform.NewRow();

				double dblSumRowNext = 0;
				double dblSumRowNextNext = 0;				

				double dblSumRowNextA = 0;
				double dblSumRowNextNextA = 0;

				string strPartyID = strItem.Split('#')[0];
				string strPartyFilter = "PartyID = " + strPartyID;
				string strFilter = 	string.Format("[{0}]={1} AND [{2}]={3} " ,
					PARTYID, strItem.Split('#')[0] ,
					ITM_ProductTable.PRODUCTID_FLD,strItem.Split('#')[1]);
				string strMySort = string.Format("[{0}] ASC, [{1}] ASC",
					PARTYID, ITM_ProductTable.PRODUCTID_FLD);
				DataRow[] drowRevisionNext = GetRevisionList(dtbScheduleNextOrigin, strPartyID);
				DataRow[] drowRevisionNextNext = GetRevisionList(dtbScheduleNextNextOrigin, strPartyID);
				DataRow[] drowRevision = GetRevisionList(dtbScheduleOrigin, strPartyID);

				#region Next Month

				DataRow[] dtrowsNext = dtbResultNext.Select(strFilter, strMySort);
				foreach(DataRow dtr in dtrowsNext)
				{
					DateTime dtmDay = ((DateTime)dtr[SCHEDULE_DATE]);
					string strDayFilter = strFilter + " AND [Day] = " + dtmDay.Day;
					int intCurrentVerion = -1;
					try
					{
						intCurrentVerion = Convert.ToInt32(drowRevisionNext[0]["PORevision"]);
						strDayFilter += " AND PORevision = " + intCurrentVerion.ToString();
					}
					catch{}
					double dblOrderQuantity = 0;
					try
					{
						dblOrderQuantity = Convert.ToDouble(dtbPOInNextMonth.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", strDayFilter));
					}
					catch{}
					dblSumRowNext += dblOrderQuantity;
					
					// adjustment of current day
					double dblAdjustOfDay = 0;
					if (intCurrentVerion > 0)
					{
						string strPreviousFilter = strFilter + " AND [Day] = " + dtmDay.Day;
						try
						{
							strPreviousFilter += " AND PORevision = " + Convert.ToInt32(drowRevisionNext[1]["PORevision"]);
							double dblOrderPrevious = 0;
							try
							{
								dblOrderPrevious = Convert.ToDouble(dtbPOInNextMonth.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", strPreviousFilter));
								dblAdjustOfDay = dblOrderQuantity - dblOrderPrevious;
							}
							catch{}
						}
						catch
						{
							// no adjusment of day
						}
					}
					dblSumRowNextA += dblAdjustOfDay;
				}
				
				#endregion

				#region Next 2 Month

				DataRow[] dtrowsNextNext = dtbResultNextNext.Select(strFilter, strMySort);
				foreach(DataRow dtr in dtrowsNextNext)
				{
					DateTime dtmDay = ((DateTime)dtr[SCHEDULE_DATE]);
					string strDayFilter = strFilter + " AND [Day] = " + dtmDay.Day;
					int intCurrentVerion = -1;
					try
					{
						intCurrentVerion = Convert.ToInt32(drowRevisionNextNext[0]["PORevision"]);
						strDayFilter += " AND PORevision = " + intCurrentVerion.ToString();
					}
					catch{}
					double dblOrderQuantity = 0;
					try
					{
						dblOrderQuantity = Convert.ToDouble(dtbPOInNext2Month.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", strDayFilter));
					}
					catch{}
					dblSumRowNextNext += dblOrderQuantity;
					
					// adjustment of current day
					double dblAdjustOfDay = 0;
					if (intCurrentVerion > 0)
					{
						string strPreviousFilter = strFilter + " AND [Day] = " + dtmDay.Day;
						try
						{
							strPreviousFilter += " AND PORevision = " + Convert.ToInt32(drowRevisionNextNext[1]["PORevision"]);
							double dblOrderPrevious = 0;
							try
							{
								dblOrderPrevious = Convert.ToDouble(dtbPOInNext2Month.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", strPreviousFilter));
								dblAdjustOfDay = dblOrderQuantity - dblOrderPrevious;
							}
							catch{}
						}
						catch
						{
							// no adjusment
						}
					}
					dblSumRowNextNextA += dblAdjustOfDay;
				}

				#endregion

				#region Current Month

				DataRow[] dtrows = dtbResult.Select(strFilter, strMySort);
				// search the latest row (max revision)
				DataRow drowLatest = FindLatestRow(dtbPOInMonth, strPartyFilter);
				double dblSumRowA = 0;

				#region foreach schedule date

				foreach(DataRow dtr in dtrows)
				{
					// fill data to the dummy row
					// these column is persistance, we always set to the first rows
					dtrNew[VENDOR] = dtrows[0][VENDOR];
					dtrNew["CategoryID"] = dtrows[0]["CategoryID"];
					dtrNew[PARTNO] = dtrows[0][PARTNO];
					dtrNew[PARTNAME] = dtrows[0][PARTNAME];
					dtrNew[MODEL] = dtrows[0][MODEL];
					dtrNew[UNITPRICE] = dtrows[0][UNITPRICE];

					// po no
					dtrNew["PONO"] = drowLatest["PONO"];
					// issue date
					dtrNew[PO_PurchaseOrderMasterTable.ORDERDATE_FLD] = drowLatest[PO_PurchaseOrderMasterTable.ORDERDATE_FLD];
					// revision
					dtrNew[PO_PurchaseOrderMasterTable.POREVISION_FLD] = drowLatest[PO_PurchaseOrderMasterTable.POREVISION_FLD];
					
					DateTime dtmDay = ((DateTime)dtr[SCHEDULE_DATE]);
					string strDayFilter = strFilter + " AND [Day] = " + dtmDay.Day;
					int intCurrentVerion = -1;
					try
					{
						intCurrentVerion = Convert.ToInt32(drowRevision[0]["PORevision"]);
						strDayFilter += " AND PORevision = " + intCurrentVerion.ToString();
					}
					catch{}
					// fill the Quantity of the day to the cell (indicate by column Dxx in this dummy rows)
					string strDateColumnToFill = COL_ORDER_PREFIX + dtmDay.Day.ToString("00");
					double dblOrderQuantity = 0;
					try
					{
						dblOrderQuantity = Convert.ToDouble(dtbPOInMonth.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", strDayFilter));
					}
					catch{}
					dtrNew[strDateColumnToFill] = dblOrderQuantity;

					// fill the Quantity of the day to the cell (indicate by column Dxx in this dummy rows)
					strDateColumnToFill = COL_ADJUSTMENT_PREFIX + dtmDay.Day.ToString("00");
					// adjustment of current day
					double dblAdjustOfDay = 0;
					if (intCurrentVerion > 0)
					{
						string strPreviousFilter = strFilter + " AND [Day] = " + dtmDay.Day;
						try
						{
							strPreviousFilter += " AND PORevision = " + Convert.ToInt32(drowRevision[1]["PORevision"]);
							double dblOrderPrevious = 0;
							try
							{
								dblOrderPrevious = Convert.ToDouble(dtbPOInMonth.Compute("SUM(" + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ")", strPreviousFilter));
							}
							catch{}
							dblAdjustOfDay = dblOrderQuantity - dblOrderPrevious;
						}
						catch
						{
							// no adjusment
						}
					}
					if (dblAdjustOfDay != 0)
						dtrNew[strDateColumnToFill] = dblAdjustOfDay;
					dblSumRowA += dblAdjustOfDay;

					// Order Delivery quantity
					dtrNew[SUMROWNEXT_O] = dblSumRowNext;
					dtrNew[SUMROWNEXTNEXT_O] = dblSumRowNextNext;

					// Adjustment Quantity
					dtrNew[SUMROW_A] = dblSumRowA;
					dtrNew[SUMROWNEXT_A] = dblSumRowNextA;
					dtrNew[SUMROWNEXTNEXT_A] = dblSumRowNextNextA;	
				}

				#endregion

				#region for item which have no schedule in this month

				if (dtrows.Length == 0)
				{
					/// fill data to the dummy row
					/// these column is persistance, we always set to the first rows
					DataRow[] drowInfo = dtbPartyItemList.Select(strFilter);
					dtrNew[VENDOR] = drowInfo[0][VENDOR];
					dtrNew["CategoryID"] = drowInfo[0]["CategoryID"];
					dtrNew[PARTNO] = drowInfo[0][PARTNO];
					dtrNew[PARTNAME] = drowInfo[0][PARTNAME];
					dtrNew[MODEL] = drowInfo[0][MODEL];
					dtrNew[UNITPRICE] = drowInfo[0][UNITPRICE];

					// po no
					dtrNew["PONO"] = drowLatest["PONO"];
					// issue date
					dtrNew[PO_PurchaseOrderMasterTable.ORDERDATE_FLD] = drowLatest[PO_PurchaseOrderMasterTable.ORDERDATE_FLD];
					// revision
					dtrNew[PO_PurchaseOrderMasterTable.POREVISION_FLD] = drowLatest[PO_PurchaseOrderMasterTable.POREVISION_FLD];

					/// Order Delivery quantity
					dtrNew[SUMROWNEXT_O] = dblSumRowNext;
					dtrNew[SUMROWNEXTNEXT_O] = dblSumRowNextNext;

					/// Adjustment Quantity
					dtrNew[SUMROWNEXT_A] = dblSumRowNextA;
					dtrNew[SUMROWNEXTNEXT_A] = dblSumRowNextNextA;	
				}

				#endregion

				#endregion

				// add to the transform data table
				dtbTransform.Rows.Add(dtrNew);				
			}
			
			#endregion

		    #region render report

		    var report = new C1Report();

		    #region Load report from definition file

		    var filePath = Path.Combine(_mstrReportDefinitionFolder, REPORT_LAYOUT_FILE);
		    if (!File.Exists(filePath))
		    {
		        return new DataTable();
		    }

		    string[] arrstrReportInDefinitionFile = report.GetReportInfo(filePath);
		    report.Load(filePath, arrstrReportInDefinitionFile[0]);

		    #endregion

            #region hightlight sunday column

            var endMonth = dtmOrderMonth.AddMonths(1).AddDays(-1);
            var script = new StringBuilder();
            for (DateTime day = dtmOrderMonth; day <= endMonth; day = day.AddDays(1))
            {
                if (day.DayOfWeek != DayOfWeek.Sunday)
                {
                    continue;
                }
                var fieldName = string.Format("D{0}Lbl", day.Day.ToString("00"));
                report.Fields[fieldName].BackColor = Color.Yellow;
                report.Fields[fieldName].ForeColor = Color.Red;
                script.AppendLine(string.Format("{0}.BackColor = RGB(255,255,0)", fieldName));
                script.AppendLine(string.Format("{0}.ForeColor = RGB(255,0,0)", fieldName));
            }

            #endregion

		    report.ReportName = REPORT_NAME;
		    report.DataSource.Recordset = dtbTransform;
		    report.Render();

            ReportBuilder.MarkRedToNegativeNumberField(report);
            report.Render();

		    // and show it in preview dialog				
		    var printPreview = new C1PrintPreviewDialog();
		    printPreview.FormTitle = REPORT_NAME;
		    printPreview.ReportViewer.Document = report.Document;
		    printPreview.Show();
			
		    UseReportViewerRenderEngine = false;

		    #endregion

			_mResult = dtbTransform;
			return dtbTransform;
		}
		private DataRow[] GetRevisionList(DataTable pdtbSchedule, string pstrPartyID)
		{
			return pdtbSchedule.Select("PartyID = " + pstrPartyID, "PORevision DESC");
		}
		private DataRow FindLatestRow(DataTable pdtbPOInMonth, string pstrFilter)
		{
			try
			{
				return pdtbPOInMonth.Select(pstrFilter, " PORevision DESC")[0];
			}
			catch
			{
				return null;
			}
		}
		/// <summary>
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objGet = drow[pstrColumnName];
					if( !arrRet.Contains(objGet)  )
					{
						arrRet.Add(objGet);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}
		
		/// <summary>
		/// /// thachnn: 14/Nov/2005
		/// Build the crosstab table to render on report
		/// contain D01 ---> D31 , A01 ---> A31, and NextMonth SumRow O A, NextNextMonth SumRow O A
		/// see Schedule of local parts in month UseCase under PCS/06-Project Design/DesignReport folder.
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildPartOrderSheetTable(ArrayList parrScheduleDateHeading)
		{
			const string strPartOrderSheetTableName = "PartOrderSheetTable";
			try
			{
				//Create table
				DataTable dtbRet = new DataTable(strPartOrderSheetTableName);
				
				//Add columns
				dtbRet.Columns.Add("PONO", typeof(System.String));
				dtbRet.Columns.Add(PO_PurchaseOrderMasterTable.POREVISION_FLD, typeof(System.String));
				dtbRet.Columns.Add(PO_PurchaseOrderMasterTable.ORDERDATE_FLD, typeof(DateTime));
				dtbRet.Columns.Add(PARTYID, typeof(System.String));
				dtbRet.Columns.Add("CategoryID", typeof(int));
				dtbRet.Columns.Add(VENDOR, typeof(System.String));
				dtbRet.Columns.Add(PARTNO, typeof(System.String));											
				dtbRet.Columns.Add(PARTNAME, typeof(System.String));
				dtbRet.Columns.Add(MODEL, typeof(System.String));
				dtbRet.Columns.Add(UNITPRICE, typeof(decimal));

				dtbRet.Columns.Add(SUMROWNEXT_O, typeof(System.Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT_O, typeof(System.Double));

				dtbRet.Columns.Add(SUMROW_A, typeof(System.Double));
				dtbRet.Columns.Add(SUMROWNEXT_A, typeof(System.Double));
				dtbRet.Columns.Add(SUMROWNEXTNEXT_A, typeof(System.Double));

				foreach(string strColumnName in parrScheduleDateHeading)
				{					
					try
					{
						dtbRet.Columns.Add(strColumnName,typeof(System.Double));
					}
					catch{}
				}
				// FILL the null column, if not exist null column (not existed date.) report will gen ###,#0 to the cell	
				for(int i = 1; i <=31; i++)												  
				{
					if(parrScheduleDateHeading.Contains(COL_ORDER_PREFIX + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(COL_ORDER_PREFIX + i.ToString("00"),typeof(System.String));							
						}
						catch{}						
					}
					if(parrScheduleDateHeading.Contains(COL_ADJUSTMENT_PREFIX + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(COL_ADJUSTMENT_PREFIX + i.ToString("00"),typeof(System.String));							
						}
						catch{}						
					}
				}
				
				return dtbRet;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Thachnn : 08/Nov/2005
		/// Browse the DataTable, get all value of PartyID-PartNo pair column, insert into ArraysList as PartyIDPartNoValue
		/// Because Item differ from each other by PartyID and partNo
		/// So this group will be unique between Items of report
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrPartyIDColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect PartyID - PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetPartyID_PartNoGROUPFromTable(DataTable pdtb, DataTable pdtbPartyItemList, string pstrPartyIDColName, string pstrPartNoColName, ArrayList arrItemList)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				string strLastPartyID = string.Empty;
				foreach (DataRow drow in pdtb.Rows)
				{
					object objPartyIDGet = drow[pstrPartyIDColName];
					object objPartNoGet = drow[pstrPartNoColName];
					string str = objPartyIDGet.ToString().Trim() +"#"+ objPartNoGet.ToString().Trim();					
					if(!arrRet.Contains(str))
						arrRet.Add(str);
					// now add item which is mass order for forecast
					if (strLastPartyID != objPartyIDGet.ToString())
					{
						strLastPartyID = objPartyIDGet.ToString();
						DataRow[] drowMassItem = pdtbPartyItemList.Select(pstrPartyIDColName + "=" + objPartyIDGet.ToString()
							+ " AND " + pstrPartNoColName + " <> " + objPartNoGet.ToString());
						foreach (DataRow drowItem in drowMassItem)
						{
							objPartNoGet = drowItem[pstrPartNoColName];
							if (arrItemList.Count > 0 && !arrItemList.Contains(objPartNoGet.ToString()))
								continue;
							str = objPartyIDGet.ToString().Trim() +"#"+ objPartNoGet.ToString().Trim();					
							if(!arrRet.Contains(str))
								arrRet.Add(str);
						}
					}
				}
				if (pdtb.Rows.Count == 0)
				{
					foreach (DataRow drowItem in pdtbPartyItemList.Rows)
					{
						object objPartyIDGet = drowItem[pstrPartyIDColName];
						object objPartNoGet = drowItem[pstrPartNoColName];
						if (arrItemList.Count > 0 && !arrItemList.Contains(objPartNoGet.ToString()))
							continue;
						string str = objPartyIDGet.ToString().Trim() +"#"+ objPartNoGet.ToString().Trim();
						if(!arrRet.Contains(str))
							arrRet.Add(str);
					}
				}
			}
			catch
			{
				arrRet.Clear();
			}
			return arrRet;
		}

	
	}	// end class report



	/// <summary>
	/// collect all task affect to the DataTable. 
	/// Must set the PurchaseOrderList datatable to the InnerDataTable of instance of this class to processing.
	/// </summary>
	public class PLReportDataHelper
	{
	    private string _connectionString;
		public PLReportDataHelper(string connectionString)
		{
		    _connectionString = connectionString;
		}
		// it is a offline cache of data from database 
		// main dataset of this report, contain all data get from database
		public DataSet mdst_MAIN_DATA_REPOSITORY = new DataSet("MAIN_DATA_REPOSITORY");
	    

		#region GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable) - TRANSFORM SOME THING

		/// <summary>
		/// Get Production Line List (table) with PPM setted
		/// </summary>
		/// <returns></returns>
		public void GetDataAndCache()
		{
            mdst_MAIN_DATA_REPOSITORY = GetPartOrderSheetMultiVendorReportDataSet(
				PartOrderSheetMultiVendorReport.pnCCNID,
				PartOrderSheetMultiVendorReport.CurrentMonth,
				PartOrderSheetMultiVendorReport.NextMonth,
				PartOrderSheetMultiVendorReport.Next2Month,
				PartOrderSheetMultiVendorReport.pstrPartyID_LIST,
				PartOrderSheetMultiVendorReport.pstrProductID_LIST
				);
		}
		
		#endregion GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable)

		public DataSet GetPartOrderSheetMultiVendorReportDataSet(int pnCCNID, DateTime pdtmMonth, DateTime pdtmNextMonth, DateTime pdtmNext2Month,
			string pstrVendorID_List, string pstrProductID_List)
		{
			DataSet dstRet = new DataSet("PartOrderSheetMultiVendorReport");
			DataTable dtbCurrentMonth	= GetPartOrderSheetMultiVendorReportData(pnCCNID,	pdtmMonth,	pstrVendorID_List,  pstrProductID_List);
			//			dtbCurrentMonth.TableName = 
			dstRet.Tables.Add(dtbCurrentMonth );

			DataTable dtbNextMonth		= GetPartOrderSheetMultiVendorReportData(pnCCNID,	pdtmNextMonth,	pstrVendorID_List,  pstrProductID_List);
			dtbNextMonth.TableName = dtbNextMonth.TableName  + "NextMonth";
			dstRet.Tables.Add(dtbNextMonth);

			DataTable dtbNextNextMonth	= GetPartOrderSheetMultiVendorReportData(pnCCNID,	pdtmNext2Month,	pstrVendorID_List,  pstrProductID_List);
			dtbNextNextMonth.TableName = dtbNextNextMonth.TableName  + "NextNextMonth";
			dstRet.Tables.Add(dtbNextNextMonth);

			return dstRet;
		}

		
		/// <summary>
		/// THACHNN: 11/10/2005
		/// Getting the data for Part Order Sheet Multi Vendor Report
		/// </summary>		
		/// <returns></returns>
		public DataTable GetPartOrderSheetMultiVendorReportData(int pnCCNID, DateTime pdtmMonth, string pstrVendorID_List, string pstrProductID_List)
		{			
			const string TABLE_NAME = "PartOrderSheetDataReport";

			string strSql = string.Empty;

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{			
				strSql =
					" Declare @pstrYear char(4) " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrCCNID int " + 
					" /*-----------------------------------*/ " + 
					" Set @pstrYear =   '" +pdtmMonth.Year.ToString("0000")+ "' " + 
					" Set @pstrMonth = '" +pdtmMonth.Month.ToString("00")+ "' " + 
					" Set @pstrCCNID = " +pnCCNID+ " " + 
					" /*-----------------------------------*/ " + 
					"  " + 
 
					" SELECT " + 
					" MST_Party.PartyID as [PartyID], " +
					" MST_Party.Code + '( ' + MST_Party.Name + ')' as [Vendor] , " + 
					" ITM_Product.CategoryID, " +
					" ITM_Product.ProductID,   " + 
					" ITM_Product.Code AS [Part No.],   " + 
					" ITM_Product.Description AS [Part Name],  " + 
					" ITM_Product.Revision AS [Model], " + 
					" ITM_Product.ListPrice AS [UnitPrice], " + 
					" DATEPART(dd, PO_DeliverySchedule.ScheduleDate)as [Day], " + 
					" Min(PO_DeliverySchedule.ScheduleDate) as [ScheduleDate], " + 
					" SUM(PO_DeliverySchedule.DeliveryQuantity) AS [Quantity] , " + 
					" SUM(PO_DeliverySchedule.Adjustment) AS [Adjustment] " + 
					"  " + 
					" FROM        " + 
					" PO_DeliverySchedule  " + 
					" INNER JOIN PO_PurchaseOrderDetail " + 
					" 	 on PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID " + 
					" INNER JOIN ITM_Product " + 
					" 	on PO_PurchaseOrderDetail.ProductID = ITM_Product.ProductID " + 
					" INNER JOIN PO_PurchaseOrderMaster " + 
					" 	on PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID " + 
					" INNER JOIN MST_CCN  " + 
					" 	ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID   " + 
					" INNER JOIN MST_Party " + 
					" 	ON PO_PurchaseOrderMaster.PartyID = MST_Party.PartyID " + 
					" WHERE     " + 
					" MST_CCN.CCNID = @pstrCCNID   " + 
					" AND DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = @pstrYear  " + 
					" AND DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = @pstrMonth " + 
					(pstrVendorID_List.Trim() == string.Empty ? (string.Empty)	: (" AND PO_PurchaseOrderMaster.PartyID IN (" +pstrVendorID_List+ ") ") ) + 
					(pstrProductID_List.Trim() == string.Empty ? (string.Empty)	: (" AND PO_PurchaseOrderDetail.ProductID IN (" +pstrProductID_List+ ") ") ) + 
					"  " + 
					"  " + 
					" GROUP BY    " + 
					" MST_Party.PartyID, " +
					" MST_Party.Code, " + 
					" MST_Party.Name, " + 
					" MST_CCN.Code,  " + 
					" ITM_Product.CategoryID,  " + 
					" ITM_Product.ProductID, " +
					" ITM_Product.Code,   " + 
					" ITM_Product.Description,  " + 
					" ITM_Product.Revision , " + 
					" ITM_Product.ListPrice , " + 
					" DATEPART(dd  , PO_DeliverySchedule.ScheduleDate) " + 
					"  " + 
					" ORDER BY " + 
					" [Vendor], " + 
					" ITM_Product.CategoryID, " + 
					" [Part Name], " + 
					" [Model], " +
					" [Part No.]";


                oconPCS = new OleDbConnection(_connectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					return dstPCS.Tables[0].Copy();
				}
				else
				{
					return new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new Exception(strSql,ex);
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
		/// public Function to get pointer to the dataTable with provided Name
		/// </summary>
		/// <param name="pstrTableNameToGet"></param>
		/// <returns></returns>
		public DataTable GetDataTable(string pstrTableNameToGet)
		{
			if(mdst_MAIN_DATA_REPOSITORY.Tables.Count > 0)
			{
				return mdst_MAIN_DATA_REPOSITORY.Tables[pstrTableNameToGet];
			}
			else
			{
				return null;
			}
		}

		public DataTable GetDataTable(int pstrTableIndexToGet)
		{
			if(mdst_MAIN_DATA_REPOSITORY.Tables.Count > 0)
			{
				return mdst_MAIN_DATA_REPOSITORY.Tables[pstrTableIndexToGet];
			}
			else
			{
				return null;
			}
		}

		public DataTable GetPOInMonth(DateTime pdtmMonth)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{			
				string strSql = "SELECT Code PONO, OrderDate, PartyID, ISNULL(PORevision,0) PORevision, ProductID,"
					+ " DATEPART(day, PO_DeliverySchedule.ScheduleDate) [Day], SUM(DeliveryQuantity) " + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD
					+ " FROM PO_DeliverySchedule JOIN PO_PurchaseOrderDetail"
					+ " 	ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ " 	ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " WHERE DATEPART(yyyy, PO_DeliverySchedule.ScheduleDate) = " + pdtmMonth.Year
					+ " AND DATEPART(mm, PO_DeliverySchedule.ScheduleDate) = " + pdtmMonth.Month
					+ " GROUP BY Code, OrderDate, PartyID, PORevision, ProductID, DATEPART(day, PO_DeliverySchedule.ScheduleDate)"
					+ " ORDER BY PartyID, PORevision, ProductID";

                oconPCS = new OleDbConnection(_connectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(Exception ex)
			{
				throw ex;
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

		public DataTable GetDateRange(DateTime pdtmMonth, string pstrPartyList)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{			
				string strSql = "SELECT DISTINCT PartyID, PORevision, Min(ScheduleDate) FromDate, Max(ScheduleDate) ToDate"
					+ " FROM PO_DeliverySchedule S JOIN PO_PurchaseOrderDetail D ON S.PurchaseOrderDetailID = D.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster M ON D.PurchaseOrderMasterID = M.PurchaseOrderMasterID"
					+ " WHERE DATEPART(yyyy, ScheduleDate) = " + pdtmMonth.Year
					+ " AND DATEPART(mm, ScheduleDate) = " + pdtmMonth.Month;
					//+ " AND ISNULL(S.CancelDelivery,0) = 0";
				if (pstrPartyList != null && pstrPartyList.Trim() != string.Empty)
					strSql += " AND PartyID IN (" + pstrPartyList + ")";
				strSql += " GROUP BY PartyID, PORevision"
					+ " ORDER BY PartyID, PORevision DESC";

                oconPCS = new OleDbConnection(_connectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(Exception ex)
			{
				throw ex;
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

		public DataTable GetPartyItemList(string pstrPartyList, string pstrProductID)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{			
				string strSql = "SELECT ProductID, PrimaryVendorID AS PartyID, ITM_Product.CategoryID,"
					+ " ITM_Product.Code [Part No.], ITM_Product.Description [Part Name], ITM_Product.Revision [Model],"
					+ " ITM_Product.ListPrice [UnitPrice], MST_Party.Code + '( ' + MST_Party.Name + ')' as [Vendor]"
					+ " FROM ITM_Product JOIN MST_Party ON ITM_Product.PrimaryVendorID = MST_Party.PartyID"
					+ " WHERE PrimaryVendorID IS NOT NULL"
					+ " AND ISNULL(MassOrder,0) = 1";
				if (pstrPartyList != null && pstrPartyList.Trim() != string.Empty)
					strSql += " AND PrimaryVendorID IN (" + pstrPartyList + ")";
				if (pstrProductID != null && pstrProductID.Trim() != string.Empty)
					strSql += " AND ProductID IN (" + pstrProductID + ")";
				strSql += " ORDER BY MST_Party.Code";

                oconPCS = new OleDbConnection(_connectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				odadPCS.Fill(dtbData);
				return dtbData;
			}
			catch(Exception ex)
			{
				throw ex;
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
	}	// end class DataReportHelper
}


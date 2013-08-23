using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
//using PCSComUtils.Common;
//using PCSComUtils.PCSExc;
//using PCSUtils.Log;
//using PCSUtils.Utils;
//using PCSUtils.Admin;
//using PCSComUtils.Admin.DS;
using System.Data.OleDb;
//using PCSUtils;
using Utils = PCSComUtils.DataAccess.Utils;
using System.Collections.Specialized;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using C1.Win.C1Chart;
using Section = C1.C1Report.Section;

using System.IO;
using Microsoft.Office.Interop;
using Range = Microsoft.Office.Interop.Excel.Range;
using Excel = Microsoft.Office.Interop.Excel;


namespace ProductionLineCapacityAndManPowerManagementReport
{
	/// <summary>
	/// Thachnn: CONCEPT to build this report
	/// 
	/// </summary>
	[Serializable]	
	public class ProductionLineCapacityAndManPowerManagementReport : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Implementation
		private string mConnectionString;
		private ReportBuilder mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = false;	

		private string mstrReportDefinitionFolder = string.Empty;

		private object mResult;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

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

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
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
		public bool UseReportViewerRenderEngine { get { return mUseReportViewerRenderEngine; } set { mUseReportViewerRenderEngine = value; } }

		
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get 
			{
				return mstrReportDefinitionFolder;
			}
			set
			{
				mstrReportDefinitionFolder = value;
			}
		}


		private string mstrReportLayoutFile = string.Empty;		
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
				return mstrReportLayoutFile;
			}
			set
			{
				mstrReportLayoutFile = value;
			}
		}


		public object Invoke(string pstrMethod, object[] pobjParameters)
		{			
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}


		#endregion

		

		public ProductionLineCapacityAndManPowerManagementReport()
		{
		}		

		
	
		const string THIS = "ExternalReportFile:ProductionLineCapacityAndManPowerManagementReport";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "ProductionLineCapacityAndManPowerManagementReport";			
		const string ZERO_STRING = "0";
		
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		const string NEXTMONTH_DATE_FORMAT = "MMM-yy";

		public static string FORMAT_REPORT_NUMBER = "#,##0.00";



		/// Result Data Table Column name			
		const string WORKCENTERID = "WorkCenterID";
		const string CODE = "Code";
		const string PROCESSNAME = "ProcessName";			
		const string BEGINDATE = "BeginDate";
		const string ENDDATE = "EndDate";
		const string CAPACITY = "Capacity";
		const string FACTOR = "Factor";
		const string CREWSIZE = "CrewSize";
		const string NOOFMACHINE = "NoOfMachine";
		const string WCTYPE = "WCType";
		const string MACHINESETUP = "MachineSetup";
		const string MACHINERUN = "MachineRun";
		const string LABORSETUP = "LaborSetup";
		const string LABORRUN = "LaborRun";

		const string CURRENTMP = "CurrentMP";
	
		const string STANDARDCAPACITY = "StandardCapacity";		

		const string LEADTIME = "LeadTime";
		const string ISMAIN = "IsMain";
		//const string SHIFT = "Shift";			

		const string HEADER = "Header";

		const string SHIFT1_ID = "1";	 
		/// ID of Shift 1S in the Quantity Table  
		///  and SumChangeCategoryPlusCheckPoint Table (dtbSumChangeCategoryPlusCheckPoint)
//		const string SHIFT2_ID = "2";
//		const string SHIFT3_ID = "3";


		string strMainWorkCenter = string.Empty;

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report		
		/// </summary>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <param name="pstrBaseProductID">Base Item</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrBaseProductID)
		{
			#region My variables				

			/// Report layout file constant
			const string REPORT_LAYOUT_FILE = "ProductionLineCapacityAndManPowerManagementReport.xml";
			const string REPORT_NAME = "ProductionLineCapacityAndManPowerManagementReport";
			short COPIES = 1;

			/// all parameter are Mandatory
			const string REPORTFLD_PARAMETER_CCN					= "lblCCN";
			const string REPORTFLD_PARAMETER_MONTH					= "lblMonth";
			const string REPORTFLD_PARAMETER_YEAR						= "lblYear";
			const string REPORTFLD_PARAMETER_PRODUCTIONLINE		= "lblProductionLine";
			const string REPORTFLD_PARAMETER_MAINWORKCENTER		= "lblMainWorkCenter";
			const string REPORTFLD_PARAMETER_BASEITEM				= "lblBaseItem";						
			string strBaseItem = string.Empty;

			int nCCNID = int.Parse(pstrCCNID);
			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);			
			int nProductionLineID = int.Parse(pstrProductionLineID);
			int nBaseProductID = int.Parse(pstrBaseProductID);
			
			string strCCN = string.Empty;
			string strMonth = string.Empty;
			string strYear = string.Empty;
			string strMainWorkCenter = string.Empty;	// we will init its value when looping throught the WorkCenterInfo array
			string strProductionLine = string.Empty;

			const string REPORTFLD_TITLE			= "fldTitle";
			const string REPORTFLD_COMPANY			= "fldCompany";
			const string REPORTFLD_ADDRESS			= "fldAddress";
			const string REPORTFLD_TEL				= "fldTel";
			const string REPORTFLD_FAX				= "fldFax";

			const string REPORTFLD_CHART			= "fldChart";
			
			float fActualPageSize = 9000.0f;

			/// key field of table containing selected fields = CODE
			/// We use WorkCenter Code field to select exactly a row from the dtbWorkCenterList
			/// Later, we will loop throught this array to build the report			
			/// Build to keep value WorkCenter Info, to keep 3 first rows of the report, ... depend on the real data of dtbResule
			Hashtable arrWorkCenterInfo = new Hashtable();	
			StringCollection arrWorkCenterCode = new StringCollection();
			
			System.Data.DataTable dtbRet;
			System.Data.DataTable dtbWorkCenterList;			
			System.Data.DataTable dtbLeadTimeTable;
			System.Data.DataTable dtbSumChangeCategoryPlusCheckPoint ;

			
			#endregion		

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strCCN = boUtil.GetCCNCodeFromID(nCCNID);	
			strProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + ": " + objBO.GetProductLineNameFromID(nProductionLineID);
			strBaseItem = objBO.GetProductCodeFromID(nBaseProductID) + ": " + objBO.GetProductNameFromID(nBaseProductID);
			
			#endregion

						
			#region BUILD some RAW DATA TABLE
			
			//// Build the WorkCenterList table: 
			///SCHEMA = WorkCenterID - Code - ProcessName - IsMain  ---> getting the meta info data on the report
			dtbWorkCenterList = BuildWorkCenterListInProductionLineTable(pstrMonth, pstrYear, pstrProductionLineID,pstrBaseProductID);

			/// Build the leadtime table.Data is provided by WorkCenter and for each Shift: 
			/// SCHEMA: WorkCenterID - ShiftID - LeadTime
			dtbLeadTimeTable = BuildLeadTimeByShiftTable(  pstrYear, pstrMonth, pstrProductionLineID 	);

			/// build the datatable contain (Change category + check point) list for all (WorkCenter - Shift)  
			/// SCHEMA: WorkCenterID - ShiftID - SumChangeCategoryPlusCheckPoint
			dtbSumChangeCategoryPlusCheckPoint = BuildSumChangeCategoryPlusCheckPointTable(  pstrYear, pstrMonth, pstrProductionLineID 	);

			#endregion BUILD some RAW DATA TABLE			


			#region BUILD DATA OF REPORT

			#region BUILD RESULT SCHEMA HashTable of WorkCenterInfo object. Also create a stringCollection of WorkCenterCode (to loop easier later throught every WorkCenter in arrWorkCenterInfo)
			/// Build array contain meta data info 
			/// (WC COde, Process Name, MachineName
			/// Machine Run, Labor Time, CurrentMP (CREWSIZE) , StandardCapacity )
			/// fill  the meta info of each WorkCenter in arrWorkCenterInfo
			foreach(DataRow drow in dtbWorkCenterList.Rows)
			{
				WorkCenterInfo objInfo = new WorkCenterInfo();
				objInfo.WorkCenterID = (int)drow[WORKCENTERID];
				objInfo.WorkCenterCode = drow[CODE].ToString();
				objInfo.ProcessName = drow[PROCESSNAME].ToString();

				try
				{
					objInfo.MachineRun = ((drow[MACHINERUN] is decimal)? Convert.ToDecimal( drow[MACHINERUN] ) : decimal.Zero );
				}	catch	{}
				try
				{
					objInfo.LaborTime = ((drow[LABORRUN] is decimal)? Convert.ToDecimal( drow[LABORRUN] ) : decimal.Zero );
				}	catch{}
				try
				{
					objInfo.CurrentMP = ((drow[CURRENTMP] is decimal)? Convert.ToDecimal( drow[CURRENTMP] ) : decimal.Zero );
				}	
				catch{}				
				try
				{
					objInfo.StandardCapacity = ((drow[STANDARDCAPACITY] is decimal)? Convert.ToDecimal( drow[STANDARDCAPACITY] ) : decimal.Zero );
				}	
				catch{}
				try
				{
					objInfo.Factor = ((drow[FACTOR] is decimal)? Convert.ToDecimal( drow[FACTOR] ) : decimal.Zero );
				}	
				catch{}

				/// PUT in the arrWOrkCenterInfo - repository
				arrWorkCenterInfo.Add(drow[CODE].ToString(),objInfo);
				arrWorkCenterCode.Add(drow[CODE].ToString());
			}           
			
			#endregion
			
			#region FILL DATA -- from datatable to the WorkCenterInfo array (hashtable) - Current, S1, ISMain
			
			decimal decS1 = Convert.ToDecimal( GetWorkingTimeOfShift1(int.Parse(SHIFT1_ID), nCCNID )   );
			decimal decDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP = 
					GetDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP(pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID);
			decimal decWorkingDayInMonthOfMainWorkCenter = 
				GetWorkingDayInMonthOfMainWorkCenter(pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID);
			
			string strFilter;
			string strSort;
			foreach(string strWorkCenterCode in arrWorkCenterCode)	
			/// GUIDE: whenever you change any data of object in a HashTable, you need to reassign it to the Hashtable if you dont want to lose your change
			{
				WorkCenterInfo objWCInfo = (WorkCenterInfo)arrWorkCenterInfo[strWorkCenterCode];
				strFilter = string.Format("[{0}]='{1}'",
					CODE,
					strWorkCenterCode);
				//OLD: using SHIFT: strSort = string.Format("[{0}] ASC, [{1}] ASC",CODE , SHIFT);
				strSort = string.Format("[{0}] ASC",CODE);
				DataRow[] arrRowSelectedWorkCenters = dtbWorkCenterList.Select(strFilter,strSort);

				// HACED: Thachnn: change the way to get the WorkingTime of Shift1
				// decimal decS1,decS2,decS3;
				// GetShiftWorkTime(dtbLeadTimeTable, objWCInfo.WorkCenterID, out decS1, out decS2, out decS3);				
				
				///  (dtbLeadTimeTable) contain Sum(WOLeadTime*Quantity) + Sum(CPOLeadTime*Quantity).    we now add the ChangeCategoryPlusCheckPoint
				// set leadtime of Shift1 of each WorkCenter to the WorkCenterInfo object
				// objWCInfo.S1 = decS1 + GetChangeCategoryPlusCheckPoint(objWCInfo.WorkCenterID ,int.Parse(SHIFT1_ID),dtbSumChangeCategoryPlusCheckPoint);
				objWCInfo.S1 = decS1;
				objWCInfo.DCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP = decDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP;
				objWCInfo.WorkingDayInMonthOfMainWorkCenter = decWorkingDayInMonthOfMainWorkCenter;

				/// getting current capacity here, sum all capacity of all shift     +      Sum all ChangeCategory+CheckPoint (all shift)
				objWCInfo.CurrentCapacity = GetCurrentCapacityOfWorkCenter(dtbLeadTimeTable ,objWCInfo.WorkCenterID) 
					+ GetChangeCategoryPlusCheckPointTotal(objWCInfo.WorkCenterID, dtbSumChangeCategoryPlusCheckPoint);

				foreach(DataRow drow in arrRowSelectedWorkCenters)
				{					
//					objWCInfo.Machine = (decimal)drow[MACHINERUN]+(decimal)drow[MACHINESETUP];
//					objWCInfo.Labor= (decimal)drow[LABORRUN]+(decimal)drow[LABORSETUP];
//					objWCInfo.LeadTime = (decimal)drow[LEADTIME];
					objWCInfo.IsMain = false;
					try
					{
						objWCInfo.IsMain = Convert.ToBoolean(drow[ISMAIN]);
						if(objWCInfo.IsMain )
						{
							strMainWorkCenter = objWCInfo.WorkCenterCode + ReportBuilder.SEPERATOR_PARAMETER_DISPLAY_INFO + objWCInfo.ProcessName;
						}
					}
					catch{}
					// NONEED: objWCInfo.StandardCapacity = dblStandardCapacity;
				}
				arrWorkCenterInfo[objWCInfo.WorkCenterCode] = objWCInfo; // reassign to the HashTable. Don't remove
			}	/// end for each WorkCenter in array
			
			// HACKED: standard capacity is now vary from each WorkCenter
//			/// recalculate the  Standard Capacity  = CurrentCapacity of the Main WorkCenter
//			dblStandardCapacity = GetStandardCapacity(arrWorkCenterInfo);

//			/// set all Standard Capacity of all WOrkCenter to the value recent get = dblStandardCapacity			
//			foreach(string strWorkCenterCode in arrWorkCenterCode)
//			{
//				WorkCenterInfo objWCInfo = (WorkCenterInfo)arrWorkCenterInfo[strWorkCenterCode];
//				objWCInfo.StandardCapacity = dblStandardCapacity;
//				arrWorkCenterInfo[objWCInfo.WorkCenterCode] = objWCInfo; // reassign to the HashTable. Don't remove
//			}				
			// ENDHACKED:


			#endregion FILL DATA
		 
			#endregion BUILD DATA OF REPORT

			
			#region RENDER REPORT
			
			ReportBuilder objRB = new ReportBuilder();				
			objRB.ReportName = REPORT_NAME;
			objRB.SourceDataTable = dtbRet = new DataTable();	// we build report base on HashTable, not DataTable, so we put new empty DataTable in to ReportBuilder to avoid error of outside processing
			
			#region INIT REPORT BUIDER OBJECT
			try
			{
				objRB.ReportDefinitionFolder = mstrReportDefinitionFolder;
				objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;					
				if(objRB.AnalyseLayoutFile() == false)
				{
					//					PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
					return new DataTable();
				}
				//objRB.UseLayoutFile = objRB.AnalyseLayoutFile();	// use layout file if any , auto drawing if not found layout file
				objRB.UseLayoutFile = true;	// always use layout file
			}
			catch
			{
				objRB.UseLayoutFile = false;
				//				PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND,MessageBoxIcon.Error);
			}
			C1.C1Report.Layout objLayout = objRB.Report.Layout;
			fActualPageSize = objLayout.PageSize.Width - (float)objLayout.MarginLeft - (float)objLayout.MarginRight;
			#endregion				
		
			objRB.MakeDataTableForRender();
				
			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();				
			printPreview.FormTitle = REPORT_NAME;
			objRB.ReportViewer = printPreview.ReportViewer;				
			objRB.RenderReport();			

			

			if(dtbWorkCenterList.Rows.Count > 0)	/// if having WorkCenter working in this production Line
			{
				#region BUILD THE REPORT
				/// Anchor field to draw
				const string ANCHOR_WORKCENTER = "lblWorkCenter";
				const string ANCHOR_PROCESSNAME= "lblProcessName";
				const string ANCHOR_MACHINERUN = "lblMachineRun";
				const string ANCHOR_LABORTIME = "lblLaborTime";
				const string ANCHOR_CAPACITYTOTAL = "lblCapacityTotal";
				const string ANCHOR_MANPOWERFACTOR = "lblManPowerFactor";
				const string ANCHOR_NECESSARYMANPOWER = "lblNecessaryManPowerValue";// "lblS1";
				const string ANCHOR_CURRENTMANPOWER = "lblCurrentManPowerValue";// "lblS2";
				const string ANCHOR_REMAINMANPOWER = "lblRemainManPowerValue";// "lblS3";
				const string ANCHOR_CURRENT_CAP = "lblCurrentCapacity";
				const string ANCHOR_STANDARD_CAP = "lblStandardCapacity";
				const string ANCHOR_REMAIN_CAP = "lblRemainCapacity";
				const string ANCHOR_EFFECT = "lblEffect";

				const string TOTALROW_SUFFIX = "_Total";

				Field fldWorkCenter = objRB.GetFieldByName(ANCHOR_WORKCENTER);
				Field fldProcessName = objRB.GetFieldByName(ANCHOR_PROCESSNAME);							
				Field fldMachineRun = objRB.GetFieldByName(ANCHOR_MACHINERUN);
				Field fldLaborTime = objRB.GetFieldByName(ANCHOR_LABORTIME);
				Field fldCapacityTotal = objRB.GetFieldByName(ANCHOR_CAPACITYTOTAL);
				Field fldMPFactor = objRB.GetFieldByName(ANCHOR_MANPOWERFACTOR);
				Field fldNecessaryMP = objRB.GetFieldByName(ANCHOR_NECESSARYMANPOWER);
				Field fldCurrentMP = objRB.GetFieldByName(ANCHOR_CURRENTMANPOWER);
				Field fldRemainMP = objRB.GetFieldByName(ANCHOR_REMAINMANPOWER);
				Field fldCurrentCapacity = objRB.GetFieldByName(ANCHOR_CURRENT_CAP);
				Field fldStandardCapacity = objRB.GetFieldByName(ANCHOR_STANDARD_CAP);
				Field fldRemainCapacity = objRB.GetFieldByName(ANCHOR_REMAIN_CAP);
				Field fldProductivity = objRB.GetFieldByName(ANCHOR_EFFECT);

				// get section, draw all in the Header
				Section sec = objRB.GetSectionByType(SectionTypeEnum.Header);
				sec.Visible = true;
				int nNoOfWorkCenter = arrWorkCenterInfo.Count;
				/// each WorkCenter column Width = (width - left header)(no of workcenter + 1 column for total)
				double dblColumnWidth = (fActualPageSize - fldWorkCenter.Width)/(nNoOfWorkCenter + 1);			
	
				int nColIndex = 0;	
				/// for each WorkCenter, draw Info on several row
				foreach(string strWorkCenterCode in arrWorkCenterCode)	/// each WorkCenter, nColIndex will increase
				{
					/// Getting the information for each WorkCenter
					WorkCenterInfo objWCInfo = (WorkCenterInfo)arrWorkCenterInfo[strWorkCenterCode];
					Field fld;		
					Field fldDrawn;
					int nRowIndex = 0;	// nRowIndex will increase for each Row drawn.
					/// each cell will be mark as : nRowIndex+"_"+nColIndex
					string strConvertedStringDecimal = string.Empty;
					
					string FORMAT = ProductionLineCapacityAndManPowerManagementReport.FORMAT_REPORT_NUMBER;

					fld = fldWorkCenter;
					fldDrawn = DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,   objWCInfo.WorkCenterCode,   nColIndex ,   dblColumnWidth,   FieldAlignEnum.CenterMiddle);
					nRowIndex++;

					fld = fldProcessName;
					fldDrawn =DrawPLCMCell(sec,   fld,   nRowIndex+"_"+nColIndex,   objWCInfo.ProcessName,   nColIndex,   dblColumnWidth,   FieldAlignEnum.LeftMiddle);
					nRowIndex++;

					fld = fldMachineRun;
					fldDrawn =DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.MachineRun.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;

					fld = fldLaborTime;
					fldDrawn =DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.LaborTime.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;

					fld = fldCapacityTotal;
					fldDrawn =DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.CapacityTotal.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;

					
					try
					{
						strConvertedStringDecimal = Convert.ToDecimal (objWCInfo.MPFactor).ToString(FORMAT+ "000000");
					}		catch{}
					fld = fldMPFactor;
					fldDrawn =DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  strConvertedStringDecimal ,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;

					try
					{
						strConvertedStringDecimal = Convert.ToDecimal( objWCInfo.NecessaryMP).ToString(FORMAT);
					}		catch{}
					fld = fldNecessaryMP;
					fldDrawn =DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex, strConvertedStringDecimal ,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);					
					nRowIndex++;				

					fld = fldCurrentMP;
					fldDrawn =DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.CurrentMP.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					try
					{
						strConvertedStringDecimal = Convert.ToDecimal(objWCInfo.RemainMP).ToString(FORMAT);
					}		catch{}
					fld = fldRemainMP;
					fldDrawn = DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  strConvertedStringDecimal ,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;
					

					fld = fldCurrentCapacity;
					fldDrawn = DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.CurrentCapacity.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldStandardCapacity;
					fldDrawn = DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.StandardCapacity.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldRemainCapacity;
					fldDrawn = DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.RemainCapacity.ToString(FORMAT),  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldProductivity;
					fldDrawn = DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.Effect,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					nColIndex++;
				}

				#region BUILD TOTAL FIELD
			
				Field fldWorkCenterTotalRow = objRB.GetFieldByName(ANCHOR_WORKCENTER+TOTALROW_SUFFIX);
				Field fldProcessNameTotalRow = objRB.GetFieldByName(ANCHOR_PROCESSNAME+TOTALROW_SUFFIX);				
				Field fldMachineRunTotalRow = objRB.GetFieldByName(ANCHOR_MACHINERUN+TOTALROW_SUFFIX);
				Field fldLaborTimeTotalRow = objRB.GetFieldByName(ANCHOR_LABORTIME+TOTALROW_SUFFIX);
				Field fldCapacityTotalTotalRow = objRB.GetFieldByName(ANCHOR_CAPACITYTOTAL+TOTALROW_SUFFIX);
				Field fldMPFactorTotalRow = objRB.GetFieldByName(ANCHOR_MANPOWERFACTOR+TOTALROW_SUFFIX);			

				Field fldNecessaryMPTotalRow = objRB.GetFieldByName(ANCHOR_NECESSARYMANPOWER+TOTALROW_SUFFIX);
				Field fldCurrentMPTotalRow = objRB.GetFieldByName(ANCHOR_CURRENTMANPOWER+TOTALROW_SUFFIX);
				Field fldRemainMPTotalRow = objRB.GetFieldByName(ANCHOR_REMAINMANPOWER+TOTALROW_SUFFIX);
				Field fldNecessaryMPValue = objRB.GetFieldByName(ANCHOR_NECESSARYMANPOWER);
				Field fldCurrentMPValue = objRB.GetFieldByName(ANCHOR_CURRENTMANPOWER);
				Field fldRemainMPValue = objRB.GetFieldByName(ANCHOR_REMAINMANPOWER);

				Field fldCurrentCapacityTotalRow = objRB.GetFieldByName(ANCHOR_CURRENT_CAP+TOTALROW_SUFFIX);
				Field fldStandardCapacityTotalRow = objRB.GetFieldByName(ANCHOR_STANDARD_CAP+TOTALROW_SUFFIX);
				Field fldRemainCapacityTotalRow = objRB.GetFieldByName(ANCHOR_REMAIN_CAP+TOTALROW_SUFFIX);
				Field fldProductivityTotalRow = objRB.GetFieldByName(ANCHOR_EFFECT+TOTALROW_SUFFIX);

				ArrayList arrTotalRow = new ArrayList();			
				arrTotalRow.Add(fldWorkCenterTotalRow);
				arrTotalRow.Add(fldProcessNameTotalRow);
				arrTotalRow.Add(fldMachineRunTotalRow);
				arrTotalRow.Add(fldLaborTimeTotalRow);
				arrTotalRow.Add(fldCapacityTotalTotalRow);
				arrTotalRow.Add(fldMPFactorTotalRow);	                
				
				
				arrTotalRow.Add(fldNecessaryMPTotalRow);
				arrTotalRow.Add(fldCurrentMPTotalRow);
				arrTotalRow.Add(fldRemainMPTotalRow);

//				arrTotalRow.Add(fldNecessaryMPValue);
//				arrTotalRow.Add(fldCurrentMPValue);
//				arrTotalRow.Add(fldRemainMPValue);


				arrTotalRow.Add(fldCurrentCapacityTotalRow);
				arrTotalRow.Add(fldStandardCapacityTotalRow);
				arrTotalRow.Add(fldRemainCapacityTotalRow);
				arrTotalRow.Add(fldProductivityTotalRow);

				foreach(Field fld in arrTotalRow)
				{
					fld.Calculated = true;
					fld.Width = dblColumnWidth;
					fld.Left = fldWorkCenter.Left + fldWorkCenter.Width + nColIndex*dblColumnWidth;				
					fld.Align = FieldAlignEnum.RightMiddle;
					fld.MarginRight = 30;				
				}


				/// caculate total fields on the report
				if(arrWorkCenterCode.Count > 0)
				{
					decimal dblMachineRunTotal = decimal.Zero;
					decimal dblLaborTimeTotal = decimal.Zero;
					decimal dblCapacityTotalTotal = decimal.Zero;
					decimal dblMPFactorTotal = decimal.Zero;					
					decimal dblNecessaryMPTotal = decimal.Zero;
					decimal dblCurrentMPTotal = decimal.Zero;
					decimal dblRemainMPTotal = decimal.Zero;
					
					foreach(WorkCenterInfo objWCInfo in arrWorkCenterInfo.Values)
					{						
						dblMachineRunTotal += objWCInfo.MachineRun;
						dblLaborTimeTotal += objWCInfo.LaborTime;
						dblCapacityTotalTotal += objWCInfo.CapacityTotal;
						try
						{
							decimal decLooper = Convert.ToDecimal(objWCInfo.MPFactor);	// because of this is a string.
							dblMPFactorTotal += decLooper;
						}catch{}

						try
						{
							decimal decLooper = Convert.ToDecimal(objWCInfo.NecessaryMP);	// because of this is a string.
							dblNecessaryMPTotal += decLooper;
						}		catch{}						
						dblCurrentMPTotal += objWCInfo.CurrentMP;

						try
						{
							decimal decLooper = Convert.ToDecimal(objWCInfo.RemainMP);	// because of this is a string.
							dblRemainMPTotal += decLooper;
						}		catch{}						
					}
			
					fldWorkCenterTotalRow.Align = FieldAlignEnum.CenterMiddle;
					//			fldProcessNameTotalRow = 
					
					// right total cell
//					fldNecessaryMPTotalRow.Text = decimal.Round(dblS1Total,2).ToString();
//					fldCurrentMPTotalRow.Text = decimal.Round(dblS2Total,2).ToString();
//					fldRemainMPTotalRow.Text = decimal.Round(dblS3Total,2).ToString();

					fldMachineRunTotalRow.Value = dblMachineRunTotal;
					fldLaborTimeTotalRow.Value = dblLaborTimeTotal;
					fldCapacityTotalTotalRow.Value = dblCapacityTotalTotal;
					fldMPFactorTotalRow.Value = dblMPFactorTotal;
					fldMPFactorTotalRow.Format = ProductionLineCapacityAndManPowerManagementReport.FORMAT_REPORT_NUMBER + "000000";


					/// left total cell 
					fldNecessaryMPValue.Value = dblNecessaryMPTotal;
					fldCurrentMPValue.Value = dblCurrentMPTotal;
					fldRemainMPValue.Value = dblRemainMPTotal;
					//			fldCurrentCapacityTotalRow = 
					//			fldStandardCapacityTotalRow = 
					//			fldRemainCapacityTotalRow = 
					//			fldProductivityTotalRow = 

				}	// end if(arrWorkCenterCode.Count > 0)
 
				#endregion	//end build total field

				#endregion

				if(dtbWorkCenterList.Rows.Count > 0)
				{
					#region BUILD CHART, save to image in clipboard, and then put in the report field fldChart			

					Field fldChart = objRB.GetFieldByName(REPORTFLD_CHART);
			
					#region	INIT

					//				string APP_PATH = @"D:\PCS Project\07-Construction\Source\PCS\PCSMain\bin\Debug";				
					string EXCEL_REPORT_FOLDER = "ExcelReport";			
					string EXCEL_FILE = "ProductionLineCapacityAndManPowerManagementReport.xls";
			
					//				if( ! Directory.Exists(APP_PATH + Path.DirectorySeparatorChar + 	EXCEL_REPORT_FOLDER) )
					//				{
					//					Directory.CreateDirectory(APP_PATH + Path.DirectorySeparatorChar + 	EXCEL_REPORT_FOLDER);
					//				}

					//				string strTemplateFilePath = APP_PATH + Path.DirectorySeparatorChar + 
					//					REPORT_DEFINITION_FOLDER + Path.DirectorySeparatorChar + 
					//					EXCEL_FILE;
					string strTemplateFilePath = mstrReportDefinitionFolder + Path.DirectorySeparatorChar + EXCEL_FILE;

					//				string strDestinationFilePath = APP_PATH + Path.DirectorySeparatorChar + 
					//					EXCEL_REPORT_FOLDER + Path.DirectorySeparatorChar + 
					//					Path.GetFileNameWithoutExtension(EXCEL_FILE) + FormControlComponents.NowToUTCString() + ".XLS";

					string strDestinationFilePath = mstrReportDefinitionFolder + Path.DirectorySeparatorChar + 
						Path.GetFileNameWithoutExtension(EXCEL_FILE) + FormControlComponents.NowToUTCString() + ".XLS";

					/// Copy layout excel report file to ExcelReport folder with a UTC datetime name
					File.Copy(strTemplateFilePath,	strDestinationFilePath,	true);

					PCSUtils.Utils.ExcelReportBuilder objXLS = new ExcelReportBuilder(strDestinationFilePath);			
			
					#endregion

					try
					{            		        
						#region BUILD THE REPORT ON EXCEL FILE
                
						/// get max number of WOrkCenter on the report:
						/// arrWorkCenterInfo.Count
						///  == number of chart column						
						
						int nNumberOfChartColumn = arrWorkCenterCode.Count;

						string[] arrExcelColumnHeading = new string[nNumberOfChartColumn ];
						double[,] arrCurrent = new double[1,nNumberOfChartColumn];						
						double[,] arrRemain = new double[1,nNumberOfChartColumn];
						double[,] arrStandard = new double[1,nNumberOfChartColumn];
					
						/// getting value
						BuildChartData(arrWorkCenterCode,arrWorkCenterInfo,
							ref arrExcelColumnHeading,
							ref arrCurrent,
							ref arrRemain,
							ref arrStandard
							);

						objXLS.GetRange( objXLS.GetCell(1,1),objXLS.GetCell(1,nNumberOfChartColumn) ).Value2 = arrExcelColumnHeading;

						objXLS.GetRange( objXLS.GetCell(3,1),objXLS.GetCell(3,nNumberOfChartColumn) ).Value2 = arrStandard;	/// standard (now, it's vary from each WorkCenter)
						objXLS.GetRange( objXLS.GetCell(2,1),objXLS.GetCell(2,nNumberOfChartColumn) ).Value2 = arrCurrent;	/// current
						objXLS.GetRange( objXLS.GetCell(4,1),objXLS.GetCell(4,nNumberOfChartColumn) ).Value2 = arrRemain;	/// remain					

						Excel.ChartObject chart = objXLS.GetChart();
						((Excel.Series)chart.Chart.SeriesCollection(1)).Values =  arrCurrent;
						((Excel.Series)chart.Chart.SeriesCollection(2)).Values =  objXLS.GetRange( objXLS.GetCell(3,1),objXLS.GetCell(3,nNumberOfChartColumn) );
						((Excel.Series)chart.Chart.SeriesCollection(3)).Values =  arrRemain;						

						chart.Chart.CopyPicture(Excel.XlPictureAppearance.xlScreen,Excel.XlCopyPictureFormat.xlBitmap,
							Excel.XlPictureAppearance.xlScreen);
						Image image =  (Image)Clipboard.GetDataObject().GetData(typeof(Bitmap));
						/// DEBUG: to view the chart export to image
						/// image.Save("c:\\"+EXCEL_FILE+".chartExcel.Emf",System.Drawing.Imaging.ImageFormat.Emf);

						fldChart.Visible = true;
						fldChart.Text = "";
						fldChart.Picture = image;

						#endregion
			
					}
					catch // (Exception ex)
					{		
						/// TESTED: remove if needed						MessageBox.Show("Can not inter-operate with Excel: " + ex.Message,"Production Control System",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
			}	/// end if having WorkCenter working in this production Line
			
			
			#region MODIFY THE REPORT LAYOUT				
			//objRB.DrawPredefinedField(REPORTFLD_TITLE, lblReportTitle.Text.Trim());			

			#region REM - COMPANY INFO // header information get from system params
			/*
			try
			{
				objRB.DrawPredefinedField(REPORTFLD_COMPANY,SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME));
			}
			catch{}
			try
			{
				objRB.DrawPredefinedField(REPORTFLD_ADDRESS,SystemProperty.SytemParams.Get(SystemParam.ADDRESS));					
			}
			catch{}
			try
			{
				objRB.DrawPredefinedField(REPORTFLD_TEL,SystemProperty.SytemParams.Get(SystemParam.TEL));					
			}
			catch{}
			try
			{
				objRB.DrawPredefinedField(REPORTFLD_FAX,SystemProperty.SytemParams.Get(SystemParam.FAX));					
			}
			catch{}
			*/
			#endregion


			#region DRAW Parameters
				
			System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_CCN).Text, strCCN);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_MONTH).Text, pstrMonth);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_YEAR).Text, pstrYear);			
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_PRODUCTIONLINE).Text, strProductionLine);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_MAINWORKCENTER).Text,strMainWorkCenter);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_BASEITEM).Text,strBaseItem);
			
			
			/// anchor the Parameter drawing canvas cordinate to the fldTitle
			C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
			objRB.GetSectionByName(HEADER).CanGrow = true;
			objRB.DrawParameters( objRB.GetSectionByName(HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

			#endregion			

			#endregion
			
			// ReportBuilder.ReformatNumberInC1Report(objRB.Report);
			objRB.RefreshReport();
			

			/// force the copies number
			printPreview.FormTitle = objRB.GetFieldByName("fldTitle").Text;
			printPreview.Show();
			#endregion
			
			UseReportViewerRenderEngine = false;
			mResult = arrWorkCenterInfo;
			return dtbWorkCenterList;
		}	
		

        /// <summary>
		/// Draw cell in this type of report.
		/// </summary>
		/// <param name="psec"></param>
		/// <param name="pfldAnchor"></param>
		/// <param name="pstrName"></param>
		/// <param name="pobjValue"></param>
		/// <param name="pnColIndex"></param>
		/// <param name="pdblWidth"></param>
		/// <param name="pAlign"></param>
		/// <returns></returns>
		private Field DrawPLCMCell(Section psec, Field pfldAnchor, string pstrName, object pobjValue, int pnColIndex, double pdblWidth, FieldAlignEnum pAlign)
		{	
			// create new field
			Field fldRet = mReportBuilder.DrawField(psec, 
				pstrName,
				pobjValue,
                pfldAnchor.Left + pfldAnchor.Width + pnColIndex*pdblWidth, pfldAnchor.Top,  pdblWidth , pfldAnchor.Height	);
					
			fldRet.Visible = true;

            fldRet.Height = pfldAnchor.Height;
			fldRet.Top = pfldAnchor.Top;

			fldRet.Calculated = false;
			fldRet.WordWrap = true;
			//fldRet.CanGrow = true;			
			fldRet.MarginLeft = 30;
			fldRet.BackColor = Color.White;
			fldRet.BorderStyle = C1.C1Report.BorderStyleEnum.Solid;
			fldRet.BorderColor = Color.Gray;
			//fldRet.CanShrink = true;
			fldRet.Anchor = AnchorEnum.TopAndBottom;
			fldRet.Font.Name = "Arial Narrow";
			fldRet.Font.Size = 5f;			
			//fldRet.Format = ProductionLineCapacityAndManPowerManagementReport.FORMAT_REPORT_NUMBER;

			if(pobjValue.ToString() == WorkCenterInfo.DIVIDE_BY_ZERO_SIGN)
			{
				fldRet.Align = FieldAlignEnum.CenterMiddle;
				return fldRet;
			}

			fldRet.Align = pAlign;
			if(pAlign.ToString().StartsWith("Left"))
			{
				fldRet.MarginLeft = 30;
			}
			if(pAlign.ToString().StartsWith("Right"))
			{
				fldRet.MarginRight = 30;
				// it makes everything going to be 2digit after floating point: fldRet.Text = decimal.Parse(pobjValue.ToString()).ToString("#,##0.00");
				//fldRet.Value = pobjValue.ToString();
			}

			
			return fldRet;
		}
			
		
		private void VoidF1()
		{
		}



		/// <summary>
		/// Get work time of 3 shift in a work order
		/// 1S,2S,3S
		/// Put to 3 output parameters
		/// 
		/// depend on BuildLeadTimeByShiftTable() function
		/// </summary>
		/// <param name="pdtbLeadTimeTable">Lead  Time Table, you should get from BuildLeadTimeByShiftTable() Function</param>
		/// <param name="pnWorkCenterID">WorkCenter ID to analyse</param>
		/// <param name="pdecS1">hold work time of 1S</param>
		/// <param name="pdecS2">hold work time of 2S</param>
		/// <param name="pdecS3">hold work time of 3S</param>
		private void GetShiftWorkTime(DataTable pdtbLeadTimeTable ,int pnWorkCenterID,out decimal pdecS1,out decimal pdecS2,out decimal pdecS3)
		{
			const string SHIFT_ID = "ShiftID";
			const string LEADTIME = "LeadTime";	

			pdecS1 = 0;
			pdecS2 = 0;
			pdecS3 = 0;
		    
			foreach(DataRow drow in pdtbLeadTimeTable.Rows)
			{
				if( (int)drow[WORKCENTERID] == pnWorkCenterID )
				{
					if(drow[SHIFT_ID].ToString() == SHIFT1_ID)
						pdecS1 = decimal.Parse(drow[LEADTIME].ToString());
//					if(drow[SHIFT_ID].ToString() == SHIFT2_ID)
//						pdecS2 = decimal.Parse(drow[LEADTIME].ToString());
//					if(drow[SHIFT_ID].ToString() == SHIFT3_ID)
//						pdecS3 = decimal.Parse(drow[LEADTIME].ToString());					
				}
			}		

		}


		/// <summary>
		/// Get all work time of all shifts in a work order	
		/// SElect all row with Selected WOrkCenterID, sum all the LeadTime (dont care about ShiftID field)		
		/// DEPEND: on BuildLeadTimeByShiftTable() function				
		/// </summary>
		/// <param name="pdtbLeadTimeTable">Lead  Time Table, you should get from BuildLeadTimeByShiftTable() Function</param>
		/// <param name="pnWorkCenterID">WorkCenter ID to analyse</param>		
		/// <returns>decimal value: is the WOrkCenter's CurrentCapacity (sum all LeadTime of all Shifts of current WorkCenter)</returns>
		private decimal GetCurrentCapacityOfWorkCenter(DataTable pdtbLeadTimeTable ,int pnWorkCenterID)
		{			
			const string LEADTIME = "LeadTime";	
		
			decimal decRet = 0;
		    
			foreach(DataRow drow in pdtbLeadTimeTable.Rows)
			{
				if( (int)drow[WORKCENTERID] == pnWorkCenterID )
				{	
					try
					{
						decRet += Convert.ToDecimal(drow[LEADTIME]);
					}
					catch{}
				}
			}

			return decRet;
		} // end function get CurrentCapacity


		/// <summary>
		/// Thachnn: 10/11/2005
		/// execute the input sql clause
		/// return the object result
		/// throw all exception to outside
		/// </summary>
		/// <param name="pstrSql">SQL clause to execute</param>
		/// <returns>object</returns>
		private object ExecuteScalar(string pstrSql)
		{			
			const string METHOD_NAME = THIS + ".ExecuteScalar()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
		
				string strSql = pstrSql;				

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
			
			object objRet = ocmdPCS.ExecuteScalar();
		
			if (oconPCS!=null) 
			{
				if (oconPCS.State != ConnectionState.Closed) 
				{
					oconPCS.Close();
				}
			}	
            
			return objRet;
		}


		/// <summary>		
		/// Return DataTable contain list of WorkCenter in provided ProductionLine
		/// and some meta-info data of each WorkCenter
		/// SCHEMA: 
		/// </summary>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		private DataTable BuildWorkCenterListInProductionLineTable(string pstrMonth, string pstrYear, string pstrProductionLineID, string pstrBaseProductID)
		{				
			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			string pstrCurrentStartDateOfMonth = pstrYear + "-" + pstrMonth +"-" + "01" ;
			string pstrCurrentEndDateOfMonth = pstrYear + "-" + pstrMonth +"-" + DateTime.DaysInMonth(int.Parse(pstrYear), int.Parse(pstrMonth)) ;

			string TABLE_NAME = "WorkCenterListTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
					
" Declare @pstrYear char(4) " + 
" Declare @pstrMonth char(2) " + 
" Declare @pstrProductionLineID int " + 
" Declare @pstrBaseProductID int " + 

" Declare @pstrCurrentStartDateOfMonth datetime " + 
" Declare @pstrCurrentEndDateOfMonth datetime " + 


" Set @pstrYear = '" +pstrYear+ "' " + 
" Set @pstrMonth = '" +pstrMonth+ "' " + 
" Set @pstrProductionLineID = "+pstrProductionLineID+" " + 
" Set @pstrBaseProductID = "+pstrBaseProductID+" " + 

" Set @pstrCurrentStartDateOfMonth = '" +pstrCurrentStartDateOfMonth+ "' " + 
" Set @pstrCurrentEndDateOfMonth = '" +pstrCurrentEndDateOfMonth+ "' " + 
 
 /*----------------- WORK CENTER LIST ------------------*/
 " select distinct  " + 
 " MST_WorkCenter.WorkCenterID as [WorkCenterID],  " + 
 " MST_WorkCenter.Code as [Code],  " + 
 " MST_WorkCenter.Name as [ProcessName],  " + 
 " MST_WorkCenter.IsMain as [IsMain],  " + 
 " /* AVG or MAX if it has many Routings per WorkCenter ? */ " + 
 " isnull(Avg(ITM_Routing.MachineRunTime) , 0) as [MachineRun], " + 
 " isnull(Avg(ITM_Routing.LaborRunTime) , 0) as [LaborRun], " + 
 " isnull(Avg(PRO_WCCapacity.CrewSize), 0 ) as [CurrentMP], " + 
 " IsNull(Min(STANDARDCAPACITY.StandardCapacity) ,0)   as [StandardCapacity], " + 
 " isnull(Avg(PRO_WCCapacity.Factor), 100 ) as [Factor] " +
 "  " + 
 " from  " + 
 " (select * from MST_WorkCenter where ProductionLineID = @pstrProductionLineID) as MST_WorkCenter " + 
 "  " + 
 " left join PRO_WCCapacity    " + 
 " 	on MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID " + 
 " 	and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 "  " + 
 " 	/* Capacity period is intersected with StartMonth and EndMonth period */  " + 
 " 	and  @pstrCurrentStartDateOfMonth <= PRO_WCCapacity.EndDate " + 
 " 	and  @pstrCurrentEndDateOfMonth >= PRO_WCCapacity.BeginDate " + 
 "  " + 
 " left join ITM_Routing " + 
 " 	on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID " + 
 " 	and ITM_Routing.ProductID  = @pstrBaseProductID " + 
 "  " + 
 " left join  " + 
 " ( " + 
 " 	/****** STANDARD CAPACITY **************/ " + 
 " 	select WorkCenterID,  " + 
 " 	/* BeginDate, EndDate,  " + 
 " 	DateDiff(dd, BeginDate, EndDate) + 1 as NoOfDay,  " + 
 " 	Capacity , */ " + 
 " 	Sum ( (DateDiff(dd, BeginDate, EndDate) + 1) * Isnull(Capacity,0) )   as StandardCapacity " + 
 " 	 " + 
 " 	from PRO_WCCapacity " + 
 " 	 " + 
 " 	where  " + 
 " 	    datepart(mm, BeginDate) = @pstrMonth " + 
 " 	and datepart(mm, EndDate) = @pstrMonth " + 
 " 	and datepart(yyyy, BeginDate) = @pstrYear " + 
 " 	and datepart(yyyy, EndDate) = @pstrYear " + 
 " 	 " + 
 " 	group by WorkCenterID " + 
 " 	/** END **** STANDARD CAPACITY **************/ " + 
 " ) as STANDARDCAPACITY " + 
 " 	on MST_WorkCenter.WorkCenterID = STANDARDCAPACITY.WorkCenterID " + 
 "  " + 
 " group by  " + 
 " MST_WorkCenter.WorkCenterID,  " + 
 " MST_WorkCenter.Code,  " + 
 " MST_WorkCenter.Name,  " + 
 " MST_WorkCenter.IsMain  " + 
 "  " + 
 " order by [Code] " +  
 /*----------------- WORK CENTER LIST ------------------*/ 
					"  " ;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbRet = dstPCS.Tables[TABLE_NAME].Copy();
				}
				else
				{
					dtbRet = new DataTable();
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

			return dtbRet;
		}


		/// <summary>
		/// Get the leadtime Table for each shift, of each WorkCenter
		/// Return table will have schema like:
		/// WorkCenterID - LeadTime - ShiftID
		/// 
		/// Ordered by : WOrkCenterID and ShiftID
		/// </summary>
		/// <returns></returns>
		private DataTable BuildLeadTimeByShiftTable(string pstrYear, string pstrMonth, string pstrProductionLineID)
		{	
			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			string LEADTIME_TABLE_NAME = "LeadTimeTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
 " Declare @pstrYear char(4) " + 
 " Declare @pstrMonth char(2) " + 
 " Declare @pstrProductionLineID int " + 
 "  " + 
 " /*-----------------------------------*/ " + 
 " Set @pstrYear = '" +pstrYear+ "' " + 
 " Set @pstrMonth = '" +pstrMonth+ "' " + 
 " Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
 " /*-----------------------------------*/ " +  
 "  " + 
 " SELECT WorkCenterID, ShiftID, " + 
 " SUM([LeadTime]) as [LeadTime] " + 
 " FROM " + 
 "  " + 
 " ( " + 
 " select  " + 
 " isnull(SumPairQuantity_WO.WorkCenterID,SumPairQuantity_CPO.WorkCenterID) as WorkCenterID, " + 
 " isNull(SumPairQuantity_WO.ShiftID, SumPairQuantity_CPO.ShiftID) as ShiftID, " + 
 " isnull(SumPairQuantity_WO.SumPairQuantity_WO,0) + isnull(SumPairQuantity_CPO.SumPartQuantity_CPO,0)  as [LeadTime]/* plus WO and CPO */ " + 
 " from " + 
 " 	(	/* Quantity * WorkOrder-LeadTime */ " + 
 " 	select T_WOLEADTIME.WorkCenterID, T_QUANTITY.ShiftID, T_QUANTITY.WorkOrderDetailID,  Sum(T_QUANTITY.Quantity*T_WOLEADTIME.WOLeadTime ) as SumPairQuantity_WO " + 
 "  " + 
 " 	from  " + 
 " 		( " + 
 " 		/*  Get the WO leadtime table by Work Center and by Shift */ " + 
 " 		/*Parameter: Month, Year, Production Line */ " + 
 " 		select " + 
 " 		PRO_WORouting.WorkCenterID,PRO_WORouting.ProductID, " + 
 " 		[WOLeadTime] =     " + 
 " 		CASE    " + 
 " 		WHEN PRO_WORouting.Type = 1 THEN PRO_WORouting.FixLT    " + 
 " 		WHEN PRO_WORouting.Type = 0 AND PRO_WORouting.Pacer = 'L' THEN PRO_WORouting.LaborSetupTime   + PRO_WORouting.LaborRunTime    " + 
 " 		WHEN PRO_WORouting.Type = 0 AND PRO_WORouting.Pacer = 'M' THEN PRO_WORouting.MachineSetupTime + PRO_WORouting.MachineRunTime     " + 
 " 		WHEN PRO_WORouting.Type = 0 AND PRO_WORouting.Pacer = 'B' THEN PRO_WORouting.MachineSetupTime + PRO_WORouting.MachineRunTime  " + 
 " 									     + PRO_WORouting.LaborSetupTime   + PRO_WORouting.LaborRunTime    " + 
 " 		END " + 
 " 		 " + 
 " 		FROM " + 
 " 		MST_WorkCenter		 " + 
 " 		JOIN PRO_WORouting " + 
 " 		on MST_WOrkCenter.WorkCenterID = PRO_WORouting.WorkCenterID  " + 
 " 		and MST_WorkCenter.ProductionLineID =   @pstrProductionLineID " + 
 "		/*HACKED: add condition: status = 2 ---> WorkOrder was released */ " + 
 "		and PRO_WORouting.WorkOrderDetailID in (Select WorkOrderDetailID from PRO_WorkOrderDetail where Status = 2) " + 
 " 		) as T_WOLEADTIME  " + 
 " 		join  " + 
 " 		(		 " + 
 " 		/* getting the Quantity of the main WorkCenter */ " + 
 " 		/*Parameter: Month, Year, Production Line, " + 
 " 		WorkCenter = Main, " + 
 " 		Shift = (1,2,3) */ " + 
 " 		select  " + 
 " 		PRO_DCPResultMaster.WorkCenterID, " + 
 " 		PRO_DCPResultMaster.WorkOrderDetailID, " + 
 " 		PRO_DCPResultDetail.ShiftID, " + 
 " 		PRO_DCPResultMaster.ProductID, " + 
 " 		PRO_DCPResultDetail.Quantity " + 
 " 		 " + 
 " 		from PRO_DCPResultDetail " + 
 " 		join PRO_DCPResultMaster on PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID " + 
 " 		join MST_WorkCenter on PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID " + 
 " 		 " + 
 " 		where  " + 
 " 		    DatePart(mm  ,PRO_DCPResultDetail.WorkingDate) = @pstrMonth " + 
 " 		and DatePart(yyyy,PRO_DCPResultDetail.WorkingDate) = @pstrYear " + 
 " 		and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 " /*		and MST_WorkCenter.IsMain = 1	*/ " + 
 " 	/*	and ShiftID in (1,2,3)  */  " + 
 " 		) as T_QUANTITY " + 
 "  " + 
 " 	on T_WOLEADTIME.ProductID = T_QUANTITY.ProductID " + 
 " /*	and T_WOLEADTIME.WorkCenterID = T_QUANTITY.WorkCenterID	*/ " + 
 " 	and T_QUANTITY.WorkOrderDetailID is not null " + 
 " 	group by T_WOLEADTIME.WorkCenterID, T_QUANTITY.ShiftID, T_QUANTITY.WorkOrderDetailID " + 
 " 	) as SumPairQuantity_WO " + 
 "  " + 
 " FULL OUTER JOIN " + 
 "  " + 
 " 	(	/* Quantity * CPO-LeadTime */ " + 
 " 	select T_CPOLEADTIME.WorkCenterID, T_QUANTITY.ShiftID, T_QUANTITY.WorkOrderDetailID,  Sum(T_QUANTITY.Quantity*T_CPOLEADTIME.CPOLeadTime ) as SumPartQuantity_CPO " + 
 "  " + 
 " 	from  " + 
 " 		(		 " + 
 " 		/*  Get the CPO leadtime table by Work Center and by Shift */ " + 
 " 		/*Parameter: Month, Year, Production Line */ " + 
 " 		select " + 
 " 		ITM_Routing.WorkCenterID,ITM_Routing.ProductID, " + 
 " 		[CPOLeadTime] =     " + 
 " 		CASE    " + 
 " 		WHEN ITM_Routing.Type = 1 THEN ITM_Routing.FixLT    " + 
 " 		WHEN ITM_Routing.Type = 0 AND ITM_Routing.Pacer = 'L' THEN ITM_Routing.LaborSetupTime   + ITM_Routing.LaborRunTime    " + 
 " 		WHEN ITM_Routing.Type = 0 AND ITM_Routing.Pacer = 'M' THEN ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime     " + 
 " 		WHEN ITM_Routing.Type = 0 AND ITM_Routing.Pacer = 'B' THEN ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime  " + 
 " 									 + ITM_Routing.LaborSetupTime   + ITM_Routing.LaborRunTime    " + 
 " 		END " + 
 " 		 " + 
 " 		FROM " + 
 " 		MST_WorkCenter		 " + 
 " 		JOIN ITM_Routing " + 
 " 		on MST_WOrkCenter.WorkCenterID = ITM_Routing.WorkCenterID  " + 
 " 		and MST_WorkCenter.ProductionLineID =   @pstrProductionLineID " + 
 " 		) as T_CPOLEADTIME  " + 
 " 		join  " + 
 " 		(		 " + 
 " 		/* getting the Quantity of the main WorkCenter */ " + 
 " 		/*Parameter: Month, Year, Production Line, " + 
 " 		WorkCenter = Main, " + 
 " 		Shift = (1,2,3) */ " + 
 " 		select  " + 
 " 		PRO_DCPResultMaster.WorkCenterID, " + 
 " 		PRO_DCPResultMaster.WorkOrderDetailID, " + 
 " 		PRO_DCPResultDetail.ShiftID, " + 
 " 		PRO_DCPResultMaster.ProductID, " + 
 " 		PRO_DCPResultDetail.Quantity " + 
 " 		 " + 
 " 		from PRO_DCPResultDetail " + 
 " 		join PRO_DCPResultMaster on PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID " + 
 " 		join MST_WorkCenter on PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID " + 
 " 		 " + 
 " 		where  " + 
 " 		    DatePart(mm  ,PRO_DCPResultDetail.WorkingDate) = @pstrMonth " + 
 " 		and DatePart(yyyy,PRO_DCPResultDetail.WorkingDate) = @pstrYear " + 
 " 		and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 " /*		and MST_WorkCenter.IsMain = 1	*/ " + 
 " 	/*	and ShiftID in (1,2,3) */  " + 
 " 		) as T_QUANTITY " + 
 "  " + 
 " 	on T_CPOLEADTIME.ProductID = T_QUANTITY.ProductID " + 
 " /*	and T_CPOLEADTIME.WorkCenterID = T_QUANTITY.WorkCenterID	*/ /*we don't care the WC here, so the result will calculate multiple LeadTime of each WC with all Quantity entry*/ " + 
 " 	and T_QUANTITY.WorkOrderDetailID is null " + 
 " 	group by T_CPOLEADTIME.WorkCenterID, T_QUANTITY.ShiftID, T_QUANTITY.WorkOrderDetailID " + 
 " 	) as SumPairQuantity_CPO " + 
 " /* FULL OUTER JOIN */ " + 
 " on  SumPairQuantity_WO.WorkCenterID = SumPairQuantity_CPO.WorkCenterID " + 
 " and SumPairQuantity_WO.ShiftID  = SumPairQuantity_CPO.ShiftID " + 
 "  " + 
 " ) as T_INNER " + 
 " GROUP BY /*Sum on the WOrkCenterID and ShiftID to get the LeadTime of each WC and Shift */ " + 
 " WorkCenterID, ShiftID " 
					;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, LEADTIME_TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbRet = dstPCS.Tables[LEADTIME_TABLE_NAME].Copy();
				}
				else
				{
					dtbRet = new DataTable();
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

			return dtbRet;
		}


		/// <summary>
		/// We need the Summary of product made from MainWorkCenter of Current ProductionLine, 
		/// it is the DCPQuantity (don't care about whether it is converted to WO or not) of the MainWorkCenter in the selected ProductionLine
		/// </summary>
		/// <returns></returns>
		private decimal GetDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			decimal decRet = decimal.Zero;

			// TODO: implement this function to get the Summary of product made from MainWorkCenter of Current ProductionLine
			// it is the DCPQuantity (don't care about whether it is converted to WO or not) of the MainWorkCenter in the selected ProductionLine
			string strSQL = 			
/********* GetDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP **********/
					
				" Declare @pstrCCNID int  " + 
				" Declare @pstrYear char(4) " + 
				" Declare @pstrMonth char(2) " + 
				" Declare @pstrProductionLineID int " +
				" Set @pstrCCNID = " +pstrCCNID+ " " + 
				" Set @pstrYear = '" +pstrYear+ "' " + 
				" Set @pstrMonth = '" +pstrMonth+ "' " + 
				" Set @pstrProductionLineID = "+pstrProductionLineID+" " + 
 "  " + 
 " select  " + 
 " IsNull(Sum( IsNull(MASTER.Quantity,0.00) ), 0.00) " + 
 "  from  " + 
 " PRO_DCPResultMaster as MASTER  " + 
 " join PRO_DCPResultDetail as DETAIL on MASTER.DCPResultMasterID = DETAIL.DCPResultMasterID " + 
 " join MST_WorkCenter as WC on MASTER.WorkCenterID = WC.WorkCenterID " + 
 "  " + 
 " where WC.ProductionLineID = @pstrProductionLineID " + 
 " and WC.IsMain = 1 and WC.CCNID = @pstrCCNID " + 
 " and DatePart(yyyy,DETAIL.WorkingDate) = @pstrYear " + 
 " and DatePart(mm  ,DETAIL.WorkingDate) = @pstrMonth " + 
 "  " 
	 ;
/**END *** GetDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP **********/

			try
			{
				decRet = Convert.ToDecimal(ExecuteScalar(strSQL));
			}
			catch{}
			return decRet;


		}
		

		private decimal GetWorkingDayInMonthOfMainWorkCenter(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			// field names of this  function query
			const string BEGIN_DATE = "BeginDate";
			const string END_DATE = "EndDate";
			const string WORKING_DAY = "WorkingDay";

			decimal decRet = decimal.Zero;
			// TODO: implement this function


			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			string pstrCurrentStartDateOfMonth = pstrYear + "-" + pstrMonth +"-" + "01" ;
			string pstrCurrentEndDateOfMonth = pstrYear + "-" + pstrMonth +"-" + DateTime.DaysInMonth(int.Parse(pstrYear), int.Parse(pstrMonth)) ;

			string TABLE_NAME = "WorkCenterListTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 					
					" Declare @pstrCCNID int " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrProductionLineID int " + 				
					" Declare @pstrCurrentStartDateOfMonth datetime " + 
					" Declare @pstrCurrentEndDateOfMonth datetime " + 

					" Set @pstrCCNID = '" +pstrCCNID+ "' " + 
					" Set @pstrYear = '" +pstrYear+ "' " + 
					" Set @pstrMonth = '" +pstrMonth+ "' " + 
					" Set @pstrProductionLineID = "+pstrProductionLineID+" " +
					" Set @pstrCurrentStartDateOfMonth = '" +pstrCurrentStartDateOfMonth+ "' " + 
					" Set @pstrCurrentEndDateOfMonth = '" +pstrCurrentEndDateOfMonth+ "' " + 
 
 " select " +BEGIN_DATE+ " , " +END_DATE+ " ,  " + 
 " ( (DateDiff(dd, BeginDate, EndDate) + 1)  )   as " +WORKING_DAY+ "  " + 
 "  " + 
 " from PRO_WCCapacity  " + 
 " join MST_WorkCenter  " + 
 " on PRO_WCCapacity.WorkCenterID = MST_WorkCenter.WorkCenterID  " + 
 " and MST_WorkCenter.CCNID = @pstrCCNID " + 
 " where   " + 
 " BeginDate <= '" +pstrCurrentEndDateOfMonth+ "' " + 
 " and EndDate >= '" +pstrCurrentStartDateOfMonth+ "' " + 
 " and MST_WorkCenter.IsMain = 1 " + 
 " and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 "  " + 
 " order by BeginDate, EndDate " + 
			
					"  " ;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbRet = dstPCS.Tables[TABLE_NAME].Copy();
				}
				else
				{
					dtbRet = null;
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

			if(dtbRet != null && dtbRet.Rows.Count > 0)            
			{
				DataRow dtrFirst = dtbRet.Rows[0];
				DataRow dtrLast = dtbRet.Rows[dtbRet.Rows.Count-1];
				DateTime dtmFirstDayOfMonth = new DateTime(int.Parse(pstrYear),int.Parse(pstrMonth),1);
				DateTime dtmLastDayOfMonth = new DateTime(int.Parse(pstrYear),int.Parse(pstrMonth), DateTime.DaysInMonth(int.Parse(pstrYear), int.Parse(pstrMonth))  );

                // process the first row
				if( ((DateTime)dtrFirst[BEGIN_DATE]).CompareTo(dtmFirstDayOfMonth) < 0 )
				{
					dtrFirst[BEGIN_DATE] = dtmFirstDayOfMonth;
					dtrFirst[WORKING_DAY] = ((DateTime)dtrFirst[END_DATE]).Day - ((DateTime)dtrFirst[BEGIN_DATE]).Day + 1;
				}

				// and the, process the last row
				if( ((DateTime)dtrLast[END_DATE]).CompareTo(dtmLastDayOfMonth) > 0 )
				{
					dtrLast[END_DATE] = dtmLastDayOfMonth;
					dtrLast[WORKING_DAY] = ((DateTime)dtrLast[END_DATE]).Day - ((DateTime)dtrLast[BEGIN_DATE]).Day + 1;
				}

				foreach(DataRow drow in dtbRet.Rows)
				{
					try
					{
						decRet += Convert.ToDecimal(drow[WORKING_DAY]);
					}
					catch{}
				}
			}

			return decRet;
		}

		/// <summary>		
		/// Return Reference Info table: we can get the Sum(Change Category + Check Point) from this month
		/// for each WorkCenter, and each Shift
		/// Schema: WorkCenterID - ShiftID - SumChangeCategoryPlusCheckPoint
		/// EXAMPLE:
		/// 8	2	30.00000
		/// 8	3	9.00000		
		/// </summary>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		private DataTable BuildSumChangeCategoryPlusCheckPointTable(string pstrYear, string pstrMonth, string pstrProductionLineID)
		{	
			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			string TABLE_NAME = "SumChangeCategoryPlusCheckPointTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
/* Sum (ChangeCategory+CheckPoint) of main WorkCenter */
/* Parameter: Month, Year, Production Line,
WorkCenter = Main, Shift = (1,2,3) , Type  <> 0 */
/* Type = 1 :  Change category */
/* Type = 2 :  Check Point */

 " Declare @pstrYear char(4) " + 
 " Declare @pstrMonth char(2) " + 
 " Declare @pstrProductionLineID int " + 
 "  " + 
 " /*-----------------------------------*/ " + 
 " Set @pstrYear = '" +pstrYear+ "' " + 
 " Set @pstrMonth = '" +pstrMonth+ "' " + 
 " Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
 " /*-----------------------------------*/ " + 

 " select  " + 
 " PRO_DCPResultMaster.WorkCenterID, " + 
 " PRO_DCPResultDetail.ShiftID , " + 
 " Sum (IsNull(PRO_DCPResultDetail.TotalSecond, 0) ) as [SumChangeCategoryPlusCheckPoint] " + 
 " /* PRO_DCPResultDetail.Type  */ " + 
 "  " + 
 " from PRO_DCPResultDetail " + 
 " join PRO_DCPResultMaster on PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID " + 
 " join MST_WorkCenter on PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID " + 
 "  " + 
 " where  " + 
 "     DatePart(mm  ,PRO_DCPResultDetail.WorkingDate) = @pstrMonth " + 
 " and DatePart(yyyy,PRO_DCPResultDetail.WorkingDate) = @pstrYear " + 
 " and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 " and MST_WorkCenter.IsMain = 1 " + 
 " /* and ShiftID in (1,2,3) */  " + 
 " and Type <> 0 " + 
 "  " + 
 " Group By  " + 
 " PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultDetail.ShiftID  " + 
 "  " 
					;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbRet = dstPCS.Tables[TABLE_NAME].Copy();
				}
				else
				{
					dtbRet = new DataTable();
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

			return dtbRet;
		}


		/// <summary>
		/// Thachnn: 17/01/2006
		/// Depend on the BuildSumChangeCategoryPlusCheckPointTable() declared upper.
		/// <author>Thachnn</author>
		/// </summary>
		/// <param name="pnWorkCenterID"></param>
		/// <param name="pnShiftID"></param>
		/// <param name="pdtbSource"></param>
		/// <returns></returns>
		private decimal GetChangeCategoryPlusCheckPoint(int pnWorkCenterID, int pnShiftID, DataTable pdtbSource)
		{
			decimal decRet = 0;

			DataRow[] drows = pdtbSource.Select("WorkCenterID = " +pnWorkCenterID+ " AND ShiftID = " +pnShiftID );

			foreach(DataRow drow in drows)
			{
				decRet += (decimal)drow["SumChangeCategoryPlusCheckPoint"];
			}

			return decRet;
		}	
        
	

		/// <summary>
		/// Clone the ShiftPattern form, get the Working Time of SHIFT1 (of current CCN).
		///  THis value is use to calculate the MPFactor
		/// </summary>
		/// <param name="pintSHIFT1_ID">Provide the SHIFT_ID (in the database here)</param>
		/// <param name="pintCCNID"></param>
		/// <returns>double value (in second)</returns>
		private double  GetWorkingTimeOfShift1(int pintSHIFT1_ID, int pintCCNID)
		{			
			PRO_ShiftPatternVO voPRO_ShiftPattern = (PRO_ShiftPatternVO) GetShiftPartternByShiftCode(pintSHIFT1_ID, pintCCNID);
			
			TimeSpan tsTotalTime = tsTotalTime = voPRO_ShiftPattern.WorkTimeTo - voPRO_ShiftPattern.WorkTimeFrom;
			TimeSpan tsRegularStop = new TimeSpan();
			TimeSpan tsRefreshing = new TimeSpan();
			TimeSpan tsExtraStop = new TimeSpan();

			DateTime dtmSpecialDay = new DateTime(1,1,1);			
			if ((voPRO_ShiftPattern.RegularStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString()) 
				&& (voPRO_ShiftPattern.RegularStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString()))
			{
				tsRegularStop = voPRO_ShiftPattern.RegularStopTo - voPRO_ShiftPattern.RegularStopFrom;				
			}
			if ((voPRO_ShiftPattern.RefreshingFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString()) 
				&& (voPRO_ShiftPattern.RefreshingTo.ToShortDateString() != dtmSpecialDay.ToShortDateString()))
			{
				tsRefreshing = voPRO_ShiftPattern.RefreshingTo - voPRO_ShiftPattern.RefreshingFrom;				
			}
			if ((voPRO_ShiftPattern.ExtraStopFrom.ToShortDateString() != dtmSpecialDay.ToShortDateString()) 
				&& (voPRO_ShiftPattern.ExtraStopTo.ToShortDateString() != dtmSpecialDay.ToShortDateString()))
			{
				tsExtraStop = voPRO_ShiftPattern.ExtraStopTo - voPRO_ShiftPattern.ExtraStopFrom;				
			}
			TimeSpan tsWorkingTime = new TimeSpan();
			tsWorkingTime = tsTotalTime - (tsRefreshing + tsRegularStop + tsExtraStop);			
			return tsWorkingTime.TotalSeconds;
		}


		/// <summary>
		/// GetShiftPartternByShiftCode
		/// Copy from PRO_ShiftPatternDS.cs
		/// UPdate when need
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <param name="pintShiftID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
		private object GetShiftPartternByShiftCode(int pintShiftID, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			DateTime dtmSpecialDay = new DateTime(1000/01/01);

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;		
			
			string strSql=	"SELECT SHIFTPATTERNID , SHIFTID , CCNID ,COMMENT , " +
				" EFFECTDATEFROM  , EFFECTDATETO , WORKTIMEFROM , WORKTIMETO , " +
				" REGULARSTOPFROM ,  REGULARSTOPTO , REFRESHINGFROM , REFRESHINGTO, " +
				" EXTRASTOPFROM , EXTRASTOPTO " + 
				 " FROM PRO_ShiftPattern " +
				 " WHERE CCNID  = " + pintCCNID + 
				 " AND SHIFTID   = " + pintShiftID;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				PRO_ShiftPatternVO objObject = new PRO_ShiftPatternVO();

			while (odrPCS.Read())
			{ 
				objObject.ShiftPatternID = int.Parse(odrPCS["SHIFTPATTERNID"].ToString().Trim());
				objObject.ShiftID = int.Parse(odrPCS["SHIFTID"].ToString().Trim());
				objObject.CCNID = int.Parse(odrPCS["CCNID"].ToString().Trim());
				objObject.Comment = odrPCS["COMMENT"].ToString().Trim();
				objObject.EffectDateFrom = DateTime.Parse(odrPCS["EFFECTDATEFROM"].ToString().Trim());
				objObject.EffectDateTo = DateTime.Parse(odrPCS["EFFECTDATETO"].ToString().Trim());
				objObject.WorkTimeFrom = DateTime.Parse(odrPCS["WORKTIMEFROM"].ToString().Trim());
				objObject.WorkTimeTo = DateTime.Parse(odrPCS["WORKTIMETO"].ToString().Trim());
				if (odrPCS["REGULARSTOPFROM"].ToString().Trim() != string.Empty)
				{
					objObject.RegularStopFrom = DateTime.Parse(odrPCS["REGULARSTOPFROM"].ToString().Trim());
				}
				else
					objObject.RegularStopFrom = dtmSpecialDay;
				if (odrPCS["REGULARSTOPTO"].ToString().Trim() != string.Empty)
				{
					objObject.RegularStopTo = DateTime.Parse(odrPCS["REGULARSTOPTO"].ToString().Trim());
				}
				else
					objObject.RegularStopTo = dtmSpecialDay;
				if (odrPCS["REFRESHINGFROM"].ToString().Trim() != string.Empty)
				{
					objObject.RefreshingFrom = DateTime.Parse(odrPCS["REFRESHINGFROM"].ToString().Trim());
				}
				else
					objObject.RefreshingFrom = dtmSpecialDay;
				if (odrPCS["REFRESHINGTO"].ToString().Trim() != string.Empty)
				{
					objObject.RefreshingTo = DateTime.Parse(odrPCS["REFRESHINGTO"].ToString().Trim());
				}
				else
					objObject.RefreshingTo = dtmSpecialDay;
				if (odrPCS["EXTRASTOPFROM"].ToString().Trim() != string.Empty)
				{
					objObject.ExtraStopFrom = DateTime.Parse(odrPCS["EXTRASTOPFROM"].ToString().Trim());
				}
				else
					objObject.ExtraStopFrom = dtmSpecialDay;
				if (odrPCS["EXTRASTOPTO"].ToString().Trim() != string.Empty)
				{
					objObject.ExtraStopTo = DateTime.Parse(odrPCS["EXTRASTOPTO"].ToString().Trim());
				}
				else
					objObject.ExtraStopTo = dtmSpecialDay;

			}			
			
			if (oconPCS!=null) 
			{
				if (oconPCS.State != ConnectionState.Closed) 
				{
					oconPCS.Close();
				}
			}

			return objObject;
		}


		/// <summary>
		/// Thachnn: 17/01/2006
		/// Depend on the BuildSumChangeCategoryPlusCheckPointTable() declared upper.
		/// <author>Thachnn</author>
		/// </summary>
		/// <param name="pnWorkCenterID"></param>		
		/// <param name="pdtbSource">ChangeCategoryPlusCheckPoint Table</param>
		/// <returns></returns>
		private decimal GetChangeCategoryPlusCheckPointTotal(int pnWorkCenterID, DataTable pdtbSource)
		{
			decimal decRet = 0;

			DataRow[] drows = pdtbSource.Select("WorkCenterID = " +pnWorkCenterID);

			foreach(DataRow drow in drows)
			{
				decRet += (decimal)drow["SumChangeCategoryPlusCheckPoint"];
			}

			return decRet;
		}	


		
		#region CHART

		// C1.Win.C1Chart.C1Chart _chart;
		const string CHART_WORKCENTER = "Work Center";
		const string CHART_CURRENTCAPACITY = "Current Capacity";
		const string CHART_STANDARDCAPACITY = "Standard Capacity";
		const string CHART_REMAINCAPACITY = "Remain Capacity";

		/// <summary>
		/// /// thachnn: 30/Nov/2005
		/// Build some arrays for render Excel chart
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>void</returns>
		private void BuildChartData(StringCollection parrWorkCenterCode, Hashtable parrWorkCenterInfo, 
			ref string[] parrWorkCenter, 
			ref double[,] parrCurrent, 
			ref double[,] parrRemain, 
			ref double[,] parrStandard
			)
		{
			int nNumberOfChartColumn = parrWorkCenterInfo.Count;

			int i = 0;
			foreach(string strColumnName in parrWorkCenterCode)
			{	
				/// BUILD WorkCenter -Heading of the chart (in the X Axis)
				//parrWorkCenter[i] = parrWorkCenterCode[i];
				parrWorkCenter[i] = strColumnName;

				double dblCurrent = 0.0;
				double dblRemain = 0.0;
				double dblStandard = 0.0;

				/// BUILD CURRENT CAPACITY OF EACH CHART
				try
				{
					dblCurrent = System.Convert.ToDouble(((WorkCenterInfo)parrWorkCenterInfo[strColumnName]).CurrentCapacity);				
				}
				catch{}
				/// BUILD REMAIN CAPACITY OF EACH CHART
				try
				{
					dblRemain = System.Convert.ToDouble(((WorkCenterInfo)parrWorkCenterInfo[strColumnName]).RemainCapacity);
				}
				catch{}
				/// BUILD STANDARD CAPACITY OF EACH CHART
				try
				{
					dblStandard = System.Convert.ToDouble(((WorkCenterInfo)parrWorkCenterInfo[strColumnName]).StandardCapacity);
				}
				catch{}
				
				parrCurrent[0,i] = (dblCurrent > dblStandard ? dblStandard : dblCurrent );
				parrRemain[0,i]  = Math.Abs(dblRemain);
				parrStandard[0,i]  = dblStandard;

				i++;
			}		

			
		}
		

		#endregion

	}





	/// <summary>
	/// Represent an entity contain data for a ProductionLineCapacityAndManPowerManagement report column
	/// </summary>
	struct WorkCenterInfo
	{
		public static string DIVIDE_BY_ZERO_SIGN = " ";

		private int mWorkCenterID;
		private string mWorkCenterCode;
		private string mProcessName;		
		
		private decimal dblMachineRun;
		private decimal dblLaborTime;
		//private decimal dblCapacityTotal;
	
		//private decimal dblMPFactor;

		//private decimal dblNecessaryMP;
		private decimal dblCurrentMP;

		private decimal dblCurrentCapacity;
		private decimal dblStandardCapacity;
		
		private decimal dblFactor;

		private bool blnIsMain;

		private decimal dblS1;
		private decimal dblDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP;
		private decimal dblWorkingDayInMonthOfMainWorkCenter;
		

		/* ------------------------------------------------- */
		public int WorkCenterID
		{
			get
			{
				return mWorkCenterID;
			}
			set
			{
				mWorkCenterID = value;
			}
		}

		public string WorkCenterCode
		{
			get
			{
				return mWorkCenterCode;
			}
			set
			{
				mWorkCenterCode = value;
			}
		}

		public string ProcessName
		{
			get
			{
				return mProcessName;
			}
			set
			{
				mProcessName = value;
			}
		}
		
	
		public decimal MachineRun
		{
			get
			{
				return dblMachineRun;
			}
			set
			{
				dblMachineRun = value;
			}
		}
		public decimal LaborTime
		{
			get
			{
				return dblLaborTime;
			}
			set
			{
				dblLaborTime = value;
			}
		}

		/// <summary>
		///  = MachineRun + LaborTime
		/// </summary>
		public decimal CapacityTotal
		{
			get
			{
				return MachineRun + LaborTime;
			}
		
		}

	

		/// <summary>
		/// LaborTime / Time of Shift 1S		
		/// </summary>
		public string MPFactor
		{
			get
			{
				//return dblMPFactor;
				string str = (S1 != decimal.Zero && Factor != decimal.Zero) ? (  (Decimal)(LaborTime / (S1 * (Factor/100) )   )  ).ToString() : DIVIDE_BY_ZERO_SIGN  ;
				return str;
			}			
		}
		
	
		/// <summary>
		/// = GetDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP() * MPFactor
		/// </summary>
		public string NecessaryMP
		{
			get
			{
				if(MPFactor != DIVIDE_BY_ZERO_SIGN)
				{
					return Convert.ToDecimal(  
						dblDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP 
						* Convert.ToDecimal(MPFactor) 
						/ dblWorkingDayInMonthOfMainWorkCenter
						).ToString() ;
				}
				else
				{
					return MPFactor;
				}
			}
		}


		/// <summary>
		/// Crew size of this WorkCenter
		/// </summary>
		public decimal CurrentMP
		{
			get
			{			
				return dblCurrentMP;
			}
			set
			{
				dblCurrentMP = value;
			}
		}

		/// <summary>
		/// return NecessaryMP - CurrentMP;
		/// </summary>
		public string RemainMP
		{
			get
			{			
				try
				{
					return (  CurrentMP - System.Convert.ToDecimal(NecessaryMP) ).ToString();
				}
				catch
				{
					return DIVIDE_BY_ZERO_SIGN;
				}
			}		
		}



		/// <summary>	
		/// 		
		/// </summary>
		public decimal CurrentCapacity
		{
			get
			{				
				return dblCurrentCapacity;
			}
			set
			{
				dblCurrentCapacity = value;
			}
		}

		/// <summary>
		/// meta info		
		/// </summary>
		public decimal StandardCapacity
		{			
			get
			{
				return dblStandardCapacity;
			}
			set
			{
				dblStandardCapacity = value;
			}
		}

		/// <summary>
		/// meta info		
		/// </summary>
		public decimal Factor
		{			
			get
			{
				return dblFactor;
			}
			set
			{
				dblFactor = value;
			}
		}




		/// <summary>
		/// While get, we return CurrentCapacity - StandardCapacity;
		/// </summary>
		public decimal RemainCapacity
		{
			get
			{
				return StandardCapacity - CurrentCapacity;
			}
		}

		/// <summary>		
		/// While get, we return (StandardCapacity/CurrentCapacity)*100;
		/// </summary>
		public string Effect
		{
			get
			{
				if(CurrentCapacity != 0)
				{
					return decimal.Round(( StandardCapacity / CurrentCapacity  )*100, 2).ToString();
				}
				else return DIVIDE_BY_ZERO_SIGN;
			}			
		}



		/// <summary>
		/// meta info
		/// </summary>
		public bool IsMain
		{
			get
			{
				return blnIsMain;
			}
			set
			{
				blnIsMain = value;
			}
		}




		/// <summary>
		/// this member is use only for calculating MPFactor
		/// </summary>
		public decimal S1
		{
			get
			{
				return dblS1;
			}
			set
			{
				dblS1 = value;
			}
		}
	
		/// <summary>
		/// this member is use only for calculate the NecessaryMP
		/// </summary>
		public decimal DCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP
		{
			get
			{
				return dblDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP;
			}
			set
			{
				dblDCPQuantityOfMainWorkCenterInThisProductionLine_ForNecessaryMP = value;
			}
		}

		/// <summary>
		/// this member is use only for calculate the NecessaryMP
		/// </summary>
		public decimal WorkingDayInMonthOfMainWorkCenter
		{
			get
			{
				return dblWorkingDayInMonthOfMainWorkCenter;
			}
			set
			{
				dblWorkingDayInMonthOfMainWorkCenter = value;
			}
		}



	}



	/// <summary>
	/// This VO class is copied from the PCSComProduction
	/// </summary>
	public class PRO_ShiftPatternVO
	{
		private int mShiftPatternID;
		private int mShiftID;
		private int mCCNID;
		private string mComment;
		private DateTime mEffectDateFrom;
		private DateTime mEffectDateTo;
		private DateTime mWorkTimeFrom;
		private DateTime mWorkTimeTo;
		private DateTime mRegularStopFrom;
		private DateTime mRegularStopTo;
		private DateTime mRefreshingFrom;
		private DateTime mRefreshingTo;
		private DateTime mExtraStopFrom;
		private DateTime mExtraStopTo;

		public int ShiftPatternID
		{
			set { mShiftPatternID = value; }
			get { return mShiftPatternID; }
		}
		public int ShiftID
		{
			set { mShiftID = value; }
			get { return mShiftID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public DateTime EffectDateFrom
		{
			set { mEffectDateFrom = value; }
			get { return mEffectDateFrom; }
		}
		public DateTime EffectDateTo
		{
			set { mEffectDateTo = value; }
			get { return mEffectDateTo; }
		}
		public DateTime WorkTimeFrom
		{
			set { mWorkTimeFrom = value; }
			get { return mWorkTimeFrom; }
		}
		public DateTime WorkTimeTo
		{
			set { mWorkTimeTo = value; }
			get { return mWorkTimeTo; }
		}
		public DateTime RegularStopFrom
		{
			set { mRegularStopFrom = value; }
			get { return mRegularStopFrom; }
		}
		public DateTime RegularStopTo
		{
			set { mRegularStopTo = value; }
			get { return mRegularStopTo; }
		}
		public DateTime RefreshingFrom
		{
			set { mRefreshingFrom = value; }
			get { return mRefreshingFrom; }
		}
		public DateTime RefreshingTo
		{
			set { mRefreshingTo = value; }
			get { return mRefreshingTo; }
		}
		public DateTime ExtraStopFrom
		{
			set { mExtraStopFrom = value; }
			get { return mExtraStopFrom; }
		}
		public DateTime ExtraStopTo
		{
			set { mExtraStopTo = value; }
			get { return mExtraStopTo; }
		}
	}
}

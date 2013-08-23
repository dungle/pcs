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


namespace ProductionLineCapacityManagementReport
{
	/// <summary>
	/// Thachnn: CONCEPT to build this report
	/// 
	/// </summary>
	[Serializable]	
	public class ProductionLineCapacityManagementReport : MarshalByRefObject, IDynamicReport
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

		

		public ProductionLineCapacityManagementReport()
		{
		}		

		
	
		const string THIS = "ExternalReportFile:ProductionLineCapacityManagementReport";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "ProductionLineCapacityManagementReport";			
		const string ZERO_STRING = "0";
		
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		const string NEXTMONTH_DATE_FORMAT = "MMM-yy";



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
		const string LEADTIME = "LeadTime";
		const string ISMAIN = "IsMain";
		//const string SHIFT = "Shift";			

		const string HEADER = "Header";



		const string SHIFT1_ID = "1";
		const string SHIFT2_ID = "2";
		const string SHIFT3_ID = "3";



		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			#region My variables				

			/// Report layout file constant
			const string REPORT_LAYOUT_FILE = "ProductionLineCapacityManagementReport.xml";
			const string REPORT_NAME = "ProductionLineCapacityManagementReport";
			short COPIES = 1;

			/// all parameter are Mandatory
			const string REPORTFLD_PARAMETER_MONTH				= "lblMonth";
			const string REPORTFLD_PARAMETER_YEAR				= "lblYear";			
			const string REPORTFLD_PARAMETER_PRODUCTIONLINE		= "lblProductionLine";			
			

			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);			
			int nProductionLineID = int.Parse(pstrProductionLineID);
			
			
			string strMonth = string.Empty;
			string strYear = string.Empty;
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
			//strCCN = boUtil.GetCCNCodeFromID(nCCNID);	
			strProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + ": " + objBO.GetProductLineNameFromID(nProductionLineID);					
			
			#endregion

						
			#region BUILD some RAW DATA TABLE
			
			//// Build the WorkCenterList table
			dtbWorkCenterList = BuildWorkCenterListInProductionLineTable(pstrYear,pstrMonth,pstrProductionLineID);

			/// Build the leadtime table.Data is provided by WorkCenter and for each Shift
			dtbLeadTimeTable = BuildLeadTimeByShiftTable(  pstrYear, pstrMonth, pstrProductionLineID 	);

			/// build the datatable contain (Change category + check point) list for all (WorkCenter - Shift)
			dtbSumChangeCategoryPlusCheckPoint = BuildSumChangeCategoryPlusCheckPointTable(  pstrYear, pstrMonth, pstrProductionLineID 	);

			#endregion BUILD some RAW DATA TABLE			


			#region BUILD DATA OF REPORT

			#region BUILD RESULT TABLE SCHEMA
			/// Build array contain meta data info (WC COde, Process Name, MachineName to build the first 3 row of the Report)			
			/// build the meta info of each WorkCenter in arrWorkCenterInfo
			foreach(DataRow drow in dtbWorkCenterList.Rows)
			{
				WorkCenterInfo objInfo = new WorkCenterInfo();
				objInfo.WorkCenterID = (int)drow[WORKCENTERID];
				objInfo.WorkCenterCode = drow[CODE].ToString();
				objInfo.ProcessName = drow[PROCESSNAME].ToString();				
				arrWorkCenterInfo.Add(drow[CODE].ToString(),objInfo);

				arrWorkCenterCode.Add(drow[CODE].ToString());
			}           
			
			#endregion

			#region FILL DATA

			decimal dblStandardCapacity = decimal.One;

			string strFilter;
			string strSort;
			foreach(string strWorkCenterCode in arrWorkCenterCode)
			{
				WorkCenterInfo objWCInfo = (WorkCenterInfo)arrWorkCenterInfo[strWorkCenterCode];
				strFilter = string.Format("[{0}]='{1}'",
					CODE,
					strWorkCenterCode);
				//OLD: using SHIFT: strSort = string.Format("[{0}] ASC, [{1}] ASC",CODE , SHIFT);
				strSort = string.Format("[{0}] ASC",CODE);
				DataRow[] arrRowSelectedWorkCenters = dtbWorkCenterList.Select(strFilter,strSort);

				decimal decS1,decS2,decS3;
				GetShiftWorkTime(dtbLeadTimeTable, objWCInfo.WorkCenterID, out decS1, out decS2, out decS3);
				
				/// Sum(WOLeadTime*Quantity) + Sum(CPOLeadTime*Quantity) + ChangeCategoryPlusCheckPoint
				objWCInfo.S1 = decS1 + GetChangeCategoryPlusCheckPoint(objWCInfo.WorkCenterID ,int.Parse(SHIFT1_ID),dtbSumChangeCategoryPlusCheckPoint);
				objWCInfo.S2 = decS2 + GetChangeCategoryPlusCheckPoint(objWCInfo.WorkCenterID ,int.Parse(SHIFT2_ID),dtbSumChangeCategoryPlusCheckPoint);;
				objWCInfo.S3 = decS3 + GetChangeCategoryPlusCheckPoint(objWCInfo.WorkCenterID ,int.Parse(SHIFT3_ID),dtbSumChangeCategoryPlusCheckPoint);;

				foreach(DataRow drow in arrRowSelectedWorkCenters)
				{					
//					objWCInfo.Machine = (decimal)drow[MACHINERUN]+(decimal)drow[MACHINESETUP];
//					objWCInfo.Labor= (decimal)drow[LABORRUN]+(decimal)drow[LABORSETUP];
//					objWCInfo.LeadTime = (decimal)drow[LEADTIME];
					objWCInfo.IsMain = false;
					try
					{
						objWCInfo.IsMain = Convert.ToBoolean(drow[ISMAIN]);
					}
					catch{}
					// NONEED: objWCInfo.StandardCapacity = dblStandardCapacity;
				}
				arrWorkCenterInfo[objWCInfo.WorkCenterCode] = objWCInfo; // reassign to the HashTable. Don't remove
			}			

			/// recalculate the  Standard Capacity by = Sum (S1 S2 S3) of the Main WorkCenter
			dblStandardCapacity = GetStandardCapacity(arrWorkCenterInfo);
			/// set all Standard Capacity of all WOrkCenter to the value recent get = dblStandardCapacity 			
			foreach(string strWorkCenterCode in arrWorkCenterCode)
			{
				WorkCenterInfo objWCInfo = (WorkCenterInfo)arrWorkCenterInfo[strWorkCenterCode];

				objWCInfo.StandardCapacity = dblStandardCapacity;

				arrWorkCenterInfo[objWCInfo.WorkCenterCode] = objWCInfo; // reassign to the HashTable. Don't remove
			}
						


			#endregion
		
			#endregion
			
			

			#region RENDER REPORT
			
			ReportBuilder objRB = new ReportBuilder();				
			objRB.ReportName = REPORT_NAME;
			objRB.SourceDataTable = dtbRet = new DataTable();	// we build report base on HashTable, not DataTable, so we put new DataTable in to ReportBuilder to avoid error
			
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

			

			if(dtbWorkCenterList.Rows.Count > 0)
			{
				#region BUILD THE REPORT
				/// Anchor field to draw
				const string ANCHOR_WORKCENTER = "lblWorkCenter";
				const string ANCHOR_PROCESSNAME= "lblProcessName";			
				const string ANCHOR_S1 = "lblS1";
				const string ANCHOR_S2 = "lblS2";
				const string ANCHOR_S3 = "lblS3";
				const string ANCHOR_CURRENT_CAP = "lblCurrentCapacity";
				const string ANCHOR_STANDARD_CAP = "lblStandardCapacity";
				const string ANCHOR_REMAIN_CAP = "lblRemainCapacity";
				const string ANCHOR_PRODUCTIVITY = "lblProductivity";

				const string TOTALROW_SUFFIX = "1";

				Field fldWorkCenter = objRB.GetFieldByName(ANCHOR_WORKCENTER);
				Field fldProcessName = objRB.GetFieldByName(ANCHOR_PROCESSNAME);			
				/// Thachnn: DELETED: modify usecase
				Field fldS1 = objRB.GetFieldByName(ANCHOR_S1);
				Field fldS2 = objRB.GetFieldByName(ANCHOR_S2);
				Field fldS3 = objRB.GetFieldByName(ANCHOR_S3);
				Field fldCurrentCapacity = objRB.GetFieldByName(ANCHOR_CURRENT_CAP);
				Field fldStandardCapacity = objRB.GetFieldByName(ANCHOR_STANDARD_CAP);
				Field fldRemainCapacity = objRB.GetFieldByName(ANCHOR_REMAIN_CAP);
				Field fldProductivity = objRB.GetFieldByName(ANCHOR_PRODUCTIVITY);

				// get section, draw all in the Header
				Section sec = objRB.GetSectionByType(SectionTypeEnum.Header);
				sec.Visible = true;
				int nNoOfWorkCenter = arrWorkCenterInfo.Count;
				/// each WorkCenter column Width = (width - left header)(no of workcenter + 1 column for total)
				double dblColumnWidth = (fActualPageSize - fldWorkCenter.Width)/(nNoOfWorkCenter + 1);			
	
				int nColIndex = 0;	
				/// for each WorkCenter, draw Info on several row
				foreach(string strWorkCenterCode in arrWorkCenterCode)
				{
					/// Getting the information for each WorkCenter
					WorkCenterInfo objWCInfo = (WorkCenterInfo)arrWorkCenterInfo[strWorkCenterCode];
					Field fld;								
					int nRowIndex = 0;

					fld = fldWorkCenter;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,   objWCInfo.WorkCenterCode,   nColIndex ,   dblColumnWidth,   FieldAlignEnum.CenterMiddle);
					nRowIndex++;

					fld = fldProcessName;
					DrawPLCMCell(sec,   fld,   nRowIndex+"_"+nColIndex,   objWCInfo.ProcessName,   nColIndex,   dblColumnWidth,   FieldAlignEnum.LeftMiddle);
					nRowIndex++;												

					fld = fldS1;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.S1,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldS2;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.S2,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldS3;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.S3,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldCurrentCapacity;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.CurrentCapacity,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldStandardCapacity;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.StandardCapacity,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldRemainCapacity;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.RemainCapacity,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					fld = fldProductivity;
					DrawPLCMCell(sec,  fld,  nRowIndex+"_"+nColIndex,  objWCInfo.Productivity,  nColIndex,  dblColumnWidth,  FieldAlignEnum.RightMiddle);
					nRowIndex++;				

					nColIndex++;
				}

				#region BUILD TOTAL FIELD
			
				Field fldWorkCenterTotalRow = objRB.GetFieldByName(ANCHOR_WORKCENTER+TOTALROW_SUFFIX);
				Field fldProcessNameTotalRow = objRB.GetFieldByName(ANCHOR_PROCESSNAME+TOTALROW_SUFFIX);
				/// Thachnn: DELETED: modify usecase
				Field fldS1TotalRow = objRB.GetFieldByName(ANCHOR_S1+TOTALROW_SUFFIX);
				Field fldS2TotalRow = objRB.GetFieldByName(ANCHOR_S2+TOTALROW_SUFFIX);
				Field fldS3TotalRow = objRB.GetFieldByName(ANCHOR_S3+TOTALROW_SUFFIX);
				Field fldCurrentCapacityTotalRow = objRB.GetFieldByName(ANCHOR_CURRENT_CAP+TOTALROW_SUFFIX);
				Field fldStandardCapacityTotalRow = objRB.GetFieldByName(ANCHOR_STANDARD_CAP+TOTALROW_SUFFIX);
				Field fldRemainCapacityTotalRow = objRB.GetFieldByName(ANCHOR_REMAIN_CAP+TOTALROW_SUFFIX);
				Field fldProductivityTotalRow = objRB.GetFieldByName(ANCHOR_PRODUCTIVITY+TOTALROW_SUFFIX);

				ArrayList arrTotalRow = new ArrayList();			
				arrTotalRow.Add(fldWorkCenterTotalRow);
				arrTotalRow.Add(fldProcessNameTotalRow);
                /// Thachnn: delete, because of using new usecase				
				arrTotalRow.Add(fldS1TotalRow);
				arrTotalRow.Add(fldS2TotalRow);
				arrTotalRow.Add(fldS3TotalRow);
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


				if(arrWorkCenterCode.Count > 0)
				{
					//Thachnn: change usecase

					/// get the first WorkCenter S 1 2 3 values
					decimal dblS1Total = ((WorkCenterInfo) arrWorkCenterInfo[arrWorkCenterCode[0]]).S1;
					decimal dblS2Total = ((WorkCenterInfo) arrWorkCenterInfo[arrWorkCenterCode[0]]).S2;
					decimal dblS3Total = ((WorkCenterInfo) arrWorkCenterInfo[arrWorkCenterCode[0]]).S3;

					/// get the min of S1 2 3 of all work center
					foreach(WorkCenterInfo objWCInfo in arrWorkCenterInfo.Values)
					{
						//Thachnn: change usecase						
						dblS1Total = Math.Min(dblS1Total, objWCInfo.S1);
						dblS2Total = Math.Min(dblS2Total, objWCInfo.S2);
						dblS3Total = Math.Min(dblS3Total, objWCInfo.S3);
					}
			
					fldWorkCenterTotalRow.Align = FieldAlignEnum.CenterMiddle;
					//			fldProcessNameTotalRow = 
					/// Thachnn: delete, because of using new usecase
					fldS1TotalRow.Text = decimal.Round(dblS1Total,2).ToString();
					fldS1TotalRow.Format= "#,##0.00";
					fldS2TotalRow.Text = decimal.Round(dblS2Total,2).ToString();
					fldS2TotalRow.Format= "#,##0.00";
					fldS3TotalRow.Text = decimal.Round(dblS3Total,2).ToString();
					fldS3TotalRow.Format= "#,##0.00";
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
					string EXCEL_FILE = "ProductionLineCapacityManagementReport.xls";
			
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
					
						/// getting value
						BuildChartData(arrWorkCenterCode,arrWorkCenterInfo,
							ref arrExcelColumnHeading,
							ref arrCurrent,
							ref arrRemain);

						objXLS.GetRange( objXLS.GetCell(1,1),objXLS.GetCell(1,nNumberOfChartColumn) ).Value2 = arrExcelColumnHeading;
						objXLS.GetRange( objXLS.GetCell(3,1),objXLS.GetCell(3,nNumberOfChartColumn) ).Value2 = dblStandardCapacity;

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
					catch(Exception ex)
					{		
						/// : Test: remove if needed
						MessageBox.Show("Can not inter-operate with Excel: " + ex.Message,"Production Control System",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
				
			}

			
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
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_MONTH).Text, pstrMonth);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_YEAR).Text, pstrYear);
			//arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_CCN).Text,strCCN);			
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_PRODUCTIONLINE).Text, strProductionLine);
			
			
			/// anchor the Parameter drawing canvas cordinate to the fldTitle
			C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
			objRB.GetSectionByName(HEADER).CanGrow = true;
			objRB.DrawParameters( objRB.GetSectionByName(HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

			#endregion			


			#region REM - RENAME THE COLUMN HEADING TEXT
			/*
			ArrayList arrColumnHeadings = new ArrayList();				
			for(int i = 0; i <= 31; i++) /// clear the heading text
			{
				objRB.DrawPredefinedField(COL_PREFIX+i.ToString("00")+"Lbl","");
			}

			for(int i = 0; i <= 31; i++)
			{
				/// Paint the EMPTY Colummn to AQUA
				try
				{
					if(arrDueDateHeading.Contains(COL_PREFIX+i.ToString("00"))   )
					{
						string strHeading = arrDayNumberMapToDayWithDayOfWeek[i.ToString("00")].Substring(0,6);
						objRB.DrawPredefinedField(COL_PREFIX+i.ToString("00")+"Lbl",strHeading);
					}
					else
					{
						string strHeading = arrFullDayNumberMapToDayWithDayOfWeek[i.ToString("00")].Substring(0,6);
						objRB.DrawPredefinedField(COL_PREFIX+i.ToString("00")+"Lbl",strHeading);
						objRB.Report.Fields[COL_PREFIX+i.ToString("00")+"Lbl"].BackColor = Color.WhiteSmoke;
					}
				}
				catch	// draw continue, don't care about error value in the parrValuesToFill
				{
					//break;
				}
				/// Paint the WEEKEND Colummn to Blue
				try
				{
					if(objRB.Report.Fields[COL_PREFIX+i.ToString("00")+"Lbl"] != null)
					{
						/// this variable contain sat, sun, mon tue, ...
						string strDateName = objRB.GetFieldByName(COL_PREFIX+i.ToString("00")+"Lbl").Text.Substring(3,3);
						if(strDateName == "Sat" || strDateName == "Sun")
						{
							objRB.Report.Fields[COL_PREFIX+i.ToString("00")+"Lbl"].BackColor = Color.Yellow;
							objRB.Report.Fields[COL_PREFIX+i.ToString("00")+"Lbl"].ForeColor = Color.Red;
						}
					}
				}
				catch	// draw continue, don't care about error value in the parrValuesToFill
				{
					//break;
				}
			}		
			*/
			#endregion
			
			#endregion
			
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
				fldRet.Text = decimal.Parse(pobjValue.ToString()).ToString("#,##0.00");
			}

			///fldRet.Format = voReportField.Format;
			return fldRet;
		}
			
		

		/// <summary>
		/// Thachnn: 29/11/2005
		/// Get standard Capacity by:
		/// Looping in the HashTable WorkCenterInfo
		/// Check the main WorkCenter
		/// Sum S1 + S2 +S3 of Main WorkCenter		
		/// </summary>
		/// <param name="parrWorkCenterInfo"></param>
		/// <returns></returns>
		private decimal GetStandardCapacity(Hashtable parrWorkCenterInfo)
		{		
			decimal dblRet = decimal.Zero;
		
			foreach (DictionaryEntry objEntry in parrWorkCenterInfo)
			{
				WorkCenterInfo objWCInfo = (WorkCenterInfo)objEntry.Value;
				if(objWCInfo.IsMain)
				{
					dblRet = objWCInfo.S1 + objWCInfo.S2 + objWCInfo.S3;
					break;	// SPEED:
				}
			}

			return dblRet;
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
					if(drow[SHIFT_ID].ToString() == SHIFT2_ID)
						pdecS2 = decimal.Parse(drow[LEADTIME].ToString());
					if(drow[SHIFT_ID].ToString() == SHIFT3_ID)
						pdecS3 = decimal.Parse(drow[LEADTIME].ToString());					
				}
			}		

		}


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
				return ocmdPCS.ExecuteScalar();				
		
			if (oconPCS!=null) 
			{
				if (oconPCS.State != ConnectionState.Closed) 
				{
					oconPCS.Close();
				}
			}			
		}




		#region TEST CHART: Ugly , C1 FAIL
		


		C1.Win.C1Chart.C1Chart _chart;
		const string CHART_WORKCENTER = "Work Center";
		const string CHART_CURRENTCAPACITY = "Current Capacity";
		const string CHART_STANDARDCAPACITY = "Standard Capacity";
		const string CHART_REMAINCAPACITY = "Remain Capacity";

		// copy data from a data source to the chart
		// c1c          chart
		// series       index of the series to bind (0-based, will add if necessary)
		// datasource   datasource object (cannot be DataTable, DataView is OK)
		// field        name of the field that contains the y values
		// labels       name of the field that contains the x labels
		private void BindSeries(C1Chart c1c, int series, object dataSource, string field, string labels)
		{
			// check data source object
			ITypedList il = (ITypedList)dataSource;
			IList list = (IList)dataSource;
			if (list == null || il == null) 
				throw new ApplicationException("Invalid DataSource object.");

			// add series if necessary
			ChartDataSeriesCollection coll = c1c.ChartGroups[0].ChartData.SeriesList;
			while (series >= coll.Count)
				coll.AddNewSeries();

			// copy series data
			if (list.Count == 0) return;
			PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), list.Count);
			PropertyDescriptorCollection pdc = il.GetItemProperties(null);
			PropertyDescriptor pd = pdc[field];
			if (pd == null) 
				throw new ApplicationException(string.Format("Invalid field name used for Y values ({0}).", field));

			int i;
			for (i = 0; i < list.Count; i++)
			{
				data[i].X = i;
				try
				{
					data[i].Y = float.Parse(pd.GetValue(list[i]).ToString());
				}
				catch
				{
					data[i].Y = float.NaN;
				}
				coll[series].PointData.CopyDataIn(data);
				coll[series].Label = field;
			}

			// copy series labels
			if (labels != null && labels.Length > 0)
			{
				pd = pdc[labels];
				if (pd == null) 
					throw new ApplicationException(string.Format("Invalid field name used for X values ({0}).", labels));
				Axis ax = c1c.ChartArea.AxisX;
				ax.ValueLabels.Clear();
				for (i = 0; i < list.Count; i++)
				{
					string label = pd.GetValue(list[i]).ToString();
					ax.ValueLabels.Add(i, label);
				}
				ax.AnnoMethod = AnnotationMethodEnum.ValueLabels;
			}
		}
		private void BindSeries(C1Chart c1c, int series, object dataSource, string field)
		{
			BindSeries(c1c, series, dataSource, field, null);
		}
	

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
			ref double[,] parrRemain
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
				
				parrCurrent[0,i] = dblCurrent;
				parrRemain[0,i]  = dblRemain;

				i++;
			}		

			
		}
		

		#endregion



		/// <summary>		
		/// Return DataTable contain list of WorkCenter in provided ProductionLine
		/// </summary>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		private DataTable BuildWorkCenterListInProductionLineTable(string pstrYear, string pstrMonth, string pstrProductionLineID)
		{	
			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			string TABLE_NAME = "WorkCenterListTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
					" select      " + 
					" MST_WorkCenter.WorkCenterID,  " + 
					" MST_WorkCenter.IsMain as [IsMain],    " + 
					" MST_WorkCenter.Code as [Code],    " + 
					" MST_WorkCenter.Name as [ProcessName] " + 
					"  " + 
					" from     " + 
					" MST_WorkCenter    " + 
					" join PRO_ProductionLine    " + 
					" on MST_WOrkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID     " + 
					"  " + 
					" WHERE  " + 
					" PRO_ProductionLine.ProductionLineID =   " + pstrProductionLineID + " " + 						
					"  " + 
					" order by [Code]  " + 
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
 " 		and ShiftID in (1,2,3) " + 
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
 " 		and ShiftID in (1,2,3) " + 
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
 " and ShiftID in (1,2,3) " + 
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
        
	}



	/// <summary>
	/// Represent an entity contain data for a ProductionLineCapacityManagement report column
	/// </summary>
	struct WorkCenterInfo
	{
		public static string DIVIDE_BY_ZERO_SIGN = "-";

		private int mWorkCenterID;
		private string mWorkCenterCode;
		private string mProcessName;		
	
		private decimal dblS1;
		private decimal dblS2;
		private decimal dblS3;
		private decimal dblCurrentCapacity;
		private decimal dblStandardCapacity;
//		private decimal dblRemainCapacity;
//		private decimal dblProductivity;
		private bool blnIsMain;


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
		
	
		
		

		/// <summary>
		/// TODO: change usecase  - re caculate S1 
		///Tong so nagn luc yeu cau cua tat ca cac item di qua Work Center nay (tinh theo ca 1)		
		///	Sum(Quantity * LeadTime) + Change category + Check point			
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
		/// TODO: change usecase  - re caculate S2
		///Tong so nagn luc yeu cau cua tat ca cac item di qua Work Center nay (tinh theo ca 1)		
		///Sum(Quantity * LeadTime) + Change category + Check point			
		/// </summary>
		public decimal S2
		{
			get
			{			
				return dblS2;
			}
			set
			{
				dblS2 = value;
			}
		}

		/// <summary>
		/// TODO: change usecase  - re caculate S3
		///Tong so nagn luc yeu cau cua tat ca cac item di qua Work Center nay (tinh theo ca 1)		
		///	Sum(Quantity * LeadTime) + Change category + Check point			
		/// </summary>
		public decimal S3
		{
			get
			{			
				return dblS3;
			}
			set
			{
				dblS3 = value;
			}
		}


		/// <summary>
		/// While get, we return dblS1 + dblS2 + dblS3
		/// </summary>
		public decimal CurrentCapacity
		{
			get
			{
				return decimal.Round(S1 + S2 + S3, 2);
			}
//			set
//			{
//				dblCurrentCapacity = value;
//			}
		}

		/// <summary>
		/// Stadard capacity , is the S1 + S2 + S3 of the Main WorkCenter
		/// Must Caculate before getting result ( use GetStandardCapacity()  function )
		/// </summary>
		public decimal StandardCapacity
		{
			get
			{
				return decimal.Round(dblStandardCapacity, 2);
			}
			set
			{
				dblStandardCapacity = value;
			}
		}

		/// <summary>
		/// While get, we return dblCurrentCapacity - dblStandardCapacity;
		/// </summary>
		public decimal RemainCapacity
		{
			get
			{
				return decimal.Round(CurrentCapacity - StandardCapacity  , 2);
			}			
		}

		/// <summary>
		/// While get, we return (dblStandardCapacity/dblCurrentCapacity)*100;
		/// </summary>
		public string Productivity
		{
			get
			{
				if(CurrentCapacity != 0)
				{
					return decimal.Round((StandardCapacity / CurrentCapacity )*100, 2).ToString();
				}
				else return DIVIDE_BY_ZERO_SIGN;
			}			
		}



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
	}

}

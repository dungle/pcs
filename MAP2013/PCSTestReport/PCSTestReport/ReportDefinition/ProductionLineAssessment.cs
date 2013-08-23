using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text;
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
using PCSComUtils.Common;
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


namespace ProductionLineAssessment
{
	/// <summary>
	/// Thachnn: CONCEPT to build this report
	/// 
	/// </summary>
	[Serializable]	
	public class ProductionLineAssessment : MarshalByRefObject, IDynamicReport
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

		
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ProductionLineAssessment()
		{
		}		

		const string THIS = "ExternalReportFile:ProductionLineAssessment";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "ProductionLineAssessment";			
		const string ZERO_STRING = "0";
		
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		const string NEXTMONTH_DATE_FORMAT = "MMM-yy";

		public static string FORMAT_REPORT_NUMBER = "#,##0.00";

		const string FLD = "fld";
		const string TITLE = "Title";	

		const string PAGE = "Page";
		const string HEADER = "Header";
	

		
		#region		MULTI-THREADING Section

		/// <summary>
		/// We need to delay a short period to get more stable after each thread calling (because of, each thread do a huge thing, connect and query DATABASE)
		/// </summary>
		int THREAD_EACHCALL_DELAY = 2000;
		/// <summary>
		/// Wait time to pooling the Threading return values (results of multi thread processing)
		/// </summary>
		int THREAD_MAINSTREAM_DELAY = 15000;

		/// <summary>
		/// Store thread Name and pointer to the thread.
		/// </summary>
		Hashtable arrThreadManager = new Hashtable();

		/// <summary>
		/// NameValueCollection: key is the ThreadName, 
		/// value now is the ThreadParameter (ElementID - MAxPORevision).
		/// It will have value when we run the RunThreadXXX function
		/// </summary>
		Hashtable arrThreadingProcessParameters = new Hashtable();


		/// <summary>
		/// Hashtable: key is the ProductionID, result is the ThreadReturnValue object. 
		/// When modify value of Properties of  inner object of this Hashtable, remember to reassign the object into HashTable again.
		/// This Hashtable: is modify value in the child worker thread (syncronize using MUTEX)
		/// </summary>
		Hashtable arrThreadingProcessReturnValues = new Hashtable();		

		/// <summary>
		/// This array (hashtable) store the ElementID and relate MAXimum VERSION.
		/// It gets value from database.
		/// key = ElementID, value = VERSION (string)
		/// </summary>
		Hashtable arrElementID_Version = new Hashtable();

		StringCollection arrElementCode = new StringCollection();
		StringCollection arrElementID = new StringCollection();
		

		Mutex objMutex = new Mutex(false);
		int nCountOfDoneThread = 0;	


		// public static for other class reading.
		public static int nCCNID;
		public static int nMonth;
		public static int nYear;
		public static int nPPMDefault = 40;		
		public static string strProportionStandard = "0.95";		



		#endregion		MULTI-THREADING Section
		


		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report		
		/// 
		/// 
		/// ------------------------------ MAIN FLOW OF THIS REPORT ----------------------------------
		/// BUILD THE ELEMENT LIST TABLE
		///
		/// GETTING PROGRESS NUMBERS FROM THE OTHER REPORT (PL DELIVERY AND PL PRODUCTION)
		/// FILL TO THE DTB ELEMENT LIST TABLE
		/// 
		/// DO THE ANALISYS AND UPDATE RANK, POINT OF EACH PL
		/// 
		/// SUM THE TOTAL POINT FOR EACH PL
		/// RANK THE TOTAL POINT
		/// 
		/// RENDER TO REPORT
		/// 
		/// GET THE TOTAL POINT COLUMN TO THE ARRAY, FILL TO THE EXCEL FILE
		/// DRAW CHART BASE ON TOTAL POINT
		/// 
		/// SHOW THE REPORT			
		/// END -------------------------- MAIN FLOW OF THIS REPORT ----------------------------------
		/// </summary>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPPMDefault"></param>
		/// <param name="pstrPPMDefault">Base Item</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPPMDefault)
		{
			#region My variables				

			/// Report layout file constant
			const string REPORT_LAYOUT_FILE = "ProductionLineAssessment.xml";
			const string REPORT_NAME = "ProductionLineAssessment";
			short COPIES = 1;

			/// all parameter are Mandatory
			const string REPORTFLD_PARAMETER_CCN						= "lblCCN";
			const string REPORTFLD_PARAMETER_MONTH					= "lblMonth";
			const string REPORTFLD_PARAMETER_YEAR						= "lblYear";
			const string REPORTFLD_PARAMETER_PPMDEFAULT				= "lblPPMDefault";		
						
			ProductionLineAssessment.nCCNID = int.Parse(pstrCCNID);
			ProductionLineAssessment.nMonth = int.Parse(pstrMonth);
			ProductionLineAssessment.nYear = int.Parse(pstrYear);
			try
			{
				ProductionLineAssessment.nPPMDefault = int.Parse(pstrPPMDefault);			
			}
			catch{}

			string strCCN = string.Empty;
			string strMonth = string.Empty;
			string strYear = string.Empty;
			DateTime dtmFromDate = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
			DateTime dtmToDate = dtmFromDate.AddMonths(1).AddSeconds(-1);
			

			const string REPORTFLD_TITLE			= "fldTitle";
			const string REPORTFLD_COMPANY			= "fldCompany";
			const string REPORTFLD_ADDRESS			= "fldAddress";
			const string REPORTFLD_TEL				= "fldTel";
			const string REPORTFLD_FAX				= "fldFax";

			const string REPORTFLD_CHART			= "fldChart";
			

			float fActualPageSize = 9000.0f;		

			
			
			/// custom object to access and modify the dtbElementList
			ReportDataHelper objDataHelper = new ReportDataHelper();
			
			#endregion		

			// TEST: thread debug
			System.Diagnostics.Trace.WriteLine("====================== START THE PL ASSESSMENT ==========================");

			#region	GETTING THE PARAMETER 			
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strCCN = boUtil.GetCCNCodeFromID(nCCNID);
			
			#endregion
		
			/// ------------------------------ MAIN FLOW OF THIS REPORT ----------------------------------			
			/// ##1## BUILD THE PRODUCTION LINE LIST TABLE
			#region BUILD some RAW DATA TABLE and Array
			
			//// Build the ElementList table: 			
			///	/// SCHEMA: 
			/// 		[ProductionLineID]		///  		[Department]			///  		[ProductionLine]
			///  		[DeliveryProgress]		///  		[DeliveryRank]			///  		[DeliveryPoint]		
			///  		[ProductionProgress]		///  		[ProductionRank]		///  		[ProductionPoint]		
			///  		[QCPPM]						///  		[QCRank]				///  		[QCPoint]		
			///  		[SummaryPoint]			///  		[SummaryRank]		///  		[Comment]		
			
			objDataHelper.GetDataAndCache();
			
			foreach(DataRow drow in objDataHelper.ElementList.Rows)
			{
				arrElementID.Add(drow[RC.PRODUCTIONLINEID].ToString());
			}

			arrElementID_Version = GetMaxVersionForEachReportElement(pstrCCNID, pstrYear, pstrMonth);

			DataTable dtbProducts = objDataHelper.ListAllItem();
			DataTable dtbBOM = objDataHelper.ListBOM();
			DataTable dtbProductionPlan = objDataHelper.GetProductionPlan(dtmFromDate, dtmToDate);
			DataTable dtbProductionActual = objDataHelper.GetProductionActual(dtmFromDate, dtmToDate);
			DataTable dtbDeliveryPlan = objDataHelper.GetPlanForParent(dtmFromDate, dtmToDate);
			DataTable dtbMiscIssue = objDataHelper.GetDataFromMiscIssue(dtmFromDate, dtmToDate);
			DataTable dtbIssue = objDataHelper.GetDataFromIssueMaterial(dtmFromDate, dtmToDate);

			#endregion BUILD some RAW DATA TABLE


			#region BUILD DATA OF REPORT
			
			#region FILL DATA FROM OTHER REPORT TO THE MAIN DATATABLE
			
			/// #### CACULATE THE PPM BY "XUAT TRA CONG DOAN TRUOC" * 1,000,000/"TONG SO LUONG ISSUE ACTUAL"			
			// calculated in the main script VendorList
			/// #### GETTING PROGRESS NUMBERS FROM THE OTHER REPORT (PL DELIVERY AND PL PRODUCTION)			
			// foreach Element (is ProductionLine) in the arrElement (we can treat as the Threading): RUN the thread to process.
			foreach(string strEachElementID  in arrElementID)
			{                
				RunThreadProductionProgress(strEachElementID, (string)arrElementID_Version[strEachElementID] );
				System.Diagnostics.Trace.WriteLine("Main calling: Suppend 1sec after call a thread");
				Thread.Sleep(THREAD_EACHCALL_DELAY);

				RunThreadDeliveryProgress(strEachElementID, (string)arrElementID_Version[strEachElementID] );
				System.Diagnostics.Trace.WriteLine("Main calling: Suppend 1sec after call a thread");
				Thread.Sleep(THREAD_EACHCALL_DELAY);
			}
            while(nCountOfDoneThread < arrThreadManager.Count)
			{
				// TEST: thread debug
				System.Diagnostics.Trace.WriteLine("\n\nMain Thread: I check all you, but you're working. I'm continue sleeping 20 sec \n\n");
				Thread.Sleep(THREAD_MAINSTREAM_DELAY);
			}

			// TEST: thread debug
			System.Diagnostics.Trace.WriteLine("");
			System.Diagnostics.Trace.WriteLine("================================================");
			System.Diagnostics.Trace.WriteLine("Main calling: I've finished calling all threads in my work");		


			/// #### FILL TO THE DTB_Element_LIST TABLE			
			foreach(ThreadReturnValue i in arrThreadingProcessReturnValues.Values)
			{
				objDataHelper.SetPLRow(i.ElementID, RC.PRODUCTION + RC.PROGRESS , i.ProductionProgress);
				objDataHelper.SetPLRow(i.ElementID, RC.DELIVERY + RC.PROGRESS		, i.DeliveryProgress);
				//objDataHelper.SetPLRow(i.ProductionLineID, RC.QC + RC.PPM						, i.PPM);
			}

			/// #### FILL PPM VALUE TO THE DTB_Element_LIST TABLE
			// already in the ElementList_query

			#endregion FILL DATA
		 
			/// #### SET POINT FOR DELIVERY PRODUCTION, QUALITY CONTROL
			objDataHelper.SetPointToFieldBaseOnOtherField(RC.DELIVERY		+ RC.POINT, RC.DELIVERY		+ RC.PROGRESS,  100m, 95m, 0.5m);
			objDataHelper.SetPointToFieldBaseOnOtherField(RC.PRODUCTION	+ RC.POINT, RC.PRODUCTION + RC.PROGRESS,   100m, 95m, 0.5m);
			objDataHelper.SetPointToFieldBaseOnOtherField(RC.QC				+ RC.POINT, RC.QC				+ RC.PPM,  Convert.ToDecimal(nPPMDefault), Convert.ToDecimal(nPPMDefault+10), 1m);					

			/// #### DO THE ANALISYS AND UPDATE RANK			
			objDataHelper.SetRankBaseOnPoint(RC.DELIVERY		+ RC.RANK, RC.DELIVERY		+ RC.POINT, true);
			objDataHelper.SetRankBaseOnPoint(RC.PRODUCTION	+ RC.RANK, RC.PRODUCTION	+ RC.POINT, true);
			objDataHelper.SetRankBaseOnPoint(RC.QC					+ RC.RANK, RC.QC				+ RC.POINT, true);
		
			/// #### SUM THE TOTAL POINT FOR EACH PL
			objDataHelper.SumPoint_Delivery_Production_QualityControl();

			/// #### ANALISE OVERALL RANK 
			objDataHelper.SetRankBaseOnPoint(RC.SUMMARY	+ RC.RANK, RC.SUMMARY + RC.POINT, true);			

			/// #### REORDER THE  DATATABLE
			// auto completed after ranking the summary point

			#endregion BUILD DATA OF REPORT


			
			/// #### RENDER TO REPORT
			#region RENDER REPORT
			
			ReportBuilder objRB = mReportBuilder;// new ReportBuilder();				
			objRB.ReportName = REPORT_NAME;
			// update value of new fields
			objDataHelper.UpdateTotalValue(dtmFromDate, dtbDeliveryPlan, dtbIssue, dtbMiscIssue,
				dtbProductionPlan, dtbProductionActual, dtbProducts, dtbBOM);
			objRB.SourceDataTable = objDataHelper.ElementList;	// we build report base on HashTable, not DataTable, so we put new empty DataTable in to ReportBuilder to avoid error of outside processing
			
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
			

			if(objDataHelper.ElementList.Rows.Count > 0)	/// if having Element working
			{			
					#region BUILD CHART, save to image in clipboard, and then put in the report field fldChart			

					Field fldChart = objRB.GetFieldByName(REPORTFLD_CHART);
			
					#region	INIT

					//				string APP_PATH = @"D:\PCS Project\07-Construction\Source\PCS\PCSMain\bin\Debug";				
					string EXCEL_REPORT_FOLDER = "ExcelReport";			
					string EXCEL_FILE = "ProductionLineAssessment.xls";
			
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

					
					/// #### GET THE TOTAL POINT COLUMN TO THE ARRAY, FILL TO THE EXCEL FILE
					/// #### DRAW CHART BASE ON TOTAL POINT
					try
					{            		
						#region BUILD THE REPORT ON EXCEL FILE
				
						int nNumberOfChartColumn = objDataHelper.ElementList.Rows.Count;

						string[] arrExcelColumnHeading = new string[nNumberOfChartColumn ];
						double[,] arrPoint = new double[1,nNumberOfChartColumn];						
						double[,] arrStandard = new double[1,nNumberOfChartColumn];
					
						/// getting value  GET THE TOTAL POINT COLUMN TO THE ARRAY
						BuildChartData(objDataHelper.ElementList,
							ref arrExcelColumnHeading, 
							ref arrPoint,
							ref arrStandard);

						objXLS.GetRange( objXLS.GetCell(1,1),objXLS.GetCell(1,nNumberOfChartColumn) ).Value2 = arrExcelColumnHeading;
						objXLS.GetRange( objXLS.GetCell(2,1),objXLS.GetCell(2,nNumberOfChartColumn) ).Value2 = arrPoint;	
						objXLS.GetRange( objXLS.GetCell(3,1),objXLS.GetCell(3,nNumberOfChartColumn) ).Value2 = arrStandard;				

						Excel.ChartObject chart = objXLS.GetChart();
						((Excel.Series)chart.Chart.SeriesCollection(1)).Values =  objXLS.GetRange( objXLS.GetCell(2,1),objXLS.GetCell(2,nNumberOfChartColumn) );
						((Excel.Series)chart.Chart.SeriesCollection(2)).Values =  objXLS.GetRange( objXLS.GetCell(3,1),objXLS.GetCell(3,nNumberOfChartColumn) );				

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
						/// TESTED: remove if needed											MessageBox.Show("Can not inter-operate with Excel: " + ex.Message,"Production Control System",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
			}	/// end if having Element working
			
			
			#region MODIFY THE REPORT LAYOUT				
	
			#region DRAW Parameters
				
			System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_CCN).Text, strCCN);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_MONTH).Text, pstrMonth);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_YEAR).Text, pstrYear);
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_PPMDEFAULT).Text, pstrPPMDefault);
			
			
			
			
			/// anchor the Parameter drawing canvas cordinate to the fldTitle
			C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
			objRB.GetSectionByName(PAGE + HEADER).CanGrow = true;
			objRB.DrawParameters( objRB.GetSectionByName(PAGE + HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

			#endregion			

			#endregion
			
			// ReportBuilder.ReformatNumberInC1Report(objRB.Report);
			objRB.MarkRedToNegativeNumberField();
			objRB.RefreshReport();
			

			/// #### SHOW THE REPORT						
			/// force the copies number
			printPreview.FormTitle = objRB.GetFieldByName(FLD + TITLE).Text;
			printPreview.Show();		
			#endregion
			/// END ----------------------- MAIN FLOW OF THIS REPORT ----------------------------------		
			
			UseReportViewerRenderEngine = false;
			mResult = objDataHelper.ElementList;
			return objDataHelper.ElementList;
		}	



		/// <summary>
		/// Worker funtion,
		/// new thread object,
		/// take parameter, assign, to arrThreadingProcessParameters
		/// and then start theading (call the GetProductionProgress_ProportionAchievePlanPercent function)
		/// </summary>
		/// <param name="pstrTheadName"></param>
		/// <returns></returns>
		private bool RunThreadProductionProgress(string pstrElementID, string pstrMaxRevision)
		{
			bool blnRet = false;	//  assign Thread running status here
			
			// new thread
			Thread myThread = new Thread(new ThreadStart(GetProductionProgress_ProportionAchievePlanPercent));			
			// naming that thread
			myThread.Name = pstrElementID + RC.PRODUCTION;
			// add to the ThreadManager
			arrThreadManager.Add(myThread.Name,myThread);
			// add value to this array, then the running thread will know which parameter it will take to do processing
			arrThreadingProcessParameters.Add(myThread.Name, new ThreadParameter(pstrElementID, pstrMaxRevision) );

			// run process with new create thread
			try
			{
				myThread.Start();
				//myThread.Join();
			}			
				// catch Exception: return false
			catch(System.Threading.SynchronizationLockException ex)
			{return false;}
			catch(System.Threading.ThreadAbortException ex)
			{return false;}
			catch(System.Threading.ThreadInterruptedException ex)
			{return false;}
			catch(System.Threading.ThreadStateException ex)
			{return false;}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace);
				return false;
			}

			// read the ReturnValues
			// write thread results to share varriable	if needed		

			// if process here, it means Thread starting is OK.
			blnRet = true;
			return blnRet;		
		}


		/// <summary>
		/// Worker funtion,
		/// new thread object,
		/// take parameter, assign, to arrThreadingProcessParameters
		/// and then start theading (call the GetVendorDeliveryProgress_ProportionAchievePlanPercent function)
		/// </summary>
		/// <param name="pstrTheadName"></param>
		/// <returns></returns>
		private bool RunThreadDeliveryProgress(string pstrElementID, string pstrMaxRevision)
		{
			bool blnRet = false;	//  assign Thread running status here
			
			// new thread
			Thread myThread = new Thread(new ThreadStart(GetDeliveryProgress_ProportionAchievePlanPercent));			
			// naming that thread
			myThread.Name = pstrElementID + RC.DELIVERY;
			// add to the ThreadManager
			arrThreadManager.Add(myThread.Name,myThread);
			// add value to this array, then the running thread will know which parameter it will take to do processing
			arrThreadingProcessParameters.Add(myThread.Name, new ThreadParameter(pstrElementID, pstrMaxRevision) );

			// run process with new create thread
			try
			{
				myThread.Start();
				//myThread.Join();
			}			
				// catch Exception: return false
			catch(System.Threading.SynchronizationLockException ex)
			{return false;}
			catch(System.Threading.ThreadAbortException ex)
			{return false;}
			catch(System.Threading.ThreadInterruptedException ex)
			{return false;}
			catch(System.Threading.ThreadStateException ex)
			{return false;}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message + ex.StackTrace);
				return false;
			}

			// read the ReturnValues
			// write thread results to share varriable	if needed		

			// if process here, it means Thread starting is OK.
			blnRet = true;
			return blnRet;		
		}


	
			
		/// <summary>
		/// /// thachnn: 30/Nov/2005
		/// Build some arrays for render Excel chart
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>void</returns>
		private void BuildChartData(DataTable pdtbElementData, 
			ref string[] parrElement, 
			ref double[,] parrPoint, 
			ref double[,] parrStandard
			)
		{
			int nNumberOfChartColumn = pdtbElementData.Rows.Count;

			int i = 0;
			foreach(DataRow drow in pdtbElementData.Rows)
			{	
				/// BUILD ProductionLine -Heading of the chart (in the X Axis)
				//parrProductionLine[i] = parrProductionLineCode[i];
				parrElement[i] = drow[RC.PRODUCTIONLINE].ToString();

				double dblPoint = 0.0;
				double dblStandard = 0.0;
				
				try
				{
					
					dblPoint = Decimal.ToDouble( ReportDataHelper.ConvertDataRowToELEMENTObject(drow).SummaryPoint ) ;
				}
				catch{}
				
				try
				{
					dblStandard = Decimal.ToDouble(  ReportDataHelper.ConvertDataRowToELEMENTObject(drow).Standard );
				}
				catch{}
				
				parrPoint[0,i] = dblPoint;
				parrStandard[0,i]  = dblStandard;

				i++;
			}		

			
		}
		


			
		/// <summary>
		/// This function will return a Hashtable contain ProductionLineID - MaxWORevision pair (both in STRING TYPE).
		/// Revision number must be larger than 0
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <returns>Hashtable, with key = ElementID, value = MaxWORevision</returns>
		private Hashtable GetMaxVersionForEachReportElement(string pstrCCNID, string pstrYear, string pstrMonth)
		{
			Hashtable arrRet = new Hashtable();

			const string ELEMENTID ="ProductionLineID";
			const string MAXVERSION ="MaxVersion";

			// after this step, we will have list of pair: ElementID - MaxRevision
			#region DB QUERY

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;			
			DataTable dtbTemp = new DataTable();
            
			#region MAIN SQL QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 
				" Declare @pstrMonth char(2) " + 
				" Declare @pstrYear char(4) " + 
				" /*-----------------------------------*/ " + 
				" Set @pstrCCNID = " +pstrCCNID+ " " + 
				" Set @pstrYear = '" +pstrYear+ "' " + 
				" Set @pstrMonth = '" +pstrMonth+ "' " +  
				
/******************** START - get max Version for each Production Line **************************/
 " SELECT " + 
 " distinct  " + 
 " WOMASTER.ProductionLineID as    [" +ELEMENTID+ "] ,     " + 
 " Max(DCOMASTER.Version) as [" +MAXVERSION+ "] " + 
 "  " + 
 " FROM " + 
 " PRO_WorkOrderDetail as WODETAIL " + 
 " join PRO_WorkOrderMaster WOMASTER " + 
 " on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
 " and WOMASTER.CCNID = @pstrCCNID " + 
 " and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
 " and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
 "  " + 
 " join PRO_DCOptionMaster DCOMASTER " + 
 " on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
 " and DCOMASTER.CCNID = @pstrCCNID		 " + 
 " and	DatePart(mm   ,DCOMASTER.AsOfDate) = @pstrMonth " + 
 " and 	DatePart(yyyy ,DCOMASTER.AsOfDate) = @pstrYear " + 
 " and DCOMASTER.Version > 0 " + 
 "  " + 
 " GROUP BY  " + 
 " WOMASTER.ProductionLineID " + 
 "  " + 
/********************* END - get max Version for each Production Line *********************/
 "  " ;

	
			#endregion MAIN QUERY

			try 
			{				
				oconPCS = null;
				ocmdPCS = null;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbTemp);
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
			#endregion DB QUERY

			foreach(DataRow drow in dtbTemp.Rows)
			{
				if(drow[ELEMENTID] != DBNull.Value && drow[MAXVERSION] != DBNull.Value )
				{
					arrRet.Add(drow[ELEMENTID].ToString() , drow[MAXVERSION].ToString() );
				}
			}
			
			return arrRet;
		}	// end function



		
		#region			MULTI THREAD RUNNING TO GET PROPORTIONACHIEVEPLANPERCENT of each ChildReport
		

		/// <summary>
		/// Hardworking processing, running in each Thread.
		/// This processing will get the strElementID from the Parameter array (this array is build in order to pass parameter to thread)
		/// Run the ProductionProgressReport with parameter getting from the Parameter array
		/// Store strResult (the ProportionPercent) in the arrThreadingProcessReturnValues (this array is build in order to store return value of the thread processing)
		/// </summary>
		public void GetProductionProgress_ProportionAchievePlanPercent()
		{
			ProductionLineProductionProgressReport objPP = new ProductionLineProductionProgressReport();
			objPP.PCSConnectionString = mConnectionString;
			objPP.ReportDefinitionFolder = mstrReportDefinitionFolder;
			objPP.ReportLayoutFile = "ProductionLineProductionProgressReport.xml";
			objPP.UseReportViewerRenderEngine = false;

			// it is really a parameter, but we can't pass the parameter to the ThreadStart delegate, so we must use this way to pass parameter			
			string pstrElementID = ((ThreadParameter)arrThreadingProcessParameters[Thread.CurrentThread.Name]).ElementID;
			string pstrMaxVersionParameter = ((ThreadParameter)arrThreadingProcessParameters[Thread.CurrentThread.Name]).MaxVersion;
			// all other parameters are the share variables (they are consistent in this report scope)

			// running external report process
			string strResult = "-";

			#region SPEED UP: short circuit if there is no PORevision
			if(pstrElementID == string.Empty || pstrElementID == null ||
				pstrMaxVersionParameter == string.Empty || pstrMaxVersionParameter == null)
			{
				// do nothing, result still is  "-"  UNKNOWN               
			}
				#endregion SPEED UP
			else
			{
				objPP.ExecuteReport(nCCNID.ToString() ,nYear.ToString(),nMonth.ToString(),    
					pstrElementID, string.Empty , pstrMaxVersionParameter , strProportionStandard);

				strResult = objPP.Result.ToString();
			}
            			

			// START PROTECTED ACCESS
			objMutex.WaitOne();			
			if(arrThreadingProcessReturnValues.ContainsKey(pstrElementID)   )
			{
				ThreadReturnValue objPPro_DPro_PPM_VO = (ThreadReturnValue)arrThreadingProcessReturnValues[pstrElementID];	// extract and casting
				objPPro_DPro_PPM_VO.ProductionProgress = strResult;	// MODIFY PROPERTIES OF INNER OBJECT
				arrThreadingProcessReturnValues[pstrElementID] = objPPro_DPro_PPM_VO;	// reassign
			}
			else
			{
                ThreadReturnValue objPPro_DPro_PPM_VO = new ThreadReturnValue(int.Parse(pstrElementID), strResult, string.Empty,string.Empty);
				arrThreadingProcessReturnValues.Add(pstrElementID, objPPro_DPro_PPM_VO);
			}            

			// almost DONE of this thread bussiness
			nCountOfDoneThread++;
			objMutex.ReleaseMutex();
			// END PROTECTED ACCESS

			// TEST: thread debug
			System.Diagnostics.Trace.WriteLine("ProductionProgressReport running with <" + pstrElementID +"> result is <"+strResult+">");
		}
	
		
	
		/// <summary>
		/// Hardworking processing, running in each Thread.
		/// This processing will get the strElementID from the Parameter array (this array is build in order to pass parameter to thread)
		/// Run the DeliveryProgressReport with parameter getting from the Parameter array
		/// Store strResult (the ProportionPercent) in the arrThreadingProcessReturnValues (this array is build in order to store return value of the thread processing)
		/// </summary>
		public void GetDeliveryProgress_ProportionAchievePlanPercent()
		{
			ProductionLineDeliveryProgressReport objPP = new ProductionLineDeliveryProgressReport();
			objPP.PCSConnectionString = mConnectionString;
			objPP.ReportDefinitionFolder = mstrReportDefinitionFolder;
			objPP.ReportLayoutFile = "ProductionLineDeliveryProgressReport.xml";
			objPP.UseReportViewerRenderEngine = false;

			// it is really a parameter, but we can't pass the parameter to the ThreadStart delegate, so we must use this way to pass parameter			
			string pstrElementID = ((ThreadParameter)arrThreadingProcessParameters[Thread.CurrentThread.Name]).ElementID;
			string pstrMaxVersionParameter = ((ThreadParameter)arrThreadingProcessParameters[Thread.CurrentThread.Name]).MaxVersion;
			// all other parameters are the share variables (they are consistent in this report scope)

			// running external report process
			string strResult = "-";

			#region SPEED UP: short circuit if there is no PORevision
			if(pstrElementID == string.Empty || pstrElementID == null ||
				pstrMaxVersionParameter == string.Empty || pstrMaxVersionParameter == null)
			{
				// do nothing, result still is  "-"  UNKNOWN               
			}
				#endregion SPEED UP
			else
			{
				objPP.ExecuteReport(nCCNID.ToString() ,nYear.ToString(),nMonth.ToString(),    
					pstrElementID, string.Empty , pstrMaxVersionParameter , strProportionStandard);

				strResult = objPP.Result.ToString();
			}
            			

			// START PROTECTED ACCESS
			objMutex.WaitOne();			
			if(arrThreadingProcessReturnValues.ContainsKey(pstrElementID)   )
			{
				ThreadReturnValue objPPro_DPro_PPM_VO = (ThreadReturnValue)arrThreadingProcessReturnValues[pstrElementID];	// extract and casting
				objPPro_DPro_PPM_VO.DeliveryProgress = strResult;	// MODIFY PROPERTIES OF INNER OBJECT
				arrThreadingProcessReturnValues[pstrElementID] = objPPro_DPro_PPM_VO;	// reassign
			}
			else
			{
				ThreadReturnValue objPPro_DPro_PPM_VO = new ThreadReturnValue(int.Parse(pstrElementID), string.Empty, strResult, string.Empty);
				arrThreadingProcessReturnValues.Add(pstrElementID,objPPro_DPro_PPM_VO);
			}            

			// almost DONE of this thread bussiness
			nCountOfDoneThread++;
			objMutex.ReleaseMutex();
			// END PROTECTED ACCESS

			// TEST: thread debug
			System.Diagnostics.Trace.WriteLine("DeliveryProgressReport running with <" + pstrElementID +"> result is <"+strResult+">");
		}
	
		
		
		#endregion


	}	// end class report

	/// <summary>
	/// collect all task affect to the DataTable. 
	/// Must set the ElementList datatable to the InnerDataTable of instance of this class to processing.
	/// </summary>
	public class ReportDataHelper
	{		
		
		// it is a offline cache of data from database 
		// main dataset of this report, contain all data get from database
		private DataSet mdst_MAIN_DATA_REPOSITORY = new DataSet("MAIN_DATA_REPOSITORY");
		
		/// <summary>
		/// /// SCHEMA: 
		/// 		[ProductionLineID]		///  		[Department]			///  		[ProductionLine]
		///  		[DeliveryProgress]		///  		[DeliveryRank]			///  		[DeliveryPoint]		
		///  		[ProductionProgress]		///  		[ProductionRank]		///  		[ProductionPoint]		
		///  		[QCPPM]						///  		[QCRank]				///  		[QCPoint]		
		///  		[SummaryPoint]			///  		[SummaryRank]		///  		[Comment]
		/// </summary>
		private DataTable mdtbElementList;
		public DataTable ElementList
		{
			get
			{
				return mdtbElementList;
			}
			set
			{
				mdtbElementList = value;
			}
		}
		

		#region GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable) - TRANSFORM SOME THING

		public DataTable GetPlanForParent(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT DISTINCT SUM(ISNULL((PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity)/((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0)) AS Quantity,"
					+ " ITM_BOM.ComponentID AS ProductID, PRO_DCPResultMaster.WorkCenterID, ITM_BOM.LeadTimeOffset, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " MST_WorkCenter.ProductionLineID"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND WorkingDate >= ?"
					+ " AND WorkingDate <= ?"
					+ " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, ITM_BOM.ComponentID, LeadTimeOffset, PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime,"
					+ " MST_WorkCenter.ProductionLineID"
					+ " HAVING SUM(ISNULL((PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity)/((100 - ISNULL(ITM_BOM.Shrink,0))/100), 0)) <> 0"
					+ " ORDER BY MST_WorkCenter.ProductionLineID, PRO_DCPResultMaster.DCOptionMasterID, ITM_BOM.ComponentID, WorkingDate";


                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		public DataTable GetDataFromIssueMaterial(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT ProductionLineID, SUM(ISNULL(CommitQuantity,0)) AS Quantity, PRO_IssuePurpose.Code AS Purpose"
					+ " FROM PRO_IssueMaterialDetail JOIN PRO_IssueMaterialMaster"
					+ " ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID"
					+ " JOIN PRO_IssuePurpose ON PRO_IssueMaterialMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID"
					+ " JOIN PRO_ProductionLine P ON PRO_IssueMaterialDetail.LocationID = P.LocationID"
					+ " WHERE PostDate >= ? AND PostDate <= ?"
					+ " GROUP BY ProductionLineID, PRO_IssuePurpose.Code ORDER BY ProductionLineID";

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
		public DataTable GetDataFromMiscIssue(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT DesLocationID, SourceLocationID, Quantity,"
					+ " PRO_IssuePurpose.Code AS Purpose"
					+ " FROM IV_MiscellaneousIssueDetail JOIN IV_MiscellaneousIssueMaster"
					+ " ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID"
					+ " JOIN PRO_IssuePurpose ON IV_MiscellaneousIssueMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate <= ?";

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
		public DataTable GetDeliveryActual(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT ProductionLineID, SUM(IssueQuantity + MiscQuantity) AS Quantity"
					+ " FROM"
					+ " (SELECT ProductionLineID, SUM(ISNULL(CommitQuantity,0)) AS IssueQuantity, 0 AS MiscQuantity"
					+ " FROM PRO_IssueMaterialMaster M JOIN PRO_IssueMaterialDetail D ON M.IssueMaterialMasterID = D.IssueMaterialMasterID"
					+ " JOIN PRO_ProductionLine P ON D.LocationID = P.LocationID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate <= ?"
					+ " AND ProductionLineID IS NOT NULL"
					+ " GROUP BY ProductionLineID"
					+ " UNION ALL"
					+ " SELECT ProductionLineID, 0 AS IssueQuantity, SUM(ISNULL(Quantity,0)) AS MiscQuantity"
					+ " FROM IV_MiscellaneousIssueMaster M JOIN IV_MiscellaneousIssueDetail D"
					+ " ON M.MiscellaneousIssueMasterID = D.MiscellaneousIssueMasterID"
					+ " JOIN PRO_ProductionLine P ON M.DesLocationID = P.LocationID"
					+ " WHERE PostDate >= ?"
					+ " AND PostDate <= ?"
					+ " AND ProductionLineID IS NOT NULL"
					+ " GROUP BY ProductionLineID) AS A"
					+ " GROUP BY ProductionLineID ORDER BY ProductionLineID";
			
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate1", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate1", OleDbType.Date)).Value = pdtmToDate;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		public DataTable GetProductionPlan(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql	+= " SELECT DISTINCT SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)) AS Quantity,"
					+ " PRO_DCPResultMaster.ProductID, PRO_DCPResultMaster.WorkCenterID, WorkingDate,"
					+ " PRO_DCPResultMaster.DCOptionMasterID, MST_WorkCenter.ProductionLineID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime"
					+ " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster"
					+ " ON PRO_DCPResultMaster.DCPResultMasterID = PRO_DCPResultDetail.DCPResultMasterID"
					+ " JOIN MST_WorkCenter ON PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID"
					+ " WHERE MST_WorkCenter.IsMain = 1"
					+ " AND WorkingDate >= ?"
					+ " AND WorkingDate <= ?"
					+ " GROUP BY PRO_DCPResultMaster.DCOptionMasterID, MST_WorkCenter.ProductionLineID, "
					+ " PRO_DCPResultDetail.WorkingDate,"
					+ " PRO_DCPResultMaster.WorkCenterID, PRO_DCPResultMaster.ProductID,"
					+ " PRO_DCPResultDetail.StartTime, PRO_DCPResultDetail.EndTime"
					+ " HAVING SUM(ISNULL(PRO_DCPResultDetail.Quantity, 0)) <> 0"
					+ " ORDER BY MST_WorkCenter.ProductionLineID, PRO_DCPResultMaster.ProductID, WorkingDate, PRO_DCPResultMaster.DCOptionMasterID";
			
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		public DataTable GetProductionActual(DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql	+= " SELECT P.ProductionLineID, SUM(ISNULL(CompletedQuantity,0)) AS Quantity"
					+ " FROM PRO_WorkOrderCompletion W JOIN ITM_Product P ON W.ProductID = P.ProductID"
					+ " WHERE W.PostDate >= ?"
					+ " AND W.PostDate <= ?"
					+ " GROUP BY P.ProductionLineID"
					+ " ORDER BY P.ProductionLineID";
			
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		public DataTable ListAllItem()
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT ProductID, ProductionLineID FROM ITM_Product WHERE ProductionLineID IS NOT NULl";
			
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		public DataTable ListBOM()
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT ProductID, ComponentID FROM ITM_BOM";
			
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				return dtbData;
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

		private bool IsRootItem(string pstrProductID, DataTable pdtbBOM)
		{
			if (pdtbBOM.Select("ComponentID = " + pstrProductID).Length > 0)
				return false;
			else
				return true;
		}
		/// <summary>
		/// Get  Element List (table) with PPM setted
		/// </summary>
		/// <returns></returns>
		public bool GetDataAndCache()
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				// TODO: THACH: need TO RE WRITE THIS TEST QUERY - ADD PARAMETER ACTUAL
				strSql = 	

 " Declare @pstrYear char(4) " + 
 " Declare @pstrMonth char(2) " + 
 " Declare @pstrPPMDefault int " + 
 " Declare @pstrCCNID int " + 
 " Declare @pstrWorkOrderMasterID_2 int " + 
 " Declare @pstrInArray varchar(40) " + 
 " Declare @pstrOutArray varchar(40) " + 
 " /*-----------------------------------*/ " + 
 "  " + 
 " /*-----------------------------------*/ " + 
 " Set @pstrYear = '" + ProductionLineAssessment.nYear + "' " + 
 " Set @pstrMonth = '" + ProductionLineAssessment.nMonth + "' " + 
 " Set @pstrPPMDefault = " + ProductionLineAssessment.nPPMDefault +
 " Set @pstrCCNID = " + ProductionLineAssessment.nCCNID +
 " Set @pstrWorkOrderMasterID_2 = 329 " + 
 " /*-----------------------------------*/ " + 
 "  " + 
 "  " + 
 " 	 " + 
 " /*----------------- PRODUCTION LINE LIST ------------------*/ " + 
 " select  " + 
 " PRO_ProductionLine.ProductionLineID as [ProductionLineID], " + 
 " MST_Department.Code as [Department], " + 
					" MST_Department.Name as [DepartmentName], " + 
 " PRO_ProductionLine.Code as [ProductionLine], " + 
					" PRO_ProductionLine.Name as [ProductionLineName], " + 
 " ISNULL(PRO_ProductionLine.LocationID,0) AS LocationID, " + 
 " '0.00' as [DeliveryProgress], " + 
 " 0 as [DeliveryRank], " + 
 " 0 as [DeliveryPoint], " + 
 " 0.00 as [DeliveryPlan], " + 
 " 0.00 as [DeliveryActual], " + 
 " 0.00 as [ProgressDelivery], " + 
 " 0.00 as [ProductionPlan], " + 
 " 0.00 as [ProductionActual], " + 
 " 0.00 as [ProgressProduction], " + 
 " 0.00 as [TotalIssue], " + 
 " 0.00 as [TotalReturn], " + 
 " '0.00' as [ProductionProgress], " + 
 " 0 as [ProductionRank], " + 
 " 0 as [ProductionPoint], " + 
 " Isnull(PPMTABLE.QCPPM, 0)   as [QCPPM], " + 
 " 0 as [QCRank], " + 
 " 0 as [QCPoint], " + 
 " 0 as [SummaryPoint], " + 
 " 0 as [SummaryRank], " + 
 " 0 as [Standard], " + 
 " ' ' as [Comment] " + 
 "  " + 
 "  " + 
 "  " + 
 " from PRO_ProductionLine " + 
 " join MST_Department " + 
 " on PRO_ProductionLine.DepartmentID = MST_Department.DepartmentID	 " + 
 " /*----------------- PRODUCTION LINE LIST ------------------*/ " + 
 " 	 " + 
 "  " + 
 " LEFT join " + 
 " ( " + 
 "  " + 
 "  " + 
 " 	select ACTUALISSUETABLE.ProductionLineID, " + 
 " 	Isnull ( (RETURNTABLE.ReturnPreviousQuantity / ACTUALISSUETABLE.ActualQuantity)*1000000   , 0 ) as QCPPM " + 
 " 	 " + 
 " 	from  " + 
 " 	( " + 
 " 		 " + 
 " 		/************************* ACTUAL TABLE ***************************************************************************/ " + 
 " 		/*  SIMPLE TEMPLATE FOR OVERALL SCRIPT */ " + 
 " 		select ACTUAL_DATA_TABLE.ProductionLineID, Sum(ACTUAL_DATA_TABLE.ActualQuantity) as ActualQuantity from  " + 
 " 		( " + 
 " 			select  " + 
 " 			isNull(SHIPTABLE.ProductionLineID,isNull(ISSUEWOTABLE.ProductionLineID,MISCISSUETABLE.ProductionLineID)) as ProductionLineID, " + 
 " 			isNull(SHIPTABLE.ShipQuantity,0)+isNull(ISSUEWOTABLE.IssueWOQuantity,0)+isNull(MISCISSUETABLE.MiscIssueQuantity,0) as ActualQuantity " + 
 " 			from  " + 
 " 			(	 " + 
 " 			/**** SHIPPING MANAGEMENT ****  BY PRODUCTIONLINE ***** SIMPLE QUERY ******************************************/ " + 
 " 		 " + 
 " 				SELECT  " + 
 " 				PL_LOCATION.ProductionLineID, " + 
 " 				PL_LOCATION.Code, " + 
 " 				SUM(ISNULL(SO_CommitInventoryDetail.CommitQuantity, 0)) AS ShipQuantity	 " + 
 " 				from SO_CommitInventoryDetail " + 
 " 				join (select ProductionLineID, Code, LocationID from PRO_ProductionLine) as PL_LOCATION " + 
 " 				on SO_CommitInventoryDetail.LocationID = PL_LOCATION.LocationID /* COmmitInventoryDetail.LocationID is the FromLocation for this query */ " + 
 " 				and SO_CommitInventoryDetail.Shipped = 1 " + 
 " 				and Datepart(mm   , SO_CommitInventoryDetail.ShipDate) = @pstrMonth " + 
 " 				and Datepart(yyyy , SO_CommitInventoryDetail.ShipDate) = @pstrYear " + 
 " 				 " + 
 " 				GROUP BY  " + 
 " 				PL_LOCATION.ProductionLineID , " + 
 " 				PL_LOCATION.Code " + 
 " 			 " + 
 " 			/**** SHIPPING MANAGEMENT ****  BY PRODUCTIONLINE ***** SIMPLE QUERY ******************************************/ " + 
 " 			)as 	SHIPTABLE  " + 
 " 		 " + 
 " 			full outer join  " + 
 " 			(	 " + 
 " 			/**** ISSUE 4 WORK ORDER ****  BY PRODUCTIONLINE ***********************************************/ " + 
 " 		 " + 
 " 				SELECT  " + 
 " 				PL_LOCATION.ProductionLineID , " + 
 " 				PL_LOCATION.Code, " + 
 " 				IsNull(Sum(DETAIL.CommitQuantity),0) as [IssueWOQuantity] " + 
 " 				 " + 
 " 				FROM PRO_IssueMaterialDetail as DETAIL " + 
 " 				JOIN PRO_IssueMaterialMaster as MASTER ON MASTER.IssueMaterialMasterID = DETAIL.IssueMaterialMasterID " + 
 " 				join (select ProductionLineID, Code, LocationID from PRO_ProductionLine) as PL_LOCATION " + 
 " 				on DETAIL.LocationID = PL_LOCATION.LocationID " + 
 " 				 " + 
 " 				where  " + 
 " 					MASTER.CCNID = @pstrCCNID " + 
 " 					and DATEPART(mm  , MASTER.PostDate) = @pstrMonth " + 
 " 					and DATEPART(yyyy, MASTER.PostDate) = @pstrYear	 " + 
 " 				GROUP BY  " + 
 " 				PL_LOCATION.ProductionLineID , " + 
 " 				PL_LOCATION.Code " + 
 " 		 " + 
 " 			/**** ISSUE 4 WORK ORDER ****  BY PRODUCTIONLINE ***********************************************/ " + 
 " 			) as	ISSUEWOTABLE " + 
 " 			on SHIPTABLE.ProductionLineID = ISSUEWOTABLE.ProductionLineID " + 
 " 		 " + 
 " 			full outer join  " + 
 " 			(	 " + 
 " 			/**** MISC ISSUE ****  BY PRODUCTIONLINE ***********************************************/ " + 
 " 		 " + 
 " 				select  " + 
 " 				PL_LOCATION.ProductionLineID , " + 
 " 				PL_LOCATION.Code, " + 
 " 				IsNull(Sum(DETAIL.Quantity),0) as MiscIssueQuantity " + 
 " 				 " + 
 " 				from IV_MiscellaneousIssueMaster as MASTER  " + 
 " 				join IV_MiscellaneousIssueDetail as DETAIL on MASTER.MiscellaneousIssueMasterID = DETAIL.MiscellaneousIssueMasterID " + 
 " 				join (select ProductionLineID, Code, LocationID from PRO_ProductionLine) as PL_LOCATION " + 
 " 				on MASTER.DesLocationID = PL_LOCATION.LocationID " + 
 " 				 " + 
 " 				WHERE  " + 
 " 					MASTER.CCNID =  @pstrCCNID " + 
 " 					and DATEPART(mm  ,MASTER.PostDate) = @pstrMonth " + 
 " 					and DATEPART(yyyy,MASTER.PostDate) = @pstrYear " + 
 " 				group by  " + 
 " 				PL_LOCATION.ProductionLineID , " + 
 " 				PL_LOCATION.Code " + 
 " 		 " + 
 " 			/**** MISC ISSUE ****  BY PRODUCTIONLINE ***********************************************/ " + 
 " 			) as	MISCISSUETABLE " + 
 " 			on  ISSUEWOTABLE.ProductionLineID = MISCISSUETABLE.ProductionLineID " + 
 " 			and SHIPTABLE.ProductionLineID = MISCISSUETABLE.ProductionLineID " + 
 " 		 " + 
 " 		 " + 
 " 		) as ACTUAL_DATA_TABLE " + 
 " 		group by " + 
 " 		ACTUAL_DATA_TABLE.ProductionLineID " + 
 " 		 " + 
 " 		/************************* ACTUAL TABLE ***************************************************************************/ " + 
 " 	 " + 
 " 	) as ACTUALISSUETABLE " + 
 " 	 " + 
 " 	LEFT join  " + 
 " 	 " + 
 " 	( " + 
 " 	 " + 
 " 	/**** MISC ISSUE ****  RETURN TO PREVIOUS ***********************************************/ " + 
 " 	select  " + 
 " 	PL_LOCATION.ProductionLineID , " + 
 " 	PL_LOCATION.Code, " + 
 " 	IsNull(Sum(DETAIL.Quantity),0) as ReturnPreviousQuantity " + 
 " 	 " + 
 " 	from IV_MiscellaneousIssueMaster as MASTER  " + 
 " 	join IV_MiscellaneousIssueDetail as DETAIL on MASTER.MiscellaneousIssueMasterID = DETAIL.MiscellaneousIssueMasterID " + 
 " 	 " + 
 " 	join (select ProductionLineID, Code, LocationID from PRO_ProductionLine) as PL_LOCATION " + 
 " 	on MASTER.DesLocationID = PL_LOCATION.LocationID " + 
 " 	 " + 
 " 	WHERE  " + 
 " 		MASTER.CCNID =  @pstrCCNID " + 
 " 		and DATEPART(mm  ,MASTER.PostDate) = @pstrMonth " + 
 " 		and DATEPART(yyyy,MASTER.PostDate) = @pstrYear	 " + 
 " 		and MASTER.IssuePurposeID = 5 /* xuat tra cong doan truoc */ " + 
 " 	group by  " + 
 " 	PL_LOCATION.ProductionLineID , " + 
 " 	PL_LOCATION.Code " + 
 " 	/**** MISC ISSUE *****  RETURN TO PREVIOUS *********************************************/ " + 
 " 	 " + 
 " 	 " + 
 " 	) as RETURNTABLE " + 
 " 	on ACTUALISSUETABLE.ProductionLineID = RETURNTABLE.ProductionLineID " + 
 "  " + 
 " ) as PPMTABLE " + 
 " on PRO_ProductionLine.ProductionLineID = PPMTABLE.ProductionLineID		 " + 
 " 	 "
;

				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(mdst_MAIN_DATA_REPOSITORY);				
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

			mdtbElementList = mdst_MAIN_DATA_REPOSITORY.Tables[0];

			return true;
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
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
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
		public DataTable GetCycles(string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
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
		private ArrayList GetPlanningPeriod(string pstrCCNID)
		{
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = "SELECT DISTINCT PlanningPeriod FROM PRO_DCOptionMaster WHERE CCNID = " + pstrCCNID;
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
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
		#endregion GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable)

		public bool UpdateTotalValue(DateTime pdtmFromDate, DataTable pdtbDeliveryPlan, DataTable pdtbIssue,
			DataTable pdtbMiscIssue, DataTable pdtbProductionPlan, DataTable pdtbProductionActual,
			DataTable pdtbProducts, DataTable pdtbBOM)
		{
			DataTable dtbPlanningOffset = GetPlanningOffset(ProductionLineAssessment.nCCNID.ToString());
			DataTable dtbCycles = GetCycles(ProductionLineAssessment.nCCNID.ToString());
			ArrayList arrPlanningPeriod = GetPlanningPeriod(ProductionLineAssessment.nCCNID.ToString());
			foreach (DataRow row in mdtbElementList.Rows)
			{
				string strProductionLineID = row[RC.PRODUCTIONLINEID].ToString();
				string strLocationID = row["LocationID"].ToString();
				// list all product of production line
				DataRow[] drowProducts = pdtbProducts.Select(RC.PRODUCTIONLINEID + "=" + strProductionLineID);
				DataTable dtbCyclePro = dtbCycles.Copy();
				dtbCyclePro = RefineCycle(strProductionLineID, dtbPlanningOffset, dtbCycles);
				StringBuilder sbCycleIDs;
				DataTable dtbCyclesCurrentMonth = ArrangeCycles(pdtmFromDate, DateTime.MinValue, dtbCyclePro, arrPlanningPeriod, out sbCycleIDs);
				decimal decDeliveryPlan = 0, decDeliveryActual = 0, decProductionPlan = 0, decProductionActual = 0;
				decimal decTotalIssue = 0, decTotalReturn = 0;
				string strFilter = "ProductionLineID = " + strProductionLineID;
				
				#region Delivery Plan
				foreach (DataRow drowItem in drowProducts)
				{
					string strProductID = drowItem["ProductID"].ToString();
					// root item, we will consider production plan as delivery plan
					if (IsRootItem(strProductID, pdtbBOM))
					{
						#region production plan

						DataRow[] drowPlan = pdtbProductionPlan.Select(strFilter + " AND ProductID = " + strProductID);
						foreach (DataRow drowData in drowPlan)
						{
							DateTime dtmWorkingDate = Convert.ToDateTime(drowData["WorkingDate"]);
							string strCycleID = GetCycleOfDate(dtmWorkingDate, dtbCyclesCurrentMonth);
							if (drowData["DCOptionMasterID"].ToString() != strCycleID)
								continue;
							try
							{
								decProductionPlan += Convert.ToDecimal(drowData["Quantity"]);
							}
							catch{}
						}

						#endregion

						decDeliveryPlan += decProductionPlan;
					}
					else // delivery plan for parent
					{
						#region production plan

						DataRow[] drowPlan = pdtbProductionPlan.Select(strFilter + " AND ProductID = " + strProductID);
						foreach (DataRow drowData in drowPlan)
						{
							DateTime dtmWorkingDate = Convert.ToDateTime(drowData["WorkingDate"]);
							string strCycleID = GetCycleOfDate(dtmWorkingDate, dtbCyclesCurrentMonth);
							if (drowData["DCOptionMasterID"].ToString() != strCycleID)
								continue;
							try
							{
								decProductionPlan += Convert.ToDecimal(drowData["Quantity"]);
							}
							catch{}
						}

						#endregion

						#region delivery plan

						drowPlan = pdtbDeliveryPlan.Select(strFilter + " AND ProductID = " + strProductID);
						foreach (DataRow drowData in drowPlan)
						{
							DateTime dtmWorkingDate = Convert.ToDateTime(drowData["WorkingDate"]);
							string strCycleID = GetCycleOfDate(dtmWorkingDate, dtbCyclesCurrentMonth);
							if (drowData["DCOptionMasterID"].ToString() != strCycleID)
								continue;
							try
							{
								decDeliveryPlan += Convert.ToDecimal(drowData["Quantity"]);
							}
							catch{}
						}

						#endregion
					}
				}
				#endregion

				#region Delivery Actual
				// issue material
				try
				{
					decDeliveryActual += Convert.ToDecimal(pdtbIssue.Compute("SUM(Quantity)", strFilter));
					decTotalIssue += decDeliveryActual;
				}
				catch{}
				// misc. issue (xuat chuyen kho)
				try
				{
					string strMyFilter = strFilter + " AND Purpose = " + (int)PurposeEnum.LocToLoc
						+ " AND SourceLocationID = " + strLocationID;
					decDeliveryActual += Convert.ToDecimal(pdtbMiscIssue.Compute("SUM(Quantity)", strMyFilter));
				}
				catch{}
				#endregion

				#region Production Actual
				try
				{
					decProductionActual += Convert.ToDecimal(pdtbProductionActual.Compute("SUM(Quantity)", strFilter));
				}
				catch{}
				#endregion

				#region Total Return
				try
				{
					string strMyFilter = strFilter + " AND Purpose = " + (int)PurposeEnum.ReturnPrevious
						+ " AND DesLocationID = " + strLocationID;
					decTotalReturn += Convert.ToDecimal(pdtbMiscIssue.Compute("SUM(Quantity)", strMyFilter));
				}
				catch{}
				#endregion

				row[RC.DELIVERYPLAN] = decDeliveryPlan;
				row[RC.DELIVERYACTUAL] = decDeliveryActual;
				row[RC.PROGRESSDELIVERY] = decDeliveryActual - decDeliveryPlan;
				row[RC.PRODUCTIONPLAN] = decProductionPlan;
				row[RC.PRODUCTIONACTUAL] = decProductionActual;
				row[RC.PROGRESSPRODUCTION] = decProductionActual - decProductionPlan;
			}
			return true;
		}
		

		/// <summary>
		/// public Function to get pointer to the dataTable with provided Name
		/// </summary>
		/// <param name="pstrTableNameToGet"></param>
		/// <returns></returns>
		public DataTable GetDataTableByName(string pstrTableNameToGet)
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



		#region      INNER DATA (process on the mdtbElementList) MANIPULATE FUNCTION		

		public DataRow GetPLRow(string pstrPLCode)
		{
			foreach(DataRow drow in mdtbElementList.Rows)
			{
				if(drow[RC.PRODUCTIONLINE].ToString().Equals(pstrPLCode))
				{
					return drow;
				}
			}

			return null;
		}


		public DataRow GetPLRow(int pintPL_ID)
		{
			foreach(DataRow drow in mdtbElementList.Rows)
			{
				if( (int)drow[RC.PRODUCTIONLINEID] == pintPL_ID)
				{
					return drow;
				}
			}

			return null;
		}


		public bool SetPLRow(string pstrPLCode, string pstrColummName, object pobjValue)
		{
			foreach(DataRow drow in mdtbElementList.Rows)
			{
				if(drow[RC.PRODUCTIONLINE].ToString().Equals(pstrPLCode)  )
				{
					drow[pstrColummName] = pobjValue;
					return true;
				}
			}
			
			return false;
		}
		public bool SetPLRow(int pintID, string pstrColummName, object pobjValue)
		{
			foreach(DataRow drow in mdtbElementList.Rows)
			{
				if((int)drow[RC.PRODUCTIONLINEID] == pintID  )
				{
					drow[pstrColummName] = pobjValue;
					return true;
				}
			}
			
			return false;
		}

		

		/// <summary>
		/// Highest rank = 1
		/// Lowest Rank = InnerDataTable.Rows.Count
		/// Rank need to base on Point column in the innerDataTable
		/// </summary>
		/// <param name="pstrSetToField"></param>
		/// <param name="pstrBaseOnField"></param>
		public void SetRankBaseOnPoint(string pstrSetToField, string pstrBaseOnField, bool pblnAllowSameRanking)
		{
			const int HIGHEST_RANK = 1;
			if(pblnAllowSameRanking)
			{

				int nCurrentRank = 1;

				// after caculate the point, total point will never equals with DBNull because of we try catch DBNull then convert to 0m
				DataRow[] arrSortedRows = mdtbElementList.Select(string.Empty, pstrBaseOnField + " DESC");

				// copy only the table schema
				DataTable dtbSorted = mdtbElementList.Clone();
				// and then, fill data in the sort datarows to the new schema table
				
				for(int i = 0; i < arrSortedRows.Length ; i++)
				{
					DataRow drow = arrSortedRows[i];					
					DataRow dtrNextRow  = null;

					// get next row, if can't get, nextRow will == null
					if(i < arrSortedRows.Length -1 )
					{
						dtrNextRow = arrSortedRows[i+1];
					}

					drow[pstrSetToField] = nCurrentRank;
					dtbSorted.ImportRow(drow);

					// compare CURRENT ROW POINT and NEXT ROW POINT
					if(dtrNextRow != null)
					{
						bool blnCurrentRow_HasSamePointWith_PreviousRow = ( drow[pstrBaseOnField].Equals( dtrNextRow[pstrBaseOnField])  );

						if(blnCurrentRow_HasSamePointWith_PreviousRow == false)
						{
							nCurrentRank++;
						}
					}
				}

				// reassign the result table to the inner table pointer
				mdtbElementList = dtbSorted;                
			}

			#region not same ranking
			else	// do not allow same ranking
			{				
				// int nLowestRank = mdtbInnerDataTable.Rows.Count;

				// after caculate the point, total point will never equals with DBNull because of we try catch DBNull then convert to 0m
				DataRow[] arrSortedRows = mdtbElementList.Select(string.Empty, pstrBaseOnField + " DESC");

				// copy only the table schema
				DataTable dtbSorted = mdtbElementList.Clone();
				// and then, fill data in the sort datarows to the new schema table
				int i = HIGHEST_RANK;
				foreach(DataRow drow in arrSortedRows)
				{
					drow[pstrSetToField] = i;
					dtbSorted.ImportRow(drow);
					i++;
				}
				mdtbElementList = dtbSorted;
			}
			#endregion
		}

		
		public static ELEMENT ConvertDataRowToELEMENTObject(DataRow prow)
		{
			ELEMENT objRet = new ELEMENT();

			objRet.ProductionLineID = Convert.ToInt32(prow[RC.PRODUCTIONLINEID]);
			objRet.Department = Convert.ToString(prow[RC.DEPARTMENT]);
			objRet.ProductionLine = Convert.ToString(prow[RC.PRODUCTIONLINE]);
			objRet.DeliveryProgress = Convert.ToDecimal(prow[RC.DELIVERY + RC.PROGRESS]);
			objRet.DeliveryRank = Convert.ToDecimal(prow[RC.DELIVERY + RC.RANK]);
			objRet.DeliveryPoint = Convert.ToDecimal(prow[RC.DELIVERY + RC.POINT]);
			objRet.ProductionProgress = Convert.ToDecimal(prow[RC.PRODUCTION + RC.PROGRESS]);
			objRet.ProductionRank = Convert.ToDecimal(prow[RC.PRODUCTION + RC.RANK]);
			objRet.ProductionPoint = Convert.ToDecimal(prow[RC.PRODUCTION + RC.POINT]);
			objRet.QualityPPM = Convert.ToDecimal(prow[RC.QC + RC.PPM]);
			objRet.QualityRank = Convert.ToDecimal(prow[RC.QC + RC.RANK]);
			objRet.QualityPoint = Convert.ToDecimal(prow[RC.QC + RC.POINT]);
			objRet.SummaryPoint = Convert.ToDecimal(prow[RC.SUMMARY + RC.POINT]);
			objRet.SummaryRank = Convert.ToDecimal(prow[RC.SUMMARY + RC.RANK]);
			objRet.Standard = Convert.ToDecimal(prow[RC.STANDARD]);
			objRet.Comment = Convert.ToString(prow[RC.COMMENT]);

			return objRet;
		}


		// BELOW IS COMPLETED

		/// <summary>
		///  loop through all rows of InnerTable
		///  Set SummaryPoint = DeliveryPoint + ProductionPoint + QCPoint
		/// </summary>
		public void SumPoint_Delivery_Production_QualityControl()
		{
			decimal d1, d2 ,d3 , d4;

			foreach(DataRow drow in mdtbElementList.Rows)
			{
				d1 = 0m;
				d2 = 0m;
				d3 = 0m;
				d4 = 0m;
			
				if ( !(drow[RC.DELIVERY + RC.POINT] is DBNull) )						
					d1 = Convert.ToDecimal( drow[RC.DELIVERY + RC.POINT] ) ;

				if ( !(drow[RC.PRODUCTION + RC.POINT] is DBNull) )
					d2 = Convert.ToDecimal(  drow[RC.PRODUCTION + RC.POINT] ) ;

				if ( !(drow[RC.QC + RC.POINT] is DBNull) )						
					d3 = Convert.ToDecimal(  drow[RC.QC + RC.POINT]  ) ;

				d4 = d1 + d2 + d3;
				drow[RC.SUMMARY + RC.POINT] =	d4;				
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrSetToField"></param>
		/// <param name="pstrBaseOnField"></param>
		/// <param name="pdecFrom"></param>
		/// <param name="pdecTo"></param>
		/// <param name="pdecStep"></param>
		public void SetPointToFieldBaseOnOtherField(string pstrSetToField, string pstrBaseOnField, 
			decimal pdecFrom, decimal pdecTo, decimal pdecStep)
		{
			if(   (pdecFrom == pdecTo) ||   
				(  (Math.Abs(pdecFrom - pdecTo) / pdecStep ) != 10)  ||
				pdecStep == 0m )
			{
				return;
			}

			bool blnHigherIsBetter = pdecFrom > pdecTo;
			
			foreach(DataRow drow in mdtbElementList.Rows)
			{
				decimal decProgress = 0m;
				// if progress is null, we treat like it equals 0
				try
				{
					decProgress = Convert.ToDecimal(drow[pstrBaseOnField]);
				}
				catch{}
					
				drow[pstrSetToField] = CaculatePoint(blnHigherIsBetter, 
					decProgress,
					pdecFrom, pdecTo, pdecStep
					);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pblnHigherIsBetter">when pdecFrom > pdecTo and pdecFrom cost 10point, put TRUE here</param>
		/// <param name="pdecProgress">Progress to assess and caculate point</param>
		/// <param name="pdecFrom"></param>
		/// <param name="pdecTo"></param>
		/// <param name="pdecStep"></param>
		/// <returns></returns>
		private decimal CaculatePoint(bool pblnHigherIsBetter, decimal pdecProgress, 	
			decimal pdecFrom, decimal pdecTo, decimal pdecStep)
		{
			if(pblnHigherIsBetter)
			{
				return (pdecProgress - pdecTo) / pdecStep;				
			}
			else
			{
				return (10 + pdecFrom - pdecProgress) / pdecStep;				
			}			
		}
		

		#endregion      INNER DATA MANIPULATE FUNCTION

	}	// end class DataReportHelper

	/// <summary>
	/// Struct for storing Thread function parameter.
	/// We feed the Thead (running GetData Function) with an object of this type, to provided parameter for it to run.
	/// </summary>
	public struct ThreadParameter
	{
		public string ElementID;
		public string MaxVersion;

		public ThreadParameter(string p_ElementID, string p_MaxVersion)
		{
			this.ElementID = p_ElementID;
			this.MaxVersion = p_MaxVersion;
		}
	}

	/// <summary>
	/// this Report constant
	/// </summary>
	public struct RC
	{
		
		public static string DELIVERY = "Delivery";
		public static string PRODUCTION = "Production";
		public static string QC = "QC";
		public static string SUMMARY = "Summary";

		public static string PRODUCTIONLINEID = "ProductionLineID";
		public static string DEPARTMENT = "Department";
		public static string DEPARTMENTNAME = "DepartmentName";
		public static string PRODUCTIONLINE = "ProductionLine";
		public static string PRODUCTIONLINENAME = "ProductionLineName";
		public static string PROGRESS = "Progress";
		public static string RANK = "Rank";
		public static string POINT = "Point";

		public static string DELIVERYPLAN = "DeliveryPlan";
		public static string DELIVERYACTUAL = "DeliveryActual";
		public static string PROGRESSDELIVERY = "ProgressDelivery";
		public static string PRODUCTIONPLAN = "ProductionPlan";
		public static string PRODUCTIONACTUAL = "ProductionActual";
		public static string PROGRESSPRODUCTION = "ProgressProduction";
		
		public static string PPM = "PPM";

		public static string STANDARD = "Standard";
		public static string COMMENT = "Comment";
	}

	/// <summary>
	/// Element
	/// </summary>
	public class ELEMENT : IComparable
	{
		#region PRIVATE (BUT PUBLIC FOR LAZY IMPLEMENTATION) FIELDS				
		public int ProductionLineID;
		public string Department;
		public string ProductionLine;

		public decimal DeliveryProgress;
		public decimal DeliveryRank;
		public decimal DeliveryPoint;

		public decimal ProductionProgress;
		public decimal ProductionRank;
		public decimal ProductionPoint;

		public decimal QualityPPM;
		public decimal QualityRank;
		public decimal QualityPoint;

		public decimal SummaryPoint;
		public decimal SummaryRank;

		public decimal Standard;

		public string Comment;

		#endregion

		#region PUBLIC PROPERTIES		
		
		/// <summary>
		///  lazy implementation of data object, public all member field because this is a simple data object.
		/// </summary>

		#endregion
	
		
		#region PUBLIC METHODS		
		public ELEMENT()
		{
		}		

		public ELEMENT(int pintProductionLineID,
			string pstrDepartment, string pstrProductionLine,
			decimal pdecDeliveryProgress, decimal pdecDeliveryRank, decimal pdecDeliveryPoint,
			decimal pdecProductionProgress, decimal pdecProductionRank, decimal pdecProductionPoint,
			decimal pdecQualityPPM, decimal pdecQualityRank, decimal pdecQualityPoint,
			decimal pdecSummaryPoint, decimal pdecSummaryRank, 
			decimal pdecStandard,
			string pstrComment)
		{
			this.ProductionLineID = pintProductionLineID;
			 this.Department = pstrDepartment;
			 this.ProductionLine = pstrProductionLine;
			 this.DeliveryProgress = pdecDeliveryProgress; 
			 this.DeliveryRank =  pdecDeliveryRank; 
			 this.DeliveryPoint =  pdecDeliveryPoint;
			 this.ProductionProgress =  pdecProductionProgress; 
			 this.ProductionRank =  pdecProductionRank; 
			 this.ProductionPoint =  pdecProductionPoint;
			 this.QualityPPM =  pdecQualityPPM; 
			 this.QualityRank =  pdecQualityRank; 
			 this.QualityPoint =  pdecQualityPoint;
			 this.SummaryPoint =  pdecSummaryPoint; 
			 this.SummaryRank =  pdecSummaryRank; 
			this.Standard =  pdecStandard; 
			 this.Comment = pstrComment;
		}

	
		#endregion	

		#region IComparable Members

		public int CompareTo(object obj)
		{
			if (!(obj is ELEMENT))
				throw new ArgumentException();

			ELEMENT t2 = (ELEMENT) obj;

			//first compare ProductionLineID			
			int cmp1 = this.ProductionLineID.CompareTo(t2.ProductionLineID);
			if (cmp1 != 0) return cmp1;
			return this.ProductionLine.CompareTo(t2.ProductionLine);			
		}

		#endregion
	}	// end class ELEMENT


	public class ThreadReturnValue
	{
		private int mnElementID;
		private string mstrProductionProgress;
		private string mstrDeliveryProgress;
		private string mstrPPM;
		
		public int ElementID
		{
			get
			{
				return mnElementID;
			}
			set
			{
				mnElementID = value;
			}
		}
		public string ProductionProgress
		{
			get
			{
				return mstrProductionProgress == string.Empty ? "0" : mstrProductionProgress;
			}
			set
			{
				mstrProductionProgress = value;
			}
		}
		public string DeliveryProgress
		{
			get
			{
				return mstrDeliveryProgress == string.Empty? "0" : mstrDeliveryProgress;
			}
			set
			{
				mstrDeliveryProgress = value;
			}
		}
		public string PPM
		{
			get
			{
				return mstrPPM == string.Empty? "0":mstrPPM ;
			}
			set
			{
				mstrPPM = value;
			}
		}

		

		public ThreadReturnValue(){}
		public ThreadReturnValue(int pintElementID, string pstrProductionProgress, string pstrDeliveryProgress, string pstrPPM)
		{
			this.ElementID = pintElementID;
			this.ProductionProgress = pstrProductionProgress;
			this.DeliveryProgress = pstrDeliveryProgress;
			this.PPM = pstrPPM;
		}
	}


	///  REMEMBER TO REMOVE THE SHORT CIRCUIT IN CHILD REPORT CLASS	 
	///  find the SHORT CIRCUIT  keyword
	///
	#region       CHILD REPORT
	/// <summary>
	/// <author>Thachnn</author>
	/// This report is a very complex report.
	/// It combines severals .NET DataTables with C1Report and Interop with Excel to get some chart images.
	/// 
	/// 
	/// --- The first 3 rows: using Sum() function of C1Report VBScript to calculate. Sum() function work rather well.
	/// 
	/// --- Auto generate the DayInMonth, DayOfWeek serries (by using 2 fuctions of mine from ReportBuilder)
	/// 
	/// --- In the detail section:
	/// There are many fields put here (about 200fields). Some info-fields like PartNo, PartName, Model. .... and a large pack of indexed fields (from 1 till 31).
	/// They are fldPlanxx, fldAdjxx, fldActualxx, fldReturnxx, fldProgressDayxx, fldProgressAccumulatexx and fldAssessmentxx.
	/// We do not spread these 200 fields vertically. We group 'em into 6 rows, 
	/// Plan, Adj and Actual, Return, ProgressDay field are fill from tables (built in C# code). 
	/// Other fields like , Progress Accumulate, Assessment was calculate on render time, 
	/// (by VBScript on the C1Report XML layout file)
	/// But if we render in render time by C1, there are some problems we can't hanlde, C1 is rather bad if we process too much field calculating.
	/// 
	/// Actual -- get from dtbActualTable
	/// REturn -- get from dtbReturnTable
	/// --- all indexed fields will be re-render, re-spread depending on the number of day in month ( using the Spread function of mine in the ReportBuilder)
	/// 
	/// --- There are some sumRow() cell at the end of each line (in detail fields). 
	///			 They are generated by a small program. they have text like: fldPlan01+fldPlan02+ ...
	///			 These C1 VB may work well, but I really don't love it like the first time I see C1. Sometime it raise a very crazy unknown bsht.
	/// --- count "Achieve", count "Not Achieve" on each row is now process by the C1
	/// 
	/// --- Some field on the report is calculate by the VBScript (store on the report layout XML file). 
	/// For example: calc some percent cell on each detail row.
	/// Remember that the C1Report VBScript count() function is not work well , so in the last to rows (grand sum by day, at the bottom of report): AchieveDay and Not Achieve/Day, we should calculate these values by C# (and I did that)
	/// 
	/// --- About the chart:
	/// We put 3 first rows of report (PlanTotal, Actual Total, Progress Total) to the Excel files to generate the DetailChart
	/// We put AchievePercent (right most, bottom most field) to the "ProportionAchievePlan" Excel cell
	/// put ProportionStandard (right most, near top field) to the "ProportionStandard" Excel cell
	/// Then we copy 2 chart from Excel file to images, bind to the fldChart and fldTotalChart on the C1Report
	/// 
	/// The last line I drop here: DON"T BELIEVE IN 3rd Vendor COmponent Provider. C1REport is blsht when you processing huge report.
	/// </summary>
	[Serializable]	
	public class ProductionLineDeliveryProgressReport : MarshalByRefObject, IDynamicReport		            
	{
		#region IDynamicReport Implementation
	
		private string mConnectionString;
		private ReportBuilder mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = false;	

		private string mstrReportDefinitionFolder = string.Empty;
		string mstrReportLayoutFile = string.Empty;

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


	
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
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

		

		public ProductionLineDeliveryProgressReport()
		{
		}	
	
		#region GLOBAL CONSTANT
		
		const string THIS = "ExternalReportFile:ProductionLineDeliveryProgressReport";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "ProductionLineDeliveryProgressReport";	
		
		const string ZERO_STRING = "0";
		const string ASSESSMENT_OK = "O";
		const string ASSESSMENT_NG = "X";
		const string MONTH_DATE_FORMAT = "MMM";

		/// Report layout file constant
		const string REPORT_LAYOUT_FILE = "ProductionLineDeliveryProgressReport.xml";
		const string REPORT_NAME = "ProductionLineDeliveryProgressReport";
		short COPIES = 1;

		/// all parameter are Mandatory
		const string REPORTFLD_PARAMETER_CCN						= "fldParameterCCN";
		const string REPORTFLD_PARAMETER_MONTH					= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_YEAR						= "fldParameterYear";			
		const string REPORTFLD_PARAMETER_ELEMENT		
			= "fldParameterProductionLine";
		const string REPORTFLD_PARAMETER_VERSION1			= "fldParameterVersion1";
		const string REPORTFLD_PARAMETER_VERSION2			= "fldParameterVersion2";
		const string REPORTFLD_PROPORTIONSTANDARDPERCENT	= "fldProportionStandardPercent";


		/// Result Data Table Column names, contain:
		/// PRODUCTID, CATEGORY,PARTNO,MODEL,BEGIN
		/// DATE
		/// PLAN+QUANTITY, ACTUAL+QUANTITY
		const string PRODUCTID = "ProductID";
		const string CATEGORY = "Category";
		const string PARTNO = "PartNo";
		const string PARTNAME = "PartName";
		const string MODEL = "Model";
		const string BEGIN = "ProgressBeginQuantity";

		const string DATE = "Day";
		const string QUANTITY = "Quantity";	// suffix for PLAN,ACTUAL , RETURN column

		const string VERSION = "Version";

		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYOFWEEK = "lblDayOfWeek";


		/// other constants			
		const string PLAN = "Plan";
		const string SO = "SO";
		const string WOBOM = "WOBOM";

		const string ADJ = "Adj";
		const string ACTUAL = "Actual";
		const string PROGRESSDAY = "ProgressDay";
		const string PROGRESS = "ProgressAccumulate";
		const string ASSESSMENT = "Assessment";
		//		const string RETURN = "Return";
		const string ROWCOUNTPASS = "RowCountPass";
		const string ROWCOUNTFAIL = "RowCountFail";
		const string ROWPERCENT = "RowPercent";

		const string FLD = "fld";		
		const string LBL = "lbl";
		const string HEADING = "DayHeading";

		const string REPORTFLD_TITLE = FLD + "Title";


		const string PLANFAIL = "PlanFailD";
		const string PLANPASS = "PlanPassD";

		/// chart fields
		const string REPORTFLD_CHART	= "fldChart";
		const string REPORTFLD_TOTALCHART = "fldTotalChart";

		const string REPORTFLD_TOTALPASS = "fldPlanPassSumRow";
		const string REPORTFLD_TOTALFAIL = "fldPlanFailSumRow";		


		string META_TABLE_NAME = "MetaTable";
		string PLAN_TABLE_NAME_1 = "PlanTable1";
		string PLAN_TABLE_NAME_2 = "PlanTable2";
		string ACTUAL_TABLE_NAME = "ActualTable";
		string ADJ_TABLE_NAME = "AdjTable";
		string SO_TABLE_NAME = "SOTable";
		string WOBOM_TABLE_NAME = "WOBOMTable";
		string BEGINQUANTITY_TABLE_NAME = "BEGINQUANTITYTable";
		//string RETURN_TABLE_NAME = "ReturnTable";

		#endregion GLOBAL CONSTANT


		#region GLOBAL VAR	

		DataSet dstMAIN = new DataSet();	

		#endregion GLOBAL VAR


		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrWorkOrderMasterID_1"></param>
		/// <param name="pstrWorkOrderMasterID_2"></param>
		/// <param name="pstrProportionStandard">You must fill 0.xx here ( a number less than 1 and greater or equal 0)</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, 
			string pstrProductionLineID /*report main element*/, 
			string pstrVersion_1, string pstrVersion_2, string pstrProportionStandard)
		{
			#region My variables						

			int nCCNID = int.Parse(pstrCCNID);
			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);			
			int nProductionLineID = int.Parse(pstrProductionLineID);
			int nVersion_2 = -1;	// if input is null or string.Empty, we get the maximum Version to calculate
			if(pstrVersion_2 == null || pstrVersion_2.Trim() == string.Empty)
			{
				nVersion_2	= GetMaxVersion(pstrCCNID);
			}
			else
			{
				nVersion_2	= int.Parse(pstrVersion_2);	
			}

			int nVersion_1 = -1;
			if(pstrVersion_1 == null || pstrVersion_1.Trim() == string.Empty)
			{
				nVersion_1 = GetPreviousVersion(pstrCCNID, pstrVersion_2);
			}

			// not mandatory, so we will the default value 0.95 for other processing
			double dblProportionStandard = 0.95d;
			dblProportionStandard = ReportBuilder.ToDouble(pstrProportionStandard);			
			
			//  for display on the Report parameter Section
			string strReportParameter_CCN = string.Empty;
			string strReportParameter_Month = pstrMonth;
			string strReportParameter_Year = pstrYear;
			string strReportParameter_ProductionLine = string.Empty;
			string strReportParameter_Version1 = (nVersion_1 < 0 ? pstrVersion_1 : nVersion_1.ToString()  );
			string strReportParameter_Version2 =  (pstrVersion_2 == string.Empty  ? (nVersion_2 + " (Lastest)" ) : pstrVersion_2);
						
			float fActualPageSize = 9000.0f;			

			/// contain array of string: Name of the column (with days have data in the dtbSourceData)
			/// FOr Example:
			/// dtbSourceData contain: 01-Oct: has Plan Quantity
			/// 02-Oct has Actual Quantity
			/// So arrHasValueDateHeading contain: Plan01, Actual02
			ArrayList arrHasValueDateHeading = new ArrayList();				

			/// Keep count of PlanPass and PlanFail for all days (columns).
			Hashtable arrColumnPass = new Hashtable();
			Hashtable arrColumnFail = new Hashtable();

			/// Keep count of PlanPass and PlanFail for all ITEMS  (rows).
			Hashtable arrRowPass = new Hashtable();
			Hashtable arrRowFail = new Hashtable();
			
			// get data and cache all in the dstMAIN			
			dstMAIN = GetDataAndCache(pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID, 
				nVersion_1.ToString() , nVersion_2.ToString() , pstrProportionStandard);	
			dstMAIN.DataSetName = pstrCCNID + pstrYear + pstrMonth + pstrProductionLineID + pstrVersion_1 + pstrVersion_2 + pstrProportionStandard;			

			System.Data.DataTable dtbMetaTable;
			dtbMetaTable  = dstMAIN.Tables[META_TABLE_NAME];		

			System.Data.DataTable dtbPlanTable;
			dtbPlanTable  = dstMAIN.Tables[PLAN_TABLE_NAME_2];		
			// Modify the PLAN TABLE - get the real PlanDay (depend on Working Time of active working day)
			dtbPlanTable = ModifyPlanTable(dtbPlanTable, pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID);
			dtbPlanTable = SumAndGroupBy(dtbPlanTable, PRODUCTID, PLAN + DATE, PLAN + QUANTITY);
			
			System.Data.DataTable dtbSOTable;
			dtbSOTable  = dstMAIN.Tables[SO_TABLE_NAME];

			System.Data.DataTable dtbWOBOMTable;
			dtbWOBOMTable = dstMAIN.Tables[WOBOM_TABLE_NAME];

			System.Data.DataTable dtbBEGINTable;
			dtbBEGINTable = dstMAIN.Tables[BEGINQUANTITY_TABLE_NAME];

			System.Data.DataTable dtbActualTable;
			dtbActualTable = dstMAIN.Tables[ACTUAL_TABLE_NAME];

			System.Data.DataTable dtbPlanTableWO1;
			System.Data.DataTable dtbAdjTable;
			dtbPlanTableWO1  = dstMAIN.Tables[PLAN_TABLE_NAME_1];
			
			dtbAdjTable = BuildAdjTable(dtbPlanTableWO1 , dtbPlanTable);
			if(pstrVersion_1 == pstrVersion_2)	// short circuit, make faster when 2 version is the same, all adjust field is null (zer0)
			{
				dtbAdjTable = new DataTable(ADJ_TABLE_NAME);
				dtbAdjTable.Columns.Add(PRODUCTID);
				dtbAdjTable.Columns.Add(ADJ + DATE, typeof(Int32) );
				dtbAdjTable.Columns.Add(ADJ + QUANTITY, typeof(Decimal) );
			}
			dtbAdjTable.TableName = ADJ_TABLE_NAME;
			dstMAIN.Tables.Add(dtbAdjTable);
				
			//System.Data.DataTable dtbReturnTable;
			//dtbReturnTable = dstMAIN.Tables[RETURN_TO_VENDOR_TABLE_NAME];
		
			#endregion  My Variables

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strReportParameter_CCN = boUtil.GetCCNCodeFromID(nCCNID);	
			strReportParameter_ProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + ": " + objBO.GetProductLineNameFromID(nProductionLineID);
			
			#endregion	
			
			/// transform TABLE column names
			/// transform TABLE will contain :
			/// PRODUCTID, 
			/// META INFO  = CATEGORY,PARTNO,MODEL,
			/// BEGIN QUANTITY
			/// PLAN+i.ToString("00")
			/// ADJ +i.ToString("00")
			/// ACTUAL+i.ToString("00")
			/// RETURN+i.ToString("00")
			/// ProgressDay, Progress, Assessment
			#region TRANSFORM ORIGINAL TABLE FOR REPORT		
	
			#region GETTING THE DATE HEADING
			/// arrPlanDate and arrActualDate contain DateTime object from actual dtbSourceData
			ArrayList arrPlanDate = GetColumnValuesFromTable(dtbPlanTable,PLAN+DATE);
			arrPlanDate = GetColumnValuesFromTable(dtbSOTable,SO+DATE, arrPlanDate );
			arrPlanDate = GetColumnValuesFromTable(dtbWOBOMTable,WOBOM+DATE, arrPlanDate);

			ArrayList arrActualDate = GetColumnValuesFromTable(dtbActualTable,ACTUAL+DATE);
			//ArrayList arrReturnDate = GetColumnValuesFromTable(dtbReturnTable,RETURN+DATE);			
			ArrayList arrAdjDate = GetColumnValuesFromTable(dtbAdjTable,ADJ+DATE);

			//ArrayList arrItems = GetCategory_PartNo_Model_ProductID_FromTable(dtbPlanTable,CATEGORY,PARTNO,MODEL,PRODUCTID);
			ArrayList arrItems = GetColumnValuesFromTable(dtbPlanTable, PRODUCTID);

			/// PUSH: has-value (in the dtbSourceData) to the arrHasValueDateHeading
			/// 
			/// HACKED: Thachnn: 20/12/2005
			/// don't remove this dummy code of casting object in arrPlanDate to int
			/// because sometime, data in the database is not correct, return dbnull to the arrPlanDate. If we use normal foreach(int nDay in arrPlanDate)
			/// exception of Invalid cast will be throw
			/// In this case: ActualDate can be omit and = DBNull because an Item can be Plan, but it didn't produce in any day in this month
			foreach(object obj  in arrPlanDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = PLAN + dtm.Day.ToString("00");					
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			
			foreach(object obj in arrActualDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = ACTUAL + dtm.Day.ToString("00");					
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			//			foreach(object obj in arrReturnDate)
			//			{
			//				try
			//				{
			//					int nDay = (int)obj;
			//					DateTime dtm = new DateTime(nYear,nMonth,nDay);
			//					string strColumnName = RETURN + dtm.Day.ToString("00");			
			//					arrHasValueDateHeading.Add(strColumnName);
			//				}
			//				catch{}
			//			}

			foreach(object obj in arrAdjDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = ADJ + dtm.Day.ToString("00");					
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			/// ENDHACKED: Thachnn: 20/12/2005
			/// after this snip of code. arrHasValueDateHeading will contain Actual01, Actual02 or Plan03 Plan04, or Adj03 Adj04, Return02, Return03  ... 
			/// depend on the DataTable
			/// Which day has value (Plan , Adj, or Actual,Return), the columnName will exist in the arrHasValueDateHeading
			/// and then, the Transform DataTable dtbTransform will has some columns named like string in arrHasValueDateHeading			

			#endregion		
            			
			DataTable dtbTransform = BuildTransformTable(arrHasValueDateHeading);
		
			#endregion  TRANSFORM ORIGINAL TABLE FOR REPORT
						
			#region FILL ABSOLUTE DATA FROM Plan && Actual && Adjust && Return to the TRANSFORM DATATABLE
			
			/// GUIDE: with each Items
			foreach(object obj /* ProductID */  in arrItems)
			{
				string strItem = obj.ToString();
				// Create DUMMYROW FIRST
				DataRow dtrNew = dtbTransform.NewRow();

				#region	- fill ITEM meta info to the new dummy row				
				
				string strFilterMeta = string.Empty;
				
				strFilterMeta = string.Format("[{0}]='{1}' ",		
					PRODUCTID,	strItem);

				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrows = dtbMetaTable.Select(strFilterMeta);

				/// GUIDE: for each rows in result (datarow contain map ProductID -- MetaInfo)
				foreach(DataRow dtr in dtrows)
				{
					// fill data to the dummy row
					dtrNew[PRODUCTID] = dtr[PRODUCTID];
					dtrNew[CATEGORY] = dtr[CATEGORY];
					dtrNew[PARTNO] = dtr[PARTNO];
					dtrNew[PARTNAME] = dtr[PARTNAME];
					dtrNew[MODEL] = dtr[MODEL];
					// TODO: Thachnn: maybe we will ad Begin Quantity to the MetaInfo Table @!!!   dtrNew[BEGIN] = dtr[BEGIN];
				}

				#endregion	- fill ITEM meta info to the new dummy row
			
				#region	- fill PLAN quantity to the new dummy row				
								
				string strFilterPlan = string.Empty;
				
				strFilterPlan = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);			
				
				/// GUIDE: get all rows of this Item from the dtbPlan
				DataRow[] dtrowsPlan = dtbPlanTable.Select(strFilterPlan);

				/// GUIDE: for each rows in of this Item OF dtbPlan - fill plan quantity ITEM
				foreach(DataRow dtr in dtrowsPlan)
				{					
					/// Fill Plan Quantity to destination column of Transform table, in this new rows					
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[PLAN+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[PLAN+QUANTITY];				
				}

				#endregion - fill PLAN quantity to the new dummy row
				
				#region	- fill SO quantity to the new dummy row				
								
				string strFilterSO = string.Empty;
				
				strFilterSO = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);			
				
				/// GUIDE: get all rows of this Item from the dtbSO
				DataRow[] dtrowsSO = dtbSOTable.Select(strFilterSO);

				/// GUIDE: for each rows in of this Item OF dtbSO - fill SO quantity ITEM
				foreach(DataRow dtr in dtrowsSO)
				{
					/// ADD SO Quantity to PLAN COLUMN
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[SO+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = ReportBuilder.ToDecimal( dtrNew[strDateColumnToFill])  + ReportBuilder.ToDecimal( dtr[SO+QUANTITY]) ;
				}

				#endregion - fill SO quantity to the new dummy row

				#region	- fill WOBOM quantity to the new dummy row
								
				string strFilterWOBOM = string.Empty;
				
				strFilterWOBOM = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);
				
				/// GUIDE: get all rows of this Item from the dtbWOBOM
				DataRow[] dtrowsWOBOM = dtbWOBOMTable.Select(strFilterWOBOM);

				/// GUIDE: for each rows in of this Item OF dtbWOBOM - fill WOBOM quantity ITEM
				foreach(DataRow dtr in dtrowsWOBOM)
				{
					/// ADD WOBOM Quantity to PLAN COLUMN
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[WOBOM+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = ReportBuilder.ToDecimal( dtrNew[strDateColumnToFill])  + ReportBuilder.ToDecimal( dtr[WOBOM+QUANTITY]) ;
				}

				#endregion - fill WOBOM quantity to the new dummy row

				// TODO: Thachnn: Add BEGINQUANTITY Table here
				#region - fill BEGIN quantity to the new dummy row
				
				string strFilterBEGIN = string.Empty;
				strFilterBEGIN = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem	);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsBEGIN = dtbBEGINTable.Select(strFilterBEGIN);

				/// GUIDE: for each rows  of this Item in BEGIN Completion DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsBEGIN)
				{
					/// Fill BEGIN Quantity to destination column of Transform table, in this new rows					
					string strDateColumnToFill = BEGIN;
					dtrNew[strDateColumnToFill] = dtr[BEGIN];
				}
				#endregion - fill BEGIN  quantity to the new dummy row

				#region - fill ACTUAL quantity to the new dummy row
				
				string strFilterActual = string.Empty;
				strFilterActual = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem	);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsActual = dtbActualTable.Select(strFilterActual);

				/// GUIDE: for each rows  of this Item in Actual Completion DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsActual)
				{
					/// Fill Actual Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = ACTUAL + ((DateTime)dtr[ACTUAL+DATE]).Day.ToString("00");
					string strDateColumnToFill = ACTUAL + Convert.ToInt32( dtr[ACTUAL+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[ACTUAL+QUANTITY];
				}
				#endregion - fill ACTUAL  quantity to the new dummy row

				#region - fill ADJUST quantity to the new dummy row
				
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilterAdjust = string.Empty;
				strFilterAdjust = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem		);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsAdjust = dtbAdjTable.Select(strFilterAdjust);

				/// GUIDE: for each rows  of this Item in Adjust DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsAdjust)
				{
					/// Fill Actual Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = ADJ + ((DateTime)dtr[ADJ +DATE]).Day.ToString("00");
					string strDateColumnToFill = ADJ + Convert.ToInt32( dtr[ADJ+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[ADJ+QUANTITY];
				}
				#endregion - fill ADJUST quantity to the new dummy row
	
				#region - fill RETURN quantity to the new dummy row

				//				/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
				//				/// so we put IsNull in the filter string (to select from dtbResult);
				//				string strFilterReturn = string.Empty;
				//				strFilterReturn = 
				//					string.Format("[{0}]='{1}' ",
				//					PRODUCTID,
				//					strItem.Split('#')[3]
				//					);		
				//				
				//				/// GUIDE: get all rows of this Item from the dtbSourceData
				//				DataRow[] dtrowsReturn = dtbReturnTable.Select(strFilterReturn);
				//
				//				/// GUIDE: for each rows  of this Item in Return DataTable- fill return quantity to the dummy ROW
				//				foreach(DataRow dtr in dtrowsReturn)
				//				{
				//					/// Fill Return Quantity to destination column of Transform table, in this new rows
				//					//strDateColumnToFill = RETURN + ((DateTime)dtr[RETURN+DATE]).Day.ToString("00");
				//					string strDateColumnToFill = RETURN + Convert.ToInt32( dtr[RETURN+DATE]).ToString("00");
				//					dtrNew[strDateColumnToFill] = dtr[RETURN+QUANTITY];
				//				}
				#endregion - fill RETURN quantity to the new dummy row


				// add to the transform data table
				dtbTransform.Rows.Add(dtrNew);				
			}	    
			#endregion FILL DATA FROM Plan DTB && ActualCompletion DTB && Adjust DTB to the TRANSFORM DATATABLE

			
			#region CALCULATE the Sum of Plan, sum of Actual, sum of Progress (on top of the report) to generate a chart in EXCEL			
			double[,] arrSumPlan = new double[1,31];
			double[,] arrSumActual = new double[1,31];
			double[,] arrSumProgress = new double[1,31]; 
			
			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth)  ; i++)
			{
				string strCounter = i.ToString("00");

				/// sum on the top of the report, calculate to put in the excel file to generate a chart.
				//string str = "Sum(Plan"+i.ToString("00")+")";
				try
				{
					arrSumPlan[0,i-1] = double.Parse(dtbTransform.Compute("Sum(Plan"+i.ToString("00")+")" , string.Empty ).ToString());
				}
				catch{}
				
				try
				{
					arrSumActual[0,i-1] = double.Parse(dtbTransform.Compute("Sum(Actual"+i.ToString("00")+")" , string.Empty).ToString());
				}
				catch{}

				/// progress SUm will be caculate in the next section , after render the report, we will get the real value of upper sum field on report
				/// because the progress value is caculate on render time, depend on the real actual data on the rendered report				
			}		// end foreach Day(i)
			#endregion calculate the Sum of Plan, sum of Actual, sum of Progress (on top of the report) to generate a chart in EXCEL

			#region CALCULATE the ProgressDay column

			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth); i++)
			{
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);				
				foreach(DataRow rowItem in dtbTransform.Rows)
				{
					decimal decPlan = ReportBuilder.ToDecimal(rowItem[PLAN+strCounter]);
					decimal decActual = ReportBuilder.ToDecimal(rowItem[ACTUAL+strCounter]);
					//					decimal decReturn = ReportBuilder.ToDecimal(rowItem[RETURN+strCounter]);					

					rowItem[PROGRESSDAY+strCounter] = decActual - decPlan; // - decReturn;
				}			
			}	

			#endregion calculate the ProgressDay column

			#region CALCULATE , fill Progress quantity to the new dummy row

			for(int i = 1 ; i <= 31 /*DateTime.DaysInMonth(nYear,nMonth)*/  ; i++)
			{
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);
                
				foreach(object obj  in arrItems)
				{
					string strItem = obj.ToString();					
					string strFilterProgress = 
						string.Format("[{0}]='{1}' ",
						PRODUCTID,	strItem	);
				
					/// GUIDE: get rows ( in fact, it is only one) of this Item from the dtbTransform
					DataRow[] dtrowsItemAllInfo = dtbTransform.Select(strFilterProgress);
			
					decimal decCurrentACTUAL = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][ACTUAL+ i.ToString("00")] );
					decimal decCurrentPLAN = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][PLAN+ i.ToString("00")]) ;
					//					decimal decCurrentRETURN = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][RETURN+ i.ToString("00")]) ;
					
					decimal decPreviousPROGRESS = decimal.Zero;
					if(i == 1)
						decPreviousPROGRESS = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][BEGIN] );
					else					
						decPreviousPROGRESS = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][PROGRESS + (i-1).ToString("00")]  ) /*Previous*/ ;					

					dtrowsItemAllInfo[0][PROGRESS + i.ToString("00")] = 
						decPreviousPROGRESS
						+decCurrentACTUAL
						-decCurrentPLAN;
					//						-decCurrentRETURN;
					
				}	// end each Items (of current day  = i)
			}		// end foreach Day(i)
			
			#endregion - calculate , fill Progress quantity to the new dummy row			
			
			// keep sum of whole report PASS or FAIL
			int intTotalCountPass = 0;			int intTotalCountFail = 0;						
			#region ASSESS the PROGRESS, fill ASSESSMENT and CALCULATE the count of FAIL and PASS
			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth); i++)
			{
				int intColumnPass = 0;			int intColumnFail = 0;
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);		
				
				foreach(DataRow rowItem in dtbTransform.Rows)
				{					
					decimal decPlan = ReportBuilder.ToDecimal(rowItem[PLAN+strCounter]);
					decimal decActual = ReportBuilder.ToDecimal(rowItem[ACTUAL+strCounter]);
					//					decimal decReturn = ReportBuilder.ToDecimal(rowItem[RETURN+strCounter]);
					decimal decProgressDay = decActual - decPlan; // - decReturn;
					decimal decProgress = ReportBuilder.ToDecimal(rowItem[PROGRESS+strCounter]);

					if (decPlan == decimal.Zero  && decActual == decimal.Zero )
					{ /* Ignore, don't assess the progress */ }
					else
					{
						if (decProgressDay == decimal.Zero)
						{
							rowItem[ASSESSMENT+strCounter] = ASSESSMENT_OK;
							intTotalCountPass ++;	// total
							rowItem[ROWCOUNTPASS] = ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) + 1;
							intColumnPass++;							
						}
						else if ((decProgressDay > 0) && (decProgress <= 0))
						{
							rowItem[ASSESSMENT+strCounter] = ASSESSMENT_OK;
							intTotalCountPass ++;
							rowItem[ROWCOUNTPASS] = ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) + 1;
							intColumnPass++;
						}
						else
						{
							rowItem[ASSESSMENT+strCounter] = ASSESSMENT_NG;
							intTotalCountFail ++;
							rowItem[ROWCOUNTFAIL] = ReportBuilder.ToInt32(rowItem[ROWCOUNTFAIL]) + 1;
							intColumnFail++;
						}
					}										
				}	// end each rowItem in Transform table				
				arrColumnPass.Add(FLD + PLANPASS + strCounter, intColumnPass);
				arrColumnFail.Add  (FLD + PLANFAIL  + strCounter, intColumnFail );
			}	// end foreach i			

			#endregion calculate the count of Plan FAIL and Plan PASS			

			#region CALCULATE the Percent (column in dtbTransform) for each Row
			foreach(DataRow rowItem in dtbTransform.Rows)
			{
				int nSum = ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) + ReportBuilder.ToInt32(rowItem[ROWCOUNTFAIL]);
				if( nSum != 0 )
				{
					double dblPercent = (double)ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) / nSum;
					rowItem[ROWPERCENT] = (dblPercent*100).ToString("#0.00") + "%";
				}					
			}
			#endregion CALCULATE the Percent for each Row



			#region  SHORT CIRCUIT this function (uncomment these line for the summary report: ProductionLineAssessment)
						if((intTotalCountPass + intTotalCountFail) == 0 )
						{
							mResult = "-";
						}
						else
						{
							decimal decTemp  = ( (decimal)intTotalCountPass * 100) / (intTotalCountPass + intTotalCountFail) ;
							mResult = decTemp.ToString("#,##0.00");
						}
						return dtbTransform;
			#endregion SHORT CIRCUIT this function
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
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName, ArrayList arrOriginal)
		{
			ArrayList arrRet = arrOriginal;
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
				// arrRet.Clear();
			}
			return arrRet;
		}



		/// <summary>
		/// build a new datatable with column = productid, category,partno,model,begin,
		/// and somecolumn with names in arrHasValueDateHeading
		/// Index column is : Plan, Adj, Actual, Return, ProgressDay, Progress Accumulate, Assessment
		/// 
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildTransformTable(ArrayList parrHasValueDateHeading)
		{
			DataTable dtbRet = new DataTable(TABLE_NAME);
			dtbRet.Columns.Add(PRODUCTID,typeof(System.Int32) );
			dtbRet.Columns.Add(CATEGORY,typeof(System.String) );
			dtbRet.Columns.Add(PARTNO,typeof(System.String));
			dtbRet.Columns.Add(PARTNAME,typeof(System.String));
			dtbRet.Columns.Add(MODEL,typeof(System.String));
			dtbRet.Columns.Add(BEGIN,typeof(System.Double));			
			dtbRet.Columns.Add(ROWCOUNTPASS,typeof(System.Int32));
			dtbRet.Columns.Add(ROWCOUNTFAIL,typeof(System.Int32));
			dtbRet.Columns.Add(ROWPERCENT,typeof(System.String));

			/// fill the column (Double type) in which the date exist in the dtbSourceData (has value contain in the parrDueDateHeading)
			/// then fill the column with String type (so that it will display correctly in the report, not #,##0.00, because it has null value)
					
			foreach(string strColumnName in parrHasValueDateHeading)
			{					
				try
				{
					dtbRet.Columns.Add(strColumnName,typeof(System.Double));
				}
				catch{}
			}
			// FILL the null column				
			for(int i = 1; i <=31; i++)												  
			{
				if(parrHasValueDateHeading.Contains(PLAN + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(PLAN + i.ToString("00"),typeof(System.String));						
					}
					catch{}
				}
				if(parrHasValueDateHeading.Contains(ADJ + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(ADJ + i.ToString("00"),typeof(System.String));
					}
					catch{}
				}
				if(parrHasValueDateHeading.Contains(ACTUAL + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(ACTUAL + i.ToString("00"),typeof(System.String));
					}
					catch{}
				}

				//				if(parrHasValueDateHeading.Contains(RETURN + i.ToString("00")) == false )
				//				{		
				//					try
				//					{
				//						dtbRet.Columns.Add(RETURN + i.ToString("00"),typeof(System.String));
				//					}
				//					catch{}
				//				}
				try
				{
					dtbRet.Columns.Add(PROGRESSDAY + i.ToString("00"),typeof(System.Double));
				}
				catch{}
				try
				{
					dtbRet.Columns.Add(PROGRESS + i.ToString("00"),typeof(System.Double));
				}
				catch{}

				try
				{
					dtbRet.Columns.Add(ASSESSMENT + i.ToString("00"),typeof(System.String));
				}
				catch{}

			} 	// FILL the null column
			
			return dtbRet;		
		}	// end build transform tables

		
		/// <summary>
		/// Thachnn : 08/Nov/2005
		/// Browse the DataTable, get all value of Category, PartNo column, insert into ArraysList as CategoryValue#PartNoValue
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrCategoryColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect CategoryValue#PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>		
		private ArrayList GetCategory_PartNo_Model_ProductID_FromTable(DataTable pdtb, string pstrCategoryColName, string pstrPartNoColName, string pstrModelColName, string pstrProductID)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objCategoryGet = drow[pstrCategoryColName];
					object objPartNoGet = drow[pstrPartNoColName];
					object objModelGet = drow[pstrModelColName];
					object objProductIDGet = drow[pstrProductID];
					string str = objCategoryGet.ToString().Trim() + "#" + objPartNoGet.ToString().Trim() + "#" + objModelGet.ToString().Trim() + "#" + objProductIDGet.ToString().Trim();
					if( !arrRet.Contains(str)  )
					{
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

	

		/// <summary>
		/// Thachnn: 10/11/2005
		/// execute the input sql clause
		/// return the object result
		/// throw all exception to outside
		/// </summary>
		/// <param name="pstrSql">SQL clause to execute</param>
		/// <returns>object</returns>
		public object ExecuteScalar(string pstrSql)
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

	



		/// <summary>
		/// <author>Thachnn</author>
		/// Get the datatable: 
		/// contain Mapping ProductID with it adjustment from TABLE1 to TABLE2, in a specific days in month)
		/// Return table will have schema like:
		/// ProductID - AdjDay - AdjQuantity
		/// 200	1	110.00000
		/// 200	2	10.00000		
		/// 127	13	53.00000
		/// 127	31	50.00000		
		/// </summary>
		/// <param name="pdtbPlanTable1"></param>
		/// <param name="pdtbPlanTable2"></param>
		/// <returns></returns>
		private DataTable BuildAdjTable(DataTable pdtbTable1, DataTable pdtbTable2 )
		{	
			/// TABLE RET = TABLE1 - TABLE2

			/// build schema for ADJ table			
			DataTable dtbRet = new DataTable(ADJ_TABLE_NAME);
			dtbRet.Columns.Add(PRODUCTID);
			dtbRet.Columns.Add(ADJ + DATE, typeof(Int32) );
			dtbRet.Columns.Add(ADJ + QUANTITY, typeof(Decimal) );
			
			// using inner datatable.  not modify the original input parameter (Table1 and Table2)
			DataTable dtb1 = pdtbTable1.Copy();
			DataTable dtb2 = pdtbTable2.Copy();

			/// FOREACH iROW IN TABLE2, 
			/// dtbRet newROW = iROW(2)
			/// if found relative-newROW    row in Table1 (productid is the same, planday is the same), 
			/// ---- subtract PlanQuantity of current row (newROW).
			/// ---- Add found row in Table1 in the UsedRowedInTableWO1
			/// add newROW to dtbRET
			/// 
			/// Table1.Remove(UsedRowedInTableWO1);
			/// 
			/// FOREACH iROW1 in TABLE1
			/// CLone, Negate and ADD to the dtbRet
			

            
			if(dtb1.Rows.Count == 0)
			{
				return dtbRet;
			}			

			ArrayList arrUsedRowInTable1 = new ArrayList();
			int nPID = int.MinValue;
			int nDay = int.MinValue;			
			foreach(DataRow iRow in dtb2.Rows)
			{
				DataRow newRow = dtbRet.NewRow();
				nPID = System.Convert.ToInt32(iRow[PRODUCTID]);
				nDay = System.Convert.ToInt32(iRow[PLAN + DATE]);
				decimal dblAQ = Convert.ToDecimal( iRow[PLAN + QUANTITY]  );

				decimal dblRelativeFromTable1 = 0;
				foreach( DataRow jRow in dtb1.Select("[PRODUCTID]=" +nPID+ " and [PLANDAY]=" +nDay)   )
				{
					dblRelativeFromTable1 += Convert.ToDecimal( jRow[PLAN + QUANTITY]) ;
					arrUsedRowInTable1.Add(jRow);	/// mark that we used this rows. We don't include its value later.
				}

				newRow[PRODUCTID] = nPID;
				newRow[ADJ + DATE] = nDay;
				newRow[ADJ + QUANTITY] = dblAQ - dblRelativeFromTable1;
				dtbRet.Rows.Add(newRow);
			}

			foreach(DataRow jRow in arrUsedRowInTable1)
			{
				try
				{
					dtb1.Rows.Remove(jRow);			
				}
				catch{}				
			}
            
			foreach(DataRow iRow in dtb1.Rows)
			{
				DataRow newRow = dtbRet.NewRow();
				newRow[PRODUCTID] = iRow[PRODUCTID];
				newRow[ADJ + DATE] = iRow[PLAN + DATE];
				try
				{
					
					newRow[ADJ + QUANTITY] = decimal.Negate( (decimal)iRow[PLAN + QUANTITY] );
				}
				catch{}				

				dtbRet.Rows.Add(newRow);
			}

			return dtbRet;
		}



		/// <summary>
		/// Get all data for this report and cache in the dstMAIN dataset
		/// just improve the speed for this report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <param name="pstrWorkOrderMasterID_1"></param>
		/// <param name="pstrWorkOrderMasterID_2"></param>
		/// <param name="pstrProportionStandard"></param>
		private DataSet GetDataAndCache(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrVersion_1, string pstrVersion_2, string pstrProportionStandard)
		{	
			DataSet dstRET = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			string pstrPreviousYear = pstrYear;
			string pstrPreviousMonth = pstrMonth;

			if(pstrMonth == "1" || pstrMonth == "01" )
			{
				pstrPreviousMonth = "12";
				pstrPreviousYear = (int.Parse(pstrYear) - 1).ToString();
			}
			else
			{
				pstrPreviousMonth = (int.Parse(pstrMonth) - 1).ToString();
			}



			#region MAIN SQL QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 
				" Declare @pstrMonth char(2) " + 
				" Declare @pstrYear char(4) " + 
				" Declare @pstrPreviousMonth char(2) " + 
				" Declare @pstrPreviousYear char(4) " + 

				" Declare @pstrProductionLineID int " + 
				" Declare @pstrVersion_1 int " + 			
				" Declare @pstrVersion_2 int " + 			
				"  " + 				
				" /*-----------------------------------*/ " + 
				"  " + 
				" Set @pstrCCNID = " + pstrCCNID + " " + 
				" Set @pstrYear = '" + pstrYear + "' " + 
				" Set @pstrMonth = '"+ pstrMonth +"' " + 
				" Set @pstrPreviousYear = '" + pstrPreviousYear + "' " + 
				" Set @pstrPreviousMonth = '"+ pstrPreviousMonth +"' " + 

				" Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 

				(pstrVersion_1.Trim() == string.Empty ?  (string.Empty) : (" Set @pstrVersion_1 = " +pstrVersion_1 + " ")  )  + 
				(pstrVersion_2.Trim() == string.Empty ?  (string.Empty) : (" Set @pstrVersion_2 = " +pstrVersion_2 + " ")  )  + 				
				"  " + 					
				"  " ;
			/*-----------------------------------*/

			#endregion MAIN QUERY

			#region META _DATA

			string strSql_META_TABLE =

				" select distinct  " + 
				" ITEM.ProductID as [ProductID] , " + 
				" ITEM.Code as [PartNo], " + 
				" ITEM.Description as [PartName], " + 
				" CAT.Code as [Category] , " + 
				" ITEM.Revision as [Model] " + 
				"  " + 
				" FROM PRO_WorkOrderMaster as WOMASTER " + 
				" join PRO_WorkOrderDetail as WODETAIL " + 
				" on WOMASTER.WorkOrderMasterID = WODETAIL.WorkOrderMasterID " + 
				" join ITM_Product ITEM " + 
				" on WODETAIL.ProductID = ITEM.ProductID " + 
				" join ITM_Category as CAT " + 
				" on ITEM.CategoryID = CAT.CategoryID " + 
				"  " + 
				" where WOMASTER.CCNID = @pstrCCNID " + 
				" and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" and DatePart(mm  ,DueDate) = @pstrMonth " + 
				" and DatePart(yyyy,DueDate) = @pstrYear " + 
				"  " ; 
			#endregion META _DATA


			#region PLANTABLE - 2 - MAIN

			/// Get the datatable : contain Mapping ProductID with it Plan from WO2, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - PlanDay - PlanQuantity
			/// 200	1	110.00000
			/// 200	2	10.00000					
			/// 127	31	50.00000		
			/// TODO: need to group by to make PID and PlanDay is unique key
			/// //private DataTable BuildPlanTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrWorkOrderMasterID)
			string strSqlPLANTABLE_2 =	/* newer */
                	
				/*************************PLAN TABLE *******************************************************************************/			
		
				" SELECT  " + 
				" PLANTABLE.ProductID, " + 
				" PLANTABLE.PlanDate, " + 
				" PLANTABLE.PlanQuantity " + 
				"  " + 
				" FROM " + 
				" ( " + 
				" 	/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	select  " + 
				" 	WODETAIL.ProductID as [ProductID], " + 
				" 	WODETAIL.StartDate as [PlanDate], " + 
				" 	SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 	DCOMASTER.Version " + 
				" 	 " + 
				" 	from " + 
				" 	PRO_WorkOrderDetail as WODETAIL " + 
				" 	join PRO_WorkOrderMaster WOMASTER " + 
				" 	on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 	and WOMASTER.CCNID = @pstrCCNID " + 
				" 	and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
				" 	and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
				" 	and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 	 " + 
				" 	join PRO_DCOptionMaster DCOMASTER " + 
				" 	on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 	and DCOMASTER.Version <= @pstrVersion_2		 " + 
				" 	and DCOMASTER.CCNID = @pstrCCNID		 " +  
				"			/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				"	and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				"	and /*Begin of current month*/ convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 
				" 	 " + 
				" 	group by  " + 
				" 	WODETAIL.ProductID, " + 
				" 	WODETAIL.StartDate, " + 
				" 	DCOMASTER.Version " + 
				" 	 " + 
				" 	/*************************PLAN TABLE *******************************************************************************/ " + 
				" ) as PLANTABLE " + 
				" join " + 
				" ( " + 
				" 	select " + 
				" 	PLANTABLE.ProductID, " + 
				" 	PLANTABLE.PlanDate, " + 
				" 	Max(PLANTABLE.Version) as [Version] " + 
				" 	 " + 
				" 	from  " + 
				" 	( " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 		select  " + 
				" 		WODETAIL.ProductID as [ProductID], " + 
				" 		WODETAIL.StartDate as [PlanDate], " + 
				" 		SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		from " + 
				" 		PRO_WorkOrderDetail as WODETAIL " + 
				" 		join PRO_WorkOrderMaster WOMASTER " + 
				" 		on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 		and WOMASTER.CCNID = @pstrCCNID " + 
				" 		and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
				" 		and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
				" 		and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 		 " + 
				" 		join PRO_DCOptionMaster DCOMASTER " + 
				" 		on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 		and DCOMASTER.Version <= @pstrVersion_2		 " + 
				" 		and DCOMASTER.CCNID = @pstrCCNID		 " + 
				"			/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				"	and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				"	and /*Begin of current month*/ convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 
				" 		 " + 
				" 		group by  " + 
				" 		WODETAIL.ProductID, " + 
				" 		WODETAIL.StartDate, " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	) as PLANTABLE " + 
				" 	group by  " + 
				" 	PLANTABLE.ProductID, " + 
				" 	PLANTABLE.PlanDate " + 
				" ) as MAXVERSIONTABLE " + 
				"  " + 
				" on PLANTABLE.ProductID = MAXVERSIONTABLE.ProductID " + 
				" and PLANTABLE.PlanDate = MAXVERSIONTABLE.PlanDate " + 
				" and PLANTABLE.Version = MAXVERSIONTABLE.Version " + 
				"  " 
				/*************************PLAN TABLE *******************************************************************************/
				;

			#endregion PLANTABLE - MAIN
			/* ============================================================== */

			#region PLANTABLE - 1 - NOT MANDATORY - Take all WorkOrder in month_Yearm CCN, where Version = Version_1

			/// Get the datatable : contain Mapping ProductID with it Plan from WO2, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - PlanDay - PlanQuantity
			/// 200	1	110.00000
			/// 200	2	10.00000
			/// 127	31	50.00000		
			/// TODO: need to group by to make PID and PlanDay is unique key
			/// //private DataTable BuildPlanTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrWorkOrderMasterID)
			string strSqlPLANTABLE_1 =

	
				/*************************PLAN TABLE *******************************************************************************/

				" SELECT  " + 
				" ITM_Product.ProductID as [ProductID], " + 
				" ITM_Category.Code as [Category],  " + 
				" ITM_Product.Code as [Part No.],  " + 
				" ITM_Product.Description as [PartName],  " + 
				" ITM_Product.Revision  as [Model], " + 
				" IsNull(PROGRESSBEGINQUANTITYTABLE.ProgressBeginQuantity,0.00) as [ProgressBeginQuantity], " + 
				" PLANTABLE.PlanDay as [PlanDay], " + 
				" IsNull(PLANTABLE.PlanQuantity,0.00) as [PlanQuantity] " + 
				"  " + 
				" FROM " + 
				"  " + 
				" (	/*PLANTABLE*/ " + 
				" 	select INNERTABLE.ProductID, INNERTABLE.PlanDay, Sum(IsNull(INNERTABLE.PlanQuantity, 0.00) ) as [PlanQuantity] from  " + 
				" 	(  " + 
				" 		/* INNERTABLE*/ " + 
				" 		select  " + 
				" 		isNull(WORKORDERTABLE.ProductID,isNull(WOBOMUSETABLE.ProductID,SOTABLE.ProductID)) as [ProductID], " + 
				" 		isNull(WORKORDERTABLE.PlanDay,isNull(WOBOMUSETABLE.WOBOMDay,SOTABLE.SODay)) as [PlanDay], " + 
				" 		isNull(WORKORDERTABLE.PlanQuantity,0.00)+isNull(WOBOMUSETABLE.WOBOMQuantity,0.00)+isNull(SOTABLE.SOQuantity,0.00) as [PlanQuantity] " + 
				" 	 " + 
				" 		from  " + 
				" 		(	 " + 
				" 			/* " + 
				" 			Get the PlanQuantity and the PlanDate from WorkOrder " + 
				" 			Sum all the Quantity in a day  " + 
				" 			*/ " + 
				" 			/* ***************************** WORK ORDER *********************** */ " + 
				" 			 " + 
				" 			select  " + 
				" 			ITM_Product.ProductID as [ProductID], " + 
				" 			DatePart(dd,PRO_WorkOrderDetail.StartDate) as [PlanDay], " + 
				" 			/* MIN(PRO_DCPResultDetail.WorkingDate) as PlanDate, */ " + 
				" 			SUM(PRO_WorkOrderDetail.OrderQuantity) as [PlanQuantity] " + 
				" 			 " + 
				" 			from " + 
				" 			ITM_Product " + 
				" 			join PRO_WorkOrderDetail " + 
				" 			on ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID " + 
				" 			and ITM_Product.CCNID = @pstrCCNID " + 

				" 			and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrMonth " + 
				" 			and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrYear " + 
				" 		 " + 
				" 			join PRO_WorkOrderMaster " + 
				" 			on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 			and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID " + 
				/* new add */
				" 			and PRO_WorkOrderMaster.DCOptionMasterID in  " + 
				" 				(select DCOptionMasterID from PRO_DCOptionMaster DCOMASTER " + 
				" 					where 	CCNID = @pstrCCNID " + 
				"					and	Version = @pstrVersion_1 " + 
				" 					and	DatePart(mm   ,DCOMASTER.AsOfDate) = @pstrMonth " + 
				" 					and 	DatePart(yyyy ,DCOMASTER.AsOfDate) = @pstrYear " + 
				" 				) " + 

				" 			 " + 
				" 			group by  " + 
				" 			ITM_Product.ProductID, " + 
				" 			DatePart(dd,PRO_WorkOrderDetail.StartDate) " + 
				" 				/* ***************************** WORK ORDER *********************** */ " + 
				" 	 " + 
				" 		) as WORKORDERTABLE " + 
				" 		 " + 
				"  " + 
				" 		left outer join  " + 
				" 		/***Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/ " + 
				" 		( " + 
				" 			select SO_SaleoRderDetail.ProductID, " + 
				" 			DATEPART(dd,SO_DeliverySchedule.ScheduleDate) as [SODay], " + 
				" 			ISNULL( SUM(SO_DeliverySchedule.DeliveryQuantity) ,0.00) as [SOQuantity] " + 
				" 			from SO_DeliverySchedule " + 
				" 			join SO_SaleOrderDetail on SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID " + 
				" 			join SO_SaleOrderMaster on SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID " + 
				" 			and SO_SaleOrderMaster.CCNID = @pstrCCNID " + 
				" 			 " + 
				" 			WHERE DATEPART(yyyy,SO_DeliverySchedule.ScheduleDate) = @pstrYear " + 
				" 			and DATEPART(mm,SO_DeliverySchedule.ScheduleDate) = @pstrMonth " + 
				" 			/* AND ProductID IN ( + pstrListOfItem + ) */ " + 
				" 			and SO_SaleOrderMaster.ShipFromLocID =  " + 
				" 			(SELECT MasterLocationID FROM MST_Location JOIN PRO_ProductionLine " + 
				" 			ON MST_Location.LocationID = PRO_ProductionLine.LocationID " + 
				" 			WHERE PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 			 " + 
				" 			GROUP BY " + 
				" 			SO_SaleoRderDetail.ProductID, " + 
				" 			DATEPART(dd,SO_DeliverySchedule.ScheduleDate)  " + 
				" 			/***************************************************************************************/ " + 
				" 			/***END       Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/ " + 
				" 		) as SOTABLE " + 
				" 		on WORKORDERTABLE.ProductID = SOTABLE.ProductID " + 
				" 		and WORKORDERTABLE.PlanDay = SOTABLE.SODay " + 
				" 	 " + 
				" 		/*full*/ left outer join 	 " + 
				" 		/************* " + 
				" 			Get the Number of UseItem in WorkOrderBOM , Day of Order " + 
				" 			Sum all the Quantity in a day  " + 
				" 			only get the first child of item (in WOrkOrderBOM, for example: A(B,C)  D(A,B,C) , then B Quantity displayed is B of A and B of C, dont include B of A of D " + 
				" 		*/	 " + 
				" 		(	 " + 
				" 			SELECT      " + 
				" /* 			PRO_WorkOrderDetail.ProductID, */ " + 
				" 			ITM_BOM.ComponentID as [ProductID],  " + 
				" 			SUM((PRO_WorkOrderDetail.OrderQuantity * ITM_BOM.Quantity) / ((100 - ISNULL(ITM_BOM.Shrink, 0)) / 100)) AS [WOBOMQuantity],  " + 
				" 			/* ITM_BOM.Quantity as [WOBOMQuantity],  */ " + 
				" 			DatePart(dd, PRO_WorkOrderDetail.StartDate) as WOBOMDay " + 
				" 			 " + 
				" 			FROM           " + 
				" 			PRO_WorkOrderDetail JOIN ITM_BOM  " + 
				" 			ON PRO_WorkOrderDetail.ProductID = ITM_BOM.ProductID  " + 
				" 			and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrMonth " + 
				" 			and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrYear " + 
				" 		 " + 
//				" 			JOIN PRO_WorkOrderBOMDetail  " + 
//				" 			ON  PRO_WorkOrderBomMaster.WorkOrderBomMasterID = PRO_WorkOrderBomDetail.WorkOrderBomMasterID " + 
//				" 		 " + 
				" 			join PRO_WorkOrderMaster " + 
				" 			on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 			/*   and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID   */ /* dont care about production line here, get all data that this item is use, even in other production line */ " + 
				" 			and PRO_WorkOrderMaster.CCNID = @pstrCCNID	 " + 
				" 			 " + 
				" 			WHERE      PRO_WorkOrderDetail.Status NOT IN (3, 4) " + 
				" 			GROUP BY  " + 
				" 			/* PRO_WorkOrderDetail.ProductID, */ " + 
				" 			ITM_BOM.ComponentID,  " + 
				" 			DatePart(dd, PRO_WorkOrderDetail.StartDate) /* , ITM_BOM.Quantity */ " + 
				" 		) as WOBOMUSETABLE		 " + 
				" 		on WORKORDERTABLE.ProductID = WOBOMUSETABLE.ProductID " + 
				" 		and WORKORDERTABLE.PlanDay = WOBOMUSETABLE.WOBOMDay  	 " + 
				" 		and WOBOMUSETABLE.ProductID = SOTABLE.ProductID " + 
				" 		and WOBOMUSETABLE.WOBOMDay = SOTABLE.SODay " + 
				" 		 " + 
				" 	 " + 
				" 	) as INNERTABLE " + 
				" 	group by " + 
				" 	INNERTABLE.ProductID, INNERTABLE.PlanDay " + 
				"  " + 
				" ) as PLANTABLE " + 
				" 	 " + 
				"  " + 
				" /* Join to get Info data : Category Code, Product Code */ " + 
				" join ITM_Product " + 
				" on PLANTABLE.ProductID = ITM_Product.ProductID " + 
				" and ITM_Product.CCNID = @pstrCCNID " + 
				" LEFT join ITM_Category " + 
				" on ITM_Product.CategoryID = ITM_Category.CategoryID " + 
				"  " + 
				"  " + 
				" LEFT Join " + 
				" ( " + 
				" 	/* ============== PROGRESS BEGIN QUANTITY WITH Month = Parameter Month - 1 ================ */ " + 
				" 	SELECT  " + 
				" 	IsNull(PLANTABLE.ProductID,ACTUALTABLE.ProductID) as [ProductID], " + 
				" 	(IsNull(ACTUALTABLE.ActualQuantity, 0.00) - IsNull(PLANTABLE.PlanQuantity,0.00) )    as [ProgressBeginQuantity]  " + 
				" 	 " + 
				" 	FROM	 " + 
				" 	( " + 
				" 		/*PLANTABLE*/ " + 
				" 		select INNERTABLE.ProductID, Sum(IsNull(INNERTABLE.PlanQuantity,0.00) ) as [PlanQuantity] from  " + 
				" 		( /* INNERTABLE*/ " + 
				" 			select  " + 
				" 			isNull(WORKORDERTABLE.ProductID,isNull(WOBOMUSETABLE.ProductID,SOTABLE.ProductID)) as [ProductID], " + 
				" 			isNull(WORKORDERTABLE.PlanDay,isNull(WOBOMUSETABLE.WOBOMDay,SOTABLE.SODay)) as [PlanDay], " + 
				" 			isNull(WORKORDERTABLE.PlanQuantity,0.00)+isNull(WOBOMUSETABLE.WOBOMQuantity,0.00)+isNull(SOTABLE.SOQuantity,0.00) as [PlanQuantity] " + 
				" 		 " + 
				" 			from  " + 
				" 			(	 " + 
				" 				/* " + 
				" 				Get the PlanQuantity and the PlanDate from WorkOrder " + 
				" 				Sum all the Quantity in a day  " + 
				" 				*/ " + 
				" 				/* ***************************** WORK ORDER *********************** */ " + 
				" 				 " + 
				" 				select  " + 
				" 				ITM_Product.ProductID as [ProductID], " + 
				" 				DatePart(dd,PRO_WorkOrderDetail.StartDate) as [PlanDay], " + 
				" 				/* MIN(PRO_DCPResultDetail.WorkingDate) as PlanDate, */ " + 
				" 				SUM(PRO_WorkOrderDetail.OrderQuantity) as [PlanQuantity] " + 
				" 				 " + 
				" 				from " + 
				" 				ITM_Product " + 
				" 				join PRO_WorkOrderDetail " + 
				" 				on ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID " + 
				" 				and ITM_Product.CCNID = @pstrCCNID " + 

				" 				and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousMonth " + 
				" 				and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousYear " + 
				" 			 " + 
				" 				join PRO_WorkOrderMaster " + 
				" 				on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 				and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID " + 
				/* new add, remember the previousMonth Year */
				" 			and PRO_WorkOrderMaster.DCOptionMasterID in  " + 
				" 				(select DCOptionMasterID from PRO_DCOptionMaster DCOMASTER " + 
				" 					where 	CCNID = @pstrCCNID " + 
				"					and	Version = @pstrVersion_1 " + 
				" 					and		DatePart(mm   ,DCOMASTER.AsOfDate) = @pstrPreviousMonth " + 
				" 					and 	DatePart(yyyy ,DCOMASTER.AsOfDate) = @pstrPreviousYear " + 
				" 				) " + 

				" 				 " + 
				" 				group by  " + 
				" 				ITM_Product.ProductID, " + 
				" 				DatePart(dd,PRO_WorkOrderDetail.StartDate) " + 
				" 					/* ***************************** WORK ORDER *********************** */ " + 
				" 		 " + 
				" 			) as WORKORDERTABLE " + 
				" 			 " + 
				"  " + 
				" 			left outer join  " + 
				" 			/***Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/ " + 
				" 			( " + 
				" 				select SO_SaleoRderDetail.ProductID, " + 
				" 				DATEPART(dd,SO_DeliverySchedule.ScheduleDate) as [SODay], " + 
				" 				ISNULL( SUM(SO_DeliverySchedule.DeliveryQuantity) ,0.00) as [SOQuantity] " + 
				" 				from SO_DeliverySchedule " + 
				" 				join SO_SaleOrderDetail on SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID " + 
				" 				join SO_SaleOrderMaster on SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID " + 
				" 				and SO_SaleOrderMaster.CCNID = @pstrCCNID " + 
				" 				 " + 
				" 				WHERE DATEPART(yyyy,SO_DeliverySchedule.ScheduleDate) = @pstrPreviousYear " + 
				" 				and DATEPART(mm,SO_DeliverySchedule.ScheduleDate) = @pstrPreviousMonth " + 
				" 				/* AND ProductID IN ( + pstrListOfItem + ) */ " + 
				" 				and SO_SaleOrderMaster.ShipFromLocID =  " + 
				" 				(SELECT MasterLocationID FROM MST_Location JOIN PRO_ProductionLine " + 
				" 				ON MST_Location.LocationID = PRO_ProductionLine.LocationID " + 
				" 				WHERE PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 				 " + 
				" 				GROUP BY " + 
				" 				SO_SaleoRderDetail.ProductID, " + 
				" 				DATEPART(dd,SO_DeliverySchedule.ScheduleDate)  " + 
				" 				/***************************************************************************************/ " + 
				" 				/***END       Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/ " + 
				" 			) as SOTABLE " + 
				" 			on WORKORDERTABLE.ProductID = SOTABLE.ProductID " + 
				" 			and WORKORDERTABLE.PlanDay = SOTABLE.SODay " + 
				"  " + 
				" 		 " + 
				" 			/*full*/ left outer join 	 " + 
				" 			/************* " + 
				" 				Get the Number of UseItem in WorkOrderBOM , Day of Order " + 
				" 				Sum all the Quantity in a day  " + 
				" 				only get the first child of item (in WOrkOrderBOM, for example: A(B,C)  D(A,B,C) , then B Quantity displayed is B of A and B of C, dont include B of A of D " + 
				" 			*/	 " + 
				" 			(	 " + 
				" 				SELECT      " + 
				" 	/* 			PRO_WorkOrderDetail.ProductID, */ " + 
				" 				ITM_BOM.ComponentID as [ProductID],  " + 
				" 				SUM((PRO_WorkOrderDetail.OrderQuantity * ITM_BOM.Quantity) / ((100 - ISNULL(ITM_BOM.Shrink, 0)) / 100)) AS [WOBOMQuantity],  " + 
				" 				/* ITM_BOM.Quantity as [WOBOMQuantity],  */ " + 
				" 				DatePart(dd, PRO_WorkOrderDetail.StartDate) as WOBOMDay " + 
				" 				 " + 
				" 				FROM           " + 
				" 				PRO_WorkOrderDetail JOIN ITM_BOM  " + 
				" 				ON PRO_WorkOrderDetail.ProductID = ITM_BOM.ProductID  " + 
				" 				and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousMonth " + 
				" 				and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousYear " + 
//				" 			 " + 
//				" 				JOIN PRO_WorkOrderBOMDetail  " + 
//				" 				ON  PRO_WorkOrderBomMaster.WorkOrderBomMasterID = PRO_WorkOrderBomDetail.WorkOrderBomMasterID " + 
				" 			 " + 
				" 				join PRO_WorkOrderMaster " + 
				" 				on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 				/*   and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID   */ /* dont care about production line here, get all data that this item is use, even in other production line */ " + 
				" 				and PRO_WorkOrderMaster.CCNID = @pstrCCNID	 " + 
				" 				 " + 
				" 				WHERE      PRO_WorkOrderDetail.Status NOT IN (3, 4) " + 
				" 				GROUP BY  " + 
				" 				/* PRO_WorkOrderDetail.ProductID, */ " + 
				" 				ITM_BOM.ComponentID,  " + 
				" 				DatePart(dd, PRO_WorkOrderDetail.StartDate) /* , ITM_BOM.Quantity */ " + 
				" 			) as WOBOMUSETABLE		 " + 
				" 			on WORKORDERTABLE.ProductID = WOBOMUSETABLE.ProductID " + 
				" 			and WORKORDERTABLE.PlanDay = WOBOMUSETABLE.WOBOMDay  		 " + 
				" 			and WOBOMUSETABLE.ProductID = SOTABLE.ProductID " + 
				" 			and WOBOMUSETABLE.WOBOMDay = SOTABLE.SODay			 " + 
				" 	 " + 
				" 		 " + 
				" 		) as INNERTABLE " + 
				" 		group by " + 
				" 		INNERTABLE.ProductID " + 
				" 	 " + 
				" 	) as PLANTABLE " + 
				" 	 " + 
				" 	 " + 
				" 	FULL OUTER JOIN /* ACTUAL JOIN PLAN, TO SUBTRACT */ " + 
				" 	(	 " + 
				" 		/************************* ACTUAL TABLE ***************************************************************************/ " + 
				" 		/*  SIMPLE TEMPLATE FOR OVERALL SCRIPT */ " + 
				" 		select ACTUAL_DATA_TABLE.ProductID, Sum(ACTUAL_DATA_TABLE.ActualQuantity) as ActualQuantity from  " + 
				" 		( " + 
				" 			select  " + 
				" 			isNull(ISSUEWOTABLE.ProductID,MISCISSUETABLE.ProductID) as ProductID, " + 
				" 			isNull(ISSUEWOTABLE.IssueWODay,MISCISSUETABLE.MiscIssueDay) as ActualDay, " + 
				" 			isNull(ISSUEWOTABLE.IssueWOQuantity,0.00)+isNull(MISCISSUETABLE.MiscIssueQuantity,0.00) as ActualQuantity " + 
				" 			from 	 " + 
				" 			(	 " + 
				" 			/**** ISSUE MATERIAL FOR WORKORDER *********************************************/ " + 
				" 				SELECT  " + 
				" 				PRO_IssueMaterialDetail.ProductID, " + 
				" 				datepart(dd,PRO_IssueMaterialMaster.PostDate) as [IssueWODay], " + 
				" 				SUM(ISNULL(PRO_IssueMaterialDetail.CommitQuantity,0)) AS [IssueWOQuantity] " + 
				" 				 " + 
				" 				FROM PRO_IssueMaterialDetail " + 
				" 				JOIN PRO_IssueMaterialMaster " + 
				" 				ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID " + 
				" 				 " + 
				" 				where  " + 
				" 				PRO_IssueMaterialMaster.CCNID = @pstrCCNID " + 
				" 				and DATEPART(mm  ,PRO_IssueMaterialMaster.PostDate) = @pstrPreviousMonth " + 
				" 				and DATEPART(yyyy,PRO_IssueMaterialMaster.PostDate) = @pstrPreviousYear	 " + 
				" 				and PRO_IssueMaterialDetail.LocationID IN (select LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 				and PRO_IssueMaterialMaster.IssuePurposeID = 1 /* Issue by Plan */ " + 
				" 				 " + 
				" 				GROUP BY  " + 
				" 				PRO_IssueMaterialDetail.ProductID, " + 
				" 				Datepart(dd,PRO_IssueMaterialMaster.PostDate) " + 
				" 			/**** end ISSUE MATERIAL FOR WORKORDER *********************************************/ " + 
				" 			) as	ISSUEWOTABLE " + 
				" 			 " + 
				" 			full outer join  " + 
				" 			( " + 
				" 			 " + 
				" 			/**** MISC ISSUE *******************************************************/ " + 
				" 			/*  select * from enm_BinType " + 
				" 			1 	OK	BIN containing Good items                                                                           	 " + 
				" 			2	NG	BIN containing No-Good items                                                                        	 " + 
				" 			3	DS	Destroy - Containing destroy items                                                                  	 " + 
				" 			4	BF	Buffer - BIN containing items transfered from previous Production Line " + 
				" 			*/ " + 
				" 				SELECT  " + 
				" 				ProductID,  " + 
				" 				DATEPART(dd,IV_MiscellaneousIssueMaster.PostDate) as MiscIssueDay, " + 
				" 				SUM(ISNULL(IV_MiscellaneousIssueDetail.Quantity, 0.00)) AS MiscIssueQuantity " + 
				" 				 " + 
				" 				 " + 
				" 				FROM IV_MiscellaneousIssueDetail  " + 
				" 				JOIN IV_MiscellaneousIssueMaster " + 
				" 				ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID " + 
				" 				/* JOIN PRO_IssuePurpose " + 
				" 				ON IV_MiscellaneousIssueMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID */ " + 
				" 				 " + 
				" 				WHERE  " + 
				" 				IV_MiscellaneousIssueMaster.CCNID =  @pstrCCNID " + 
				" 				and DATEPART(mm  ,IV_MiscellaneousIssueMaster.PostDate) = @pstrPreviousMonth " + 
				" 				and DATEPART(yyyy,IV_MiscellaneousIssueMaster.PostDate) = @pstrPreviousYear	 " + 
				" 				and IV_MiscellaneousIssueMaster.SourceLocationID IN (select LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 				/*  " + 
				" 				AND PRO_IssuePurpose.Description <> N' OUT_ABNORMAL ' " + 
				" 				AND PRO_IssuePurpose.Description <> N' NG_PART_RETURN '  " + 
				" 				*/ " + 
				" 				 " + 
				" 				/* " + 
				" 				AND IV_MiscellaneousIssueMaster.SourceBinID IN  " + 
				" 				( " + 
				" 					SELECT BinID FROM MST_BIN " + 
				" 					WHERE LocationID = IV_MiscellaneousIssueMaster.SourceLocationID " + 
				" 					AND MST_BIN.BinTypeID <>  2  " + 
				" 				) " + 
				" 				AND IV_MiscellaneousIssueMaster.DesBinID IN  " + 
				" 				( " + 
				" 					SELECT BinID FROM MST_BIN " + 
				" 					WHERE LocationID = IV_MiscellaneousIssueMaster.DesLocationID " + 
				" 					AND MST_BIN.BinTypeID = 1   " + 
				" 				) " + 
				" 				*/		 " + 
				" 				GROUP BY ProductID, 	DATEPART(dd,IV_MiscellaneousIssueMaster.PostDate) " + 
				" 			/**** MISC ISSUE *******************************************************/ " + 
				" 		 " + 
				" 			) as	MISCISSUETABLE " + 
				" 			on ISSUEWOTABLE.ProductID = MISCISSUETABLE.ProductID " + 
				" 			and ISSUEWOTABLE.IssueWODay = MISCISSUETABLE.MiscIssueDay " + 
				" 		 " + 
				" 		) as ACTUAL_DATA_TABLE " + 
				" 		group by " + 
				" 		ACTUAL_DATA_TABLE.ProductID, ACTUAL_DATA_TABLE.ActualDay " + 
				" 		 " + 
				" 		/************************* ACTUAL TABLE ***************************************************************************/ " + 
				" 		 " + 
				" 	) as ACTUALTABLE " + 
				" 	 " + 
				" 	on PLANTABLE.ProductID = ACTUALTABLE.ProductID	 " + 
				"  " + 
				" ) as PROGRESSBEGINQUANTITYTABLE " + 
				"  " + 
				" on PLANTABLE.ProductID = PROGRESSBEGINQUANTITYTABLE.ProductID " + 
				"  " + 
				"  " 
				/*************************PLAN TABLE *******************************************************************************/
				;

			#endregion PLANTABLE - 1 - NOT MANDATORY - Take all WorkOrder in month_Yearm CCN, where Version = Version_1
			/* ============================================================== */
		
			#region SOTABLE (add into the PlanQuantity)

			string strSqlSOTABLE = 
				/****************************SO TABLE *********************************************************/
				/***Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/
				" select SO_SaleoRderDetail.ProductID, " + 
				" DATEPART(dd,SO_DeliverySchedule.ScheduleDate) as [SODay], " + 
				" ISNULL( SUM(SO_DeliverySchedule.DeliveryQuantity) ,0.00) as [SOQuantity] " + 
				" from SO_DeliverySchedule " + 
				" join SO_SaleOrderDetail on SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID " + 
				" join SO_SaleOrderMaster on SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID " + 
				" and SO_SaleOrderMaster.CCNID = @pstrCCNID " + 
				"  " + 
				" WHERE DATEPART(yyyy,SO_DeliverySchedule.ScheduleDate) = @pstrYear " + 
				" and DATEPART(mm,SO_DeliverySchedule.ScheduleDate) = @pstrMonth " + 
				" /* AND ProductID IN ( + pstrListOfItem + ) */ " + 
				" and SO_SaleOrderMaster.ShipFromLocID =  " + 
				" (SELECT MasterLocationID FROM MST_Location JOIN PRO_ProductionLine " + 
				" ON MST_Location.LocationID = PRO_ProductionLine.LocationID " + 
				" WHERE PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				"  " + 
				" GROUP BY " + 
				" SO_SaleoRderDetail.ProductID, " + 
				" DATEPART(dd,SO_DeliverySchedule.ScheduleDate)  " 
				/***************************************************************************************/
				/***END       Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/
				/****************************SO TABLE *********************************************************/

			
				;

			#endregion SOTABLE (add into the PlanQuantity)

			/* ============================================================== */

			#region WOBOM TABLE

			string strSqlWOBOMTABLE = 
				
				/****************************WOBOM TABLE *********************************************************/
				/*************
					Get the Number of UseItem in WorkOrderBOM , Day of Order
					Sum all the Quantity in a day 
					only get the first child of item (in WOrkOrderBOM, for example: A(B,C)  D(A,B,C) , then B Quantity displayed is B of A and B of C, dont include B of A of D
				*/	
				" SELECT      " + 
				" /* 			PRO_WorkOrderDetail.ProductID, */ " + 
				" ITM_BOM.ComponentID as [ProductID],  " + 
				" SUM((PRO_WorkOrderDetail.OrderQuantity * ITM_BOM.Quantity) / ((100 - ISNULL(ITM_BOM.Shrink, 0)) / 100)) AS [WOBOMQuantity],  " + 
				" /* ITM_BOM.Quantity as [WOBOMQuantity],  */ " + 
				" DatePart(dd, PRO_WorkOrderDetail.StartDate) as WOBOMDay " + 
				"  " + 
				" FROM           " + 
				" PRO_WorkOrderDetail JOIN ITM_BOM  " + 
				" ON PRO_WorkOrderDetail.ProductID = ITM_BOM.ProductID  " + 
				" and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrMonth " + 
				" and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrYear " + 
//				"  " + 
//				" JOIN PRO_WorkOrderBOMDetail  " + 
//				" ON  PRO_WorkOrderBomMaster.WorkOrderBomMasterID = PRO_WorkOrderBomDetail.WorkOrderBomMasterID " + 
				"  " + 
				" join PRO_WorkOrderMaster " + 
				" on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" /*   and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID   */ /* dont care about production line here, get all data that this item is use, even in other production line */ " + 
				" and PRO_WorkOrderMaster.CCNID = @pstrCCNID	 " + 
				"  " + 
				" WHERE      PRO_WorkOrderDetail.Status NOT IN (3, 4) " + 
				" GROUP BY  " + 
				" /* PRO_WorkOrderDetail.ProductID, */ " + 
				" ITM_BOM.ComponentID,  " + 
				" DatePart(dd, PRO_WorkOrderDetail.StartDate) /* , ITM_BOM.Quantity */ " 
				/****************************WOBOM TABLE *********************************************************/
				;

			#endregion WOBOM TABLE

			/* ============================================================== */

			#region BEGINQUANTITY TABLE

			string strSqlBEGINQUANTITYTABLE = 				
				
				/************************* PROGRESS BEGIN TABLE ***************************************************************************/
				" /* ============== PROGRESS BEGIN QUANTITY WITH Month = Parameter Month - 1 ================ */ " + 
				" SELECT  " + 
				" IsNull(PLANTABLE.ProductID,ACTUALTABLE.ProductID) as [ProductID], " + 
				" (IsNull(ACTUALTABLE.ActualQuantity, 0.00) - IsNull(PLANTABLE.PlanQuantity,0.00) )    as [ProgressBeginQuantity]  " + 
				"  " + 
				" FROM	 " + 
				" ( " + 
				" 	/*PLANTABLE*/ " + 
				" 	select INNERTABLE.ProductID, Sum(IsNull(INNERTABLE.PlanQuantity,0.00) ) as [PlanQuantity] from  " + 
				" 	( /* INNERTABLE*/ " + 
				" 		select  " + 
				" 		isNull(WORKORDERTABLE.ProductID,isNull(WOBOMUSETABLE.ProductID,SOTABLE.ProductID)) as [ProductID], " + 
				" 		isNull(WORKORDERTABLE.PlanDay,isNull(WOBOMUSETABLE.WOBOMDay,SOTABLE.SODay)) as [PlanDay], " + 
				" 		isNull(WORKORDERTABLE.PlanQuantity,0.00)+isNull(WOBOMUSETABLE.WOBOMQuantity,0.00)+isNull(SOTABLE.SOQuantity,0.00) as [PlanQuantity] " + 
				" 	 " + 
				" 		from  " + 
				" 		(	 " + 
				" 			/* " + 
				" 			Get the PlanQuantity and the PlanDate from WorkOrder " + 
				" 			Sum all the Quantity in a day  " + 
				" 			*/ " + 
				" 			/* ***************************** WORK ORDER *********************** */ " + 
				" 			 " + 
				" 			select  " + 
				" 			ITM_Product.ProductID as [ProductID], " + 
				" 			DatePart(dd,PRO_WorkOrderDetail.StartDate) as [PlanDay], " + 
				" 			/* MIN(PRO_DCPResultDetail.WorkingDate) as PlanDate, */ " + 
				" 			SUM(PRO_WorkOrderDetail.OrderQuantity) as [PlanQuantity] " + 
				" 			 " + 
				" 			from " + 
				" 			ITM_Product " + 
				" 			join PRO_WorkOrderDetail " + 
				" 			on ITM_Product.ProductID = PRO_WorkOrderDetail.ProductID " + 
				" 			and ITM_Product.CCNID = @pstrCCNID " + 
				" 			 " + 
				" 			and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousMonth " + 
				" 			and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousYear " + 
				" 		 " + 
				" 			join PRO_WorkOrderMaster " + 
				" 			on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 			and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID " + 
				" 			/* new add , remember to put PreviousMonth and Year */ " + 
				" 			and PRO_WorkOrderMaster.DCOptionMasterID in  " + 
				" 			(select DCOptionMasterID from PRO_DCOptionMaster DCOMASTER " + 
				" 				where 	CCNID = @pstrCCNID " + 
				" 			/*	and 	Version = @pstrVersion_1 */  " + 
				"	/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				"	and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrPreviousYear + '-' + @pstrPreviousMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				"	and /*Begin of current month*/ convert(datetime, @pstrPreviousYear + '-' + @pstrPreviousMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 
				" 			) " + 
				"  " + 
				" 			group by  " + 
				" 			ITM_Product.ProductID, " + 
				" 			DatePart(dd,PRO_WorkOrderDetail.StartDate) " + 
				" 				/* ***************************** WORK ORDER *********************** */ " + 
				" 	 " + 
				" 		) as WORKORDERTABLE " + 
				" 		 " + 
				"  " + 
				" 		left outer join  " + 
				" 		/***Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/ " + 
				" 		( " + 
				" 			select SO_SaleoRderDetail.ProductID, " + 
				" 			DATEPART(dd,SO_DeliverySchedule.ScheduleDate) as [SODay], " + 
				" 			ISNULL( SUM(SO_DeliverySchedule.DeliveryQuantity) ,0.00) as [SOQuantity] " + 
				" 			from SO_DeliverySchedule " + 
				" 			join SO_SaleOrderDetail on SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID " + 
				" 			join SO_SaleOrderMaster on SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID " + 
				" 			and SO_SaleOrderMaster.CCNID = @pstrCCNID " + 
				" 			 " + 
				" 			WHERE DATEPART(yyyy,SO_DeliverySchedule.ScheduleDate) = @pstrPreviousYear " + 
				" 			and DATEPART(mm,SO_DeliverySchedule.ScheduleDate) = @pstrPreviousMonth " + 
				" 			/* AND ProductID IN ( + pstrListOfItem + ) */ " + 
				" 			and SO_SaleOrderMaster.ShipFromLocID =  " + 
				" 			(SELECT MasterLocationID FROM MST_Location JOIN PRO_ProductionLine " + 
				" 			ON MST_Location.LocationID = PRO_ProductionLine.LocationID " + 
				" 			WHERE PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 			 " + 
				" 			GROUP BY " + 
				" 			SO_SaleoRderDetail.ProductID, " + 
				" 			DATEPART(dd,SO_DeliverySchedule.ScheduleDate)  " + 
				" 			/***************************************************************************************/ " + 
				" 			/***END       Get SO Delivery Schedule Quantity (the first table for Delivery Plan Row) ***/ " + 
				" 		) as SOTABLE " + 
				" 		on WORKORDERTABLE.ProductID = SOTABLE.ProductID " + 
				" 		and WORKORDERTABLE.PlanDay = SOTABLE.SODay " + 
				"  " + 
				" 	 " + 
				" 		/*full*/ left outer join 	 " + 
				" 		/************* " + 
				" 			Get the Number of UseItem in WorkOrderBOM , Day of Order " + 
				" 			Sum all the Quantity in a day  " + 
				" 			only get the first child of item (in WOrkOrderBOM, for example: A(B,C)  D(A,B,C) , then B Quantity displayed is B of A and B of C, dont include B of A of D " + 
				" 		*/	 " + 
				" 		(	 " + 
				" 			SELECT      " + 
				" /* 			PRO_WorkOrderDetail.ProductID, */ " + 
				" 			ITM_BOM.ComponentID as [ProductID],  " + 
				" 			SUM((PRO_WorkOrderDetail.OrderQuantity * ITM_BOM.Quantity) / ((100 - ISNULL(ITM_BOM.Shrink, 0)) / 100)) AS [WOBOMQuantity],  " + 
				" 			/* ITM_BOM.Quantity as [WOBOMQuantity],  */ " + 
				" 			DatePart(dd, PRO_WorkOrderDetail.StartDate) as WOBOMDay " + 
				" 			 " + 
				" 			FROM           " + 
				" 			PRO_WorkOrderDetail JOIN ITM_BOM  " + 
				" 			ON PRO_WorkOrderDetail.ProductID = ITM_BOM.ProductID  " + 
				" 			and DatePart(mm   ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousMonth " + 
				" 			and DatePart(yyyy ,PRO_WorkOrderDetail.StartDate) = @pstrPreviousYear " + 
//				" 		 " + 
//				" 			JOIN PRO_WorkOrderBOMDetail  " + 
//				" 			ON  PRO_WorkOrderBomMaster.WorkOrderBomMasterID = PRO_WorkOrderBomDetail.WorkOrderBomMasterID " + 
				" 		 " + 
				" 			join PRO_WorkOrderMaster " + 
				" 			on PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 			/*   and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID   */ /* dont care about production line here, get all data that this item is use, even in other production line */ " + 
				" 			and PRO_WorkOrderMaster.CCNID = @pstrCCNID	 " + 
				" 			 " + 
				" 			WHERE      PRO_WorkOrderDetail.Status NOT IN (3, 4) " + 
				" 			GROUP BY  " + 
				" 			/* PRO_WorkOrderDetail.ProductID, */ " + 
				" 			ITM_BOM.ComponentID,  " + 
				" 			DatePart(dd, PRO_WorkOrderDetail.StartDate) /* , ITM_BOM.Quantity */ " + 
				" 		) as WOBOMUSETABLE		 " + 
				" 		on WORKORDERTABLE.ProductID = WOBOMUSETABLE.ProductID " + 
				" 		and WORKORDERTABLE.PlanDay = WOBOMUSETABLE.WOBOMDay  		 " + 
				" 		and WOBOMUSETABLE.ProductID = SOTABLE.ProductID " + 
				" 		and WOBOMUSETABLE.WOBOMDay = SOTABLE.SODay			 " + 
				"  " + 
				" 	 " + 
				" 	) as INNERTABLE " + 
				" 	group by " + 
				" 	INNERTABLE.ProductID " + 
				"  " + 
				" ) as PLANTABLE " + 
				"  " + 
				"  " + 
				" FULL OUTER JOIN /* ACTUAL JOIN PLAN, TO SUBTRACT */ " + 
				" (	 " + 
				" 	/************************* ACTUAL TABLE ***************************************************************************/ " + 
				" 	/*  SIMPLE TEMPLATE FOR OVERALL SCRIPT */ " + 
				" 	select ACTUAL_DATA_TABLE.ProductID, Sum(ACTUAL_DATA_TABLE.ActualQuantity) as ActualQuantity from  " + 
				" 	( " + 
				" 		select  " + 
				" 		isNull(ISSUEWOTABLE.ProductID,MISCISSUETABLE.ProductID) as ProductID, " + 
				" 		isNull(ISSUEWOTABLE.IssueWODay,MISCISSUETABLE.MiscIssueDay) as ActualDay, " + 
				" 		isNull(ISSUEWOTABLE.IssueWOQuantity,0.00)+isNull(MISCISSUETABLE.MiscIssueQuantity,0.00) as ActualQuantity " + 
				" 		from 	 " + 
				" 		(	 " + 
				" 		/**** ISSUE MATERIAL FOR WORKORDER *********************************************/ " + 
				" 			SELECT  " + 
				" 			PRO_IssueMaterialDetail.ProductID, " + 
				" 			datepart(dd,PRO_IssueMaterialMaster.PostDate) as [IssueWODay], " + 
				" 			SUM(ISNULL(PRO_IssueMaterialDetail.CommitQuantity,0)) AS [IssueWOQuantity] " + 
				" 			 " + 
				" 			FROM PRO_IssueMaterialDetail " + 
				" 			JOIN PRO_IssueMaterialMaster " + 
				" 			ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID " + 
				" 			 " + 
				" 			where  " + 
				" 			PRO_IssueMaterialMaster.CCNID = @pstrCCNID " + 
				" 			and DATEPART(mm  ,PRO_IssueMaterialMaster.PostDate) = @pstrPreviousMonth " + 
				" 			and DATEPART(yyyy,PRO_IssueMaterialMaster.PostDate) = @pstrPreviousYear	 " + 
				" 			and PRO_IssueMaterialDetail.LocationID IN (select LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 			and PRO_IssueMaterialMaster.IssuePurposeID = 1 /* Issue by Plan */ " + 
				" 			 " + 
				" 			GROUP BY  " + 
				" 			PRO_IssueMaterialDetail.ProductID, " + 
				" 			Datepart(dd,PRO_IssueMaterialMaster.PostDate) " + 
				" 		/**** end ISSUE MATERIAL FOR WORKORDER *********************************************/ " + 
				" 		) as	ISSUEWOTABLE " + 
				" 		 " + 
				" 		full outer join  " + 
				" 		( " + 
				" 		 " + 
				" 		/**** MISC ISSUE *******************************************************/ " + 
				" 		/*  select * from enm_BinType " + 
				" 		1 	OK	BIN containing Good items                                                                           	 " + 
				" 		2	NG	BIN containing No-Good items                                                                        	 " + 
				" 		3	DS	Destroy - Containing destroy items                                                                  	 " + 
				" 		4	BF	Buffer - BIN containing items transfered from previous Production Line " + 
				" 		*/ " + 
				" 			SELECT  " + 
				" 			ProductID,  " + 
				" 			DATEPART(dd,IV_MiscellaneousIssueMaster.PostDate) as MiscIssueDay, " + 
				" 			SUM(ISNULL(IV_MiscellaneousIssueDetail.Quantity, 0.00)) AS MiscIssueQuantity " + 
				" 			 " + 
				" 			 " + 
				" 			FROM IV_MiscellaneousIssueDetail  " + 
				" 			JOIN IV_MiscellaneousIssueMaster " + 
				" 			ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID " + 
				" 			/* JOIN PRO_IssuePurpose " + 
				" 			ON IV_MiscellaneousIssueMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID */ " + 
				" 			 " + 
				" 			WHERE  " + 
				" 			IV_MiscellaneousIssueMaster.CCNID =  @pstrCCNID " + 
				" 			and DATEPART(mm  ,IV_MiscellaneousIssueMaster.PostDate) = @pstrPreviousMonth " + 
				" 			and DATEPART(yyyy,IV_MiscellaneousIssueMaster.PostDate) = @pstrPreviousYear	 " + 
				" 			and IV_MiscellaneousIssueMaster.SourceLocationID IN (select LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 			/*  " + 
				" 			AND PRO_IssuePurpose.Description <> N' OUT_ABNORMAL ' " + 
				" 			AND PRO_IssuePurpose.Description <> N' NG_PART_RETURN '  " + 
				" 			*/ " + 
				" 			 " + 
				" 			/* " + 
				" 			AND IV_MiscellaneousIssueMaster.SourceBinID IN  " + 
				" 			( " + 
				" 				SELECT BinID FROM MST_BIN " + 
				" 				WHERE LocationID = IV_MiscellaneousIssueMaster.SourceLocationID " + 
				" 				AND MST_BIN.BinTypeID <>  2  " + 
				" 			) " + 
				" 			AND IV_MiscellaneousIssueMaster.DesBinID IN  " + 
				" 			( " + 
				" 				SELECT BinID FROM MST_BIN " + 
				" 				WHERE LocationID = IV_MiscellaneousIssueMaster.DesLocationID " + 
				" 				AND MST_BIN.BinTypeID = 1   " + 
				" 			) " + 
				" 			*/		 " + 
				" 			GROUP BY ProductID, 	DATEPART(dd,IV_MiscellaneousIssueMaster.PostDate) " + 
				" 		/**** MISC ISSUE *******************************************************/ " + 
				" 	 " + 
				" 		) as	MISCISSUETABLE " + 
				" 		on ISSUEWOTABLE.ProductID = MISCISSUETABLE.ProductID " + 
				" 		and ISSUEWOTABLE.IssueWODay = MISCISSUETABLE.MiscIssueDay " + 
				" 	 " + 
				" 	) as ACTUAL_DATA_TABLE " + 
				" 	group by " + 
				" 	ACTUAL_DATA_TABLE.ProductID, ACTUAL_DATA_TABLE.ActualDay " + 
				" 	 " + 
				" 	/************************* ACTUAL TABLE ***************************************************************************/ " + 
				" 	 " + 
				" ) as ACTUALTABLE " + 
				"  " + 
				" on PLANTABLE.ProductID = ACTUALTABLE.ProductID	 " 


				/************************* PROGRESS BEGIN TABLE ***************************************************************************/
				;

			#endregion BEGINQUANTITY TABLE

			/* ============================================================== */

			#region ACTUALTABLE
			/// Get the datatable : contain Mapping ProductID with it Actual COmpletion quantity, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - ActualDay - ActualQuantity
			/// 200	1	110.00000			
			/// 200	4	40.00000		
			/// 127	31	50.00000		
			/// </summary>
			/// <returns></returns>
			// private DataTable BuildActualTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrWorkOrderMasterID_2)

			string strSqlACTUALTABLE = 
			
				" /************************* ACTUAL TABLE ***************************************************************************/ " + 
				" /*  SIMPLE TEMPLATE FOR OVERALL SCRIPT */ " + 
				" select ACTUAL_DATA_TABLE.ProductID, ACTUAL_DATA_TABLE.ActualDay, Sum(ACTUAL_DATA_TABLE.ActualQuantity) as ActualQuantity from  " + 
				" ( " + 
				" 	select  " + 
				" 	isNull(ISSUEWOTABLE.ProductID,MISCISSUETABLE.ProductID) as ProductID, " + 
				" 	isNull(ISSUEWOTABLE.IssueWODay,MISCISSUETABLE.MiscIssueDay) as ActualDay, " + 
				" 	isNull(ISSUEWOTABLE.IssueWOQuantity,0.00)+isNull(MISCISSUETABLE.MiscIssueQuantity,0.00) as ActualQuantity " + 
				" 	from 	 " + 
				" 	(	 " + 
				" 	/**** ISSUE MATERIAL FOR WORKORDER *********************************************/ " + 
				" 		SELECT  " + 
				" 		PRO_IssueMaterialDetail.ProductID, " + 
				" 		datepart(dd,PRO_IssueMaterialMaster.PostDate) as [IssueWODay], " + 
				" 		SUM(ISNULL(PRO_IssueMaterialDetail.CommitQuantity,0.00)) AS [IssueWOQuantity] " + 
				" 		 " + 
				" 		FROM PRO_IssueMaterialDetail " + 
				" 		JOIN PRO_IssueMaterialMaster " + 
				" 		ON PRO_IssueMaterialDetail.IssueMaterialMasterID = PRO_IssueMaterialMaster.IssueMaterialMasterID " + 
				" 		 " + 
				" 		where  " + 
				" 		PRO_IssueMaterialMaster.CCNID = @pstrCCNID " + 
				" 		and DATEPART(mm  ,PRO_IssueMaterialMaster.PostDate) = @pstrMonth " + 
				" 		and DATEPART(yyyy,PRO_IssueMaterialMaster.PostDate) = @pstrYear	 " + 
				" 		and PRO_IssueMaterialDetail.LocationID IN (select LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 		and PRO_IssueMaterialMaster.IssuePurposeID = 1 /* Issue by Plan */ " + 
				" 		 " + 
				" 		GROUP BY  " + 
				" 		PRO_IssueMaterialDetail.ProductID, " + 
				" 		Datepart(dd,PRO_IssueMaterialMaster.PostDate) " + 
				" 	/**** end ISSUE MATERIAL FOR WORKORDER *********************************************/ " + 
				" 	) as	ISSUEWOTABLE " + 
				" 	 " + 
				" 	full outer join  " + 
				" 	( " + 
				" 	 " + 
				" 	/**** MISC ISSUE *******************************************************/ " + 
				" 	/*  select * from enm_BinType " + 
				" 	1 	OK	BIN containing Good items                                                                           	 " + 
				" 	2	NG	BIN containing No-Good items                                                                        	 " + 
				" 	3	DS	Destroy - Containing destroy items                                                                  	 " + 
				" 	4	BF	Buffer - BIN containing items transfered from previous Production Line " + 
				" 	*/ " + 
				" 		SELECT  " + 
				" 		ProductID,  " + 
				" 		DATEPART(dd,IV_MiscellaneousIssueMaster.PostDate) as MiscIssueDay, " + 
				" 		SUM(ISNULL(IV_MiscellaneousIssueDetail.Quantity, 0.00)) AS MiscIssueQuantity " + 
				" 		 " + 
				" 		 " + 
				" 		FROM IV_MiscellaneousIssueDetail  " + 
				" 		JOIN IV_MiscellaneousIssueMaster " + 
				" 		ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID " + 
				" 		/* JOIN PRO_IssuePurpose " + 
				" 		ON IV_MiscellaneousIssueMaster.IssuePurposeID = PRO_IssuePurpose.IssuePurposeID */ " + 
				" 		 " + 
				" 		WHERE  " + 
				" 		IV_MiscellaneousIssueMaster.CCNID =  @pstrCCNID " + 
				" 		and DATEPART(mm  ,IV_MiscellaneousIssueMaster.PostDate) = @pstrMonth " + 
				" 		and DATEPART(yyyy,IV_MiscellaneousIssueMaster.PostDate) = @pstrYear	 " + 
				" 		and IV_MiscellaneousIssueMaster.SourceLocationID IN (select LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 		/*  " + 
				" 		AND PRO_IssuePurpose.Description <> N' OUT_ABNORMAL ' " + 
				" 		AND PRO_IssuePurpose.Description <> N' NG_PART_RETURN '  " + 
				" 		*/ " + 
				" 		 " + 
				" 		/* " + 
				" 		AND IV_MiscellaneousIssueMaster.SourceBinID IN  " + 
				" 		( " + 
				" 			SELECT BinID FROM MST_BIN " + 
				" 			WHERE LocationID = IV_MiscellaneousIssueMaster.SourceLocationID " + 
				" 			AND MST_BIN.BinTypeID <>  2  " + 
				" 		) " + 
				" 		AND IV_MiscellaneousIssueMaster.DesBinID IN  " + 
				" 		( " + 
				" 			SELECT BinID FROM MST_BIN " + 
				" 			WHERE LocationID = IV_MiscellaneousIssueMaster.DesLocationID " + 
				" 			AND MST_BIN.BinTypeID = 1   " + 
				" 		) " + 
				" 		*/		 " + 
				" 		GROUP BY ProductID, 	DATEPART(dd,IV_MiscellaneousIssueMaster.PostDate) " + 
				" 	/**** MISC ISSUE *******************************************************/ " + 
				"  " + 
				" 	) as	MISCISSUETABLE " + 
				" 	on ISSUEWOTABLE.ProductID = MISCISSUETABLE.ProductID " + 
				" 	and ISSUEWOTABLE.IssueWODay = MISCISSUETABLE.MiscIssueDay " + 
				"  " + 
				" ) as ACTUAL_DATA_TABLE " + 
				" group by " + 
				" ACTUAL_DATA_TABLE.ProductID, ACTUAL_DATA_TABLE.ActualDay " + 
				"  " + 
				" /************************* ACTUAL TABLE ***************************************************************************/ " 
				;

			#endregion ACTUALTABLE

			/* ============================================================== */
				
			try 
			{
				
				oconPCS = null;
				ocmdPCS = null;
			
				strSql += 
					strSql_META_TABLE + "\n" +  
					strSqlPLANTABLE_2 /* newer */ + "\n" +  
					strSqlPLANTABLE_1 + "\n" + 
					strSqlSOTABLE + "\n" +
					strSqlWOBOMTABLE + "\n" +
					strSqlBEGINQUANTITYTABLE + "\n" +
					strSqlACTUALTABLE + "\n" 
					;
	

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstRET);

				dstRET.Tables[0].TableName = META_TABLE_NAME;
				dstRET.Tables[1].TableName = PLAN_TABLE_NAME_2;
				dstRET.Tables[2].TableName = PLAN_TABLE_NAME_1;
				dstRET.Tables[3].TableName = SO_TABLE_NAME;
				dstRET.Tables[4].TableName = WOBOM_TABLE_NAME;
				dstRET.Tables[5].TableName = BEGINQUANTITY_TABLE_NAME;
				dstRET.Tables[6].TableName = ACTUAL_TABLE_NAME;

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
			
			return dstRET;
		}


		/// <summary>
		/// This function return the Maximum DCP Version.
		/// If on Error, or there is no previous, it will return a NEGATIVE value.
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		private int GetMaxVersion(string pstrCCNID) /* string pstrYear, string pstrMonth, string pstrElementID, */			
		{
			const int NO_VERSION = -1;
			int intRet = NO_VERSION;
			
			#region DB QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 				
				" /*-----------------------------------*/ " + 
				" Set @pstrCCNID = " +pstrCCNID+ " " + 								
				" /*-----------------------------------*/ " + 
				"  " + 			
				" select   " + 
				" IsNull( Max(Version) , 0) as MaxVersion " + 
				" from    " + 
				" PRO_DCOptionMaster  as DCOMASTER   	 " + 
				"  " + 
				" where DCOMASTER.CCNID = @pstrCCNID    " ;				

			try
			{
				intRet = Convert.ToInt32( ExecuteScalar(strSql) );
			}
			catch
			{}

			#endregion DB QUERY

			intRet = intRet == 0 ? -1 : intRet;

			return intRet;
		}	// end function


		/// <summary>
		/// This function return the Previous DCP Version of pstrCurrentVersion.
		/// If on Error, or there is no previous, it will return a NEGATIVE value.
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrPreviousPOVersion"></param>
		/// <returns></returns>
		private int GetPreviousVersion(string pstrCCNID, /* string pstrYear, string pstrMonth, string pstrElementID, */
			string pstrCurrentVersion)
		{
			const int NO_VERSION = -1;
			int intRet = NO_VERSION;
			
			#region DB QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 							
				" Declare @pstrVersion_2 int " + 							
				" /*-----------------------------------*/ " + 
				" Set @pstrCCNID = " +pstrCCNID+ " " + 				
				" Set @pstrVersion = " +pstrCurrentVersion+ " " + 				
				" /*-----------------------------------*/ " + 
				"  " + 			
				" select   " + 
				" IsNull( Max(Version) , 0) as PreviousVersion " + 
				" from    " + 
				" PRO_DCOptionMaster  as DCOMASTER   				 " + 
				"  " + 
				" where DCOMASTER.CCNID = @pstrCCNID    " + 
				" and DCOMASTER.Version < @pstrVersion_2		";			

			try
			{
				intRet = Convert.ToInt32( ExecuteScalar(strSql) );
			}
			catch
			{}

			#endregion DB QUERY

			intRet = intRet == 0 ? NO_VERSION : intRet;

			return intRet;
		}	// end function


	

		/// <summary>
		/// Modify the original Plantable (WorkOrderTable) to the Real Plan table (which have real "PlanDay", depend on the real working time of day.
		/// (We should remember that, WorkOrder start from 4AM of 12/04/2006 may has real "PlanDay" = 13, because of Shift3 of 11/04/2006 last to 6h14AM of 12/04/2006 )
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="pdtbOriginalWorkOrderPlan"></param>
		/// <param name="pdtbWorkingTimeMappingToDay"></param>
		/// <returns></returns>
		private DataTable ModifyPlanTable(DataTable pdtbOriginalWorkOrderPlan, string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			const string PLANDATE = "PlanDate";

			DataTable dtbWorkingTime = GetAllPeriodOfWorkingTime(pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID );

			// clone the schema
			DataTable dtbRet = pdtbOriginalWorkOrderPlan.Clone();
			// add new column = PLANDAY
			dtbRet.Columns.Add(PLAN + DATE, typeof(int) );
			// remove the fulltime column PLANDATE (contain yyyy mm dd hh:mm:ss)
			dtbRet.Columns.Remove(PLANDATE);
		
			// foreach row in WorkOrderTable, if PlanDate is in any WorkingTime of any Day, add it in the dtbRet table with that Day value in PlanDay column
			foreach(DataRow drow in pdtbOriginalWorkOrderPlan.Rows)
			{				
				DateTime dtmPlanDateBeforeResolve = DateTime.MinValue;
				if(drow[PLANDATE] != DBNull.Value)
				{
					dtmPlanDateBeforeResolve = DateTime.Parse(drow[PLANDATE].ToString().Trim());
				}

				int nRealWorkingDay = 	GetRealWorkingDay(dtmPlanDateBeforeResolve, dtbWorkingTime);

				if(nRealWorkingDay  > 0 && nRealWorkingDay <= 31)
				{
					DataRow dtrNew = dtbRet.NewRow();
					dtrNew[PLAN + DATE] = nRealWorkingDay;
					dtrNew[PRODUCTID] = drow[PRODUCTID];
					dtrNew[PLAN + QUANTITY] = drow[PLAN + QUANTITY];
					dtbRet.Rows.Add(dtrNew);
				}

			}
			
			return dtbRet;
		}


	
		/// <summary>
		/// get the reference table for GetRealWorkingDay() function
		/// result is the table with each record contain: 
		/// BeginDate, EndDate (of configured WCCapacity)
		/// WorkTimeFrom, WorkTimeTo	(Real working time of each shift in a working day)
		/// 
		/// SCHEMA: BeginDate, EndDate, WorkTimeFrom, WorkTimeTo		
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		private DataTable GetAllPeriodOfWorkingTime(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = 
  
					" Declare @pstrCCNID int " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrProductionLineID int " + 
					/*-----------------------------------*/
					"  " + 
					" Set @pstrCCNID = " +pstrCCNID+ " " + 
					" Set @pstrYear = '" +pstrYear+ "' " + 
					" Set @pstrMonth = '" +pstrMonth+ "' " + 
					" Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					" 	 " + 
					" select distinct  " + 
					"  " + 
					" WCC.BeginDate, " + 
					" WCC.EndDate, " + 
					" SP.WorkTimeFrom, " + 
					" SP.WorkTimeTo  " + 
					" from  " + 
					" PRO_Shift as S " + 
					" join PRO_ShiftPattern as SP " + 
					" 	on S.ShiftID = SP.ShiftID " + 
					/* " 	and ShiftDesc IN ('1S','2S','3S') " + */ /*allow all shift*/
					" join PRO_ShiftCapacity as SC " + 
					" 	on S.ShiftID = SC.ShiftID " + 
					" join PRO_WCCapacity as WCC " + 
					" 	on WCC.WCCapacityID = SC.WCCapacityID " +  
					" 	/* Take all the relate to Parameter Year-month period of WCCapacity. BeginDate < first day of NextMonth. EndDate >= first day of CurrentProvidedMonth */ " + 
					" 	and WCC.BeginDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
					" 	and convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= WCC.EndDate  " + 
					"  " + 
					" join MST_WorkCenter as WC " + 
					" 	on WCC.WorkCenterID = WC.WorkCenterID " + 
					" 	and WC.ProductionLineID = @pstrProductionLineID " + 
					" 	and WC.CCNID = @pstrCCNID " + 
					"  " ;
 
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
		/// Put the DateTime need to Resolve in
		/// Reference the WorkingTime table
		/// if the ResolveTime is in the working time of any shift, of a configured period, determine the real WorkingDay, and return.
		/// </summary>
		/// <param name="pdtmNeedToResolve"></param>
		/// <param name="pdtbWorkingTime"></param>
		/// <returns></returns>
		private int GetRealWorkingDay(DateTime pdtmNeedToResolve, DataTable pdtbWorkingTime)
		{
			const string BEGINDATE = "BeginDate";
			const string ENDDATE = "EndDate";
			const string WORKTIMEFROM = "WorkTimeFrom";
			const string WORKTIMETO = "WorkTimeTo";			

			if(pdtmNeedToResolve == DateTime.MinValue)
			{
				return 0;
			}		

			int iRet = 0;
			try
			{
				foreach(DataRow drow in pdtbWorkingTime.Rows)
				{
					if(1 <= iRet && iRet <= 31)
					{
						return iRet;
					}
					else // if pdtmNeedToResolve is in any period, modify the iRet, and then the next loop will break, function return
					{
						DateTime dtmBeginDate = (DateTime)drow[BEGINDATE];
						DateTime dtmEndDate = (DateTime)drow[ENDDATE];
						DateTime dtmWorkTimeFrom = (DateTime)drow[WORKTIMEFROM];
						DateTime dtmWorkTimeTo = (DateTime)drow[WORKTIMETO];

						if(dtmBeginDate <= pdtmNeedToResolve && 	/* NeedToResolve > beginDate (yyyymmdd 00 00 00) */
							new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day )  <= dtmEndDate)	/* start time of each Date of NeedToResolve <= EndDate  */
						{
							int nActualDay = pdtmNeedToResolve.Day;
							int nAdjustDay = dtmWorkTimeTo.Day - dtmWorkTimeFrom.Day;

							int nFromHour = dtmWorkTimeFrom.Hour;
							int nFromMinute = dtmWorkTimeFrom.Minute;
							int nFromSecond = dtmWorkTimeFrom.Second;
							int nFromMilisecond = dtmWorkTimeFrom.Millisecond;

							int nToHour = dtmWorkTimeTo.Hour;
							int nToMinute = dtmWorkTimeTo.Minute;
							int nToSecond = dtmWorkTimeTo.Second;
							int nToMilisecond = dtmWorkTimeTo.Millisecond;

							// slide the WorkTimeFrom (prototype 2005/01/01 xxxxxxx) to the actualWorkTimeFrom (2006/04/24 xxxx) 
							// where TimeNeedToResolve is 2006/04/24 yyy)
							DateTime dtmActualWorkTimeFrom = (new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month,pdtmNeedToResolve.Day)) /*Start Actual day*/
								.Add(	/* add the time amount from start of the day to the WorkTimeFrom day*/
								dtmWorkTimeFrom  .Subtract  (new DateTime(dtmWorkTimeFrom.Year, dtmWorkTimeFrom.Month, dtmWorkTimeFrom.Day)  )
								);
							DateTime dtmActualWorkTimeTo = dtmActualWorkTimeFrom.Add(
								dtmWorkTimeTo.Subtract(dtmWorkTimeFrom)
								);
							
							if(dtmActualWorkTimeFrom  <= pdtmNeedToResolve && pdtmNeedToResolve < dtmActualWorkTimeTo)	// RESOLVE is in the Shift worktime
							{
								int intDayDiff = dtmWorkTimeFrom.Day - 1;
								iRet = pdtmNeedToResolve.Day - intDayDiff;
							}	// end RESOLVE is in the Shift worktime

						}	// end resolve date is in the period worktime
					}
                    
				}	// end foreach datarow in REFERENCE TABLE
	
			}	// end try, there is error. perhap the cast action is fail, can't cast from DBNull
			catch
			{ 
				return 0;
			}

			return iRet;
		}

		
		/// <summary>
		/// values in pstrProductIDColName, pstrDayColName, pstrSumColName must not be NULL
		/// </summary>
		/// <param name="pdtbOriginal"></param>
		/// <param name="pstrProductIDColName"></param>
		/// <param name="pstrDayColName"></param>
		/// <param name="pstrSumColName"></param>
		/// <returns></returns>
		private DataTable SumAndGroupBy(DataTable pdtbOriginal, 
			string pstrProductIDColName,
			string pstrDayColName,
			string pstrSumColName)
		{
			DataTable dtbRet = pdtbOriginal.Clone();			

			ArrayList arrItem_Day = GetUniqueComlexKeyFromTable(pdtbOriginal, pstrProductIDColName, pstrDayColName);
			foreach(string strItemDay in arrItem_Day)
			{					
				string strFilter = string.Empty;			
				strFilter = 
					string.Format("[{0}]='{1}' AND [{2}]='{3}' ",
					pstrProductIDColName,
					strItemDay.Split('#')[0],
					pstrDayColName,
					strItemDay.Split('#')[1]					
					);				
				
				// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrows = pdtbOriginal.Select(strFilter);

				if(dtrows.Length > 0)
				{
					// Create DUMMYROW FIRST
					DataRow dtrNew = dtbRet.NewRow();

					double dblSumForThisItemDay = 0d;
					// GUIDE: for each rows in of this Item OF DTBSourceData - adjust the dblSumForThisItemDay
					foreach(DataRow dtr in dtrows)
					{					
						dblSumForThisItemDay += ReportBuilder.ToDouble( dtr[pstrSumColName] );
					}

					dtrNew[pstrProductIDColName] = dtrows[0][pstrProductIDColName];
					dtrNew[pstrDayColName] = dtrows[0][pstrDayColName];
					dtrNew[pstrSumColName] = dblSumForThisItemDay;

					dtbRet.Rows.Add(dtrNew);
				}
			}

			return dtbRet;
		}


		/// <summary>
		/// Get key pair: ProductID#Day
		/// Deliminate character is "#"
		/// </summary>
		/// <param name="pdtb"></param>
		/// <param name="pstrProductID"></param>
		/// <param name="pstrDay"></param>
		/// <returns></returns>
		private ArrayList GetUniqueComlexKeyFromTable(DataTable pdtb, string pstrProductID, string pstrDay)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{	
					object objProductIDGet = drow[pstrProductID];
					object objDayGet = drow[pstrDay];
					string str = objProductIDGet.ToString().Trim() + "#" + objDayGet.ToString().Trim();
					if( !arrRet.Contains(str)  )
					{
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

	}


	/// <summary>
	/// <author>Thachnn</author>
	/// This report is a very complex report.
	/// It combines severals .NET DataTables with C1Report and Interop with Excel to get some chart images.
	/// 
	/// 
	/// --- The first 3 rows: using Sum() function of C1Report VBScript to calculate. Sum() function work rather well.
	/// 
	/// --- Auto generate the DayInMonth, DayOfWeek serries (by using 2 fuctions of mine from ReportBuilder)
	/// 
	/// --- In the detail section:
	/// There are many fields put here (about 200fields). Some info-fields like PartNo, PartName, Model. .... and a large pack of indexed fields (from 1 till 31).
	/// They are fldPlanxx, fldAdjxx, fldActualxx, fldReturnxx, fldProgressDayxx, fldProgressAccumulatexx and fldAssessmentxx.
	/// We do not spread these 200 fields vertically. We group 'em into 6 rows, 
	/// Plan, Adj and Actual, Return, ProgressDay field are fill from tables (built in C# code). 
	/// Other fields like , Progress Accumulate, Assessment was calculate on render time, 
	/// (by VBScript on the C1Report XML layout file)
	/// But if we render in render time by C1, there are some problems we can't hanlde, C1 is rather bad if we process too much field calculating.
	/// 
	/// Actual -- get from dtbActualTable
	/// REturn -- get from dtbReturnTable
	/// --- all indexed fields will be re-render, re-spread depending on the number of day in month ( using the Spread function of mine in the ReportBuilder)
	/// 
	/// --- There are some sumRow() cell at the end of each line (in detail fields). 
	///			 They are generated by a small program. they have text like: fldPlan01+fldPlan02+ ...
	///			 These C1 VB may work well, but I really don't love it like the first time I see C1. Sometime it raise a very crazy unknown bsht.
	/// --- count "Achieve", count "Not Achieve" on each row is now process by the C1
	/// 
	/// --- Some field on the report is calculate by the VBScript (store on the report layout XML file). 
	/// For example: calc some percent cell on each detail row.
	/// Remember that the C1Report VBScript count() function is not work well , so in the last to rows (grand sum by day, at the bottom of report): AchieveDay and Not Achieve/Day, we should calculate these values by C# (and I did that)
	/// 
	/// --- About the chart:
	/// We put 3 first rows of report (PlanTotal, Actual Total, Progress Total) to the Excel files to generate the DetailChart
	/// We put AchievePercent (right most, bottom most field) to the "ProportionAchievePlan" Excel cell
	/// put ProportionStandard (right most, near top field) to the "ProportionStandard" Excel cell
	/// Then we copy 2 chart from Excel file to images, bind to the fldChart and fldTotalChart on the C1Report
	/// 
	/// The last line I drop here: DON"T BELIEVE IN 3rd Vendor COmponent Provider. C1REport is blsht when you processing huge report.
	/// </summary>
	[Serializable]	
	public class ProductionLineProductionProgressReport : MarshalByRefObject, IDynamicReport		            
	{
		#region IDynamicReport Implementation
	
		private string mConnectionString;
		private ReportBuilder mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = false;	

		private string mstrReportDefinitionFolder = string.Empty;
		string mstrReportLayoutFile = string.Empty;

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


	
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
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

		

		public ProductionLineProductionProgressReport()
		{
		}	
	
		#region GLOBAL CONSTANT
		
		const string THIS = "ExternalReportFile:ProductionLineProductionProgressReport";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "ProductionLineProductionProgressReport";	
		
		const string ZERO_STRING = "0";
		const string ASSESSMENT_OK = "O";
		const string ASSESSMENT_NG = "X";
		const string MONTH_DATE_FORMAT = "MMM";

		/// Report layout file constant
		const string REPORT_LAYOUT_FILE = "ProductionLineProductionProgressReport.xml";
		const string REPORT_NAME = "ProductionLineProductionProgressReport";
		short COPIES = 1;

		/// all parameter are Mandatory
		const string REPORTFLD_PARAMETER_CCN						= "fldParameterCCN";
		const string REPORTFLD_PARAMETER_MONTH					= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_YEAR						= "fldParameterYear";			
		const string REPORTFLD_PARAMETER_ELEMENT		
			= "fldParameterProductionLine";
		const string REPORTFLD_PARAMETER_VERSION1			= "fldParameterVersion1";
		const string REPORTFLD_PARAMETER_VERSION2			= "fldParameterVersion2";
		const string REPORTFLD_PROPORTIONSTANDARDPERCENT	= "fldProportionStandardPercent";


		/// Result Data Table Column names, contain:
		/// PRODUCTID, CATEGORY,PARTNO,MODEL,BEGIN
		/// DATE
		/// PLAN+QUANTITY, ACTUAL+QUANTITY
		const string PRODUCTID = "ProductID";
		const string CATEGORY = "Category";
		const string PARTNO = "PartNo";
		const string PARTNAME = "PartName";
		const string MODEL = "Model";
		const string BEGIN = "ProgressBeginQuantity";

		const string DATE = "Day";
		const string QUANTITY = "Quantity";	// suffix for PLAN,ACTUAL , RETURN column

		const string VERSION = "Version";

		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYOFWEEK = "lblDayOfWeek";


		/// other constants			
		const string PLAN = "Plan";
		const string SO = "SO";
		const string WOBOM = "WOBOM";

		const string ADJ = "Adj";
		const string ACTUAL = "Actual";
		const string PROGRESSDAY = "ProgressDay";
		const string PROGRESS = "ProgressAccumulate";
		const string ASSESSMENT = "Assessment";
		//		const string RETURN = "Return";
		const string ROWCOUNTPASS = "RowCountPass";
		const string ROWCOUNTFAIL = "RowCountFail";
		const string ROWPERCENT = "RowPercent";

		const string FLD = "fld";		
		const string LBL = "lbl";
		const string HEADING = "DayHeading";

		const string REPORTFLD_TITLE = FLD + "Title";


		const string PLANFAIL = "PlanFailD";
		const string PLANPASS = "PlanPassD";

		/// chart fields
		const string REPORTFLD_CHART	= "fldChart";
		const string REPORTFLD_TOTALCHART = "fldTotalChart";

		const string REPORTFLD_TOTALPASS = "fldPlanPassSumRow";
		const string REPORTFLD_TOTALFAIL = "fldPlanFailSumRow";		


		string META_TABLE_NAME = "MetaTable";
		string PLAN_TABLE_NAME_1 = "PlanTable1";
		string PLAN_TABLE_NAME_2 = "PlanTable2";
		string ACTUAL_TABLE_NAME = "ActualTable";
		string ADJ_TABLE_NAME = "AdjTable";
		string SO_TABLE_NAME = "SOTable";
		string WOBOM_TABLE_NAME = "WOBOMTable";
		string BEGINQUANTITY_TABLE_NAME = "BEGINQUANTITYTable";
		//string RETURN_TABLE_NAME = "ReturnTable";

		#endregion GLOBAL CONSTANT


		#region GLOBAL VAR	

		DataSet dstMAIN = new DataSet();	

		#endregion GLOBAL VAR


		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrWorkOrderMasterID_1"></param>
		/// <param name="pstrWorkOrderMasterID_2"></param>
		/// <param name="pstrProportionStandard">You must fill 0.xx here ( a number less than 1 and greater or equal 0)</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, 
			string pstrProductionLineID /*report main element*/, 
			string pstrVersion_1, string pstrVersion_2, string pstrProportionStandard)
		{
			#region My variables						

			int nCCNID = int.Parse(pstrCCNID);
			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);			
			int nProductionLineID = int.Parse(pstrProductionLineID);
			int nVersion_2 = -1;	// if input is null or string.Empty, we get the maximum Version to calculate
			if(pstrVersion_2 == null || pstrVersion_2.Trim() == string.Empty)
			{
				nVersion_2	= GetMaxVersion(pstrCCNID);
			}
			else
			{
				nVersion_2	= int.Parse(pstrVersion_2);	
			}

			int nVersion_1 = -1;
			if(pstrVersion_1 == null || pstrVersion_1.Trim() == string.Empty)
			{
				nVersion_1 = GetPreviousVersion(pstrCCNID, pstrVersion_2);
			}

			// not mandatory, so we will the default value 0.95 for other processing
			double dblProportionStandard = 0.95d;
			dblProportionStandard = ReportBuilder.ToDouble(pstrProportionStandard);			
			
			//  for display on the Report parameter Section
			string strReportParameter_CCN = string.Empty;
			string strReportParameter_Month = pstrMonth;
			string strReportParameter_Year = pstrYear;
			string strReportParameter_ProductionLine = string.Empty;
			string strReportParameter_Version1 = (nVersion_1 < 0 ? pstrVersion_1 : nVersion_1.ToString()  );
			string strReportParameter_Version2 =  (pstrVersion_2 == string.Empty  ? (nVersion_2 + " (Lastest)" ) : pstrVersion_2);
						
			float fActualPageSize = 9000.0f;			

			/// contain array of string: Name of the column (with days have data in the dtbSourceData)
			/// FOr Example:
			/// dtbSourceData contain: 01-Oct: has Plan Quantity
			/// 02-Oct has Actual Quantity
			/// So arrHasValueDateHeading contain: Plan01, Actual02
			ArrayList arrHasValueDateHeading = new ArrayList();				

			/// Keep count of PlanPass and PlanFail for all days (columns).
			Hashtable arrColumnPass = new Hashtable();
			Hashtable arrColumnFail = new Hashtable();

			/// Keep count of PlanPass and PlanFail for all ITEMS  (rows).
			Hashtable arrRowPass = new Hashtable();
			Hashtable arrRowFail = new Hashtable();
			
			// get data and cache all in the dstMAIN			
			dstMAIN = GetDataAndCache(pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID, 
				nVersion_1.ToString() , nVersion_2.ToString() , pstrProportionStandard);	
			dstMAIN.DataSetName = pstrCCNID + pstrYear + pstrMonth + pstrProductionLineID + pstrVersion_1 + pstrVersion_2 + pstrProportionStandard;			

			System.Data.DataTable dtbMetaTable;
			dtbMetaTable  = dstMAIN.Tables[META_TABLE_NAME];		

			System.Data.DataTable dtbPlanTable;
			dtbPlanTable  = dstMAIN.Tables[PLAN_TABLE_NAME_2];		
			// Modify the PLAN TABLE - get the real PlanDay (depend on Working Time of active working day)
			dtbPlanTable = ModifyPlanTable(dtbPlanTable, pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID);
			dtbPlanTable = SumAndGroupBy(dtbPlanTable, PRODUCTID, PLAN + DATE, PLAN + QUANTITY);
			
			System.Data.DataTable dtbSOTable;
			dtbSOTable  = dstMAIN.Tables[SO_TABLE_NAME];

			System.Data.DataTable dtbWOBOMTable;
			dtbWOBOMTable = dstMAIN.Tables[WOBOM_TABLE_NAME];

			System.Data.DataTable dtbBEGINTable;
			dtbBEGINTable = dstMAIN.Tables[BEGINQUANTITY_TABLE_NAME];

			System.Data.DataTable dtbActualTable;
			dtbActualTable = dstMAIN.Tables[ACTUAL_TABLE_NAME];

			System.Data.DataTable dtbPlanTableWO1;
			System.Data.DataTable dtbAdjTable;
			dtbPlanTableWO1  = dstMAIN.Tables[PLAN_TABLE_NAME_1];
			
			dtbAdjTable = BuildAdjTable(dtbPlanTableWO1 , dtbPlanTable);
			if(pstrVersion_1 == pstrVersion_2)	// short circuit, make faster when 2 version is the same, all adjust field is null (zer0)
			{
				dtbAdjTable = new DataTable(ADJ_TABLE_NAME);
				dtbAdjTable.Columns.Add(PRODUCTID);
				dtbAdjTable.Columns.Add(ADJ + DATE, typeof(Int32) );
				dtbAdjTable.Columns.Add(ADJ + QUANTITY, typeof(Decimal) );
			}
			dtbAdjTable.TableName = ADJ_TABLE_NAME;
			dstMAIN.Tables.Add(dtbAdjTable);
				
			//System.Data.DataTable dtbReturnTable;
			//dtbReturnTable = dstMAIN.Tables[RETURN_TO_VENDOR_TABLE_NAME];
		
			#endregion  My Variables

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strReportParameter_CCN = boUtil.GetCCNCodeFromID(nCCNID);	
			strReportParameter_ProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + ": " + objBO.GetProductLineNameFromID(nProductionLineID);
			
			#endregion	
			
			/// transform TABLE column names
			/// transform TABLE will contain :
			/// PRODUCTID, 
			/// META INFO  = CATEGORY,PARTNO,MODEL,
			/// BEGIN QUANTITY
			/// PLAN+i.ToString("00")
			/// ADJ +i.ToString("00")
			/// ACTUAL+i.ToString("00")
			/// RETURN+i.ToString("00")
			/// ProgressDay, Progress, Assessment
			#region TRANSFORM ORIGINAL TABLE FOR REPORT		
	
			#region GETTING THE DATE HEADING
			/// arrPlanDate and arrActualDate contain DateTime object from actual dtbSourceData
			ArrayList arrPlanDate = GetColumnValuesFromTable(dtbPlanTable,PLAN+DATE);
			arrPlanDate = GetColumnValuesFromTable(dtbSOTable,SO+DATE, arrPlanDate );
			arrPlanDate = GetColumnValuesFromTable(dtbWOBOMTable,WOBOM+DATE, arrPlanDate);

			ArrayList arrActualDate = GetColumnValuesFromTable(dtbActualTable,ACTUAL+DATE);
			//ArrayList arrReturnDate = GetColumnValuesFromTable(dtbReturnTable,RETURN+DATE);			
			ArrayList arrAdjDate = GetColumnValuesFromTable(dtbAdjTable,ADJ+DATE);

			//ArrayList arrItems = GetCategory_PartNo_Model_ProductID_FromTable(dtbPlanTable,CATEGORY,PARTNO,MODEL,PRODUCTID);
			ArrayList arrItems = GetColumnValuesFromTable(dtbPlanTable, PRODUCTID);

			/// PUSH: has-value (in the dtbSourceData) to the arrHasValueDateHeading
			/// 
			/// HACKED: Thachnn: 20/12/2005
			/// don't remove this dummy code of casting object in arrPlanDate to int
			/// because sometime, data in the database is not correct, return dbnull to the arrPlanDate. If we use normal foreach(int nDay in arrPlanDate)
			/// exception of Invalid cast will be throw
			/// In this case: ActualDate can be omit and = DBNull because an Item can be Plan, but it didn't produce in any day in this month
			foreach(object obj  in arrPlanDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = PLAN + dtm.Day.ToString("00");					
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			
			foreach(object obj in arrActualDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = ACTUAL + dtm.Day.ToString("00");					
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			//			foreach(object obj in arrReturnDate)
			//			{
			//				try
			//				{
			//					int nDay = (int)obj;
			//					DateTime dtm = new DateTime(nYear,nMonth,nDay);
			//					string strColumnName = RETURN + dtm.Day.ToString("00");			
			//					arrHasValueDateHeading.Add(strColumnName);
			//				}
			//				catch{}
			//			}

			foreach(object obj in arrAdjDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = ADJ + dtm.Day.ToString("00");					
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			/// ENDHACKED: Thachnn: 20/12/2005
			/// after this snip of code. arrHasValueDateHeading will contain Actual01, Actual02 or Plan03 Plan04, or Adj03 Adj04, Return02, Return03  ... 
			/// depend on the DataTable
			/// Which day has value (Plan , Adj, or Actual,Return), the columnName will exist in the arrHasValueDateHeading
			/// and then, the Transform DataTable dtbTransform will has some columns named like string in arrHasValueDateHeading			

			#endregion		
            			
			DataTable dtbTransform = BuildTransformTable(arrHasValueDateHeading);
		
			#endregion  TRANSFORM ORIGINAL TABLE FOR REPORT
						
			#region FILL ABSOLUTE DATA FROM Plan && Actual && Adjust && Return to the TRANSFORM DATATABLE
			
			/// GUIDE: with each Items
			foreach(object obj /* ProductID */  in arrItems)
			{
				string strItem = obj.ToString();
				// Create DUMMYROW FIRST
				DataRow dtrNew = dtbTransform.NewRow();

				#region	- fill ITEM meta info to the new dummy row				
				
				string strFilterMeta = string.Empty;
				
				strFilterMeta = string.Format("[{0}]='{1}' ",		
					PRODUCTID,	strItem);

				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrows = dtbMetaTable.Select(strFilterMeta);

				/// GUIDE: for each rows in result (datarow contain map ProductID -- MetaInfo)
				foreach(DataRow dtr in dtrows)
				{
					// fill data to the dummy row
					dtrNew[PRODUCTID] = dtr[PRODUCTID];
					dtrNew[CATEGORY] = dtr[CATEGORY];
					dtrNew[PARTNO] = dtr[PARTNO];
					dtrNew[PARTNAME] = dtr[PARTNAME];
					dtrNew[MODEL] = dtr[MODEL];
					// TODO: Thachnn: maybe we will ad Begin Quantity to the MetaInfo Table @!!!   dtrNew[BEGIN] = dtr[BEGIN];
				}

				#endregion	- fill ITEM meta info to the new dummy row
			
				#region	- fill PLAN quantity to the new dummy row				
								
				string strFilterPlan = string.Empty;
				
				strFilterPlan = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);			
				
				/// GUIDE: get all rows of this Item from the dtbPlan
				DataRow[] dtrowsPlan = dtbPlanTable.Select(strFilterPlan);

				/// GUIDE: for each rows in of this Item OF dtbPlan - fill plan quantity ITEM
				foreach(DataRow dtr in dtrowsPlan)
				{					
					/// Fill Plan Quantity to destination column of Transform table, in this new rows					
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[PLAN+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[PLAN+QUANTITY];				
				}

				#endregion - fill PLAN quantity to the new dummy row
				
				#region	- fill SO quantity to the new dummy row				
								
				string strFilterSO = string.Empty;
				
				strFilterSO = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);			
				
				/// GUIDE: get all rows of this Item from the dtbSO
				DataRow[] dtrowsSO = dtbSOTable.Select(strFilterSO);

				/// GUIDE: for each rows in of this Item OF dtbSO - fill SO quantity ITEM
				foreach(DataRow dtr in dtrowsSO)
				{
					/// ADD SO Quantity to PLAN COLUMN
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[SO+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = ReportBuilder.ToDecimal( dtrNew[strDateColumnToFill])  + ReportBuilder.ToDecimal( dtr[SO+QUANTITY]) ;
				}

				#endregion - fill SO quantity to the new dummy row

				#region	- fill WOBOM quantity to the new dummy row
								
				string strFilterWOBOM = string.Empty;
				
				strFilterWOBOM = 
					string.Format("[{0}]='{1}' ",					
					PRODUCTID,	strItem	);
				
				/// GUIDE: get all rows of this Item from the dtbWOBOM
				DataRow[] dtrowsWOBOM = dtbWOBOMTable.Select(strFilterWOBOM);

				/// GUIDE: for each rows in of this Item OF dtbWOBOM - fill WOBOM quantity ITEM
				foreach(DataRow dtr in dtrowsWOBOM)
				{
					/// ADD WOBOM Quantity to PLAN COLUMN
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[WOBOM+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = ReportBuilder.ToDecimal( dtrNew[strDateColumnToFill])  + ReportBuilder.ToDecimal( dtr[WOBOM+QUANTITY]) ;
				}

				#endregion - fill WOBOM quantity to the new dummy row

				// TODO: Thachnn: Add BEGINQUANTITY Table here
				#region - fill BEGIN quantity to the new dummy row
				
				string strFilterBEGIN = string.Empty;
				strFilterBEGIN = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem	);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsBEGIN = dtbBEGINTable.Select(strFilterBEGIN);

				/// GUIDE: for each rows  of this Item in BEGIN Completion DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsBEGIN)
				{
					/// Fill BEGIN Quantity to destination column of Transform table, in this new rows					
					string strDateColumnToFill = BEGIN;
					dtrNew[strDateColumnToFill] = dtr[BEGIN];
				}
				#endregion - fill BEGIN  quantity to the new dummy row

				#region - fill ACTUAL quantity to the new dummy row
				
				string strFilterActual = string.Empty;
				strFilterActual = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem	);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsActual = dtbActualTable.Select(strFilterActual);

				/// GUIDE: for each rows  of this Item in Actual Completion DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsActual)
				{
					/// Fill Actual Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = ACTUAL + ((DateTime)dtr[ACTUAL+DATE]).Day.ToString("00");
					string strDateColumnToFill = ACTUAL + Convert.ToInt32( dtr[ACTUAL+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[ACTUAL+QUANTITY];
				}
				#endregion - fill ACTUAL  quantity to the new dummy row

				#region - fill ADJUST quantity to the new dummy row
				
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilterAdjust = string.Empty;
				strFilterAdjust = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,	strItem		);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsAdjust = dtbAdjTable.Select(strFilterAdjust);

				/// GUIDE: for each rows  of this Item in Adjust DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsAdjust)
				{
					/// Fill Actual Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = ADJ + ((DateTime)dtr[ADJ +DATE]).Day.ToString("00");
					string strDateColumnToFill = ADJ + Convert.ToInt32( dtr[ADJ+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[ADJ+QUANTITY];
				}
				#endregion - fill ADJUST quantity to the new dummy row
	
				#region - fill RETURN quantity to the new dummy row

				//				/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
				//				/// so we put IsNull in the filter string (to select from dtbResult);
				//				string strFilterReturn = string.Empty;
				//				strFilterReturn = 
				//					string.Format("[{0}]='{1}' ",
				//					PRODUCTID,
				//					strItem.Split('#')[3]
				//					);		
				//				
				//				/// GUIDE: get all rows of this Item from the dtbSourceData
				//				DataRow[] dtrowsReturn = dtbReturnTable.Select(strFilterReturn);
				//
				//				/// GUIDE: for each rows  of this Item in Return DataTable- fill return quantity to the dummy ROW
				//				foreach(DataRow dtr in dtrowsReturn)
				//				{
				//					/// Fill Return Quantity to destination column of Transform table, in this new rows
				//					//strDateColumnToFill = RETURN + ((DateTime)dtr[RETURN+DATE]).Day.ToString("00");
				//					string strDateColumnToFill = RETURN + Convert.ToInt32( dtr[RETURN+DATE]).ToString("00");
				//					dtrNew[strDateColumnToFill] = dtr[RETURN+QUANTITY];
				//				}
				#endregion - fill RETURN quantity to the new dummy row


				// add to the transform data table
				dtbTransform.Rows.Add(dtrNew);				
			}	    
			#endregion FILL DATA FROM Plan DTB && ActualCompletion DTB && Adjust DTB to the TRANSFORM DATATABLE

			
			#region CALCULATE the Sum of Plan, sum of Actual, sum of Progress (on top of the report) to generate a chart in EXCEL			
			double[,] arrSumPlan = new double[1,31];
			double[,] arrSumActual = new double[1,31];
			double[,] arrSumProgress = new double[1,31]; 
			
			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth)  ; i++)
			{
				string strCounter = i.ToString("00");

				/// sum on the top of the report, calculate to put in the excel file to generate a chart.
				//string str = "Sum(Plan"+i.ToString("00")+")";
				try
				{
					arrSumPlan[0,i-1] = double.Parse(dtbTransform.Compute("Sum(Plan"+i.ToString("00")+")" , string.Empty ).ToString());
				}
				catch{}
				
				try
				{
					arrSumActual[0,i-1] = double.Parse(dtbTransform.Compute("Sum(Actual"+i.ToString("00")+")" , string.Empty).ToString());
				}
				catch{}

				/// progress SUm will be caculate in the next section , after render the report, we will get the real value of upper sum field on report
				/// because the progress value is caculate on render time, depend on the real actual data on the rendered report				
			}		// end foreach Day(i)
			#endregion calculate the Sum of Plan, sum of Actual, sum of Progress (on top of the report) to generate a chart in EXCEL

			#region CALCULATE the ProgressDay column

			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth); i++)
			{
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);				
				foreach(DataRow rowItem in dtbTransform.Rows)
				{
					decimal decPlan = ReportBuilder.ToDecimal(rowItem[PLAN+strCounter]);
					decimal decActual = ReportBuilder.ToDecimal(rowItem[ACTUAL+strCounter]);
					//					decimal decReturn = ReportBuilder.ToDecimal(rowItem[RETURN+strCounter]);					

					rowItem[PROGRESSDAY+strCounter] = decActual - decPlan; // - decReturn;
				}			
			}	

			#endregion calculate the ProgressDay column

			#region CALCULATE , fill Progress quantity to the new dummy row

			for(int i = 1 ; i <= 31 /*DateTime.DaysInMonth(nYear,nMonth)*/  ; i++)
			{
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);
                
				foreach(object obj  in arrItems)
				{
					string strItem = obj.ToString();					
					string strFilterProgress = 
						string.Format("[{0}]='{1}' ",
						PRODUCTID,	strItem	);
				
					/// GUIDE: get rows ( in fact, it is only one) of this Item from the dtbTransform
					DataRow[] dtrowsItemAllInfo = dtbTransform.Select(strFilterProgress);
			
					decimal decCurrentACTUAL = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][ACTUAL+ i.ToString("00")] );
					decimal decCurrentPLAN = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][PLAN+ i.ToString("00")]) ;
					//					decimal decCurrentRETURN = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][RETURN+ i.ToString("00")]) ;
					
					decimal decPreviousPROGRESS = decimal.Zero;
					if(i == 1)
						decPreviousPROGRESS = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][BEGIN] );
					else					
						decPreviousPROGRESS = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][PROGRESS + (i-1).ToString("00")]  ) /*Previous*/ ;					

					dtrowsItemAllInfo[0][PROGRESS + i.ToString("00")] = 
						decPreviousPROGRESS
						+decCurrentACTUAL
						-decCurrentPLAN;
					//						-decCurrentRETURN;
					
				}	// end each Items (of current day  = i)
			}		// end foreach Day(i)
			
			#endregion - calculate , fill Progress quantity to the new dummy row			
			
			// keep sum of whole report PASS or FAIL
			int intTotalCountPass = 0;			int intTotalCountFail = 0;						
			#region ASSESS the PROGRESS, fill ASSESSMENT and CALCULATE the count of FAIL and PASS
			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth); i++)
			{
				int intColumnPass = 0;			int intColumnFail = 0;
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);		
				
				foreach(DataRow rowItem in dtbTransform.Rows)
				{					
					decimal decPlan = ReportBuilder.ToDecimal(rowItem[PLAN+strCounter]);
					decimal decActual = ReportBuilder.ToDecimal(rowItem[ACTUAL+strCounter]);
					//					decimal decReturn = ReportBuilder.ToDecimal(rowItem[RETURN+strCounter]);
					decimal decProgressDay = decActual - decPlan; // - decReturn;
					decimal decProgress = ReportBuilder.ToDecimal(rowItem[PROGRESS+strCounter]);

					if (decPlan == decimal.Zero  && decActual == decimal.Zero )
					{ /* Ignore, don't assess the progress */ }
					else
					{
						if (decProgressDay == decimal.Zero)
						{
							rowItem[ASSESSMENT+strCounter] = ASSESSMENT_OK;
							intTotalCountPass ++;	// total
							rowItem[ROWCOUNTPASS] = ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) + 1;
							intColumnPass++;							
						}
						else if ((decProgressDay > 0) && (decProgress <= 0))
						{
							rowItem[ASSESSMENT+strCounter] = ASSESSMENT_OK;
							intTotalCountPass ++;
							rowItem[ROWCOUNTPASS] = ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) + 1;
							intColumnPass++;
						}
						else
						{
							rowItem[ASSESSMENT+strCounter] = ASSESSMENT_NG;
							intTotalCountFail ++;
							rowItem[ROWCOUNTFAIL] = ReportBuilder.ToInt32(rowItem[ROWCOUNTFAIL]) + 1;
							intColumnFail++;
						}
					}										
				}	// end each rowItem in Transform table				
				arrColumnPass.Add(FLD + PLANPASS + strCounter, intColumnPass);
				arrColumnFail.Add  (FLD + PLANFAIL  + strCounter, intColumnFail );
			}	// end foreach i			

			#endregion calculate the count of Plan FAIL and Plan PASS			

			#region CALCULATE the Percent (column in dtbTransform) for each Row
			foreach(DataRow rowItem in dtbTransform.Rows)
			{
				int nSum = ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) + ReportBuilder.ToInt32(rowItem[ROWCOUNTFAIL]);
				if( nSum != 0 )
				{
					double dblPercent = (double)ReportBuilder.ToInt32(rowItem[ROWCOUNTPASS]) / nSum;
					rowItem[ROWPERCENT] = (dblPercent*100).ToString("#0.00") + "%";
				}					
			}
			#endregion CALCULATE the Percent for each Row



			#region  SHORT CIRCUIT this function (uncomment these line for the summary report: ProductionLineAssessment)
						if((intTotalCountPass + intTotalCountFail) == 0 )
						{
							mResult = "-";
						}
						else
						{
							decimal decTemp  = ( (decimal)intTotalCountPass * 100) / (intTotalCountPass + intTotalCountFail) ;
							mResult = decTemp.ToString("#,##0.00");
						}
						return dtbTransform;
			#endregion SHORT CIRCUIT this function
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
		/// Thachnn : 15/Oct/2005
		/// Browse the DataTable, get all value of column with provided named.
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>
		/// <param name="pstrColumnName">COlumn Name in pdtb DataTable to collect values from</param>
		/// <returns>ArrayList of object, collect from pdtb's column named pstrColumnName. Empty ArrayList if error or not found any row in pdtb.</returns>
		private static ArrayList GetColumnValuesFromTable(DataTable pdtb, string pstrColumnName, ArrayList arrOriginal)
		{
			ArrayList arrRet = arrOriginal;
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
				// arrRet.Clear();
			}
			return arrRet;
		}



		/// <summary>
		/// build a new datatable with column = productid, category,partno,model,begin,
		/// and somecolumn with names in arrHasValueDateHeading
		/// Index column is : Plan, Adj, Actual, Return, ProgressDay, Progress Accumulate, Assessment
		/// 
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildTransformTable(ArrayList parrHasValueDateHeading)
		{
			DataTable dtbRet = new DataTable(TABLE_NAME);
			dtbRet.Columns.Add(PRODUCTID,typeof(System.Int32) );
			dtbRet.Columns.Add(CATEGORY,typeof(System.String) );
			dtbRet.Columns.Add(PARTNO,typeof(System.String));
			dtbRet.Columns.Add(PARTNAME,typeof(System.String));
			dtbRet.Columns.Add(MODEL,typeof(System.String));
			dtbRet.Columns.Add(BEGIN,typeof(System.Double));			
			dtbRet.Columns.Add(ROWCOUNTPASS,typeof(System.Int32));
			dtbRet.Columns.Add(ROWCOUNTFAIL,typeof(System.Int32));
			dtbRet.Columns.Add(ROWPERCENT,typeof(System.String));

			/// fill the column (Double type) in which the date exist in the dtbSourceData (has value contain in the parrDueDateHeading)
			/// then fill the column with String type (so that it will display correctly in the report, not #,##0.00, because it has null value)
					
			foreach(string strColumnName in parrHasValueDateHeading)
			{					
				try
				{
					dtbRet.Columns.Add(strColumnName,typeof(System.Double));
				}
				catch{}
			}
			// FILL the null column				
			for(int i = 1; i <=31; i++)												  
			{
				if(parrHasValueDateHeading.Contains(PLAN + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(PLAN + i.ToString("00"),typeof(System.String));						
					}
					catch{}
				}
				if(parrHasValueDateHeading.Contains(ADJ + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(ADJ + i.ToString("00"),typeof(System.String));
					}
					catch{}
				}
				if(parrHasValueDateHeading.Contains(ACTUAL + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(ACTUAL + i.ToString("00"),typeof(System.String));
					}
					catch{}
				}

				try
				{
					dtbRet.Columns.Add(PROGRESSDAY + i.ToString("00"),typeof(System.Double));
				}
				catch{}
				try
				{
					dtbRet.Columns.Add(PROGRESS + i.ToString("00"),typeof(System.Double));
				}
				catch{}

				try
				{
					dtbRet.Columns.Add(ASSESSMENT + i.ToString("00"),typeof(System.String));
				}
				catch{}

			} 	// FILL the null column
			
			return dtbRet;		
		}	// end build transform tables

		
		/// <summary>
		/// Thachnn : 08/Nov/2005
		/// Browse the DataTable, get all value of Category, PartNo column, insert into ArraysList as CategoryValue#PartNoValue
		/// </summary>
		/// <param name="pdtb">DataTable to collect values</param>			
		/// <param name="pstrCategoryColName"></param>
		/// <param name="pstrPartNoColName"></param>
		/// <returns>ArrayList of object, collect CategoryValue#PartNoValue pairs from pdtb. Empty ArrayList if error or not found any row in pdtb.</returns>		
		private ArrayList GetCategory_PartNo_Model_ProductID_FromTable(DataTable pdtb, string pstrCategoryColName, string pstrPartNoColName, string pstrModelColName, string pstrProductID)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{
					object objCategoryGet = drow[pstrCategoryColName];
					object objPartNoGet = drow[pstrPartNoColName];
					object objModelGet = drow[pstrModelColName];
					object objProductIDGet = drow[pstrProductID];
					string str = objCategoryGet.ToString().Trim() + "#" + objPartNoGet.ToString().Trim() + "#" + objModelGet.ToString().Trim() + "#" + objProductIDGet.ToString().Trim();
					if( !arrRet.Contains(str)  )
					{
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

	

		/// <summary>
		/// Thachnn: 10/11/2005
		/// execute the input sql clause
		/// return the object result
		/// throw all exception to outside
		/// </summary>
		/// <param name="pstrSql">SQL clause to execute</param>
		/// <returns>object</returns>
		public object ExecuteScalar(string pstrSql)
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

	



		/// <summary>
		/// <author>Thachnn</author>
		/// Get the datatable: 
		/// contain Mapping ProductID with it adjustment from TABLE1 to TABLE2, in a specific days in month)
		/// Return table will have schema like:
		/// ProductID - AdjDay - AdjQuantity
		/// 200	1	110.00000
		/// 200	2	10.00000		
		/// 127	13	53.00000
		/// 127	31	50.00000		
		/// </summary>
		/// <param name="pdtbPlanTable1"></param>
		/// <param name="pdtbPlanTable2"></param>
		/// <returns></returns>
		private DataTable BuildAdjTable(DataTable pdtbTable1, DataTable pdtbTable2 )
		{	
			/// TABLE RET = TABLE1 - TABLE2

			/// build schema for ADJ table			
			DataTable dtbRet = new DataTable(ADJ_TABLE_NAME);
			dtbRet.Columns.Add(PRODUCTID);
			dtbRet.Columns.Add(ADJ + DATE, typeof(Int32) );
			dtbRet.Columns.Add(ADJ + QUANTITY, typeof(Decimal) );
			
			// using inner datatable.  not modify the original input parameter (Table1 and Table2)
			DataTable dtb1 = pdtbTable1.Copy();
			DataTable dtb2 = pdtbTable2.Copy();

			/// FOREACH iROW IN TABLE2, 
			/// dtbRet newROW = iROW(2)
			/// if found relative-newROW    row in Table1 (productid is the same, planday is the same), 
			/// ---- subtract PlanQuantity of current row (newROW).
			/// ---- Add found row in Table1 in the UsedRowedInTableWO1
			/// add newROW to dtbRET
			/// 
			/// Table1.Remove(UsedRowedInTableWO1);
			/// 
			/// FOREACH iROW1 in TABLE1
			/// CLone, Negate and ADD to the dtbRet
			

            
			if(dtb1.Rows.Count == 0)
			{
				return dtbRet;
			}			

			ArrayList arrUsedRowInTable1 = new ArrayList();
			int nPID = int.MinValue;
			int nDay = int.MinValue;			
			foreach(DataRow iRow in dtb2.Rows)
			{
				DataRow newRow = dtbRet.NewRow();
				nPID = System.Convert.ToInt32(iRow[PRODUCTID]);
				nDay = System.Convert.ToInt32(iRow[PLAN + DATE]);
				decimal dblAQ = Convert.ToDecimal( iRow[PLAN + QUANTITY]  );

				decimal dblRelativeFromTable1 = 0;
				foreach( DataRow jRow in dtb1.Select("[PRODUCTID]=" +nPID+ " and [PLANDAY]=" +nDay)   )
				{
					dblRelativeFromTable1 += Convert.ToDecimal( jRow[PLAN + QUANTITY]) ;
					arrUsedRowInTable1.Add(jRow);	/// mark that we used this rows. We don't include its value later.
				}

				newRow[PRODUCTID] = nPID;
				newRow[ADJ + DATE] = nDay;
				newRow[ADJ + QUANTITY] = dblAQ - dblRelativeFromTable1;
				dtbRet.Rows.Add(newRow);
			}

			foreach(DataRow jRow in arrUsedRowInTable1)
			{
				try
				{
					dtb1.Rows.Remove(jRow);			
				}
				catch{}				
			}
            
			foreach(DataRow iRow in dtb1.Rows)
			{
				DataRow newRow = dtbRet.NewRow();
				newRow[PRODUCTID] = iRow[PRODUCTID];
				newRow[ADJ + DATE] = iRow[PLAN + DATE];
				try
				{
					
					newRow[ADJ + QUANTITY] = decimal.Negate( (decimal)iRow[PLAN + QUANTITY] );
				}
				catch{}				

				dtbRet.Rows.Add(newRow);
			}

			return dtbRet;
		}



		/// <summary>
		/// Get all data for this report and cache in the dstMAIN dataset
		/// just improve the speed for this report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <param name="pstrWorkOrderMasterID_1"></param>
		/// <param name="pstrWorkOrderMasterID_2"></param>
		/// <param name="pstrProportionStandard"></param>
		private DataSet GetDataAndCache(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrVersion_1, string pstrVersion_2, string pstrProportionStandard)
		{	
			DataSet dstRET = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			string pstrPreviousYear = pstrYear;
			string pstrPreviousMonth = pstrMonth;

			if(pstrMonth == "1" || pstrMonth == "01" )
			{
				pstrPreviousMonth = "12";
				pstrPreviousYear = (int.Parse(pstrYear) - 1).ToString();
			}
			else
			{
				pstrPreviousMonth = (int.Parse(pstrMonth) - 1).ToString();
			}



			#region MAIN SQL QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 
				" Declare @pstrMonth char(2) " + 
				" Declare @pstrYear char(4) " + 
				" Declare @pstrPreviousMonth char(2) " + 
				" Declare @pstrPreviousYear char(4) " + 

				" Declare @pstrProductionLineID int " + 
				" Declare @pstrVersion_1 int " + 			
				" Declare @pstrVersion_2 int " + 			
				"  " + 				
				" /*-----------------------------------*/ " + 
				"  " + 
				" Set @pstrCCNID = " + pstrCCNID + " " + 
				" Set @pstrYear = '" + pstrYear + "' " + 
				" Set @pstrMonth = '"+ pstrMonth +"' " + 
				" Set @pstrPreviousYear = '" + pstrPreviousYear + "' " + 
				" Set @pstrPreviousMonth = '"+ pstrPreviousMonth +"' " + 

				" Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 

				(pstrVersion_1.Trim() == string.Empty ?  (string.Empty) : (" Set @pstrVersion_1 = " +pstrVersion_1 + " ")  )  + 
				(pstrVersion_2.Trim() == string.Empty ?  (string.Empty) : (" Set @pstrVersion_2 = " +pstrVersion_2 + " ")  )  + 				
				"  " + 					
				"  " ;
			/*-----------------------------------*/

			#endregion MAIN QUERY

			#region META _DATA

			string strSql_META_TABLE =

				" select distinct  " + 
				" ITEM.ProductID as [ProductID] , " + 
				" ITEM.Code as [PartNo], " + 
				" ITEM.Description as [PartName], " + 
				" CAT.Code as [Category] , " + 
				" ITEM.Revision as [Model] " + 
				"  " + 
				" FROM PRO_WorkOrderMaster as WOMASTER " + 
				" join PRO_WorkOrderDetail as WODETAIL " + 
				" on WOMASTER.WorkOrderMasterID = WODETAIL.WorkOrderMasterID " + 
				" join ITM_Product ITEM " + 
				" on WODETAIL.ProductID = ITEM.ProductID " + 
				" join ITM_Category as CAT " + 
				" on ITEM.CategoryID = CAT.CategoryID " + 
				"  " + 
				" where WOMASTER.CCNID = @pstrCCNID " + 
				" and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" and DatePart(mm  ,DueDate) = @pstrMonth " + 
				" and DatePart(yyyy,DueDate) = @pstrYear " + 
				"  " ; 
			#endregion META _DATA


			#region PLANTABLE - 2 - MAIN

		
			string strSqlPLANTABLE_2 =	/* newer */
                	
				/*************************PLAN TABLE *******************************************************************************/			
		
				" SELECT  " + 
				" PLANTABLE.ProductID, " + 
				" PLANTABLE.PlanDate, " + 
				" PLANTABLE.PlanQuantity " + 
				"  " + 
				" FROM " + 
				" ( " + 
				" 	/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	select  " + 
				" 	WODETAIL.ProductID as [ProductID], " + 
				" 	WODETAIL.StartDate as [PlanDate], " + 
				" 	SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 	DCOMASTER.Version " + 
				" 	 " + 
				" 	from " + 
				" 	PRO_WorkOrderDetail as WODETAIL " + 
				" 	join PRO_WorkOrderMaster WOMASTER " + 
				" 	on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 	and WOMASTER.CCNID = @pstrCCNID " + 
				" 	and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
				" 	and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
				" 	and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 	 " + 
				" 	join PRO_DCOptionMaster DCOMASTER " + 
				" 	on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 	and DCOMASTER.Version <= @pstrVersion_2		 " + 
				" 	and DCOMASTER.CCNID = @pstrCCNID		 " + 				
				"	/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				" and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				" and /*Begin of current month*/ convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 

				" 	 " + 
				" 	group by  " + 
				" 	WODETAIL.ProductID, " + 
				" 	WODETAIL.StartDate, " + 
				" 	DCOMASTER.Version " + 
				" 	 " + 
				" 	/*************************PLAN TABLE *******************************************************************************/ " + 
				" ) as PLANTABLE " + 
				" join " + 
				" ( " + 
				" 	select " + 
				" 	PLANTABLE.ProductID, " + 
				" 	PLANTABLE.PlanDate, " + 
				" 	Max(PLANTABLE.Version) as [Version] " + 
				" 	 " + 
				" 	from  " + 
				" 	( " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 		select  " + 
				" 		WODETAIL.ProductID as [ProductID], " + 
				" 		WODETAIL.StartDate as [PlanDate], " + 
				" 		SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		from " + 
				" 		PRO_WorkOrderDetail as WODETAIL " + 
				" 		join PRO_WorkOrderMaster WOMASTER " + 
				" 		on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 		and WOMASTER.CCNID = @pstrCCNID " + 
				" 		and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
				" 		and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
				" 		and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 		 " + 
				" 		join PRO_DCOptionMaster DCOMASTER " + 
				" 		on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 		and DCOMASTER.Version <= @pstrVersion_2		 " + 
				" 		and DCOMASTER.CCNID = @pstrCCNID		 " + 
				"	/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				" and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				" and /*Begin of current month*/ convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 

				" 		 " + 
				" 		group by  " + 
				" 		WODETAIL.ProductID, " + 
				" 		WODETAIL.StartDate, " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	) as PLANTABLE " + 
				" 	group by  " + 
				" 	PLANTABLE.ProductID, " + 
				" 	PLANTABLE.PlanDate " + 
				" ) as MAXVERSIONTABLE " + 
				"  " + 
				" on PLANTABLE.ProductID = MAXVERSIONTABLE.ProductID " + 
				" and PLANTABLE.PlanDate = MAXVERSIONTABLE.PlanDate " + 
				" and PLANTABLE.Version = MAXVERSIONTABLE.Version " + 
				"  " 
				/*************************PLAN TABLE *******************************************************************************/
				;

			#endregion PLANTABLE - MAIN
			/* ============================================================== */

			#region PLANTABLE - 1 - NOT MANDATORY - version = Version_1
	
			string strSqlPLANTABLE_1 =
	
				/*************************PLAN TABLE *******************************************************************************/			
		
				" SELECT  " + 
				" PLANTABLE.ProductID, " + 
				" PLANTABLE.PlanDate, " + 
				" PLANTABLE.PlanQuantity " + 
				"  " + 
				" FROM " + 
				" ( " + 
				" 	/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	select  " + 
				" 	WODETAIL.ProductID as [ProductID], " + 
				" 	WODETAIL.StartDate as [PlanDate], " + 
				" 	SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 	DCOMASTER.Version " + 
				" 	 " + 
				" 	from " + 
				" 	PRO_WorkOrderDetail as WODETAIL " + 
				" 	join PRO_WorkOrderMaster WOMASTER " + 
				" 	on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 	and WOMASTER.CCNID = @pstrCCNID " + 
				" 	and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
				" 	and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
				" 	and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 	 " + 
				" 	join PRO_DCOptionMaster DCOMASTER " + 
				" 	on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 	and DCOMASTER.Version <= @pstrVersion_1		 " + 
				" 	and DCOMASTER.CCNID = @pstrCCNID		 " + 
				" 	and	DatePart(mm   ,DCOMASTER.AsOfDate) = @pstrMonth " + 
				" 	and 	DatePart(yyyy ,DCOMASTER.AsOfDate) = @pstrYear " + 
				" 	 " + 
				" 	group by  " + 
				" 	WODETAIL.ProductID, " + 
				" 	WODETAIL.StartDate, " + 
				" 	DCOMASTER.Version " + 
				" 	 " + 
				" 	/*************************PLAN TABLE *******************************************************************************/ " + 
				" ) as PLANTABLE " + 
				" join " + 
				" ( " + 
				" 	select " + 
				" 	PLANTABLE.ProductID, " + 
				" 	PLANTABLE.PlanDate, " + 
				" 	Max(PLANTABLE.Version) as [Version] " + 
				" 	 " + 
				" 	from  " + 
				" 	( " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 		select  " + 
				" 		WODETAIL.ProductID as [ProductID], " + 
				" 		WODETAIL.StartDate as [PlanDate], " + 
				" 		SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		from " + 
				" 		PRO_WorkOrderDetail as WODETAIL " + 
				" 		join PRO_WorkOrderMaster WOMASTER " + 
				" 		on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 		and WOMASTER.CCNID = @pstrCCNID " + 
				" 		and DatePart(mm   , WODETAIL.StartDate) = @pstrMonth " + 
				" 		and DatePart(yyyy , WODETAIL.StartDate) = @pstrYear " + 
				" 		and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 		 " + 
				" 		join PRO_DCOptionMaster DCOMASTER " + 
				" 		on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 		and DCOMASTER.Version <= @pstrVersion_1		 " + 
				" 		and DCOMASTER.CCNID = @pstrCCNID		 " + 
				" 		and	DatePart(mm   ,DCOMASTER.AsOfDate) = @pstrMonth " + 
				" 		and 	DatePart(yyyy ,DCOMASTER.AsOfDate) = @pstrYear " + 
				" 		 " + 
				" 		group by  " + 
				" 		WODETAIL.ProductID, " + 
				" 		WODETAIL.StartDate, " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	) as PLANTABLE " + 
				" 	group by  " + 
				" 	PLANTABLE.ProductID, " + 
				" 	PLANTABLE.PlanDate " + 
				" ) as MAXVERSIONTABLE " + 
				"  " + 
				" on PLANTABLE.ProductID = MAXVERSIONTABLE.ProductID " + 
				" and PLANTABLE.PlanDate = MAXVERSIONTABLE.PlanDate " + 
				" and PLANTABLE.Version = MAXVERSIONTABLE.Version " + 
				"  " 
				/*************************PLAN TABLE *******************************************************************************/

				;

			#endregion PLANTABLE - 1 - NOT MANDATORY - Take all WorkOrder in month_Yearm CCN, where Version = Version_1
			/* ============================================================== */
		
			#region SOTABLE 

			string strSqlSOTABLE = 

				/****************************SO TABLE *********************************************************/
				" select 0 as ProductID, " + 
				" 0 as [SODay], " + 
				" 0.00 as [SOQuantity] " + 
				" where 0>1 "
				/****************************SO TABLE *********************************************************/			
				;

			#endregion SOTABLE (add into the PlanQuantity)

			/* ============================================================== */

			#region WOBOM TABLE

			string strSqlWOBOMTABLE = 
				
				/****************************WOBOM TABLE *********************************************************/
				" SELECT      " + 
				" 0 as [ProductID],  " + 
				" 0 as WOBOMDay, " + 
				" 0.00 AS [WOBOMQuantity]  " + 
				" where 0>1 " 
				/****************************WOBOM TABLE *********************************************************/
				;

			#endregion WOBOM TABLE

			/* ============================================================== */

			#region BEGINQUANTITY TABLE

			string strSqlBEGINQUANTITYTABLE = 				
				
				/************************* PROGRESS BEGIN TABLE ***************************************************************************/
				/* ============== PROGRESS BEGIN QUANTITY WITH Month = Parameter Month - 1 ================ */
				" SELECT  " + 
				" IsNull(PREV_PLANTABLE.ProductID,  PREV_ACTUALTABLE.ProductID) as [ProductID], " + 
				" ( IsNull(PREV_ACTUALTABLE.ActualQuantity, 0.00) - IsNull(PREV_PLANTABLE.PlanQuantity, 0.00) )    as [ProgressBeginQuantity]  " + 
				"  " + 
				" FROM " + 
				" (		 " + 
				" 	 " + 
				" 	/*************************GENERAL PLAN TABLE *********** Previous , add sum , and remove the Version constrant ************************/ " + 
				" 	SELECT  " + 
				" 	PLANTABLE.ProductID, " + 
				" 	Sum(IsNull(PLANTABLE.PlanQuantity, 0.00)) as [PlanQuantity] " + 
				" 	 " + 
				" 	FROM " + 
				" 	( " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 		select  " + 
				" 		WODETAIL.ProductID as [ProductID], " + 
				" 		WODETAIL.StartDate as [PlanDate], " + 
				" 		SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		from " + 
				" 		PRO_WorkOrderDetail as WODETAIL " + 
				" 		join PRO_WorkOrderMaster WOMASTER " + 
				" 		on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 		and WOMASTER.CCNID = @pstrCCNID " + 
				" 		and DatePart(mm   , WODETAIL.StartDate) = @pstrPreviousMonth " + 
				" 		and DatePart(yyyy , WODETAIL.StartDate) = @pstrPreviousYear " + 
				" 		and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 		 " + 
				" 		join PRO_DCOptionMaster DCOMASTER " + 
				" 		on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 		/*and DCOMASTER.Version <= @pstrVersion_2*/ " + 
				" 		and DCOMASTER.CCNID = @pstrCCNID		 " + 
				"	/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				"	and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrPreviousYear + '-' + @pstrPreviousMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				"	and /*Begin of current month*/ convert(datetime, @pstrPreviousYear + '-' + @pstrPreviousMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 

				" 		 " + 
				" 		group by  " + 
				" 		WODETAIL.ProductID, " + 
				" 		WODETAIL.StartDate, " + 
				" 		DCOMASTER.Version " + 
				" 		 " + 
				" 		/*************************PLAN TABLE *******************************************************************************/ " + 
				" 	) as PLANTABLE " + 
				" 	join " + 
				" 	( " + 
				" 		select " + 
				" 		PLANTABLE.ProductID, " + 
				" 		PLANTABLE.PlanDate, " + 
				" 		Max(PLANTABLE.Version) as [Version] " + 
				" 		 " + 
				" 		from  " + 
				" 		( " + 
				" 			/*************************PLAN TABLE *******************************************************************************/ " + 
				" 			select  " + 
				" 			WODETAIL.ProductID as [ProductID], " + 
				" 			WODETAIL.StartDate as [PlanDate], " + 
				" 			SUM(IsNull(WODETAIL.OrderQuantity, 0.00)) as [PlanQuantity], " + 
				" 			DCOMASTER.Version " + 
				" 			 " + 
				" 			from " + 
				" 			PRO_WorkOrderDetail as WODETAIL " + 
				" 			join PRO_WorkOrderMaster WOMASTER " + 
				" 			on WODETAIL.WorkOrderMasterID = WOMASTER.WorkOrderMasterID " + 
				" 			and WOMASTER.CCNID = @pstrCCNID " + 
				" 			and DatePart(mm   , WODETAIL.StartDate) = @pstrPreviousMonth " + 
				" 			and DatePart(yyyy , WODETAIL.StartDate) = @pstrPreviousYear " + 
				" 			and WOMASTER.ProductionLineID = @pstrProductionLineID " + 
				" 			 " + 
				" 			join PRO_DCOptionMaster DCOMASTER " + 
				" 			on WOMASTER.DCOptionMasterID = DCOMASTER.DCOptionMasterID " + 
				" 			/*and DCOMASTER.Version <= @pstrVersion_2*/ " + 
				" 			and DCOMASTER.CCNID = @pstrCCNID		 " + 
		
				"	/* Take all the relate to Parameter Year-month period of DCO.FromDate (AsOfDate) < first day of NextMonth. EndDate (AsOfDate + PlanHorizon) >= first day of CurrentProvidedMonth */ " + 
				"	and /*FromDate*/ DCOMASTER.AsOfDate < dateadd (month, 1, convert(datetime, @pstrPreviousYear + '-' + @pstrPreviousMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
				"	and /*Begin of current month*/ convert(datetime, @pstrPreviousYear + '-' + @pstrPreviousMonth + '-' + '01' ) <= DATEADD(day, DCOMASTER.PlanHorizon, DCOMASTER.AsOfDate) /*EndDate*/ " + 

				" 			 " + 
				" 			group by  " + 
				" 			WODETAIL.ProductID, " + 
				" 			WODETAIL.StartDate, " + 
				" 			DCOMASTER.Version " + 
				" 			 " + 
				" 			/*************************PLAN TABLE *******************************************************************************/ " + 
				" 		) as PLANTABLE " + 
				" 		group by  " + 
				" 		PLANTABLE.ProductID, " + 
				" 		PLANTABLE.PlanDate " + 
				" 	) as MAXVERSIONTABLE " + 
				" 	 " + 
				" 	on PLANTABLE.ProductID = MAXVERSIONTABLE.ProductID " + 
				" 	and PLANTABLE.PlanDate = MAXVERSIONTABLE.PlanDate " + 
				" 	and PLANTABLE.Version = MAXVERSIONTABLE.Version " + 
				" 	 " + 
				" 	GROUP BY " + 
				" 	PLANTABLE.ProductID " + 
				" 	/*************************GENERAL PLAN TABLE *********** Previous , add sum , and remove the Version constrant ************************/ " + 
				" ) as PREV_PLANTABLE " + 
				"  " + 
				" FULL outer join   " + 
				" (	 " + 
				" 	 " + 
				" 	/************************* ACTUAL TABLE ***************************************************************************/ " + 
				" 	/* get the actual work order completion - map to the ProductID */ " + 
				" 	select " + 
				" 	INNERTABLE.[ProductID], " + 
				" 	SUM(IsNull(INNERTABLE.[CompletedQuantity], 0.00) ) as [ActualQuantity] " + 
				" 	 " + 
				" 	FROM " + 
				" 	( " + 
				" 		select   " + 
				" 		PRO_WorkOrderCompletion.ProductID as [ProductID], " + 
				" 		DATEPART(dd,PRO_WorkOrderCompletion.PostDate) as [ActualDay], " + 
				" 		IsNull(PRO_WorkOrderCompletion.CompletedQuantity ,0.00) as [CompletedQuantity] " + 
				" 		 " + 
				" 		from ITM_Product " + 
				" 		join PRO_WorkOrderCompletion " + 
				" 		on ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID	 " + 
				" 		and ITM_Product.CCNID = @pstrCCNID " + 
				" 		/* NEW USECASE */ " + 
				" 		and PRO_WorkOrderCompletion.LocationID IN (select distinct LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				" 	 " + 
				" 		join PRO_WorkOrderMaster " + 
				" 		on PRO_WorkOrderCompletion.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 		and DATEPART(mm  ,PRO_WorkOrderCompletion.PostDate) = @pstrPreviousMonth " + 
				" 		and DATEPART(yyyy,PRO_WorkOrderCompletion.PostDate) = @pstrPreviousYear " + 
				" 		and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID		 " + 
				" 	/*	and PRO_WorkOrderMaster.WorkOrderMasterID = @pstrWorkOrderMasterID_2	*/ " + 
				" 	 " + 
				" 	) INNERTABLE " + 
				" 	 " + 
				" 	group by " + 
				" 	INNERTABLE.ProductID " + 
				" 	 " + 
				" 	/************************* ACTUAL TABLE ***************************************************************************/ " + 
				" ) as PREV_ACTUALTABLE " + 
				"  " + 
				" on PREV_PLANTABLE.ProductID = PREV_ACTUALTABLE.ProductID	 " 

				/************************* PROGRESS BEGIN TABLE ***************************************************************************/
				;

			#endregion BEGINQUANTITY TABLE

			/* ============================================================== */

			#region ACTUALTABLE
			/// Get the datatable : contain Mapping ProductID with it Actual COmpletion quantity, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - ActualDay - ActualQuantity
			/// 200	1	110.00000			
			/// 200	4	40.00000		
			/// 127	31	50.00000		
			/// </summary>
			/// <returns></returns>
			// private DataTable BuildActualTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID, string pstrWorkOrderMasterID_2)

			string strSqlACTUALTABLE = 
			
		
				" /************************* ACTUAL TABLE ***************************************************************************/ " + 
				" /* get the actual work order completion - map to the ProductID */ " + 
				" select " + 
				" INNERTABLE.[ProductID], " + 
				" INNERTABLE.[ActualDay], " + 
				" SUM(IsNull(INNERTABLE.[CompletedQuantity], 0.00) ) as [ActualQuantity] " + 
				"  " + 
				" FROM " + 
				" ( " + 
				" 	select   " + 
				" 	PRO_WorkOrderCompletion.ProductID as [ProductID], " + 
				" 	DATEPART(dd,PRO_WorkOrderCompletion.PostDate) as [ActualDay], " + 
				" 	IsNull(PRO_WorkOrderCompletion.CompletedQuantity ,0.00) as [CompletedQuantity] " + 
				" 	 " + 
				" 	from ITM_Product " + 
				" 	join PRO_WorkOrderCompletion " + 
				" 	on ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID	 " + 
				" 	and ITM_Product.CCNID = @pstrCCNID " + 
				" 	/* NEW USECASE */ " + 
				" 	and PRO_WorkOrderCompletion.LocationID IN (select distinct LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " + 
				"  " + 
				" 	join PRO_WorkOrderMaster " + 
				" 	on PRO_WorkOrderCompletion.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
				" 	and DATEPART(mm  ,PRO_WorkOrderCompletion.PostDate) = @pstrMonth " + 
				" 	and DATEPART(yyyy,PRO_WorkOrderCompletion.PostDate) = @pstrYear " + 
				" 	and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID		 " + 
				" /*	and PRO_WorkOrderMaster.WorkOrderMasterID = @pstrWorkOrderMasterID_2	*/ " + 
				"  " + 
				" ) INNERTABLE " + 
				"  " + 
				" group by " + 
				" INNERTABLE.ProductID, " + 
				" INNERTABLE.ActualDay " + 
				"  " + 
				" /************************* ACTUAL TABLE ***************************************************************************/ " 
				;

			#endregion ACTUALTABLE

			/* ============================================================== */
				
			try 
			{
				
				oconPCS = null;
				ocmdPCS = null;
			
				strSql += 
					strSql_META_TABLE + "\n" +  
					strSqlPLANTABLE_2 /* newer */ + "\n" +  
					strSqlPLANTABLE_1 + "\n" + 
					strSqlSOTABLE + "\n" +
					strSqlWOBOMTABLE + "\n" +
					strSqlBEGINQUANTITYTABLE + "\n" +
					strSqlACTUALTABLE + "\n" 
					;
	

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstRET);

				dstRET.Tables[0].TableName = META_TABLE_NAME;
				dstRET.Tables[1].TableName = PLAN_TABLE_NAME_2;
				dstRET.Tables[2].TableName = PLAN_TABLE_NAME_1;
				dstRET.Tables[3].TableName = SO_TABLE_NAME;
				dstRET.Tables[4].TableName = WOBOM_TABLE_NAME;
				dstRET.Tables[5].TableName = BEGINQUANTITY_TABLE_NAME;
				dstRET.Tables[6].TableName = ACTUAL_TABLE_NAME;

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
			
			return dstRET;
		}


		/// <summary>
		/// This function return the Maximum DCP Version.
		/// If on Error, or there is no previous, it will return a NEGATIVE value.
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		private int GetMaxVersion(string pstrCCNID) /* string pstrYear, string pstrMonth, string pstrElementID, */			
		{
			const int NO_VERSION = -1;
			int intRet = NO_VERSION;
			
			#region DB QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 				
				" /*-----------------------------------*/ " + 
				" Set @pstrCCNID = " +pstrCCNID+ " " + 								
				" /*-----------------------------------*/ " + 
				"  " + 			
				" select   " + 
				" IsNull( Max(Version) , 0) as MaxVersion " + 
				" from    " + 
				" PRO_DCOptionMaster  as DCOMASTER   	 " + 
				"  " + 
				" where DCOMASTER.CCNID = @pstrCCNID    " ;				

			try
			{
				intRet = Convert.ToInt32( ExecuteScalar(strSql) );
			}
			catch
			{}

			#endregion DB QUERY

			intRet = intRet == 0 ? -1 : intRet;

			return intRet;
		}	// end function


		/// <summary>
		/// This function return the Previous DCP Version of pstrCurrentVersion.
		/// If on Error, or there is no previous, it will return a NEGATIVE value.
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrPreviousPOVersion"></param>
		/// <returns></returns>
		private int GetPreviousVersion(string pstrCCNID, /* string pstrYear, string pstrMonth, string pstrElementID, */
			string pstrCurrentVersion)
		{
			const int NO_VERSION = -1;
			int intRet = NO_VERSION;
			
			#region DB QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 							
				" Declare @pstrVersion_2 int " + 							
				" /*-----------------------------------*/ " + 
				" Set @pstrCCNID = " +pstrCCNID+ " " + 				
				" Set @pstrVersion = " +pstrCurrentVersion+ " " + 				
				" /*-----------------------------------*/ " + 
				"  " + 			
				" select   " + 
				" IsNull( Max(Version) , 0) as PreviousVersion " + 
				" from    " + 
				" PRO_DCOptionMaster  as DCOMASTER   				 " + 
				"  " + 
				" where DCOMASTER.CCNID = @pstrCCNID    " + 
				" and DCOMASTER.Version < @pstrVersion_2		";			

			try
			{
				intRet = Convert.ToInt32( ExecuteScalar(strSql) );
			}
			catch
			{}

			#endregion DB QUERY

			intRet = intRet == 0 ? NO_VERSION : intRet;

			return intRet;
		}	// end function


	

		/// <summary>
		/// Modify the original Plantable (WorkOrderTable) to the Real Plan table (which have real "PlanDay", depend on the real working time of day.
		/// (We should remember that, WorkOrder start from 4AM of 12/04/2006 may has real "PlanDay" = 13, because of Shift3 of 11/04/2006 last to 6h14AM of 12/04/2006 )
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="pdtbOriginalWorkOrderPlan"></param>
		/// <param name="pdtbWorkingTimeMappingToDay"></param>
		/// <returns></returns>
		private DataTable ModifyPlanTable(DataTable pdtbOriginalWorkOrderPlan, string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			const string PLANDATE = "PlanDate";

			DataTable dtbWorkingTime = GetAllPeriodOfWorkingTime(pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID );

			// clone the schema
			DataTable dtbRet = pdtbOriginalWorkOrderPlan.Clone();
			// add new column = PLANDAY
			dtbRet.Columns.Add(PLAN + DATE, typeof(int) );
			// remove the fulltime column PLANDATE (contain yyyy mm dd hh:mm:ss)
			dtbRet.Columns.Remove(PLANDATE);
		
			// foreach row in WorkOrderTable, if PlanDate is in any WorkingTime of any Day, add it in the dtbRet table with that Day value in PlanDay column
			foreach(DataRow drow in pdtbOriginalWorkOrderPlan.Rows)
			{				
				DateTime dtmPlanDateBeforeResolve = DateTime.MinValue;
				if(drow[PLANDATE] != DBNull.Value)
				{
					dtmPlanDateBeforeResolve = DateTime.Parse(drow[PLANDATE].ToString().Trim());
				}

				int nRealWorkingDay = 	GetRealWorkingDay(dtmPlanDateBeforeResolve, dtbWorkingTime);

				if(nRealWorkingDay  > 0 && nRealWorkingDay <= 31)
				{
					DataRow dtrNew = dtbRet.NewRow();
					dtrNew[PLAN + DATE] = nRealWorkingDay;
					dtrNew[PRODUCTID] = drow[PRODUCTID];
					dtrNew[PLAN + QUANTITY] = drow[PLAN + QUANTITY];
					dtbRet.Rows.Add(dtrNew);
				}

			}
			
			return dtbRet;
		}


	
		/// <summary>
		/// get the reference table for GetRealWorkingDay() function
		/// result is the table with each record contain: 
		/// BeginDate, EndDate (of configured WCCapacity)
		/// WorkTimeFrom, WorkTimeTo	(Real working time of each shift in a working day)
		/// 
		/// SCHEMA: BeginDate, EndDate, WorkTimeFrom, WorkTimeTo
		/// 
		/// </summary>
		/// <author>Thachnn</author>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		private DataTable GetAllPeriodOfWorkingTime(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			OleDbConnection oconPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = 
  
					" Declare @pstrCCNID int " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrProductionLineID int " + 
					/*-----------------------------------*/
					"  " + 
					" Set @pstrCCNID = " +pstrCCNID+ " " + 
					" Set @pstrYear = '" +pstrYear+ "' " + 
					" Set @pstrMonth = '" +pstrMonth+ "' " + 
					" Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					" 	 " + 

					" select distinct     " + 
					"  " + 
					" WCC.BeginDate,    " + 
					" WCC.EndDate,    " + 
					" SP.WorkTimeFrom,    " + 
					" SP.WorkTimeTo     " + 
					" from     " + 
					" PRO_Shift as S    " + 
					" join PRO_ShiftPattern as SP    " + 
					" 	on S.ShiftID = SP.ShiftID    " + 
					" /*  	and ShiftDesc IN ('1S','2S','3S')   */ /*allow all shift*/ " + 
					" join PRO_ShiftCapacity as SC    " + 
					" 	on S.ShiftID = SC.ShiftID    " + 
					" join PRO_WCCapacity as WCC    " + 
					" 	on WCC.WCCapacityID = SC.WCCapacityID    " + 
					" 	 " + 
					" 	/* Take all the relate to Parameter Year-month period of WCCapacity. BeginDate < first day of NextMonth. EndDate >= first day of CurrentProvidedMonth */ " + 
					" 	and WCC.BeginDate < dateadd (month, 1, convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) ) /*beginning of next month*/ " + 
					" 	and convert(datetime, @pstrYear + '-' + @pstrMonth + '-' + '01' ) <= WCC.EndDate  " + 
					"  " + 
					" join MST_WorkCenter as WC    " + 
					" 	on WCC.WorkCenterID = WC.WorkCenterID    " + 
					" 	and WC.ProductionLineID = @pstrProductionLineID    " + 
					" 	and WC.CCNID = @pstrCCNID    " + 
					"  " + 
					"  " + 
					"  " ;
 
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
		/// Put the DateTime need to Resolve in
		/// Reference the WorkingTime table
		/// if the ResolveTime is in the working time of any shift, of a configured period, determine the real WorkingDay, and return.
		/// </summary>
		/// <param name="pdtmNeedToResolve"></param>
		/// <param name="pdtbWorkingTime"></param>
		/// <returns></returns>
		private int GetRealWorkingDay(DateTime pdtmNeedToResolve, DataTable pdtbWorkingTime)
		{
			const string BEGINDATE = "BeginDate";
			const string ENDDATE = "EndDate";
			const string WORKTIMEFROM = "WorkTimeFrom";
			const string WORKTIMETO = "WorkTimeTo";			

			if(pdtmNeedToResolve == DateTime.MinValue)
			{
				return 0;
			}		

			int iRet = 0;
			try
			{
				foreach(DataRow drow in pdtbWorkingTime.Rows)
				{
					if(1 <= iRet && iRet <= 31)
					{
						return iRet;
					}
					else // if pdtmNeedToResolve is in any period, modify the iRet, and then the next loop will break, function return
					{
						DateTime dtmBeginDate = (DateTime)drow[BEGINDATE];
						DateTime dtmEndDate = (DateTime)drow[ENDDATE];
						DateTime dtmWorkTimeFrom = (DateTime)drow[WORKTIMEFROM];
						DateTime dtmWorkTimeTo = (DateTime)drow[WORKTIMETO];

						if(dtmBeginDate <= pdtmNeedToResolve && 	/* NeedToResolve > beginDate (yyyymmdd 00 00 00) */
							new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day )  <= dtmEndDate)	/* start time of each Date of NeedToResolve <= EndDate  */
						{
							int nActualDay = pdtmNeedToResolve.Day;
							int nAdjustDay = dtmWorkTimeTo.Day - dtmWorkTimeFrom.Day;

							int nFromHour = dtmWorkTimeFrom.Hour;
							int nFromMinute = dtmWorkTimeFrom.Minute;
							int nFromSecond = dtmWorkTimeFrom.Second;
							int nFromMilisecond = dtmWorkTimeFrom.Millisecond;

							int nToHour = dtmWorkTimeTo.Hour;
							int nToMinute = dtmWorkTimeTo.Minute;
							int nToSecond = dtmWorkTimeTo.Second;
							int nToMilisecond = dtmWorkTimeTo.Millisecond;

							// slide the WorkTimeFrom (prototype 2005/01/01 xxxxxxx) to the actualWorkTimeFrom (2006/04/24 xxxx) 
							// where TimeNeedToResolve is 2006/04/24 yyy)
							DateTime dtmActualWorkTimeFrom = (new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month,pdtmNeedToResolve.Day)) /*Start Actual day*/
								.Add(	/* add the time amount from start of the day to the WorkTimeFrom day*/
								dtmWorkTimeFrom  .Subtract  (new DateTime(dtmWorkTimeFrom.Year, dtmWorkTimeFrom.Month, dtmWorkTimeFrom.Day)  )
								);
							DateTime dtmActualWorkTimeTo = dtmActualWorkTimeFrom.Add(
								dtmWorkTimeTo.Subtract(dtmWorkTimeFrom)
								);
							
							if(dtmActualWorkTimeFrom  <= pdtmNeedToResolve && pdtmNeedToResolve < dtmActualWorkTimeTo)	// RESOLVE is in the Shift worktime
							{
								int intDayDiff = dtmWorkTimeFrom.Day - 1;
								iRet = pdtmNeedToResolve.Day - intDayDiff;
							}	// end RESOLVE is in the Shift worktime

						}	// end resolve date is in the period worktime
					}
                    
				}	// end foreach datarow in REFERENCE TABLE
	
			}	// end try, there is error. perhap the cast action is fail, can't cast from DBNull
			catch
			{ 
				return 0;
			}

			return iRet;
		}

		
		/// <summary>
		/// values in pstrProductIDColName, pstrDayColName, pstrSumColName must not be NULL
		/// </summary>
		/// <param name="pdtbOriginal"></param>
		/// <param name="pstrProductIDColName"></param>
		/// <param name="pstrDayColName"></param>
		/// <param name="pstrSumColName"></param>
		/// <returns></returns>
		private DataTable SumAndGroupBy(DataTable pdtbOriginal, 
			string pstrProductIDColName,
			string pstrDayColName,
			string pstrSumColName)
		{
			DataTable dtbRet = pdtbOriginal.Clone();			

			ArrayList arrItem_Day = GetUniqueComlexKeyFromTable(pdtbOriginal, pstrProductIDColName, pstrDayColName);
			foreach(string strItemDay in arrItem_Day)
			{					
				string strFilter = string.Empty;			
				strFilter = 
					string.Format("[{0}]='{1}' AND [{2}]='{3}' ",
					pstrProductIDColName,
					strItemDay.Split('#')[0],
					pstrDayColName,
					strItemDay.Split('#')[1]					
					);				
				
				// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrows = pdtbOriginal.Select(strFilter);

				if(dtrows.Length > 0)
				{
					// Create DUMMYROW FIRST
					DataRow dtrNew = dtbRet.NewRow();

					double dblSumForThisItemDay = 0d;
					// GUIDE: for each rows in of this Item OF DTBSourceData - adjust the dblSumForThisItemDay
					foreach(DataRow dtr in dtrows)
					{					
						dblSumForThisItemDay += ReportBuilder.ToDouble( dtr[pstrSumColName] );
					}

					dtrNew[pstrProductIDColName] = dtrows[0][pstrProductIDColName];
					dtrNew[pstrDayColName] = dtrows[0][pstrDayColName];
					dtrNew[pstrSumColName] = dblSumForThisItemDay;

					dtbRet.Rows.Add(dtrNew);
				}
			}

			return dtbRet;
		}


		/// <summary>
		/// Get key pair: ProductID#Day
		/// Deliminate character is "#"
		/// </summary>
		/// <param name="pdtb"></param>
		/// <param name="pstrProductID"></param>
		/// <param name="pstrDay"></param>
		/// <returns></returns>
		private ArrayList GetUniqueComlexKeyFromTable(DataTable pdtb, string pstrProductID, string pstrDay)
		{
			ArrayList arrRet = new ArrayList();
			try
			{
				foreach (DataRow drow in pdtb.Rows)
				{	
					object objProductIDGet = drow[pstrProductID];
					object objDayGet = drow[pstrDay];
					string str = objProductIDGet.ToString().Trim() + "#" + objDayGet.ToString().Trim();
					if( !arrRet.Contains(str)  )
					{
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

	}


	#endregion  CHILD REPORT
}

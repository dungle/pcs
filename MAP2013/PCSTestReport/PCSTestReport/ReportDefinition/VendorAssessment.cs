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


namespace VendorAssessment
{
	/// <summary>
	/// Thachnn: CONCEPT to build this report
	/// 
	/// </summary>
	[Serializable]	
	public class VendorAssessment : MarshalByRefObject, IDynamicReport
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
		public VendorAssessment()
		{
		}		

		const string THIS = "ExternalReportFile:VendorAssessment";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "VendorAssessment";			
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
		int THREAD_EACHCALL_DELAY = 1000;
		/// <summary>
		/// Wait time to pooling the Threading return values (results of multi thread processing)
		/// </summary>
		int THREAD_MAINSTREAM_DELAY = 10000;

		/// <summary>
		/// Store thread Name and pointer to the thread.
		/// </summary>
		Hashtable arrThreadManager = new Hashtable();

		/// <summary>
		/// NameValueCollection: key is the ThreadName, 
		/// value now is the ThreadParameter (PartyID - MAxPORevision).
		/// It will have value when we run the RunThreadXXX function
		/// </summary>
		Hashtable arrThreadingProcessParameters = new Hashtable();

		/// <summary>
		/// Hashtable: key is the ProductionID, result is the DPro_PPM_VO object. 
		/// When modify value of Properties of  inner object of this Hashtable, remember to reassign the object into HashTable again.
		/// This Hashtable: is modify value in the child worker thread (syncronize using MUTEX)
		/// </summary>
		Hashtable arrThreadingProcessReturnValues = new Hashtable();		

		/// <summary>
		/// This array (hashtable) store the PartyID and relate MAXimum PORevision.
		/// It gets value from database.
		/// key = PartyID, value = MaxPORevision (string)
		/// </summary>
		Hashtable arrPartyID_MaxPORevision = new Hashtable();

		StringCollection arrPartyCode = new StringCollection();
		StringCollection arrPartyID = new StringCollection();
		

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
		/// BUILD THE PRODUCTION LINE LIST TABLE
		///
		/// GETTING PROGRESS NUMBERS FROM THE OTHER REPORT (VENDOR DELIVERY ASSESMENT) (multi threading)
		/// FILL TO THE DTBPARTYLIST TABLE
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
			const string REPORT_LAYOUT_FILE = "VendorAssessment.xml";
			const string REPORT_NAME = "VendorAssessment";
			short COPIES = 1;

			/// all parameter are Mandatory
			const string REPORTFLD_PARAMETER_CCN						= "lblCCN";
			const string REPORTFLD_PARAMETER_MONTH					= "lblMonth";
			const string REPORTFLD_PARAMETER_YEAR						= "lblYear";
			const string REPORTFLD_PARAMETER_PPMDEFAULT				= "lblPPMDefault";					
			
			VendorAssessment.nCCNID = int.Parse(pstrCCNID);
			VendorAssessment.nMonth = int.Parse(pstrMonth);
			VendorAssessment.nYear = int.Parse(pstrYear);
			try
			{
				VendorAssessment.nPPMDefault = int.Parse(pstrPPMDefault);			
			}
			catch{}

			string strCCN = string.Empty;
			string strMonth = string.Empty;
			string strYear = string.Empty;
			string strPurchaseOrder = string.Empty;

			const string REPORTFLD_TITLE			= "fldTitle";
			const string REPORTFLD_COMPANY			= "fldCompany";
			const string REPORTFLD_ADDRESS			= "fldAddress";
			const string REPORTFLD_TEL				= "fldTel";
			const string REPORTFLD_FAX				= "fldFax";

			const string REPORTFLD_CHART			= "fldChart";
			

			float fActualPageSize = 9000.0f;		

			System.Data.DataTable dtbPartyList;
			
			/// custom object to access and modify the dtbPartyList
			PLReportDataHelper objPLTable = new PLReportDataHelper();		
			
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
			
			//// Build the PartyList table: 			
			///	/// SCHEMA: 
			/// 		[PartyID]		///  		[Department]			///  		[Party]
			///  		[DeliveryProgress]		///  		[DeliveryRank]			///  		[DeliveryPoint]		
			///  		[ProductionProgress]		///  		[ProductionRank]		///  		[ProductionPoint]		
			///  		[QCPPM]						///  		[QCRank]				///  		[QCPoint]		
			///  		[SummaryPoint]			///  		[SummaryRank]		///  		[Comment]		
			
			objPLTable.GetDataAndCache();

			foreach(DataRow drow in objPLTable.PartyList.Rows)
			{
				arrPartyID.Add(drow[RC.VENDORID].ToString());
			}

			// total delivery plan in month
			DataTable dtbTotalDeliveryPlan = objPLTable.GetTotalPlan(pstrMonth, pstrYear, pstrCCNID);
			DataTable dtbTotalDeliveryActual = objPLTable.GetTotalActual(pstrMonth, pstrYear, pstrCCNID);
			DataTable dtbTotalReturn = objPLTable.GetTotalReturn(pstrMonth, pstrYear, pstrCCNID);
			arrPartyID_MaxPORevision = GetMaxPORevisionForEachParty(pstrCCNID, pstrYear, pstrMonth);

			#endregion BUILD some RAW DATA TABLE


			#region BUILD DATA OF REPORT
			
			#region FILL DATA FROM OTHER REPORT TO THE MAIN DATATABLE
			
			/// #### CACULATE THE PPM BY "XUAT TRA CONG DOAN TRUOC" * 1,000,000/"TONG SO LUONG ISSUE ACTUAL"
			// calculated in the main script VendorList

			/// #### GETTING PROGRESS NUMBERS FROM THE OTHER REPORT (PL DELIVERY AND PL PRODUCTION)
			// DONE: implement multi process to improve speed
			// foreach Vendor in the arrParty (we can treat as the Threading): RUN the thread to process.
			foreach(string strEachPartyID  in arrPartyID)
			{                
				RunThreadDeliveryProgress(strEachPartyID, (string)arrPartyID_MaxPORevision[strEachPartyID] );
				System.Diagnostics.Trace.WriteLine("Main calling: Suppend 1sec after call a thread");
				Thread.Sleep(THREAD_EACHCALL_DELAY);
			}

			while(nCountOfDoneThread < arrThreadManager.Count)
			{
				// TEST: thread debug
				System.Diagnostics.Trace.WriteLine("\n\nMain Thread: I check all you, but you're working. I'm continue sleeping 10 sec \n\n");
				Thread.Sleep(THREAD_MAINSTREAM_DELAY);
			}

			// TEST: thread debug
			System.Diagnostics.Trace.WriteLine("");
			System.Diagnostics.Trace.WriteLine("================================================");
			System.Diagnostics.Trace.WriteLine("Main calling: I've finished calling all threads in my work");		


			/// #### FILL TO THE DTBPRODUCTIONLINELIST TABLE			
			foreach(DPro_PPM_VO i in arrThreadingProcessReturnValues.Values)
			{				
				objPLTable.SetPLRow(i.PartyID, RC.DELIVERY + RC.PROGRESS		, i.DeliveryProgress);
				//objPLTable.SetPLRow(i.PartyID, RC.QC + RC.PPM						, i.PPM);
			}

			/// #### FILL PPM VALUE TO THE DTB VENDOR LIST TABLE
			// already in

			#endregion FILL DATA


		 
			/// #### SET POINT FOR DELIVERY PRODUCTION, QUALITY CONTROL
			objPLTable.SetPointToFieldBaseOnOtherField(RC.DELIVERY		+ RC.POINT, RC.DELIVERY		+ RC.PROGRESS,  100m, 95m, 0.5m);			
			objPLTable.SetPointToFieldBaseOnOtherField(RC.QC				+ RC.POINT, RC.QC				+ RC.PPM,  Convert.ToDecimal(nPPMDefault), Convert.ToDecimal(nPPMDefault+10), 1m);					

			/// #### DO THE ANALISYS AND UPDATE RANK			
			objPLTable.SetRankBaseOnPoint(RC.DELIVERY		+ RC.RANK, RC.DELIVERY		+ RC.POINT, true);			
			objPLTable.SetRankBaseOnPoint(RC.QC					+ RC.RANK, RC.QC				+ RC.POINT, true);
		
			/// #### SUM THE TOTAL POINT FOR EACH PL
			objPLTable.SumPoint_Delivery_Production_QualityControl();

			/// #### ANALISE OVERALL RANK 
			objPLTable.SetRankBaseOnPoint(RC.SUMMARY	+ RC.RANK, RC.SUMMARY + RC.POINT, true);			

			/// #### REORDER THE  DATATABLE
			// auto completed after ranking the summary point

			#endregion BUILD DATA OF REPORT


			
			/// #### RENDER TO REPORT
			#region RENDER REPORT
			
			ReportBuilder objRB = new ReportBuilder();				
			objRB.ReportName = REPORT_NAME;
			// update 5 new fields value
			objPLTable.UpdateTotalValue(dtbTotalDeliveryPlan, dtbTotalDeliveryActual, dtbTotalReturn);
			objRB.SourceDataTable = objPLTable.PartyList;	// we build report base on HashTable, not DataTable, so we put new empty DataTable in to ReportBuilder to avoid error of outside processing
			
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
			

			if(objPLTable.PartyList.Rows.Count > 0)	/// if having Party working in this production Line
			{			
					#region BUILD CHART, save to image in clipboard, and then put in the report field fldChart			

					Field fldChart = objRB.GetFieldByName(REPORTFLD_CHART);
			
					#region	INIT

					//				string APP_PATH = @"D:\PCS Project\07-Construction\Source\PCS\PCSMain\bin\Debug";				
					string EXCEL_REPORT_FOLDER = "ExcelReport";			
					string EXCEL_FILE = "VendorAssessment.xls";
			
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
				
						int nNumberOfChartColumn = objPLTable.PartyList.Rows.Count;

						string[] arrExcelColumnHeading = new string[nNumberOfChartColumn ];
						double[,] arrPoint = new double[1,nNumberOfChartColumn];						
						double[,] arrStandard = new double[1,nNumberOfChartColumn];
					
						/// getting value  GET THE TOTAL POINT COLUMN TO THE ARRAY
						BuildChartData(objPLTable.PartyList,
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
			}	/// end if having Party working in this production Line
			
			
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
			mResult = objPLTable.PartyList;
			return objPLTable.PartyList;
		}	

		/// <summary>
		/// Worker funtion,
		/// new thread object,
		/// take parameter, assign, to arrThreadingProcessParameters
		/// and then start theading (call the GetVendorDeliveryProgress_ProportionAchievePlanPercent function)
		/// </summary>
		/// <param name="pstrTheadName"></param>
		/// <returns></returns>
		private bool RunThreadDeliveryProgress(string pstrPartyID, string pstrMaxPORevision)
		{
			bool blnRet = false;	//  assign Thread running status here
			
			// new thread
			Thread myThread = new Thread(new ThreadStart(GetVendorDeliveryProgress_ProportionAchievePlanPercent));			
			// naming that thread
			myThread.Name = pstrPartyID + RC.DELIVERY;
			// add to the ThreadManager
			arrThreadManager.Add(myThread.Name, myThread);
			// add value to this array, then the running thread will know which parameter it will take to do processing
			arrThreadingProcessParameters.Add(myThread.Name, new ThreadParameter(pstrPartyID, pstrMaxPORevision) );

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
		private void BuildChartData(DataTable pdtbPartyData, 
			ref string[] parrParty, 
			ref double[,] parrPoint, 
			ref double[,] parrStandard
			)
		{
			int nNumberOfChartColumn = pdtbPartyData.Rows.Count;

			int i = 0;
			foreach(DataRow drow in pdtbPartyData.Rows)
			{	
				/// BUILD Party -Heading of the chart (in the X Axis)
				//parrParty[i] = parrPartyCode[i];
				parrParty[i] = drow[RC.VENDORNAME].ToString();

				double dblPoint = 0.0;
				double dblStandard = 0.0;
				
				try
				{
					
					dblPoint = Decimal.ToDouble( PLReportDataHelper.ConvertDataRowToPLObject(drow).SummaryPoint ) ;
				}
				catch{}
				
				try
				{
					dblStandard = Decimal.ToDouble(  PLReportDataHelper.ConvertDataRowToPLObject(drow).Standard );
				}
				catch{}
				
				parrPoint[0,i] = dblPoint;
				parrStandard[0,i]  = dblStandard;

				i++;
			}		

			
		}
		
		
		/// <summary>
		/// This function will return a Hashtable contain PartyID - MaxPORevision pair (both in STRING TYPE).
		/// Revision number must be larger than 0
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <returns>Hashtable, with key = PartyID, value = MaxPORevision</returns>
		private Hashtable GetMaxPORevisionForEachParty(string pstrCCNID, string pstrYear, string pstrMonth)
		{
			Hashtable arrRet = new Hashtable();

			const string PARTYID ="PartyID";
			const string MAXPOREVISION ="MaxPORevision";

			// after this step, we will have list of pair: PartyID - MaxPORevision
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
				" /*-----------------------------------*/ " + 
				"  " + 
				 " select      " + 
 " POMASTER.PartyID as  " +PARTYID+ "  ,     " + 
 " Max(PORevision) as  " +MAXPOREVISION+ "    " + 
 " from       " + 
 " PO_PurchaseOrderMaster as POMASTER " + 
 " join PO_PurchaseOrderDetail as PODETAIL " + 
 " on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
 " join PO_DeliverySchedule PODELI " + 
 " on PODETAIL.PurchaseOrderDetailID = PODELI.PurchaseOrderDetailID " + 
 "  " + 
 " where  " + 
 " POMASTER.CCNID = @pstrCCNID       " + 
 " and DatePart(mm   , PODELI.ScheduleDate) = @pstrMonth       " + 
 " and DatePart(yyyy , PODELI.ScheduleDate) = @pstrYear        " + 
 " and POMASTER.PORevision > 0    " + 
 "  " + 
 " Group by    " + 
 " POMASTER.PartyID    " + 
				"  " ;
			/*-----------------------------------*/

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
				if(drow[PARTYID] != DBNull.Value && drow[MAXPOREVISION] != DBNull.Value )
				{
					arrRet.Add(drow[PARTYID].ToString() , drow[MAXPOREVISION].ToString() );
				}
			}			
			
			return arrRet;
		}	// end function

		
		#region			MULTI THREAD RUNNING TO GET PROPORTIONACHIEVEPLANPERCENT of each ChildReport
				
		/// <summary>
		/// Hardworking processing, running in each Thread.
		/// This processing will get the strPartyID from the Parameter array (this array is build in order to pass parameter to thread)
		/// Run the PartyDeliveryProgressReport with parameter getting from the Parameter array
		/// Store strResult (the ProportionPercent) in the arrThreadingProcessReturnValues (this array is build in order to store return value of the thread processing)
		/// </summary>
		public void GetVendorDeliveryProgress_ProportionAchievePlanPercent()
		{
			VendorDeliveryAssessment objPP = new VendorDeliveryAssessment();
			objPP.PCSConnectionString = mConnectionString;
			objPP.ReportDefinitionFolder = mstrReportDefinitionFolder;
			objPP.ReportLayoutFile = "VendorDeliveryAssessment.xml";
			objPP.UseReportViewerRenderEngine = false;

			// it is really a parameter, but we can't pass the parameter to the ThreadStart delegate, so we must use this way to pass parameter
			string pstrPartyID = ((ThreadParameter)arrThreadingProcessParameters[Thread.CurrentThread.Name]).PartyID;
			string pstrMaxPORevParameter = ((ThreadParameter)arrThreadingProcessParameters[Thread.CurrentThread.Name]).MaxPORevision;

			// all other parameters are the share variables (they are consistent in this report scope)

			// running external report process
			string strResult = "-";

			#region SPEED UP: short circuit if there is no PORevision
			if(pstrPartyID == string.Empty || pstrPartyID == null ||
				pstrMaxPORevParameter == string.Empty || pstrMaxPORevParameter == null)
			{
                // do nothing, result still is  "-"  UNKNOWN               
			}
			#endregion SPEED UP
			else
			{
				objPP.ExecuteReport(nCCNID.ToString() ,nYear.ToString(),nMonth.ToString(),    
					pstrPartyID, string.Empty , pstrMaxPORevParameter , strProportionStandard);

				strResult = objPP.Result.ToString();
			}
			
			
			// START PROTECTED ACCESS
			objMutex.WaitOne();			
			if(arrThreadingProcessReturnValues.ContainsKey(pstrPartyID)   )
			{
				DPro_PPM_VO objDPro_PPM_VO = (DPro_PPM_VO)arrThreadingProcessReturnValues[pstrPartyID];	// extract and casting
				objDPro_PPM_VO.DeliveryProgress = strResult;	// MODIFY PROPERTIES OF INNER OBJECT
				arrThreadingProcessReturnValues[pstrPartyID] = objDPro_PPM_VO;	// reassign
			}
			else
			{
				DPro_PPM_VO objDPro_PPM_VO = new DPro_PPM_VO(int.Parse(pstrPartyID), strResult, string.Empty);
				arrThreadingProcessReturnValues.Add(pstrPartyID,objDPro_PPM_VO);
			}            

			// almost DONE of this thread bussiness
			nCountOfDoneThread++;
			objMutex.ReleaseMutex();
			// END PROTECTED ACCESS

			// TEST: thread debug
			System.Diagnostics.Trace.WriteLine("PartyDeliveryProgressReport running with <" + pstrPartyID +"> result is <"+strResult+">");
		}
		
		
		#endregion
	}	// end class report
	/// <summary>
	/// collect all task affect to the DataTable. 
	/// Must set the PartyList datatable to the InnerDataTable of instance of this class to processing.
	/// </summary>
	public class PLReportDataHelper
	{		
		// it is a offline cache of data from database 
		// main dataset of this report, contain all data get from database
		private DataSet mdst_MAIN_DATA_REPOSITORY = new DataSet("MAIN_DATA_REPOSITORY");
		
		/// <summary>
		/// Party List table: holding data of Vendor and PPM of selected month.
		/// /// SCHEMA: 
		/// 		[PartyID]		///  		[Department]			///  		[Party]
		///  		[DeliveryProgress]		///  		[DeliveryRank]			///  		[DeliveryPoint]		
		///  		[ProductionProgress]		///  		[ProductionRank]		///  		[ProductionPoint]		
		///  		[QCPPM]						///  		[QCRank]				///  		[QCPoint]		
		///  		[SummaryPoint]			///  		[SummaryRank]		///  		[Comment]
		/// </summary>
		private DataTable mdtbPartyList;
		public DataTable PartyList
		{
			get
			{
				return mdtbPartyList;
			}
			set
			{
				mdtbPartyList = value;
			}
		}
		

		#region GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable) - TRANSFORM SOME THING

		public DataTable GetTotalPlan(string pstrMonth, string pstrYear, string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT PartyID, SUM(ISNULL(DeliveryQuantity,0)) AS PlanQuantity"
					+ " FROM PO_PurchaseOrderMaster PM JOIN PO_PurchaseOrderDetail PD"
					+ " ON PM.PurchaseOrderMasterID = PD.PurchaseOrderMasterID"
					+ " JOIN PO_DeliverySchedule DS ON PD.PurchaseOrderDetailID = DS.PurchaseOrderDetailID"
					+ " WHERE DATEPART(mm, ScheduleDate) = " + pstrMonth
					+ " AND DATEPART(yyyy, ScheduleDate) = " + pstrYear
					+ " GROUP BY PartyID ORDER BY PartyID";
			
				
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

		public DataTable GetTotalActual(string pstrMonth, string pstrYear, string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT POMASTER.PartyID as [PartyID], Sum(IsNull(POREDETAIL.ReceiveQuantity, 0.00)) as [ActualQuantity]"
					+ " from PO_PurchaseOrderReceiptMaster as POREMASTER"
					+ " join PO_PurchaseOrderReceiptDEtail as POREDETAIL"
					+ " on POREMASTER.PurchaseOrderReceiptID = POREDETAIL.PurchaseOrderReceiptID"
					+ " and DATEPART(mm  , POREMASTER.PostDate) = " + pstrMonth
					+ " and DATEPART(yyyy, POREMASTER.PostDate) = " + pstrYear
					+ " and POREMASTER.CCNID = " + pstrCCNID
					+ " join PO_PurchaseOrderMaster as POMASTER"
					+ " on POREDETAIL.PurchaseOrderMasterID = POMASTER.PurchaseOrderMasterID"
					+ " Group by POMASTER.PartyID ORDER BY POMASTER.PartyID";


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

		public DataTable GetTotalReturn(string pstrMonth, string pstrYear, string pstrCCNID)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			try 
			{
				DataTable dtbData = new DataTable();
				oconPCS = null;
				ocmdPCS = null;

				strSql = "SELECT RETURNMASTER.PartyID as [PartyID], IsNull( Sum( IsNull(RETURNDETAIL.Quantity,0.00) ), 0.00)  as [ReturnToVendorQuantity]"
					+ " from PO_ReturnToVendorMaster as RETURNMASTER"
					+ " join PO_ReturnToVendorDetail as RETURNDETAIL"
					+ " on RETURNMASTER.ReturnToVendorMasterID = RETURNDETAIL.ReturnToVendorMasterID "
					+ " and DATEPART(mm  , RETURNMASTER.PostDate) = " + pstrMonth
					+ " and DATEPART(yyyy, RETURNMASTER.PostDate) = " + pstrYear
					+ " and RETURNMASTER.CCNID = " + pstrCCNID
					+ " Group by RETURNMASTER.PartyID ORDER BY RETURNMASTER.PartyID";
			
				
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

		/// <summary>
		/// Get Production Line List (table) with PPM values
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
					/* Main Idea of this query is: Get the VENDOR List first,
					Left join with the PPM Table

					PPM Table (Quantity is ReturnQUantity/ActualDeliveryQuantity, share key is PartyiD) is componented by:
					RETURNTABLE ---> we get the Return to previous Location Quantity
					ACTUALTABLE ----> componented by: SHIPPING, ISSUE4WO, MISCISSUE (sum all the  QUantity, with share key is PartyID)

					*/

					" Declare @pstrYear char(4) " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrPPMDefault int " + 
					" Declare @pstrCCNID int " +  
					" /*-----------------------------------*/ " + 
					"  " + 
					" /*-----------------------------------*/ " + 
					" Set @pstrYear = '" +VendorAssessment.nYear+ "' " + 
					" Set @pstrMonth = '" +VendorAssessment.nMonth+ "' " + 
					" Set @pstrPPMDefault = " +VendorAssessment.nPPMDefault+ " " + 
					" Set @pstrCCNID = " +VendorAssessment.nCCNID+ " " +  
					" /*-----------------------------------*/ " + 
					" 	 " + 
					"   " + 
					" /*----------------- VENDOR LIST ------------------*/ " + 
					" select  " + 
					" MST_Party.PartyID as [PartyID], " + 
					" MST_Party.Code as [PartyCode], " + 
					" MST_Party.Name as [PartyName], " + 
					" 0.00 AS DeliveryActual, " + 
					" 0.00 AS DeliveryPlan, " + 
					" 0.00 AS Progress, " + 
					" '0.00' as [DeliveryProgress], " + 
					" 0 as [DeliveryRank], " + 
					" 0 as [DeliveryPoint], " + 
					" 0.00 AS TotalReceipt, " + 
					" 0.00 AS TotalReturn, " + 
					" Isnull(PPMTABLE.QCPPM, 0) as [QCPPM], " + 
					" 0 as [QCRank], " + 
					" 0 as [QCPoint], " + 
					" 0 as [SummaryPoint], " + 
					" 0 as [SummaryRank], " + 
					" 0 as [Standard], " + 
					" ' ' as [Comment] " + 
					"  " + 
					"  " + 
					"  " + 
					" from MST_Party " + 
					" /*----------------- VENDOR LIST ------------------*/ " + 
					" 	 " + 
					" LEFT join " + 
					" ( " + 
					"  " + 
					" 	select ACTUALISSUETABLE.PartyID , " + 
					" 	Isnull ( (RETURNTABLE.ReturnToVendorQuantity / ACTUALISSUETABLE.ActualQuantity)*1000000   , 0 ) as QCPPM " + 
					" 	 " + 
					" 	from  " + 
					" 	(		 " + 
					" 		/*BEGIN  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
					" 		select  " + 
					" 		POMASTER.PartyID as [PartyID],  " + 
					" 		Sum(IsNull(POREDETAIL.ReceiveQuantity, 0.00)) as [ActualQuantity] " + 
					" 		 " + 
					" 		from PO_PurchaseOrderReceiptMaster as POREMASTER " + 
					" 		join PO_PurchaseOrderReceiptDEtail as POREDETAIL " + 
					" 		on POREMASTER.PurchaseOrderReceiptID = POREDETAIL.PurchaseOrderReceiptID " + 
					" 		and DATEPART(mm  , POREMASTER.PostDate) = @pstrMonth " + 
					" 		and DATEPART(yyyy, POREMASTER.PostDate) = @pstrYear " + 
					" 		and POREMASTER.CCNID = @pstrCCNID " + 
					" 		/* and POREDETAIL.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID_2 */ " + 
					" 		 " + 
					" 		join PO_PurchaseOrderMaster as POMASTER " + 
					" 		on POREDETAIL.PurchaseOrderMasterID = POMASTER.PurchaseOrderMasterID " + 
					" 		 " + 
					" 		Group by  " + 
					" 		POMASTER.PartyID  " + 
					" 		/*END  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
					" 		 " + 
					" 	) as ACTUALISSUETABLE " + 
					" 	 " + 
					" 	LEFT join  " + 
					" 	 " + 
					" 	( " + 
					" 		/*BEGIN ********************RETURN TO VENDOR ***************************************************/ " + 
					" 		 " + 
					" 		select  " + 
					" 		RETURNMASTER.PartyID as [PartyID], " + 
					" 		IsNull( Sum( IsNull(RETURNDETAIL.Quantity,0.00) ), 0.00)  as [ReturnToVendorQuantity] " + 
					" 		 " + 
					" 		from PO_ReturnToVendorMaster as RETURNMASTER " + 
					" 		join PO_ReturnToVendorDetail as RETURNDETAIL " + 
					" 		on RETURNMASTER.ReturnToVendorMasterID = RETURNDETAIL.ReturnToVendorMasterID  " + 
					" 		/* and RETURNMASTER.PurchaseOrderMasterID = @pstrPurchaseOrderMasterID_2 */ " + 
					" 		and DATEPART(mm  , RETURNMASTER.PostDate) = @pstrMonth " + 
					" 		and DATEPART(yyyy, RETURNMASTER.PostDate) = @pstrYear " + 
					" 		and RETURNMASTER.CCNID = @pstrCCNID " + 
					" 		 " + 
					" 		Group by " + 
					" 		RETURNMASTER.PartyID " + 
					" 		 " + 
					" 		/*END **********************RETURN TO VENDOR ***************************************************/	 " + 
					" 	 " + 
					" 	) as RETURNTABLE " + 
					" 	on ACTUALISSUETABLE.PartyID = RETURNTABLE.PartyID " + 
					"  " + 
					" ) as PPMTABLE " + 
					" on MST_Party.PartyID = PPMTABLE.PartyID	 " + 
					"  ORDER BY MST_Party.Code";

				
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

			mdtbPartyList = mdst_MAIN_DATA_REPOSITORY.Tables[0];

			return true;
		}
		
		#endregion GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable)

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



		#region      INNER DATA (process on the mdtbPartyList) MANIPULATE FUNCTION		

		public DataRow GetPLRow(string pstrPLCode)
		{
			foreach(DataRow drow in mdtbPartyList.Rows)
			{
				if(drow[RC.VENDORNAME].ToString().Equals(pstrPLCode))
				{
					return drow;
				}
			}

			return null;
		}


		public DataRow GetPLRow(int pintPL_ID)
		{
			foreach(DataRow drow in mdtbPartyList.Rows)
			{
				if( (int)drow[RC.VENDORID] == pintPL_ID)
				{
					return drow;
				}
			}

			return null;
		}


		public bool SetPLRow(string pstrPLCode, string pstrColummName, object pobjValue)
		{
			foreach(DataRow drow in mdtbPartyList.Rows)
			{
				if(drow[RC.VENDORNAME].ToString().Equals(pstrPLCode)  )
				{
					drow[pstrColummName] = pobjValue;
					return true;
				}
			}
			
			return false;
		}
		public bool SetPLRow(int pintID, string pstrColummName, object pobjValue)
		{
			foreach(DataRow drow in mdtbPartyList.Rows)
			{
				if((int)drow[RC.VENDORID] == pintID  )
				{
					drow[pstrColummName] = pobjValue;
					return true;
				}
			}
			
			return false;
		}

		public bool UpdateTotalValue(DataTable pdtbTotalPlan, DataTable pdtbTotalActual, DataTable pdtbTotalReturn)
		{
			foreach (DataRow row in mdtbPartyList.Rows)
			{
				string strPartyID = row[RC.VENDORID].ToString();
				decimal decTotalPlan = 0, decTotalActual = 0, decTotalReturn = 0;
				try
				{
					decTotalPlan = Convert.ToDecimal(pdtbTotalPlan.Compute("SUM(PlanQuantity)", "PartyID = " + strPartyID));
				}
				catch{}
				try
				{
					decTotalActual = Convert.ToDecimal(pdtbTotalActual.Compute("SUM(ActualQuantity)", "PartyID = " + strPartyID));
				}
				catch{}
				try
				{
					decTotalReturn = Convert.ToDecimal(pdtbTotalReturn.Compute("SUM(ReturnToVendorQuantity)", "PartyID = " + strPartyID));
				}
				catch{}
				row[RC.DELIVERYPLAN] = decTotalPlan;
				row[RC.DELIVERYACTUAL] = decTotalActual;
				row[RC.PROGRESS] = decTotalActual - decTotalPlan;
				row[RC.TOTALRECEIPT] = decTotalActual;
				row[RC.TOTALRETURN] = decTotalReturn;
			}
			return true;
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
				DataRow[] arrSortedRows = mdtbPartyList.Select(string.Empty, pstrBaseOnField + " DESC");

				// copy only the table schema
				DataTable dtbSorted = mdtbPartyList.Clone();
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
				mdtbPartyList = dtbSorted;                
			}

			#region not same ranking
			else	// do not allow same ranking
			{				
				// int nLowestRank = mdtbInnerDataTable.Rows.Count;

				// after caculate the point, total point will never equals with DBNull because of we try catch DBNull then convert to 0m
				DataRow[] arrSortedRows = mdtbPartyList.Select(string.Empty, pstrBaseOnField + " DESC");

				// copy only the table schema
				DataTable dtbSorted = mdtbPartyList.Clone();
				// and then, fill data in the sort datarows to the new schema table
				int i = HIGHEST_RANK;
				foreach(DataRow drow in arrSortedRows)
				{
					drow[pstrSetToField] = i;
					dtbSorted.ImportRow(drow);
					i++;
				}
				mdtbPartyList = dtbSorted;
			}
			#endregion
		}

		
		public static PL ConvertDataRowToPLObject(DataRow prow)
		{
			PL objRet = new PL();

			objRet.PartyID = Convert.ToInt32(prow[RC.VENDORID]);
			objRet.PartyCode = Convert.ToString(prow[RC.VENDORCODE]);
			objRet.PartyName = Convert.ToString(prow[RC.VENDORNAME]);
			objRet.DeliveryProgress = Convert.ToDecimal(prow[RC.DELIVERY + RC.PROGRESS]);
			objRet.DeliveryRank = Convert.ToDecimal(prow[RC.DELIVERY + RC.RANK]);
			objRet.DeliveryPoint = Convert.ToDecimal(prow[RC.DELIVERY + RC.POINT]);			
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

			foreach(DataRow drow in mdtbPartyList.Rows)
			{
				d1 = 0m;
				d2 = 0m;
				d3 = 0m;
				d4 = 0m;
			
				if ( !(drow[RC.DELIVERY + RC.POINT] is DBNull) )						
					d1 = Convert.ToDecimal( drow[RC.DELIVERY + RC.POINT] ) ;

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
			
			foreach(DataRow drow in mdtbPartyList.Rows)
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
		public string PartyID;
		public string MaxPORevision;

		public ThreadParameter(string p_PartyID, string p_MaxPORevision)
		{
            this.PartyID = p_PartyID;
			this.MaxPORevision = p_MaxPORevision;
		}
	}

	/// <summary>
	/// this Report constant
	/// </summary>
	public struct RC
	{
		
		public static string DELIVERY = "Delivery";		
		public static string QC = "QC";
		public static string SUMMARY = "Summary";
		public static string DELIVERYPLAN = "DeliveryPlan";
		public static string DELIVERYACTUAL = "DeliveryActual";
		public static string TOTALRECEIPT = "TotalReceipt";
		public static string TOTALRETURN = "TotalReturn";

		public static string VENDORID = "PartyID";
		public static string VENDORCODE = "PartyCode";
		public static string VENDORNAME = "PartyName";
		
		public static string PROGRESS = "Progress";
		public static string RANK = "Rank";
		public static string POINT = "Point";

		public static string PPM = "PPM";

		public static string STANDARD = "Standard";
		public static string COMMENT = "Comment";
	}

	/// <summary>
	/// Party
	/// </summary>
	public class PL : IComparable
	{
		#region PRIVATE (BUT PUBLIC FOR LAZY IMPLEMENTATION) FIELDS				
		public int PartyID;
		public string PartyCode;
		public string PartyName;

		public decimal DeliveryProgress;
		public decimal DeliveryRank;
		public decimal DeliveryPoint;
	
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
		public PL()
		{
		}		

		public PL(int pintPartyID,
			string pstrPartyCode , string pstrPartyName,
			decimal pdecDeliveryProgress, decimal pdecDeliveryRank, decimal pdecDeliveryPoint,			
			decimal pdecQualityPPM, decimal pdecQualityRank, decimal pdecQualityPoint,
			decimal pdecSummaryPoint, decimal pdecSummaryRank, 
			decimal pdecStandard,
			string pstrComment)
		{
			this.PartyID = pintPartyID;
			 this.PartyCode = pstrPartyCode;
			 this.PartyName = pstrPartyName;
			 this.DeliveryProgress = pdecDeliveryProgress; 
			 this.DeliveryRank =  pdecDeliveryRank; 
			 this.DeliveryPoint =  pdecDeliveryPoint;			 
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
			if (!(obj is PL))
				throw new ArgumentException();

			PL t2 = (PL) obj;

			//first compare PartyID			
			int cmp1 = this.PartyID.CompareTo(t2.PartyID);
			if (cmp1 != 0) return cmp1;
			return this.PartyCode.CompareTo(t2.PartyCode);
		}

		#endregion
	}	// end class PL


	public class DPro_PPM_VO 
	{
		private int mnPartyID;		
		private string mstrDeliveryProgress;
		private string mstrPPM;
		
		public int PartyID
		{
			get
			{
				return mnPartyID;
			}
			set
			{
				mnPartyID = value;
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

		public DPro_PPM_VO()
		{}

		public DPro_PPM_VO(int pintPartyID, string pstrDeliveryProgress, string pstrPPM)
		{
			this.mnPartyID = pintPartyID;			
			this.mstrDeliveryProgress = pstrDeliveryProgress;
			this.mstrPPM = pstrPPM;
		}
	}


	public class PartyCollection : CollectionBase
	{		
		public PartyCollection()
		{
		}


		
		#region	Implement Collection
		public void Add(PL p_tm)
		{
			this.InnerList.Add(p_tm);
		}

		public PL this[int idx]
		{
			get
			{
				return (PL) this.InnerList[idx];
			}
			set
			{
				this.InnerList[idx] = value;
			}
		}

		public PL this[string sdx]
		{
			get
			{
				foreach(PL iPL in this.InnerList)
					if (iPL.PartyCode.Equals(sdx)) 
						return iPL;

				return null;			
			}
			set
			{			
				foreach(PL iPL in this.InnerList)
				{
					if (iPL.PartyCode.Equals(sdx)) 
					{
						int idx = InnerList.IndexOf(iPL);
						this.InnerList[idx] = value;
						break;
					}
				}					
			}
		}

		public bool Contains(PL t)
		{
			foreach(PL t2 in this.InnerList)
				if (t2.CompareTo(t) == 0) return true;
			return false;
		}
		public void Sort()
		{
			this.InnerList.Sort();
		}

		public void Remove(PL t)
		{
			if (! this.Contains(t)) return;
			for(int i=0;i<this.InnerList.Count;i++)
			{
				PL t2 = (PL) this.InnerList[i];
				if (t2.CompareTo(t) == 0)
				{
					this.RemoveAt(i);
					return;
				}
			}
		}

		public int BinarySearch(PL t)
		{
			return this.InnerList.BinarySearch(t);
		}
		public void Reverse()
		{
			this.InnerList.Reverse();
		}
		public int IndexOf(PL t)
		{
			return this.InnerList.IndexOf(t);
		}

		public int IndexOf(PL t, int start)
		{
			return this.InnerList.IndexOf(t, start);
		}
		public int LastIndexOf(PL t)
		{
			return this.InnerList.LastIndexOf(t);
		}

		public int LastIndexOf(PL t, int start)
		{
			return this.InnerList.LastIndexOf(t, start);
		}

		public void RemoveRange(int index, int count)
		{
			this.InnerList.RemoveRange(index, count);
		}		

		public void CopyTo(PL[] typemapArr, int start)
		{
			this.InnerList.CopyTo(typemapArr, start);
		}

		public PL[] ToArray()
		{
			return (PL[]) this.InnerList.ToArray(typeof(PL));
		}


		#endregion

		
	}



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
	public class VendorDeliveryAssessment : MarshalByRefObject, IDynamicReport		            
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

		

		public VendorDeliveryAssessment()
		{
		}		

		
	
		#region GLOBAL CONSTANT
		
		const string THIS = "ExternalReportFile:VendorDeliveryAssessment";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "VendorDeliveryAssessment";	
		const string ZERO_STRING = "0";
		const string ASSESSMENT_OK = "O";
		const string ASSESSMENT_NG = "X";
		const string MONTH_DATE_FORMAT = "MMM";

		/// Report layout file constant
		const string REPORT_LAYOUT_FILE = "VendorDeliveryAssessment.xml";
		const string REPORT_NAME = "VendorDeliveryAssessment";
		short COPIES = 1;

		/// all parameter are Mandatory
		const string REPORTFLD_PARAMETER_CCN						= "fldParameterCCN";
		const string REPORTFLD_PARAMETER_MONTH					= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_YEAR						= "fldParameterYear";			
		const string REPORTFLD_PARAMETER_VENDOR		= "fldParameterParty";
		const string REPORTFLD_PARAMETER_POREVISION1			= "fldParameterPORevision1";
		const string REPORTFLD_PARAMETER_POREVISION2			= "fldParameterPORevision2";		
		const string REPORTFLD_PROPORTIONSTANDARDPERCENT	= "fldProportionStandardPercent";

		/// Result Data Table Column names, contain:
		/// PRODUCTID, CATEGORY,PARTNO,MODEL,BEGIN
		/// DATE
		/// PLAN+QUANTITY, ACTUAL+QUANTITY
		const string PRODUCTID = "ProductID";
		const string CATEGORY = "Category";
		const string PARTNO = "Part No.";
		const string PARTNAME = "PartName";
		const string MODEL = "Model";
		const string BEGIN = "ProgressBeginQuantity";

		const string DATE = "Day";
		const string QUANTITY = "Quantity";	// suffix for PLAN,ACTUAL , RETURN column

		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYOFWEEK = "lblDayOfWeek";


		/// other constants			
		const string PLAN = "Plan";
		const string ADJ = "Adj";
		const string ACTUAL = "Actual";
		const string PROGRESSDAY = "ProgressDay";
		const string PROGRESS = "ProgressAccumulate";
		const string ASSESSMENT = "Assessment";
		const string RETURN = "Return";
		const string ROWCOUNTPASS = "RowCountPass";
		const string ROWCOUNTFAIL = "RowCountFail";
		const string ROWPERCENT = "RowPercent";

		const string FLD = "fld";		
		const string LBL = "lbl";
		const string HEADING = "DayHeading";


		const string PLANFAIL = "PlanFailD";
		const string PLANPASS = "PlanPassD";

		/// chart fields
		const string REPORTFLD_CHART	= "fldChart";
		const string REPORTFLD_TOTALCHART = "fldTotalChart";

		const string REPORTFLD_TOTALPASS = "fldPlanPassSumRow";
		const string REPORTFLD_TOTALFAIL = "fldPlanFailSumRow";		

		#endregion GLOBAL CONSTANT


		#region GLOBAL VAR
		

		DataSet dstPLPP = new DataSet();
		
		string PLAN_TABLE_NAME_1 = "PlanTable1";
		string PLAN_TABLE_NAME_2 = "PlanTable2";
		string ACTUAL_TABLE_NAME = "ActualTable";
		string ADJ_TABLE_NAME = "AdjTable";
		string RETURN_TO_VENDOR_TABLE_NAME = "ReturnTable";

		#endregion GLOBAL VAR

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrPurchaseOrderMasterID_1"></param>
		/// <param name="pstrPurchaseOrderMasterID_2"></param>
		/// <param name="pstrProportionStandard">You must fill 0.xx here ( a number less than 1 and greater or equal 0)</param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrPORevision_1, string pstrPORevision_2, string pstrProportionStandard)
		{
			#region My variables

			int nCCNID = int.Parse(pstrCCNID);
			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);			
			int nPartyID = int.Parse(pstrPartyID);			
			int nRevision_2 = int.Parse(pstrPORevision_2);			
			int nRevision_1 = GetPreviousPORevision(pstrCCNID, pstrYear,  pstrMonth, pstrPartyID, pstrPORevision_2);
			
			double dblProportionStandard = 0.95d;
			try
			{
				dblProportionStandard = Convert.ToDouble(pstrProportionStandard);
			}
			catch{}	// not mandatory, so we will the default value 0.95 for other processing
			
			string strCCN = string.Empty;
			string strMonth = pstrMonth;
			string strYear = pstrYear;
			string strParty = string.Empty;
			string strRevision1 = nRevision_1 < 0 ? pstrPORevision_1 : nRevision_1.ToString();
			string strRevision2 = pstrPORevision_2;
						
			float fActualPageSize = 9000.0f;			

			/// contain array of string: Name of the column (with days have data in the dtbSourceData)
			/// FOr Example:
			/// dtbSourceData contain: 01-Oct: has Plan Quantity
			/// 02-Oct has Actual Quantity
			/// So arrHasValueDateHeading contain: Plan01, Actual02
			ArrayList arrHasValueDateHeading = new ArrayList();	

			/// Build to keep value of pair: 01 --> 01-Mon, ... depend on the real data of dtbResule
			NameValueCollection arrDayNumberMapToDayWithDayOfWeek = new NameValueCollection();

			/// Keep count of PlanPass and PlanFail for all days (columns).
			Hashtable arrColumnPass = new Hashtable();
			Hashtable arrColumnFail = new Hashtable();

			/// Keep count of PlanPass and PlanFail for all ITEMS  (rows).
			Hashtable arrRowPass = new Hashtable();
			Hashtable arrRowFail = new Hashtable();


			// get data and cache all in the dstPLPP
			dstPLPP.DataSetName = pstrCCNID + pstrYear + pstrMonth + pstrPartyID + pstrPORevision_1 + pstrPORevision_2 + pstrProportionStandard;			
			GetDataAndCache(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, 
				strRevision1, strRevision2, pstrProportionStandard);		
			
			System.Data.DataTable dtbPlanTable;
			dtbPlanTable  = dstPLPP.Tables[PLAN_TABLE_NAME_2];		
			
			System.Data.DataTable dtbActualTable;
			dtbActualTable = dstPLPP.Tables[ACTUAL_TABLE_NAME];

			System.Data.DataTable dtbPlanTableWO1;
			System.Data.DataTable dtbAdjTable;
			dtbPlanTableWO1  = dstPLPP.Tables[PLAN_TABLE_NAME_1];
			dtbAdjTable = BuildAdjTable(dtbPlanTableWO1 , dtbPlanTable);
			dtbAdjTable.TableName = ADJ_TABLE_NAME;
			dstPLPP.Tables.Add(dtbAdjTable);
			
			System.Data.DataTable dtbReturnTable;
			dtbReturnTable = dstPLPP.Tables[RETURN_TO_VENDOR_TABLE_NAME];

			#endregion  My Variables

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strCCN = boUtil.GetCCNCodeFromID(nCCNID);	
			strParty = objBO.GetVendorCodeFromID(nPartyID) + ": " + objBO.GetVendorNameFromID(nPartyID);

		
			
			#endregion	
			
			/// transform TABLE column names
			/// transform TABLE will contain :
			/// PRODUCTID, CATEGORY,PARTNO,MODEL,
			/// BEGIN
			/// PLAN+i.ToString("00")
			/// ADJ +i.ToString("00")
			/// ACTUAL+i.ToString("00")
			/// RETURN+i.ToString("00")
			#region TRANSFORM ORIGINAL TABLE FOR REPORT
	
			#region GETTING THE DATE HEADING
			/// arrPlanDate and arrActualDate contain DateTime object from actual dtbSourceData
			ArrayList arrPlanDate = GetColumnValuesFromTable(dtbPlanTable,PLAN+DATE);			
			ArrayList arrActualDate = GetColumnValuesFromTable(dtbActualTable,ACTUAL+DATE);
			ArrayList arrReturnDate = GetColumnValuesFromTable(dtbReturnTable,RETURN+DATE);			
			ArrayList arrAdjDate = GetColumnValuesFromTable(dtbAdjTable,ADJ+DATE);

			ArrayList arrItems = GetCategory_PartNo_Model_ProductID_FromTable(dtbPlanTable,CATEGORY,PARTNO,MODEL,PRODUCTID);

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
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
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
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}
			foreach(object obj in arrReturnDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = RETURN + dtm.Day.ToString("00");
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
					arrHasValueDateHeading.Add(strColumnName);
				}
				catch{}
			}

			foreach(object obj in arrAdjDate)
			{
				try
				{
					int nDay = (int)obj;
					DateTime dtm = new DateTime(nYear,nMonth,nDay);
					string strColumnName = ADJ + dtm.Day.ToString("00");
					arrDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
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
            
			#region BUILD TRANSFORM TABLE SCHEMA		
			DataTable dtbTransform = BuildTransformTable(arrHasValueDateHeading);
			#endregion
			
			#region FILL ABSOLUTE DATA FROM Plan && Actual && Adjust && Return to the TRANSFORM DATATABLE
			
			/// GUIDE: with each Items
			foreach(string strItem in arrItems)
			{
				// Create DUMMYROW FIRST
				DataRow dtrNew = dtbTransform.NewRow();
						
				#region	- fill ITEM info and plan quantity to the new dummy row				
				
				/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilter = string.Empty;
				if(strItem.Split('#')[0] == string.Empty)
				{
					strFilter = 
						string.Format("[{0}] is null AND [{1}]='{2}' AND [{3}]='{4}'",
						CATEGORY,							
						PARTNO,
						strItem.Split('#')[1],
						MODEL,
						strItem.Split('#')[2]
						);
				}
				else
				{
					strFilter = 
						string.Format("[{0}]='{1}' AND [{2}]='{3}' AND [{4}]='{5}'",
						CATEGORY,
						strItem.Split('#')[0],
						PARTNO,
						strItem.Split('#')[1],
						MODEL,
						strItem.Split('#')[2]
						);
				}

				string strSort = string.Format("[{0}] ASC,[{1}] ASC,[{2}] ASC ",CATEGORY,PARTNO,MODEL);
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrows = dtbPlanTable.Select(strFilter,strSort);

				/// GUIDE: for each rows in of this Item OF DTBSourceData - fill plan quantity and some meta info about ITEM
				foreach(DataRow dtr in dtrows)
				{
					// fill data to the dummy row
					dtrNew[PRODUCTID] = dtrows[0][PRODUCTID];
					dtrNew[CATEGORY] = dtrows[0][CATEGORY];
					dtrNew[PARTNO] = dtrows[0][PARTNO];
					dtrNew[PARTNAME] = dtrows[0][PARTNAME];
					dtrNew[MODEL] = dtrows[0][MODEL];
					dtrNew[BEGIN] = dtrows[0][BEGIN];

					/// Fill Plan Quantity to destination column of Transform table, in this new rows
					//string strDateColumnToFill = PLAN + ((DateTime)dtr[PLAN+DATE]).Day.ToString("00");
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[PLAN+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[PLAN+QUANTITY];				
				}

				#endregion - fill ITEM info and plan quantity to the new dummy row
				
				
				#region - fill actual quantity to the new dummy row

				/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilterActualCompletion = string.Empty;
				strFilterActualCompletion = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,
					strItem.Split('#')[3]
					);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsActualCompletion = dtbActualTable.Select(strFilterActualCompletion);

				/// GUIDE: for each rows  of this Item in Actual Completion DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsActualCompletion)
				{
					/// Fill Actual Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = ACTUAL + ((DateTime)dtr[ACTUAL+DATE]).Day.ToString("00");
					string strDateColumnToFill = ACTUAL + Convert.ToInt32( dtr[ACTUAL+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[ACTUAL+QUANTITY];
				}
				#endregion - fill actual  quantity to the new dummy row

			
				#region - fill adjust quantity to the new dummy row
				
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilterAdjust = string.Empty;
				strFilterAdjust = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,
					strItem.Split('#')[3]
					);		
				
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
				#endregion - fill adjust quantity to the new dummy row
	

				#region - fill return quantity to the new dummy row

				/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilterReturn = string.Empty;
				strFilterReturn = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,
					strItem.Split('#')[3]
					);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsReturn = dtbReturnTable.Select(strFilterReturn);

				/// GUIDE: for each rows  of this Item in Return DataTable- fill return quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsReturn)
				{
					/// Fill Return Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = RETURN + ((DateTime)dtr[RETURN+DATE]).Day.ToString("00");
					string strDateColumnToFill = RETURN + Convert.ToInt32( dtr[RETURN+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[RETURN+QUANTITY];
				}
				#endregion - fill Return quantity to the new dummy row


				// add to the transform data table
				dtbTransform.Rows.Add(dtrNew);				
			}	    
			#endregion FILL DATA FROM Plan DTB && ActualCompletion DTB && Adjust DTB to the TRANSFORM DATATABLE

			#endregion  TRANSFORM ORIGINAL TABLE FOR REPORT

			#region calculate the Sum of Plan, sum of Actual, sum of Progress (on top of the report) to generate a chart in EXCEL			
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

			#region calculate the ProgressDay column

			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth); i++)
			{
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);				
				foreach(DataRow rowItem in dtbTransform.Rows)
				{
					decimal decPlan = ReportBuilder.ToDecimal(rowItem[PLAN+strCounter]);
					decimal decActual = ReportBuilder.ToDecimal(rowItem[ACTUAL+strCounter]);
					decimal decReturn = ReportBuilder.ToDecimal(rowItem[RETURN+strCounter]);					

					rowItem[PROGRESSDAY+strCounter] = decActual - decPlan - decReturn;
				}			
			}	

			#endregion calculate the ProgressDay column

			#region - calculate , fill Progress quantity to the new dummy row

			for(int i = 1 ; i <= 31 /*DateTime.DaysInMonth(nYear,nMonth)*/  ; i++)
			{
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);
                
				foreach(string strItem in arrItems)
				{                    			
					/// strItem.Split('#')[3] ==  PRODUCTID					
					string strFilterProgress = 
						string.Format("[{0}]='{1}' ",
						PRODUCTID,
						strItem.Split('#')[3]
						);
				
					/// GUIDE: get rows ( in fact, it is only one) of this Item from the dtbTransform
					DataRow[] dtrowsItemAllInfo = dtbTransform.Select(strFilterProgress);
			
					decimal decCurrentACTUAL = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][ACTUAL+ i.ToString("00")] );
					decimal decCurrentPLAN = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][PLAN+ i.ToString("00")]) ;
					decimal decCurrentRETURN = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][RETURN+ i.ToString("00")]) ;
					
					decimal decPreviousPROGRESS = decimal.Zero;
					if(i == 1)
						decPreviousPROGRESS = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][BEGIN] );
					else					
						decPreviousPROGRESS = ReportBuilder.ToDecimal( dtrowsItemAllInfo[0][PROGRESS + (i-1).ToString("00")]  ) /*Previous*/ ;					

					dtrowsItemAllInfo[0][PROGRESS + i.ToString("00")] = 
						decPreviousPROGRESS
						+decCurrentACTUAL
						-decCurrentPLAN
						-decCurrentRETURN;
					
				}	// end each Items (of current day  = i)
			}		// end foreach Day(i)
			
			#endregion - calculate , fill Progress quantity to the new dummy row			
			
			int intTotalCountPass = 0;			int intTotalCountFail = 0;						
			#region : ASSESS the PROGRESS, fill ASSESSMENT and CALCULATE the count of FAIL and PASS
			for(int i = 1 ; i <= DateTime.DaysInMonth(nYear,nMonth); i++)
			{
				int intColumnPass = 0;			int intColumnFail = 0;
				string strCounter = i.ToString(ReportBuilder.FORMAT_DAY_2CHAR);		
				
				foreach(DataRow rowItem in dtbTransform.Rows)
				{					
					decimal decPlan = ReportBuilder.ToDecimal(rowItem[PLAN+strCounter]);
					decimal decActual = ReportBuilder.ToDecimal(rowItem[ACTUAL+strCounter]);
					decimal decReturn = ReportBuilder.ToDecimal(rowItem[RETURN+strCounter]);
					decimal decProgressDay = decActual - decPlan - decReturn;
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
				arrColumnFail.Add(FLD + PLANFAIL  + strCounter, intColumnFail );
			}	// end foreach i			

			#endregion calculate the count of Plan FAIL and Plan PASS			
			
			#region CALCULATE the Percent for each Row
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

			#region  SHORT CIRCURT this function			
						if((intTotalCountPass + intTotalCountFail) == 0 )
						{
							mResult = "-";
						}
						else
						{
							decimal decTemp  = ( (decimal)intTotalCountPass * 100) / (intTotalCountPass + intTotalCountFail) ;
							mResult = decTemp.ToString("#,##0.00");
						}
						return new DataTable();
			#endregion SHORT CIRCURT this function
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
		/// build a new datatable with column = productid, category,partno,model,begin,
		/// and somecolumn with names in arrHasValueDateHeading
		/// Index column is : Plan, Adj, Actual, Return, ProgressDay, Progress Accumulate, Assessment
		/// </summary>
		/// <remarks>		
		/// </remarks>
		/// <returns>DataTable</returns>
		private DataTable BuildTransformTable(ArrayList parrHasValueDateHeading)
		{
			DataTable dtbRet = new DataTable(REPORT_NAME);
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
						// TEST:						dtbRet.Columns[PLAN + i.ToString("00")].DefaultValue = 0d;
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

				if(parrHasValueDateHeading.Contains(RETURN + i.ToString("00")) == false )
				{		
					try
					{
						dtbRet.Columns.Add(RETURN + i.ToString("00"),typeof(System.String));
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
		}

		
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
		/// Get the datatable : contain Mapping ProductID with it adjustment from rev1 to rev2, in a specific days in month)
		/// Return table will have schema like:
		/// ProductID - AdjDay - AdjQuantity
		/// 200	1	110.00000
		/// 127	13	53.00000
		/// 127	31	50.00000
		/// 
		/// 				
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrPurchaseOrderMasterID_1"></param>
		/// <param name="pstrPurchaseOrderMasterID_2"></param>		
		/// <returns></returns>
		private DataTable BuildAdjTable(DataTable pdtbPlanTableWO1, DataTable pdtbPlanTableWO2 )
		{	
			/// TABLE1 - TABLE2

			/// build schema for ADJ table
			const string ADJ_TABLE_NAME = "AdjTable";
			DataTable dtbRet = new DataTable(ADJ_TABLE_NAME);
			dtbRet.Columns.Add(PRODUCTID);
			dtbRet.Columns.Add(ADJ + DATE, typeof(Int32) );
			dtbRet.Columns.Add(ADJ + QUANTITY, typeof(Decimal) );			
			
			// HACKED: improved Speed
			//DataTable dtbPlanTableWO1 = BuildPlanTable(pstrCCNID,pstrYear,pstrMonth,pstrPartyID, pstrPurchaseOrderMasterID_1);
			DataTable dtbPlanTableWO1 = pdtbPlanTableWO1;
			/* DataTable dtbPlanTableWO2 = BuildPlanTable(pstrCCNID,pstrYear,pstrMonth,pstrPartyID, pstrPurchaseOrderMasterID_2); */
			DataTable dtbPlanTableWO2 = pdtbPlanTableWO2;

			
			/// FOREACH iROW IN TABLE2, 
			/// dtbRet newROW = iROW
			/// if found relative-newROW    row in Table1 (productid is the same, planday is the same), 
			/// ---- subtract PlanQuantity of current row (newROW).
			/// ---- Add found row in Table1 in the UsedRowedInTableWO1
			/// add newROW to dtbRET
			/// 
			/// Table1.Remove(UsedRowedInTableWO1);
			/// 
			/// FOREACH iROW1 in TABLE1
			/// add to the dtbRet

			/// REVIEW: need to review: sure that PlanTable is have unique key with PID - PlanDay
			
			if(pdtbPlanTableWO1.Rows.Count <= 0)
			{
				return dtbRet;
			}

			int nPID = int.MinValue;
			int nDay = int.MinValue;
			ArrayList arrUsedRowInPlanTableWO1 = new ArrayList();
			foreach(DataRow iRow in dtbPlanTableWO2.Rows)
			{
				DataRow newRow = dtbRet.NewRow();
				nPID = System.Convert.ToInt32(iRow[PRODUCTID]);				
				nDay = System.Convert.ToInt32(iRow[PLAN + DATE]);
				decimal dblAQ = Convert.ToDecimal( iRow[PLAN + QUANTITY]  );

				decimal dblRelativeFromTable1 = 0;
				foreach( DataRow jRow in dtbPlanTableWO1.Select("["+PRODUCTID+"]=" +nPID+ " and [" +PLAN+DATE+ "]=" +nDay)   )
				{
					dblRelativeFromTable1 += Convert.ToDecimal( jRow[PLAN + QUANTITY]) ;
					arrUsedRowInPlanTableWO1.Add(jRow);	/// mark that we used this rows. We don't include its value later.
				}

				newRow[PRODUCTID] = nPID;
				newRow[ADJ + DATE] = nDay;
				newRow[ADJ + QUANTITY] = dblAQ - dblRelativeFromTable1;
				dtbRet.Rows.Add(newRow);
			}

			foreach(DataRow jRow in arrUsedRowInPlanTableWO1)			
				dtbPlanTableWO1.Rows.Remove(jRow);
			
            
			foreach(DataRow iRow in dtbPlanTableWO1.Rows)
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
		/// Get all data for this report and cache in the dstPLPP dataset
		/// just improve the speed for this report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrPurchaseOrderMasterID_1"></param>
		/// <param name="pstrPurchaseOrderMasterID_2"></param>
		/// <param name="pstrProportionStandard"></param>
		private void GetDataAndCache(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrPORevision_1, string pstrPORevision_2, string pstrProportionStandard)
		{	
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

				" Declare @pstrPartyID int " + 
				" Declare @pstrPORevision_1 int " + 			
				" Declare @pstrPORevision_2 int " + 			
				"  " + 				
				" /*-----------------------------------*/ " + 
				"  " + 
				" Set @pstrCCNID = " + pstrCCNID + " " + 
				" Set @pstrYear = '" + pstrYear + "' " + 
				" Set @pstrMonth = '"+ pstrMonth +"' " + 
				" Set @pstrPreviousYear = '" + pstrPreviousYear + "' " + 
				" Set @pstrPreviousMonth = '"+ pstrPreviousMonth +"' " + 

				" Set @pstrPartyID = " +pstrPartyID+ " " + 
				(pstrPORevision_1.Trim() == string.Empty ?  (string.Empty) : (" Set @pstrPORevision_1 = " +pstrPORevision_1 + " ")  )  + 			
				" Set @pstrPORevision_2 = " +pstrPORevision_2 + " "  + 			
				"  " + 					
				"  " ;
			/*-----------------------------------*/


			#endregion MAIN QUERY


			#region PLANTABLE - 2 - MAIN

			/// Get the datatable : contain Mapping ProductID with it Plan from WO2, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - PlanDay - PlanQuantity
			/// 200	1	110.00000
			/// 200	2	10.00000
			/// 200	3	10.00000
			/// 200	4	40.00000
			/// 200	5	15.00000
			/// 127	7	50.00000
			/// 127	8	47.50000
			/// 127	9	52.50000
			/// 127	12	46.50000
			/// 127	13	53.00000
			/// 127	31	50.00000		
			/// 
			/// //private DataTable BuildPlanTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrPurchaseOrderMasterID)
			string strSqlPLANTABLE =				 

				

				/*END  ******************** PLAN  , IS PO_PurchaseOrder ***************************************************/
				" SELECT  " + 
				" ITM_Product.ProductID as [ProductID], " + 
				" ITM_Category.Code as [Category],  " + 
				" ITM_Product.Code as [Part No.],  " + 
				" ITM_Product.Description as [PartName],  " + 
				" ITM_Product.Revision  as [Model], " + 
				" IsNull(PROGRESSBEGINQUANTITYTABLE.ProgressBeginQuantity,0.00) as [ProgressBeginQuantity], " + 
				" PLANTABLE.PlanDay, " + 
				" PLANTABLE.PlanQuantity " + 
				"  " + 
				" FROM " + 
				" (		 " + 
				" 	SELECT  " + 
				" 	INNERTABLE.ProductID, " + 
				" 	INNERTABLE.PlanDay, " + 
				" 	CASE " + 
				" 		WHEN RQty < OQty THEN RQty " + 
				" 		ELSE OQty " + 
				" 	END as [PlanQuantity] " + 
				" 	 " + 
				" 	FROM  " + 
				" 	( " + 
				" 		SELECT  " + 
				" 		UNIONTABLE.ProductID, " + 
				" 		UNIONTABLE.PlanDay, " + 
				" 		Sum(UNIONTABLE.OQty) as [OQty], " + 
				" 		Sum(UNIONTABLE.RQty) as [RQty] " + 
				" 		FROM " + 
				" 		( /* Inner table: union of OrderQuantityTable and ReceivedQuantityTable */ " + 
				" 			(	 " + 
				" 				select  " + 
				" 				PODETAIL.ProductID as [ProductID], " + 
				" 				DatePart(dd, PODELI.ScheduleDate) as [PlanDay], " + 
				" 				SUM(PODELI.DeliveryQuantity) as [OQty], " + 
				" 				null as [RQty] " + 
				" 				 " + 
				" 				from  " + 
				" 				PO_PurchaseOrderMaster as POMASTER " + 
				" 				join PO_PurchaseOrderDetail as PODETAIL " + 
				" 				on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
				" 				and POMASTER.PORevision = @pstrPORevision_2  " + 
				" 				and POMASTER.CCNID = @pstrCCNID " + 
				" 				and POMASTER.PartyID = @pstrPartyID " + 
				" 				and PODETAIL.ApproverID is not null /* APPROVED */ " + 
				" 			 " + 
				" 				join PO_DeliverySchedule as PODELI " + 
				" 				on PODETAIL.PurchaseOrderDetailID = PODELI.PurchaseOrderDetailID " + 
				" 				and DatePart(mm   , PODELI.ScheduleDate) = @pstrMonth " + 
				" 				and DatePart(yyyy , PODELI.ScheduleDate) = @pstrYear " + 
				" 				 " + 
				" 				group by  " + 
				" 				PODETAIL.ProductID, " + 
				" 				DatePart(dd, PODELI.ScheduleDate) " + 
				" 			)  " + 
				" 		 " + 
				" 			UNION /* getting the ReceivedQuantity */  " + 
				" 			( " + 
				" 				select  " + 
				" 				PODETAIL.ProductID as [ProductID], " + 
				" 				DatePart(dd, PODELI.ScheduleDate) as [PlanDay], " + 
				" 				null as [OQty], " + 
				" 				Sum(PODELI.ReceivedQuantity) as [RQty] " + 
				" 				 " + 
				" 				from " + 
				" 				PO_PurchaseOrderMaster as POMASTER " + 
				" 				join PO_PurchaseOrderDetail as PODETAIL " + 
				" 				on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
				" 				and POMASTER.PORevision = @pstrPORevision_2  " + 
				" 				and POMASTER.CCNID = @pstrCCNID " + 
				" 				and POMASTER.PartyID = @pstrPartyID " + 
				" 				and PODETAIL.ApproverID is not null /* APPROVED */ " + 
				" 				and PODETAIL.Closed = 1 /* CLOSED */ " + 
				" 				 " + 
				" 				join PO_DeliverySchedule as PODELI " + 
				" 				on PODETAIL.PurchaseOrderDetailID = PODELI.PurchaseOrderDetailID " + 
				" 				and DatePart(mm   , PODELI.ScheduleDate) = @pstrMonth " + 
				" 				and DatePart(yyyy , PODELI.ScheduleDate) = @pstrYear " + 
				" 				and PODELI.ReceivedQuantity is not null /* RECEIVED */ " + 
				" 				 " + 
				" 				group by  " + 
				" 				PODETAIL.ProductID, " + 
				" 				DatePart(dd, PODELI.ScheduleDate) " + 
				" 			)			 " + 
				" 		 " + 
				" 		) as UNIONTABLE /*END: Inner table: union of OrderQuantityTable and ReceivedQuantityTable */ " + 
				" 		group by ProductID,  " + 
				" 				PlanDay " + 
				" 	) as INNERTABLE " + 
				"  " + 
				" ) as PLANTABLE " + 
				"  " + 
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
				" 	(ACTUALTABLE.ActualQuantity - PLANTABLE.PlanQuantity )    as ProgressBeginQuantity  " + 
				" 	 " + 
				" 	FROM " + 
				" 	(							 " + 
				" 		SELECT  " + 
				" 		INNERTABLE.ProductID, " + 
				" 		 " + 
				" 		CASE " + 
				" 			WHEN RQty < OQty THEN RQty " + 
				" 			ELSE OQty " + 
				" 		END as [PlanQuantity] " + 
				" 		 " + 
				" 		FROM  " + 
				" 		( " + 
				" 			SELECT  " + 
				" 			UNIONTABLE.ProductID, " + 
				" 			 " + 
				" 			Sum(UNIONTABLE.OQty) as [OQty], " + 
				" 			Sum(UNIONTABLE.RQty) as [RQty] " + 
				" 			FROM " + 
				" 			( /* Inner table: union of OrderQuantityTable and ReceivedQuantityTable */ " + 
				" 				(	 " + 
				" 					select  " + 
				" 					PODETAIL.ProductID as [ProductID], " + 
				" 					DatePart(dd, PODELI.ScheduleDate) as [PlanDay], " + 
				" 					SUM(PODELI.DeliveryQuantity) as [OQty], " + 
				" 					null as [RQty] " + 
				" 					 " + 
				" 					from  " + 
				" 					PO_PurchaseOrderMaster as POMASTER " + 
				" 					join PO_PurchaseOrderDetail as PODETAIL " + 
				" 					on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
				" 					and POMASTER.PORevision = @pstrPORevision_2  " + 
				" 					and POMASTER.CCNID = @pstrCCNID " + 
				" 					and POMASTER.PartyID = @pstrPartyID " + 
				" 					and PODETAIL.ApproverID is not null /* APPROVED */ " + 
				" 				 " + 
				" 					join PO_DeliverySchedule as PODELI " + 
				" 					on PODETAIL.PurchaseOrderDetailID = PODELI.PurchaseOrderDetailID " + 
				" 					and DatePart(mm   , PODELI.ScheduleDate) = @pstrPreviousMonth " + 
				" 					and DatePart(yyyy , PODELI.ScheduleDate) = @pstrPreviousYear " + 
				" 					 " + 
				" 					group by  " + 
				" 					PODETAIL.ProductID, " + 
				" 					DatePart(dd, PODELI.ScheduleDate) " + 
				" 				)  " + 
				" 			 " + 
				" 				UNION /* getting the ReceivedQuantity */  " + 
				" 				( " + 
				" 					select  " + 
				" 					PODETAIL.ProductID as [ProductID], " + 
				" 					DatePart(dd, PODELI.ScheduleDate) as [PlanDay], " + 
				" 					null as [OQty], " + 
				" 					Sum(PODELI.ReceivedQuantity) as [RQty] " + 
				" 					 " + 
				" 					from " + 
				" 					PO_PurchaseOrderMaster as POMASTER " + 
				" 					join PO_PurchaseOrderDetail as PODETAIL " + 
				" 					on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
				" 					and POMASTER.PORevision = @pstrPORevision_2  " + 
				" 					and POMASTER.CCNID = @pstrCCNID " + 
				" 					and POMASTER.PartyID = @pstrPartyID " + 
				" 					and PODETAIL.ApproverID is not null /* APPROVED */ " + 
				" 					and PODETAIL.Closed = 1 /* CLOSED */ " + 
				" 					 " + 
				" 					join PO_DeliverySchedule as PODELI " + 
				" 					on PODETAIL.PurchaseOrderDetailID = PODELI.PurchaseOrderDetailID " + 
				" 					and DatePart(mm   , PODELI.ScheduleDate) = @pstrPreviousMonth " + 
				" 					and DatePart(yyyy , PODELI.ScheduleDate) = @pstrPreviousYear " + 
				" 					and PODELI.ReceivedQuantity is not null /* RECEIVED */ " + 
				" 					 " + 
				" 					group by  " + 
				" 					PODETAIL.ProductID, " + 
				" 					DatePart(dd, PODELI.ScheduleDate) " + 
				" 				)			 " + 
				" 			 " + 
				" 			) as UNIONTABLE /*END: Inner table: union of OrderQuantityTable and ReceivedQuantityTable */ " + 
				" 			group by ProductID  " + 
				" 					 " + 
				" 		) as INNERTABLE " + 
				" 		 " + 
				" 	) as PLANTABLE " + 
				" 	 " + 
				" 	LEFT join  " + 
				" 	( " + 
				" 		/*BEGIN  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
				" 		select  " + 
				" 		POREDETAIL.ProductID,  " + 
				" 		Sum(IsNull(POREDETAIL.ReceiveQuantity, 0.00)) as [ActualQuantity] " + 
				" 		 " + 
				" 		from PO_PurchaseOrderReceiptMaster as POREMASTER " + 
				" 		join PO_PurchaseOrderReceiptDEtail as POREDETAIL " + 
				" 		on POREMASTER.PurchaseOrderReceiptID = POREDETAIL.PurchaseOrderReceiptID " + 
				" 		and DATEPART(mm  , POREMASTER.PostDate) = @pstrPreviousMonth " + 
				" 		and DATEPART(yyyy, POREMASTER.PostDate) = @pstrPreviousYear " + 
				" 		and POREMASTER.CCNID = @pstrCCNID " + 
				" 		 " + 
				" 		join PO_PurchaseOrderMaster as POMASTER " + 
				" 		on POREDETAIL.PurchaseOrderMasterID = POMASTER.PurchaseOrderMasterID " + 
				" 		and POMASTER.PartyID = @pstrPartyID " + 
				" 		and POMASTER.PORevision = @pstrPORevision_2 " + 
				" 		 " + 
				" 		Group by  " + 
				" 		POREDETAIL.ProductID		 " + 
				" 		/*END  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/		 " + 
				" 	) as ACTUALTABLE " + 
				" 	 " + 
				" 	on PLANTABLE.ProductID = ACTUALTABLE.ProductID	 " + 
				" ) as PROGRESSBEGINQUANTITYTABLE " + 
				"  " + 
				" on PLANTABLE.ProductID = PROGRESSBEGINQUANTITYTABLE.ProductID " + 
				/*END  ******************** PLAN  , IS PO_PurchaseOrder ***************************************************/


				"  " 	
				;

			#endregion PLANTABLE - MAIN
			/* ============================================================== */

			#region PLANTABLE - 1 - NOT MANDATORY			
			/// REV1 = SUm all order quantity of all POLine (of all PO of Provided Rev1). 			
			/// Get the datatable : contain Mapping ProductID with it Plan from Rev1, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - PlanDay - PlanQuantity
			/// 200	1	110.00000
			/// 200	2	10.00000			
			/// 127	7	50.00000
			/// 127	8	47.50000						
			string strSqlPLANTABLE_1 =			
			
				/*START  ******************** PLAN  OF Revision 1, get Sum of Order Quantity of all POLine, ***************************************************/
				" SELECT   " + 
				" PODETAIL.ProductID as [ProductID], " + 
				" DatePart(dd, PODELI.ScheduleDate) as [PlanDay], " + 
				" SUM(PODELI.DeliveryQuantity) as [PlanQuantity] " + 
				"  " + 
				" from " + 
				" PO_PurchaseOrderMaster as POMASTER " + 
				" join PO_PurchaseOrderDetail as PODETAIL " + 
				" on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
				" and POMASTER.PORevision = @pstrPORevision_1  " + 
				" and POMASTER.CCNID = @pstrCCNID " + 
				" and POMASTER.PartyID = @pstrPartyID " + 
				"  " + 
				" join PO_DeliverySchedule as PODELI " + 
				" on PODETAIL.PurchaseOrderDetailID = PODELI.PurchaseOrderDetailID " + 
				" and DatePart(mm   , PODELI.ScheduleDate) = @pstrMonth " + 
				" and DatePart(yyyy , PODELI.ScheduleDate) = @pstrYear " + 
				"  " + 
				" group by  " + 
				" PODETAIL.ProductID, " + 
				" DatePart(dd, PODELI.ScheduleDate) "
				/*END  ******************** PLAN  OF Revision 1, get Sum of Order Quantity of all POLine, ***************************************************/
				;

			#endregion PLANTABLE -1 NOT MANDATORY
			/* ============================================================== */

			#region ACTUALTABLE
			/// Get the datatable : contain Mapping ProductID with it Actual  PO RECEIPT quantity, in a specific days in month)
			/// Return table will have schema like:
			/// ProductID - ActualDay - ActualQuantity
			/// 200	1	110.00000
			/// 200	2	10.00000
			/// 200	3	10.00000
			/// 127	12	46.50000
			/// 127	13	53.00000
			/// 127	31	50.00000		
			/// </summary>
			/// <returns></returns>
			// private DataTable BuildActualTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrPurchaseOrderMasterID_2)

			string strSqlACTUALTABLE =  
				" /*BEGIN  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
				" select  " + 
				" POREDETAIL.ProductID,  " + 
				" DatePart(dd, POREMASTER.PostDate) as [ActualDay], " + 
				" Sum(IsNull(POREDETAIL.ReceiveQuantity, 0.00) ) as [ActualQuantity] " + 
				"  " + 
				" from PO_PurchaseOrderReceiptMaster as POREMASTER " + 
				" join PO_PurchaseOrderReceiptDEtail as POREDETAIL " + 
				" on POREMASTER.PurchaseOrderReceiptID = POREDETAIL.PurchaseOrderReceiptID " + 
				" and DATEPART(mm  , POREMASTER.PostDate) = @pstrMonth " + 
				" and DATEPART(yyyy, POREMASTER.PostDate) = @pstrYear " + 
				" and POREMASTER.CCNID = @pstrCCNID " +  
				"  " + 
				" join PO_PurchaseOrderMaster as POMASTER " + 
				" on POREDETAIL.PurchaseOrderMasterID = POMASTER.PurchaseOrderMasterID " + 
				" and POMASTER.PartyID = @pstrPartyID " + 
				" and POMASTER.PORevision = @pstrPORevision_2 " + 
				"  " + 
				" Group by  " + 
				" POREDETAIL.ProductID, " + 
				" DatePart(dd, POREMASTER.PostDate) " + 
				" /*END  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
				"  " 				;


			#endregion ACTUALTABLE

			/* ============================================================== */
			

			#region RETURN TO VENDOR TABLE

			string strSqlRETURNTOVENDORTABLE = 

                    
				/*BEGIN ********************RETURN TO VENDOR ***************************************************/

				" select  " + 
				" RETURNDETAIL.ProductID, " + 
				" DatePart(dd, RETURNMASTER.PostDate) as [ReturnDay], " + 
				" Sum(RETURNDETAIL.Quantity) as [ReturnQuantity] " + 
				"  " + 
				" from PO_ReturnToVendorMaster as RETURNMASTER " + 
				" join PO_ReturnToVendorDetail as RETURNDETAIL " + 
				" on RETURNMASTER.ReturnToVendorMasterID = RETURNDETAIL.ReturnToVendorMasterID  " + 
				" and DATEPART(mm  , RETURNMASTER.PostDate) = @pstrMonth " + 
				" and DATEPART(yyyy, RETURNMASTER.PostDate) = @pstrYear " + 
				" and RETURNMASTER.CCNID = @pstrCCNID " + 
				" and RETURNMASTER.PartyID = @pstrPartyID " + 
				"  " + 
				" join PO_PurchaseOrderMaster as POMASTER " + 
				" on RETURNMASTER.PurchaseOrderMasterID = POMASTER.PurchaseOrderMasterID " + 
				" and POMASTER.PORevision = @pstrPORevision_2 " +  
				"  " + 
				" Group by " + 
				" RETURNDETAIL.ProductID, " + 
				" DatePart(dd, RETURNMASTER.PostDate) " + 
				/*END **********************RETURN TO VENDOR ***************************************************/

				"  " ;

			#endregion RETURN TO VENDOR TABLE	
			

			#region ADJTABLE



			#endregion ADJTABLE	
			


			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql += strSqlPLANTABLE + "\n" +  
					strSqlPLANTABLE_1 + "\n" + 
					strSqlACTUALTABLE + "\n" +
					strSqlRETURNTOVENDORTABLE + "\n"
					;
	

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPLPP);

				dstPLPP.Tables[0].TableName = PLAN_TABLE_NAME_2;
				dstPLPP.Tables[1].TableName = PLAN_TABLE_NAME_1;
				dstPLPP.Tables[2].TableName = ACTUAL_TABLE_NAME;
				dstPLPP.Tables[3].TableName = RETURN_TO_VENDOR_TABLE_NAME;
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
		/// This function return the previous PO Revision of provided Revision .
		/// If on Error, or there is no previous, it will return a negative value.
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrPreviousPORevision"></param>
		/// <returns></returns>
		private int GetPreviousPORevision(string pstrCCNID, string pstrYear, string pstrMonth, 
			string pstrPartyID, string pstrCurrentPORevision)
		{
			const int NO_REV = -1;
			int intRet = NO_REV;
			
			#region DB QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 
				" Declare @pstrMonth char(2) " + 
				" Declare @pstrYear char(4) " + 
				" Declare @pstrPartyID int " + 
				" /*-----------------------------------*/ " + 
				" Set @pstrCCNID = " +pstrCCNID+ " " + 
				" Set @pstrMonth = '" +pstrMonth+ "' " + 
				" Set @pstrYear = '" +pstrYear+ "' " + 				
				" Set @pstrPartyID = '" +pstrPartyID+ "' " + 
				" /*-----------------------------------*/ " + 
				"  " + 
			
				" select   " + 
				" IsNull( Max(PORevision) , -1) as MaxPORevision " + 
				" from    " + 
				" PO_PurchaseOrderMaster as POMASTER   				 " + 
				"  " + 
				" where POMASTER.CCNID = @pstrCCNID    " + 
				" and DatePart(mm   , POMASTER.OrderDate) = @pstrMonth    " + 
				" and DatePart(yyyy , POMASTER.OrderDate) = @pstrYear     " +  
				" and POMASTER.PartyID = @pstrPartyID " + 
				" and POMASTER.PORevision < " + pstrCurrentPORevision ;
			/*-----------------------------------*/

			try
			{
				intRet = Convert.ToInt32( ExecuteScalar(strSql) );
			}
			catch
			{}

			#endregion DB QUERY			

			return intRet;
		}	// end function




	}	// end class


	#endregion  CHILD REPORT
}

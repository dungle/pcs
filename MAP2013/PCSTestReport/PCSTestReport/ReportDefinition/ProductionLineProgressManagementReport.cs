using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Data.OleDb;
using PCSComUtils.Common;
using Utils = PCSComUtils.DataAccess.Utils;
using System.Collections.Specialized;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProductionLineProgressManagementReport
{
	[Serializable]
	public class ProductionLineProgressManagementReport : MarshalByRefObject, IDynamicReport
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
		public ProductionLineProgressManagementReport()
		{
		}

		#region variables

		const string THIS = "ExternalReportFile:ProductionLineProgressManagementReport";
		const string TABLE_NAME = "ProductionLineProgressManagementReport";	
		/// Report layout file constant
		const string REPORT_LAYOUT_FILE = "ProductionLineProgressManagementReport.xml";
		const string REPORT_NAME = "ProductionLineProgressManagementReport";
		short COPIES = 1;
		/// all parameter are Mandatory
		const string REPORTFLD_PARAMETER_CCN				= "fldParameterCCN";
		const string REPORTFLD_PARAMETER_MONTH				= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_YEAR				= "fldParameterYear";			
		const string REPORTFLD_PARAMETER_PRODUCTIONLINE		= "fldParameterProductionLine";							
		/// Result Data Table Column names, contain:
		/// PRODUCTID, CATEGORY,PARTNO,MODEL,BEGIN
		/// DATE
		/// PLAN+QUANTITY, ACTUAL+QUANTITY
		const string PRODUCTID = "ProductID";
		const string CATEGORY = "Category";
		const string PARTNO = "Part No.";
		const string MODEL = "Model";
		const string BEGIN = "BeginQuantity";
		const string DATE = "Day";
		const string QUANTITY = "Quantity";	// suffix for PLAN,ACTUAL column
		const string PREFIX_DAYINMONTH = "lblDayInMonth";
		const string PREFIX_DAYOFWEEK = "lblDayOfWeek";
		/// other constants			
		const string PLAN = "Plan";
		const string ACTUAL = "Actual";
		const string PROGRESS = "Progress";
		const string ASSESSMENT = "Assessment";
		const string FLD = "fld";		
		const string PLANFAIL = "PlanFailD";
		const string PLANPASS = "PlanPassD";
		/// chart fields
		const string REPORTFLD_CHART	= "fldChart";

		#endregion

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// Modify the REPORT VIEWER to display the report
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{
			#region My variables

			int nCCNID = int.Parse(pstrCCNID);
			int nMonth = int.Parse(pstrMonth);
			int nYear = int.Parse(pstrYear);			
			int nProductionLineID = int.Parse(pstrProductionLineID);		
			
			string strCCN = string.Empty;
			string strProductionLine = string.Empty;
			DateTime pdtmMonth = new DateTime(Convert.ToInt32(pstrYear), Convert.ToInt32(pstrMonth), 1);
			pdtmMonth = pdtmMonth.AddSeconds(-1);
			/// key field of table containing selected fields
			/// We use WorkCenter Code field to select exactly a row from the dtbSourceData
			/// Later, we will loop throught this array to build the report
			/// Build to keep value WorkCenter Info, to keep 3 first rows of the report, ... depend on the real data of dtbResule
			Hashtable arrWorkCenterInfo = new Hashtable();	
			
			/// contain array of string: Name of the column (with days have data in the dtbSourceData)
			/// FOr Example:
			/// dtbSourceData contain: 01-Oct: has Plan Quantity
			/// 02-Oct has Actual Quantity
			/// So arrHasValueDateHeading contain: Plan01, Actual02
			ArrayList arrHasValueDateHeading = new ArrayList();	

			/// Build to keep value of pair: 01 --> 01-Mon, ... depend on the real data of dtbResule
			NameValueCollection arrDayNumberMapToDayWithDayOfWeek = new NameValueCollection();

			/// Build to keep value of pair: 01 --> 01-Mon, ... NOT DEPEND on the real data. Always Full from 1 - 31 (or 30, 29, depend on Month)
			NameValueCollection arrFULLDayNumberMapToDayWithDayOfWeek = new NameValueCollection();


			/// Keep count of PlanFail for all days.
			Hashtable arrCountPlanFail = new Hashtable();
			/// Keep count of PlanPass for all days.
			Hashtable arrCountPlanPass = new Hashtable();

			System.Data.DataTable dtbSourceData;
			System.Data.DataTable dtbActualCompletionTable;

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			#endregion

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strCCN = boUtil.GetCCNCodeFromID(nCCNID);	
			strProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + "-" + objBO.GetProductLineNameFromID(nProductionLineID);
			
			#endregion
		
			#region BUILD THE DATA TABLE
			/// in the actual day, when we left join with ACTUALTABLE, ActualDay, ActualQuantity can be null. We must represent it by: That Item will be produced 0 at day 1 of month
			/// IsNull() 
			///
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
					" Declare @pstrCCNID int " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrProductionLineID int " + 
					" Declare @strFromDate smalldatetime " + 
					"  " + 
					" Declare @pstrInArray varchar(40) " + 
					" Declare @pstrOutArray varchar(40) " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					" Set @pstrCCNID = " + pstrCCNID + " " + 
					" Set @pstrYear = '" + pstrYear + "' " + 
					" Set @pstrMonth = '"+ pstrMonth +"' " + 
					" Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
					" Set @strFromDate = '"+pstrYear+"-"+pstrMonth+"-01' " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					"  " + 
					" SELECT  " + 
					" ITM_Product.ProductID as [ProductID], " + 
					" ITM_Category.Code as [Category],  " + 
					" ITM_Product.Code as [Part No.],  " + 
					" ITM_Product.Revision  as [Model], " + 
					" PLANTABLE.PlanDay, " + 
					" PLANTABLE.PlanQuantity " + 
 

					"  " + 
					" FROM " + 
					" (	 " + 
					" 	/*===============> new solution " + 
					" 	Get the PlanQuantity and the PlanDate from a view " + 
					" 	Sum all the Quantity in a day of DCP Planning " + 
					" 	*/ " + 
					" 	 " + 
					" 	select  " + 
					" 	ITM_Product.ProductID as [ProductID], " + 
					" 	DatePart(dd,PRO_DCPResultDetail.WorkingDate) as [PlanDay], " + 
					" 	/* MIN(PRO_DCPResultDetail.WorkingDate) as PlanDate, */ " + 
					" 	SUM(PRO_DCPResultDetail.Quantity) as [PlanQuantity] " + 
					" 	 " + 
					" 	from " + 
					" 	PRO_DCPResultDetail " + 
					" 	join PRO_DCPResultMaster " + 
					" 	on PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID " + 
					" 	and DatePart(mm   ,PRO_DCPResultDetail.WorkingDate) = @pstrMonth " + 
					" 	and DatePart(yyyy ,PRO_DCPResultDetail.WorkingDate) = @pstrYear " + 
					" 	 " + 
					" 	join ITM_Product " + 
					" 	on ITM_Product.ProductID = PRO_DCPResultMaster.ProductID " + 
					" 	and ITM_Product.CCNID = @pstrCCNID " + 
					" 	 " + 
					" 	join MST_WorkCenter " + 
					" 	on PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID  /*WorkCenterID of PRO_DCPResultMaster can be null. We still don't want null value here */ " + 
					" 	and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
					" 	and MST_WorkCenter.CCNID = @pstrCCNID " + 
					" 	 " + 
					" 	group by  " + 
					" 	ITM_Product.ProductID, " + 
					" 	DatePart(dd,PRO_DCPResultDetail.WorkingDate) " + 
					" ) as PLANTABLE " + 
					"  " +  
					"  " + 
					" /* Join to get Info data : Category Code, Product Code */ " + 
					" join ITM_Product " + 
					" on PLANTABLE.ProductID = ITM_Product.ProductID " + 
					" LEFT join ITM_Category " + 
					" on ITM_Product.CategoryID = ITM_Category.CategoryID ";

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbSourceData = dstPCS.Tables[TABLE_NAME].Copy();
				}
				else
				{
					dtbSourceData = new DataTable();
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
			#endregion			 
		
			#region BUILD THE ACTUAL COMPLETION DATATABLE - MAPPING PRODUCT WITH ACTUAL COMPLETION
			dtbActualCompletionTable = BuildActualTable(pstrCCNID,pstrYear,pstrMonth,pstrProductionLineID);
			#endregion BUILD THE ACTUAL COMPLETION DATATABLE - MAPPING PRODUCT WITH ACTUAL COMPLETION

			#region BEGIN TABLE
			DataTable dtbBegin = BuildBeginTable(pstrCCNID, pdtmMonth, pstrProductionLineID);
			#endregion

			#region TRANSFORM ORIGINAL TABLE FOR REPORT


			#region BUILD THE FULL DayWithDayOfWeek Pair	// full from 1 to 31 (or 30, 29 , depend on Month)
			DateTime dtmTemp = new DateTime(nYear,nMonth,1);
			for(int i = 0 ; i <DateTime.DaysInMonth(nYear,nMonth) ; i++)
			{
				DateTime dtm = dtmTemp.AddDays(i);
				arrFULLDayNumberMapToDayWithDayOfWeek.Add(dtm.Day.ToString("00"), dtm.Day.ToString("00")+"-"+dtm.DayOfWeek.ToString().Substring(0,3)  );
			}

			#endregion
	
			#region GETTING THE DATE HEADING
			/// arrPlanDate and arrActualDate contain DateTime object from actual dtbSourceData
			ArrayList arrPlanDate = GetColumnValuesFromTable(dtbSourceData,PLAN+DATE);
			ArrayList arrActualDate = GetColumnValuesFromTable(dtbActualCompletionTable,ACTUAL+DATE);

			ArrayList arrItems = GetCategory_PartNo_Model_ProductID_FromTable(dtbSourceData,CATEGORY,PARTNO,MODEL,PRODUCTID);

			/// PUSH: has-value (in the dtbSourceData) to the arrHaSValueDateHeading
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
			/// ENDHACKED: Thachnn: 20/12/2005
			/// after this snip of code. arrHasValueDateHeading will contain Actual01, Actual02 or Plan03 Plan04 ... depend on the DataTable
			/// Which day has value (Plan or Actual), the columnName will exist in the arrHasValueDateHeading
			/// and then, the Transform DataTable dtbTransform will has some columns named like string in arrHasValueDateHeading
			

			#endregion		

			#region BUILD TRANSFORM TABLE SCHEMA		
			DataTable dtbTransform = BuildTransformTable(arrHasValueDateHeading);
			#endregion
			
			#region FILL DATA FROM SOURCE DATATABLE AND ACTUALCOMPLETION DATATABLE TO THE TRANSFORM DATATABLE
			
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
				DataRow[] dtrows = dtbSourceData.Select(strFilter,strSort);

				/// GUIDE: for each rows in of this Item OF DTBSourceData - fill plan quantity and some meta info about ITEM
				foreach(DataRow dtr in dtrows)
				{
					// fill data to the dummy row
					dtrNew[PRODUCTID] = dtrows[0][PRODUCTID];
					dtrNew[CATEGORY] = dtrows[0][CATEGORY];
					dtrNew[PARTNO] = dtrows[0][PARTNO];
					dtrNew[MODEL] = dtrows[0][MODEL];
					
					/// Fill Plan Quantity to destination column of Transform table, in this new rows
					string strDateColumnToFill = PLAN + Convert.ToInt32( dtr[PLAN+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[PLAN+QUANTITY];				
				}

				#endregion - fill ITEM info and plan quantity to the new dummy row
				
				#region - fill actual completion quantity to the new dummy row

				/// if strItem.Split('#')[0] ==  string.empty, its mean Category value is null
				/// so we put IsNull in the filter string (to select from dtbResult);
				string strFilterActualCompletion = string.Empty;
				strFilterActualCompletion = 
					string.Format("[{0}]='{1}' ",
					PRODUCTID,
					strItem.Split('#')[3]
					);		
				
				/// GUIDE: get all rows of this Item from the dtbSourceData
				DataRow[] dtrowsActualCompletion = dtbActualCompletionTable.Select(strFilterActualCompletion);

				/// GUIDE: for each rows  of this Item in Actual Completion DataTable- fill actual quantity to the dummy ROW
				foreach(DataRow dtr in dtrowsActualCompletion)
				{
					/// Fill Actual Quantity to destination column of Transform table, in this new rows
					//strDateColumnToFill = ACTUAL + ((DateTime)dtr[ACTUAL+DATE]).Day.ToString("00");
					string strDateColumnToFill = ACTUAL + Convert.ToInt32( dtr[ACTUAL+DATE]).ToString("00");
					dtrNew[strDateColumnToFill] = dtr[ACTUAL+QUANTITY];
				}
				#endregion - fill actual completion quantity to the new dummy row

				#region - fill begin progress quantity to the new dummy row

				if (nMonth == 1 || nMonth == 7)
					dtrNew[BEGIN] = 0;
				else
				{
					string strFilterBegin = string.Empty;
					strFilterBegin = 
						string.Format("[{0}]='{1}' ",
						              PRODUCTID,
						              strItem.Split('#')[3]
							);		
				
					DataRow[] dtrowsBegin = dtbBegin.Select(strFilterBegin);

					foreach(DataRow dtr in dtrowsBegin)
						dtrNew[BEGIN] = dtr[BEGIN];
				}

				#endregion

				// add to the transform data table
				dtbTransform.Rows.Add(dtrNew);				
			}	    
			#endregion FILL DATA FROM SOURCE DATATABLE AND ACTUALCOMPLETION DATATABLE TO THE TRANSFORM DATATABLE


			#endregion

			#region PREPARE FOR RENDER REPORT: calculate the count of Plan FAIL and Plan PASS
			/// calculate the Sum of Plan, sum of Actual, sum of Progress (on top of the report) to generate a chart
			double[,] arrSumPlan = new double[1,31];
			double[,] arrSumActual = new double[1,31];
			double[,] arrSumProgress = new double[1,31]; 
			
			for(int i = 1 ; i <= 31 ; i++)
			{
				string strCounter = i.ToString("00");

				/// compute with the positive condition
				String strFilter = string.Format("(({0}{1}/{2}{3})*100 >= {4})  AND ( ({0}{1}/{2}{3})*100 <= {5})",
					ACTUAL,strCounter,
					PLAN,	strCounter,
					95,
					105
					);					
				int nCountValueByColumn = 0;	// count the PASS number
				try
				{
					string str1 = dtbTransform.Compute("Count(ProductID)",strFilter).ToString();
					nCountValueByColumn = int.Parse(str1);
				}
				catch(Exception ex)
				{
					//MessageBox.Show(ex.Message);
				}
				
				arrCountPlanPass.Add(FLD + PLANPASS + i.ToString("00"),  nCountValueByColumn);
				arrCountPlanFail.Add  (FLD + PLANFAIL  + i.ToString("00"), dtbTransform.Rows.Count - nCountValueByColumn);

				string str = "Sum(Plan"+i.ToString("00")+")";
				try
				{
					arrSumPlan[0,i-1] = double.Parse(dtbTransform.Compute("Sum(Plan"+i.ToString("00")+")" , string.Empty ).ToString());
				}
				catch(Exception ex){}
				
				try
				{
					arrSumActual[0,i-1] = double.Parse(dtbTransform.Compute("Sum(Actual"+i.ToString("00")+")" , string.Empty).ToString());
				}
				catch(Exception ex){}

				/// progress SUm will be caculate in the next section , after render the report, we will get the real value of upper sum field on report
				/// because the progress value is caculate on render time, depend on the real actual data on the rendered report
				
			}		
			#endregion calculate the count of Plan FAIL and Plan PASS
			
			#region RENDER REPORT
			
			ReportBuilder objRB = new ReportBuilder();				
			objRB.ReportName = REPORT_NAME;
			objRB.SourceDataTable = dtbTransform;
			
			#region INIT REPORT BUIDER OBJECT
			try
			{
				objRB.ReportDefinitionFolder = mstrReportDefinitionFolder;
				objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;					
				if(objRB.AnalyseLayoutFile() == false)
					return new DataTable();
				objRB.UseLayoutFile = true;	// always use layout file
			}
			catch
			{
				objRB.UseLayoutFile = false;
			}

			C1.C1Report.Layout objLayout = objRB.Report.Layout;
			#endregion				
		
			objRB.MakeDataTableForRender();
				
			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();				
			printPreview.FormTitle = REPORT_NAME;
			objRB.ReportViewer = printPreview.ReportViewer;				
			objRB.RenderReport();			

			if(dtbSourceData.Rows.Count > 0)
			{	
				for(int i = 0 ; i <= DateTime.DaysInMonth(nYear,nMonth) ;  )
				{				
					try
					{
						arrSumProgress[0,i] = (double)objRB.GetFieldByName("fldSumProgress" + (++i).ToString("00")).Value;
					}
					catch{}
				}


				#region BUILD CHART, save to image in clipboard, and then put in the report field fldChart		
				
			

				Field fldChart = objRB.GetFieldByName(REPORTFLD_CHART);
			
				#region	INIT

				//				string APP_PATH = @"D:\PCS Project\07-Construction\Source\PCS\PCSMain\bin\Debug";				
				string EXCEL_REPORT_FOLDER = "ExcelReport";			
				string EXCEL_FILE = "ProductionLineProgressManagementReport.xls";
			
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
                
					objXLS.GetRange("A1", "AE1").Value2 = arrSumPlan;
					objXLS.GetRange("A2", "AE2").Value2 = arrSumActual;
					objXLS.GetRange("A3", "AE3").Value2 = arrSumProgress;

					DateTime dtmForExcel = new DateTime(nYear,nMonth,1);
					string[] arrExcelColumnHeading = new string[DateTime.DaysInMonth(nYear,nMonth)];
					for(int i = 1; i <= DateTime.DaysInMonth(nYear,nMonth) ; i++ )
					{						
						arrExcelColumnHeading[i-1] = i +"-" + dtmForExcel.ToString("MMM");
					}
					objXLS.GetRange("A4","AE4").Value2 = arrExcelColumnHeading;

					

					Excel.ChartObject chart = objXLS.GetChart();

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
					/// TODO: Test: remove if needed
					/// MessageBox.Show("Can not inter operate with Excel: " + ex.Message,"Production Control System",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CCN,strCCN);			
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MONTH, pstrMonth);
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_YEAR, pstrYear);			
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_PRODUCTIONLINE, strProductionLine);			
			#endregion		
			

			#region RENAME THE COLUMN HEADING TEXT
			for(int i = 0; i <= 31; i++) /// clear the heading text
				objRB.DrawPredefinedField(PREFIX_DAYINMONTH+i.ToString(ReportBuilder.FORMAT_DAY_2CHAR),"");
			objRB.DrawPredefinedList_DaysOfWeek(nYear, nMonth,
				PREFIX_DAYINMONTH,
				PREFIX_DAYOFWEEK,
				1 ,DateTime.DaysInMonth(nYear , nMonth ) );

			#endregion
			
			/// PUSH THE COUNT OF PLANFAIL AND PLANPASS			
			foreach(DictionaryEntry objEntry in arrCountPlanFail)
			{				
				objRB.DrawPredefinedField(objEntry.Key.ToString()  ,objEntry.Value.ToString() );				
			}		
			foreach(DictionaryEntry objEntry in arrCountPlanPass)
			{				
				objRB.DrawPredefinedField(objEntry.Key.ToString()  ,objEntry.Value.ToString());				
			}		

			#region HIDE the column of not-existed day in current month
			// 1. IN1T :: what to clear
			string[] arrFieldToClear = {
										   FLD + PLAN,
										   FLD + ACTUAL,
										   FLD + PROGRESS,
										   FLD + ASSESSMENT,
										   FLD + "Sum" + PLAN,
										   FLD + "Sum" + ACTUAL,
										   FLD + "Sum" + PROGRESS,			
										   FLD + PLANFAIL,
										   FLD + PLANPASS,
										   PREFIX_DAYINMONTH,	/*// also hide the Day Heading */
										   PREFIX_DAYOFWEEK
									   };		// contain name of field need to clear if day column is not exist
            
			objRB.HideColumnNotExistInMonth(nYear,nMonth, arrFieldToClear);

			#endregion HIDE the column of not-existed day in current month

			StringCollection arrFieldNames = new StringCollection();
			arrFieldNames.AddRange(arrFieldToClear);			

			string LEFT_ANCHOR_FLD = PREFIX_DAYINMONTH + "01";
			string LEFT_MARGIN_FLD = "lblPlanTotal";

			objRB.SpreadColumnsWithinWidth(arrFieldNames, 1,  DateTime.DaysInMonth(nYear,nMonth),
				objRB.GetFieldByName(LEFT_ANCHOR_FLD).Left,   objRB.ActualPageWidth - objRB.GetFieldByName(LEFT_MARGIN_FLD).Width);

			#endregion
			
			objRB.RefreshReport();

			/// force the copies number
			printPreview.FormTitle = objRB.GetFieldByName("fldTitle").Text;
			printPreview.Show();
			#endregion
			
			UseReportViewerRenderEngine = false;
			mResult = arrWorkCenterInfo;
			//return dtbSourceData;
			return dtbTransform;
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
			dtbRet.Columns.Add(MODEL,typeof(System.String));
			dtbRet.Columns.Add(BEGIN,typeof(System.Double));		

			/// fill the column (Double type) in which the date exist in the dtbSourceData (has value contain in the parrDueDateHeading)
			/// then fill the column with String type (so that it will display correctly in the report, not #,##0.00, because it has null value)
			try
			{				
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
					if(parrHasValueDateHeading.Contains(ACTUAL + i.ToString("00")) == false )
					{		
						try
						{
							dtbRet.Columns.Add(ACTUAL + i.ToString("00"),typeof(System.String));
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
		/// Get the datatable : contain Mapping ProductID with it Actual COmpletion quantity, in a specific days in month)
		/// Return table will have schema like:
		/// ProductID - ActualDay - ActualQuantity
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
		/// </summary>
		/// <returns></returns>
		private DataTable BuildActualTable(string pstrCCNID, string pstrYear, string pstrMonth, string pstrProductionLineID)
		{	
			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;

			string ACTUAL_TABLE_NAME = "ActualTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
					" Declare @pstrCCNID int " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrProductionLineID int " + 
					" /*-----------------------------------*/ " + 
					" Set @pstrCCNID = " +pstrCCNID+ " " + 
					" Set @pstrYear = '"+pstrYear+"' " + 
					" Set @pstrMonth = '"+pstrMonth+"' " + 
					" Set @pstrProductionLineID = "+pstrProductionLineID+" " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					" select " + 
					" INNERTABLE.[ProductID], " + 
					" /*  --INNERTABLE.[WOCompletionNo],  */ " + 
					" INNERTABLE.[ActualDay], " + 
					" SUM(INNERTABLE.[CompletedQuantity]) as [ActualQuantity] " + 
					"  " + 
					" FROM " + 
					" ( " + 
					" 	select   " + 
					" 	PRO_WorkOrderCompletion.ProductID as [ProductID], " + 
					" 	DATEPART(dd,PRO_WorkOrderCompletion.PostDate) as [ActualDay], " + 
					" 	PRO_WorkOrderCompletion.CompletedQuantity as [CompletedQuantity] " + 
					" 	 " + 
					" 	from ITM_Product " + 
					" 	join PRO_WorkOrderCompletion " + 
					" 	on ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID	 " + 
					" 	and ITM_Product.CCNID = @pstrCCNID " + 
					" 	and PRO_WorkOrderCompletion.LocationID IN (select distinct LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID) " +
					"  " + 
					" 	join PRO_WorkORderMaster " + 
					" 	on PRO_WorkOrderCompletion.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID " + 
					" 	and DATEPART(mm  ,PRO_WorkOrderCompletion.PostDate) = @pstrMonth " + 
					" 	and DATEPART(yyyy,PRO_WorkOrderCompletion.PostDate) = @pstrYear " + 
					" 	and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID		 " + 
					" 	join MST_Bin " + 
					" 	on PRO_WorkOrderCompletion.BinID = MST_Bin.BinID AND MST_Bin.BinTypeID = 1 " + // OK Bin
					" ) INNERTABLE " + 
					"  " + 
					" group by " + 
					" INNERTABLE.ProductID, " + 
					" /*   --INNERTABLE.WOCompletionNo,   */ " + 
					" INNERTABLE.ActualDay " 
					;

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ACTUAL_TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbRet = dstPCS.Tables[ACTUAL_TABLE_NAME].Copy();
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
		/// Get begin progress
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pdtmPreviousMonth">Previous month</param>
		/// <param name="pstrProductionLineID"></param>
		/// <returns></returns>
		private DataTable BuildBeginTable(string pstrCCNID, DateTime pdtmPreviousMonth, string pstrProductionLineID)
		{	
			DataTable dtbRet;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			DateTime dtmBeginDate;
			DateTime dtmEndDate = pdtmPreviousMonth;
			if (pdtmPreviousMonth.Month >= 7)
				dtmBeginDate = new DateTime(pdtmPreviousMonth.Year, 7, 1);
			else
				dtmBeginDate = new DateTime(pdtmPreviousMonth.Year, 1, 1);

			string TABLE_NAME = "BeginTable";
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
					" Declare @pstrCCNID int " + 
					" Declare @pstrMonth char(2) " + 
					" Declare @pstrYear char(4) " + 
					" Declare @pstrProductionLineID int " + 
					" Declare @BeginDate datetime " + 
					" Declare @EndDate datetime " + 
					" /*-----------------------------------*/ " + 
					" Set @pstrCCNID = " +pstrCCNID+ " " + 
					" Set @pstrYear = '"+pdtmPreviousMonth.Year+"' " + 
					" Set @pstrMonth = '"+pdtmPreviousMonth.Month+"' " + 
					" Set @pstrProductionLineID = "+pstrProductionLineID+" " + 
					" Set @BeginDate = '"+ dtmBeginDate.ToString("yyyy-MM-dd") +"' " + 
					" Set @EndDate = '"+ dtmEndDate.ToString("yyyy-MM-dd") +"' " + 
					" /*-----------------------------------*/ " + 
					"  " + 
					" SELECT   " +
					" ISNULL(ActualTable.ProductID, PlanTable.ProductID) ProductID, PlanQuantity, ActualQuantity,  " +
					" ISNULL(ActualQuantity,0) - ISNULL(PlanQuantity,0) AS " + BEGIN +
					"   " +
					" FROM  " +
					" ((select     " +
					" 	PRO_WorkOrderCompletion.ProductID, " +
					" 	SUM(PRO_WorkOrderCompletion.CompletedQuantity) AS ActualQuantity " +
					" 	   " +
					" 	from ITM_Product   " +
					" 	join PRO_WorkOrderCompletion   " +
					" 	on ITM_Product.ProductID = PRO_WorkOrderCompletion.ProductID	   " +
					" 	and ITM_Product.CCNID = @pstrCCNID   " +
					" 	and PRO_WorkOrderCompletion.LocationID IN (select distinct LocationID from PRO_ProductionLine where PRO_ProductionLine.ProductionLineID = @pstrProductionLineID)  " +
					"    " +
					" 	join PRO_WorkORderMaster   " +
					" 	on PRO_WorkOrderCompletion.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID   " +
					" 	and PRO_WorkOrderCompletion.PostDate BETWEEN @BeginDate AND @EndDate" +
					" 	and PRO_WorkOrderMaster.ProductionLineID = @pstrProductionLineID	 " +
					" 	group by PRO_WorkOrderCompletion.ProductID) AS ActualTable " +
					" FULL OUTER JOIN " +
					" ( " +
					" select   " +
					" 	ITM_Product.ProductID as [ProductID],  " +
					" 	SUM(PRO_DCPResultDetail.Quantity) as [PlanQuantity]  " +
					" 	  " +
					" 	from  " +
					" 	PRO_DCPResultDetail  " +
					" 	join PRO_DCPResultMaster  " +
					" 	on PRO_DCPResultDetail.DCPResultMasterID = PRO_DCPResultMaster.DCPResultMasterID  " +
					" 	and PRO_DCPResultDetail.WorkingDate BETWEEN @BeginDate AND @EndDate" +
					" 	  " +
					" 	join ITM_Product  " +
					" 	on ITM_Product.ProductID = PRO_DCPResultMaster.ProductID  " +
					" 	and ITM_Product.CCNID = @pstrCCNID  " +
					" 	  " +
					" 	join MST_WorkCenter  " +
					" 	on PRO_DCPResultMaster.WorkCenterID = MST_WorkCenter.WorkCenterID  " +
					" 	and MST_WorkCenter.ProductionLineID = @pstrProductionLineID  " +
					" 	and MST_WorkCenter.CCNID = @pstrCCNID  " +
					" 	  " +
					" 	group by   " +
					" 	ITM_Product.ProductID " +
					" ) AS PlanTable " +
					" ON ActualTable.ProductID = PlanTable.ProductID " +
					" ) " ;
				
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
		}
	}
}

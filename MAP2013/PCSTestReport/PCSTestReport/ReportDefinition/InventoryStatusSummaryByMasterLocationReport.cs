using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
//using PCSAssemblyLoader;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using PCSUtils;
using Utils = PCSComUtils.DataAccess.Utils;
using PCSUtils.Utils;
using C1.Win.C1Preview;

namespace InventoryStatusSummaryByMasterLocationReport
{
	/// <summary>	
	/// </summary>
	[Serializable]
	public class InventoryStatusSummaryByMasterLocationReport : MarshalByRefObject, IDynamicReport
	{	
		#region IDynamicReport Implementation
		private string mConnectionString;
		private ReportBuilder mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl mReportViewer;
		private object mResult;
		private bool mUseReportViewerRenderEngine = false;
		private string mstrReportDefinitionFolder = string.Empty;

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
		public bool UseReportViewerRenderEngine
		{
			get
			{
				return mUseReportViewerRenderEngine;
			}
			set
			{
				mUseReportViewerRenderEngine = value;
			}
		}


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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pstrMethod"></param>
		/// <param name="pobjParameters"></param>
		/// <returns></returns>
		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}


		#endregion



		PCSComUtils.Common.BO.UtilsBO objUtilBO = new PCSComUtils.Common.BO.UtilsBO();

		public InventoryStatusSummaryByMasterLocationReport()
		{
			
		}


		/// <summary>
		/// Thachnn: 28/10/2005
		/// Preview the report for this form
		/// Using the "InventoryStatusSummaryByMasterLocationReport.xml" layout
		/// </summary>
		/// <history>Thachnn: 29/12/2005: Add parameter display to the report. Change USECASE.</history>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public DataTable ExecuteReport(string pstrCCNID, string pstrMasterLocationID, string pstrCategoryID, string pstrParameterModel)
		{	
			#region Constants
			string mstrReportDefFolder = mstrReportDefinitionFolder;
			const string REPORT_LAYOUT_FILE = "InventoryStatusSummaryByMasterLocationReport.xml";
			const string TABLE_NAME = "InventoryStatusSummaryByMasterLocationReport";
			const string REPORT_NAME = "Inventory Status Summary By Master Location";

			const string PAGE = "Page";
			const string HEADER = "Header";

			const string REPORTFLD_TITLE = "fldTitle";

			const string REPORTFLD_COMPANY			= "fldCompany";
			const string REPORTFLD_ADDRESS			= "fldAddress";
			const string REPORTFLD_TEL				= "fldTel";
			const string REPORTFLD_FAX				= "fldFax";

			const string REPORTFLD_DAY				= "fldDay";
			const string REPORTFLD_MONTH			= "fldMonth";
			const string REPORTFLD_YEAR				= "fldYear";			


			const string REPORTFLD_CATEGORY			= "fldCategory";
			const string REPORTFLD_PARTNUMBER		= "fldPartNumber";
			const string REPORTFLD_PARTNAME			= "fldPartName";
			const string REPORTFLD_MODEL			= "fldModel";
			const string REPORTFLD_STOCKUM			= "fldStockUM";
			const string REPORTFLD_LOCATION			= "fldLocation";
			const string REPORTFLD_OHQTY			= "fldOHQty";
			const string REPORTFLD_COMMITQTY		= "fldCommitQty";
			const string REPORTFLD_AVAILABLEQTY		= "fldAvailableQty";
			const string REPORTFLD_TYPE				= "fldType";
			const string REPORTFLD_SOURCE			= "fldSource";
			const string REPORTFLD_SAFETYSTOCK		= "fldSafetyStock";
			const string REPORTFLD_LOT				= "fldLot"; 
			const string REPORTFLD_WARNING			= "fldWarning";	
			

			#region QUERY COLUMMS
			const string CATEGORY_COL = "[Category]";
			const string PARTNUMBER_COL = "[Part No.]";
			const string PARTNAME_COL = "[Part Name]";
			const string MODEL_COL = "[Model]";
			const string STOCKUM_COL = "[Stock UM]";
			const string LOCATION_COL = "[Location]";				
			const string OHQTY_COL = "[OH Qty]";
			const string COMMITQTY_COL = "[Commit Qty]";
			const string AVAILABLEQTY_COL = "[Available Qty]";
			const string TYPE_COL = "[Type]";
			const string SOURCE_COL = "[Source]";
			const string SAFETYSTOCK_COL = "[Safety Stock]";
			const string LOT_COL = "[Lot]";
			const string WARNING_COL = "[Warning]";                


			#endregion


			#endregion				

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			string strCCN = boUtil.GetCCNCodeFromID(int.Parse(pstrCCNID));
			string strMasterLocation = objBO.GetMasterLocationCodeFromID(int.Parse(pstrMasterLocationID)) + ": " + objBO.GetMasterLocationNameFromID(int.Parse(pstrMasterLocationID));
					
			string strCategory = string.Empty;
			try
			{
				strCategory = objBO.GetCategoryCodeFromID(pstrCategoryID);
			}
			catch{}

			#endregion

			float fActualPageSize  = 9000f;
				
			#region  Build dtbResult DataTable
			DataTable dtbResult;
			try 
			{			
				dtbResult = GetInventoryStatusData(pstrCCNID, pstrMasterLocationID, pstrCategoryID, pstrParameterModel);
			}
			catch
			{
				dtbResult = new DataTable();
			}		
			#endregion

			ReportBuilder objRB;	
			objRB = new ReportBuilder();
			
			objRB.ReportName = REPORT_NAME;
			objRB.SourceDataTable = dtbResult;

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
			//grid.DataSource = objRB.RenderDataTable;

			#region RENDER TO PRINT PREVIEW				
			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
			printPreview.FormTitle = REPORT_NAME;
			objRB.ReportViewer = printPreview.ReportViewer;				
			objRB.RenderReport();			


			#region COMPANY INFO  header information get from system params
//			try
//			{
//				objRB.DrawPredefinedField(REPORTFLD_COMPANY,SystemProperty.SytemParams.Get(SystemParam.COMPANY_NAME));
//			}
//			catch{}
//			try
//			{
//				objRB.DrawPredefinedField(REPORTFLD_ADDRESS,SystemProperty.SytemParams.Get(SystemParam.ADDRESS));					
//			}
//			catch{}
//			try
//			{
//				objRB.DrawPredefinedField(REPORTFLD_TEL,SystemProperty.SytemParams.Get(SystemParam.TEL));					
//			}
//			catch{}
//			try
//			{
//				objRB.DrawPredefinedField(REPORTFLD_FAX,SystemProperty.SytemParams.Get(SystemParam.FAX));					
//			}
//			catch{}

			#endregion

			#region DRAW Parameters
			
			const string CCN = "CCN";
			const string MASTER_LOCATION = "Master Location";
			const string LOCATION = "Location";
			const string CATEGORY = "Category";
			const string MODEL = "Model";

			System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
			arrParamAndValue.Add(CCN, strCCN);
			arrParamAndValue.Add(MASTER_LOCATION, strMasterLocation);			
			if(pstrCategoryID.Trim() != string.Empty)
			{
				arrParamAndValue.Add(CATEGORY, strCategory);
			}
			if(pstrParameterModel.Trim() != string.Empty)
			{
				arrParamAndValue.Add(MODEL, pstrParameterModel);
			}
		
			/// anchor the Parameter drawing canvas cordinate to the fldTitle
			C1.C1Report.Field fldTitle = objRB.GetFieldByName(REPORTFLD_TITLE);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top  + 1.3*fldTitle.RenderHeight;
			objRB.GetSectionByName(PAGE + HEADER).CanGrow = true;
			objRB.DrawParameters( objRB.GetSectionByName(PAGE + HEADER) ,dblStartX , dblStartY , arrParamAndValue, objRB.Report.Font.Size);

			#endregion

			/// there are some hardcode numbers here
			/// but these numbers are use ONLY ONE and ONLY USED HERE, so we don't need to define constant for it.
			objRB.DrawBoxGroup_Madeby_Checkedby_Approvedby(objRB.GetSectionByName(PAGE + HEADER), 16005 -1400-1400-1400, 600, 1400, 1300, 200);


			#region DAY--MONTH--YEAR INFO
			DateTime dtm;
			try
			{
				dtm = objUtilBO.GetDBDate();
			}
			catch
			{
				dtm = DateTime.Now;
			}

			try
			{
				objRB.DrawPredefinedField(REPORTFLD_DAY,dtm.Day.ToString("00"));
			}
			catch{}
			try
			{
				objRB.DrawPredefinedField(REPORTFLD_MONTH,dtm.Month.ToString("00"));
			}
			catch{}
			try
			{
				objRB.DrawPredefinedField(REPORTFLD_YEAR,dtm.Year.ToString("0000"));
			}
			catch{}				
			#endregion
		
			try	// mapping report field with table column
			{
				objRB.DrawPredefinedField(REPORTFLD_CATEGORY,CATEGORY_COL);
				objRB.DrawPredefinedField(REPORTFLD_PARTNUMBER,PARTNUMBER_COL);
				objRB.DrawPredefinedField(REPORTFLD_PARTNAME,PARTNAME_COL);
				objRB.DrawPredefinedField(REPORTFLD_MODEL,MODEL_COL);
				objRB.DrawPredefinedField(REPORTFLD_STOCKUM,STOCKUM_COL);
				objRB.DrawPredefinedField(REPORTFLD_LOCATION,LOCATION_COL);				
				objRB.DrawPredefinedField(REPORTFLD_OHQTY,OHQTY_COL);
				objRB.DrawPredefinedField(REPORTFLD_COMMITQTY,COMMITQTY_COL);
				objRB.DrawPredefinedField(REPORTFLD_AVAILABLEQTY,AVAILABLEQTY_COL);
				objRB.DrawPredefinedField(REPORTFLD_TYPE,TYPE_COL);
				objRB.DrawPredefinedField(REPORTFLD_SOURCE,SOURCE_COL);
				objRB.DrawPredefinedField(REPORTFLD_SAFETYSTOCK,SAFETYSTOCK_COL);
				objRB.DrawPredefinedField(REPORTFLD_LOT,LOT_COL);
				objRB.DrawPredefinedField(REPORTFLD_WARNING,WARNING_COL);                
			}
			catch{}		
			

			objRB.RefreshReport();
			printPreview.Show();				

			#endregion

			UseReportViewerRenderEngine = false;
			mResult = dtbResult;
			return dtbResult;	
		}


	
		/// <summary>
		/// Thachnn: 28/10/2005 - my bd
		/// Return data for Inventory Status Report
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
		public DataTable GetInventoryStatusData(string pstrCCNID, string pstrMasterLocationID, string pstrCategoryID, string pstrModel)
		{
			//const string METHOD_NAME = ".GetInventoryStatusFromCCNMasLocLocationCategory()";			
			const string TABLE_NAME = "InventoryStatusData";
			

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			
			string strSql =	
				" Declare @CCNID int " + 
				" Declare @MasterLocationID int " + 
				" Declare @LocationID int " + 
				" Declare @CategoryID int " + 
				" Declare @Model varchar(50) " + 
				" /*-----------------------------------*/ " + 
				" Set @CCNID = " +pstrCCNID+ " " + 
				" Set @MasterLocationID = " +pstrMasterLocationID+ " " + 							
				" Set @CategoryID = " + (pstrCategoryID.Trim() == string.Empty ? byte.MinValue.ToString()  : pstrCategoryID )  + " " + 
				" Set @Model = '" +pstrModel+ "' " + 
				" /*-----------------------------------*/ " + 
 " SELECT " + 
 " T.[Category],  " + 
 " T.[Part No.],  " + 
 " T.[Part Name],  " + 
 " T.[Model],  " + 
 " T.[Stock UM],  " + 
 " Sum(T.[OH Qty]) as [OH Qty], " + 
 " Sum([Commit Qty]) as [Commit Qty],     " + 
 " Sum(T.[OH Qty]) - Sum(T.[Commit Qty]) AS [Available Qty],  " + 
 " T.[Type],  " + 
 " T.[Source],     " + 
 " T.[Safety Stock], " + 
 " T.[Lot],  " + 
 " [Warning] = case     " + 
 " 		when Sum(T.[OH Qty]) - Sum(T.[Commit Qty]) - T.[Safety Stock] < 0 then 'X'    " + 
 " 		when Sum(T.[OH Qty]) - Sum(T.[Commit Qty]) - T.[Safety Stock] > 0 then ''    " + 
 " 	end   " + 
 "  " + 
 " FROM " + 
 " ( " + 
 " 	SELECT " + 
 " 	ITM_Category.Code 				AS [Category],  " + 
 " 	ITM_Product.Code 				as [Part No.],  " + 
 " 	ITM_Product.Description 			as [Part Name],  " + 
 " 	ITM_Product.Revision 				as [Model],  " + 
 " 	MST_UnitOfMeasure.Code 				AS [Stock UM],  " + 
 " 	isnull(IV_LocationCache.OHQuantity    , 0 )	AS [OH Qty],  " + 
 " 	isnull(IV_LocationCache.CommitQuantity, 0 )	AS [Commit Qty],     " + 
 " 	ITM_ProductType.Code 				AS [Type],  " + 
 " 	ITM_Source.Code 				AS [Source],     " + 
 " 	isnull(ITM_Product.SafetyStock,0) 		AS [Safety Stock], " + 
 "  IV_LocationCache.Lot 				AS [Lot]     " + 
 " 		 " + 
 " 	FROM 	ITM_Product  " + 
 " 		INNER JOIN  	IV_LocationCache 	ON ITM_Product.ProductID = IV_LocationCache.ProductID  " + 
 " 		INNER JOIN   	MST_Location 		ON IV_LocationCache.LocationID = MST_Location.LocationID  " + 
 " 		INNER JOIN   	MST_MasterLocation 	ON dbo.MST_Location.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID  " + 
 " 		INNER JOIN   	MST_CCN 		ON dbo.MST_MasterLocation.CCNID = dbo.MST_CCN.CCNID  " + 
 " 		INNER JOIN  	MST_UnitOfMeasure 	ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID  " + 
 " 		LEFT OUTER JOIN   ITM_Source 		ON ITM_Product.SourceID = ITM_Source.SourceID  " + 
 " 		LEFT OUTER JOIN   ITM_ProductType 	ON ITM_Product.ProductTypeID = ITM_ProductType.ProductTypeID  " + 
 " 		LEFT OUTER JOIN   ITM_Category 		ON ITM_Product.CategoryID = ITM_Category.CategoryID   	 " + 
 
" WHERE     " +				
(pstrCategoryID.Trim() == string.Empty ? (string.Empty) : (" ITM_Category.CategoryID IN (" + pstrCategoryID + ") and  ") ) + 
(pstrModel.Trim()        == string.Empty ? (string.Empty) : (" ITM_Product.Revision IN (" + pstrModel + ") and  ") ) + 
" MST_MasterLocation.CCNID =    @CCNID  and  " + 
" MST_MasterLocation.MasterLocationID = @MasterLocationID  " + 

" ) as T " + 
 " GROUP BY  " + 
 " [Category],  " + 
 " [Part No.],  " + 
 " [Part Name],  " + 
 " [Model],  " + 
 " [Stock UM],  " +  
 " [Type],  " + 
 " [Source],     " + 
 " [Safety Stock],    " + 
 " [Lot]  " + 
 "  " + 
 " ORDER BY [Part No.] " + 
"  " 
				;
			
			
			
			oconPCS = new OleDbConnection(mConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
		
			ocmdPCS.Connection.Open();				
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dstPCS, TABLE_NAME);

			if(dstPCS.Tables.Count > 0)
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}		
				return dstPCS.Tables[TABLE_NAME];
			}
			else
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}		
				return new DataTable();
			}
		
		}


		/// <summary>
		/// Get actual offday day in a specific month = DaysInMonth - Workingday
		/// </summary>
		/// <param name="pintMonth"></param>
		/// <param name="pintYear"></param>
		/// <returns>Actual working day of month</returns>		
		private int GetOffDayInMonth(int pintMonth, int pintYear)
		{
//			UtilsBO boUtils = new UtilsBO();
//			ArrayList arrHoliday = boUtils.GetHolidaysInYear(pintYear);			                		
			
			int intOffDays = DateTime.DaysInMonth(pintYear,pintMonth) - GetWorkingDayInMonth(pintMonth,pintYear); 

//			for(int i = 1; i <= intDaysInMonth; i++)
//			{
//				if(arrHoliday.Contains(dtmTemp))
//				{
//					intOffDays++;
//				}
//				if(arrOffDay.Contains(dtmTemp.DayOfWeek))
//				{
//					intOffDays--;
//				}                       
//
//				dtmTemp = dtmTemp.AddDays(1);
//			}
			return intOffDays;
		}

		/// <summary>
		/// Get actual working day in a specific month
		/// </summary>
		/// <param name="pintMonth"></param>
		/// <param name="pintYear"></param>
		/// <returns>Actual working day of month</returns>
		/// <author> Tuan TQ, 23 Nov, 2005</author>
		private int GetWorkingDayInMonth(int pintMonth, int pintYear)
		{

			int intDaysInMonth = DateTime.DaysInMonth(pintYear,pintMonth);

			PCSComUtils.Common.BO.UtilsBO	 boUtils = new PCSComUtils.Common.BO.UtilsBO();
			ArrayList arrHoliday = boUtils.GetHolidaysInYear(pintYear);
			ArrayList arrOffDay = boUtils.GetWorkingDayByYear(pintYear); 			

			DateTime dtmTemp = new DateTime(pintYear, pintMonth, 1);
			int intWorkingDays = intDaysInMonth; 

			for(int i = 1; i <= intDaysInMonth; i++)
			{
				if(arrHoliday.Contains(dtmTemp))
				{
					intWorkingDays--;
				}
				if(arrOffDay.Contains(dtmTemp.DayOfWeek))
				{
					intWorkingDays--;
				}                       

				dtmTemp = dtmTemp.AddDays(1);
			}
			return intWorkingDays;
		}
	}
}

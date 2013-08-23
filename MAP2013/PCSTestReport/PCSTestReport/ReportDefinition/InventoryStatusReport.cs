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

namespace InventoryStatusReport
{
	/// <summary>	
	/// </summary>
	[Serializable]
	public class InventoryStatusReport : MarshalByRefObject, IDynamicReport
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

		public InventoryStatusReport()
		{
			
		}


		/// <summary>
		/// Thachnn: 28/10/2005
		/// Preview the report for this form
		/// Using the "InventoryStatusReport.xml" layout
		/// </summary>
		/// <history>Thachnn: 29/12/2005: Add parameter display to the report. Change USECASE.</history>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public DataTable ExecuteReport(string pstrCCNID, string pstrMasterLocationID, string pstrLocationID, string pstrCategoryID, string pstrParameterModel)
		{	
			#region Constants
			string mstrReportDefFolder = mstrReportDefinitionFolder;
			const string REPORT_LAYOUT_FILE = "InventoryStatusReport.xml";
			const string TABLE_NAME = "WorkingSchemeReport";
			const string REPORT_NAME = "Inventory Status";

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

			string strLocation = string.Empty;
			try
			{
				strLocation = objBO.GetLocationCodeFromID(int.Parse(pstrLocationID));
			}
			catch{}
			
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
				dtbResult = GetInventoryStatusData(pstrCCNID, pstrMasterLocationID, pstrLocationID, pstrCategoryID, pstrParameterModel);
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
			if(pstrLocationID.Trim() != string.Empty)
			{
				arrParamAndValue.Add(LOCATION, strLocation);
			}
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
			objRB.DrawBoxGroup_Madeby_Checkedby_Approvedby(objRB.GetSectionByName(PAGE + HEADER), 15945 -1400-1400-1400, 600, 1400, 1300, 200);


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
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		public DataTable ExecuteReportOLD(string pstrCCNID, string pstrYear, string pstrMonth, string pstrMPSCycleID, string pstrProductionLineID)
		{
			//const string METHOD_NAME = ".ExecuteReport()";
			const string TABLE_NAME = "WorkingSchemeReport";
			const string SUB_TABLE_NAME = "ShiftTotalReport";

			string REPORT_NAME = "WorkingSchemeReport";
			const string SUB_REPORT_NAME = "ShiftTotalReport";
			string REPORT_LAYOUT_FILE = "InventoryStatusReport.xml";			
			
			


			short COPIES = 1;

			const string ENDSTOCK = "EndStock";
			const string CHANGECATEGORY = "Change Category";
			const string LEADTIME = "Lead Time";
			const string REQUIRECAPACITY = "Require Capacity";
			const string STANDARDCAPACITY = "Standard Capacity";
			const string COMPARESECOND = "Compare Second";
			const string COMPAREPERCENT = "Compare Percent";

			string strFromDate = string.Format("{0}-{1}-01",pstrYear,pstrMonth);	// begin date of the selected month

		
			const string REPORTFLD_WORKINGDAYS				= "fldParameterWorkingdays";
			const string REPORTFLD_OFFDAYS				= "fldParameterOffdays";

			const string REPORTFLD_PARAMETER_CCN				= "fldParameterCCN";
			const string REPORTFLD_PARAMETER_YEAR				= "fldParameterYear";			
			const string REPORTFLD_PARAMETER_MONTH				= "fldParameterMonth";
			const string REPORTFLD_PARAMETER_CYCLE				= "fldParameterCycle";
			const string REPORTFLD_PARAMETER_PRODUCTIONLINE		= "fldParameterProductionLine";

			int nCCNID = int.Parse(pstrCCNID);
			int nYear = int.Parse(pstrYear);			
			int nMonth = int.Parse(pstrMonth);			
			int nCycle = int.Parse(pstrMPSCycleID);
			int nProductionLineID = int.Parse(pstrProductionLineID);
			int nWorkingDays;
			int nOffDays;		
						
			string strCCN = string.Empty;
			string strCycle = string.Empty;
			string strProductionLine = string.Empty;	

			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strCCN = boUtil.GetCCNCodeFromID(nCCNID);			
			strCycle = objBO.GetMPSCycleFromID(nCycle) + "-" + objBO.GetMPSCycleDescriptionFromID(nCycle);
			strProductionLine = objBO.GetProductLineCodeFromID(nProductionLineID) + "-" + objBO.GetProductLineNameFromID(nProductionLineID);			
		
			// refer to mr.TuanTQ to get WOrkingDayInMonth
			nWorkingDays = GetWorkingDayInMonth(nMonth,nYear);
			nOffDays = DateTime.DaysInMonth(nYear,nMonth) - nWorkingDays;

			#endregion

			System.Data.DataTable dtbSourceData;
			System.Data.DataTable dtbSubReportData;

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			#region BUILD THE DATA TABLE
			
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				#region SQL Query
				strSql =  
 " Declare @strFromDate smalldatetime " + 
 " Declare @pstrCCNID int " + 
 " Declare @pstrProductionLineID int " + 
 " Declare @pstrMPSCycleID int " + 
 " Declare @pstrMonth char(2) " + 
 " Declare @pstrYear char(4) " + 
 "  " + 
 " Declare @pstrInArray varchar(40) " + 
 " Declare @pstrOutArray varchar(40) " + 
 "  " + 
 "  " + 
 " Set @strFromDate = '"+pstrYear+"-"+nMonth.ToString("00")+"-01' " + 
 " Set @pstrCCNID = " + pstrCCNID + " " + 
 " Set @pstrProductionLineID = " +pstrProductionLineID+ " " + 
 " Set @pstrMPSCycleID = " +pstrMPSCycleID+ " " + 
 " Set @pstrYear = '" +pstrYear+ "' " + 
 " Set @pstrMonth = '" +nMonth.ToString("00")+ "' " + 
 "  " + 
 "  " + 
 " select   " + 
 " P.ProductID as [ProductID], " + 
 " ITM_Category.Code as [Category], " + 
 " P.Code as [Part Number], " + 
 " P.Description as [Part Name], " + 
 " P.Revision as [Model], " + 
 " IsNull(CPOTable.Quantity,0) as [Plan], " + 
 " IsNull(BeginStockTable.[Begin Stock],0.00) as [Begin Stock], " + 
 " (IsNull(BeginStockTable.[Begin Stock],0) - IsNull(CPOTable.Quantity,0)) As [EndStock], " + 
 "  " + 
 "  " + 
 " CAST(IsNull((ChangeTimeTable.ChangeTime) , 0.00) as decimal(20,5) ) as [Change Category], " + 
 "  " + 
 " [Lead Time] =  " + 
 " CASE " + 
 " WHEN ITM_Routing.Pacer = 'L' THEN ITM_Routing.LaborSetupTime + ITM_Routing.LaborRunTime " + 
 " WHEN ITM_Routing.Pacer = 'M' THEN ITM_Routing.MachineSetupTime + ITM_Routing.MachineRunTime  " + 
 " WHEN ITM_Routing.Pacer = 'B' THEN  ITM_Routing.MachineRunTime + ITM_Routing.LaborRunTime  " + 
 " END, " + 
 "  " + 
 " 0.00 as [Require Capacity] " + 
 "  " + 
 " from  " + 
 " MST_WorkCenter join ITM_Routing " + 
 " on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID and ITM_Routing.Type = 0 " + 
 " left join PRO_ProductionLine " + 
 " on MST_WOrkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID  " + 
 " join ITM_Product as P " + 
 " on P.ProductID = ITM_Routing.ProductID " + 
 " join ITM_Category " + 
 " on P.CategoryID = ITM_Category.CategoryID " + 
 " join PRO_WCCapacity " + 
 " on MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID " + 
 " join PRO_ShiftCapacity " + 
 " on PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID " + 
 " join PRO_Shift " + 
 " on PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID  " + 
 "  " + 
 "  " + 
 " /* --######-- BEGIN Get Begin  Quantity of Product */ " + 
 " left join " + 
 " ( " + 
 " 		SELECT  DISTINCT  " + 
 " 					ITM_Product.ProductID, " + 
 "  " + 
 " 					( " + 
 " 				ISNULL(IV_MasLocCache.OHQuantity, 0)  " + 
 "  " + 
 " 					-  (SELECT ISNULL(SUM(TransQuantity), 0)  " + 
 " 					FROM v_TransactionHistory inFrom_Today_TransHis  " + 
 " 					WHERE ProductID = ITM_Product.ProductID " + 
 " 							AND CCNID = @pstrCCNID " + 
 " 								AND PostDate BETWEEN  @strFromDate  AND GetDate() " + 
 " 								AND TranTypeID IN (8, 11, 13, 19, 20, 16, 17) " + 
 " 					)   " + 
 "  " + 
 " 					+  (SELECT ISNULL(SUM(TransQuantity), 0)  " + 
 " 								FROM  v_TransactionHistory   " + 
 " 								WHERE ProductID = ITM_Product.ProductID " + 
 " 									AND CCNID = @pstrCCNID " + 
 " 									AND PostDate BETWEEN @strFromDate  AND GetDate() " + 
 " 									AND TranTypeID IN (12, 14, 15) " + 
 " 					)             " + 
 " 				) " + 
 " 					as [Begin Stock] " + 
 "  " + 
 " 		FROM ITM_Product     " + 
 " 		INNER JOIN IV_MasLocCache ON ITM_Product.ProductID = IV_MasLocCache.ProductID " + 
 " 		WHERE ITM_Product.CCNID = @pstrCCNID  " + 
 "  " + 
 " 		GROUP BY  " + 
 " 		ITM_Product.ProductID, " + 
 " 		IV_MasLocCache.OHQuantity  " + 
 "  " + 
 " ) " + 
 " as BeginStockTable " + 
 " on P.ProductID = BeginStockTable.ProductID " + 
 " /* ######-- END Get Begin  Quantity of Product */ " + 
 "  " + 
 "  " + 
 " /* BEGIN: Getting the Change time of Product  */ " + 
 " left join   " + 
 " ( " + 
 " 	select  " + 
 " 	CCMatrix.DestProductID as [ProductID], " + 
 " 	Sum(CCMatrix.ChangeTime) as [ChangeTime] " + 
 " 	 " + 
 " 	from PRO_ChangeCategoryMatrix as CCMatrix " + 
 " 	 " + 
 " 	/*BEGIN: Join to get condition on parameter: ProductionLineID*/ " + 
 " 	join PRO_ChangeCategoryMaster CCMaster  " + 
 " 	on CCMaster.ChangeCategoryMasterID = CCMatrix.ChangeCategoryMasterID " + 
 " 	join MST_WorkCenter " + 
 " 	on MST_WorkCenter.WorkCenterID = CCMaster.WorkCenterID " + 
 " 	and MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 " 	and MST_WorkCenter.CCNID = @pstrCCNID " + 
 " 	/*END: Join to get condition on parameter: CCNID, ProductionLineID*/ " + 
 " 	 " + 
 " 	group by  " + 
 " 	DestProductID " + 
 " ) " + 
 " as ChangeTimeTable " + 
 " on P.ProductID = ChangeTimeTable.ProductID " + 
 " /*-- END: Getting the Change time of Product  */ " + 
 "  " + 
 "  " + 
 "  " + 
 " /* BEGIN GETTING CPO PLAN QUANTITY  */ " + 
 " left join   " + 
 " ( " + 
 " 	SELECT      " + 
 " 	MTR_CPO.MPSCycleOptionMasterID, " + 
 " 	ITM_Product.ProductID,  " + 
 " 	ITM_Product.Revision AS [Model],  " + 
 " 	ITM_Product.Code AS [Part Number],   " + 
 " /*	--DATEPART(yyyy, MTR_CPO.DueDate) as [Year], */ " + 
 " /*	--DATEPART(mm, MTR_CPO.DueDate)as [Month],  */ " + 
 " 	SUM(MTR_CPO.Quantity) AS [Quantity]  " + 
 " 	 " + 
 " 	FROM        " + 
 " 	MTR_CPO  " + 
 " 	  INNER JOIN MST_CCN  " + 
 " 	  ON MTR_CPO.CCNID = MST_CCN.CCNID   " + 
 " 	INNER JOIN ITM_Product  " + 
 " 	  ON MTR_CPO.ProductID = ITM_Product.ProductID " + 
 " 	left outer JOIN ITM_Category  " + 
 " 	  ON ITM_Product.CategoryID = ITM_Category.CategoryID  " + 
 " 	 " + 
 " 	WHERE     " + 
 " 	MTR_CPO.CCNID = @pstrCCNID AND   " + 
 " 	MTR_CPO.MPSCycleOptionMasterID = @pstrMPSCycleID AND  " + 
 " 	DATEPART(yyyy, MTR_CPO.DueDate) = @pstrYear AND  " + 
 " 	DATEPART(mm, MTR_CPO.DueDate) = @pstrMonth   " + 
 " 	 " + 
 " 	GROUP BY    " + 
 " 	MTR_CPO.MPSCycleOptionMasterID, " + 
 " 	MST_CCN.Code,  " + 
 " 	ITM_Category.Code,   " + 
 " 	ITM_Product.ProductID, " + 
 " 	ITM_Product.Code,  " + 
 " 	ITM_Product.Description,  " + 
 " 	ITM_Product.Revision, " + 
 " 	DatePart(yyyy,MTR_CPO.DueDate),  " + 
 " 	DatePart(mm,MTR_CPO.DueDate) " + 
 " ) " + 
 " as CPOTable " + 
 " on P.ProductID = CPOTable.ProductID " + 
 " /* END GETTING CPO PLAN QUANTITY   */ " + 
 "  " + 
 "  " + 
 "  " + 
 "  " + 
 " WHERE " + 
 " PRO_ProductionLine.ProductionLineID = @pstrProductionLineID " + 
 " and DATEPART(yyyy, PRO_WCCapacity.BeginDate ) <= @pstrYear " + 
 " and DATEPART(yyyy, PRO_WCCapacity.EndDate ) >= @pstrYear " + 
 " and DATEPART(mm, PRO_WCCapacity.BeginDate ) <= @pstrMonth " + 
 " and DATEPART(mm, PRO_WCCapacity.EndDate ) >= @pstrMonth " + 
 " and MST_WOrkCenter.IsMain = 1 " + 
 "  " + 
 "  " + 
 " /* GROUP BY of outside query*/ " + 
 " group by 			 " + 
 " P.ProductID, " + 
 " ITM_Category.Code, " + 
 " P.Code, " + 
 " P.Description, " + 
 " P.Revision, " + 
 " CPOTable.Quantity,  " + 
 " BeginStockTable.[Begin Stock], " + 
 "  " + 
 " (IsNull(CPOTable.Quantity,0) - IsNull(BeginStockTable.[Begin Stock],0)), " + 
 "  " + 
 " ITM_Routing.Pacer, " + 
 " ITM_Routing.LaborSetupTime, " + 
 " ITM_Routing.LaborRunTime, " + 
 " ITM_Routing.MachineSetupTime, " + 
 " ITM_Routing.MachineRunTime, " + 
 " CAST(IsNull((ChangeTimeTable.ChangeTime) , 0.00) as decimal(20,5) ) " + 
 "  " + 
 " Order by [Category],[Part Number],[Model] " + 
 "  " + 
 "  " + 
 "  " + 
 " /**********************************************************************************/ " + 
 " /**********************************************************************************/ " + 
 " /**********************************************************************************/ " + 
 "  " + 
 "  " + 
 " /* BEGIN StandardCapacity caculate */ " + 
 " select   " + 
 " PRO_Shift.ShiftDesc as [Shift], " + 
 " sum(PRO_WCCapacity.Capacity) as [Standard Capacity] " + 
 "  " + 
 " from  " + 
 " MST_WorkCenter " + 
 " join PRO_WCCapacity " + 
 " on MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID " + 
 " join PRO_ShiftCapacity " + 
 " on PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID " + 
 " join PRO_Shift " + 
 " on PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID  " + 
 "  " + 
 " /* left join MTR_CPO " + 
 " on MTR_CPO.ProductID = ITM_Product.ProductID " + 
 " and MTR_CPO.MPSCycleOptionMasterID = @pstrMPSCycleID " + 
 " */ " + 
 "  " + 
 " WHERE " + 
 " MST_WorkCenter.ProductionLineID = @pstrProductionLineID " + 
 " and PRO_WCCapacity.CCNID = @pstrCCNID " + 
 " and MST_WOrkCenter.IsMain = 1 " + 
 "  " + 
 " and DATEPART(yyyy, PRO_WCCapacity.BeginDate ) <= @pstrYear " + 
 " and DATEPART(yyyy, PRO_WCCapacity.EndDate ) >= @pstrYear " + 
 " and DATEPART(mm, PRO_WCCapacity.BeginDate ) <= @pstrMonth " + 
 " and DATEPART(mm, PRO_WCCapacity.EndDate ) >= @pstrMonth " + 
 "  " + 
 "  " + 
 " group by  " + 
 " PRO_Shift.ShiftDesc " + 
 "  " + 
 " /* END StandardCapacity caculate */ " + 
 "  " + 
 "  " ;
				
				#endregion



				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				if(dstPCS.Tables.Count > 0)
				{
					dtbSourceData = dstPCS.Tables[0].Copy();
					dtbSubReportData = dstPCS.Tables[1].Copy();

				}
				else
				{
					dtbSourceData = new DataTable();
					dtbSubReportData = new DataTable();
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
						
			#region TRANSFORM DATATABLE FOR REPORT
			// only re-calculate Require Capacity, Compare Second, and Compare Percent column		
			
			/// Calculate the Require Capacity
			foreach(DataRow drow in dtbSourceData.Rows)
			{
				drow[REQUIRECAPACITY] = (decimal)drow[ENDSTOCK] * (decimal)drow[LEADTIME] + (decimal)drow[CHANGECATEGORY] ;
			}		

			decimal decSumOfRequireCapacity = 0;
			/// calculate the Sum of Require Capacity			
			foreach(DataRow drow in dtbSourceData.Rows)
			{				 
				decSumOfRequireCapacity += (decimal)drow[REQUIRECAPACITY];
			}		
			

			dtbSubReportData.Columns.Add(COMPARESECOND, typeof(System.Decimal));
			dtbSubReportData.Columns.Add(COMPAREPERCENT);

			/// calculate the 2 Compare Column
			foreach(DataRow drow in dtbSubReportData.Rows)
			{				
				drow[COMPARESECOND] = (decimal)drow[STANDARDCAPACITY] - decSumOfRequireCapacity;

				if(decSumOfRequireCapacity != Decimal.Zero)
				{
					decimal decPercentValue = ((decimal)drow[STANDARDCAPACITY]*100)  / decSumOfRequireCapacity; /// Percent
					drow[COMPAREPERCENT] = decPercentValue.ToString("#,##0.00") + "%";
				}
			}		
			

			#endregion
					
			#region RENDER REPORT
	
			ReportWithSubReportBuilder objRB = new ReportWithSubReportBuilder();

			objRB.ReportName = REPORT_NAME;				
			objRB.SourceDataTable = dtbSourceData;			
			objRB.SubReportDataSources.Add(SUB_REPORT_NAME, dtbSubReportData);
			
			objRB.ReportDefinitionFolder = mstrReportDefinitionFolder;
			objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;			
			objRB.UseLayoutFile = true;
			objRB.MakeDataTableForRender();

			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
			
			printPreview.FormTitle = REPORT_NAME;
			//Attach report viewer
			objRB.ReportViewer = printPreview.ReportViewer;				
			objRB.RenderReport();
            
			#region MODIFY THE REPORT LAYOUT
			
			objRB.DrawBoxGroup_Madeby_Checkedby_Approvedby(objRB.GetSectionByName("Header"),100,100,1300,1500,200);

			objRB.DrawPredefinedField(REPORTFLD_WORKINGDAYS, nWorkingDays.ToString());
			objRB.DrawPredefinedField(REPORTFLD_OFFDAYS, nOffDays.ToString());

			#region PUSH PARAMETER VALUE				

			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CCN,strCCN);
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_YEAR, nYear.ToString("0000"));
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MONTH, nMonth.ToString("00"));			
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CYCLE,strCycle);
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_PRODUCTIONLINE, strProductionLine);	
			#endregion		
			
			#endregion	
					
			objRB.RefreshReport();
				
			printPreview.Show();
			#endregion

			UseReportViewerRenderEngine = false;
			mResult = dtbSourceData;			
			return dtbSourceData;
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
		public DataTable GetInventoryStatusData(string pstrCCNID, string pstrMasterLocationID, string pstrLocationID, string pstrCategoryID, string pstrModel)
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
				" Set @LocationID =  " + (pstrLocationID.Trim()  == string.Empty ? byte.MinValue.ToString()  : pstrLocationID )   + " " + 
				" Set @CategoryID = " + (pstrCategoryID.Trim() == string.Empty ? byte.MinValue.ToString()  : pstrCategoryID )  + " " + 
				" Set @Model = '" +pstrModel+ "' " + 
				" /*-----------------------------------*/ " + 
				" SELECT " + 
				" ITM_Category.Code AS Category,  " + 
				" ITM_Product.Code as [Part No.],  " + 
				" ITM_Product.Description as [Part Name],  " + 
				" ITM_Product.Revision as [Model],  " + 
				" MST_UnitOfMeasure.Code AS [Stock UM],  " + 
				" MST_Location.Code AS Location,     " + 
				" IV_LocationCache.OHQuantity AS [OH Qty],  " + 
				" IV_LocationCache.CommitQuantity AS [Commit Qty],     " + 
				" isnull(IV_LocationCache.OHQuantity,0) - isnull(IV_LocationCache.CommitQuantity,0) AS [Available Qty],  " + 
				" ITM_ProductType.Code AS Type,  " + 
				" ITM_Source.Code AS Source,     " + 
				" ITM_Product.SafetyStock AS [Safety Stock],    " + 
				" IV_LocationCache.Lot AS [Lot],    " + 
				" [Warning] = case     " + 
				" 		when isnull(IV_LocationCache.OHQuantity,0) - isnull(IV_LocationCache.CommitQuantity,0) - isnull(ITM_Product.SafetyStock,0) < 0 then 'X'    " + 
				" 		when isnull(IV_LocationCache.OHQuantity,0) - isnull(IV_LocationCache.CommitQuantity,0) - isnull(ITM_Product.SafetyStock,0) > 0 then ''    " + 
				" 	end    " + 
				" 	 " + 
				" FROM 	ITM_Product  " + 
				" 	INNER JOIN   IV_LocationCache ON ITM_Product.ProductID = IV_LocationCache.ProductID  " + 
				" 	INNER JOIN   MST_Location ON IV_LocationCache.LocationID = MST_Location.LocationID  " + 
				" 	INNER JOIN   dbo.MST_MasterLocation ON dbo.MST_Location.MasterLocationID = dbo.MST_MasterLocation.MasterLocationID  " + 
				" 	INNER JOIN   dbo.MST_CCN ON dbo.MST_MasterLocation.CCNID = dbo.MST_CCN.CCNID  " + 
				" 	INNER JOIN   MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID  " + 
				" 	LEFT OUTER JOIN   ITM_Source ON ITM_Product.SourceID = ITM_Source.SourceID  " + 
				" 	LEFT OUTER JOIN   ITM_ProductType ON ITM_Product.ProductTypeID = ITM_ProductType.ProductTypeID  " + 
				" 	LEFT OUTER JOIN   ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID   	 " + 
				" WHERE     " + 				

				(pstrLocationID.Trim() == string.Empty ? (string.Empty) : (" MST_Location.LocationID =    @LocationID and  ") ) + 
				(pstrCategoryID.Trim() == string.Empty ? (string.Empty) : (" ITM_Category.CategoryID IN (" + pstrCategoryID + ")      and  ") ) + 
				(pstrModel.Trim()        == string.Empty ? (string.Empty) : (" ITM_Product.Revision IN (" + pstrModel + ") and  ") ) + 
				" MST_MasterLocation.CCNID =    @CCNID  and  " + 
				" MST_MasterLocation.MasterLocationID = @MasterLocationID  " + 
				"  " + 
				" ORDER BY ITM_Product.Description   " + 
				"  "	;
			
			
			
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

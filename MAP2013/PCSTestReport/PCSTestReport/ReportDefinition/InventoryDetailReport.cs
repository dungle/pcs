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

namespace InventoryDetailReport
{
	/// <summary>	
	/// </summary>
	[Serializable]
	public class InventoryDetailReport : MarshalByRefObject, IDynamicReport
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

		public InventoryDetailReport()
		{
			
		}


		/// <summary>
		/// Thachnn: 28/10/2005
		/// Preview the report for this form
		/// Using the "InventoryDetailReport.xml" layout
		/// </summary>
		/// <history>Thachnn: 29/12/2005: Add parameter display to the report. Change USECASE.</history>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public DataTable ExecuteReport(string pstrCCNID, string pstrMasterLocationID, string pstrLocationID,string pstrBinID , string pstrCategoryID, string pstrParameterModel)
		{	
			#region Constants
			string mstrReportDefFolder = mstrReportDefinitionFolder;
			const string REPORT_LAYOUT_FILE = "InventoryDetailReport.xml";
			const string TABLE_NAME = "InventoryDetailReport";
			const string REPORT_NAME = "Inventory Detail";

			const string PAGE = "Page";
			const string HEADER = "Header";

			const string REPORTFLD_TITLE = "fldTitle";

			const string REPORTFLD_COMPANY			= "fldCompany";
			const string REPORTFLD_ADDRESS			= "fldAddress";
			const string REPORTFLD_TEL				= "fldTel";
			const string REPORTFLD_FAX				= "fldFax";

			const string REPORTFLD_DATETIME				= "fldDateTime";			

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

			string strBin = string.Empty;
			try
			{
				strBin = objBO.GetBinCodeFromID(int.Parse(pstrBinID));
			}
			catch{}

			
			string strCategory = string.Empty;
			try
			{
				strCategory = objBO.GetCategoryCodeFromID(int.Parse(pstrCategoryID));
			}
			catch{}

			#endregion

			float fActualPageSize  = 9000f;
				
			#region  Build dtbResult DataTable
			DataTable dtbResult;
			try 
			{			
				dtbResult = GetInventoryDetailData(pstrCCNID, pstrMasterLocationID, pstrLocationID, pstrBinID, pstrCategoryID, pstrParameterModel);
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
			const string BIN = "Bin";
			const string CATEGORY = "Category";
			const string MODEL = "Model";

			System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
			arrParamAndValue.Add(CCN, strCCN);
			arrParamAndValue.Add(MASTER_LOCATION, strMasterLocation);
			if(pstrLocationID.Trim() != string.Empty)
			{
				arrParamAndValue.Add(LOCATION, strLocation);
			}
			if(pstrBinID.Trim() != string.Empty)
			{
				arrParamAndValue.Add(BIN, strBin);
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
				objRB.DrawPredefinedField(REPORTFLD_DATETIME,dtm.ToString("dd-MM-yyyy hh:mm"));
			}
			catch{}

			#endregion
		
			
			objRB.RefreshReport();
			printPreview.Show();				

			#endregion

			UseReportViewerRenderEngine = false;
			mResult = dtbResult;
			return dtbResult;	
		}


	
		/// <summary>
		/// Thachnn: 28/10/2005 - my bd
		/// Return data for Inventory Detail Report
		/// </summary>
		/// <param name="pnCCNID"></param>
		/// <param name="pnMasterLocationID"></param>
		/// <param name="pnLocationID"></param>
		/// <param name="pnCategoryID"></param>
		/// <returns></returns>
		public DataTable GetInventoryDetailData(string pstrCCNID, string pstrMasterLocationID, string pstrLocationID, string pstrBinID, string pstrCategoryID, string pstrModel)
		{
			//const string METHOD_NAME = ".GetInventoryDetailFromCCNMasLocLocationCategory()";			
			const string TABLE_NAME = "InventoryDetailData";
			

			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			
			string strSql =					
			" Declare @CCNID int " + 
			" Declare @MasterLocationID int " + 
			" Declare @LocationID int " + 
			" Declare @BinID int " + 
			" Declare @CategoryID int " + 
			" Declare @Model varchar(50) " + 
			" /*-----------------------------------*/ " + 
			" Set @CCNID = " +pstrCCNID+ " " + 
			" Set @MasterLocationID = " +pstrMasterLocationID+ " " + 
			" Set @LocationID = '" +pstrLocationID+ "' " + 
			" Set @BinID = '" +pstrBinID+ "' " + 
			" Set @CategoryID = '" +pstrCategoryID+ "' " + 
			" Set @Model = '" +pstrModel+ "' " + 
			" /*-----------------------------------*/ " + 
			"  " + 
			" SELECT  " + 
			" MST_Location.Code AS [Location],     " + 
			" ENM_BinType.Name as [BinType], " + 
			" MST_Bin.Code AS [Bin],     " + 
			" ITM_Category.Code AS [Category],  " + 
			" ITM_Product.Code as [PartNumber],  " + 
			" ITM_Product.Description as [PartName],  " + 
			" ITM_Product.Revision as [Model],  " + 
			" MST_UnitOfMeasure.Code AS [StockUM],  " + 
			" IV_BinCache.OHQuantity AS [OHQuantity], " + 
			" IV_BinCache.CommitQuantity AS [CommitQuantity],    " +
			" isnull(IV_BinCache.OHQuantity,0) - isnull(IV_BinCache.CommitQuantity,0) AS [AvailableQuantity] " +
			"  " + 
			" 	 " + 
			" FROM 	ITM_Product  " + 
			" join IV_BinCache on ITM_Product.ProductID = IV_BinCache.ProductID " + 
			" join MST_Location ON IV_BinCache.LocationID = MST_Location.LocationID " + 
			" join MST_Bin on IV_BinCache.BinID = MST_Bin.BinID " + 
			" left join ENM_BinType on MST_Bin.BinTypeID = ENM_BinType.BinTypeID " + 
			" left join ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID " + 
			" join MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID  " + 
			"  " + 
			" WHERE     " + 
			" ITM_Product.CCNID =		@CCNID  		and   " + 
			" MST_Location.MasterLocationID = @MasterLocationID	and   " + 
			(pstrLocationID.Trim() == string.Empty ? (string.Empty) : (" MST_Location.LocationID =    @LocationID and   ") ) + 
			(pstrBinID.Trim() == string.Empty ? (string.Empty) : (" MST_Bin.BinID = 	@BinID 	and   ") ) + 
			(pstrCategoryID.Trim() == string.Empty ? (string.Empty) : (" ITM_Product.CategoryID IN (" + pstrCategoryID + ") and   ") ) + 
			(pstrModel.Trim()        == string.Empty ? (string.Empty) : (" ITM_Product.Revision IN (" + pstrModel + ") and   ") ) + 
			" IV_BinCache.CCNID =		@CCNID			  " + 
			"  " + 
			"  " + 
			" /* Bin Condition */ " + 
			"  " + 
			" ORDER BY  " + 
			" [Location],     " + 
			" [BinType], " + 
			" [Bin],     " + 
			" [Category],  " + 
			" [PartNumber],  " + 
			" [PartName],  " + 
			" [Model],  " + 
			" [StockUM] " 
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


	
	}
}

using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Data.OleDb;
using Utils = PCSComUtils.DataAccess.Utils;
using System.Collections.Specialized;
using PCSUtils.Utils;
using C1.Win.C1Preview;
using C1.C1Report;
using C1.Win.C1Chart;
using Section = C1.C1Report.Section;
using System.IO;



namespace PurchaseOrderManagementReport
{
	/// <summary>
	/// Thachnn: CONCEPT to build this report
	/// 
	/// </summary>
	[Serializable]	
	public class PurchaseOrderManagementReport : MarshalByRefObject, IDynamicReport
	{
		#region IDynamicReport Implementation
		private string mConnectionString;
		private ReportBuilder mReportBuilder = new ReportBuilder();		
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = true;	

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
		public PurchaseOrderManagementReport()
		{
		}		

		const string THIS = "ExternalReportFile:PurchaseOrderManagementReport";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		const string TABLE_NAME = "PurchaseOrderManagementReport";			
		const string ZERO_STRING = "0";
		
		const string ISSUE_DATE_FORMAT = "dd-MMM-yyyy";
		const string NEXTMONTH_DATE_FORMAT = "MMM-yy";

		public static string FORMAT_REPORT_NUMBER = "#,##0.00";

		const string FLD = "fld";
		const string TITLE = "Title";	

		const string PAGE = "Page";
		const string HEADER = "Header";
	
		// STATIC SHARE VARIABLES FOR OTHER CLASS ACCESS
		public static int pnCCNID;
        public static string pstrPurchaseOrderID_LIST = string.Empty;
		public static string pstrPartyID_LIST = string.Empty;
		public static string pstrProductID_LIST = string.Empty;

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
			string pstrPurchaseOrderID_LIST, string pstrPartyID_LIST, string pstrProductID_LIST)
		{
			#region My variables				

			/// Report layout file constant
			const string REPORT_LAYOUT_FILE = "PurchaseOrderManagementReport.xml";
			const string REPORT_NAME = "PurchaseOrderManagementReport";
			short COPIES = 1;

			/// all parameter are Mandatory
			const string REPORTFLD_PARAMETER_CCN				= "lblCCN";
			const string REPORTFLD_PARAMETER_PURCHASEORDER		= "lblPurchaseOrder";
			const string REPORTFLD_PARAMETER_MAKER				= "lblMaker";
			const string REPORTFLD_PARAMETER_PRODUCT			= "lblProduct";
						
			PurchaseOrderManagementReport.pnCCNID = int.Parse(pstrCCNID);
			PurchaseOrderManagementReport.pstrPurchaseOrderID_LIST = pstrPurchaseOrderID_LIST;
			PurchaseOrderManagementReport.pstrPartyID_LIST = pstrPartyID_LIST;
			PurchaseOrderManagementReport.pstrProductID_LIST = pstrProductID_LIST;			

			string strCCN = string.Empty;
			string strPurchaseOrderLIST = string.Empty;
			string strPartyLIST = string.Empty;
			string strProductLIST = string.Empty;

			const string REPORTFLD_TITLE			= "fldTitle";
			float fActualPageSize = 9000.0f;			
			
			/// custom object to access and modify the dtbPurchaseOrderList
			PLReportDataHelper objPLTable = new PLReportDataHelper();		
			
			#endregion		
			
		
			/// ------------------------------ MAIN FLOW OF THIS REPORT ----------------------------------			
			/// ##1## BUILD THE PRODUCTION LINE LIST TABLE
			#region BUILD some RAW DATA TABLE and Array		
			
			objPLTable.GetDataAndCache();		
			
			#endregion BUILD some RAW DATA TABLE

			#region	GETTING THE PARAMETER 			
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
			strCCN = boUtil.GetCCNCodeFromID(PurchaseOrderManagementReport.pnCCNID);
			strPurchaseOrderLIST = GetListFromDataTable(objPLTable.GetDataTable(1) );
			strPartyLIST = GetListFromDataTable(objPLTable.GetDataTable(2));
			strProductLIST = GetListFromDataTable(objPLTable.GetDataTable(3));

			#endregion
			
			#region BUILD DATA OF REPORT
			
			
			#endregion BUILD DATA OF REPORT
			
			/// #### RENDER TO REPORT
			#region RENDER REPORT
			
			ReportBuilder objRB = mReportBuilder;
			objRB.ReportName = REPORT_NAME;
			objRB.SourceDataTable = objPLTable.PurchaseOrderList;	// we build report base on HashTable, not DataTable, so we put new empty DataTable in to ReportBuilder to avoid error of outside processing
			//return objPLTable.PurchaseOrderList;

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
			
			#region MODIFY THE REPORT LAYOUT				
	
			#region DRAW Parameters

			const int MAX_PARA_LENGTH_TO_SHOW = 150;
				
			System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
			arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_CCN).Text, strCCN);			

			if(strPurchaseOrderLIST.Length > 100)
				strPurchaseOrderLIST = strPurchaseOrderLIST.Substring(0,MAX_PARA_LENGTH_TO_SHOW) + " ...";
			if(strPartyLIST.Length > 100)
				strPartyLIST = strPartyLIST.Substring(0,MAX_PARA_LENGTH_TO_SHOW) + " ...";
			if(strProductLIST.Length > 100)
				strProductLIST = strProductLIST.Substring(0,MAX_PARA_LENGTH_TO_SHOW) + " ...";

			if(pstrPurchaseOrderID_LIST != string.Empty)
			{
				arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_PURCHASEORDER).Text, strPurchaseOrderLIST);
			}
			if(pstrPartyID_LIST != string.Empty)
			{
				arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_MAKER).Text, strPartyLIST);
			}
			if(pstrProductID_LIST != string.Empty)
			{
				arrParamAndValue.Add(objRB.GetFieldByName(REPORTFLD_PARAMETER_PRODUCT).Text, strProductLIST);
			}
						
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
			mResult = objPLTable.PurchaseOrderList;
			return objPLTable.PurchaseOrderList;
		}

		private string GetListFromDataTable(DataTable pdtb)
		{
			string strRet = string.Empty;
			foreach(DataRow drow in pdtb.Rows )
			{
                strRet += " " + drow[0] + ",";
			}
			if(strRet.Length > 1 )
			{
				strRet = strRet.TrimEnd(',');
			}
			return strRet;
		}

		private string GetListFromDataTable(DataTable pdtb, string pstrColumnName)
		{
			string strRet = string.Empty;
			foreach(DataRow drow in pdtb.Rows )
			{
				strRet += " " + drow[pstrColumnName] + ",";
			}
			if(strRet.Length > 1 )
			{
				strRet = strRet.TrimEnd(',');
			}
			return strRet;
		}


	}	// end class report





	/// <summary>
	/// collect all task affect to the DataTable. 
	/// Must set the PurchaseOrderList datatable to the InnerDataTable of instance of this class to processing.
	/// </summary>
	public class PLReportDataHelper
	{		
		
		// it is a offline cache of data from database 
		// main dataset of this report, contain all data get from database
		public DataSet mdst_MAIN_DATA_REPOSITORY = new DataSet("MAIN_DATA_REPOSITORY");

		
		/// <summary>
		/// SCHEMA: 		
		/// </summary>
		private DataTable mdtbPurchaseOrderList;
		public DataTable PurchaseOrderList
		{
			get
			{
				return mdtbPurchaseOrderList;
			}
			set
			{
				mdtbPurchaseOrderList = value;
			}
		}
		

		#region GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable) - TRANSFORM SOME THING

		/// <summary>
		/// Get Production Line List (table) with PPM setted
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
			
				strSql = 	

 " Declare @pstrCCNID int " + 
 " /*-----------------------------------*/ " + 
 " Set @pstrCCNID = " +PurchaseOrderManagementReport.pnCCNID+ " " + 
 " /*-----------------------------------*/ " + 
 " SELECT  " + 
 " POTABLE.PONo, " + 
 " POTABLE.OrderDate, " + 
 " POTABLE.VendorCode, " + 
 " POTABLE.VendorName, " + 
 " POTABLE.MakerCode, " + 
 " POTABLE.MakerName, " + 
 " POTABLE.POLine, " + 
 " POTABLE.PartNo, " + 
 " POTABLE.PartName, " + 
 " POTABLE.Model, " + 
 " POTABLE.UM, " + 
 " POTABLE.OrderQuantity, " + 
 " POTABLE.ApprovalStatus, " + 
 " POTABLE.ApproverCode, " + 
 " POTABLE.ApproverName, " + 
 " POTABLE.ApprovalDate, " + 
 " CASE  " + 
 " WHEN RECEIVETABLE.ReceiveQuantity > 0 THEN 1 " + 
 " ELSE 0 " + 
 " END as [ReceivingStatus], " + 
 " RECEIVETABLE.ReceiveQuantity as [ReceiveQuantity], " + 
 " LASTRECEIVETABLE.LastReceiptDate as [LastReceiptDate], " + 
 " RETURNTABLE.ReturnQuantity as [ReturnQuantity], " + 
 " (POTABLE.OrderQuantity - RECEIVETABLE.ReceiveQuantity) as [RemainQuantity], " + 
 " POTABLE.CloseStatus " + 
 "  " + 
 " FROM  " + 
 " ( " + 
 " 	/*START  ******************* MAIN PO TABLE ***************************************************/ " + 
 " 	select " + 
 " 	POMASTER.CCNID as [CCNID], " + 
 " 	POMASTER.PurchaseOrderMasterID as [PurchaseOrderMasterID],  " + 
 " 	POMASTER.Code AS [PONo], " + 
 " 	POMASTER.OrderDate as [OrderDate], " + 
 " 	VENDOR.Code as [VendorCode], " + 
 " 	VENDOR.Name as [VendorName], " + 
 " 	MAKER.PartyID as [PartyID], " + 
 " 	MAKER.Code as [MakerCode], " + 
 " 	MAKER.Name as [MakerName],	 " + 
 " 	PODETAIL.Line as [POLine], " + 
 " 	PODETAIL.ProductID as [ProductID], " + 
 " 	PRODUCT.Code as [PartNo], " + 
 " 	PRODUCT.Description as [PartName],  " + 
 " 	PRODUCT.Revision as [Model], " + 
 " 	UM.Code as [UM], " + 
 " 	PODETAIL.OrderQuantity as [OrderQuantity], " + 
 " 	 " + 
 " 	CASE  " + 
 " 	WHEN PODETAIL.ApproverID is null THEN 0 " + 
 " 	ELSE 1 " + 
 " 	END as [ApprovalStatus], " + 
 " 	 " + 
 " 	EMPLOYEE.Code as [ApproverCode], " + 
 " 	EMPLOYEE.Name as [ApproverName], " + 
 " 	PODETAIL.ApprovalDate as [ApprovalDate], " + 
 " 	 " + 
 " 	CASE  " + 
 " 	WHEN PODETAIL.Closed is null OR PODETAIL.Closed = 0 THEN 0 " + 
 " 	ELSE 1 " + 
 " 	END as [CloseStatus] " + 
 " 	  " + 
 " 	from " + 
 " 	PO_PurchaseOrderMaster as POMASTER " + 
 " 	join PO_PurchaseOrderDetail as PODETAIL " + 
 " 	on POMASTER.PurchaseOrderMasterID = PODETAIL.PurchaseOrderMasterID " + 
 " 	join ITM_Product as PRODUCT " + 
 " 	on PODETAIL.ProductID = PRODUCT.ProductID " + 
 " 	 " + 
 " 	LEFT join MST_Party as MAKER " + 
 " 	on POMASTER.PartyID = MAKER.PartyID " + 
 " 	LEFT join MST_PartyContact as VENDOR " + 
 " 	on POMASTER.PartyContactID = VENDOR.PartyContactID " + 
 " 	LEFT join MST_UnitOfMeasure as UM " + 
 " 	on PRODUCT.StockUMID = UM.UnitOfMeasureID " + 
 " 	LEFT join MST_Employee AS EMPLOYEE " + 
 " 	on PODETAIL.ApproverID = EMPLOYEE.EmployeeID " + 
 "  " + 
 " 	/*END  ******************** MAIN PO TABLE ***************************************************/ " + 
 " )as POTABLE " + 
 "  " + 
 " LEFT JOIN " + 
 " ( " + 
 " 	/*BEGIN  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
 " 	select  " + 
 " 	POREDETAIL.PurchaseOrderMasterID, " + 
 " 	POREDETAIL.ProductID,  " + 
 " 	Sum(IsNull(POREDETAIL.ReceiveQuantity, 0.00)) as [ReceiveQuantity] " + 
 " 	 " + 
 " 	from PO_PurchaseOrderReceiptMaster as POREMASTER " + 
 " 	join PO_PurchaseOrderReceiptDEtail as POREDETAIL " + 
 " 	on POREMASTER.PurchaseOrderReceiptID = POREDETAIL.PurchaseOrderReceiptID " + 
 " 	and POREMASTER.CCNID = @pstrCCNID " + 
 " 	 " + 
 " 	join PO_PurchaseOrderMaster as POMASTER " + 
 " 	on POREDETAIL.PurchaseOrderMasterID = POMASTER.PurchaseOrderMasterID " + 
 " 	 " + 
 " 	Group by  " + 
 " 	POREDETAIL.PurchaseOrderMasterID, " + 
 " 	POREDETAIL.ProductID " + 
 " 	/*END  ******************** ACTUAL QUANTITY, IS PO RECEIPT ***************************************************/ " + 
 "  " + 
 " ) as RECEIVETABLE " + 
 " on POTABLE.PurchaseOrderMasterID = RECEIVETABLE.PurchaseOrderMasterID " + 
 " and POTABLE.ProductID = RECEIVETABLE.ProductID " + 
 "  " + 
 " LEFT JOIN " + 
 " (	/* START ** LAST RECEIPTDATE ** */ " + 
 " 	select  " + 
 " 	POREMASTER.PurchaseOrderMasterID, " + 
 " 	POREDETAIL.ProductID, " + 
 " 	Max(PostDate) as [LastReceiptDate] " + 
 " 	from PO_PurchaseOrderReceiptMaster as POREMASTER " + 
 " 	join PO_PurchaseOrderReceiptDetail as POREDETAIL " + 
 " 	on POREMASTER.PurchaseOrderReceiptID = POREDETAIL.PurchaseOrderReceiptID " + 
 " 	and POREMASTER.CCNID = @pstrCCNID " + 
 " 	 " + 
 " 	group by  " + 
 " 	POREMASTER.PurchaseOrderMasterID, " + 
 " 	POREDETAIL.ProductID " + 
 " 	/* END  ** LAST RECEIPTDATE ** */ " + 
 " ) as LASTRECEIVETABLE " + 
 " on POTABLE.PurchaseOrderMasterID = LASTRECEIVETABLE.PurchaseOrderMasterID " + 
 " and POTABLE.ProductID = LASTRECEIVETABLE.ProductID " + 
 "  " + 
 " LEFT JOIN " + 
 " ( " + 
 " 	/*BEGIN ********************RETURN TO VENDOR ***************************************************/ " + 
 " 	select  " + 
 " 	RETURNMASTER.PurchaseOrderMasterID as [PurchaseOrderMasterID], " + 
 " 	RETURNDETAIL.ProductID as [ProductID], " + 
 " 	Sum(IsNull(RETURNDETAIL.Quantity , 0)) as [ReturnQuantity] " + 
 " 	 " + 
 " 	from PO_ReturnToVendorMaster as RETURNMASTER " + 
 " 	join PO_ReturnToVendorDetail as RETURNDETAIL " + 
 " 	on RETURNMASTER.ReturnToVendorMasterID = RETURNDETAIL.ReturnToVendorMasterID  " + 
 " 	and RETURNMASTER.CCNID = @pstrCCNID " + 
 " 	 " + 
 " 	Group by " + 
 " 	RETURNMASTER.PurchaseOrderMasterID , " + 
 " 	RETURNDETAIL.ProductID " + 
 " 	/*END **********************RETURN TO VENDOR ***************************************************/ " + 
 "  " + 
 " ) as RETURNTABLE " + 
 " on POTABLE.PurchaseOrderMasterID = RETURNTABLE.PurchaseOrderMasterID " + 
 " and POTABLE.ProductID = RETURNTABLE.ProductID " + 
 "  " + 
 "  " + 
 " WHERE	POTABLE.CCNID = @pstrCCNID  " + 
((PurchaseOrderManagementReport.pstrPurchaseOrderID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and POTABLE.PurchaseOrderMasterID in (" +PurchaseOrderManagementReport.pstrPurchaseOrderID_LIST+ ") /*PurchaseOrderMasterID_LIST*/	 ")) + 
((PurchaseOrderManagementReport.pstrPartyID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and POTABLE.PartyID in (" +PurchaseOrderManagementReport.pstrPartyID_LIST+ ") /*PartyIDLIST  MakerIDLIST*/ ")) + 
((PurchaseOrderManagementReport.pstrProductID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and POTABLE.ProductID in (" +PurchaseOrderManagementReport.pstrProductID_LIST+ ") /*ProductID_LIST*/ ")) + 
		" "
;
				string strSQL_POLIST = " select POTABLE.Code from PO_PurchaseOrderMaster  as POTABLE where 1=1 and POTABLE.PurchaseOrderMasterID in (" +PurchaseOrderManagementReport.pstrPurchaseOrderID_LIST + ")";
				string strSQL_PARTYLIST = " select PARTYTABLE.Code from MST_Party  as PARTYTABLE where 1=1 and PARTYTABLE.PartyID in (" +PurchaseOrderManagementReport.pstrPartyID_LIST + ")";
				string strSQL_PRODUCTLIST = " select PRODUCTTABLE.Code from ITM_Product as PRODUCTTABLE where 1=1 and PRODUCTTABLE.ProductID in (" +PurchaseOrderManagementReport.pstrProductID_LIST + ")";

				if(PurchaseOrderManagementReport.pstrPurchaseOrderID_LIST == string.Empty)
				{					
					strSQL_POLIST = "select null where 0=1";
				}
				if(PurchaseOrderManagementReport.pstrPartyID_LIST == string.Empty)
				{
					strSQL_PARTYLIST = "select null where 0=1";
				}
				if(PurchaseOrderManagementReport.pstrProductID_LIST == string.Empty)
				{
					strSQL_PRODUCTLIST = "select null where 0=1";
				}

				strSql += strSQL_POLIST;
				strSql += strSQL_PARTYLIST;
				strSql += strSQL_PRODUCTLIST;

				
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

			mdtbPurchaseOrderList = mdst_MAIN_DATA_REPOSITORY.Tables[0];

			return true;
		}
		
		#endregion GET DATA and CACHE TO INNER DATA REPOSITORY (Dataset and DataTable)


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


		#region      INNER DATA (process on the mdtbPurchaseOrderList) MANIPULATE FUNCTION		

		public DataRow GetPLRow(string pstrPLCode)
		{
			foreach(DataRow drow in mdtbPurchaseOrderList.Rows)
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
			foreach(DataRow drow in mdtbPurchaseOrderList.Rows)
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
			foreach(DataRow drow in mdtbPurchaseOrderList.Rows)
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
			foreach(DataRow drow in mdtbPurchaseOrderList.Rows)
			{
				if((int)drow[RC.PRODUCTIONLINEID] == pintID  )
				{
					drow[pstrColummName] = pobjValue;
					return true;
				}
			}
			
			return false;
		}

		#endregion      INNER DATA MANIPULATE FUNCTION

	}	// end class DataReportHelper


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

		public static string PPM = "PPM";

		public static string STANDARD = "Standard";
		public static string COMMENT = "Comment";
	}

}

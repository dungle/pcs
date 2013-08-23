using System;
using System.Collections;
using System.Collections.Specialized;

using System.Drawing;
using System.Drawing.Imaging;

using System.Data;
using System.Data.OleDb;

using System.Reflection;
using System.Threading;

using System.ComponentModel;
using System.Windows.Forms;

using Utils = PCSComUtils.DataAccess.Utils;
using PCSUtils.Utils;

using C1.Win.C1Preview;
using C1.C1Report;
using C1.Win.C1Chart;
using Section = C1.C1Report.Section;

using System.IO;


//using Microsoft.Office.Interop;
//using Range = Microsoft.Office.Interop.Excel.Range;
//using Excel = Microsoft.Office.Interop.Excel;

namespace VATReportClassifiedByProducts
{
	/// <summary>	
	/// <author>Thachnn 06062006</author>
	/// This report use Report and SubReport render engine from ReportBuilder. It's similar to WorkingSchemeReport.
	/// IN LAYOUT ASPECT: This report has 2 part: 
	///		detail part: list all Shipping
	///		[page break]
	///		master part: group by Customer
	///	IN DATA ASPECT: We use GetDataAndCache function to get all data, transform it to 2 DataTable, and then, paste into the ReportWithSubReport render engine
	///	
	/// </summary>
	[Serializable]
	public class VATReportClassifiedByProducts : MarshalByRefObject, IDynamicReport
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

		public VATReportClassifiedByProducts()
		{
			
		}


		#region GLOBAL CONSTANT
		
		const string DETAIL_TABLE_NAME = "VATReportClassifiedByProducts";
		const string MASTER_TABLE_NAME = "TotalByCustomerReport";
		
		string WOBOM_TABLE_NAME = "WOBOMTable";
		string BEGINQUANTITY_TABLE_NAME = "BEGINQUANTITYTable";	


		string REPORT_NAME = "VATReportClassifiedByProducts";
		const string SUB_REPORT_NAME = "TotalByCustomerReport";
		string REPORT_LAYOUT_FILE = "VATReportClassifiedByProducts.xml";			

		short COPIES = 1;

		const string ENDSTOCK = "EndStock";
		const string CHANGECATEGORY = "Change Category";
		const string LEADTIME = "Lead Time";
		const string REQUIRECAPACITY = "Require Capacity";
		const string STANDARDCAPACITY = "Standard Capacity";
		const string COMPARESECOND = "Compare Second";
		const string COMPAREPERCENT = "Compare Percent";


		const string REPORTFLD_WORKINGDAYS				= "fldParameterWorkingdays";
		const string REPORTFLD_OFFDAYS				= "fldParameterOffdays";

		const string REPORTFLD_PARAMETER_CCN				= "fldParameterCCN";
		const string REPORTFLD_PARAMETER_YEAR				= "fldParameterYear";			
		const string REPORTFLD_PARAMETER_MONTH				= "fldParameterMonth";
		const string REPORTFLD_PARAMETER_CODE				= "fldParameterCode";	


		const string THIS = "ExternalReportFile:ProductionLineDeliveryProgressReport";
		const string METHOD_NAME = THIS + ".ExecuteReport()";

		
		
		const string ZERO_STRING = "0";
		const string ASSESSMENT_OK = "O";
		const string ASSESSMENT_NG = "X";
		const string MONTH_DATE_FORMAT = "MMM";
		
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


		
		#endregion GLOBAL CONSTANT

		#region GLOBAL VAR
		DataSet dstMAIN = new DataSet();	
		#endregion GLOBAL VAR

		/// <summary>
		/// Main flow function, generate the result data Table for the REPORT VIEWER		
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrCode"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrCode, string pstrMakeItem)
		{
			#region My varriables
			
			int nCCNID = int.Parse(pstrCCNID);
			int nYear = int.Parse(pstrYear);			
			int nMonth = int.Parse(pstrMonth);
			string strCode = pstrCode;						
//			string strCCN = string.Empty;			

			int intMakeItem = -1;
			if (pstrMakeItem != null && pstrMakeItem != string.Empty)
				intMakeItem = Convert.ToInt32(Convert.ToBoolean(pstrMakeItem));
			dstMAIN = GetDataAndCache(pstrCCNID, pstrYear, pstrMonth, intMakeItem);
			dstMAIN.DataSetName = pstrCCNID + pstrYear + pstrMonth + pstrCode;

			System.Data.DataTable dtbDetailTable  ;
			dtbDetailTable  = dstMAIN.Tables[DETAIL_TABLE_NAME];
			System.Data.DataTable dtbMasterTable  ;
			dtbMasterTable  = dstMAIN.Tables[MASTER_TABLE_NAME];		
			
//			dtbPlanTable = ModifyPlanTable(dtbPlanTable, pstrCCNID, pstrYear, pstrMonth, pstrProductionLineID);
//			dtbPlanTable = SumAndGroupBy(dtbPlanTable, PRODUCTID, PLAN + DATE, PLAN + QUANTITY);
			
//			System.Data.DataTable dtbSOTable;
//			dtbSOTable  = dstMAIN.Tables[SO_TABLE_NAME];

			#endregion My varriables
	
			#region	GETTING THE PARAMETER
			PCSComUtils.Common.BO.UtilsBO boUtil = new PCSComUtils.Common.BO.UtilsBO();
			PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO objBO = new PCSComUtils.Framework.ReportFrame.BO.C1PrintPreviewDialogBO();
//			strCCN = boUtil.GetCCNCodeFromID(nCCNID);

			#endregion GETTING THE PARAMETER
						
			// ## 1 ## TRANSFORM DATATABLE FOR REPORT

			#region RENDER REPORT
	
			ReportWithSubReportBuilder objRB = new ReportWithSubReportBuilder();

			objRB.ReportName = REPORT_NAME;				
			objRB.SourceDataTable = dtbDetailTable;			
			objRB.SubReportDataSources.Add(SUB_REPORT_NAME, dtbMasterTable);
			
			objRB.ReportDefinitionFolder = mstrReportDefinitionFolder;
			objRB.ReportLayoutFile = REPORT_LAYOUT_FILE;			
			objRB.UseLayoutFile = true;
			objRB.MakeDataTableForRender();

			// and show it in preview dialog				
			PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();		
			
			//Attach report viewer
			objRB.ReportViewer = printPreview.ReportViewer;
			objRB.RenderReport();

            
			#region MODIFY THE REPORT LAYOUT

			#region PUSH PARAMETER VALUE
			// objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CCN,strCCN);
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_YEAR, nYear.ToString("0000"));
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_MONTH, nMonth.ToString("00"));						
			objRB.DrawPredefinedField(REPORTFLD_PARAMETER_CODE, strCode);	
			#endregion
			
			#endregion	
					
			objRB.RefreshReport();
				
			/// force the copies number
			printPreview.FormTitle = objRB.GetFieldByName("fldTitle").Text;
			printPreview.Show();
			#endregion

			UseReportViewerRenderEngine = false;
			mResult = dtbDetailTable;			
			return dtbDetailTable;
		}


		/// <summary>
		/// Get all data for this report and cache in the dstMAIN dataset
		/// just improve the speed for this report		
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrCode"></param>
		/// <returns></returns>
		private DataSet GetDataAndCache(string pstrCCNID, string pstrYear, string pstrMonth, int pintMakeItem)
		{	
			DataSet dstRET = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			#region MAIN SQL QUERY
				
			string strSql = 
				" Declare @pstrCCNID int " + 
				" Declare @pstrMonth char(2) " + 
				" Declare @pstrYear char(4) " + 	
				"  " + 				
				" /*-----------------------------------*/ " + 
				"  " + 
				" Set @pstrCCNID = " + pstrCCNID + " " + 
				" Set @pstrYear = '" + pstrYear + "' " + 
				" Set @pstrMonth = '"+ pstrMonth +"' " + 				
				
				"  " ;
			/*-----------------------------------*/

			#endregion MAIN QUERY
			

			#region DETAIL TABLE - MAIN
			
			string strSqlDETAIL = " select " + 
				" KyHieuHoaDon, " + 
				" So, " + 
				" Ngay, " + 
				" TenNguoiMua, " + 
				" MaSoThueNguoiMua, " + 
				" MatHang, " + 
				" Sum(IsNull(DoanhSoBan, 0.00) ) as [DoanhSoBan], " + 
				" ThueSuatVAT, " + 
				" Sum(IsNull(ThueGTGT, 0.00) ) as [ThueGTGT], " + 
				" GhiChu " + 
				"  " + 
				" from  " + 
				" (	/*start group by section */ " + 
				" 	SELECT " + 
				" 	SHIPMASTER.ShipCode as [KyHieuHoaDon], " + 
				" 	SHIPMASTER.InvoiceNo as [So], " + 
				" 	SHIPMASTER.ShippedDate as [Ngay], " + 
				" 	PARTY.Name as [TenNguoiMua], " + 
				" 	PARTY.VATCode as [MaSoThueNguoiMua], " + 
				" 	SHIPMASTER.Comment as [MatHang], " + 
				" 	SHIPDETAIL.InvoiceQty * SHIPDETAIL.Price * SHIPMASTER.ExchangeRate as [DoanhSoBan], " + 
				" 	PRODUCT.VAT as [ThueSuatVAT], " + 
				" 	SHIPDETAIL.VatAmount * SHIPMASTER.ExchangeRate as [ThueGTGT], " + 
				" 	Case SO_Type.Code  " + 
				" 	When 2 then 'Hàng XK' " + 
				" 	else '' " + 
				" 	end as [GhiChu]  " + 
				" 	 " + 
				" 	FROM " + 
				" 	SO_ConfirmShipMaster as SHIPMASTER " + 
				" 	join SO_SaleOrderMaster as SOMASTER " + 
				" 	on SHIPMASTER.SaleOrderMasterID = SOMASTER.SaleOrderMasterID " + 
				" 	and SHIPMASTER.CCNID = @pstrCCNID " + 
				" 	and DatePart(yyyy, SHIPMASTER.ShippedDate) = @pstrYear " + 
				" 	and DatePart(mm, SHIPMASTER.ShippedDate) = @pstrMonth " + 
				" 	 " + 
				" 	join SO_ConfirmShipDetail as SHIPDETAIL " + 
				" 	on SHIPMASTER.ConfirmShipMasterID = SHIPDETAIL.ConfirmShipMasterID " + 
				" 	join MST_Party as PARTY " + 
				" 	on SOMASTER.PartyID = PARTY.PartyID " + 
				" 	join ITM_Product as PRODUCT " + 
				" 	on SHIPDETAIL.ProductID = PRODUCT.ProductID ";
			if (pintMakeItem >= 0)
				strSqlDETAIL += " AND PRODUCT.MakeItem = " + pintMakeItem;
			strSqlDETAIL += " 	left join SO_Type " + 
				" 	on SOMASTER.TypeID = SO_Type.TypeID " + 
				"  " + 
				" ) as DETAILDATA /* end group by section */ " + 
				"  " + 
				" Group by " + 
				" KyHieuHoaDon, " + 
				" So, " + 
				" Ngay, " + 
				" TenNguoiMua, " + 
				" MaSoThueNguoiMua, " + 
				" MatHang, " + 
				" ThueSuatVAT, " + 
				" GhiChu " + 
				"  " + 
				" order by Ngay, So, TenNguoiMua, MatHang, DoanhSoBan ";

			#endregion DETAIL - MAIN
			/* ============================================================== */

			#region MASTER TOTAL 
			string strSqlMASTER =  "select 'TEST KhachHang' as KhachHang, 'TEST TongTriGia' as TongTriGia"  ;

			#endregion MASTER TOTAL 

			/* ============================================================== */
				
			try 
			{
				
				oconPCS = null;
				ocmdPCS = null;
			
				strSql += 
					strSqlDETAIL + "\n" +  					
					strSqlMASTER + "\n" 
					;
	

				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstRET);

				dstRET.Tables[0].TableName = DETAIL_TABLE_NAME;
				dstRET.Tables[1].TableName = MASTER_TABLE_NAME;
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
		}	// end GetDataAndCache function
	}
}

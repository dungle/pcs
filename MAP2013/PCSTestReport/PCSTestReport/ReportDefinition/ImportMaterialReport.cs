using System;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;

using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace ImportMaterialReport
{
	[Serializable]
	public class ImportMaterialReport : MarshalByRefObject, IDynamicReport
	{
		public ImportMaterialReport()
		{
		}

		#region Constants
		
		private const string TABLE_NAME = "tbl_OutsideProcessing";	

		private const string NAME_FLD = "Name";
		private const string EXCHANGE_RATE_FLD = "ExchangeRate";		
		private const string PRODUCT_ID_FLD = "ProductID";
		private const string PRODUCT_CODE_FLD = "Code";
		private const string PRODUCT_NAME_FLD = "Description";
		
		
		#endregion		

		#region IDynamicReport Members
		
		private bool mUseReportViewerRenderEngine = false;
		private string mConnectionString;
		private ReportBuilder mReportBuilder;
		private C1PrintPreviewControl mReportViewer;

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
		/// this IDynamicReport or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get { return mUseReportViewerRenderEngine; }
			set { mUseReportViewerRenderEngine = value; }
		}

		private string mstrReportDefinitionFolder = string.Empty;
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get { return mstrReportDefinitionFolder; }
			set { mstrReportDefinitionFolder = value; }
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

		#endregion
		
		#region Detailed Item Price By PO Receipt Datat: Tuan TQ
				
		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetHomeCurrency(string pstrCCNID)
		{			
			const string HOME_CURRENCY_FLD = "HomeCurrencyID";

			OleDbConnection oconPCS = null;

			try
			{
				string strResult = string.Empty;
				OleDbDataReader odrPCS = null;				
				
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + HOME_CURRENCY_FLD;
				strSql += " FROM MST_CCN";
				strSql += " WHERE CCNID = " + pstrCCNID;
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[HOME_CURRENCY_FLD].ToString().Trim();
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		DataTable GetImportMaterialReport(string pstrCCNID,
			string pstrFromDate,
			string pstrToDate,
			string pstrCurrencyIDList
			)
		{
			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
            StringBuilder strSqlBuilder = new StringBuilder();

			strSqlBuilder.Append("SELECT DISTINCT PO_InvoiceMaster.PostDate, ");
			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceNo, ");
			strSqlBuilder.Append(" MST_Currency.Code as CurrencyCode, ");
			strSqlBuilder.Append(" PO_InvoiceMaster.ExchangeRate, ");
			strSqlBuilder.Append(" MST_Party.Code AS PartyCode, ");
			strSqlBuilder.Append(" MST_Party.Name as PartyName, ");

			strSqlBuilder.Append(" ITM_Product.Code AS PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, "); 
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as InvoiceUM, ");
			strSqlBuilder.Append(" ITM_Category.Code AS CategoryCode, ");
			strSqlBuilder.Append(" PO_InvoiceDetail.UnitPrice, ");

			strSqlBuilder.Append(" SUM(PO_InvoiceDetail.InvoiceQuantity) as InvoiceQuantity, ");
			
			strSqlBuilder.Append(" SUM(PO_InvoiceDetail.VATAmount) as VATAmount, ");
			strSqlBuilder.Append(" SUM(PO_InvoiceDetail.ImportTaxAmount) as ImportTaxAmount, ");
			strSqlBuilder.Append(" SUM(PO_InvoiceDetail.Inland) as Inland, ");
			strSqlBuilder.Append(" SUM(PO_InvoiceDetail.CIFAmount) as CIFAmount, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT 	ISNULL(SUM(cst_FreightDetail.Amount), 0)  ");
			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append("  WHERE PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("      AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("      AND cst_FreightMaster.ACPurposeID = 3 ");
			strSqlBuilder.Append(" ) as CreditAmount, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT 	ISNULL(SUM(cst_FreightDetail.VATAmount), 0)  ");
			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" WHERE PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("       AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("       AND cst_FreightMaster.ACPurposeID = 3 ");
			strSqlBuilder.Append(" ) as CreditVATAmount, ");
			
			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT 	ISNULL(SUM(cst_FreightDetail.Amount), 0) ");
			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" WHERE PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("       AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("       AND cst_FreightMaster.ACPurposeID = 4 ");
			strSqlBuilder.Append(" ) as DebitAmount, ");

			strSqlBuilder.Append(" ( ");
			strSqlBuilder.Append(" SELECT 	ISNULL(SUM(cst_FreightDetail.VATAmount), 0) ");
			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID ");
			strSqlBuilder.Append(" 	INNER JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" WHERE PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID ");
			strSqlBuilder.Append("       AND cst_FreightDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append("       AND cst_FreightMaster.ACPurposeID = 4 ");
			strSqlBuilder.Append(" ) as DebitVATAmount ");

			strSqlBuilder.Append(" FROM    PO_InvoiceMaster ");
			strSqlBuilder.Append(" INNER JOIN MST_Currency  ON MST_Currency.CurrencyID = PO_InvoiceMaster.CurrencyID ");
			strSqlBuilder.Append(" INNER JOIN MST_Party ON PO_InvoiceMaster.PartyID = MST_Party.PartyID ");
			strSqlBuilder.Append(" INNER JOIN PO_InvoiceDetail ON PO_InvoiceMaster.InvoiceMasterID = PO_InvoiceDetail.InvoiceMasterID ");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON PO_InvoiceDetail.ProductID = ITM_Product.ProductID ");
			strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON MST_UnitOfMeasure.UnitOfMeasureID = PO_InvoiceDetail.InvoiceUMID ");
			strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID"); 
			
			strSqlBuilder.Append(" WHERE PO_InvoiceMaster.CCNID = " + pstrCCNID);

			//From date
			if(pstrFromDate != string.Empty && pstrFromDate != null)
			{
				strSqlBuilder.Append("      AND PO_InvoiceMaster.PostDate >= '" + pstrFromDate + "'");
			}

			//To date
			if(pstrToDate != string.Empty && pstrToDate != null)
			{
				strSqlBuilder.Append("      AND PO_InvoiceMaster.PostDate <= '" + pstrToDate + "'");
			}			

			//Currency List
			if(pstrCurrencyIDList != string.Empty && pstrCurrencyIDList != null)
			{
				strSqlBuilder.Append("      AND PO_InvoiceMaster.CurrencyID IN (" + pstrCurrencyIDList + ")");
			}

			strSqlBuilder.Append(" GROUP BY ");
			strSqlBuilder.Append(" PO_InvoiceMaster.PostDate,");
			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceMasterID,");
			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceNo,");
			strSqlBuilder.Append(" MST_Currency.Code,");
			strSqlBuilder.Append(" PO_InvoiceMaster.ExchangeRate,");
			strSqlBuilder.Append(" MST_Party.Code,");
			strSqlBuilder.Append(" MST_Party.Name,");
			strSqlBuilder.Append(" PO_InvoiceDetail.UnitPrice,");
			strSqlBuilder.Append(" ITM_Product.ProductID,");
			strSqlBuilder.Append(" ITM_Product.Code,");
			strSqlBuilder.Append(" ITM_Product.Description, ");
			strSqlBuilder.Append(" ITM_Product.Revision,");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code,");
			strSqlBuilder.Append(" ITM_Category.Code");

			//Write SQL string for debugging
			Console.WriteLine(strSqlBuilder.ToString());
			
			oconPCS = new OleDbConnection(mConnectionString);
			ocmdPCS = new OleDbCommand(strSqlBuilder.ToString(), oconPCS);

			ocmdPCS.CommandTimeout = 1000;
			ocmdPCS.Connection.Open();
			OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
			odadPCS.Fill(dtbResult);

			return dtbResult;
		}		
		
		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCompanyFullName()
		{			
			const string FULL_COMPANY_NAME = "CompanyFullName";
			OleDbConnection oconPCS = null;

			try
			{
				string strResult = string.Empty;
				OleDbDataReader odrPCS = null;				
				
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT [Value]"
					+ " FROM Sys_Param"
					+ " WHERE [Name] = '" + FULL_COMPANY_NAME + "'";
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS["Value"].ToString().Trim();
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;
				}
			}
		}


		
		/// <summary>
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCCNInfoByID(string pstrID)
		{
			string strResult = string.Empty;
			OleDbConnection oconPCS = null;
			
			try
			{
				OleDbDataReader odrPCS = null;
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE_FLD + ", " + PRODUCT_NAME_FLD
					+ " FROM MST_CCN"
					+ " WHERE MST_CCN.CCNID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + " (" + odrPCS[PRODUCT_NAME_FLD].ToString().Trim() + ")";
					}
				}

				return strResult;
			
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}
		
		/// <summary>
		/// Get PO Receipt Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetPOReceiptType(string pstrID)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Description as " + PRODUCT_NAME_FLD;
				strSql +=	" FROM enm_POReceiptType";
				strSql +=	" WHERE POReceiptTypeCode = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[PRODUCT_NAME_FLD].ToString().Trim();
					}
				}
			
				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}
		
		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCurrencyInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code";
				strSql += " FROM MST_Currency";
				strSql += " WHERE CurrencyID IN (" +  pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > 250)				
				{
					int i = strResult.IndexOf(SEMI_COLON, 250);
					if(i > 0)
					{
						strResult = strResult.Substring(0, i + SEMI_COLON.Length) + "...";
					}
				}

				return strResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
					oconPCS = null;

				}
			}
		}	
		
		#endregion Delivery To Customer Schedule Report: Tuan TQ
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}		
		
		/// <summary>
		/// Build and show Detai lItem Price By PO Receipt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tuan TQ, 11 Apr, 2006</author>
		public DataTable ExecuteReport(string pstrCCNID,
			string pstrFromDate,
			string pstrToDate,
			string pstrCurrencyIDList
			)
		{
			try
			{
				string strPOTypeID = Convert.ToString((int)PCSComUtils.Common.POReceiptTypeEnum.ByInvoice);

				const string DATE_HOUR_FORMAT = "dd-MM-yyyy HH:mm";
				const string NUMERIC_FORMAT = "#,##0.00";

				const string REPORT_TEMPLATE = "ImportMaterialReport.xml";
				const string RPT_PAGE_HEADER = "PageHeader";

				const string REPORT_NAME = "ImportMaterialReport";
				const string RPT_TITLE_FLD = "fldTitle";
				const string RPT_COMPANY_FLD = "fldCompany";
				
				const string RPT_CCN_FLD = "CCN";				
				const string RPT_FROM_DATE_FLD = "From Date";
				const string RPT_TO_DATE_FLD = "To Date";
				const string RPT_CURRENCY_FLD = "Currency";				
				
				DataTable dtbDataSource = null;

				dtbDataSource = GetImportMaterialReport(pstrCCNID, pstrFromDate, pstrToDate, pstrCurrencyIDList);

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();				
								
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;

				//Set Datasource
				reportBuilder.SourceDataTable = dtbDataSource;
								
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE;				
				
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();

				// and show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
								
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();

				reportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, GetCompanyFullName());
				//Draw parameters
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				
				arrParamAndValue.Add(RPT_CCN_FLD, GetCCNInfoByID(pstrCCNID));

				if(pstrFromDate != null && pstrFromDate != string.Empty)
				{
					arrParamAndValue.Add(RPT_FROM_DATE_FLD, DateTime.Parse(pstrFromDate).ToString(DATE_HOUR_FORMAT));
				}

				if(pstrToDate != null && pstrToDate != string.Empty)
				{
					arrParamAndValue.Add(RPT_TO_DATE_FLD, DateTime.Parse(pstrToDate).ToString(DATE_HOUR_FORMAT));
				}

				if(pstrCurrencyIDList != null && pstrCurrencyIDList != string.Empty)
				{
					arrParamAndValue.Add(RPT_CURRENCY_FLD, GetCurrencyInfo(pstrCurrencyIDList));
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);

				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD).Text;
				}
				catch
				{}

				reportBuilder.RefreshReport();
				printPreview.Show();

				//return table
				return dtbDataSource;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		#endregion Public Method			
	}
}

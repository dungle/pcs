using System;
using System.Diagnostics;
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

namespace ReceivingMaterialReport
{
	[Serializable]
	public class ReceivingMaterialReport : MarshalByRefObject, IDynamicReport
	{
		public ReceivingMaterialReport()
		{
		}

		#region Constants
		
		private const string TABLE_NAME = "tbl_OutsideProcessing";	

		private const string NAME_FLD = "Name";
		private const string EXCHANGE_RATE_FLD = "ExchangeRate";		
		private const string PRODUCT_ID_FLD = "ProductID";
		private const string PRODUCT_CODE_FLD = "Code";
		private const string PRODUCT_NAME_FLD = "Description";

		private const string LOCAL_STRING = "Local";
		private const string FOREIGN_STRING = "Foreign";
		
		
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
		
		#region Receiving Material Report: Tuan TQ
				
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
				DataTable dtb = new DataTable();				
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

		DataTable GetReceivingMaterialReportData(string pstrCCNID, 
			string pstrMasLocID,
			string pstrFromDate, 
			string pstrToDate,
			string pstrProductIDList,
			string pstrCurrencyID,
			string pstrExchangeRate, 
			string pstrHomeCurrency,
			string pstrVendorType,
			string pstrVendorIDList)
		{		

			DataTable dtbResult = new DataTable();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			
            //Freight exchange rate
			string strFreightExchangeRate = " ((";
			strFreightExchangeRate += "SELECT ISNULL(freight.ExchangeRate, 1) as ExchangeRate";
			strFreightExchangeRate += " FROM cst_FreightMaster freight WHERE freight.FreightMasterID = cst_FreightMaster.FreightMasterID";
			strFreightExchangeRate += " AND freight.ACPurposeID = " + (int)ACPurpose.Freight;
			strFreightExchangeRate += ")";
			
			//PO exchange rate
			string strPOExchangeRate = " ((";
			strPOExchangeRate += "SELECT ISNULL(po.ExchangeRate, 1) as ExchangeRate";
			strPOExchangeRate += " FROM PO_PurchaseOrderMaster po WHERE po.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID ";
			strPOExchangeRate += ")";

			//Invoice exchange rate
			string strInvoiceExchangeRate = " ((";
			strInvoiceExchangeRate += "SELECT ISNULL(invoice.ExchangeRate, 1) as ExchangeRate";
			strInvoiceExchangeRate += " FROM PO_InvoiceMaster invoice WHERE invoice.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID";
			strInvoiceExchangeRate += ")";
			
			if(pstrExchangeRate != string.Empty && pstrExchangeRate != null)
			{
				if(pstrCurrencyID != pstrHomeCurrency)
				{
					//Freight
					strFreightExchangeRate += @"  /";
					strFreightExchangeRate += pstrExchangeRate;

					//PO 
					strPOExchangeRate += @"  /";
					strPOExchangeRate += pstrExchangeRate;

					//Invoice
					strInvoiceExchangeRate += @"  /";
					strInvoiceExchangeRate += pstrExchangeRate;
				}
			}
			else
			{
				//Exchange rate of view currency				
				string strViewExchangeRate = string.Empty;				
				strViewExchangeRate += " (SELECT ISNULL( (SELECT TOP 1 ISNULL(Rate, 1)";
				strViewExchangeRate += "       FROM MST_ExchangeRate rate";
				strViewExchangeRate += "       WHERE rate.Approved = 1";
				strViewExchangeRate += "             AND rate.CurrencyID = " + pstrCurrencyID;
				strViewExchangeRate += "       ORDER BY BeginDate DESC), 1)";
				strViewExchangeRate += "  )";
				
				//Freight
				strFreightExchangeRate += @"  /";
				strFreightExchangeRate += strViewExchangeRate;

				//PO 
				strPOExchangeRate += @"  /";
				strPOExchangeRate += strViewExchangeRate;

				//Invoice
				strInvoiceExchangeRate += @"  /";
				strInvoiceExchangeRate += strViewExchangeRate;

			}

			strFreightExchangeRate += " )";
			strPOExchangeRate += " )";
			strInvoiceExchangeRate += " )";

			StringBuilder strSqlBuilder = new StringBuilder();
			
			strSqlBuilder.Append("DECLARE @CurrencyID AS INT SET @CurrencyID = " + pstrCurrencyID);
			strSqlBuilder.Append(" SELECT a.* FROM (");
			strSqlBuilder.Append(" SELECT 	DISTINCT ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID, ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.PostDate, ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.ReceiveNo, ");

			strSqlBuilder.Append(" PO_InvoiceMaster.InvoiceNo, ");
			strSqlBuilder.Append(" PO_PurchaseOrderMaster.Code as PONo,		 ");
			strSqlBuilder.Append(" InvoiceCurrency.Code as CurrencyCode, ");
			strSqlBuilder.Append(" InvoiceParty.Code as PartyCode,	 ");
			strSqlBuilder.Append(" InvoiceParty.Name as PartyName, ");
			strSqlBuilder.Append(" InvoiceParty.PartyID, ");

			strSqlBuilder.Append(" Case When InvoiceParty.CountryID = MST_CCN.CountryID then 'Local' ");
			strSqlBuilder.Append("      Else 'Foreign'   ");      
			strSqlBuilder.Append(" End as PartyType, ");
			strSqlBuilder.Append(" ITM_Product.ProductID, ");
			strSqlBuilder.Append(" ITM_Product.Code AS PartNo, ");
			strSqlBuilder.Append(" ITM_Product.Description as PartName, ");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel, ");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as BuyingUM, ");
			strSqlBuilder.Append(" ITM_Category.Code as CategoryCode, ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity,	 ");

			strSqlBuilder.Append(" MST_Location.Code AS LocationCode, ");
			strSqlBuilder.Append(" MST_BIN.Code AS BinCode, ");
			
			strSqlBuilder.Append(" PO_InvoiceDetail.UnitPrice,");
			strSqlBuilder.Append(" PO_InvoiceDetail.VAT,");
			strSqlBuilder.Append(" (CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN PO_InvoiceDetail.VATAmount ");
			strSqlBuilder.Append(" ELSE PO_InvoiceDetail.VATAmount * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate
			strSqlBuilder.Append(") as VATAmount, ");

			strSqlBuilder.Append(" CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN PO_InvoiceDetail.CIFAmount ");
			strSqlBuilder.Append(" ELSE (PO_InvoiceDetail.CIFAmount) * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate
			strSqlBuilder.Append(" as CIFAmount, ");

			strSqlBuilder.Append(" CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN PO_InvoiceDetail.Inland ");
			strSqlBuilder.Append(" ELSE ISNULL(PO_InvoiceDetail.Inland, 0) * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate
			strSqlBuilder.Append(" as InlandCost, ");

			strSqlBuilder.Append(" (CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN (ISNULL(PO_InvoiceDetail.CIFAmount, 0) + ISNULL(PO_InvoiceDetail.Inland, 0) )");
			strSqlBuilder.Append(" ELSE (ISNULL(PO_InvoiceDetail.CIFAmount, 0) + ISNULL(PO_InvoiceDetail.Inland, 0) ) * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END) ");
			//End-Exchange rate			
			strSqlBuilder.Append(" as Amount,");

			strSqlBuilder.Append(" CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN PO_InvoiceDetail.ImportTaxAmount ");
			strSqlBuilder.Append(" ELSE ISNULL(PO_InvoiceDetail.ImportTaxAmount, 0) * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate
			strSqlBuilder.Append(" as ImportTax,	");
			
			strSqlBuilder.Append(" ISNULL(");
			strSqlBuilder.Append(" CASE	WHEN cst_FreightMaster.CurrencyID = @CurrencyID THEN cst_FreightDetail.Amount ");
			strSqlBuilder.Append(" ELSE cst_FreightDetail.Amount * ");
			//Exchange rate by Freight currency			
			strSqlBuilder.Append(strFreightExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate
			strSqlBuilder.Append(" 	, 0)");
			strSqlBuilder.Append(" as FreightAmount,");
			
			strSqlBuilder.Append(" (");

			strSqlBuilder.Append(" CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN (ISNULL(PO_InvoiceDetail.CIFAmount, 0) + ISNULL(PO_InvoiceDetail.Inland, 0) ) ");
			strSqlBuilder.Append(" ELSE (ISNULL(PO_InvoiceDetail.CIFAmount, 0) + ISNULL(PO_InvoiceDetail.Inland, 0) ) * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate

			strSqlBuilder.Append(" )");

			strSqlBuilder.Append(" +");

			strSqlBuilder.Append(" (");
			strSqlBuilder.Append(" CASE	WHEN PO_InvoiceMaster.CurrencyID = @CurrencyID THEN ISNULL(PO_InvoiceDetail.ImportTaxAmount, 0) ");
			strSqlBuilder.Append(" ELSE PO_InvoiceDetail.ImportTaxAmount * ");
			//Exchange rate by Invoice currency			
			strSqlBuilder.Append(strInvoiceExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate
			strSqlBuilder.Append("  )		 ");
			
			strSqlBuilder.Append(" + ");
			strSqlBuilder.Append(" (ISNULL(");
			strSqlBuilder.Append(" CASE	WHEN cst_FreightMaster.CurrencyID = @CurrencyID THEN cst_FreightDetail.Amount ");
			strSqlBuilder.Append(" ELSE cst_FreightDetail.Amount * ");
			//Exchange rate by Freight currency			
			strSqlBuilder.Append(strFreightExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate

			strSqlBuilder.Append(" , 0))");
			strSqlBuilder.Append(" as TotalAmount");

			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster");
			strSqlBuilder.Append(" INNER JOIN PO_PurchaseOrderReceiptDetail ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID	 ");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON ITM_Product.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");
			strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON PO_PurchaseOrderReceiptDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID");
			strSqlBuilder.Append(" INNER JOIN MST_MasterLocation ON PO_PurchaseOrderReceiptMaster.MasterLocationID = MST_MasterLocation.MasterLocationID ");
			strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");

			strSqlBuilder.Append(" LEFT JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID");
			// 17-8-2006 dungla: only retrieve data of freight transaction
			strSqlBuilder.Append(" AND cst_FreightMaster.ACPurposeID = " + (int)ACPurpose.Freight);
			strSqlBuilder.Append(" LEFT JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" 			       AND cst_FreightDetail.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");
			
//			strSqlBuilder.Append(" LEFT JOIN PO_ReturnToVendorMaster ON PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_ReturnToVendorMaster.InvoiceMasterID");
//			strSqlBuilder.Append(" LEFT JOIN PO_ReturnToVendorDetail ON PO_ReturnToVendorDetail.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID");
//			strSqlBuilder.Append(" AND PO_ReturnToVendorMaster.ReturnToVendorMasterID = PO_ReturnToVendorDetail.ReturnToVendorMasterID");

			strSqlBuilder.Append(" LEFT JOIN MST_Location ON PO_PurchaseOrderReceiptDetail.LocationID = MST_Location.LocationID	");
			strSqlBuilder.Append(" LEFT JOIN MST_BIN ON MST_BIN.BinID = PO_PurchaseOrderReceiptDetail.BinID");

			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID ");
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderDetail ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID ");

			strSqlBuilder.Append(" LEFT JOIN MST_CCN ON MST_CCN.CCNID = PO_PurchaseOrderReceiptMaster.CCNID");

			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceMaster ON PO_PurchaseOrderReceiptMaster.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID");
			strSqlBuilder.Append(" LEFT JOIN MST_Party InvoiceParty ON InvoiceParty.PartyID = PO_InvoiceMaster.PartyID");

			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster POInMaster ON PO_PurchaseOrderReceiptMaster.PurchaseOrderMasterID = POInMaster.PurchaseOrderMasterID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Party POParty ON POParty.PartyID = POInMaster.PartyID");

			strSqlBuilder.Append(" LEFT JOIN MST_Currency InvoiceCurrency ON InvoiceCurrency.CurrencyID = PO_InvoiceMaster.CurrencyID");
			strSqlBuilder.Append(" LEFT JOIN MST_Currency POCurrency ON POCurrency.CurrencyID = POInMaster.CurrencyID");

			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceDetail ON PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID");
			strSqlBuilder.Append(" 			   AND PO_InvoiceDetail.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");
			strSqlBuilder.Append(" 			   AND PO_InvoiceDetail.DeliveryScheduleID = PO_PurchaseOrderReceiptDetail.DeliveryScheduleID");

			strSqlBuilder.Append(" WHERE PO_PurchaseOrderReceiptMaster.InvoiceMasterID IS NOT NULL");


			strSqlBuilder.Append(" UNION");

			strSqlBuilder.Append(" SELECT 	DISTINCT");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID,");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.PostDate,");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptMaster.ReceiveNo,");

			strSqlBuilder.Append(" '' as InvoiceNo,");
			strSqlBuilder.Append(" POInMaster.Code as PONo,		");
			strSqlBuilder.Append(" POCurrency.Code as CurrencyCode,");
			strSqlBuilder.Append(" POParty.Code as PartyCode,	");
			strSqlBuilder.Append(" POParty.Name as PartyName,");
			strSqlBuilder.Append(" POParty.PartyID,");

			strSqlBuilder.Append(" Case When POParty.CountryID = MST_CCN.CountryID then 'Local'");
			strSqlBuilder.Append("          Else 'Foreign'");
			strSqlBuilder.Append("     End as PartyType,");
			strSqlBuilder.Append(" ITM_Product.ProductID,");
			strSqlBuilder.Append(" ITM_Product.Code AS PartNo,");
			strSqlBuilder.Append(" ITM_Product.Description as PartName,");
			strSqlBuilder.Append(" ITM_Product.Revision as PartModel,");
			strSqlBuilder.Append(" MST_UnitOfMeasure.Code as BuyingUM,");
			strSqlBuilder.Append(" ITM_Category.Code as CategoryCode,");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity,	");

			strSqlBuilder.Append(" MST_Location.Code AS LocationCode,");
			strSqlBuilder.Append(" MST_BIN.Code AS BinCode,");
			
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice,");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.VAT,");			
			
			strSqlBuilder.Append(" CASE	WHEN PO_PurchaseOrderMaster.CurrencyID = @CurrencyID THEN ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.VAT * 0.01 ");
			strSqlBuilder.Append(" ELSE PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.VAT * 0.01 * ");
			//Exchange rate by PO currency			
			strSqlBuilder.Append(strPOExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate			
			strSqlBuilder.Append(" as VATAmount,");

			strSqlBuilder.Append(" CASE	WHEN PO_PurchaseOrderMaster.CurrencyID = @CurrencyID THEN ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice ");
			strSqlBuilder.Append(" ELSE PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice * ");
			//Exchange rate by PO currency			
			strSqlBuilder.Append(strPOExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate			
			strSqlBuilder.Append(" as CIFAmount,	");

			strSqlBuilder.Append(" 0 as InlandCost,	");

			strSqlBuilder.Append(" CASE	WHEN PO_PurchaseOrderMaster.CurrencyID = @CurrencyID THEN ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice * ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity ");
			strSqlBuilder.Append(" ELSE PO_PurchaseOrderDetail.UnitPrice * ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");

			//Exchange rate by PO currency			
			strSqlBuilder.Append(strPOExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate

			strSqlBuilder.Append(" as Amount,");

			strSqlBuilder.Append(" 0 as ImportTax,	");

			strSqlBuilder.Append(" ISNULL(");
			strSqlBuilder.Append(" CASE	WHEN cst_FreightMaster.CurrencyID = @CurrencyID THEN ");
			strSqlBuilder.Append(" cst_FreightDetail.Amount ");
			strSqlBuilder.Append(" ELSE cst_FreightDetail.Amount *");

			//Exchange rate by Freight currency			
			strSqlBuilder.Append(strFreightExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate

			strSqlBuilder.Append(" , 0)	as FreightAmount,");
			
			strSqlBuilder.Append(" (");
			strSqlBuilder.Append(" CASE	WHEN PO_PurchaseOrderMaster.CurrencyID = @CurrencyID THEN ");
			strSqlBuilder.Append(" PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice ");
			strSqlBuilder.Append(" ELSE PO_PurchaseOrderReceiptDetail.ReceiveQuantity * ");
			strSqlBuilder.Append(" PO_PurchaseOrderDetail.UnitPrice * ");
			
			//Exchange rate by PO currency			
			strSqlBuilder.Append(strPOExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate			
			strSqlBuilder.Append(" )");

			strSqlBuilder.Append(" + ");

			strSqlBuilder.Append(" (ISNULL(");
			strSqlBuilder.Append(" CASE	WHEN cst_FreightMaster.CurrencyID = @CurrencyID THEN ");
			strSqlBuilder.Append(" cst_FreightDetail.Amount ");
			strSqlBuilder.Append(" ELSE cst_FreightDetail.Amount *");

			//Exchange rate by Freight currency			
			strSqlBuilder.Append(strFreightExchangeRate);
			strSqlBuilder.Append(" END ");
			//End-Exchange rate

			strSqlBuilder.Append(" , 0)");
			strSqlBuilder.Append(" )");
			strSqlBuilder.Append(" as TotalAmount");
			
			strSqlBuilder.Append(" FROM    PO_PurchaseOrderReceiptMaster");
			strSqlBuilder.Append(" INNER JOIN PO_PurchaseOrderReceiptDetail ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID	 ");
			strSqlBuilder.Append(" INNER JOIN ITM_Product ON ITM_Product.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");
			strSqlBuilder.Append(" INNER JOIN MST_UnitOfMeasure ON PO_PurchaseOrderReceiptDetail.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID");
			strSqlBuilder.Append(" INNER JOIN MST_MasterLocation ON PO_PurchaseOrderReceiptMaster.MasterLocationID = MST_MasterLocation.MasterLocationID ");
			strSqlBuilder.Append(" LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ");

			strSqlBuilder.Append(" LEFT JOIN cst_FreightMaster ON cst_FreightMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID");
			// 17-8-2006 dungla: only retrieve data of freight transaction
			strSqlBuilder.Append(" AND cst_FreightMaster.ACPurposeID = " + (int)ACPurpose.Freight);
			strSqlBuilder.Append(" LEFT JOIN cst_FreightDetail ON cst_FreightDetail.FreightMasterID = cst_FreightMaster.FreightMasterID ");
			strSqlBuilder.Append(" 			           AND cst_FreightDetail.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");

			strSqlBuilder.Append(" LEFT JOIN MST_Location ON PO_PurchaseOrderReceiptDetail.LocationID = MST_Location.LocationID	");
			strSqlBuilder.Append(" LEFT JOIN MST_BIN ON MST_BIN.BinID = PO_PurchaseOrderReceiptDetail.BinID");

			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID ");
			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderDetail ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID ");

			strSqlBuilder.Append(" LEFT JOIN MST_CCN ON MST_CCN.CCNID = PO_PurchaseOrderReceiptMaster.CCNID");

			strSqlBuilder.Append(" LEFT JOIN PO_PurchaseOrderMaster POInMaster ON PO_PurchaseOrderReceiptMaster.PurchaseOrderMasterID = POInMaster.PurchaseOrderMasterID ");
			strSqlBuilder.Append(" LEFT JOIN MST_Party POParty ON POParty.PartyID = POInMaster.PartyID");

			strSqlBuilder.Append(" LEFT JOIN MST_Currency POCurrency ON POCurrency.CurrencyID = POInMaster.CurrencyID");

			strSqlBuilder.Append(" LEFT JOIN PO_InvoiceDetail ON PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID");
			strSqlBuilder.Append(" 			   AND PO_InvoiceDetail.ProductID = PO_PurchaseOrderReceiptDetail.ProductID");
			strSqlBuilder.Append(" 			   AND PO_InvoiceDetail.DeliveryScheduleID = PO_PurchaseOrderReceiptDetail.DeliveryScheduleID");

			strSqlBuilder.Append(" WHERE PO_PurchaseOrderReceiptMaster.InvoiceMasterID IS NULL");

			// Close query and assign alias
			strSqlBuilder.Append(" ) a ");
			
			//Set default WHERE clause
			strSqlBuilder.Append(" WHERE 1 = 1 ");

			//From date
			if(pstrFromDate != string.Empty && pstrFromDate != null)
			{
				strSqlBuilder.Append("      AND a.PostDate >= '" + pstrFromDate + "'");
			}

			//To date
			if(pstrToDate != string.Empty && pstrToDate != null)
			{
				strSqlBuilder.Append("      AND a.PostDate <= '" + pstrToDate + "'");
			}			

			//Product List
			if(pstrProductIDList != string.Empty && pstrProductIDList != null)
			{
				strSqlBuilder.Append("      AND a.ProductID IN (" + pstrProductIDList + ")");
			}

			if(pstrVendorIDList != string.Empty && pstrVendorIDList != null)
			{
				strSqlBuilder.Append(" AND a.PartyID IN ( " + pstrVendorIDList + ")");
			}

			if(pstrVendorType != string.Empty && pstrVendorIDList != null)
			{
				strSqlBuilder.Append(" AND a.PartyType = '" + pstrVendorType.Replace("'", "''") + "'");				
			}

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
		private string GetProductInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code, Description";
				strSql += " FROM ITM_Product";
				strSql += " WHERE ProductID IN (" +  pstrIDList + ")";
				
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



		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetPartyInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code, Name";
				strSql += " FROM MST_Party";
				strSql += " WHERE PartyID IN (" +  pstrIDList + ")";
				
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

		
		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetMasterLocationInfoByID(string pstrID)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE_FLD + ", " + NAME_FLD
					+ " FROM MST_MasterLocation"
					+ " WHERE MasterLocationID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + " (" + odrPCS[NAME_FLD].ToString().Trim() + ")";
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
		private Hashtable GetCurrencyInfo(string pstrID)
		{
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT ISNULL(rate.Rate, 1) as " + EXCHANGE_RATE_FLD;
				strSql += " , currency.Code";
				strSql += " FROM MST_Currency currency";
				strSql += " LEFT JOIN  MST_ExchangeRate rate ON rate.CurrencyID = currency.CurrencyID AND rate.Approved = 1";
				strSql += " WHERE currency.CurrencyID = " + pstrID;
				strSql += " ORDER BY rate.BeginDate DESC";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Hashtable htbResult = new Hashtable();

				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						htbResult.Add(PRODUCT_CODE_FLD, odrPCS[PRODUCT_CODE_FLD]);
						htbResult.Add(EXCHANGE_RATE_FLD, odrPCS[EXCHANGE_RATE_FLD]);						
					}
				}
			
				return htbResult;
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
			string pstrMasLocID,
			string pstrFromDate, 
			string pstrToDate,			
			string pstrCurrencyID,
			string pstrExchangeRate, 			
			string pstrProductIDList,
			string pstrVendorType,
			string pstrVendorIDList
			)
		{
			string strPOTypeID = Convert.ToString((int)PCSComUtils.Common.POReceiptTypeEnum.ByInvoice);

			const string DATE_HOUR_FORMAT = "dd-MM-yyyy HH:mm";
			const string NUMERIC_FORMAT = "#,##0.00";

			const string REPORT_TEMPLATE_LOCAL = "ReceivingMaterialReport_Local.xml";
			const string REPORT_TEMPLATE = "ReceivingMaterialReport.xml";

			const string RPT_PAGE_HEADER = "PageHeader";

			const string REPORT_NAME = "ReceivingMaterialReport";
			const string RPT_TITLE_FLD = "fldTitle";
			const string RPT_COMPANY_FLD = "fldCompany";
				
			const string RPT_CCN_FLD = "CCN";
			const string RPT_MASTER_LOCATION_FLD = "Master Location";
			const string RPT_FROM_DATE_FLD = "From Date";
			const string RPT_TO_DATE_FLD = "To Date";				
			const string RPT_PART_NUMBER_FLD = "Part Number";
			const string RPT_CURRENCY_FLD = "Currency";
			const string RPT_EXCHANGE_RATE_FLD = "Exchange Rate";
			const string RPT_VENDOR_FLD = "Vendor";
			const string RPT_VENDOR_TYPE_FLD = "Vendor Type";
				
			DataTable dtbDataSource = null;	
				
			string strHomeCurrency = GetHomeCurrency(pstrCCNID);

			dtbDataSource = GetReceivingMaterialReportData(pstrCCNID, 
				pstrMasLocID, 																	
				pstrFromDate, 
				pstrToDate,
				pstrProductIDList,
				pstrCurrencyID, 
				pstrExchangeRate, 
				strHomeCurrency, 
				pstrVendorType,
				pstrVendorIDList);

			//Create builder object
			ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();				
								
			//Set report name
			reportBuilder.ReportName = REPORT_NAME;

			//Set Datasource
			reportBuilder.SourceDataTable = dtbDataSource;
								
			//Set report layout location
			reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
			if(pstrVendorType != null && pstrVendorType != string.Empty)
			{
				if(pstrVendorType.ToUpper() == LOCAL_STRING.ToUpper())
				{
					reportBuilder.ReportLayoutFile = REPORT_TEMPLATE_LOCAL;
				}
				else
				{
					reportBuilder.ReportLayoutFile = REPORT_TEMPLATE;
				}
			}
			else
			{
				reportBuilder.ReportLayoutFile = REPORT_TEMPLATE;
			}				
				
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
			arrParamAndValue.Add(RPT_MASTER_LOCATION_FLD, GetMasterLocationInfoByID(pstrMasLocID));
				
			Hashtable htbCurrency = GetCurrencyInfo(pstrCurrencyID);
			if(htbCurrency != null)
			{
				arrParamAndValue.Add(RPT_CURRENCY_FLD, htbCurrency[PRODUCT_CODE_FLD].ToString());

				if(strHomeCurrency == pstrCurrencyID)
				{
					arrParamAndValue.Add(RPT_EXCHANGE_RATE_FLD, decimal.One.ToString(NUMERIC_FORMAT));
				}
				else
				{
					if(pstrExchangeRate != string.Empty && pstrExchangeRate != null)
					{
						arrParamAndValue.Add(RPT_EXCHANGE_RATE_FLD, decimal.Parse(pstrExchangeRate).ToString(NUMERIC_FORMAT));
					}
					else
					{
						arrParamAndValue.Add(RPT_EXCHANGE_RATE_FLD, decimal.Parse(htbCurrency[EXCHANGE_RATE_FLD].ToString()).ToString(NUMERIC_FORMAT));
					}
				}
			}				

			if(pstrFromDate != null && pstrFromDate != string.Empty)
			{
				arrParamAndValue.Add(RPT_FROM_DATE_FLD, DateTime.Parse(pstrFromDate).ToString(DATE_HOUR_FORMAT));
			}

			if(pstrToDate != null && pstrToDate != string.Empty)
			{
				arrParamAndValue.Add(RPT_TO_DATE_FLD, DateTime.Parse(pstrToDate).ToString(DATE_HOUR_FORMAT));
			}

			if(pstrProductIDList != null && pstrProductIDList != string.Empty)
			{
				arrParamAndValue.Add(RPT_PART_NUMBER_FLD, GetProductInfo(pstrProductIDList));
			}
				
			if(pstrVendorType != null && pstrVendorType != string.Empty)
			{
				arrParamAndValue.Add(RPT_VENDOR_TYPE_FLD, pstrVendorType);
			}
			
			if(pstrVendorIDList != null && pstrVendorIDList != string.Empty)
			{
				arrParamAndValue.Add(RPT_VENDOR_FLD, GetPartyInfo(pstrVendorIDList));
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
		
		#endregion Public Method			
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;
using C1.C1Report;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Utils;
using C1PrintPreviewDialog = PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog;

namespace LocalReceivingMaterialReport
{
	[Serializable]
	public class LocalReceivingMaterialReport : MarshalByRefObject, IDynamicReport
	{
		public LocalReceivingMaterialReport()
		{
		}

		#region Constants

		private const string NAME_FLD = "Name";
		private const string EXCHANGE_RATE_FLD = "ExchangeRate";
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
			get { return mstrReportLayoutFile; }
			set { mstrReportLayoutFile = value; }
		}

		#endregion

		#region Receiving Material Report: Tuan TQ

		/// <summary>
		/// Get CCN Info
		/// </summary>
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

				string strSql = "SELECT " + HOME_CURRENCY_FLD;
				strSql += " FROM MST_CCN";
				strSql += " WHERE CCNID = " + pstrCCNID;

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if (odrPCS != null)
				{
					if (odrPCS.Read())
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

		/// <summary>
		/// Get CCN Info
		/// </summary>
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

				string strSql = "SELECT [Value]"
					+ " FROM Sys_Param"
					+ " WHERE [Name] = '" + FULL_COMPANY_NAME + "'";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if (odrPCS != null)
				{
					if (odrPCS.Read())
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


		private DataTable GetReceiptData(string pstrCCNID, string pstrMasLocID, DateTime pdtmFromDate, DateTime pdtmToDate,
		                                 string pstrProductIDList, string pstrCurrencyID, string pstrExchangeRate, string pstrVendorIDList)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				string strRate = string.Empty;
				if (pstrExchangeRate != null && pstrExchangeRate != string.Empty)
					strRate += " / " + pstrExchangeRate;
				else
					strRate += " / (SELECT ISNULL( (SELECT TOP 1 ISNULL(Rate, 1)"
						+ " 	FROM MST_ExchangeRate rate"
						+ " 	WHERE rate.Approved = 1     "
						+ " 	AND rate.CurrencyID = " + pstrCurrencyID
						+ " 	ORDER BY BeginDate DESC), 1))";
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT MST_Party.PartyID, MST_Party.Code AS PartyCode, MST_Party.Name AS PartyName, "
					+ " ITM_Product.ProductID, ITM_Product.Code AS PartNo, ITM_Product.Description AS PartName,"
					+ " ITM_Product.Revision AS PartModel, MST_UnitOfMeasure.Code AS BuyingUM, ITM_Category.Code AS CategoryCode, "
					+ " ITM_Category.Code as CategoryCode,  ReceiveQuantity, "
					+ " ISNULL((CASE WHEN ReceiptType = 2 OR ReceiptType = 3 "
					+ " 	THEN  (SELECT Detail.UnitPrice   FROM PO_PurchaseOrderDetail Detail JOIN PO_PurchaseOrderMaster Master    "
					+ " 		ON Detail.PurchaseOrderMasterID = Master.PurchaseOrderMasterID   "
					+ " 		WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID    "
					+ " 		AND Detail.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID    "
					+ " 		AND Detail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID) "
					+ " 	WHEN ReceiptType = 4 THEN  (SELECT PO_InvoiceDetail.UnitPrice   "
					+ " 		FROM PO_InvoiceDetail JOIN PO_InvoiceMaster ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID   "
					+ " 		WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID    "
					+ " 		AND PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID)  END),0) *  "
					+ " PO_PurchaseOrderReceiptDetail.ReceiveQuantity *   "
					+ " ISNULL((CASE WHEN ReceiptType = 2 OR ReceiptType = 3 THEN (SELECT Master.ExchangeRate FROM PO_PurchaseOrderDetail Detail "
					+ " 		JOIN PO_PurchaseOrderMaster Master ON Detail.PurchaseOrderMasterID = Master.PurchaseOrderMasterID "
					+ " 		WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID "
					+ " 		AND Detail.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID "
					+ " 		AND Detail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID) "
					+ " 	WHEN ReceiptType = 4 THEN (SELECT PO_InvoiceMaster.ExchangeRate FROM PO_InvoiceDetail "
					+ " 		JOIN PO_InvoiceMaster ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID "
					+ " 		WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID "
					+ " 		AND PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID) END),0)  "
					+ strRate + " AS Amount, "
					+ " 0.01 *  "
					+ " ISNULL((CASE WHEN ReceiptType = 2 OR ReceiptType = 3 THEN  "
					+ " 				(SELECT Detail.UnitPrice * Detail.VAT"
					+ " 				FROM PO_PurchaseOrderDetail Detail JOIN PO_PurchaseOrderMaster Master    "
					+ " 				ON Detail.PurchaseOrderMasterID = Master.PurchaseOrderMasterID   "
					+ "				WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID    "
					+ " 				AND Detail.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID    "
					+ " 				AND Detail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID) "
					+ "			WHEN ReceiptType = 4 THEN  (SELECT PO_InvoiceDetail.UnitPrice * PO_InvoiceDetail.VAT"
					+ " 				FROM PO_InvoiceDetail JOIN PO_InvoiceMaster ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID   "
					+ " 				WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID    "
					+ " 				AND PO_InvoiceDetail.InvoiceMasterID = PO_PurchaseOrderReceiptMaster.InvoiceMasterID)  END),0) "
					+ " *  PO_PurchaseOrderReceiptDetail.ReceiveQuantity "
					+ " *  ISNULL((CASE WHEN ReceiptType = 2 OR ReceiptType = 3 THEN "
					+ " 				(SELECT Master.ExchangeRate FROM PO_PurchaseOrderDetail Detail "
					+ " 				JOIN PO_PurchaseOrderMaster Master ON Detail.PurchaseOrderMasterID = Master.PurchaseOrderMasterID "
					+ " 				WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID "
					+ " 				AND Detail.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID "
					+ " 				AND Detail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID) "
					+ " 				WHEN ReceiptType = 4 THEN (SELECT PO_InvoiceMaster.ExchangeRate FROM PO_InvoiceDetail "
					+ " 				JOIN PO_InvoiceMaster ON PO_InvoiceDetail.InvoiceMasterID = PO_InvoiceMaster.InvoiceMasterID "
					+ " 				WHERE ProductID = PO_PurchaseOrderReceiptDetail.ProductID AND PO_InvoiceDetail.InvoiceMasterID "
					+ " 				= PO_PurchaseOrderReceiptMaster.InvoiceMasterID) END),0)"
					+ strRate + " AS VATAmount"
					+ " FROM PO_PurchaseOrderReceiptMaster INNER JOIN PO_PurchaseOrderReceiptDetail "
					+ " ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN PO_PurchaseOrderDetail"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN ITM_Product ON PO_PurchaseOrderReceiptDetail.ProductID = ITM_Product.ProductID"
					+ " JOIN MST_Party ON PO_PurchaseOrderMaster.PartyID = MST_Party.PartyID"
					+ " LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID"
					+ " LEFT JOIN MST_UnitOfMeasure ON PO_PurchaseOrderDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID"
					+ " WHERE PO_PurchaseOrderMaster.CCNID = " + pstrCCNID
					+ " AND PO_PurchaseOrderMaster.MasterLocationID = " + pstrMasLocID
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate >= ?"
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate <= ?";
				if (pstrVendorIDList != null && pstrVendorIDList != string.Empty)
					strSql += " AND MST_Party.PartyID IN (" + pstrVendorIDList + ")";
				if (pstrProductIDList != null && pstrProductIDList != string.Empty)
					strSql += " AND ITM_Product.ProductID IN (" + pstrProductIDList + ")";

				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
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

		private DataTable GetReturnData(string pstrCCNID, string pstrMasLocID, DateTime pdtmFromDate, DateTime pdtmToDate,
		                                string pstrProductIDList, string pstrCurrencyID, string pstrExchangeRate, string pstrVendorIDList)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				string strRate = string.Empty;
				if (pstrExchangeRate != null && pstrExchangeRate != string.Empty)
					strRate += " / " + pstrExchangeRate;
				else
					strRate += " / (SELECT ISNULL( (SELECT TOP 1 ISNULL(Rate, 1)"
						+ " 	FROM MST_ExchangeRate rate"
						+ " 	WHERE rate.Approved = 1     "
						+ " 	AND rate.CurrencyID = " + pstrCurrencyID
						+ " 	ORDER BY BeginDate DESC), 1))";
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT ProductID, InvoiceMasterID, PurchaseOrderMasterID, PartyID, Quantity, "
					+ " Amount * (CASE	WHEN ByPO = 1 THEN "
					+ " 			(SELECT ExchangeRate FROM PO_PurchaseOrderMaster WHERE PurchaseOrderMasterID = re_Master.PurchaseOrderMasterID)"
					+ " 				WHEN ByInvoice = 1 THEN"
					+ " 			(SELECT ExchangeRate FROM PO_InvoiceMaster WHERE InvoiceMasterID = re_Master.InvoiceMasterID)"
					+ " 		  END)"
					+ strRate + " AS Amount,"
					+ " VATAmount * (CASE	WHEN ByPO = 1 THEN "
					+ " 			(SELECT ExchangeRate FROM PO_PurchaseOrderMaster WHERE PurchaseOrderMasterID = re_Master.PurchaseOrderMasterID)"
					+ " 				WHEN ByInvoice = 1 THEN"
					+ " 			(SELECT ExchangeRate FROM PO_InvoiceMaster WHERE InvoiceMasterID = re_Master.InvoiceMasterID)"
					+ " 		  END)"
					+ strRate + " AS VATAmount"
					+ " FROM PO_ReturnToVendorDetail re_Detail"
					+ " INNER JOIN PO_ReturnToVendorMaster re_Master ON re_Detail.ReturnToVendorMasterID = re_Master.ReturnToVendorMasterID  "
					+ " WHERE re_Master.PostDate BETWEEN ?  AND ?"
					+ " AND re_Master.CCNID = " + pstrCCNID
					+ " AND re_Master.MasterLocationID = " + pstrMasLocID;
				if (pstrVendorIDList != null && pstrVendorIDList != string.Empty)
					strSql += " AND PartyID IN (" + pstrVendorIDList + ")";
				if (pstrProductIDList != null && pstrProductIDList != string.Empty)
					strSql += " AND ProductID IN (" + pstrProductIDList + ")";

				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
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

		private ArrayList GetPartyIDs(string pstrCCNID, string pstrMasLocID, DateTime pdtmFromDate, DateTime pdtmToDate,
		                              string pstrProductIDList, string pstrVendorIDList)
		{
			OleDbConnection oconPCS = null;
			OleDbCommand cmdPCS = null;
			OleDbDataAdapter odadPCS = null;
			try
			{
				oconPCS = new OleDbConnection(mConnectionString);
				string strSql = "SELECT DISTINCT PO_PurchaseOrderMaster.PartyID"
					+ " FROM PO_PurchaseOrderReceiptMaster INNER JOIN PO_PurchaseOrderReceiptDetail "
					+ " ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN PO_PurchaseOrderDetail"
					+ " ON PO_PurchaseOrderReceiptDetail.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN MST_CCN ON PO_PurchaseOrderMaster.CCNID = MST_CCN.CCNID"
					+ " WHERE PO_PurchaseOrderReceiptMaster.PostDate >= ?"
					+ " AND PO_PurchaseOrderReceiptMaster.PostDate <= ?"
					+ " AND PO_PurchaseOrderMaster.CCNID = " + pstrCCNID
					+ " AND PO_PurchaseOrderMaster.MasterLocationID = " + pstrMasLocID
					+ " AND PO_PurchaseOrderMaster.PartyID IN (SELECT PartyID FROM MST_Party WHERE MST_Party.CountryID = MST_CCN.CountryID)";
				if (pstrVendorIDList != null && pstrVendorIDList != string.Empty)
					strSql += " AND PO_PurchaseOrderMaster.PartyID IN (" + pstrVendorIDList + ")";
				if (pstrProductIDList != null && pstrProductIDList != string.Empty)
					strSql += " AND PO_PurchaseOrderReceiptDetail.ProductID IN (" + pstrProductIDList + ")";

				cmdPCS = new OleDbCommand(strSql, oconPCS);
				cmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
				cmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
				cmdPCS.Connection.Open();

				DataTable dtbDelivery = new DataTable();
				odadPCS = new OleDbDataAdapter(cmdPCS);
				odadPCS.Fill(dtbDelivery);
				ArrayList arrResult = new ArrayList();
				foreach (DataRow drowData in dtbDelivery.Rows)
					if (!arrResult.Contains(drowData["PartyID"].ToString()))
						arrResult.Add(drowData["PartyID"].ToString());
				return arrResult;
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
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

				string strSql = "SELECT " + PRODUCT_CODE_FLD + ", " + PRODUCT_NAME_FLD
					+ " FROM MST_CCN"
					+ " WHERE MST_CCN.CCNID = " + pstrID;

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if (odrPCS != null)
				{
					if (odrPCS.Read())
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
		/// Get Location Info
		/// </summary>
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

				string strSql = "SELECT Code, Description";
				strSql += " FROM ITM_Product";
				strSql += " WHERE ProductID IN (" + pstrIDList + ")";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if (odrPCS != null)
				{
					while (odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if (strResult.Length > 250)
				{
					int i = strResult.IndexOf(SEMI_COLON, 250);
					if (i > 0)
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

				string strSql = "SELECT Code, Name";
				strSql += " FROM MST_Party";
				strSql += " WHERE PartyID IN (" + pstrIDList + ")";

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if (odrPCS != null)
				{
					while (odrPCS.Read())
					{
						strResult += odrPCS[PRODUCT_CODE_FLD].ToString().Trim() + SEMI_COLON;
					}
				}

				if (strResult.Length > 250)
				{
					int i = strResult.IndexOf(SEMI_COLON, 250);
					if (i > 0)
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

				string strSql = "SELECT " + PRODUCT_CODE_FLD + ", " + NAME_FLD
					+ " FROM MST_MasterLocation"
					+ " WHERE MasterLocationID = " + pstrID;

				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if (odrPCS != null)
				{
					if (odrPCS.Read())
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

				string strSql = "SELECT ISNULL(rate.Rate, 1) as " + EXCHANGE_RATE_FLD;
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

				if (odrPCS != null)
				{
					if (odrPCS.Read())
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
		/// Build and show Local Receiving Material
		/// </summary>
		/// <author> Tuan TQ, 11 Jun, 2006</author>
		public DataTable ExecuteReport(string pstrCCNID,
		                               string pstrMasLocID,
		                               string pstrFromDate,
		                               string pstrToDate,
		                               string pstrCurrencyID,
		                               string pstrExchangeRate,
		                               string pstrProductIDList,
		                               string pstrVendorIDList
			)
		{
			const string DATE_HOUR_FORMAT = "dd-MM-yyyy HH:mm";
			const string NUMERIC_FORMAT = "#,##0.00";

			const string REPORT_TEMPLATE = "LocalReceivingMaterialReport.xml";

			const string RPT_PAGE_HEADER = "PageHeader";

			const string REPORT_NAME = "LocalReceivingMaterialReport";
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

			DataTable dtbDataSource = null;

			string strHomeCurrency = GetHomeCurrency(pstrCCNID);

			DateTime dtmFromDate = DateTime.MinValue;
			DateTime dtmToDate = DateTime.MinValue;
			try
			{
				dtmFromDate = Convert.ToDateTime(pstrFromDate);
			}
			catch{}
			try
			{
				dtmToDate = Convert.ToDateTime(pstrToDate);
			}
			catch{}

			#region build table schema

			dtbDataSource = new DataTable();
			dtbDataSource.Columns.Add(new DataColumn("PartyCode", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("PartyName", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("ProductID", typeof (int)));
			dtbDataSource.Columns.Add(new DataColumn("PartNo", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("PartName", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("PartModel", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("BuyingUM", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("CategoryCode", typeof (string)));
			dtbDataSource.Columns.Add(new DataColumn("ReceiveQuantity", typeof (decimal)));
			dtbDataSource.Columns.Add(new DataColumn("Amount", typeof (decimal)));
			dtbDataSource.Columns.Add(new DataColumn("VATAmount", typeof (decimal)));
			dtbDataSource.Columns.Add(new DataColumn("ReturnedQuantity", typeof (decimal)));
			dtbDataSource.Columns["ReturnedQuantity"].AllowDBNull = true;
			dtbDataSource.Columns.Add(new DataColumn("ReturnedAmount", typeof (decimal)));
			dtbDataSource.Columns["ReturnedAmount"].AllowDBNull = true;
			dtbDataSource.Columns.Add(new DataColumn("ReturnedVATAmount", typeof (decimal)));
			dtbDataSource.Columns["ReturnedVATAmount"].AllowDBNull = true;

			#endregion

			// get receive data
			DataTable dtbReceipt = GetReceiptData(pstrCCNID, pstrMasLocID, dtmFromDate, dtmToDate,
				pstrProductIDList, pstrCurrencyID, pstrExchangeRate, pstrVendorIDList);
			// get return data
			DataTable dtbReturn = GetReturnData(pstrCCNID, pstrMasLocID, dtmFromDate, dtmToDate,
				pstrProductIDList, pstrCurrencyID, pstrExchangeRate, pstrVendorIDList);

			// list of party
			ArrayList arrParty = GetPartyIDs(pstrCCNID, pstrMasLocID, dtmFromDate, dtmToDate, pstrProductIDList, pstrVendorIDList);
			foreach (string strPartyID in arrParty)
			{
				// item of current party
				DataRow[] drowsItem = dtbReceipt.Select("PartyID = " + strPartyID, "ProductID ASC");
				string strLastProductID = string.Empty;
				foreach (DataRow drowItem in drowsItem)
				{
					if (strLastProductID != drowItem["ProductID"].ToString())
					{
						strLastProductID = drowItem["ProductID"].ToString();

						string strFilter = "PartyID = " + strPartyID + " AND ProductID = " + strLastProductID;

						#region general information

						DataRow drowResult = dtbDataSource.NewRow();
						drowResult["PartyCode"] = drowItem["PartyCode"];
						drowResult["PartyName"] = drowItem["PartyName"];
						drowResult["PartNo"] = drowItem["PartNo"];
						drowResult["PartName"] = drowItem["PartName"];
						drowResult["PartModel"] = drowItem["PartModel"];
						drowResult["BuyingUM"] = drowItem["BuyingUM"];
						drowResult["CategoryCode"] = drowItem["CategoryCode"];
						drowResult["ProductID"] = drowItem["ProductID"];

						#endregion

						#region receive data

						decimal decReceiveQuantity = 0, decAmount = 0, decVATAmount = 0;
						try
						{
							decReceiveQuantity = Convert.ToDecimal(dtbReceipt.Compute("SUM(ReceiveQuantity)", strFilter));
						}
						catch{}
						try
						{
							decAmount = Convert.ToDecimal(dtbReceipt.Compute("SUM(Amount)", strFilter));
						}
						catch{}
						try
						{
							decVATAmount = Convert.ToDecimal(dtbReceipt.Compute("SUM(VATAmount)", strFilter));
						}
						catch{}

						drowResult["ReceiveQuantity"] = decReceiveQuantity;
						drowResult["Amount"] = decAmount;
						drowResult["VATAmount"] = decVATAmount;

						#endregion

						#region return data

						decimal decReturnedQuantity = 0, decReturnedAmount = 0, decReturnedVATAmount = 0;
						try
						{
							decReturnedQuantity = Convert.ToDecimal(dtbReturn.Compute("SUM(Quantity)", strFilter));
						}
						catch{}
						try
						{
							decReturnedAmount = Convert.ToDecimal(dtbReturn.Compute("SUM(Amount)", strFilter));
						}
						catch{}
						try
						{
							decReturnedVATAmount = Convert.ToDecimal(dtbReturn.Compute("SUM(VATAmount)", strFilter));
						}
						catch{}

						if (decReturnedQuantity != 0)
							drowResult["ReturnedQuantity"] = decReturnedQuantity;
						if (decReturnedAmount != 0)
							drowResult["ReturnedAmount"] = decReturnedAmount;
						if (decReturnedVATAmount != 0)
							drowResult["ReturnedVATAmount"] = decReturnedVATAmount;

						#endregion

						// insert to result table
						dtbDataSource.Rows.Add(drowResult);
					}
				}
			}
				
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
			C1PrintPreviewDialog printPreview = new C1PrintPreviewDialog();

			//Attach report viewer
			reportBuilder.ReportViewer = printPreview.ReportViewer;
			reportBuilder.RenderReport();

			reportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, GetCompanyFullName());
			//Draw parameters				
			NameValueCollection arrParamAndValue = new NameValueCollection();

			arrParamAndValue.Add(RPT_CCN_FLD, GetCCNInfoByID(pstrCCNID));
			arrParamAndValue.Add(RPT_MASTER_LOCATION_FLD, GetMasterLocationInfoByID(pstrMasLocID));

			Hashtable htbCurrency = GetCurrencyInfo(pstrCurrencyID);
			if (htbCurrency != null)
			{
				arrParamAndValue.Add(RPT_CURRENCY_FLD, htbCurrency[PRODUCT_CODE_FLD].ToString());

				if (strHomeCurrency == pstrCurrencyID)
				{
					arrParamAndValue.Add(RPT_EXCHANGE_RATE_FLD, decimal.One.ToString(NUMERIC_FORMAT));
				}
				else
				{
					if (pstrExchangeRate != string.Empty && pstrExchangeRate != null)
					{
						arrParamAndValue.Add(RPT_EXCHANGE_RATE_FLD, decimal.Parse(pstrExchangeRate).ToString(NUMERIC_FORMAT));
					}
					else
					{
						arrParamAndValue.Add(RPT_EXCHANGE_RATE_FLD, decimal.Parse(htbCurrency[EXCHANGE_RATE_FLD].ToString()).ToString(NUMERIC_FORMAT));
					}
				}
			}

			if (pstrFromDate != null && pstrFromDate != string.Empty)
			{
				arrParamAndValue.Add(RPT_FROM_DATE_FLD, DateTime.Parse(pstrFromDate).ToString(DATE_HOUR_FORMAT));
			}

			if (pstrToDate != null && pstrToDate != string.Empty)
			{
				arrParamAndValue.Add(RPT_TO_DATE_FLD, DateTime.Parse(pstrToDate).ToString(DATE_HOUR_FORMAT));
			}

			if (pstrProductIDList != null && pstrProductIDList != string.Empty)
			{
				arrParamAndValue.Add(RPT_PART_NUMBER_FLD, GetProductInfo(pstrProductIDList));
			}

			if (pstrVendorIDList != null && pstrVendorIDList != string.Empty)
			{
				arrParamAndValue.Add(RPT_VENDOR_FLD, GetPartyInfo(pstrVendorIDList));
			}

			//Anchor the Parameter drawing canvas cordinate to the fldTitle
			Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD);
			double dblStartX = fldTitle.Left;
			double dblStartY = fldTitle.Top + 1.3*fldTitle.RenderHeight;
			reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
			reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);

			try
			{
				printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD).Text;
			}
			catch
			{
			}

			reportBuilder.RefreshReport();
			printPreview.Show();

			//return table
			return dtbDataSource;
		}

		#endregion Public Method			
	}
}
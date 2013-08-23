using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using C1.Win.C1Preview;

using PCSComUtils.Common;
using PCSUtils.Utils;

namespace SaleTransactionHistoryReport
{
	[Serializable]
	public class SaleTransactionHistoryReport : MarshalByRefObject, IDynamicReport
	{
		public SaleTransactionHistoryReport()
		{
		}

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

		#region Processing Data		
		
		private const string PRODUCT_CODE = "Code";
		private const string PRODUCT_NAME = "Description";
		private const string PRODUCT_MODEL = "Revision";
		private const string SEMI_COLON = "; ";

		private const int MAX_LENGTH = 250;

		private const string CODE_FIELD = "Code";
		private const string NAME_FIELD = "Name";		

		private DataTable GetSaleTransactionHistoryData(string pstrCCNID, string pstrMasLocID, string pstrFromDate, string pstrToDate, 
			string pstrProductIDList, string pstrCustomerIDList, string pstrCurrencyIDList)
		{
			OleDbConnection cn = null;
			
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbBOMData = new DataTable();
			
				//Build WHERE clause
				string strWhereClause = " WHERE SO_ConfirmShipMaster.CCNID =" + pstrCCNID;
				strWhereClause += " AND SO_ConfirmShipMaster.MasterLocationID = " + pstrMasLocID;
                
				//From Date
				if(pstrFromDate != null && pstrFromDate != string.Empty)
				{
					strWhereClause += " AND SO_ConfirmShipMaster.ShippedDate >='" + pstrFromDate + "' "; 
				}

				//To Date
				if(pstrToDate != null && pstrToDate != string.Empty)
				{
					strWhereClause += " AND SO_ConfirmShipMaster.ShippedDate <='" + pstrToDate + "' "; 
				}
				
				//Product ID List
				if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				{
					strWhereClause += " AND ITM_Product.ProductID IN (" + pstrProductIDList + ") "; 
				}
				
				//Currency ID List
				if(pstrCurrencyIDList != null && pstrCurrencyIDList != string.Empty)
				{
					strWhereClause += " AND MST_Currency.CurrencyID IN (" + pstrCurrencyIDList + ") "; 
				}

				//Customer ID List
				if(pstrCustomerIDList != null && pstrCustomerIDList != string.Empty)
				{
					strWhereClause += " AND MST_Party.PartyID IN (" + pstrCustomerIDList + ") "; 
				}

				//Build SQL string
				string strSql = " SELECT  DISTINCT SO_ConfirmShipMaster.ConfirmShipNo, SO_ConfirmShipDetail.ConfirmShipDetailID, ";
				strSql += " SO_ConfirmShipMaster.ShippedDate, ";
				strSql += " SO_SaleOrderMaster.Code AS SaleOrderNo, ";
				strSql += " MST_Currency.Code as CurrencyCode, ";
				strSql += " SO_ConfirmShipMaster.ExchangeRate, ";

				strSql += " SO_SaleOrderDetail.SaleOrderLine, ";
				strSql += " ITM_Product.Code AS PartNo, ";
				strSql += " ITM_Product.Description as PartName, ";
				strSql += " ITM_Product.Revision as PartModel, ";
				strSql += " MST_UnitOfMeasure.Code AS SellingUM, ";
				strSql += " ITM_Category.Code AS CategoryCode, ";
				strSql += " ITM_Product.Code + ITM_Product.Description + ITM_Product.Revision as PartInfo,";

				strSql += " SO_ConfirmShipDetail.Price as UnitPrice, ";
				strSql += " MST_Party.Code as PartyCode,";
				strSql += " MST_Party.Name as PartyName,";

				strSql += " ( ";
				strSql += " (SELECT SUM( detail.Price * detail.InvoiceQty) ";
				strSql += " FROM SO_ConfirmShipDetail detail ";
				strSql += " WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ";
				strSql += " AND detail.ProductID = ITM_Product.ProductID ";
				strSql += " )	 ";
				strSql += " / ";
				strSql += " ( ";
				strSql += " Case  ";
				strSql += "     When ( ";
				strSql += " 	  SELECT SUM(detail.InvoiceQty) ";
				strSql += " 	  FROM SO_ConfirmShipDetail detail ";
				strSql += " 	  WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ";
				strSql += " 	  AND detail.ProductID = ITM_Product.ProductID ";
				strSql += "          ) = 0  ";
				strSql += "      then 1 ";
				strSql += "          else  ";  
				strSql += " 	( ";
				strSql += " 	  SELECT SUM(detail.InvoiceQty) ";
				strSql += " 	  FROM SO_ConfirmShipDetail detail ";
				strSql += " 	  WHERE detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID ";
				strSql += " 	  AND detail.ProductID = ITM_Product.ProductID ";
				strSql += "          ) ";
				strSql += "       end     ";  
				strSql += "  ) ";
				strSql += " ) ";
				strSql += "  as AVGPrice, ";

				strSql += " (SELECT SUM(detail.InvoiceQty)";
				strSql += " FROM SO_ConfirmShipDetail detail";
				strSql += " WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID";
				strSql += " AND detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID";
				strSql += " AND detail.ConfirmShipDetailID = SO_ConfirmShipDetail.ConfirmShipDetailID";
				strSql += " ) as Quantity,";

				strSql += " (SELECT SUM(detail.InvoiceQty)";
				strSql += " FROM SO_ConfirmShipDetail detail";
				strSql += " WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID";
				strSql += " AND detail.ConfirmShipMasterID = SO_ConfirmShipMaster.ConfirmShipMasterID";
				strSql += " AND detail.ConfirmShipDetailID = SO_ConfirmShipDetail.ConfirmShipDetailID";
				strSql += " )";
				strSql += " * SO_ConfirmShipDetail.Price as Amount,";

				strSql += " SO_ConfirmShipDetail.VATAmount";
				
				strSql += " FROM    SO_ConfirmShipMaster  ";
				strSql += " INNER JOIN SO_ConfirmShipDetail ON SO_ConfirmShipMaster.ConfirmShipMasterID = SO_ConfirmShipDetail.ConfirmShipMasterID  ";
				strSql += " INNER JOIN SO_SaleOrderMaster ON SO_ConfirmShipMaster.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID  ";
				strSql += " INNER JOIN SO_SaleOrderDetail ON SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID  ";
				strSql += " INNER JOIN SO_DeliverySchedule ON SO_ConfirmShipDetail.DeliveryScheduleID = SO_DeliverySchedule.DeliveryScheduleID  ";
				strSql += " INNER JOIN MST_Party ON MST_Party.PartyID = SO_SaleOrderMaster.PartyID";

				strSql += " INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID  ";
				strSql += " INNER JOIN MST_Currency ON SO_ConfirmShipMaster.CurrencyID = MST_Currency.CurrencyID ";
				strSql += " INNER JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID ";
				strSql += " LEFT JOIN ITM_Category ON ITM_Category.CategoryID = ITM_Product.CategoryID ";
				
				//Add WHERE clause
				strSql += strWhereClause;
				
				strSql += " ORDER BY SO_ConfirmShipMaster.ShippedDate,";
				strSql += " SO_SaleOrderMaster.Code,";
				strSql += " ITM_Product.Code,"; 				
				strSql += " SO_SaleOrderDetail.SaleOrderLine";
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbBOMData);
				
				return dtbBOMData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
					if (cn.State != ConnectionState.Closed)
						cn.Close();
			}

			
		}

		/// <summary>
		/// Get Transaction Type Data for subreport
		/// </summary>
		/// <returns></returns>
		private String GetTranTypeName(string pstrTranTypeID)
		{
			const string DESCRIPTION_FLD = "Description";

			OleDbConnection cn = null;
			
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbTranTypeData = new DataTable();

				string strSql = " SELECT [Description]";
				strSql += " FROM MST_TranType";
				strSql += " WHERE TranTypeID = " + pstrTranTypeID;				
				
				OleDbDataAdapter odad = new OleDbDataAdapter(strSql, cn);

				odad.Fill(dtbTranTypeData);
				if(dtbTranTypeData != null)
				{
					if(dtbTranTypeData.Rows.Count > 0)
					{
						return dtbTranTypeData.Rows[0][DESCRIPTION_FLD].ToString();
					}
				}

				return string.Empty;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (cn != null)
				{
					if (cn.State != ConnectionState.Closed)
						cn.Close();

					cn = null;
				}
			}
			
		}

		
		/// <summary>
		/// Get Master Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetMasterLocationInfoByID(string pstrID)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_MasterLocation"
					+ " WHERE MST_MasterLocation.MasterLocationID = " + pstrID;

			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
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
		private string GetLocationInfoByID(string pstrID)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_Location"
					+ " WHERE MST_Location.LocationID = " + pstrID;
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
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
		/// Get Currency Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCurrencyInfoByID(string pstrIDList)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code"
					+ " FROM MST_Currency"
					+ " WHERE MST_Currency.CurrencyID IN (" + pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[CODE_FIELD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, MAX_LENGTH);
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
		/// Get Currency Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCustomerInfoByID(string pstrIDList)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
					+ " FROM MST_Party"
					+ " WHERE MST_Party.PartyID IN (" + pstrIDList + ")";
				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				if(odrPCS != null)
				{
					while(odrPCS.Read())
					{
						strResult += odrPCS[CODE_FIELD].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, MAX_LENGTH);
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
		/// Get CCN Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCCNInfoByID(string pstrID)
		{			
			DataSet dstPCS = new DataSet();

			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + CODE_FIELD + ", " + NAME_FIELD
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
						strResult = odrPCS[CODE_FIELD].ToString().Trim() + " (" + odrPCS[NAME_FIELD].ToString().Trim() + ")";
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
		/// Get Product Information
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private Hashtable GetProductInfoByID(string pstrID)
		{					
			Hashtable htbResult = new Hashtable();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;

			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE + ", " + PRODUCT_NAME + ", " + PRODUCT_MODEL
					+ " FROM ITM_Product"
					+ " WHERE ITM_Product.ProductID = " + pstrID;
			
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();
				if(odrPCS != null)
				{
					if(odrPCS.Read())
					{
						htbResult.Add(PRODUCT_CODE, odrPCS[PRODUCT_CODE]);
						htbResult.Add(PRODUCT_NAME, odrPCS[PRODUCT_NAME]);
						htbResult.Add(PRODUCT_MODEL, odrPCS[PRODUCT_MODEL]);
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
		
		/// <summary>
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetProductInfo(string pstrIDList)
		{			
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
						strResult += odrPCS[PRODUCT_CODE].ToString().Trim() + SEMI_COLON;
					}
				}

				if(strResult.Length > MAX_LENGTH)				
				{
					int i = strResult.IndexOf(SEMI_COLON, MAX_LENGTH);
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

		#endregion Processing Data		
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}
		
		public DataTable ExecuteReport(string pstrCCNID, string pstrMasLocID, string pstrFromDate, string pstrToDate, 
			string pstrProductIDList, string pstrCustomerIDList, string pstrCurrencyIDList
			)
		{	
			try
			{
				const string DATE_FORMAT = "dd-MM-yyyy HH:mm";
				const string REPORT_NAME = "SaleTransactionHistoryReport";				
				const string REPORT_LAYOUT = "SaleTransactionHistoryReport.xml";
				
				const int FOOTER_HEIGHT = 10;

				const string RPT_PAGE_HEADER = "PageHeader";
				const string RPT_CONFIRM_SHIP_NO_FOOTER = "ConfirmShipNoFooter";
				
				//Report field names
				const string RPT_FIELD_PREFIX = "fld";

				const string RPT_TITLE_FIELD = "fldTitle";				

				const string RPT_CCN	     = "CCN";
				const string RPT_FROM_DATE   = "From Date";
				const string RPT_TO_DATE     = "To Date";				
				const string RPT_PART_NUMBER = "Part Number";				
				const string RPT_MASTER_LOCATION = "Master Location";
				const string RPT_CURRENCY = "Currency";
				const string RPT_CUSTOMER = "Customer";

				DataTable dtbTranHis = GetSaleTransactionHistoryData(pstrCCNID, pstrMasLocID, pstrFromDate, pstrToDate, pstrProductIDList, pstrCustomerIDList, pstrCurrencyIDList);

				//Create builder object
				ReportBuilder reportBuilder = new ReportBuilder();
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
			
				//Set Datasource
				reportBuilder.SourceDataTable = dtbTranHis;				
			
				//Set report layout location
				reportBuilder.ReportDefinitionFolder = mstrReportDefinitionFolder;
				reportBuilder.ReportLayoutFile = REPORT_LAYOUT;
			
				reportBuilder.UseLayoutFile = true;
				reportBuilder.MakeDataTableForRender();			

				// And show it in preview dialog				
				PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog	printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();
							
				//Attach report viewer
				reportBuilder.ReportViewer = printPreview.ReportViewer;				
				reportBuilder.RenderReport();				
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				string strCode = GetCCNInfoByID(pstrCCNID);
				arrParamAndValue.Add(RPT_CCN, strCode);
								
				//Master Location
				strCode = GetMasterLocationInfoByID(pstrMasLocID);
				arrParamAndValue.Add(RPT_MASTER_LOCATION, strCode);
							
				//From Date
				if(pstrFromDate != null && pstrFromDate != string.Empty)
				{
					arrParamAndValue.Add(RPT_FROM_DATE, DateTime.Parse(pstrFromDate).ToString(DATE_FORMAT));
				}
				
				//To Date
				if(pstrToDate != null && pstrToDate != string.Empty)
				{
					arrParamAndValue.Add(RPT_TO_DATE, DateTime.Parse(pstrToDate).ToString(DATE_FORMAT));
				}
				
				//Product Information
				if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_PART_NUMBER, GetProductInfo(pstrProductIDList));					
				}

				//Customer Information
				if(pstrCustomerIDList != null && pstrCustomerIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_CUSTOMER, GetCustomerInfoByID(pstrCustomerIDList));					
				}

				//Currency Information
				if(pstrCurrencyIDList != null && pstrCurrencyIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_CURRENCY, GetCurrencyInfoByID(pstrCurrencyIDList));					
				}

				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, reportBuilder.Report.Font.Size);			
				
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
				}
				catch{}

				//Hide footer if items were selected				
				if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				{
					C1.C1Report.Section confirmFooter = reportBuilder.GetSectionByName(RPT_CONFIRM_SHIP_NO_FOOTER);
					if(confirmFooter != null)
					{
						foreach(C1.C1Report.Field field in confirmFooter.Fields)
						{
							field.Top = 0;
							field.Visible = !(field.Name.StartsWith(RPT_FIELD_PREFIX));							
						}						
						confirmFooter.Height = FOOTER_HEIGHT;
					}
				}

				reportBuilder.RefreshReport();
				printPreview.Show();
			
				return dtbTranHis;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		
		#endregion Public Method
	}
}
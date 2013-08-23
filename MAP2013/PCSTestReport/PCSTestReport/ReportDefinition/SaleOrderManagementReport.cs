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

namespace SaleOrderManagementReport
{
	[Serializable]
	public class SaleOrderManagementReport : MarshalByRefObject, IDynamicReport
	{
		public SaleOrderManagementReport()
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

		private const string CODE_FIELD = "Code";
		private const string NAME_FIELD = "Name";		

		private DataTable GetSaleOrderManagementData(string pstrCCNID, string pstrMasLocID, string pstrPartyIDList, string pstrSaleOrderIDList, string pstrProductIDList)
		{
			OleDbConnection cn = null;
			
			try
			{
				cn = new OleDbConnection(mConnectionString);
				DataTable dtbBOMData = new DataTable();
			
				//Build WHERE clause
				string strWhereClause = " WHERE SO_SaleOrderMaster.CCNID =" + pstrCCNID;
				strWhereClause += " AND SO_SaleOrderMaster.ShipFromLocID = " + pstrMasLocID;
                
				//From Date
				if(pstrPartyIDList != null && pstrPartyIDList != string.Empty)
				{
					strWhereClause += " AND SO_SaleOrderMaster.PartyID IN (" + pstrPartyIDList + ") "; 
				}

				//To Date
				if(pstrSaleOrderIDList != null && pstrSaleOrderIDList != string.Empty)
				{
					strWhereClause += " AND SO_SaleOrderMaster.SaleOrderMasterID IN (" + pstrSaleOrderIDList + ") "; 
				}
				
				//Product ID List
				if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				{
					strWhereClause += " AND ITM_Product.ProductID IN (" + pstrProductIDList + ") "; 
				}

				//Build SQL string
				string strSql = " SELECT  DISTINCT";
				strSql += " SO_SaleOrderMaster.Code as SaleOrderNo, ";
				strSql += " SO_SaleOrderMaster.TransDate,";
				strSql += " MST_Party.Code AS PartyCode, ";
				strSql += " MST_Party.Name as PartyName,        ";
				strSql += " SO_SaleOrderDetail.SaleOrderLine,	";
				strSql += " ITM_Product.Code AS PartNo,";
				strSql += " ITM_Product.Description as PartName,";
				strSql += " ITM_Product.Revision as PartModel,";
				strSql += " MST_UnitOfMeasure.Code as SellingUM,";
				strSql += " SO_SaleOrderDetail.OrderQuantity,";

				strSql += " Case ";
				strSql += " 	when (SELECT SUM(SO_CommitInventoryDetail.CommitQuantity)";
				strSql += " 	      FROM SO_DeliverySchedule ";
				strSql += " 	          INNER JOIN SO_CommitInventoryDetail ON SO_DeliverySchedule.DeliveryScheduleID = SO_CommitInventoryDetail.DeliveryScheduleID 	";
				strSql += " 	      WHERE SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID)";
				strSql += " 	    is null then Char(168)";
				strSql += " 	else Char(254)";
				strSql += " end as IsCommitted,";
				
				strSql += " ISNULL((SELECT SUM(SO_CommitInventoryDetail.CommitQuantity)";
				strSql += " 	FROM SO_DeliverySchedule ";
				strSql += " 	     INNER JOIN SO_CommitInventoryDetail ON SO_DeliverySchedule.DeliveryScheduleID = SO_CommitInventoryDetail.DeliveryScheduleID ";
				strSql += " 	WHERE SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID 	";
				strSql += " 	)";
				strSql += " , 0) as CommittedQty,";
				
				strSql += " Case ";
				strSql += " 	when (SELECT SUM(commitValue.DeliveryQuantity)";
				strSql += " 	      FROM SO_ConfirmShipDetail ";
				strSql += " 	      INNER JOIN SO_DeliverySchedule commitValue ON SO_ConfirmShipDetail.DeliveryScheduleID = commitValue.DeliveryScheduleID";
				strSql += " 	      WHERE SO_SaleOrderDetail.SaleOrderDetailID = SO_ConfirmShipDetail.SaleOrderDetailID)";
				strSql += " 	    is null then char(168)";
				strSql += " 	else Char(254)";
				strSql += " end as IsShipped,";

				strSql += " ISNULL( (SELECT SUM(detail.InvoiceQty)";
				strSql += " FROM SO_ConfirmShipDetail detail";
				strSql += " INNER JOIN SO_ConfirmShipMaster master ON detail.ConfirmShipMasterID = master.ConfirmShipMasterID";
				strSql += " WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID";
				strSql += " AND master.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID";
				strSql += " ), 0) as ShippedQty,";

				strSql += " case ";
				strSql += " when ";
				strSql += " (SO_SaleOrderDetail.OrderQuantity";
				strSql += " - ";
				strSql += " ISNULL( (SELECT SUM(detail.InvoiceQty)";
				strSql += " FROM SO_ConfirmShipDetail detail";
				strSql += " INNER JOIN SO_ConfirmShipMaster master ON detail.ConfirmShipMasterID = master.ConfirmShipMasterID";
				strSql += " WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID";
				strSql += " AND master.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID";
				strSql += " ), 0))";
				strSql += " < 0 then 0";
				strSql += " else";
				strSql += " (SO_SaleOrderDetail.OrderQuantity ";
				strSql += " - ";
				strSql += " ISNULL( (SELECT SUM(detail.InvoiceQty)";
				strSql += " FROM SO_ConfirmShipDetail detail";
				strSql += " INNER JOIN SO_ConfirmShipMaster master ON detail.ConfirmShipMasterID = master.ConfirmShipMasterID";
				strSql += " WHERE detail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID";
				strSql += " AND master.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID";
				strSql += " ), 0))";
				strSql += " End as RemainQty,";
				
				strSql += " (";
				strSql += " SELECT Top 1 ShippedDate";
				strSql += " FROM SO_ConfirmShipMaster";
				strSql += "      INNER JOIN SO_ConfirmShipDetail ON  SO_ConfirmShipMaster.ConfirmShipMasterID = SO_ConfirmShipDetail.ConfirmShipMasterID";
				strSql += " WHERE SO_ConfirmShipDetail.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID	";
				strSql += " ORDER BY ShippedDate DESC";
				strSql += " )";
				strSql += " as LastShippedDate,";

				strSql += " ISNULL((SELECT SUM(SO_ReturnedGoodsDetail.QuantityOfSelling)";
				strSql += "        FROM SO_ReturnedGoodsDetail ";
				strSql += "        WHERE SO_SaleOrderDetail.SaleOrderDetailID = SO_ReturnedGoodsDetail.SaleOrderDetailID ";
				strSql += " 	)";
				strSql += " , 0) as ReturnedQty";

				strSql += " FROM    SO_SaleOrderMaster"; 
				strSql += " INNER JOIN SO_SaleOrderDetail ON SO_SaleOrderMaster.SaleOrderMasterID = SO_SaleOrderDetail.SaleOrderMasterID"; 
				strSql += " INNER JOIN MST_UnitOfMeasure ON SO_SaleOrderDetail.SellingUMID = MST_UnitOfMeasure.UnitOfMeasureID"; 
				strSql += " INNER JOIN ITM_Product ON SO_SaleOrderDetail.ProductID = ITM_Product.ProductID"; 
				strSql += " INNER JOIN MST_Party ON SO_SaleOrderMaster.PartyID = MST_Party.PartyID";
								
				//Add WHERE clause
				strSql += strWhereClause;

				strSql += " ORDER BY SaleOrderNo,";
				strSql += " SaleOrderLine ASC";
				
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
						strResult += odrPCS[PRODUCT_CODE].ToString().Trim() + SEMI_COLON;
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
						strResult += odrPCS[PRODUCT_CODE].ToString().Trim() + SEMI_COLON;
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
		private string GetSaleOrderInfo(string pstrIDList)
		{			
			const string SEMI_COLON = "; ";
			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT Code";
				strSql += " FROM SO_SaleOrderMaster";
				strSql += " WHERE SaleOrderMasterID IN (" +  pstrIDList + ")";
				
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

		#endregion Processing Data		
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}
		
		public DataTable ExecuteReport(string pstrCCNID, string pstrMasLocID, string pstrPartyIDList, string pstrSaleOrderIDList, string pstrProductIDList)
		{	
			try
			{
				const string DATE_FORMAT = "dd-MM-yyyy HH:mm";
				const string REPORT_NAME = "SaleOrderManagementReport";				
				const string REPORT_LAYOUT = "SaleOrderManagementReport.xml";

				const string RPT_PAGE_HEADER = "PageHeader";
				//Report field names
				const string RPT_TITLE_FIELD = "fldTitle";

				const string RPT_CCN	  = "CCN";
				const string RPT_SO_NO    = "SO No.";
				const string RPT_CUSTOMER = "Customer";				
				const string RPT_PART_NUMBER = "Part Number";
				const string RPT_MASTER_LOCATION = "Master Location";

				DataTable dtbTranHis = GetSaleOrderManagementData(pstrCCNID, pstrMasLocID, pstrPartyIDList, pstrSaleOrderIDList, pstrProductIDList);

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
				
				//Party Information
				if(pstrPartyIDList != null && pstrPartyIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_CUSTOMER, GetPartyInfo(pstrPartyIDList));					
				}

				//Sale Order Information
				if(pstrSaleOrderIDList != null && pstrSaleOrderIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_SO_NO, GetSaleOrderInfo(pstrSaleOrderIDList));					
				}

				//Product Information
				if(pstrProductIDList != null && pstrProductIDList != string.Empty)
				{				
					arrParamAndValue.Add(RPT_PART_NUMBER, GetProductInfo(pstrProductIDList));					
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
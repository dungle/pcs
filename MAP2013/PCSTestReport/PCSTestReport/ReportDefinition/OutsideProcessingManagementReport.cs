using System;
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

namespace OutsideProcessingManagementReport
{
	[Serializable]
	public class OutsideProcessingManagementReport : MarshalByRefObject, IDynamicReport
	{
		public OutsideProcessingManagementReport()
		{
		}

		#region Constants

		private const string REPORT_TEMPLATE = "OutsideProcessingManagementReport.xml";
		private const string REPORT_NAME = "OutsideProcessingReport";				
		private const string SUB_REPORT_NAME = "SubReport";
		private const string RPT_TITLE_FIELD = "fldTitle";				

		private const int MAX_DAYS_IN_MONTH = 31;
		private const string TABLE_NAME = "tbl_OutsideProcessing";
		
		private const string IS_PARENT_VALUE = "1";

		private const string BOM_QUANTITY_FLD = "BOM_Quantity";

		private const string NAME_FLD = "Name";		
		private const string IS_PARENTD_FLD = "IsParent";
		private const string PRODUCT_ID_FLD = "ProductID";
		private const string CHILD_PRODUCT_ID_FLD = "ChildProductID";
		private const string PRODUCT_CODE_FLD = "Code";
		private const string PRODUCT_NAME_FLD = "Description";
		private const string PRODUCT_MODEL_FLD = "Revision";
		private const string PARENT_ID_FLD = "ParentID";

		private const string ROW_INDEX_FLD = "RowIndex";

		private const string BUYING_UM_FLD = "BuyingUM";
		
		private const string BEGIN_STOCK_FLD = "BeginStock";

		private const string TOTAL_ORDER_FLD = "TotalOrder";
		private const string TOTAL_RECEIPT_FLD = "TotalReceipt";
		private const string TOTAL_PARENT_RETURN_FLD = "TotalParentReturn";
		private const string TOTAL_DESTROY_FLD = "TotalDestroy";

		private const string TOTAL_ISSUE_FLD = "TotalIssue";
		private const string TOTAL_CHILD_RETURN_FLD = "TotalChildReturn";
		private const string TOTAL_STOCK_FLD = "TotalStock";

		private const string ORDER_FLD_PREFIX = "Order_";
		private const string RECEIPT_FLD_PREFIX = "Receipt_";
		private const string PARENT_RETURN_FLD_PREFIX = "ParentReturn_";
		private const string DESTROY_FLD_PREFIX = "Destroy_";
		private const string ISSUE_FLD_PREFIX = "Issue_";
		private const string CHILD_RETURN_FLD_PREFIX = "ChildReturn_";
		private const string SHORTAGE_FLD_PREFIX = "Shortage_";
		private const string STOCK_FLD_PREFIX = "Stock_";		

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
		
		#region Outside Processing Management Report: Tuan TQ
		
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
		/// Get Location Info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetLocationInfoByID(string pstrID)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE_FLD + ", " + NAME_FLD
					+ " FROM MST_Location"
					+ " WHERE LocationID = " + pstrID;
				
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
		/// Get customer info
		/// </summary>
		/// <param name="pstrID"></param>
		/// <returns></returns>
		private string GetCustomerInfoByID(string pstrID)
		{			
			string strResult = string.Empty;

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			try
			{
				OleDbCommand ocmdPCS = null;

				string strSql =	"SELECT " + PRODUCT_CODE_FLD + ", " + NAME_FLD
					+ " FROM MST_Party"
					+ " WHERE PartyID = " + pstrID;
				
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
		/// Get order quantity of parent
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetOrderData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			DataTable dtbResultTable = new DataTable();
			
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT  PO_PurchaseOrderDetail.ProductID,";
				strSql += " SUM(PO_DeliverySchedule.DeliveryQuantity) as Quantity,";
				strSql += " Day(PO_DeliverySchedule.ScheduleDate) as DayCol";
				strSql += " FROM PO_DeliverySchedule";
				strSql += " INNER JOIN PO_PurchaseOrderDetail ON PO_PurchaseOrderDetail.PurchaseOrderDetailID = PO_DeliverySchedule.PurchaseOrderDetailID";
				strSql += " INNER JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderMaster.PurchaseOrderMasterID = PO_PurchaseOrderDetail.PurchaseOrderMasterID";
				strSql += " WHERE PO_PurchaseOrderMaster.PartyID ="  + pstrPartyID;
				strSql += " AND PO_PurchaseOrderMaster.CCNID =" + pstrCCNID;
				strSql += " AND Month(PO_DeliverySchedule.ScheduleDate) =" + pstrMonth;
				strSql += " AND Year(PO_DeliverySchedule.ScheduleDate) =" + pstrYear;
				strSql += " AND PO_PurchaseOrderDetail.Closed <> 1";
				strSql += " AND PO_PurchaseOrderDetail.ApproverID IS NOT NULL ";

				strSql += " GROUP BY PO_PurchaseOrderDetail.ProductID, Day(PO_DeliverySchedule.ScheduleDate)";
				
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		
		/// <summary>
		/// Get receipt quantity of parent
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetReceiptData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			DataTable dtbResultTable = new DataTable();
			
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT PO_PurchaseOrderReceiptDetail.ProductID,";
				strSql += " SUM(PO_PurchaseOrderReceiptDetail.ReceiveQuantity) as Quantity,";
				strSql += " Day(PO_PurchaseOrderReceiptMaster.PostDate) as DayCol";
				strSql += " FROM PO_PurchaseOrderReceiptDetail";
				strSql += " INNER JOIN PO_PurchaseOrderReceiptMaster ON PO_PurchaseOrderReceiptMaster.PurchaseOrderReceiptID = PO_PurchaseOrderReceiptDetail.PurchaseOrderReceiptID";
				strSql += " WHERE PO_PurchaseOrderReceiptMaster.ReceiptType =" + (int)PCSComUtils.Common.POReceiptTypeEnum.ByOutside;
				strSql += " AND PO_PurchaseOrderReceiptMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(PO_PurchaseOrderReceiptMaster.PostDate) =" + pstrMonth;
				strSql += " AND Year(PO_PurchaseOrderReceiptMaster.PostDate) = " + pstrYear;
				strSql += " GROUP BY PO_PurchaseOrderReceiptDetail.ProductID, Day(PO_PurchaseOrderReceiptMaster.PostDate)";
				
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		
		/// <summary>
		/// Get destroy quantity of parent
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetDestroyData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			DataTable dtbResultTable = new DataTable();
			
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT IV_MiscellaneousIssueDetail.ProductID,";
				strSql += " SUM(IV_MiscellaneousIssueDetail.Quantity) as Quantity,";
				strSql += " Day(IV_MiscellaneousIssueMaster.PostDate) as DayCol";
				strSql += " FROM IV_MiscellaneousIssueDetail";
				strSql += " INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID";
				strSql += " WHERE IV_MiscellaneousIssueMaster.IssuePurposeID = " + (int)PCSComUtils.Common.PurposeEnum.Scrap;
				strSql += " AND IV_MiscellaneousIssueMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(IV_MiscellaneousIssueMaster.PostDate) = " + pstrMonth;
				strSql += " AND Year(IV_MiscellaneousIssueMaster.PostDate) = " + pstrYear;
				
				if(pstrLocationID != string.Empty && pstrLocationID != null)
				{
					strSql += " AND IV_MiscellaneousIssueMaster.SourceLocationID = " + pstrLocationID;
				}

				strSql += " GROUP BY IV_MiscellaneousIssueDetail.ProductID, Day(IV_MiscellaneousIssueMaster.PostDate)";
				
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		
		/// <summary>
		/// Get Return Quantity of parent
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetParentReturnData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			DataTable dtbResultTable = new DataTable();
			
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT PO_ReturnToVendorDetail.ProductID,";
				strSql += " SUM(PO_ReturnToVendorDetail.Quantity) as Quantity,";
				strSql += " Day(PO_ReturnToVendorMaster.PostDate) as DayCol";
				strSql += " FROM PO_ReturnToVendorDetail";
				strSql += " INNER JOIN PO_ReturnToVendorMaster ON PO_ReturnToVendorDetail.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID";
				strSql += " WHERE PO_ReturnToVendorMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(PO_ReturnToVendorMaster.PostDate) = " + pstrMonth;
				strSql += " AND Year(PO_ReturnToVendorMaster.PostDate) = " + pstrYear;
				strSql += " GROUP BY PO_ReturnToVendorDetail.ProductID, Day(PO_ReturnToVendorMaster.PostDate)";
				
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		
		/// <summary>
		/// Get Return Quantity of children
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetChildReturnData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{			
			OleDbConnection oconPCS = null;
			DataTable dtbTemp = new DataTable();

			try
			{
				string strSql = "SELECT IV_MiscellaneousIssueDetail.ProductID,";
				strSql += " SUM(IV_MiscellaneousIssueDetail.Quantity) as Quantity,";
				strSql += " Day(IV_MiscellaneousIssueMaster.PostDate) as DayCol";
				strSql += " FROM IV_MiscellaneousIssueDetail";
				strSql += " INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID";
				strSql += " WHERE IV_MiscellaneousIssueMaster.IssuePurposeID = " + (int)PCSComUtils.Common.PurposeEnum.ReturnPrevious;
				strSql += " AND IV_MiscellaneousIssueMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(IV_MiscellaneousIssueMaster.PostDate) = " + pstrMonth;
				strSql += " AND Year(IV_MiscellaneousIssueMaster.PostDate) = " + pstrYear;

				if(pstrLocationID != string.Empty && pstrLocationID != null)
				{
					strSql += " AND IV_MiscellaneousIssueMaster.SourceLocationID = " + pstrLocationID;
				}

				strSql += " GROUP BY IV_MiscellaneousIssueDetail.ProductID, Day(IV_MiscellaneousIssueMaster.PostDate)";
                				
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);

				
				odadPCS.Fill(dtbTemp);
				
				return dtbTemp;

			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}		

		
		/// <summary>
		/// Get parent product based on child product and other additional conditions
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <param name="pstrChildID"></param>
		/// <returns></returns>
		public DataTable GetParentProductData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID, string pstrChildID)
		{
			DataTable dtbResultTable = BuildDataTemplateTable();
			
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT  DISTINCT ";
				strSql += " parent.ProductID,";
				strSql += " 0 as " + PARENT_ID_FLD + ",";
				strSql += pstrChildID + " as " + CHILD_PRODUCT_ID_FLD + ",";
				strSql += " parent.Code,";
				strSql += " parent.Description,";
				strSql += " parent.Revision,";
				strSql += " 1 as " + IS_PARENTD_FLD + ",";
				strSql += " MST_UnitOfMeasure.Code as BuyingUM";

				strSql += " FROM ITM_Product parent";
				strSql += " INNER JOIN PO_PurchaseOrderDetail poDetail ON poDetail.ProductID = parent.ProductID";
				strSql += " INNER JOIN PO_PurchaseOrderMaster poMaster ON poMaster.PurchaseOrderMasterID = poDetail.PurchaseOrderMasterID";
				strSql += " INNER JOIN MST_UnitOfMeasure ON poDetail.BuyingUMID = MST_UnitOfMeasure.UnitOfMeasureID";
				strSql += " INNER JOIN ITM_Routing ON ITM_Routing.ProductID = parent.ProductID";
				strSql += " INNER JOIN ITM_BOM ON ITM_BOM.ProductID = parent.ProductID";
				strSql += " INNER JOIN PO_DeliverySchedule ON poDetail.PurchaseOrderDetailID = PO_DeliverySchedule.PurchaseOrderDetailID";

				//Where clause
				strSql += " WHERE ITM_Routing.Type = " + (int)PCSComUtils.Common.OperationType.Outside;
				strSql += " AND poMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(PO_DeliverySchedule.ScheduleDate) = " + pstrMonth;
				strSql += " AND Year(PO_DeliverySchedule.ScheduleDate) = " + pstrYear;
				strSql += " AND ISNULL(poDetail.Closed,0) <> 1";
				strSql += " AND poDetail.ApproverID IS NOT NULL ";

				if(pstrPartyID != null && pstrPartyID != string.Empty)
				{
					strSql += " AND poMaster.PartyID = " + pstrPartyID;
				}
				
				if(pstrChildID != null && pstrChildID != string.Empty)
				{
					strSql += " AND ITM_BOM.ComponentID = " + pstrChildID;
				}
				
				strSql += " ORDER BY parent.Revision, parent.Code ASC";
								
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);

				Console.WriteLine(strSql);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}
		
		
		/// <summary>
		/// Get child product data
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetChildProductData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			const string SQL_DATETIME_FORMAT = "yyyy-MM-dd";

			DataTable dtbResultTable = BuildDataTemplateTable();
			DateTime dtmPostDate = new DateTime(int.Parse(pstrYear), int.Parse(pstrMonth), 1);

			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT DISTINCT child.ProductID,";
				strSql += " child.ProductID as " + CHILD_PRODUCT_ID_FLD + ",";
				strSql += " child.Code,";
				strSql += " child.Revision,";
				strSql += " child.Description,";
				strSql += " 0 as " + IS_PARENTD_FLD + ",";
				strSql += " ITM_BOM.ProductID as " + PARENT_ID_FLD + ",";

				strSql += "(SELECT (";

				strSql += " ISNULL(SUM(ISNULL(lc.OHQuantity, 0) - ISNULL(lc.CommitQuantity, 0)), 0)";
				strSql += " -  ISNULL((SELECT ISNULL(SUM(ISNULL(TH.Quantity,0)),0)";
				strSql += " FROM MST_TransactionHistory TH";
				strSql += " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID";
				strSql += " WHERE TT.Type = " +  (int)TransactionHistoryType.In;
				strSql += " AND TH.PostDate >= '" + dtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
				strSql += " AND TH.CCNID = lc.CCNID";
				strSql += " AND TH.MasterLocationID = lc.MasterLocationID";
				strSql += " AND TH.LocationID = lc.LocationID";				
				strSql += " AND TH.ProductID = lc.ProductID";
				strSql += " )";
				strSql += " ,0)";

				strSql += " -  ISNULL((SELECT ISNULL(SUM(ISNULL(TH.Quantity,0)),0)";
				strSql += " FROM MST_TransactionHistory TH";
				strSql += " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID";
				strSql += " WHERE TT.Type = 2";
				strSql += " AND TH.PostDate >= '" + dtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
				strSql += " AND TH.CCNID = lc.CCNID";
				strSql += " AND TH.MasterLocationID = lc.MasterLocationID";
				strSql += " AND TH.LocationID = lc.LocationID";				
				strSql += " AND TH.ProductID = lc.ProductID";
				strSql += " )";
				strSql += " ,0)";

				strSql += " +  ISNULL((SELECT ISNULL(SUM(ISNULL(TH.Quantity,0)),0)";
				strSql += " FROM MST_TransactionHistory TH";
				strSql += " INNER JOIN dbo.MST_TranType TT ON TT.TranTypeID = TH.TranTypeID";
				strSql += " WHERE TT.Type = 0";
				strSql += " AND TH.PostDate >= '" + dtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
				strSql += " AND TH.CCNID = lc.CCNID";
				strSql += " AND TH.MasterLocationID = lc.MasterLocationID";
				strSql += " AND TH.LocationID = lc.LocationID";				
				strSql += " AND TH.ProductID = lc.ProductID";
				strSql += " )";
				strSql += " ,0)"; 
				strSql += " )";
								
				strSql += " FROM IV_LocationCache lc";
				strSql += " WHERE lc.CCNID = " + pstrCCNID;				
				strSql += " AND lc.LocationID = " + pstrLocationID;
				strSql += " AND lc.ProductID = child.ProductID";
				strSql += " GROUP BY lc.ProductID, lc.LocationID, lc.MasterLocationID, lc.CCNID";

				strSql += ") as " + BEGIN_STOCK_FLD + ",";

				strSql += " ISNULL(ITM_BOM.Quantity, 0) as " + BOM_QUANTITY_FLD;
				strSql += " FROM ITM_Product child";
				strSql += " INNER JOIN ITM_BOM ON ITM_BOM.ComponentID = child.ProductID";
				strSql += " WHERE ITM_BOM.ProductID IN (";

				//Parent info
				strSql += "SELECT  DISTINCT parent.ProductID";				
				strSql += " FROM ITM_Product parent";
				strSql += " INNER JOIN PO_PurchaseOrderDetail poDetail ON poDetail.ProductID = parent.ProductID";
				strSql += " INNER JOIN PO_PurchaseOrderMaster poMaster ON poMaster.PurchaseOrderMasterID = poDetail.PurchaseOrderMasterID";				
				strSql += " INNER JOIN ITM_Routing ON ITM_Routing.ProductID = parent.ProductID";				
				strSql += " INNER JOIN PO_DeliverySchedule ON poDetail.PurchaseOrderDetailID = PO_DeliverySchedule.PurchaseOrderDetailID";

				//Where clause
				strSql += " WHERE ITM_Routing.Type = " + (int)PCSComUtils.Common.OperationType.Outside;
				strSql += " AND poMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(PO_DeliverySchedule.ScheduleDate) = " + pstrMonth;
				strSql += " AND Year(PO_DeliverySchedule.ScheduleDate) = " + pstrYear;
				strSql += " AND ISNULL(poDetail.Closed,0) <> 1";
				strSql += " AND poDetail.ApproverID IS NOT NULL ";
				
				if(pstrPartyID != null && pstrPartyID != string.Empty)
				{
					strSql += " AND poMaster.PartyID = " + pstrPartyID;
				}
				//End-Parent info

				strSql += ")";

				strSql += " ORDER BY child.Code, child.Description";
	
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);

				Console.WriteLine(strSql);

				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}

		
		/// <summary>
		/// Get Issue Quantity of children
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <param name="pstrYear"></param>
		/// <param name="pstrMonth"></param>
		/// <param name="pstrPartyID"></param>
		/// <param name="pstrLocationID"></param>
		/// <returns></returns>
		private DataTable GetIssueData(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			DataTable dtbResultTable = new DataTable();
			
			OleDbConnection oconPCS = null;
			try
			{
				string strSql = "SELECT IV_MiscellaneousIssueDetail.ProductID,";
				strSql += " SUM(IV_MiscellaneousIssueDetail.Quantity) as Quantity,";
				strSql += " Day(IV_MiscellaneousIssueMaster.PostDate) as DayCol";
				strSql += " FROM IV_MiscellaneousIssueDetail";
				strSql += " INNER JOIN IV_MiscellaneousIssueMaster ON IV_MiscellaneousIssueDetail.MiscellaneousIssueMasterID = IV_MiscellaneousIssueMaster.MiscellaneousIssueMasterID";
				strSql += " WHERE IV_MiscellaneousIssueMaster.IssuePurposeID = " + (int)PCSComUtils.Common.PurposeEnum.Outside;
				strSql += " AND IV_MiscellaneousIssueMaster.CCNID = " + pstrCCNID;
				strSql += " AND Month(IV_MiscellaneousIssueMaster.PostDate) = " + pstrMonth;
				strSql += " AND Year(IV_MiscellaneousIssueMaster.PostDate) = " + pstrYear;
				
				if(pstrLocationID != string.Empty && pstrLocationID != null)
				{
					strSql += " AND IV_MiscellaneousIssueMaster.DesLocationID = " + pstrLocationID;
				}

				strSql += " GROUP BY IV_MiscellaneousIssueDetail.ProductID, Day(IV_MiscellaneousIssueMaster.PostDate)";
				
				oconPCS = new OleDbConnection(mConnectionString);
				OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
			
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResultTable);

				return dtbResultTable;			
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed) oconPCS.Close();
					oconPCS = null;
				}
			}
		}		
				
		
		
		/// <summary>
		/// Change header & column header of report
		/// </summary>
		/// <param name="pobjReportBuilder"></param>
		/// <param name="pintReportYear.ToString()"></param>
		/// <author> Tuan TQ, 27 Mar, 2006</author>
		private void ChangeReportDisplayInfo(PCSUtils.Utils.ReportBuilder pobjReportBuilder, string pstrCCN, string pstrReportYear, string pstrReportMonth, string pstrPartyID, string pstrLocationID)
		{
			try
			{
				#region Constants			

				const string SHORT_DATE_FORMAT = "d-MMM";
						
				//Report Field's Name
				const string RPT_COMPANY_FLD  = "fldCompany";			
				const string RPT_PAGE_HEADER = "PageHeader";
				//Report field names
				const string RPT_TITLE_FIELD = "fldTitle";

				const string RPT_CCN_FLD = "CCN";
				const string RPT_MONTH_FLD = "Month";
				const string RPT_YEAR_FLD = "Year";
				const string RPT_PARTY_CODE_NAME_FLD = "Vendor";
				const string RPT_LOCATION_FLD = "Location";				

				const string RPT_DAY_PREFIX  = "fldDay_";
				const string RPT_DAY_OF_WEEK_PREFIX  = "fldDayOfWeek_";			

				const string RPT_ORDER_PREFIX  = "fldOrder_";
				const string RPT_RECEIPT_PREFIX  = "fldReceipt_";
				const string RPT_PARENT_RETURN_PREFIX  = "fldParentReturn_";
				const string RPT_DESTROY_PREFIX  = "fldDestroy_";

				const string RPT_ISSUE_PREFIX  = "fldIssue_";
				const string RPT_CHILD_RETURN_PREFIX  = "fldChildReturn_";
				const string RPT_SHORTAGE_PREFIX  = "fldShortage_";
				const string RPT_STOCK_PREFIX  = "fldStock_";

				#endregion Constants
			
				//set start day in month
				int iReportYear = int.Parse(pstrReportYear);
				int iReportMonth = int.Parse(pstrReportMonth);

				DateTime dtmReportDay = new DateTime(iReportYear, iReportMonth, 1);
				int iDaysInMonth = DateTime.DaysInMonth(iReportYear, iReportMonth);
			
				//Change report header's value
				pobjReportBuilder.DrawPredefinedField(RPT_COMPANY_FLD, GetCompanyFullName());
				
				//Draw parameters				
				System.Collections.Specialized.NameValueCollection arrParamAndValue = new System.Collections.Specialized.NameValueCollection();
				
				arrParamAndValue.Add(RPT_CCN_FLD, GetCCNInfoByID(pstrCCN));				
				arrParamAndValue.Add(RPT_YEAR_FLD, pstrReportYear);
				arrParamAndValue.Add(RPT_MONTH_FLD, pstrReportMonth);

				if(pstrPartyID != null && pstrPartyID != string.Empty)
				{
					arrParamAndValue.Add(RPT_PARTY_CODE_NAME_FLD, GetCustomerInfoByID(pstrPartyID));
				}
				
				if(pstrLocationID != null && pstrLocationID != string.Empty)
				{
					arrParamAndValue.Add(RPT_LOCATION_FLD, GetLocationInfoByID(pstrLocationID));
				}
				
				//Anchor the Parameter drawing canvas cordinate to the fldTitle
				C1.C1Report.Field fldTitle = pobjReportBuilder.GetFieldByName(RPT_TITLE_FIELD);
				double dblStartX = fldTitle.Left;
				double dblStartY = fldTitle.Top  + 1.3 * fldTitle.RenderHeight;
				pobjReportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
				pobjReportBuilder.DrawParameters(pobjReportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY, arrParamAndValue, pobjReportBuilder.Report.Font.Size);				

				//loop and change caption
				for(int i = 1; i <= iDaysInMonth; i++)
				{
					pobjReportBuilder.DrawPredefinedField(RPT_DAY_PREFIX + i, dtmReportDay.ToString(SHORT_DATE_FORMAT));
					pobjReportBuilder.DrawPredefinedField(RPT_DAY_OF_WEEK_PREFIX + i, dtmReportDay.DayOfWeek.ToString().Substring(0, 3));				

					if(dtmReportDay.DayOfWeek == DayOfWeek.Sunday)
					{
						pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].BackColor = Color.Yellow;
						pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].ForeColor = Color.Red;

						pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].BackColor = Color.Yellow;
						pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].ForeColor = Color.Red;						
					}

					dtmReportDay = dtmReportDay.AddDays(1);
				}				
				
				//Hide fields those are not displayed
				for(int i = iDaysInMonth + 1; i<= MAX_DAYS_IN_MONTH; i++)
				{
					pobjReportBuilder.Report.Fields[RPT_DAY_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[RPT_DAY_OF_WEEK_PREFIX + i.ToString()].Visible = false;
					
					//Child product report in main report
					pobjReportBuilder.Report.Fields[RPT_ISSUE_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[RPT_CHILD_RETURN_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[RPT_SHORTAGE_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[RPT_STOCK_PREFIX + i.ToString()].Visible = false;					
					
					//Parent product infor in subreport
					pobjReportBuilder.Report.Fields[SUB_REPORT_NAME].Subreport.Fields[RPT_ORDER_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[SUB_REPORT_NAME].Subreport.Fields[RPT_RECEIPT_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[SUB_REPORT_NAME].Subreport.Fields[RPT_PARENT_RETURN_PREFIX + i.ToString()].Visible = false;
					pobjReportBuilder.Report.Fields[SUB_REPORT_NAME].Subreport.Fields[RPT_DESTROY_PREFIX + i.ToString()].Visible = false;
				}				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}


		/// <summary>
		/// Recalculate summary columns of datasource
		/// </summary>
		/// <param name="pdtbData"></param>
		/// <param name="pblnParentTable"></param>
		private void RecalculateData(DataTable pdtbData, bool pblnParentTable)
		{
			try
			{				
				foreach(DataRow drow in pdtbData.Rows)
				{
					drow[ROW_INDEX_FLD] = 1;

					if(pblnParentTable)
					{
						decimal decTotalOrder = decimal.Zero;						
						decimal decTotalReceipt = decimal.Zero;
						decimal decTotalParentReturn = decimal.Zero;
						decimal decTotalDestroy = decimal.Zero;

						for(int i = 1; i <= MAX_DAYS_IN_MONTH; i++)
						{
							//sum total order quantity
							if(drow[ORDER_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&& !drow[ORDER_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decTotalOrder += decimal.Parse(drow[ORDER_FLD_PREFIX + i.ToString()].ToString());
							}

							//sum total receipt quantity
							if(drow[RECEIPT_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&& !drow[RECEIPT_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decTotalReceipt += decimal.Parse(drow[RECEIPT_FLD_PREFIX + i.ToString()].ToString());
							}

							//sum total parent return quantity
							if(drow[PARENT_RETURN_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&& !drow[PARENT_RETURN_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decTotalParentReturn += decimal.Parse(drow[PARENT_RETURN_FLD_PREFIX + i.ToString()].ToString());
							}

							//sum total destroy quantity
							if(drow[DESTROY_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&& !drow[DESTROY_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decTotalDestroy += decimal.Parse(drow[DESTROY_FLD_PREFIX + i.ToString()].ToString());
							}
						}

						//assign value for total columns
						drow[TOTAL_DESTROY_FLD] = decTotalDestroy;
						drow[TOTAL_ORDER_FLD] = decTotalOrder;
						drow[TOTAL_PARENT_RETURN_FLD] = decTotalParentReturn;
						drow[TOTAL_RECEIPT_FLD] = decTotalReceipt;
					}
					else
					{
						decimal decTotalIssue = decimal.Zero;						
						decimal decTotalChildReturn = decimal.Zero;
						decimal decStockQuatity = decimal.Zero;

						for(int i = 1; i <= MAX_DAYS_IN_MONTH; i++)
						{
							//sum total issue quantity
							if(drow[ISSUE_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&& !drow[ISSUE_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decTotalIssue += decimal.Parse(drow[ISSUE_FLD_PREFIX + i.ToString()].ToString());
							}

							//sum total child return quantity
							if(drow[CHILD_RETURN_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&& !drow[CHILD_RETURN_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decTotalChildReturn += decimal.Parse(drow[CHILD_RETURN_FLD_PREFIX + i.ToString()].ToString());
							}
						}						
						
						//Stock quantity = nearest none-null stock quantity
						for(int i = 31; i >= 1; i--)
						{
							if(drow[STOCK_FLD_PREFIX + i.ToString()].ToString().Trim() != string.Empty
							&&  !drow[STOCK_FLD_PREFIX + i.ToString()].Equals(DBNull.Value))
							{
								decStockQuatity = decimal.Parse(drow[STOCK_FLD_PREFIX + i.ToString()].ToString());
							}
						}

						//assign value for total columns
						drow[TOTAL_CHILD_RETURN_FLD] = decTotalChildReturn;
						drow[TOTAL_ISSUE_FLD] = decTotalIssue;
						drow[TOTAL_STOCK_FLD] = decStockQuatity;
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}

		
		/// <summary>
		/// Create Outside Processing data template
		/// </summary>
		/// <returns></returns>
		/// <author> Tuan TQ, 21 Mar, 2006</author>
		private DataTable BuildDataTemplateTable()
		{
			try
			{
				DataTable dtbReport = new DataTable();

				//Add column
				dtbReport.Columns.Add(ROW_INDEX_FLD, typeof(System.Int16));				
				dtbReport.Columns.Add(PRODUCT_ID_FLD, typeof(System.String));
				dtbReport.Columns.Add(CHILD_PRODUCT_ID_FLD, typeof(System.String));
				dtbReport.Columns.Add(PRODUCT_MODEL_FLD, typeof(System.String));
				dtbReport.Columns.Add(PRODUCT_CODE_FLD, typeof(System.String));
				dtbReport.Columns.Add(PRODUCT_NAME_FLD, typeof(System.String));
				dtbReport.Columns.Add(IS_PARENTD_FLD, typeof(System.String));
				dtbReport.Columns.Add(BUYING_UM_FLD, typeof(System.String));				
				dtbReport.Columns.Add(PARENT_ID_FLD, typeof(System.String));

				dtbReport.Columns.Add(BEGIN_STOCK_FLD, typeof(System.Decimal));
				dtbReport.Columns.Add(BOM_QUANTITY_FLD, typeof(System.Decimal));
				
				for(int i = 1; i <= MAX_DAYS_IN_MONTH; i++)
				{
					dtbReport.Columns.Add(ORDER_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(RECEIPT_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(PARENT_RETURN_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(DESTROY_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(ISSUE_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(CHILD_RETURN_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(SHORTAGE_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					dtbReport.Columns.Add(STOCK_FLD_PREFIX + i.ToString(), typeof(System.Decimal));
					
					//set formula for [Shortage] and [Stock In Vendor] columns
					if(i == 1)
					{
						dtbReport.Columns[SHORTAGE_FLD_PREFIX + i.ToString()].Expression = "ISNULL(" + ORDER_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + BEGIN_STOCK_FLD + ", 0) - ( ISNULL(" + ISSUE_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + CHILD_RETURN_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + DESTROY_FLD_PREFIX + i.ToString() + ", 0))";
						dtbReport.Columns[STOCK_FLD_PREFIX + i.ToString()].Expression = "ISNULL(" + BEGIN_STOCK_FLD + ", 0) + ISNULL(" + ISSUE_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + CHILD_RETURN_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + RECEIPT_FLD_PREFIX + i.ToString() + ", 0) + ISNULL("  + PARENT_RETURN_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + DESTROY_FLD_PREFIX + i.ToString() + ", 0)";
					}
					else
					{
						dtbReport.Columns[SHORTAGE_FLD_PREFIX + i.ToString()].Expression = "ISNULL( " + ORDER_FLD_PREFIX + i.ToString() + ", 0) + ISNULL(" + SHORTAGE_FLD_PREFIX + Convert.ToSingle(i-1) + ", 0) - (ISNULL(" + ISSUE_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + CHILD_RETURN_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + DESTROY_FLD_PREFIX + i.ToString() + ", 0))";
						dtbReport.Columns[STOCK_FLD_PREFIX + i.ToString()].Expression = "ISNULL(" + STOCK_FLD_PREFIX + Convert.ToString(i-1) + ", 0) + ISNULL(" + ISSUE_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + CHILD_RETURN_FLD_PREFIX + i.ToString() + ", 0) - ISNULL("  + RECEIPT_FLD_PREFIX + i.ToString() + ", 0) + ISNULL(" + PARENT_RETURN_FLD_PREFIX + i.ToString() + ", 0) - ISNULL(" + DESTROY_FLD_PREFIX + i.ToString() + ", 0)";
					}
				}

				dtbReport.Columns.Add(TOTAL_DESTROY_FLD, typeof(System.Decimal));
				dtbReport.Columns.Add(TOTAL_ORDER_FLD, typeof(System.Decimal));
				dtbReport.Columns.Add(TOTAL_PARENT_RETURN_FLD, typeof(System.Decimal));
				dtbReport.Columns.Add(TOTAL_RECEIPT_FLD, typeof(System.Decimal));

				dtbReport.Columns.Add(TOTAL_CHILD_RETURN_FLD, typeof(System.Decimal));
				dtbReport.Columns.Add(TOTAL_ISSUE_FLD, typeof(System.Decimal));
				dtbReport.Columns.Add(TOTAL_STOCK_FLD, typeof(System.Decimal));

				dtbReport.DefaultView.Sort = PRODUCT_CODE_FLD + ", " + PRODUCT_NAME_FLD;
				dtbReport.Columns[ROW_INDEX_FLD].DefaultValue = 1;

				return dtbReport;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}		

		
		/// <summary>
		/// Build Data table for Outside Processing Report
		/// </summary>
		/// <param name="pdtbData">Source Data</param>
		/// <returns>Data with template as data template of report</returns>
		/// <author> Tuan TQ, 21 Mar, 2006</author>
		private DataTable BuildReportTable(DataTable pdtbSourceData, int pintDaysInMonth, string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{		
			const string QUANTITY_FLD = "Quantity";
			const string DAY_FLD = "DayCol";
            
			try
			{
				if(pdtbSourceData == null)
				{
					return null;
				}

				//Get child data
				DataTable dtbChildData = GetChildProductData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				//Order data (of Parent Product)
				DataTable dtbOrderData;
				dtbOrderData = GetOrderData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				//Destroy data (of Parent Product)
				DataTable dtbDestroyData;
				dtbDestroyData = GetDestroyData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);

				//Receipt data (of Parent Product)
				DataTable dtbReceiptData;
				dtbReceiptData = GetReceiptData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				//Return data (of Parent Product)
				DataTable dtbParentReturnData;
				dtbParentReturnData = GetParentReturnData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);

				//Return data (of Child Product)
				DataTable dtbChildReturnData;
				dtbChildReturnData = GetChildReturnData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				//Issue return data (of Child Product)
				DataTable dtbIssueData;
				dtbIssueData = GetIssueData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				//Collection of processed item				
				DataRow[] arrItem = null;
			
				//select condition string
				string strSelectCondition = string.Empty;
				
				foreach(DataRow dtRowSource in pdtbSourceData.Rows)
				{		
					strSelectCondition = PRODUCT_ID_FLD + "=" + dtRowSource[PRODUCT_ID_FLD].ToString();					
					
					if(dtRowSource[IS_PARENTD_FLD].ToString().Trim() == IS_PARENT_VALUE)
					{
						#region Update Parent Product Infor
						//Get order quantity
						arrItem = dtbOrderData.Select(strSelectCondition);
						foreach(DataRow drow in arrItem)
						{
							dtRowSource[ORDER_FLD_PREFIX + drow[DAY_FLD].ToString()] = drow[QUANTITY_FLD];
						}
						
						//Get order quantity
						arrItem = dtbReceiptData.Select(strSelectCondition);
						foreach(DataRow drow in arrItem)
						{
							dtRowSource[RECEIPT_FLD_PREFIX + drow[DAY_FLD].ToString()] = drow[QUANTITY_FLD];
						}

						//Get return quantity
						arrItem = dtbParentReturnData.Select(strSelectCondition);
						foreach(DataRow drow in arrItem)
						{
							dtRowSource[PARENT_RETURN_FLD_PREFIX + drow[DAY_FLD].ToString()] = drow[QUANTITY_FLD];
						}

						//Get return quantity
						arrItem = dtbDestroyData.Select(strSelectCondition);
						foreach(DataRow drow in arrItem)
						{
							dtRowSource[DESTROY_FLD_PREFIX + drow[DAY_FLD].ToString()] = drow[QUANTITY_FLD];
						}
						#endregion
					}					
					else
					{
						#region Update child product infor

						//Get issue quantity
						arrItem = dtbIssueData.Select(strSelectCondition);
						foreach(DataRow drow in arrItem)
						{
							dtRowSource[ISSUE_FLD_PREFIX + drow[DAY_FLD].ToString()] = drow[QUANTITY_FLD];
						}
						
						//Get child return quantity
						arrItem = dtbChildReturnData.Select(strSelectCondition);
						foreach(DataRow drow in arrItem)
						{
							dtRowSource[CHILD_RETURN_FLD_PREFIX + drow[DAY_FLD].ToString()] = drow[QUANTITY_FLD];
						}
											
						#region Get child data to calculate Shortage & Stock quantity

						decimal decQuantity = decimal.Zero;
						string strFilter = 	PRODUCT_ID_FLD + "=" + dtRowSource[PARENT_ID_FLD].ToString();							

						DataRow[] arrParentInfo;

						//Get order quantity
						arrParentInfo = dtbOrderData.Select(strFilter);
						foreach(DataRow drowParent in arrParentInfo)
						{
							decQuantity = decimal.Parse(drowParent[QUANTITY_FLD].ToString()) 
									     * decimal.Parse(dtRowSource[BOM_QUANTITY_FLD].ToString());

							if(dtRowSource[ORDER_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString().Trim() != string.Empty
							&& !dtRowSource[ORDER_FLD_PREFIX + drowParent[DAY_FLD].ToString()].Equals(DBNull.Value))
							{
								decQuantity += decimal.Parse(dtRowSource[ORDER_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString());
							}

							dtRowSource[ORDER_FLD_PREFIX + drowParent[DAY_FLD].ToString()] = decQuantity;
						}
						
						//Get receipt quantity
						arrParentInfo = dtbReceiptData.Select(strFilter);
						foreach(DataRow drowParent in arrParentInfo)
						{							
							decQuantity = 0;
							if(drowParent[QUANTITY_FLD].ToString().Trim() != string.Empty
							&& !drowParent[QUANTITY_FLD].Equals(DBNull.Value)
							&& dtRowSource[BOM_QUANTITY_FLD].ToString().Trim() != string.Empty
							&& !dtRowSource[BOM_QUANTITY_FLD].Equals(DBNull.Value))
							{
								decQuantity = decimal.Parse(drowParent[QUANTITY_FLD].ToString()) * decimal.Parse(dtRowSource[BOM_QUANTITY_FLD].ToString());
							}

							if(dtRowSource[RECEIPT_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString().Trim() != string.Empty
							&& !dtRowSource[RECEIPT_FLD_PREFIX + drowParent[DAY_FLD].ToString()].Equals(DBNull.Value))
							{
								decQuantity += decimal.Parse(dtRowSource[RECEIPT_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString());	
							}

							dtRowSource[RECEIPT_FLD_PREFIX + drowParent[DAY_FLD].ToString()] = decQuantity;								
						}

						//Get return quantity
						arrParentInfo = dtbParentReturnData.Select(strFilter);
						foreach(DataRow drowParent in arrParentInfo)
						{
							decQuantity = 0;
							if(drowParent[QUANTITY_FLD].ToString().Trim() != string.Empty
								&& !drowParent[QUANTITY_FLD].Equals(DBNull.Value)
								&& dtRowSource[BOM_QUANTITY_FLD].ToString().Trim() != string.Empty
								&& !dtRowSource[BOM_QUANTITY_FLD].Equals(DBNull.Value))
							{
								decQuantity = decimal.Parse(drowParent[QUANTITY_FLD].ToString()) * decimal.Parse(dtRowSource[BOM_QUANTITY_FLD].ToString());
							}								    

							if(dtRowSource[PARENT_RETURN_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString().Trim() != string.Empty
							&& !dtRowSource[PARENT_RETURN_FLD_PREFIX + drowParent[DAY_FLD].ToString()].Equals(DBNull.Value))
							{
								decQuantity += decimal.Parse(dtRowSource[PARENT_RETURN_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString());
							}

							dtRowSource[PARENT_RETURN_FLD_PREFIX + drowParent[DAY_FLD].ToString()] = decQuantity;
						}

						//Get destroy quantity
						arrParentInfo = dtbDestroyData.Select(strFilter);
						foreach(DataRow drowParent in arrParentInfo)
						{
							decQuantity = 0;
							if(drowParent[QUANTITY_FLD].ToString().Trim() != string.Empty
								&& !drowParent[QUANTITY_FLD].Equals(DBNull.Value)
								&& dtRowSource[BOM_QUANTITY_FLD].ToString().Trim() != string.Empty
								&& !dtRowSource[BOM_QUANTITY_FLD].Equals(DBNull.Value))
							{
								decQuantity = decimal.Parse(drowParent[QUANTITY_FLD].ToString()) * decimal.Parse(dtRowSource[BOM_QUANTITY_FLD].ToString());
							}

							if(dtRowSource[DESTROY_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString().Trim() != string.Empty
							&& !dtRowSource[DESTROY_FLD_PREFIX + drowParent[DAY_FLD].ToString()].Equals(DBNull.Value))
							{
								decQuantity += decimal.Parse(dtRowSource[DESTROY_FLD_PREFIX + drowParent[DAY_FLD].ToString()].ToString());
							}

							dtRowSource[DESTROY_FLD_PREFIX + drowParent[DAY_FLD].ToString()] = decQuantity;							
						}

						#endregion

						#endregion
					}
					
				}

				return pdtbSourceData;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}				
				

		#endregion
		
		#region Public Method

		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}		
		
		/// <summary>
		/// Build and show Outside Processing Report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <author> Tuan TQ, 21 Mar, 2006</author>
		public DataTable ExecuteReport(string pstrCCNID, string pstrYear, string pstrMonth, string pstrPartyID, string pstrLocationID)
		{
			try
			{				
				DataTable dtbParentSource = null;
				
				DataTable dtbChildSource = null;

				int iReportMonth = int.Parse(pstrMonth);
				int iReportYear  = int.Parse(pstrYear);				
				
				//initiate parent source
				dtbParentSource = BuildDataTemplateTable();

				//Get child data
				dtbChildSource = GetChildProductData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				if(dtbChildSource == null || dtbParentSource  == null)
				{
					return null;
				}
				
				//Get total days in report month
				int iDaysInMonth = DateTime.DaysInMonth(iReportYear, iReportMonth);				
	
				foreach(DataRow drowChild in dtbChildSource.Rows)
				{
					string strChildProductID = drowChild[PRODUCT_ID_FLD].ToString();

					DataTable dtbParentSourceTemp = GetParentProductData(pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID, strChildProductID);
					if(dtbParentSourceTemp != null)
					{
						BuildReportTable(dtbParentSourceTemp, iDaysInMonth, pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
					}				
						
					foreach(DataRow drowParent in dtbParentSourceTemp.Rows)
					{
						//Check duplicate
						string strFilter = PRODUCT_ID_FLD + " = '" + drowParent[PRODUCT_ID_FLD].ToString() ;
						strFilter += "' AND " + CHILD_PRODUCT_ID_FLD + " = '" + strChildProductID + "'";

						Console.WriteLine("strFilter = " + strFilter);
						DataRow[] arrDuplicated = dtbParentSource.Select(strFilter);

						//Add to Parent DataSource if not exist
						if(arrDuplicated.Length == 0)
						{
							DataRow drowNewParent = dtbParentSource.NewRow();
							for(int i = 0; i< dtbParentSource.Columns.Count; i++)
							{
								drowNewParent[i] = drowParent[i];
							}
							dtbParentSource.Rows.Add(drowNewParent);
						}						
					}										
				}

				//Build datasource again
				BuildReportTable(dtbChildSource, iDaysInMonth, pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);

				//Create builder object
				ReportWithSubReportBuilder reportBuilder = new ReportWithSubReportBuilder();				
				
				//check data values
				RecalculateData(dtbParentSource, true);
				RecalculateData(dtbChildSource, false);
				
				//Set report name
				reportBuilder.ReportName = REPORT_NAME;
				//Set Datasource
				reportBuilder.SourceDataTable = dtbChildSource;
				//set sub report datasource
				reportBuilder.SubReportDataSources.Add(SUB_REPORT_NAME, dtbParentSource);				
				
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
				
				//Change report header & column header
				ChangeReportDisplayInfo(reportBuilder, pstrCCNID, pstrYear, pstrMonth, pstrPartyID, pstrLocationID);
				
				try
				{
					printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FIELD).Text;
				}
				catch
				{}

				reportBuilder.RefreshReport();
				printPreview.Show();

				//return table
				return dtbChildSource;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}
		
		#endregion Public Method			
	}
}
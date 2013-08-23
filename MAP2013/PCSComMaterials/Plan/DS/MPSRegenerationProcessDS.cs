using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Plan.DS
{
	/// <summary>
	/// Summary description for MPSRegenerationProcessDS.
	/// </summary>
	public class MPSRegenerationProcessDS 
	{
		private const string THIS = "PCSComMaterials.Plan.DS.MPSRegenerationProcessDS";
		public MPSRegenerationProcessDS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Object GetObjectVO(int pintID)
		{
			throw new NotImplementedException();
		}

		public DataSet List()
		{
			throw new NotImplementedException();
		}

		public void Delete(int pintID)
		{
			throw new NotImplementedException();
		}

		public void Update(Object pobjObjecVO)
		{
			throw new NotImplementedException();
		}

		public void Add(Object pobjObjectVO)
		{
			throw new NotImplementedException();
		}

		public void UpdateDataSet(DataSet pData)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Retrieve all components of BOM and return DataTable.
		/// - pintProductID = current ProductID
		/// 
		/// SELECT BomID, ProductID, ComponentID, EffectiveBeginDate, EffectiveEndDate, LeadTimeOffset, Quantity, RoutingID, 
		/// 	Shrink, Ancestor, EffectiveEndDay, EffectiveBeginDay, Alternative, Line, 
		/// 	ITM_Product.PlanType, ITM_Product.LotSize, ITM_Product.OrderQuantity, ITM_Product.QuantityMultiple,
		/// 	ISNULL(ITM_Product.OrderPolicyID, 0) AS ITM_Product.OrderPolicy, ITM_Product.ScrapRate
		/// FROM  ITM_BOM JOIN ITM_Product ON ITM_BOM.ProductID = ITM_Product.ProductID
		/// WHERE ProductID = pintProductID
		/// </summary>
		public DataTable GetBOMComponents(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetBOMComponents()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ITM_Product.ProductID, ITM_Product.Code, ITM_Product.Revision, "
					+ "ITM_Product.Description, ITM_Product.SetupDate, ITM_Product.VAT, ITM_Product.ImportTax, "
					+ "ITM_Product.ExportTax, ITM_Product.SpecialTax, ITM_Product.MakeItem, "
					+ "ITM_Product.PartNumber, ITM_Product.OtherInfo1, ITM_Product.OtherInfo2, "
					+ "ITM_Product.Length, ITM_Product.Width, ITM_Product.Height, ITM_Product.Weight, "
					+ "ITM_Product.FinishedGoods, ITM_Product.ShelfLife, ITM_Product.LotControl, "
					+ "ITM_Product.QAStatus, ITM_Product.Stock, ITM_Product.PlanType, ITM_Product.AutoConversion, "
					+ "ITM_Product.OrderQuantity, ITM_Product.LTRequisition, ITM_Product.LTSafetyStock, "
					+ "ITM_Product.OrderQuantityMultiple, ITM_Product.ScrapPercent, ITM_Product.MinimumStock, "
					+ "ITM_Product.MaximumStock, ITM_Product.ConversionTolerance, ITM_Product.VoucherTolerance, "
					+ "ITM_Product.ReceiveTolerance, ITM_Product.IssueSize, ITM_Product.LTFixedTime, "
					+ "ITM_Product.LTVariableTime, ITM_Product.LTOrderPrepare, ITM_Product.LTShippingPrepare, "
					+ "ITM_Product.LTSalesATP, ITM_Product.ShipToleranceID, ITM_Product.BuyerID, "
					+ "ITM_Product.BOMDescription, ITM_Product.BomIncrement, ITM_Product.RoutingDescription, "
					+ "ITM_Product.CreateDateTime, ITM_Product.UpdateDateTime, ITM_Product.CostMethod, "
					+ "ITM_Product.RoutingIncrement, ITM_Product.CCNID, ITM_Product.CategoryID, "
					+ "ITM_Product.CostCenterID, ITM_Product.DeleteReasonID, ITM_Product.DeliveryPolicyID, "
					+ "ITM_Product.FormatCodeID, ITM_Product.FreightClassID, ITM_Product.HazardID, "
					+ "ITM_Product.OrderPolicyID, ITM_Product.OrderRuleID, ITM_Product.SourceID, "
					+ "ITM_Product.StockUMID, ITM_Product.SellingUMID, ITM_Product.HeightUMID, "
					+ "ITM_Product.WidthUMID, ITM_Product.LengthUMID, ITM_Product.BuyingUMID, "
					+ "ITM_Product.WeightUMID, ITM_Product.LotSize, ITM_Product.MasterLocationID, "
					+ "ITM_Product.LocationID, ITM_Product.BinID, ITM_Product.PrimaryVendorID, "
					+ "ITM_Product.VendorLocationID, ITM_Product.OrderPoint, ITM_Product.SafetyStock, "
					+ "ITM_Product.AGCID, ITM_Product.ParentProductID, ITM_Product.LTDockToStock, "
					+ "ITM_Product.PartNameVN, ITM_Product.LicenseFee, ITM_Product.InventorID, "
					+ "ITM_Product.ProductTypeID, ITM_Product.TaxCode, ITM_BOM.ProductID AS ParentID, "
					+ "ITM_BOM.Quantity AS RequiredQuantity FROM ITM_Product "
					+ "JOIN ITM_BOM ON ITM_Product.ProductID = ITM_BOM.ComponentID "
					+ "WHERE ITM_BOM.ProductID = " + pintProductID
					+ " ORDER BY ITM_Product.ProductID";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve demand from Sale Orders
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintProductID">Product</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetrieveSaleOrders(int pintCCNID, int pintProductID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveSaleOrders()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CODE_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", "
					+ "(" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - "
					+ " ISNULL((SELECT SUM(" + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ") "
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
					+ " = " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "), 0)) "
					+ "AS 'DemandQuantity', "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UMRATE_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME
					+ " JOIN " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
					+ " JOIN  " + SO_DeliveryScheduleTable.TABLE_NAME
					+ " ON " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " WHERE " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD
					+ " - ISNULL((SELECT SUM(" + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ") "
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
					+ " = " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD + "), 0) > 0 "
					+ " AND " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " >= ?"
					+ " AND " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= ?"
					+ " AND " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + " = " + pintProductID
					+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + " = " + pintMasterLocationID
					+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CCNID_FLD + " = " + pintCCNID
                    + " GROUP BY " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CODE_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UMRATE_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " ORDER BY " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD;
					
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve demand from Sale Orders
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetrieveSaleOrders(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveSaleOrders()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CODE_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", "
					+ "(" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - "
					+ " ISNULL((SELECT SUM(ISNULL(" + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ",0)) "
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
					+ " = " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + "), 0)) "
					+ "AS 'DemandQuantity', "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UMRATE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " FROM " + SO_SaleOrderMasterTable.TABLE_NAME
					+ " JOIN " + SO_SaleOrderDetailTable.TABLE_NAME
					+ " ON " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
					+ " JOIN  " + SO_DeliveryScheduleTable.TABLE_NAME
					+ " ON " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD
					+ " = " + SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
					+ " WHERE " + "(" + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - "
					+ " (SELECT ISNULL(SUM(ISNULL(" + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ",0)), 0) "
					+ " FROM " + SO_CommitInventoryDetailTable.TABLE_NAME
					+ " WHERE " + SO_CommitInventoryDetailTable.TABLE_NAME + "." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
					+ " = " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ")) > 0"
					+ " AND " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " >= ?"
					+ " AND " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " <= ?"
					+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + " = " + pintMasterLocationID
					+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CCNID_FLD + " = " + pintCCNID
					+ " GROUP BY " + SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.CODE_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", "
					+ SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.UMRATE_FLD + ", "
					+ SO_SaleOrderDetailTable.TABLE_NAME + "." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ", "
					+ SO_SaleOrderMasterTable.TABLE_NAME + "." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
					+ " ORDER BY " + SO_DeliveryScheduleTable.TABLE_NAME + "." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " ASC";
					
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		public DataTable RetrieveParents(int pintCCNID, int pintComponentID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveParents()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT DISTINCT (ISNULL("
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ",0) * ISNULL("
					+ PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.REQUIREDQUANTITY_FLD + ",0) -"
					+ " (SELECT ISNULL(SUM(" + PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + "),0) "
					+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME
					+ " JOIN " + PRO_IssueMaterialMasterTable.TABLE_NAME
					+ " ON " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD
					+ "=" + PRO_IssueMaterialMasterTable.TABLE_NAME + "." + PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD
					+ " WHERE " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.PRODUCTID_FLD + "=" + pintComponentID
					+ " AND " + PRO_IssueMaterialMasterTable.TABLE_NAME + "." + PRO_IssueMaterialMasterTable.WORKORDERDETAILID_FLD 
					+ "= " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ")) AS 'DemandQuantity',"
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + ", "
					+ ITM_ProductTable.CODE_FLD + ", " + ITM_ProductTable.REVISION_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + ", "
					+ ITM_ProductTable.SETUPDATE_FLD + ", " + ITM_ProductTable.VAT_FLD + ", "
					+ ITM_ProductTable.IMPORTTAX_FLD + ", " + ITM_ProductTable.EXPORTTAX_FLD + ", "
					+ ITM_ProductTable.SPECIALTAX_FLD + ", " + ITM_ProductTable.MAKEITEM_FLD + ", "
					+ ITM_ProductTable.PARTNUMBER_FLD + ", " + ITM_ProductTable.OTHERINFO1_FLD + ", "
					+ ITM_ProductTable.OTHERINFO2_FLD + ", " + ITM_ProductTable.LENGTH_FLD + ", "
					+ ITM_ProductTable.WIDTH_FLD + ", " + ITM_ProductTable.HEIGHT_FLD + ", "
					+ ITM_ProductTable.WEIGHT_FLD + ", " + ITM_ProductTable.FINISHEDGOODS_FLD + ", "
					+ ITM_ProductTable.SHELFLIFE_FLD + ", " + ITM_ProductTable.LOTCONTROL_FLD + ", "
					+ ITM_ProductTable.QASTATUS_FLD + ", " + ITM_ProductTable.STOCK_FLD + ", "
					+ ITM_ProductTable.PLANTYPE_FLD + ", " + ITM_ProductTable.AUTOCONVERSION_FLD + ", "
					+ ITM_ProductTable.LTREQUISITION_FLD + ", " + ITM_ProductTable.LTSAFETYSTOCK_FLD + ", "
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ", " + ITM_ProductTable.SCRAPPERCENT_FLD + ", "
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ", " + ITM_ProductTable.MAXIMUMSTOCK_FLD + ", "
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ", " + ITM_ProductTable.VOUCHERTOLERANCE_FLD + ", "
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ", " + ITM_ProductTable.ISSUESIZE_FLD + ", "
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ", " + ITM_ProductTable.LTVARIABLETIME_FLD + ", "
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ", " + ITM_ProductTable.LTORDERPREPARE_FLD + ", "
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ", " + ITM_ProductTable.LTSALESATP_FLD + ", "
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ", " + ITM_ProductTable.BUYERID_FLD + ", "
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ", " + ITM_ProductTable.BOMINCREMENT_FLD + ", "
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ", " + ITM_ProductTable.CREATEDATETIME_FLD + ", "
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ", " + ITM_ProductTable.COSTMETHOD_FLD + ", "
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CCNID_FLD + ", "
					+ ITM_ProductTable.CATEGORYID_FLD + ", " + ITM_ProductTable.COSTCENTERID_FLD + ", "
					+ ITM_ProductTable.DELETEREASONID_FLD + ", " + ITM_ProductTable.DELIVERYPOLICYID_FLD + ", "
					+ ITM_ProductTable.FORMATCODEID_FLD + ", " + ITM_ProductTable.FREIGHTCLASSID_FLD + ", "
					+ ITM_ProductTable.HAZARDID_FLD + ", " + ITM_ProductTable.ORDERPOLICYID_FLD + ", "
					+ ITM_ProductTable.ORDERRULEID_FLD + ", " + ITM_ProductTable.SOURCEID_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.STOCKUMID_FLD + ", "
					+ ITM_ProductTable.SELLINGUMID_FLD + ", " + ITM_ProductTable.HEIGHTUMID_FLD + ", "
					+ ITM_ProductTable.WIDTHUMID_FLD + ", " + ITM_ProductTable.LENGTHUMID_FLD + ", "
					+ ITM_ProductTable.BUYINGUMID_FLD + ", " + ITM_ProductTable.WEIGHTUMID_FLD + ", "
					+ ITM_ProductTable.LOTSIZE_FLD + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.MASTERLOCATIONID_FLD + ", "
					+ ITM_ProductTable.LOCATIONID_FLD + ", " + ITM_ProductTable.BINID_FLD + ", "
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ", " + ITM_ProductTable.VENDORLOCATIONID_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD + ", " + ITM_ProductTable.SAFETYSTOCK_FLD + ", "
					+ ITM_ProductTable.AGCID_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME + " JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + "=" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD
					+ " JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD
					+ " JOIN " + PRO_WorkOrderBomMasterTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + PRO_WorkOrderBomMasterTable.TABLE_NAME + "." + PRO_WorkOrderBomMasterTable.WORKORDERDETAILID_FLD
					+ " JOIN " + PRO_WorkOrderBomDetailTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderBomMasterTable.TABLE_NAME + "." + PRO_WorkOrderBomMasterTable.WORKORDERBOMMASTERID_FLD + "=" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.WORKORDERBOMMASTERID_FLD
					+ " WHERE " + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + "=" + pintComponentID
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD
					+ " =" + pintMasterLocationID
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ">= ?"
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + "<= ?";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve demand from Parent work orders
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetrieveParents(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveParents()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT DISTINCT (ISNULL("
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ",0) * ISNULL("
					+ PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.REQUIREDQUANTITY_FLD + ",0) -"
					+ " ISNULL((SELECT SUM(ISNULL(" + PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ",0)) "
					+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME
					+ " JOIN " + PRO_IssueMaterialMasterTable.TABLE_NAME
					+ " ON " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD
					+ "=" + PRO_IssueMaterialMasterTable.TABLE_NAME + "." + PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD
					+ " WHERE " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.PRODUCTID_FLD
					+ "=" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD
					+ " AND " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD 
					+ "= " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "), 0)) AS 'DemandQuantity',"
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ", "
					+ PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + ", "
					+ "ISNULL(" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.SHRINK_FLD + ", 0) AS '" + PRO_WorkOrderBomDetailTable.SHRINK_FLD + "',"
					+ "ISNULL(" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD + ", 0) AS '" + PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD + "'"
					+ " FROM " + ITM_ProductTable.TABLE_NAME + " JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + "=" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD
					+ " JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD
					+ " JOIN " + PRO_WorkOrderBomMasterTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + PRO_WorkOrderBomMasterTable.TABLE_NAME + "." + PRO_WorkOrderBomMasterTable.WORKORDERDETAILID_FLD
					+ " JOIN " + PRO_WorkOrderBomDetailTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderBomMasterTable.TABLE_NAME + "." + PRO_WorkOrderBomMasterTable.WORKORDERBOMMASTERID_FLD + "=" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.WORKORDERBOMMASTERID_FLD
					+ " WHERE " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD
					+ " =" + pintMasterLocationID
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.Released
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ">= ?"
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + "<= ?"
					+ " ORDER BY " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve demand from Parent work orders
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">As Of Date</param>
		/// <param name="pdtmToDate">Due Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetrieveParents(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveParents()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT DISTINCT (ISNULL("
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ",0) * ISNULL("
					+ PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.REQUIREDQUANTITY_FLD + ",0) -"
					+ " ISNULL((SELECT SUM(ISNULL(" + PRO_IssueMaterialDetailTable.COMMITQUANTITY_FLD + ",0)) "
					+ " FROM " + PRO_IssueMaterialDetailTable.TABLE_NAME
					+ " JOIN " + PRO_IssueMaterialMasterTable.TABLE_NAME
					+ " ON " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.ISSUEMATERIALMASTERID_FLD
					+ "=" + PRO_IssueMaterialMasterTable.TABLE_NAME + "." + PRO_IssueMaterialMasterTable.ISSUEMATERIALMASTERID_FLD
					+ " WHERE " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.PRODUCTID_FLD
					+ "=" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD
					+ " AND " + PRO_IssueMaterialDetailTable.TABLE_NAME + "." + PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD 
					+ "= " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "), 0)) AS 'DemandQuantity',"
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ", "
					+ PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + ", "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ", "
					+ ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + ", "
					+ "ISNULL(" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.SHRINK_FLD + ", 0) AS '" + PRO_WorkOrderBomDetailTable.SHRINK_FLD + "',"
					+ "ISNULL(" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD + ", 0) AS '" + PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD + "'"
					+ " FROM " + ITM_ProductTable.TABLE_NAME + " JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + "=" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD
					+ " JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD
					+ " JOIN " + PRO_WorkOrderBomMasterTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + PRO_WorkOrderBomMasterTable.TABLE_NAME + "." + PRO_WorkOrderBomMasterTable.WORKORDERDETAILID_FLD
					+ " JOIN " + PRO_WorkOrderBomDetailTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderBomMasterTable.TABLE_NAME + "." + PRO_WorkOrderBomMasterTable.WORKORDERBOMMASTERID_FLD + "=" + PRO_WorkOrderBomDetailTable.TABLE_NAME + "." + PRO_WorkOrderBomDetailTable.WORKORDERBOMMASTERID_FLD
					+ " WHERE " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STATUS_FLD + "=" + (int)WOLineStatus.Released
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ">= ?"
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + "<= ?"
					+ " ORDER BY " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmFromDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmToDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		public DataTable RetrieveSupplyFromWO(int pintCCNID, int pintMasterLocationID, int pintProductID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveSupplyFromWO()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ", "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.TRANSDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.LINE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ", "
					+ "(" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + "-"
					+ "(SELECT ISNULL(SUM(" + PRO_WorkOrderCompletionTable.TABLE_NAME + "." + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + "),0) "
					+ " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME
					+ " WHERE " + PRO_WorkOrderCompletionTable.TABLE_NAME + "." + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD
					+ " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD
					+ " AND " + PRO_WorkOrderCompletionTable.TABLE_NAME + "." + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD 
					+ " = " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ")) AS 'SupplyQuantity'" + ","
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ", "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD
					+ " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD
					+ " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD
					+ " WHERE " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + " = " + pintProductID
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ">= ?"
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + "<= ?"
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD
					+ " =" + pintMasterLocationID
					+ " ORDER BY " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve supply from Work Orders
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetrieveSupplyFromWO(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveSupplyFromWO()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ", "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.TRANSDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.LINE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ", "
					+ "(" + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + "-"
					+ " ISNULL((SELECT SUM(ISNULL(" + PRO_WorkOrderCompletionTable.TABLE_NAME + "." + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ",0)) "
					+ " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME
					+ " WHERE " + PRO_WorkOrderCompletionTable.TABLE_NAME + "." + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD
					+ " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD
					+ " AND " + PRO_WorkOrderCompletionTable.TABLE_NAME + "." + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD 
					+ " = " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "), 0)) AS 'SupplyQuantity'" + ","
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ", "
					+ PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ", "
					+ PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD
					+ " FROM " + PRO_WorkOrderMasterTable.TABLE_NAME
					+ " JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ " ON " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD
					+ " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD
					+ " WHERE " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ">= ?"
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + "<= ?"
					+ " AND " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STATUS_FLD + "= " + (int)WOLineStatus.Released
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD
					+ " =" + pintMasterLocationID
					+ " ORDER BY " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + " ASC";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		public DataTable RetriveSupplyFromPO(int pintCCNID, int pintMasterLocationID, int pintProductID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetriveSupplyFromPO()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT	"
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + ", "
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + ", "
					+ "(ISNULL(" + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", 0) - ISNULL("
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ",0)) AS 'SupplyQuantity', "
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD 
					+ "=" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
					+ " JOIN " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ "=" + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD
					+ "- ISNULL(" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ",0) > 0"
					+ " AND ISNULL(" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + ", 0) > 0"
					+ " AND " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">= ?"
					+ " AND " + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<= ?"
					+ " AND " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD
					+ " =" + pintMasterLocationID
					+ " AND " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CCNID_FLD + "=" + pintCCNID;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve supply from Purchase Orders
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetriveSupplyFromPO(int pintCCNID, int pintMasterLocationID, DateTime pdtmAsOfDate, DateTime pdtmDueDate)
		{
			const string METHOD_NAME = THIS + ".RetriveSupplyFromPO()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT	"
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + ", "
					+ PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.ORDERDATE_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ", "
					+ PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.LINE_FLD + ", "
					+ "(ISNULL(" + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", 0) - ISNULL("
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ",0)) AS 'SupplyQuantity', "
					+ PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD
					+ " FROM " + PO_PurchaseOrderMasterTable.TABLE_NAME
					+ " JOIN " + PO_PurchaseOrderDetailTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD 
					+ "=" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
					+ " JOIN " + PO_DeliveryScheduleTable.TABLE_NAME
					+ " ON " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
					+ "=" + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD
					+ " WHERE " + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD
					+ "- ISNULL(" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ",0) > 0"
					+ " AND ISNULL(" + PO_PurchaseOrderDetailTable.TABLE_NAME + "." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + ", 0) > 0"
					+ " AND " + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">= ?"
					+ " AND " + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<= ?"
					+ " AND " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD
					+ " =" + pintMasterLocationID
					+ " AND " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CCNID_FLD + "=" + pintCCNID
					+ " ORDER BY " + PO_DeliveryScheduleTable.TABLE_NAME + "." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " ASC";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Retrieve all top level item return DataTable
		/// 
		/// use subordinate query to get all product that does not have any parent
		/// all product that have product id not in the list of component id in BOM
		/// 
		/// SELECT	DISTINCT BomID, ITM_BOM.ProductID, ComponentID, EffectiveBeginDate, EffectiveEndDate, 
		/// LeadTimeOffset, Quantity, RoutingID, Shrink, Ancestor, EffectiveEndDay, 
		/// EffectiveBeginDay, Alternative, Line, ITM_Product.PlanType, ITM_Product.LotSize, ITM_Product.OrderQuantity, ITM_Product.QuantityMultiple,
		/// ISNULL(ITM_Product.OrderPolicyID, 0) AS ITM_Product.OrderPolicy, ITM_Product.ScrapRate
		/// FROM  ITM_BOM JOIN ITM_Product ON ITM_BOM.ProductID = ITM_Product.ProductID
		/// WHERE ITM_BOM.ProductID NOT IN (SELECT ComponentID FROM ITM_BOM)
		/// AND ITM_ProductID.CCNID = pintCCNID
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable GetTopLevelItems(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetTopLevelItems()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT DISTINCT ITM_Product.ProductID, ITM_Product.Code, ITM_Product.Revision, "
					+ "ITM_Product.Description, ITM_Product.SetupDate, ITM_Product.VAT, ITM_Product.ImportTax, "
					+ "ITM_Product.ExportTax, ITM_Product.SpecialTax, ITM_Product.MakeItem, "
					+ "ITM_Product.PartNumber, ITM_Product.OtherInfo1, ITM_Product.OtherInfo2, "
					+ "TM_Product.Length, ITM_Product.Width, ITM_Product.Height, ITM_Product.Weight, "
					+ "TM_Product.FinishedGoods, ITM_Product.ShelfLife, ITM_Product.LotControl, "
					+ "ITM_Product.QAStatus, ITM_Product.Stock, ITM_Product.PlanType, ITM_Product.AutoConversion, "
					+ "ITM_Product.OrderQuantity, ITM_Product.LTRequisition, ITM_Product.LTSafetyStock, "
					+ "TM_Product.OrderQuantityMultiple, ITM_Product.ScrapPercent, ITM_Product.MinimumStock, "
					+ "ITM_Product.MaximumStock, ITM_Product.ConversionTolerance, ITM_Product.VoucherTolerance, "
					+ "ITM_Product.ReceiveTolerance, ITM_Product.IssueSize, ITM_Product.LTFixedTime, "
					+ "ITM_Product.LTVariableTime, ITM_Product.LTOrderPrepare, ITM_Product.LTShippingPrepare, "
					+ "ITM_Product.LTSalesATP, ITM_Product.ShipToleranceID, ITM_Product.BuyerID, "
					+ "ITM_Product.BOMDescription, ITM_Product.BomIncrement, ITM_Product.RoutingDescription, "
					+ "ITM_Product.CreateDateTime, ITM_Product.UpdateDateTime, ITM_Product.CostMethod, "
					+ "ITM_Product.RoutingIncrement, ITM_Product.CCNID, ITM_Product.CategoryID, "
					+ "ITM_Product.CostCenterID, ITM_Product.DeleteReasonID, ITM_Product.DeliveryPolicyID, "
					+ "ITM_Product.FormatCodeID, ITM_Product.FreightClassID, ITM_Product.HazardID, "
					+ "ITM_Product.OrderPolicyID, ITM_Product.OrderRuleID, ITM_Product.SourceID, "
					+ "ITM_Product.StockUMID, ITM_Product.SellingUMID, ITM_Product.HeightUMID, "
					+ "ITM_Product.WidthUMID, ITM_Product.LengthUMID, ITM_Product.BuyingUMID, "
					+ "ITM_Product.WeightUMID, ITM_Product.LotSize, ITM_Product.MasterLocationID, "
					+ "ITM_Product.LocationID, ITM_Product.BinID, ITM_Product.PrimaryVendorID, "
					+ "ITM_Product.VendorLocationID, ITM_Product.OrderPoint, ITM_Product.SafetyStock, "
					+ "ITM_Product.AGCID, ITM_Product.ParentProductID, ITM_Product.LTDockToStock, "
					+ "ITM_Product.PartNameVN, ITM_Product.LicenseFee, ITM_Product.InventorID, "
					+ "ITM_Product.ProductTypeID, ITM_Product.TaxCode FROM ITM_Product"
					+ " WHERE ITM_Product.ProductID"
					+ " NOT IN (SELECT ITM_BOM.ComponentID FROM ITM_BOM)"
					+ " AND ITM_Product.CCNID = " + pintCCNID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}

		/// <summary>
		/// Retrieve all CPO of parent of current product
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintParentID">Parent Item</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <returns>DataTable</returns>
		public DataTable RetrieveOutOfRageCPOs(int pintCCNID, int pintMasterLocationID, int pintParentID, DateTime pdtmAsOfDate)
		{
			const string METHOD_NAME = THIS + ".RetrieveOutOfRageCPOs()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT	"
					+ MTR_CPOTable.CCNID_FLD + ", "
					+ MTR_CPOTable.CONVERTED_FLD + ", "
					+ MTR_CPOTable.CPOID_FLD + ", "
					+ MTR_CPOTable.DUEDATE_FLD + ", "
					+ MTR_CPOTable.ISMPS_FLD + ", "
					+ MTR_CPOTable.MASTERLOCATIONID_FLD + ", "
					+ MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ", "
					+ MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ", "
					+ MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ", "
					+ MTR_CPOTable.POGENERATEDID_FLD + ", "
					+ MTR_CPOTable.PRODUCTID_FLD + ", "
					+ MTR_CPOTable.QUANTITY_FLD + ", "
					+ MTR_CPOTable.REFDETAILID_FLD + ", "
					+ MTR_CPOTable.REFMASTERID_FLD + ", "
					+ MTR_CPOTable.REFTYPE_FLD + ", "
					+ MTR_CPOTable.STARTDATE_FLD + ", "
					+ MTR_CPOTable.STOCKUMID_FLD + ", "
					+ MTR_CPOTable.WOGENERATEDID_FLD
					+ " FROM " + MTR_CPOTable.TABLE_NAME
					+ " WHERE " + MTR_CPOTable.PRODUCTID_FLD + "=" + pintParentID
					+ " AND " + MTR_CPOTable.STARTDATE_FLD + " < ?"
					+ " AND " + MTR_CPOTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + MTR_CPOTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, MTR_CPOTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}

		/// <summary>
		/// Retrieve all CPO of parent of current product
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <param name="pintProductID">Product</param>
		/// <param name="pdtmAsOfDate">As Of Date</param>
		/// <param name="pdtmDueDate">Due Date</param>
		/// <param name="pintCycleOptionMasterID">MPS Cycle Option Master</param>
		/// <returns>Object</returns>
		public object RetrieveParentCPOs(int pintCCNID, int pintMasterLocationID, int pintProductID, DateTime pdtmAsOfDate, DateTime pdtmDueDate, int pintCycleOptionMasterID)
		{
			const string METHOD_NAME = THIS + ".GetTopLevelItems()";
			
			OleDbDataReader odrPCS = null;
			OleDbCommand ocmdPCS = null;
			OleDbConnection oconPCS = null;
			try 
			{
				string strSql = "SELECT	"
					+ MTR_CPOTable.CCNID_FLD + ", "
					+ MTR_CPOTable.CONVERTED_FLD + ", "
					+ MTR_CPOTable.CPOID_FLD + ", "
					+ MTR_CPOTable.DUEDATE_FLD + ", "
					+ MTR_CPOTable.ISMPS_FLD + ", "
					+ MTR_CPOTable.MASTERLOCATIONID_FLD + ", "
					+ MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ", "
					+ MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ", "
					+ MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ", "
					+ MTR_CPOTable.POGENERATEDID_FLD + ", "
					+ MTR_CPOTable.PRODUCTID_FLD + ", "
					+ MTR_CPOTable.QUANTITY_FLD + ", "
					+ MTR_CPOTable.REFDETAILID_FLD + ", "
					+ MTR_CPOTable.REFMASTERID_FLD + ", "
					+ MTR_CPOTable.REFTYPE_FLD + ", "
					+ MTR_CPOTable.STARTDATE_FLD + ", "
					+ MTR_CPOTable.STOCKUMID_FLD + ", "
					+ MTR_CPOTable.WOGENERATEDID_FLD
					+ " FROM " + MTR_CPOTable.TABLE_NAME
					+ " WHERE " + MTR_CPOTable.PRODUCTID_FLD + "=" + pintProductID
					+ " AND " + MTR_CPOTable.STARTDATE_FLD + " >= ?"
					+ " AND " + MTR_CPOTable.STARTDATE_FLD + " <= ?"
					+ " AND " + MTR_CPOTable.CCNID_FLD + "=" + pintCCNID
					+ " AND " + MTR_CPOTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID
					+ " AND " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = pdtmAsOfDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = pdtmDueDate;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				
				odrPCS = ocmdPCS.ExecuteReader();
				MTR_CPOVO objObject = new MTR_CPOVO();

				while (odrPCS.Read())
				{ 
					objObject.CPOID = int.Parse(odrPCS[MTR_CPOTable.CPOID_FLD].ToString().Trim());
					objObject.Quantity = Decimal.Parse(odrPCS[MTR_CPOTable.QUANTITY_FLD].ToString().Trim());
					objObject.StartDate = DateTime.Parse(odrPCS[MTR_CPOTable.STARTDATE_FLD].ToString().Trim());
					objObject.DueDate = DateTime.Parse(odrPCS[MTR_CPOTable.DUEDATE_FLD].ToString().Trim());
					if (odrPCS[MTR_CPOTable.REFMASTERID_FLD] != DBNull.Value)
						objObject.RefMasterID = int.Parse(odrPCS[MTR_CPOTable.REFMASTERID_FLD].ToString().Trim());
					if (odrPCS[MTR_CPOTable.REFDETAILID_FLD] != DBNull.Value)
						objObject.RefDetailID = int.Parse(odrPCS[MTR_CPOTable.REFDETAILID_FLD].ToString().Trim());
					if (odrPCS[MTR_CPOTable.REFTYPE_FLD] != DBNull.Value)
						objObject.RefType = int.Parse(odrPCS[MTR_CPOTable.REFTYPE_FLD].ToString().Trim());
					if (odrPCS[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD] != DBNull.Value)
						objObject.NetAvailableQuantity = Decimal.Parse(odrPCS[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].ToString().Trim());
					objObject.CCNID = int.Parse(odrPCS[MTR_CPOTable.CCNID_FLD].ToString().Trim());
					objObject.ProductID = int.Parse(odrPCS[MTR_CPOTable.PRODUCTID_FLD].ToString().Trim());
					objObject.MasterLocationID = int.Parse(odrPCS[MTR_CPOTable.MASTERLOCATIONID_FLD].ToString().Trim());
					objObject.StockUMID = int.Parse(odrPCS[MTR_CPOTable.STOCKUMID_FLD].ToString().Trim());
					objObject.IsMPS = bool.Parse(odrPCS[MTR_CPOTable.ISMPS_FLD].ToString().Trim());
					
					if(!odrPCS[MTR_CPOTable.POGENERATEDID_FLD].Equals(DBNull.Value))
					{
						objObject.POGeneratedID = int.Parse(odrPCS[MTR_CPOTable.POGENERATEDID_FLD].ToString().Trim());
					}
					else
					{
						objObject.POGeneratedID = 0;
					}
					
					if(!odrPCS[MTR_CPOTable.WOGENERATEDID_FLD].Equals(DBNull.Value))
					{
						objObject.WOGeneratedID = int.Parse(odrPCS[MTR_CPOTable.WOGENERATEDID_FLD].ToString().Trim());
					}
					else
					{
						objObject.WOGeneratedID = 0;
					}

					if(!odrPCS[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
					{
						objObject.MRPCycleOptionMasterID = int.Parse(odrPCS[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					}
					else
					{
						objObject.MRPCycleOptionMasterID = 0;
					}

					if(!odrPCS[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
					{
						objObject.MPSCycleOptionMasterID = int.Parse(odrPCS[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
					}
					else
					{
						objObject.MPSCycleOptionMasterID = 0;
					}

					objObject.Converted = bool.Parse(odrPCS[MTR_CPOTable.CONVERTED_FLD].ToString().Trim());

				}
				return objObject;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// This method uses to get all product of CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
		public DataTable GetAllProducts(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".GetAllProduct()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT ITM_Product.ProductID, ITM_Product.Code, ITM_Product.Revision, "
					+ "ITM_Product.Description, ITM_Product.SetupDate, ITM_Product.VAT, ITM_Product.ImportTax, "
					+ "ITM_Product.ExportTax, ITM_Product.SpecialTax, ITM_Product.MakeItem, "
					+ "ITM_Product.PartNumber, ITM_Product.OtherInfo1, ITM_Product.OtherInfo2, "
					+ "ITM_Product.Length, ITM_Product.Width, ITM_Product.Height, ITM_Product.Weight, "
					+ "ITM_Product.FinishedGoods, ITM_Product.ShelfLife, ITM_Product.LotControl, "
					+ "ITM_Product.QAStatus, ITM_Product.Stock, ITM_Product.PlanType, ITM_Product.AutoConversion, "
					+ "ITM_Product.OrderQuantity, ITM_Product.LTRequisition, ITM_Product.LTSafetyStock, "
					+ "ITM_Product.OrderQuantityMultiple, ITM_Product.ScrapPercent, ITM_Product.MinimumStock, "
					+ "ITM_Product.MaximumStock, ITM_Product.ConversionTolerance, ITM_Product.VoucherTolerance, "
					+ "ITM_Product.ReceiveTolerance, ITM_Product.IssueSize, ITM_Product.LTFixedTime, "
					+ "ITM_Product.LTVariableTime, ITM_Product.LTOrderPrepare, ITM_Product.LTShippingPrepare, "
					+ "ITM_Product.LTSalesATP, ITM_Product.ShipToleranceID, ITM_Product.BuyerID, "
					+ "ITM_Product.BOMDescription, ITM_Product.BomIncrement, ITM_Product.RoutingDescription, "
					+ "ITM_Product.CreateDateTime, ITM_Product.UpdateDateTime, ITM_Product.CostMethod, "
					+ "ITM_Product.RoutingIncrement, ITM_Product.CCNID, ITM_Product.CategoryID, "
					+ "ITM_Product.CostCenterID, ITM_Product.DeleteReasonID, ITM_Product.DeliveryPolicyID, "
					+ "ITM_Product.FormatCodeID, ITM_Product.FreightClassID, ITM_Product.HazardID, "
					+ "ITM_Product.OrderPolicyID, ITM_Product.OrderRuleID, ITM_Product.SourceID, "
					+ "ITM_Product.StockUMID, ITM_Product.SellingUMID, ITM_Product.HeightUMID, "
					+ "ITM_Product.WidthUMID, ITM_Product.LengthUMID, ITM_Product.BuyingUMID, "
					+ "ITM_Product.WeightUMID, ITM_Product.LotSize, ITM_Product.MasterLocationID, "
					+ "ITM_Product.LocationID, ITM_Product.BinID, ITM_Product.PrimaryVendorID, "
					+ "ITM_Product.VendorLocationID, ITM_Product.OrderPoint, ITM_Product.SafetyStock, "
					+ "ITM_Product.AGCID, ITM_Product.ParentProductID, ITM_Product.LTDockToStock, "
					+ "ITM_Product.PartNameVN, ITM_Product.LicenseFee, ITM_Product.InventorID, "
					+ "ITM_Product.ProductTypeID, ITM_Product.TaxCode, ITM_BOM.ProductID AS ParentID, "
					+ "ITM_BOM.Quantity AS RequiredQuantity, ITM_BOM.LeadTimeOffset, ITM_BOM.Shrink FROM ITM_Product "
					+ "LEFT JOIN ITM_BOM ON ITM_Product.ProductID = ITM_BOM.ComponentID "
					+ "WHERE ITM_Product.CCNID = " + pintCCNID
					+ " ORDER BY ITM_BOM.ProductID, ITM_Product.ProductID";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
		}
		/// <summary>
		/// Get all sale orders in the period of time
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Sale Orders</returns>
		public DataTable GetTotalSO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetTotalSO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	ISNULL(SUM(ISNULL(SO_DeliverySchedule.DeliveryQuantity, 0)), 0) AS 'DeliveryQuantity', SO_SaleOrderDetail.ProductID,"
					+ " SO_SaleOrderMaster.ShipFromLocID,"
					+ " SO_DeliverySchedule.ScheduleDate AS DueDate,"
					+ " MST_WorkCenter.WorkCenterID"
					+ " FROM	SO_DeliverySchedule"
					+ " JOIN SO_SaleOrderDetail"
					+ " ON SO_DeliverySchedule.SaleOrderDetailID = SO_SaleOrderDetail.SaleOrderDetailID"
					+ " JOIN SO_SaleOrderMaster"
					+ " ON SO_SaleOrderDetail.SaleOrderMasterID = SO_SaleOrderMaster.SaleOrderMasterID"
					+ " JOIN ITM_Routing"
					+ " ON SO_SaleOrderDetail.ProductID = ITM_Routing.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1"
					+ " WHERE	SO_DeliverySchedule.ScheduleDate >= ?"
					+ " AND SO_DeliverySchedule.ScheduleDate <= ?"
					+ " AND SO_SaleOrderMaster.CCNID = " + pintCCNID
					+ " GROUP BY SO_SaleOrderMaster.ShipFromLocID, MST_WorkCenter.WorkCenterID, SO_SaleOrderDetail.ProductID, SO_DeliverySchedule.ScheduleDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
				ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
				ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
		/// <summary>
		/// Get all purchase orders in the period of time
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Purchase Orders</returns>
		public DataTable GetTotalPO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetTotalPO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	ISNULL(SUM(ISNULL(PO_DeliverySchedule.DeliveryQuantity, 0)), 0) AS 'DeliveryQuantity', PO_PurchaseOrderDetail.ProductID,"
					+ " PO_PurchaseOrderMaster.MasterLocationID,"
					+ " PO_DeliverySchedule.ScheduleDate AS DueDate,"
					+ " MST_WorkCenter.WorkCenterID"
					+ " FROM	PO_DeliverySchedule"
					+ " JOIN PO_PurchaseOrderDetail"
					+ " ON PO_DeliverySchedule.PurchaseOrderDetailID = PO_PurchaseOrderDetail.PurchaseOrderDetailID"
					+ " JOIN PO_PurchaseOrderMaster"
					+ " ON PO_PurchaseOrderDetail.PurchaseOrderMasterID = PO_PurchaseOrderMaster.PurchaseOrderMasterID"
					+ " JOIN ITM_Routing"
					+ " ON PO_PurchaseOrderDetail.ProductID = ITM_Routing.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1"
					+ " WHERE	PO_DeliverySchedule.ScheduleDate >= ?"
					+ " AND PO_DeliverySchedule.ScheduleDate <= ?"
					+ " AND ISNULL(PO_PurchaseOrderDetail.ApproverID, 0) > 0"
					+ " AND ISNULL(PO_PurchaseOrderDetail.Closed, 0) = 0"
					+ " AND PO_PurchaseOrderMaster.CCNID = " + pintCCNID
					+ " GROUP BY PO_PurchaseOrderMaster.MasterLocationID, MST_WorkCenter.WorkCenterID, PO_PurchaseOrderDetail.ProductID, PO_DeliverySchedule.ScheduleDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
				ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
				ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

		/// <summary>
		/// Get all work orders in the period of time
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Work Orders</returns>
		public DataTable GetTotalWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetTotalWO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = "SELECT	ISNULL(SUM(ISNULL(PRO_WorkOrderDetail.OrderQuantity, 0)), 0) AS 'OrderQuantity', PRO_WorkOrderDetail.ProductID, "
					+ " PRO_WorkOrderMaster.MasterLocationID,"
					+ " PRO_WorkOrderDetail.DueDate,"
					+ " MST_WorkCenter.WorkCenterID"
					+ " FROM	PRO_WorkOrderDetail"
					+ " JOIN PRO_WorkOrderMaster"
					+ " ON PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID"
					+ " JOIN ITM_Routing"
					+ " ON PRO_WorkOrderDetail.ProductID = ITM_Routing.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1"
					+ " WHERE	PRO_WorkOrderDetail.DueDate >= ?"
					+ " AND PRO_WorkOrderDetail.DueDate <= ?"
					+ " AND PRO_WorkOrderDetail.Status = " + (int)WOLineStatus.Released
					+ " AND PRO_WorkOrderMaster.CCNID = " + pintCCNID
					+ " GROUP BY PRO_WorkOrderMaster.MasterLocationID, MST_WorkCenter.WorkCenterID, PRO_WorkOrderDetail.ProductID, PRO_WorkOrderDetail.DueDate";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
				ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
				ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}

	
		/// <summary>
		/// Get all work orders in the period of time
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Demand Work Orders</returns>
		public DataTable GetDemandWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			const string METHOD_NAME = THIS + ".GetDemandWO()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				DataTable dtbData = new DataTable();
				Utils objUtils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				string strSql = 
					"SELECT	ISNULL(SUM(ISNULL(PRO_WorkOrderDetail.OrderQuantity, 0) * ISNULL(ITM_BOM.Quantity,0) / (1 - ISNULL(ITM_BOM.Shrink,0) * 0.01) ), 0) AS 'OrderQuantity', ITM_BOM.ComponentID AS ProductID, "
					+ " PRO_WorkOrderMaster.MasterLocationID, "
					+ " DateAdd(ss,-ISNULL(ITM_BOM.LeadTimeOffset,0),PRO_WorkOrderDetail.DueDate) AS DueDate,"
					+ " MST_WorkCenter.WorkCenterID"
					+ " FROM	PRO_WorkOrderDetail"
					+ " JOIN PRO_WorkOrderMaster"
					+ " ON PRO_WorkOrderDetail.WorkOrderMasterID = PRO_WorkOrderMaster.WorkOrderMasterID"
					+ " JOIN ITM_BOM ON ITM_BOM.ProductID = PRO_WorkOrderDetail.ProductID"
					+ " JOIN ITM_Routing"
					+ " ON ITM_BOM.ComponentID = ITM_Routing.ProductID"
					+ " JOIN MST_WorkCenter"
					+ " ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID AND MST_WorkCenter.IsMain = 1"
					+ " WHERE	DateAdd(ss,-ISNULL(ITM_BOM.LeadTimeOffset,0),PRO_WorkOrderDetail.StartDate) >= ?"
					+ " AND DateAdd(ss,-ISNULL(ITM_BOM.LeadTimeOffset,0),PRO_WorkOrderDetail.StartDate) <= ?"
					+ " AND PRO_WorkOrderDetail.Status = " + (int)WOLineStatus.Released
					+ " AND PRO_WorkOrderMaster.CCNID = " + pintCCNID
					+ " GROUP BY PRO_WorkOrderMaster.MasterLocationID, MST_WorkCenter.WorkCenterID, ITM_BOM.ComponentID, PRO_WorkOrderDetail.DueDate, ITM_BOM.LeadTimeOffset";

				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date));
				ocmdPCS.Parameters["FromDate"].Value = pdtmFromDate;
				ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date));
				ocmdPCS.Parameters["ToDate"].Value = pdtmToDate;

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadData = new OleDbDataAdapter(ocmdPCS);
				odadData.Fill(dtbData);
				return dtbData;
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
					if (oconPCS.State != ConnectionState.Closed)
						oconPCS.Close();
			}
		}
	}
}

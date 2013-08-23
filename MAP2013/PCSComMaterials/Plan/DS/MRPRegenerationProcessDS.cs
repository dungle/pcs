using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Text;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;



namespace PCSComMaterials.Plan.DS
{
    /// <summary>
    /// Summary description for MRPRegenerationProcessDS.
    /// </summary>
    public class MRPRegenerationProcessDS 
    {
        private const string THIS = "PCSComMaterials.Plan.DS.MRPRegenerationProcessDS";
        private const string V_WO_MI_PLANNING = "v_WO_MaterialIssue_Planning";
        private const string V_WO_BOM_PLANNING = "v_WO_BOM_Planning";
        private const string V_ITEM_BOM_PLANNING = "v_itm_Bom_Planning";
        private const string REQUEST_WO_QTITY_FLD = "requestWOQtity";
        private const string REPLENISH_PO_QTITY_FLD = "ReplenishItemQtity";

        public MRPRegenerationProcessDS()
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
        /// 
        /// </summary>
        /// <param name="pintItemID"></param>
        /// <param name="pdtLimitCyclePlanDate"></param>
        /// <param name="pdtAsOfDate"></param>
        /// <param name="pintMasLocID"></param>
        /// <returns></returns>
        public DataTable getAllReleaseSOForItem(int pintItemID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseSOForItem()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "SM." + SO_SaleOrderMasterTable.CODE_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
                    + "D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " AS " + MTR_CPOTable.DUEDATE_FLD + ","
                    + "(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - ISNULL(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ", 0)) AS " + REQUEST_WO_QTITY_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
                    + "D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
                    + " FROM " + SO_DeliveryScheduleTable.TABLE_NAME + " D LEFT JOIN " + SO_CommitInventoryDetailTable.TABLE_NAME + " C "
                    + " ON D." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + " = C." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
                    + " JOIN " + SO_SaleOrderDetailTable.TABLE_NAME + " SD ON D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + " = SD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
                    + " JOIN " + SO_SaleOrderMasterTable.TABLE_NAME + " SM ON SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = SD." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
                    + " WHERE D." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - ISNULL(" + "C." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ", 0) > 0"
                    + " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<=?"
                    + " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">=?"
                    + " AND SD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + "=" + pintItemID
                    + " AND SM." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + "=" + pintMasLocID
                    + " GROUP BY SM." + SO_SaleOrderMasterTable.CODE_FLD + ", SM." + SO_SaleOrderMasterTable.TRANSDATE_FLD
                    + ", SD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", SD." + SO_SaleOrderDetailTable.UNITPRICE_FLD
                    + ", D." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
                    + ", D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", C." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD
                    + ", D." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", " + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD
                    + ", " + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", SD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD
                    + ", SD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
                    + " ORDER BY " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD;


                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pintItemID"></param>
        /// <param name="pdtLimitCyclePlanDate"></param>
        /// <param name="pdtAsOfDate"></param>
        /// <param name="pintMasLocID"></param>
        /// <returns></returns>
        public DataTable getAllReleasePOForItem(int pintItemID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseSOForItem()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
                    + "D." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + "- ISNULL(" + PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ", 0) as " + REPLENISH_PO_QTITY_FLD + ""
                    + " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME + " PD INNER JOIN " + PO_DeliveryScheduleTable.TABLE_NAME + " D "
                    + " ON D." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + " = PD." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
                    + " INNER JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME + " PM ON PM." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "= PD." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
                    + " WHERE ISNULL(PD." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ", 0) <= PD." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD
                    + " AND D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<=?"
                    + " AND D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">=?"
                    + " AND PD." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + pintItemID
                    + " AND PD." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " is not Null"
                    + " AND PD." + PO_PurchaseOrderDetailTable.CLOSED_FLD + " <> " + 1.ToString()
                    + " AND PM." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " ORDER BY " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pintItemID"></param>
        /// <param name="pdtLimitCyclePlanDate"></param>
        /// <param name="pdtAsOfDate"></param>
        /// <param name="pintMasLocID"></param>
        /// <returns></returns>
        public DataTable getAllReleaseWOForItem(int pintItemID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseWOForItem()";
            const string ISSUE_QTITY_FLD = "IssueQtity";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " SELECT "
                    + "A." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + "*" + "A." + PRO_WorkOrderBomDetailTable.REQUIREDQUANTITY_FLD + "-" + "IsNull(B." + ISSUE_QTITY_FLD + ", 0) As " + REQUEST_WO_QTITY_FLD + ","
                    + "A." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
                    + "A." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
                    + "A." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
                    + "A." + PRO_WorkOrderBomDetailTable.SHRINK_FLD + ","
                    + "A." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD
                    + " FROM " + V_WO_BOM_PLANNING + " A Left Join " + V_WO_MI_PLANNING + " B "
                    + " ON A." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = B." + PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD
                    + " WHERE " + "A." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + "=" + pintItemID
                    + " AND " + "A." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ">=?"
                    + " AND " + "A." + PRO_WorkOrderDetailTable.DUEDATE_FLD + "<=?"
                    + " AND " + "A." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " ORDER BY " + PRO_WorkOrderDetailTable.DUEDATE_FLD;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pintItemID"></param>
        /// <param name="pdtLimitCyclePlanDate"></param>
        /// <param name="pdtAsOfDate"></param>
        /// <returns></returns>
        public DataTable getAllCPOForItem(int pintItemID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID, int pintMPSID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseWOForItem()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "A." + ITM_BOMTable.QUANTITY_FLD + "*" + "B." + ITM_BOMTable.QUANTITY_FLD + " As " + REQUEST_WO_QTITY_FLD + ","
                    + "A." + MTR_CPOTable.STARTDATE_FLD + ","
                    + "A." + MTR_CPOTable.DUEDATE_FLD + ","
                    + "A." + MTR_CPOTable.STOCKUMID_FLD + ","
                    + "B." + ITM_BOMTable.SHRINK_FLD + ","
                    + "B." + ITM_BOMTable.COMPONENTID_FLD
                    + " FROM " + MTR_CPOTable.TABLE_NAME + " A, " + V_ITEM_BOM_PLANNING + " B "
                    + " WHERE " + "A." + MTR_CPOTable.PRODUCTID_FLD + " =B." + ITM_ProductTable.PRODUCTID_FLD
                    + " AND " + "A." + MTR_CPOTable.STARTDATE_FLD + ">=?"
                    + " AND " + "A." + MTR_CPOTable.DUEDATE_FLD + "<=?"
                    + " AND " + "B." + ITM_BOMTable.COMPONENTID_FLD + "=" + pintItemID
                    + " AND " + "A." + MTR_CPOTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " AND " + "A." + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintMPSID
                    + " ORDER BY " + MTR_CPOTable.DUEDATE_FLD;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                //				ocmdPCS.Parameters.Add(ITM_BOMTable.EFFECTIVEBEGINDATE_FLD, OleDbType.Date);
                //				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].Value = pdtAsOfDate;
                //
                //				ocmdPCS.Parameters.Add(ITM_BOMTable.EFFECTIVEENDDATE_FLD, OleDbType.Date);
                //				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEENDDATE_FLD].Value = pdtLimitCyclePlanDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Get all of PO released for all mrp items
        /// </summary>
        /// <param name="pstrItemsID">List all mrp items</param>
        /// <param name="pdtLimitCyclePlanDate">The end date of cycle</param>
        /// <param name="pdtAsOfDate">As Of Date value</param>
        /// <param name="pintMasLocID">MasterlocationID</param>
        /// <returns></returns>
        /// <authour>TuanDM</authour>
        public DataTable getAllReleasePOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID, int pintCCNID)
        {
            const string METHOD_NAME = THIS + ".getAllReleasePOForItems()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "PD." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
                    + "D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
                    + "D." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + "- ISNULL(" + PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD + ", 0) as " + REPLENISH_PO_QTITY_FLD + ""
                    + " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME + " PD INNER JOIN " + PO_DeliveryScheduleTable.TABLE_NAME + " D "
                    + " ON D." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + " = PD." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
                    + " INNER JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME + " PM ON PM." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "= PD." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
                    // 16-11-2006 dungla: exclude local PO
                    + " JOIN MST_Party ON PM.MakerID = MST_Party.PartyID"
                    + " WHERE ISNULL(PD." + PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD + ", 0) <= PD." + PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD
                    + " AND D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<?"
                    + " AND D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">=?"
                    + " AND PD." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + " in " + pstrItemsID
                    + " AND PD." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " is not Null"
                    //+ " AND isnull(PD."+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ",0) <> " + 1.ToString()
                    + " AND isnull(D." + PO_DeliveryScheduleTable.CANCELDELIVERY_FLD + ",0) <> " + 1.ToString()
                    + " AND PM." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " AND MST_Party.CountryID NOT IN (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pintCCNID + ")"
                    + " ORDER BY " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Get all of PO released for all mrp items
        /// </summary>
        /// <param name="pstrItemsID">List all mrp items</param>
        /// <param name="pdtLimitCyclePlanDate">The end date of cycle</param>
        /// <param name="pdtAsOfDate">As Of Date value</param>
        /// <param name="pintMasLocID">MasterlocationID</param>
        /// <returns></returns>
        /// <authour>TuanDM</authour>
        public DataTable getAllReleasePOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleasePOForItems()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "PD." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + ","
                    + "D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ","
                    + "D." + PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " as " + REPLENISH_PO_QTITY_FLD + ""
                    + " FROM " + PO_PurchaseOrderDetailTable.TABLE_NAME + " PD INNER JOIN " + PO_DeliveryScheduleTable.TABLE_NAME + " D "
                    + " ON D." + PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD + " = PD." + PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
                    + " INNER JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME + " PM ON PM." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD + "= PD." + PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD
                    + " WHERE D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<?"
                    + " AND D." + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">=?"
                    + " AND PD." + PO_PurchaseOrderDetailTable.PRODUCTID_FLD + " in " + pstrItemsID
                    + " AND PD." + PO_PurchaseOrderDetailTable.APPROVERID_FLD + " is not Null"
                    //+ " AND isnull(PD."+ PO_PurchaseOrderDetailTable.CLOSED_FLD + ",0) <> " + 1.ToString()
                    + " AND isnull(D." + PO_DeliveryScheduleTable.CANCELDELIVERY_FLD + ",0) <> " + 1.ToString()
                    + " AND PM." + PO_PurchaseOrderMasterTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " ORDER BY " + PO_DeliveryScheduleTable.SCHEDULEDATE_FLD;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Get all CPO of MPS cycle for all mrp items
        /// </summary>
        /// <param name="pstrItemsID">List all mrp items</param>
        /// <param name="pdtLimitCyclePlanDate">The end date of cycle</param>
        /// <param name="pdtAsOfDate">As Of Date value</param>
        /// <param name="pintMasLocID">MasterlocationID</param>
        /// <param name="pintMPSID">MPSCycleOptionMasterID</param>
        /// <returns></returns>
        /// <authour>TuanDM</authour>
        public DataTable getAllCPOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID, int pintMPSID)
        {
            const string METHOD_NAME = THIS + ".getAllCPOForItems()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "A." + ITM_BOMTable.QUANTITY_FLD + "*" + "B." + ITM_BOMTable.QUANTITY_FLD + "*100/ (100 - Isnull(" + ITM_BOMTable.SHRINK_FLD + ",0))" + " As " + REQUEST_WO_QTITY_FLD + ","
                    + "A." + MTR_CPOTable.STARTDATE_FLD + ","
                    + "A." + MTR_CPOTable.DUEDATE_FLD + ","
                    + "A." + MTR_CPOTable.STOCKUMID_FLD + ","
                    + "B." + ITM_BOMTable.SHRINK_FLD + ","
                    + "Isnull(B." + ITM_BOMTable.LEADTIMEOFFSET_FLD + ", 0) as LeadTimeOffSet,"
                    + "B." + ITM_BOMTable.COMPONENTID_FLD + " as " + ITM_ProductTable.PRODUCTID_FLD
                    + " FROM " + MTR_CPOTable.TABLE_NAME + " A, " + V_ITEM_BOM_PLANNING + " B "
                    + " WHERE " + "A." + MTR_CPOTable.PRODUCTID_FLD + " =B." + ITM_ProductTable.PRODUCTID_FLD
                    + " AND " + "A." + MTR_CPOTable.STARTDATE_FLD + ">=?"
                    + " AND " + "A." + MTR_CPOTable.DUEDATE_FLD + "<=?"
                    + " AND " + "B." + ITM_BOMTable.COMPONENTID_FLD + " in " + pstrItemsID
                    + " AND " + "A." + MTR_CPOTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " AND " + "A." + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintMPSID + " and " + MTR_CPOTable.ISMPS_FLD + "=" + 1.ToString()
                    + " ORDER BY B.ProductID," + MTR_CPOTable.DUEDATE_FLD;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                //				ocmdPCS.Parameters.Add(ITM_BOMTable.EFFECTIVEBEGINDATE_FLD, OleDbType.Date);
                //				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEBEGINDATE_FLD].Value = pdtAsOfDate;
                //
                //				ocmdPCS.Parameters.Add(ITM_BOMTable.EFFECTIVEENDDATE_FLD, OleDbType.Date);
                //				ocmdPCS.Parameters[ITM_BOMTable.EFFECTIVEENDDATE_FLD].Value = pdtLimitCyclePlanDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Get all of Released WO for all mrp items
        /// </summary>
        /// <param name="pstrItemsID">List all mrp items</param>
        /// <param name="pdtLimitCyclePlanDate">The end date of cycle</param>
        /// <param name="pdtAsOfDate">As Of Date value</param>
        /// <param name="pintMasLocID">MasterlocationID</param>
        /// <returns></returns>
        /// <authour>TuanDM</authour>
        public DataTable getAllReleaseWOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseWOForItem()";
            const string ISSUE_QTITY_FLD = "IssueQtity";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " SELECT "
                    + "A." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + "*" + "A." + PRO_WorkOrderBomDetailTable.REQUIREDQUANTITY_FLD + "-" + "IsNull(B." + ISSUE_QTITY_FLD + ", 0) As " + REQUEST_WO_QTITY_FLD + ","
                    + "A." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
                    + "A." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
                    + "A." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
                    + "A." + PRO_WorkOrderBomDetailTable.SHRINK_FLD + ","
                    + "Isnull(A." + PRO_WorkOrderBomDetailTable.LEADTIMEOFFSET_FLD + ",0) as LeadTimeOffSet,"
                    + "A." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + " as " + ITM_ProductTable.PRODUCTID_FLD
                    + " FROM " + V_WO_BOM_PLANNING + " A Left Join " + V_WO_MI_PLANNING + " B "
                    + " ON A." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = B." + PRO_IssueMaterialDetailTable.WORKORDERDETAILID_FLD
                    + " WHERE " + "A." + PRO_WorkOrderBomDetailTable.COMPONENTID_FLD + " in " + pstrItemsID
                    + " AND " + "A." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ">=?"
                    + " AND " + "A." + PRO_WorkOrderDetailTable.STARTDATE_FLD + "<=?"
                    + " AND " + "A." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + "=" + pintMasLocID
                    + " ORDER BY " + PRO_WorkOrderDetailTable.STARTDATE_FLD;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public DataTable getAllCPOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseCPOForItems()";
            const string ISSUE_QTITY_FLD = "IssueQtity";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " SELECT (PRO_DCPResultDetail.Quantity * ITM_BOM.Quantity) requestWOQtity,"
                    + " PRO_DCPResultDetail.StartTime StartDate, PRO_DCPResultDetail.EndTime DueDate, "
                    + " ITM_Product.StockUMID, ISNULL(ITM_BOM.Shrink,0) AS Shrink,"
                    + " ISNULL(ITM_BOM.LeadTimeOffSet,0) AS LeadTimeOffSet, ITM_BOM.ComponentID ProductID"
                    + " FROM PRO_DCPResultDetail JOIN PRO_DCPResultMaster ON PRO_DCPResultDetail.DCPResultMasterID ="
                    + " PRO_DCPResultMaster.DCPResultMasterID"
                    + " JOIN ITM_BOM ON PRO_DCPResultMaster.ProductID = ITM_BOM.ProductID"
                    + " JOIN ITM_Product ON ITM_BOM.ComponentID = ITM_Product.ProductID"
                    + " AND ITM_Product.ProductID IN " + pstrItemsID
                    + " AND StartTime >= ?"
                    + " AND StartTime < ?"
                    + " ORDER BY ITM_BOM.ComponentID, StartTime";
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Get all Released SO for all MRPItems 
        /// </summary>
        /// <param name="pstrItemsID">List all mrp items</param>
        /// <param name="pdtLimitCyclePlanDate">The end date of cycle</param>
        /// <param name="pdtAsOfDate">As Of Date value</param>
        /// <param name="pintMasLocID">MasterlocationID</param>
        /// <returns></returns>
        /// <authour>TuanDM</authour>
        public DataTable getAllReleaseSOForItemsInCycle(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseSOForItem()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "SD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.CODE_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
                    + "D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " AS " + MTR_CPOTable.DUEDATE_FLD + ","
                    + "(" + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - ISNULL(" + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ", 0)) AS " + REQUEST_WO_QTITY_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
                    + "D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
                    + " FROM " + SO_DeliveryScheduleTable.TABLE_NAME + " D LEFT JOIN " + SO_CommitInventoryDetailTable.TABLE_NAME + " C "
                    + " ON D." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + " = C." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
                    + " JOIN " + SO_SaleOrderDetailTable.TABLE_NAME + " SD ON D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + " = SD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
                    + " JOIN " + SO_SaleOrderMasterTable.TABLE_NAME + " SM ON SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = SD." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
                    + " WHERE D." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " - ISNULL(" + "C." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD + ", 0) > 0"
                    + " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<?"
                    + " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">=?"
                    + " AND SD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + " in " + pstrItemsID
                    + " AND SM." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + "=" + pintMasLocID
                    + " GROUP BY SD.ProductID, SM." + SO_SaleOrderMasterTable.CODE_FLD + ", SM." + SO_SaleOrderMasterTable.TRANSDATE_FLD
                    + ", SD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", SD." + SO_SaleOrderDetailTable.UNITPRICE_FLD
                    + ", D." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
                    + ", D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", C." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD
                    + ", D." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", " + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD
                    + ", " + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", SD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD
                    + ", SD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
                    + " ORDER BY " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD;


                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public DataTable getAllReleaseSOForItems(StringBuilder pstrItemsID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintMasLocID)
        {
            const string METHOD_NAME = THIS + ".getAllReleaseSOForItem()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + "SD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.CODE_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.TRANSDATE_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
                    + "D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + " AS " + MTR_CPOTable.DUEDATE_FLD + ","
                    + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + " AS " + REQUEST_WO_QTITY_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.UNITPRICE_FLD + ","
                    + "D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD + ","
                    + "SD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ","
                    + "SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
                    + " FROM " + SO_DeliveryScheduleTable.TABLE_NAME + " D LEFT JOIN " + SO_CommitInventoryDetailTable.TABLE_NAME + " C "
                    + " ON D." + SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD + " = C." + SO_CommitInventoryDetailTable.DELIVERYSCHEDULEID_FLD
                    + " JOIN " + SO_SaleOrderDetailTable.TABLE_NAME + " SD ON D." + SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + " = SD." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
                    + " JOIN " + SO_SaleOrderMasterTable.TABLE_NAME + " SM ON SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = SD." + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD
                    + " WHERE D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "<?"
                    + " AND D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ">=?"
                    + " AND SD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + " in " + pstrItemsID
                    + " AND SM." + SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD + "=" + pintMasLocID
                    + " GROUP BY SD.ProductID, SM." + SO_SaleOrderMasterTable.CODE_FLD + ", SM." + SO_SaleOrderMasterTable.TRANSDATE_FLD
                    + ", SD." + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ", SD." + SO_SaleOrderDetailTable.UNITPRICE_FLD
                    + ", D." + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD
                    + ", D." + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + ", C." + SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD
                    + ", D." + SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD + ", " + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD
                    + ", " + SO_SaleOrderDetailTable.UNITPRICE_FLD + ", SD." + SO_SaleOrderDetailTable.SELLINGUMID_FLD
                    + ", SD." + SO_SaleOrderDetailTable.STOCKUMID_FLD + ", SM." + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD
                    + " ORDER BY SD." + SO_SaleOrderDetailTable.PRODUCTID_FLD + "," + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD;


                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.PLANHORIZON_FLD].Value = pdtmToDate;

                ocmdPCS.Parameters.Add(MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date);
                ocmdPCS.Parameters[MTR_MPSCycleOptionMasterTable.ASOFDATE_FLD].Value = pdtmFromDate;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }


        //**************************************************************************              
        ///    <Description>
        ///       This method uses to check commit quantity
        ///    </Description>
        ///    <Inputs>
        ///       CCNID, MasterLocationID, CommitQuantity, ProductID
        ///    </Inputs>
        ///    <Outputs>
        ///       decimal
        ///    </Outputs>
        ///    <Returns>
        ///       return decimal.MinusOne if select query return a null value
        ///       else return query value
        ///    </Returns>
        ///    <Authors>
        ///       DungLa
        ///    </Authors>
        ///    <History>
        ///       21-Apr-2005
        ///    </History>
        ///    <Notes>
        ///    </Notes>
        //**************************************************************************
        public DataTable GetAvailableQuantity(int pintCCNID, int pintMasterLocationID, StringBuilder pstrItemsID)
        {
            const string METHOD_NAME = THIS + ".GetAvailableQuantity()";

            DataSet dstData = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT ProductID, ISNULL("
                    + IV_MasLocCacheTable.OHQUANTITY_FLD + ", 0) - ISNULL(" + IV_MasLocCacheTable.COMMITQUANTITY_FLD + ", 0) as " + IV_MasLocCacheTable.OHQUANTITY_FLD
                    + " FROM " + IV_MasLocCacheTable.TABLE_NAME
                    + " WHERE " + IV_MasLocCacheTable.PRODUCTID_FLD + " in " + pstrItemsID
                    + " AND " + IV_MasLocCacheTable.CCNID_FLD + "=" + pintCCNID
                    + " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintMasterLocationID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odapPCS = new OleDbDataAdapter();
                odapPCS.SelectCommand = ocmdPCS;

                odapPCS.Fill(dstData, IV_MasLocCacheTable.OHQUANTITY_FLD);
                return dstData.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Get working day in a year
        /// </summary>
        /// <returns></returns>
        /// <authour>TuanDM</authour>
        public DataTable GetAllWorkingDays()
        {
            const string METHOD_NAME = THIS + ".GetAllWorkingDays()";
            DataSet dstPCS = new DataSet();

            ArrayList arrDayOfWeek = new ArrayList();

            OleDbConnection oconPCS = null;
            try
            {
                string strSql = "SELECT "
                    + MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD + ","
                    + MST_WorkingDayMasterTable.SUN_FLD + ","
                    + MST_WorkingDayMasterTable.CCNID_FLD + ","
                    + MST_WorkingDayMasterTable.YEAR_FLD + ","
                    + MST_WorkingDayMasterTable.MON_FLD + ","
                    + MST_WorkingDayMasterTable.TUE_FLD + ","
                    + MST_WorkingDayMasterTable.WED_FLD + ","
                    + MST_WorkingDayMasterTable.THU_FLD + ","
                    + MST_WorkingDayMasterTable.FRI_FLD + ","
                    + MST_WorkingDayMasterTable.SAT_FLD
                    + " FROM " + MST_WorkingDayMasterTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MST_WorkingDayMasterTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }


        /// <summary>
        /// Get all holidays in the schedule
        /// </summary>
        /// <param name="penuSchedule"></param>
        /// <returns></returns>
        public DataTable GetAllHolidays(ScheduleMethodEnum penuSchedule)
        {
            const string METHOD_NAME = THIS + ".GetAllHolidays()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            try
            {
                string strSql = "SELECT "
                    + MST_WorkingDayMasterTable.TABLE_NAME + "." + MST_WorkingDayMasterTable.YEAR_FLD + ","
                    + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.WORKINGDAYDETAILID_FLD + ","
                    + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.OFFDAY_FLD + ","
                    + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.COMMENT_FLD + ","
                    + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD
                    + " FROM " + MST_WorkingDayDetailTable.TABLE_NAME
                    + " INNER JOIN " + MST_WorkingDayMasterTable.TABLE_NAME
                    + " ON " + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD + "=" + MST_WorkingDayMasterTable.TABLE_NAME + "." + MST_WorkingDayMasterTable.WORKINGDAYMASTERID_FLD;
                strSql += " ORDER BY " + MST_WorkingDayDetailTable.TABLE_NAME + "." + MST_WorkingDayDetailTable.OFFDAY_FLD;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MST_WorkingDayDetailTable.TABLE_NAME);

                return dstPCS.Tables[0];
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public DataTable GetPOReceipt(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
        {
            const string METHOD_NAME = THIS + ".GetPOReceipt()";

            OleDbConnection oconPCS = null;
            try
            {
                string strSql = "SELECT SUM(ReceiveQuantity) AS Quantity, M.MasterLocationID, ProductID"
                    + " FROM PO_PurchaseOrderReceiptDetail D JOIN PO_PurchaseOrderReceiptMaster M"
                    + " ON M.PurchaseOrderReceiptID = D.PurchaseOrderReceiptID"
                    + " WHERE M.PostDate >= ?"
                    + " AND M.PostDate < ?"
                    + " GROUP BY M.MasterLocationID, ProductID";

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                DataTable dtbData = new DataTable();
                odadPCS.Fill(dtbData);

                return dtbData;
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public DataTable GetPONotReceipt(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
        {
            const string METHOD_NAME = THIS + ".GetPONotReceipt()";

            OleDbConnection oconPCS = null;
            try
            {
                string strSql = "SELECT SUM(DeliveryQuantity) AS Quantity, ShipToLocID AS MasterLocationID, ProductID"
                    + " FROM PO_DeliverySchedule JOIN PO_PurchaseOrderDetail"
                    + " ON PO_DeliverySchedule.PurchaseOrderDetailID =  PO_PurchaseOrderDetail.PurchaseOrderDetailID"
                    + " JOIN PO_PurchaseOrderMaster ON PO_PurchaseOrderMaster.PurchaseOrderMasterID=PO_PurchaseOrderDetail.PurchaseOrderMasterID"
                    + " WHERE PO_PurchaseOrderDetail.ApproverID > 0"
                    + " AND PO_DeliverySchedule.ScheduleDate >=?"
                    + " AND PO_DeliverySchedule.ScheduleDate < ?"
                    + " GROUP BY ShipToLocID, ProductID";

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                DataTable dtbData = new DataTable();
                odadPCS.Fill(dtbData);

                return dtbData;
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public DataTable GetReturnToVendor(DateTime pdtmFromDate, DateTime pdtmToDate)
        {
            const string METHOD_NAME = THIS + ".GetReturnToVendor()";

            OleDbConnection oconPCS = null;
            try
            {
                string strSql = "SELECT SUM(Quantity) AS Quantity, MasterLocationID, ProductID"
                    + " FROM PO_ReturnToVendorDetail JOIN PO_ReturnToVendorMaster"
                    + " ON PO_ReturnToVendorDetail.ReturnToVendorMasterID = PO_ReturnToVendorMaster.ReturnToVendorMasterID"
                    + " WHERE PO_ReturnToVendorMaster.PostDate >=?"
                    + " AND PO_ReturnToVendorMaster.PostDate < ?"
                    + " GROUP BY MasterLocationID, ProductID";

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                OleDbCommand ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                DataTable dtbData = new DataTable();
                odadPCS.Fill(dtbData);

                return dtbData;
            }
            catch (OleDbException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }
        public void UpdateBeginMRP(DataTable data)
        {
            const string METHOD_NAME = THIS + ".UpdateBeginMRP()";
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            OleDbDataAdapter odadPCS = new OleDbDataAdapter();

            try
            {
                string strSql = "SELECT " + IV_BeginMRPTable.BEGINMRPID_FLD + ","
                    + IV_BeginMRPTable.ASOFTDATE_FLD + ","
                    + IV_BeginMRPTable.LOCATIONID_FLD + ","
                    + IV_BeginMRPTable.PRODUCTID_FLD + ","
                    + IV_BeginMRPTable.QUANTITYMAP_FLD + ","
                    + IV_BeginMRPTable.QUANTITY_FLD
                    + " FROM " + IV_BeginMRPTable.TABLE_NAME;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odadPCS.SelectCommand.CommandTimeout = 10000;
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                odadPCS.Update(data);

            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    else
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                else
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (InvalidOperationException ex)
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
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }

        public void ClosePOInRange(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID,
            string pstrVendorID, string pstrCategoryID, string pstrModel, string pstrProductID)
        {
            const string METHOD_NAME = THIS + ".ClosePOInRange()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "UPDATE PO_DeliverySchedule"
                    + " SET CancelDelivery = 1"
                    + " WHERE ScheduleDate >= ?"
                    + " AND ScheduleDate <= ?"
                    + " AND PurchaseOrderDetailID IN"
                    + " (SELECT PurchaseOrderDetailID FROM PO_PurchaseOrderDetail D JOIN PO_PurchaseOrderMaster M"
                    + " ON D.PurchaseOrderMasterID = M.PurchaseOrderMasterID"
                    + " JOIN MST_Party ON M.MakerID = MST_Party.PartyID"
                    + " AND CountryID = (SELECT CountryID FROM MST_CCN WHERE CCNID = " + pintCCNID + ")"
                    + " AND ProductID IN (SELECT ProductID FROM ITM_Product WHERE 1=1";
                if (pstrVendorID != null && pstrVendorID != string.Empty)
                    strSql += " AND PrimaryVendorID IN (" + pstrVendorID + ")";
                if (pstrCategoryID != null && pstrCategoryID != string.Empty)
                    strSql += " AND CategoryID IN (" + pstrCategoryID + ")";
                if (pstrModel != null && pstrModel != string.Empty)
                    strSql += " AND Revision IN (" + pstrModel + ")";
                if (pstrProductID != null && pstrProductID != string.Empty)
                    strSql += " AND ProductID IN (" + pstrProductID + ")";
                strSql += "))";
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToDate;

                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    else
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                else
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
            }
            finally
            {
                if (oconPCS != null)
                {
                    if (oconPCS.State != ConnectionState.Closed)
                    {
                        oconPCS.Close();
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using PCSComProcurement.Purchase.DS;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace PCSComProcurement.Purchase.BO
{
    /// <summary>
    /// Purchase order receipt business object
    /// </summary>
    public class PurchaseOrderReceiptBO
    {
        private const string This = "PCSComProcurement.Purchase.BO.PurchaseOrderReceiptBO";
        private static readonly object SyncRoot = new object();
        private static PurchaseOrderReceiptBO _instance;

        private PurchaseOrderReceiptBO()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static PurchaseOrderReceiptBO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new PurchaseOrderReceiptBO();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets the receipt master.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        /// <returns></returns>
        public PO_PurchaseOrderReceiptMaster GetReceiptMaster(int masterId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.PO_PurchaseOrderReceiptMasters.SingleOrDefault(r => r.PurchaseOrderReceiptID == masterId);
            }
        }

        /// <summary>
        /// Lists the receipt detail by receipt master.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        /// <returns></returns>
        public DataSet ListReceiptDetailByReceiptMaster(int masterId)
        {
            var dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
            return dsReceiptDetail.List(masterId);
        }

        /// <summary>
        /// Gets the invoice master.
        /// </summary>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns></returns>
        public PO_InvoiceMaster GetInvoiceMaster(string invoiceNumber)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.PO_InvoiceMasters.SingleOrDefault(i => i.InvoiceNo.ToLower() == invoiceNumber.ToLower());
            }
        }

        /// <summary>
        /// Gets the purchase order master.
        /// </summary>
        /// <param name="purchaseOrderMasterId">The purchase order master id.</param>
        /// <returns></returns>
        public PO_PurchaseOrderMaster GetPurchaseOrderMaster(int purchaseOrderMasterId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return
                    dataContext.PO_PurchaseOrderMasters.SingleOrDefault(
                        p => p.PurchaseOrderMasterID == purchaseOrderMasterId);
            }
        }

        /// <summary>
        /// Gets the purchase order master.
        /// </summary>
        /// <param name="poNumber">The po number.</param>
        /// <returns></returns>
        public PO_PurchaseOrderMaster GetPurchaseOrderMaster(string poNumber)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.PO_PurchaseOrderMasters.SingleOrDefault(p => p.Code.ToLower() == poNumber.ToLower());
            }
        }

        /// <summary>
        /// Gets the purchase order detail.
        /// </summary>
        /// <param name="poDetailId">The po detail id.</param>
        /// <returns></returns>
        public PO_PurchaseOrderDetail GetPurchaseOrderDetail(int poDetailId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.PO_PurchaseOrderDetails.SingleOrDefault(p => p.PurchaseOrderDetailID == poDetailId);
            }
        }

        /// <summary>
        /// Lists the by PO code.
        /// </summary>
        /// <param name="pONumber">The p O number.</param>
        /// <param name="slipDate">The slip date.</param>
        /// <returns></returns>
        public DataSet ListByPurchaseOrderCode(string pONumber, DateTime slipDate)
        {
            var dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
            return dsReceiptDetail.ListByPurchaseOrderCode(pONumber, slipDate);
        }

        /// <summary>
        /// Lists the by invoice.
        /// </summary>
        /// <param name="invoiceMasterId">The invoice master id.</param>
        /// <returns></returns>
        public DataSet ListByInvoice(int invoiceMasterId)
        {
            var dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
            return dsReceiptDetail.ListByInvoice(invoiceMasterId);
        }

        
        /// <summary>
        /// Gets the BOM.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="checkAvailable">if set to <c>true</c> [check available].</param>
        /// <returns></returns>
        public List<ITM_BOM> GetBOM(PCSDataContext dataContext, int productId, bool checkAvailable)
        {
            if (dataContext == null)
                dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return checkAvailable
                           ? dataContext.ITM_BOMs.Where(
                               b => b.ProductID == productId && !b.ITM_Product.AllowNegativeQty.GetValueOrDefault(false)).ToList
                                 ()
                           : dataContext.ITM_BOMs.Where(b => b.ProductID == productId).ToList();
        }

        /// <summary>
        /// Gets the location and bin.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="productionLineId">The production line id.</param>
        /// <returns></returns>
        public int[] GetLocationAndBin(PCSDataContext dataContext, int productionLineId)
        {
            if (dataContext == null)
                dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var query = from productionLine in dataContext.PRO_ProductionLines
                            join bin in dataContext.MST_BINs on productionLine.LocationID equals bin.LocationID
                            where bin.BinTypeID == (int?) BinTypeEnum.IN
                                  && productionLine.ProductionLineID == productionLineId
                            select new {productionLine.LocationID, bin.BinID};
                var result = new[] {(int) query.First().LocationID, query.First().BinID};
                return result;
        }

        /// <summary>
        /// Gets the available quantity.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <param name="binId">The bin id.</param>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        private static decimal GetAvailableQuantity(int locationId, int binId, int productId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                var binCache =
                    dataContext.IV_BinCaches.FirstOrDefault(
                        b => b.LocationID == locationId && b.BinID == binId && b.ProductID == productId);
                return binCache != null ? binCache.OHQuantity.GetValueOrDefault(0) : decimal.Zero;
            }
        }

        /// <summary>
        /// Checks the return to vendor
        /// </summary>
        /// <param name="receiptMaster">The receipt master.</param>
        /// <param name="productIds">The product ids.</param>
        /// <param name="byInvoice">if set to <c>true</c> [by invoice].</param>
        /// <returns></returns>
        public string CheckReturn(PO_PurchaseOrderReceiptMaster receiptMaster, List<int> productIds, bool byInvoice)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                var masters = byInvoice
                                  ? dataContext.PO_ReturnToVendorMasters.Where(
                                      m => m.InvoiceMasterID == receiptMaster.InvoiceMasterID)
                                  : dataContext.PO_ReturnToVendorMasters.Where(
                                      m => m.PurchaseOrderMasterID == receiptMaster.PurchaseOrderMasterID);
                var query = from master in masters
                            join detail in dataContext.PO_ReturnToVendorDetails on master.ReturnToVendorMasterID equals
                                detail.ReturnToVendorMasterID
                            where productIds.Contains(detail.ProductID.GetValueOrDefault(0))
                            select master.RTVNo;
                return query.Count() > 0 ? string.Join(",", query.ToArray()) : string.Empty;
            }
        }

        public void DeletePOReceipt(int purchaseOrderReceiptId)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        //0. Variables
                        const int constInspStatus = 11;
                        int oldTranTypeId =
                            dataContext.MST_TranTypes.SingleOrDefault(
                                t => t.Code.Equals(TransactionTypeEnum.POPurchaseOrderReceipts.ToString())).TranTypeID;
                        int newTranTypeId =
                            dataContext.MST_TranTypes.SingleOrDefault(
                                t => t.Code.Equals(TransactionTypeEnum.DeleteTransaction.ToString())).TranTypeID;

                        //1. Get info Purchase Order Receipt
                        var receiptMaster =
                            dataContext.PO_PurchaseOrderReceiptMasters.SingleOrDefault(
                                r => r.PurchaseOrderReceiptID == purchaseOrderReceiptId);
                        var receiptDetails = receiptMaster.PO_PurchaseOrderReceiptDetails;

                        //3. Subtract and Update Inventory and TransactionHistory
                        switch (receiptMaster.ReceiptType)
                        {
                            case (int) POReceiptTypeEnum.ByInvoice:
                            case (int) POReceiptTypeEnum.ByDeliverySlip:

                                #region 3.1 Update TransactionHistory Inventory ByDeliverySlip & Invoice

                                foreach (var receiptDetail in receiptDetails)
                                {
                                    //3.1.1 subtract Inventory
                                    int productId = receiptDetail.ProductID.GetValueOrDefault(0);

                                    int intBinId = receiptDetail.BinID.GetValueOrDefault(0);
                                    int intLocationId = receiptDetail.LocationID.GetValueOrDefault(0);
                                    if (intLocationId == 0)
                                        throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                                 new Exception());
                                    // get ReceiveQuantity from PO Receipt
                                    decimal decReceiveQuantity = receiptDetail.ReceiveQuantity;
                                    decimal decUMRate = receiptDetail.UMRate;
                                    var decReceiveQuantityUMRate = decReceiveQuantity*decUMRate;

                                    #region // update subtract cache

                                    var binCache =
                                        dataContext.IV_BinCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                      && b.BinID == intBinId &&
                                                                                      b.LocationID == intLocationId);
                                    if ((binCache.OHQuantity.GetValueOrDefault(0) -
                                         binCache.CommitQuantity.GetValueOrDefault(0)) < decReceiveQuantityUMRate)
                                        throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE,
                                                                 GetType().FullName + ".DeletePOReceipt()", null);

                                    binCache.OHQuantity = binCache.OHQuantity - decReceiveQuantityUMRate;
                                    // location cache
                                    var locationCache =
                                        dataContext.IV_LocationCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                           &&
                                                                                           b.MasterLocationID ==
                                                                                           receiptMaster.
                                                                                               MasterLocationID &&
                                                                                           b.LocationID == intLocationId);
                                    locationCache.OHQuantity = locationCache.OHQuantity - decReceiveQuantityUMRate;
                                    // master location cache
                                    var masLocCache =
                                        dataContext.IV_MasLocCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                         &&
                                                                                         b.MasterLocationID ==
                                                                                         receiptMaster.MasterLocationID &&
                                                                                         b.CCNID == receiptMaster.CCNID);
                                    masLocCache.OHQuantity = masLocCache.OHQuantity - decReceiveQuantityUMRate;

                                    #endregion
                                }
                                break;

                                #endregion

                            case (int) POReceiptTypeEnum.ByOutside:

                                #region 3.3 Update TransactionHistory Inventory ByOutside

                                var dtLocbin = GetLocationAndBin(dataContext, receiptMaster.ProductionLineID.GetValueOrDefault(0));
                                if (dtLocbin.Length == 0)
                                    throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                             new Exception());
                                int intProLocationId = dtLocbin[0];
                                int intProBinId = dtLocbin[1];

                                foreach (var receiptDetail in receiptDetails)
                                {
                                    #region 3.3.1 get info from PO Receipt details

                                    int intProductId = receiptDetail.ProductID.GetValueOrDefault(0);
                                    int intBinId = receiptDetail.BinID.GetValueOrDefault(0);
                                    int intLocationId = receiptDetail.LocationID.GetValueOrDefault(0);
                                    decimal decReceiveQuantity = receiptDetail.ReceiveQuantity;
                                    decimal decUMRate = receiptDetail.UMRate;
                                    decimal decReceiveQuantityUMRate = decReceiveQuantity*decUMRate;
                                    if (intLocationId == 0)
                                        throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                                 new Exception());

                                    #endregion

                                    #region 3.3.2 Add Inventory by Bom

                                    var bomList = GetBOM(dataContext, intProductId, false);

                                    foreach (var drowBom in bomList)
                                    {
                                        #region add cache

                                        int intComponentId = drowBom.ComponentID;
                                        decimal decQuantity = drowBom.Quantity.GetValueOrDefault(0)*decReceiveQuantity;

                                        var binCache =
                                            dataContext.IV_BinCaches.SingleOrDefault(b => b.ProductID == intComponentId
                                                                                          && b.BinID == intProBinId &&
                                                                                          b.LocationID ==
                                                                                          intProLocationId);

                                        binCache.OHQuantity = binCache.OHQuantity + decQuantity;
                                        // location cache
                                        var locationCache =
                                            dataContext.IV_LocationCaches.SingleOrDefault(
                                                b => b.ProductID == intComponentId
                                                     && b.MasterLocationID == receiptMaster.MasterLocationID &&
                                                     b.LocationID == intProLocationId);
                                        locationCache.OHQuantity = locationCache.OHQuantity - decQuantity;
                                        // master location cache
                                        var masLocCache =
                                            dataContext.IV_MasLocCaches.SingleOrDefault(
                                                b => b.ProductID == intComponentId
                                                     && b.MasterLocationID == receiptMaster.MasterLocationID &&
                                                     b.CCNID == receiptMaster.CCNID);
                                        masLocCache.OHQuantity = masLocCache.OHQuantity - decQuantity;

                                        #endregion
                                    }

                                    #endregion

                                    #region // update subtract cache

                                    var binCache1 =
                                        dataContext.IV_BinCaches.SingleOrDefault(b => b.ProductID == intProductId
                                                                                      && b.BinID == intBinId &&
                                                                                      b.LocationID == intLocationId);
                                    if ((binCache1.OHQuantity.GetValueOrDefault(0) -
                                         binCache1.CommitQuantity.GetValueOrDefault(0)) < decReceiveQuantityUMRate)
                                        throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE,
                                                                 GetType().FullName + ".DeletePOReceipt()", null);

                                    binCache1.OHQuantity = binCache1.OHQuantity - decReceiveQuantityUMRate;
                                    // location cache
                                    var locationCache1 =
                                        dataContext.IV_LocationCaches.SingleOrDefault(b => b.ProductID == intProductId
                                                                                           &&
                                                                                           b.MasterLocationID ==
                                                                                           receiptMaster.
                                                                                               MasterLocationID &&
                                                                                           b.LocationID == intLocationId);
                                    locationCache1.OHQuantity = locationCache1.OHQuantity - decReceiveQuantityUMRate;
                                    // master location cache
                                    var masLocCache1 =
                                        dataContext.IV_MasLocCaches.SingleOrDefault(b => b.ProductID == intProductId
                                                                                         &&
                                                                                         b.MasterLocationID ==
                                                                                         receiptMaster.MasterLocationID &&
                                                                                         b.CCNID == receiptMaster.CCNID);
                                    masLocCache1.OHQuantity = masLocCache1.OHQuantity - decReceiveQuantityUMRate;

                                    #endregion
                                }
                                break;

                                #endregion
                        }

                        // UnClose Purchase Order details
                        var poDetails =
                            dataContext.PO_PurchaseOrderDetails.Where(
                                d =>
                                receiptDetails.Select(r => r.PurchaseOrderDetailID).Contains(d.PurchaseOrderDetailID)).
                                ToList();
                        poDetails.ForEach(d => d.Closed = false);

                        // Update PO_DeliverySchedule 

                        #region Update Total Delivery of PO detail

                        foreach (var poDetail in poDetails)
                        {
                            decimal decReceiveQuantity =
                                receiptDetails.Where(d => d.PurchaseOrderDetailID == poDetail.PurchaseOrderDetailID).Sum
                                    (d => d.ReceiveQuantity);
                            decimal decCurrentReceived = poDetail.TotalDelivery.GetValueOrDefault(0);
                            poDetail.TotalDelivery = decCurrentReceived - decReceiveQuantity;
                        }

                        // Update DeliverySchedule
                        foreach (var receiptDetail in receiptDetails)
                        {
                            decimal decQuantityReceipt = receiptDetail.ReceiveQuantity;
                            var deliverySchedule = dataContext.PO_DeliverySchedules.SingleOrDefault(d => d.DeliveryScheduleID == receiptDetail.DeliveryScheduleID.GetValueOrDefault(0));
                            decimal currentReceived = deliverySchedule.ReceivedQuantity.GetValueOrDefault(0);
                            deliverySchedule.ReceivedQuantity = currentReceived - decQuantityReceipt;
                        }

                        #endregion

                        //4. Delete Rows in PO_PurchaseOrderReceiptDetail
                        dataContext.PO_PurchaseOrderReceiptDetails.DeleteAllOnSubmit(receiptDetails);
                        //5. Delete Row in PO_PurchaseOrderReceiptMaster
                        dataContext.PO_PurchaseOrderReceiptMasters.DeleteOnSubmit(receiptMaster);

                        // Update TransactionHistory
                        var transHistory =
                            dataContext.MST_TransactionHistories.Where(
                                h => h.RefMasterID == purchaseOrderReceiptId && h.TranTypeID == oldTranTypeId);
                        foreach (var history in transHistory)
                        {
                            history.TranTypeID = newTranTypeId;
                            history.InspStatus = constInspStatus;
                        }
                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                    }
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.CASCADE_DELETE_PREVENT)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, "DeletePOReceipt", ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, "DeletePOReceipt", ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, "DeletePOReceipt", ex);
            }
        }

        public void DeleteRowPOReceipt(int purchaseOrderReceiptId, int detailId)
        {
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        //0. Variables
                        const int constInspStatus = 11;
                        int oldTranTypeId =
                            dataContext.MST_TranTypes.SingleOrDefault(
                                t => t.Code.Equals(TransactionTypeEnum.POPurchaseOrderReceipts.ToString())).TranTypeID;
                        int newTranTypeId =
                            dataContext.MST_TranTypes.SingleOrDefault(
                                t => t.Code.Equals(TransactionTypeEnum.DeleteTransaction.ToString())).TranTypeID;

                        //1. Get info Purchase Order Receipt
                        var receiptMaster =
                            dataContext.PO_PurchaseOrderReceiptMasters.SingleOrDefault(
                                r => r.PurchaseOrderReceiptID == purchaseOrderReceiptId);
                        var receiptDetail =
                            receiptMaster.PO_PurchaseOrderReceiptDetails.SingleOrDefault(
                                d => d.PurchaseOrderReceiptDetailID == detailId);

                        //3.1.1 subtract Inventory
                        int productId = receiptDetail.ProductID.GetValueOrDefault(0);

                        int intBinId = receiptDetail.BinID.GetValueOrDefault(0);
                        int intLocationId = receiptDetail.LocationID.GetValueOrDefault(0);
                        if (intLocationId == 0)
                            throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                     new Exception());
                        // get ReceiveQuantity from PO Receipt
                        decimal decReceiveQuantity = receiptDetail.ReceiveQuantity;
                        decimal decUMRate = receiptDetail.UMRate;
                        var decReceiveQuantityUMRate = decReceiveQuantity*decUMRate;

                        //3. Subtract and Update Inventory and TransactionHistory
                        switch (receiptMaster.ReceiptType)
                        {
                            case (int) POReceiptTypeEnum.ByInvoice:
                            case (int) POReceiptTypeEnum.ByDeliverySlip:

                                #region 3.1 Update TransactionHistory Inventory ByDeliverySlip & Invoice

                                #region // update subtract cache

                                var binCachePO = dataContext.IV_BinCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                               && b.BinID == intBinId &&
                                                                                               b.LocationID ==
                                                                                               intLocationId);
                                if ((binCachePO.OHQuantity.GetValueOrDefault(0) -
                                     binCachePO.CommitQuantity.GetValueOrDefault(0)) < decReceiveQuantityUMRate)
                                    throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE,
                                                             GetType().FullName + ".DeletePOReceipt()", null);

                                binCachePO.OHQuantity = binCachePO.OHQuantity - decReceiveQuantityUMRate;
                                // location cache
                                var locationCachePO =
                                    dataContext.IV_LocationCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                       &&
                                                                                       b.MasterLocationID ==
                                                                                       receiptMaster.MasterLocationID &&
                                                                                       b.LocationID == intLocationId);
                                locationCachePO.OHQuantity = locationCachePO.OHQuantity - decReceiveQuantityUMRate;
                                // master location cache
                                var masLocCachePO =
                                    dataContext.IV_MasLocCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                     &&
                                                                                     b.MasterLocationID ==
                                                                                     receiptMaster.MasterLocationID &&
                                                                                     b.CCNID == receiptMaster.CCNID);
                                masLocCachePO.OHQuantity = masLocCachePO.OHQuantity - decReceiveQuantityUMRate;

                                #endregion

                                break;

                                #endregion

                            case (int) POReceiptTypeEnum.ByOutside:

                                #region 3.3 Update TransactionHistory Inventory ByOutside

                                var dtLocbin = GetLocationAndBin(dataContext, receiptMaster.ProductionLineID.GetValueOrDefault(0));
                                if (dtLocbin.Length == 0)
                                    throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                             new Exception());
                                int intProLocationId = dtLocbin[0];
                                int intProBinId = dtLocbin[1];

                                #region 3.3.1 get info from PO Receipt details

                                decReceiveQuantity = receiptDetail.ReceiveQuantity;
                                decUMRate = receiptDetail.UMRate;
                                decReceiveQuantityUMRate = decReceiveQuantity*decUMRate;
                                if (intLocationId == 0)
                                    throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                             new Exception());

                                #endregion

                                #region 3.3.2 Add Inventory by Bom

                                var bomList = GetBOM(dataContext, productId, false);

                                foreach (var drowBom in bomList)
                                {
                                    #region add cache

                                    int intComponentId = drowBom.ComponentID;
                                    decimal decQuantity = drowBom.Quantity.GetValueOrDefault(0)*decReceiveQuantity;

                                    var binCache =
                                        dataContext.IV_BinCaches.SingleOrDefault(b => b.ProductID == intComponentId
                                                                                      && b.BinID == intProBinId &&
                                                                                      b.LocationID == intProLocationId);

                                    binCache.OHQuantity = binCache.OHQuantity + decQuantity;
                                    // location cache
                                    var locationCache =
                                        dataContext.IV_LocationCaches.SingleOrDefault(b => b.ProductID == intComponentId
                                                                                           &&
                                                                                           b.MasterLocationID ==
                                                                                           receiptMaster.
                                                                                               MasterLocationID &&
                                                                                           b.LocationID ==
                                                                                           intProLocationId);
                                    locationCache.OHQuantity = locationCache.OHQuantity - decQuantity;
                                    // master location cache
                                    var masLocCache =
                                        dataContext.IV_MasLocCaches.SingleOrDefault(b => b.ProductID == intComponentId
                                                                                         &&
                                                                                         b.MasterLocationID ==
                                                                                         receiptMaster.MasterLocationID &&
                                                                                         b.CCNID == receiptMaster.CCNID);
                                    masLocCache.OHQuantity = masLocCache.OHQuantity - decQuantity;

                                    #endregion
                                }

                                #endregion

                                #region // update subtract cache

                                var binCache1 = dataContext.IV_BinCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                              && b.BinID == intBinId &&
                                                                                              b.LocationID ==
                                                                                              intLocationId);
                                if ((binCache1.OHQuantity.GetValueOrDefault(0) -
                                     binCache1.CommitQuantity.GetValueOrDefault(0)) < decReceiveQuantityUMRate)
                                    throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE,
                                                             GetType().FullName + ".DeletePOReceipt()", null);

                                binCache1.OHQuantity = binCache1.OHQuantity - decReceiveQuantityUMRate;
                                // location cache
                                var locationCache1 =
                                    dataContext.IV_LocationCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                       &&
                                                                                       b.MasterLocationID ==
                                                                                       receiptMaster.MasterLocationID &&
                                                                                       b.LocationID == intLocationId);
                                locationCache1.OHQuantity = locationCache1.OHQuantity - decReceiveQuantityUMRate;
                                // master location cache
                                var masLocCache1 =
                                    dataContext.IV_MasLocCaches.SingleOrDefault(b => b.ProductID == productId
                                                                                     &&
                                                                                     b.MasterLocationID ==
                                                                                     receiptMaster.MasterLocationID &&
                                                                                     b.CCNID == receiptMaster.CCNID);
                                masLocCache1.OHQuantity = masLocCache1.OHQuantity - decReceiveQuantityUMRate;

                                #endregion

                                break;

                                #endregion
                        }

                        // Update TransactionHistory
                        var transHistory =
                            dataContext.MST_TransactionHistories.Where(
                                h => h.RefMasterID == purchaseOrderReceiptId && h.TranTypeID == oldTranTypeId);
                        foreach (var history in transHistory)
                        {
                            history.TranTypeID = newTranTypeId;
                            history.InspStatus = constInspStatus;
                        }
                        dataContext.PO_PurchaseOrderReceiptDetails.DeleteOnSubmit(receiptDetail);
                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                    }
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.CASCADE_DELETE_PREVENT)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, "DeletePOReceipt", ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, "DeletePOReceipt", ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, "DeletePOReceipt", ex);
            }
        }

        private static IEnumerable<int> GetComponentOfItem(PCSDataContext dataContext, int productId)
        {
            return dataContext.ITM_BOMs.Where(b => b.ProductID == productId).Select(b => b.ComponentID);
        }

        private static decimal GetQuantityFromCache<T>(IEnumerable<T> pdtbCacheData, int pintId, int pintProductId, int pintType, out decimal odecCommitQuantity)
        {
            odecCommitQuantity = 0;
            var onhandQuantity = decimal.Zero;
            try
            {
                switch (pintType)
                {
                    case 1: // master location
                        var masLoc = pdtbCacheData.Cast<IV_MasLocCache>().FirstOrDefault(
                                e => e.ProductID == pintProductId && e.MasterLocationID == pintId);
                        if (masLoc != null)
                        {
                            odecCommitQuantity = masLoc.CommitQuantity.GetValueOrDefault(0);
                            onhandQuantity = masLoc.OHQuantity.GetValueOrDefault(0);
                        }
                        break;
                    case 2: // location
                        var loc = pdtbCacheData.Cast<IV_LocationCache>().FirstOrDefault(
                                e => e.ProductID == pintProductId && e.LocationID == pintId);
                        if (loc != null)
                        {
                            odecCommitQuantity = loc.CommitQuantity.GetValueOrDefault(0);
                            onhandQuantity = loc.OHQuantity.GetValueOrDefault(0);
                        }
                        break;
                    default: // bin
                        var bin = pdtbCacheData.Cast<IV_BinCache>().FirstOrDefault(
                                e => e.ProductID == pintProductId && e.BinID == pintId);
                        if (bin != null)
                        {
                            odecCommitQuantity = bin.CommitQuantity.GetValueOrDefault(0);
                            onhandQuantity = bin.OHQuantity.GetValueOrDefault(0);
                        }
                        break;
                }
                return onhandQuantity;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Adds the master receipt.
        /// </summary>
        /// <param name="receiptMaster">The receipt master.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="ccnId">The CCN id.</param>
        /// <param name="currentDate">The current date.</param>
        /// <returns></returns>
        public int AddMasterReceipt(PO_PurchaseOrderReceiptMaster receiptMaster, DataSet dataSource, int ccnId, DateTime currentDate)
        {
            const string methodName = This + ".AddMasterReceipt()";
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    PO_PurchaseOrderReceiptMaster newMaster;
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        #region Variables

                        var deliveryScheduleIds = new List<int>();
                        var locationIds = new List<int>();
                        var binIds = new List<int>();
                        var productIds = new List<int>();
                        int intTranTypeId =
                            dataContext.MST_TranTypes.SingleOrDefault(
                                t =>
                                t.Code.ToLower() == TransactionTypeEnum.POPurchaseOrderReceipts.ToString().ToLower()).
                                TranTypeID;
                        var serverDate = dataContext.GetServerDate();

                        #endregion

                        #region validate data

                        int intProLocationId = 0, intProBinId = 0;
                        if (receiptMaster.ProductionLineID > 0)
                        {
                            var dtbLocationBin = GetLocationAndBin(dataContext, receiptMaster.ProductionLineID.GetValueOrDefault(0));
                            if (dtbLocationBin.Length == 0)
                                throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                         new Exception());
                            intProLocationId = dtbLocationBin[0];
                            intProBinId = dtbLocationBin[1];
                            locationIds.Add(intProLocationId);
                            binIds.Add(intProBinId);
                        }

                        #endregion

                        #region receipt master & detail data

                        var receiptDetailList = new List<PO_PurchaseOrderReceiptDetail>();
                        newMaster = new PO_PurchaseOrderReceiptMaster
                                            {
                                                CCNID = receiptMaster.CCNID,
                                                InvoiceMasterID = receiptMaster.InvoiceMasterID,
                                                LastChange = receiptMaster.LastChange,
                                                MasterLocationID = receiptMaster.MasterLocationID,
                                                POSlipNo = receiptMaster.POSlipNo,
                                                PostDate = receiptMaster.PostDate,
                                                ProductionLineID = receiptMaster.ProductionLineID,
                                                PurchaseOrderMasterID = receiptMaster.PurchaseOrderMasterID,
                                                Purpose = receiptMaster.Purpose,
                                                ReceiptType = receiptMaster.ReceiptType,
                                                ReceiveNo = receiptMaster.ReceiveNo,
                                                RefNo = receiptMaster.RefNo,
                                                UserName = receiptMaster.UserName
                                            };
                        var purchaseOrderDetailIds = new List<int>();
                        // create new detail then add to master object
                        foreach (DataRow drowData in dataSource.Tables[0].Rows)
                        {
                            // ignore deleted row
                            if (drowData.RowState == DataRowState.Deleted)
                                continue;
                            var detail = CreateNewDetail(drowData);
                            newMaster.PO_PurchaseOrderReceiptDetails.Add(detail);
                            receiptDetailList.Add(detail);

                            purchaseOrderDetailIds.Add(
                                Convert.ToInt32(drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD]));
                            locationIds.Add(Convert.ToInt32(drowData[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD]));
                            binIds.Add(Convert.ToInt32(drowData[PO_PurchaseOrderReceiptDetailTable.BINID_FLD]));
                            productIds.Add(Convert.ToInt32(drowData[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD]));
                            productIds.AddRange(GetComponentOfItem(dataContext, (int) drowData[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD]));
                        }
                        // temporary save master and detail to database
                        dataContext.PO_PurchaseOrderReceiptMasters.InsertOnSubmit(newMaster);
                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                        #endregion

                        #region prepare data

                        int purchaseOrderMasterId = newMaster.PurchaseOrderMasterID.GetValueOrDefault(0);
                        // Check receipted to Close PO
                        var purchaseOrderDetails = (from receiptDetail in dataContext.PO_PurchaseOrderReceiptDetails
                                                    join orderDetail in dataContext.PO_PurchaseOrderDetails on
                                                        receiptDetail.PurchaseOrderDetailID equals
                                                        orderDetail.PurchaseOrderDetailID
                                                    where orderDetail.PurchaseOrderMasterID == purchaseOrderMasterId
                                                    group receiptDetail by new
                                                                               {
                                                                                   orderDetail.PurchaseOrderMasterID,
                                                                                   orderDetail.PurchaseOrderDetailID,
                                                                                   orderDetail.OrderQuantity
                                                                               }
                                                    into g
                                                    select new
                                                               {
                                                                   g.Key.PurchaseOrderMasterID,
                                                                   g.Key.PurchaseOrderDetailID,
                                                                   g.Key.OrderQuantity,
                                                                   ReceiveQuantity = g.Sum(d => d.ReceiveQuantity)
                                                               }).Where(x => x.ReceiveQuantity >= x.OrderQuantity);
                        if (purchaseOrderDetailIds.Count > 0)
                            purchaseOrderDetails =
                                purchaseOrderDetails.Where(e => purchaseOrderDetailIds.Contains(e.PurchaseOrderDetailID));
                        var query = purchaseOrderDetails.Select(p => p.PurchaseOrderDetailID).ToList();
                        var closedPOs =
                            dataContext.PO_PurchaseOrderDetails.Where(
                                d => d.PurchaseOrderMasterID == purchaseOrderMasterId
                                     && query.Contains(d.PurchaseOrderDetailID)).ToList();
                        foreach (var poPurchaseOrderDetail in closedPOs)
                            poPurchaseOrderDetail.Closed = true;

                        #endregion

                        #region update inventory

                        decimal decOHQuantity;
                        foreach (var drowData in newMaster.PO_PurchaseOrderReceiptDetails)
                        {
                            int intProductId = drowData.ProductID.GetValueOrDefault(0);
                            if (!deliveryScheduleIds.Contains(drowData.DeliveryScheduleID.GetValueOrDefault(0)))
                                deliveryScheduleIds.Add(drowData.DeliveryScheduleID.GetValueOrDefault(0));

                            // get ReceiveQuantity from PO Receipt
                            decimal decReceiveQuantity = drowData.ReceiveQuantity;
                            decimal decUMRate = drowData.UMRate;
                            decimal decReceiveQuantityUMRate = decReceiveQuantity*decUMRate;

                            #region Inventory

                            int intBinId = drowData.BinID.GetValueOrDefault(0);
                            if (drowData.LocationID == null)
                                throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty,
                                                         new Exception());
                            int intLocationId = drowData.LocationID.GetValueOrDefault(0);

                            #region master location cache

                            var masLocCache =
                                dataContext.IV_MasLocCaches.SingleOrDefault(
                                    e => e.MasterLocationID == newMaster.MasterLocationID && e.ProductID == intProductId);
                            if (masLocCache != null)
                            {
                                masLocCache.OHQuantity = masLocCache.OHQuantity == null
                                                             ? decReceiveQuantityUMRate
                                                             : masLocCache.OHQuantity + decReceiveQuantityUMRate;
                            }
                            else
                            {
                                masLocCache = new IV_MasLocCache
                                                  {
                                                      CCNID = ccnId,
                                                      MasterLocationID = newMaster.MasterLocationID,
                                                      ProductID = intProductId,
                                                      OHQuantity = decReceiveQuantityUMRate
                                                  };
                                dataContext.IV_MasLocCaches.InsertOnSubmit(masLocCache);
                            }
                            dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                            #endregion

                            #region location cache

                            var locCache =
                                dataContext.IV_LocationCaches.SingleOrDefault(
                                    e => e.LocationID == intLocationId && e.ProductID == intProductId);
                            if (locCache != null)
                            {
                                locCache.OHQuantity = locCache.OHQuantity == null
                                                          ? decReceiveQuantityUMRate
                                                          : locCache.OHQuantity + decReceiveQuantityUMRate;
                            }
                            else
                            {
                                locCache = new IV_LocationCache
                                               {
                                                   CCNID = ccnId,
                                                   MasterLocationID = newMaster.MasterLocationID,
                                                   LocationID = intLocationId,
                                                   ProductID = intProductId,
                                                   OHQuantity = decReceiveQuantityUMRate
                                               };
                                dataContext.IV_LocationCaches.InsertOnSubmit(locCache);
                            }
                            dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                            #endregion

                            #region bin cache

                            var binCache =
                                dataContext.IV_BinCaches.SingleOrDefault(
                                    e => e.BinID == intBinId && e.ProductID == intProductId);
                            if (binCache != null)
                            {
                                binCache.OHQuantity = locCache.OHQuantity == null
                                                          ? decReceiveQuantityUMRate
                                                          : binCache.OHQuantity + decReceiveQuantityUMRate;
                            }
                            else
                            {
                                binCache = new IV_BinCache
                                               {
                                                   CCNID = ccnId,
                                                   MasterLocationID = newMaster.MasterLocationID,
                                                   LocationID = intLocationId,
                                                   BinID = intBinId,
                                                   ProductID = intProductId,
                                                   OHQuantity = decReceiveQuantityUMRate
                                               };
                                dataContext.IV_BinCaches.InsertOnSubmit(binCache);
                            }
                            dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                            #endregion

                            #endregion

                            #region Transasction History

                            decimal decCommitQuantity;
                            var drowTransaction = new MST_TransactionHistory
                                                      {
                                                          CCNID = ccnId,
                                                          TransDate = serverDate,
                                                          PostDate = newMaster.PostDate,
                                                          RefMasterID = newMaster.PurchaseOrderReceiptID,
                                                          RefDetailID = drowData.PurchaseOrderReceiptDetailID,
                                                          ProductID = intProductId,
                                                          TranTypeID = intTranTypeId,
                                                          UserName = newMaster.UserName,
                                                          Quantity = decReceiveQuantity,
                                                          MasterLocationID = newMaster.MasterLocationID,
                                                          LocationID = intLocationId,
                                                          BinID = intBinId,
                                                          StockUMID = drowData.StockUMID
                                                      };
                            decOHQuantity = GetQuantityFromCache(dataContext.IV_MasLocCaches, newMaster.MasterLocationID,
                                                                 intProductId, 1, out decCommitQuantity);
                            drowTransaction.MasLocOHQuantity = decOHQuantity;
                            drowTransaction.MasLocCommitQuantity = decCommitQuantity;
                            decOHQuantity = GetQuantityFromCache(dataContext.IV_LocationCaches, intLocationId,
                                                                 intProductId, 2, out decCommitQuantity);
                            drowTransaction.LocationOHQuantity = decOHQuantity;
                            drowTransaction.LocationCommitQuantity = decCommitQuantity;
                            decOHQuantity = GetQuantityFromCache(dataContext.IV_BinCaches, intBinId, intProductId, 3,
                                                                 out decCommitQuantity);
                            drowTransaction.BinOHQuantity = decOHQuantity;
                            drowTransaction.BinCommitQuantity = decCommitQuantity;
                            dataContext.MST_TransactionHistories.InsertOnSubmit(drowTransaction);
                            dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                            #endregion
                        }

                        #endregion

                        #region Receipt by outside

                        if (newMaster.ReceiptType == (int) POReceiptTypeEnum.ByOutside)
                        {
                            foreach (var drow in newMaster.PO_PurchaseOrderReceiptDetails)
                            {
                                var dtbBom = GetBOM(dataContext, drow.ProductID.GetValueOrDefault(0), false);
                                foreach (var drowBom in dtbBom)
                                {
                                    #region subtract cache

                                    // Get available quantity by Postdate
                                    int intComponentId = drowBom.ComponentID;

                                    decimal decBOMQty = drowBom.Quantity.GetValueOrDefault(0);
                                    decimal decOrderQty = drow.ReceiveQuantity;

                                    if (!drowBom.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                                    {
                                        decimal decAvail = GetAvailableQuantity(intProLocationId, intProBinId,
                                                                                intComponentId);
                                        if (decAvail < decOrderQty*decBOMQty)
                                            throw new PCSBOException(ErrorCode.MESSAGE_ISSUE_MATERIAL_TO_OUTSIDE,
                                                                     string.Empty, new Exception());
                                    }

                                    #region master location cache

                                    var masLocCache =
                                        dataContext.IV_MasLocCaches.SingleOrDefault(
                                            e =>
                                            e.MasterLocationID == newMaster.MasterLocationID &&
                                            e.ProductID == intComponentId);
                                    decimal decReceiveQuantity = decBOMQty*decOrderQty;
                                    if (masLocCache != null)
                                    {
                                        masLocCache.OHQuantity = masLocCache.OHQuantity == null
                                                                     ? -decReceiveQuantity
                                                                     : masLocCache.OHQuantity - decReceiveQuantity;
                                    }
                                    else
                                    {
                                        masLocCache = new IV_MasLocCache
                                                          {
                                                              CCNID = ccnId,
                                                              MasterLocationID = newMaster.MasterLocationID,
                                                              ProductID = intComponentId,
                                                              OHQuantity = -decReceiveQuantity
                                                          };
                                        dataContext.IV_MasLocCaches.InsertOnSubmit(masLocCache);
                                    }
                                    dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                                    #endregion

                                    #region location cache

                                    var locCache =
                                        dataContext.IV_LocationCaches.SingleOrDefault(
                                            e => e.LocationID == intProLocationId && e.ProductID == intComponentId);
                                    if (locCache != null)
                                    {
                                        locCache.OHQuantity = locCache.OHQuantity == null
                                                                  ? -decReceiveQuantity
                                                                  : locCache.OHQuantity - decReceiveQuantity;
                                    }
                                    else
                                    {
                                        locCache = new IV_LocationCache
                                                       {
                                                           CCNID = ccnId,
                                                           MasterLocationID = newMaster.MasterLocationID,
                                                           LocationID = intProLocationId,
                                                           ProductID = intComponentId,
                                                           OHQuantity = -decReceiveQuantity
                                                       };
                                        dataContext.IV_LocationCaches.InsertOnSubmit(locCache);
                                    }
                                    dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                                    #endregion

                                    #region bin cache

                                    var binCache =
                                        dataContext.IV_BinCaches.SingleOrDefault(
                                            e => e.BinID == intProBinId && e.ProductID == intComponentId);
                                    if (binCache != null)
                                    {
                                        binCache.OHQuantity = locCache.OHQuantity == null
                                                                  ? -decReceiveQuantity
                                                                  : binCache.OHQuantity - decReceiveQuantity;
                                    }
                                    else
                                    {
                                        binCache = new IV_BinCache
                                                       {
                                                           CCNID = ccnId,
                                                           MasterLocationID = newMaster.MasterLocationID,
                                                           LocationID = intProLocationId,
                                                           BinID = intProBinId,
                                                           ProductID = intComponentId,
                                                           OHQuantity = -decReceiveQuantity
                                                       };
                                        dataContext.IV_BinCaches.InsertOnSubmit(binCache);
                                    }
                                    dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                                    #endregion

                                    #endregion

                                    #region Transasction History

                                    decimal decCommitQuantity;
                                    var drowTransaction = new MST_TransactionHistory
                                                              {
                                                                  CCNID = ccnId,
                                                                  TransDate = serverDate,
                                                                  PostDate = newMaster.PostDate,
                                                                  RefMasterID = newMaster.PurchaseOrderReceiptID,
                                                                  RefDetailID = drow.PurchaseOrderReceiptDetailID,
                                                                  ProductID = intComponentId,
                                                                  TranTypeID = intTranTypeId,
                                                                  UserName = newMaster.UserName,
                                                                  Quantity = -decReceiveQuantity,
                                                                  MasterLocationID = newMaster.MasterLocationID,
                                                                  LocationID = intProLocationId,
                                                                  BinID = intProBinId,
                                                                  StockUMID = drowBom.ITM_Product.StockUMID
                                                              };
                                    decOHQuantity = GetQuantityFromCache(dataContext.IV_MasLocCaches,
                                                                         newMaster.MasterLocationID, intComponentId, 1,
                                                                         out decCommitQuantity);
                                    drowTransaction.MasLocOHQuantity = decOHQuantity;
                                    drowTransaction.MasLocCommitQuantity = decCommitQuantity;
                                    decOHQuantity = GetQuantityFromCache(dataContext.IV_LocationCaches, intProLocationId,
                                                                         intComponentId, 2, out decCommitQuantity);
                                    drowTransaction.LocationOHQuantity = decOHQuantity;
                                    drowTransaction.LocationCommitQuantity = decCommitQuantity;
                                    decOHQuantity = GetQuantityFromCache(dataContext.IV_BinCaches, intProBinId,
                                                                         intComponentId, 3, out decCommitQuantity);
                                    drowTransaction.BinOHQuantity = decOHQuantity;
                                    drowTransaction.BinCommitQuantity = decCommitQuantity;
                                    dataContext.MST_TransactionHistories.InsertOnSubmit(drowTransaction);
                                    dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);

                                    #endregion
                                }
                            }
                        }

                        #endregion

                        #region Update Received Quantity for Delivery Schedule

                        var schedules =
                            dataContext.PO_DeliverySchedules.Where(
                                d => deliveryScheduleIds.Contains(d.DeliveryScheduleID)).ToList();
                        foreach (var poDeliverySchedule in schedules)
                        {
                            decimal receiveQuantity =
                                newMaster.PO_PurchaseOrderReceiptDetails.Where(
                                    d => d.DeliveryScheduleID == poDeliverySchedule.DeliveryScheduleID).Sum(
                                        p => p.ReceiveQuantity);
                            decimal currentReceived = poDeliverySchedule.ReceivedQuantity ?? 0;
                            poDeliverySchedule.ReceivedQuantity = currentReceived + receiveQuantity;
                        }

                        #endregion

                        #region Update Total Delivery of PO detail

                        var poDetails =
                            dataContext.PO_PurchaseOrderDetails.Where(
                                d => purchaseOrderDetailIds.Contains(d.PurchaseOrderDetailID));
                        foreach (var poPurchaseOrderDetail in poDetails)
                        {
                            decimal receiveQuantity =
                                newMaster.PO_PurchaseOrderReceiptDetails.Where(
                                    d => d.PurchaseOrderDetailID == poPurchaseOrderDetail.PurchaseOrderDetailID).Sum(
                                        p => p.ReceiveQuantity);
                            decimal currentReceived = poPurchaseOrderDetail.TotalDelivery ?? 0;
                            poPurchaseOrderDetail.TotalDelivery = currentReceived + receiveQuantity;
                            // if total delivery of PO Line >= to order quantity then auto close of PO Line
                            if (poPurchaseOrderDetail.TotalDelivery >= poPurchaseOrderDetail.OrderQuantity)
                                poPurchaseOrderDetail.Closed = true;
                        }

                        #endregion

                        dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
                    }

                    trans.Complete();
                    return newMaster.PurchaseOrderReceiptID;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
            }
        }

        private static PO_PurchaseOrderReceiptDetail CreateNewDetail(DataRow rowDetail)
        {
            var newDetail = new PO_PurchaseOrderReceiptDetail
                                {
                                    BinID = (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.BINID_FLD],
                                    BuyingUMID = (int)rowDetail[PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD],
                                    DeliveryScheduleID = (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD],
                                    InvoiceDetailID = rowDetail[PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD] == DBNull.Value ? null : (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.INVOICEDETAILID_FLD],
                                    LocationID = (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD],
                                    Lot = rowDetail[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].ToString(),
                                    ProductID = (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD],
                                    UMRate = (decimal)rowDetail[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD],
                                    StockUMID = (int)rowDetail[PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD],
                                    ReceiveQuantity = (decimal)rowDetail[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD],
                                    PurchaseOrderMasterID = rowDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD] == DBNull.Value ? null : (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD],
                                    PurchaseOrderDetailID = rowDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD] == DBNull.Value ? null : (int?)rowDetail[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD],
                                    QAStatus = rowDetail[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD] == DBNull.Value ? null : (byte?)rowDetail[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD],
                                    Serial = rowDetail[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD] == DBNull.Value ? string.Empty : rowDetail[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].ToString()
                                };
            return newDetail;
        }

        /// <summary>
        /// Gets the bin cache data.
        /// </summary>
        /// <param name="locationId">Location id</param>
        /// <param name="binId">The bin id.</param>
        /// <returns></returns>
        public List<IV_BinCache> GetBinCacheData(int locationId, int binId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.IV_BinCaches.Where(b => b.LocationID == locationId && b.BinID == binId).ToList();
            }
        }
    }
}

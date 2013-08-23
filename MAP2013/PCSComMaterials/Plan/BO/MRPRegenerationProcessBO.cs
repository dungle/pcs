using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using PCSComMaterials.Plan.DS;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;
using IsolationLevel=System.Transactions.IsolationLevel;

namespace PCSComMaterials.Plan.BO

{
	/// <summary>
	/// Summary description for MRPRegenerationProcessBO.
	/// </summary>
	public class MRPRegenerationProcessBO
	{
	    private class AvalableQuantity
	    {
            internal int LocationId { get; set; }
            internal int ProductId { get; set; }
            internal decimal? SupplyQuantity { get; set; }
	    }
        private class ReleasedPurchaseOrder
        {
            internal int ProductId { get; set; }
            internal DateTime ScheduleDate { get; set; }
            internal decimal DeliveryQuantity { get; set; }
        }
        private class SupplyPurchaseOrder: ReleasedPurchaseOrder
        {
            internal bool Used { get; set; }
        }
        private class CPODetail
        {
            internal int ProductId { get; set; }
            internal DateTime DueDate { get; set; }
            internal DateTime StartTime { get; set; }
            internal decimal Quantity { get; set; }
            internal int? StockUmId { get; set; }
            internal decimal? Shrink { get; set; }
            internal decimal? LeadTimeOffset { get; set; }
        }
        private class DemandMrp
        {
            internal int ProductId { get; set; }
            internal DateTime DueDate { get; set; }
            internal decimal DemandQuantity { get; set; }
            internal bool Calculated { get; set; }
        }
        private class ReleaseSaleOrder
        {
            internal int ProductId { get; set; }
            internal string Code { get; set; }
            internal DateTime TransDate { get; set; }
            internal int SaleOrderLine { get; set; }
            internal DateTime DueDate { get; set; }
            internal decimal Quantity { get; set; }
            internal decimal? UnitPrice { get; set; }
            internal int SaleOrderDetailId { get; set; }
            internal int? SellingUMId { get; set; }
            internal int? StockUMId { get; set; }
            internal int SaleOrderMasterId { get; set; }
        }
        private class PurchaseOrder
        {
            internal int ProductId { get; set; }
            internal int MasterLocationId { get; set; }
            internal decimal Quantity { get; set; }
        }

        private readonly List<MTR_CPO> _cpoList = new List<MTR_CPO>();
        private const string THIS = "PCSComMaterials.Plan.BO.MRPRegenerationProcessBO";

	    /// <summary>
		/// GetCycleByMasterID
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 21 2006</date>
		public object GetCycleByMasterID(int pintMasterID)
		{
			MTR_MRPCycleOptionMasterDS dsMTR_MRPCycleOptionMaster = new MTR_MRPCycleOptionMasterDS();
			return dsMTR_MRPCycleOptionMaster.GetObjectVO(pintMasterID);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pdtAsOfDate"></param>
		/// <param name="pintThroughHorizon"></param>
		/// <returns></returns>
		private static DateTime GetLimitPlaningCycle(DateTime pdtAsOfDate ,int pintThroughHorizon)
		{
            DateTime dtReturn = pdtAsOfDate.AddDays(pintThroughHorizon);
            dtReturn = new DateTime(dtReturn.Year, dtReturn.Month, dtReturn.Day, 23, 59, 59);
            return dtReturn;
		}

		/// <summary>
		/// MRP Generation CPO
		/// </summary>
		private List<IV_BeginMRP> GenerateMrp(PCSDataContext dataContext, List<ITM_Product> items, MTR_MRPCycleOptionDetail cycleDetail, int masterLocId,
            MTR_MRPCycleOptionMaster cycleMaster, PRO_DCOptionMaster dcpCycleMaster, PRO_DCOptionMaster dcpPreCycleMaster,
            List<PurchaseOrder> notReceiptPO, List<PurchaseOrder> receiptPO, List<PurchaseOrder> returnToVendor, DateTime dtmServerDate)
		{
            DateTime dtDueDate = GetLimitPlaningCycle(cycleMaster.AsOfDate, cycleMaster.PlanHorizon.GetValueOrDefault(0));
            var availableQuantityPlan = new List<AvalableQuantity>();
            var dtmBeginMonth = new DateTime(dtmServerDate.Year, dtmServerDate.Month, 1);

            #region begin for niguri

            var beginNiguri = new List<IV_BeginMRP>();
		    var beginMrp = dataContext.IV_BeginMRPs.Where(b => b.AsOfDate.Equals(cycleMaster.AsOfDate)).ToList();

            #endregion

            if (cycleDetail.OnHand)
            {
                availableQuantityPlan = GetAvailableQuantityPlan(dataContext, dtmBeginMonth);
            }

            #region Calculate demand and supply

            var allDemand = new List<DemandMrp>();
            var allRequestPrevious = new List<DemandMrp>();
            var itemsReplenishPrevious = GetAllReleasePOForItems(dataContext, items, dtmBeginMonth, cycleMaster.AsOfDate, masterLocId);
            allRequestPrevious.AddRange(GetAllReleaseSOForItems(dataContext, items, dtmBeginMonth, cycleMaster.AsOfDate, masterLocId).Select(
                                            p => new DemandMrp
                                            {
                                                DemandQuantity = (decimal)p.DeliveryQuantity,
                                                DueDate = (DateTime)p.ScheduleDate,
                                                ProductId = p.SO_SaleOrderDetail.ProductID
                                            }));
            // change request: use dcp result instead of work order
            allRequestPrevious.AddRange(GetAllCPOForItems(dataContext, items, dtmBeginMonth, cycleMaster.AsOfDate, dcpPreCycleMaster.DCOptionMasterID).Select(
                                            p => new DemandMrp
                                            {
                                                DemandQuantity = p.Quantity,
                                                DueDate = p.DueDate,
                                                ProductId = p.ProductId
                                            }));
            if (cycleDetail.DemandWO)
            {
                var workOrderItems = GetAllCPOForItems(dataContext, items, cycleMaster.AsOfDate, dtDueDate, dcpCycleMaster.DCOptionMasterID);
                allDemand.AddRange(workOrderItems.Select(p => new DemandMrp
                {
                    DemandQuantity = p.Quantity,
                    DueDate = p.StartTime,
                    ProductId = p.ProductId
                }));
            }

            //Step 2 : Get all request for Items
            if (cycleDetail.SaleOrder)
            {
                var saleItems = GetAllReleaseSOForItemsInCycle(dataContext, items, cycleMaster.AsOfDate, dtDueDate, masterLocId);
                allDemand.AddRange(saleItems.Select(p => new DemandMrp
                {
                    DemandQuantity = p.Quantity,
                    DueDate = p.DueDate,
                    ProductId = p.ProductId
                }));
            }

            //Get All Replenish Items
            var itemsReplenish = new List<SupplyPurchaseOrder>();
            if (cycleDetail.PurchaseOrder)
            {
                itemsReplenish = GetSupplyPurchaseOrder(dataContext, items, cycleMaster.AsOfDate, dtDueDate, masterLocId, cycleMaster.CCNID);
            }

            #endregion

            #region Generate for each item

            foreach (var item in items)
            {
                ITM_Product product = item;
                DateTime dtmDueDate;

                var allItemsRequest = allDemand.Where(p => p.ProductId == product.ProductID).OrderBy(p => p.DueDate).ToList();
                var allItemsReplenish = itemsReplenish.Where(p => p.ProductId == product.ProductID).ToList();

                #region 09-10-2006 dungla: calculate stock at as of date for NIGURI REPORT

                var decLocationQuantity = availableQuantityPlan.Where(
                    b => b.ProductId == product.ProductID && b.LocationId == product.LocationID).
                    Sum(b => b.SupplyQuantity);

                #endregion

                decimal decAvailable = 0, decAvailableNigury = 0;
                if (cycleDetail.OnHand)
                {
                    decAvailable = GetAllRemainBeforeRun(product.ProductID, allRequestPrevious,
                                                         itemsReplenishPrevious);
                    decAvailableNigury = decAvailable;

                    #region thuytpt request: move demand from safety stock to end of cycle

                    if (product.SafetyStock.HasValue)
                        decAvailable -= (decimal)product.SafetyStock;

                    #endregion

                    decLocationQuantity += decAvailableNigury;
                    var availablePlan = availableQuantityPlan.Where(p => p.ProductId == product.ProductID).Sum(p => p.SupplyQuantity.GetValueOrDefault(0));
                    decAvailable += availablePlan;
                    decAvailableNigury += availablePlan;
                }

                #region 29-06-2006 dungla: recalculate begin onhand quantity based on cycle option

                decimal decPONotReceipt = 0, decReturnToVendor = 0;
                if (cycleMaster.IncludedRemainPO.GetValueOrDefault(false))
                {
                    decPONotReceipt = receiptPO.Where(
                                          p =>
                                          p.MasterLocationId == masterLocId && p.ProductId == product.ProductID)
                                          .Sum(p => p.Quantity)
                                      -
                                      notReceiptPO.Where(
                                          p =>
                                          p.MasterLocationId == masterLocId && p.ProductId == product.ProductID)
                                          .Sum(p => p.Quantity);

                    if (cycleMaster.IncludedReturnToVendor.GetValueOrDefault(false))
                    {
                        decReturnToVendor =
                            returnToVendor.Where(
                                p => p.MasterLocationId == masterLocId && p.ProductId == product.ProductID).Sum(
                                p => p.Quantity);
                    }
                }
                // Begin OH = OH(AsofDate) + Sum(PO.Delivery - POReceipt) - Sum(ReturnToVendor)
                decAvailable = decAvailable - decPONotReceipt - decReturnToVendor;
                decAvailableNigury = decAvailableNigury - decPONotReceipt - decReturnToVendor;
                decLocationQuantity = decLocationQuantity - decPONotReceipt - decReturnToVendor;
                var beginRow = beginMrp.SingleOrDefault(p => p.ProductID == product.ProductID && p.LocationID == product.LocationID && p.AsOfDate.Equals(cycleMaster.AsOfDate));
                if (beginRow != null) // already exits, need to update new quantity
                {
                    var existedRow = new IV_BeginMRP
                                         {
                                             AsOfDate = beginRow.AsOfDate,
                                             LocationID = beginRow.LocationID,
                                             ProductID = beginRow.ProductID,
                                             Quantity = decLocationQuantity,
                                             QuantityMAP = decAvailableNigury
                                         };
                    beginNiguri.Add(existedRow);
                }
                else // make a new record
                {
                    beginRow = new IV_BeginMRP
                    {
                        AsOfDate = cycleMaster.AsOfDate,
                        LocationID = product.LocationID.GetValueOrDefault(0),
                        ProductID = product.ProductID,
                        Quantity = decLocationQuantity,
                        QuantityMAP = decAvailableNigury
                    };
                    beginNiguri.Add(beginRow);
                }

                #endregion

                decimal decReplenish = 0;
                for (int i = 0; i < allItemsRequest.Count; i++)
                {
                    var itemRequest = allItemsRequest[i];
                    if (itemRequest.Calculated)
                    {
                        continue;
                    }
                    var decRequest = itemRequest.DemandQuantity;
                    dtmDueDate = itemRequest.DueDate;
                    // now find all request have same due date and not calculated
                    for (int j = i + 1; j < allItemsRequest.Count(); j++)
                    {
                        var nextRequest = allItemsRequest[j];
                        if (!itemRequest.DueDate.Equals(nextRequest.DueDate))
                        {
                            break;
                        }
                        if (nextRequest.Calculated)
                        {
                            continue;
                        }
                        decRequest += nextRequest.DemandQuantity;
                        // mark as calculated
                        nextRequest.Calculated = true;
                    }
                    foreach (var replenish in allItemsReplenish.Where(replenish => replenish.ScheduleDate <= itemRequest.DueDate && !replenish.Used))
                    {
                        decReplenish += replenish.DeliveryQuantity;
                        replenish.Used = true;
                    }
                    decimal decRemain = decAvailable - decRequest;
                    decAvailable = decRemain >= 0 ? decRemain : 0;
                    if (decRemain < 0)
                    {
                        decRemain = decRemain + decReplenish;
                        decReplenish = decRemain >= 0 ? decRemain : 0;
                    }
                    if (decRemain < 0)
                    {
                        decReplenish = 0;
                        decRemain = -decRemain;
                        decimal decReal = decRemain;
                        var newCPO = new MTR_CPO
                        {
                            ProductID = product.ProductID,
                            StockUMID = product.StockUMID,
                            //12-08-2006 dungla edit for thuypt request: due date must substract sale atp
                            DueDate =
                                product.LTSalesATP.HasValue
                                    ? dtmDueDate.AddDays(-(double)product.LTSalesATP)
                                    : dtmDueDate,
                            MasterLocationID = masterLocId,
                            CCNID = cycleMaster.CCNID
                        };

                        if (product.ScrapPercent != null)
                        {
                            decRemain = decRemain * 100 / (100 - (decimal)product.ScrapPercent.Value);
                        }
                        if (product.LotSize.HasValue && product.LotSize.Value > 0)
                        {
                            int lotSize = product.LotSize.Value;
                            decRemain = Math.Max(decRemain, lotSize);
                            if (decRemain <= lotSize)
                            {
                                decRemain = lotSize;
                            }
                            else
                            {
                                decimal intMulti = decimal.Floor(decRemain / lotSize);
                                if (intMulti * lotSize < decRemain)
                                {
                                    decRemain = (intMulti + 1) * lotSize;
                                }
                            }
                        }
                        if (decRemain != decimal.Truncate(decRemain))
                        {
                            decRemain = decimal.Truncate(decRemain) + 1;
                        }

                        newCPO.Quantity = decRemain;
                        _cpoList.Add(newCPO);
                        decAvailable = product.ScrapPercent != null
                                           ? decRemain * (100 - (decimal)product.ScrapPercent.Value) / 100 -
                                             decReal
                                           : decRemain - decReal;
                    }
                }
            }

            #endregion

            return beginNiguri;
		}

	    private static List<AvalableQuantity> GetAvailableQuantityPlan(PCSDataContext dataContext, DateTime dtmBeginMonth)
	    {
            dtmBeginMonth = dtmBeginMonth.AddMonths(-1);
	        var query = from balanceBin in dataContext.IV_BalanceBins
	                    join bin in dataContext.MST_BINs on balanceBin.BinID equals bin.BinID
	                    where bin.BinTypeID.GetValueOrDefault(0) != (int) BinTypeEnum.LS
	                          && bin.BinTypeID.GetValueOrDefault(0) != (int) BinTypeEnum.NG
	                          && balanceBin.EffectDate == dtmBeginMonth
	                    group balanceBin by new {balanceBin.LocationID, balanceBin.ProductID}
	                    into g
	                    select new AvalableQuantity
	                               {
	                                   LocationId = g.Key.LocationID,
	                                   ProductId = g.Key.ProductID,
	                                   SupplyQuantity = g.Sum(p => p.OHQuantity)
	                               };
	        return query.ToList();
	    }
        
        private static List<ReleasedPurchaseOrder> GetAllReleasePOForItems(PCSDataContext dataContext, List<ITM_Product> items, DateTime fromDate, DateTime toDate, int masterLocationId)
        {
            var query = from poDetail in dataContext.PO_PurchaseOrderDetails
                    join delivery in dataContext.PO_DeliverySchedules on poDetail.PurchaseOrderDetailID equals
                        delivery.PurchaseOrderDetailID
                    where poDetail.ApproverID.HasValue && !delivery.CancelDelivery.GetValueOrDefault(false)
                          && poDetail.PO_PurchaseOrderMaster.ShipToLocID == masterLocationId
                          && delivery.ScheduleDate >= fromDate
                          && delivery.ScheduleDate < toDate
                    select
                        new ReleasedPurchaseOrder
                        {
                            ProductId = poDetail.ProductID.Value,
                            ScheduleDate = delivery.ScheduleDate,
                            DeliveryQuantity = delivery.DeliveryQuantity
                        };
            var resultList = new List<ReleasedPurchaseOrder>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }

        private static List<PurchaseOrder> GetPONotReceipt(PCSDataContext dataContext, IEnumerable<ITM_Product> items, DateTime fromDate, DateTime toDate)
        {
            var query = from delivery in dataContext.PO_DeliverySchedules
                        join poDetail in dataContext.PO_PurchaseOrderDetails on delivery.PurchaseOrderDetailID
                            equals
                            poDetail.PurchaseOrderDetailID
                        join master in dataContext.PO_PurchaseOrderMasters on poDetail.PurchaseOrderMasterID equals
                            master.PurchaseOrderMasterID
                        where poDetail.ApproverID.GetValueOrDefault(0) > 0
                              && delivery.ScheduleDate >= fromDate
                              && delivery.ScheduleDate < toDate
                        group delivery by new {master.ShipToLocID, poDetail.ProductID}
                        into g
                        select new PurchaseOrder
                                   {
                                       MasterLocationId = g.Key.ShipToLocID.GetValueOrDefault(0),
                                       ProductId = g.Key.ProductID.GetValueOrDefault(0),
                                       Quantity = g.Sum(p => p.DeliveryQuantity)
                                   };
            var resultList = new List<PurchaseOrder>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }

        private static List<PurchaseOrder> GetPOReceipt(PCSDataContext dataContext, IEnumerable<ITM_Product> items, DateTime fromDate, DateTime toDate)
        {
            var query = from detail in dataContext.PO_PurchaseOrderReceiptDetails
                        join master in dataContext.PO_PurchaseOrderReceiptMasters on detail.PurchaseOrderReceiptID
                            equals
                            master.PurchaseOrderReceiptID
                        where master.PostDate >= fromDate
                              && master.PostDate < toDate
                        group detail by new {master.MasterLocationID, detail.ProductID}
                        into g
                        select new PurchaseOrder
                                   {
                                       MasterLocationId = g.Key.MasterLocationID,
                                       ProductId = g.Key.ProductID.GetValueOrDefault(0),
                                       Quantity = g.Sum(p => p.ReceiveQuantity)
                                   };
            var resultList = new List<PurchaseOrder>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }

        private static List<PurchaseOrder> GetReturnToVendor(PCSDataContext dataContext, IEnumerable<ITM_Product> items, DateTime fromDate, DateTime toDate)
        {
            var query = from detail in dataContext.PO_ReturnToVendorDetails
                    join master in dataContext.PO_ReturnToVendorMasters on detail.ReturnToVendorMasterID equals
                        master.ReturnToVendorMasterID
                    where master.PostDate >= fromDate
                          && master.PostDate < toDate
                    group detail by new { master.MasterLocationID, detail.ProductID }
                        into g
                        select new PurchaseOrder
                        {
                            MasterLocationId = g.Key.MasterLocationID,
                            ProductId = g.Key.ProductID.GetValueOrDefault(0),
                            Quantity = g.Sum(p => p.Quantity)
                        };
            var resultList = new List<PurchaseOrder>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }
        
        private static List<SupplyPurchaseOrder> GetSupplyPurchaseOrder(PCSDataContext dataContext, IEnumerable<ITM_Product> items, DateTime fromDate, DateTime toDate, int masterLocationId, int ccnId)
        {
            var query = from poDetail in dataContext.PO_PurchaseOrderDetails
                        join delivery in dataContext.PO_DeliverySchedules on poDetail.PurchaseOrderDetailID equals
                            delivery.PurchaseOrderDetailID
                        join master in dataContext.PO_PurchaseOrderMasters on poDetail.PurchaseOrderMasterID equals
                            master.PurchaseOrderMasterID
                        // exclude Local PO
                        join party in dataContext.MST_Parties on master.MakerID equals party.PartyID
                        where
                            poDetail.TotalDelivery.GetValueOrDefault(0) <=
                            poDetail.OrderQuantity.GetValueOrDefault(0)
                            && poDetail.ApproverID.HasValue && !delivery.CancelDelivery.GetValueOrDefault(false)
                            && poDetail.PO_PurchaseOrderMaster.MasterLocationID == masterLocationId
                            && delivery.ScheduleDate >= fromDate
                            && delivery.ScheduleDate < toDate
                            &&
                            !dataContext.MST_CCNs.Where(c => c.CCNID == ccnId && c.CountryID.HasValue).Select(
                                c => c.CountryID).Contains(party.CountryID)
                        orderby delivery.ScheduleDate
                        select
                            new SupplyPurchaseOrder
                                {
                                    ProductId = poDetail.ProductID.Value,
                                    ScheduleDate = delivery.ScheduleDate,
                                    DeliveryQuantity = delivery.DeliveryQuantity
                                };
            var resultList = new List<SupplyPurchaseOrder>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }

        private static List<SO_DeliverySchedule> GetAllReleaseSOForItems(PCSDataContext dataContext, List<ITM_Product> items, DateTime fromDate, DateTime toDate, int masterLocationId)
        {
            var query = from delivery in dataContext.SO_DeliverySchedules
                        join detail in dataContext.SO_SaleOrderDetails on delivery.SaleOrderDetailID equals
                            detail.SaleOrderDetailID
                        where delivery.ScheduleDate < toDate
                              && delivery.ScheduleDate >= fromDate
                              && detail.SO_SaleOrderMaster.ShipFromLocID.GetValueOrDefault(0) == masterLocationId
                        select new {delivery, detail.ProductID};
            var resultList = new List<SO_DeliverySchedule>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductID == item.ProductID).Select(q => q.delivery));
            }
            return resultList;
        }

        private static List<ReleaseSaleOrder> GetAllReleaseSOForItemsInCycle(PCSDataContext dataContext, IEnumerable<ITM_Product> items, DateTime fromDate, DateTime toDate, int masterLocationId)
        {
            var query = from delivery in dataContext.SO_DeliverySchedules
                        join detail in dataContext.SO_SaleOrderDetails on delivery.SaleOrderDetailID equals
                            detail.SaleOrderDetailID
                        join master in dataContext.SO_SaleOrderMasters on detail.SaleOrderMasterID equals
                            master.SaleOrderMasterID
                        where delivery.ScheduleDate < toDate
                              && delivery.ScheduleDate >= fromDate
                              && detail.SO_SaleOrderMaster.ShipFromLocID.GetValueOrDefault(0) == masterLocationId
                        join commit in dataContext.SO_CommitInventoryDetails on delivery.DeliveryScheduleID equals
                            commit.DeliveryScheduleID
                            into gj
                        from commitDetail in gj.DefaultIfEmpty()
                        select new ReleaseSaleOrder
                                   {
                                       Code = master.Code,
                                       DueDate = (DateTime) delivery.ScheduleDate,
                                       ProductId = detail.ProductID,
                                       Quantity =
                                           commitDetail == null
                                               ? (decimal) delivery.DeliveryQuantity
                                               : (decimal) delivery.DeliveryQuantity - commitDetail.CommitQuantity,
                                       SaleOrderDetailId = detail.SaleOrderDetailID,
                                       SaleOrderLine = detail.SaleOrderLine,
                                       SaleOrderMasterId = master.SaleOrderMasterID,
                                       SellingUMId = detail.SellingUMID,
                                       StockUMId = detail.StockUMID,
                                       TransDate = (DateTime) master.TransDate,
                                       UnitPrice = detail.UnitPrice
                                   };
            var resultList = new List<ReleaseSaleOrder>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }

        private static List<CPODetail> GetAllCPOForItems(PCSDataContext dataContext, IEnumerable<ITM_Product> items, DateTime fromDate, DateTime toDate, int dcOptionMasterID)
        {
            var query = from detail in dataContext.PRO_DCPResultDetails
                    join master in dataContext.PRO_DCPResultMasters on detail.DCPResultMasterID equals
                        master.DCPResultMasterID
                    join bom in dataContext.ITM_BOMs on master.ProductID equals bom.ProductID
                    join product in dataContext.ITM_Products on bom.ComponentID equals product.ProductID
                    where detail.StartTime < toDate
                          && detail.StartTime >= fromDate
                          && master.DCOptionMasterID == dcOptionMasterID
                    select
                        new CPODetail
                        {
                            ProductId = bom.ComponentID,
                            DueDate = detail.EndTime,
                            StartTime = detail.StartTime,
                            LeadTimeOffset = bom.LeadTimeOffset,
                            Quantity = detail.Quantity * bom.Quantity.GetValueOrDefault(0),
                            Shrink = bom.Shrink,
                            StockUmId = product.StockUMID
                        };
            var resultList = new List<CPODetail>();
            foreach (var item in items)
            {
                resultList.AddRange(query.Where(q => q.ProductId == item.ProductID));
            }
            return resultList;
        }

	    public void GenMRPPlanOffLine(int ccnId, int cycleMasterId, string vendorId, string categoryId, string model, string producId)
		{
            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
		    {
		        using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
		        {
		            dataContext.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
		            //Get CycleOptionMaster
		            var cycleMaster = dataContext.MTR_MRPCycleOptionMasters.FirstOrDefault(c => c.MRPCycleOptionMasterID == cycleMasterId);
                    var dcpCycleMaster = dataContext.PRO_DCOptionMasters.FirstOrDefault(c => c.AsOfDate == cycleMaster.AsOfDate);
                    // get dcp cycle of previous month
		            var dcpPreCycleMaster = dataContext.PRO_DCOptionMasters.FirstOrDefault(c => c.AsOfDate == cycleMaster.AsOfDate.AddMonths(-1));

		            var workingDays = dataContext.MST_WorkingDayMasters.ToList();
		            var holidays = dataContext.MST_WorkingDayDetails.ToList();

                    if (!CheckCalendarConfig(cycleMaster, workingDays))
                    {
                        throw new PCSBOException(ErrorCode.MESSAGE_DCP_CONFIG_CALENDAR_FROM_X_TO_Y, string.Empty, null);
                    }

		            //Get CycleOptionDetail
		            var cycleDetails = cycleMaster.MTR_MRPCycleOptionDetails;

                    //Get All MRP Non Make Item
		            var items = from product in dataContext.ITM_Products.ToList()
		                        where product.PlanType == (byte) PlanTypeEnum.MRP
		                              && !product.MakeItem
		                        select product;
                    List<ITM_Product> itemList = new List<ITM_Product>();
                    itemList.AddRange(items);
		            List<int> vendorList = new List<int>();
		            if (!string.IsNullOrEmpty(vendorId))
		            {
                        vendorList = new List<string>(vendorId.Split(',')).ConvertAll(Convert.ToInt32);
		                items = from item in items
		                        where vendorList.Contains(item.PrimaryVendorID.GetValueOrDefault(0))
		                        select item;
                        itemList.Clear();
                        itemList.AddRange(items);
		                /*foreach (var id in vendorList)
		                {
                            itemList.AddRange(items.Where(i => i.PrimaryVendorID.GetValueOrDefault(0) == id));
		                }*/
		            }
		            if (!string.IsNullOrEmpty(categoryId))
		            {
                        var categoryList = new List<string>(categoryId.Split(',')).ConvertAll(Convert.ToInt32);
                        items = from item in items
                                where categoryList.Contains(item.CategoryID.GetValueOrDefault(0))
                                select item;
                        itemList.Clear();
                        itemList.AddRange(items);
                        /*foreach (var id in categoryList)
                        {
                            itemList.AddRange(items.Where(i => i.CategoryID.GetValueOrDefault(0) == id));
                        }*/
		            }
		            if (!string.IsNullOrEmpty(model))
		            {
		                var modelList = new List<string>(model.Split(','));
                        items = from item in items
                                where modelList.Contains(item.Revision)
                                select item;
                        itemList.Clear();
                        /*foreach (var id in modelList)
                        {
                            itemList.AddRange(items.Where(i => i.Revision == id));
                        }*/
		            }
                    List<int> productIdList = new List<int>();
		            if (!string.IsNullOrEmpty(producId))
		            {
                        productIdList = new List<string>(producId.Split(',')).ConvertAll(Convert.ToInt32);
                        items = from item in items
                                where productIdList.Contains(item.ProductID)
                                select item;
                        itemList.Clear();
		                itemList.AddRange(items);
                        /*foreach (var id in productIdList)
                        {
                            itemList.AddRange(items.Where(i => i.ProductID == id));
                        }*/
		            }

		            var notReceiptPO = new List<PurchaseOrder>();
		            var receiptPO = new List<PurchaseOrder>();
		            var returnToVendor = new List<PurchaseOrder>();
		            DateTime dtmServerDate = dataContext.GetServerDate();

		            if (cycleMaster.IncludedRemainPO.GetValueOrDefault(false))
		            {
		                // change request: begin of current month minus day before
		                var dtmBeginMonth = new DateTime(dtmServerDate.Year, dtmServerDate.Month, 1);
		                DateTime dtmFromDate = dtmBeginMonth.AddDays(-cycleMaster.DaysBeforeAsOfDate.GetValueOrDefault(0));
                        notReceiptPO = GetPONotReceipt(dataContext, itemList, dtmFromDate, dtmBeginMonth);
                        receiptPO = GetPOReceipt(dataContext, itemList, dtmFromDate, dtmBeginMonth);
		                if (cycleMaster.IncludedReturnToVendor.GetValueOrDefault(false))
		                {
                            returnToVendor = GetReturnToVendor(dataContext, itemList, dtmFromDate, dtmBeginMonth);
		                }
		            }

		            //Declare the dataset for all information (Scan all MasLoc)
		            foreach (var cycleDetail in cycleDetails)
		            {
                        var beginNiguri = GenerateMrp(dataContext, itemList, cycleDetail, cycleDetail.MasterLocationID,
                            cycleMaster, dcpCycleMaster, dcpPreCycleMaster,
                            notReceiptPO, receiptPO, returnToVendor, dtmServerDate);

		                DateTime dtmToDate = GetLimitPlaningCycle(cycleMaster.AsOfDate, cycleMaster.PlanHorizon.GetValueOrDefault(0));
		                if (_cpoList.Count == 0)
		                {
                            UpdateBegin(dataContext, beginNiguri, cycleMasterId, cycleMaster.AsOfDate, dtmToDate, ccnId, itemList, dtmServerDate);
		                    return;
		                }
		                var realCPOList = new List<MTR_CPO>();
                        foreach (var item in itemList)
		                {
		                    ITM_Product product = item;
		                    var policyItem = product.ITM_OrderPolicy;
		                    if (policyItem != null)
		                    {
		                        if (!string.IsNullOrEmpty(policyItem.Code))
		                        {
		                            var policy = (OrderPolicyEnum)Enum.Parse(typeof(OrderPolicyEnum), policyItem.Code, true);
		                            switch (policy)
		                            {
		                                case OrderPolicyEnum.Daily:
		                                case OrderPolicyEnum.Monthly:
		                                case OrderPolicyEnum.Quarterly:
		                                case OrderPolicyEnum.Weekly:
		                                case OrderPolicyEnum.Yearly:
		                                    GroupByOrderPolicys(cycleMasterId, product, realCPOList, _cpoList, policy, workingDays, holidays);
		                                    break;
		                                default:
		                                    break;
		                            }
		                        }
		                        else
		                        {
		                            var drowAllItemCPO = _cpoList.Where(p => p.ProductID == product.ProductID).OrderBy(p => p.DueDate).ToList();
		                            var intPolicyDays = (int)decimal.Floor(policyItem.OrderPolicyDays.GetValueOrDefault(0));
		                            if (intPolicyDays >= 1)
		                            {
		                                int intResult;
		                                var dtmMaxDate = (DateTime)drowAllItemCPO.Last().DueDate;
		                                int intN = Math.DivRem(dtmMaxDate.Subtract(cycleMaster.AsOfDate).Days, intPolicyDays, out intResult);
		                                if (intN * intPolicyDays < dtmMaxDate.Subtract(cycleMaster.AsOfDate).Days) intN++;
		                                for (int i = 0; i < intN; i++)
		                                {
		                                    decimal decQuantity = 0;
		                                    var dtmDueDate = new DateTime();
		                                    int intFirst = 0;
		                                    for (int j = 0; j < drowAllItemCPO.Count(); j++)
		                                    {
		                                        if ((DateTime)drowAllItemCPO[j].DueDate >= cycleMaster.AsOfDate.AddDays(intPolicyDays * i)
		                                            && (DateTime)drowAllItemCPO[j].DueDate <= cycleMaster.AsOfDate.AddDays(intPolicyDays * (i + 1)))
		                                        {
		                                            decQuantity += (decimal)drowAllItemCPO[j].Quantity;
		                                            if (intFirst == 0)
		                                            {
		                                                dtmDueDate = (DateTime)drowAllItemCPO[j].DueDate;
		                                            }
		                                            intFirst++;
		                                        }
		                                        if ((DateTime)drowAllItemCPO[j].DueDate > cycleMaster.AsOfDate.AddDays(intPolicyDays * (i + 1)))
		                                        {
		                                            break;
		                                        }
		                                    }
		                                    if (decQuantity > 0)
		                                    {
		                                        var drowNewCPO = new MTR_CPO
		                                                             {
		                                                                 ProductID = product.ProductID,
		                                                                 StockUMID = product.StockUMID,
		                                                                 CCNID = drowAllItemCPO.First().CCNID,
		                                                                 MasterLocationID = drowAllItemCPO.First().MasterLocationID,
		                                                                 Quantity = decQuantity,
		                                                                 DueDate = dtmDueDate.AddDays(-(double)product.LTSalesATP.GetValueOrDefault(0)),
		                                                                 StartDate = GetStartDateForCPO(product, workingDays, holidays, dtmDueDate, decQuantity),
		                                                                 IsMPS = false,
		                                                                 MRPCycleOptionMasterID = cycleMasterId
		                                                             };
		                                        realCPOList.Add(drowNewCPO);
		                                    }
		                                }
		                            }
		                            else
		                            {
		                                foreach (var drowCPO in drowAllItemCPO)
		                                {
		                                    drowCPO.StartDate = GetStartDateForCPO(product, workingDays, holidays, (DateTime)drowCPO.DueDate, (decimal)drowCPO.Quantity);
		                                    drowCPO.IsMPS = false;
		                                    drowCPO.MRPCycleOptionMasterID = cycleMasterId;
		                                    realCPOList.Add(drowCPO);
		                                }
		                            }
		                        }
		                    }
		                    else
		                    {
		                        var allCpo = _cpoList.Where(p => p.ProductID == product.ProductID).OrderBy(p => p.DueDate);
		                        foreach (var drowCPO in allCpo)
		                        {
		                            drowCPO.StartDate = GetStartDateForCPO(product, workingDays, holidays, (DateTime)drowCPO.DueDate, (decimal)drowCPO.Quantity);
		                            drowCPO.IsMPS = false;
		                            drowCPO.MRPCycleOptionMasterID = cycleMasterId;
		                            realCPOList.Add(drowCPO);
		                        }
		                    }
		                }

		                UpdateCPODataset(dataContext, beginNiguri, realCPOList, cycleMasterId, cycleMaster.AsOfDate, dtmToDate, ccnId, itemList, dtmServerDate);
		            }
                    trans.Complete();
		        }
		    }
		}

        private static void UpdateCPODataset(PCSDataContext dataContext, ICollection<IV_BeginMRP> beginNiguri, List<MTR_CPO> listCpo, int cycleMasterId, DateTime pdtmFromDate, DateTime pdtmToDate, int ccnId, IEnumerable<ITM_Product> items, DateTime serverDate)
		{
            const string methodName = THIS + ".UpdateCPODataset()";
            try
            {
                var master = dataContext.MTR_MRPCycleOptionMasters.SingleOrDefault(
                            o => o.MRPCycleOptionMasterID == cycleMasterId);
                if (master != null)
                {
                    master.MPSGenDate = serverDate;
                }
                string itemList = string.Join(",", items.Select(p => p.ProductID.ToString()).ToArray());
                var sql = string.Format("DELETE MTR_CPO WHERE CCNID = {0} AND MRPCycleOptionMasterID = {1} AND ProductID IN ({2})",
                                         ccnId, cycleMasterId, itemList);
                dataContext.ExecuteCommand(sql);
                var tableCpo = CollectionHelper.ConvertTo(listCpo);
                dataContext.MTR_CPOs.BulkCopy(tableCpo);
                if (beginNiguri.Count > 0)
                {
                    // delete old records first
                    var deleteSql = string.Format("DELETE IV_BeginMRP WHERE AsOfDate = '{0}'", master.AsOfDate.ToString("yyyy-MM-dd"));
                    dataContext.ExecuteCommand(deleteSql);
                    dataContext.IV_BeginMRPs.BulkCopy(beginNiguri);
                }

                // close local PO in cycle
                var poDetails = from detail in dataContext.PO_PurchaseOrderDetails
                                join poMaster in dataContext.PO_PurchaseOrderMasters on detail.PurchaseOrderMasterID
                                    equals poMaster.PurchaseOrderMasterID
                                join party in dataContext.MST_Parties on poMaster.MakerID equals party.PartyID
                                where party.CountryID == dataContext.MST_CCNs.SingleOrDefault(c => c.CCNID == ccnId).CountryID
                                select detail;
                var resultList = new List<PO_PurchaseOrderDetail>();
                foreach (var item in items)
                {
                    resultList.AddRange(poDetails.Where(q => q.ProductID == item.ProductID));
                }
                var poList = resultList.Select(p => p.PurchaseOrderDetailID.ToString());
                if (poList.Count() > 0)
                {
                    var sqlUpdate = "UPDATE PO_DeliverySchedule"
                              + " SET CancelDelivery = 1"
                              + " WHERE ScheduleDate >= '{0}'"
                              + " AND ScheduleDate <= '{1}'"
                              + " AND PurchaseOrderDetailID IN({2})";
                    var detailList = string.Join(",", poList.ToArray());
                    sqlUpdate = string.Format(sqlUpdate, pdtmFromDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                        pdtmToDate.ToString("yyyy-MM-dd HH:mm:ss"), detailList);
                    dataContext.ExecuteCommand(sqlUpdate);
                }

                // submit changes
                dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
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
		
		private static void UpdateBegin(PCSDataContext dataContext, ICollection<IV_BeginMRP> beginNiguri, int cycleMasterId, DateTime pdtmFromDate, DateTime pdtmToDate, int ccnId, IEnumerable<ITM_Product> items, DateTime serverDate)
		{
            const string methodName = THIS + ".UpdateBegin()";
            try
            {
                var master = dataContext.MTR_MRPCycleOptionMasters.SingleOrDefault(
                                o => o.MRPCycleOptionMasterID == cycleMasterId);
                if (master != null)
                    master.MPSGenDate = serverDate;

                if (beginNiguri.Count > 0)
                {
                    // delete old records first
                    var deleteSql = string.Format("DELETE IV_BeginMRP WHERE AsOfDate = '{0}'",
                                                  master.AsOfDate.ToString("yyyy-MM-dd"));
                    dataContext.ExecuteCommand(deleteSql);
                    dataContext.IV_BeginMRPs.BulkCopy(beginNiguri);
                }
                // close local PO in cycle
                var poDetails = from detail in dataContext.PO_PurchaseOrderDetails
                                join poMaster in dataContext.PO_PurchaseOrderMasters on detail.PurchaseOrderMasterID
                                    equals poMaster.PurchaseOrderMasterID
                                join party in dataContext.MST_Parties on poMaster.MakerID equals party.PartyID
                                where
                                    party.CountryID ==
                                    dataContext.MST_CCNs.SingleOrDefault(c => c.CCNID == ccnId).CountryID
                                select detail;
                var resultList = new List<PO_PurchaseOrderDetail>();
                foreach (var item in items)
                {
                    resultList.AddRange(poDetails.Where(q => q.ProductID == item.ProductID));
                }
                var poList = resultList.Select(p => p.PurchaseOrderDetailID.ToString());
                if (poDetails.Count() > 0)
                {
                    var sql = "UPDATE PO_DeliverySchedule"
                              + " SET CancelDelivery = 1"
                              + " WHERE ScheduleDate >= '{0}'"
                              + " AND ScheduleDate <= '{1}'"
                              + " AND PurchaseOrderDetailID IN({2})";
                    var detailList = string.Join(",", poList.ToArray());
                    sql = string.Format(sql, pdtmFromDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                        pdtmToDate.ToString("yyyy-MM-dd HH:mm:ss"), detailList);
                    dataContext.ExecuteCommand(sql);
                }

                // submit changes
                dataContext.SubmitChanges();
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
		
		/// <summary>
		/// Check if user is setting calendar yet
		/// </summary>
        private static bool CheckCalendarConfig(MTR_MRPCycleOptionMaster cycleMaster, List<MST_WorkingDayMaster> workingDays)
		{
            DateTime dtmEndCycle = cycleMaster.AsOfDate.AddDays((double) cycleMaster.PlanHorizon);
            for (int i = cycleMaster.AsOfDate.Year; i <= dtmEndCycle.Year; i++)
            {
                if (workingDays.Count(d => d.Year == i) == 0)
                    return false;
            }
            return true;
		}

		private static DateTime ConvertWorkingDayOffLine(IEnumerable<MST_WorkingDayMaster> allWorksDay, IEnumerable<MST_WorkingDayDetail> holidays, DateTime pdtmDate, decimal pdecNumberOfDay, ScheduleMethodEnum penuSchedule)
		{
            DateTime dtmConvert = pdtmDate;
            var arrDayOfWeek = new ArrayList();

            var intNumberOfDay = (int)decimal.Floor(pdecNumberOfDay);
            var dblRemainder = (double)(pdecNumberOfDay - intNumberOfDay);

            if (allWorksDay != null)
            {
                var workingDay = allWorksDay.SingleOrDefault(w => w.Year == pdtmDate.Year);
                if (workingDay != null)
                {
                    if (!workingDay.Mon)
                        arrDayOfWeek.Add(DayOfWeek.Monday);
                    if (!workingDay.Tue)
                        arrDayOfWeek.Add(DayOfWeek.Tuesday);
                    if (!workingDay.Wed)
                        arrDayOfWeek.Add(DayOfWeek.Wednesday);
                    if (!workingDay.Thu)
                        arrDayOfWeek.Add(DayOfWeek.Thursday);
                    if (!workingDay.Fri)
                        arrDayOfWeek.Add(DayOfWeek.Friday);
                    if (!workingDay.Sat)
                        arrDayOfWeek.Add(DayOfWeek.Saturday);
                    if (!workingDay.Sun)
                        arrDayOfWeek.Add(DayOfWeek.Sunday);
                }
            }

            var arrHolidays = new ArrayList();
            if (holidays != null)
            {
                var allHoliday = holidays.Where(h => h.MST_WorkingDayMaster.Year == pdtmDate.Year);
                if (allHoliday.Count() != 0)
                {
                    //have data--> create new array list to add items
                    foreach (var holiday in allHoliday)
                    {
                        DateTime dtmTemp = holiday.OffDay;
                        //truncate hour, minute, second
                        dtmTemp = new DateTime(dtmTemp.Year, dtmTemp.Month, dtmTemp.Day);
                        arrHolidays.Add(dtmTemp);
                    }
                }
            }

            switch (penuSchedule)
            {
                case ScheduleMethodEnum.Forward:
                    #region Forward
                    for (int i = 0; i < intNumberOfDay; i++)
                    {
                        //Add day
                        dtmConvert = dtmConvert.AddDays(1);
                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            dtmConvert = dtmConvert.AddDays(1);
                        //goto next day if the day is off day
                        while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                        {
                            dtmConvert = dtmConvert.AddDays(1);
                            if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                                dtmConvert = dtmConvert.AddDays(1);
                        }

                        //goto next day if the day is holidayday
                        while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            dtmConvert = dtmConvert.AddDays(1);
                    }
                    //Add remainder
                    dtmConvert = dtmConvert.AddDays(dblRemainder);

                    //goto next day if the day is holidayday
                    while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        dtmConvert = dtmConvert.AddDays(1);

                    //goto next day if the day is off day
                    while (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                    {
                        dtmConvert = dtmConvert.AddDays(1);
                        if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            dtmConvert = dtmConvert.AddDays(1);
                    }

                    //goto next day if the day is holidayday
                    while (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                        dtmConvert = dtmConvert.AddDays(1);
                    #endregion
                    break;
                case ScheduleMethodEnum.Backward:
                    #region Backward

                    dtmConvert = dtmConvert.AddDays(-intNumberOfDay);
                    bool isOk = false;
                    while (!isOk)
                    {
                        DateTime dtmOld = dtmConvert;
                        if (arrHolidays.Contains(new DateTime(dtmConvert.Year, dtmConvert.Month, dtmConvert.Day)))
                            dtmConvert = dtmConvert.AddDays(-1);
                        if (arrDayOfWeek.Contains(dtmConvert.DayOfWeek))
                            dtmConvert = dtmConvert.AddDays(-1);
                        if (dtmOld.Equals(dtmConvert)) isOk = true;
                    }

                    #endregion
                    break;
            }

            return dtmConvert;
		}

        private static void GroupByOrderPolicys(int cycleMasterId, ITM_Product drowItem, List<MTR_CPO> realCPOList, IEnumerable<MTR_CPO> pdstInputCPO, OrderPolicyEnum penumOrderPolicy, List<MST_WorkingDayMaster> workingDays, List<MST_WorkingDayDetail> holidays)
		{
            GroupOrder(cycleMasterId, drowItem, realCPOList, pdstInputCPO, workingDays, holidays, penumOrderPolicy);
		}

	    private static void GroupOrder(int cycleMasterId, ITM_Product item, ICollection<MTR_CPO> realCPOList, IEnumerable<MTR_CPO> inputCPO, List<MST_WorkingDayMaster> workingDays, List<MST_WorkingDayDetail> holidays, OrderPolicyEnum policy)
	    {
	        var listCPO = inputCPO.Where(p => p.ProductID == item.ProductID).OrderBy(p => p.DueDate);
            if (listCPO.Count() > 0)
	        {
	            DateTime dtmDueDate;
                DateTime dtmNextStep;
	            switch (policy)
	            {
	                case OrderPolicyEnum.Daily:
	                    dtmDueDate = (DateTime)listCPO.Last().DueDate;
	                    var firstDueDate = (DateTime)listCPO.First().DueDate;
	                    dtmNextStep = new DateTime(firstDueDate.Year, firstDueDate.Month, firstDueDate.Day, 0, 0, 0);
	                    break;
                    case OrderPolicyEnum.Weekly:
	                    dtmDueDate = GetDueDateInOneWeek((DateTime) listCPO.First().DueDate);
                        dtmNextStep = GetStartDateInOneWeek((DateTime)listCPO.First().DueDate);
	                    break;
	                case OrderPolicyEnum.Monthly:
                        dtmDueDate = GetDueDateInOneMonth((DateTime) listCPO.First().DueDate);
                        dtmNextStep = GetStartDateInOneMonth((DateTime)listCPO.First().DueDate);
	                    break;
	                case OrderPolicyEnum.Quarterly:
                        dtmDueDate = GetDueDateInOneQuarter((DateTime) listCPO.First().DueDate);
                        dtmNextStep = GetStartDateInOneQuarter((DateTime)listCPO.First().DueDate);
	                    break;
	                default:
                        dtmDueDate = GetDueDateInOneYear((DateTime) listCPO.First().DueDate);
                        dtmNextStep = GetStartDateInOneYear((DateTime)listCPO.First().DueDate);
	                    break;
	            }
	            var dtmCPODueDate = new DateTime();
	            while (dtmNextStep <= (DateTime) listCPO.Last().DueDate)
	            {
	                decimal decQuantity = 0;
	                int intFirst = 0;
	                foreach (var drowCPO in listCPO)
	                {
	                    if (dtmNextStep <= (DateTime) drowCPO.DueDate && (DateTime)drowCPO.DueDate <= dtmDueDate)
	                    {
	                        decQuantity += (decimal) drowCPO.Quantity;
	                        if (intFirst == 0)
	                        {
	                            dtmCPODueDate = (DateTime) drowCPO.DueDate;
	                        }
	                        intFirst++;
	                    }
	                    if ((DateTime) drowCPO.DueDate > dtmDueDate) 
	                    {
	                        break;
	                    }
	                }
	                if (decQuantity > 0)
	                {
	                    var drowNewCPO = new MTR_CPO
	                                         {
	                                             ProductID = item.ProductID,
	                                             StockUMID = item.StockUMID,
	                                             CCNID = listCPO.First().CCNID,
	                                             MasterLocationID = listCPO.First().MasterLocationID,
	                                             Quantity = decQuantity,
	                                             DueDate = dtmCPODueDate,
	                                             StartDate = GetStartDateForCPO(item, workingDays, holidays, dtmCPODueDate, decQuantity),
	                                             IsMPS = false,
                                                 MRPCycleOptionMasterID = cycleMasterId
	                                         };
                        realCPOList.Add(drowNewCPO);
	                }
	                dtmNextStep = dtmDueDate.AddDays(1);
	                switch (policy)
	                {
                        case OrderPolicyEnum.Weekly:
                            dtmDueDate = GetDueDateInOneWeek(dtmNextStep);
                            dtmNextStep = GetStartDateInOneWeek(dtmNextStep);
                            break;
                        case OrderPolicyEnum.Monthly:
                            dtmDueDate = GetDueDateInOneMonth(dtmNextStep);
                            dtmNextStep = GetStartDateInOneMonth(dtmNextStep);
	                        break;
                        case OrderPolicyEnum.Quarterly:
                            dtmDueDate = GetDueDateInOneQuarter(dtmNextStep);
                            dtmNextStep = GetStartDateInOneQuarter(dtmNextStep);
	                        break;
                        case OrderPolicyEnum.Yearly:
                            dtmDueDate = GetDueDateInOneYear(dtmNextStep);
                            dtmNextStep = GetStartDateInOneYear(dtmNextStep);
	                        break;
	                }
	            }
	        }
	    }

	    /// <summary>
		/// </summary>
		/// <param name="pdtmInDate"></param>
		/// <returns></returns>
		private static DateTime GetDueDateInOneWeek(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime(pdtmInDate.Year, pdtmInDate.Month, pdtmInDate.Day, 23, 59, 59, 99);
			while (dtmOutDate.DayOfWeek != DayOfWeek.Sunday)
			{
				dtmOutDate = dtmOutDate.AddDays(1);
			}
			return dtmOutDate;
		}

        private static DateTime GetStartDateInOneWeek(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime(pdtmInDate.Year, pdtmInDate.Month, pdtmInDate.Day, 0, 0, 0, 0);
			while (dtmOutDate.DayOfWeek != DayOfWeek.Monday)
			{
				dtmOutDate = dtmOutDate.AddDays(-1);
			}
			return dtmOutDate;
		}

        private static DateTime GetDueDateInOneMonth(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime(pdtmInDate.Year, pdtmInDate.Month, 1, 23, 59, 59, 99);

			if (pdtmInDate.Month == 1 || pdtmInDate.Month == 3 || pdtmInDate.Month == 5 || pdtmInDate.Month == 7
			|| pdtmInDate.Month == 8 || pdtmInDate.Month == 10 || pdtmInDate.Month == 12)
			{
				dtmOutDate = dtmOutDate.AddDays(30);
			}
			else if (pdtmInDate.Month == 2)
			{
				while (dtmOutDate.Month == dtmOutDate.AddDays(1).Month)
				{
					dtmOutDate = dtmOutDate.AddDays(1);
				}	
			}
			else dtmOutDate = dtmOutDate.AddDays(29);
			return dtmOutDate;
		}

        private static DateTime GetStartDateInOneMonth(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime(pdtmInDate.Year, pdtmInDate.Month, 1, 0, 0, 0, 0);
			return dtmOutDate;
		}

        private static DateTime GetDueDateInOneQuarter(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime();
				
			if (pdtmInDate.Month <= 3)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 3, 31, 23, 59, 59, 99);
			}
			else if (pdtmInDate.Month <= 6)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 6, 30, 23, 59, 59, 99);
			}
			else if (pdtmInDate.Month <= 9)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 9, 30, 23, 59, 59, 99);
			}
			else if (pdtmInDate.Month <= 12)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 12, 31, 23, 59, 59, 99);
			}
			return dtmOutDate;
		}

        private static DateTime GetStartDateInOneQuarter(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime();
				
			if (pdtmInDate.Month <= 3)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 1, 1, 0, 0, 0, 0);
			}
			else if (pdtmInDate.Month <= 6)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 4, 1, 0, 0, 0, 0);
			}
			else if (pdtmInDate.Month <= 9)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 7, 1, 0, 0, 0, 0);
			}
			else if (pdtmInDate.Month <= 12)
			{
				dtmOutDate = new DateTime(pdtmInDate.Year, 10, 1, 0, 0, 0, 0);
			}
			return dtmOutDate;
		}

        private static DateTime GetStartDateInOneYear(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime(pdtmInDate.Year, 1, 1, 0, 0, 0, 0);
			return dtmOutDate;
		}

        private static DateTime GetDueDateInOneYear(DateTime pdtmInDate)
		{
			var dtmOutDate = new DateTime(pdtmInDate.Year, 12, 31, 23, 59, 59, 99);
			return dtmOutDate;
		}

		private static DateTime GetStartDateForCPO(ITM_Product drowItem, List<MST_WorkingDayMaster> workingDays, List<MST_WorkingDayDetail> holidays, DateTime pdtmCPODueDate, decimal pdecQuantity)
		{
		    const decimal divider = 86400;
			decimal decFixedTime = drowItem.LTFixedTime.GetValueOrDefault(0);
		    decimal decVari = drowItem.LTVariableTime.GetValueOrDefault(0);
            decimal decDock = drowItem.LTDockToStock.GetValueOrDefault(0);
		    decimal decSafe = drowItem.LTSafetyStock.GetValueOrDefault(0);
		    decimal decShip = drowItem.LTShippingPrepare.GetValueOrDefault(0);
            decimal decRequis = drowItem.LTRequisition.GetValueOrDefault(0);
            return ConvertWorkingDayOffLine(workingDays, holidays, pdtmCPODueDate,
                                            Math.Abs(decFixedTime / divider + pdecQuantity * decVari / divider +
                                                     decDock / divider + decSafe / divider + decShip / divider +
                                                     decRequis / divider), ScheduleMethodEnum.Backward);
		}

		private static decimal GetAllRemainBeforeRun(int productId, IEnumerable<DemandMrp> allDemand, IEnumerable<ReleasedPurchaseOrder> itemReplenish)
		{
		    decimal replenishQty = itemReplenish.Where(p => p.ProductId == productId).Sum(p => p.DeliveryQuantity);
		    decimal demandQty = allDemand.Where(p => p.ProductId == productId).Sum(p => p.DemandQuantity);
		    return replenishQty - demandQty;
		}

		public DataTable GetPONotReceipt(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
		{
			MRPRegenerationProcessDS dsProcess = new MRPRegenerationProcessDS();
			return dsProcess.GetPONotReceipt(pdtmFromDate, pdtmToDate, pintCCNID);
		}
		public DataTable GetPOReceipt(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCCNID)
		{
			MRPRegenerationProcessDS dsProcess = new MRPRegenerationProcessDS();
			return dsProcess.GetPOReceipt(pdtmFromDate, pdtmToDate, pintCCNID);
		}
		public DataTable GetReturnToVendor(DateTime dtmFromDate, DateTime dtmToDate)
		{
			MRPRegenerationProcessDS dsProcess = new MRPRegenerationProcessDS();
			return dsProcess.GetReturnToVendor(dtmFromDate, dtmToDate);
		}
	}
}

 

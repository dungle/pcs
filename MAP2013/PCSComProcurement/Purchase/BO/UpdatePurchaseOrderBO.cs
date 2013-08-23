using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace PCSComProcurement.Purchase.BO
{
    public class UpdatePurchaseOrderBO
    {
        /// <summary>
        /// Adjusts the purchase orders.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="partyId"></param>
        public void AdjustPurchaseOrder(DateTime fromDate, DateTime toDate, int? partyId)
        {
            var methodName = GetType().FullName + ".UpdateCPODataset()";
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var serverDate = dataContext.GetServerDate();
                        var newDeliveries = new List<PO_DeliverySchedule>();
                        var newDetails = new List<PO_PurchaseOrderDetail>();
                        var updatedDetail = new List<int>();
                        // find all purchase orders in range was approved
                        var approvedPOs = (from orderDetail in dataContext.PO_PurchaseOrderDetails
                                           join orderMaster in dataContext.PO_PurchaseOrderMasters on
                                               orderDetail.PurchaseOrderMasterID equals
                                               orderMaster.PurchaseOrderMasterID
                                           join schedule in dataContext.PO_DeliverySchedules on
                                               orderDetail.PurchaseOrderDetailID equals schedule.PurchaseOrderDetailID
                                           where orderDetail.ApproverID.GetValueOrDefault(0) > 0
                                                 && schedule.ScheduleDate.CompareTo(fromDate) >= 0
                                                 && schedule.ScheduleDate.CompareTo(toDate) <= 0
                                                 && schedule.ReceivedQuantity.GetValueOrDefault(0) == 0
                                                 && partyId.GetValueOrDefault(0) > 0 ? orderMaster.PartyID == partyId.GetValueOrDefault(0) : orderMaster.PartyID > 0
                                           select new {
                                                          orderMaster.PartyID,
                                                          orderMaster.PurchaseOrderMasterID,
                                                          orderDetail.PurchaseOrderDetailID,
                                                          orderDetail.ProductID,
                                                          schedule.DeliveryScheduleID,
                                                          schedule.ScheduleDate,
                                                          schedule.DeliveryQuantity
                                                      }).ToList();
                        // find all un-approved purchase orders in range
                        var unApprovedPOs = (from orderDetail in dataContext.PO_PurchaseOrderDetails
                                            join orderMaster in dataContext.PO_PurchaseOrderMasters on
                                                orderDetail.PurchaseOrderMasterID equals orderMaster.PurchaseOrderMasterID
                                            join schedule in dataContext.PO_DeliverySchedules on orderDetail.PurchaseOrderDetailID equals schedule.PurchaseOrderDetailID 
                                            where orderDetail.ApproverID.GetValueOrDefault(0) == 0
                                                  && schedule.ScheduleDate.CompareTo(fromDate) >= 0
                                                  && schedule.ScheduleDate.CompareTo(toDate) <= 0
                                                  && partyId.GetValueOrDefault(0) > 0 ? orderMaster.PartyID == partyId.GetValueOrDefault(0) : orderMaster.PartyID > 0
                                            select new {
                                                          orderMaster.PartyID,
                                                          orderMaster.PurchaseOrderMasterID,
                                                          orderDetail.PurchaseOrderDetailID,
                                                          orderDetail.ProductID,
                                                          schedule.DeliveryScheduleID,
                                                          schedule.ScheduleDate,
                                                          schedule.DeliveryQuantity
                                                      }).ToList();

                        foreach (var approvedPO in approvedPOs)
                        {
                            #region find new delivery with same schedule date

                            var delivery = dataContext.PO_DeliverySchedules.FirstOrDefault(d => d.DeliveryScheduleID == approvedPO.DeliveryScheduleID);
                            var newDelivery = unApprovedPOs.FirstOrDefault(e =>
                                    e.PartyID == approvedPO.PartyID && e.ProductID == approvedPO.ProductID &&
                                    e.ScheduleDate.Equals(approvedPO.ScheduleDate));
                            if (newDelivery != null)
                            {
                                delivery.Adjustment = newDelivery.DeliveryQuantity - delivery.DeliveryQuantity;
                                delivery.DeliveryQuantity = newDelivery.DeliveryQuantity;
                            }
                            else
                            {
                                // old delivery does not exist in new schedules
                                delivery.Adjustment = -delivery.DeliveryQuantity;
                                delivery.DeliveryQuantity = 0;
                            }
                            if (!updatedDetail.Contains(delivery.PurchaseOrderDetailID))
                                updatedDetail.Add(delivery.PurchaseOrderDetailID);

                            #endregion
                        }

                        // new delivery schedule does not exist in current schedules
                        foreach (var unApprovedPO in unApprovedPOs)
                        {
                            var approvedSchedule = approvedPOs.FirstOrDefault(
                                    d => d.PartyID == unApprovedPO.PartyID && d.ProductID == unApprovedPO.ProductID &&
                                    d.ScheduleDate == unApprovedPO.ScheduleDate);
                            if (approvedSchedule == null)
                            {
                                var approvedPO = approvedPOs.FirstOrDefault(d => d.PartyID == unApprovedPO.PartyID && d.ProductID == unApprovedPO.ProductID);
                                if (approvedPO != null)
                                {
                                    // check in new local list if we insert it or not
                                    if (newDeliveries.Any(d => d.PO_PurchaseOrderDetail.ProductID == unApprovedPO.ProductID
                                        && d.PO_PurchaseOrderDetail.PO_PurchaseOrderMaster.PartyID == unApprovedPO.PartyID
                                        && d.ScheduleDate == unApprovedPO.ScheduleDate))
                                        continue;
                                    var poDetail = dataContext.PO_PurchaseOrderDetails.FirstOrDefault(p => p.PurchaseOrderDetailID == approvedPO.PurchaseOrderDetailID);
                                    var maxDeliveryLine = poDetail.PO_DeliverySchedules.Max(d => d.DeliveryLine);
                                    var newSchedule = new PO_DeliverySchedule
                                                          {
                                                              DeliveryQuantity = unApprovedPO.DeliveryQuantity,
                                                              Adjustment = unApprovedPO.DeliveryQuantity,
                                                              PurchaseOrderDetailID = poDetail.PurchaseOrderDetailID,
                                                              ScheduleDate = unApprovedPO.ScheduleDate,
                                                              DeliveryLine = maxDeliveryLine == 0 ? 1 : maxDeliveryLine + 1
                                                          };
                                    if (!updatedDetail.Contains(poDetail.PurchaseOrderDetailID))
                                        updatedDetail.Add(poDetail.PurchaseOrderDetailID);
                                    // add new delivery schedule to po detail
                                    poDetail.PO_DeliverySchedules.Add(newSchedule);
                                    // add to local list
                                    newDeliveries.Add(newSchedule);
                                }
                                else // need to add new purchase order detail and its delivery schedules
                                {
                                    // check in new local list if we insert it or not
                                    if (newDetails.Any(d => d.ProductID == unApprovedPO.ProductID && d.PO_PurchaseOrderMaster.PartyID == unApprovedPO.PartyID))
                                        continue;
                                    var unApprovedDetail = dataContext.PO_PurchaseOrderDetails.FirstOrDefault(p => p.PurchaseOrderDetailID == unApprovedPO.PurchaseOrderDetailID);
                                    var approvedMaster = approvedPOs.FirstOrDefault(p => p.PartyID == unApprovedPO.PartyID);
                                    var poMaster = dataContext.PO_PurchaseOrderMasters.FirstOrDefault(p => p.PurchaseOrderMasterID == approvedMaster.PurchaseOrderMasterID);
                                    var maxLine = poMaster.PO_PurchaseOrderDetails.Max(d => d.Line);
                                    // product in new order does not exists in current order
                                    var newOrderDetail = new PO_PurchaseOrderDetail
                                    {
                                        ProductID = unApprovedDetail.ProductID,
                                        BuyingUMID = unApprovedDetail.BuyingUMID,
                                        DiscountAmount = unApprovedDetail.DiscountAmount,
                                        ImportTax = unApprovedDetail.ImportTax,
                                        NetAmount = unApprovedDetail.NetAmount,
                                        OrderQuantity = unApprovedDetail.OrderQuantity,
                                        Line = maxLine > 0 ? maxLine + 1 : 1,
                                        RequiredDate = unApprovedDetail.RequiredDate,
                                        SpecialTax = unApprovedDetail.SpecialTax,
                                        SpecialTaxAmount = unApprovedDetail.SpecialTaxAmount,
                                        StockUMID = unApprovedDetail.StockUMID,
                                        TotalAmount = unApprovedDetail.TotalAmount,
                                        UMRate = unApprovedDetail.UMRate,
                                        UnitPrice = unApprovedDetail.UnitPrice,
                                        VAT = unApprovedDetail.VAT,
                                        VATAmount = unApprovedDetail.VATAmount,
                                        VendorItem = unApprovedDetail.VendorItem,
                                        VendorRevision = unApprovedDetail.VendorRevision,
                                        ApproverID = SystemProperty.UserID,
                                        ApprovalDate = serverDate
                                    };
                                    // add to local list
                                    newDetails.Add(newOrderDetail);
                                    foreach (var deliverySchedule in unApprovedDetail.PO_DeliverySchedules)
                                    {
                                        var newSchedule = new PO_DeliverySchedule
                                        {
                                            DeliveryQuantity = deliverySchedule.DeliveryQuantity,
                                            Adjustment = deliverySchedule.DeliveryQuantity,
                                            ScheduleDate = deliverySchedule.ScheduleDate,
                                            DeliveryLine = deliverySchedule.DeliveryLine
                                        };
                                        // add delivery to new order dteail
                                        newOrderDetail.PO_DeliverySchedules.Add(newSchedule);
                                        newDeliveries.Add(newSchedule);
                                    }
                                    // add new order detail to order master
                                    poMaster.PO_PurchaseOrderDetails.Add(newOrderDetail);
                                }
                            }
                        }
                        // now recalculate order quantity of approved purchase order detail
                        foreach (var detailId in updatedDetail)
                        {
                            var detail = dataContext.PO_PurchaseOrderDetails.FirstOrDefault(d => d.PurchaseOrderDetailID == detailId);
                            var orderQuantity = detail.PO_DeliverySchedules.Sum(d => d.DeliveryQuantity);
                            detail.OrderQuantity = orderQuantity;
                        }

                        dataContext.SubmitChanges();
                    }
                    trans.Complete();
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
    }
}
using System;
using System.Collections;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using PCSComMaterials.Plan.DS;
using PCSComUtils.Common;
using PCSComProduction.DCP.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using IsolationLevel = System.Transactions.IsolationLevel;
using PCSComUtils.DataContext;
using System.Collections.Generic;

namespace PCSComProduction.DCP.BO
{
	public class DCPEstimateBO
	{
        #region Constants
        private const string THIS = "PCSComProduction.DCP.BO.DCPEstimateBO";
        #endregion Constants

		/// <summary>
        /// Gets the work center list.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="cycleOptionMasterId">The cycle option master id.</param>
        /// <param name="productionLineId">The production line id.</param>
        /// <returns></returns>
		private static List<WorkCenterData> GetWorkCenterList(PCSDataContext dataContext, int cycleOptionMasterId, List<int> productionLineId)
		{
		    var planningOffsets = dataContext.PRO_PlanningOffsets.Where(p => p.DCOptionMasterID == cycleOptionMasterId);
		    var relateWorkCenter = from workCenter in dataContext.MST_WorkCenters
		                           join productionLine in dataContext.PRO_ProductionLines on workCenter.ProductionLineID
		                               equals productionLine.ProductionLineID
		                           join department in dataContext.MST_Departments on productionLine.DepartmentID equals
		                               department.DepartmentID
		                           where workCenter.IsMain.GetValueOrDefault(false)
		                                 && workCenter.RunDCP.GetValueOrDefault(false)
		                           join offset in planningOffsets on productionLine.ProductionLineID equals
		                               offset.ProductionLineID
		                               into gj
		                           from g in gj.DefaultIfEmpty()
		                           select new WorkCenterData
		                                      {
		                                          BalancePlanning = productionLine.BalancePlanning.GetValueOrDefault(false),
		                                          WorkCenterCode = workCenter.Code,
		                                          DepartmentCode = department.Code,
                                                  Offset = g == null ? 0 : g.Offset,
		                                          PlanningOffsetId = g == null ? 0 : g.PlanningOffsetID,
		                                          PlanningStartDate = g == null ? null : g.PlanningStartDate,
		                                          ProductionLineId = productionLine.ProductionLineID,
		                                          RoundUpDaysException = productionLine.RoundUpDaysException.GetValueOrDefault(0),
		                                          SetMinProduce = workCenter.SetMinProduce.GetValueOrDefault(false),
		                                          WorkCenterId = workCenter.WorkCenterID
		                                      };

            if (productionLineId.Count > 0)
            {
                relateWorkCenter = relateWorkCenter.Where(w => productionLineId.Contains(w.ProductionLineId));
            }
			
			var workCenterList = new List<WorkCenterData>();

            foreach (var workCenterData in relateWorkCenter)
            {
                WorkCenterData data = workCenterData;
                if (!workCenterList.Any(w => w.WorkCenterId.Equals(data.WorkCenterId)))
                {
                    data.WorkCenterLevel = -1;
                    data.WorkCenterAncessors = string.Empty;
                    workCenterList.Add(data);
                }
            }
			return workCenterList;
		}

        private static Dictionary<int, List<OrderInfo>> GetDemandAndSupply(PCSDataContext dataContext, int ccnId, DateTime fromDate, DateTime toDate)
		{
            var result = new Dictionary<int, List<OrderInfo>>();
			if (fromDate < toDate)
			{
				// get all SOs from current date to as of date
				var futureSOs = GetTotalSO(dataContext, ccnId, fromDate, toDate);
                result.Add(1, futureSOs);
				// get all POs from current date to as of date
				var futurePOs = GetTotalPO(dataContext, ccnId, fromDate, toDate);
                result.Add(2, futurePOs);
				// get all supply WOs from current date to as of date
				var futureSupplyWOs = GetTotalWO(dataContext, ccnId, fromDate, toDate);
                result.Add(3, futureSupplyWOs);
				// get all demand WOs from current date to as of date
				var futureDemandWOs = GetDemandWO(dataContext, ccnId, fromDate, toDate);
                result.Add(4, futureDemandWOs);
			}
            return result;
		}

		#region IObjectBO Members

        /// <summary>
        /// Get all sale orders in the period of time
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="ccnId">The CCN id.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>List of Sale Orders</returns>
		private static List<OrderInfo> GetTotalSO(PCSDataContext dataContext, int ccnId, DateTime fromDate, DateTime toDate)
		{
            var query = from schedule in dataContext.SO_DeliverySchedules
                        join orderDetail in dataContext.SO_SaleOrderDetails on schedule.SaleOrderDetailID equals
                            orderDetail.SaleOrderDetailID
                        join orderMaster in dataContext.SO_SaleOrderMasters on orderDetail.SaleOrderMasterID equals
                            orderMaster.SaleOrderMasterID
                        join routing in dataContext.ITM_Routings on orderDetail.ProductID equals routing.ProductID
                        join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                            workCenter.WorkCenterID
                        where workCenter.IsMain.GetValueOrDefault(false)
                              && schedule.ScheduleDate.GetValueOrDefault(DateTime.Now).CompareTo(fromDate) >= 0
                              && schedule.ScheduleDate.GetValueOrDefault(DateTime.Now).CompareTo(toDate) <= 0
                              && orderMaster.CCNID == ccnId
                        group schedule by
                            new
                                {
                                    orderMaster.ShipFromLocID,
                                    workCenter.WorkCenterID,
                                    orderDetail.ProductID,
                                    schedule.ScheduleDate
                                }
                        into g
                        select new OrderInfo
                                   {
                                       DueDate = g.Key.ScheduleDate.GetValueOrDefault(DateTime.Now),
                                       ProductId = g.Key.ProductID,
                                       Quantity = g.Sum(d => d.DeliveryQuantity.GetValueOrDefault(0)),
                                       MasterLocationId = g.Key.ShipFromLocID.GetValueOrDefault(0),
                                       WorkCenterId = g.Key.WorkCenterID
                                   };

            return query.ToList();
		}

        /// <summary>
        /// Get all purchase orders in the period of time
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="ccnId">The CCN id.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>List of Purchase Orders</returns>
		private static List<OrderInfo> GetTotalPO(PCSDataContext dataContext, int ccnId, DateTime fromDate, DateTime toDate)
		{
            var query = from schedule in dataContext.PO_DeliverySchedules
                        join orderDetail in dataContext.PO_PurchaseOrderDetails on schedule.PurchaseOrderDetailID equals
                            orderDetail.PurchaseOrderDetailID
                        join orderMaster in dataContext.PO_PurchaseOrderMasters on orderDetail.PurchaseOrderMasterID
                            equals
                            orderMaster.PurchaseOrderMasterID
                        join routing in dataContext.ITM_Routings on orderDetail.ProductID equals routing.ProductID
                        join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                            workCenter.WorkCenterID
                        where workCenter.IsMain.GetValueOrDefault(false)
                              && schedule.ScheduleDate.CompareTo(fromDate) >= 0
                              && schedule.ScheduleDate.CompareTo(toDate) <= 0
                              && orderMaster.CCNID == ccnId
                              && orderDetail.ApproverID.GetValueOrDefault(0) > 0
                              && !orderDetail.Closed.GetValueOrDefault(false)
                        group schedule by
                            new
                                {
                                    orderMaster.MasterLocationID,
                                    workCenter.WorkCenterID,
                                    orderDetail.ProductID,
                                    schedule.ScheduleDate
                                }
                        into g
                        select new OrderInfo
                                   {
                                       DueDate = g.Key.ScheduleDate,
                                       ProductId = g.Key.ProductID.GetValueOrDefault(0),
                                       Quantity = g.Sum(d => d.DeliveryQuantity),
                                       MasterLocationId = g.Key.MasterLocationID.GetValueOrDefault(0),
                                       WorkCenterId = g.Key.WorkCenterID
                                   };
            return query.ToList();
		}

        /// <summary>
        /// Get all work orders in the period of time
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="ccnId">The CCN id.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>List of Work Orders</returns>
        private static List<OrderInfo> GetTotalWO(PCSDataContext dataContext, int ccnId, DateTime fromDate, DateTime toDate)
		{
            var query = from orderDetail in dataContext.PRO_WorkOrderDetails
                        join orderMaster in dataContext.PRO_WorkOrderMasters on orderDetail.WorkOrderMasterID equals
                            orderMaster.WorkOrderMasterID
                        join routing in dataContext.ITM_Routings on orderDetail.ProductID equals routing.ProductID
                        join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                            workCenter.WorkCenterID
                        where workCenter.IsMain.GetValueOrDefault(false)
                              && orderDetail.DueDate.CompareTo(fromDate) >= 0
                              && orderDetail.DueDate.CompareTo(toDate) <= 0
                              && orderMaster.CCNID == ccnId
                              && orderDetail.Status.GetValueOrDefault(0).Equals((byte) WOLineStatus.Released)
                        group orderDetail by
                            new
                                {
                                    orderMaster.MasterLocationID,
                                    workCenter.WorkCenterID,
                                    orderDetail.ProductID,
                                    orderDetail.DueDate
                                }
                        into g
                        select new OrderInfo
                                   {
                                       DueDate = g.Key.DueDate,
                                       ProductId = g.Key.ProductID,
                                       Quantity = g.Sum(d => d.OrderQuantity),
                                       MasterLocationId = g.Key.MasterLocationID,
                                       WorkCenterId = g.Key.WorkCenterID
                                   };
            return query.ToList();
		}
		/// <summary>
		/// Get all demand work orders in the period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Work Orders</returns>
		public DataTable GetTotalDemandWO(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, int pintWorkCenterID)
		{
			MPSRegenerationProcessDS dsMPS = new MPSRegenerationProcessDS();
			return dsMPS.RetrieveParents(pintCCNID, pdtmFromDate, pdtmToDate);
		}

        /// <summary>
        /// Get all demand work orders in the period of time
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="ccnId">The CCN id.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>List of Work Orders</returns>
        private static List<OrderInfo> GetDemandWO(PCSDataContext dataContext, int ccnId, DateTime fromDate, DateTime toDate)
        {
            var query = from orderDetail in dataContext.PRO_WorkOrderDetails
                        join orderMaster in dataContext.PRO_WorkOrderMasters on orderDetail.WorkOrderMasterID equals
                            orderMaster.WorkOrderMasterID
                        join bom in dataContext.ITM_BOMs on orderDetail.ProductID equals bom.ProductID
                        join routing in dataContext.ITM_Routings on bom.ComponentID equals routing.ProductID
                        join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                            workCenter.WorkCenterID
                        where workCenter.IsMain.GetValueOrDefault(false)
                              &&
                              orderDetail.StartDate.AddSeconds(-(double)bom.LeadTimeOffset.GetValueOrDefault(0)).
                                  CompareTo(
                                      fromDate) >= 0
                              &&
                              orderDetail.StartDate.AddSeconds(-(double)bom.LeadTimeOffset.GetValueOrDefault(0)).
                                  CompareTo(
                                      toDate) <= 0
                              && orderMaster.CCNID == ccnId
                              && orderDetail.Status.GetValueOrDefault(0).Equals((byte)WOLineStatus.Released)
                        group new { orderDetail, bom } by
                            new
                                {
                                    orderMaster.MasterLocationID,
                                    workCenter.WorkCenterID,
                                    bom.ComponentID,
                                    orderDetail.DueDate,
                                    bom.LeadTimeOffset
                                }
                        into g
                        select new OrderInfo
                                   {
                                       DueDate =
                                           g.Key.DueDate.AddSeconds(-(double) g.Key.LeadTimeOffset.GetValueOrDefault(0)),
                                       ProductId = g.Key.ComponentID,
                                       Quantity =
                                           g.Sum(
                                               d =>
                                               (d.orderDetail.OrderQuantity*d.bom.Quantity.GetValueOrDefault(0))/
                                               (1 - d.bom.Shrink.GetValueOrDefault(0)/100)),
                                       MasterLocationId = g.Key.MasterLocationID,
                                       WorkCenterId = g.Key.WorkCenterID
                                   };
            return query.ToList();
        }
		
		#endregion

        /// <summary>
        /// Gets the cycle master.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="cycleOptionMaserId">The cycle option maser id.</param>
        /// <returns><see cref="PRO_DCOptionMaster"/></returns>
		private static PRO_DCOptionMaster GetCycleMaster(PCSDataContext dataContext, int cycleOptionMaserId)
		{
            return dataContext.PRO_DCOptionMasters.SingleOrDefault(o => o.DCOptionMasterID == cycleOptionMaserId);
		}

        /// <summary>
        /// Gets the delivery schedule data.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>DeliveryScheduleData</returns>
		private static List<DeliveryScheduleData> GetDeliveryScheduleData(PCSDataContext dataContext, DateTime fromDate, DateTime toDate)
		{
		    var query = from schedule in dataContext.SO_DeliverySchedules
		           join soDetail in dataContext.SO_SaleOrderDetails on schedule.SaleOrderDetailID equals
		               soDetail.SaleOrderDetailID
		           join routing in dataContext.ITM_Routings on soDetail.ProductID equals routing.ProductID
		           join product in dataContext.ITM_Products on soDetail.ProductID equals product.ProductID
		           join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals workCenter.WorkCenterID
		           where schedule.ScheduleDate.HasValue
		                 && schedule.ScheduleDate.Value.CompareTo(fromDate) > 0
		                 && schedule.ScheduleDate.Value.CompareTo(toDate) <= 0
		           join checkPoint in dataContext.PRO_CheckPoints on new {soDetail.ProductID, workCenter.WorkCenterID} equals
		               new {checkPoint.ProductID, checkPoint.WorkCenterID}
		               into gj
		           from g in gj.DefaultIfEmpty()
		           select new DeliveryScheduleData
		                      {
		                          CapacityBottleId = 0,
		                          DelayTime = g == null ? 0 : g.DelayTime,
		                          Quantity = schedule.DeliveryQuantity.GetValueOrDefault(0),
		                          FixLeadTime = routing.FixLT.GetValueOrDefault(0),
		                          MaxProduce = product.MaxProduce.GetValueOrDefault(0),
		                          MaxRoundUpToMin = product.MaxRoundUpToMin.GetValueOrDefault(0),
		                          MaxRoundUpToMultiple = product.MaxRoundUpToMultiple.GetValueOrDefault(0),
		                          MinProduce = product.MinProduce.GetValueOrDefault(0),
		                          OrderQuantityMultiple = product.OrderQuantityMultiple.GetValueOrDefault(1),
		                          ProductId = soDetail.ProductID,
		                          Revision = product.Revision,
		                          RoutingId = routing.RoutingID,
		                          SafetyStock = product.SafetyStock.GetValueOrDefault(0),
		                          SamplePattern = g == null ? (byte)0 : g.SamplePattern,
		                          SampleRate = g == null ? 0 : g.SampleRate,
		                          ScheduleDate = schedule.ScheduleDate.GetValueOrDefault(DateTime.Now),
		                          StartTime = schedule.ScheduleDate.GetValueOrDefault(DateTime.Now),
		                          EndTime = schedule.ScheduleDate.GetValueOrDefault(DateTime.Now),
		                          ScrapPercent = product.ScrapPercent.GetValueOrDefault(0),
		                          LeadTime = product.LTVariableTime.GetValueOrDefault(0),
		                          WorkCenterCode = workCenter.Code,
		                          WorkCenterId = workCenter.WorkCenterID
		                      };
            var result = query.ToList();
            return result;
		}

        private static List<DeliveryScheduleData> GetDeliveryForParent(PCSDataContext dataContext, DateTime fromDate, DateTime toDate)
        {
            // get more delivery from dcp result
            var query = from dcpDetail in dataContext.PRO_DCPResultDetails
                           join dcpMaster in dataContext.PRO_DCPResultMasters on dcpDetail.DCPResultMasterID equals
                               dcpMaster.DCPResultMasterID
                           join bom in dataContext.ITM_BOMs on dcpMaster.ProductID equals bom.ProductID
                           join routing in dataContext.ITM_Routings on bom.ComponentID equals routing.ProductID
                           join product in dataContext.ITM_Products on bom.ComponentID equals product.ProductID
                           join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                               workCenter.WorkCenterID
                           where workCenter.IsMain.GetValueOrDefault(false)
                           && dcpDetail.StartTime.AddDays(-(double)bom.LeadTimeOffset.GetValueOrDefault(0) / 86400).CompareTo(fromDate) >= 0
                           && dcpDetail.StartTime.AddDays(-(double)bom.LeadTimeOffset.GetValueOrDefault(0) / 86400).CompareTo(toDate) <= 0
                           join checkPoint in dataContext.PRO_CheckPoints on new { routing.ProductID, workCenter.WorkCenterID } equals
                    new { checkPoint.ProductID, checkPoint.WorkCenterID }
                    into gj
                           from g in gj.DefaultIfEmpty()
                           select new DeliveryScheduleData
                           {
                               CapacityBottleId = 0,
                               DelayTime = g == null ? 0 : g.DelayTime,
                               Quantity = (dcpDetail.Quantity * bom.Quantity.GetValueOrDefault(0)) / ((100 - bom.Shrink.GetValueOrDefault(0)) / 100),
                               FixLeadTime = routing.FixLT.GetValueOrDefault(0),
                               MaxProduce = product.MaxProduce.GetValueOrDefault(0),
                               MaxRoundUpToMin = product.MaxRoundUpToMin.GetValueOrDefault(0),
                               MaxRoundUpToMultiple = product.MaxRoundUpToMultiple.GetValueOrDefault(0),
                               MinProduce = product.MinProduce.GetValueOrDefault(0),
                               OrderQuantityMultiple = product.OrderQuantityMultiple.GetValueOrDefault(1),
                               ProductId = bom.ComponentID,
                               Revision = product.Revision,
                               RoutingId = routing.RoutingID,
                               SafetyStock = product.SafetyStock.GetValueOrDefault(0),
                               SamplePattern = g == null ? (byte)0 : g.SamplePattern,
                               SampleRate = g == null ? 0 : g.SampleRate,
                               ScheduleDate = dcpDetail.WorkingDate.GetValueOrDefault(DateTime.Now),
                               StartTime = dcpDetail.StartTime,
                               EndTime = dcpDetail.EndTime,
                               ScrapPercent = product.ScrapPercent.GetValueOrDefault(0),
                               LeadTime = product.LTVariableTime.GetValueOrDefault(0),
                               WorkCenterCode = workCenter.Code,
                               WorkCenterId = workCenter.WorkCenterID
                           };
            return query.ToList();
        }

        /// <summary>
        /// Gets the BOM info.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="ignoreMoveTime">if set to <c>true</c> [ignore move time].</param>
        /// <returns></returns>
		private static List<BOMData> GetBOMInfo(PCSDataContext dataContext, bool ignoreMoveTime)
		{
            var query = from bom in dataContext.ITM_BOMs
                        join product in dataContext.ITM_Products on bom.ComponentID equals product.ProductID
                        join routing in dataContext.ITM_Routings on bom.ComponentID equals routing.ProductID
                        join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                            workCenter.WorkCenterID
                        join routing2 in dataContext.ITM_Routings on bom.ProductID equals routing2.ProductID
                        join workCenter2 in dataContext.MST_WorkCenters on routing2.WorkCenterID equals
                            workCenter2.WorkCenterID
                        where product.MakeItem && workCenter2.IsMain.GetValueOrDefault(false)
                        join checkPoint in dataContext.PRO_CheckPoints on routing.ProductID equals checkPoint.ProductID
                            into
                            gj
                        from g in gj.DefaultIfEmpty()
                        select new BOMData
                                   {
                                       ComponentId = bom.ComponentID,
                                       ProductId = bom.ProductID,
                                       Quantity = bom.Quantity.GetValueOrDefault(0),
                                       LeadTimeOffset = bom.LeadTimeOffset.GetValueOrDefault(0),
                                       Shrink = bom.Shrink.GetValueOrDefault(0),
                                       WorkCenterId = workCenter.WorkCenterID,
                                       WorkCenterCode = workCenter.Code,
                                       SamplePattern = g == null ? (byte) 0 : g.SamplePattern,
                                       SampleRate = g == null ? 0 : g.SampleRate,
                                       DelayTime = g == null ? 0 : g.DelayTime,
                                       RoutingId = routing.RoutingID,
                                       ParentWorkCenterId = workCenter2.WorkCenterID,
                                       ParentWorkCenterCode = workCenter2.Code,
                                       Revision = product.Revision,
                                       OrderQuantityMultiple = product.OrderQuantityMultiple.GetValueOrDefault(1),
                                       MinProduce = product.MinProduce.GetValueOrDefault(0),
                                       MaxProduce = product.MaxProduce.GetValueOrDefault(0),
                                       ScrapPercent = product.ScrapPercent.GetValueOrDefault(0),
                                       MaxRoundUpToMin = product.MaxRoundUpToMin.GetValueOrDefault(0),
                                       MaxRoundUpToMultiple = product.MaxRoundUpToMultiple.GetValueOrDefault(0),
                                       FixLeadTime = routing.FixLT.GetValueOrDefault(0),
                                       LeadTime = ignoreMoveTime
                                                      ? routing.Pacer == 'M'
                                                            ? routing.MachineSetupTime + routing.MachineRunTime
                                                            : routing.Pacer == 'L'
                                                                  ? routing.LaborRunTime + routing.LaborSetupTime
                                                                  : routing.MachineRunTime + routing.MachineSetupTime +
                                                                    routing.LaborSetupTime + routing.LaborRunTime
                                                      : routing.Pacer == 'M'
                                                            ? routing.MachineSetupTime + routing.MachineRunTime +
                                                              routing.MoveTime
                                                            : routing.Pacer == 'L'
                                                                  ? routing.LaborRunTime + routing.LaborSetupTime +
                                                                    routing.MoveTime
                                                                  : routing.MachineRunTime + routing.MachineSetupTime +
                                                                    routing.LaborSetupTime + routing.LaborRunTime +
                                                                    routing.MoveTime
                                   };
            return query.ToList();
		}

        /// <summary>
        /// Gets the product info.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <returns></returns>
		private static List<ProductInfo> GetProductInfo(PCSDataContext dataContext)
		{
            var query = from product in dataContext.ITM_Products
                        join routing in dataContext.ITM_Routings on product.ProductID equals routing.ProductID
                        join workCenter in dataContext.MST_WorkCenters on routing.WorkCenterID equals
                            workCenter.WorkCenterID
                        join checkPoint in dataContext.PRO_CheckPoints on
                            new {workCenter.WorkCenterID, product.ProductID} equals
                            new {checkPoint.WorkCenterID, checkPoint.ProductID}
                            into gj
                        from g in gj.DefaultIfEmpty()
                        select new ProductInfo
                                   {
                                       ProductId = product.ProductID,
                                       Revision = product.Revision,
                                       SafetyStock = product.SafetyStock.GetValueOrDefault(0),
                                       MinProduce = product.MinProduce.GetValueOrDefault(0),
                                       MaxProduce = product.MaxProduce.GetValueOrDefault(0),
                                       ScrapPercent = product.ScrapPercent.GetValueOrDefault(0),
                                       OrderQuantityMultiple = product.OrderQuantityMultiple.GetValueOrDefault(0),
                                       MaxRoundUpToMin = product.MaxRoundUpToMin.GetValueOrDefault(0),
                                       MaxRoundUpToMultiple = product.MaxRoundUpToMultiple.GetValueOrDefault(0),
                                       CategoryId = product.CategoryID,
                                       GroupPriority = 0,
                                       HasParent = dataContext.ITM_BOMs.Any(b => b.ComponentID == product.ProductID),
                                       LeadTime = product.LTVariableTime,
                                       FixLeadTime = routing.FixLT.GetValueOrDefault(0),
                                       WorkCenterId = workCenter.WorkCenterID,
                                       WorkCenterCode = workCenter.Code,
                                       RoutingId = routing.RoutingID,
                                       SamplePattern = g == null ? (byte) 0 : g.SamplePattern,
                                       SampleRate = g == null ? 0 : g.SampleRate,
                                       DelayTime = g == null ? 0 : g.DelayTime
                                   };
            return query.ToList();
		}

        /// <summary>
        /// Gets the production group.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <returns></returns>
		private static List<ProductionGroupInfo> GetProductionGroup(PCSDataContext dataContext)
		{
		    var mainWorkCenter = dataContext.MST_WorkCenters.Where(w => w.IsMain.GetValueOrDefault(false));
            var query = from pg in dataContext.PRO_ProductionGroups
                        join workCenter in mainWorkCenter on pg.ProductionLineID equals workCenter.ProductionLineID
                        join pgProduct in dataContext.PRO_PGProducts on pg.ProductionGroupID equals
                            pgProduct.ProductionGroupID
                        select new ProductionGroupInfo
                                   {
                                       CapacityOfGroup = pg.CapacityOfGroup,
                                       GroupProductionMax = pg.GroupProductionMax,
                                       Priority = pg.Priority.GetValueOrDefault(100),
                                       ProductId = pgProduct.ProductID,
                                       ProductionGroupId = pg.ProductionGroupID,
                                       ProductionLineId = pg.ProductionLineID,
                                       WorkCenterId = workCenter.WorkCenterID
                                   };
            return query.ToList();
		}

        /// <summary>
        /// Selects the planning offset.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="cycleOptionMasterId">The cycle option master id.</param>
        /// <returns></returns>
		private static List<PlanningOffsetInfo> SelectPlanningOffset(PCSDataContext dataContext, int cycleOptionMasterId)
		{
            var query = from offset in dataContext.PRO_PlanningOffsets
                        join workCenter in dataContext.MST_WorkCenters on offset.ProductionLineID equals
                            workCenter.ProductionLineID
                        where workCenter.IsMain.GetValueOrDefault(false)
                              && offset.DCOptionMasterID == cycleOptionMasterId
                        select new PlanningOffsetInfo
                                   {
                                       Offset = offset.Offset,
                                       PlanningOffsetId = offset.PlanningOffsetID,
                                       PlanningStartDate = offset.PlanningStartDate,
                                       WorkCenterId = workCenter.WorkCenterID
                                   };
            return query.ToList();
		}

        /// <summary>
        /// Gets the shifts.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="productionLineList">The production line list.</param>
        /// <returns></returns>
		private static List<ShiftInfo> GetShifts(PCSDataContext dataContext, List<int> productionLineList)
		{
		    var list = from shift in dataContext.PRO_Shifts
		               join shiftCapacity in dataContext.PRO_ShiftCapacities on shift.ShiftID equals shiftCapacity.ShiftID
		               join wcCapacity in dataContext.PRO_WCCapacities on shiftCapacity.WCCapacityID equals
		                   wcCapacity.WCCapacityID
		               join workCenter in dataContext.MST_WorkCenters on wcCapacity.WorkCenterID equals
		                   workCenter.WorkCenterID
		               join shiftPattern in dataContext.PRO_ShiftPatterns on shift.ShiftID equals shiftPattern.ShiftID
		               where workCenter.IsMain.GetValueOrDefault(false)
		               select new ShiftInfo
		                          {
		                              BeginDate = wcCapacity.BeginDate,
		                              EndDate = wcCapacity.EndDate,
		                              ProductionLineId = workCenter.ProductionLineID.GetValueOrDefault(0),
		                              ShiftId = shift.ShiftID,
		                              WorkTimeFrom = shiftPattern.WorkTimeFrom,
		                              WorkTimeTo = shiftPattern.WorkTimeTo
		                          };
            return productionLineList.Count > 0 ? list.Where(p => productionLineList.Contains(p.ProductionLineId)).ToList() : list.ToList();
		}

        /// <summary>
        /// Gets the top level work center.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="productionLines">The production lines.</param>
        /// <returns></returns>
		private static List<MST_WorkCenter> GetTopLevelWorkCenter(PCSDataContext dataContext, List<int> productionLines)
		{
            var query = productionLines.Count > 0
                            ? dataContext.MST_WorkCenters.Where(
                                w => productionLines.Contains(w.ProductionLineID.GetValueOrDefault(0))
                                     && w.IsMain.GetValueOrDefault(false) &&
                                     w.RunDCP.GetValueOrDefault(false))
                            : (from workCenter in dataContext.MST_WorkCenters
                               join routing in dataContext.ITM_Routings on workCenter.WorkCenterID equals
                                   routing.WorkCenterID
                               where workCenter.IsMain.GetValueOrDefault(false)
                                     && workCenter.RunDCP.GetValueOrDefault(false)
                                     && dataContext.ITM_BOMs.Select(b => b.ProductID).Contains(routing.ProductID)
                                     && !dataContext.ITM_BOMs.Select(b => b.ComponentID).Contains(routing.ProductID)
                               select workCenter);
            return query.ToList();
		}

        private static void UpdateDCPResult(PCSDataContext dataContext, int dcOptionMasterId, List<int> productionLines,
            List<PRO_DCPResultMaster> dcpResultMaster, List<PlanningOffsetInfo> planningOffsetInfos,
            List<DCP_BeginQuantity> beginData, List<WorkCenterData> workCenterList)
		{
            const string methodName = THIS + ".UpdateDCPResult()";
            try
            {
                DateTime serverDate = dataContext.GetServerDate();
                var master = dataContext.PRO_DCOptionMasters.SingleOrDefault(
                            o => o.DCOptionMasterID == dcOptionMasterId);
                if (master != null)
                    master.LastUpdate = serverDate;
                
                string sql;
                if (productionLines.Count > 0)
                {
                    var lines = string.Join(",", Array.ConvertAll(productionLines.ToArray(), Convert.ToString));
                    sql = string.Format("DELETE FROM PRO_DCPResultMaster WHERE DCOptionMasterID = {0}"
                                        + " AND WorkCenterID IN (SELECT WorkCenterID FROM MST_WorkCenter"
                                        + " WHERE IsMain = 1 AND ProductionLineID IN ({1}))",
                                        dcOptionMasterId, lines);
                }
                else
                {
                    sql = string.Format("DELETE FROM PRO_DCPResultMaster WHERE DCOptionMasterID = {0}", dcOptionMasterId);
                }

                dataContext.ExecuteCommand(sql);
                dataContext.PRO_DCPResultMasters.InsertAllOnSubmit(dcpResultMaster);

                foreach (var drowPlanningOfs in planningOffsetInfos)
                {
                    //find workcenter
                    var workCenter = workCenterList.FirstOrDefault(w => w.PlanningOffsetId == drowPlanningOfs.PlanningOffsetId);
                    var offset = dataContext.PRO_PlanningOffsets.SingleOrDefault(p => p.PlanningOffsetID == drowPlanningOfs.PlanningOffsetId);
                    offset.PlanningStartDate = workCenter.PlanningStartDate;
                    DateTime fromDate = drowPlanningOfs.PlanningStartDate.GetValueOrDefault(DateTime.Now);
                    DateTime toDate = fromDate.AddDays(master.PlanHorizon.GetValueOrDefault(0) + 1);
                    //close workorders
                    string closeSql = string.Format("UPDATE PRO_WorkOrderDetail SET Status = {0},"
                                                    + " MfgCloseDate = GETDATE()"
                                                    + " WHERE DueDate > '{1}' AND DueDate < '{2}'"
                                                    + " AND Status = {3}", (int)WOLineStatus.MfgClose,
                                                    fromDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    toDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                    (int)WOLineStatus.Released);
                    dataContext.ExecuteCommand(closeSql);
                }

                string deleteBegin = string.Format("DELETE FROM DCP_BeginQuantity WHERE DCOptionMasterID = {0}", dcOptionMasterId);
                dataContext.ExecuteCommand(deleteBegin);
                dataContext.DCP_BeginQuantities.InsertAllOnSubmit(beginData);

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
		/// Get year does not configs
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public int GetLackOfYearInCalendar(int pintDCOptionMasterID)
		{
			return (new PRO_DCOptionMasterDS()).GetLackOfYearInCalendar(pintDCOptionMasterID);
		}
		/// <summary>
		/// Get 2 year from ... to ... need to config 
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public string[] GetFromYearToYear(int pintDCOptionMasterID)
		{
			return (new PRO_DCOptionMasterDS()).GetFromYearToYear(pintDCOptionMasterID);
		}
		/// <summary>
		/// Get WC was not config
		/// </summary>
		/// <param name="pintDCOptionMasterID"></param>
		/// <returns></returns>
		public string[] GetWorkCenterNotConfig(int pintDCOptionMasterID)
		{
			return (new PRO_DCOptionMasterDS()).GetWorkCenterNotConfig(pintDCOptionMasterID);
        }

        public void UpdateBeginStock(PRO_DCOptionMasterVO voCycle, string pstrCCNID, DateTime dtmServerDate, List<int> parrProLineID)
        {
            DCPReportBO boReport = new DCPReportBO();

            DataTable dtbCache;
            DataTable dtbBeginStock = boReport.GetBeginStockForReportData();
            if (dtmServerDate.Month == 1 || dtmServerDate.Month == 7)
            {
                dtbCache = boReport.GetBeginNetQuantity(pstrCCNID);
            }
            else
            {
                dtbCache = boReport.GetBeginBalance(voCycle.PlanningPeriod.Date);
            }
            // get list of production line
            DataTable dtbListProductionLine = null;
            StringBuilder sbListLine = new StringBuilder();
            if (parrProLineID == null)
            {
                dtbListProductionLine = boReport.ListProductionLine();
                foreach (DataRow drowLine in dtbListProductionLine.Rows)
                {
                    sbListLine.Append(drowLine["ProductionLineID"].ToString()).Append(",");
                }
            }
            else
            {
                foreach (int productionLineId in parrProLineID)
                {
                    sbListLine.Append(productionLineId).Append(",");
                }
            }

            sbListLine.Append("0");
            // get list of all product
            DataTable dtbListProduct = boReport.ListProduct(pstrCCNID, sbListLine.ToString());
            // calculate foreach product
            foreach (DataRow drowProduct in dtbListProduct.Rows)
            {
                string strProductID = drowProduct["ProductID"].ToString();
                decimal decQuantity = 0;
                try
                {
                    decQuantity = Convert.ToDecimal(dtbCache.Compute("SUM(Quantity)", "ProductID = " + strProductID));
                }
                catch { }
                // only update for effect month by planning period
                string strFilter = string.Format("ProductID = {0} AND EffectDate = '{1}'", strProductID, voCycle.PlanningPeriod.Date);
                // update existing record
                if (dtbBeginStock.Select(strFilter).Length > 0)
                {
                    DataRow drowProductStock = dtbBeginStock.Select(strFilter)[0];
                    drowProductStock["Quantity"] = decQuantity;
                    drowProductStock["LastUpdate"] = dtmServerDate;
                    drowProductStock["EffectDate"] = voCycle.PlanningPeriod.Date;
                    drowProductStock["Username"] = SystemProperty.UserName;
                }
                else // else add new record
                {
                    DataRow drowProductStock = dtbBeginStock.NewRow();
                    drowProductStock["Quantity"] = decQuantity;
                    drowProductStock["ProductID"] = strProductID;
                    drowProductStock["LastUpdate"] = dtmServerDate;
                    drowProductStock["EffectDate"] = voCycle.PlanningPeriod.Date;
                    drowProductStock["Username"] = SystemProperty.UserName;
                    dtbBeginStock.Rows.Add(drowProductStock);
                }
            }
            // update dataSet
            DataSet dstData = new DataSet();
            dstData.Tables.Add(dtbBeginStock);
            boReport.UpdateBeginStockData(dstData);
        }

        /// <summary>
        /// Gets the real working day.
        /// </summary>
        /// <param name="pdtmNeedToResolve">The Date need to resolve.</param>
        /// <param name="pdtbWorkingTime">The working time.</param>
        /// <returns></returns>
        private static DateTime GetRealWorkingDay(DateTime pdtmNeedToResolve, List<PRO_ShiftPattern> pdtbWorkingTime)
        {
            var drowShifts = pdtbWorkingTime.OrderBy(w => w.WorkTimeFrom);

            if (drowShifts.Count() <= 0)
            {
                return DateTime.MinValue;
            }

            var firstShift = drowShifts.FirstOrDefault();
            DateTime dtmResolvedDate = pdtmNeedToResolve;
            DateTime workTimeFrom = firstShift.WorkTimeFrom.GetValueOrDefault(DateTime.Now);
            //change shift configured day to working day
            DateTime dtmStartTime = new DateTime(pdtmNeedToResolve.Year, pdtmNeedToResolve.Month, pdtmNeedToResolve.Day,
                workTimeFrom.Hour, workTimeFrom.Minute, workTimeFrom.Second);

            while (dtmResolvedDate < dtmStartTime)
            {
                dtmStartTime = dtmStartTime.AddDays(-1);
            }

            return dtmStartTime;
        }

        #region biz process to run dcp estimate

        #region Main function

        /// <summary>
        /// Assigns the group priority.
        /// </summary>
        /// <param name="products">The products.</param>
        /// <param name="productionGroups">The production groups.</param>
        private static void AssignGroupPriority(List<ProductInfo> products, List<ProductionGroupInfo> productionGroups)
        {
            foreach (var product in products)
            {
                int productId = product.ProductId;
                //select group
                var group = productionGroups.Where(g => g.ProductId == productId).OrderBy(g => g.Priority).FirstOrDefault();
                product.GroupPriority = group != null ? group.Priority : 100;
            }
        }

        private List<PRO_WCCapacity> GetWorkingDateFromWorkCenter(PCSDataContext dataContext, List<int> productionLineId)
        {
            var query = from wcCapacity in dataContext.PRO_WCCapacities
                        join workCenter in dataContext.MST_WorkCenters on wcCapacity.WorkCenterID equals
                            workCenter.WorkCenterID
                        where
                            workCenter.IsMain.GetValueOrDefault(false) &&
                            productionLineId.Contains(workCenter.ProductionLineID.GetValueOrDefault(0))
                        select wcCapacity;
            return query.ToList();
        }

        /// <summary>
        /// Gets the first valid work day.
        /// </summary>
        /// <param name="validDays">The valid days.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        private static DateTime GetFirstValidWorkDay(List<PRO_WCCapacity> validDays, DateTime fromDate, DateTime toDate)
        {
            var validDay = fromDate;
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                if (validDays.Any(d => d.BeginDate.CompareTo(date) <= 0 && d.EndDate.CompareTo(date) >= 0))
                {
                    validDay = date;
                    break;
                }
            }
            return validDay;
        }

        private static DateTime ConvertWorkingDay(List<PRO_ShiftPattern> workingTime, List<PRO_WCCapacity> validDays, DateTime workingDay, DateTime date, decimal numberOfDay)
        {
            DateTime dtmConvert = date;
            
            dtmConvert = dtmConvert.AddDays(-(double)numberOfDay);
            bool isOk = false;
            while (!isOk)
            {
                DateTime dtmOld = dtmConvert;
                DateTime dtmConverted = new DateTime(workingDay.Year, workingDay.Month, workingDay.Day);
                var day = validDays.FirstOrDefault(d => d.BeginDate.CompareTo(dtmConverted) <= 0 && d.EndDate.CompareTo(dtmConverted) >= 0);
                if (day == null)
                {
                    day = validDays.FirstOrDefault(d => d.BeginDate.CompareTo(dtmConverted) <= 0);
                    if (day == null)
                    {
                        if (validDays.FirstOrDefault() != null)
                        {
                            dtmConvert = validDays.FirstOrDefault().BeginDate;
                        }
                        break;
                    }

                    dtmConvert = dtmConvert.AddDays(-1);
                    workingDay = GetRealWorkingDay(dtmConvert, workingTime);
                }

                if (dtmOld == dtmConvert)
                {
                    isOk = true;
                }
            }

            return dtmConvert;
        }

        private static void AddjustScheduleDateForParent(List<DeliveryScheduleData> deliveryForParent, int workCenterId, List<PRO_ShiftPattern> workingTime, List<PRO_WCCapacity> validDays, DateTime fromDate, DateTime toDate)
        {
            var dtmFirstValidDay = GetFirstValidWorkDay(validDays, fromDate, toDate);
            DateTime scheduleDate;
            foreach (var delivery in deliveryForParent.Where(d => d.WorkCenterId == workCenterId))
            {
                var startTime = delivery.StartTime;
                var endTime = delivery.EndTime;
                // over quantity from parent
                if (startTime.Equals(endTime))
                {
                    continue;
                }

                DateTime dtmTemp = new DateTime(startTime.Year, startTime.Month, startTime.Day);
                if (dtmTemp <= dtmFirstValidDay && dtmTemp >= fromDate)
                {
                    startTime = new DateTime(dtmFirstValidDay.Year, dtmFirstValidDay.Month, dtmFirstValidDay.Day,
                        startTime.Hour, startTime.Minute, startTime.Second);
                    scheduleDate = GetRealWorkingDay(startTime, workingTime);
                    delivery.StartTime = startTime;
                    delivery.ScheduleDate = scheduleDate;
                    continue;
                }
                decimal decLeadTimeOffset = 0;
                try
                {
                    decLeadTimeOffset = delivery.LeadTime.GetValueOrDefault(0);
                }
                catch { }
                decimal decNumOfDay = decLeadTimeOffset / 86400;
                // convert to valid work day
                startTime = ConvertWorkingDay(workingTime, validDays, delivery.ScheduleDate, startTime, decNumOfDay);
                scheduleDate = GetRealWorkingDay(startTime, workingTime);
                delivery.StartTime = startTime;
                delivery.ScheduleDate = scheduleDate;
            }
        }

        /// <summary>
        /// Processing DCP
        /// </summary>
        /// <param name="cycleOptionMasterId">The DCP cycle option master id.</param>
        /// <param name="parrProductionLine">The production line list.</param>
        /// <returns></returns>
        /// <author>dungla</author>
        public int RunDCP(int cycleOptionMasterId, List<int> parrProductionLine)
        {
            using (var trans = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions{IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    #region Prepare data
                    var cycleMaster = GetCycleMaster(dataContext, cycleOptionMasterId);
                    var fromDate = cycleMaster.AsOfDate ?? DateTime.Now;
                    var toDate = fromDate.AddDays(cycleMaster.PlanHorizon.GetValueOrDefault(0) + 1);
                    var deliveryScheduleData = GetDeliveryScheduleData(dataContext, fromDate, toDate);
                    var deliveryForParent = GetDeliveryForParent(dataContext, fromDate, toDate);
                    var workCenterList = GetWorkCenterList(dataContext, cycleOptionMasterId, parrProductionLine);
                    var bomInfo = GetBOMInfo(dataContext, cycleMaster.IgnoreMoveTime.GetValueOrDefault(false));
                    var dcpResultMaster = new List<PRO_DCPResultMaster>();
                    var productInfo = GetProductInfo(dataContext);
                    var rawResultList = new List<RawResultData>();
                    var productionGroups = GetProductionGroup(dataContext);
                    var planningOffsets = SelectPlanningOffset(dataContext, cycleOptionMasterId);
                    var shifts = GetShifts(dataContext, parrProductionLine);
                    var workingTime = GetWorkingTime(dataContext);
                    var validDays = GetWorkingDateFromWorkCenter(dataContext, parrProductionLine);
                    // dungla 25-09-2006: begin data for other purpose
                    var beginData = dataContext.DCP_BeginQuantities.Where(b => b.DCOptionMasterID == cycleOptionMasterId).ToList();

                    ReCalculateLeadTime(deliveryScheduleData, bomInfo, productInfo, cycleMaster.IncludeCheckPoint.GetValueOrDefault(false));
                    AssignGroupPriority(productInfo, productionGroups);
                    
                    #endregion

                    #region Sort production line
                    var topLevelWorkCenter = GetTopLevelWorkCenter(dataContext, parrProductionLine);
                    SortWorkCenterList(topLevelWorkCenter, workCenterList, bomInfo);
                    #endregion

                    #region Feed bottles algorithm

                    foreach (var workCenter in workCenterList)
                    {
                        var wcValidDays = validDays.Where(v => v.WorkCenterID == workCenter.WorkCenterId);
                        AddjustScheduleDateForParent(deliveryForParent, workCenter.WorkCenterId, workingTime, wcValidDays.ToList(), fromDate, toDate);
                    }
                    deliveryScheduleData.AddRange(deliveryForParent);

                    int intResult = AdjustDeliveryAndBottles(dataContext, cycleMaster, deliveryScheduleData, workCenterList,
                        bomInfo, productInfo, rawResultList, beginData);
                    #endregion

                    #region Write final results
                    WriteFinalResult(dataContext, cycleOptionMasterId, rawResultList, workCenterList, shifts, dcpResultMaster);
                    #endregion Write final results

                    #region Update DCP result

                    UpdateDCPResult(dataContext, cycleOptionMasterId, parrProductionLine, dcpResultMaster, planningOffsets, beginData, workCenterList);

                    #endregion

                    trans.Complete();
                    return intResult;
                }
            }
        }

        #endregion Main function

        #region Bottle feed algorithm

        private const string Outside = "MAKER";

        private static int AdjustDeliveryAndBottles(PCSDataContext dataContext, PRO_DCOptionMaster dcOptionMaster, List<DeliveryScheduleData> deliveryScheduleDatas,
            List<WorkCenterData> workCenterList, List<BOMData> bomDatas, List<ProductInfo> productInfos, List<RawResultData> rawResultDatas,
            List<DCP_BeginQuantity> beginData)
        {
            #region Preparing
            bool useOnHand = dcOptionMaster.OnHand.GetValueOrDefault(false);
            int ccnId = dcOptionMaster.CCNID.GetValueOrDefault(0);
            DateTime asOfDate = dcOptionMaster.AsOfDate.GetValueOrDefault(DateTime.Now);
            int planHorizon = dcOptionMaster.PlanHorizon.GetValueOrDefault(0);

            DateTime serverDate = dataContext.GetServerDate();

            var dicFuturePOs = new Dictionary<int, List<OrderInfo>>();
            var dicFutureSupplyWOs = new Dictionary<int, List<OrderInfo>>();
            var dicFutureDemandWOs = new Dictionary<int, List<OrderInfo>>();
            var dicFutureSOs = new Dictionary<int, List<OrderInfo>>();

            var dictionary = GetDemandAndSupply(dataContext, ccnId, serverDate, asOfDate);
            //cache all future WO & PO by workcenter
            var futureSOs = dictionary[1];
            var futurePOs = dictionary[2];
            var futureSupplyWOs = dictionary[3];
            var futureDemandWOs = dictionary[4];

            #endregion Preparing

            //walk through workcenter base on level
            var orderedWorkCenterList = workCenterList.OrderBy(w => w.WorkCenterLevel);
            foreach (var workCenter in orderedWorkCenterList)
            {
                #region Preparing
                string departmentCode = workCenter.DepartmentCode;
                string workCenterCode = workCenter.WorkCenterCode;
                int workCenterId = workCenter.WorkCenterId;
                bool isOutside = (departmentCode.StartsWith(Outside, StringComparison.OrdinalIgnoreCase));

                workCenter.PlanningStartDate = null;

                var planningStartDate = asOfDate;

                if (serverDate >= planningStartDate)
                {
                    return 1;
                }
                planningStartDate = planningStartDate.AddMinutes(375);

                var futureDic = GetDemandAndSupply(serverDate, planningStartDate, dictionary, workCenterId);
                var wcFutureSupplyWOs = futureDic[3];
                var wcFutureDemandWOs = futureDic[4];

                if (!dicFuturePOs.ContainsKey(workCenterId))
                {
                    dicFuturePOs.Add(workCenterId, futurePOs);
                }

                if (!dicFutureSOs.ContainsKey(workCenterId))
                {
                    dicFutureSOs.Add(workCenterId, futureSOs);
                }

                if (!dicFutureSupplyWOs.ContainsKey(workCenterId))
                {
                    dicFutureSupplyWOs.Add(workCenterId, wcFutureSupplyWOs);
                }

                if (!dicFutureDemandWOs.ContainsKey(workCenterId))
                {
                    dicFutureDemandWOs.Add(workCenterId, wcFutureDemandWOs);
                }
                
                #endregion Preparing

                //adjust delivery quantity base on inventory
                #region Onhand-Produce adjust
                List<DeliveryScheduleData> arrDeliveries;
                var arrProducts = productInfos.Where(p => p.WorkCenterId == workCenterId);
                //foreach product
                if (useOnHand)
                {
                    foreach (var product in arrProducts)
                    {
                        int productId = product.ProductId;

                        futurePOs = dicFuturePOs[workCenterId];
                        futureSupplyWOs = dicFutureSupplyWOs[workCenterId];
                        futureDemandWOs = dicFutureDemandWOs[workCenterId];
                        futureSOs = dicFutureSOs[workCenterId];

                        // ignore onhand quantity
                        bool useCacheAsBegin = dcOptionMaster.UseCacheAsBegin.GetValueOrDefault(false);
                        decimal decStock = GetOnHandAtAsOfDate(productId, futureSOs, futurePOs, futureSupplyWOs,
                            futureDemandWOs, true, useCacheAsBegin);

                        //first of all, subtract for over quantity
                        //select all parent
                        var arrParent = bomDatas.Where(b => b.ComponentId == productId);
                        decimal decTotalParentOverQuantity = (from parent in arrParent
                                                              let parentProductId = parent.ProductId
                                                              let decShrink = parent.Shrink
                                                              let decBOMQty = parent.Quantity
                                                              let decParentOverQuantity = deliveryScheduleDatas.Where(d => d.ProductId == parentProductId && d.CapacityBottleId < 0).Sum(d => d.Quantity)
                                                              select decParentOverQuantity*(1 - decShrink/100)/decBOMQty).Sum();
                        decStock -= decTotalParentOverQuantity;
                        if (decStock < 0)
                        {
                            decStock = 0;
                        }

                        #region dungla 25-09-2006: save begin data for other purpose

                        var beginRow = beginData.FirstOrDefault(b => b.ProductID == productId && b.DCOptionMasterID== dcOptionMaster.DCOptionMasterID);
                        // update exist record
                        if (beginRow != null)
                        {
                            beginRow.Quantity = decStock;
                        }
                        else
                        {
                            // create new record
                            beginRow = new DCP_BeginQuantity
                                           {
                                               DCOptionMasterID = dcOptionMaster.DCOptionMasterID,
                                               ProductID = productId,
                                               Quantity = decStock
                                           };
                            beginData.Add(beginRow);
                        }

                        #endregion

                        //forward day by day
                        DateTime currentDay = planningStartDate;
                        for (int intIdx = 0; intIdx <= planHorizon + (planningStartDate.Subtract(asOfDate)).TotalDays; intIdx++)
                        {
                            //get all deliveries
                            arrDeliveries = deliveryScheduleDatas.Where(d => d.ProductId == productId
                                                                             && d.WorkCenterId == workCenterId
                                                                             && d.ScheduleDate.CompareTo(currentDay) >= 0
                                                                             && d.ScheduleDate.CompareTo(currentDay.AddDays(1)) <= 0).ToList();
                            decimal decTotalSupply = 0;
                            decStock += decTotalSupply;
                            if (decStock > 0)
                            {
                                //reduce delivery
                                foreach (var drowDelivery in arrDeliveries)
                                {
                                    decimal decDeliveryQty = drowDelivery.Quantity;

                                    if (decDeliveryQty < decStock)
                                    {
                                        drowDelivery.Quantity = 0;
                                        decStock -= decDeliveryQty;
                                    }
                                    else
                                    {
                                        drowDelivery.Quantity = decDeliveryQty - decStock;
                                        decStock = 0;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                decStock = 0;
                                break;
                            }
                            currentDay = currentDay.AddDays(1);
                        }
                        decimal decSafetyStock = product.SafetyStock;
                        product.SafetyStock = decStock > decSafetyStock ? 0 : decSafetyStock - decStock;
                    }
                }
                #endregion Onhand-Produce Adjust

                //feed bottles
                #region Assign deliveries to bottles

                //select all delivery
                arrDeliveries = deliveryScheduleDatas.Where(d => d.WorkCenterId == workCenterId && d.Quantity > 0).OrderBy(d => d.ProductId).ThenByDescending(d => d.ScheduleDate).ToList();
                if (arrDeliveries.Count() > 0)
                {
                    var delivery = arrDeliveries.FirstOrDefault() ?? new DeliveryScheduleData();
                    int groupProductId = delivery.ProductId;
                    DateTime groupScheduleDate = delivery.ScheduleDate;
                    decimal decGroupQuantity = 0;
                    List<BOMData> arrChildProducts;
                    DateTime groupStartDate;
                    decimal decFixLeadTime;
                    int routingId;
                    var currentDelivery = new DeliveryScheduleData();
                    foreach (var deliverySchedule in arrDeliveries)
                    {
                        currentDelivery = deliverySchedule;
                        currentDelivery.CapacityBottleId = 1;
                        int currentProductId = currentDelivery.ProductId;
                        DateTime currentScheduleDate = currentDelivery.ScheduleDate;
                        if ((currentProductId != groupProductId) || (currentScheduleDate != groupScheduleDate))
                        {
                            decFixLeadTime = isOutside ? currentDelivery.FixLeadTime
                                                 : currentDelivery.LeadTime.GetValueOrDefault(0);
                            // ignore leadtime
                            groupStartDate = groupScheduleDate;
                            routingId = currentDelivery.RoutingId;
                            AddRawResult(rawResultDatas, groupProductId, 0, decGroupQuantity, 0, decFixLeadTime, routingId,
                                workCenterId, workCenterCode, 0, 0, 0, groupStartDate, 0, 0);

                            //generate child
                            arrChildProducts = bomDatas.Where(b => b.ProductId == groupProductId).ToList();
                            foreach (var childProduct in arrChildProducts)
                            {
                                decimal decShrink = childProduct.Shrink;
                                var childDelivery = new DeliveryScheduleData
                                                        {
                                                            Quantity = Decimal.Round(decGroupQuantity * childProduct.Quantity / (1 - decShrink / 100), 0),
                                                            ScheduleDate = groupStartDate.AddSeconds(-Convert.ToDouble(childProduct.LeadTimeOffset)),
                                                            ProductId = childProduct.ComponentId,
                                                            RoutingId = childProduct.WorkCenterId,
                                                            WorkCenterCode = childProduct.WorkCenterCode,
                                                            SamplePattern = childProduct.SamplePattern,
                                                            SampleRate = childProduct.SampleRate,
                                                            DelayTime = childProduct.DelayTime,
                                                            LeadTime = childProduct.LeadTime,
                                                            FixLeadTime = childProduct.FixLeadTime,
                                                            MinProduce = childProduct.MinProduce,
                                                            MaxProduce = childProduct.MaxProduce,
                                                            CheckpointPerItem = childProduct.CheckpointPerItem,
                                                            ScrapPercent = childProduct.ScrapPercent,
                                                            OrderQuantityMultiple = childProduct.OrderQuantityMultiple,
                                                            Revision = childProduct.Revision,
                                                            MaxRoundUpToMin = childProduct.MaxRoundUpToMin,
                                                            MaxRoundUpToMultiple = childProduct.MaxRoundUpToMultiple,
                                                            CapacityBottleId = -2
                                                        };
                                deliveryScheduleDatas.Add(childDelivery);
                            }

                            //begin another group
                            groupProductId = currentProductId;
                            groupScheduleDate = currentScheduleDate;
                            decGroupQuantity = 0;
                        }
                        decimal decCurrentQuantity = currentDelivery.Quantity;
                        decimal decScraptPercent = (decimal)currentDelivery.ScrapPercent;
                        //calculate scrapt percent
                        decGroupQuantity += Decimal.Round((decCurrentQuantity / (1 - decScraptPercent / 100)), 0);
                    }
                    decFixLeadTime = isOutside ? currentDelivery.FixLeadTime
                                         : currentDelivery.LeadTime.GetValueOrDefault(0);
                    // ignore leadtime
                    groupStartDate = groupScheduleDate;
                    routingId = currentDelivery.RoutingId;
                    AddRawResult(rawResultDatas, groupProductId, 0, decGroupQuantity, 0, decFixLeadTime, routingId,
                        workCenterId, workCenterCode, 0, 0, 0, groupStartDate, 0, 0);

                    //generate child
                    arrChildProducts = bomDatas.Where(b => b.ProductId == groupProductId).ToList();
                    foreach (var childProduct in arrChildProducts)
                    {
                        decimal decShrink = childProduct.Shrink;
                        var childDelivery = new DeliveryScheduleData
                                                {
                                                    Quantity =
                                                        Decimal.Round(
                                                            decGroupQuantity*childProduct.Quantity/(1 - decShrink/100),
                                                            0),
                                                    ScheduleDate = groupStartDate,
                                                    ProductId = childProduct.ComponentId,
                                                    RoutingId = childProduct.WorkCenterId,
                                                    WorkCenterCode = childProduct.WorkCenterCode,
                                                    SamplePattern = childProduct.SamplePattern,
                                                    SampleRate = childProduct.SampleRate,
                                                    DelayTime = childProduct.DelayTime,
                                                    LeadTime = childProduct.LeadTime,
                                                    FixLeadTime = childProduct.FixLeadTime,
                                                    MinProduce = childProduct.MinProduce,
                                                    MaxProduce = childProduct.MaxProduce,
                                                    CheckpointPerItem = childProduct.CheckpointPerItem,
                                                    ScrapPercent = childProduct.ScrapPercent,
                                                    OrderQuantityMultiple = childProduct.OrderQuantityMultiple,
                                                    Revision = childProduct.Revision,
                                                    MaxRoundUpToMin = childProduct.MaxRoundUpToMin,
                                                    MaxRoundUpToMultiple = childProduct.MaxRoundUpToMultiple,
                                                    CapacityBottleId = -2
                                                };
                        deliveryScheduleDatas.Add(childDelivery);
                    }
                }

                #endregion Assign deliveries to bottles
            }
            return 0;
        }

        /// <summary>
        /// Gets the on hand at as of date.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="futureSOs">The future Sale order.</param>
        /// <param name="futurePOs">The future Purchase order.</param>
        /// <param name="futureSupplyWOs">The future supply Work order.</param>
        /// <param name="futureDemandWOs">The future demand Work Order.</param>
        /// <param name="includeSO">if set to <c>true</c> [include SO].</param>
        /// <param name="useCacheAsBegin">if set to <c>true</c> [use cache as begin].</param>
        /// <returns></returns>
        private static decimal GetOnHandAtAsOfDate(int productId, List<OrderInfo> futureSOs, List<OrderInfo> futurePOs,
            List<OrderInfo> futureSupplyWOs, List<OrderInfo> futureDemandWOs, bool includeSO, bool useCacheAsBegin)
        {
            decimal decCurrentOH = 0; // TODO get current onhand from all bin
            if (useCacheAsBegin)
                return decCurrentOH;
            decimal decQty = decCurrentOH;
            decQty += futurePOs.Where(s => s.ProductId == productId).Sum(s => s.Quantity);
            decQty += futureSupplyWOs.Where(s => s.ProductId == productId).Sum(s => s.Quantity);
            decQty -= futureDemandWOs.Where(s => s.ProductId == productId).Sum(s => s.Quantity);
            if (includeSO)
                decQty -= futureSOs.Where(s => s.ProductId == productId).Sum(s => s.Quantity);
            return decQty;
        }

        private static Dictionary<int, List<OrderInfo>> GetDemandAndSupply(DateTime fromDate, DateTime toDate, Dictionary<int, List<OrderInfo>> dictionary, int workCenterId)
        {
            var futureSOs = dictionary[1];
            var futurePOs = dictionary[2];
            var futureSupplyWOs = dictionary[3];
            var futureDemandWOs = dictionary[4];
            var result = new Dictionary<int, List<OrderInfo>>();
            if (fromDate < toDate)
            {
                var so = from orderInfo in futureSOs
                         where orderInfo.DueDate.CompareTo(fromDate) >= 0
                               && orderInfo.DueDate.CompareTo(toDate) <= 0
                               && orderInfo.WorkCenterId == workCenterId 
                               select orderInfo;
                result.Add(1, so.ToList());
                var po = from orderInfo in futurePOs
                         where orderInfo.DueDate.CompareTo(fromDate) >= 0
                               && orderInfo.DueDate.CompareTo(toDate) <= 0
                               && orderInfo.WorkCenterId == workCenterId
                         select orderInfo;
                result.Add(2, po.ToList());
                var swo = from orderInfo in futureSupplyWOs
                         where orderInfo.DueDate.CompareTo(fromDate) >= 0
                               && orderInfo.DueDate.CompareTo(toDate) <= 0
                               && orderInfo.WorkCenterId == workCenterId
                         select orderInfo;
                result.Add(3, swo.ToList());
                var dwo = from orderInfo in futureDemandWOs
                         where orderInfo.DueDate.CompareTo(fromDate) >= 0
                               && orderInfo.DueDate.CompareTo(toDate) <= 0
                               && orderInfo.WorkCenterId == workCenterId
                         select orderInfo;
                result.Add(4, dwo.ToList());
            }
            return result;            
        }


        /// <summary>
        /// Sorts the work center list.
        /// </summary>
        /// <param name="topLevelWorkCenter">The top level work center.</param>
        /// <param name="workCenterList">The work center list.</param>
        /// <param name="bomList">The bom list.</param>
        private static void SortWorkCenterList(List<MST_WorkCenter> topLevelWorkCenter, List<WorkCenterData> workCenterList, List<BOMData> bomList)
        {
            var workCenterStack = new Stack();
            foreach (var topWorkCenter in topLevelWorkCenter)
            {
                MST_WorkCenter center = topWorkCenter;
                workCenterStack.Push(center.WorkCenterID);
                var workCenter = workCenterList.FirstOrDefault(w => w.WorkCenterId == center.WorkCenterID);
                if (workCenter != null)
                {
                    workCenter.WorkCenterLevel = 0;
                }
            }

            string workCenterAncessors;
            while (workCenterStack.Count > 0)
            {
                var workCenterId = (int)(workCenterStack.Pop());
                var workCenter = workCenterList.FirstOrDefault(w => w.WorkCenterId == workCenterId) ?? new WorkCenterData();
                var workCenterLevel = workCenter.WorkCenterLevel;
                workCenterAncessors = workCenter.WorkCenterAncessors ?? string.Empty;

                //push all child
                var children = bomList.Where(b => b.ParentWorkCenterId == workCenterId);
                foreach (var child in children)
                {
                    BOMData workCenterChild = child;
                    if (workCenterList.Any(w => w.WorkCenterId == workCenterChild.WorkCenterId))
                    {
                        continue;
                    }
                    var workCenterInList = workCenterList.FirstOrDefault(w => w.WorkCenterId == workCenterChild.WorkCenterId) ?? new WorkCenterData();

                    if (workCenterInList.WorkCenterLevel >= workCenterLevel + 1)
                    {
                        continue;
                    }

                    string[] ancessors = workCenterAncessors.Split(',');
                    // is cyclic
                    if (ancessors.Any(a => a.Equals(workCenterChild.WorkCenterId.ToString())))
                    {
                        continue;
                    }

                    workCenterInList.WorkCenterLevel = workCenterLevel + 1;
                    workCenterInList.WorkCenterAncessors = workCenterAncessors + "," + workCenterId;
                    if (!workCenterStack.Contains(workCenterChild.WorkCenterId))
                    {
                        workCenterStack.Push(workCenterChild.WorkCenterId);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the raw result.
        /// </summary>
        /// <param name="rawResultList">The raw result list.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="bottleId">The bottle id.</param>
        /// <param name="produceQuantity">The produce quantity.</param>
        /// <param name="produceOrder">The produce order.</param>
        /// <param name="leadTime">The lead time.</param>
        /// <param name="routingId">The routing id.</param>
        /// <param name="workCenterId">The work center id.</param>
        /// <param name="workCenterCode">The work center code.</param>
        /// <param name="samplePattern">The sample pattern.</param>
        /// <param name="sampleRate">The sample rate.</param>
        /// <param name="delayTime">The delay time.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="checkpointPerItem">The checkpoint per item.</param>
        /// <param name="safetyStockAmount">The safety stock amount.</param>
        private static void AddRawResult(List<RawResultData> rawResultList, int productId, int bottleId, decimal produceQuantity, int produceOrder,
            decimal leadTime, int routingId, int workCenterId, string workCenterCode, int samplePattern, decimal sampleRate, decimal delayTime,
            DateTime startTime, decimal checkpointPerItem, decimal safetyStockAmount)
        {
            if (produceQuantity <= 0)
            {
                return;
            }
            var rawResult = new RawResultData
                                {
                                    ProductId = productId,
                                    CapacityBottleId = bottleId,
                                    ProduceQuantity = produceQuantity,
                                    ProduceOrder = produceOrder,
                                    LeadTime = leadTime,
                                    RoutingId = routingId,
                                    WorkCenterId = workCenterId,
                                    WorkCenterCode = workCenterCode,
                                    SamplePattern = (byte)samplePattern,
                                    SampleRate = sampleRate,
                                    DelayTime = delayTime,
                                    StartTime = startTime,
                                    CheckpointPerItem = checkpointPerItem,
                                    SafetyStockAmount = safetyStockAmount
                                };
            rawResultList.Add(rawResult);
        }

        /// <summary>
        /// Writes the final result.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="dcOptionMasterId">The dc option master id.</param>
        /// <param name="rawResultList">The raw result list.</param>
        /// <param name="workCenterList">The work center list.</param>
        /// <param name="shifts">The shifts.</param>
        /// <param name="dcpResultMaster">The DCP result master.</param>
        private static void WriteFinalResult(PCSDataContext dataContext, int dcOptionMasterId, List<RawResultData> rawResultList, List<WorkCenterData> workCenterList,
            List<ShiftInfo> shifts, List<PRO_DCPResultMaster> dcpResultMaster)
        {
            // working time
            var workingTimes = GetWorkingTime(dataContext);
            foreach (var workCenter in workCenterList)
            {
                int workCenterId = workCenter.WorkCenterId;
                int productionLineId = workCenter.ProductionLineId;
                string departmentCode = workCenter.DepartmentCode;
                bool isOutside = (departmentCode.StartsWith(Outside, StringComparison.OrdinalIgnoreCase));
                if (isOutside)
                {
                    #region Outside processing

                    var arrRawResults = rawResultList.Where(r => r.WorkCenterId == workCenterId).OrderBy(r => r.StartTime);

                    foreach (var rawResult in arrRawResults)
                    {
                        decimal decDeliveryQuantity = rawResult.ProduceQuantity;
                        decimal decProductLeadTime = rawResult.LeadTime.GetValueOrDefault(0);
                        DateTime dtmStartTime = rawResult.StartTime;
                        DateTime dtmEndTime = dtmStartTime.AddSeconds(Convert.ToDouble(decProductLeadTime));
                        int productId = rawResult.ProductId;
                        DateTime dtmWorkingDate = GetRealWorkingDay(dtmStartTime, workingTimes);
                        var resultMaster = new PRO_DCPResultMaster
                                               {
                                                   DCOptionMasterID = dcOptionMasterId,
                                                   Quantity = decimal.Floor(decDeliveryQuantity),
                                                   StartDateTime = dtmStartTime,
                                                   WorkCenterID = workCenterId,
                                                   ProductID = productId,
                                                   RoutingID = rawResult.RoutingId,
                                                   DueDateTime = dtmEndTime,
                                                   PRO_DCPResultDetails = new EntitySet<PRO_DCPResultDetail>
                                                                              {
                                                                                  new PRO_DCPResultDetail
                                                                                      {
                                                                                          StartTime = dtmStartTime,
                                                                                          EndTime = dtmEndTime,
                                                                                          Quantity = decDeliveryQuantity,
                                                                                          Percentage = 100,
                                                                                          ShiftID = 2, // Shift 2S
                                                                                          TotalSecond = decimal.Floor(decProductLeadTime),
                                                                                          Type = (byte?)DCPResultTypeEnum.Running,
                                                                                          WorkingDate = dtmWorkingDate.Date
                                                                                      }
                                                                              }
                                               };

                        dcpResultMaster.Add(resultMaster);
                    }

                    #endregion
                }
                else
                {
                    #region Inside processing

                    var arrRawResults = rawResultList.Where(r => r.WorkCenterId == workCenterId).OrderBy(r => r.StartTime);

                    foreach (var drowRawResult in arrRawResults)
                    {
                        //master
                        decimal decDeliveryQuantity = drowRawResult.ProduceQuantity;
                        decimal decProductLeadTime = drowRawResult.LeadTime.GetValueOrDefault(0);
                        DateTime dtmStartTime = drowRawResult.StartTime;
                        var drowShifts = shifts.Where(s => s.ProductionLineId == productionLineId
                                                           && s.BeginDate.CompareTo(dtmStartTime.Date) <= 0
                                                           && s.EndDate.CompareTo(dtmStartTime.Date) >= 0);
                        int shiftId;
                        if (drowShifts.Count() == 1)
                        {
                            var firstShift = drowShifts.FirstOrDefault();
                            shiftId = firstShift.ShiftId;
                            DateTime dtmStartShift = firstShift.WorkTimeFrom.GetValueOrDefault(DateTime.Now);
                            dtmStartTime = new DateTime(dtmStartTime.Year, dtmStartTime.Month, dtmStartTime.Day, dtmStartShift.Hour, dtmStartShift.Minute, dtmStartShift.Second);
                        }
                        else
                        {
                            shiftId = 2;
                            dtmStartTime = new DateTime(dtmStartTime.Year, dtmStartTime.Month, dtmStartTime.Day, 14, 50, 0);
                        }
                        DateTime dtmEndTime = dtmStartTime.AddSeconds(Convert.ToDouble(decProductLeadTime * decDeliveryQuantity));
                        int productId = drowRawResult.ProductId;

                        var resultMaster = new PRO_DCPResultMaster
                                               {
                                                   DCOptionMasterID = dcOptionMasterId,
                                                   Quantity = decimal.Floor(decDeliveryQuantity),
                                                   StartDateTime = dtmStartTime,
                                                   WorkCenterID = workCenterId,
                                                   ProductID = productId,
                                                   RoutingID = drowRawResult.RoutingId,
                                                   DueDateTime = dtmEndTime,
                                                   PRO_DCPResultDetails = new EntitySet<PRO_DCPResultDetail>
                                                                              {
                                                                                  new PRO_DCPResultDetail
                                                                                      {
                                                                                          StartTime = dtmStartTime,
                                                                                          EndTime = dtmEndTime,
                                                                                          Quantity = decDeliveryQuantity,
                                                                                          Percentage = 100,
                                                                                          ShiftID = shiftId,
                                                                                          TotalSecond = decimal.Floor(decDeliveryQuantity * decProductLeadTime),
                                                                                          Type = (byte?)DCPResultTypeEnum.Running,
                                                                                          WorkingDate = dtmStartTime.Date
                                                                                      }
                                                                              }
                                               };
                        
                        dcpResultMaster.Add(resultMaster);
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// Gets the working time.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <returns></returns>
	    private static List<PRO_ShiftPattern> GetWorkingTime(PCSDataContext dataContext)
	    {
	        string[] shiftDesc = new[] {"1S", "2S", "3S"};
	        return (from pattern in dataContext.PRO_ShiftPatterns
	                join shift in dataContext.PRO_Shifts on pattern.ShiftID equals shift.ShiftID
	                where shiftDesc.Contains(shift.ShiftDesc)
	                select pattern).ToList();
	    }

	    /// <summary>
        /// Res the calculate lead time.
        /// </summary>
        /// <param name="deliverySchedules">The delivery schedules.</param>
        /// <param name="bomInfos">The bom infos.</param>
        /// <param name="productInfos">The product infos.</param>
        /// <param name="includeCheckpoint">if set to <c>true</c> [include checkpoint].</param>
        private static void ReCalculateLeadTime(List<DeliveryScheduleData> deliverySchedules, List<BOMData> bomInfos, List<ProductInfo> productInfos, bool includeCheckpoint)
        {
            foreach (DeliveryScheduleData schedule in deliverySchedules)
                UpdateCheckPoint(schedule, includeCheckpoint);
            foreach (var bomInfo in bomInfos)
                UpdateCheckPoint(bomInfo, includeCheckpoint);
            foreach (var productInfo in productInfos)
                UpdateCheckPoint(productInfo, includeCheckpoint);
        }

        /// <summary>
        /// Updates the check point.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="includeCheckpoint">if set to <c>true</c> [include checkpoint].</param>
        private static void UpdateCheckPoint(BaseInfo info, bool includeCheckpoint)
        {
            try
            {
                int intSamplePattern = info.SamplePattern;
                decimal decSampleRate = info.SampleRate;
                decimal decDelayTime = info.DelayTime;
                decimal decLeadTime = info.LeadTime.GetValueOrDefault(0);
                decimal decCheckpointTime = 0;
                switch (intSamplePattern)
                {
                    case 1: //CHECKPOINT_BY_QTY:
                        decCheckpointTime = decDelayTime / decSampleRate;
                        break;
                    case 2: //CHECKPOINT_BY_TIME:
                        decCheckpointTime = decDelayTime / (decSampleRate / decLeadTime);
                        break;
                }
                if (!includeCheckpoint)
                    decCheckpointTime = 0;
                info.CheckpointPerItem = decCheckpointTime;
            }
            catch
            {
                info.CheckpointPerItem = 0;
            }
        }

        #endregion

        #endregion
    }
}
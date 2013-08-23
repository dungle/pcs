using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using PCSComMaterials.Inventory.DS;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.BO
{
	public class DCPReportBO
	{
        private const string THIS = "PCSComProduction.DCP.BO.DCPReportBO";

	    /// <summary>
		/// Gets Cycle Detail from Master
		/// </summary>
		/// <param name="pintCycleMasterID">Cycle Master ID</param>
		/// <returns>Cycle Detail</returns>
	
		public DataTable GetCycleDetail(int pintCycleMasterID)
		{
			PRO_DCOptionDetailDS dsDetail = new PRO_DCOptionDetailDS();
			return dsDetail.GetDetailByMaster(pintCycleMasterID).Tables[0];
		}
		/// <summary>
		/// Gets all Production Line
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetAllProductionLine(int pintCCNID)
		{
			PRO_ProductionLineDS dsProductionLine = new PRO_ProductionLineDS();
			return dsProductionLine.List(pintCCNID);
		}
		
		public DataTable GetTotalWO(string pstrCCNID)
		{
			DCPReportDS dsDCPReport = new DCPReportDS();
			return dsDCPReport.GetTotalWO(pstrCCNID);
		}

		/// <summary>
		/// Gets working time of work center
		/// </summary>
		/// <param name="pintProductionLineID">Production Line</param>
		/// <returns>DataTable</returns>
		public DataTable GetWorkingTime(int pintProductionLineID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetWorkingTime(pintProductionLineID);
		}
		/// <summary>
		/// Gets working time of work center
		/// </summary>
		/// <returns>DataTable</returns>
	    public DataTable GetWorkingTime()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetWorkingTime();
		}
		public ArrayList GetWorkingDayByYear(int pintYear)
		{
			UtilsDS dsUtil = new UtilsDS();
			return dsUtil.GetWorkingDayByYear(pintYear);
		}
		public ArrayList GetHolidaysInYear(int pintYear)
		{
			UtilsDS dsUtils = new UtilsDS();
			return dsUtils.GetHolidaysInYear(pintYear);
		}
		public DataTable ListProductionLine()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.ListProductionLine();
		}
	
		public DataTable ListProduct(string pstrCCNID, string pstrProductionLineList)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.ListProduct(pstrCCNID, pstrProductionLineList);
		}
	
		public DataTable GetPlanningOffset(string pstrCCNID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetPlanningOffset(pstrCCNID);
		}

		public DataTable GetBeginStockForReportData()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetBeginStockForReportData();
		}
	
		public DataTable GetBeginNetQuantity(string pstrCCNID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetBeginNetQuantity(pstrCCNID);
		}
	
		public DataTable GetTransactionHistory()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetTransactionHistory();
		}
	
		public DataTable GetDeliveryForSO()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetDeliveryForSO();
		}
	
		public DataTable GetProduce()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetProduce();
		}
	
		public DataTable GetDeliveryForParent(string pstrCCNID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetDemandWO(pstrCCNID);
		}
	
		public DataTable GetWorkingDateFromWCCapacity(int pintProductionLineID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetWorkingDateFromWCCapacity(pintProductionLineID);
		}
	
		public ArrayList GetPlanningPeriod(string pstrCCNID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetPlanningPeriod(pstrCCNID);
		}
	
		public DataTable GetCycles(string pstrCCNID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetCycles(pstrCCNID);
		}
	
		public void UpdateBeginStockData(DataSet pdstData)
		{
			DCPReportDS dsReport = new DCPReportDS();
			dsReport.UpdateBeginStockData(pdstData);
		}
	
		public object GetCyclerMasterObject(int pintMasterID)
		{
			PRO_DCOptionMasterDS dsOption = new PRO_DCOptionMasterDS();
			DataRow drowData = dsOption.GetDCOptionMaster(pintMasterID);
			PRO_DCOptionMasterVO voOption = new PRO_DCOptionMasterVO();
			try
			{
				voOption.DCOptionMasterID = Convert.ToInt32(drowData[PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD]);
			}
			catch{}
			try
			{
				voOption.AsOfDate = Convert.ToDateTime(drowData[PRO_DCOptionMasterTable.ASOFDATE_FLD]);
			}
			catch{}
			try
			{
				voOption.UseCacheAsBegin = Convert.ToBoolean(drowData[PRO_DCOptionMasterTable.USECACHE_ASBEGIN_FLD]);
			}
			catch{}
			voOption.CCNID = Convert.ToInt32(drowData[PRO_DCOptionMasterTable.CCNID_FLD]);
			try
			{
				voOption.LastUpdate = Convert.ToDateTime(drowData[PRO_DCOptionMasterTable.LASTUPDATE_FLD]);
			}
			catch{}
			try
			{
				voOption.PlanHorizon = Convert.ToInt32(drowData[PRO_DCOptionMasterTable.PLANHORIZON_FLD]);
			}
			catch{}
			try
			{
				voOption.PlanningPeriod = Convert.ToDateTime(drowData[PRO_DCOptionMasterTable.PLANNINGPERIOD_FLD]);
			}
			catch{}
			try
			{
				voOption.Version = Convert.ToInt32(drowData[PRO_DCOptionMasterTable.VERSION_FLD]);
			}
			catch{}
			return voOption;
		}

		/// <summary>
		/// Get begin data of cycle
		/// </summary>
		/// <param name="pintCycleId"></param>
		/// <returns></returns>
		public DataTable GetBeginData(int pintCycleId)
		{
			PRO_DCOptionMasterDS dsDCOptionMaster = new PRO_DCOptionMasterDS();
			return dsDCOptionMaster.GetBeginData(pintCycleId);
		}
		/// <summary>
		/// Get begin balance in OK and Buffer bin
		/// </summary>
		/// <param name="pdtmMonth">Effect date</param>
		/// <returns></returns>
		public DataTable GetBeginBalance(DateTime pdtmMonth)
		{
			IV_BalanceBinDS dsBalance = new IV_BalanceBinDS();
			return dsBalance.GetBeginBalance(pdtmMonth);
		}

        /// <summary>
        ///     get begin stock of selected date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<IV_BeginDCPReport> GetBeginStock(DateTime date)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            // get begin stock of previous month and current month
            return dataContext.IV_BeginDCPReports.Where(b => b.EffectDate == date).ToList();
        }

        public PRO_PlanningOffset GetPlanningOffset(int cycleId)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.PRO_PlanningOffsets.FirstOrDefault(b => b.DCOptionMasterID == cycleId);
        }

        public List<PRO_DCOptionMaster> ListCycle(DateTime year)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.PRO_DCOptionMasters.Where(c => c.AsOfDate.GetValueOrDefault(DateTime.Now).Year == year.Year).ToList();
        }

        public List<PRO_ProductionLine> GetProductionLine()
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.PRO_ProductionLines.ToList();
        }

        public List<ITM_Product> ListProduct(List<int> productionLines)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var result = new List<ITM_Product>();
            foreach (var productionLine in productionLines)
            {
                result.AddRange(dataContext.ITM_Products.Where(p => p.ProductionLineID == productionLine));
            }
            return result;
        }

        public List<DeliveryScheduleData> GetDeliveryForParent(DateTime fromDate, DateTime toDate)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var query = from dcpDetail in dataContext.PRO_DCPResultDetails
                        join dcpMaster in dataContext.PRO_DCPResultMasters on dcpDetail.DCPResultMasterID equals
                            dcpMaster.DCPResultMasterID
                        join optionMaster in dataContext.PRO_DCOptionMasters on dcpMaster.DCOptionMasterID equals optionMaster.DCOptionMasterID 
                        join bom in dataContext.ITM_BOMs on dcpMaster.ProductID equals bom.ProductID
                        join workCenter in dataContext.MST_WorkCenters on dcpMaster.WorkCenterID equals workCenter.WorkCenterID
                        where optionMaster.AsOfDate <= toDate
                        && optionMaster.AsOfDate >= fromDate
                        select new DeliveryScheduleData
                        {
                            Quantity = (dcpDetail.Quantity * bom.Quantity.GetValueOrDefault(0)) / ((100 - bom.Shrink.GetValueOrDefault(0)) / 100),
                            ProductId = bom.ComponentID,
                            ScheduleDate = dcpDetail.WorkingDate.GetValueOrDefault(DateTime.Now),
                            StartTime = dcpDetail.StartTime,
                            EndTime = dcpDetail.EndTime,
                            LeadTime = bom.LeadTimeOffset,
                            WorkCenterId = workCenter.WorkCenterID
                        };
            return query.ToList();
        }

        public List<PRO_WCCapacity> GetWorkingDateFromWCCapacity(List<int> productionLineId)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var query = from wcCapacity in dataContext.PRO_WCCapacities
                        join workCenter in dataContext.MST_WorkCenters on wcCapacity.WorkCenterID equals
                            workCenter.WorkCenterID
                        where workCenter.IsMain.GetValueOrDefault(false) &&
                            productionLineId.Contains(workCenter.ProductionLineID.GetValueOrDefault(0))
                        select wcCapacity;
            return query.ToList();
        }

        public List<DeliveryScheduleData> GetDeliveryForSale(DateTime fromDate, DateTime toDate)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var query = dataContext.SO_DeliverySchedules.Where(d => d.ScheduleDate >= fromDate && d.ScheduleDate < toDate).Select(
                d =>
                new DeliveryScheduleData {ProductId = d.SO_SaleOrderDetail.ProductID, Quantity = (decimal)d.DeliveryQuantity});
            return query.ToList();
        }

        public List<DeliveryScheduleData> GetProduce(DateTime fromDate, DateTime toDate)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var query = from dcpDetail in dataContext.PRO_DCPResultDetails
                        join dcpMaster in dataContext.PRO_DCPResultMasters on dcpDetail.DCPResultMasterID equals
                            dcpMaster.DCPResultMasterID
                        join optionMaster in dataContext.PRO_DCOptionMasters on dcpMaster.DCOptionMasterID equals optionMaster.DCOptionMasterID
                        join workCenter in dataContext.MST_WorkCenters on dcpMaster.WorkCenterID equals workCenter.WorkCenterID
                        where optionMaster.AsOfDate <= toDate
                        && optionMaster.AsOfDate >= fromDate
                        select new DeliveryScheduleData
                        {
                            Quantity = dcpDetail.Quantity,
                            ProductId = dcpMaster.ProductID.GetValueOrDefault(0),
                            StartTime = dcpDetail.StartTime,
                            EndTime = dcpDetail.EndTime
                        };
            return query.ToList();
        }

        /// <summary>
        /// Gets the working time.
        /// </summary>
        /// <returns></returns>
        public List<PRO_ShiftPattern> ListWorkingTime()
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            var shiftDesc = new[] { "1S", "2S", "3S" };
            return (from pattern in dataContext.PRO_ShiftPatterns
                    join shift in dataContext.PRO_Shifts on pattern.ShiftID equals shift.ShiftID
                    where shiftDesc.Contains(shift.ShiftDesc)
                    select pattern).ToList();
        }

        public void UpdateBeginStock(List<IV_BeginDCPReport> beginDcp, DateTime effectDate, List<ITM_Product> productList)
        {
            const string methodName = THIS + ".UpdateBeginStock()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var productids = productList.Select(p => p.ProductID.ToString()).ToArray();
                        var query = string.Join(",", productids);
                        var deleteCommand = string.Format("DELETE FROM IV_BeginDCPReport WHERE effectDate = '{0}' AND ProductID IN ({1})", effectDate.ToString("yyyy-MM-dd"), query);

                        dataContext.ExecuteCommand(deleteCommand);
                        dataContext.IV_BeginDCPReports.InsertAllOnSubmit(beginDcp);

                        // submit changes
                        dataContext.SubmitChanges();
                        trans.Complete();
                    }
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
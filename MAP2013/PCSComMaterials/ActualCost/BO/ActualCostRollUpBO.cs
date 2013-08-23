using System;
using System.Data;

using PCSComMaterials.Inventory.DS;
using PCSComProduct.Items.DS;
using PCSComProduct.STDCost.DS;
using PCSComUtils.Common;

using PCSComMaterials.ActualCost.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComMaterials.ActualCost.BO
{
	public interface IActualCostRollUpBO
	{
		DataTable GetTopItems(int pintCCNID);
		DataTable GetBOM(int pintCCNID);
		DataTable GetWOBOM(int pintCCNID);
		DataTable GetCostElements(int pintCCNID);
		DataTable GetAllItems(int pintCCNID);
		DataTable GetBeginQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, DataTable pdtbAllItems, bool pblnIsFirstPeriod);
		DataTable GetSTDCost(int pintCCNID);
		DataTable GetCostOfPeriod(int pintPeriodID);
		DataTable GetRecoverableQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetScrapQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetWOCompletedQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetPOReceipt(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetPOReceiptByInvoice(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetAdjusment(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetAllocationAmount(int pintPeriodID);
		DataTable GetExchangeRate(int pintCCNID);
		bool IsRollup(int pintPeriodID);
		void SaveData(DataSet pdstData, object pobjPeriod);
		object GetPeriod(int pintPeriodID);
		object GetPeriod(DateTime pdtmToDate);
		DataTable FindLastPeriodHasCost(DateTime pdtmFromDate);
		DataTable GetReturnGoodsReceipt(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetDataFromMiscIssue(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetOnhandCacheQuantity(int pintCCNID);
		DataTable GetCompletionQuantityForComponent(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetDSCacheQuantity(int pintCCNID);
		DataTable GetShipTransaction(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetReturnToVendor(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetCostFromCostCenterRate(int pintCCNID);
		DataTable GetAdditionalCharge(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetShipAdjustment(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		DataTable GetComponentScrap(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate);
		void ChargeAllocation(DataSet pdstData, object pobjPeriod);
		DataTable ListCategoryForProduct();
		DataTable ListProduct(int pintCCNID);
		DataTable ListItemNotSold(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCycleID);
		DataTable ListItemCostPrice(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCycleID);
	}
	/// <summary>
	/// Summary description for ActualCostRollUpBO.
	/// </summary>
	
	public class ActualCostRollUpBO : IActualCostRollUpBO
	{
		public ActualCostRollUpBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			CST_ActCostAllocationMasterVO voPeriod = (CST_ActCostAllocationMasterVO)pObjectVO;
			dsActualCost.Delete(voPeriod.ActCostAllocationMasterID);
		}

		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			dsActualCost.UpdateDataSet(dstData);
		}

		/// <summary>
		/// Update into Database
		/// </summary>
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
	
		public object GetPeriod(int pintPeriodID)
		{
			CST_ActCostAllocationMasterDS dsPeriod = new CST_ActCostAllocationMasterDS();
			return dsPeriod.GetObjectVO(pintPeriodID);
		}
	
		public object GetPeriod(DateTime pdtmToDate)
		{
			CST_ActCostAllocationMasterDS dsPeriod = new CST_ActCostAllocationMasterDS();
			return dsPeriod.GetObjectVO(pdtmToDate);
		}
		/// <summary>
		/// Gets top level items of CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>Top Level Items</returns>
	
		public DataTable GetTopItems(int pintCCNID)
		{
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			return dsBOM.GetTopLevelItem(pintCCNID);
		}
		/// <summary>
		/// Gets all Item BOM structure of system in a CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>Item BOM structure</returns>
	
		public DataTable GetBOM(int pintCCNID)
		{
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			return dsBOM.GetBOMStructure(pintCCNID);
		}
		/// <summary>
		/// Gets BOM structure from Work Order
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>Work Order BOM structure</returns>
	
		public DataTable GetWOBOM(int pintCCNID)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetWOBOM(pintCCNID);
		}
		/// <summary>
		/// List all cost elements of CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>Cost Elements</returns>
	
		public DataTable GetCostElements(int pintCCNID)
		{
			STD_CostElementDS dsCostElement = new STD_CostElementDS();
			return dsCostElement.ListAll();
		}
		/// <summary>
		/// List all Items of CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>All Item</returns>
	
		public DataTable GetAllItems(int pintCCNID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.List(pintCCNID);
		}
		/// <summary>
		/// Calculate Begin quantity from transaction history and onhand quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <param name="pdtbAllItems">All Items</param>
		/// <returns>List of Item which have begin quantity</returns>
	
		public DataTable GetBeginQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate, DataTable pdtbAllItems, bool pblnIsFirstPeriod)
		{
			MST_TransactionHistoryDS dsTranHis = new MST_TransactionHistoryDS();
			IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
			// in quantity
			DataTable dtbInQuantity = dsTranHis.GetInQuantity(pintCCNID, pdtmFromDate, pdtmToDate);
			DataTable dtbResult = dtbInQuantity.Clone();
			// out quantity
			DataTable dtbOutQuantity = dsTranHis.GetOutQuantity(pintCCNID, pdtmFromDate, pdtmToDate);
			// onhand quantity
			DataTable dtbOHQuantity = dsMasLocCache.GetQuantityOnHand(pintCCNID, pblnIsFirstPeriod);
			foreach (DataRow drowItem in pdtbAllItems.Rows)
			{
				string strProductID = drowItem[ITM_ProductTable.PRODUCTID_FLD].ToString();
				string strFilter = ITM_ProductTable.PRODUCTID_FLD + "='" + strProductID + "'";
				decimal decInQty = 0, decOutQty = 0, decOHQty = 0, decBeginQty = 0;
				DataRow[] drowsIN = dtbInQuantity.Select(strFilter);
				DataRow[] drowsOut = dtbOutQuantity.Select(strFilter);
				DataRow[] drowsOH = dtbOHQuantity.Select(strFilter);
				if (drowsIN.Length == 0 && drowsOut.Length == 0 && drowsOH.Length == 0)
					continue;
				foreach (DataRow drowData in drowsIN)
				{
					try
					{
						decInQty += Convert.ToDecimal(drowData[MST_TransactionHistoryTable.QUANTITY_FLD]);
					}
					catch{}
				}
				foreach (DataRow drowData in drowsOut)
				{
					try
					{
						decOutQty += Convert.ToDecimal(drowData[MST_TransactionHistoryTable.QUANTITY_FLD]);
					}
					catch{}
				}
				foreach (DataRow drowData in drowsOH)
				{
					try
					{
						decOHQty += Convert.ToDecimal(drowData[IV_MasLocCacheTable.OHQUANTITY_FLD]);
					}
					catch{}
				}
				// Begin quantity = Onhand - In + Out
				decBeginQty = decOHQty - decInQty + decOutQty;
				if (decBeginQty != 0)
				{
					DataRow drowBegin = dtbResult.NewRow();
					drowBegin[ITM_ProductTable.PRODUCTID_FLD] = drowItem[ITM_ProductTable.PRODUCTID_FLD];
					drowBegin[MST_TransactionHistoryTable.QUANTITY_FLD] = decBeginQty;
					dtbResult.Rows.Add(drowBegin);
				}
			}
			return dtbResult;
		}
		/// <summary>
		/// Get standard cost of all item in CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>STD Cost of Items</returns>
	
		public DataTable GetSTDCost(int pintCCNID)
		{
			CST_STDItemCostDS dsSTDCost = new CST_STDItemCostDS();
			return dsSTDCost.List(pintCCNID);
		}
		/// <summary>
		/// Gets cost of a period
		/// </summary>
		/// <param name="pintPeriodID">Period ID</param>
		/// <returns>Cost</returns>
	
		public DataTable GetCostOfPeriod(int pintPeriodID)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.List(pintPeriodID);
		}
		/// <summary>
		/// Gets all recoverable quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>Recoverable Materials</returns>
	
		public DataTable GetRecoverableQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_RecoverMaterialDetailDS dsRecoverable = new CST_RecoverMaterialDetailDS();
			return dsRecoverable.List(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// Gets scrap quantity in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>Scrap Materials</returns>
	
		public DataTable GetScrapQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			IV_MiscellaneousIssueDetailDS dsMisc = new IV_MiscellaneousIssueDetailDS();
			return dsMisc.GetDestroyQuantity(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// Gets all completed quantity from work order
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
	
		public DataTable GetWOCompletedQuantity(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetCompletionQuantity(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetCompletionQuantityForComponent(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetCompletionQuantityForComponent(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// List all PO Receipt transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
	
		public DataTable GetPOReceipt(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetPOReceipt(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// List all PO Receipt by invoice transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
	
		public DataTable GetPOReceiptByInvoice(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetPOReceiptByInvoice(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// List all adjustment transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
	
		public DataTable GetAdjusment(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			IV_AdjustmentDS dsAdjustment = new IV_AdjustmentDS();
			return dsAdjustment.List(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// Get Additional Charge (including Freight, Import Tax, Credit Note, Debit Note)
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
	
		public DataTable GetAdditionalCharge(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			cst_FreightDetailDS dsFreight = new cst_FreightDetailDS();
			return dsFreight.ListAll(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// Gets allocation amount of period
		/// </summary>
		/// <param name="pintPeriodID">Period</param>
		/// <returns>Allocation Amount</returns>
	
		public DataTable GetAllocationAmount(int pintPeriodID)
		{
			CST_AllocationResultDS dsAllocation = new CST_AllocationResultDS();
			return dsAllocation.List(pintPeriodID);
		}
		/// <summary>
		/// Gets exchange rate in CCN
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <returns>Exchange Rates</returns>
	
		public DataTable GetExchangeRate(int pintCCNID)
		{
			MST_ExchangeRateDS dsExchangeRate = new MST_ExchangeRateDS();
			return dsExchangeRate.List(pintCCNID);
		}
		/// <summary>
		/// Check if period is rollup or not
		/// </summary>
		/// <param name="pintPeriodID">PeriodID</param>
		/// <returns>True: already roll, False if failure</returns>
	
		public bool IsRollup(int pintPeriodID)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.IsRollup(pintPeriodID);
		}
	
		public void SaveData(DataSet pdstData, object pobjPeriod)
		{
			CST_ActCostAllocationMasterVO voPeriod = (CST_ActCostAllocationMasterVO)pobjPeriod;
			// delete old cost first
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			try
			{
				dsActualCost.Delete(voPeriod.ActCostAllocationMasterID);
			}
			catch
			{
				throw new PCSBOException(ErrorCode.MESSAGE_CAN_NOT_DELETE, "SaveData", null);
			}
			// update actual cost
			dsActualCost.UpdateDataSet(pdstData);
			// update period rollupdate
			CST_ActCostAllocationMasterDS dsPeriod = new CST_ActCostAllocationMasterDS();
			dsPeriod.Update(pobjPeriod);
		}

		/// <summary>
		/// Find the last period which have item cost to use as beginning cost of current period
		/// </summary>
		/// <param name="pdtmFromDate">From Date of Current Period</param>
		/// <returns>periods which have item cost</returns>
	
		public DataTable FindLastPeriodHasCost(DateTime pdtmFromDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.FindLastPeriodHasCost(pdtmFromDate);
		}
		/// <summary>
		/// Gets return goods receipt transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetReturnGoodsReceipt(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetReturnGoodsReceipt(pintCCNID, pdtmFromDate, pdtmToDate);
		}
		/// <summary>
		/// Gets all data from misc. issue transaction in period of time
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>DataTable</returns>
	
		public DataTable GetDataFromMiscIssue(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			IV_MiscellaneousIssueDetailDS dsMiscIssue = new IV_MiscellaneousIssueDetailDS();
			return dsMiscIssue.List(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetOnhandCacheQuantity(int pintCCNID)
		{
			IV_MasLocCacheDS dsMasLocCache = new IV_MasLocCacheDS();
			return dsMasLocCache.GetOnhandQuantity(pintCCNID);
		}
	
		public DataTable GetBalanceQuantity(DateTime pdtmDate)
		{
			IV_BalanceBinDS dsBalanceBin = new IV_BalanceBinDS();
			return dsBalanceBin.GetBalanceQuantity(pdtmDate);
		}
	
		public DataTable GetDSCacheQuantity(int pintCCNID)
		{
			IV_BinCacheDS dsBIN = new IV_BinCacheDS();
			return dsBIN.GetDSCacheQuantity(pintCCNID);
		}
	
		public DataTable GetShipTransaction(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetShipTransaction(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetShipAdjustment(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetShipAdjustment(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetReturnToVendor(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetReturnToVendor(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetCostFromCostCenterRate(int pintCCNID)
		{
			CST_ActualCostHistoryDS dsActualCost = new CST_ActualCostHistoryDS();
			return dsActualCost.GetCostingAdjustment(pintCCNID);
		}
		/// <summary>
		/// Get Component scrap
		/// </summary>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns></returns>
	
		public DataTable GetComponentScrap(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActual = new CST_ActualCostHistoryDS();
			return dsActual.GetComponentScrap(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public void ChargeAllocation(DataSet pdstData, object pobjPeriod)
		{
			CST_DSAndRecycleAllocationDS dsAlloc = new CST_DSAndRecycleAllocationDS();
			// delete old data first
			dsAlloc.Delete(((CST_ActCostAllocationMasterVO)pobjPeriod).ActCostAllocationMasterID);
			// update new data
			dsAlloc.UpdateDataSet(pdstData);
		}

	
		public DataTable ListCategoryForProduct()
		{
			ITM_CategoryDS dsCategory = new ITM_CategoryDS();
			return dsCategory.ListForProduct().Tables[0];
		}

	
		public DataTable ListProduct(int pintCCNID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.List(pintCCNID);
		}
	
		public DataTable ListItemNotSold(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCycleID)
		{
			CST_ActualCostHistoryDS dsActual = new CST_ActualCostHistoryDS();
			return dsActual.ListItemNotSold(pdtmFromDate, pdtmToDate, pintCycleID);
		}
	
		public DataTable ListItemCostPrice(DateTime pdtmFromDate, DateTime pdtmToDate, int pintCycleID)
		{
			CST_ActualCostHistoryDS dsActual = new CST_ActualCostHistoryDS();
			return dsActual.ListItemCostPrice(pdtmFromDate, pdtmToDate, pintCycleID);
		}
	
		public DataTable ChargeOHAmount(int pintCycleID)
		{
			CST_ActualCostHistoryDS dsActual = new CST_ActualCostHistoryDS();
			return dsActual.ChargeOHAmount(pintCycleID);
		}

	
		public DataTable GetCategorySoldInPeriod(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActual = new CST_ActualCostHistoryDS();
			return dsActual.GetCategorySoldInPeriod(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetItemSoldInPeriod(int pintCCNID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			CST_ActualCostHistoryDS dsActual = new CST_ActualCostHistoryDS();
			return dsActual.GetItemSoldInPeriod(pintCCNID, pdtmFromDate, pdtmToDate);
		}
	}
}

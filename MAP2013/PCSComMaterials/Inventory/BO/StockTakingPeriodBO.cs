//Using PCS'namespaces
using System;
using System.Data;

using PCSComMaterials.Inventory.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;

using PCSComUtils.MasterSetup.DS;

namespace PCSComMaterials.Inventory.BO
{
	public interface IStockTakingPeriodBO
	{
		void DeleteByID(int pintStockTakingPeriodID);
		int AddAndReturnID(object pObjectDetail);
		bool CheckIfDataWasUsed(int pintStockTakingPeriodID);
		DataSet GetStockTakingByPeriodID(int pintStockTakingPeriodID);
		decimal GetOnHandQtyInBinCache(int pintLocationID, int pintBinID, int pintProductID);
		int GetMasterLocationIDByLocationID(int pintLocationID);
	}

	/// <summary>
	/// Summary description for IVMaterialScrapBO.
	/// </summary>
	
	public class StockTakingPeriodBO : IStockTakingPeriodBO
	{
		public StockTakingPeriodBO()
		{
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
	
		public void Add(object pObjectDetail)
		{
			IV_StockTakingPeriodDS dsStockTakingPeriod = new IV_StockTakingPeriodDS();
			//add to database
			dsStockTakingPeriod.Add(pObjectDetail);
		}

	
		public int AddAndReturnID(object pObjectDetail)
		{
			IV_StockTakingPeriodDS dsStockTakingPeriod = new IV_StockTakingPeriodDS();
			//add to database
			return dsStockTakingPeriod.AddAndReturnID(pObjectDetail);
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			IV_StockTakingPeriodDS dsPeriod = new IV_StockTakingPeriodDS();
			return dsPeriod.GetObjectVO(pintID);
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			IV_StockTakingPeriodDS dsStockTakingPeriod = new IV_StockTakingPeriodDS();
			//update
			dsStockTakingPeriod.Update(pObjectDetail);
		}

		/// <summary>
		/// Delete by ID
		/// </summary>
		/// <param name="pintStockTakingPeriodID"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
	
		public void DeleteByID(int pintStockTakingPeriodID)
		{
			//delete Stock Taking

			//delete StockTakingPeriod
			IV_StockTakingPeriodDS dsStockTakingPeriod = new IV_StockTakingPeriodDS();
			dsStockTakingPeriod.Delete(pintStockTakingPeriodID);
		}

		/// <summary>
		/// Check if data of this Period was used by another transaction
		/// </summary>
		/// <param name="pintStockTakingPeriodID"></param>
		/// <returns>return false if was used, true if not</returns>
		/// <author>Trada</author>
		/// <date>Wednesday, July 26 2006</date>
	
		public bool CheckIfDataWasUsed(int pintStockTakingPeriodID)
		{
			IV_StockTakingDS dsIV_StockTaking = new IV_StockTakingDS();
			DataSet dstStockTaking = dsIV_StockTaking.GetStockTakingByPeriodID(pintStockTakingPeriodID);
			if (dstStockTaking.Tables[0].Rows.Count > 0)
			{
				return false;
			}
			else
				return true;
		}

		/// <summary>
		/// Get StockTaking by PeriodID
		/// </summary>
		/// <param name="pintStockTakingPeriodID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, August 14 2006</date>
	
		public DataSet GetStockTakingByPeriodID(int pintStockTakingPeriodID)
		{
			IV_StockTakingDS dsStockTaking = new IV_StockTakingDS();
			return dsStockTaking.GetStockTakingByPeriodID(pintStockTakingPeriodID);
		}

		/// <summary>
		/// Get On-hand Qty in bin cache
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, August 14 2006</date>
	
		public decimal GetOnHandQtyInBinCache(int pintLocationID, int pintBinID, int pintProductID)
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.GetQuantityOnHand(pintLocationID, pintBinID, pintProductID);
		}

		/// <summary>
		/// GetMasterLocationIDByLocationID
		/// </summary>
		/// <param name="pintLocationID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, August 18 2006</date>
	
		public int GetMasterLocationIDByLocationID(int pintLocationID)
		{
			MST_LocationDS dsLocation = new MST_LocationDS();
			return dsLocation.GetMasterLocationIDByLocationID(pintLocationID);
		}

	
		public void UpdateInventory(DataTable pdtbAdjustmentTable, DataTable pdtbCache, string pstrComment)
		{
			// update adjustment
			if (pdtbAdjustmentTable.Rows.Count > 0)
			{
				DataSet dstData = new DataSet();
				dstData.Tables.Add(pdtbAdjustmentTable);
				IV_AdjustmentDS dsAdjustment = new IV_AdjustmentDS();
				dsAdjustment.UpdateDataSet(dstData);
				IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
				DataSet dstCache = new DataSet();
				dstCache.Tables.Add(pdtbCache);
				dsBinCache.UpdateDataSetForTaking(dstCache);
				// update location and master location cache
				IV_LocationCacheDS dsLocation = new IV_LocationCacheDS();
				dsLocation.UpdateAllQuantityFromBin();
				// update transaction history
				InventoryUtilsBO boIVUtils = new InventoryUtilsBO();
				DataSet dstTransaction = boIVUtils.ListTransactionHistory(0);
				// get the list of new adjustment
				DataSet dstAdjust = dsAdjustment.List(pstrComment);
				foreach (DataRow drowData in dstAdjust.Tables[0].Rows)
				{
					// each row will be on transaction history
					#region Transaction history
					//SaveTransactionHistory
					DataRow drowTransaction = dstTransaction.Tables[0].NewRow();
					drowTransaction[MST_TransactionHistoryTable.CCNID_FLD] = drowData[IV_AdjustmentTable.CCNID_FLD];
					drowTransaction[MST_TransactionHistoryTable.TRANSDATE_FLD] = drowData[IV_AdjustmentTable.POSTDATE_FLD];
					drowTransaction[MST_TransactionHistoryTable.POSTDATE_FLD] = drowData[IV_AdjustmentTable.POSTDATE_FLD];
					drowTransaction[MST_TransactionHistoryTable.REFMASTERID_FLD] = drowData[IV_AdjustmentTable.ADJUSTMENTID_FLD];
					drowTransaction[MST_TransactionHistoryTable.PRODUCTID_FLD] = drowData[IV_AdjustmentTable.PRODUCTID_FLD];
					drowTransaction[MST_TransactionHistoryTable.TRANTYPEID_FLD] = 17;
					drowTransaction[MST_TransactionHistoryTable.USERNAME_FLD] = drowData[IV_AdjustmentTable.USERNAME_FLD];
					drowTransaction[MST_TransactionHistoryTable.QUANTITY_FLD] = drowData[IV_AdjustmentTable.ADJUSTQUANTITY_FLD];
					drowTransaction[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD] = drowData[IV_AdjustmentTable.MASTERLOCATIONID_FLD];
					drowTransaction[MST_TransactionHistoryTable.LOCATIONID_FLD] = drowData[IV_AdjustmentTable.LOCATIONID_FLD];
					drowTransaction[MST_TransactionHistoryTable.BINID_FLD] = drowData[IV_AdjustmentTable.BINID_FLD];
					drowTransaction[MST_TransactionHistoryTable.STOCKUMID_FLD] = drowData[IV_AdjustmentTable.STOCKUMID_FLD];
					dstTransaction.Tables[0].Rows.Add(drowTransaction);
					#endregion
				}
				MST_TransactionHistoryDS dsTransactionHistory = new MST_TransactionHistoryDS();
				dsTransactionHistory.UpdateDataSet(dstTransaction);
			}
		}
	
		public void UpdateDifferent(DataTable pdtbDiff)
		{
			// update to different table
			DataSet dstDataDiff = new DataSet();
			dstDataDiff.Tables.Add(pdtbDiff);
			IV_StockTakingDifferentDS dsDiff = new IV_StockTakingDifferentDS();
			dsDiff.UpdateDataSet(dstDataDiff);
		}
	
		public DataTable ListAllCache()
		{
			IV_BinCacheDS dsBinCache = new IV_BinCacheDS();
			return dsBinCache.ListAllCache();
		}
	
		public DataTable ListItemToUpdate(int pintPeriodID)
		{
			IV_StockTakingDS dsStockTaking = new IV_StockTakingDS();
			return dsStockTaking.ListItemToUpdate(pintPeriodID);
		}
	
		public DataTable GetAdjustmentSchema()
		{
			IV_AdjustmentDS dsAdjustment = new IV_AdjustmentDS();
			return dsAdjustment.GetSchema();
		}
	
		public DataTable GetStockTakingDifferent(int pintPeriodID)
		{
			IV_StockTakingDifferentDS dsDiff = new IV_StockTakingDifferentDS();
			return dsDiff.List(pintPeriodID);
		}

	
		public DataTable ListLocation()
		{
			MST_LocationDS dsLocation = new MST_LocationDS();
			return dsLocation.List().Tables[0];
		}
	
		public DataTable ListTransactionHistory(DateTime pdtmStockTakingDate)
		{
			MST_TransactionHistoryDS dsTrans = new MST_TransactionHistoryDS();
			return dsTrans.ListForUpdateStockTaking(pdtmStockTakingDate);
		}

        /// <summary>
        ///     Call store procedure to update cache, balance, close period (stock taking and working period)
        ///     then active next working period
        /// </summary>
        /// <param name="periodId">Stock Taking Period</param>
        /// <param name="stockTakingDate">Effect Date</param>
	    public void CloseStockTaking(int periodId, DateTime stockTakingDate)
	    {
            // update the bin cache and balance first
	        IV_StockTakingPeriodDS dsPeriod = new IV_StockTakingPeriodDS();
            dsPeriod.CloseStockTaking(periodId, stockTakingDate);
	    }

        public void UpdateBeginStock(int periodId, DateTime effectDate)
        {
            // update the bin cache and balance first
            IV_StockTakingPeriodDS dsPeriod = new IV_StockTakingPeriodDS();
            dsPeriod.UpdateBeginStock(periodId, effectDate);
        }
	}
}


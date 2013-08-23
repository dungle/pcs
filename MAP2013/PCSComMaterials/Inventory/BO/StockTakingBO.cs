using System;
using System.Data;


using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.Common.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComMaterials.Inventory.DS;

namespace PCSComMaterials.Inventory.BO
{
	public interface IStockTakingBO
	{
		void UpdateStockTaking(object pobjStockTakingPeriodVO, DataSet pdstData);		
		DataSet GetDataFromStockTaking(int pintStockTakingPeriodID);
		string FillLocation(int pintLocationID);
	}

	/// <summary>
	/// Summary description for IVMaterialScrapBO.
	/// </summary>
	
	public class StockTakingBO : IStockTakingBO
	{
		public StockTakingBO()
		{			
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
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// GetDataFromStockTaking
		/// </summary>
		/// <param name="pintStockTakingPeriodID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, July 24 2006</date>
	
		public DataSet GetDataFromStockTaking(int pintStockTakingPeriodID)
		{
			DataSet dstStockTaking = new DataSet();
			IV_StockTakingPeriodDS dsStockTakingPeriod = new IV_StockTakingPeriodDS();
			dstStockTaking = dsStockTakingPeriod.GetDataByStockTakingID(pintStockTakingPeriodID);
			return dstStockTaking;
		}
		/// <summary>
		/// fill Location by ProductionLineID
		/// </summary>
		/// <param name="pintLocationID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, August  15 2006</date>
	
		public string FillLocation(int pintLocationID)
		{
			MST_LocationDS dsLocation = new MST_LocationDS();
			return dsLocation.GetCodeFromID(pintLocationID);
		}
		/// <summary>
		/// UpdateStockTaking
		/// </summary>
		/// <param name="pobjStockTakingMasterVO"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, July 25 2006</date>
	
		public void UpdateStockTaking(object pobjStockTakingMasterVO, DataSet pdstData)
		{
			IV_StockTakingMasterVO voStockTakingMaster = (IV_StockTakingMasterVO) pobjStockTakingMasterVO;
			//Update StockTakingPeriod
			IV_StockTakingMasterDS dsStockTakingMaster = new IV_StockTakingMasterDS();
			dsStockTakingMaster.Update(voStockTakingMaster);
			pdstData.Tables[1].TableName = "IV_StockTaking";
			//Set value for StockTakingPeriodID column in the dataset
			foreach (DataRow drow in pdstData.Tables["IV_StockTaking"].Rows)
			{
				if (drow.RowState == DataRowState.Deleted) continue;
				drow[IV_StockTakingTable.STOCKTAKINGMASTERID_FLD] = voStockTakingMaster.StockTakingMasterID;
			}
			//update Stock Taking
			IV_StockTakingDS dsStockTaking = new IV_StockTakingDS();
			dsStockTaking.UpdateDataSet(pdstData);
		}
	
		public int AddNewStockTaking(object pobjStockTakingMasterVO, DataSet pdstData)
		{
			IV_StockTakingMasterVO voStockTakingMaster = (IV_StockTakingMasterVO) pobjStockTakingMasterVO;
			//Update StockTakingPeriod
			IV_StockTakingMasterDS dsStockTakingMaster = new IV_StockTakingMasterDS();
			voStockTakingMaster.StockTakingMasterID = dsStockTakingMaster.AddAndReturnID(voStockTakingMaster);
			//Set value for StockTakingPeriodID column in the dataset
			foreach (DataRow drow in pdstData.Tables[IV_StockTakingTable.TABLE_NAME].Rows)
			{
				if (drow.RowState == DataRowState.Deleted) continue;
				drow[IV_StockTakingTable.STOCKTAKINGMASTERID_FLD] = voStockTakingMaster.StockTakingMasterID;
			}
			//update Stock Taking
			IV_StockTakingDS dsStockTaking = new IV_StockTakingDS();
			dsStockTaking.UpdateDataSet(pdstData);
			return voStockTakingMaster.StockTakingMasterID;
		}
	
		public void DeleteStockTaking(int pintStockTakingMasterID)
		{
			IV_StockTakingDS dsStockTaking = new IV_StockTakingDS();
			dsStockTaking.DeleteStockTaking(pintStockTakingMasterID);
		}

		public DataTable GetQuantityFromBin(int pintLocationID,int pintBinID)
		{
			IV_StockTakingDS dsStockTaking = new IV_StockTakingDS();
			return dsStockTaking.GetQuantityFromBin(pintLocationID,pintBinID);
		}

	}
}


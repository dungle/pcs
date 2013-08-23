using System;
using System.Data;



//Using PCS's Namespaces
using PCSComMaterials.Inventory.BO;
using PCSComUtils.Common.BO;

using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSComMaterials.ActualCost.DS;

namespace PCSComMaterials.ActualCost.BO
{
	public interface IRecoverableMaterialBO
	{
		int AddAndReturnID(object pobjMasterVO, DataSet pdstData);		
		DataSet ListByMasterID(int pintMasterID);
		DataSet ListBomOfProduct(int pintProductID);
		object GetObjectMasterByID(int pintMasterID);
		bool HasMultiDestination(int pintMasterID);
		DataTable GetDataForSlip(int pintMasterID);
		DataSet GetMasterByID(int pintMasterID);
	}
	/// <summary>
	/// Summary description for RecoverMaterialBO.
	/// </summary>
	
	public class RecoverableMaterialBO : IRecoverableMaterialBO
	{
		public RecoverableMaterialBO()
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
	
		public DataSet ListByMasterID(int pintMasterID)
		{
			return new CST_RecoverMaterialDetailDS().ListByMasterID(pintMasterID);
		}
	
		public DataSet ListBomOfProduct(int pintProductID)
		{
			return new CST_RecoverMaterialDetailDS().ListBomDetailOfProduct(pintProductID);
		}
	
		public object GetObjectMasterByID(int pintMasterID)
		{
			return new CST_RecoverMaterialMasterDS().GetObjectVO(pintMasterID);
		}
	
		public DataSet GetMasterByID(int pintMasterID)
		{
			return new CST_RecoverMaterialMasterDS().ListByMasterID(pintMasterID);
		}
	
		public int AddAndReturnID(object pobjMasterVO, DataSet pdstData)
		{
			CST_RecoverMaterialMasterVO voRecoverMaterialMaster = new CST_RecoverMaterialMasterVO();
			voRecoverMaterialMaster = (CST_RecoverMaterialMasterVO) pobjMasterVO;
			int intMasterID = new CST_RecoverMaterialMasterDS().AddAndReturnID(pobjMasterVO);

			//update Detail
			for (int i =0; i <pdstData.Tables[0].Rows.Count; i++)
			{
				if (pdstData.Tables[0].Rows[i].RowState == DataRowState.Deleted) continue;
				pdstData.Tables[0].Rows[i][CST_RecoverMaterialDetailTable.RECOVERMATERIALMASTERID_FLD] = intMasterID;
			}
			new CST_RecoverMaterialDetailDS().UpdateDataSet(pdstData);

			//subtract
			InventoryUtilsBO boInventoryUtils = new InventoryUtilsBO();
			boInventoryUtils.UpdateSubtractOHQuantity(voRecoverMaterialMaster.CCNID, voRecoverMaterialMaster.MasterLocationID, voRecoverMaterialMaster.FromLocationID,
				voRecoverMaterialMaster.FromBinID, voRecoverMaterialMaster.ProductID, voRecoverMaterialMaster.Quantity, string.Empty, string.Empty);
			//update Transaction history
			
			MST_TransactionHistoryVO voMST_TransactionHistory = new MST_TransactionHistoryVO();
			voMST_TransactionHistory.CCNID = voRecoverMaterialMaster.CCNID;
			voMST_TransactionHistory.MasterLocationID = voRecoverMaterialMaster.MasterLocationID;
			voMST_TransactionHistory.LocationID = voRecoverMaterialMaster.FromLocationID;
			voMST_TransactionHistory.BinID = voRecoverMaterialMaster.FromBinID;
			voMST_TransactionHistory.ProductID = voRecoverMaterialMaster.ProductID;
			voMST_TransactionHistory.RefMasterID = intMasterID;
			voMST_TransactionHistory.PostDate = voRecoverMaterialMaster.PostDate;
			voMST_TransactionHistory.TransDate = new UtilsBO().GetDBDate();
			voMST_TransactionHistory.Quantity = - voRecoverMaterialMaster.Quantity;
			boInventoryUtils.SaveTransactionHistory(TransactionType.RECOVERABLE_MATERIAL, (int) PurposeEnum.ThanhLyLinhKienSauHuy, voMST_TransactionHistory);
			//add
			foreach (DataRow drow in pdstData.Tables[0].Rows)
			{
				if ((drow[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString() != string.Empty)
					&& (drow[CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString() != string.Empty))
				{
					boInventoryUtils.UpdateAddOHQuantity(voRecoverMaterialMaster.CCNID, voRecoverMaterialMaster.MasterLocationID, 
						int.Parse(drow[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString()), 
						int.Parse(drow[CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString()), int.Parse(drow[CST_RecoverMaterialDetailTable.PRODUCTID_FLD].ToString()), 
						decimal.Parse(drow[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].ToString()), string.Empty, string.Empty);
					//Save history to MST_TransactionHistory
					voMST_TransactionHistory = new MST_TransactionHistoryVO();
					voMST_TransactionHistory.CCNID = voRecoverMaterialMaster.CCNID;
					voMST_TransactionHistory.MasterLocationID = voRecoverMaterialMaster.MasterLocationID;
					voMST_TransactionHistory.LocationID = int.Parse(drow[CST_RecoverMaterialDetailTable.TOLOCATIONID_FLD].ToString());
					voMST_TransactionHistory.BinID = int.Parse(drow[CST_RecoverMaterialDetailTable.TOBINID_FLD].ToString());
					voMST_TransactionHistory.ProductID = int.Parse(drow[CST_RecoverMaterialDetailTable.PRODUCTID_FLD].ToString());
					voMST_TransactionHistory.RefMasterID = intMasterID;
					//Se update lai TrantypeID
					//voMST_TransactionHistory.TranTypeID = (int)TransactionTypeEnum.IVMiscellaneousIssue;
					voMST_TransactionHistory.PostDate = voRecoverMaterialMaster.PostDate;
					voMST_TransactionHistory.TransDate = new UtilsBO().GetDBDate();
					voMST_TransactionHistory.Quantity = decimal.Parse(drow[CST_RecoverMaterialDetailTable.RECOVERQUANTITY_FLD].ToString());
					voMST_TransactionHistory.StockUMID = int.Parse(drow[CST_RecoverMaterialDetailTable.UNITOFMEASUREID_FLD].ToString());
					boInventoryUtils.SaveTransactionHistory(TransactionType.RECOVERABLE_MATERIAL, (int) PurposeEnum.TanDungLinhKienSauHuy, voMST_TransactionHistory);
				}
				
			}
			return intMasterID;
		}
		/// <summary>
		/// Determine whenever freight detail has more than one destination (to location and to bin)
		/// </summary>
		/// <param name="pintMasterID">Freigth Master ID</param>
		/// <returns>true if has more than one. else false</returns>
	
		public bool HasMultiDestination(int pintMasterID)
		{
			CST_RecoverMaterialDetailDS dsRecoverDetail = new CST_RecoverMaterialDetailDS();
			return dsRecoverDetail.HasMultiDestination(pintMasterID);
		}
		/// <summary>
		/// Gets data for Recycled Slip
		/// </summary>
		/// <param name="pintMasterID">RecoverableMasterID</param>
		/// <returns></returns>
	
		public DataTable GetDataForSlip(int pintMasterID)
		{
			CST_RecoverMaterialDetailDS dsDetail = new CST_RecoverMaterialDetailDS();
			return dsDetail.GetDataForSlip(pintMasterID);
		}
	}
}

using System;
using System.Collections;
using System.Data;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComMaterials.Inventory.DS;



namespace PCSComMaterials.Inventory.BO
{
	public interface IIVInventoryAdjustmentBO
	{
		int AddAndReturnID(object pobjObject);
		DataSet GetAvalableQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID);
	}
	/// <summary>
	/// Summary description for IVInventoryAdjustmentBO.
	/// </summary>
	
	
	public class IVInventoryAdjustmentBO : IIVInventoryAdjustmentBO
	{
		public IVInventoryAdjustmentBO()
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
		/// <summary>
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjObject"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, July 27 2005</date>
	
		public int AddAndReturnID(object pobjObject)
		{
			string METHODE_NAME = "AddAndReturnID()";
			IV_AdjustmentVO voIV_Adjustment = (IV_AdjustmentVO)pobjObject;
			decimal decRemain = 0;
			//Check Available Quantity
			InventoryUtilsBO boInventoryUtils = new InventoryUtilsBO();
			DateTime dtmCurrentDate = new UtilsBO().GetDBDate().AddDays(1);
			if (voIV_Adjustment.AdjustQuantity < 0)
			{
				decRemain = boInventoryUtils.GetAvailableQtyByPostDate(dtmCurrentDate, voIV_Adjustment.CCNID, voIV_Adjustment.MasterLocationID, voIV_Adjustment.LocationID, voIV_Adjustment.BinID, voIV_Adjustment.ProductID)
					+ voIV_Adjustment.AdjustQuantity;  

				if (decRemain < 0)
				{
					throw new PCSBOException(ErrorCode.MESSAGE_IV_ADJUSTMENT_ADJUSTQTY_MUST_BE_SMALLER_THAN_AVAILABLEQTY, METHODE_NAME, new Exception());
				}
				else
				{
					decimal decAvailableQty = boInventoryUtils.GetAvailableQtyByPostDate(new UtilsBO().GetDBDate(), voIV_Adjustment.CCNID, voIV_Adjustment.MasterLocationID, voIV_Adjustment.LocationID, voIV_Adjustment.BinID, voIV_Adjustment.ProductID);
					if (-voIV_Adjustment.AdjustQuantity > decAvailableQty)
					{
						throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, METHODE_NAME, new Exception());
					}
				}
			}

			//AddAndReturnID
			int pintIV_AdjustmentID;
			IV_AdjustmentDS dsIV_Adjustment = new IV_AdjustmentDS();
			pintIV_AdjustmentID = dsIV_Adjustment.AddAndReturnID(pobjObject);
				
			//Update Add Onhand Quantity
			boInventoryUtils.UpdateAddOHQuantity(voIV_Adjustment.CCNID, voIV_Adjustment.MasterLocationID, voIV_Adjustment.LocationID, voIV_Adjustment.BinID, 
				voIV_Adjustment.ProductID, voIV_Adjustment.AdjustQuantity, voIV_Adjustment.Lot, voIV_Adjustment.Serial);
			//Save history to MST_TransactionHistory
			MST_TransactionHistoryVO voMST_TransactionHistory = new MST_TransactionHistoryVO();
			voMST_TransactionHistory.CCNID = voIV_Adjustment.CCNID;
			voMST_TransactionHistory.MasterLocationID = voIV_Adjustment.MasterLocationID;
			voMST_TransactionHistory.LocationID = voIV_Adjustment.LocationID;
			voMST_TransactionHistory.BinID = voIV_Adjustment.BinID;
			voMST_TransactionHistory.ProductID = voIV_Adjustment.ProductID;
			voMST_TransactionHistory.RefMasterID = pintIV_AdjustmentID;
			voMST_TransactionHistory.TranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionType.INVENTORY_ADJUSTMENT);			
			voMST_TransactionHistory.PostDate = voIV_Adjustment.PostDate;
			voMST_TransactionHistory.TransDate = new UtilsBO().GetDBDate();
			voMST_TransactionHistory.Quantity = voIV_Adjustment.AdjustQuantity;
			voMST_TransactionHistory.StockUMID = voIV_Adjustment.StockUMID;
			boInventoryUtils.SaveTransactionHistory(TransactionType.INVENTORY_ADJUSTMENT, (int) PurposeEnum.Adjustment, voMST_TransactionHistory);
			return pintIV_AdjustmentID;
		}
		/// <summary>
		/// GetAvalableQuantity
		/// </summary>
		/// <param name="pintLocationID"></param>
		/// <param name="pintBinID"></param>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, October 17 2005</date>
	
		public DataSet GetAvalableQuantity(int pintCCNID, int pintMasterLocationID, int pintLocationID, int pintBinID, int pintProductID)
		{
			if (pintBinID == 0)
			{
				IV_LocationCacheDS dsIV_LocationCache = new IV_LocationCacheDS();
				return dsIV_LocationCache.GetAvailableQtyAndInsStatusByProduct(pintCCNID, pintMasterLocationID, pintLocationID, pintProductID);
			}
			else
			{
				IV_BinCacheDS dsIV_BinCache = new IV_BinCacheDS();
				return dsIV_BinCache.GetAvailableQtyAndInsStatusByProduct(pintCCNID, pintMasterLocationID, pintLocationID, pintBinID, pintProductID);
			}
		}
		/// <summary>
		/// DeleteInventoryAdjustment
		/// </summary>
		/// <param name="printInventoryAdjustmentID"></param>
		/// <returns></returns>
		/// <author>CanhNV</author>
		/// <date>24-03-2007</date>
	
		public void DeleteInventoryAdjustment(int printInventoryAdjustmentID)
		{
			// 0. Variable
            IV_AdjustmentDS objAdjustmentDS = new IV_AdjustmentDS();
            IV_AdjustmentVO voAdjustment = new IV_AdjustmentVO();
            MST_TransactionHistoryVO voTransactionHistory = new MST_TransactionHistoryVO();
            
			int InspStatus = 17;
			
			// 1. Get Infomation of InventoryAdjustment
			voAdjustment = (IV_AdjustmentVO) objAdjustmentDS.GetObjectVOByAdjustmentID(printInventoryAdjustmentID);

			// 2. Delete InventoryAdjustment
            objAdjustmentDS.DeleteByAdjustmentID(printInventoryAdjustmentID);

			#region Set voTransactionHistory value
			voTransactionHistory.TransDate = new UtilsBO().GetDBDate();
			voTransactionHistory.TranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionType.INVENTORY_ADJUSTMENT);		
			voTransactionHistory.ProductID = voAdjustment.ProductID;
			voTransactionHistory.CCNID = voAdjustment.CCNID;
			voTransactionHistory.Lot = voAdjustment.Lot;								
			voTransactionHistory.StockUMID = voAdjustment.StockUMID;
			voTransactionHistory.Serial = voAdjustment.Serial;							
			voTransactionHistory.PostDate = voAdjustment.PostDate;
			voTransactionHistory.RefMasterID = voAdjustment.AdjustmentID;
			voTransactionHistory.Quantity = voAdjustment.AdjustQuantity;
			voTransactionHistory.MasterLocationID = voAdjustment.MasterLocationID;
			voTransactionHistory.LocationID = voAdjustment.LocationID;
			voTransactionHistory.BinID = voAdjustment.BinID;
			#endregion

			// 3. Update Inventory
			new InventoryUtilsBO().UpdateSubtractOHQuantity(voAdjustment.CCNID,
				voTransactionHistory.MasterLocationID,
				voTransactionHistory.LocationID,
				voTransactionHistory.BinID,
				voTransactionHistory.ProductID,
				voTransactionHistory.Quantity,
				string.Empty,
				string.Empty);
			
			// 4. Update TransactionHistory
			new MST_TransactionHistoryDS().UpdateTranType(voAdjustment.AdjustmentID,voTransactionHistory.TranTypeID,(int) TransactionTypeEnum.DeleteTransaction,InspStatus);
		}

		/// <summary>
		/// DeleteInventoryAdjustmentTransaction
		/// </summary>
		/// <param name="printInventoryAdjustmentID"></param>
		/// <returns></returns>
		/// <author>CanhNV</author>
		/// <date>24-03-2007</date>
	
		public void DeleteInventoryAdjustmentTransaction(int printInventoryAdjustmentID)
		{
			// 0. Variable
			IV_AdjustmentDS objAdjustmentDS = new IV_AdjustmentDS();
			IV_AdjustmentVO voAdjustment = new IV_AdjustmentVO();
			MST_TransactionHistoryVO voTransactionHistory = new MST_TransactionHistoryVO();
			decimal decQuantity = 0;
			int InspStatus = 172;
			
			// 1. Get Infomation of InventoryAdjustment
			voAdjustment = (IV_AdjustmentVO) objAdjustmentDS.GetObjectVOByAdjustmentID(printInventoryAdjustmentID);

			// 2. Delete InventoryAdjustment
			objAdjustmentDS.DeleteByAdjustmentID(printInventoryAdjustmentID);

			#region Set voTransactionHistory value
			voTransactionHistory.TransDate = new UtilsBO().GetDBDate();
			voTransactionHistory.TranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionType.INVENTORY_ADJUSTMENT);		
			voTransactionHistory.ProductID = voAdjustment.ProductID;
			voTransactionHistory.CCNID = voAdjustment.CCNID;
			voTransactionHistory.Lot = voAdjustment.Lot;								
			voTransactionHistory.StockUMID = voAdjustment.StockUMID;
			voTransactionHistory.Serial = voAdjustment.Serial;							
			voTransactionHistory.PostDate = voAdjustment.PostDate;
			voTransactionHistory.RefMasterID = voAdjustment.AdjustmentID;
			voTransactionHistory.Quantity = voAdjustment.AdjustQuantity;
			voTransactionHistory.MasterLocationID = voAdjustment.MasterLocationID;
			voTransactionHistory.LocationID = voAdjustment.LocationID;
			voTransactionHistory.BinID = voAdjustment.BinID;
			#endregion

			// 3. Update TransactionHistory
			new MST_TransactionHistoryDS().UpdateTranType(voAdjustment.AdjustmentID,voTransactionHistory.TranTypeID,(int) TransactionTypeEnum.DeleteTransaction,InspStatus);
		}
	}
}

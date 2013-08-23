using System;
using System.Data;

using PCSComMaterials.Inventory.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSComSale.Order.DS;


namespace PCSComSale.Order.BO
{
	public class SOCancelCommitmentBO
	{
		public object GetObjectSaleOrderMaster(string psrtCode)
		{
			try
			{
				SO_SaleOrderMasterDS dsSaleOrderMasterDS = new SO_SaleOrderMasterDS();
				return dsSaleOrderMasterDS.GetObjectVO(psrtCode);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	
		public DataSet ListCancelable(int pintSaleOrderMasterID)
		{
			try
			{
				SO_CommitInventoryDetailDS dsCommitInventory = new SO_CommitInventoryDetailDS();
				return dsCommitInventory.ListCancelable(pintSaleOrderMasterID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void AutoDeleteCommitInventoryMaster()
		{
			try
			{
				SO_CommitInventoryMasterDS dsCommitInventory = new SO_CommitInventoryMasterDS();
				dsCommitInventory.AutoDeleteCommitMaster();
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void  CancelCommitment(DataTable pdtbData, int pintCCNID)
		{
			const string Cancel = "Cancel", True = "True";
            SO_CommitInventoryDetailDS dsCommitDetail = new SO_CommitInventoryDetailDS();
            UtilsBO boUtils = new UtilsBO();
            foreach (DataRow drowData in pdtbData.Rows)
            {
                if ((drowData.RowState == DataRowState.Modified) && (drowData[Cancel].ToString() == True))
                {
                    decimal decRate = boUtils.GetUMRate(int.Parse(drowData[SO_CommitInventoryDetailTable.SELLINGUMID_FLD].ToString()), int.Parse(drowData[ITM_ProductTable.STOCKUMID_FLD].ToString()));
                    int intProductID = int.Parse(drowData[SO_CommitInventoryDetailTable.PRODUCTID_FLD].ToString());
                    int intSOMasterID = int.Parse(drowData[SO_CommitInventoryDetailTable.COMMITINVENTORYMASTERID_FLD].ToString());
                    int intSODetailID = int.Parse(drowData[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].ToString());
                    Decimal decQuantity = Decimal.Parse(drowData[SO_CommitInventoryDetailTable.COMMITQUANTITY_FLD].ToString());
                    int intMasLocID = 0;
                    if (drowData[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD].ToString() != string.Empty)
                    {
                        intMasLocID = int.Parse(drowData[SO_CommitInventoryDetailTable.MASTERLOCATIONID_FLD].ToString());
                    }
                    int intLocationID = 0;
                    if (drowData[SO_CommitInventoryDetailTable.LOCATIONID_FLD].ToString() != string.Empty)
                    {
                        intLocationID = int.Parse(drowData[SO_CommitInventoryDetailTable.LOCATIONID_FLD].ToString());
                    }
                    int intBinID = 0;
                    if (drowData[SO_CommitInventoryDetailTable.BINID_FLD].ToString() != string.Empty)
                    {
                        intBinID = int.Parse(drowData[SO_CommitInventoryDetailTable.BINID_FLD].ToString());
                    }
                    MST_TransactionHistoryVO voTransaction = new MST_TransactionHistoryVO();
                    voTransaction.CCNID = pintCCNID;
                    voTransaction.MasterLocationID = intMasLocID;
                    voTransaction.ProductID = intProductID;
                    voTransaction.LocationID = intLocationID;
                    voTransaction.BinID = intBinID;
                    voTransaction.RefMasterID = intSOMasterID;
                    voTransaction.RefDetailID = intSODetailID;
                    voTransaction.PostDate = boUtils.GetDBDate();
                    voTransaction.TransDate = boUtils.GetDBDate();
                    voTransaction.Quantity = decQuantity * decRate;
                    voTransaction.TranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionType.CANCEL_COMMITMENT);
                    voTransaction.StockUMID = (int)drowData[SO_CommitInventoryDetailTable.STOCKUMID_FLD];

                    new InventoryUtilsBO().UpdateSubtractCommitQuantity(pintCCNID, intMasLocID, intLocationID, intBinID, intProductID, decQuantity * decRate, string.Empty, string.Empty);
                    dsCommitDetail.Delete(int.Parse(drowData[SO_CommitInventoryDetailTable.COMMITINVENTORYDETAILID_FLD].ToString()));
                    new InventoryUtilsBO().SaveTransactionHistory(TransactionType.CANCEL_COMMITMENT, (int)PurposeEnum.CancelCommitment, voTransaction);
                }
            }
            AutoDeleteCommitInventoryMaster();
		}
	
		public string GetBuyingLocName(int pintID)
		{
			try
			{
				MST_PartyLocationDS dsParty = new MST_PartyLocationDS();
				return ((MST_PartyLocationVO) dsParty.GetObjectVO(pintID)).Code;
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

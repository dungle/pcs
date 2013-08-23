using System;
using System.Data;
using PCSComMaterials.Inventory.BO;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComProduction.ScrapRecording.DS;
using PCSComProduction.WorkOrder.DS;

namespace PCSComProduction.ScrapRecording.BO
{
	public class ComponentScrapBO
	{
		/// <summary>
		/// GetAvailableQuantity
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <param name="pintWODetailID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, June 29 2005</date>
	
		public decimal GetAvailableQuantity(int pintProductID, int pintWODetailID)
		{
            PRO_IssueMaterialDetailDS dsIssueMaterialDetail = new PRO_IssueMaterialDetailDS();
            return dsIssueMaterialDetail.GetAvailableQuantity(pintProductID, pintWODetailID);
		}
	
		public void UpdateCompScrapMasterAndDetail(object pobjComponentScrapMaster, DataSet pdstComponentScrapDetail)
		{
            PRO_ComponentScrapMasterDS dsComponentScrapMaster = new PRO_ComponentScrapMasterDS();
            //Update ComponentScrapMaster
            dsComponentScrapMaster.Update(pobjComponentScrapMaster);
            //Update ComponentScrapDetail 
            PRO_ComponentScrapDetailDS dsComponentScrapDetail = new PRO_ComponentScrapDetailDS();
            dsComponentScrapDetail.UpdateDataSet(pdstComponentScrapDetail);
		}
		/// <summary>
		/// Get Production Line code where ScrapMasterID
		/// </summary>
		/// <param name="pintScrapMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, August 9 2006</date>
	
		public DataTable GetProductionLineByScrapMasterID(int pintScrapMasterID)
		{
			PRO_ComponentScrapMasterDS dsComponentScrapMaster = new PRO_ComponentScrapMasterDS();
			return dsComponentScrapMaster.GetProductionLineCodeByScrapMasterID(pintScrapMasterID);
		}
		/// <summary>
		/// GetComponentScrapDetailByMasterID
		/// </summary>
		/// <param name="pintComponentScrapMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, July 1 2005</date>
	
		public DataSet GetComponentScrapDetailByMasterID(int pintComponentScrapMasterID)
		{
			try
			{
				PRO_ComponentScrapDetailDS dsComponentScrapDetail = new PRO_ComponentScrapDetailDS();
				return dsComponentScrapDetail.GetComponentScrapDetailByMasterID(pintComponentScrapMasterID);
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
		/// <summary>
		/// AddComponentScrapAndReturnID
		/// </summary>
		/// <param name="pobjCompScrapMasterVO"></param>
		/// <param name="pdstData"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, July 1 2005</date>

        public int AddComponentScrapAndReturnID(object pobjCompScrapMasterVO, DataSet pdstData)
		{
		    //Add a new CompScrapMasterVO to PRO_ComponentScrapMaster Table
		    int pintComponentScrapMasterID;
		    PRO_ComponentScrapMasterDS dsComponentScrapMaster = new PRO_ComponentScrapMasterDS();
		    pintComponentScrapMasterID = dsComponentScrapMaster.AddAndReturnID(pobjCompScrapMasterVO);
		    PRO_ComponentScrapMasterVO voComponentScrapMaster = (PRO_ComponentScrapMasterVO) pobjCompScrapMasterVO;
		    //UpdateDataSet into PRO_ComponentScrapDetail Table
		    foreach (DataRow drow in pdstData.Tables[0].Rows)
		    {
		        drow[PRO_ComponentScrapDetailTable.COMPONENTSCRAPMASTERID_FLD] = pintComponentScrapMasterID;
		    }

		    foreach (DataRow drow in pdstData.Tables[0].Rows)
		    {
		        InventoryUtilsBO boInventoryUtils = new InventoryUtilsBO();
		        //update add inventory
		        boInventoryUtils.UpdateAddOHQuantity(voComponentScrapMaster.CCNID, voComponentScrapMaster.MasterLocationID,
		                                             int.Parse(drow[PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].ToString()),
		                                             int.Parse(drow[PRO_ComponentScrapDetailTable.TOBINID_FLD].ToString()),
		                                             int.Parse(drow[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()),
		                                             decimal.Parse(
		                                                 drow[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString()),
		                                             string.Empty, string.Empty);
		        //update subtract inventory
		        boInventoryUtils.UpdateSubtractOHQuantity(voComponentScrapMaster.CCNID,
		                                                  voComponentScrapMaster.MasterLocationID,
		                                                  int.Parse(
		                                                      drow[PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString()),
		                                                  int.Parse(
		                                                      drow[PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString()),
		                                                  int.Parse(
		                                                      drow[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString()),
		                                                  decimal.Parse(
		                                                      drow[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString()),
		                                                  string.Empty, string.Empty);
		        //update transaction history
		        MST_TransactionHistoryVO voTransactionHistory = new MST_TransactionHistoryVO();
		        voTransactionHistory.CCNID = voComponentScrapMaster.CCNID;
		        voTransactionHistory.MasterLocationID = voComponentScrapMaster.MasterLocationID;
		        voTransactionHistory.PostDate = voComponentScrapMaster.PostDate;
		        voTransactionHistory.TransDate = (new UtilsBO()).GetDBDate();
		        voTransactionHistory.RefMasterID = pintComponentScrapMasterID;
		        //voTransactionHistory.RefDetailID = int.Parse(drow[PRO_ComponentScrapDetailTable.COMPONENTSCRAPDETAILID_FLD].ToString());
		        voTransactionHistory.LocationID = int.Parse(drow[PRO_ComponentScrapDetailTable.FROMLOCATIONID_FLD].ToString());
		        voTransactionHistory.BinID = int.Parse(drow[PRO_ComponentScrapDetailTable.FROMBINID_FLD].ToString());
		        voTransactionHistory.Quantity =
		            -decimal.Parse(drow[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString());
		        voTransactionHistory.ProductID = int.Parse(drow[PRO_ComponentScrapDetailTable.COMPONENTID_FLD].ToString());
		        voTransactionHistory.PurposeID = (int) PurposeEnum.Scrap;
		        voTransactionHistory.TranTypeID =
		            new MST_TranTypeDS().GetTranTypeID(TransactionTypeEnum.IVMiscellaneousIssue.ToString());
		        voTransactionHistory.StockUMID = int.Parse(drow[ITM_ProductTable.STOCKUMID_FLD].ToString());
		        boInventoryUtils.SaveTransactionHistory(TransactionTypeEnum.IVMiscellaneousIssue.ToString(),
		                                                (int) PurposeEnum.Scrap, voTransactionHistory);
		        voTransactionHistory.LocationID = int.Parse(drow[PRO_ComponentScrapDetailTable.TOLOCATIONID_FLD].ToString());
		        voTransactionHistory.BinID = int.Parse(drow[PRO_ComponentScrapDetailTable.TOBINID_FLD].ToString());
		        voTransactionHistory.Quantity = decimal.Parse(drow[PRO_ComponentScrapDetailTable.SCRAPQUANTITY_FLD].ToString());
		        boInventoryUtils.SaveTransactionHistory(TransactionTypeEnum.IVMiscellaneousIssue.ToString(),
		                                                (int) PurposeEnum.Scrap, voTransactionHistory);
		    }
		    PRO_ComponentScrapDetailDS dsComponentScrapDetail = new PRO_ComponentScrapDetailDS();
		    dsComponentScrapDetail.UpdateDataSet(pdstData);
		    return pintComponentScrapMasterID;
		}

	    /// <summary>
		/// DeleteComponentScrapMasterAndDetail
		/// </summary>
		/// <param name="pintComponentScrapMasterID"></param>
		/// <param name="pdstData"></param>
		/// <author>Trada</author>
		/// <date>Monday, July 4 2005</date>
	
		public void DeleteComponentScrapMasterAndDetail(int pintComponentScrapMasterID, DataSet pdstData)
		{
			try
			{
				foreach (DataRow drow in pdstData.Tables[0].Rows)
				{
					if (drow.RowState != DataRowState.Deleted)
					{
						drow.Delete();
					}
				}
				//Delete Detail
				PRO_ComponentScrapDetailDS dsComponentScrapDetail = new PRO_ComponentScrapDetailDS();
				dsComponentScrapDetail.UpdateDataSet(pdstData);

				//Delete Master
				PRO_ComponentScrapMasterDS dsComponentScrapMaster = new PRO_ComponentScrapMasterDS();
				dsComponentScrapMaster.Delete(pintComponentScrapMasterID);
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
	}
}

using System;
using System.Collections;
using System.Data;
using PCSComMaterials.Inventory.BO;
using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComSale.Order.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.Admin.DS;
using PCSComMaterials.Inventory.DS;


namespace PCSComSale.Order.BO
{
	public class SOReturnGoodsReceiveBO
	{
		private const string THIS = "PCSComSale.Order.BO.SOReturnGoodsReceiveBO";
		#region ISOReturnGoodsReceiveBO Members
		public decimal GetTotalReceivedQuantity(int pintSaleOrderMasterID,int pintProductID, string pstrLot, string pstrSerial)
		{
			SO_ReturnedGoodsDetailDS objSO_ReturnedGoodsDetailDS = new SO_ReturnedGoodsDetailDS();
			return objSO_ReturnedGoodsDetailDS.GetTotalReceivedQuantity(pintSaleOrderMasterID,pintProductID,pstrLot,pstrSerial);		
		}
		public DataSet GetLocation()
		{
			DataSet dstLocation = new DataSet();
			DataTable dt ;
			//get Master Location
			MST_MasterLocationDS objMST_MasterLocationDS = new MST_MasterLocationDS();
			dt = objMST_MasterLocationDS.List().Tables[0].Copy();
			dstLocation.Tables.Add(dt);
			return dstLocation;
		}
		public DataTable GetCNN()
		{
			MST_CCNDS objMST_CCNDS = new MST_CCNDS();
			return objMST_CCNDS.List().Tables[0];
		}
		public DataTable GetBuyingLocation()
		{
			// TODO:  Add SOReturnGoodsReceiveBO.GetBuyingLocation implementation
			return null;
		}
		public DataSet GetReturnGoodsDetail(int pintReturnedGoodsID)
		{
			SO_ReturnedGoodsDetailDS objSO_ReturnedGoodsDetailDS = new SO_ReturnedGoodsDetailDS();

			DataSet dsData = objSO_ReturnedGoodsDetailDS.ListReturnedGoodsDetail(pintReturnedGoodsID);
			return dsData;
		}
		public DataTable GetReturnGoodsMaster(int pintReturnedGoodsID)
		{
			SO_ReturnedGoodsMasterDS objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();
			return objSO_ReturnedGoodsMasterDS.GetReturnGoodsMaster(pintReturnedGoodsID);
		}
		public string GetMaxReturnedGoodsNumber(string strYearMonthDay)
		{
			SO_ReturnedGoodsMasterDS objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();
			return  "" ; //objSO_ReturnedGoodsMasterDS.GetMaxReturnedGoodsNumber(strYearMonthDay);
		}
		public DataTable GetUnitOfMeasure()
		{
			MST_UnitOfMeasureDS objMST_UnitOfMeasureDS = new MST_UnitOfMeasureDS();
				
			DataTable dt = objMST_UnitOfMeasureDS.List().Tables[0];
			//DataRow drEmptyRow = dt.NewRow();
			//dt.Rows.InsertAt(drEmptyRow,0);
			return dt;
		}
		public DataTable GetPartyLocation()
		{
			MST_PartyLocationDS objMST_PartyLocationDS = new MST_PartyLocationDS();
			return objMST_PartyLocationDS.List().Tables[0];
		}
		public DataTable GetPartyContact()
		{
			MST_PartyContactDS objMST_PartyContactDS = new MST_PartyContactDS();
			return objMST_PartyContactDS.List().Tables[0];
		}
		public void DeleteReturnedGoods(int pintReturnedGoodsMasterID)
		{
			//1. Variable
			int enm_InspStatus = 8;
			int constOldTranTypeID = (new MST_TranTypeDS()).GetTranTypeID(TransactionType.RETURN_GOODS_RECEIVE);

			SO_ReturnedGoodsDetailDS objSO_ReturnedGoodsDetailDS = new SO_ReturnedGoodsDetailDS();
			SO_ReturnedGoodsMasterDS objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();

			//2. Get ReturnedGoodsMaster
			SO_ReturnedGoodsMasterVO voRuturnGoodsMaster = new SO_ReturnedGoodsMasterVO();
			voRuturnGoodsMaster = (SO_ReturnedGoodsMasterVO) objSO_ReturnedGoodsMasterDS.GetObjectVO(pintReturnedGoodsMasterID);

			//Get List ReturnGoodsDetail by ReturnGoodsMasterID
            DataSet dsRGD = objSO_ReturnedGoodsDetailDS.ListReturnedGoodsDetail(pintReturnedGoodsMasterID);

			foreach (DataRow row in dsRGD.Tables[0].Rows)
			{
				//Update Inventory
				int MaslocID = (int) row[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD];
				int locID = (int) row[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD];
				int BinID = (int) row[SO_ReturnedGoodsDetailTable.BINID_FLD];
				int ProductID = (int) row[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD];
				decimal decQuantityReceipt = (decimal) row[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD];

				new InventoryUtilsBO().UpdateSubtractOHQuantity(voRuturnGoodsMaster.CCNID,
				    MaslocID,
					locID,
					BinID,
					ProductID,
					decQuantityReceipt,
					string.Empty,
					string.Empty);				
			}

			// Update TransactionHistory
			new MST_TransactionHistoryDS().UpdateTranType(voRuturnGoodsMaster.ReturnedGoodsMasterID, constOldTranTypeID, (int)TransactionTypeEnum.DeleteTransaction,enm_InspStatus);

			//delete the detail first
			objSO_ReturnedGoodsDetailDS.DeleteAllReturnedGoodsDetail(pintReturnedGoodsMasterID);

			//delete the master later
			objSO_ReturnedGoodsMasterDS.Delete(pintReturnedGoodsMasterID);
		}
		public int GetUserIDByUserName(string pstrUserName)
		{
			Sys_UserDS objSys_UserDS = new Sys_UserDS();
			return objSys_UserDS.GetUserIDByUserName(pstrUserName);
		}
		public int  AddNewReturnedGoods(object objReturnedGoodsMaster, DataSet dsReturnedGoodsDetail)
		{
			const string BALANCE_QUANTITY = "BalanceQty";
			const string METHOD_NAME = THIS + ".AddNewReturnedGoods()";
			SO_ReturnedGoodsMasterDS  objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();
			SO_ReturnedGoodsDetailDS objSO_ReturnedGoodsDetailDS = new SO_ReturnedGoodsDetailDS();
				
			//First add the master record
			int intReturnedGoodMasterID = objSO_ReturnedGoodsMasterDS.AddReturnedGoodsAndReturnID(objReturnedGoodsMaster);
			UtilsBO boUtils = new UtilsBO();
			if (((SO_ReturnedGoodsMasterVO) objReturnedGoodsMaster).SaleOrderMasterID > 0)
			{
				DataTable dstCheckBalanceOfAllItems = GetSaleOrderTotalCommit(((SO_ReturnedGoodsMasterVO) objReturnedGoodsMaster).SaleOrderMasterID);
				
				//update the dataset all of this id
				for (int i = 0; i < dsReturnedGoodsDetail.Tables[0].Rows.Count; i++)
				{
					if (dsReturnedGoodsDetail.Tables[0].Rows[i].RowState != DataRowState.Deleted)
					{
						dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD] = intReturnedGoodMasterID;
						DataRow[] drowsData = dstCheckBalanceOfAllItems.Select(ITM_ProductTable.PRODUCTID_FLD + "=" + dsReturnedGoodsDetail.Tables[0].Rows[i][ITM_ProductTable.PRODUCTID_FLD].ToString());
						if (drowsData.Length > 0)
						{
							decimal decRate = boUtils.GetUMRate(int.Parse(drowsData[0][SO_SaleOrderDetailTable.SELLINGUMID_FLD].ToString()), int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()));
							if (decRate == 0)
							{
								Exception ex = new Exception();
								Hashtable htbUMCode = new Hashtable();
								htbUMCode.Add(MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))).Code);
								htbUMCode.Add(ITM_ProductTable.STOCKUMID_FLD, ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][ITM_ProductTable.STOCKUMID_FLD].ToString()))).Code);
								throw new PCSBOException(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, METHOD_NAME, ex, htbUMCode);
							}
							if ((decimal)drowsData[0][BALANCE_QUANTITY]*decRate < (decimal)dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD])
								throw new PCSException(ErrorCode.MESSAGE_RGA_RECEIVEQTYTOCOMMIT, dsReturnedGoodsDetail.Tables[0].Rows[i][ITM_ProductTable.PRODUCTID_FLD].ToString(), new Exception());
							dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.QUANTITYOFSELLING_FLD] = (decimal)dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD]/decRate;
						}
						
					}
				}
			}
			else
			{
				for (int i = 0; i < dsReturnedGoodsDetail.Tables[0].Rows.Count; i++)
				{
					if (dsReturnedGoodsDetail.Tables[0].Rows[i].RowState != DataRowState.Deleted)
					{
						dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.RETURNEDGOODSMASTERID_FLD] = intReturnedGoodMasterID;
						decimal decRate = boUtils.GetUMRate(int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][ITM_ProductTable.STOCKUMID_FLD].ToString()), int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()));
						if (decRate == 0)
						{
							Exception ex = new Exception();
							Hashtable htbUMCode = new Hashtable();
							htbUMCode.Add(MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))).Code);
							htbUMCode.Add(ITM_ProductTable.STOCKUMID_FLD, ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(dsReturnedGoodsDetail.Tables[0].Rows[i][ITM_ProductTable.STOCKUMID_FLD].ToString()))).Code);
							throw new PCSBOException(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, METHOD_NAME, ex, htbUMCode);
						}
						dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.QUANTITYOFSELLING_FLD] = (decimal)dsReturnedGoodsDetail.Tables[0].Rows[i][SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD]/decRate;
					}
				}
			}
			//second add the detail
			objSO_ReturnedGoodsDetailDS.UpdateDataSetReturnedGoodsDetail(dsReturnedGoodsDetail, intReturnedGoodMasterID);

			//update history data
			SO_ReturnedGoodsMasterVO objSO_ReturnedGoodsMasterVO = (SO_ReturnedGoodsMasterVO) objReturnedGoodsMaster;
			objSO_ReturnedGoodsMasterVO.ReturnedGoodsMasterID = intReturnedGoodMasterID;
				
			UpdateInventoryInfor(dsReturnedGoodsDetail,true,objSO_ReturnedGoodsMasterVO);
			return intReturnedGoodMasterID;
		}
		public void UpdateIVMasterLocationCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, int pintCCNID,decimal pintAVGCost)
		{
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
			{
				//in case of adding a new returned goods 
				//we don't care the deleted record
				//we only care the other states : Modified and AddNew
				return ;
			}

			/*
					MasterLocCache (CCN, MasLoc, CODE(PRODUCTID), AVG Cost, On Hand Qty)
						AVG Cost = SO_CommitInventoryDetail.CostOfGoodsSold (SO -> SO Detail -> CommitInventoryDetail)
				*/
			IV_MasLocCacheDS objIV_MasLocCacheDS = new IV_MasLocCacheDS();
			bool blnHasProductID = objIV_MasLocCacheDS.HasProductID(int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString()),
				pintCCNID,
				int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString()));

			//Initialize the VO for the MasLocCache object
			IV_MasLocCacheVO objIV_MasLocCacheVO = new IV_MasLocCacheVO();
			objIV_MasLocCacheVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString());
			objIV_MasLocCacheVO.MasterLocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString());
			objIV_MasLocCacheVO.CCNID = pintCCNID;
			// edited by dungla: remove cast to float type
			objIV_MasLocCacheVO.AVGCost = pintAVGCost;

			//first we implement the case of totally adding new returned goods record
			//incase of updating an existing returned goods record
			//according to Mr.Nguyen Manh Cuong ==> Implement later ==> because it is very very complicated
			if (blnNewReturnedGood)
			{
				objIV_MasLocCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString());
			}
			else
			{
				//this one is implemented later
			}
			if (blnHasProductID)
			{
				//update an existing product
				objIV_MasLocCacheDS.UpdateReturnedGoods(objIV_MasLocCacheVO);
			}
			else
			{
				//Insert a new product into inventory
				objIV_MasLocCacheDS.AddReturnedGoods(objIV_MasLocCacheVO);
			}
		}
		public void UpdateIVLocationCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, int pintCCNID)
		{
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
			{
				//in case of adding a new returned goods 
				//we don't care the deleted record
				//we only care the other states : Modified and AddNew
				return ;
			}

			/*
					MasterLocCache (CCN, MasLoc, CODE(PRODUCTID), AVG Cost, On Hand Qty)
						AVG Cost = SO_CommitInventoryDetail.CostOfGoodsSold (SO -> SO Detail -> CommitInventoryDetail)
				*/
			IV_LocationCacheDS objIV_LocationCacheDS = new IV_LocationCacheDS();
			bool blnHasProductID = objIV_LocationCacheDS.HasProductID(int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString()),
				pintCCNID,
				int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString()),
				int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString()));

			//Initialize the VO for the MasLocCache object
			IV_LocationCacheVO objIV_LocationCacheVO = new IV_LocationCacheVO();
			objIV_LocationCacheVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString());
			objIV_LocationCacheVO.MasterLocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString());
			objIV_LocationCacheVO.CCNID = pintCCNID;
			objIV_LocationCacheVO.LocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString());


			//first we implement the case of totally adding new returned goods record
			//incase of updating an existing returned goods record
			//according to Mr.Nguyen Manh Cuong ==> Implement later ==> because it is very very complicated
			if (blnNewReturnedGood)
			{
				objIV_LocationCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString());
			}
			else
			{
				//this one is implemented later
			}
			if (blnHasProductID)
			{
				//update an existing product
				objIV_LocationCacheDS.UpdateReturnedGoods(objIV_LocationCacheVO);
					
			}
			else
			{
				//Insert a new product into inventory
				objIV_LocationCacheDS.AddReturnedGoods(objIV_LocationCacheVO);
			}
		}
		public void UpdateIVBinCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, int pintCCNID)
		{
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
			{
				//in case of adding a new returned goods 
				//we don't care the deleted record
				//we only care the other states : Modified and AddNew
				return ;
			}

			/*
					MasterLocCache (CCN, MasLoc, CODE(PRODUCTID), AVG Cost, On Hand Qty)
						AVG Cost = SO_CommitInventoryDetail.CostOfGoodsSold (SO -> SO Detail -> CommitInventoryDetail)
				*/
			IV_BinCacheDS objIV_BinCacheDS = new IV_BinCacheDS();
			bool blnHasProductID = objIV_BinCacheDS.HasProductID(int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString()),
				pintCCNID,
				int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString()),
				int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString()),
				int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.BINID_FLD].ToString()));

			//Initialize the VO for the MasLocCache object
			IV_BinCacheVO objIV_BinCacheVO = new IV_BinCacheVO();
			objIV_BinCacheVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString());
			objIV_BinCacheVO.MasterLocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString());
			objIV_BinCacheVO.CCNID = pintCCNID;
			objIV_BinCacheVO.LocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString());
			objIV_BinCacheVO.BinID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.BINID_FLD].ToString());


			//first we implement the case of totally adding new returned goods record
			//incase of updating an existing returned goods record
			//according to Mr.Nguyen Manh Cuong ==> Implement later ==> because it is very very complicated
			if (blnNewReturnedGood)
			{
				objIV_BinCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString());
			}
			else
			{
				//this one is implemented later
			}
			if (blnHasProductID)
			{
				//update an existing product
				objIV_BinCacheDS.UpdateReturnedGoods(objIV_BinCacheVO);
					
			}
			else
			{
				//Insert a new product into inventory
				objIV_BinCacheDS.AddReturnedGoods(objIV_BinCacheVO);
			}
		}
		public void UpdateInventoryInfor (DataSet dsReturnedGoodsDetail,bool blnNewReturnedGood, SO_ReturnedGoodsMasterVO pobjSO_ReturnedGoodsMasterVO)
		{
			const string AVG_COST_FLD = "AVGCost";
			const string METHOD_NAME = THIS + "UpdateInventoryInfor()";
			DataTable dtSaleOrderTotalCommit = null;
			//Get list of commited sale order and average cost
			if (pobjSO_ReturnedGoodsMasterVO.SaleOrderMasterID > 0)
			{
				dtSaleOrderTotalCommit = GetAvgCommitCost(pobjSO_ReturnedGoodsMasterVO.SaleOrderMasterID);
			}
			UtilsBO boUtils = new UtilsBO();
			foreach (DataRow drowReturnedGoodsDetail in dsReturnedGoodsDetail.Tables[0].Rows)
			{
				decimal decUMRate = boUtils.GetUMRate(int.Parse(drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()), int.Parse(drowReturnedGoodsDetail[ITM_ProductTable.STOCKUMID_FLD].ToString()));
				if (decUMRate == 0)
				{
					Exception ex = new Exception();
					Hashtable htbUMCode = new Hashtable();
					htbUMCode.Add(MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD, ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString()))).Code);
					htbUMCode.Add(ITM_ProductTable.STOCKUMID_FLD, ((MST_UnitOfMeasureVO) boUtils.GetUMInfor(int.Parse(drowReturnedGoodsDetail[ITM_ProductTable.STOCKUMID_FLD].ToString()))).Code);
					throw new PCSBOException(ErrorCode.MESSAGE_UMRATE_IS_NOT_CONFIGURATED, METHOD_NAME, ex, htbUMCode);
				}
				if (blnNewReturnedGood && drowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
				{
					//in case of adding a new returned goods 
					//we don't care the deleted record
					//we only care the other states : Modified and AddNew
					continue;
				}

				//calculate the avergae cost
				decimal dcmAVGCost = -1;
				if (pobjSO_ReturnedGoodsMasterVO.SaleOrderMasterID >0 && drowReturnedGoodsDetail.RowState != DataRowState.Deleted)
				{
					//Find this product and SaleOrderMaster
					string strFindString = ITM_ProductTable.PRODUCTID_FLD + "=" + drowReturnedGoodsDetail[ITM_ProductTable.PRODUCTID_FLD].ToString() ;
					strFindString += " AND " + SO_ReturnedGoodsMasterTable.SALEORDERMASTERID_FLD + "=" + pobjSO_ReturnedGoodsMasterVO.SaleOrderMasterID.ToString() ;
					DataRow[] drowResult = dtSaleOrderTotalCommit.Select(strFindString);
					if (drowResult.Length > 0)
					{
						if (drowResult[0][AVG_COST_FLD] != DBNull.Value
							&& drowResult[0][AVG_COST_FLD].ToString().Trim() != String.Empty) 
						{
							dcmAVGCost = Decimal.Parse (drowResult[0][AVG_COST_FLD].ToString());
						}
					}
				}

				#region UPDATE INTO TABLE IV_CostHistory
				/*
					 * update IV_CostHistory
					 * 
					 */
				UpdateCostHistory(drowReturnedGoodsDetail,blnNewReturnedGood,pobjSO_ReturnedGoodsMasterVO,dcmAVGCost);
				#endregion

				#region INSERT INTO LotFIFO Table
				/*
						update LotFIFO
						first check this product Item if it is a actual cost
						if it is actual cost and if there is lot control ==> insert into Lot FIFO
					*/
				//					int intProductID = int.Parse(drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim());
				//					ITM_ProductDS objITM_ProductDS = new ITM_ProductDS();
				//					bool blnIsProductActualCost = objITM_ProductDS.IsActualCost(intProductID);
				//					if (blnIsProductActualCost) 
				//					{
				//						if (drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOT_FLD].ToString().Trim() != String.Empty)
				//						{
				//							//initialize the VO class
				//							IV_LotFIFOVO objIV_LotFIFOVO = new IV_LotFIFOVO();
				//							objIV_LotFIFOVO.CCNID = pobjSO_ReturnedGoodsMasterVO.CCNID;
				//							objIV_LotFIFOVO.ProductID = intProductID;
				//							objIV_LotFIFOVO.ReceiveDate = pobjSO_ReturnedGoodsMasterVO.TransDate;
				//							objIV_LotFIFOVO.Lot = drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOT_FLD].ToString().Trim();
				//
				//							//get the Cost at the time this product was sold
				//							//just go into the table DetailCOmmitInventory to get this
				//							SO_CommitInventoryDetailDS objSO_CommitInventoryDetailDS = new SO_CommitInventoryDetailDS();
				//							objIV_LotFIFOVO.ActualCost21 =  objSO_CommitInventoryDetailDS.GetCostOfGoodsSold(intProductID,objIV_LotFIFOVO.Lot);
				//
				//							//insert a new record into this table
				//							IV_LotFIFODS objIV_LotFIFODS = new IV_LotFIFODS();
				//							bool blnHasProductLot = objIV_LotFIFODS.HasProduct(objIV_LotFIFOVO.Lot,objIV_LotFIFOVO.CCNID,objIV_LotFIFOVO.ProductID);
				//							if (!blnHasProductLot)
				//							{
				//								objIV_LotFIFODS.AddReturnedGoods(objIV_LotFIFOVO);
				//							}
				//							else
				//							{
				//								//update the onhand quantity
				//								objIV_LotFIFODS.UpdateLotFIFO(objIV_LotFIFOVO);
				//							}
				//						}
				//					}
				#endregion
					
				int intBinID = 0;
				if (drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD].ToString() != string.Empty)
				{
					intBinID = (int) drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD];
				}
				new InventoryUtilsBO().UpdateAddOHQuantity(pobjSO_ReturnedGoodsMasterVO.CCNID,
					pobjSO_ReturnedGoodsMasterVO.MasterLocationID,
					int.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString()),
					intBinID,
					int.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString()),
					Decimal.Parse(drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString()) * decUMRate,
					//drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOT_FLD].ToString(),
					string.Empty,
					//drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.SERIAL_FLD].ToString()
					string.Empty);
				#region INSERT and UPDATE into IV_ItemSerial
				//if this detail row has Serial field (user inputted)
				//we have to update or insert into this table
				if (drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.SERIAL_FLD].ToString().Trim() != String.Empty)
				{
					//UpdateIVItemSerial(drowReturnedGoodsDetail,blnNewReturnedGood,pobjSO_ReturnedGoodsMasterVO);
				}
				#endregion

				#region UPDATE INTO TABLE MST_TransactionHistory
				/*
					 * update MST_TransactionHistory
					 * 
					 */
				UpdateTransactionHistory(drowReturnedGoodsDetail,blnNewReturnedGood,pobjSO_ReturnedGoodsMasterVO,dcmAVGCost, decUMRate);
				#endregion
			}
		}
		public void UpdateReturnedGoods(object objReturnedGoodsMaster, DataSet dsReturnedGoodsDetail)
		{
			SO_ReturnedGoodsMasterDS  objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();
			SO_ReturnedGoodsDetailDS objSO_ReturnedGoodsDetailDS = new SO_ReturnedGoodsDetailDS();

			//first update the master
			objSO_ReturnedGoodsMasterDS.UpdateReturnedGoods(objReturnedGoodsMaster);

			//second update the detail
			objSO_ReturnedGoodsDetailDS.UpdateDataSetReturnedGoodsDetail(dsReturnedGoodsDetail,((SO_ReturnedGoodsMasterVO)objReturnedGoodsMaster).ReturnedGoodsMasterID);
		}
		public object GetReturnedGoodsMasterInfo(int pintReturnedGoodsMasterID)
		{
			SO_ReturnedGoodsMasterDS objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();
			object objSO_ReturnedGoodsMasterVO;
			objSO_ReturnedGoodsMasterVO = objSO_ReturnedGoodsMasterDS.GetReturnedGoodsMasterVO(pintReturnedGoodsMasterID);
			return objSO_ReturnedGoodsMasterVO;
		}
		public string BuildWhereClauseToSearchProduct(int pintSaleOrderMasterID)
		{
			string strWhereClause = String.Empty;
			strWhereClause = " WHERE " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD ;
			strWhereClause += " in (SELECT "  + SO_SaleOrderDetailTable.PRODUCTID_FLD ;
			strWhereClause += "     FROM " + SO_SaleOrderDetailTable.TABLE_NAME ;
			strWhereClause += "     WHERE " + SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD + "=" + pintSaleOrderMasterID ;
			strWhereClause += "     )";

			return strWhereClause;
		}
		public string BuildWhereClauseToSearchSaleOrder(int pintShipFromLocID)
		{
			string strWhereClause = String.Empty;
			strWhereClause = " WHERE " + SO_SaleOrderMasterTable.TABLE_NAME + "." +  SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD ;
			strWhereClause += " IN ( SELECT " + SO_CommitInventoryMasterTable.SALEORDERMASTERID_FLD ; 
			strWhereClause += "      FROM  " + SO_CommitInventoryMasterTable.TABLE_NAME  ;
			strWhereClause += "     )"
				// and " + SO_SaleOrderMasterTable.TABLE_NAME + "." +  SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD + "=1";
				+ " AND " + SO_SaleOrderMasterTable.TABLE_NAME + "." +  SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD
				+ "=" + pintShipFromLocID;

			return strWhereClause;
		}
		public DataTable GetSaleOrderTotalCommit(int pintSaleOrderMasterID)
		{
			SO_SaleOrderDetailDS objSO_SaleOrderDetailDS = new SO_SaleOrderDetailDS();
			return objSO_SaleOrderDetailDS.GetSaleOrderTotalCommit(pintSaleOrderMasterID);
		}
		public DataTable GetAvgCommitCost(int pintSaleOrderMasterID)
		{
			SO_SaleOrderDetailDS objSO_SaleOrderDetailDS = new SO_SaleOrderDetailDS();
			return objSO_SaleOrderDetailDS.GetAvgCommitCost(pintSaleOrderMasterID);
		}
		public DataTable GetCustomerInfo(int pintPartyID)
		{
			MST_PartyDS objMST_PartyDS = new MST_PartyDS();
			return objMST_PartyDS.GetCustomerInfo(pintPartyID);
		}
		public string GetReturnedGoodsMasterIDByNumber(string pstrReturnedGoodsNumber)
		{
			SO_ReturnedGoodsMasterDS objSO_ReturnedGoodsMasterDS = new SO_ReturnedGoodsMasterDS();
			return objSO_ReturnedGoodsMasterDS.GetReturnedGoodsMasterIDByNumber(pstrReturnedGoodsNumber);
		}
		public DataTable GetQAStatus()
		{
			const string ID_FIELD = "ID";
			const string VALUE_FIELD = "VALUE";

			DataTable dtQAStatus = new DataTable();
			dtQAStatus.Columns.Add(ID_FIELD);
			dtQAStatus.Columns.Add(VALUE_FIELD);
			DataRow drNewRow = dtQAStatus.NewRow();

			drNewRow = dtQAStatus.NewRow();
			dtQAStatus.Rows.Add(drNewRow);

			drNewRow = dtQAStatus.NewRow();
			drNewRow[ID_FIELD] = "1";
			drNewRow[VALUE_FIELD] ="not source quality assured and requires inspection";
			dtQAStatus.Rows.Add(drNewRow);

			drNewRow = dtQAStatus.NewRow();
			drNewRow[ID_FIELD] = "2";
			drNewRow[VALUE_FIELD] ="not source quality assured but does not require inspection";
			dtQAStatus.Rows.Add(drNewRow);

			drNewRow = dtQAStatus.NewRow();
				
			drNewRow[ID_FIELD] = "3";
			drNewRow[VALUE_FIELD] ="source quality assured";
			dtQAStatus.Rows.Add(drNewRow);

			return dtQAStatus;
		}
		public void UpdateTransactionHistory(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, SO_ReturnedGoodsMasterVO pobjSO_ReturnedGoodsMasterVO,decimal pdcmAvgCost, decimal pdecUMRate)
		{
			const string RETURNED_GOODS_TRANSACTION_NAME = "SOReturnGoodsReceive";
			const string QASTATUS_STATUS_1 = "1";
			const string QASTATUS_STATUS_3 = "3";
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
			{
				//in case of adding a new returned goods 
				//we don't care the deleted record
				//we only care the other states : Modified and AddNew
				return ;
			}
			MST_TransactionHistoryVO objMST_TransactionHistoryVO = new MST_TransactionHistoryVO();
			objMST_TransactionHistoryVO.MasterLocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString().Trim());
			if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.BINID_FLD].ToString().Trim() != String.Empty) 
			{
				objMST_TransactionHistoryVO.BinID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.BINID_FLD].ToString().Trim());
			}
			objMST_TransactionHistoryVO.TransDate = new UtilsBO().GetDBDate();
			objMST_TransactionHistoryVO.PostDate  = pobjSO_ReturnedGoodsMasterVO.TransDate;
			objMST_TransactionHistoryVO.RefMasterID = pobjSO_ReturnedGoodsMasterVO.ReturnedGoodsMasterID;
			objMST_TransactionHistoryVO.RefDetailID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD].ToString().Trim());
			objMST_TransactionHistoryVO.Lot = pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOT_FLD].ToString().Trim();
			objMST_TransactionHistoryVO.Serial = pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.SERIAL_FLD].ToString().Trim();

			//try to get the inspection status
			if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ToString().Trim() == String.Empty) 
			{
				objMST_TransactionHistoryVO.InspStatus = 0;
			}
			else
			{
				if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ToString().Trim() == QASTATUS_STATUS_1) 
				{
					objMST_TransactionHistoryVO.InspStatus = int.Parse(QASTATUS_STATUS_1);
				}
				else
				{
					objMST_TransactionHistoryVO.InspStatus = int.Parse(QASTATUS_STATUS_3);
				}
			}

			objMST_TransactionHistoryVO.CCNID = pobjSO_ReturnedGoodsMasterVO.CCNID;
				
			//get the TransType ID
			MST_TranTypeDS objMST_TranTypeDS = new MST_TranTypeDS();
			objMST_TransactionHistoryVO.TranTypeID = objMST_TranTypeDS.GetIDFromCode(RETURNED_GOODS_TRANSACTION_NAME);


			objMST_TransactionHistoryVO.PartyID = pobjSO_ReturnedGoodsMasterVO.PartyID;
			objMST_TransactionHistoryVO.PartyLocationID = pobjSO_ReturnedGoodsMasterVO.PartyLocationID;
			objMST_TransactionHistoryVO.LocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOCATIONID_FLD].ToString().Trim());
			objMST_TransactionHistoryVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString().Trim());
			if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString().Trim() != String.Empty) 
			{
				objMST_TransactionHistoryVO.StockUMID = int.Parse(pdrowReturnedGoodsDetail[ITM_ProductTable.STOCKUMID_FLD].ToString().Trim());
			}
				
			//calculate the NewAvg Cost
			//1.Get the OnHand Quantity from Master Location Cache
			//2.Calculate the Average Cost 
			//3.Then NewAvgCost = (ReceiveQty + OnHanQty)/ ((ReceiveQty * AvgCost) + (OnHanQty * AvgCost))
			IV_MasLocCacheDS objIV_MasLocCacheDS = new IV_MasLocCacheDS();
			decimal dcmOnHanQty = objIV_MasLocCacheDS.GetOnHanQty(objMST_TransactionHistoryVO.ProductID,objMST_TransactionHistoryVO.CCNID,objMST_TransactionHistoryVO.MasterLocationID);
			if (pdcmAvgCost < 0)
			{
				pdcmAvgCost = 0;
			}
			decimal dcmReceiveQty = decimal.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString());

			try 
			{
				objMST_TransactionHistoryVO.NewAvgCost = (dcmReceiveQty + dcmOnHanQty) / ((dcmReceiveQty * pdcmAvgCost)  + (dcmOnHanQty * pdcmAvgCost));
			}
			catch
			{
				objMST_TransactionHistoryVO.NewAvgCost = 0;
			}

			objMST_TransactionHistoryVO.Quantity = dcmReceiveQty*pdecUMRate;
			
			new InventoryUtilsBO().SaveTransactionHistory(TransactionTypeEnum.SOReturnGoodsReceive.ToString(), (int) PurposeEnum.ReturnGoodReceipt, objMST_TransactionHistoryVO);
		}
		public void UpdateCostHistory(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, SO_ReturnedGoodsMasterVO pobjSO_ReturnedGoodsMasterVO,decimal pdcmAvgCost)
		{
			const string RETURNED_GOODS_TRANSACTION_NAME = "SOReturnGoodsReceive";
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
			{
				//in case of adding a new returned goods 
				//we don't care the deleted record
				//we only care the other states : Modified and AddNew
				return ;
			}
			IV_CostHistoryVO objIV_CostHistoryVO = new IV_CostHistoryVO();
			if (pdcmAvgCost < 0)
			{
				pdcmAvgCost = 0;
			}
			objIV_CostHistoryVO.ICDHItemCost21 = pdcmAvgCost;
			objIV_CostHistoryVO.ReceiveDate = pobjSO_ReturnedGoodsMasterVO.TransDate;
			objIV_CostHistoryVO.ReceiveRef = pobjSO_ReturnedGoodsMasterVO.ReturnedGoodsMasterID;
			objIV_CostHistoryVO.ReceiveRefLine = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RETURNEDGOODSDETAILID_FLD].ToString());
			//QA Status
			if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ToString().Trim() != String.Empty) 
			{
				objIV_CostHistoryVO.QAStatus = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.QASTATUS_FLD].ToString());
			}
			else
			{
				objIV_CostHistoryVO.QAStatus = 0;
			}
			//Party ID
			objIV_CostHistoryVO.PartyID = pobjSO_ReturnedGoodsMasterVO.PartyID;
			//Party Location
			objIV_CostHistoryVO.PartyLocationID = pobjSO_ReturnedGoodsMasterVO.PartyLocationID;
			//Master Location
			objIV_CostHistoryVO.MasterLocationID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.MASTERLOCATIONID_FLD].ToString());
			//Product ID
			objIV_CostHistoryVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.PRODUCTID_FLD].ToString());
			//CCN ID
			objIV_CostHistoryVO.CCNID = pobjSO_ReturnedGoodsMasterVO.CCNID;
			//Unit of measure
			if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString().Trim() != String.Empty) 
			{
				objIV_CostHistoryVO.StockUMID = int.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.UNITID_FLD].ToString());
			}
			//get the TransType ID
			MST_TranTypeDS objMST_TranTypeDS = new MST_TranTypeDS();
			objIV_CostHistoryVO.TranTypeID = objMST_TranTypeDS.GetIDFromCode(RETURNED_GOODS_TRANSACTION_NAME);

			//Insert this record into the IV_CostHistory
			IV_CostHistoryDS objIV_CostHistoryDS = new IV_CostHistoryDS();
			objIV_CostHistoryDS.AddReturnedGoods(objIV_CostHistoryVO);
		}
		#endregion
	}
}

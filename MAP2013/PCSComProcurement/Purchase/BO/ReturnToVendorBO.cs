using System;
using System.Data;


using PCSComMaterials.Inventory.BO;
using PCSComMaterials.Inventory.DS;
using PCSComProcurement.Purchase.DS;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.BO
{
	public class ReturnToVendorBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.ReturnToVendorBO";
		DateTime dtmCurrentDate = new UtilsBO().GetDBDate().AddDays(1);

		public DataSet GetReturnGoodsDetail(int pintReturnToVendorMaster)
		{
			PO_ReturnToVendorDetailDS objPO_ReturnToVendorDetailDS = new PO_ReturnToVendorDetailDS();
			DataSet dsData = objPO_ReturnToVendorDetailDS.ListReturnToVendorDetail(pintReturnToVendorMaster);
			dsData.Tables[0].Columns[PO_ReturnToVendorDetailTable.LINE_FLD].AutoIncrement = true;
			dsData.Tables[0].Columns[PO_ReturnToVendorDetailTable.LINE_FLD].AutoIncrementSeed = objPO_ReturnToVendorDetailDS.GetMaxReturnToVendorDetailLine(pintReturnToVendorMaster) + 1;
			return dsData;
		}
	
		public DataTable GetReturnToVendorMasterInfo(int pintReturnToVendorMaster)
		{
			PO_ReturnToVendorMasterDS objPO_ReturnToVendorMasterDS = new PO_ReturnToVendorMasterDS();
			return objPO_ReturnToVendorMasterDS.GetReturnToVendorMasterInfo(pintReturnToVendorMaster);			
		}
	
		private void CheckOnHandQty(DataSet dstData, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO) 
		{
			const string METHOD_NAME = THIS + ".GetOnhandQty()";
			new IV_BinCacheDS();
			IV_LocationCacheDS objIV_LocationCacheDS = new IV_LocationCacheDS();
			InventoryUtilsBO boIVUtils = new InventoryUtilsBO();
			new MST_UMRateDS();
			decimal dcmOnHandQty, decOnHandCurrent =0;
			try {
				for (int i=0; i<dstData.Tables[0].Rows.Count;i++)
				{
					if (dstData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
					{
						//dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD] = intReturnToVendorMasterID;
						//get Quantity, location, bin, umrate
						string strBINID = dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.BINID_FLD].ToString().Trim();
						dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString().Trim();
						if (strBINID != String.Empty)
						{
							IV_BinCacheVO objIV_BinCacheVO = new IV_BinCacheVO();
							objIV_BinCacheVO.ProductID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
							objIV_BinCacheVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
							objIV_BinCacheVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
							objIV_BinCacheVO.LocationID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());
							objIV_BinCacheVO.BinID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.BINID_FLD].ToString());
							//get onhand qty from Bin Cache
							if (dstData.Tables[0].Rows[i][ITM_ProductTable.LOTCONTROL_FLD].ToString() != true.ToString())
							{
//								dcmOnHandQty = boIVUtils.GetAvailableQtyByPostDate(pobjPO_ReturnToVendorMasterVO.PostDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
//									objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
//								decOnHandCurrent = boIVUtils.GetAvailableQtyByPostDate(new UtilsBO().GetDBDate(), objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
//									objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
								dcmOnHandQty = boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
									objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
								decOnHandCurrent = boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
									objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
							}
							else
							{
								objIV_BinCacheVO.Lot = dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.LOT_FLD].ToString();
//								dcmOnHandQty = boIVUtils.GetAvailableQtyByPostDate(pobjPO_ReturnToVendorMasterVO.PostDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
//									objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
								dcmOnHandQty = boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
									objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
							}
						}
						else
						{
							IV_LocationCacheVO objIV_LocationCacheVO = new IV_LocationCacheVO();
							objIV_LocationCacheVO.ProductID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
							objIV_LocationCacheVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
							objIV_LocationCacheVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
							objIV_LocationCacheVO.LocationID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());
						
							//get onhand qty from location cache
							if (dstData.Tables[0].Rows[i][ITM_ProductTable.LOTCONTROL_FLD].ToString() != true.ToString())
							{
//								dcmOnHandQty =  boIVUtils.GetAvailableQtyByPostDate(pobjPO_ReturnToVendorMasterVO.PostDate, objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
//									objIV_LocationCacheVO.LocationID, 0, objIV_LocationCacheVO.ProductID);
//								decOnHandCurrent =  boIVUtils.GetAvailableQtyByPostDate(new UtilsBO().GetDBDate(), objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
//									objIV_LocationCacheVO.LocationID, 0, objIV_LocationCacheVO.ProductID);
								dcmOnHandQty =  boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
									objIV_LocationCacheVO.LocationID, 0, objIV_LocationCacheVO.ProductID);
								decOnHandCurrent =  boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
									objIV_LocationCacheVO.LocationID, 0, objIV_LocationCacheVO.ProductID);
							}
							else
							{
								objIV_LocationCacheVO.Lot = dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.LOT_FLD].ToString();
								dcmOnHandQty = objIV_LocationCacheDS.GetAvailableQuantityByLot(objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
									objIV_LocationCacheVO.LocationID, objIV_LocationCacheVO.Lot, objIV_LocationCacheVO.ProductID);	
							}
						}
						decimal dcmUMRate = 1;
						if (pobjPO_ReturnToVendorMasterVO.PurchaseOrderMasterID != 0)
						{
							//get the UMRate 
						
							if (dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.UMRATE_FLD].ToString() != String.Empty)
							{
								dcmUMRate = decimal.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.UMRATE_FLD].ToString());
							}
							else
							{
								dcmUMRate = 1;
							}
						}
						decimal dcmReturnQuantity = decimal.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString());
						if ( (dcmOnHandQty - (dcmReturnQuantity * dcmUMRate)) < 0) 
						{
							throw new PCSBOException(ErrorCode.MESSAGE_RGA_OVERONHANDQTY,i.ToString(),null);
						}
						else
						{
							if (decOnHandCurrent < (dcmReturnQuantity * dcmUMRate))
							{
								throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, i.ToString(),null);
							}
						}
					}
				}

			}			
			catch (PCSBOException ex) 
			{
				throw ex;
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
	
		public int AddNewReturnToVendor(PO_ReturnToVendorMasterVO pobjReturnToVendorMasterVO, DataSet pdstDetail)
		{
			try 
			{
				//check onhand quantity
				CheckOnHandQty(pdstDetail,pobjReturnToVendorMasterVO);

				//store the master first
				PO_ReturnToVendorMasterDS objPO_ReturnToVendorMasterDS = new PO_ReturnToVendorMasterDS();
				PO_ReturnToVendorDetailDS objPO_ReturnToVendorDetailDS = new PO_ReturnToVendorDetailDS();
				//first we need to add to the master and return the latest ID 
				int intReturnToVendorMasterID = objPO_ReturnToVendorMasterDS.AddNewReturnToVendor(pobjReturnToVendorMasterVO);
				
				//assign this ID into the detail
				//update the dataset all of this id
				for (int i=0; i<pdstDetail.Tables[0].Rows.Count;i++)
				{
					if (pdstDetail.Tables[0].Rows[i].RowState != DataRowState.Deleted)
					{
						pdstDetail.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.RETURNTOVENDORMASTERID_FLD] = intReturnToVendorMasterID;
					}
				}
				//Add this database into database
				if (pobjReturnToVendorMasterVO.PurchaseOrderMasterID != 0)
				{
					objPO_ReturnToVendorDetailDS.UpdateReturnToVendorDataSet(pdstDetail, intReturnToVendorMasterID);
				}
				else if (pobjReturnToVendorMasterVO.InvoiceMasterID != 0)
				{
					objPO_ReturnToVendorDetailDS.UpdateDataSetForInvoice(pdstDetail);
				}
			
				// Add value to pobjReturnToVendorMasterVO
				pobjReturnToVendorMasterVO.ReturnToVendorMasterID = intReturnToVendorMasterID;

				//Update inventory data				
				UpdateInventoryInfor(pdstDetail,true, pobjReturnToVendorMasterVO);
				// HACK: Trada 27-12-2005
				//Update MST_TransactionHistory
				new UtilsBO();
				InventoryUtilsBO boInventoryUtils = new InventoryUtilsBO();
				PO_ReturnToVendorDetailDS dsReturnToVendorDetail = new PO_ReturnToVendorDetailDS();
				pdstDetail = dsReturnToVendorDetail.ListReturnToVendorDetail(intReturnToVendorMasterID);
				foreach (DataRow drow in pdstDetail.Tables[0].Rows)
				{
					MST_TransactionHistoryVO voTransactionHistory = new MST_TransactionHistoryVO();

					voTransactionHistory.CCNID = pobjReturnToVendorMasterVO.CCNID;
					voTransactionHistory.MasterLocationID = pobjReturnToVendorMasterVO.MasterLocationID;
					voTransactionHistory.PartyID = pobjReturnToVendorMasterVO.PartyID;
					voTransactionHistory.PostDate = pobjReturnToVendorMasterVO.PostDate;
					voTransactionHistory.PartyLocationID = pobjReturnToVendorMasterVO.PurchaseLocID;

					//HACK: Modify by Tuan TQ 03 Apr, 2006. Fix error no. 3279
					//Rem by Tuan TQ: 03 Apr, 2006
					//voTransactionHistory.RefMasterID = intReturnToVendorMasterID;

					//Rem by Tuan TQ: 15 June, 2006
					//voTransactionHistory.RefMasterID = pobjReturnToVendorMasterVO.PurchaseOrderMasterID;
					
					//HACK: Modified by Tuan TQ: 15 June, 2006. RefMasterID must be RTV MasterID (for Stock Card report) 
					voTransactionHistory.RefMasterID = intReturnToVendorMasterID;
					//end hack

					//End Hack

					voTransactionHistory.LocationID = int.Parse(drow[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());
					if (drow[PO_ReturnToVendorDetailTable.BINID_FLD].ToString() != string.Empty)
					{
						voTransactionHistory.BinID = int.Parse(drow[PO_ReturnToVendorDetailTable.BINID_FLD].ToString());
					}
					if (drow[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString() != string.Empty)
					{
						voTransactionHistory.RefDetailID = int.Parse(drow[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString());
					}
					if (drow[PO_ReturnToVendorDetailTable.LOT_FLD].ToString() != string.Empty)
					{
						voTransactionHistory.Lot = drow[PO_ReturnToVendorDetailTable.LOT_FLD].ToString();
					}
					if (drow[PO_ReturnToVendorDetailTable.SERIAL_FLD].ToString() != string.Empty)
					{
						voTransactionHistory.Serial = drow[PO_ReturnToVendorDetailTable.SERIAL_FLD].ToString();
					}

					if (drow[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString() != string.Empty)
					{
						voTransactionHistory.ProductID = int.Parse(drow[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
					}

					if (drow[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString() != string.Empty)
					{
						voTransactionHistory.StockUMID = int.Parse(drow[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString());
					}

					voTransactionHistory.Quantity = Decimal.Parse(drow[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString());
					
					voTransactionHistory.TransDate = new UtilsBO().GetDBDate();
					
					boInventoryUtils.SaveTransactionHistory(Constants.TRANTYPE_PORETURNTOVENDOR, (int)PurposeEnum.ReturnToVendor, voTransactionHistory);
				} 
				// END: Trada 27-12-2005
				return intReturnToVendorMasterID;
			}
			catch (PCSBOException ex) 
			{
				throw ex;
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
	
		public MST_PartyVO GetPartyInfo(int pintPartyID)
		{
			try 
			{
				MST_PartyDS objMST_PartyDS = new MST_PartyDS();
				return (MST_PartyVO)objMST_PartyDS.GetObjectVO(pintPartyID);
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
	
		public string GetPartyLocationCode(int pintPartyLocationID)
		{
			try 
			{
				MST_PartyLocationDS objMST_PartyLocationDS = new MST_PartyLocationDS();
				return objMST_PartyLocationDS.GetLocationCode(pintPartyLocationID);

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
	
		public DataTable GetListOfReceivedProductsFromPurchaseOrder(int pintPurchaseOrderMasterID)
		{
			PO_PurchaseOrderDetailDS objPO_PurchaseOrderDetailDS = new PO_PurchaseOrderDetailDS();
			return objPO_PurchaseOrderDetailDS.GetListOfReceivedProductsFromPurchaseOrder(pintPurchaseOrderMasterID);
		}
	
		public DataTable GetListOfReceivedProductsFromInvoice(int pintInvoiceMasterID)
		{
			PO_ReturnToVendorDetailDS dsDetail = new PO_ReturnToVendorDetailDS();
			return dsDetail.GetListOfReceivedProductsFromInvoice(pintInvoiceMasterID);
		}
		
		public string BuildWhereClauseToSearchPurchaseOrder()
		{
			try 
			{
				string strWhereClause = String.Empty;
				strWhereClause = " WHERE " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." +  PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD ;
				strWhereClause += " IN ( SELECT " + PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERMASTERID_FLD ; 
				strWhereClause += "      FROM  " + PO_PurchaseOrderReceiptDetailTable.TABLE_NAME  ;
				strWhereClause += "     ) " ;

				return strWhereClause;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet GetDetailByInvoiceMasterID(int pintInvoiceMasterID)
		{
			PO_InvoiceDetailDS dsPO_InvoiceDetail = new PO_InvoiceDetailDS();
			return dsPO_InvoiceDetail.GetDetailByInvoiceMasterToReturn(pintInvoiceMasterID);
		}

		public decimal GetTwoUnitOfMeasureRate(int pintUnitID1, int pintUnitID2)
		{
			try 
			{
				MST_UMRateDS objMST_UMRateDS = new MST_UMRateDS();
				return objMST_UMRateDS.GetUMRate(pintUnitID1,pintUnitID2);
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
	
		public void UpdateInventoryInfor (DataSet dsReturnToVendorDetail,bool blnNewRecord, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			const string AVG_COST_FLD = "AVGCost";
			try 
			{
				UtilsBO boUtils = new UtilsBO();
				foreach (DataRow drowReturnedGoodsDetail in dsReturnToVendorDetail.Tables[0].Rows)
				{
					if (blnNewRecord && drowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
					{
						//in case of adding a new returned goods 
						//we don't care the deleted record
						//we only care the other states : Modified and AddNew
						continue;
					}
					int intStockUMID = int.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString());
					int intBuyingUMID = int.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString());
					Decimal decUMRate = boUtils.GetUMRate(intBuyingUMID, intStockUMID);
					if (decUMRate != 0)
					{
						drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD] = Decimal.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString())* decUMRate;	
					}
					else
					{
						throw new PCSException(ErrorCode.MESSAGE_MUST_SET_UMRATE, string.Empty, new Exception());
					}
					//calculate the avergae cost

					#region Update IV_BinCache
					if (drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD].ToString().Trim() != String.Empty)
					{
						//update bin cache
						UpdateIVBinCache(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
					}
					#endregion

					#region update into the IV_locationCache
					UpdateIVLocationCache(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
					#endregion

					#region Update into the IV_MasterLocationCache 
					UpdateIVMasterLocationCache(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
					#endregion


					#region UPDATE INTO TABLE MST_TransactionHistory
					/*
					 * update MST_TransactionHistory
					 * 
					 */
					//UpdateTransactionHistory(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
					#endregion

					#region INSERT INTO TABLE IV_CostHistory
					if (drowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.LOT_FLD].ToString().Trim() != String.Empty)
					{
						UpdateCostHistory(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
					}
					#endregion

					#region Add By CanhNV: {Update add Inventory for Bom tree}

					if (pobjPO_ReturnToVendorMasterVO.ProductionLineId > 0)
					{ 
						//1.Get BomDetail by ProductID
						DataTable dtBomDetail = new ITM_BOMDS().ListBomDetailOfProduct((int) drowReturnedGoodsDetail[ITM_ProductTable.PRODUCTID_FLD]);
						if (dtBomDetail.Rows.Count <=0)
						{
							return;
						}
						
						//2.Get LocationID and BinID by ProductionLineID
						DataTable dtLocBin = new PO_ReturnToVendorMasterDS().GetLocationBin(pobjPO_ReturnToVendorMasterVO.ProductionLineId);
						if(dtLocBin.Rows.Count == 0)
							throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
						int intProLocationID = Convert.ToInt32(dtLocBin.Rows[0][MST_BINTable.LOCATIONID_FLD]);
						int intProBinID = Convert.ToInt32(dtLocBin.Rows[0][MST_BINTable.BINID_FLD]);

						//3.Scan DataTable
						foreach (DataRow dataRow in dtBomDetail.Rows)
						{
							//3.1.Set value to voTransactionHistory
							MST_TransactionHistoryVO voTransactionHistory = new MST_TransactionHistoryVO();
							voTransactionHistory.TransDate = new UtilsBO().GetDBDate();
							voTransactionHistory.TranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionType.MATERIAL_ISSUE);		
                            voTransactionHistory.InspStatus = new MST_TranTypeDS().GetTranTypeID(TransactionType.RETURN_TO_VENDOR);
							voTransactionHistory.ProductID = (int) dataRow[ITM_BOMTable.COMPONENTID_FLD];
							voTransactionHistory.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;									
							voTransactionHistory.PostDate = pobjPO_ReturnToVendorMasterVO.PostDate;

							voTransactionHistory.RefMasterID = pobjPO_ReturnToVendorMasterVO.ReturnToVendorMasterID;
                            voTransactionHistory.RefDetailID = (int)drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD];
							
							voTransactionHistory.Quantity = -1*(decimal)drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD]*(decimal)dataRow[ITM_BOMTable.QUANTITY_FLD];
							decimal decQuantity = voTransactionHistory.Quantity;
							
							voTransactionHistory.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
							voTransactionHistory.LocationID = intProLocationID;
							voTransactionHistory.BinID = intProBinID;

							//3.2.Update Inventory
							new InventoryUtilsBO().UpdateSubtractOHQuantity(voTransactionHistory.CCNID,
								voTransactionHistory.MasterLocationID,
								voTransactionHistory.LocationID,
								voTransactionHistory.BinID,
								voTransactionHistory.ProductID,
								decQuantity,
								string.Empty,
								string.Empty);


							//3.3.Update TransactionHistory
							new InventoryUtilsBO().SaveTransactionHistory(TransactionType.MATERIAL_ISSUE, (int) PurposeEnum.ReturnToVendor, voTransactionHistory);
						}
					}

					#endregion
				}
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (PCSException ex)
			{
				throw ex;
			}			
		}
	
		public void UpdateIVBinCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			try
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
				bool blnHasProductID = objIV_BinCacheDS.HasProductID(int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString()),
					pobjPO_ReturnToVendorMasterVO.CCNID,
					pobjPO_ReturnToVendorMasterVO.MasterLocationID,
					int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString()),
					int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD].ToString()));

				//Initialize the VO for the MasLocCache object
				IV_BinCacheVO objIV_BinCacheVO = new IV_BinCacheVO();
				objIV_BinCacheVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
				objIV_BinCacheVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
				objIV_BinCacheVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
				objIV_BinCacheVO.LocationID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());
				objIV_BinCacheVO.BinID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD].ToString());


				//first we implement the case of totally adding new returned goods record
				//incase of updating an existing returned goods record
				//according to Mr.Nguyen Manh Cuong ==> Implement later ==> because it is very very complicated
				if (blnNewReturnedGood)
				{
					objIV_BinCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) * (-1);
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
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void UpdateCostHistory(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			const string RETURNED_GOODS_TRANSACTION_NAME = "POReturnToVendor";
			try
			{
				IV_CostHistoryVO objIV_CostHistoryVO = new IV_CostHistoryVO();

				objIV_CostHistoryVO.ReceiveDate = pobjPO_ReturnToVendorMasterVO.PostDate;
				objIV_CostHistoryVO.ReceiveRef = pobjPO_ReturnToVendorMasterVO.ReturnToVendorMasterID;
				objIV_CostHistoryVO.ReceiveRefLine = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString());
				//QA Status
				objIV_CostHistoryVO.QAStatus = 0;
				//Party ID
				objIV_CostHistoryVO.PartyID = pobjPO_ReturnToVendorMasterVO.PartyID;
				//Master Location
				objIV_CostHistoryVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
				//Product ID
				objIV_CostHistoryVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
				//CCN ID
				objIV_CostHistoryVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
				//Unit of measure
				if (pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString().Trim() != String.Empty) 
				{
					objIV_CostHistoryVO.StockUMID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString());
				}
				//get the TransType ID
				MST_TranTypeDS objMST_TranTypeDS = new MST_TranTypeDS();
				objIV_CostHistoryVO.TranTypeID = objMST_TranTypeDS.GetIDFromCode(RETURNED_GOODS_TRANSACTION_NAME);

				//Insert this record into the IV_CostHistory
				IV_CostHistoryDS objIV_CostHistoryDS = new IV_CostHistoryDS();
				objIV_CostHistoryDS.AddReturnedGoods(objIV_CostHistoryVO);
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
		public void UpdateIVLocationCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			try
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
				bool blnHasProductID = objIV_LocationCacheDS.HasProductID(int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString()),
					pobjPO_ReturnToVendorMasterVO.CCNID,
					pobjPO_ReturnToVendorMasterVO.MasterLocationID,
					int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString()));

				//Initialize the VO for the MasLocCache object
				IV_LocationCacheVO objIV_LocationCacheVO = new IV_LocationCacheVO();
				objIV_LocationCacheVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
				objIV_LocationCacheVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
				objIV_LocationCacheVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
				objIV_LocationCacheVO.LocationID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());


				//first we implement the case of totally adding new returned goods record
				//incase of updating an existing returned goods record
				//according to Mr.Nguyen Manh Cuong ==> Implement later ==> because it is very very complicated
				if (blnNewReturnedGood)
				{
					objIV_LocationCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) * (-1);
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
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public void UpdateIVMasterLocationCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			try
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

				bool blnHasProductID = objIV_MasLocCacheDS.HasProductID(int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString()),
					pobjPO_ReturnToVendorMasterVO.CCNID,
					pobjPO_ReturnToVendorMasterVO.MasterLocationID);

				//Initialize the VO for the MasLocCache object
				IV_MasLocCacheVO objIV_MasLocCacheVO = new IV_MasLocCacheVO();
				objIV_MasLocCacheVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
				objIV_MasLocCacheVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
				objIV_MasLocCacheVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
				//objIV_MasLocCacheVO.AVGCost = (float)pintAVGCost;

				//first we implement the case of totally adding new returned goods record
				//incase of updating an existing returned goods record
				//according to Mr.Nguyen Manh Cuong ==> Implement later ==> because it is very very complicated
				if (blnNewReturnedGood)
				{
					objIV_MasLocCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) * (-1);
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
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		public void UpdateTransactionHistory(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
//			const string RETURNED_GOODS_TRANSACTION_NAME = "POReturnToVendor";
//			const string QASTATUS_STATUS_1 = "1";
//			const string QASTATUS_STATUS_3 = "3";
//
//			try
//			{
//				if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
//				{
//					//in case of adding a new returned goods 
//					//we don't care the deleted record
//					//we only care the other states : Modified and AddNew
//					return ;
//				}
//				MST_TransactionHistoryVO objMST_TransactionHistoryVO = new MST_TransactionHistoryVO();
//				objMST_TransactionHistoryVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
//				if (pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.BINID_FLD].ToString().Trim() != String.Empty) 
//				{
//					objMST_TransactionHistoryVO.BinID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD].ToString().Trim());
//				}
//				objMST_TransactionHistoryVO.TransDate = pobjPO_ReturnToVendorMasterVO.PostDate;
//				objMST_TransactionHistoryVO.RefMasterID = pobjPO_ReturnToVendorMasterVO.ReturnToVendorMasterID;
//				objMST_TransactionHistoryVO.RefDetailID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString().Trim());
//				objMST_TransactionHistoryVO.Lot = pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOT_FLD].ToString().Trim();
//				objMST_TransactionHistoryVO.Serial = pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.SERIAL_FLD].ToString().Trim();
//
//				//try to get the inspection status
//				objMST_TransactionHistoryVO.InspStatus = 0;
//
//				objMST_TransactionHistoryVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
//				
//				//get the TransType ID
//				MST_TranTypeDS objMST_TranTypeDS = new MST_TranTypeDS();
//				objMST_TransactionHistoryVO.TranTypeID = objMST_TranTypeDS.GetIDFromCode(RETURNED_GOODS_TRANSACTION_NAME);
//
//
//				objMST_TransactionHistoryVO.PartyID = pobjPO_ReturnToVendorMasterVO.PartyID;
//				//objMST_TransactionHistoryVO.PartyLocationID = pobjPO_ReturnToVendorMasterVO.ShipFormLocID;
//				objMST_TransactionHistoryVO.PartyLocationID = pobjPO_ReturnToVendorMasterVO.PurchaseLocID;
//				objMST_TransactionHistoryVO.LocationID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString().Trim());
//				objMST_TransactionHistoryVO.ProductID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString().Trim());
//				if (pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString().Trim() != String.Empty) 
//				{
//					objMST_TransactionHistoryVO.StockUMID = int.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString().Trim());
//				}
//				
//				//calculate the NewAvg Cost
//				//1.Get the OnHand Quantity from Master Location Cache
//				//2.Calculate the Average Cost 
//				//3.Then NewAvgCost = (ReceiveQty + OnHanQty)/ ((ReceiveQty * AvgCost) + (OnHanQty * AvgCost))
//				/*
//				IV_MasLocCacheDS objIV_MasLocCacheDS = new IV_MasLocCacheDS();
//				decimal dcmOnHanQty = objIV_MasLocCacheDS.GetOnHanQty(objMST_TransactionHistoryVO.ProductID,objMST_TransactionHistoryVO.CCNID,objMST_TransactionHistoryVO.MasterLocationID);
//				if (pdcmAvgCost < 0)
//				{
//					pdcmAvgCost = 0;
//				}
//				decimal dcmReceiveQty = decimal.Parse(pdrowReturnedGoodsDetail[SO_ReturnedGoodsDetailTable.RECEIVEQUANTITY_FLD].ToString());
//
//				try 
//				{
//					objMST_TransactionHistoryVO.NewAvgCost = (dcmReceiveQty + dcmOnHanQty) / ((dcmReceiveQty * pdcmAvgCost)  + (dcmOnHanQty * pdcmAvgCost));
//				}
//				catch
//				{
//					objMST_TransactionHistoryVO.NewAvgCost = 0;
//				}
//				*/
//
//
//
//				//using the DS class to add this record into database
//				MST_TransactionHistoryDS objMST_TransactionHistoryDS = new MST_TransactionHistoryDS();
//				//objMST_TransactionHistoryDS.AddReturnedGoods(objMST_TransactionHistoryVO);
//				objMST_TransactionHistoryDS.Add(objMST_TransactionHistoryVO);
//
//			}
//			catch (PCSDBException ex)
//			{
//				throw ex;
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}

		}
	
		public string GetUnitCode(int pintUnitID)
		{
			try 
			{
				MST_UnitOfMeasureDS objMST_UnitOfMeasureDS = new MST_UnitOfMeasureDS();
				MST_UnitOfMeasureVO objMST_UnitOfMeasureVO = (MST_UnitOfMeasureVO) objMST_UnitOfMeasureDS.GetObjectVO(pintUnitID);
				return objMST_UnitOfMeasureVO.Code;
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
		public void DeleteReturnToVendor(PO_ReturnToVendorMasterVO pvoReturnToVendorMaster,DataSet pdstReturnToVendorDetail)
		{
			InventoryUtilsBO boInven = new InventoryUtilsBO();
			//ReturnToVendorBO objReturnToVendorBO = new ReturnToVendorBO();
			//DataSet dstReturnToVendorDetail = GetReturnGoodsDetail(pintReturnToVendorMasterID);
			foreach(DataRow drowDetail in pdstReturnToVendorDetail.Tables[0].Rows)
			{
				boInven.UpdateAddOHQuantity(pvoReturnToVendorMaster.CCNID,pvoReturnToVendorMaster.MasterLocationID,(int)drowDetail["LocationID"],(int)drowDetail["BinID"],(int)drowDetail["ProductID"],(decimal)drowDetail["Quantity"],string.Empty,string.Empty);
			}
			(new PO_ReturnToVendorMasterDS()).DeleteReturnToVendor(pvoReturnToVendorMaster.ReturnToVendorMasterID);
		}
		public void DeleteReturnToVendor(int printReturnToVendorMasterID)
		{
			#region 1. Variable
			InventoryUtilsBO boInventory = new InventoryUtilsBO();
            PO_ReturnToVendorMasterDS RTVMasterDS = new PO_ReturnToVendorMasterDS();
            PO_ReturnToVendorDetailDS RTVDetailDS = new PO_ReturnToVendorDetailDS();
			MST_TransactionHistoryDS TransactionDS = new MST_TransactionHistoryDS();
            DataSet dsRTVDetail = null;
            PO_ReturnToVendorMasterVO voRTVMaster = new PO_ReturnToVendorMasterVO();
			#endregion

			#region 2. Get Master Infomation
            voRTVMaster = (PO_ReturnToVendorMasterVO) RTVMasterDS.GetObjectVORTV(printReturnToVendorMasterID);
			if (voRTVMaster == null)
			{
				return;
			}
			#endregion

			#region 3. Get Detail Infomation
            dsRTVDetail = RTVDetailDS.ListReturnToVendorDetail(voRTVMaster.ReturnToVendorMasterID);
			#endregion

			#region 4. Update Inventory
			foreach (DataRow drow in dsRTVDetail.Tables[0].Rows)
			{
				//4.0 Variable
				int LocationID = (int)drow[PO_ReturnToVendorDetailTable.LOCATIONID_FLD];
				int BinID = (int)drow[PO_ReturnToVendorDetailTable.BINID_FLD];
				int ProductionID = (int)drow[PO_ReturnToVendorDetailTable.PRODUCTID_FLD];
				decimal decQuantity = (decimal) drow[PO_ReturnToVendorDetailTable.QUANTITY_FLD];
				//4.1 Update Add Item in Detail 
				boInventory.UpdateAddOHQuantity(voRTVMaster.CCNID,voRTVMaster.MasterLocationID,LocationID,BinID,ProductionID,decQuantity,null,null);

				#region 4.2. Update Substract Bom Item
				if (voRTVMaster.ProductionLineId > 0)
				{ 
					//4.2.1.Get BomDetail by ProductID
					DataTable dtBomDetail = new ITM_BOMDS().ListBomDetailOfProduct((int) drow[ITM_ProductTable.PRODUCTID_FLD]);
					if (dtBomDetail.Rows.Count <=0)
					{
						return;
					}
						
					//4.2.2.Get LocationID and BinID by ProductionLineID
					DataTable dtLocBin = new PO_ReturnToVendorMasterDS().GetLocationBin(voRTVMaster.ProductionLineId);
					
					int intProLocationID = Convert.ToInt32(dtLocBin.Rows[0][MST_BINTable.LOCATIONID_FLD]);
					int intProBinID = Convert.ToInt32(dtLocBin.Rows[0][MST_BINTable.BINID_FLD]);

					//4.2.3.Scan DataTable
					foreach (DataRow dataRow in dtBomDetail.Rows)
					{
						int intBomProductionID = (int) dataRow[ITM_BOMTable.COMPONENTID_FLD];
						decimal decBomQuantity = (decimal)drow[PO_ReturnToVendorDetailTable.QUANTITY_FLD]*(decimal)dataRow[ITM_BOMTable.QUANTITY_FLD];
						
						new InventoryUtilsBO().UpdateSubtractOHQuantity(voRTVMaster.CCNID,
							voRTVMaster.MasterLocationID,
							intProLocationID,
							intProBinID,
							intBomProductionID,
							decBomQuantity,
							string.Empty,
							string.Empty);						
					}
				}
				#endregion
				
			}
			#endregion

			#region 5. Update TransactionHistory 
			int OldTranTypeIDBom = new MST_TranTypeDS().GetTranTypeID(TransactionType.MATERIAL_ISSUE);
			int OldTranTypeIDDetail = new MST_TranTypeDS().GetTranTypeID(TransactionType.RETURN_TO_VENDOR);
			int InspStatus=12;
			//5.1 Update TransactionHistory by Item detail
			TransactionDS.UpdateTranType(voRTVMaster.ReturnToVendorMasterID,OldTranTypeIDDetail,(int)TransactionTypeEnum.DeleteTransaction,InspStatus);
			//5.2 Update TransactionHistory by Bom Item
			TransactionDS.UpdateTranType(voRTVMaster.ReturnToVendorMasterID,OldTranTypeIDBom,(int)TransactionTypeEnum.DeleteTransaction,InspStatus);
			#endregion
			
			#region 6. Delete ReturnToVendor
            RTVMasterDS.DeleteReturnToVendor(voRTVMaster.ReturnToVendorMasterID);
			#endregion

		}
	}
}

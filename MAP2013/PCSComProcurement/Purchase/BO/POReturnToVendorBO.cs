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
	public interface IPOReturnToVendor{}
	
	
	public class POReturnToVendorBO : IPOReturnToVendor
	{
		#region IObjectBO Members

	
		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			// TODO:  Add POReturnToVendor.UpdateDataSet implementation
		}

	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add POReturnToVendor.Update implementation
		}

	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add POReturnToVendor.Delete implementation
		}

	
		public void Add(object pObjectDetail)
		{
			// TODO:  Add POReturnToVendor.Add implementation
		}

	
		public object GetObjectVO(int pintID, string VOclass)
		{
			PO_ReturnToVendorMasterDS dsMaster = new PO_ReturnToVendorMasterDS();
			return dsMaster.GetObjectVO(pintID);
		}
	
		public object GetMasterInfo(int pintID, out DataRow odrowInfo)
		{
			odrowInfo = null;
			PO_ReturnToVendorMasterDS dsMaster = new PO_ReturnToVendorMasterDS();
			odrowInfo = dsMaster.GetReturnToVendorMasterInfo(pintID).Rows[0];
			return dsMaster.GetObjectVO(pintID);
		}

		#endregion

	
		public DataSet GetDetailData(int pintReturnMasterID)
		{
			PO_ReturnToVendorDetailDS dsDetail = new PO_ReturnToVendorDetailDS();
			return dsDetail.List(pintReturnMasterID);
		}
	
		public DataRow GetVendorInfo(int pintVendorLocationID)
		{
			MST_PartyDS dsParty = new MST_PartyDS();
			return dsParty.GetVendorInfo(pintVendorLocationID);
		}
	
		public DataTable GetListOfReceivedProductsFromPurchaseOrder(int pintPurchaseOrderMasterID)
		{
			PO_PurchaseOrderDetailDS objPO_PurchaseOrderDetailDS = new PO_PurchaseOrderDetailDS();
			return objPO_PurchaseOrderDetailDS.GetListOfReceivedProductsFromPurchaseOrder(pintPurchaseOrderMasterID);
		}
	
		public DataSet GetDetailByInvoiceMasterID(int pintInvoiceMasterID)
		{
			PO_ReturnToVendorDetailDS dsDetail = new PO_ReturnToVendorDetailDS();
			DataSet dstTemp = dsDetail.GetDetailByInvoiceMasterToReturn(pintInvoiceMasterID);
			DataSet dstData = dstTemp.Clone();
			int intLine = 0;
			foreach (DataRow drowData in dstTemp.Tables[0].Rows)
			{
				DataRow drowNew = dstData.Tables[0].NewRow();
				foreach (DataColumn dcolData in dstData.Tables[0].Columns)
					drowNew[dcolData.ColumnName] = drowData[dcolData.ColumnName];
				drowNew[PO_ReturnToVendorDetailTable.LINE_FLD] = ++ intLine;
				dstData.Tables[0].Rows.Add(drowNew);
			}
			return dstData;
		}
	
		public int AddNewReturnToVendor(PO_ReturnToVendorMasterVO pobjReturnToVendorMasterVO, DataSet pdstDetail)
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
				objPO_ReturnToVendorDetailDS.UpdateReturnToVendorDataSet(pdstDetail, intReturnToVendorMasterID);
			else if (pobjReturnToVendorMasterVO.InvoiceMasterID != 0)
				objPO_ReturnToVendorDetailDS.UpdateDataSetForInvoice(pdstDetail);

			// re-load dataset
			pdstDetail = objPO_ReturnToVendorDetailDS.List(intReturnToVendorMasterID);
			
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

				//HACK: Modified by Tuan TQ: 15 June, 2006. RefMasterID must be RTV MasterID (for Stock Card report) 
				voTransactionHistory.RefMasterID = intReturnToVendorMasterID;
				//end hack

				//End Hack

				voTransactionHistory.LocationID = int.Parse(drow[PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());
				if (drow[PO_ReturnToVendorDetailTable.BINID_FLD].ToString() != string.Empty)
					voTransactionHistory.BinID = int.Parse(drow[PO_ReturnToVendorDetailTable.BINID_FLD].ToString());
				if (drow[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString() != string.Empty)
					voTransactionHistory.RefDetailID = int.Parse(drow[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD].ToString());
				if (drow[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString() != string.Empty)
					voTransactionHistory.ProductID = int.Parse(drow[PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
				if (drow[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString() != string.Empty)
					voTransactionHistory.StockUMID = int.Parse(drow[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString());
				voTransactionHistory.Quantity = Decimal.Parse(drow[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString());
				voTransactionHistory.TransDate = new UtilsBO().GetDBDate();
					
				boInventoryUtils.SaveTransactionHistory(Constants.TRANTYPE_PORETURNTOVENDOR, (int)PurposeEnum.ReturnToVendor, voTransactionHistory);
			} 
			// END: Trada 27-12-2005
			return intReturnToVendorMasterID;
		}
		private void CheckOnHandQty(DataSet dstData, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO) 
		{
			DateTime dtmCurrentDate = new UtilsBO().GetDBDate().AddDays(1);
			InventoryUtilsBO boIVUtils = new InventoryUtilsBO();
			decimal dcmOnHandQty, decOnHandCurrent =0;
			for (int i=0; i<dstData.Tables[0].Rows.Count;i++)
			{
				if (dstData.Tables[0].Rows[i].RowState != DataRowState.Deleted)
				{
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
						dcmOnHandQty = boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
							objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
						decOnHandCurrent = boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_BinCacheVO.CCNID, objIV_BinCacheVO.MasterLocationID,
							objIV_BinCacheVO.LocationID, objIV_BinCacheVO.BinID, objIV_BinCacheVO.ProductID);
					}
					else
					{
						IV_LocationCacheVO objIV_LocationCacheVO = new IV_LocationCacheVO();
						objIV_LocationCacheVO.ProductID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.PRODUCTID_FLD].ToString());
						objIV_LocationCacheVO.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
						objIV_LocationCacheVO.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;
						objIV_LocationCacheVO.LocationID = int.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.LOCATIONID_FLD].ToString());
						
						dcmOnHandQty =  boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
							objIV_LocationCacheVO.LocationID, 0, objIV_LocationCacheVO.ProductID);
						decOnHandCurrent =  boIVUtils.GetAvailableQtyByPostDate(dtmCurrentDate, objIV_LocationCacheVO.CCNID, objIV_LocationCacheVO.MasterLocationID,
							objIV_LocationCacheVO.LocationID, 0, objIV_LocationCacheVO.ProductID);
					}
					decimal dcmUMRate = 1;
					if (pobjPO_ReturnToVendorMasterVO.PurchaseOrderMasterID != 0)
					{
						//get the UMRate 
						
						if (dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.UMRATE_FLD].ToString() != String.Empty)
							dcmUMRate = decimal.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.UMRATE_FLD].ToString());
						else
							dcmUMRate = 1;
					}
					decimal dcmReturnQuantity = decimal.Parse(dstData.Tables[0].Rows[i][PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString());
					if ( (dcmOnHandQty - (dcmReturnQuantity * dcmUMRate)) < 0) 
						throw new PCSBOException(ErrorCode.MESSAGE_RGA_OVERONHANDQTY,i.ToString(),null);
					else
						if (decOnHandCurrent < (dcmReturnQuantity * dcmUMRate))
						throw new PCSBOException(ErrorCode.MESSAGE_AVAILABLE_WAS_USED_AFTER_POSTDATE, i.ToString(),null);
				}
			}
		}
		private void UpdateInventoryInfor(DataSet dsReturnToVendorDetail,bool blnNewRecord, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			UtilsBO boUtils = new UtilsBO();
			foreach (DataRow drowReturnedGoodsDetail in dsReturnToVendorDetail.Tables[0].Rows)
			{
				if (blnNewRecord && drowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
					continue;
				int intStockUMID = int.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.STOCKUMID_FLD].ToString());
				int intBuyingUMID = int.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BUYINGUMID_FLD].ToString());
				Decimal decUMRate = boUtils.GetUMRate(intBuyingUMID, intStockUMID);
				if (decUMRate != 0)
					drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD] = Decimal.Parse(drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString())* decUMRate;	
				else
					throw new PCSException(ErrorCode.MESSAGE_MUST_SET_UMRATE, string.Empty, new Exception());
				
				#region Update IV_BinCache
				if (drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.BINID_FLD].ToString().Trim() != String.Empty)
					UpdateIVBinCache(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
				#endregion

				#region update into the IV_locationCache
				UpdateIVLocationCache(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
				#endregion

				#region Update into the IV_MasterLocationCache 
				UpdateIVMasterLocationCache(drowReturnedGoodsDetail,blnNewRecord,pobjPO_ReturnToVendorMasterVO);
				#endregion

				#region Add By CanhNV: {Update add Inventory for Bom tree}

				if (pobjPO_ReturnToVendorMasterVO.ProductionLineId > 0)
				{ 
					//1.Get BomDetail by ProductID
					DataTable dtBomDetail = new ITM_BOMDS().ListBomDetailOfProduct((int) drowReturnedGoodsDetail[ITM_ProductTable.PRODUCTID_FLD]);
					if (dtBomDetail.Rows.Count <=0)
						return;
						
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
						voTransactionHistory.TranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionTypeEnum.IVMiscellaneousIssue.ToString());		
						voTransactionHistory.InspStatus = new MST_TranTypeDS().GetTranTypeID(TransactionTypeEnum.POReturnToVendor.ToString());
						voTransactionHistory.ProductID = (int) dataRow[ITM_BOMTable.COMPONENTID_FLD];
						voTransactionHistory.CCNID = pobjPO_ReturnToVendorMasterVO.CCNID;									
						voTransactionHistory.PostDate = pobjPO_ReturnToVendorMasterVO.PostDate;

						voTransactionHistory.RefMasterID = pobjPO_ReturnToVendorMasterVO.ReturnToVendorMasterID;
						try
						{
							voTransactionHistory.RefDetailID = (int)drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.RETURNTOVENDORDETAILID_FLD];
						}
						catch{}
							
						voTransactionHistory.Quantity = (decimal)drowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD]*(decimal)dataRow[ITM_BOMTable.QUANTITY_FLD];
						decimal decQuantity = voTransactionHistory.Quantity;
							
						voTransactionHistory.MasterLocationID = pobjPO_ReturnToVendorMasterVO.MasterLocationID;
						voTransactionHistory.LocationID = intProLocationID;
						voTransactionHistory.BinID = intProBinID;

						//3.2.Update Inventory
						// 
						new InventoryUtilsBO().UpdateAddOHQuantity(voTransactionHistory.CCNID,
							voTransactionHistory.MasterLocationID,
							voTransactionHistory.LocationID,
							voTransactionHistory.BinID,
							voTransactionHistory.ProductID,
							decQuantity,
							string.Empty,
							string.Empty);


						//3.3.Update TransactionHistory
						new InventoryUtilsBO().SaveTransactionHistory(TransactionTypeEnum.IVMiscellaneousIssue.ToString(), (int) PurposeEnum.ReturnToVendor, voTransactionHistory);
					}
				}

				#endregion
			}
		}

		private void UpdateIVBinCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
				return;

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


			if (blnNewReturnedGood)
				objIV_BinCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) * (-1);
			if (blnHasProductID)
				objIV_BinCacheDS.UpdateReturnedGoods(objIV_BinCacheVO);
			else
				objIV_BinCacheDS.AddReturnedGoods(objIV_BinCacheVO);
		}
		private void UpdateIVLocationCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted) 
				return;

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
				objIV_LocationCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) * (-1);
			
			if (blnHasProductID)
				objIV_LocationCacheDS.UpdateReturnedGoods(objIV_LocationCacheVO);
			else
				objIV_LocationCacheDS.AddReturnedGoods(objIV_LocationCacheVO);
		}
		private void UpdateIVMasterLocationCache(DataRow pdrowReturnedGoodsDetail,bool blnNewReturnedGood, PO_ReturnToVendorMasterVO pobjPO_ReturnToVendorMasterVO)
		{
			if (blnNewReturnedGood && pdrowReturnedGoodsDetail.RowState == DataRowState.Deleted)
				return;

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
				objIV_MasLocCacheVO.OHQuantity = Decimal.Parse(pdrowReturnedGoodsDetail[PO_ReturnToVendorDetailTable.QUANTITY_FLD].ToString()) * (-1);
			if (blnHasProductID)
				objIV_MasLocCacheDS.UpdateReturnedGoods(objIV_MasLocCacheVO);
			else
				objIV_MasLocCacheDS.AddReturnedGoods(objIV_MasLocCacheVO);
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
			int OldTranTypeIDBom = new MST_TranTypeDS().GetTranTypeID(TransactionTypeEnum.IVMiscellaneousIssue.ToString());
			int OldTranTypeIDDetail = new MST_TranTypeDS().GetTranTypeID(TransactionTypeEnum.POReturnToVendor.ToString());
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

using System;
using System.Data;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using PCSComMaterials.Inventory.DS;
using PCSComMaterials.Inventory.BO;
using PCSComProcurement.Purchase.DS;
using PCSComProduct.Costing.DS;
using PCSComProduct.Items.BO;
using PCSComProduct.Items.DS;
using PCSComUtils.Common;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;


namespace PCSComProcurement.Purchase.BO
{
	public class POPurchaseOrderReceiptsBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.POPurchaseOrderReceiptsBO";

	    public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  

		}
	
		public void UpdateDataSet(DataSet dstData)
		{
			//			try
			//			{
			PO_PurchaseOrderReceiptDetailDS dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			dsReceiptDetail.UpdateDataSet(dstData);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public int AddMasterReceipt(object pobjMasterObject, DataSet pdstDetailData, int pintCCNID, DateTime pdtmServerDate)
		{
			#region Variables

			PO_PurchaseOrderReceiptMasterDS dsMasterReceipt = new PO_PurchaseOrderReceiptMasterDS();
			PO_PurchaseOrderReceiptDetailDS dsDetail = new PO_PurchaseOrderReceiptDetailDS();
			PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
			MST_TransactionHistoryDS dsTransaction = new MST_TransactionHistoryDS();
			ITM_BOMDS dsBOM = new ITM_BOMDS();
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			InventoryUtilsBO boIv = new InventoryUtilsBO();
			StringBuilder sbDeliveryScheduleIDs = new StringBuilder();
			StringBuilder sbLocationID = new StringBuilder();
			StringBuilder sbBinID = new StringBuilder();
			StringBuilder sbProductID = new StringBuilder();
			int intTranTypeID = new MST_TranTypeDS().GetTranTypeID(TransactionTypeEnum.POPurchaseOrderReceipts.ToString());

			#endregion

			#region validate data
			PO_PurchaseOrderReceiptMasterVO voMasterReceipt = (PO_PurchaseOrderReceiptMasterVO)pobjMasterObject;
			int intProLocationID = 0, intProBinID = 0;
			DataTable dtbLocationBin = new DataTable();
			if (voMasterReceipt.ProductionLineID > 0)
			{
				dtbLocationBin = dsDetail.GetLocationBin(voMasterReceipt.ProductionLineID);
				if(dtbLocationBin.Rows.Count == 0)
					throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
				intProLocationID = Convert.ToInt32(dtbLocationBin.Rows[0][MST_BINTable.LOCATIONID_FLD]);
				intProBinID = Convert.ToInt32(dtbLocationBin.Rows[0][MST_BINTable.BINID_FLD]);
				sbLocationID.Append(intProLocationID.ToString()).Append(",");
				sbBinID.Append(intProBinID.ToString()).Append(",");
			}
			#endregion

			#region receipt master & detail data

			// add new Master Receipt first
			voMasterReceipt.PurchaseOrderReceiptID = dsMasterReceipt.AddAndReturnID(pobjMasterObject);
			string strPODetailIDs = "(";
			// assign master receipt to each detail
			foreach (DataRow drowData in pdstDetailData.Tables[0].Rows)
			{
				// ignore deleted row
				if (drowData.RowState == DataRowState.Deleted)
					continue;
				drowData[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD] = voMasterReceipt.PurchaseOrderReceiptID;
				strPODetailIDs += drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD] + ",";
				sbLocationID.Append(drowData[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].ToString()).Append(",");
				sbBinID.Append(drowData[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].ToString()).Append(",");
				sbProductID.Append(drowData[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].ToString()).Append(",");
				string strComponentID = dsBOM.GetComponentOfItem(drowData[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].ToString());
				if (strComponentID != string.Empty)
					sbProductID.Append(strComponentID);
			}
			if(strPODetailIDs.Length > 1)
				strPODetailIDs = strPODetailIDs.Substring(0,strPODetailIDs.Length-1) + ")";
			else
				strPODetailIDs = string.Empty;
			sbLocationID.Append("0");
			sbBinID.Append("0");
			sbProductID.Append("0");
			// update detail dataset
			dsDetail.UpdateDataSet(pdstDetailData);

			#endregion

			#region prepare data

			// Check receipted to Close PO
			dsDetail.CheckToClosePO(voMasterReceipt.PurchaseOrderMasterID,strPODetailIDs);
			// refresh the detail list to get extract ID from database
			DataTable dtbNewDetail = dsDetail.ListByMaster(voMasterReceipt.PurchaseOrderReceiptID);
			// onhand data for transaction history
			DataSet dstOnhandData = dsTransaction.RetrieveCacheData(voMasterReceipt.MasterLocationID, sbLocationID.ToString(),
				sbBinID.ToString(), sbProductID.ToString());
			DataTable dtbMasLocCacheData = dstOnhandData.Tables[0];
			DataTable dtbLocCacheData = dstOnhandData.Tables[1];
			DataTable dtbBinCacheData = dstOnhandData.Tables[2];
			// get transaction history table schema
			DataTable dtbTransaction = dsTransaction.GetSchema();

			#endregion

			#region update inventory

			if ((dtbNewDetail != null) && (dtbNewDetail.Rows.Count > 0))
			{
				foreach (DataRow drowData in dtbNewDetail.Rows)
				{
					int intProductID = Convert.ToInt32(drowData[ITM_ProductTable.PRODUCTID_FLD]);
					sbDeliveryScheduleIDs.Append(drowData[PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD].ToString()).Append(",");
					
					decimal decReceiveQuantity = 0, decReceiveQuantityUMRate = 0, decUMRate = 0;
					int intBinID = 0, intLocationID = 0;
					// get ReceiveQuantity from PO Receipt
					try
					{
						decReceiveQuantity = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());
						decUMRate = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString().Trim());
						decReceiveQuantityUMRate = decReceiveQuantity * decUMRate;
					}
					catch{}
					
					#region Inventory
					try
					{
						intBinID = int.Parse(drowData[MST_BINTable.BINID_FLD].ToString());
					}
					catch{}
					try
					{
						intLocationID = int.Parse(drowData[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
					}
					catch
					{
						throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
					}
					boIv.UpdateAddOHQuantity(pintCCNID, voMasterReceipt.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantityUMRate, string.Empty, string.Empty);

					#endregion

					#region Transasction History
					decimal decOHQuantity = 0, decCommitQuantity = 0;
					DataRow drowTransaction = dtbTransaction.NewRow();
					drowTransaction[MST_TransactionHistoryTable.CCNID_FLD] = voMasterReceipt.CCNID;
					drowTransaction[MST_TransactionHistoryTable.TRANSDATE_FLD] = DateTime.Now;
					drowTransaction[MST_TransactionHistoryTable.POSTDATE_FLD] = voMasterReceipt.PostDate;
					drowTransaction[MST_TransactionHistoryTable.REFMASTERID_FLD] = voMasterReceipt.PurchaseOrderReceiptID;
					drowTransaction[MST_TransactionHistoryTable.REFDETAILID_FLD] = drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];
					drowTransaction[MST_TransactionHistoryTable.PRODUCTID_FLD] = intProductID;
					drowTransaction[MST_TransactionHistoryTable.TRANTYPEID_FLD] = intTranTypeID;
					drowTransaction[MST_TransactionHistoryTable.USERNAME_FLD] = voMasterReceipt.Username;
					drowTransaction[MST_TransactionHistoryTable.QUANTITY_FLD] = decReceiveQuantity;
					drowTransaction[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD] = voMasterReceipt.MasterLocationID;
					decOHQuantity = GetQuantityFromCache(dtbMasLocCacheData, voMasterReceipt.MasterLocationID, intProductID, 1, out decCommitQuantity);
					drowTransaction[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD] = decOHQuantity;
					drowTransaction[MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD] = decCommitQuantity;
					drowTransaction[MST_TransactionHistoryTable.LOCATIONID_FLD] = intLocationID;
					decOHQuantity = GetQuantityFromCache(dtbLocCacheData, intLocationID, intProductID, 2, out decCommitQuantity);
					drowTransaction[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD] = decOHQuantity;
					drowTransaction[MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD] = decCommitQuantity;
					drowTransaction[MST_TransactionHistoryTable.BINID_FLD] = intBinID;
					decOHQuantity = GetQuantityFromCache(dtbBinCacheData, intBinID, intProductID, 3, out decCommitQuantity);
					drowTransaction[MST_TransactionHistoryTable.BINOHQUANTITY_FLD] = decOHQuantity;
					drowTransaction[MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD] = decCommitQuantity;
					drowTransaction[MST_TransactionHistoryTable.STOCKUMID_FLD] = drowData[ITM_ProductTable.STOCKUMID_FLD];
					dtbTransaction.Rows.Add(drowTransaction);
					#endregion
				}
			}

			#endregion

			#region Receipt by outside

			if(voMasterReceipt.ReceiptType == (int)POReceiptTypeEnum.ByOutside)
			{
				BomBO boBom = new BomBO();
				foreach (DataRow drow in dtbNewDetail.Rows)
				{
					DataTable dtbBom = boBom.ListBOMDetailsOfProduct(Convert.ToInt32(drow[ITM_ProductTable.PRODUCTID_FLD]));
					foreach(DataRow drowBom in dtbBom.Rows)
					{
						#region subtract cache

						// Get available quantity by Postdate
						int intComponentID = Convert.ToInt32(drowBom[ITM_BOMTable.COMPONENTID_FLD]);

						decimal decAvail = boIv.GetAvailableQtyByPostDate(pdtmServerDate,
							voMasterReceipt.CCNID,voMasterReceipt.MasterLocationID,intProLocationID,intProBinID,
							intComponentID);
						decimal decBOMQty = Convert.ToDecimal(drowBom[ITM_BOMTable.QUANTITY_FLD]);
						decimal decOrderQty = Convert.ToDecimal(drow[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
						if(decAvail < decOrderQty * decBOMQty)
							throw new PCSBOException(ErrorCode.MESSAGE_ISSUE_MATERIAL_TO_OUTSIDE, string.Empty, new Exception());
						try
						{
							boIv.UpdateSubtractOHQuantity(voMasterReceipt.CCNID,voMasterReceipt.MasterLocationID,
								intProLocationID,intProBinID,Convert.ToInt32(drowBom[ITM_BOMTable.COMPONENTID_FLD]),decBOMQty*decOrderQty,string.Empty,string.Empty);
						}
						catch (PCSBOException ex)
						{
							throw ex;
						}

						#endregion

						#region Transasction History
						decimal decOHQuantity = 0, decCommitQuantity = 0;
						DataRow drowTransaction = dtbTransaction.NewRow();
						drowTransaction[MST_TransactionHistoryTable.CCNID_FLD] = voMasterReceipt.CCNID;
						drowTransaction[MST_TransactionHistoryTable.TRANSDATE_FLD] = DateTime.Now;
						drowTransaction[MST_TransactionHistoryTable.POSTDATE_FLD] = voMasterReceipt.PostDate;
						drowTransaction[MST_TransactionHistoryTable.REFMASTERID_FLD] = voMasterReceipt.PurchaseOrderReceiptID;
						drowTransaction[MST_TransactionHistoryTable.REFDETAILID_FLD] = drow[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];
						drowTransaction[MST_TransactionHistoryTable.PRODUCTID_FLD] = intComponentID;
						drowTransaction[MST_TransactionHistoryTable.TRANTYPEID_FLD] = intTranTypeID;
						drowTransaction[MST_TransactionHistoryTable.USERNAME_FLD] = voMasterReceipt.Username;
						drowTransaction[MST_TransactionHistoryTable.QUANTITY_FLD] = -decOrderQty * decBOMQty;
						drowTransaction[MST_TransactionHistoryTable.MASTERLOCATIONID_FLD] = voMasterReceipt.MasterLocationID;
						decOHQuantity = GetQuantityFromCache(dtbMasLocCacheData, voMasterReceipt.MasterLocationID, intComponentID, 1, out decCommitQuantity);
						drowTransaction[MST_TransactionHistoryTable.MASLOCOHQUANTITY_FLD] = decOHQuantity;
						drowTransaction[MST_TransactionHistoryTable.MASLOCCOMMITQUANTITY_FLD] = decCommitQuantity;
						drowTransaction[MST_TransactionHistoryTable.LOCATIONID_FLD] = intProLocationID;
						decOHQuantity = GetQuantityFromCache(dtbLocCacheData, intProLocationID, intComponentID, 2, out decCommitQuantity);
						drowTransaction[MST_TransactionHistoryTable.LOCATIONOHQUANTITY_FLD] = decOHQuantity;
						drowTransaction[MST_TransactionHistoryTable.LOCATIONCOMMITQUANTITY_FLD] = decCommitQuantity;
						drowTransaction[MST_TransactionHistoryTable.BINID_FLD] = intProBinID;
						decOHQuantity = GetQuantityFromCache(dtbBinCacheData, intProBinID, intComponentID, 3, out decCommitQuantity);
						drowTransaction[MST_TransactionHistoryTable.BINOHQUANTITY_FLD] = decOHQuantity;
						drowTransaction[MST_TransactionHistoryTable.BINCOMMITQUANTITY_FLD] = decCommitQuantity;
						drowTransaction[MST_TransactionHistoryTable.STOCKUMID_FLD] = drowBom[ITM_ProductTable.STOCKUMID_FLD];
						dtbTransaction.Rows.Add(drowTransaction);
						#endregion
					}
				}
			}

			#endregion

			#region Update Received Quantity for Delivery Schedule
			sbDeliveryScheduleIDs.Append("0");
			DataSet dstSchedule = dsSchedule.List(sbDeliveryScheduleIDs.ToString());
			foreach (DataRow drowData in dstSchedule.Tables[0].Rows)
			{
				string strFilter = PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD + "="
					+ drowData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD];
				decimal decReceiveQuantity = Convert.ToDecimal(dtbNewDetail.Compute("SUM(" + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ")", strFilter));
				decimal decCurrentReceived = 0;
				try
				{
					decCurrentReceived = Convert.ToDecimal(drowData[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD]);
				}
				catch{}
				drowData[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD] = decCurrentReceived + decReceiveQuantity;
			}
			dsSchedule.UpdateDataSet(dstSchedule);
			#endregion

			#region Update Total Delivery of PO detail
			DataSet dstPODetail = dsPODetail.List(strPODetailIDs);
			foreach (DataRow drowData in dstPODetail.Tables[0].Rows)
			{
				string strFilter = PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "="
					+ drowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
				decimal decReceiveQuantity = Convert.ToDecimal(dtbNewDetail.Compute("SUM(" + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ")", strFilter));
				decimal decCurrentReceived = 0;
				try
				{
					decCurrentReceived = Convert.ToDecimal(drowData[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD]);
				}
				catch{}
				drowData[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD] = decCurrentReceived + decReceiveQuantity;
			}
			dsPODetail.UpdateDataSetForReceipt(dstPODetail);
			#endregion

			#region Close PO

			// check if total delivery of PO Line >= to order quantity
			// then update auto close of PO Line
			foreach (DataRow drowData in pdstDetailData.Tables[0].Rows)
			{
				// ignore deleted row
				if (drowData.RowState == DataRowState.Deleted)
					continue;
				// now get PO detail id of current row
				int intPODetailID = int.Parse(drowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
				// get PODetail object
				PO_PurchaseOrderDetailVO voPODetail = new PO_PurchaseOrderDetailVO();
				voPODetail = (PO_PurchaseOrderDetailVO)dsPODetail.GetObjectVO(intPODetailID);

				PO_PurchaseOrderMasterDS objPO_PurchaseOrderMasterDS = new PO_PurchaseOrderMasterDS();
				// total delivery of po line
				decimal decTotalDelivery = GetTotalDelivery(voPODetail.PurchaseOrderDetailID);
				if (voPODetail.OrderQuantity <= decTotalDelivery)
				{
					// close po line first
					objPO_PurchaseOrderMasterDS.ClosePurchaseOrderLine(voPODetail.PurchaseOrderDetailID);
					//close the Purchase Order
					objPO_PurchaseOrderMasterDS.ClosePurchaseOrder(voPODetail.PurchaseOrderMasterID);
				}
			}

			#endregion

			#region save transaction history

			DataSet dstData = new DataSet();
			dstData.Tables.Add(dtbTransaction);
			dsTransaction.UpdateDataSet(dstData);

			#endregion

			return voMasterReceipt.PurchaseOrderReceiptID;
		}
		/// <summary>
		/// Get quantity from cache table
		/// </summary>
		/// <param name="pdtbCacheData">Cache Table</param>
		/// <param name="pintID">Cache ID</param>
		/// <param name="pintProductID"></param>
		/// <param name="pintType">1: Master Location | 2: Location | 3: Bin</param>
		/// <param name="odecCommitQuantity">Commit Quantity</param>
		/// <returns>Onhand Quantity</returns>
		private decimal GetQuantityFromCache(DataTable pdtbCacheData, int pintID, int pintProductID, int pintType, out decimal odecCommitQuantity)
		{
			odecCommitQuantity = 0;
			try
			{
				string strFilter = ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID;
				switch(pintType)
				{
					case 1: // master location
						strFilter += " AND " + IV_MasLocCacheTable.MASTERLOCATIONID_FLD + "=" + pintID;
						break;
					case 2: // location
						strFilter += " AND " + IV_LocationCacheTable.LOCATIONID_FLD + "=" + pintID;
						break;
					default: // bin
						strFilter += " AND " + IV_BinCacheTable.BINID_FLD + "=" + pintID;
						break;
				}
				odecCommitQuantity = Convert.ToDecimal(pdtbCacheData.Select(strFilter)[0][IV_BinCacheTable.COMMITQUANTITY_FLD]);
				return Convert.ToDecimal(pdtbCacheData.Select(strFilter)[0][IV_BinCacheTable.OHQUANTITY_FLD]);
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				return 0;
			}
		}
		public void UpdateMasterReceipt(object pobjMasterObject, DataSet pdstDetailData, int pintCCNID)
		{
			// update master receipt first
			PO_PurchaseOrderReceiptMasterDS dsMasterReceipt = new PO_PurchaseOrderReceiptMasterDS();
			dsMasterReceipt.Update(pobjMasterObject);
			UpdateDataSet(pdstDetailData);
			// update inventory
			UpdateInventory(pobjMasterObject, pdstDetailData, pintCCNID);
		}

		public object GetReceiptMasterVO(int pintID)
		{
			//			try
			//			{
			PO_PurchaseOrderReceiptMasterDS dsReceiptMaster = new PO_PurchaseOrderReceiptMasterDS();
			return dsReceiptMaster.GetObjectVO(pintID);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public MST_PartyVO GetCustomerInfo(int pintPartyID)
		{
			//			try
			//			{
			MST_PartyDS dsParty = new MST_PartyDS();
			return (MST_PartyVO)dsParty.GetObjectVO(pintPartyID);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public MST_PartyLocationVO GetPartyLocation(int pintPartyID, string pstrCode)
		{
			//			try
			//			{
			MST_PartyLocationDS dsPartyLocation = new MST_PartyLocationDS();
			return (MST_PartyLocationVO)dsPartyLocation.GetObjectVO(pintPartyID, pstrCode);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
	
		public MST_PartyLocationVO GetPartyLocation(int pintPartyLocationID)
		{
			//			try
			//			{
			MST_PartyLocationDS dsPartyLocation = new MST_PartyLocationDS();
			return (MST_PartyLocationVO)dsPartyLocation.GetObjectVO(pintPartyLocationID);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
	
		public DataSet ListReceiptDetailByReceiptMaster(int pintReceiptMasterID)
		{
			//			try
			//			{
			PO_PurchaseOrderReceiptDetailDS dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			MST_LocationDS dsLocation = new MST_LocationDS();
			MST_BINDS dsBin = new MST_BINDS();
			DataSet dstData = dsReceiptDetail.List(pintReceiptMasterID);
			// now we need to fill Bin, Location and Lot if any
			foreach (DataRow drowData in dstData.Tables[0].Rows)
			{
				int intBinID = 0;
				int intLocationID = 0;
				try
				{
					intBinID = int.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.BINID_FLD].ToString().Trim());
				}
				catch{}
				try
				{
					intLocationID = int.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.LOCATIONID_FLD].ToString().Trim());
				}
				catch{}
				MST_LocationVO voLocation = new MST_LocationVO();
				MST_BINVO voBin = new MST_BINVO();
				// if product have location then update data
				if (intLocationID > 0)
				{
					// get Location object
					voLocation = (MST_LocationVO)dsLocation.GetObjectVO(intLocationID);
					// update data row
					drowData[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = voLocation.Code;
					drowData[MST_LocationTable.LOCATIONID_FLD] = voLocation.LocationID;
				}
				// if product have bin then update data
				if (intBinID > 0)
				{
					// get bin object
					voBin = (MST_BINVO)dsBin.GetObjectVO(intBinID);
					// update data row
					drowData[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = voBin.Code;
					drowData[MST_BINTable.BINID_FLD] = voBin.BinID;
				}
			}
			return dstData;
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public DataSet ListByPOID(int pintPOMasterID)
		{
			//			try
			//			{
			PO_PurchaseOrderReceiptDetailDS dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			DataSet dstData = dsReceiptDetail.ListByPOID(pintPOMasterID);
			// fill default Location, Bin, Lot if any
			dstData = AssignDefaultInfo(dstData, false);
			return dstData;
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public DataSet ListByItem(int pintProductID)
		{
			//			try
			//			{
			PO_PurchaseOrderReceiptDetailDS dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			return dsReceiptDetail.ListByItem(pintProductID);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public object GetPOMasterVO(int pintPOMasterID)
		{
			PO_PurchaseOrderMasterDS dsPOMaster = new PO_PurchaseOrderMasterDS();
			return dsPOMaster.GetObjectVO(pintPOMasterID);
		}
		/// <summary>
		/// This method uses to get PO Master VO object from PO Code
		/// </summary>
		/// <param name="pstrPOCode">PO Number</param>
		/// <returns>PO_PurchaseOrderMasterVO</returns>
	
		public object GetPOMasterVO(string pstrPOCode)
		{
			PO_PurchaseOrderMasterDS dsPOMaster = new PO_PurchaseOrderMasterDS();
			return dsPOMaster.GetObjectVO(pstrPOCode);
		}
		public object GetPODetailVO(int pintPODetailID)
		{
			//			try
			//			{
			PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
			return dsPODetail.GetObjectVO(pintPODetailID);
			//			}
			//			catch (PCSException ex)
			//			{
			//				throw ex;
			//			}
			//			catch (Exception ex)
			//			{
			//				throw ex;
			//			}
		}
		public object GetProductInfo(int pintProductID)
		{
			try
			{
				ITM_ProductDS dsProduct = new ITM_ProductDS();
				return dsProduct.GetProductInfo(pintProductID);
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
		public object GetUnitOfMeasureInfo(int pintID)
		{
			try
			{
				MST_UnitOfMeasureDS dsUM = new MST_UnitOfMeasureDS();
				return dsUM.GetObjectVO(pintID);
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
		public object GetMasterLocationInfo(int pintMasterLocationID)
		{
			try
			{
				MST_MasterLocationDS dsMasterLocation = new MST_MasterLocationDS();
				return dsMasterLocation.GetObjectVO(pintMasterLocationID);
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
		public object GetLocationInfo(int pintLocationID)
		{
			try
			{
				MST_LocationDS dsLocation = new MST_LocationDS();
				return dsLocation.GetObjectVO(pintLocationID);
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
		public object GetBinInfo(int pintBinID)
		{
			try
			{
				MST_BINDS dsBin = new MST_BINDS();
				return dsBin.GetObjectVO(pintBinID);
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
		public void UpdateInventory(object pobjMasterReceipt, DataSet pdstDetailData, int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".UpdateInventory()";
			InventoryUtilsBO boIVUtils = new InventoryUtilsBO();
			if ((pdstDetailData != null) && (pdstDetailData.Tables.Count > 0))
			{
				PO_PurchaseOrderReceiptMasterVO voMasterReceipt = (PO_PurchaseOrderReceiptMasterVO)pobjMasterReceipt;
				PO_PurchaseOrderDetailVO voPODetail = new PO_PurchaseOrderDetailVO();
				PO_DeliveryScheduleVO voSchedule = new PO_DeliveryScheduleVO();
				PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
				PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
				
				foreach (DataRow drowData in pdstDetailData.Tables[0].Rows)
				{
					int intProductID = int.Parse(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					int intDeliveryScheduleID = int.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD].ToString().Trim());
					string strLot = drowData[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].ToString().Trim();
					string strSerial = drowData[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].ToString().Trim();
					// PurchaseOrderDetail
					voPODetail = (PO_PurchaseOrderDetailVO)dsPODetail.GetObjectVO(int.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD].ToString().Trim()));

					decimal decReceiveQuantity = 0;
					decimal decReceiveQuantityUMRate = 0;
					decimal decUMRate = 0;
					int intBinID = 0;
					int intLocationID = 0;
					// get ReceiveQuantity from PO Receipt
					try
					{
						decReceiveQuantity = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());
						decUMRate = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString().Trim());
						decReceiveQuantityUMRate = decReceiveQuantity * decUMRate;
					}
					catch{}
					
					#region Inventory
					try
					{
						intBinID = int.Parse(drowData[MST_BINTable.BINID_FLD].ToString());
					}
					catch{}
					try
					{
						intLocationID = int.Parse(drowData[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
					}
					catch
					{
						throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, METHOD_NAME, new Exception());
					}
					boIVUtils.UpdateAddOHQuantity(pintCCNID, voMasterReceipt.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantityUMRate, strLot, strSerial);

					#endregion

					// update the delivery schedule
					voSchedule = (PO_DeliveryScheduleVO)dsSchedule.GetObjectVO(intDeliveryScheduleID);
					// update received quantity without UM rate
					voSchedule.ReceivedQuantity += decReceiveQuantity;
					// update schedule
					dsSchedule.Update(voSchedule);
					// get current total delivery of PO Detail
					decimal decTotalDelivery = dsPODetail.GetTotalDelivery(voPODetail.PurchaseOrderDetailID);
					// update total delivery of PO Detail
					dsPODetail.UpdateTotalDelivery(decReceiveQuantity + decTotalDelivery, voPODetail.PurchaseOrderDetailID);
				}
			}
		}
		public decimal GetTotalDelivery(int pintPODetailID)
		{
			try
			{
				PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
				return dsDetail.GetTotalDelivery(pintPODetailID);
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
		/// <summary>
		/// GetTotalReceiptQuantity
		/// </summary>
		/// <param name="pintDeliveryScheduleID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Saturday, June 10 2006</date>
	
		public decimal GetTotalReceiptQuantity(int pintDeliveryScheduleID)
		{
			PO_PurchaseOrderReceiptDetailDS dsDetail = new PO_PurchaseOrderReceiptDetailDS();
			return dsDetail.GetTotalReceiptQuantityByDeliveryScheduleID(pintDeliveryScheduleID);
		}
		public decimal GetTotalReceiveQuantity(int pintPODetailID)
		{
			try
			{
				PO_PurchaseOrderReceiptDetailDS dsDetail = new PO_PurchaseOrderReceiptDetailDS();
				return dsDetail.GetTotalReceiveQuantity(pintPODetailID);
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
		public bool IsReceived(int pintPODetailID)
		{
			try
			{
				PO_PurchaseOrderReceiptDetailDS dsDetail = new PO_PurchaseOrderReceiptDetailDS();
				return dsDetail.IsReceived(pintPODetailID);
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
		public decimal GetUMRate(int pintInUMID, int pintOutUMID)
		{
			try
			{
				MST_UMRateDS dsUMRate = new MST_UMRateDS();
				return dsUMRate.GetUMRate(pintInUMID, pintOutUMID);
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

	    /// <summary>
		/// List receipt detail by PO code
		/// </summary>
		/// <param name="pstrPOCode">PO Code</param>
		/// <param name="pdtmSlipDate">The schedule date</param>
		/// <returns>Receipt Details</returns>
	
		public DataSet ListByPOCode(string pstrPOCode, DateTime pdtmSlipDate)
		{
			PO_PurchaseOrderReceiptDetailDS dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			DataSet dstData = dsReceiptDetail.ListByPOCode(pstrPOCode, pdtmSlipDate);
			/// assign default value of Location, Bin and Lot if any
			dstData = AssignDefaultInfo(dstData, true);
			return dstData;
		}
		/// <summary>
		/// List receipt detail by Invoice
		/// </summary>
		/// <param name="pInvoiceMasterID">Invoice Master ID</param>
		/// <returns>Receipt Detail</returns>
	
		public DataSet ListByInvoice(int pInvoiceMasterID)
		{
			PO_PurchaseOrderReceiptDetailDS dsReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			DataSet dstData = dsReceiptDetail.ListByInvoice(pInvoiceMasterID);
			/// assign default value of Location, Bin and Lot if any
			dstData = AssignDefaultInfo(dstData, true);
			return dstData;
		}
		/// <summary>
		/// Assign default Location, Bin and Lot if any
		/// </summary>
		/// <param name="pdstData">Data to assign</param>
		/// <param name="pblnByInvoice">Receipt by invoice or not</param>
		/// <returns>Assigned Data</returns>
		private DataSet AssignDefaultInfo(DataSet pdstData, bool pblnByInvoice)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			MST_LocationDS dsLocation = new MST_LocationDS();
			MST_BINDS dsBin = new MST_BINDS();
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			// now we need to fill Bin, Location and Lot if any
			foreach (DataRow drowData in pdstData.Tables[0].Rows)
			{
				// get product information
				ITM_ProductVO voProduct = new ITM_ProductVO();
				MST_LocationVO voLocation = new MST_LocationVO();
				MST_BINVO voBin = new MST_BINVO();
				PO_DeliveryScheduleVO voSchedule = new PO_DeliveryScheduleVO();
				int intDeliveryScheduleID = 0;
				try
				{
					intDeliveryScheduleID = int.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD].ToString().Trim());
				}
				catch{}
				voProduct = (ITM_ProductVO)dsProduct.GetObjectVO(int.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].ToString().Trim()));
				// if product have location then update data
				if (voProduct.LocationID > 0)
				{
					// get Location object
					voLocation = (MST_LocationVO)dsLocation.GetObjectVO(voProduct.LocationID);
					// update data row
					drowData[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = voLocation.Code;
					drowData[MST_LocationTable.LOCATIONID_FLD] = voLocation.LocationID;
				}
				// if product have bin then update data
				if (voProduct.BinID > 0)
				{
					// get bin object
					voBin = (MST_BINVO)dsBin.GetObjectVO(voProduct.BinID);
					// update data row
					drowData[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = voBin.Code;
					drowData[MST_BINTable.BINID_FLD] = voBin.BinID;
				}
				// we need to get remain quantity from delivery schedule
				voSchedule = (PO_DeliveryScheduleVO)dsSchedule.GetObjectVO(intDeliveryScheduleID);
				if (!pblnByInvoice)
					drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] = voSchedule.DeliveryQuantity - voSchedule.ReceivedQuantity;

				#region // HACK: DEL dungla 10-28-2005

				//				//get the total receive quantity
				//				decimal dcmToTalReceiveQty = GetTotalReceiveQuantity(int.Parse(drowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString()));
				//				//get the total delivery schedule
				//				decimal decTotalOrderQty = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString()) ;// GetTotalDelivery(int.Parse(drowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString()));
				//
				//				//get the remain quantity
				//				if (decTotalOrderQty - dcmToTalReceiveQty > 0) 
				//				{
				//					drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] = decTotalOrderQty - dcmToTalReceiveQty;
				//				}
				//				else
				//				{
				//					drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] = 0;
				//				}

				#endregion // END: DEL dungla 10-28-2005
			}
			return pdstData;
		}

		/// <summary>
		/// Save transaction history
		/// </summary>
	
		public void SaveTransaction(object pobjReceiptMaster)
		{
			MST_TransactionHistoryDS dsTransactionHistory = new MST_TransactionHistoryDS();
			dsTransactionHistory.Add(pobjReceiptMaster);
		}

		/// <summary>
		/// Get onhand quantity from master location
		/// </summary>
		/// <param name="pintProductID">Product</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master Location</param>
		/// <returns>OnHand quantity</returns>
	
		public decimal GetOnHandQty(int pintProductID, int pintCCNID, int pintMasterLocationID)
		{
			IV_MasLocCacheDS dsMasLoc = new IV_MasLocCacheDS();
			return dsMasLoc.GetOnHanQty(pintProductID, pintCCNID, pintMasterLocationID);
		}

		/// <summary>
		/// Get Avegrate cost of product in master location
		/// </summary>
		/// <param name="pintProductID">Product</param>
		/// <param name="pintCCNID">CCN</param>
		/// <param name="pintMasterLocationID">Master LocationID</param>
		/// <returns>Avg Cost</returns>
	
		public decimal GetAvgCost(int pintProductID, int pintCCNID, int pintMasterLocationID)
		{
			IV_MasLocCacheDS dsMasLoc = new IV_MasLocCacheDS();
			return dsMasLoc.GetAvgCost(pintProductID, pintCCNID, pintMasterLocationID);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns>Avg Cost</returns>
	
		public DataTable GetLocationBin(int pintProductionLineID)
		{
			PO_PurchaseOrderReceiptDetailDS dsPO = new PO_PurchaseOrderReceiptDetailDS();
			return dsPO.GetLocationBin(pintProductionLineID);
		}
		/// <summary>
		/// Assign common value for transaction history object
		/// </summary>
		/// <param name="pobjTransactionHistory">Transaction history object</param>
		/// <returns>New TransactionHistory object</returns>
	
		public object AssignCommonValue(object pobjTransactionHistory)
		{
			MST_TransactionHistoryVO voTransHis = (MST_TransactionHistoryVO)pobjTransactionHistory;
			// assign username
			voTransHis.Username = SystemProperty.UserName;
			//1. MasLocOHQuantity
			if(voTransHis.Lot != null)
			{
				voTransHis.MasLocOHQuantity = (new IV_MasLocCacheDS()).GetQuantityOnHandByLot(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID, voTransHis.Lot);
				voTransHis.MasLocCommitQuantity = (new IV_MasLocCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.Lot, voTransHis.ProductID);
			}
			else
			{
				voTransHis.MasLocOHQuantity = (new IV_MasLocCacheDS()).GetQuantityOnHand(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID);
				voTransHis.MasLocCommitQuantity = (new IV_MasLocCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.ProductID);
			}
				
			// 2. LocationOHQuantity
			// If MST_TransactionHistoryVO.LocationID > 0
			// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.LocationOHQuantity = IV_LocationCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
			// - Else MST_TransactionHistoryVO.LocationOHQuantity = IV_LocationCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID)
			if(voTransHis.LocationID > 0 )
			{
				if(voTransHis.Lot != null)
				{
					voTransHis.LocationOHQuantity = (new IV_LocationCacheDS()).GetQuantityOnHandByLot(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.ProductID, voTransHis.Lot);
					voTransHis.LocationCommitQuantity = (new IV_LocationCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.Lot, voTransHis.ProductID);
				}
				else
				{
					voTransHis.LocationOHQuantity = (new IV_LocationCacheDS()).GetQuantityOnHand(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.ProductID);
					voTransHis.LocationCommitQuantity = (new IV_LocationCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.ProductID);
				}
			}
				
			// 3. BinOHQuantity
			// If MST_TransactionHistoryVO.BinID > 0
			// - If MST_TransactionHistoryVO.Lot != null: MST_TransactionHistoryVO.BinOHQuantity = IV_BinCacheDS.GetQuantityOnHandByLot(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.BinID, MST_TransactionHistoryVO.ProductID, MST_TransactionHistoryVO.Lot)
			// - Else MST_TransactionHistoryVO.BinOHQuantity = IV_BinCacheDS.GetQuantityOnHand(MST_TransactionHistoryVO.CCNID, MST_TransactionHistoryVO.MasterLocationID, MST_TransactionHistoryVO.BinID, MST_TransactionHistoryVO.LocationID, MST_TransactionHistoryVO.ProductID)
				
			if(voTransHis.BinID > 0 )
			{
				if(voTransHis.Lot != null)
				{
					voTransHis.BinOHQuantity = (new IV_BinCacheDS()).GetQuantityOnHandByLot(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.ProductID, voTransHis.Lot);
					voTransHis.BinCommitQuantity = (new IV_BinCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.Lot, voTransHis.ProductID);
				}
				else
				{
					voTransHis.BinOHQuantity = (new IV_BinCacheDS()).GetQuantityOnHand(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.ProductID);
					voTransHis.BinCommitQuantity = (new IV_BinCacheDS()).GetCommitQuantity(voTransHis.CCNID, voTransHis.MasterLocationID, voTransHis.LocationID, voTransHis.BinID, voTransHis.ProductID);
				}
			}

			MST_TranTypeDS dsTransType = new MST_TranTypeDS();
			voTransHis.TranTypeID = dsTransType.GetIDFromCode(TransactionTypeEnum.POPurchaseOrderReceipts.ToString());

			//Reassign TransactionHistory
			return voTransHis;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintPOReceiptMasterID"></param>
		/// <returns></returns>
	
		public DataTable GetPOReceiptMaster(int pintPOReceiptMasterID)
		{
			PO_PurchaseOrderReceiptMasterDS dsPOMaster = new PO_PurchaseOrderReceiptMasterDS();
			return dsPOMaster.GetPOReceiptMaster(pintPOReceiptMasterID);
		}

		/// <summary>
		/// Gets invoice master infor
		/// </summary>
		/// <param name="pintInvoiceMasterID"></param>
		/// <returns></returns>
	
		public object GetInvoiceInfo(int pintInvoiceMasterID)
		{
			PO_InvoiceMasterDS dsInvoice = new PO_InvoiceMasterDS();
			return dsInvoice.GetObjectVO(pintInvoiceMasterID);
		}
		/// <summary>
		/// Delete Purchase Order Receipt
		/// </summary>
		/// <param name="printPurchaseOrderReceiptId"></param>
		/// <returns></returns>
	
		public void DeletePOReceipt(int printPurchaseOrderReceiptId)
		{
			//0. Variables
			int constInspStatus = 11;
			MST_TranTypeDS dsTranType = new MST_TranTypeDS();
			int intOldTranTypeID = dsTranType.GetTranTypeID(TransactionTypeEnum.POPurchaseOrderReceipts.ToString());
			int intNewTranTypeID = dsTranType.GetTranTypeID(TransactionTypeEnum.DeleteTransaction.ToString());

			//1. Get info Purchase Order Receipt
			PO_PurchaseOrderReceiptMasterDS objPOReceiptMasterDS = new PO_PurchaseOrderReceiptMasterDS();
            PO_PurchaseOrderReceiptMasterVO voPurchaseOrderMaster = new PO_PurchaseOrderReceiptMasterVO();
			voPurchaseOrderMaster = (PO_PurchaseOrderReceiptMasterVO) objPOReceiptMasterDS.GetObjectVO(printPurchaseOrderReceiptId);

			//2. Get info detail to Dataset
			PO_PurchaseOrderReceiptDetailDS objPOReceiptDetailDS = new PO_PurchaseOrderReceiptDetailDS();
			DataSet dstData = objPOReceiptDetailDS.List(printPurchaseOrderReceiptId);

            InventoryUtilsBO objInventoryBO = new InventoryUtilsBO();
			MST_TransactionHistoryDS objTransactionHistoryDS = new MST_TransactionHistoryDS();
			
			//3. Subtract and Update Inventory and TransactionHistory
			switch(voPurchaseOrderMaster.ReceiptType)
			{
				case (int)POReceiptTypeEnum.ByDeliverySlip:
					#region 3.1 Update TransactionHistory Inventory ByDeliverySlip
					foreach (DataRow drowData in dstData.Tables[0].Rows)
					{
						//3.1.1 subtract Inventory
						int intProductID = Convert.ToInt32(drowData[ITM_ProductTable.PRODUCTID_FLD]);
						
						decimal decReceiveQuantity = 0, decReceiveQuantityUMRate = 0, decUMRate = 0;
						int intBinID = 0, intLocationID = 0;
						// get ReceiveQuantity from PO Receipt
						try
						{
							decReceiveQuantity = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());
							decUMRate = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString().Trim());
							decReceiveQuantityUMRate = decReceiveQuantity * decUMRate;
						}
						catch{}
					
						try
						{
							intBinID = int.Parse(drowData[MST_BINTable.BINID_FLD].ToString());
						}
						catch{}
						try
						{
							intLocationID = int.Parse(drowData[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
						}
						catch
						{
							throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
						}
						objInventoryBO.UpdateSubtractOHQuantity(voPurchaseOrderMaster.CCNID, voPurchaseOrderMaster.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantityUMRate, string.Empty, string.Empty);
						
						//3.1.2 Update TransactionHistory
//						int RefPurchaseMaster = (int)drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD];
//						int RefPurchaseDetail = (int)drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];
//						objTransactionHistoryDS.UpdateTranType(RefPurchaseMaster,RefPurchaseDetail,constTranTypeID,constInspStatus);
					}
					break;
					#endregion
				case (int)POReceiptTypeEnum.ByInvoice:
					#region 3.2 Update TransactionHistory Inventory ByInvoice
					foreach (DataRow drowData in dstData.Tables[0].Rows)
					{
						//3.1.1 subtract Inventory
						int intProductID = Convert.ToInt32(drowData[ITM_ProductTable.PRODUCTID_FLD]);
						
						decimal decReceiveQuantity = 0, decReceiveQuantityUMRate = 0, decUMRate = 0;
						int intBinID = 0, intLocationID = 0;
						// get ReceiveQuantity from PO Receipt
						try
						{
							decReceiveQuantity = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());
							decUMRate = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString().Trim());
							decReceiveQuantityUMRate = decReceiveQuantity * decUMRate;
						}
						catch{}
					
						try
						{
							intBinID = int.Parse(drowData[MST_BINTable.BINID_FLD].ToString());
						}
						catch{}
						try
						{
							intLocationID = int.Parse(drowData[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
						}
						catch
						{
							throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
						}
						objInventoryBO.UpdateSubtractOHQuantity(voPurchaseOrderMaster.CCNID, voPurchaseOrderMaster.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantityUMRate, string.Empty, string.Empty);
						
						//3.1.2 Update TransactionHistory
//						int RefPurchaseMaster = (int)drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD];
//						int RefPurchaseDetail = (int)drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];
//						objTransactionHistoryDS.UpdateTranType(RefPurchaseMaster,RefPurchaseDetail,constTranTypeID,constInspStatus);
					}
					break;
					#endregion
				case (int)POReceiptTypeEnum.ByOutside:
					#region 3.3 Update TransactionHistory Inventory ByOutside
					DataTable dtLocbin = objPOReceiptDetailDS.GetLocationBin(voPurchaseOrderMaster.ProductionLineID);
					if(dtLocbin.Rows.Count == 0)
						throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
					int intProLocationID = Convert.ToInt32(dtLocbin.Rows[0][MST_BINTable.LOCATIONID_FLD]);
					int intProBinID = Convert.ToInt32(dtLocbin.Rows[0][MST_BINTable.BINID_FLD]);

					foreach (DataRow drowData in dstData.Tables[0].Rows)
					{
						#region 3.3.1 get info from PO Receipt details
						int intProductID = Convert.ToInt32(drowData[ITM_ProductTable.PRODUCTID_FLD]);
						decimal decReceiveQuantity = 0, decReceiveQuantityUMRate = 0, decUMRate = 0;
						int intBinID = 0, intLocationID = 0;
						try
						{
							decReceiveQuantity = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());
							decUMRate = decimal.Parse(drowData[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString().Trim());
							decReceiveQuantityUMRate = decReceiveQuantity * decUMRate;
						}
						catch{}
						try
						{
							intBinID = int.Parse(drowData[MST_BINTable.BINID_FLD].ToString());
						}
						catch{}
						try
						{
							intLocationID = int.Parse(drowData[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
						}
						catch
						{
							throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
						}
						#endregion

						#region 3.3.2 Add Inventory by Bom
						BomBO boBom = new BomBO();
						DataTable dtbBom = boBom.ListBOMDetailsOfProduct(intProductID);
						
						foreach(DataRow drowBom in dtbBom.Rows)
						{
							#region subtract cache
							// Get available quantity by Postdate
							int intComponentID = Convert.ToInt32(drowBom[ITM_BOMTable.COMPONENTID_FLD]);
							decimal decBOMQty = Convert.ToDecimal(drowBom[ITM_BOMTable.QUANTITY_FLD]);
							objInventoryBO.UpdateAddOHQuantity(voPurchaseOrderMaster.CCNID,voPurchaseOrderMaster.MasterLocationID,
									intProLocationID,intProBinID,intComponentID,decBOMQty*decReceiveQuantity,string.Empty,string.Empty);
							#endregion
						}
						#endregion
						
						//3.3.3 subtract Inventory by Purchase Order Receipt
						objInventoryBO.UpdateSubtractOHQuantity(voPurchaseOrderMaster.CCNID, voPurchaseOrderMaster.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantityUMRate, string.Empty, string.Empty);
						
						//3.3.4 Update TransactionHistory
//						int RefPurchaseMasterBom = (int)drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTID_FLD];
//						int RefPurchaseDetailBom = (int)drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];
//						objTransactionHistoryDS.UpdateTranType(RefPurchaseMasterBom,RefPurchaseDetailBom,constTranTypeID,constInspStatus);
					}
					break;
					#endregion
			}
			//4. Delete Rows in PO_PurchaseOrderReceiptDetail
            objPOReceiptDetailDS.Delete(voPurchaseOrderMaster.PurchaseOrderReceiptID);		
			//5. Delete Row in PO_PurchaseOrderReceiptMaster
			objPOReceiptMasterDS.Delete(voPurchaseOrderMaster.PurchaseOrderReceiptID);

			//6. UnClose Purchase Order details
			string strPODetailIDs = "(";
            PO_PurchaseOrderMasterDS objPurchaseOrderMasterDS = new PO_PurchaseOrderMasterDS();
			foreach (DataRow drowData in dstData.Tables[0].Rows)
			{
				// close po line first
				objPurchaseOrderMasterDS.OpenPurchaseOrderLine((int) drowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD]);				
				strPODetailIDs += drowData[PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD] + ",";
			}
			if(strPODetailIDs.Length > 1)
				strPODetailIDs = strPODetailIDs.Substring(0,strPODetailIDs.Length-1) + ")";
			else
				strPODetailIDs = string.Empty;

			//7. Update PO_DeliverySchedule 
            #region Update Total Delivery of PO detail
			PO_PurchaseOrderDetailDS objPODetailDS = new PO_PurchaseOrderDetailDS();
			DataSet dstPODetail = objPODetailDS.List(strPODetailIDs);
			//DataTable dtbNewDetail = objPOReceiptDetailDS.ListByMaster(voPurchaseOrderMaster.PurchaseOrderReceiptID);

			foreach (DataRow drowData in dstPODetail.Tables[0].Rows)
			{
				string strFilter = PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERDETAILID_FLD + "="
					+ drowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
				decimal decReceiveQuantity = Convert.ToDecimal(dstData.Tables[0].Compute("SUM(" + PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD + ")", strFilter));
				decimal decCurrentReceived = 0;
				try
				{
					decCurrentReceived = Convert.ToDecimal(drowData[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD]);
				}
				catch{}
				drowData[PO_PurchaseOrderDetailTable.TOTALDELIVERY_FLD] = decCurrentReceived - decReceiveQuantity;				
			}
			objPODetailDS.UpdateDataSetForReceipt(dstPODetail);
			
			// Update DeliverySchedule
			PO_DeliveryScheduleDS objPODeliveryScheduleDS = new PO_DeliveryScheduleDS();
			foreach (DataRow drow in dstData.Tables[0].Rows)
			{
				decimal decQuantityReceipt = (decimal) drow[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD];
				int printDeliveryScheduleID = (int) drow[PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD];

				objPODeliveryScheduleDS.UpdateSubstractQuantityReceipt(printDeliveryScheduleID,decQuantityReceipt);
			}

			#endregion

			// Update TransactionHistory
			objTransactionHistoryDS.UpdateTranType(voPurchaseOrderMaster.PurchaseOrderReceiptID,
				intOldTranTypeID, intNewTranTypeID, constInspStatus);
		}


		/// <summary>
		/// DeleteRowPOReceipt
		/// </summary>
		/// <param name="printPOReceiptMasterID","printPOReceiptDetailID"></param>
		/// <returns></returns>
	
		public void DeleteRowPOReceipt(int printPOReceiptMasterID, int printPOReceiptDetailID)
		{
			//1. Variable
			PO_PurchaseOrderReceiptDetailDS objPORDetailDS = new PO_PurchaseOrderReceiptDetailDS();
			int constInspStatus = 11;
			MST_TranTypeDS dsTranType = new MST_TranTypeDS();
			int intOldTranTypeID = dsTranType.GetTranTypeID(TransactionTypeEnum.POPurchaseOrderReceipts.ToString());
			int intNewTranTypeID = dsTranType.GetTranTypeID(TransactionTypeEnum.DeleteTransaction.ToString());
			PO_PurchaseOrderReceiptMasterDS objPOReceiptMasterDS = new PO_PurchaseOrderReceiptMasterDS();
			PO_PurchaseOrderReceiptMasterVO voPurchaseOrderMaster = new PO_PurchaseOrderReceiptMasterVO();
			InventoryUtilsBO objInventoryBO = new InventoryUtilsBO();
			
			//2. Get Master Infomation
			voPurchaseOrderMaster = (PO_PurchaseOrderReceiptMasterVO) objPOReceiptMasterDS.GetObjectVO(printPOReceiptMasterID);

			//3. Get info row detail to DataRow
			PO_PurchaseOrderReceiptDetailDS objPOReceiptDetailDS = new PO_PurchaseOrderReceiptDetailDS();
			DataRow drDataRow = objPOReceiptDetailDS.GetRow(printPOReceiptMasterID,printPOReceiptDetailID);
			
			int intProductID = Convert.ToInt32(drDataRow[ITM_ProductTable.PRODUCTID_FLD]);
			decimal decReceiveQuantity = 0;
			int intBinID = 0, intLocationID = 0;

			//4. Update Inventory
			switch(voPurchaseOrderMaster.ReceiptType)
			{
				case (int)POReceiptTypeEnum.ByDeliverySlip:
					#region 3.1 Update TransactionHistory Inventory ByDeliverySlip
					// get ReceiveQuantity from PO Receipt
					try
					{
						decReceiveQuantity = decimal.Parse(drDataRow[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());					
					}
					catch{}
					try
					{
						intBinID = int.Parse(drDataRow[MST_BINTable.BINID_FLD].ToString());
					}
					catch{}
					try
					{
						intLocationID = int.Parse(drDataRow[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
					}
					catch
					{
						throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
					}
					objInventoryBO.UpdateSubtractOHQuantity(voPurchaseOrderMaster.CCNID, voPurchaseOrderMaster.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantity, string.Empty, string.Empty);					
					break;
					#endregion
				case (int)POReceiptTypeEnum.ByInvoice:
					#region 3.2 Update TransactionHistory Inventory ByInvoice
					// get ReceiveQuantity from PO Receipt
						try
						{
							decReceiveQuantity = decimal.Parse(drDataRow[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());							
						}
						catch{}
					
						try
						{
							intBinID = int.Parse(drDataRow[MST_BINTable.BINID_FLD].ToString());
						}
						catch{}
						try
						{
							intLocationID = int.Parse(drDataRow[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
						}
						catch
						{
							throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
						}
						objInventoryBO.UpdateSubtractOHQuantity(voPurchaseOrderMaster.CCNID, voPurchaseOrderMaster.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantity, string.Empty, string.Empty);
																
					break;
					#endregion
				case (int)POReceiptTypeEnum.ByOutside:
					#region 3.3 Update TransactionHistory Inventory ByOutside
					DataTable dtLocbin = objPOReceiptDetailDS.GetLocationBin(voPurchaseOrderMaster.ProductionLineID);
					if(dtLocbin.Rows.Count == 0)
						throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
					int intProLocationID = Convert.ToInt32(dtLocbin.Rows[0][MST_BINTable.LOCATIONID_FLD]);
					int intProBinID = Convert.ToInt32(dtLocbin.Rows[0][MST_BINTable.BINID_FLD]);

					#region 3.3.1 get info from PO Receipt details
					try
					{
						decReceiveQuantity = decimal.Parse(drDataRow[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString().Trim());						
					}
					catch{}
					try
					{
						intBinID = int.Parse(drDataRow[MST_BINTable.BINID_FLD].ToString());
					}
					catch{}
					try
					{
						intLocationID = int.Parse(drDataRow[MST_LocationTable.LOCATIONID_FLD].ToString().Trim());
					}
					catch
					{
						throw new PCSBOException(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, string.Empty, new Exception());
					}
					#endregion

					#region 3.3.2 Add Inventory by Bom
					BomBO boBom = new BomBO();
					DataTable dtbBom = boBom.ListBOMDetailsOfProduct(intProductID);
					
					foreach(DataRow drowBom in dtbBom.Rows)
					{
						#region subtract cache
						// Get available quantity by Postdate
						int intComponentID = Convert.ToInt32(drowBom[ITM_BOMTable.COMPONENTID_FLD]);
						decimal decBOMQty = Convert.ToDecimal(drowBom[ITM_BOMTable.QUANTITY_FLD]);
						objInventoryBO.UpdateAddOHQuantity(voPurchaseOrderMaster.CCNID,voPurchaseOrderMaster.MasterLocationID,
							intProLocationID,intProBinID,intComponentID,decBOMQty*decReceiveQuantity,string.Empty,string.Empty);
						#endregion
					}
					#endregion
					
					//3.3.3 subtract Inventory by Purchase Order Receipt
					objInventoryBO.UpdateSubtractOHQuantity(voPurchaseOrderMaster.CCNID, voPurchaseOrderMaster.MasterLocationID, intLocationID, intBinID, intProductID, decReceiveQuantity, string.Empty, string.Empty);
										
					break;
					#endregion
			}

			//3. Update TransactionHistory
			objPORDetailDS.UpdateTranType(printPOReceiptMasterID,printPOReceiptDetailID,intOldTranTypeID,intNewTranTypeID,constInspStatus);
			//4. Delete row in PurchaseOrderReceipt Detail (One row)
            objPORDetailDS.DeleteRowDetail(printPOReceiptMasterID,printPOReceiptDetailID);
		}


	
		public string CheckReturn(PO_PurchaseOrderReceiptMasterVO pvoReceiptMaster, string pstrProductID, bool pblnByInvoice)
		{
			PO_PurchaseOrderReceiptMasterDS dsReceiptMaster = new PO_PurchaseOrderReceiptMasterDS();
			return dsReceiptMaster.CheckReturn(pvoReceiptMaster, pstrProductID, pblnByInvoice);
		}
	}
}

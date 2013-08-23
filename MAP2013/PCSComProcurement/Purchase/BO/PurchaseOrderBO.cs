using System;
using System.Collections;
using System.Data;
using System.Linq;
using PCSComMaterials.Plan.DS;
using PCSComProcurement.Purchase.DS;
using PCSComProduct.Items.DS;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;


namespace PCSComProcurement.Purchase.BO
{
	/// <summary>
	/// Summary description for .
	/// </summary>
	public class PurchaseOrderBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.PurchaseOrderBO";
		const int INT_BEGIN_DATA_ROW = 3;
		const int INDEX_CODE = 2;
		const int INDEX_NAME = 3;
		const int INDEX_REVISION = 4;
		
		public object GetMasterVO(int pintPOMasterID)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			return dsMaster.GetObjectVO(pintPOMasterID);
		}
	
		public DataRow LoadObjectVO(int pintID)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			return dsMaster.LoadObjectVO(pintID);
		}
	
		public object GetPartyVO(int pintID)
		{
			MST_PartyDS dsMaster = new MST_PartyDS();
			return dsMaster.GetObjectVO(pintID);
		}

	
		public DataSet ListDetailByMaster(int pintID)
		{
			PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
			return dsDetail.List(pintID);
		}

	
		public object GetExchangeRate(int pintCurrencyID,DateTime pdtmOrderDate)
		{
			MST_ExchangeRateDS dsMST = new MST_ExchangeRateDS();
			return dsMST.GetExchangeRate(pintCurrencyID,pdtmOrderDate);
		}
	
		public void UpdatePurchaseOrder(object pvoPOMaster,DataSet pdstDetail)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			dsMaster.Update(pvoPOMaster);
			if(pdstDetail != null)
			{
				foreach (DataRow objRow in pdstDetail.Tables[0].Rows)
				{
					if(objRow.RowState == DataRowState.Deleted) continue;
					objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = ((PO_PurchaseOrderMasterVO)pvoPOMaster).PurchaseOrderMasterID;
				}
			}
			PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
			dsDetail.UpdateDataSet(pdstDetail);
		}
	
		public int AddNewPurchaseOrder(object pvoPOMaster,DataSet pdstPODetail)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			int intPOMasterID = dsMaster.AddAndReturnID(pvoPOMaster);
			foreach (DataRow objRow in pdstPODetail.Tables[0].Rows)
			{
				if(objRow.RowState == DataRowState.Deleted) continue;
				objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = intPOMasterID;
			}
			PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
			dsDetail.UpdateDataSet(pdstPODetail);
			return intPOMasterID;
		}

	
		public DataTable ListUnitOfMeasure()
		{
			MST_UnitOfMeasureDS dsUnitOfMeasure = new MST_UnitOfMeasureDS();
			return dsUnitOfMeasure.List().Tables[0];	
		}

	
		public object GetUnitOfMeasure(int pintID)
		{
			MST_UnitOfMeasureDS dsUnitOfMeasure = new MST_UnitOfMeasureDS();
			return dsUnitOfMeasure.GetObjectVO(pintID);		
		}

	
		public void DeletePurchaseOrder(int pintMasterID)
		{
			PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
			DataSet dstDetail = dsDetail.List(pintMasterID);
			foreach(DataRow drow in dstDetail.Tables[0].Rows)
			{
				dsDetail.Delete(int.Parse(drow[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString()));
			}
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			dsMaster.Delete(pintMasterID);
		}
	
		public object GetProductVO(int pintID)
		{
			ITM_ProductDS dsProduct = new ITM_ProductDS();
			return dsProduct.GetObjectVO(pintID);
		}
		
		/// <summary>
		/// CheckReferenceAndRevisionNo
		/// </summary>
		/// <param name="pstrReferenceNo"></param>
		/// <param name="pstrRevision"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, September 22 2006</date>
	
		public bool CheckReferenceAndRevisionNo(string pstrReferenceNo, string pstrRevision, int pintPOMasterID)
		{
			PO_PurchaseOrderMasterDS dsPurchaseOrderMaster = new PO_PurchaseOrderMasterDS();
			DataSet	dstCount = new DataSet();
			dstCount = dsPurchaseOrderMaster.CheckReferenceAndRevisionNo(pstrReferenceNo, pstrRevision, pintPOMasterID);
			if (dstCount.Tables[0].Rows.Count > 0)
			{
				if (int.Parse(dstCount.Tables[0].Rows[0][0].ToString()) > 0)
				{
					return false;
				}
			}	
			
			
			return true;
		}
	
		public object GetPurchaseOrderByCode(string pstrCode)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			return dsMaster.GetObjectVO(pstrCode);
		}
		
	
		public int IsValidateData(string pstrValue,string pstrTable,string pstrField,string pstrCodition)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			return dsMaster.IsValidateData(pstrValue,pstrTable,pstrField,pstrCodition);
		}

		public DataRow GetDataRow(string pstrKeyField,string pstrValue,string pstrTable,string pstrField,string pstrCodition)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			return dsMaster.GetDataRow(pstrKeyField,pstrValue,pstrTable,pstrField,pstrCodition);
		}
		public void Add(object pObjectDetail)
		{
			// TODO:  

		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  

		}
	
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
			// TODO:  

		}

		public DataRow GetVendorLocationByVendor(int pintPartyID)
		{
			return null;
		}
		public DataSet GetItemVendorReference(int pintPartyID, Hashtable pobjProductID)
		{
			return null;
		}

		/// <summary>
		/// Add new for case : Convert CPO to new PO
		/// </summary>
		/// <param name="pdstCPODetail"></param>
		/// <param name="pobjMasterVO"></param>
		/// <param name="pdstDetail"></param>
		///<author>TuanDM</author>
	
		public int AddPOAndDelSchedule(System.Data.DataSet pdstCPODetail, object pobjMasterVO, System.Data.DataSet pdstDetail)
		{
			//Add Master and Get returning ID
			PO_PurchaseOrderMasterDS dsPOMaster = new PO_PurchaseOrderMasterDS();
			int intMasterID = 	dsPOMaster.AddAndReturnID(pobjMasterVO);

			//Add detail
			ArrayList arlDueDate = new ArrayList();
			foreach (DataRow drowDetail in pdstDetail.Tables[0].Rows)
			{
				drowDetail[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = intMasterID;
				arlDueDate.Add(drowDetail[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString());
			}
			PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
				
			dsPODetail.UpdateDataSet(pdstDetail);

			//Get PODetail DataSet which includes PURCHASEORDERDETAILID_FLD
			pdstDetail = dsPODetail.List(intMasterID);
				
			//Create DeliverySchedule -- and add DeliverySchedule
			PO_DeliveryScheduleVO voSchedule;
			for (int i =0; i < pdstDetail.Tables[0].Rows.Count; i++)
			{
				voSchedule = new PO_DeliveryScheduleVO();
				voSchedule.DeliveryLine = 1;
				voSchedule.PurchaseOrderDetailID = int.Parse(pdstDetail.Tables[0].Rows[i][PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
				voSchedule.ScheduleDate = DateTime.Parse(arlDueDate[i].ToString());
				voSchedule.DeliveryQuantity = decimal.Parse(pdstDetail.Tables[0].Rows[i][PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString());
				new PO_DeliveryScheduleDS().Add(voSchedule);
			}
	
			//Update CPODetail
			new MTR_CPODS().SetPOMasterID(null, intMasterID);

			//return ID
			return intMasterID;
		}
		/// <summary>
		/// Update PO and Delivery Schedule in case : CPO to Exist PO
		/// </summary>
		/// <param name="pdstCPODetail"></param>
		/// <param name="pobjMasterVO"></param>
		/// <param name="pdstDetail"></param>
		/// <author>TuanDM</author>
	
		public void UpdatePOAndDelSchedule(System.Data.DataSet pdstCPODetail, object pobjMasterVO, System.Data.DataSet pdstDetail)
		{
			//Update PO Master
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			dsMaster.Update(pobjMasterVO);

			//Update PO Detail
			ArrayList arlIndexs = new ArrayList();
			ArrayList arlDueDate = new ArrayList();
			int intinD = -1;
			if(pdstDetail != null)
				foreach (DataRow objRow in pdstDetail.Tables[0].Rows)
				{
					intinD++;
					if(objRow.RowState == DataRowState.Deleted) continue;
					objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = ((PO_PurchaseOrderMasterVO)pobjMasterVO).PurchaseOrderMasterID;
					if (objRow[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD].ToString() != string.Empty)
					{
						arlDueDate.Add(objRow[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
						arlIndexs.Add(intinD); 
					}
				}
			PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
			dsDetail.UpdateDataSet(pdstDetail);

			//Update DeliverySchedule
			pdstDetail = dsDetail.List(((PO_PurchaseOrderMasterVO) pobjMasterVO).PurchaseOrderMasterID);
				
			//Create DeliverySchedule -- and add DeliverySchedule
			for (int i =0; i < arlIndexs.Count; i++)
			{
				PO_DeliveryScheduleVO voSchedule = new PO_DeliveryScheduleVO();
				voSchedule.DeliveryLine = 1;
				voSchedule.PurchaseOrderDetailID = int.Parse(pdstDetail.Tables[0].Rows[int.Parse(arlIndexs[i].ToString())][PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD].ToString());
				voSchedule.ScheduleDate = DateTime.Parse(arlDueDate[i].ToString());
				voSchedule.DeliveryQuantity = decimal.Parse(pdstDetail.Tables[0].Rows[int.Parse(arlIndexs[i].ToString())][PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString());
				new PO_DeliveryScheduleDS().Add(voSchedule);
			}
				
			//Update CPODetail
			new MTR_CPODS().SetPOMasterID(null, ((PO_PurchaseOrderMasterVO) pobjMasterVO).PurchaseOrderMasterID);
		}
		public DataRow GetVendorContact(int pintPartyLocationID)
		{
			return null;
		}

		/// <summary>
		/// Add new for case : Convert CPO to new PO
		/// </summary>
		/// <param name="pobjMasterVO"></param>
		/// <param name="pdstDetail"></param>
		/// <param name="pdstDelivery"></param>
		///<author>TuanDM</author>
	
		public int AddPOAndDelScheduleImmediate(object pobjMasterVO, System.Data.DataSet pdstDetail, DataSet pdstDelivery, ArrayList parlCPOIDs)
		{
			//Add Master and Get returning ID
			PO_PurchaseOrderMasterDS dsPOMaster = new PO_PurchaseOrderMasterDS();
			int intMasterID = 	dsPOMaster.AddAndReturnID(pobjMasterVO);

			//Add detail
			foreach (DataRow drowDetail in pdstDetail.Tables[0].Rows)
			{
				drowDetail[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = intMasterID;
			}
			PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
			dsPODetail.UpdateDataSet(pdstDetail);

			//Get PODetail DataSet which includes PURCHASEORDERDETAILID_FLD
			pdstDetail = dsPODetail.ListToGetID(intMasterID);
				
			//Create DeliverySchedule -- and add DeliverySchedule
			int intPOLineID =0;
			int i =0;
			PO_DeliveryScheduleDS dsDelivery = new PO_DeliveryScheduleDS();
				
			while (i++ < pdstDetail.Tables[0].Rows.Count)
			{
				DataSet dstNewDelPO = pdstDelivery.Clone();
					
				DataRow[] drowSameDelivery = pdstDelivery.Tables[0].Select(ITM_ProductTable.PRODUCTID_FLD + "='" + pdstDetail.Tables[0].Rows[i-1][ITM_ProductTable.PRODUCTID_FLD].ToString() + "'");
					
				intPOLineID = (int) pdstDetail.Tables[0].Rows[i-1][PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD];
				foreach (DataRow t in drowSameDelivery)
				{
				    int k = GetIndexForDeliveryLine(dstNewDelPO, (DateTime) t[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
				    if (k > -1)
				    {
				        dstNewDelPO.Tables[0].Rows[k][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = (decimal)dstNewDelPO.Tables[0].Rows[k][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] + (decimal) t[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
				        t[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = intPOLineID;
				    }
				    else
				    {
				        t[PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = intPOLineID;
				        t[PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = dstNewDelPO.Tables[0].Rows.Count + 1;
				        dstNewDelPO.Tables[0].ImportRow(t);
				    }
				}
				dsDelivery.UpdateDataSet(dstNewDelPO);
			}
			//Update CPODetail
			MTR_CPODS dsCPO = new MTR_CPODS();
			if (parlCPOIDs.Count > 0)
			{
				if (Convert.ToInt32(parlCPOIDs[0]) > 0)
				{
					dsCPO.SetPOMasterID(parlCPOIDs, intMasterID);
				}
				else
				{
					dsCPO.SetPOMasterIDForDCPDetail(parlCPOIDs, intMasterID);
				}
			}

			return intMasterID;
		}

		/// <summary>
		/// Add new for case : Convert CPO to new PO
		/// </summary>
		/// <param name="pobjMasterVO"></param>
		/// <param name="pdstDetail"></param>
		/// <param name="pdstDelivery"></param>
		///<author>TuanDM</author>
	
		public void UpdatePOAndDelScheduleImmediate(object pobjMasterVO, System.Data.DataSet pdstDetail, DataSet pdstDelivery, ArrayList parlCPOIDs)
		{
			//Add Master and Get returning ID
			PO_PurchaseOrderMasterDS dsPOMaster = new PO_PurchaseOrderMasterDS();
			dsPOMaster.Update(pobjMasterVO);
		    var voMaster = (PO_PurchaseOrderMasterVO) pobjMasterVO;

			//Update detail
			DataSet dstPODetail = pdstDetail.Copy();
			PO_PurchaseOrderDetailDS dsPODetail = new PO_PurchaseOrderDetailDS();
			foreach (DataRow drowDetail in pdstDetail.Tables[0].Rows)
			{
				if (drowDetail.RowState == DataRowState.Added)
				{
					drowDetail[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = ((PO_PurchaseOrderMasterVO) pobjMasterVO).PurchaseOrderMasterID;
				}
			}
			dsPODetail.UpdateDataSet(pdstDetail);

			//Get PODetail DataSet which includes PURCHASEORDERDETAILID_FLD
			pdstDetail = dsPODetail.ListToGetID(((PO_PurchaseOrderMasterVO) pobjMasterVO).PurchaseOrderMasterID);
				
			//Create DeliverySchedule -- and add DeliverySchedule
			int intPOLineID =0;
			PO_DeliveryScheduleDS dsDelivery = new PO_DeliveryScheduleDS();
			for (int i =0; i <dstPODetail.Tables[0].Rows.Count; i++)
			{
				DataRow[] drowSameDelivery = pdstDelivery.Tables[0].Select(ITM_ProductTable.PRODUCTID_FLD + "='" + dstPODetail.Tables[0].Rows[i][ITM_ProductTable.PRODUCTID_FLD].ToString() + "'");
                    				
				intPOLineID = (int) pdstDetail.Tables[0].Select(PO_PurchaseOrderDetailTable.LINE_FLD + "='" + dstPODetail.Tables[0].Rows[i][PO_PurchaseOrderDetailTable.LINE_FLD].ToString() + "'")[0][PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
				DataSet dstNewDelPO = dsDelivery.GetDeliverySchedule(intPOLineID);
				int intbaseCount = dstNewDelPO.Tables[0].Rows.Count;
				for (int j =0; j <drowSameDelivery.Length; j++)
				{
					int k = GetIndexForDeliveryLine(dstNewDelPO, (DateTime) drowSameDelivery[j][PO_DeliveryScheduleTable.SCHEDULEDATE_FLD]);
					if (k > -1)
					{
						dstNewDelPO.Tables[0].Rows[k][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = (decimal)dstNewDelPO.Tables[0].Rows[k][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] + (decimal) drowSameDelivery[j][PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD];
					}
					else
					{
						drowSameDelivery[j][PO_DeliveryScheduleTable.PURCHASEORDERDETAILID_FLD] = intPOLineID;
						drowSameDelivery[j][PO_DeliveryScheduleTable.DELIVERYLINE_FLD] = j+1 + intbaseCount;
						dstNewDelPO.Tables[0].ImportRow(drowSameDelivery[j]);
					}
				}
				dsDelivery.UpdateDataSet(dstNewDelPO);
			}

			//Update CPODetail
			MTR_CPODS dsCPO = new MTR_CPODS();
			dsCPO.SetPOMasterID(parlCPOIDs, ((PO_PurchaseOrderMasterVO) pobjMasterVO).PurchaseOrderMasterID);
		}

		private int GetIndexForDeliveryLine(DataSet pdstData, DateTime dtmDuedate)
		{
			int i =0;
			foreach (DataRow drowData in pdstData.Tables[0].Rows)
			{
				if ( (DateTime)drowData[PO_DeliveryScheduleTable.SCHEDULEDATE_FLD] == dtmDuedate)
					break;
				i += 1;
			}
			if (i == pdstData.Tables[0].Rows.Count) i = -1;
			return i;
		}


		#region Import functions

	
		public int ImportNewMappingData(DataTable dtImpData, int intPartyID, int intCCNID, int intMaxLine, DataSet dstMappingData)
		{
			int intResult = 0;
			int intMaxID = 0;
			dstMappingData.Tables[0].DefaultView.Sort = PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD;
			try
			{
				intMaxID = int.Parse(dstMappingData.Tables[0].Rows[dstMappingData.Tables[0].Rows.Count - 1][PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
			}
			catch
			{
				intMaxID = 0;							
			}
			dstMappingData.Tables[0].DefaultView.Sort = string.Empty;
			
			ITM_ProductDS dsProduct = new ITM_ProductDS();

			for (int i = INT_BEGIN_DATA_ROW; i < dtImpData.Rows.Count; i++)
			{
				string strItemCode = dtImpData.Rows[i][INDEX_CODE].ToString();
				string strDescription = dtImpData.Rows[i][INDEX_NAME].ToString();
				string strRevision = dtImpData.Rows[i][INDEX_REVISION].ToString();

				//find out total quantity at last column
				decimal dcmOrderQty = decimal.Parse(dtImpData.Rows[i][dtImpData.Columns.Count - 1].ToString());
					
				//check if this item existed, update quantity only
				DataRow[] arrRows = dstMappingData.Tables[0].Select(ITM_ProductTable.CODE_FLD + "='" + strItemCode + "'");
				if (arrRows.Length > 0)
				{
					arrRows[0][PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = decimal.Parse(arrRows[0][PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD].ToString()) + dcmOrderQty;
					continue;
				}

				if (dcmOrderQty <= 0)
					continue;

				ITM_ProductVO voProduct = (ITM_ProductVO)dsProduct.GetObjectVO(strItemCode, strDescription, strRevision);

				//New row
				DataRow dr = dstMappingData.Tables[0].NewRow();

				UtilsBO boUtils = new UtilsBO();
				//fill row
				dr[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = dcmOrderQty;
				dr[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = voProduct.ProductID;
				dr[ITM_ProductTable.CODE_FLD] = voProduct.Code;
				dr[ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
				dr[ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
				dr[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = voProduct.BuyingUMID;
				dr[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = voProduct.ListPrice;
				dr[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] =  voProduct.ListPrice * dcmOrderQty;
				dr[PO_PurchaseOrderDetailTable.UMRATE_FLD] = boUtils.GetUMRate(voProduct.StockUMID, voProduct.BuyingUMID);
				dr[PO_PurchaseOrderDetailTable.LINE_FLD] = ++intMaxLine;
				dr[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] = ++intMaxID;
				dr[PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
				dstMappingData.Tables[0].Rows.Add(dr);
			}

			return intResult;
		}

	
		public int ImportNewPurchaseOrder(object pvoPOMaster, DataSet pdstDetail)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			int intPOMasterID = dsMaster.AddAndReturnID(pvoPOMaster);
	
			if(pdstDetail.Tables.Count > 0)
			{
				foreach (DataRow objRow in pdstDetail.Tables[0].Rows)
				{
					if(objRow.RowState == DataRowState.Deleted)
						continue;
					objRow[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = intPOMasterID;
				}
				PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
				dsDetail.UpdateDataSetForImport(pdstDetail, intPOMasterID);
			}
			return intPOMasterID;
		}
	
		public DataSet ListScheduleForImport(int pintMasterID)
		{
			PO_DeliveryScheduleDS dsDelivery = new PO_DeliveryScheduleDS();
			return dsDelivery.ListForImport(pintMasterID);
		}
	
		public void UpdateScheduleForImport(DataSet pdstData)
		{
			PO_DeliveryScheduleDS dsDelivery = new PO_DeliveryScheduleDS();
			dsDelivery.UpdateDataSet(pdstData);
		}

	
		public void UpdateDeletedRowInDataSet(DataSet pdstDelSchData, int pintPOMasterID)
		{
			PO_DeliveryScheduleDS dsDelSch = new PO_DeliveryScheduleDS();
			dsDelSch.UpdateDeletedRowInDataSet(pdstDelSchData, pintPOMasterID);
		}
	
		public int ImportUpdateMappingData(DataTable dtImpData, int intPartyID, int intCCNID, int intMaxLine, DataSet dstMappingData)
		{
			int intResult = 0;
			const string TEMP_QTY_COL_NAME = "TempQty";

			//Add new column for temp qty
			DataColumn objCol = new DataColumn(TEMP_QTY_COL_NAME);
			objCol.DataType = typeof(Decimal);
			objCol.DefaultValue = 0;
			dstMappingData.Tables[0].Columns.Add(objCol);
			int intMaxID = 0;
			dstMappingData.Tables[0].DefaultView.Sort = PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD;
			try
			{
				intMaxID = int.Parse(dstMappingData.Tables[0].Rows[dstMappingData.Tables[0].Rows.Count - 1][PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
			}
			catch
			{
				intMaxID = 0;							
			}
			dstMappingData.Tables[0].DefaultView.Sort = string.Empty;
			
			//walk through data	
			ITM_ProductDS dsProduct = new ITM_ProductDS();
				
			for (int i = INT_BEGIN_DATA_ROW; i < dtImpData.Rows.Count; i++)
			{
				//findout Item Code
				string strItemCode = dtImpData.Rows[i][INDEX_CODE].ToString();
				string strDescription = dtImpData.Rows[i][INDEX_NAME].ToString();
				string strRevision = dtImpData.Rows[i][INDEX_REVISION].ToString();
				//find out total quantity at last column
				decimal dcmOrderQty = int.Parse(dtImpData.Rows[i][dtImpData.Columns.Count - 1].ToString());
					
				//check if this item existed, update quantity only
				DataRow[] arrRows = dstMappingData.Tables[0].Select(ITM_ProductTable.CODE_FLD + "='" + strItemCode + "'");
				if (arrRows.Length > 0)
				{
					arrRows[0][TEMP_QTY_COL_NAME] = decimal.Parse(arrRows[0][TEMP_QTY_COL_NAME].ToString()) + dcmOrderQty;
					continue;
				}
				if (dcmOrderQty <= 0)
					continue;

				ITM_ProductVO voProduct = (ITM_ProductVO)dsProduct.GetObjectVO(strItemCode, strDescription, strRevision);

				//New row
				DataRow dr = dstMappingData.Tables[0].NewRow();

				UtilsBO boUtils = new UtilsBO();
				//fill row
				dr[TEMP_QTY_COL_NAME] = dcmOrderQty;
				dr[PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = dcmOrderQty;
				dr[PO_PurchaseOrderDetailTable.PRODUCTID_FLD] = voProduct.ProductID;
				dr[ITM_ProductTable.CODE_FLD] = voProduct.Code;
				dr[ITM_ProductTable.DESCRIPTION_FLD] = voProduct.Description;
				dr[ITM_ProductTable.REVISION_FLD] = voProduct.Revision;
				dr[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD] = voProduct.BuyingUMID;
				dr[PO_PurchaseOrderDetailTable.UNITPRICE_FLD] = voProduct.ListPrice;
				dr[PO_PurchaseOrderDetailTable.TOTALAMOUNT_FLD] =  voProduct.ListPrice * dcmOrderQty;
				dr[PO_PurchaseOrderDetailTable.UMRATE_FLD] = boUtils.GetUMRate(voProduct.StockUMID, voProduct.BuyingUMID);
				dr[PO_PurchaseOrderDetailTable.LINE_FLD] = ++intMaxLine;
				dr[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] = ++intMaxID;
				dr[PO_PurchaseOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
				dr[TEMP_QTY_COL_NAME] = dcmOrderQty;
				dstMappingData.Tables[0].Rows.Add(dr);
			}
			if (intResult != 0) 
			{
				dstMappingData.Tables[0].Columns.Remove(objCol);
				return intResult;
			}
			//refine data, with correct line
			int intLine = 1;
			for (int i = 0; i < dstMappingData.Tables[0].Rows.Count; i++)
			{
				if (int.Parse(dstMappingData.Tables[0].Rows[i][TEMP_QTY_COL_NAME].ToString()) == 0)
					dstMappingData.Tables[0].Rows[i][PO_PurchaseOrderDetailTable.LINE_FLD] = -1;
				else
				{
					//Update Line
					dstMappingData.Tables[0].Rows[i][PO_PurchaseOrderDetailTable.LINE_FLD] = intLine;
					//Update quantity
					dstMappingData.Tables[0].Rows[i][PO_PurchaseOrderDetailTable.ORDERQUANTITY_FLD] = dstMappingData.Tables[0].Rows[i][TEMP_QTY_COL_NAME];
					intLine++;
				}
			}

			dstMappingData.Tables[0].Columns.Remove(objCol);
			return intResult;
		}

	
		public DataTable GetRemainQuantity(int pintMasterID)
		{
			return null;
		}
	
		public void UpdateInsertedRowInDataSet(DataSet pdstDelSchData, int pintMasterID)
		{
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			dsSchedule.UpdateInsertedRowInDataSet(pdstDelSchData, pintMasterID);
		}

	
		public void ImportUpdatePurchaseOrder(int pintPOMasterID, DataSet pdstDetail, ref int pintErrorLine)
		{
			const string TEMP_QTY_COL_NAME = "TempQty";
			const string METHOD_NAME = THIS + ".ImportUpdateSaleOrder()";
			const string SUMCOMMITQUANTITY_FLD = "SUMCommitQuantity";

			if(pdstDetail.Tables.Count > 0)
			{
				foreach (DataRow drowDetail in pdstDetail.Tables[0].Rows)
				{
					if(drowDetail.RowState == DataRowState.Deleted)
						continue;
					if (int.Parse(drowDetail[PO_PurchaseOrderDetailTable.LINE_FLD].ToString()) == -1)
						drowDetail.Delete();
					else
						drowDetail[PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD] = pintPOMasterID;
				}
				PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();

				//update sale order detail dataset
				dsDetail.UpdateDataSetForImport(pdstDetail,pintPOMasterID);
			}
			pintErrorLine = -1;
		}

		#endregion

		#region Delete estimate purchase order funcation
		
        /// <summary>
		/// Delete purchase order of estimate month
		/// </summary>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <param name="pintPOType">Purchase Type</param>
		/// <param name="pstrVendorID">Vendor List</param>
		/// <param name="pstrItemID">Item list</param>
	    public void DeleteEstimatePO(DateTime pdtmFromDate, DateTime pdtmToDate, int pintPOType, string pstrVendorID, string pstrItemID)
		{
			PO_PurchaseOrderMasterDS dsMaster = new PO_PurchaseOrderMasterDS();
			// get list of purchase order master ID to be delete
			DataTable dtbMaster = dsMaster.ListMasterToDelete(pdtmFromDate, pdtmToDate, pintPOType, pstrItemID, pstrItemID).Tables[0];
			string strMasterId = dtbMaster.Rows.Cast<DataRow>().Aggregate("0", (current, drowMaster) => current + ("," + drowMaster[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD]));
		    // delete delivery schedule first
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			dsSchedule.DeleteByPOMaster(strMasterId);
			// delete purchase order detail
			PO_PurchaseOrderDetailDS dsDetail = new PO_PurchaseOrderDetailDS();
			dsDetail.DeleteByMaster(strMasterId);
			// delete purchase order master
			dsMaster.Delete(strMasterId);
		}
		#endregion
	}
}

using System;
using System.Data;
using PCSComMaterials.Inventory.DS;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

namespace PCSComSale.Order.BO
{
	public class SOConfirmShipManagementBO
	{
		private const string This = "PCSComSale.Order.BO.ISOConfirmShipBO";
		public object GetSaleOrderMasterVO(int pintSOMasterId)
		{
            var dsSOMaster = new SO_SaleOrderMasterDS();
            return dsSOMaster.GetObjectVO(pintSOMasterId);
		}

        public DataSet GetCommitedDelSchLines(int pintSOMasterId, string strGateIDs, DateTime pdtmFromDate, DateTime pdtmToDate, int locationId, int binId, int type, decimal exchangeRate = 1)
		{
			var dsMaster = new SO_ConfirmShipMasterDS();
			return dsMaster.GetCommitedDelSchLines(pintSOMasterId, strGateIDs, pdtmFromDate, pdtmToDate, locationId, binId, type, exchangeRate);
		}
		public DataSet GetExistedForView(int pintMasterId, bool pblnIsInvoice)
		{
			DataSet dstData;
			if (!pblnIsInvoice)
			{
				var dsCsDetail = new SO_ConfirmShipDetailDS();
				dstData = dsCsDetail.ListForReview(pintMasterId);
			}
			else
			{
				var dsCsDetail = new SO_InvoiceDetailDS();
				dstData = dsCsDetail.ListForReview(pintMasterId);
			}
			return dstData;
		}
		public int AddSOInvoiceMaster(object pobjSOInvoice, DataSet pdstData)
		{
            const string methodName = This + ".AddSOInvoiceMaster()";
            var objMaster = new SO_InvoiceMaster();
		    try
		    {
		        var objVO = (SO_InvoiceMasterVO) pobjSOInvoice;
		        using (var trans = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(1)))
		        {
		            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
		            {
		                #region Insert Invoice Master

		                objMaster.ConfirmShipNo = objVO.ConfirmShipNo;
		                if (objVO.ShippedDate != DateTime.MinValue)
		                {
		                    objMaster.ShippedDate = objVO.ShippedDate;
		                }
		                objMaster.SaleOrderMasterID = objVO.SaleOrderMasterID;
		                objMaster.MasterLocationID = objVO.MasterLocationID;
		                objMaster.CurrencyID = objVO.CurrencyID;
		                objMaster.ExchangeRate = objVO.ExchangeRate;
		                objMaster.ShipCode = objVO.ShipCode;
		                objMaster.FromPort = objVO.FromPort;
		                objMaster.UserName = SystemProperty.UserName;
		                objMaster.CNo = objVO.CNo;
		                objMaster.Measurement = objVO.Measurement;
		                objMaster.GrossWeight = objVO.GrossWeight;
		                objMaster.NetWeight = objVO.NetWeight;
		                objMaster.IssuingBank = objVO.IssuingBank;
		                objMaster.LCNo = objVO.LCNo;
		                objMaster.VesselName = objVO.VesselName;
		                objMaster.Comment = objVO.Comment;
		                objMaster.ReferenceNo = objVO.ReferenceNo;
		                objMaster.InvoiceNo = objVO.InvoiceNo;
		                objMaster.PONumber = objVO.PONumber;
		                if (objVO.LCDate != DateTime.MinValue)
		                {
		                    objMaster.LCDate = objVO.LCDate;
		                }
		                if (objVO.OnBoardDate != DateTime.MinValue)
		                {
		                    objMaster.OnBoardDate = objVO.OnBoardDate;
		                }
		                if (objVO.InvoiceDate != DateTime.MinValue)
		                {
		                    objMaster.InvoiceDate = objVO.InvoiceDate;
		                }
		                objMaster.CCNID = objVO.CCNID;
		                objMaster.BinID = objVO.BinID;
		                objMaster.LocationID = objVO.LocationID;

		                #endregion

		                #region Insert Detail

		                var listDetail = new List<SO_InvoiceDetail>();

		                foreach (DataRow dr in pdstData.Tables[0].Rows)
		                {
		                    if (dr.RowState == DataRowState.Deleted)
		                        continue;
		                    var objDetail = new SO_InvoiceDetail
		                    {
                                PONumber = dr["PONumber"].ToString()
                            };
		                    if (dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
		                    {
		                        objDetail.DeliveryScheduleID = Convert.ToInt32(dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD]);
		                    }
		                    if (dr[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
		                    {
		                        objDetail.ProductID = Convert.ToInt32(dr[ITM_ProductTable.PRODUCTID_FLD]);
		                    }
		                    if (dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
		                    {
		                        objDetail.SaleOrderDetailID =
		                            Convert.ToInt32(dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]);
		                    }
		                    if (dr[SO_ConfirmShipDetailTable.PRICE_FLD] != DBNull.Value)
		                    {
		                        objDetail.Price = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.PRICE_FLD]);
		                    }
		                    if (dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD] != DBNull.Value)
		                    {
		                        objDetail.InvoiceQty = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD]);
		                    }
		                    if (dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD] != DBNull.Value)
		                    {
		                        objDetail.NetAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
		                    }
		                    if (dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD] != DBNull.Value)
		                    {
		                        objDetail.VATAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD]);
		                    }
		                    if (dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD] != DBNull.Value)
		                    {
		                        objDetail.VATPercent = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD]);
		                    }
		                    listDetail.Add(objDetail);
		                }
		                objMaster.SO_InvoiceDetails.AddRange(listDetail);

		                #endregion

		                db.SO_InvoiceMasters.InsertOnSubmit(objMaster);
		                db.SubmitChanges();
		            }
		            trans.Complete();
		        }
		    }
		    catch (PCSBOException ex)
		    {
		        if (ex.mCode == ErrorCode.SQLDUPLICATE_KEYCODE)
		            throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
		        if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
		            throw new PCSDBException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, methodName, ex);
		        throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
		    }

		    return objMaster.InvoiceMasterID;
		}
		public void ModifyInvoice(object pobjInvoiceMaster, DataSet pdstData, List<int> removedId)
		{
            //Update Master
            using (var trans = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(1)))
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var objVO = (SO_InvoiceMasterVO)pobjInvoiceMaster;

                    #region update SO_InvoiceMaster
                    var objMaster = db.SO_InvoiceMasters.SingleOrDefault(e => e.CCNID == objVO.CCNID && e.InvoiceMasterID == objVO.InvoiceMasterID );
                    if (objMaster == null)
                        return;
                    objMaster.ConfirmShipNo = objVO.ConfirmShipNo;
                    if (objVO.ShippedDate != DateTime.MinValue)
                    {
                        objMaster.ShippedDate = objVO.ShippedDate;
                    }
                    objMaster.SaleOrderMasterID = objVO.SaleOrderMasterID;
                    objMaster.MasterLocationID = objVO.MasterLocationID;
                    objMaster.CurrencyID = objVO.CurrencyID;
                    objMaster.ExchangeRate = objVO.ExchangeRate;
                    objMaster.ShipCode = objVO.ShipCode;
                    objMaster.FromPort = objVO.FromPort;
                    objMaster.CNo = objVO.CNo;
                    objMaster.Measurement = objVO.Measurement;
                    objMaster.GrossWeight = objVO.GrossWeight;
                    objMaster.NetWeight = objVO.NetWeight;
                    objMaster.IssuingBank = objVO.IssuingBank;
                    objMaster.LCNo = objVO.LCNo;
                    objMaster.VesselName = objVO.VesselName;
                    objMaster.Comment = objVO.Comment;
                    objMaster.ReferenceNo = objVO.ReferenceNo;
                    objMaster.InvoiceNo = objVO.InvoiceNo;
                    objMaster.PONumber = objVO.PONumber;
                    if (objVO.LCDate != DateTime.MinValue)
                    {
                        objMaster.LCDate = objVO.LCDate;
                    }
                    if (objVO.OnBoardDate != DateTime.MinValue)
                    {
                        objMaster.OnBoardDate = objVO.OnBoardDate;
                    }
                    if (objVO.InvoiceDate != DateTime.MinValue)
                    {
                        objMaster.InvoiceDate = objVO.InvoiceDate;
                    }
                    objMaster.CCNID = objVO.CCNID;
                    objMaster.BinID = objVO.BinID;
                    objMaster.LocationID = objVO.LocationID;
                    objMaster.UserName = SystemProperty.UserName;
                    db.SubmitChanges();
                    #endregion

                    #region update SO_InvoiceDetail
                    var listDetail = new List<SO_InvoiceDetail>();
                    if (pdstData != null && pdstData.Tables.Count > 0)
                    {
                        foreach (DataRow dr in pdstData.Tables[0].Rows.Cast<DataRow>().Where(dr => dr.RowState != DataRowState.Deleted))
                        {
                            if (dr[PO_InvoiceDetailTable.INVOICEDETAILID_FLD] != DBNull.Value)
                            {
                                var objDetail = db.SO_InvoiceDetails.SingleOrDefault(e => e.InvoiceDetailID == Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICEDETAILID_FLD]));

                                if (objDetail != null)
                                {
                                    objDetail.InvoiceMasterID = objMaster.InvoiceMasterID;
                                    objDetail.PONumber = dr["PONumber"].ToString();
                                    if (dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                                    {
                                        objDetail.DeliveryScheduleID = Convert.ToInt32(dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD]);
                                    }
                                    if (dr[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
                                    {
                                        objDetail.ProductID = Convert.ToInt32(dr[ITM_ProductTable.PRODUCTID_FLD]);
                                    }
                                    if (dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
                                    {
                                        objDetail.SaleOrderDetailID = Convert.ToInt32(dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]);
                                    }
                                    if (dr[SO_ConfirmShipDetailTable.PRICE_FLD] != DBNull.Value)
                                    {
                                        objDetail.Price = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.PRICE_FLD]);
                                    }
                                    if (dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceQty = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD]);
                                    }
                                    if (dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.NetAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                                    }
                                    if (dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VATAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD]);
                                    }
                                    if (dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VATPercent = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD]);
                                    }
                                }
                            }
                            else
                            {
                                var objDetail = new SO_InvoiceDetail
                                {
                                    InvoiceMasterID = 0,
                                    PONumber = dr["PONumber"].ToString()
                                };
                                if (dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                                {
                                    objDetail.DeliveryScheduleID = Convert.ToInt32(dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD]);
                                }
                                if (dr[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
                                {
                                    objDetail.ProductID = Convert.ToInt32(dr[ITM_ProductTable.PRODUCTID_FLD]);
                                }
                                if (dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
                                {
                                    objDetail.SaleOrderDetailID = Convert.ToInt32(dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]);
                                }
                                if (dr[SO_ConfirmShipDetailTable.PRICE_FLD] != DBNull.Value)
                                {
                                    objDetail.Price = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.PRICE_FLD]);
                                }
                                if (dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD] != DBNull.Value)
                                {
                                    objDetail.InvoiceQty = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD]);
                                }
                                if (dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD] != DBNull.Value)
                                {
                                    objDetail.NetAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                                }
                                if (dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                                {
                                    objDetail.VATAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD]);
                                }
                                if (dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD] != DBNull.Value)
                                {
                                    objDetail.VATPercent = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD]);
                                }
                                listDetail.Add(objDetail);
                            }
                        }
                        db.SO_InvoiceDetails.InsertAllOnSubmit(listDetail);
                    }

                    #endregion

                    #region delete removed id from grid

                    foreach (var detail in removedId.Select(id => db.SO_InvoiceDetails.FirstOrDefault(s => s.InvoiceDetailID == id)).Where(detail => detail != null))
                    {
                        db.SO_InvoiceDetails.DeleteOnSubmit(detail);
                    }

                    #endregion

                    // submit all changes to database
                    db.SubmitChanges();
                }
                trans.Complete();
            }
		}
        public int AddShipData(SO_ConfirmShipMasterVO voConfirmShipMaster, DataSet pdstData)
        {
            const string methodName = This + ".AddShipData()";
            try
            {
                var confirmShipMaster = new SO_ConfirmShipMaster();

                using (var trans = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(1)))
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var serverDate = db.GetServerDate();
                        #region Insert SO_ConfirmShipMaster
                        
                        confirmShipMaster.ConfirmShipNo = voConfirmShipMaster.ConfirmShipNo;
                        if (voConfirmShipMaster.ShippedDate != DateTime.MinValue)
                        {
                            confirmShipMaster.ShippedDate = voConfirmShipMaster.ShippedDate;
                        }
                        confirmShipMaster.SaleOrderMasterID = voConfirmShipMaster.SaleOrderMasterID;
                        confirmShipMaster.MasterLocationID = voConfirmShipMaster.MasterLocationID;
                        confirmShipMaster.CurrencyID = voConfirmShipMaster.CurrencyID;
                        confirmShipMaster.ExchangeRate = voConfirmShipMaster.ExchangeRate;
                        confirmShipMaster.ShipCode = voConfirmShipMaster.ShipCode;
                        confirmShipMaster.FromPort = voConfirmShipMaster.FromPort;
                        confirmShipMaster.UserName = SystemProperty.UserName;
                        confirmShipMaster.CNo = voConfirmShipMaster.CNo;
                        confirmShipMaster.Measurement = voConfirmShipMaster.Measurement;
                        confirmShipMaster.GrossWeight = voConfirmShipMaster.GrossWeight;
                        confirmShipMaster.NetWeight = voConfirmShipMaster.NetWeight;
                        confirmShipMaster.IssuingBank = voConfirmShipMaster.IssuingBank;
                        confirmShipMaster.LCNo = voConfirmShipMaster.LCNo;
                        confirmShipMaster.VesselName = voConfirmShipMaster.VesselName;
                        confirmShipMaster.Comment = voConfirmShipMaster.Comment;
                        confirmShipMaster.ReferenceNo = voConfirmShipMaster.ReferenceNo;
                        confirmShipMaster.InvoiceNo = voConfirmShipMaster.InvoiceNo;
                        confirmShipMaster.PONumber = voConfirmShipMaster.PONumber;
                        if (voConfirmShipMaster.LCDate != DateTime.MinValue)
                        {
                            confirmShipMaster.LCDate = voConfirmShipMaster.LCDate;
                        }
                        if (voConfirmShipMaster.OnBoardDate != DateTime.MinValue)
                        {
                            confirmShipMaster.OnBoardDate = voConfirmShipMaster.OnBoardDate;
                        }
                        if (voConfirmShipMaster.InvoiceDate != DateTime.MinValue)
                        {
                            confirmShipMaster.InvoiceDate = voConfirmShipMaster.InvoiceDate;
                        }
                        confirmShipMaster.CCNID = voConfirmShipMaster.CCNID;
                        confirmShipMaster.BinID = voConfirmShipMaster.BinId;
                        confirmShipMaster.LocationID = voConfirmShipMaster.LocationId;
                        confirmShipMaster.UserName = SystemProperty.UserName;
                        confirmShipMaster.LastChange = serverDate;
                        db.SO_ConfirmShipMasters.InsertOnSubmit(confirmShipMaster);
                        db.SubmitChanges();

                        #endregion

                        #region Insert Detail
                        
                        #region insert SO_ConfirmShipDetail
                        var listShipDetail = new List<SO_ConfirmShipDetail>();

                        foreach (DataRow dr in pdstData.Tables[0].Rows)
                        {
                            if (dr.RowState == DataRowState.Deleted)
                                continue;
                            var objShipDetail = new SO_ConfirmShipDetail
                            {
                                ConfirmShipMasterID = confirmShipMaster.ConfirmShipMasterID,
                                PONumber = dr["PONumber"].ToString()
                            };
                            if (dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                            {
                                objShipDetail.DeliveryScheduleID = Convert.ToInt32(dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD]);
                            }
                            if (dr[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
                            {
                                objShipDetail.ProductID = Convert.ToInt32(dr[ITM_ProductTable.PRODUCTID_FLD]);
                            }
                            if (dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
                            {
                                objShipDetail.SaleOrderDetailID = Convert.ToInt32(dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.PRICE_FLD] != DBNull.Value)
                            {
                                objShipDetail.Price = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.PRICE_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD] != DBNull.Value)
                            {
                                objShipDetail.InvoiceQty = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD] != DBNull.Value)
                            {
                                objShipDetail.NetAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                            {
                                objShipDetail.VATAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD] != DBNull.Value)
                            {
                                objShipDetail.VATPercent = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD]);
                            }
                            listShipDetail.Add(objShipDetail);
                        }
                        db.SO_ConfirmShipDetails.InsertAllOnSubmit(listShipDetail);
                        db.SubmitChanges();

                        #endregion

                        #endregion

                        #region cache data

                        var listData = from objdata in db.SO_ConfirmShipDetails
                                       where objdata.ConfirmShipMasterID == confirmShipMaster.ConfirmShipMasterID
                                       select objdata;
                        int? binId = confirmShipMaster.BinID;
                        int? locationId = confirmShipMaster.LocationID;
                        int masterLocationId = confirmShipMaster.MasterLocationID;
                        int ccnId = confirmShipMaster.CCNID;

                        foreach (var drowData in listData)
                        {
                            #region Update subtract from Bin cache
                            if (binId > 0)
                            {
                                var objBins = db.IV_BinCaches.FirstOrDefault(e => e.BinID == binId && e.CCNID == ccnId && e.MasterLocationID == masterLocationId && e.ProductID == drowData.ProductID && e.LocationID == locationId);
                                if (objBins != null)
                                {
                                    objBins.OHQuantity = objBins.OHQuantity.GetValueOrDefault(0) - drowData.InvoiceQty;
                                }
                                else
                                {
                                    objBins = new IV_BinCache
                                    {
                                        OHQuantity = -drowData.InvoiceQty,
                                        BinID = binId.GetValueOrDefault(0),
                                        CCNID = ccnId,
                                        MasterLocationID = masterLocationId,
                                        LocationID = locationId.GetValueOrDefault(0),
                                        ProductID = drowData.ProductID
                                    };
                                    db.IV_BinCaches.InsertOnSubmit(objBins);
                                }
                            }
                            #endregion

                            #region Update Location cache
                            if (locationId > 0)
                            {
                                var objLocations = db.IV_LocationCaches.FirstOrDefault(e => e.CCNID == ccnId && e.MasterLocationID == masterLocationId && e.ProductID == drowData.ProductID && e.LocationID == locationId);
                                if (objLocations != null)
                                {
                                    objLocations.OHQuantity = objLocations.OHQuantity.GetValueOrDefault(0) - drowData.InvoiceQty;
                                }
                                else
                                {
                                    objLocations = new IV_LocationCache
                                    {
                                        OHQuantity = -drowData.InvoiceQty,
                                        CCNID = ccnId,
                                        MasterLocationID = masterLocationId,
                                        LocationID = locationId.GetValueOrDefault(0),
                                        ProductID = drowData.ProductID
                                    };
                                    db.IV_LocationCaches.InsertOnSubmit(objLocations);
                                }
                            }
                            #endregion

                            #region Update Master Location cache
                            if (masterLocationId > 0)
                            {
                                var objMasLocations = db.IV_MasLocCaches.FirstOrDefault(e => e.CCNID == ccnId && e.MasterLocationID == masterLocationId && e.ProductID == drowData.ProductID);
                                if (objMasLocations != null)
                                {
                                    objMasLocations.OHQuantity = objMasLocations.OHQuantity.GetValueOrDefault(0) - drowData.InvoiceQty;
                                }
                                else
                                {
                                    objMasLocations = new IV_MasLocCache
                                    {
                                        OHQuantity = -drowData.InvoiceQty,
                                        CCNID = ccnId,
                                        MasterLocationID = masterLocationId,
                                        ProductID = drowData.ProductID
                                    };
                                    db.IV_MasLocCaches.InsertOnSubmit(objMasLocations);
                                }
                            }
                            #endregion

                            #region transaction history

                            var transHistory = new MST_TransactionHistory
                            {
                                CCNID = confirmShipMaster.CCNID,
                                PostDate = confirmShipMaster.InvoiceDate,
                                MasterLocationID = confirmShipMaster.MasterLocationID,
                                LocationID = confirmShipMaster.LocationID,
                                BinID = confirmShipMaster.BinID != 0 ? confirmShipMaster.BinID : null,
                                RefMasterID = confirmShipMaster.ConfirmShipMasterID,
                                RefDetailID = drowData.ConfirmShipDetailID,
                                Lot = "",
                                Serial = "",
                                ProductID = drowData.ProductID,
                                StockUMID = null,
                                Quantity = drowData.InvoiceQty,
                                TransDate = serverDate,
                                TranTypeID = 22,
                                UserName = SystemProperty.UserName
                            };

                            db.MST_TransactionHistories.InsertOnSubmit(transHistory);

                            #endregion

                        }

                        #endregion

                        // submit all changes to database
                        db.SubmitChanges();
                    }
                    trans.Complete();
                }
                return confirmShipMaster.ConfirmShipMasterID;
            }
            catch (PCSBOException ex)
            {
                if (ex.mCode == ErrorCode.SQLDUPLICATE_KEYCODE)
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, methodName, ex);
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                    throw new PCSDBException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, methodName, ex);
                throw new PCSDBException(ErrorCode.ERROR_DB, methodName, ex);
            }
        }
        public void ModifyShip(SO_ConfirmShipMasterVO voConfirmShipMaster, DataSet pdstData, List<int> removeIds)
        {
            //Update Master
            using (var trans = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(1)))
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var serverDate = db.GetServerDate();

                    #region Update  SO_ConfirmShipMaster
                    var objConfirmShipMaster = db.SO_ConfirmShipMasters.SingleOrDefault(e => e.CCNID == voConfirmShipMaster.CCNID
                        && e.ConfirmShipNo == voConfirmShipMaster.ConfirmShipNo);
                    if (objConfirmShipMaster != null)
                    {
                        objConfirmShipMaster.ConfirmShipNo = voConfirmShipMaster.ConfirmShipNo;
                        if (voConfirmShipMaster.ShippedDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.ShippedDate = voConfirmShipMaster.ShippedDate;
                        }
                        objConfirmShipMaster.SaleOrderMasterID = voConfirmShipMaster.SaleOrderMasterID;
                        objConfirmShipMaster.MasterLocationID = voConfirmShipMaster.MasterLocationID;
                        objConfirmShipMaster.CurrencyID = voConfirmShipMaster.CurrencyID;
                        objConfirmShipMaster.ExchangeRate = voConfirmShipMaster.ExchangeRate;
                        objConfirmShipMaster.ShipCode = voConfirmShipMaster.ShipCode;
                        objConfirmShipMaster.FromPort = voConfirmShipMaster.FromPort;
                        objConfirmShipMaster.CNo = voConfirmShipMaster.CNo;
                        objConfirmShipMaster.Measurement = voConfirmShipMaster.Measurement;
                        objConfirmShipMaster.GrossWeight = voConfirmShipMaster.GrossWeight;
                        objConfirmShipMaster.NetWeight = voConfirmShipMaster.NetWeight;
                        objConfirmShipMaster.IssuingBank = voConfirmShipMaster.IssuingBank;
                        objConfirmShipMaster.LCNo = voConfirmShipMaster.LCNo;
                        objConfirmShipMaster.VesselName = voConfirmShipMaster.VesselName;
                        objConfirmShipMaster.Comment = voConfirmShipMaster.Comment;
                        objConfirmShipMaster.ReferenceNo = voConfirmShipMaster.ReferenceNo;
                        objConfirmShipMaster.InvoiceNo = voConfirmShipMaster.InvoiceNo;
                        objConfirmShipMaster.PONumber = voConfirmShipMaster.PONumber;
                        if (voConfirmShipMaster.LCDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.LCDate = voConfirmShipMaster.LCDate;
                        }
                        if (voConfirmShipMaster.OnBoardDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.OnBoardDate = voConfirmShipMaster.OnBoardDate;
                        }
                        if (voConfirmShipMaster.InvoiceDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.InvoiceDate = voConfirmShipMaster.InvoiceDate;
                        }
                        objConfirmShipMaster.CCNID = voConfirmShipMaster.CCNID;
                        objConfirmShipMaster.BinID = voConfirmShipMaster.BinId;
                        objConfirmShipMaster.LocationID = voConfirmShipMaster.LocationId;
                        objConfirmShipMaster.UserName = SystemProperty.UserName;
                        objConfirmShipMaster.LastChange = serverDate;
                        db.SubmitChanges();
                    }
                    else
                    {
                        objConfirmShipMaster = new SO_ConfirmShipMaster
                                                   {
                                                       ConfirmShipNo = voConfirmShipMaster.ConfirmShipNo
                                                   };
                        if (voConfirmShipMaster.ShippedDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.ShippedDate = voConfirmShipMaster.ShippedDate;
                        }
                        objConfirmShipMaster.SaleOrderMasterID = voConfirmShipMaster.SaleOrderMasterID;
                        objConfirmShipMaster.MasterLocationID = voConfirmShipMaster.MasterLocationID;
                        objConfirmShipMaster.CurrencyID = voConfirmShipMaster.CurrencyID;
                        objConfirmShipMaster.ExchangeRate = voConfirmShipMaster.ExchangeRate;
                        objConfirmShipMaster.ShipCode = voConfirmShipMaster.ShipCode;
                        objConfirmShipMaster.FromPort = voConfirmShipMaster.FromPort;
                        objConfirmShipMaster.CNo = voConfirmShipMaster.CNo;
                        objConfirmShipMaster.Measurement = voConfirmShipMaster.Measurement;
                        objConfirmShipMaster.GrossWeight = voConfirmShipMaster.GrossWeight;
                        objConfirmShipMaster.NetWeight = voConfirmShipMaster.NetWeight;
                        objConfirmShipMaster.IssuingBank = voConfirmShipMaster.IssuingBank;
                        objConfirmShipMaster.LCNo = voConfirmShipMaster.LCNo;
                        objConfirmShipMaster.VesselName = voConfirmShipMaster.VesselName;
                        objConfirmShipMaster.Comment = voConfirmShipMaster.Comment;
                        objConfirmShipMaster.ReferenceNo = voConfirmShipMaster.ReferenceNo;
                        objConfirmShipMaster.InvoiceNo = voConfirmShipMaster.InvoiceNo;
                        if (voConfirmShipMaster.LCDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.LCDate = voConfirmShipMaster.LCDate;
                        }
                        if (voConfirmShipMaster.OnBoardDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.OnBoardDate = voConfirmShipMaster.OnBoardDate;
                        }
                        if (voConfirmShipMaster.InvoiceDate != DateTime.MinValue)
                        {
                            objConfirmShipMaster.InvoiceDate = voConfirmShipMaster.InvoiceDate;
                        }
                        objConfirmShipMaster.CCNID = voConfirmShipMaster.CCNID;
                        objConfirmShipMaster.BinID = voConfirmShipMaster.BinId;
                        objConfirmShipMaster.LocationID = voConfirmShipMaster.LocationId;
                        objConfirmShipMaster.UserName = SystemProperty.UserName;
                        objConfirmShipMaster.LastChange = serverDate;
                        db.SO_ConfirmShipMasters.InsertOnSubmit(objConfirmShipMaster);
                        db.SubmitChanges();
                    }
                    #endregion

                    #region remove old transaction history

                    db.MST_TransactionHistories.DeleteAllOnSubmit(db.MST_TransactionHistories.Where(t => t.TranTypeID == 22
                        && t.RefMasterID == objConfirmShipMaster.ConfirmShipMasterID));
                    db.SubmitChanges();

                    #endregion

                    #region update cache for remove item

                    foreach (var removeId in removeIds)
                    {
                        var detail = db.SO_ConfirmShipDetails.FirstOrDefault(d => d.ConfirmShipDetailID == removeId);
                        if (detail != null)
                        {
                            var removebinId = objConfirmShipMaster.BinID;
                            var removelocationId = objConfirmShipMaster.LocationID;
                            var removemasterLocationId = objConfirmShipMaster.MasterLocationID;
                            var removeccnid = objConfirmShipMaster.CCNID;

                            #region Update add from Bin cache
                            if (removebinId > 0)
                            {

                                var objBins = db.IV_BinCaches.FirstOrDefault(e => e.BinID == removebinId && e.CCNID == removeccnid && e.MasterLocationID == removemasterLocationId && e.ProductID == detail.ProductID && e.LocationID == removelocationId);
                                if (objBins != null)
                                {
                                    objBins.OHQuantity = objBins.OHQuantity + detail.InvoiceQty;
                                }
                                else
                                {
                                    objBins = new IV_BinCache
                                    {
                                        OHQuantity = detail.InvoiceQty,
                                        BinID = removebinId.GetValueOrDefault(0),
                                        CCNID = removeccnid,
                                        MasterLocationID = removemasterLocationId,
                                        LocationID = removelocationId.GetValueOrDefault(0),
                                        ProductID = detail.ProductID
                                    };
                                    db.IV_BinCaches.InsertOnSubmit(objBins);
                                }
                            }
                            #endregion

                            #region Update Location cache
                            if (removelocationId > 0)
                            {
                                var locationCache = db.IV_LocationCaches.FirstOrDefault(e => e.CCNID == removeccnid && e.MasterLocationID == removemasterLocationId && e.ProductID == detail.ProductID && e.LocationID == removelocationId);
                                if (locationCache != null)
                                {
                                    locationCache.OHQuantity = locationCache.OHQuantity + detail.InvoiceQty;
                                }
                                else
                                {
                                    locationCache = new IV_LocationCache
                                    {
                                        OHQuantity = detail.InvoiceQty,
                                        CCNID = removeccnid,
                                        MasterLocationID = removemasterLocationId,
                                        LocationID = removelocationId.GetValueOrDefault(0),
                                        ProductID = detail.ProductID
                                    };
                                    db.IV_LocationCaches.InsertOnSubmit(locationCache);
                                }
                            }
                            #endregion

                            #region Update Master Location cache
                            if (removemasterLocationId > 0)
                            {
                                var masLocCache = db.IV_MasLocCaches.FirstOrDefault(e => e.CCNID == removeccnid && e.MasterLocationID == removemasterLocationId && e.ProductID == detail.ProductID);
                                if (masLocCache != null)
                                {
                                    masLocCache.OHQuantity = masLocCache.OHQuantity + detail.InvoiceQty;
                                }
                                else
                                {
                                    masLocCache = new IV_MasLocCache
                                    {
                                        OHQuantity = detail.InvoiceQty,
                                        CCNID = removeccnid,
                                        MasterLocationID = removemasterLocationId,
                                        ProductID = detail.ProductID
                                    };
                                    db.IV_MasLocCaches.InsertOnSubmit(masLocCache);
                                }
                            }
                            #endregion

                            #region Update MST_TransactionHistory

                            var voMstTransactionHistory = new MST_TransactionHistory
                            {
                                CCNID = objConfirmShipMaster.CCNID,
                                PostDate = objConfirmShipMaster.InvoiceDate,
                                MasterLocationID = objConfirmShipMaster.MasterLocationID,
                                LocationID = objConfirmShipMaster.LocationID,
                                BinID = objConfirmShipMaster.BinID != 0
                                            ? objConfirmShipMaster.BinID
                                            : null,
                                RefMasterID = objConfirmShipMaster.ConfirmShipMasterID,
                                RefDetailID = detail.ConfirmShipDetailID,
                                ProductID = detail.ProductID,
                                StockUMID = null,
                                Quantity = detail.InvoiceQty,
                                TransDate = serverDate,
                                TranTypeID = 27,
                                UserName = SystemProperty.UserName
                            };

                            db.MST_TransactionHistories.InsertOnSubmit(voMstTransactionHistory);

                            #endregion

                            // remove the detail
                            db.SO_ConfirmShipDetails.DeleteOnSubmit(detail);
                        }
                    }

                    #endregion

                    #region Update SO_ConfirmShipDetail

                    if (pdstData != null && pdstData.Tables.Count > 0)
                    {
                        foreach (DataRow dr in pdstData.Tables[0].Rows)
                        {
                            if (dr.RowState == DataRowState.Deleted)
                            {
                                continue;
                            }

                            var isNew = false;

                            var objShipDetail = db.SO_ConfirmShipDetails.FirstOrDefault(d => d.ConfirmShipDetailID == Convert.ToInt32(dr[SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD]));
                            if (objShipDetail == null)
                            {
                                isNew = true;
                                objShipDetail = new SO_ConfirmShipDetail
                                {
                                    ConfirmShipMasterID = objConfirmShipMaster.ConfirmShipMasterID,
                                };
                            }
                            objShipDetail.PONumber = dr["PONumber"].ToString();
                            if (dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                            {
                                objShipDetail.DeliveryScheduleID = Convert.ToInt32(dr[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD]);
                            }
                            if (dr[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
                            {
                                objShipDetail.ProductID = Convert.ToInt32(dr[ITM_ProductTable.PRODUCTID_FLD]);
                            }
                            if (dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] != DBNull.Value)
                            {
                                objShipDetail.SaleOrderDetailID = Convert.ToInt32(dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.PRICE_FLD] != DBNull.Value)
                            {
                                objShipDetail.Price = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.PRICE_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD] != DBNull.Value)
                            {
                                objShipDetail.InvoiceQty = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.INVOICEQTY_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD] != DBNull.Value)
                            {
                                objShipDetail.NetAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                            {
                                objShipDetail.VATAmount = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATAMOUNT_FLD]);
                            }
                            if (dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD] != DBNull.Value)
                            {
                                objShipDetail.VATPercent = Convert.ToDecimal(dr[SO_ConfirmShipDetailTable.VATPERCENT_FLD]);
                            }
                            if (isNew)
                            {
                                db.SO_ConfirmShipDetails.InsertOnSubmit(objShipDetail);
                            }
                        }
                    }
                    db.SubmitChanges();
                    
                    #endregion

                    #region Bin , Location , MasLocation
                    var listData = from objdata in db.SO_ConfirmShipDetails
                                   where objdata.ConfirmShipMasterID == objConfirmShipMaster.ConfirmShipMasterID
                                   select objdata;
                    var binId = objConfirmShipMaster.BinID;
                    var locationId = objConfirmShipMaster.LocationID;
                    var masterLocationId = objConfirmShipMaster.MasterLocationID;
                    var ccnid = objConfirmShipMaster.CCNID;

                    foreach (var drowData in listData)
                    {
                        var dOldInvoiceQty = pdstData.Tables[0].Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted
                            && drowData.ProductID == Convert.ToInt32(row["ProductID"])
                            && row["OldInvoiceQty"] != DBNull.Value).Sum(row => Convert.ToDecimal(row["OldInvoiceQty"]));

                        #region Update subtract from Bin cache
                        if (binId > 0)
                        {

                            var objBins = db.IV_BinCaches.FirstOrDefault(e => e.BinID == binId && e.CCNID == ccnid && e.MasterLocationID == masterLocationId && e.ProductID == drowData.ProductID && e.LocationID == locationId);
                            if (!drowData.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                            {
                                if (objBins == null || objBins.OHQuantity - (drowData.InvoiceQty - dOldInvoiceQty) < 0)
                                {
                                    throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, drowData.ProductID.ToString(), new PCSException());
                                }
                            }
                            if (objBins != null)
                            {
                                objBins.OHQuantity = objBins.OHQuantity - (drowData.InvoiceQty - dOldInvoiceQty);
                            }
                            else
                            {
                                objBins = new IV_BinCache
                                {
                                    OHQuantity = -drowData.InvoiceQty,
                                    BinID = binId.GetValueOrDefault(0),
                                    CCNID = ccnid,
                                    MasterLocationID = masterLocationId,
                                    LocationID = locationId.GetValueOrDefault(0),
                                    ProductID = drowData.ProductID
                                };
                                db.IV_BinCaches.InsertOnSubmit(objBins);
                            }
                        }
                        #endregion

                        #region Update Location cache
                        if (locationId > 0)
                        {
                            var locationCache = db.IV_LocationCaches.FirstOrDefault(e => e.CCNID == ccnid && e.MasterLocationID == masterLocationId && e.ProductID == drowData.ProductID && e.LocationID == locationId);
                            if (!drowData.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                            {
                                if (locationCache == null || locationCache.OHQuantity - (drowData.InvoiceQty - dOldInvoiceQty) < 0)
                                {
                                    throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, drowData.ProductID.ToString(), new PCSException());
                                }
                            }
                            if (locationCache != null)
                            {
                                locationCache.OHQuantity = locationCache.OHQuantity - (drowData.InvoiceQty - dOldInvoiceQty);
                            }
                            else
                            {
                                locationCache = new IV_LocationCache
                                {
                                    OHQuantity = -drowData.InvoiceQty,
                                    CCNID = ccnid,
                                    MasterLocationID = masterLocationId,
                                    LocationID = locationId.GetValueOrDefault(0),
                                    ProductID = drowData.ProductID
                                };
                                db.IV_LocationCaches.InsertOnSubmit(locationCache);
                            }
                        }
                        #endregion

                        #region Update Master Location cache
                        if (masterLocationId > 0)
                        {
                            var masLocCache = db.IV_MasLocCaches.FirstOrDefault(e => e.CCNID == ccnid && e.MasterLocationID == masterLocationId && e.ProductID == drowData.ProductID);
                            if (!drowData.ITM_Product.AllowNegativeQty.GetValueOrDefault(false))
                            {
                                if (masLocCache == null || masLocCache.OHQuantity - (drowData.InvoiceQty - dOldInvoiceQty) < 0)
                                {
                                    throw new PCSBOException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, drowData.ProductID.ToString(), new PCSException());
                                }
                            }
                            if (masLocCache != null)
                            {
                                masLocCache.OHQuantity = masLocCache.OHQuantity - (drowData.InvoiceQty - dOldInvoiceQty);
                            }
                            else
                            {
                                masLocCache = new IV_MasLocCache
                                {
                                    OHQuantity = -drowData.InvoiceQty,
                                    CCNID = ccnid,
                                    MasterLocationID = masterLocationId,
                                    ProductID = drowData.ProductID
                                };
                                db.IV_MasLocCaches.InsertOnSubmit(masLocCache);
                            }
                        }
                        #endregion

                        #region Update MST_TransactionHistory
                        
                        var voMstTransactionHistory = new MST_TransactionHistory
                                                           {
                                                               CCNID = objConfirmShipMaster.CCNID,
                                                               PostDate = objConfirmShipMaster.InvoiceDate,
                                                               MasterLocationID = objConfirmShipMaster.MasterLocationID,
                                                               LocationID = objConfirmShipMaster.LocationID,
                                                               BinID = objConfirmShipMaster.BinID != 0
                                                                           ? objConfirmShipMaster.BinID
                                                                           : null,
                                                               RefMasterID = objConfirmShipMaster.ConfirmShipMasterID,
                                                               RefDetailID = drowData.ConfirmShipDetailID,
                                                               ProductID = drowData.ProductID,
                                                               StockUMID = null,
                                                               Quantity = drowData.InvoiceQty,
                                                               TransDate = serverDate,
                                                               TranTypeID = 22,
                                                               UserName = SystemProperty.UserName
                                                           };

                        db.MST_TransactionHistories.InsertOnSubmit(voMstTransactionHistory);
                        #endregion

                    }
                    #endregion

                    // submit all changes to database
                    db.SubmitChanges();
                }
                trans.Complete();
            }
        }
		public DataSet ListByMaster(int pintMasterId)
		{
			var dsDetail = new SO_ConfirmShipDetailDS();
			return dsDetail.ListByMaster(pintMasterId);
		}
		public DataSet ListNotCommitted()
		{
			var dsCommitInventory = new SO_CommitInventoryDetailDS();
			return dsCommitInventory.ListNotCommitted();
		}
        /// <summary>
        /// Gets the bin cache data.
        /// </summary>
        /// <param name="binId">The bin id.</param>
        /// <returns></returns>
        public DataTable GetBinCacheData(int binId)
        {
            var binDs = new IV_BinCacheDS();
            return binDs.GetCacheData(binId);
        }
	}
}
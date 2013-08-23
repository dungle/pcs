using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComProcurement.Purchase.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PCSComProcurement.Purchase.BO
{
	public class POInvoiceBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.POInvoiceBO";
		public int AddAndReturn(object pvoInvoiceMaster, DataSet pdstDetail)
		{
            const string METHOD_NAME = THIS + ".AddAndReturnID()";
            PO_InvoiceMaster objMaster = new PO_InvoiceMaster();  
			try
			{
                using (var trans = new TransactionScope())
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {

                        #region Insert Master 
                        PO_InvoiceMasterVO objObject = (PO_InvoiceMasterVO)pvoInvoiceMaster;				
				         
				        
                        objMaster.InvoiceNo=objObject.InvoiceNo;
                        objMaster.PostDate=objObject.PostDate;

                        objMaster.ExchangeRate=objObject.ExchangeRate;
                        if (objObject.BLDate != DateTime.MinValue)
                        {
                            objMaster.BLDate = objObject.BLDate;
                        }
				
                        //objMaster.InvoiceNo=objObject.InvoiceNo;
                        //objMaster.PostDate=objObject.PostDate;
                        if (objObject.InformDate != DateTime.MinValue)
                        {
                            objMaster.InformDate = objObject.InformDate;
                        }
                        if (objObject.DeclarationDate != DateTime.MinValue)
                        {
                            objMaster.DeclarationDate = objObject.DeclarationDate;
                        }
					
					    objMaster.BLNumber=objObject.BLNumber;
                        objMaster.TaxInformNumber=objObject.TaxInformNumber;

					    objMaster.TaxDeclarationNumber=objObject.TaxDeclarationNumber;
                        objMaster.TotalInlandAmount=objObject.TotalInlandAmount;
					
					    objMaster.TotalCIPAmount=objObject.TotalCIPAmount;
                        objMaster.TotalCIFAmount=objObject.TotalCIFAmount;

					    objMaster.TotalImportTax=objObject.TotalImportTax;
                        objMaster.TotalBeforeVATAmount=objObject.TotalBeforeVATAmount;

                       
                        objMaster.TotalVATAmount=objObject.TotalVATAmount;
                        objMaster.CCNID=objObject.CCNID;

					    objMaster.PartyID =objObject.PartyID;
                        objMaster.CurrencyID=objObject.CurrencyID;
                        if (objObject.CarrierID > 0)
                        {
                            objMaster.CarrierID = objObject.CarrierID;
                        }
                        if (objObject.PaymentTermID > 0)
                        {
                            objMaster.PaymentTermID = objObject.PaymentTermID;
                        }
                        if (objObject.DeliveryTermID > 0)
                        {
                            objMaster.DeliveryTermID = objObject.DeliveryTermID;
                        }
                        db.PO_InvoiceMasters.InsertOnSubmit(objMaster);
                        db.SubmitChanges();
                        #endregion
                        foreach (DataRow objRow in pdstDetail.Tables[0].Rows)
                        {
                            if (objRow.RowState == DataRowState.Deleted) continue;
                            objRow[PO_InvoiceDetailTable.INVOICEMASTERID_FLD] = objMaster.InvoiceMasterID;
                        }

                        #region Insert Detail
                        List<PO_InvoiceDetail>listDetail=new List<PO_InvoiceDetail>();
                        //PO_InvoiceDetailDS dsDetail = new PO_InvoiceDetailDS();
                        //dsDetail.UpdateDataSet(pdstDetail);
                        if(pdstDetail!=null && pdstDetail.Tables.Count>0)
                        {
                            foreach (DataRow dr in pdstDetail.Tables[0].Rows)
                            {
                                try
                                {
                                    PO_InvoiceDetail objDetail = new PO_InvoiceDetail();

                                    objDetail.InvoiceDetailID = 0;
                                    if (dr[PO_InvoiceDetailTable.INVOICELINE_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceLine = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICELINE_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEMASTERID_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceMasterID = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICEMASTERID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceQuantity = Convert.ToDecimal(dr[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.UNITPRICE_FLD] != DBNull.Value)
                                    {
                                        objDetail.UnitPrice = Convert.ToDecimal(dr[PO_InvoiceDetailTable.UNITPRICE_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.VAT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VAT = Convert.ToDouble(dr[PO_InvoiceDetailTable.VAT_FLD]);
                                    }
                                    //

                                    if (dr[PO_InvoiceDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VATAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.VATAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.IMPORTTAX_FLD] != DBNull.Value)
                                    {
                                        objDetail.ImportTax = Convert.ToDouble(dr[PO_InvoiceDetailTable.IMPORTTAX_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.ImportTaxAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INLAND_FLD] != DBNull.Value)
                                    {
                                        objDetail.Inland = Convert.ToDecimal(dr[PO_InvoiceDetailTable.INLAND_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.BeforeVATAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.CIFAMOUNT_FLD] != DBNull.Value)
                                    {   //
                                        objDetail.CIFAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.CIFAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.CIPAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.CIPAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.CIPAMOUNT_FLD]);
                                    }

                                    objDetail.Note = dr[PO_InvoiceDetailTable.NOTE_FLD].ToString();
                                    if (dr[PO_InvoiceDetailTable.PRODUCTID_FLD] != DBNull.Value)
                                    {
                                        objDetail.ProductID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PRODUCTID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
                                    {
                                        objDetail.PurchaseOrderMasterID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD] != DBNull.Value)
                                    {
                                        objDetail.PurchaseOrderDetailID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                                    {
                                        objDetail.DeliveryScheduleID = Convert.ToInt32(dr[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEUMID_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceUMID = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICEUMID_FLD]);
                                    }
                                    listDetail.Add(objDetail);
                                }
                                catch
                                { }
                            }
                            db.PO_InvoiceDetails.InsertAllOnSubmit(listDetail);
                            db.SubmitChanges();
                            
                        }
                       
                        #endregion
                        trans.Complete();
                    }
                   
                }
				
                return objMaster.InvoiceMasterID;
			}
            catch (PCSBOException ex)
            {
                if (ex.mCode == ErrorCode.SQLDUPLICATE_KEYCODE)
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                    throw new PCSDBException(ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE, METHOD_NAME, ex);
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
		}

	
		/// <summary>
		/// Delete record by condition
		/// </summary>
		public void Delete(object pobjMasterVO, DataSet pdtsDetail)
		{
            const string METHOD_NAME = THIS + ".Delete()";
			try
			{
                 var objObject = (PO_InvoiceMasterVO)pobjMasterVO;
                 using (var trans = new TransactionScope())
                 {
                     using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                     {
                         var listData = from objdata in db.PO_InvoiceDetails
                                        where objdata.InvoiceMasterID == objObject.InvoiceMasterID
                                        select objdata;
                         if (listData.ToArray().Length > 0)
                         {
                             db.PO_InvoiceDetails.DeleteAllOnSubmit(listData.ToList());
                             db.SubmitChanges();
                         }
                         var objMaster = db.PO_InvoiceMasters.SingleOrDefault(e => e.InvoiceMasterID == objObject.InvoiceMasterID);
                       
                         if (objMaster != null)
                         {
                             db.PO_InvoiceMasters.DeleteOnSubmit(objMaster);
                             db.SubmitChanges();
                         }
                     }
                     trans.Complete();
                 }
								
			}
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
		}

	
		public object GetObjectVO(int pintID, string VOclass)
		{
            return (new PO_InvoiceMasterDS()).GetObjectVO(pintID);
		}

	
		public object GetObjectVO(string pstrInvoiceNo)
		{
            return (new PO_InvoiceMasterDS()).GetObjectVO(pstrInvoiceNo);
		}
		
		
		/// <summary>
		/// CheckIfInvoiceHasBeenReceipt
		/// </summary>
		/// <param name="pintInvoiceDetailID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, September 28 2006</date>
	
		public bool CheckIfInvoiceHasBeenReceipt(int pintInvoiceDetailID)
		{
			PO_PurchaseOrderReceiptDetailDS dsPurchaseOrderReceiptDetail = new PO_PurchaseOrderReceiptDetailDS();
			return dsPurchaseOrderReceiptDetail.IsInvoiceHasBeenReceipt(pintInvoiceDetailID);
		}
	
		public DataTable SelectPO4Invoice(Hashtable mhtbCondition)
		{
            return (new PO_InvoiceMasterDS()).SelectPO4Invoice(mhtbCondition);
		}

	
		public DateTime GetEarliestApprovedDate(string pstrPODetailIDs)
		{
            return (new PO_InvoiceMasterDS()).GetEarliestApprovedDate(pstrPODetailIDs);
		}

		/// <summary>
		/// GetDetailByMaster into Database
		/// </summary>
	
		public DataSet GetDetailByMaster(int pintMasterId)
		{
            return (new PO_InvoiceDetailDS()).GetDetailByMaster(pintMasterId);
		}

        /// <summary>
        /// Update into Database
        /// </summary>
        /// <param name="pobjMaster">The pobj master.</param>
        /// <param name="pdstDetail">The PDST detail.</param>
        /// <param name="removedId">The removed id.</param>
	    public void Update(object pobjMaster, DataSet pdstDetail, List<int> removedId)
		{
            const string METHOD_NAME = THIS + ".Update()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        var objObject = (PO_InvoiceMasterVO)pobjMaster;
                        var objMaster = db.PO_InvoiceMasters.SingleOrDefault(e => e.InvoiceMasterID == objObject.InvoiceMasterID);

                        #region update master object

                        if (objMaster != null)
                        {
                            objMaster.InvoiceNo = objObject.InvoiceNo;
                            objMaster.PostDate = objObject.PostDate;

                            objMaster.ExchangeRate = objObject.ExchangeRate;
                            if (objObject.BLDate != DateTime.MinValue)
                            {
                                objMaster.BLDate = objObject.BLDate;
                            }

                            if (objObject.InformDate != DateTime.MinValue)
                            {
                                objMaster.InformDate = objObject.InformDate;
                            }
                            if (objObject.DeclarationDate != DateTime.MinValue)
                            {
                                objMaster.DeclarationDate = objObject.DeclarationDate;
                            }

                            objMaster.BLNumber = objObject.BLNumber;
                            objMaster.TaxInformNumber = objObject.TaxInformNumber;

                            objMaster.TaxDeclarationNumber = objObject.TaxDeclarationNumber;
                            objMaster.TotalInlandAmount = objObject.TotalInlandAmount;

                            objMaster.TotalCIPAmount = objObject.TotalCIPAmount;
                            objMaster.TotalCIFAmount = objObject.TotalCIFAmount;

                            objMaster.TotalImportTax = objObject.TotalImportTax;
                            objMaster.TotalBeforeVATAmount = objObject.TotalBeforeVATAmount;


                            objMaster.TotalVATAmount = objObject.TotalVATAmount;
                            objMaster.CCNID = objObject.CCNID;

                            objMaster.PartyID = objObject.PartyID;
                            objMaster.CurrencyID = objObject.CurrencyID;
                            if (objObject.CarrierID > 0)
                            {
                                objMaster.CarrierID = objObject.CarrierID;
                            }
                            if (objObject.PaymentTermID > 0)
                            {
                                objMaster.PaymentTermID = objObject.PaymentTermID;
                            }
                            if (objObject.DeliveryTermID > 0)
                            {
                                objMaster.DeliveryTermID = objObject.DeliveryTermID;
                            }

                            db.SubmitChanges();
                        }

                        #endregion

                        #region Delete details

                        foreach (var detailId in removedId)
                        {
                            var detail = db.PO_InvoiceDetails.FirstOrDefault(d => d.InvoiceDetailID == detailId);
                            if (detail == null)
                            {
                                continue;
                            }

                            db.PO_InvoiceDetails.DeleteOnSubmit(detail);
                        }

                        #endregion

                        #region Update detail

                        if (pdstDetail != null)
                        {
                            foreach (DataRow dr in pdstDetail.Tables[0].Rows.Cast<DataRow>().Where(row => row.RowState != DataRowState.Deleted))
                            {
                                var invoiceDetailId = 0;
                                if (dr[PO_InvoiceDetailTable.INVOICEDETAILID_FLD] != DBNull.Value)
                                {
                                    invoiceDetailId = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICEDETAILID_FLD]);
                                }
                                var objDetail = db.PO_InvoiceDetails.SingleOrDefault(e => e.InvoiceDetailID == invoiceDetailId);
                                if (objDetail != null)
                                {
                                    #region update detail

                                    if (dr[PO_InvoiceDetailTable.INVOICELINE_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceLine = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICELINE_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceQuantity = Convert.ToDecimal(dr[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.UNITPRICE_FLD] != DBNull.Value)
                                    {
                                        objDetail.UnitPrice = Convert.ToDecimal(dr[PO_InvoiceDetailTable.UNITPRICE_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.VAT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VAT = Convert.ToDouble(dr[PO_InvoiceDetailTable.VAT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VATAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.VATAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.IMPORTTAX_FLD] != DBNull.Value)
                                    {
                                        objDetail.ImportTax = Convert.ToDouble(dr[PO_InvoiceDetailTable.IMPORTTAX_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.ImportTaxAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INLAND_FLD] != DBNull.Value)
                                    {
                                        objDetail.Inland = Convert.ToDecimal(dr[PO_InvoiceDetailTable.INLAND_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.BeforeVATAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.CIFAMOUNT_FLD] != DBNull.Value)
                                    {   //
                                        objDetail.CIFAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.CIFAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.CIPAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.CIPAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.CIPAMOUNT_FLD]);
                                    }
                                    objDetail.Note = dr[PO_InvoiceDetailTable.NOTE_FLD].ToString();
                                    if (dr[PO_InvoiceDetailTable.PRODUCTID_FLD] != DBNull.Value)
                                    {
                                        objDetail.ProductID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PRODUCTID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
                                    {
                                        objDetail.PurchaseOrderMasterID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD] != DBNull.Value)
                                    {
                                        objDetail.PurchaseOrderDetailID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                                    {
                                        objDetail.DeliveryScheduleID = Convert.ToInt32(dr[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEUMID_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceUMID = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICEUMID_FLD]);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region create new detail

                                    objDetail = new PO_InvoiceDetail { InvoiceMasterID = objMaster.InvoiceMasterID };
                                    if (dr[PO_InvoiceDetailTable.INVOICELINE_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceLine = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICELINE_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceQuantity = Convert.ToDecimal(dr[PO_InvoiceDetailTable.INVOICEQUANTITY_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.UNITPRICE_FLD] != DBNull.Value)
                                    {
                                        objDetail.UnitPrice = Convert.ToDecimal(dr[PO_InvoiceDetailTable.UNITPRICE_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.VAT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VAT = Convert.ToDouble(dr[PO_InvoiceDetailTable.VAT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.VATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.VATAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.VATAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.IMPORTTAX_FLD] != DBNull.Value)
                                    {
                                        objDetail.ImportTax = Convert.ToDouble(dr[PO_InvoiceDetailTable.IMPORTTAX_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.ImportTaxAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.IMPORTTAXAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INLAND_FLD] != DBNull.Value)
                                    {
                                        objDetail.Inland = Convert.ToDecimal(dr[PO_InvoiceDetailTable.INLAND_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.BeforeVATAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.BEFOREVATAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.CIFAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.CIFAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.CIFAMOUNT_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.CIPAMOUNT_FLD] != DBNull.Value)
                                    {
                                        objDetail.CIPAmount = Convert.ToDecimal(dr[PO_InvoiceDetailTable.CIPAMOUNT_FLD]);
                                    }
                                    objDetail.Note = dr[PO_InvoiceDetailTable.NOTE_FLD].ToString();
                                    if (dr[PO_InvoiceDetailTable.PRODUCTID_FLD] != DBNull.Value)
                                    {
                                        objDetail.ProductID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PRODUCTID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD] != DBNull.Value)
                                    {
                                        objDetail.PurchaseOrderMasterID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PURCHASEORDERMASTERID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD] != DBNull.Value)
                                    {
                                        objDetail.PurchaseOrderDetailID = Convert.ToInt32(dr[PO_InvoiceDetailTable.PURCHASEORDERDETAILID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD] != DBNull.Value)
                                    {
                                        objDetail.DeliveryScheduleID = Convert.ToInt32(dr[PO_InvoiceDetailTable.DELIVERYSCHEDULEID_FLD]);
                                    }
                                    if (dr[PO_InvoiceDetailTable.INVOICEUMID_FLD] != DBNull.Value)
                                    {
                                        objDetail.InvoiceUMID = Convert.ToInt32(dr[PO_InvoiceDetailTable.INVOICEUMID_FLD]);
                                    }
                                    db.PO_InvoiceDetails.InsertOnSubmit(objDetail);

                                    #endregion
                                }
                            }
                        }
                        #endregion

                        db.SubmitChanges();
                        trans.Complete();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
		}
		
	
		public object GetExchangeRate(int pintCurrencyID, DateTime pdtmOrderDate)
		{
            return (new MST_ExchangeRateDS()).GetExchangeRate(pintCurrencyID, pdtmOrderDate);
		}
	}
}
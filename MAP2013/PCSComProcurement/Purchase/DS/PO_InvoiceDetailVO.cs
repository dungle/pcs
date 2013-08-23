using System;
using System.Data;

namespace PCSComProcurement.Purchase.DS
{
	[Serializable]
	public class PO_InvoiceDetailVO
	{
		private int mInvoiceDetailID;
		private int mInvoiceLine;
		private int mInvoiceMasterID;
		private Decimal mInvoiceQuantity;
		private Decimal mUnitPrice;
		private float mVAT;
		private Decimal mVATAmount;
		private float mImportTax;
		private Decimal mImportTaxAmount;
		private Decimal mInland;
		private Decimal mBeforeVATAmount;
		private Decimal mCIFAmount;
		private Decimal mCIPAmount;
		private string mNote;
		private int mProductID;
		private int mPurchaseOrderMasterID;
		private int mPurchaseOrderDetailID;
		private int mDeliveryScheduleID;
		private int mInvoiceUMID;

		public int InvoiceDetailID
		{
			set { mInvoiceDetailID = value; }
			get { return mInvoiceDetailID; }
		}
		public int InvoiceLine
		{
			set { mInvoiceLine = value; }
			get { return mInvoiceLine; }
		}
		public int InvoiceMasterID
		{
			set { mInvoiceMasterID = value; }
			get { return mInvoiceMasterID; }
		}
		public Decimal InvoiceQuantity
		{
			set { mInvoiceQuantity = value; }
			get { return mInvoiceQuantity; }
		}
		public Decimal UnitPrice
		{
			set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public float VAT
		{
			set { mVAT = value; }
			get { return mVAT; }
		}
		public Decimal VATAmount
		{
			set { mVATAmount = value; }
			get { return mVATAmount; }
		}
		public float ImportTax
		{
			set { mImportTax = value; }
			get { return mImportTax; }
		}
		public Decimal ImportTaxAmount
		{
			set { mImportTaxAmount = value; }
			get { return mImportTaxAmount; }
		}
		public Decimal Inland
		{
			set { mInland = value; }
			get { return mInland; }
		}
		public Decimal BeforeVATAmount
		{
			set { mBeforeVATAmount = value; }
			get { return mBeforeVATAmount; }
		}
		public Decimal CIFAmount
		{
			set { mCIFAmount = value; }
			get { return mCIFAmount; }
		}
		public Decimal CIPAmount
		{
			set { mCIPAmount = value; }
			get { return mCIPAmount; }
		}
		public string Note
		{
			set { mNote = value; }
			get { return mNote; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int PurchaseOrderMasterID
		{
			set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}
		public int PurchaseOrderDetailID
		{
			set { mPurchaseOrderDetailID = value; }
			get { return mPurchaseOrderDetailID; }
		}
		public int DeliveryScheduleID
		{
			set { mDeliveryScheduleID = value; }
			get { return mDeliveryScheduleID; }
		}
		public int InvoiceUMID
		{
			set { mInvoiceUMID = value; }
			get { return mInvoiceUMID; }
		}
	}
}

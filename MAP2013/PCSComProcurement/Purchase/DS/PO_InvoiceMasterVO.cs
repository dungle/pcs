using System;
using System.Data;

namespace PCSComProcurement.Purchase.DS
{
	[Serializable]
	public class PO_InvoiceMasterVO
	{
		const int SCALE_VALUE = 2;

		private int mInvoiceMasterID;
		private string mInvoiceNo;
		private DateTime mPostDate;
		private Decimal mExchangeRate;
		private DateTime mBLDate;
		private DateTime mInformDate;
		private DateTime mDeclarationDate;
		private string mBLNumber;
		private string mTaxInformNumber;
		private string mTaxDeclarationNumber;
		private Decimal mTotalInlandAmount;
		private Decimal mTotalCIPAmount;
		private Decimal mTotalCIFAmount;
		private Decimal mTotalImportTax;
		private Decimal mTotalBeforeVATAmount;
		private Decimal mTotalVATAmount;
		private int mCCNID;
		private int mPartyID;
		private int mCurrencyID;
		private int mCarrierID;
		private int mPaymentTermID;
		private int mDeliveryTermID;

		public int InvoiceMasterID
		{
			set { mInvoiceMasterID = value; }
			get { return mInvoiceMasterID; }
		}
		public string InvoiceNo
		{
			set { mInvoiceNo = value; }
			get { return mInvoiceNo; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public Decimal ExchangeRate
		{
			set { mExchangeRate = Math.Round(value, SCALE_VALUE); }
			get { return mExchangeRate; }
		}
		public DateTime BLDate
		{
			set { mBLDate = value; }
			get { return mBLDate; }
		}
		public DateTime InformDate
		{
			set { mInformDate = value; }
			get { return mInformDate; }
		}
		public DateTime DeclarationDate
		{
			set { mDeclarationDate = value; }
			get { return mDeclarationDate; }
		}
		public string BLNumber
		{
			set { mBLNumber = value; }
			get { return mBLNumber; }
		}
		public string TaxInformNumber
		{
			set { mTaxInformNumber = value; }
			get { return mTaxInformNumber; }
		}
		public string TaxDeclarationNumber
		{
			set { mTaxDeclarationNumber = value; }
			get { return mTaxDeclarationNumber; }
		}
		public Decimal TotalInlandAmount
		{
			set { mTotalInlandAmount = Math.Round(value, SCALE_VALUE); }
			get { return mTotalInlandAmount; }
		}
		public Decimal TotalCIPAmount
		{
			set { mTotalCIPAmount = Math.Round(value, SCALE_VALUE); }
			get { return mTotalCIPAmount; }
		}
		public Decimal TotalCIFAmount
		{
			set { mTotalCIFAmount = Math.Round(value, SCALE_VALUE); }
			get { return mTotalCIFAmount; }
		}
		public Decimal TotalImportTax
		{
			set { mTotalImportTax = Math.Round(value, SCALE_VALUE); }
			get { return mTotalImportTax; }
		}
		public Decimal TotalBeforeVATAmount
		{
			set { mTotalBeforeVATAmount = Math.Round(value, SCALE_VALUE); }
			get { return mTotalBeforeVATAmount; }
		}
		public Decimal TotalVATAmount
		{
			set { mTotalVATAmount = Math.Round(value, SCALE_VALUE); }
			get { return mTotalVATAmount; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public int CarrierID
		{
			set { mCarrierID = value; }
			get { return mCarrierID; }
		}
		public int PaymentTermID
		{
			set { mPaymentTermID = value; }
			get { return mPaymentTermID; }
		}
		public int DeliveryTermID
		{
			set { mDeliveryTermID = value; }
			get { return mDeliveryTermID; }
		}
	}
}

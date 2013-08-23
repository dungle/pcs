using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PurchaseRequisitionMasterVO
	{
		private int	mPurchaseRequisitionMasterID;
		private DateTime	mOrderDate;
		private DateTime	mDeliveryDate;
		private int	mVAT;
		private int	mImportTax;
		private int	mSpecialTax;
		private string	mCode;
		private int	mCCNID;
		private int	mRequestorID;
		private int	mApproverID;
		private int	mExchangeRateID;
		private int	mDeliveryTermsID;
		private int	mPaymentTermsID;
		private int	mCarrierID;
		private int	mCurrencyID;
		private Decimal	mTotalImportTax;
		private Decimal	mTotalVAT;
		private Decimal	mTotalSpecialTax;
		private Decimal	mTotalAmount;
		private Decimal	mTotalDiscount;
		private Decimal	mTotalNetAmount;
		private DateTime	mApprovalDate;
		private int	mPartyID;
		private int	mPartyContactID;
		private int	mVendorLocID;
		private int	mShipToLocID;
		private int	mInvToLocID;
		private int	mSource;

		public int	PurchaseRequisitionMasterID
		{	set { mPurchaseRequisitionMasterID = value; }
			get { return mPurchaseRequisitionMasterID; }
		}
		public DateTime	OrderDate
		{	set { mOrderDate = value; }
			get { return mOrderDate; }
		}
		public DateTime	DeliveryDate
		{	set { mDeliveryDate = value; }
			get { return mDeliveryDate; }
		}
		public int	VAT
		{	set { mVAT = value; }
			get { return mVAT; }
		}
		public int	ImportTax
		{	set { mImportTax = value; }
			get { return mImportTax; }
		}
		public int	SpecialTax
		{	set { mSpecialTax = value; }
			get { return mSpecialTax; }
		}
		public string	Code
		{	set { mCode = value; }
			get { return mCode; }
		}
		public int	CCNID
		{	set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int	RequestorID
		{	set { mRequestorID = value; }
			get { return mRequestorID; }
		}
		public int	ApproverID
		{	set { mApproverID = value; }
			get { return mApproverID; }
		}
		public int	ExchangeRateID
		{	set { mExchangeRateID = value; }
			get { return mExchangeRateID; }
		}
		public int	DeliveryTermsID
		{	set { mDeliveryTermsID = value; }
			get { return mDeliveryTermsID; }
		}
		public int	PaymentTermsID
		{	set { mPaymentTermsID = value; }
			get { return mPaymentTermsID; }
		}
		public int	CarrierID
		{	set { mCarrierID = value; }
			get { return mCarrierID; }
		}
		public int	CurrencyID
		{	set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public Decimal	TotalImportTax
		{	set { mTotalImportTax = value; }
			get { return mTotalImportTax; }
		}
		public Decimal	TotalVAT
		{	set { mTotalVAT = value; }
			get { return mTotalVAT; }
		}
		public Decimal	TotalSpecialTax
		{	set { mTotalSpecialTax = value; }
			get { return mTotalSpecialTax; }
		}
		public Decimal	TotalAmount
		{	set { mTotalAmount = value; }
			get { return mTotalAmount; }
		}
		public Decimal	TotalDiscount
		{	set { mTotalDiscount = value; }
			get { return mTotalDiscount; }
		}
		public Decimal	TotalNetAmount
		{	set { mTotalNetAmount = value; }
			get { return mTotalNetAmount; }
		}
		public DateTime	ApprovalDate
		{	set { mApprovalDate = value; }
			get { return mApprovalDate; }
		}
		public int	PartyID
		{	set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int	PartyContactID
		{	set { mPartyContactID = value; }
			get { return mPartyContactID; }
		}
		public int	VendorLocID
		{	set { mVendorLocID = value; }
			get { return mVendorLocID; }
		}
		public int	ShipToLocID
		{	set { mShipToLocID = value; }
			get { return mShipToLocID; }
		}
		public int	InvToLocID
		{	set { mInvToLocID = value; }
			get { return mInvToLocID; }
		}
		public int	Source
		{	set { mSource = value; }
			get { return mSource; }
		}
	}
}

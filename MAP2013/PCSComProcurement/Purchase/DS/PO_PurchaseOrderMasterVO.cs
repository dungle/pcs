using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PurchaseOrderMasterVO
	{
		private int	mMasterLocationID;
		private string	mCode;
		private DateTime	mOrderDate;
		private bool	mVAT;
		private bool	mImportTax;
		private bool	mSpecialTax;
		private int	mPurchaseOrderMasterID;
		private int	mCurrencyID;
		private int	mDeliveryTermsID;
		private int	mPaymentTermsID;
		private int	mCarrierID;
		private Decimal	mTotalImportTax;
		private int	mBuyerID;
		private Decimal	mTotalVAT;
		private Decimal	mTotalSpecialTax;
		private Decimal	mTotalAmount;
		private Decimal	mTotalDiscount;
		private Decimal	mTotalNetAmount;
		private int	mCCNID;
		private int	mPartyID;
		private int	mVendorLocID;
		private int	mShipToLocID;
		private int	mInvToLocID;
		private int	mPartyContactID;
		private int	mPurchaseTypeID;
		private int	mDiscountTermID;
		private int	mPauseID;
		private int	mMakerID;
		private int	mPricingTypeID;
		private int	mMakerLocationID;
		private int mRequestDeliveryTime;
		private int	mPriority;
		private bool	mRecCompleted;
		private string	mComment;
		private string	mReferenceNo;
		private int	mPORevision;
		private decimal mExchangeRate;
		private string mVendorSO;

        public string UserName { get; set; }
        public DateTime LastChange { get; set; }

		public int	MasterLocationID
		{	set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public string	Code
		{	set { mCode = value; }
			get { return mCode; }
		}
		public int	PORevision
		{
			set { mPORevision = value; }
			get { return mPORevision; }
		}
		public DateTime	OrderDate
		{	set { mOrderDate = value; }
			get { return mOrderDate; }
		}
		public bool	VAT
		{	set { mVAT = value; }
			get { return mVAT; }
		}
		public bool	ImportTax
		{	set { mImportTax = value; }
			get { return mImportTax; }
		}
		public bool	SpecialTax
		{	set { mSpecialTax = value; }
			get { return mSpecialTax; }
		}
		public int	PurchaseOrderMasterID
		{	set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}
		public int	CurrencyID
		{	set { mCurrencyID = value; }
			get { return mCurrencyID; }
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
		public Decimal	TotalImportTax
		{	set { mTotalImportTax = value; }
			get { return mTotalImportTax; }
		}
		public int	BuyerID
		{	set { mBuyerID = value; }
			get { return mBuyerID; }
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
		public int	CCNID
		{	set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int	PartyID
		{	set { mPartyID = value; }
			get { return mPartyID; }
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
		public int	PartyContactID
		{	set { mPartyContactID = value; }
			get { return mPartyContactID; }
		}
		public int	PurchaseTypeID
		{	set { mPurchaseTypeID = value; }
			get { return mPurchaseTypeID; }
		}
		public int	DiscountTermID
		{	set { mDiscountTermID = value; }
			get { return mDiscountTermID; }
		}
		public int	PauseID
		{	set { mPauseID = value; }
			get { return mPauseID; }
		}
		public int	MakerID
		{
				set { mMakerID = value; }
			get { return mMakerID; }
		}
		public int	MakerLocationID
		{
				set { mMakerLocationID = value; }
			get { return mMakerLocationID; }
		}
		public int	RequestDeliveryTime
		{
				set { mRequestDeliveryTime = value; }
			get { return mRequestDeliveryTime; }
		}
		public int	Priority
		{	set { mPriority = value; }
			get { return mPriority; }
		}
		public bool	RecCompleted
		{	set { mRecCompleted = value; }
			get { return mRecCompleted; }
		}
		public string	Comment
		{	set { mComment = value; }
			get { return mComment; }
		}
		public decimal	ExchangeRate
		{
				set { mExchangeRate = value; }
			get { return mExchangeRate; }
		}
		public string	VendorSO
		{
			set { mVendorSO = value; }
			get { return mVendorSO; }
		}
		public string	ReferenceNo
		{
			set { mReferenceNo = value; }
			get { return mReferenceNo; }
		}
		public int PricingTypeID
		{
			get { return mPricingTypeID; }
			set { mPricingTypeID = value; }
		}
	}
}

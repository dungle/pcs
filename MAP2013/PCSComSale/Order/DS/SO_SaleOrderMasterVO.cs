using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_SaleOrderMasterVO
	{
		private int mSaleOrderMasterID;
		private string mCode;
		private DateTime mTransDate;
		private string mCustomerPurchaseOrderNo;
		private bool mVAT;
		private Decimal mVATRate;
		private bool mExportTax;
		private bool mSpecialTax;
		private Decimal mExportTaxRate;
		private bool mShipCompleted;
		private int mSalesRepresentativeID;
		private Decimal mSpecialTaxRate;
		private int mCCNID;
		private int mCurrencyID;
		private decimal mExchangeRate;
		private int mCarrierID;
		private int mPaymentTermsID;
		private int mDeliveryTermsID;
		private int mDiscountTermsID;
		private int mPauseID;
		private Decimal mTotalVATAmount;
		private Decimal mTotalExportAmount;
		private Decimal mTotalSpecialTaxAmount;
		private Decimal mTotalAmount;
		private Decimal mTotalDiscountAmount;
		private Decimal mTotalNetAmount;
		private int mPaymentMethodID;
		private Decimal mPriority;
		private DateTime mShippedDateTime;
		private int mBuyingLocID;
		private int mShipToLocID;
		private int mShipFromLocID;
		private int mBillToLocID;
		private int mPartyContactID;
		private DateTime mPackedDateTime;
		private int mSaleStatusID;
		private int mCancelReasonID;
		private int mShippedEmployeeID;
		private int mSaleTypeID;
		private int mPartyID;
		private int mLocationID;
		private int mTypeID;

		public int SaleOrderMasterID
		{
			set { mSaleOrderMasterID = value; }
			get { return mSaleOrderMasterID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public DateTime TransDate
		{
			set { mTransDate = value; }
			get { return mTransDate; }
		}
		public string CustomerPurchaseOrderNo
		{
			set { mCustomerPurchaseOrderNo = value; }
			get { return mCustomerPurchaseOrderNo; }
		}
		public bool VAT
		{
			set { mVAT = value; }
			get { return mVAT; }
		}
		public Decimal VATRate
		{
			set { mVATRate = value; }
			get { return mVATRate; }
		}
		public bool ExportTax
		{
			set { mExportTax = value; }
			get { return mExportTax; }
		}
		public bool SpecialTax
		{
			set { mSpecialTax = value; }
			get { return mSpecialTax; }
		}
		public Decimal ExportTaxRate
		{
			set { mExportTaxRate = value; }
			get { return mExportTaxRate; }
		}
		public bool ShipCompleted
		{
			set { mShipCompleted = value; }
			get { return mShipCompleted; }
		}
		public int SalesRepresentativeID
		{
			set { mSalesRepresentativeID = value; }
			get { return mSalesRepresentativeID; }
		}
		public Decimal SpecialTaxRate
		{
			set { mSpecialTaxRate = value; }
			get { return mSpecialTaxRate; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public decimal ExchangeRate
		{
			set { mExchangeRate = value; }
			get { return mExchangeRate; }
		}
		public int CarrierID
		{
			set { mCarrierID = value; }
			get { return mCarrierID; }
		}
		public int PaymentTermsID
		{
			set { mPaymentTermsID = value; }
			get { return mPaymentTermsID; }
		}
		public int DeliveryTermsID
		{
			set { mDeliveryTermsID = value; }
			get { return mDeliveryTermsID; }
		}
		public int DiscountTermsID
		{
			set { mDiscountTermsID = value; }
			get { return mDiscountTermsID; }
		}
		public int PauseID
		{
			set { mPauseID = value; }
			get { return mPauseID; }
		}
		public Decimal TotalVATAmount
		{
			set { mTotalVATAmount = value; }
			get { return mTotalVATAmount; }
		}
		public Decimal TotalExportAmount
		{
			set { mTotalExportAmount = value; }
			get { return mTotalExportAmount; }
		}
		public Decimal TotalSpecialTaxAmount
		{
			set { mTotalSpecialTaxAmount = value; }
			get { return mTotalSpecialTaxAmount; }
		}
		public Decimal TotalAmount
		{
			set { mTotalAmount = value; }
			get { return mTotalAmount; }
		}
		public Decimal TotalDiscountAmount
		{
			set { mTotalDiscountAmount = value; }
			get { return mTotalDiscountAmount; }
		}
		public Decimal TotalNetAmount
		{
			set { mTotalNetAmount = value; }
			get { return mTotalNetAmount; }
		}
		public int PaymentMethodID
		{
			set { mPaymentMethodID = value; }
			get { return mPaymentMethodID; }
		}
		public Decimal Priority
		{
			set { mPriority = value; }
			get { return mPriority; }
		}
		public DateTime ShippedDateTime
		{
			set { mShippedDateTime = value; }
			get { return mShippedDateTime; }
		}
		public int BuyingLocID
		{
			set { mBuyingLocID = value; }
			get { return mBuyingLocID; }
		}
		public int ShipToLocID
		{
			set { mShipToLocID = value; }
			get { return mShipToLocID; }
		}
		public int ShipFromLocID
		{
			set { mShipFromLocID = value; }
			get { return mShipFromLocID; }
		}
		public int BillToLocID
		{
			set { mBillToLocID = value; }
			get { return mBillToLocID; }
		}
		public int PartyContactID
		{
			set { mPartyContactID = value; }
			get { return mPartyContactID; }
		}
		public DateTime PackedDateTime
		{
			set { mPackedDateTime = value; }
			get { return mPackedDateTime; }
		}
		public int SaleStatusID
		{
			set { mSaleStatusID = value; }
			get { return mSaleStatusID; }
		}
		public int CancelReasonID
		{
			set { mCancelReasonID = value; }
			get { return mCancelReasonID; }
		}
		public int ShippedEmployeeID
		{
			set { mShippedEmployeeID = value; }
			get { return mShippedEmployeeID; }
		}
		public int SaleTypeID
		{
			set { mSaleTypeID = value; }
			get { return mSaleTypeID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}

		public int TypeID
		{
			get { return mTypeID; }
			set { mTypeID = value; }
		}
	}
}

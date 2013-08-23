using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PurchaseOrderDetailVO
	{
		private int	mPurchaseOrderDetailID;
		private int	mLine;
		private DateTime	mRequiredDate;
		private bool	mClosed;
		private Decimal	mOrderQuantity;
		private Decimal	mUnitPrice;
		private Decimal	mVATAmount;
		private Decimal	mImportTaxAmount;
		private Decimal	mSpecialTaxAmount;
		private Decimal	mTotalAmount;
		private Decimal	mDiscountAmount;
		private Decimal	mNetAmount;
		private string	mVendorItem;
		private string	mVendorRevision;
		private int	mProductID;
		private int	mStockUMID;
		private Decimal	mTotalDelivery;
		private int	mPurchaseOrderMasterID;
		private int	mBuyingUMID;
		private Decimal	mImportTax;
		private Decimal	mSpecialTax;
		private Decimal	mVAT;
		private bool	mReOpen;
		private int	mApproverID;
		private DateTime	mApprovalDate;
		private decimal	mUMRate;

		public int	PurchaseOrderDetailID
		{	set { mPurchaseOrderDetailID = value; }
			get { return mPurchaseOrderDetailID; }
		}
		public int	Line
		{	set { mLine = value; }
			get { return mLine; }
		}
		public DateTime	RequiredDate
		{	set { mRequiredDate = value; }
			get { return mRequiredDate; }
		}
		public bool	Closed
		{	set { mClosed = value; }
			get { return mClosed; }
		}
		public Decimal	OrderQuantity
		{	set { mOrderQuantity = value; }
			get { return mOrderQuantity; }
		}
		public Decimal	UnitPrice
		{	set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public Decimal	VATAmount
		{	set { mVATAmount = value; }
			get { return mVATAmount; }
		}
		public Decimal	ImportTaxAmount
		{	set { mImportTaxAmount = value; }
			get { return mImportTaxAmount; }
		}
		public Decimal	SpecialTaxAmount
		{	set { mSpecialTaxAmount = value; }
			get { return mSpecialTaxAmount; }
		}
		public Decimal	TotalAmount
		{	set { mTotalAmount = value; }
			get { return mTotalAmount; }
		}
		public Decimal	DiscountAmount
		{	set { mDiscountAmount = value; }
			get { return mDiscountAmount; }
		}
		public Decimal	NetAmount
		{	set { mNetAmount = value; }
			get { return mNetAmount; }
		}
		public string	VendorItem
		{	set { mVendorItem = value; }
			get { return mVendorItem; }
		}
		public string	VendorRevision
		{	set { mVendorRevision = value; }
			get { return mVendorRevision; }
		}
		public int	ProductID
		{	set { mProductID = value; }
			get { return mProductID; }
		}
		public int	StockUMID
		{	set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public Decimal	TotalDelivery
		{	set { mTotalDelivery = value; }
			get { return mTotalDelivery; }
		}
		public int	PurchaseOrderMasterID
		{	set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}
		public int	BuyingUMID
		{	set { mBuyingUMID = value; }
			get { return mBuyingUMID; }
		}
		public Decimal	ImportTax
		{	set { mImportTax = value; }
			get { return mImportTax; }
		}
		public Decimal	SpecialTax
		{	set { mSpecialTax = value; }
			get { return mSpecialTax; }
		}
		public Decimal	VAT
		{	set { mVAT = value; }
			get { return mVAT; }
		}
		public bool	ReOpen
		{	set { mReOpen = value; }
			get { return mReOpen; }
		}
		public int	ApproverID
		{	set { mApproverID = value; }
			get { return mApproverID; }
		}
		public DateTime	ApprovalDate
		{	set { mApprovalDate = value; }
			get { return mApprovalDate; }
		}
		public decimal	UMRate
		{
				set { mUMRate = value; }
			get { return mUMRate; }
		}
	}
}

using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_SaleOrderDetailVO
	{
		private int mSaleOrderDetailID;
		private int mSaleOrderLine;
		private bool mAutoCommit;
		private Decimal mOrderQuantity;
		private Decimal mUnitPrice;
		private Decimal mVATAmount;
		private Decimal mExportTaxAmount;
		private Decimal mSpecialTaxAmount;
		private Decimal mTotalAmount;
		private Decimal mDiscountAmount;
		private Decimal mNetAmount;
		private string mItemCustomerCode;
		private string mItemCustomerRevision;
		private int mCancelReasonID;
		private Decimal mVATPercent;
		private Decimal mExportTaxPercent;
		private Decimal mSpecialTaxPercent;
		private Decimal mUMRate;
		private Decimal mShipQuantity;
		private Decimal mBackOrderQty;
		private Decimal mStockQuantity;
		private int mProductID;
		private Decimal mConvertedQuantity;
		private int mReasonID;
		private int mSaleOrderMasterID;
		private int mStockUMID;
		private int mSellingUMID;

		public int SaleOrderDetailID
		{
			set { mSaleOrderDetailID = value; }
			get { return mSaleOrderDetailID; }
		}
		public int SaleOrderLine
		{
			set { mSaleOrderLine = value; }
			get { return mSaleOrderLine; }
		}
		public bool AutoCommit
		{
			set { mAutoCommit = value; }
			get { return mAutoCommit; }
		}
		public Decimal OrderQuantity
		{
			set { mOrderQuantity = value; }
			get { return mOrderQuantity; }
		}
		public Decimal UnitPrice
		{
			set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public Decimal VATAmount
		{
			set { mVATAmount = value; }
			get { return mVATAmount; }
		}
		public Decimal ExportTaxAmount
		{
			set { mExportTaxAmount = value; }
			get { return mExportTaxAmount; }
		}
		public Decimal SpecialTaxAmount
		{
			set { mSpecialTaxAmount = value; }
			get { return mSpecialTaxAmount; }
		}
		public Decimal TotalAmount
		{
			set { mTotalAmount = value; }
			get { return mTotalAmount; }
		}
		public Decimal DiscountAmount
		{
			set { mDiscountAmount = value; }
			get { return mDiscountAmount; }
		}
		public Decimal NetAmount
		{
			set { mNetAmount = value; }
			get { return mNetAmount; }
		}
		public string ItemCustomerCode
		{
			set { mItemCustomerCode = value; }
			get { return mItemCustomerCode; }
		}
		public string ItemCustomerRevision
		{
			set { mItemCustomerRevision = value; }
			get { return mItemCustomerRevision; }
		}
		public int CancelReasonID
		{
			set { mCancelReasonID = value; }
			get { return mCancelReasonID; }
		}
		public Decimal VATPercent
		{
			set { mVATPercent = value; }
			get { return mVATPercent; }
		}
		public Decimal ExportTaxPercent
		{
			set { mExportTaxPercent = value; }
			get { return mExportTaxPercent; }
		}
		public Decimal SpecialTaxPercent
		{
			set { mSpecialTaxPercent = value; }
			get { return mSpecialTaxPercent; }
		}
		public Decimal UMRate
		{
			set { mUMRate = value; }
			get { return mUMRate; }
		}
		public Decimal ShipQuantity
		{
			set { mShipQuantity = value; }
			get { return mShipQuantity; }
		}
		public Decimal BackOrderQty
		{
			set { mBackOrderQty = value; }
			get { return mBackOrderQty; }
		}
		public Decimal StockQuantity
		{
			set { mStockQuantity = value; }
			get { return mStockQuantity; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public Decimal ConvertedQuantity
		{
			set { mConvertedQuantity = value; }
			get { return mConvertedQuantity; }
		}
		public int ReasonID
		{
			set { mReasonID = value; }
			get { return mReasonID; }
		}
		public int SaleOrderMasterID
		{
			set { mSaleOrderMasterID = value; }
			get { return mSaleOrderMasterID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int SellingUMID
		{
			set { mSellingUMID = value; }
			get { return mSellingUMID; }
		}
	}
}

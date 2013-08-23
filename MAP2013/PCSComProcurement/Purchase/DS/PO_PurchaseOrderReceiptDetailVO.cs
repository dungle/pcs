using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PurchaseOrderReceiptDetailVO
	{
		private int	mBinId;
		private int	mLocationId;
		private int	mPurchaseOrderReceiptDetailID;
		private Decimal	mReceiveQuantity;
		private int	mPurchaseOrderReceiptID;
		private int	mStockUMID;
		private int	mProductID;
		private int	mPurchaseOrderMasterID;
		private int	mBuyingUMID;
		private int	mPurchaseOrderDetailID;
		private decimal mUMRate;
		private string mLot;
		private string mSerial;
		private int mQAStatus;
		private int mDeliveryScheduleID;
		private int mInvoiceDetailID;

		public int DeliveryScheduleID
		{
			get { return mDeliveryScheduleID; }
			set { mDeliveryScheduleID = value; }
		}

		public int	BinId
		{	set { mBinId = value; }
			get { return mBinId; }
		}
		public int	LocationId
		{	set { mLocationId = value; }
			get { return mLocationId; }
		}
		public int	PurchaseOrderReceiptDetailID
		{	set { mPurchaseOrderReceiptDetailID = value; }
			get { return mPurchaseOrderReceiptDetailID; }
		}
		public Decimal	ReceiveQuantity
		{	set { mReceiveQuantity = value; }
			get { return mReceiveQuantity; }
		}
		public int	PurchaseOrderReceiptID
		{	set { mPurchaseOrderReceiptID = value; }
			get { return mPurchaseOrderReceiptID; }
		}
		public int	StockUMID
		{	set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int	ProductID
		{	set { mProductID = value; }
			get { return mProductID; }
		}
		public int	PurchaseOrderMasterID
		{	set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}
		public int	BuyingUMID
		{	set { mBuyingUMID = value; }
			get { return mBuyingUMID; }
		}
		public int	PurchaseOrderDetailID
		{	set { mPurchaseOrderDetailID = value; }
			get { return mPurchaseOrderDetailID; }
		}
		public string Lot
		{
			get { return mLot; }
			set { mLot = value; }
		}

		public string Serial
		{
			get { return mSerial; }
			set { mSerial = value; }
		}

		public int QAStatus
		{
			get { return mQAStatus; }
			set { mQAStatus = value; }
		}

		public decimal UmRate
		{
			get { return mUMRate; }
			set { mUMRate = value; }
		}

		public int InvoiceDetailID
		{
			get { return mInvoiceDetailID; }
			set { mInvoiceDetailID = value; }
		}
	}
}

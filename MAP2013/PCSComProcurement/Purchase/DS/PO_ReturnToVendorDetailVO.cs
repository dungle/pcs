using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_ReturnToVendorDetailVO
	{
		private int mReturnToVendorDetailID;
		private int mLine;
		private Decimal mQuantity;
		private string mLot;
		private string mSerial;
		private int mStockUMID;
		private int mBuyingUMID;
		private int mMRB;
		private int mLocationID;
		private int mBinID;
		private int mReturnToVendorMasterID;
		private int mProductID;
		private Decimal mUMRate;

		public int ReturnToVendorDetailID
		{
			set { mReturnToVendorDetailID = value; }
			get { return mReturnToVendorDetailID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}
		public string Serial
		{
			set { mSerial = value; }
			get { return mSerial; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int BuyingUMID
		{
			set { mBuyingUMID = value; }
			get { return mBuyingUMID; }
		}
		public int MRB
		{
			set { mMRB = value; }
			get { return mMRB; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public int ReturnToVendorMasterID
		{
			set { mReturnToVendorMasterID = value; }
			get { return mReturnToVendorMasterID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public Decimal UMRate
		{
			set { mUMRate = value; }
			get { return mUMRate; }
		}
	}
}

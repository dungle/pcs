using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_ReturnedGoodsDetailVO
	{
		private int mReturnedGoodsDetailID;
		private Decimal mReceiveQuantity;
		private int mUnitID;
		private int mProductID;
		private Decimal mUnitPrice;
		private int mReturnedGoodsMasterID;
		private int mBinID;
		private int mLocationID;
		private int mMasterLocationID;
		private int mQAStatus;
		private string mLot;
		private string mSerial;
		private int mLine;
		private decimal mBalanceQty;

		public int ReturnedGoodsDetailID
		{
			set { mReturnedGoodsDetailID = value; }
			get { return mReturnedGoodsDetailID; }
		}
		public Decimal ReceiveQuantity
		{
			set { mReceiveQuantity = value; }
			get { return mReceiveQuantity; }
		}
		public int UnitID
		{
			set { mUnitID = value; }
			get { return mUnitID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public Decimal UnitPrice
		{
			set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public int ReturnedGoodsMasterID
		{
			set { mReturnedGoodsMasterID = value; }
			get { return mReturnedGoodsMasterID; }
		}
		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int QAStatus
		{
			set { mQAStatus = value; }
			get { return mQAStatus; }
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
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}

		public decimal BalanceQty
		{
			get { return mBalanceQty; }
			set { mBalanceQty = value; }
		}
	}
}

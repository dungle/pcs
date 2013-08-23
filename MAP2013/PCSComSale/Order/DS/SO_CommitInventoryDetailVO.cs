using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_CommitInventoryDetailVO
	{
		private int mMasterLocationID;
		private int mCommitInventoryDetailID;
		private int mLine;
		private int mInspectionStatus;
		private string mLot;
		private Decimal mCommitQuantity;
		private int mCommitInventoryMasterID;
		private int mDeliveryScheduleID;
		private int mProductID;
		private int mBinID;
		private int mLocationID;
		private string mSerial;
		private bool mPacked;
		private Decimal mUMRate;
		private int mSellingUMID;
		private int mStockUMID;
		private Decimal mSTDCost;
		private Decimal mCostOfGoodsSold;
		private bool mShipped;
		private int mCCNID;
		private DateTime mShipDate;

		public DateTime ShipDate
		{
			get { return mShipDate; }
			set { mShipDate = value; }
		}

		public int CCNID
		{
			get { return mCCNID; }
			set { mCCNID = value; }
		}

		public bool Shipped
		{
			get { return mShipped; }
			set { mShipped = value; }
		}

		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int CommitInventoryDetailID
		{
			set { mCommitInventoryDetailID = value; }
			get { return mCommitInventoryDetailID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public int InspectionStatus
		{
			set { mInspectionStatus = value; }
			get { return mInspectionStatus; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}
		public Decimal CommitQuantity
		{
			set { mCommitQuantity = value; }
			get { return mCommitQuantity; }
		}
		public int CommitInventoryMasterID
		{
			set { mCommitInventoryMasterID = value; }
			get { return mCommitInventoryMasterID; }
		}
		public int DeliveryScheduleID
		{
			set { mDeliveryScheduleID = value; }
			get { return mDeliveryScheduleID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
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
		public string Serial
		{
			set { mSerial = value; }
			get { return mSerial; }
		}
		public bool Packed
		{
			set { mPacked = value; }
			get { return mPacked; }
		}
		public Decimal UMRate
		{
			set { mUMRate = value; }
			get { return mUMRate; }
		}
		public int SellingUMID
		{
			set { mSellingUMID = value; }
			get { return mSellingUMID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public Decimal STDCost
		{
			set { mSTDCost = value; }
			get { return mSTDCost; }
		}
		public Decimal CostOfGoodsSold
		{
			set { mCostOfGoodsSold = value; }
			get { return mCostOfGoodsSold; }
		}
	}
}

using System;


namespace PCSComMaterials.Inventory.DS
{
	
	[Serializable]
	public class IV_BinCacheVO
	{
		private int mBinCacheID;
		private Decimal mOHQuantity;
		private Decimal mCommitQuantity;
		private Decimal mDemanQuantity;
		private Decimal mSupplyQuantity;
		private string mLot;
		private int mInspStatus;
		private int mBinID;
		private int mCCNID;
		private int mLocationID;
		private int mMasterLocationID;
		private int mProductID;

		public int BinCacheID
		{
			set { mBinCacheID = value; }
			get { return mBinCacheID; }
		}
		public Decimal OHQuantity
		{
			set { mOHQuantity = value; }
			get { return mOHQuantity; }
		}
		public Decimal CommitQuantity
		{
			set { mCommitQuantity = value; }
			get { return mCommitQuantity; }
		}
		public Decimal DemanQuantity
		{
			set { mDemanQuantity = value; }
			get { return mDemanQuantity; }
		}
		public Decimal SupplyQuantity
		{
			set { mSupplyQuantity = value; }
			get { return mSupplyQuantity; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}
		public int InspStatus
		{
			set { mInspStatus = value; }
			get { return mInspStatus; }
		}
		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
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
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
	}
}

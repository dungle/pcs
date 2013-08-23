using System;


namespace PCSComMaterials.Inventory.DS
{
	
	[Serializable]
	public class IV_LocationCacheVO
	{
		private int mLocationCacheID;
		private Decimal mOHQuantity;
		private Decimal mCommitQuantity;
		private Decimal mDemanQuantity;
		private Decimal mSupplyQuantity;
		private string mLot;
		private int mInspStatus;
		private int mCCNID;
		private int mProductID;
		private int mLocationID;
		private int mMasterLocationID;

		public int LocationCacheID
		{
			set { mLocationCacheID = value; }
			get { return mLocationCacheID; }
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
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
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
	}
}

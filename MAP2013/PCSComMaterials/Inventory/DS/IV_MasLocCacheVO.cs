using System;


namespace PCSComMaterials.Inventory.DS
{
	
	[Serializable]
	public class IV_MasLocCacheVO
	{
		private int mMasLocCacheID;
		private Decimal mOHQuantity;
		private Decimal mCommitQuantity;
		private Decimal mDemanQuantity;
		private Decimal mSupplyQuantity;
		private string mLot;
		private int mInspStatus;
		private int mCCNID;
		private int mMasterLocationID;
		private decimal mAVGCost;
		private Decimal mSummItemCost21;
		private int mProductID;

		public int MasLocCacheID
		{
			set { mMasLocCacheID = value; }
			get { return mMasLocCacheID; }
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
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public decimal AVGCost
		{
			set { mAVGCost = value; }
			get { return mAVGCost; }
		}
		public Decimal SummItemCost21
		{
			set { mSummItemCost21 = value; }
			get { return mSummItemCost21; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
	}
}

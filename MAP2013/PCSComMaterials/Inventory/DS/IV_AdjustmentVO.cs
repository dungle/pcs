using System;
using System.Data;

namespace PCSComMaterials.Inventory.DS
{
	[Serializable]
	public class IV_AdjustmentVO
	{
		private int mBinID;
		private int mMasterLocationID;
		private int mLocationID;
		private int mAdjustmentID;
		private DateTime mPostDate;
		private string mComment;
		private string mUserName;
		private int mCCNID;
		private string mTransNo;
		private int mProductID;
		private int mStockUMID;
		private string mSerial;
		private Decimal mAdjustQuantity;
		private string mLot;
		private decimal mAvailableQuantity;
		private bool mUsedByCosting;

		public bool UsedByCosting
		{
			get { return mUsedByCosting; }
			set { mUsedByCosting = value; }
		}

		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int AdjustmentID
		{
			set { mAdjustmentID = value; }
			get { return mAdjustmentID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public string UserName
		{
			set { mUserName = value; }
			get { return mUserName; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public string TransNo
		{
			set { mTransNo = value; }
			get { return mTransNo; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public string Serial
		{
			set { mSerial = value; }
			get { return mSerial; }
		}
		public Decimal AdjustQuantity
		{
			set { mAdjustQuantity = value; }
			get { return mAdjustQuantity; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}

		public decimal AvailableQuantity
		{
			get { return mAvailableQuantity; }
			set { mAvailableQuantity = value; }
		}
	}
}

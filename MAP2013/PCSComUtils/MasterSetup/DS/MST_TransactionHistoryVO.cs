using System;
using System.Data;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_TransactionHistoryVO
	{
		private int mMasterLocationID;
		private int mBinID;
		private Decimal mBuySellCost;
		private int mInspStatus;
		private int mTransactionHistoryID;
		private DateTime mTransDate;
		private DateTime mPostDate;
		private int mRefMasterID;
		private int mRefDetailID;
		private Decimal mCost;
		private int mCCNID;
		private int mTranTypeID;
		private int mPartyID;
		private int mPartyLocationID;
		private int mLocationID;
		private int mProductID;
		private int mStockUMID;
		private int mCurrencyID;
		private Decimal mQuantity;
		private Decimal mMasLocOHQuantity;
		private Decimal mLocationOHQuantity;
		private Decimal mBinOHQuantity;
		private string mComment;
		private Decimal mExchangeRate;
		private string mLot;
		private string mSerial;
		private Decimal mOldAvgCost;
		private Decimal mNewAvgCost;
		private decimal mMasLocCommitQuantity;
		private decimal mLocationCommitQuantity;
		private decimal mBinCommitQuantity;
		private int mPurposeID;
		private string  mUsername;

		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public Decimal BuySellCost
		{
			set { mBuySellCost = value; }
			get { return mBuySellCost; }
		}
		public int InspStatus
		{
			set { mInspStatus = value; }
			get { return mInspStatus; }
		}
		public int TransactionHistoryID
		{
			set { mTransactionHistoryID = value; }
			get { return mTransactionHistoryID; }
		}
		public DateTime TransDate
		{
			set { mTransDate = value; }
			get { return mTransDate; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public int RefMasterID
		{
			set { mRefMasterID = value; }
			get { return mRefMasterID; }
		}
		public int RefDetailID
		{
			set { mRefDetailID = value; }
			get { return mRefDetailID; }
		}
		public Decimal Cost
		{
			set { mCost = value; }
			get { return mCost; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int TranTypeID
		{
			set { mTranTypeID = value; }
			get { return mTranTypeID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int PartyLocationID
		{
			set { mPartyLocationID = value; }
			get { return mPartyLocationID; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
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
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public Decimal MasLocOHQuantity
		{
			set { mMasLocOHQuantity = value; }
			get { return mMasLocOHQuantity; }
		}
		public Decimal LocationOHQuantity
		{
			set { mLocationOHQuantity = value; }
			get { return mLocationOHQuantity; }
		}
		public Decimal BinOHQuantity
		{
			set { mBinOHQuantity = value; }
			get { return mBinOHQuantity; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public Decimal ExchangeRate
		{
			set { mExchangeRate = value; }
			get { return mExchangeRate; }
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
		public Decimal OldAvgCost
		{
			set { mOldAvgCost = value; }
			get { return mOldAvgCost; }
		}
		public Decimal NewAvgCost
		{
			set { mNewAvgCost = value; }
			get { return mNewAvgCost; }
		}

		public decimal MasLocCommitQuantity
		{
			get { return mMasLocCommitQuantity; }
			set { mMasLocCommitQuantity = value; }
		}

		public decimal LocationCommitQuantity
		{
			get { return mLocationCommitQuantity; }
			set { mLocationCommitQuantity = value; }
		}

		public decimal BinCommitQuantity
		{
			get { return mBinCommitQuantity; }
			set { mBinCommitQuantity = value; }
		}
		public int PurposeID
		{
			get {return this.mPurposeID;}
			set {this.mPurposeID = value;}
		}

		public string Username
		{
			get { return mUsername; }
			set { mUsername = value; }
		}
	}
}

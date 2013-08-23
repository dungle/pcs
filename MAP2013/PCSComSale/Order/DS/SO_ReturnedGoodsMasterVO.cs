using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_ReturnedGoodsMasterVO
	{
		private int mReturnedGoodsMasterID;
		private string mReturnedGoodsNumber;
		private int mReceiverID;
		private int mCCNID;
		private DateTime mTransDate;
		private string mDescription;
		private DateTime mPostDate;
		private int mSaleOrderMasterID;
		private int mPartyID;
		private int mPartyContactID;
		private int mPartyLocationID;
		private int mMasterLocationID;
		private int mCurrencyID;
		private decimal mExchangeRate;
		public int ReturnedGoodsMasterID
		{
			set { mReturnedGoodsMasterID = value; }
			get { return mReturnedGoodsMasterID; }
		}
		public string ReturnedGoodsNumber
		{
			set { mReturnedGoodsNumber = value; }
			get { return mReturnedGoodsNumber; }
		}
		public int ReceiverID
		{
			set { mReceiverID = value; }
			get { return mReceiverID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public DateTime TransDate
		{
			set { mTransDate = value; }
			get { return mTransDate; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public int SaleOrderMasterID
		{
			set { mSaleOrderMasterID = value; }
			get { return mSaleOrderMasterID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int PartyContactID
		{
			set { mPartyContactID = value; }
			get { return mPartyContactID; }
		}
		public int PartyLocationID
		{
			set { mPartyLocationID = value; }
			get { return mPartyLocationID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public decimal ExchangeRate
		{
			set { mExchangeRate = value; }
			get { return mExchangeRate; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_ExchangeRateVO
	{
		private int mExchangeRateID;
		private string mCode;
		private Decimal mRate;
		private string mDescription;
		private bool mApproved;
		private DateTime mApprovalDate;
		private DateTime mBeginDate;
		private DateTime mEndDate;
		private int mCCNID;
		private int mCurrencyID;

		public int ExchangeRateID
		{
			set { mExchangeRateID = value; }
			get { return mExchangeRateID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public Decimal Rate
		{
			set { mRate = value; }
			get { return mRate; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public bool Approved
		{
			set { mApproved = value; }
			get { return mApproved; }
		}
		public DateTime ApprovalDate
		{
			set { mApprovalDate = value; }
			get { return mApprovalDate; }
		}
		public DateTime BeginDate
		{
			set { mBeginDate = value; }
			get { return mBeginDate; }
		}
		public DateTime EndDate
		{
			set { mEndDate = value; }
			get { return mEndDate; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
	}
}

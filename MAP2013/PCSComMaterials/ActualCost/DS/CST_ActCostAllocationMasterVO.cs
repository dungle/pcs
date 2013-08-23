using System;
using System.Data;


namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class CST_ActCostAllocationMasterVO
	{
		private int mActCostAllocationMasterID;
		private string mPeriodName;
		private DateTime mFromDate;
		private DateTime mToDate;
		private int mCCNID;
		private int mCurrencyID;
		private DateTime mRollupDate;

		public int ActCostAllocationMasterID
		{
			set { mActCostAllocationMasterID = value; }
			get { return mActCostAllocationMasterID; }
		}

		public string PeriodName
		{
			set { mPeriodName = value; }
			get { return mPeriodName; }
		}

		public DateTime FromDate
		{
			set { mFromDate = value; }
			get { return mFromDate; }
		}

		public DateTime ToDate
		{
			set { mToDate = value; }
			get { return mToDate; }
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
		
		public DateTime RollupDate
		{
			set { mRollupDate = value; }
			get { return mRollupDate; }
		}
	}
}
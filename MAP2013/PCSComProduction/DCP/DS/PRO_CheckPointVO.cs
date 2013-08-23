using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_CheckPointVO
	{
		private int mCheckPointID;
		private int mCCNID;
		private int mProductID;
		private int mWorkCenterID;
		private int mSamplePattern;
		private Decimal mSampleRate;
		private Decimal mDelayTime;

		public int CheckPointID
		{
			set { mCheckPointID = value; }
			get { return mCheckPointID; }
		}
		public int CCNID
		{
			set { mCCNID = value;}
			get { return mCCNID;}
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
		public int SamplePattern
		{
			set { mSamplePattern = value; }
			get { return mSamplePattern; }
		}
		public Decimal SampleRate
		{
			set { mSampleRate = value; }
			get { return mSampleRate; }
		}
		public Decimal DelayTime
		{
			set { mDelayTime = value; }
			get { return mDelayTime; }
		}
	}
}

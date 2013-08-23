using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_DCPResultDetailVO
	{
		private int mDCPResultDetailID;
		private DateTime mStartTime;
		private DateTime mEndTime;
		private Decimal mTotalSecond;
		private Decimal mQuantity;
		private Decimal mPercentage;
		private int mDCPResultMasterID;
		private int mType;
		private DateTime mWorkingDate;
		private int mWOConverted;
		private Decimal mSafetyStockAmount;

		public int DCPResultDetailID
		{
			set { mDCPResultDetailID = value; }
			get { return mDCPResultDetailID; }
		}
		public DateTime StartTime
		{
			set { mStartTime = value; }
			get { return mStartTime; }
		}
		public DateTime EndTime
		{
			set { mEndTime = value; }
			get { return mEndTime; }
		}
		public Decimal TotalSecond
		{
			set { mTotalSecond = value; }
			get { return mTotalSecond; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public Decimal Percentage
		{
			set { mPercentage = value; }
			get { return mPercentage; }
		}
		public int DCPResultMasterID
		{
			set { mDCPResultMasterID = value; }
			get { return mDCPResultMasterID; }
		}
		public DateTime WorkingDate
		{
			set { mWorkingDate = value; }
			get { return mWorkingDate; }
		}
		public int WOConverted
		{
			set { mWOConverted = value; }
			get { return mWOConverted; }
		}

		public Decimal SafetyStockAmount
		{
			set { mSafetyStockAmount = value; }
			get { return mSafetyStockAmount; }
		}
	}
}

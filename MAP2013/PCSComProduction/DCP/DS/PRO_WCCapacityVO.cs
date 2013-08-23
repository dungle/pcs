using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_WCCapacityVO
	{
		private int mWCCapacityID;
		private int mMachineNo;
		private DateTime mBeginDate;
		private DateTime mEndDate;
		private Decimal mFactor;
		private Decimal mCapacity;
		private Decimal mCrewSize;
		private int mCCNID;
		private int mWorkCenterID;
		private int mWCType;
	
		public int WCCapacityID
		{
			set { mWCCapacityID = value; }
			get { return mWCCapacityID; }
		}

		public int WCType
		{
			set { mWCType = value; }
			get { return mWCType; }
		}

		public int MachineNo
		{
			set { mMachineNo = value; }
			get { return mMachineNo; }
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
		public Decimal Factor
		{
			set { mFactor = value; }
			get { return mFactor; }
		}
		public Decimal Capacity
		{
			set { mCapacity = value; }
			get { return mCapacity; }
		}
		public Decimal CrewSize
		{
			set { mCrewSize = value; }
			get { return mCrewSize; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
	}
}

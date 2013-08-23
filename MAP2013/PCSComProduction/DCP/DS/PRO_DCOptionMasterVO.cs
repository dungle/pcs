using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_DCOptionMasterVO
	{
		private int mDCOptionMasterID;
		private string mCycle;
		private string mDescription;
		private int mScheduleType;
		private bool mIgnoreMoveTime;
		private bool mSafetyStock;
		private bool mOnHand;
		private bool mIncludeCheckPoint;
		//private int mMPSCycleOptionMasterID;
		private int mScheduleCode;
		private int mCCNID;
		private int mGroupBy;
		private DateTime mLastUpdate;
		private DateTime mAsOfDate;
		private int mPlanHorizon;
		private DateTime mPlanningPeriod;
		private int mVersion;
		private bool mUseCacheAsBegin;
		public int DCOptionMasterID
		{
			set { mDCOptionMasterID = value; }
			get { return mDCOptionMasterID; }
		}
		public string Cycle
		{
			set { mCycle = value; }
			get { return mCycle; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int GroupBy
		{
			set { mGroupBy = value; }
			get { return mGroupBy; }
		}
		public int ScheduleType
		{
			set { mScheduleType = value; }
			get { return mScheduleType; }
		}
		public bool IgnoreMoveTime
		{
			set { mIgnoreMoveTime = value; }
			get { return mIgnoreMoveTime; }
		}
		public bool SafetyStock
		{
			set { mSafetyStock = value; }
			get { return mSafetyStock; }
		}
		public bool OnHand
		{
			set { mOnHand = value; }
			get { return mOnHand; }
		}
		public bool IncludeCheckPoint
		{
			set { mIncludeCheckPoint = value; }
			get { return mIncludeCheckPoint; }
		}
//		public int MPSCycleOptionMasterID
//		{
//			set { mMPSCycleOptionMasterID = value; }
//			get { return mMPSCycleOptionMasterID; }
//		}
		public int ScheduleCode
		{
			set { mScheduleCode = value; }
			get { return mScheduleCode; }
		}
		public int PlanHorizon
		{
			set { mPlanHorizon = value; }
			get { return mPlanHorizon; }
		}
		public DateTime LastUpdate
		{
			set { mLastUpdate = value; }
			get { return mLastUpdate; }
		}
		public DateTime AsOfDate
		{
			set { mAsOfDate = value; }
			get { return mAsOfDate; }
		}
		public DateTime PlanningPeriod
		{
			set { mPlanningPeriod = value; }
			get { return mPlanningPeriod; }
		}
		public int Version
		{
			set { mVersion = value; }
			get { return mVersion; }
		}

		public bool UseCacheAsBegin
		{
			get { return mUseCacheAsBegin; }
			set { mUseCacheAsBegin = value; }
		}
	}
}

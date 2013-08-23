using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_DCPCOptionVO
	{
		private int mDCPCOptionID;
		private int mMPSCycleID;
		private int mMRPCycleID;
		private DateTime mAsOfDate;
		private string mCycle;
		private string mDescription;
		private DateTime mLastUpdate;
		private int mPlanHorizon;
		private bool mAverageSchedule;
		private bool mFiniteSchedule;
		private int mWorkOrderID;
		private int mMasterLocationID;
		private int mCCNID;

		public int DCPCOptionID
		{
			set { mDCPCOptionID = value; }
			get { return mDCPCOptionID; }
		}
		public int MPSCycleID
		{
			set { mMPSCycleID = value; }
			get { return mMPSCycleID; }
		}
		public int MRPCycleID
		{
			set { mMRPCycleID = value; }
			get { return mMRPCycleID; }
		}
		public DateTime AsOfDate
		{
			set { mAsOfDate = value; }
			get { return mAsOfDate; }
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
		public DateTime LastUpdate
		{
			set { mLastUpdate = value; }
			get { return mLastUpdate; }
		}
		public int PlanHorizon
		{
			set { mPlanHorizon = value; }
			get { return mPlanHorizon; }
		}
		public bool AverageSchedule
		{
			set { mAverageSchedule = value; }
			get { return mAverageSchedule; }
		}
		public bool FiniteSchedule
		{
			set { mFiniteSchedule = value; }
			get { return mFiniteSchedule; }
		}
		public int WorkOrderID
		{
			set { mWorkOrderID = value; }
			get { return mWorkOrderID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

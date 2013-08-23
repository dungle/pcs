using System;
using System.Data;

namespace PCSComMaterials.Plan.DS
{
	[Serializable]
	public class MTR_MRPCycleOptionMasterVO
	{
		private int mMRPCycleOptionMasterID;
		private string mCycle;
		private DateTime mAsOfDate;
		private DateTime mMPSGenDate;
		private int mPlanHorizon;
		private int mCCNID;
		private string mDescription;
		private int mMPSCycleOptionMasterID;
		private bool mIncludedRemainPO;
		private int mDaysBeforeAsOfDate;
		private bool mIncludedReturnToVendor;

		public int MRPCycleOptionMasterID
		{
			set { mMRPCycleOptionMasterID = value; }
			get { return mMRPCycleOptionMasterID; }
		}
		public string Cycle
		{
			set { mCycle = value; }
			get { return mCycle; }
		}
		public DateTime AsOfDate
		{
			set { mAsOfDate = value; }
			get { return mAsOfDate; }
		}
		public DateTime MPSGenDate
		{
			set { mMPSGenDate = value; }
			get { return mMPSGenDate; }
		}
		public int PlanHorizon
		{
			set { mPlanHorizon = value; }
			get { return mPlanHorizon; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int MPSCycleOptionMasterID
		{
			set { mMPSCycleOptionMasterID = value; }
			get { return mMPSCycleOptionMasterID; }
		}

		public bool IncludedRemainPO
		{
			get { return mIncludedRemainPO; }
			set { mIncludedRemainPO = value; }
		}

		public int DaysBeforeAsOfDate
		{
			get { return mDaysBeforeAsOfDate; }
			set { mDaysBeforeAsOfDate = value; }
		}

		public bool IncludedReturnToVendor
		{
			get { return mIncludedReturnToVendor; }
			set { mIncludedReturnToVendor = value; }
		}
	}
}

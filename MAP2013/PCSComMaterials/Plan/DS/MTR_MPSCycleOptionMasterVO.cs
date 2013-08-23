using System;
using System.Data;

namespace PCSComMaterials.Plan.DS
{
	[Serializable]
	public class MTR_MPSCycleOptionMasterVO
	{
		private int mMPSCycleOptionMasterID;
		private string mCycle;
		private DateTime mAsOfDate;
		private DateTime mMPSGenDate;
		private int mPlanHorizon;
		private bool mPlanThoughHorizon;
		private int mCCNID;
		private string mDescription;
		private int mGroupBy;

		public int MPSCycleOptionMasterID
		{
			set { mMPSCycleOptionMasterID = value; }
			get { return mMPSCycleOptionMasterID; }
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
		public bool PlanThoughHorizon
		{
			set { mPlanThoughHorizon = value; }
			get { return mPlanThoughHorizon; }
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
		public int GroupBy
		{
			set { mGroupBy = value; }
			get { return mGroupBy; }
		}
	}
}

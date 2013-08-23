using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_ApprovalLevelVO
	{
		private int mApprovalLevelID;
		private string mLevel;
		private Decimal mAmount;
		private string mDescription;
		private int mCCNID;

		public int ApprovalLevelID
		{
			set { mApprovalLevelID = value; }
			get { return mApprovalLevelID; }
		}
		public string Level
		{
			set { mLevel = value; }
			get { return mLevel; }
		}
		public Decimal Amount
		{
			set { mAmount = value; }
			get { return mAmount; }
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
	}
}

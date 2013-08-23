using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_WorkCenterVO
	{
		private int mWorkCenterID;
		private string mCode;
		private string mDescription;
		private string mName;
		private int mCCNID;
		private bool mIsMain;
		
		public bool IsMain
		{
			set { mIsMain = value; }
			get { return mIsMain; }
		}

		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

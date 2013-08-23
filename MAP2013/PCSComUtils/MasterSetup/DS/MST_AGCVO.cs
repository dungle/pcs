using System;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_AGCVO
	{
		private int mAGCID;
		private string mDescription;
		private string mCode;
		private int mCCNID;

		public int AGCID
		{
			set { mAGCID = value; }
			get { return mAGCID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

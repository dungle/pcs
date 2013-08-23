using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_ReasonVO
	{
		private int mReasonID;
		private string mCode;
		private string mDescription;
		private int mCCNID;

		public int ReasonID
		{
			set { mReasonID = value; }
			get { return mReasonID; }
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
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

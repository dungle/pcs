using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_PauseVO
	{
		private int mPauseID;
		private string mCode;
		private string mDescription;

		public int PauseID
		{
			set { mPauseID = value; }
			get { return mPauseID; }
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
	}
}

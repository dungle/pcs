using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_TranTypeVO
	{
		private int mTranTypeID;
		private string mCode;
		private string mDescription;

		public int TranTypeID
		{
			set { mTranTypeID = value; }
			get { return mTranTypeID; }
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

using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_CurrencyVO
	{
		private int mCurrencyID;
		private string mCode;
		private string mName;
		private string mMask;

		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public string Mask
		{
			set { mMask = value; }
			get { return mMask; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_CountryVO
	{
		private int mCountryID;
		private string mCode;
		private string mName;

		public int CountryID
		{
			set { mCountryID = value; }
			get { return mCountryID; }
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
	}
}

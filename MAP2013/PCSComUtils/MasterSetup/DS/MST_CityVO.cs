using System;


namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_CityVO
	{
		private int mCityID;
		private string mCode;
		private string mName;
		private int mCountryID;

		public int CityID
		{
			set { mCityID = value; }
			get { return mCityID; }
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
		public int CountryID
		{
			set { mCountryID = value; }
			get { return mCountryID; }
		}
	}
}

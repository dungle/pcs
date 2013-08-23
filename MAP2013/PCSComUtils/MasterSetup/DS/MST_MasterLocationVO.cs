using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_MasterLocationVO
	{
		private int mMasterLocationID;
		private string mCode;
		private string mName;
		private string mAddress;
		private string mState;
		private string mZipPost;
		private int mCCNID;
		private int mCityID;
		private int mCountryID;

		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
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
		public string Address
		{
			set { mAddress = value; }
			get { return mAddress; }
		}
		public string State
		{
			set { mState = value; }
			get { return mState; }
		}
		public string ZipPost
		{
			set { mZipPost = value; }
			get { return mZipPost; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int CityID
		{
			set { mCityID = value; }
			get { return mCityID; }
		}
		public int CountryID
		{
			set { mCountryID = value; }
			get { return mCountryID; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_PartyLocationVO
	{
		private int mPartyLocationID;
		private string mCode;
		private string mDescription;
		private bool mDeleteReason;
		private string mAddress;
		private int mCityID;
		private int mCountryID;
		private string mState;
		private string mZipPost;
		private int mPartyID;

		public int PartyLocationID
		{
			set { mPartyLocationID = value; }
			get { return mPartyLocationID; }
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
		public bool DeleteReason
		{
			set { mDeleteReason = value; }
			get { return mDeleteReason; }
		}
		public string Address
		{
			set { mAddress = value; }
			get { return mAddress; }
		}
		public int CountryID
		{
			set { mCountryID = value; }
			get { return mCountryID; }
		}
		public int CityID
		{
			set { mCityID = value; }
			get { return mCityID; }
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
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_CarrierVO
	{
		private int mCarrierID;
		private string mCode;
		private string mName;
		private string mAddress;
		private string mPhone;
		private string mFax;
		private string mEmail;
		private string mWebSite;

		public int CarrierID
		{
			set { mCarrierID = value; }
			get { return mCarrierID; }
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
		public string Phone
		{
			set { mPhone = value; }
			get { return mPhone; }
		}
		public string Fax
		{
			set { mFax = value; }
			get { return mFax; }
		}
		public string Email
		{
			set { mEmail = value; }
			get { return mEmail; }
		}
		public string WebSite
		{
			set { mWebSite = value; }
			get { return mWebSite; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_PartyContactVO
	{
		private int mPartyContactID;
		private string mCode;
		private string mName;
		private string mTitle;
		private string mMemo;
		private string mFax;
		private string mPhone;
		private string mEmail;
		private string mExt;
		private int mPartyLocationID;
		private int mPartyID;

		public int PartyContactID
		{
			set { mPartyContactID = value; }
			get { return mPartyContactID; }
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
		public string Title
		{
			set { mTitle = value; }
			get { return mTitle; }
		}
		public string Memo
		{
			set { mMemo = value; }
			get { return mMemo; }
		}
		public string Fax
		{
			set { mFax = value; }
			get { return mFax; }
		}
		public string Phone
		{
			set { mPhone = value; }
			get { return mPhone; }
		}
		public string Email
		{
			set { mEmail = value; }
			get { return mEmail; }
		}
		public string Ext
		{
			set { mExt = value; }
			get { return mExt; }
		}
		public int PartyLocationID
		{
			set { mPartyLocationID = value; }
			get { return mPartyLocationID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
	}
}

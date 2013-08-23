using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_PartyVO
	{
		private int mPartyID;
		private string mCode;
		private string mName;
		private string mAddress;
		private string mWebSite;
		private string mState;
		private bool mDeleteReason;
		private int mType;
		private string mZipPost;
		private string mMAPBankAccountNo;
		private string mMAPBankAccountName;
		private string mVATCode;
		private int mCountryID;
		private int mCityID;
		
		//3 added properties-Added by Tuan TQ -2005-09-21
		private string mPhone;
		private string mFax;
		private string mBankAccount;
		private int mPaymentTermID;
		private int mCurrencyID;

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

		public string BankAccount
		{
			set { mBankAccount = value; }
			get { return mBankAccount; }
		}
		//End-Added by Tuan TQ -2005-09-21

		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
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
		public string WebSite
		{
			set { mWebSite = value; }
			get { return mWebSite; }
		}
		public string State
		{
			set { mState = value; }
			get { return mState; }
		}
		public bool DeleteReason
		{
			set { mDeleteReason = value; }
			get { return mDeleteReason; }
		}
		public int Type
		{
			set { mType = value; }
			get { return mType; }
		}
		public string ZipPost
		{
			set { mZipPost = value; }
			get { return mZipPost; }
		}
		public string MAPBankAccountNo
		{
			set { mMAPBankAccountNo = value; }
			get { return mMAPBankAccountNo; }
		}
		public string MAPBankAccountName
		{
			set { mMAPBankAccountName = value; }
			get { return mMAPBankAccountName; }
		}
		public string VATCode
		{
			set { mVATCode = value; }
			get { return mVATCode; }
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

		public int PaymentTermID
		{
			get { return mPaymentTermID; }
			set { mPaymentTermID = value; }
		}

		public int CurrencyID
		{
			get { return mCurrencyID; }
			set { mCurrencyID = value; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_CCNVO
	{
		private int mCCNID;
		private string mCode;
		private string mDescription;
		private string mName;
		private string mState;
		private string mZipCode;
		private string mPhone;
		private string mFax;
		private string mWebSite;
		private string mEmail;
		private string mVAT;
		private int mCountryID;
		private int mCityID;
		private int mHomeCurrencyID;
		private float mExchangeRate;
		private int mDefaultCurrencyID;
		private string mExchangeRateOperator;

		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
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
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public string State
		{
			set { mState = value; }
			get { return mState; }
		}
		public string ZipCode
		{
			set { mZipCode = value; }
			get { return mZipCode; }
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
		public string WebSite
		{
			set { mWebSite = value; }
			get { return mWebSite; }
		}
		public string Email
		{
			set { mEmail = value; }
			get { return mEmail; }
		}
		public string VAT
		{
			set { mVAT = value; }
			get { return mVAT; }
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
		public int HomeCurrencyID
		{
			set { mHomeCurrencyID = value; }
			get { return mHomeCurrencyID; }
		}
		public float ExchangeRate
		{
			set { mExchangeRate = value; }
			get { return mExchangeRate; }
		}
		public int DefaultCurrencyID
		{
			set { mDefaultCurrencyID = value; }
			get { return mDefaultCurrencyID; }
		}
		public string ExchangeRateOperator
		{
			set { mExchangeRateOperator = value; }
			get { return mExchangeRateOperator; }
		}
	}
}

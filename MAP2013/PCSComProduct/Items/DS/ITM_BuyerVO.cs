using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_BuyerVO
	{
		private int mBuyerID;
		private string mCode;
		private string mDescription;
		private string mName;
		private string mAddress;

		public int BuyerID
		{
			set { mBuyerID = value; }
			get { return mBuyerID; }
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
		public string Address
		{
			set { mAddress = value; }
			get { return mAddress; }
		}
	}
}

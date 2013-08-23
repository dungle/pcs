using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PurchaseTypeVO
	{
		private int	mPurchaseTypeID;
		private string	mCode;
		private string	mDescription;

		public int	PurchaseTypeID
		{	set { mPurchaseTypeID = value; }
			get { return mPurchaseTypeID; }
		}
		public string	Code
		{	set { mCode = value; }
			get { return mCode; }
		}
		public string	Description
		{	set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

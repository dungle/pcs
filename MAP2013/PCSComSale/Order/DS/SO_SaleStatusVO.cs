using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_SaleStatusVO
	{
		private int mSaleStatusID;
		private string mCode;
		private string mDescription;

		public int SaleStatusID
		{
			set { mSaleStatusID = value; }
			get { return mSaleStatusID; }
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
	}
}

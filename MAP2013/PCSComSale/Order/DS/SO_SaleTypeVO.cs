using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_SaleTypeVO
	{
		private int mSaleTypeID;
		private string mDescription;
		private string mCode;

		public int SaleTypeID
		{
			set { mSaleTypeID = value; }
			get { return mSaleTypeID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
	}
}

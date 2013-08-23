using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_PaymentMethodVO
	{
		private int mPaymentMethodID;
		private string mCode;
		private string mDescription;

		public int PaymentMethodID
		{
			set { mPaymentMethodID = value; }
			get { return mPaymentMethodID; }
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

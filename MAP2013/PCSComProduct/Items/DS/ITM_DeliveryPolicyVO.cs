using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_DeliveryPolicyVO
	{
		private int mDeliveryPolicyID;
		private string mCode;
		private string mDescription;
		private Decimal mDeliveryPolicyDays;

		public int DeliveryPolicyID
		{
			set { mDeliveryPolicyID = value; }
			get { return mDeliveryPolicyID; }
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
		public Decimal DeliveryPolicyDays
		{
			set { mDeliveryPolicyDays = value; }
			get { return mDeliveryPolicyDays; }
		}
	}
}

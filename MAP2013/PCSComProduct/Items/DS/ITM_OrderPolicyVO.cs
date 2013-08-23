using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_OrderPolicyVO
	{
		private int mOrderPolicyID;
		private string mCode;
		private Decimal mOrderPolicyDays;
		private string mDescription;

		public int OrderPolicyID
		{
			set { mOrderPolicyID = value; }
			get { return mOrderPolicyID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public Decimal OrderPolicyDays
		{
			set { mOrderPolicyDays = value; }
			get { return mOrderPolicyDays; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

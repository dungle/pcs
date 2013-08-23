using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_OrderRuleVO
	{
		private int mOrderRuleID;
		private string mCode;
		private string mDescription;

		public int OrderRuleID
		{
			set { mOrderRuleID = value; }
			get { return mOrderRuleID; }
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

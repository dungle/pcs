using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_RoutingStatusVO
	{
		private int mRoutingStatusID;
		private string mCode;
		private string mDescription;

		public int RoutingStatusID
		{
			set { mRoutingStatusID = value; }
			get { return mRoutingStatusID; }
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

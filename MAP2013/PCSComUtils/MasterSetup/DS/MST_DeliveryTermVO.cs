using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_DeliveryTermVO
	{
		private int mDeliveryTermID;
		private string mCode;
		private string mDescription;

		public int DeliveryTermID
		{
			set { mDeliveryTermID = value; }
			get { return mDeliveryTermID; }
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

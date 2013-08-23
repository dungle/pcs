using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_HazardVO
	{
		private string mCode;
		private string mDescription;
		private int mHazardID;

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
		public int HazardID
		{
			set { mHazardID = value; }
			get { return mHazardID; }
		}
	}
}

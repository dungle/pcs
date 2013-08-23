using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_FreightClassVO
	{
		private int mFreightClassID;
		private string mCode;
		private string mDescription;

		public int FreightClassID
		{
			set { mFreightClassID = value; }
			get { return mFreightClassID; }
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

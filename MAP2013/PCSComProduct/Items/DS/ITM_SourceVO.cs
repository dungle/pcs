using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_SourceVO
	{
		private int mSourceID;
		private string mCode;
		private string mDescription;

		public int SourceID
		{
			set { mSourceID = value; }
			get { return mSourceID; }
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

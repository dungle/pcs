using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_FormatCodeVO
	{
		private int mFormatCodeID;
		private string mCode;
		private string mDescription;

		public int FormatCodeID
		{
			set { mFormatCodeID = value; }
			get { return mFormatCodeID; }
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

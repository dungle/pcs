using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_DeleteReasonVO
	{
		private int mDeleteReasonID;
		private string mCode;
		private string mDescription;

		public int DeleteReasonID
		{
			set { mDeleteReasonID = value; }
			get { return mDeleteReasonID; }
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

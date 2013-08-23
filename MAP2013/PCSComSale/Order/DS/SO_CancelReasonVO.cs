using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_CancelReasonVO
	{
		private int mCancelReasonID;
		private string mCode;
		private string mDescription;

		public int CancelReasonID
		{
			set { mCancelReasonID = value; }
			get { return mCancelReasonID; }
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

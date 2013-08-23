using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_PictureVO
	{
		private int mPictureID;
		//private byte mPictureImage;
		private int mProductID;
		private string mDescription;

		public int PictureID
		{
			set { mPictureID = value; }
			get { return mPictureID; }
		}
//		public image PictureImage
//		{
//			set { mPictureImage = value; }
//			get { return mPictureImage; }
//		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

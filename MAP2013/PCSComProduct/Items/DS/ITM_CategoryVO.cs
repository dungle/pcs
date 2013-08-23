using System;
using System.Drawing;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_CategoryVO
	{
		private int mCategoryID;
		private string mCode;
		private string mName;
		private string mDescription;		
		private int mParentCategoryId;
		private Bitmap mPicture;
		
		//added properties, added by Tuan TQ -- 2005-09-21
		private string mCatalogNameVN;
		public string CatalogNameVN
		{
			set { mCatalogNameVN = value; }
			get { return mCatalogNameVN; }
		}

		//end added by Tuan TQ -- 2005-09-21

		public int CategoryID
		{
			set { mCategoryID = value; }
			get { return mCategoryID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}

		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}		

		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int ParentCategoryId
		{
			set { mParentCategoryId = value; }
			get { return mParentCategoryId; }
		}

		public Bitmap Picture
		{
			get { return mPicture; }
			set { mPicture = value; }
		}
	}
}

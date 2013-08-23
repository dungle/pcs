using System;
using System.Data;
namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_ChangeCategoryDetailVO
	{
		private int mChangeCategoryDetailID;
		private int mProductID;
		private int mChangeCategoryMasterID;

		public int ChangeCategoryDetailID
		{
			set { mChangeCategoryDetailID = value; }
			get { return mChangeCategoryDetailID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int ChangeCategoryMasterID
		{
			set { mChangeCategoryMasterID = value; }
			get { return mChangeCategoryMasterID; }
		}
	}
}

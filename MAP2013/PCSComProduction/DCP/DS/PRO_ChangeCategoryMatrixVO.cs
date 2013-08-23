using System;
using System.Data;
namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_ChangeCategoryMatrixVO
	{
		private int mChangeCategoryMatrixID;
		private int mSourceProductID;
		private int mDestProductID;
		private int mChangeTime;
		private int mChangeCategoryMasterID;

		public int ChangeCategoryMatrixID
		{
			set { mChangeCategoryMatrixID = value; }
			get { return mChangeCategoryMatrixID; }
		}
		public int SourceProductID
		{
			set { mSourceProductID = value; }
			get { return mSourceProductID; }
		}
		public int DestProductID
		{
			set { mDestProductID = value; }
			get { return mDestProductID; }
		}
		public int ChangeTime
		{
			set { mChangeTime = value; }
			get { return mChangeTime; }
		}
		public int ChangeCategoryMasterID
		{
			set { mChangeCategoryMasterID = value; }
			get { return mChangeCategoryMasterID; }
		}
	}
}

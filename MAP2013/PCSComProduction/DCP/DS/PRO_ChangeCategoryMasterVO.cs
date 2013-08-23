using System;
using System.Data;
namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_ChangeCategoryMasterVO
	{
		private int mChangeCategoryMasterID;
		private int mWorkCenterID;
		private int mCCNID;

		public int ChangeCategoryMasterID
		{
			set { mChangeCategoryMasterID = value; }
			get { return mChangeCategoryMasterID; }
		}
		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

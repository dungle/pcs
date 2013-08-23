using System;
using System.Data;


namespace PCSComProduct.STDCost.DS
{
	[Serializable]
	public class STD_CostElementVO
	{
		private int mCostElementID = 0;
		private string mCode = string.Empty;
		private string mName = string.Empty;
		private int mOrderNo = 0;
		private int mParentID = 0;
		private int mCostElementTypeID = 0;
		private int mIsLeaf = 0;

		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
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

		public int OrderNo
		{
			set { mOrderNo = value; }
			get { return mOrderNo; }
		}

		public int ParentID
		{
			set { mParentID = value; }
			get { return mParentID; }
		}

		public int CostElementTypeID
		{
			set { mCostElementTypeID = value; }
			get { return mCostElementTypeID; }
		}
		
		public int IsLeaf
		{
			set { mIsLeaf = value; }
			get { return mIsLeaf; }
		}
	}
}

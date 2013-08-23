using System;
using System.Data;

namespace PCSComProduct.STDCost.DS
{
	[Serializable]
	public class STD_CostElementTypeVO
	{
		private int mCostElementTypeID;
		private string mDescription;
		private int mCode;

		public int CostElementTypeID
		{
			set { mCostElementTypeID = value; }
			get { return mCostElementTypeID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
	}
}

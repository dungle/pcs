using System;
using System.Data;

namespace PCSComProduct.STDCost.DS
{
	[Serializable]
	public class STD_CostCenterRateMasterVO
	{
		private int mCostCenterRateMasterID;
		private int mCCNID;
		private string mCode;
		private string mName;

		public int CostCenterRateMasterID
		{
			set { mCostCenterRateMasterID = value; }
			get { return mCostCenterRateMasterID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
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
	}
}

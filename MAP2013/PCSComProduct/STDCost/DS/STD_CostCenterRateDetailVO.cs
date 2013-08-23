using System;
using System.Data;

namespace PCSComProduct.STDCost.DS
{
	[Serializable]
	public class STD_CostCenterRateDetailVO
	{
		private int mCostCenterRateDetailID;
		private int mCostCenterRateMasterID;
		private int mCostElementID;
		private Decimal mCost;

		public int CostCenterRateDetailID
		{
			set { mCostCenterRateDetailID = value; }
			get { return mCostCenterRateDetailID; }
		}
		public int CostCenterRateMasterID
		{
			set { mCostCenterRateMasterID = value; }
			get { return mCostCenterRateMasterID; }
		}
		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
		}
		public Decimal Cost
		{
			set { mCost = value; }
			get { return mCost; }
		}
	}
}

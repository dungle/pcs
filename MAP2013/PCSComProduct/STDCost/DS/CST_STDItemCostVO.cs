using System;
using System.Data;

namespace PCSComProduct.STDCost.DS
{
	[Serializable]
	public class CST_STDItemCostVO
	{
		private int mSTDItemCostID;
		private Decimal mCost;
		private DateTime mRollUpDate;
		private int mProductID;
		private int mCostElementID;

		public int STDItemCostID
		{
			set { mSTDItemCostID = value; }
			get { return mSTDItemCostID; }
		}
		public Decimal Cost
		{
			set { mCost = value; }
			get { return mCost; }
		}
		public DateTime RollUpDate
		{
			set { mRollUpDate = value; }
			get { return mRollUpDate; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
		}
	}
}

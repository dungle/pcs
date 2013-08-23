using System;
using System.Data;


namespace PCSComMaterials.ActualCost.DS		
{
	[Serializable]
	public class CST_ActCostAllocationDetailVO
	{
		private int mActCostAllocationDetailID;
		private Decimal mAllocationAmount;
		private int mActCostAllocationMasterID;
		private int mCostElementID;
		private int mDepartmentID;
		private int mProductionLineID;
		private int mProductID;
		private int mProductGroupID;
		private int mLine;

		public int ActCostAllocationDetailID
		{
			set { mActCostAllocationDetailID = value; }
			get { return mActCostAllocationDetailID; }
		}

		public Decimal AllocationAmount
		{
			set { mAllocationAmount = value; }
			get { return mAllocationAmount; }
		}

		public int ActCostAllocationMasterID
		{
			set { mActCostAllocationMasterID = value; }
			get { return mActCostAllocationMasterID; }
		}

		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
		}
		public int DepartmentID
		{
			set { mDepartmentID = value; }
			get { return mDepartmentID; }
		}

		public int ProductionLineID
		{
			set { mProductionLineID = value; }
			get { return mProductionLineID; }
		}

		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}

		public int ProductGroupID
		{
			set { mProductGroupID = value; }
			get { return mProductGroupID; }
		}

		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
	}
}

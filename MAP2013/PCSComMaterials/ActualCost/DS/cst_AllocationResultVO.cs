using System;
using System.Data;

namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class CST_AllocationResultVO
	{
		private int mAllocationResultID;
		private Decimal mCompletedQuantity;
		private Decimal mRate;
		private Decimal mAmount;
		private int mActCostAllocationMasterID;
		private int mDepartmentID;
		private int mProductionLineID;
		private int mProductGroupID;
		private int mCostElementID;
		private int mProductID;

		public int AllocationResultID
		{
			set { mAllocationResultID = value; }
			get { return mAllocationResultID; }
		}
		public Decimal CompletedQuantity
		{
			set { mCompletedQuantity = value; }
			get { return mCompletedQuantity; }
		}
		public Decimal Rate
		{
			set { mRate = value; }
			get { return mRate; }
		}
		public Decimal Amount
		{
			set { mAmount = value; }
			get { return mAmount; }
		}
		public int ActCostAllocationMasterID
		{
			set { mActCostAllocationMasterID = value; }
			get { return mActCostAllocationMasterID; }
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
		public int ProductGroupID
		{
			set { mProductGroupID = value; }
			get { return mProductGroupID; }
		}
		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
	}
}

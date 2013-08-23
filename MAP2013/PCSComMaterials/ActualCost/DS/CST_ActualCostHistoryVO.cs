using System;
using System.Data;

namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class CST_ActualCostHistoryVO
	{
		private int mActualCostHistory;
		private Decimal mActualCost;
		private Decimal mStdCost;
		private int mProductID;
		private int mCostElementID;
		private int mActCostAllocationMasterID;
		private decimal mQuantity;
		private decimal mBeginQuantity;
		private decimal mComponentValue;
		private decimal mComponentDSAmount;
		private decimal mComBeginCost;
		private decimal mWOCompletionQty;
		private decimal mTransactionAmount;
		private decimal mRecycleAmount;
		private decimal mRecoverableAmount;
		private decimal mFreightAmount;
		private decimal mDSAmount;
		private decimal mDSOKAmount;
		private decimal mAdjustAmount;
		private decimal mBeginCost;

		public int ActualCostHistory
		{
			set { mActualCostHistory = value; }
			get { return mActualCostHistory; }
		}
		public Decimal ActualCost
		{
			set { mActualCost = value; }
			get { return mActualCost; }
		}
		public Decimal StdCost
		{
			set { mStdCost = value; }
			get { return mStdCost; }
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
		public int ActCostAllocationMasterID
		{
			set { mActCostAllocationMasterID = value; }
			get { return mActCostAllocationMasterID; }
		}

		public decimal Quantity
		{
			get { return mQuantity; }
			set { mQuantity = value; }
		}

		public decimal BeginQuantity
		{
			get { return mBeginQuantity; }
			set { mBeginQuantity = value; }
		}

		public decimal ComponentValue
		{
			get { return mComponentValue; }
			set { mComponentValue = value; }
		}

		public decimal ComponentDSAmount
		{
			get { return mComponentDSAmount; }
			set { mComponentDSAmount = value; }
		}

		public decimal ComBeginCost
		{
			get { return mComBeginCost; }
			set { mComBeginCost = value; }
		}

		public decimal WOCompletionQty
		{
			get { return mWOCompletionQty; }
			set { mWOCompletionQty = value; }
		}

		public decimal TransactionAmount
		{
			get { return mTransactionAmount; }
			set { mTransactionAmount = value; }
		}

		public decimal RecycleAmount
		{
			get { return mRecycleAmount; }
			set { mRecycleAmount = value; }
		}

		public decimal RecoverableAmount
		{
			get { return mRecoverableAmount; }
			set { mRecoverableAmount = value; }
		}

		public decimal FreightAmount
		{
			get { return mFreightAmount; }
			set { mFreightAmount = value; }
		}

		public decimal DSAmount
		{
			get { return mDSAmount; }
			set { mDSAmount = value; }
		}

		public decimal DSOKAmount
		{
			get { return mDSOKAmount; }
			set { mDSOKAmount = value; }
		}

		public decimal AdjustAmount
		{
			get { return mAdjustAmount; }
			set { mAdjustAmount = value; }
		}

		public decimal BeginCost
		{
			get { return mBeginCost; }
			set { mBeginCost = value; }
		}
	}
}

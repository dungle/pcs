using System;

namespace PCSComMaterials.ActualCost.DS
{
	/// <summary>
	/// Summary description for CST_DSAndRecycleAllocationVO.
	/// </summary>
	[Serializable]
	public class CST_DSAndRecycleAllocationVO
	{
		private int mDSADAllocactionID;
		private int mActCostAllocationMasterID;
		private int mProductID;
		private int mCostElementID;
		private decimal mDSRate;
		private decimal mDSAmount;
		private decimal mRecycleRate;
		private decimal mRecycleAmount;
		private decimal mShippingQty;
		private decimal mReturnGoodsReceiptQty;
		private decimal mAdjustAmount;
		private decimal mOH_RecycleAmount;
		private decimal mOH_DSAmount;
		private decimal mOH_AdjustAmount;

		public int DSADAllocactionID
		{
			get { return mDSADAllocactionID; }
			set { mDSADAllocactionID = value; }
		}

		public int ActCostAllocationMasterID
		{
			get { return mActCostAllocationMasterID; }
			set { mActCostAllocationMasterID = value; }
		}

		public int ProductID
		{
			get { return mProductID; }
			set { mProductID = value; }
		}

		public decimal DSRate
		{
			get { return mDSRate; }
			set { mDSRate = value; }
		}

		public decimal DSAmount
		{
			get { return mDSAmount; }
			set { mDSAmount = value; }
		}

		public decimal RecycleRate
		{
			get { return mRecycleRate; }
			set { mRecycleRate = value; }
		}

		public decimal RecycleAmount
		{
			get { return mRecycleAmount; }
			set { mRecycleAmount = value; }
		}

		public decimal ShippingQty
		{
			get { return mShippingQty; }
			set { mShippingQty = value; }
		}

		public decimal ReturnGoodsReceiptQty
		{
			get { return mReturnGoodsReceiptQty; }
			set { mReturnGoodsReceiptQty = value; }
		}

		public int CostElementID
		{
			get { return mCostElementID; }
			set { mCostElementID = value; }
		}

		public decimal AdjustAmount
		{
			get { return mAdjustAmount; }
			set { mAdjustAmount = value; }
		}
		public decimal OH_RecycleAmount
		{
			get { return mOH_RecycleAmount; }
			set { mOH_RecycleAmount = value; }
		}

		public decimal OH_DSAmount
		{
			get { return mOH_DSAmount; }
			set { mOH_DSAmount = value; }
		}

		public decimal OH_AdjustAmount
		{
			get { return mOH_AdjustAmount; }
			set { mOH_AdjustAmount = value; }
		}
	}
}
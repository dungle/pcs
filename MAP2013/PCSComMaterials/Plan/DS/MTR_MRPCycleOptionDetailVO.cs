using System;
using System.Data;

namespace PCSComMaterials.Plan.DS
{
	[Serializable]
	public class MTR_MRPCycleOptionDetailVO
	{
		private int mMRPCycleOptionDetailID;
		private bool mOnHand;
		private bool mPurchaseOrder;
		private bool mSaleOrder;
		private bool mDemandWO;
		private int mMRPCycleOptionMasterID;
		private int mMasterLocationID;

		public int MRPCycleOptionDetailID
		{
			set { mMRPCycleOptionDetailID = value; }
			get { return mMRPCycleOptionDetailID; }
		}
		public bool OnHand
		{
			set { mOnHand = value; }
			get { return mOnHand; }
		}
		public bool PurchaseOrder
		{
			set { mPurchaseOrder = value; }
			get { return mPurchaseOrder; }
		}
		public bool SaleOrder
		{
			set { mSaleOrder = value; }
			get { return mSaleOrder; }
		}
		public bool DemandWO
		{
			set { mDemandWO = value; }
			get { return mDemandWO; }
		}
		public int MRPCycleOptionMasterID
		{
			set { mMRPCycleOptionMasterID = value; }
			get { return mMRPCycleOptionMasterID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
	}
}

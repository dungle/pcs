using System;
using System.Data;

namespace PCSComMaterials.Plan.DS
{
	[Serializable]
	public class MTR_MPSCycleOptionDetailVO
	{
		private int mMPSCycleOptionDetailID;
		private bool mOnHand;
		private bool mPurchaseOrder;
		private bool mSaleOrder;
		private bool mDemandWO;
		private bool mSupplyWO;
		private int mMasterLocationID;
		private int mMPSCycleOptionMasterID;

		//HACKED: added by Tuan TQ. 09 Jan 2005. Add more properties(req by Son HT)
		private bool mSafetyStock;
		
		public bool SafetyStock
		{
			get {return mSafetyStock;}
			set {mSafetyStock = value;}
		}

		//End hacked

		public int MPSCycleOptionDetailID
		{
			set { mMPSCycleOptionDetailID = value; }
			get { return mMPSCycleOptionDetailID; }
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
		public bool SupplyWO
		{
			set { mSupplyWO = value; }
			get { return mSupplyWO; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int MPSCycleOptionMasterID
		{
			set { mMPSCycleOptionMasterID = value; }
			get { return mMPSCycleOptionMasterID; }
		}
	}
}

using System;
using System.Data;

namespace PCSComMaterials.Plan.DS
{
	[Serializable]
	public class MTR_CPOVO
	{
		private long mCPOID;
		private Decimal mQuantity;
		private DateTime mStartDate;
		private DateTime mDueDate;
		private int mRefMasterID;
		private int mRefDetailID;
		private int mRefType;
		private Decimal mNetAvailableQuantity;
		private int mCCNID;
		private int mProductID;
		private int mMasterLocationID;
		private int mStockUMID;
		private bool mIsMPS;
		private bool mConverted;
		private int mPOGeneratedID;
		private int mWOGeneratedID;
		private int mMRPCycleOptionMasterID;
		private int mMPSCycleOptionMasterID;
		private long mParentCPOID;
		private Decimal mDemandQuantity;
		private Decimal mSupplyQuantity;
		private bool mIsSafetyStock;

		public long CPOID
		{
			set { mCPOID = value; }
			get { return mCPOID; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public DateTime StartDate
		{
			set { mStartDate = value; }
			get { return mStartDate; }
		}
		public DateTime DueDate
		{
			set { mDueDate = value; }
			get { return mDueDate; }
		}
		public int RefMasterID
		{
			set { mRefMasterID = value; }
			get { return mRefMasterID; }
		}
		public int RefDetailID
		{
			set { mRefDetailID = value; }
			get { return mRefDetailID; }
		}
		public int RefType
		{
			set { mRefType = value; }
			get { return mRefType; }
		}
		public Decimal NetAvailableQuantity
		{
			set { mNetAvailableQuantity = value; }
			get { return mNetAvailableQuantity; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public bool IsMPS
		{
			set { mIsMPS = value; }
			get { return mIsMPS; }
		}
		public bool Converted
		{
			set { mConverted = value; }
			get { return mConverted; }
		}
		public int POGeneratedID
		{
			set { mPOGeneratedID = value; }
			get { return mPOGeneratedID; }
		}
		public int WOGeneratedID
		{
			set { mWOGeneratedID = value; }
			get { return mWOGeneratedID; }
		}
		public int MRPCycleOptionMasterID
		{
			set { mMRPCycleOptionMasterID = value; }
			get { return mMRPCycleOptionMasterID; }
		}
		public int MPSCycleOptionMasterID
		{
			set { mMPSCycleOptionMasterID = value; }
			get { return mMPSCycleOptionMasterID; }
		}
		public long ParentCPOID
		{
			set { mParentCPOID = value; }
			get { return mParentCPOID; }
		}
		public Decimal DemandQuantity
		{
			set { mDemandQuantity = value; }
			get { return mDemandQuantity; }
		}
		public Decimal SupplyQuantity
		{
			set { mSupplyQuantity = value; }
			get { return mSupplyQuantity; }
		}

		public bool IsSafetyStock
		{
			get { return mIsSafetyStock; }
			set { mIsSafetyStock = value; }
		}
	}
}

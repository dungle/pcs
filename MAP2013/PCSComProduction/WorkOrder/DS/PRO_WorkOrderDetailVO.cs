using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_WorkOrderDetailVO
	{
		private int mSaleOrderMasterID;
		private int mWorkOrderDetailID;
		private int mLine;
		private Decimal mOrderQuantity;
		private DateTime mMfgCloseDate;
		private DateTime mDueDate;
		private DateTime mStartDate;
		private int mProductID;
		private int mPriority;
		private int mSaleOrderDetailID;
		private int mWorkOrderMasterID;
		private Decimal mAGC;
		private Decimal mEstCst;
		private int mStockUMID;
		private DateTime mFinCloseDate;
		private string mStatus;
		private int mIncrement;
		private string mDescription;

		public int Increment 
		{
			set
			{
				mIncrement = value;
			}
			get 
			{
				return mIncrement;
			}
		}

		public string Description
		{
			set 
			{
				mDescription = value;
			}
			get 
			{
				return mDescription;
			}
		}
		public int SaleOrderMasterID
		{
			set { mSaleOrderMasterID = value; }
			get { return mSaleOrderMasterID; }
		}
		public int WorkOrderDetailID
		{
			set { mWorkOrderDetailID = value; }
			get { return mWorkOrderDetailID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public Decimal OrderQuantity
		{
			set { mOrderQuantity = value; }
			get { return mOrderQuantity; }
		}
		public DateTime MfgCloseDate
		{
			set { mMfgCloseDate = value; }
			get { return mMfgCloseDate; }
		}
		public DateTime DueDate
		{
			set { mDueDate = value; }
			get { return mDueDate; }
		}
		public DateTime StartDate
		{
			set { mStartDate = value; }
			get { return mStartDate; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}

		public int Priority
		{
			set { mPriority = value; }
			get { return mPriority; }
		}
		public int SaleOrderDetailID
		{
			set { mSaleOrderDetailID = value; }
			get { return mSaleOrderDetailID; }
		}
		public int WorkOrderMasterID
		{
			set { mWorkOrderMasterID = value; }
			get { return mWorkOrderMasterID; }
		}
		public Decimal AGC
		{
			set { mAGC = value; }
			get { return mAGC; }
		}
		public Decimal EstCst
		{
			set { mEstCst = value; }
			get { return mEstCst; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public DateTime FinCloseDate
		{
			set { mFinCloseDate = value; }
			get { return mFinCloseDate; }
		}

		public string Status
		{
			set { mStatus = value; }
			get { return mStatus; }
		}
	}
}

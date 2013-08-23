using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_DCPResultVO
	{
		private int mDCPResultID;
		private int mWORoutingID;
		private DateTime mStartDateTime;
		private DateTime mDueDateTime;
		private Decimal mQuantity;
		private int mDCOptionMasterID;
		private int mCheckPointID;
		private int mProductID;
		private int mCPOID;
		private int mWorkOrderDetailID;
		private int mWorkCenterID;

		public int DCPResultID
		{
			set { mDCPResultID = value; }
			get { return mDCPResultID; }
		}
		public int WORoutingID
		{
			set { mWORoutingID = value; }
			get { return mWORoutingID; }
		}
		public DateTime StartDateTime
		{
			set { mStartDateTime = value; }
			get { return mStartDateTime; }
		}
		public DateTime DueDateTime
		{
			set { mDueDateTime = value; }
			get { return mDueDateTime; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public int DCOptionMasterID
		{
			set { mDCOptionMasterID = value; }
			get { return mDCOptionMasterID; }
		}
		public int CheckPointID
		{
			set { mCheckPointID = value; }
			get { return mCheckPointID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int CPOID
		{
			set { mCPOID = value; }
			get { return mCPOID; }
		}
		public int WorkOrderDetailID
		{
			set { mWorkOrderDetailID = value; }
			get { return mWorkOrderDetailID; }
		}
		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
	}
}

using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_DCPResultMasterVO
	{
		private int mDCPResultMasterID;
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
		private int mRoutingID;
		private decimal mDeliveryQuantity;
        
		public int DCPResultMasterID
		{
			set { mDCPResultMasterID = value; }
			get { return mDCPResultMasterID; }
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
		public int RoutingID
		{
			set { mRoutingID = value; }
			get { return mRoutingID; }
		}

		public decimal DeliveryQuantity
		{
			get { return mDeliveryQuantity; }
			set { mDeliveryQuantity = value; }
		}
	}
}

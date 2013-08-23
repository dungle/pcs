using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_DeliveryScheduleVO
	{
		private int	mDeliveryScheduleID;
		private int	mDeliveryLine;
		private DateTime	mScheduleDate;
		private Decimal	mDeliveryQuantity;
		private Decimal	mReceivedQuantity;
		private Decimal	mAdjustment;
		private int	mPurchaseOrderDetailID;

		public int	DeliveryScheduleID
		{	set { mDeliveryScheduleID = value; }
			get { return mDeliveryScheduleID; }
		}
		public int	DeliveryLine
		{	set { mDeliveryLine = value; }
			get { return mDeliveryLine; }
		}
		public DateTime	ScheduleDate
		{	set { mScheduleDate = value; }
			get { return mScheduleDate; }
		}
		public Decimal	DeliveryQuantity
		{	set { mDeliveryQuantity = value; }
			get { return mDeliveryQuantity; }
		}
		public Decimal	ReceivedQuantity
		{	set { mReceivedQuantity = value; }
			get { return mReceivedQuantity; }
		}
		public Decimal	Adjustment
		{
			set { mAdjustment = value; }
			get { return mAdjustment; }
		}
		public int	PurchaseOrderDetailID
		{	set { mPurchaseOrderDetailID = value; }
			get { return mPurchaseOrderDetailID; }
		}
	}
}

using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PRDeliveryScheduleVO
	{
		private int	mDeliveryScheduleID;
		private int	mDeliveryLine;
		private DateTime	mScheduleDate;
		private int	mDeliveryQuantity;
		private int	mReceivedQuantity;
		private int	mPurchaseRequisitionLinesID;

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
		public int	DeliveryQuantity
		{	set { mDeliveryQuantity = value; }
			get { return mDeliveryQuantity; }
		}
		public int	ReceivedQuantity
		{	set { mReceivedQuantity = value; }
			get { return mReceivedQuantity; }
		}
		public int	PurchaseRequisitionLinesID
		{	set { mPurchaseRequisitionLinesID = value; }
			get { return mPurchaseRequisitionLinesID; }
		}
	}
}

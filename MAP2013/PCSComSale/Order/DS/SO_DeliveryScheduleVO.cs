using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_DeliveryScheduleVO
	{
		private int mDeliveryScheduleID;
		private int mLine;
		private DateTime mRequiredDate;
		private DateTime mPromiseDate;
		private DateTime mScheduleDate;
		private Decimal mDeliveryQuantity;
		private Decimal mDeliveryNo;
		private int mSaleOrderDetailID;

		public int DeliveryScheduleID
		{
			set { mDeliveryScheduleID = value; }
			get { return mDeliveryScheduleID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public DateTime RequiredDate
		{
			set { mRequiredDate = value; }
			get { return mRequiredDate; }
		}
		public DateTime PromiseDate
		{
			set { mPromiseDate = value; }
			get { return mPromiseDate; }
		}
		public DateTime ScheduleDate
		{
			set { mScheduleDate = value; }
			get { return mScheduleDate; }
		}
		public Decimal DeliveryQuantity
		{
			set { mDeliveryQuantity = value; }
			get { return mDeliveryQuantity; }
		}
		public Decimal DeliveryNo
		{
			set { mDeliveryNo = value; }
			get { return mDeliveryNo; }
		}
		public int SaleOrderDetailID
		{
			set { mSaleOrderDetailID = value; }
			get { return mSaleOrderDetailID; }
		}
	}
}

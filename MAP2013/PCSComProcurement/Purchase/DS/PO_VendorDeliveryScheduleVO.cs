using System;
using System.Data;

namespace PCSComProcurement.Purchase.DS
{
	[Serializable]
	public class PO_VendorDeliveryScheduleVO
	{
		private int mVendorDeliverySchedule;
		private int mPartyID;
		private int mCCNID;
		private int mDeliveryType;
		private int mDelHour;
		private int mWeeklyDay;
		private int mMonthlyDate;
		private string mComment;
		private int mDelMin;
		private int mProductID;

		public int VendorDeliverySchedule
		{
			set { mVendorDeliverySchedule = value; }
			get { return mVendorDeliverySchedule; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int DeliveryType
		{
			set { mDeliveryType = value; }
			get { return mDeliveryType; }
		}
		public int DelHour
		{
			set { mDelHour = value; }
			get { return mDelHour; }
		}
		public int WeeklyDay
		{
			set { mWeeklyDay = value; }
			get { return mWeeklyDay; }
		}
		public int MonthlyDate
		{
			set { mMonthlyDate = value; }
			get { return mMonthlyDate; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public int DelMin
		{
			set { mDelMin = value; }
			get { return mDelMin; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
	}
}

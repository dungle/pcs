using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_ShiftPatternVO
	{
		private int mShiftPatternID;
		private int mShiftID;
		private int mCCNID;
		private string mComment;
		private DateTime mEffectDateFrom;
		private DateTime mEffectDateTo;
		private DateTime mWorkTimeFrom;
		private DateTime mWorkTimeTo;
		private DateTime mRegularStopFrom;
		private DateTime mRegularStopTo;
		private DateTime mRefreshingFrom;
		private DateTime mRefreshingTo;
		private DateTime mExtraStopFrom;
		private DateTime mExtraStopTo;

		public int ShiftPatternID
		{
			set { mShiftPatternID = value; }
			get { return mShiftPatternID; }
		}
		public int ShiftID
		{
			set { mShiftID = value; }
			get { return mShiftID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public DateTime EffectDateFrom
		{
			set { mEffectDateFrom = value; }
			get { return mEffectDateFrom; }
		}
		public DateTime EffectDateTo
		{
			set { mEffectDateTo = value; }
			get { return mEffectDateTo; }
		}
		public DateTime WorkTimeFrom
		{
			set { mWorkTimeFrom = value; }
			get { return mWorkTimeFrom; }
		}
		public DateTime WorkTimeTo
		{
			set { mWorkTimeTo = value; }
			get { return mWorkTimeTo; }
		}
		public DateTime RegularStopFrom
		{
			set { mRegularStopFrom = value; }
			get { return mRegularStopFrom; }
		}
		public DateTime RegularStopTo
		{
			set { mRegularStopTo = value; }
			get { return mRegularStopTo; }
		}
		public DateTime RefreshingFrom
		{
			set { mRefreshingFrom = value; }
			get { return mRefreshingFrom; }
		}
		public DateTime RefreshingTo
		{
			set { mRefreshingTo = value; }
			get { return mRefreshingTo; }
		}
		public DateTime ExtraStopFrom
		{
			set { mExtraStopFrom = value; }
			get { return mExtraStopFrom; }
		}
		public DateTime ExtraStopTo
		{
			set { mExtraStopTo = value; }
			get { return mExtraStopTo; }
		}
	}
}

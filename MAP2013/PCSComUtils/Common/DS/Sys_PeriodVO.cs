using System;
using System.Data;

namespace PCSComUtils.Common.DS
{
	[Serializable]
	public class Sys_PeriodVO
	{
		private int mPeriodID;
		private DateTime mFromDate;
		private DateTime mToDate;
		private bool mActivate;

		public int PeriodID
		{
			set { mPeriodID = value; }
			get { return mPeriodID; }
		}
		public DateTime FromDate
		{
			set { mFromDate = value; }
			get { return mFromDate; }
		}
		public DateTime ToDate
		{
			set { mToDate = value; }
			get { return mToDate; }
		}
		public bool Activate
		{
			set { mActivate = value; }
			get { return mActivate; }
		}
	}
}

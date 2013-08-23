using System;
using System.Data;

namespace PCSComUtils.Common.DS
{
	[Serializable]
	public class Sys_PostdateConfigurationVO
	{
		private int mPostdateConfigurationID;
		private int mDayBefore;
		private DateTime mLastUpdated;
		private string mUsername;
		private string mPurpose;

		public int PostdateConfigurationID
		{
			get { return mPostdateConfigurationID; }
			set { mPostdateConfigurationID = value; }
		}

		public int DayBefore
		{
			get { return mDayBefore; }
			set { mDayBefore = value; }
		}

		public DateTime LastUpdated
		{
			get { return mLastUpdated; }
			set { mLastUpdated = value; }
		}

		public string Username
		{
			get { return mUsername; }
			set { mUsername = value; }
		}

		public string Purpose
		{
			get { return mPurpose; }
			set { mPurpose = value; }
		}
	}
}

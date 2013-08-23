using System;
using System.Data;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_WorkingDayDetailVO
	{
		private int mWorkingDayDetailID;
		private DateTime mOffDay;
		private string mComment;
		private int mWorkingDayMasterID;

		public int WorkingDayDetailID
		{
			set { mWorkingDayDetailID = value; }
			get { return mWorkingDayDetailID; }
		}
		public DateTime OffDay
		{
			set { mOffDay = value; }
			get { return mOffDay; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public int WorkingDayMasterID
		{
			set { mWorkingDayMasterID = value; }
			get { return mWorkingDayMasterID; }
		}
	}
}

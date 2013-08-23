using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_ShiftCapacityVO
	{
		private int mShiftCapacityID;
		private int mWCCapacityID;
		private int mShiftID;

		public int ShiftCapacityID
		{
			set { mShiftCapacityID = value; }
			get { return mShiftCapacityID; }
		}
		public int WCCapacityID
		{
			set { mWCCapacityID = value; }
			get { return mWCCapacityID; }
		}
		public int ShiftID
		{
			set { mShiftID = value; }
			get { return mShiftID; }
		}
	}
}

using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_ShiftVO
	{
		private int mShiftID;
		private string mShiftDesc;

		public int ShiftID
		{
			set { mShiftID = value; }
			get { return mShiftID; }
		}
		public string ShiftDesc
		{
			set { mShiftDesc = value; }
			get { return mShiftDesc; }
		}
	}
}

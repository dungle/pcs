using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_VisibleControlVO
	{
		private int mVisibleControlID;
		private string mFormName;
		private string mControlName;
		private string mSubControlName;
		private int mControlGroupID;

		public int VisibleControlID
		{
			set { mVisibleControlID = value; }
			get { return mVisibleControlID; }
		}
		public string FormName
		{
			set { mFormName = value; }
			get { return mFormName; }
		}
		public string ControlName
		{
			set { mControlName = value; }
			get { return mControlName; }
		}
		public string SubControlName
		{
			set { mSubControlName = value; }
			get { return mSubControlName; }
		}
		public int ControlGroupID
		{
			set { mControlGroupID = value; }
			get { return mControlGroupID; }
		}
	}
}

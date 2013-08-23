using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_HiddenControlsVO
	{
		private int mHiddenControlsID;
		private string mFormName;
		private string mControlName;
		private string mSubControlName;

		public int HiddenControlsID
		{
			set { mHiddenControlsID = value; }
			get { return mHiddenControlsID; }
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
	}
}

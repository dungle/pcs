using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_ControlGroupVO
	{
		private int mControlGroupID;
		private string mControlGroupText;

		public int ControlGroupID
		{
			set { mControlGroupID = value; }
			get { return mControlGroupID; }
		}
		public string ControlGroupText
		{
			set { mControlGroupText = value; }
			get { return mControlGroupText; }
		}
	}
}

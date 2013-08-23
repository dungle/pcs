using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_HiddenControls_RoleVO
	{
		private int mHiddenControls_RoleID;
		private int mHiddenControlsID;
		private int mRoleID;

		public int HiddenControls_RoleID
		{
			set { mHiddenControls_RoleID = value; }
			get { return mHiddenControls_RoleID; }
		}
		public int HiddenControlsID
		{
			set { mHiddenControlsID = value; }
			get { return mHiddenControlsID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
	}
}

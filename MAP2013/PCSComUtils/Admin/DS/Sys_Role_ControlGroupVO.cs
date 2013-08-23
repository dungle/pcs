using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_Role_ControlGroupVO
	{
		private int mRole_ControlGroupID;
		private int mRoleID;
		private int mControlGroupID;

		public int Role_ControlGroupID
		{
			set { mRole_ControlGroupID = value; }
			get { return mRole_ControlGroupID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
		public int ControlGroupID
		{
			set { mControlGroupID = value; }
			get { return mControlGroupID; }
		}
	}
}

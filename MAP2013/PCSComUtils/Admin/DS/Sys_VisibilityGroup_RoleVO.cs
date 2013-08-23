using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_VisibilityGroup_RoleVO
	{
		private int mVisibilityGroup_RoleID;
		private int mGroupID;
		private int mRoleID;

		public int VisibilityGroup_RoleID
		{
			set { mVisibilityGroup_RoleID = value; }
			get { return mVisibilityGroup_RoleID; }
		}
		public int GroupID
		{
			set { mGroupID = value; }
			get { return mGroupID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
	}
}

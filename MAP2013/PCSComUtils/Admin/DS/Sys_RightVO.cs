using System;


namespace PCSComUtils.Admin.DS
{
	
	[Serializable]
	public class Sys_RightVO
	{
		private int mRightID;
		private int mPermission;
		private int mRoleID;
		private int mMenu_EntryID;

		public int RightID
		{
			set { mRightID = value; }
			get { return mRightID; }
		}
		public int Permission
		{
			set { mPermission = value; }
			get { return mPermission; }
		}
		public int Menu_EntryID
		{
			set { mMenu_EntryID = value; }
			get { return mMenu_EntryID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
	}
}

using System;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_UserToRoleVO
	{
		private int mid;
		private int mUserID;
		private int mRoleID;

		public int id
		{
			set { mid = value; }
			get { return mid; }
		}
		public int UserID
		{
			set { mUserID = value; }
			get { return mUserID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
	}
}

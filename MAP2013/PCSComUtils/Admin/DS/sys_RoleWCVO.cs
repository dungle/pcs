using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class sys_RoleWCVO
	{
		private int mRoleWCID;
		private int mRoleID;
		private int mWorkCenterID;

		public int RoleWCID
		{
			set { mRoleWCID = value; }
			get { return mRoleWCID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
	}
}

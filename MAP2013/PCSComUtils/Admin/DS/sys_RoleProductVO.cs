using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class sys_RoleProductVO
	{
		private int mRoleProductID;
		private int mRoleID;
		private int mProductID;

		public int RoleProductID
		{
			set { mRoleProductID = value; }
			get { return mRoleProductID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
	}
}

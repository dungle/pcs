using System;


namespace PCSComUtils.Admin.DS
{
	
	[Serializable]
	public class Sys_RoleVO
	{
		private int mRoleID;
		private string mName;
		private string mDescription;

		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class sys_RolePartyVO
	{
		private int mRolePartyID;
		private int mRoleID;
		private int mPartyID;

		public int RolePartyID
		{
			set { mRolePartyID = value; }
			get { return mRolePartyID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
	}
}

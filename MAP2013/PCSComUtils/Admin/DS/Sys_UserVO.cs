using System;

namespace PCSComUtils.Admin.DS
{
	
	[Serializable]
	public class Sys_UserVO
	{
		private int mUserID;
		private string mUserName;
		private string mPwd;
		private string mName;
		private DateTime mCreatedDate;
		private string mDescription;
		private int mCCNID;
		private int mEmployeeID;
		private int mMasterLocationID;
		private bool mActivate;
		private DateTime mExpiredDate;

		public int UserID
		{
			set { mUserID = value; }
			get { return mUserID; }
		}
		public string UserName
		{
			set { mUserName = value; }
			get { return mUserName; }
		}
		public string Pwd
		{
			set { mPwd = value; }
			get { return mPwd; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public DateTime CreatedDate
		{
			set { mCreatedDate = value; }
			get { return mCreatedDate; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int EmployeeID
		{
			set { mEmployeeID = value; }
			get { return mEmployeeID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public bool Activate
		{
			set { mActivate = value; }
			get { return mActivate; }
		}
		public DateTime ExpiredDate
		{
			set { mExpiredDate = value; }
			get { return mExpiredDate; }
		}
	}
}

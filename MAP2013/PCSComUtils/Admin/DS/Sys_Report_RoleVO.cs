using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_Report_RoleVO
	{
		private int mReport_RoleID;
		private string mReportID;
		private int mRoleID;

		public int Report_RoleID
		{
			set { mReport_RoleID = value; }
			get { return mReport_RoleID; }
		}
		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public int RoleID
		{
			set { mRoleID = value; }
			get { return mRoleID; }
		}
	}
}

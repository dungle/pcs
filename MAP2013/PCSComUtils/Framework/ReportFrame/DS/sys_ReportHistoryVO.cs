using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportHistoryVO
	{
		private int mHistoryID;
		private string mUserID;
		private DateTime mExecDateTime;
		private string mReportID;
		private string mTableName;

		public int HistoryID
		{
			set { mHistoryID = value; }
			get { return mHistoryID; }
		}
		public string UserID
		{
			set { mUserID = value; }
			get { return mUserID; }
		}
		public DateTime ExecDateTime
		{
			set { mExecDateTime = value; }
			get { return mExecDateTime; }
		}
		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public string TableName
		{
			set { mTableName = value; }
			get { return mTableName; }
		}
	}
}

using System;
using System.Data;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	[Serializable]
	public class Sys_FieldGroupDetailVO
	{
		private int mFieldGroupDetailID;
		private int mFieldGroupID;
		private string mReportID;
		private string mFieldName;

		public int FieldGroupDetailID
		{
			set { mFieldGroupDetailID = value; }
			get { return mFieldGroupDetailID; }
		}
		public int FieldGroupID
		{
			set { mFieldGroupID = value; }
			get { return mFieldGroupID; }
		}
		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public string FieldName
		{
			set { mFieldName = value; }
			get { return mFieldName; }
		}
	}
}


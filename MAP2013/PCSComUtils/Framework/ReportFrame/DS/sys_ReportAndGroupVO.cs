using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportAndGroupVO
	{
		private string mReportAndGroupID;
		private string mGroupID;
		private string mReportID;
		private int mReportOrder;

		public string ReportAndGroupID
		{
			get { return this.mReportAndGroupID; }
			set { this.mReportAndGroupID = value; }
		}

		public string GroupID
		{
			set { mGroupID = value; }
			get { return mGroupID; }
		}
		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public int ReportOrder
		{
			set { mReportOrder = value; }
			get { return mReportOrder; }
		}
	}
}

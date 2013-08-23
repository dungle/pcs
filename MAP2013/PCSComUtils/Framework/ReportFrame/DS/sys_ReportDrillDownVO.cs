using System;
using System.Data;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportDrillDownVO
	{
		private string mMasterReportID;
		private string mDetailReportID;
		private string mMasterPara;
		private string mDetailPara;
		private Boolean mFromColumn;
		private int mParaOrder;

		public string MasterReportID
		{
			set { mMasterReportID = value; }
			get { return mMasterReportID; }
		}
		public string DetailReportID
		{
			set { mDetailReportID = value; }
			get { return mDetailReportID; }
		}
		public string MasterPara
		{
			set { mMasterPara = value; }
			get { return mMasterPara; }
		}
		public string DetailPara
		{
			set { mDetailPara = value; }
			get { return mDetailPara; }
		}
		public Boolean FromColumn
		{
			set { mFromColumn = value; }
			get { return mFromColumn; }
		}
		public int ParaOrder
		{
			set { mParaOrder = value; }
			get { return mParaOrder; }
		}
	}
}

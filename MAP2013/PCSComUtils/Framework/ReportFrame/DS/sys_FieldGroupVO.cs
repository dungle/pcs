using System;
using System.Data;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	[Serializable]
	public class Sys_FieldGroupVO
	{
		private int mFieldGroupID;
		private string mGroupCode;
		private int mGroupOrder;
		private int mGroupLevel;
		private string mGroupNameVN;
		private string mGroupNameEN;
		private string mGroupNameJP;
		private string mReportID;
		private int mParentFieldGroupID;

		public int FieldGroupID
		{
			set { mFieldGroupID = value; }
			get { return mFieldGroupID; }
		}
		public string GroupCode
		{
			set { mGroupCode = value; }
			get { return mGroupCode; }
		}
		public int GroupOrder
		{
			set { mGroupOrder = value; }
			get { return mGroupOrder; }
		}
		public int GroupLevel
		{
			set { mGroupLevel = value; }
			get { return mGroupLevel; }
		}
		public string GroupNameVN
		{
			set { mGroupNameVN = value; }
			get { return mGroupNameVN; }
		}
		public string GroupNameEN
		{
			set { mGroupNameEN = value; }
			get { return mGroupNameEN; }
		}
		public string GroupNameJP
		{
			set { mGroupNameJP = value; }
			get { return mGroupNameJP; }
		}
		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public int ParentFieldGroupID
		{
			set { mParentFieldGroupID = value; }
			get { return mParentFieldGroupID; }
		}
	}
}

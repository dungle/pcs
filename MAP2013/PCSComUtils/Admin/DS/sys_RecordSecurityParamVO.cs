using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class sys_RecordSecurityParamVO
	{
		private int mRecordSecurityParamID;
		private string mSourceTableName;
		private string mMenuName;
		private string mSecurityTableName;

		public int RecordSecurityParamID
		{
			set { mRecordSecurityParamID = value; }
			get { return mRecordSecurityParamID; }
		}
		public string SourceTableName
		{
			set { mSourceTableName = value; }
			get { return mSourceTableName; }
		}
		public string MenuName
		{
			set { mMenuName = value; }
			get { return mMenuName; }
		}
		public string SecurityTableName
		{
			set { mSecurityTableName = value; }
			get { return mSecurityTableName; }
		}
	}
}

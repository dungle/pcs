using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportHistoryParaVO
	{
		private int mHistoryID;
		private string mParaName;
		private string mParaValue;
		private string mTagValue;
		private string mFilterField1Value;
		private string mFilterField2Value;

		public int HistoryID
		{
			set { mHistoryID = value; }
			get { return mHistoryID; }
		}
		public string ParaName
		{
			set { mParaName = value; }
			get { return mParaName; }
		}
		public string ParaValue
		{
			set { mParaValue = value; }
			get { return mParaValue; }
		}
		public string TagValue
		{
			set { mTagValue = value; }
			get { return mTagValue; }
		}
		public string FilterField1Value
		{
			set { mFilterField1Value = value; }
			get { return mFilterField1Value; }
		}
		public string FilterField2Value
		{
			set { mFilterField2Value = value; }
			get { return mFilterField2Value; }
		}
	}
}

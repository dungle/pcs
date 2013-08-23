using System;
using System.Data;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	[Serializable]
	public class Sys_PrintConfigurationVO
	{
		private int mPrintConfigurationID;
		private string mFormName;
		private string mFileName;
		private int mCopies;
		private string mDescription;
		private bool mPrintable;
		private string mFunctionName;
		private string mReportName;

		public int PrintConfigurationID
		{
			set { mPrintConfigurationID = value; }
			get { return mPrintConfigurationID; }
		}
		public string FormName
		{
			set { mFormName = value; }
			get { return mFormName; }
		}
		public string FileName
		{
			set { mFileName = value; }
			get { return mFileName; }
		}
		public int Copies
		{
			set { mCopies = value; }
			get { return mCopies; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public bool Printable
		{
			set { mPrintable = value; }
			get { return mPrintable; }
		}
		public string FunctionName
		{
			set { mFunctionName = value; }
			get { return mFunctionName; }
		}
		public string ReportName
		{
			set { mReportName = value; }
			get { return mReportName; }
		}
	}
}

using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportParaVO
	{
		private string mReportID;
		private int mParaOrder;
		private string mParaName;
		private string mParaCaption;
		private int mDataType;
		private int mWidth;
		private Boolean mOptional;
		private Boolean mTagValue;
		private Boolean mSameRow;
		private string mDefaultValue;
		private string mItems;
		private string mFromTable;
		private string mFromField;
		private string mFilterField1;
		private int mFilterField1Width;
		private string mFilterField2;
		private int mFilterField2Width;
		private string mSQLCLause;
		private string mWhereClause;
		private bool mMultiSelection;

		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public int ParaOrder
		{
			set { mParaOrder = value; }
			get { return mParaOrder; }
		}
		public string ParaName
		{
			set { mParaName = value; }
			get { return mParaName; }
		}
		public string ParaCaption
		{
			set { mParaCaption = value; }
			get { return mParaCaption; }
		}
		public int DataType
		{
			set { mDataType = value; }
			get { return mDataType; }
		}
		public int Width
		{
			set { mWidth = value; }
			get { return mWidth; }
		}
		public Boolean Optional
		{
			set { mOptional = value; }
			get { return mOptional; }
		}
		public Boolean TagValue
		{
			set { mTagValue = value; }
			get { return mTagValue; }
		}
		public Boolean SameRow
		{
			set { mSameRow = value; }
			get { return mSameRow; }
		}
		public string DefaultValue
		{
			set { mDefaultValue = value; }
			get { return mDefaultValue; }
		}
		public string Items
		{
			set { mItems = value; }
			get { return mItems; }
		}
		public string FromTable
		{
			set { mFromTable = value; }
			get { return mFromTable; }
		}
		public string FromField
		{
			set { mFromField = value; }
			get { return mFromField; }
		}
		public string FilterField1
		{
			set { mFilterField1 = value; }
			get { return mFilterField1; }
		}
		public int FilterField1Width
		{
			set { mFilterField1Width = value; }
			get { return mFilterField1Width; }
		}
		public string FilterField2
		{
			set { mFilterField2 = value; }
			get { return mFilterField2; }
		}
		public int FilterField2Width
		{
			set { mFilterField2Width = value; }
			get { return mFilterField2Width; }
		}
		public string SQLCLause
		{
			set { mSQLCLause = value; }
			get { return mSQLCLause; }
		}
		public string WhereClause
		{
			set { mWhereClause = value; }
			get { return mWhereClause; }
		}

		public bool MultiSelection
		{
			get { return mMultiSelection; }
			set { mMultiSelection = value; }
		}
	}
}

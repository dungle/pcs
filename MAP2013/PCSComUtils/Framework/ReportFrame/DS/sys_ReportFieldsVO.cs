using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportFieldsVO
	{
		private string mReportID;
		private int mFieldOrder;
		private string mFieldName;
		private string mFieldCaption;
		private string mFieldCaptionEN;
		private string mFieldCaptionVN;
		private string mFieldCaptionJP;
		private string mFont;
		private Boolean mVisisble;
		private int mType;
		private Boolean mPrintPreview;
		private int mWidth;
		private string mFormat;
		private int mSort;
		private Boolean mGroupBy;
		private Boolean mBottomGroup;
		private Boolean mSumTopPage;
		private Boolean mSumBottomPage;
		private Boolean mSumTopReport;
		private Boolean mSumBottomReport;
		private int mDataType;
		private int mAlign = PCSComUtils.Common.PCSAligmentType.NONE;

		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public int FieldOrder
		{
			set { mFieldOrder = value; }
			get { return mFieldOrder; }
		}
		public string FieldName
		{
			set { mFieldName = value; }
			get { return mFieldName; }
		}
		public string FieldCaption
		{
			set { mFieldCaption = value; }
			get { return mFieldCaption; }
		}
		public string FieldCaptionEN
		{
			set { mFieldCaptionEN = value; }
			get { return mFieldCaptionEN; }
		}
		public string FieldCaptionVN
		{
			set { mFieldCaptionVN = value; }
			get { return mFieldCaptionVN; }
		}
		public string FieldCaptionJP
		{
			set { mFieldCaptionJP = value; }
			get { return mFieldCaptionJP; }
		}
		public string Font
		{
			set { mFont = value; }
			get { return mFont; }
		}
		public Boolean Visisble
		{
			set { mVisisble = value; }
			get { return mVisisble; }
		}
		public int Type
		{
			set { mType = value; }
			get { return mType; }
		}
		public Boolean PrintPreview
		{
			set { mPrintPreview = value; }
			get { return mPrintPreview; }
		}
		public int Width
		{
			set { mWidth = value; }
			get { return mWidth; }
		}
		public string Format
		{
			set { mFormat = value; }
			get { return mFormat; }
		}
		public int Sort
		{
			set { mSort = value; }
			get { return mSort; }
		}
		public Boolean GroupBy
		{
			set { mGroupBy = value; }
			get { return mGroupBy; }
		}
		public Boolean BottomGroup
		{
			set { mBottomGroup = value; }
			get { return mBottomGroup; }
		}
		public Boolean SumTopPage
		{
			set { mSumTopPage = value; }
			get { return mSumTopPage; }
		}
		public Boolean SumBottomPage
		{
			set { mSumBottomPage = value; }
			get { return mSumBottomPage; }
		}
		public Boolean SumTopReport
		{
			set { mSumTopReport = value; }
			get { return mSumTopReport; }
		}
		public Boolean SumBottomReport
		{
			set { mSumBottomReport = value; }
			get { return mSumBottomReport; }
		}
		public int DataType
		{
			set { mDataType = value; }
			get { return mDataType; }
		}

		public int Align
		{
			get { return mAlign; }
			set { mAlign = value; }
		}
	}
}

using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportVO
	{
		private string mReportID;
		private string mReportName;
		private string mDescription;
		private string mISOCode;
		private string mReportFile;
		private string mReportType;
		private string mCommand;
		private int mMarginTop;
		private int mMarginLeft;
		private int mMarginRight;
		private int mMarginBottom;
		private int mMarginGutter;
		private Boolean mMarginGutterPos;
		private Boolean mOrientation;
		private int mPaperSize;
		private int mTableBorder;
		private string mSignatures = string.Empty;
		private string mFontReportHeader;
		private string mFontParameter;
		private string mFontPageHeader;
		private string mFontGroupBy;
		private string mFontDetail;
		private string mFontPageFooter;
		private string mFontReportFooter;
		
		/// HACKED: Thachnn: modify the sys_report table
		private Boolean mUseTemplate;
		private string mTemplateFile;
		


		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}

		public string ReportName
		{
			set { mReportName = value; }
			get { return mReportName; }
		}

		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}

		public string ISOCode
		{
			set { mISOCode = value; }
			get { return mISOCode; }
		}

		public string ReportFile
		{
			set { mReportFile = value; }
			get { return mReportFile; }
		}

		public string ReportType
		{
			set { mReportType = value; }
			get { return mReportType; }
		}

		public string Command
		{
			set { mCommand = value; }
			get { return mCommand; }
		}

		public int MarginTop
		{
			set { mMarginTop = value; }
			get { return mMarginTop; }
		}

		public int MarginLeft
		{
			set { mMarginLeft = value; }
			get { return mMarginLeft; }
		}

		public int MarginRight
		{
			set { mMarginRight = value; }
			get { return mMarginRight; }
		}

		public int MarginBottom
		{
			set { mMarginBottom = value; }
			get { return mMarginBottom; }
		}

		public int MarginGutter
		{
			set { mMarginGutter = value; }
			get { return mMarginGutter; }
		}

		public Boolean MarginGutterPos
		{
			set { mMarginGutterPos = value; }
			get { return mMarginGutterPos; }
		}

		public Boolean Orientation
		{
			set { mOrientation = value; }
			get { return mOrientation; }
		}

		public int PaperSize
		{
			set { mPaperSize = value; }
			get { return mPaperSize; }
		}

		public int TableBorder
		{
			set { mTableBorder = value; }
			get { return mTableBorder; }
		}

		public string Signatures
		{
			set { mSignatures = value; }
			get { return mSignatures; }
		}

		public string FontReportHeader
		{
			set { mFontReportHeader = value; }
			get { return mFontReportHeader; }
		}

		public string FontParameter
		{
			set { mFontParameter = value; }
			get { return mFontParameter; }
		}

		public string FontPageHeader
		{
			set { mFontPageHeader = value; }
			get { return mFontPageHeader; }
		}

		public string FontGroupBy
		{
			set { mFontGroupBy = value; }
			get { return mFontGroupBy; }
		}

		public string FontDetail
		{
			set { mFontDetail = value; }
			get { return mFontDetail; }
		}

		public string FontPageFooter
		{
			set { mFontPageFooter = value; }
			get { return mFontPageFooter; }
		}

		public string FontReportFooter
		{
			set { mFontReportFooter = value; }
			get { return mFontReportFooter; }
		}

		
		/// HACKED: Thachnn: modify the sys_report table
		public Boolean UseTemplate
		{
			set { mUseTemplate = value; }
			get { return mUseTemplate; }
		}
		public string TemplateFile
		{
			set { mTemplateFile = value; }
			get { return mTemplateFile; }
		}



	}
}
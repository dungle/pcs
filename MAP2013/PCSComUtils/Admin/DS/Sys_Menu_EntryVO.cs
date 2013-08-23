using System;


namespace PCSComUtils.Admin.DS
{
	
	[Serializable]
	public class Sys_Menu_EntryVO
	{
		private int mMenu_EntryID;
		private string mShortcut;
		private string mParent_Shortcut;
		private int mButton_Caption;
		private string mText_CaptionDefault;
		private string mText_Caption_VI_VN;
		private string mText_Caption_EN_US;
		private string mText_Caption_JA_JP;
		private string mText_Caption_Language_Default;
		private int mParent_Child;
		private string mDescription;
		private string mFormLoad;
		private int mType;
		private string mTransNoFieldName;
		private string mReportID;
		#region Fields for menu icon
		private int mCollapsedImage;
		private int mExpandedImage;
		#endregion
		#region Fields for Prefix and Transaction
		private string mPrefix;
		private string mTransFormat;
		private int mIsTransaction;
		private int mIsUserCreated;
		private string mTableName;
		#endregion


		public int Menu_EntryID
		{
			set { mMenu_EntryID = value; }
			get { return mMenu_EntryID; }
		}
		public string Shortcut
		{
			set { mShortcut = value; }
			get { return mShortcut; }
		}
		public string Parent_Shortcut
		{
			set { mParent_Shortcut = value; }
			get { return mParent_Shortcut; }
		}
		public int Button_Caption
		{
			set { mButton_Caption = value; }
			get { return mButton_Caption; }
		}
		public string Text_CaptionDefault
		{
			set { mText_CaptionDefault = value; }
			get { return mText_CaptionDefault; }
		}
		public string Text_Caption_Vi_VN
		{
			set { mText_Caption_VI_VN = value; }
			get { return mText_Caption_VI_VN; }
		}
		public string Text_Caption_EN_US
		{
			set { mText_Caption_EN_US = value; }
			get { return mText_Caption_EN_US; }
		}
		public string Text_Caption_JA_JP
		{
			set { mText_Caption_JA_JP = value; }
			get { return mText_Caption_JA_JP; }
		}
		public string Text_Caption_Language_Default
		{
			set { mText_Caption_Language_Default = value; }
			get { return mText_Caption_Language_Default; }
		}
		public int Parent_Child
		{
			set { mParent_Child = value; }
			get { return mParent_Child; }
		}
		public string FormLoad
		{
			set { mFormLoad = value; }
			get { return mFormLoad; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public string ReportID
		{
			set { mReportID = value; }
			get { return mReportID; }
		}
		public int Type
		{
			set { mType = value; }
			get { return mType; }
		}
		#region Fields for menu icon
		public int CollapsedImage
		{
			set { mCollapsedImage = value; }
			get { return mCollapsedImage; }
		}
		public int ExpandedImage
		{
			set { mExpandedImage = value; }
			get { return mExpandedImage; }
		}
		#endregion
		#region Fields for Prefix and Transaction
		public string Prefix
		{
			set { mPrefix = value; }
			get { return mPrefix; }
		}
		public string TransFormat
		{
			set { mTransFormat = value; }
			get { return mTransFormat; }
		}
		public int IsTransaction
		{
			set { mIsTransaction = value; }
			get { return mIsTransaction; }
		}
		public int IsUserCreated
		{
			set { mIsUserCreated = value; }
			get { return mIsUserCreated; }
		}
		public string TableName
		{
			set { mTableName = value; }
			get { return mTableName; }
		}

		public string TransNoFieldName
		{
			get { return mTransNoFieldName; }
			set { mTransNoFieldName = value; }
		}

		#endregion

	}
}

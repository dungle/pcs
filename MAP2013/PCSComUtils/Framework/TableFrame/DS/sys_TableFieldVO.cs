using System;


namespace PCSComUtils.Framework.TableFrame.DS
{
	
	[Serializable]
	public class sys_TableFieldVO
	{
		private int		mTableFieldID;
		private int		mTableID;
		private string	mFieldName;
		private string	mCaptionJP;
		private string	mCaptionVN;
		private string	mCaptionEN;
		private string	mCaption;
		private bool	mInvisible;
		private int		mCharacterCase;
		private int		mAlign;
		private int		mWidth;
		private int		mSortType;
		private string	mFormats;
		private bool	mReadOnly;
		private bool	mNotAllowNull;
		private bool	mIdentityColumn;
		private bool	mUniqueColumn;
		private string	mItems;
		private string	mFromTable;
		private string	mFromField;
		private string	mFilterField1;
		private string	mFilterField2;
		private string	mFilterField3;
		private int		mFieldOrder;
		private string	mCaptionJP1;
		private string	mCaptionVN1;
		private string	mCaptionEN1;
		private int		mAlign1;
		private int		mWidth1;
		private string	mFormats1;
		private string	mCaptionJP2;
		private string	mCaptionVN2;
		private string	mCaptionEN2;
		private int		mAlign2;
		private int		mWidth2;
		private string	mFormats2;
		private string	mCaptionJP3;
		private string	mCaptionVN3;
		private string	mCaptionEN3;
		private int		mAlign3;
		private int		mWidth3;
		private string	mFormats3;

		public int	TableFieldID
		{	set { mTableFieldID = value; }
			get { return mTableFieldID; }
		}
		public int	TableID
		{	set { mTableID = value; }
			get { return mTableID; }
		}
		public string	FieldName
		{	set { mFieldName = value; }
			get { return mFieldName; }
		}
		public string	CaptionJP
		{	set { mCaptionJP = value; }
			get { return mCaptionJP; }
		}
		public string	CaptionVN
		{	set { mCaptionVN = value; }
			get { return mCaptionVN; }
		}
		public string	CaptionEN
		{	set { mCaptionEN = value; }
			get { return mCaptionEN; }
		}
		public string	Caption
		{	set { mCaption = value; }
			get { return mCaption; }
		}
		public bool	Invisible
		{	set { mInvisible = value; }
			get { return mInvisible; }
		}
		public int	CharacterCase
		{	set { mCharacterCase = value; }
			get { return mCharacterCase; }
		}
		public int	Align
		{	set { mAlign = value; }
			get { return mAlign; }
		}
		public int	Width
		{	set { mWidth = value; }
			get { return mWidth; }
		}
		public int	SortType
		{	set { mSortType = value; }
			get { return mSortType; }
		}
		public string	Formats
		{	set { mFormats = value; }
			get { return mFormats; }
		}
		public bool	ReadOnly
		{	set { mReadOnly = value; }
			get { return mReadOnly; }
		}
		public bool	NotAllowNull
		{	set { mNotAllowNull = value; }
			get { return mNotAllowNull; }
		}
		public bool	IdentityColumn
		{	set { mIdentityColumn = value; }
			get { return mIdentityColumn; }
		}
		public bool	UniqueColumn
		{	set { mUniqueColumn = value; }
			get { return mUniqueColumn; }
		}
		public string	Items
		{	set { mItems = value; }
			get { return mItems; }
		}
		public string	FromTable
		{	set { mFromTable = value; }
			get { return mFromTable; }
		}
		public string	FromField
		{	set { mFromField = value; }
			get { return mFromField; }
		}
		public string	FilterField1
		{	set { mFilterField1 = value; }
			get { return mFilterField1; }
		}
		public string	FilterField2
		{	set { mFilterField2 = value; }
			get { return mFilterField2; }
		}
		public string	FilterField3
		{
			set { mFilterField3 = value; }
			get { return mFilterField3; }
		}
		public int	FieldOrder
		{	set { mFieldOrder = value; }
			get { return mFieldOrder; }
		}
		public string	CaptionJP1
		{
			set { mCaptionJP1 = value; }
			get { return mCaptionJP1; }
		}
		public string	CaptionVN1
		{
			set { mCaptionVN1 = value; }
			get { return mCaptionVN1; }
		}
		public string	CaptionEN1
		{
			set { mCaptionEN1 = value; }
			get { return mCaptionEN1; }
		}
		public int	Align1
		{
			set { mAlign1 = value; }
			get { return mAlign1; }
		}
		public int	Width1
		{
			set { mWidth1 = value; }
			get { return mWidth1; }
		}
		public string	Formats1
		{
			set { mFormats1 = value; }
			get { return mFormats1; }
		}
		public string	CaptionJP2
		{
			set { mCaptionJP2 = value; }
			get { return mCaptionJP2; }
		}
		public string	CaptionVN2
		{
			set { mCaptionVN2 = value; }
			get { return mCaptionVN2; }
		}
		public string	CaptionEN2
		{
			set { mCaptionEN2 = value; }
			get { return mCaptionEN2; }
		}
		public int	Align2
		{
			set { mAlign2 = value; }
			get { return mAlign2; }
		}
		public int	Width2
		{
			set { mWidth2 = value; }
			get { return mWidth2; }
		}
		public string	Formats2
		{
			set { mFormats2 = value; }
			get { return mFormats2; }
		}
		public string	CaptionJP3
		{
			set { mCaptionJP3 = value; }
			get { return mCaptionJP3; }
		}
		public string	CaptionVN3
		{
			set { mCaptionVN3 = value; }
			get { return mCaptionVN3; }
		}
		public string	CaptionEN3
		{
			set { mCaptionEN3 = value; }
			get { return mCaptionEN3; }
		}
		public int	Align3
		{
			set { mAlign3 = value; }
			get { return mAlign3; }
		}
		public int	Width3
		{
			set { mWidth3 = value; }
			get { return mWidth3; }
		}
		public string	Formats3
		{
			set { mFormats3 = value; }
			get { return mFormats3; }
		}

	}
}

using System;


namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_BINVO
	{
		private int mBinID;
		private string mCode;
		private string mName;
		private Decimal mLength;
		private Decimal mWidth;
		private Decimal mHeight;
		private int mLocationID;
		private int mLengthUnitID;
		private int mHeightUnitID;
		private int mWidthUnitID;

		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public Decimal Length
		{
			set { mLength = value; }
			get { return mLength; }
		}
		public Decimal Width
		{
			set { mWidth = value; }
			get { return mWidth; }
		}
		public Decimal Height
		{
			set { mHeight = value; }
			get { return mHeight; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int LengthUnitID
		{
			set { mLengthUnitID = value; }
			get { return mLengthUnitID; }
		}
		public int HeightUnitID
		{
			set { mHeightUnitID = value; }
			get { return mHeightUnitID; }
		}
		public int WidthUnitID
		{
			set { mWidthUnitID = value; }
			get { return mWidthUnitID; }
		}
	}
}

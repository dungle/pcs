using System;


namespace PCSComUtils.Framework.TableFrame.DS
{
	
	[Serializable]
	public class sys_TableVO
	{
		private int	mTableID;
		private string	mCode;
		private string	mTableName;
		private string	mTableOrView;
		private int	mHeight;
		private bool mIsViewOnly;

		public int	TableID
		{	set { mTableID = value; }
			get { return mTableID; }
		}
		public string	Code
		{	set { mCode = value; }
			get { return mCode; }
		}
		public string	TableName
		{	set { mTableName = value; }
			get { return mTableName; }
		}
		public string	TableOrView
		{	set { mTableOrView = value; }
			get { return mTableOrView; }
		}
		public int	Height
		{	set { mHeight = value; }
			get { return mHeight; }
		}
		public bool	IsViewOnly
		{
			set { mIsViewOnly = value; }
			get { return mIsViewOnly; }
		}
	}
}

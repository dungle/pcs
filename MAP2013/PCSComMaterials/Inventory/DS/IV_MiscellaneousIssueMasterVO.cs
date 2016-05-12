using System;
using System.Data;

namespace PCSComMaterials.Inventory.DS
{
	[Serializable]
	public class IV_MiscellaneousIssueMasterVO
	{
		private int mMiscellaneousIssueMasterID;
		private DateTime mPostDate;
		private string mComment;
		private string mTransNo;
		private string mUserName;
		private DateTime mLastChange;
		private int mCCNID;
		private int mDesBinID;
		private int mSourceBinID;
		private int mDesLocationID;
		private int mSourceLocationID;
		private int mDesMasLocationID;
		private int mSourceMasLocationID;
		private int mPartyID;
		private int mIssuePurposeID;

		public int MiscellaneousIssueMasterID
		{
			set { mMiscellaneousIssueMasterID = value; }
			get { return mMiscellaneousIssueMasterID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public string TransNo
		{
			set { mTransNo = value; }
			get { return mTransNo; }
		}
		public string UserName
		{
			set { mUserName = value; }
			get { return mUserName; }
		}
		public DateTime LastChange
		{
			set { mLastChange = value; }
			get { return mLastChange; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int DesBinID
		{
			set { mDesBinID = value; }
			get { return mDesBinID; }
		}
		public int SourceBinID
		{
			set { mSourceBinID = value; }
			get { return mSourceBinID; }
		}
		public int DesLocationID
		{
			set { mDesLocationID = value; }
			get { return mDesLocationID; }
		}
		public int SourceLocationID
		{
			set { mSourceLocationID = value; }
			get { return mSourceLocationID; }
		}
		public int DesMasLocationID
		{
			set { mDesMasLocationID = value; }
			get { return mDesMasLocationID; }
		}
		public int SourceMasLocationID
		{
			set { mSourceMasLocationID = value; }
			get { return mSourceMasLocationID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int IssuePurposeID
		{
			set { mIssuePurposeID = value; }
			get { return mIssuePurposeID; }
		}

        public int DestroyApproved { get; set; }
    }
}

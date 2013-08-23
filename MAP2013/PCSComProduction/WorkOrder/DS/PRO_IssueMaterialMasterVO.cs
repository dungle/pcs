using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_IssueMaterialMasterVO
	{
		private int mMasterLocationID;
		private int mIssueMaterialMasterID;
		private DateTime mPostDate;
		private string mIssueNo;
		private int mCCNID;
		private int mProductID;
		private int mWorkOrderMasterID;
		private int mWorkOrderDetailID;
		private int mToLocationID;
		private int mToBinID;
		private int mShiftID;
		private int mIssuePurposeID;

		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int IssueMaterialMasterID
		{
			set { mIssueMaterialMasterID = value; }
			get { return mIssueMaterialMasterID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string IssueNo
		{
			set { mIssueNo = value; }
			get { return mIssueNo; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int WorkOrderMasterID
		{
			set { mWorkOrderMasterID = value; }
			get { return mWorkOrderMasterID; }
		}
		public int WorkOrderDetailID
		{
			set { mWorkOrderDetailID = value; }
			get { return mWorkOrderDetailID; }
		}
		public int ToLocationID
		{
			set { mToLocationID = value; }
			get { return mToLocationID; }
		}
		public int ToBinID
		{
			set { mToBinID = value; }
			get { return mToBinID; }
		}
		public int ShiftID
		{
			set { mShiftID = value; }
			get { return mShiftID; }
		}
		public int IssuePurposeID
		{
			set { mIssuePurposeID = value; }
			get { return mIssuePurposeID; }
		}
	}
}

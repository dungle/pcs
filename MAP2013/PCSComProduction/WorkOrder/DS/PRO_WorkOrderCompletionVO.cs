using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_WorkOrderCompletionVO
	{
		private int mWorkOrderCompletionID;
		private DateTime mPostDate;
		private DateTime mWOCompletionDate;
		private string mWOCompletionNo;
		private Decimal mCompletedQuantity;
		private string mLot;
		private string mSerial;
		private int mLocationID;
		private int mBinID;
		private int mCCNID;
		private int mMasterLocationID;
		private int mProductID;
		private int mStockUMID;
		private int mWorkOrderMasterID;
		private int mWorkOrderDetailID;
		private int mQAStatus;

		//HACKED: added by Tuan TQ, 09 Dec, 2005
		private int mShiftID;
		private int mIssuePurposeID;
		private string mRemark;

		public int ShiftID
		{
			get{ return mShiftID;}
			set{ mShiftID = value;}
		}

		public int IssuePurposeID
		{
			get{ return mIssuePurposeID;}
			set{ mIssuePurposeID = value;}
		}

		public string Remark
		{
			get{ return mRemark;}
			set{ mRemark = value;}
		}

		//End Hacked

		public int WorkOrderCompletionID
		{
			set { mWorkOrderCompletionID = value; }
			get { return mWorkOrderCompletionID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public DateTime WOCompletionDate
		{
			set { mWOCompletionDate = value; }
			get { return mWOCompletionDate; }
		}
		public string WOCompletionNo
		{
			set { mWOCompletionNo = value; }
			get { return mWOCompletionNo; }
		}
		public Decimal CompletedQuantity
		{
			set { mCompletedQuantity = value; }
			get { return mCompletedQuantity; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}
		public string Serial
		{
			set { mSerial = value; }
			get { return mSerial; }
		}
		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int BinID
		{
			set { mBinID = value; }
			get { return mBinID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
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
		public int QAStatus
		{
			set { mQAStatus = value; }
			get { return mQAStatus; }
		}
	}
}

using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_IssueMaterialDetailVO
	{
		private int mIssueMaterialDetailID;
		private int mLine;
		private Decimal mCommitQuantity;
		private int mProductID;
		private int mIssueMaterialMasterID;
		private int mLocationID;
		private int mBinID;
		private string mLot;
		private string mSerial;
		private int mMasterLocationID;
		private int mStockUMID;
		private int mQAStatus;
		private int mWorkOrderMasterID;
		private int mWorkOrderDetailID;
		private Decimal mBomQuantity;

		public int IssueMaterialDetailID
		{
			set { mIssueMaterialDetailID = value; }
			get { return mIssueMaterialDetailID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public Decimal CommitQuantity
		{
			set { mCommitQuantity = value; }
			get { return mCommitQuantity; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int IssueMaterialMasterID
		{
			set { mIssueMaterialMasterID = value; }
			get { return mIssueMaterialMasterID; }
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
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int QAStatus
		{
			set { mQAStatus = value; }
			get { return mQAStatus; }
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
		public decimal BomQuantity
		{
			get { return mBomQuantity; }
			set { mBomQuantity = value; }
		}
	}
}

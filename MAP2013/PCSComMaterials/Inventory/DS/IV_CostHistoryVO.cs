using System;
using System.Data;

namespace PCSComMaterials.Inventory.DS
{
	[Serializable]
	public class IV_CostHistoryVO
	{
		private int mCostHistoryID;
		private int mCostHistorySeq;
		private Decimal mICDHItemCost21;
		private DateTime mReceiveDate;
		private int mReceiveRef;
		private int mReceiveRefLine;
		private int mQAStatus;
		private int mPartyID;
		private int mPartyLocationID;
		private int mMasterLocationID;
		private int mProductID;
		private int mCCNID;
		private int mStockUMID;
		private int mTranTypeID;

		public int CostHistoryID
		{
			set { mCostHistoryID = value; }
			get { return mCostHistoryID; }
		}
		public int CostHistorySeq
		{
			set { mCostHistorySeq = value; }
			get { return mCostHistorySeq; }
		}
		public Decimal ICDHItemCost21
		{
			set { mICDHItemCost21 = value; }
			get { return mICDHItemCost21; }
		}
		public DateTime ReceiveDate
		{
			set { mReceiveDate = value; }
			get { return mReceiveDate; }
		}
		public int ReceiveRef
		{
			set { mReceiveRef = value; }
			get { return mReceiveRef; }
		}
		public int ReceiveRefLine
		{
			set { mReceiveRefLine = value; }
			get { return mReceiveRefLine; }
		}
		public int QAStatus
		{
			set { mQAStatus = value; }
			get { return mQAStatus; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int PartyLocationID
		{
			set { mPartyLocationID = value; }
			get { return mPartyLocationID; }
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
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int TranTypeID
		{
			set { mTranTypeID = value; }
			get { return mTranTypeID; }
		}
	}
}

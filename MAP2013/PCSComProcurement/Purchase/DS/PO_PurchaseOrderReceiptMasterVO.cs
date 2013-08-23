using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_PurchaseOrderReceiptMasterVO
	{
		private int	mMasterLocationID;
		private int	mPurchaseOrderReceiptID;
		private DateTime	mPostDate;
		private string	mReceiveNo;
		private int	mPurchaseOrderMasterID;
		private string mPOSlipNo;
		private int	mReceiptType;
		private string mRefNo;
		private int mCCNID;
		private int mInvoiceMasterID;
		private int mProductionLineID;
		private string mUsername;
		private DateTime mLastChange;
		private int mPurpose;

		public int Purpose
		{
			get { return mPurpose; }
			set { mPurpose = value; }
		}

		public int	MasterLocationID
		{	set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int	PurchaseOrderReceiptID
		{	set { mPurchaseOrderReceiptID = value; }
			get { return mPurchaseOrderReceiptID; }
		}
		public DateTime	PostDate
		{	set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string	ReceiveNo
		{	set { mReceiveNo = value; }
			get { return mReceiveNo; }
		}
		public int	PurchaseOrderMasterID
		{	set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}

		public string PoSlipNo
		{
			get { return mPOSlipNo; }
			set { mPOSlipNo = value; }
		}

		public int ReceiptType
		{
			get { return mReceiptType; }
			set { mReceiptType = value; }
		}

		public string RefNo
		{
			get { return mRefNo; }
			set { mRefNo = value; }
		}

		public int CCNID
		{
			get { return mCCNID; }
			set { mCCNID = value; }
		}

		public int	InvoiceMasterID
		{
			set { mInvoiceMasterID = value; }
			get { return mInvoiceMasterID; }
		}
		public int	ProductionLineID
		{
			set { mProductionLineID = value; }
			get { return mProductionLineID; }
		}

		public string Username
		{
			get { return mUsername; }
			set { mUsername = value; }
		}

		public DateTime LastChange
		{
			get { return mLastChange; }
			set { mLastChange = value; }
		}
	}
}

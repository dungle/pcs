using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_ReturnToVendorMasterVO
	{
		private int mPartyID;
		private int mPurchaseLocID;
		private int mPurchaseOrderMasterID;
		private int mMasterLocationID;
		private int mReturnToVendorMasterID;
		private DateTime mPostDate;
		private string mRTVNo;
		private int mCCNID;
		private int mInvoiceMasterID;
		private bool mByPO;
		private bool mByInvoice;
		private int mProductionLineID;

		//private int mShipFormLocID;

		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int PurchaseLocID
		{
			set { mPurchaseLocID = value; }
			get { return mPurchaseLocID; }
		}
		public int PurchaseOrderMasterID
		{
			set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int ReturnToVendorMasterID
		{
			set { mReturnToVendorMasterID = value; }
			get { return mReturnToVendorMasterID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string RTVNo
		{
			set { mRTVNo = value; }
			get { return mRTVNo; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}

		public int InvoiceMasterID
		{
			get { return mInvoiceMasterID; }
			set { mInvoiceMasterID = value; }
		}

		public bool ByPO
		{
			get { return mByPO; }
			set { mByPO = value; }
		}		
		
		public bool ByInvoice
		{
			get { return mByInvoice; }
			set { mByInvoice = value; }
		}

		public int ProductionLineId
		{
			get { return mProductionLineID; }
			set { mProductionLineID = value; }
		}
		
//		public int ShipFormLocID
//		{
//			set { mShipFormLocID = value; }
//			get { return mShipFormLocID; }
//		}
	}
}

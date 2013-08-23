using System;
using System.Data;

namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class cst_FreightMasterVO
	{
		private int mFreightMasterID;
		private string mTranNo;
		private string mNote;
		private Decimal mExchangeRate;
		private Decimal mTotalAmount;
		private int mCCNID;
		private int mCurrencyID;
		private int mTransporterID;
		private int mVendorID;
		private int mMakerID;
		private int mACObjectID;
		private int mACPurposeID;
		private int mCostElementID;
		private int mReceiveMasterID;
		private int mReturnToVendorMasterID;
		private Decimal mGrandTotal;
		private Decimal mSubTotal;
		private Decimal mTotalVAT;
		private Decimal mVATPercent;
		private DateTime mPostDate;

		public int FreightMasterID
		{
			set { mFreightMasterID = value; }
			get { return mFreightMasterID; }
		}
		public string TranNo
		{
			set { mTranNo = value; }
			get { return mTranNo; }
		}
		public string Note
		{
			set { mNote = value; }
			get { return mNote; }
		}
		public Decimal ExchangeRate
		{
			set { mExchangeRate = value; }
			get { return mExchangeRate; }
		}
		public Decimal TotalAmount
		{
			set { mTotalAmount = value; }
			get { return mTotalAmount; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public int TransporterID
		{
			set { mTransporterID = value; }
			get { return mTransporterID; }
		}
		public int ACObjectID
		{
			set { mACObjectID = value; }
			get { return mACObjectID; }
		}
		public int MakerID
		{
			set { mMakerID = value; }
			get { return mMakerID; }
		}
		public int VendorID
		{
			set { mVendorID = value; }
			get { return mVendorID; }
		}
		public int ACPurposeID
		{
			set { mACPurposeID = value; }
			get { return mACPurposeID; }
		}
		
		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
		}
		public int ReceiveMasterID
		{
			set { mReceiveMasterID = value; }
			get { return mReceiveMasterID; }
		}
		public int ReturnToVendorMasterID
		{
			set { mReturnToVendorMasterID = value; }
			get { return mReturnToVendorMasterID; }
		}
		public Decimal GrandTotal
		{
			set { mGrandTotal = value; }
			get { return mGrandTotal; }
		}
		public Decimal SubTotal
		{
			set { mSubTotal = value; }
			get { return mSubTotal; }
		}
		public Decimal TotalVAT
		{
			set { mTotalVAT = value; }
			get { return mTotalVAT; }
		}
		public Decimal VATPercent
		{
			set { mVATPercent = value; }
			get { return mVATPercent; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
	}
}

using System;
using System.Data;

namespace PCSComSale.Order.DS
{
	[Serializable]
	public class SO_ConfirmShipMasterVO
	{
		private int mConfirmShipMasterID;
		private string mConfirmShipNo;
		private DateTime mShippedDate;
		private int mSaleOrderMasterID;
		private int mMasterLocationID;
		private int mCCNID;
		private int mCurrencyID;
		private decimal mExchangeRate;
		private int mGateID;
		private string mComment;
		private string mFromPort;
		private string mCNo;
		private decimal mMeasurement;
		private decimal mGrossWeight;
		private decimal mNetWeight;
		private string mIssuingBank;
		private DateTime mLCDate;
		private string mLCNo;
		private string mVesselName;
		private string mShipCode;
		private DateTime mOnBoardDate;
		private string mReferenceNo;
		private string mInvoiceNo;
		private DateTime mInvoiceDate;
        public int BinId { get; set; }
        public int LocationId { get; set; }

		public int ConfirmShipMasterID
		{
			set { mConfirmShipMasterID = value; }
			get { return mConfirmShipMasterID; }
		}
		public string ConfirmShipNo
		{
			set { mConfirmShipNo = value; }
			get { return mConfirmShipNo; }
		}
		public DateTime ShippedDate
		{
			set { mShippedDate = value; }
			get { return mShippedDate; }
		}
		public int SaleOrderMasterID
		{
			set { mSaleOrderMasterID = value; }
			get { return mSaleOrderMasterID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
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
		public decimal ExchangeRate
		{
			set { mExchangeRate = value; }
			get { return mExchangeRate; }
		}
		public int GateID
		{
			set { mGateID = value; }
			get { return mGateID; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
		public string FromPort
		{
			set { mFromPort = value; }
			get { return mFromPort; }
		}
		public string CNo
		{
			set { mCNo = value; }
			get { return mCNo; }
		}
		public decimal Measurement
		{
			set { mMeasurement = value; }
			get { return mMeasurement; }
		}
		public decimal GrossWeight
		{
			set { mGrossWeight = value; }
			get { return mGrossWeight; }
		}
		public decimal NetWeight
		{
			set { mNetWeight = value; }
			get { return mNetWeight; }
		}
		public string IssuingBank
		{
			set { mIssuingBank = value; }
			get { return mIssuingBank; }
		}
		public DateTime LCDate
		{
			set { mLCDate = value; }
			get { return mLCDate; }
		}
		public string LCNo
		{
			set { mLCNo = value; }
			get { return mLCNo; }
		}
		public string VesselName
		{
			set { mVesselName = value; }
			get { return mVesselName; }
		}
		public string ShipCode
		{
			set { mShipCode = value; }
			get { return mShipCode; }
		}
		public string ReferenceNo
		{
			set { mReferenceNo = value; }
			get { return mReferenceNo; }
		}
		public DateTime OnBoardDate
		{
			set { mOnBoardDate = value; }
			get { return mOnBoardDate; }
		}
		public string InvoiceNo
		{
			set { mInvoiceNo = value; }
			get { return mInvoiceNo; }
		}

		public DateTime InvoiceDate
		{
			get { return mInvoiceDate; }
			set { mInvoiceDate = value; }
		}
	}
}

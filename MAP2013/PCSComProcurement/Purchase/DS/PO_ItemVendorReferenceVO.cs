using System;
using System.Data;

namespace PCSComProcurement.Purchase.DS
{
	[Serializable]
	public class PO_ItemVendorReferenceVO
	{
		private int mItemVendorReferenceID;
		private int mCCNID;
		private string mVendorItem;
		private string mVendorItemRevision;
		private string mVendorItemDescription;
		private Decimal mMinOrderQuantity;
		private Decimal mFixedLeadTime;
		private Decimal mVarianceLeadTime;
		private Decimal mPrice;
		private Decimal mQuantity;
		private int mCarrierID;
		private int mCurrencyID;
		private int mProductID;
		private int mPartyID;
		private int mVendorLocID;
		private int mBuyingUM;
		private DateTime mEndDate;
		private Decimal mQAStatus;
		private decimal mCapacity;
		private string mCapacityPeriod;

		public decimal Capacity
		{
			get { return mCapacity; }
			set { mCapacity = value; }
		}

		public string CapacityPeriod
		{
			get { return mCapacityPeriod; }
			set { mCapacityPeriod = value; }
		}

		public int ItemVendorReferenceID
		{
			set { mItemVendorReferenceID = value; }
			get { return mItemVendorReferenceID; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public string VendorItem
		{
			set { mVendorItem = value; }
			get { return mVendorItem; }
		}
		public string VendorItemRevision
		{
			set { mVendorItemRevision = value; }
			get { return mVendorItemRevision; }
		}
		public string VendorItemDescription
		{
			set { mVendorItemDescription = value; }
			get { return mVendorItemDescription; }
		}
		public Decimal MinOrderQuantity
		{
			set { mMinOrderQuantity = value; }
			get { return mMinOrderQuantity; }
		}
		public Decimal FixedLeadTime
		{
			set { mFixedLeadTime = value; }
			get { return mFixedLeadTime; }
		}
		public Decimal VarianceLeadTime
		{
			set { mVarianceLeadTime = value; }
			get { return mVarianceLeadTime; }
		}
		public Decimal Price
		{
			set { mPrice = value; }
			get { return mPrice; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public int CarrierID
		{
			set { mCarrierID = value; }
			get { return mCarrierID; }
		}
		public int CurrencyID
		{
			set { mCurrencyID = value; }
			get { return mCurrencyID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public int VendorLocID
		{
			set { mVendorLocID = value; }
			get { return mVendorLocID; }
		}
		public int BuyingUM
		{
			set { mBuyingUM = value; }
			get { return mBuyingUM; }
		}
		public DateTime EndDate
		{
			set { mEndDate = value; }
			get { return mEndDate; }
		}
		public Decimal QAStatus
		{
			set { mQAStatus = value; }
			get { return mQAStatus; }
		}
	}
}

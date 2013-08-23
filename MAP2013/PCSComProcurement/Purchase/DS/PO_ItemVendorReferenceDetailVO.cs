using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_ItemVendorReferenceDetailVO
	{
		private int	mItemVendorReferenceDetailID;
		private int	mItemVendorReferenceID;
		private DateTime	mEndDate;
		private Decimal	mUnitPrice;
		private Decimal	mFromQuantity;
		private Decimal	mToQuantity;
		private Decimal	mFromPrice;
		private Decimal	mToPrice;

		public int	ItemVendorReferenceDetailID
		{	set { mItemVendorReferenceDetailID = value; }
			get { return mItemVendorReferenceDetailID; }
		}
		public int	ItemVendorReferenceID
		{	set { mItemVendorReferenceID = value; }
			get { return mItemVendorReferenceID; }
		}
		public DateTime	EndDate
		{	set { mEndDate = value; }
			get { return mEndDate; }
		}
		public Decimal	UnitPrice
		{	set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public Decimal	FromQuantity
		{	set { mFromQuantity = value; }
			get { return mFromQuantity; }
		}
		public Decimal	ToQuantity
		{	set { mToQuantity = value; }
			get { return mToQuantity; }
		}
		public Decimal	FromPrice
		{	set { mFromPrice = value; }
			get { return mFromPrice; }
		}
		public Decimal	ToPrice
		{	set { mToPrice = value; }
			get { return mToPrice; }
		}
	}
}

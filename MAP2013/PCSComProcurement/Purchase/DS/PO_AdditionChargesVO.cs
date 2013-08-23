using System;


namespace PCSComProcurement.Purchase.DS
{
	
	[Serializable]
	public class PO_AdditionChargesVO
	{
		private int	mAdditionChargesID;
		private Decimal	mQuantity;
		private Decimal	mUnitPrice;
		private Decimal	mAmount;
		private Decimal	mVatAmount;
		private Decimal	mTotalAmount;
		private int	mPurchaseOrderDetailID;
		private int	mPurchaseOrderMasterID;
		private int	mAddChargeID;
		private int	mReasonID;

		public int	AdditionChargesID
		{	set { mAdditionChargesID = value; }
			get { return mAdditionChargesID; }
		}
		public Decimal	Quantity
		{	set { mQuantity = value; }
			get { return mQuantity; }
		}
		public Decimal	UnitPrice
		{	set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public Decimal	Amount
		{	set { mAmount = value; }
			get { return mAmount; }
		}
		public Decimal	VatAmount
		{	set { mVatAmount = value; }
			get { return mVatAmount; }
		}
		public Decimal	TotalAmount
		{	set { mTotalAmount = value; }
			get { return mTotalAmount; }
		}
		public int	PurchaseOrderDetailID
		{	set { mPurchaseOrderDetailID = value; }
			get { return mPurchaseOrderDetailID; }
		}
		public int	PurchaseOrderMasterID
		{	set { mPurchaseOrderMasterID = value; }
			get { return mPurchaseOrderMasterID; }
		}
		public int	AddChargeID
		{	set { mAddChargeID = value; }
			get { return mAddChargeID; }
		}
		public int	ReasonID
		{	set { mReasonID = value; }
			get { return mReasonID; }
		}
	}
}

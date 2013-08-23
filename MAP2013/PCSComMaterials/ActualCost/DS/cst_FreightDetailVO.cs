using System;
using System.Data;

namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class cst_FreightDetailVO
	{
		private int mFreightDetailID;
		private Decimal mQuantity;
		private Decimal mUnitPriceCIF;
		private Decimal mAmount;
		private int mFreightMasterID;
		private int mProductID;
		private int mBuyingUMID;
		private int mLine;
		private Decimal mVATAmount;
		private int mReturnToVendorDetailID;

		public int FreightDetailID
		{
			set { mFreightDetailID = value; }
			get { return mFreightDetailID; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public Decimal UnitPriceCIF
		{
			set { mUnitPriceCIF = value; }
			get { return mUnitPriceCIF; }
		}
		public Decimal Amount
		{
			set { mAmount = value; }
			get { return mAmount; }
		}
		public int FreightMasterID
		{
			set { mFreightMasterID = value; }
			get { return mFreightMasterID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int BuyingUMID
		{
			set { mBuyingUMID = value; }
			get { return mBuyingUMID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public Decimal VATAmount
		{
			set { mVATAmount = value; }
			get { return mVATAmount; }
		}
		public int ReturnToVendorDetailID
		{
			set{mReturnToVendorDetailID = value;}
			get{return mReturnToVendorDetailID;}
		}
	}
}

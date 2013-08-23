using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_ShipToleranceVO
	{
		private int mShipToleranceID;
		private string mCode;
		private string mDescription;
		private Decimal mOverQty;
		private Decimal mUnderQty;

		public int ShipToleranceID
		{
			set { mShipToleranceID = value; }
			get { return mShipToleranceID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public Decimal OverQty
		{
			set { mOverQty = value; }
			get { return mOverQty; }
		}
		public Decimal UnderQty
		{
			set { mUnderQty = value; }
			get { return mUnderQty; }
		}
	}
}

using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_DiscountTermVO
	{
		private int mDiscountTermID;
		private string mCode;
		private string mDescription;
		private Decimal mQuantity;
		private Decimal mRate;

		public int DiscountTermID
		{
			set { mDiscountTermID = value; }
			get { return mDiscountTermID; }
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
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public Decimal Rate
		{
			set { mRate = value; }
			get { return mRate; }
		}
	}
}

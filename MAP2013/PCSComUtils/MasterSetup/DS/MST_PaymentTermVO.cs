using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_PaymentTermVO
	{
		private int mPaymentTermID;
		private string mCode;
		private string mType;
		private string mDescription;
		private Decimal mDiscountPercent;
		private int mNetDueDate;
		private int mDiscountDate;
		private int mCCNID;

		public int PaymentTermID
		{
			set { mPaymentTermID = value; }
			get { return mPaymentTermID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Type
		{
			set { mType = value; }
			get { return mType; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public Decimal DiscountPercent
		{
			set { mDiscountPercent = value; }
			get { return mDiscountPercent; }
		}
		public int NetDueDate
		{
			set { mNetDueDate = value; }
			get { return mNetDueDate; }
		}
		public int DiscountDate
		{
			set { mDiscountDate = value; }
			get { return mDiscountDate; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_BOMVO
	{
		private int mBomID;
		private int mProductID;
		private int mComponentID;
		private int mLine;
		private DateTime mEffectiveBeginDate;
		private DateTime mEffectiveEndDate;
		private Decimal mLeadTimeOffset;
		private Decimal mQuantity;
		private int mRoutingID;
		private Decimal mShrink;
		private string mAncestor;
		private int mEffectiveEndDay;
		private int mEffectiveBeginDay;
		private int mAlternative;

		public int BomID
		{
			set { mBomID = value; }
			get { return mBomID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int ComponentID
		{
			set { mComponentID = value; }
			get { return mComponentID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public DateTime EffectiveBeginDate
		{
			set { mEffectiveBeginDate = value; }
			get { return mEffectiveBeginDate; }
		}
		public DateTime EffectiveEndDate
		{
			set { mEffectiveEndDate = value; }
			get { return mEffectiveEndDate; }
		}
		public Decimal LeadTimeOffset
		{
			set { mLeadTimeOffset = value; }
			get { return mLeadTimeOffset; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public int RoutingID
		{
			set { mRoutingID = value; }
			get { return mRoutingID; }
		}
		public Decimal Shrink
		{
			set { mShrink = value; }
			get { return mShrink; }
		}
		public string Ancestor
		{
			set { mAncestor = value; }
			get { return mAncestor; }
		}
		public int EffectiveEndDay
		{
			set { mEffectiveEndDay = value; }
			get { return mEffectiveEndDay; }
		}
		public int EffectiveBeginDay
		{
			set { mEffectiveBeginDay = value; }
			get { return mEffectiveBeginDay; }
		}
		public int Alternative
		{
			set { mAlternative = value; }
			get { return mAlternative; }
		}
	}
}

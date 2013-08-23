using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_UMRateVO
	{
		private int mUMRateID;
		private Decimal mScale;
		private string mDescription;
		private int mUnitOfMeasureOutID;
		private int mUnitOfMeasureInID;

		public int UMRateID
		{
			set { mUMRateID = value; }
			get { return mUMRateID; }
		}
		public Decimal Scale
		{
			set { mScale = value; }
			get { return mScale; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int UnitOfMeasureOutID
		{
			set { mUnitOfMeasureOutID = value; }
			get { return mUnitOfMeasureOutID; }
		}
		public int UnitOfMeasureInID
		{
			set { mUnitOfMeasureInID = value; }
			get { return mUnitOfMeasureInID; }
		}
	}
}

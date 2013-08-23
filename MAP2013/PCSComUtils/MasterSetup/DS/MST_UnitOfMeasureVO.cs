using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_UnitOfMeasureVO
	{
		private int mUnitOfMeasureID;
		private string mCode;
		private string mDescription;

		public int UnitOfMeasureID
		{
			set { mUnitOfMeasureID = value; }
			get { return mUnitOfMeasureID; }
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
	}
}

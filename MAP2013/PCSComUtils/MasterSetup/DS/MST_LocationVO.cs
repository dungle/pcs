using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_LocationVO
	{
		private int mLocationID;
		private string mCode;
		private string mName;
		private string mType;
		private bool mManufacturingAccess;
		private bool mSaleAccess;
		private bool mBin;
		private int mMasterLocationID;

		public int LocationID
		{
			set { mLocationID = value; }
			get { return mLocationID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public string Type
		{
			set { mType = value; }
			get { return mType; }
		}
		public bool ManufacturingAccess
		{
			set { mManufacturingAccess = value; }
			get { return mManufacturingAccess; }
		}
		public bool SaleAccess
		{
			set { mSaleAccess = value; }
			get { return mSaleAccess; }
		}
		public bool Bin
		{
			set { mBin = value; }
			get { return mBin; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
	}
}

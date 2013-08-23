using System;
using System.Data;

namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class CST_RecoverMaterialDetailVO
	{
		private int mRecoverMaterialDetailID;
		private int mRecoverMaterialMasterID;
		private int mProductID;
		private Decimal mRecoverQuantity;
		private int mUnitOfMeasureID;
		private int mToBinID;
		private int mToLocationID;
		private int mPartyID;

		public int RecoverMaterialDetailID
		{
			set { mRecoverMaterialDetailID = value; }
			get { return mRecoverMaterialDetailID; }
		}
		public int RecoverMaterialMasterID
		{
			set { mRecoverMaterialMasterID = value; }
			get { return mRecoverMaterialMasterID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public Decimal RecoverQuantity
		{
			set { mRecoverQuantity = value; }
			get { return mRecoverQuantity; }
		}
		public int UnitOfMeasureID
		{
			set { mUnitOfMeasureID = value; }
			get { return mUnitOfMeasureID; }
		}
		public int ToBinID
		{
			set { mToBinID = value; }
			get { return mToBinID; }
		}
		public int ToLocationID
		{
			set { mToLocationID = value; }
			get { return mToLocationID; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
	}
}

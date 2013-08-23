using System;
using System.Data;

namespace PCSComMaterials.ActualCost.DS
{
	[Serializable]
	public class CST_RecoverMaterialMasterVO
	{
		private int mRecoverMaterialMasterID;
		private DateTime mPostDate;
		private string mTransNo;
		private int mCCNID;
		private int mFromLocationID;
		private int mMasterLocationID;
		private int mFromBinID;
		private Decimal mQuantity;
		private int mProductID;
		private Decimal mAvailableQty;
		private string mUserName;
		private string mComment;

		public int RecoverMaterialMasterID
		{
			set { mRecoverMaterialMasterID = value; }
			get { return mRecoverMaterialMasterID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string TransNo
		{
			set { mTransNo = value; }
			get { return mTransNo; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int FromLocationID
		{
			set { mFromLocationID = value; }
			get { return mFromLocationID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int FromBinID
		{
			set { mFromBinID = value; }
			get { return mFromBinID; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public Decimal AvailableQty
		{
			set { mAvailableQty = value; }
			get { return mAvailableQty; }
		}
		public string UserName
		{
			set { mUserName = value; }
			get { return mUserName; }
		}
		public string Comment
		{
			set { mComment = value; }
			get { return mComment; }
		}
	}
}

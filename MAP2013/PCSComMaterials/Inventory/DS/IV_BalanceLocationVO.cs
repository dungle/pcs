using System;
using System.Data;
namespace PCSComMaterials.Inventory.DS{
	[Serializable]
	public class IV_BalanceLocationVO
	{
		private int	mBalanceLocationID;
		private DateTime	mEffectDate;
		private Decimal	mOHQuantity;
		private Decimal	mCommitQuantity;
		private int	mProductID;
		private int	mLocationID;
		private int	mMasterLocationID;
		private int	mStockUMID;

		public int	BalanceLocationID
		{	set { mBalanceLocationID = value; }
			get { return mBalanceLocationID; }
		}
		public DateTime	EffectDate
		{	set { mEffectDate = value; }
			get { return mEffectDate; }
		}
		public Decimal	OHQuantity
		{	set { mOHQuantity = value; }
			get { return mOHQuantity; }
		}
		public Decimal	CommitQuantity
		{	set { mCommitQuantity = value; }
			get { return mCommitQuantity; }
		}
		public int	ProductID
		{	set { mProductID = value; }
			get { return mProductID; }
		}
		public int	LocationID
		{	set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int	MasterLocationID
		{	set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int	StockUMID
		{	set { mStockUMID = value; }
			get { return mStockUMID; }
		}
	}
}

using System;
using System.Data;
namespace PCSComMaterials.Inventory.DS{
	[Serializable]
	public class IV_StockTakingDifferentVO
	{
		private int	mStockTakingDifferentID;
		private DateTime	mStockTakingDate;
		private Decimal	mOHQuantity;
		private Decimal	mActualQuantity;
		private Decimal	mDifferentQuantity;
		private int	mStockTakingPeriodID;
		private int	mProductID;
		private int	mLocationID;
		private int	mBinID;

		public int	StockTakingDifferentID
		{	set { mStockTakingDifferentID = value; }
			get { return mStockTakingDifferentID; }
		}
		public DateTime	StockTakingDate
		{	set { mStockTakingDate = value; }
			get { return mStockTakingDate; }
		}
		public Decimal	OHQuantity
		{	set { mOHQuantity = value; }
			get { return mOHQuantity; }
		}
		public Decimal	ActualQuantity
		{	set { mActualQuantity = value; }
			get { return mActualQuantity; }
		}
		public Decimal	DifferentQuantity
		{	set { mDifferentQuantity = value; }
			get { return mDifferentQuantity; }
		}
		public int	StockTakingPeriodID
		{	set { mStockTakingPeriodID = value; }
			get { return mStockTakingPeriodID; }
		}
		public int	ProductID
		{	set { mProductID = value; }
			get { return mProductID; }
		}
		public int	LocationID
		{	set { mLocationID = value; }
			get { return mLocationID; }
		}
		public int	BinID
		{	set { mBinID = value; }
			get { return mBinID; }
		}
	}
}

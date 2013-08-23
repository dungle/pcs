using System;
using System.Data;
namespace PCSComMaterials.Inventory.DS{
	[Serializable]
	public class IV_StockTakingMasterVO
	{
		private int	mStockTakingMasterID;
		private int	mStockTakingPeriodID;
		private string	mCode;
		private DateTime	mStockTakingDate;
		private int	mDepartmentID;
		private int	mProductionLineID;
		private int	mLocationID;
		private int	mBinID;

		public int	StockTakingMasterID
		{	set { mStockTakingMasterID = value; }
			get { return mStockTakingMasterID; }
		}
		public int	StockTakingPeriodID
		{	set { mStockTakingPeriodID = value; }
			get { return mStockTakingPeriodID; }
		}
		public DateTime StockTakingDate
		{
			set { mStockTakingDate = value; }
			get { return mStockTakingDate; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public int	DepartmentID
		{	set { mDepartmentID = value; }
			get { return mDepartmentID; }
		}
		public int	ProductionLineID
		{	set { mProductionLineID = value; }
			get { return mProductionLineID; }
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

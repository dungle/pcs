using System;
using System.Data;
namespace PCSComMaterials.Inventory.DS{
	[Serializable]
	public class IV_StockTakingPeriodVO
	{
		private int	mStockTakingPeriodID;
		private string	mDescription;
		private DateTime	mStockTakingDate;
		private DateTime	mFromDate;
		private DateTime	mToDate;
		private int	mCCNID;
		private bool mClosed;

		public bool Closed
		{
			get { return mClosed; }
			set { mClosed = value; }
		}

		public int	StockTakingPeriodID
		{	set { mStockTakingPeriodID = value; }
			get { return mStockTakingPeriodID; }
		}
		public string	Description
		{	set { mDescription = value; }
			get { return mDescription; }
		}
		public DateTime	StockTakingDate
		{	set { mStockTakingDate = value; }
			get { return mStockTakingDate; }
		}
		public DateTime	FromDate
		{	set { mFromDate = value; }
			get { return mFromDate; }
		}
		public DateTime	ToDate
		{	set { mToDate = value; }
			get { return mToDate; }
		}
		public int	CCNID
		{	set { mCCNID = value; }
			get { return mCCNID; }
		}
	}
}

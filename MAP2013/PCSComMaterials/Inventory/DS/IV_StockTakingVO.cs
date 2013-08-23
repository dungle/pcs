using System;
using System.Data;
namespace PCSComMaterials.Inventory.DS{
	[Serializable]
	public class IV_StockTakingVO
	{
		private int	mStockTakingID;
		private Decimal	mQuantity;
		private string	mSlipCode;
		private string	mNote;
		private Decimal	mBookQuantity;
		private int	mProductID;
		private int	mStockUMID;
		private int	mCountingMethodID;
		private int	mStockTakingMasterID;

		public int	StockTakingID
		{	set { mStockTakingID = value; }
			get { return mStockTakingID; }
		}
		public Decimal	Quantity
		{	set { mQuantity = value; }
			get { return mQuantity; }
		}
		public string	SlipCode
		{	set { mSlipCode = value; }
			get { return mSlipCode; }
		}
		public string	Note
		{	set { mNote = value; }
			get { return mNote; }
		}
		public Decimal	BookQuantity
		{	set { mBookQuantity = value; }
			get { return mBookQuantity; }
		}
		public int	ProductID
		{	set { mProductID = value; }
			get { return mProductID; }
		}
		public int	StockUMID
		{	set { mStockUMID = value; }
			get { return mStockUMID; }
		}
		public int	CountingMethodID
		{	set { mCountingMethodID = value; }
			get { return mCountingMethodID; }
		}
		public int	StockTakingMasterID
		{	set { mStockTakingMasterID = value; }
			get { return mStockTakingMasterID; }
		}
	}
}

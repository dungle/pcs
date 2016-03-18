using System;
using System.Data;

namespace PCSComMaterials.Inventory.DS
{
	[Serializable]
	public class IV_MiscellaneousIssueDetailVO
	{
		private int mMiscellaneousIssueDetailID;
		private string mLot;
		private Decimal mQuantity;
		private int mMiscellaneousIssueMasterID;
		private int mProductID;
		private int mStockUMID;
		private decimal mAvailableQuantity;

		public int MiscellaneousIssueDetailID
		{
			set { mMiscellaneousIssueDetailID = value; }
			get { return mMiscellaneousIssueDetailID; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}
		public Decimal Quantity
		{
			set { mQuantity = value; }
			get { return mQuantity; }
		}
		public int MiscellaneousIssueMasterID
		{
			set { mMiscellaneousIssueMasterID = value; }
			get { return mMiscellaneousIssueMasterID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int StockUMID
		{
			set { mStockUMID = value; }
			get { return mStockUMID; }
		}

		public decimal AvailableQuantity
		{
			get { return mAvailableQuantity; }
			set { mAvailableQuantity = value; }
		}
        public int DepartmentID { get; set; }
        public string Reason { get; set; }
	}
}

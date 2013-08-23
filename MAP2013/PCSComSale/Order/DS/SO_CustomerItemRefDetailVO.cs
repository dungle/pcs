using System;
using System.Data;
namespace PCSComSale.Order.DS{
	[Serializable]
	public class SO_CustomerItemRefDetailVO
	{
		private int	mCustomerItemRefDetailID;
		private string	mCustomerItemCode;
		private string	mCustomerItemModel;
		private int	mProductID;
		private int	mCustomerItemRefMasterID;
		private Decimal	mUnitPrice;
		private int	mUnitOfMeasureID;

		public int	CustomerItemRefDetailID
		{	set { mCustomerItemRefDetailID = value; }
			get { return mCustomerItemRefDetailID; }
		}
		public string	CustomerItemCode
		{	set { mCustomerItemCode = value; }
			get { return mCustomerItemCode; }
		}
		public string	CustomerItemModel
		{	set { mCustomerItemModel = value; }
			get { return mCustomerItemModel; }
		}
		public int	ProductID
		{	set { mProductID = value; }
			get { return mProductID; }
		}
		public int	CustomerItemRefMasterID
		{	set { mCustomerItemRefMasterID = value; }
			get { return mCustomerItemRefMasterID; }
		}
		public Decimal	UnitPrice
		{	set { mUnitPrice = value; }
			get { return mUnitPrice; }
		}
		public int	UnitOfMeasureID
		{	set { mUnitOfMeasureID = value; }
			get { return mUnitOfMeasureID; }
		}
	}
}

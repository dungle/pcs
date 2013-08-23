using System;

namespace PCSComSale.Order.DS
{
	/// <summary>
	/// Summary description for SaleOrderInformationVO.
	/// </summary>
	[Serializable]
	public class SaleOrderInformationVO
	{
		public SaleOrderInformationVO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private int mSaleOrderMasterID;
		private int mSaleOrderDetailID;
		private string mSaleOrderNo;
		private int mSaleOrderLine;
		private int mProductID;
		private string mProductCode;
		private string mProductRevision;
		private string mProductDescription;
		private string mUnitCode;
		private string mCCNCode;
		private decimal mOrderQuantity;
		private DateTime mOrderDate;

		public int SaleOrderMasterID
		{
			get
			{
				return mSaleOrderMasterID;
			}
			set 
			{
				mSaleOrderMasterID = value;
			}
		}
		public int SaleOrderDetailID
		{
			get
			{
				return mSaleOrderDetailID;
			}
			set 
			{
				mSaleOrderDetailID = value;
			}
		}
		public string SaleOrderNo
		{
			get
			{
				return mSaleOrderNo;
			}
			set 
			{
				mSaleOrderNo = value;
			}
		}
		public int SaleOrderLine
		{
			get
			{
				return mSaleOrderLine;
			}
			set 
			{
				mSaleOrderLine = value;
			}
		}
		public int ProductID
		{
			get
			{
				return mProductID;
			}
			set 
			{
				mProductID = value;
			}
		}
		public string ProductCode
		{
			get
			{
				return mProductCode;
			}
			set 
			{
				mProductCode = value;
			}
		}
		public string ProductRevision
		{
			get
			{
				return mProductRevision;
			}
			set 
			{
				mProductRevision = value;
			}
		}
		public string ProductDescription
		{
			get
			{
				return mProductDescription;
			}
			set 
			{
				mProductDescription = value;
			}
		}
		public string UnitCode
		{
			get
			{
				return mUnitCode;
			}
			set 
			{
				mUnitCode = value;
			}
		}
		public string CCNCode
		{
			get
			{
				return mCCNCode;
			}
			set 
			{
				mCCNCode = value;
			}
		}
		public decimal OrderQuantity
		{
			get
			{
				return mOrderQuantity;
			}
			set 
			{
				mOrderQuantity = value;
			}
		}

		public DateTime OrderDate
		{
			get
			{
				return mOrderDate;
			}
			set 
			{
				mOrderDate = value;
			}
		}

	}
}

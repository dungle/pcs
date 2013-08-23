using System;
using System.Data;

namespace PCSComSale.Order.DS
{
	[Serializable]
	public class SO_InvoiceDetailVO
	{
		private int mInvoiceDetailID;
		private int mInvoiceMasterID;
		private int mSaleOrderDetailID;
		private int mDeliveryScheduleID;
		private int mProductID;

		public int InvoiceDetailID
		{
			set { mInvoiceDetailID = value; }
			get { return mInvoiceDetailID; }
		}
		public int InvoiceMasterID
		{
			set { mInvoiceMasterID = value; }
			get { return mInvoiceMasterID; }
		}
		public int SaleOrderDetailID
		{
			set { mSaleOrderDetailID = value; }
			get { return mSaleOrderDetailID; }
		}
		public int DeliveryScheduleID
		{
			set { mDeliveryScheduleID = value; }
			get { return mDeliveryScheduleID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
	}
}

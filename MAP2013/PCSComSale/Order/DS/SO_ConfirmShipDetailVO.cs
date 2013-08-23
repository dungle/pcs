using System;
using System.Data;

namespace PCSComSale.Order.DS
{
	[Serializable]
	public class SO_ConfirmShipDetailVO
	{
		private int mConfirmShipDetailID;
		private int mConfirmShipMasterID;
		private int mSaleOrderDetailID;
		private int mDeliveryScheduleID;
		private int mProductID;

		public int ConfirmShipDetailID
		{
			set { mConfirmShipDetailID = value; }
			get { return mConfirmShipDetailID; }
		}
		public int ConfirmShipMasterID
		{
			set { mConfirmShipMasterID = value; }
			get { return mConfirmShipMasterID; }
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

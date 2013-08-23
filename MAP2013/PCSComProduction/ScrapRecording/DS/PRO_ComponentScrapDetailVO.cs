using System;
using System.Data;

namespace PCSComProduction.ScrapRecording.DS
{
	[Serializable]
	public class PRO_ComponentScrapDetailVO
	{
		private int mComponentScrapDetailID;
		private string mLot;
		private string mSerial;
		private Decimal mScrapQuantity;
		private int mComponentScrapMasterID;
		private int mWorkOrderMasterID;
		private int mWorkOrderDetailID;
		private int mComponentID;
		private int mProductID;
		private int mLine;
		private int mScrapReasonID;
		private int mWORoutingID;

		public int ComponentScrapDetailID
		{
			set { mComponentScrapDetailID = value; }
			get { return mComponentScrapDetailID; }
		}
		public string Lot
		{
			set { mLot = value; }
			get { return mLot; }
		}
		public string Serial
		{
			set { mSerial = value; }
			get { return mSerial; }
		}
		public Decimal ScrapQuantity
		{
			set { mScrapQuantity = value; }
			get { return mScrapQuantity; }
		}
		public int ComponentScrapMasterID
		{
			set { mComponentScrapMasterID = value; }
			get { return mComponentScrapMasterID; }
		}
		public int WorkOrderMasterID
		{
			set { mWorkOrderMasterID = value; }
			get { return mWorkOrderMasterID; }
		}
		public int WorkOrderDetailID
		{
			set { mWorkOrderDetailID = value; }
			get { return mWorkOrderDetailID; }
		}
		public int ComponentID
		{
			set { mComponentID = value; }
			get { return mComponentID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int Line
		{
			set { mLine = value; }
			get { return mLine; }
		}
		public int ScrapReasonID
		{
			set { mScrapReasonID = value; }
			get { return mScrapReasonID; }
		}
		public int WORoutingID
		{
			set { mWORoutingID = value; }
			get { return mWORoutingID; }
		}
	}
}

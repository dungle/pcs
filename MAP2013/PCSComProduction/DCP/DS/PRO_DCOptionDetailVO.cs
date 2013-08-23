using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_DCOptionDetailVO
	{
		private int mDCOptionDetailID;
		private int mMasterLocationID;
		private bool mWorkOrder;
		private int mDCOptionMasterID;

		public int DCOptionDetailID
		{
			set { mDCOptionDetailID = value; }
			get { return mDCOptionDetailID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public bool WorkOrder
		{
			set { mWorkOrder = value; }
			get { return mWorkOrder; }
		}
		public int DCOptionMasterID
		{
			set { mDCOptionMasterID = value; }
			get { return mDCOptionMasterID; }
		}
	}
}

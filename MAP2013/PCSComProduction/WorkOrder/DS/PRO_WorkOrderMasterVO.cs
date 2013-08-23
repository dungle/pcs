using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_WorkOrderMasterVO
	{
		private int mMasterLocationID;
		private int mWorkOrderMasterID;
		private int mProductionLineID;
		private string mDescription;
		private int mCCNID;
		private string mWorkOrderNo;
		private DateTime mTransDate;
		private int mDCOptionMasterID;

		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int WorkOrderMasterID
		{
			set { mWorkOrderMasterID = value; }
			get { return mWorkOrderMasterID; }
		}
		public int ProductionLineID
		{
			set { mProductionLineID = value; }
			get { return mProductionLineID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public string WorkOrderNo
		{
			set { mWorkOrderNo = value; }
			get { return mWorkOrderNo; }
		}
		public DateTime TransDate
		{
			set { mTransDate = value; }
			get { return mTransDate; }
		}

		public int DCOptionMasterID
		{
			get { return mDCOptionMasterID; }
			set { mDCOptionMasterID = value; }
		}
	}
}

using System;


namespace PCSComProduct.Items.DS
{
	
	[Serializable]
	public class ITM_RoutingVO
	{
		private int mRoutingID;
		private int mStep;
		private int mType;
		private Decimal mMachineSetupTime;
		private Decimal mMachineRunTime;
		private Decimal mMachines;
		private Decimal mLaborRunTime;
		private Decimal mLaborSetupTime;
		private Decimal mCrewSize;
		private Decimal mSetupQuantity;
		private Decimal mStudyTime;
		private Decimal mMoveTime;
		private int mLaborCostCenterID;
		private int mMachineCostCenterID;
		private int mProductID;
		private int mFunctionID;
		private int mWorkCenterID;
		private DateTime mEffectEndDate;
		private Decimal mOSVarLT;
		private Decimal mOSFixLT;
		private DateTime mEffectBeginDate;
		private Decimal mOSOverlapPercent;
		private Decimal mOSOverlapQty;
		private Decimal mOSScheduleSeq;
		private Decimal mOSCost;
		private Decimal mOverlapPercent;
		private Decimal mOverlapQty;
		private Decimal mScheduleSeq;
		private Decimal mVarLT;
		private Decimal mFixLT;
		private int mRoutingStatusID;
		private Decimal mRunQuantity;
		private int mPartyID;
		private string mPacer;

		public int RoutingID
		{
			set { mRoutingID = value; }
			get { return mRoutingID; }
		}
		public int Step
		{
			set { mStep = value; }
			get { return mStep; }
		}
		public int Type
		{
			set { mType = value; }
			get { return mType; }
		}
		public Decimal MachineSetupTime
		{
			set { mMachineSetupTime = value; }
			get { return mMachineSetupTime; }
		}
		public Decimal MachineRunTime
		{
			set { mMachineRunTime = value; }
			get { return mMachineRunTime; }
		}
		public Decimal Machines
		{
			set { mMachines = value; }
			get { return mMachines; }
		}
		public Decimal LaborRunTime
		{
			set { mLaborRunTime = value; }
			get { return mLaborRunTime; }
		}
		public Decimal LaborSetupTime
		{
			set { mLaborSetupTime = value; }
			get { return mLaborSetupTime; }
		}
		public Decimal CrewSize
		{
			set { mCrewSize = value; }
			get { return mCrewSize; }
		}
		public Decimal SetupQuantity
		{
			set { mSetupQuantity = value; }
			get { return mSetupQuantity; }
		}
		public Decimal StudyTime
		{
			set { mStudyTime = value; }
			get { return mStudyTime; }
		}
		public Decimal MoveTime
		{
			set { mMoveTime = value; }
			get { return mMoveTime; }
		}
		public int LaborCostCenterID
		{
			set { mLaborCostCenterID = value; }
			get { return mLaborCostCenterID; }
		}
		public int MachineCostCenterID
		{
			set { mMachineCostCenterID = value; }
			get { return mMachineCostCenterID; }
		}
		public int ProductID
		{
			set { mProductID = value; }
			get { return mProductID; }
		}
		public int FunctionID
		{
			set { mFunctionID = value; }
			get { return mFunctionID; }
		}
		public int WorkCenterID
		{
			set { mWorkCenterID = value; }
			get { return mWorkCenterID; }
		}
		public DateTime EffectEndDate
		{
			set { mEffectEndDate = value; }
			get { return mEffectEndDate; }
		}
		public Decimal OSVarLT
		{
			set { mOSVarLT = value; }
			get { return mOSVarLT; }
		}
		public Decimal OSFixLT
		{
			set { mOSFixLT = value; }
			get { return mOSFixLT; }
		}
		public DateTime EffectBeginDate
		{
			set { mEffectBeginDate = value; }
			get { return mEffectBeginDate; }
		}
		public Decimal OSOverlapPercent
		{
			set { mOSOverlapPercent = value; }
			get { return mOSOverlapPercent; }
		}
		public Decimal OSOverlapQty
		{
			set { mOSOverlapQty = value; }
			get { return mOSOverlapQty; }
		}
		public Decimal OSScheduleSeq
		{
			set { mOSScheduleSeq = value; }
			get { return mOSScheduleSeq; }
		}
		public Decimal OSCost
		{
			set { mOSCost = value; }
			get { return mOSCost; }
		}
		public Decimal OverlapPercent
		{
			set { mOverlapPercent = value; }
			get { return mOverlapPercent; }
		}
		public Decimal OverlapQty
		{
			set { mOverlapQty = value; }
			get { return mOverlapQty; }
		}
		public Decimal ScheduleSeq
		{
			set { mScheduleSeq = value; }
			get { return mScheduleSeq; }
		}
		public Decimal VarLT
		{
			set { mVarLT = value; }
			get { return mVarLT; }
		}
		public Decimal FixLT
		{
			set { mFixLT = value; }
			get { return mFixLT; }
		}
		public int RoutingStatusID
		{
			set { mRoutingStatusID = value; }
			get { return mRoutingStatusID; }
		}
		public Decimal RunQuantity
		{
			set { mRunQuantity = value; }
			get { return mRunQuantity; }
		}
		public int PartyID
		{
			set { mPartyID = value; }
			get { return mPartyID; }
		}
		public string Pacer
		{
			set { mPacer = value; }
			get { return mPacer; }
		}

	}
}

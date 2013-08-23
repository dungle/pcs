using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_WorkCenterCapacityVO
	{
		private int mWorkCenterCapacityID;
		private DateTime mEffectiveBeginDate;
		private Decimal mLaborHoursPerday;
		private DateTime mEffectiveEndDate;
		private int mCrewSize;
		private Decimal mLaborShift;
		private Decimal mLaborEfficiencyFactor;
		private Decimal mMachineHoursPerday;
		private Decimal mMachineQty;
		private Decimal mMachineShift;
		private Decimal mMachineEfficiencyFactor;

		public int WorkCenterCapacityID
		{
			set { mWorkCenterCapacityID = value; }
			get { return mWorkCenterCapacityID; }
		}
		public DateTime EffectiveBeginDate
		{
			set { mEffectiveBeginDate = value; }
			get { return mEffectiveBeginDate; }
		}
		public Decimal LaborHoursPerday
		{
			set { mLaborHoursPerday = value; }
			get { return mLaborHoursPerday; }
		}
		public DateTime EffectiveEndDate
		{
			set { mEffectiveEndDate = value; }
			get { return mEffectiveEndDate; }
		}
		public int CrewSize
		{
			set { mCrewSize = value; }
			get { return mCrewSize; }
		}
		public Decimal LaborShift
		{
			set { mLaborShift = value; }
			get { return mLaborShift; }
		}
		public Decimal LaborEfficiencyFactor
		{
			set { mLaborEfficiencyFactor = value; }
			get { return mLaborEfficiencyFactor; }
		}
		public Decimal MachineHoursPerday
		{
			set { mMachineHoursPerday = value; }
			get { return mMachineHoursPerday; }
		}
		public Decimal MachineQty
		{
			set { mMachineQty = value; }
			get { return mMachineQty; }
		}
		public Decimal MachineShift
		{
			set { mMachineShift = value; }
			get { return mMachineShift; }
		}
		public Decimal MachineEfficiencyFactor
		{
			set { mMachineEfficiencyFactor = value; }
			get { return mMachineEfficiencyFactor; }
		}
	}
}

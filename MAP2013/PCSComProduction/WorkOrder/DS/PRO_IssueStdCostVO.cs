using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_IssueStdCostVO
	{
		private int mIssueStdCostID;
		private DateTime mPostDate;
		private string mTransType;
		private Decimal mCostMaterial01;
		private Decimal mCostMaterialOverHead02;
		private Decimal mCostMachineSetup03;
		private Decimal mCostMachineSetupFixed04;
		private Decimal mCostMachineSetupVar05;
		private Decimal mCostMachineRun06;
		private Decimal mCostMachineFixed07;
		private Decimal mCostMachineVariable08;
		private Decimal mCostLaborSetup09;
		private Decimal mCostLaborSetupFixed10;
		private Decimal mCostLaborSetupVariable11;
		private string mCostLaborRun12;
		private Decimal mCostLaborFixed13;
		private Decimal mCostLaborVariable14;
		private Decimal mCostOutsideProc15;
		private Decimal mCostAssemblyScrap16;
		private Decimal mCostShrink17;
		private Decimal mCostFreight18;
		private Decimal mCostUserStandard1_19;
		private Decimal mCostUserStandard2_20;
		private Decimal mCostTotalAmount21;
		private int mIssueMaterialDetailID;

		public int IssueStdCostID
		{
			set { mIssueStdCostID = value; }
			get { return mIssueStdCostID; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public string TransType
		{
			set { mTransType = value; }
			get { return mTransType; }
		}
		public Decimal CostMaterial01
		{
			set { mCostMaterial01 = value; }
			get { return mCostMaterial01; }
		}
		public Decimal CostMaterialOverHead02
		{
			set { mCostMaterialOverHead02 = value; }
			get { return mCostMaterialOverHead02; }
		}
		public Decimal CostMachineSetup03
		{
			set { mCostMachineSetup03 = value; }
			get { return mCostMachineSetup03; }
		}
		public Decimal CostMachineSetupFixed04
		{
			set { mCostMachineSetupFixed04 = value; }
			get { return mCostMachineSetupFixed04; }
		}
		public Decimal CostMachineSetupVar05
		{
			set { mCostMachineSetupVar05 = value; }
			get { return mCostMachineSetupVar05; }
		}
		public Decimal CostMachineRun06
		{
			set { mCostMachineRun06 = value; }
			get { return mCostMachineRun06; }
		}
		public Decimal CostMachineFixed07
		{
			set { mCostMachineFixed07 = value; }
			get { return mCostMachineFixed07; }
		}
		public Decimal CostMachineVariable08
		{
			set { mCostMachineVariable08 = value; }
			get { return mCostMachineVariable08; }
		}
		public Decimal CostLaborSetup09
		{
			set { mCostLaborSetup09 = value; }
			get { return mCostLaborSetup09; }
		}
		public Decimal CostLaborSetupFixed10
		{
			set { mCostLaborSetupFixed10 = value; }
			get { return mCostLaborSetupFixed10; }
		}
		public Decimal CostLaborSetupVariable11
		{
			set { mCostLaborSetupVariable11 = value; }
			get { return mCostLaborSetupVariable11; }
		}
		public string CostLaborRun12
		{
			set { mCostLaborRun12 = value; }
			get { return mCostLaborRun12; }
		}
		public Decimal CostLaborFixed13
		{
			set { mCostLaborFixed13 = value; }
			get { return mCostLaborFixed13; }
		}
		public Decimal CostLaborVariable14
		{
			set { mCostLaborVariable14 = value; }
			get { return mCostLaborVariable14; }
		}
		public Decimal CostOutsideProc15
		{
			set { mCostOutsideProc15 = value; }
			get { return mCostOutsideProc15; }
		}
		public Decimal CostAssemblyScrap16
		{
			set { mCostAssemblyScrap16 = value; }
			get { return mCostAssemblyScrap16; }
		}
		public Decimal CostShrink17
		{
			set { mCostShrink17 = value; }
			get { return mCostShrink17; }
		}
		public Decimal CostFreight18
		{
			set { mCostFreight18 = value; }
			get { return mCostFreight18; }
		}
		public Decimal CostUserStandard1_19
		{
			set { mCostUserStandard1_19 = value; }
			get { return mCostUserStandard1_19; }
		}
		public Decimal CostUserStandard2_20
		{
			set { mCostUserStandard2_20 = value; }
			get { return mCostUserStandard2_20; }
		}
		public Decimal CostTotalAmount21
		{
			set { mCostTotalAmount21 = value; }
			get { return mCostTotalAmount21; }
		}
		public int IssueMaterialDetailID
		{
			set { mIssueMaterialDetailID = value; }
			get { return mIssueMaterialDetailID; }
		}
	}
}

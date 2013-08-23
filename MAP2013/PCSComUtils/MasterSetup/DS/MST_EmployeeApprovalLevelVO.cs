using System;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_EmployeeApprovalLevelVO
	{
		private string mDescription;
		private int mEmployeeApprovalLevelID;
		private int mEmployeeID;
		private int mApprovalLevelID;

		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
		public int EmployeeApprovalLevelID
		{
			set { mEmployeeApprovalLevelID = value; }
			get { return mEmployeeApprovalLevelID; }
		}
		public int EmployeeID
		{
			set { mEmployeeID = value; }
			get { return mEmployeeID; }
		}
		public int ApprovalLevelID
		{
			set { mApprovalLevelID = value; }
			get { return mApprovalLevelID; }
		}
	}
}

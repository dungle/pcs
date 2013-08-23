using System;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_EmployeeVO
	{
		private int mEmployeeID;
		private string mCode;
		private string mName;
		private int mDepartmentID;
		private int mShift;

		public int EmployeeID
		{
			set { mEmployeeID = value; }
			get { return mEmployeeID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public int DepartmentID
		{
			set { mDepartmentID = value; }
			get { return mDepartmentID; }
		}
		public int Shift
		{
			set { mShift = value; }
			get { return mShift; }
		}
	}
}

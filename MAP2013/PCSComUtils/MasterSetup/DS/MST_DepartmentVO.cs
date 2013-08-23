using System;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_DepartmentVO
	{
		private int mDepartmentID;
		private string mCode;
		private string mName;

		public int DepartmentID
		{
			set { mDepartmentID = value; }
			get { return mDepartmentID; }
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
	}
}

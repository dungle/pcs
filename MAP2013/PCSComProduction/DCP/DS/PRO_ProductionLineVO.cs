using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_ProductionLineVO
	{
		private int mProductionLineID;
		private string mCode;
		private string mName;
		private int mDepartmentID;

		public int ProductionLineID
		{
			set { mProductionLineID = value; }
			get { return mProductionLineID; }
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
	}
}

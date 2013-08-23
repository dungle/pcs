using System;
using System.Runtime.InteropServices;

namespace PCSComProduct.Costing.DS
{
	[Guid("60B14553-798A-45ec-AB21-406BE7982341")]
	[Serializable]
	public class ITM_CostCenterVO
	{
		private int mCostCenterID;
		private string mCode;
		private string mName;
		private string mDescription;

		public int CostCenterID
		{
			set { mCostCenterID = value; }
			get { return mCostCenterID; }
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
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

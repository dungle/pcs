using System;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	[Serializable]
	public class PRO_CostElementVO
	{
		private int mCostElementID;
		private string mCode;
		private string mDescription;

		public int CostElementID
		{
			set { mCostElementID = value; }
			get { return mCostElementID; }
		}
		public string Code
		{
			set { mCode = value; }
			get { return mCode; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

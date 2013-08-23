using System;
using System.Data;

namespace PCSComProduction.WorkOrder.DS
{
	[Serializable]
	public class PRO_IssuePurposeVO
	{
		private int mIssuePurposeID;
		private string mDescription;

		public int IssuePurposeID
		{
			set { mIssuePurposeID = value; }
			get { return mIssuePurposeID; }
		}
		public string Description
		{
			set { mDescription = value; }
			get { return mDescription; }
		}
	}
}

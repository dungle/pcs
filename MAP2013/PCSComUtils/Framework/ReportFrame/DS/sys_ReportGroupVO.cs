using System;


namespace PCSComUtils.Framework.ReportFrame.DS
{
	
	[Serializable]
	public class sys_ReportGroupVO
	{
		private string mGroupID;
		private string mGroupName;
		private int mGroupOrder;

		public string GroupID
		{
			set { mGroupID = value; }
			get { return mGroupID; }
		}
		public string GroupName
		{
			set { mGroupName = value; }
			get { return mGroupName; }
		}
		public int GroupOrder
		{
			set { mGroupOrder = value; }
			get { return mGroupOrder; }
		}
	}
}

using System;


namespace PCSComUtils.Framework.TableFrame.DS
{
	
	[Serializable]
	public class sys_TableGroupVO
	{
		private int	mTableGroupID;
		private string	mCode;
		private string	mTableGroupName;
		private int	mGroupOrder;

		public int	TableGroupID
		{	set { mTableGroupID = value; }
			get { return mTableGroupID; }
		}
		public string	Code
		{	set { mCode = value; }
			get { return mCode; }
		}
		public string	TableGroupName
		{	set { mTableGroupName = value; }
			get { return mTableGroupName; }
		}
		public int	GroupOrder
		{	set { mGroupOrder = value; }
			get { return mGroupOrder; }
		}
	}
}

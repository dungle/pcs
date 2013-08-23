using System;


namespace PCSComUtils.Framework.TableFrame.DS
{
	
	[Serializable]
	public class sys_TableAndGroupVO
	{
		private int	mTableAndGroupID;
		private int	mTableGroupID;
		private int	mTableID;
		private int	mTableOrder;

		public int	TableAndGroupID
		{	set { mTableAndGroupID = value; }
			get { return mTableAndGroupID; }
		}
		public int	TableGroupID
		{	set { mTableGroupID = value; }
			get { return mTableGroupID; }
		}
		public int	TableID
		{	set { mTableID = value; }
			get { return mTableID; }
		}
		public int	TableOrder
		{	set { mTableOrder = value; }
			get { return mTableOrder; }
		}
	}
}

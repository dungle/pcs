using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_VisibilityItemVO
	{
		private int mVisibilityItemID;
		private string mName;
		private int mGroupID;
		private int mType;

		public int VisibilityItemID
		{
			set { mVisibilityItemID = value; }
			get { return mVisibilityItemID; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public int GroupID
		{
			set { mGroupID = value; }
			get { return mGroupID; }
		}
		public int Type
		{
			set { mType = value; }
			get { return mType; }
		}
	}
}

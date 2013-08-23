using System;
using System.Data;

namespace PCSComUtils.Admin.DS
{
	[Serializable]
	public class Sys_VisibilityGroupVO
	{
		private int mVisibilityGroupID;
		private string mControlName;
		private int mParentID;
		private string mFormName;
		private string mGroupText;
		private string mGroupTextVN;
		private string mGroupTextJP;
		private int mType;

		public int VisibilityGroupID
		{
			set { mVisibilityGroupID = value; }
			get { return mVisibilityGroupID; }
		}
		public string ControlName
		{
			set { mControlName = value; }
			get { return mControlName; }
		}
		public int ParentID
		{
			set { mParentID = value; }
			get { return mParentID; }
		}
		public string FormName
		{
			set { mFormName = value; }
			get { return mFormName; }
		}
		public string GroupText
		{
			set { mGroupText = value; }
			get { return mGroupText; }
		}
		public string GroupTextVN
		{
			set { mGroupTextVN = value; }
			get { return mGroupTextVN; }
		}
		public string GroupTextJP
		{
			set { mGroupTextJP = value; }
			get { return mGroupTextJP; }
		}
		public int Type
		{
			set { mType = value; }
			get { return mType; }
		}
	}
}

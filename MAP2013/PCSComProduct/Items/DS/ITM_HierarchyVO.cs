using System;
using System.Data;
namespace PCSComProduct.Items.DS
{
	[Serializable]
	public class ITM_HierarchyVO
	{
		private int mHierarchyID;
		private int mSource;
		private int mDes;

		public int HierarchyID
		{
			set { mHierarchyID = value; }
			get { return mHierarchyID; }
		}
		public int Source
		{
			set { mSource = value; }
			get { return mSource; }
		}
		public int Des
		{
			set { mDes = value; }
			get { return mDes; }
		}
	}
}

using System;
using System.Data;
namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_FunctionVO
	{
		private int mFunctionID;
		private string mCode;
		private string mDescription;

		public int FunctionID
		{
			set { mFunctionID = value; }
			get { return mFunctionID; }
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

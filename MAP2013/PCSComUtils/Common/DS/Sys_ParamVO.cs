using System;
using System.Data;

namespace PCSComUtils.Common.DS
{
	[Serializable]
	public class Sys_ParamVO
	{
		private int mParamID;
		private string mName;
		private string mValue;

		public int ParamID
		{
			set { mParamID = value; }
			get { return mParamID; }
		}
		public string Name
		{
			set { mName = value; }
			get { return mName; }
		}
		public string Value
		{
			set { mValue = value; }
			get { return mValue; }
		}
	}
}

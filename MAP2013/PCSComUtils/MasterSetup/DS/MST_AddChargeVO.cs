using System;
using System.Data;

namespace PCSComUtils.MasterSetup.DS
{
	
	[Serializable]
	public class MST_AddChargeVO
	{
		private int mAddChargeID;
		private string mCode;
		private string mDescription;
		private decimal mVAT;

		public int AddChargeID
		{
			set { mAddChargeID = value; }
			get { return mAddChargeID; }
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

		public decimal VAT
		{
			get { return mVAT; }
			set { mVAT = value; }
		}
	}
}

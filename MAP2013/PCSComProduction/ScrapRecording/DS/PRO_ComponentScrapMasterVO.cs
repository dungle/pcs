using System;
using System.Data;

namespace PCSComProduction.ScrapRecording.DS
{
	[Serializable]
	public class PRO_ComponentScrapMasterVO
	{
		private int mComponentScrapMasterID;
		private string mScrapNo;
		private DateTime mPostDate;
		private int mCCNID;
		private int mMasterLocationID;
		private int mProductionLineID;

		public int ComponentScrapMasterID
		{
			set { mComponentScrapMasterID = value; }
			get { return mComponentScrapMasterID; }
		}
		public string ScrapNo
		{
			set { mScrapNo = value; }
			get { return mScrapNo; }
		}
		public DateTime PostDate
		{
			set { mPostDate = value; }
			get { return mPostDate; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int MasterLocationID
		{
			set { mMasterLocationID = value; }
			get { return mMasterLocationID; }
		}
		public int ProductionLineID
		{
			set { mProductionLineID = value; }
			get { return mProductionLineID; }
		}
	}
}

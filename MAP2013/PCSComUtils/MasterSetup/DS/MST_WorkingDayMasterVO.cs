using System;
using System.Data;

namespace PCSComUtils.MasterSetup.DS
{
	[Serializable]
	public class MST_WorkingDayMasterVO
	{
		private int mWorkingDayMasterID;
		private bool mSun;
		private int mCCNID;
		private int mYear;
		private bool mMon;
		private bool mTue;
		private bool mWed;
		private bool mThu;
		private bool mFri;
		private bool mSat;

		public int WorkingDayMasterID
		{
			set { mWorkingDayMasterID = value; }
			get { return mWorkingDayMasterID; }
		}
		public bool Sun
		{
			set { mSun = value; }
			get { return mSun; }
		}
		public int CCNID
		{
			set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int Year
		{
			set { mYear = value; }
			get { return mYear; }
		}
		public bool Mon
		{
			set { mMon = value; }
			get { return mMon; }
		}
		public bool Tue
		{
			set { mTue = value; }
			get { return mTue; }
		}
		public bool Wed
		{
			set { mWed = value; }
			get { return mWed; }
		}
		public bool Thu
		{
			set { mThu = value; }
			get { return mThu; }
		}
		public bool Fri
		{
			set { mFri = value; }
			get { return mFri; }
		}
		public bool Sat
		{
			set { mSat = value; }
			get { return mSat; }
		}
	}
}

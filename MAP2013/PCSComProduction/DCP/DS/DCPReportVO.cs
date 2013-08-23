using System;
using System.Collections;
using System.Data;

namespace PCSComProduction.DCP.DS
{
	/// <summary>
	/// DCP Report Value Object
	/// </summary>
	[Serializable]
	public class DCPReportVO
	{
		private DataSet mHugeData;
		private DataTable mReportData;
		private ArrayList mReportDates;
		private ArrayList mShifts;
		private ArrayList mStandardCapacity;
		private ArrayList mTotalRequiredCapacity;
		private ArrayList mTotalChangeTime;
		private ArrayList mTotalCheckpointTime;
		private ArrayList mEffective;
		private ArrayList mRemainCapacity;
		private ArrayList mCategoriesOnDay;
		private ArrayList mProducts;
		private int mStartDay;
		private int mEndDay;
		private int mMonth;
		private int mYear;
		private string mCCN;
		private string mCycle;
		private string mWorkCenter;
		private string mCategory;
		private string mPartNumber;
		private string mPartName;
		private string mModel;
		private bool mIsDCPReport;
		private string mTableName;
		private ArrayList mOffdays;
		private ArrayList mHolidays;

		public ArrayList Offdays
		{
			get { return mOffdays; }
			set { mOffdays = value; }
		}

		public ArrayList Holidays
		{
			get { return mHolidays; }
			set { mHolidays = value; }
		}

		public ArrayList Products
		{
			get { return mProducts; }
			set { mProducts = value; }
		}

		public DataSet HugeData
		{
			get { return mHugeData; }
			set { mHugeData = value; }
		}

		public bool IsDCPReport
		{
			get { return mIsDCPReport; }
			set { mIsDCPReport = value; }
		}

		public string TableName
		{
			get { return mTableName; }
			set { mTableName = value; }
		}

		/// <summary>
		/// CCN Code
		/// </summary>
		public string CCN
		{
			get { return mCCN; }
			set { mCCN = value; }
		}

		/// <summary>
		/// DCP Cycle Option
		/// </summary>
		public string Cycle
		{
			get { return mCycle; }
			set { mCycle = value; }
		}

		/// <summary>
		/// Work Center Code
		/// </summary>
		public string WorkCenter
		{
			get { return mWorkCenter; }
			set { mWorkCenter = value; }
		}

		/// <summary>
		/// Category Code
		/// </summary>
		public string Category
		{
			get { return mCategory; }
			set { mCategory = value; }
		}

		/// <summary>
		/// Part Number
		/// </summary>
		public string PartNumber
		{
			get { return mPartNumber; }
			set { mPartNumber = value; }
		}

		/// <summary>
		/// Part Name
		/// </summary>
		public string PartName
		{
			get { return mPartName; }
			set { mPartName = value; }
		}

		/// <summary>
		/// Part Model
		/// </summary>
		public string Model
		{
			get { return mModel; }
			set { mModel = value; }
		}

		/// <summary>
		/// Reported Month
		/// </summary>
		public int Month
		{
			get { return mMonth; }
			set { mMonth = value; }
		}

		/// <summary>
		/// Reported Year
		/// </summary>
		public int Year
		{
			get { return mYear; }
			set { mYear = value; }
		}

		/// <summary>
		/// Begin day of report
		/// </summary>
		public int StartDay
		{
			get { return mStartDay; }
			set { mStartDay = value; }
		}

		/// <summary>
		/// End day of report
		/// </summary>
		public int EndDay
		{
			get { return mEndDay; }
			set { mEndDay = value; }
		}

		/// <summary>
		/// Categories/Days
		/// </summary>
		public ArrayList CategoriesOnDay
		{
			get { return mCategoriesOnDay; }
			set { mCategoriesOnDay = value; }
		}

		/// <summary>
		/// Remaind Capacity
		/// </summary>
		public ArrayList RemainCapacity
		{
			get { return mRemainCapacity; }
			set { mRemainCapacity = value; }
		}

		public ArrayList Effective
		{
			get { return mEffective; }
			set { mEffective = value; }
		}

		public ArrayList TotalRequiredCapacity
		{
			get { return mTotalRequiredCapacity; }
			set { mTotalRequiredCapacity = value; }
		}

		public ArrayList TotalChangeTime
		{
			get { return mTotalChangeTime; }
			set { mTotalChangeTime = value; }
		}

		public ArrayList TotalCheckpointTime
		{
			get { return mTotalCheckpointTime; }
			set { mTotalCheckpointTime = value; }
		}

		public ArrayList StandardCapacity
		{
			get { return mStandardCapacity; }
			set { mStandardCapacity = value; }
		}

		public ArrayList Shifts
		{
			get { return mShifts; }
			set { mShifts = value; }
		}
		/// <summary>
		/// Report Data
		/// </summary>
		public DataTable ReportData
		{
			get { return mReportData; }
			set { mReportData = value; }
		}

		/// <summary>
		/// All Report Dates
		/// </summary>
		public ArrayList ReportDates
		{
			get { return mReportDates; }
			set { mReportDates = value; }
		}
	}
}

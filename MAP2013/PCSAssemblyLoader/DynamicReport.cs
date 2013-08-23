using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.PCSExc;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.DataAccess;
using PCSComUtils.Common;

namespace PCSAssemblyLoader
{
	/// <summary>
	/// This class is the super class that the others report file must be inherited
	/// </summary>
	[Serializable]
	public class DynamicReport
	{
		public const string THIS = "PCSAssemblyLoader.DynamicReport";
		private string mTableName;
		private DataTable mReportData;
		private bool mUseNumberList = false;
		private bool mIsDCPReport = false;
		private ArrayList mReportDates;
		private ArrayList mShifts;
		private ArrayList mStandardCapacity;
		private ArrayList mTotalRequiredCapacity;
		private ArrayList mTotalChangeTime;
		private ArrayList mTotalCheckpointTime;
		private ArrayList mEffective;
		private ArrayList mRemainCapacity;
		private ArrayList mCategoriesOnDay;
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
		private DataTable mAllChild;

		public DataTable AllChild
		{
			get { return mAllChild; }
			set { mAllChild = value; }
		}

		public string CCN
		{
			get { return mCCN; }
			set { mCCN = value; }
		}

		public string Cycle
		{
			get { return mCycle; }
			set { mCycle = value; }
		}

		public string WorkCenter
		{
			get { return mWorkCenter; }
			set { mWorkCenter = value; }
		}

		public string Category
		{
			get { return mCategory; }
			set { mCategory = value; }
		}

		public string PartNumber
		{
			get { return mPartNumber; }
			set { mPartNumber = value; }
		}

		public string PartName
		{
			get { return mPartName; }
			set { mPartName = value; }
		}

		public string Model
		{
			get { return mModel; }
			set { mModel = value; }
		}

		public int Month
		{
			get { return mMonth; }
			set { mMonth = value; }
		}

		public int Year
		{
			get { return mYear; }
			set { mYear = value; }
		}

		public int StartDay
		{
			get { return mStartDay; }
			set { mStartDay = value; }
		}

		public int EndDay
		{
			get { return mEndDay; }
			set { mEndDay = value; }
		}

		public ArrayList CategoriesOnDay
		{
			get { return mCategoriesOnDay; }
			set { mCategoriesOnDay = value; }
		}

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

		public string TableName
		{
			get { return mTableName; }
			set { mTableName = value; }
		}

		public DataTable ReportData
		{
			get { return mReportData; }
			set { mReportData = value; }
		}

		public ArrayList ReportDates
		{
			get { return mReportDates; }
			set { mReportDates = value; }
		}

		public bool UseNumberList
		{
			get { return mUseNumberList; }
			set { mUseNumberList = value; }
		}

		public bool IsDCPReport
		{
			get { return mIsDCPReport; }
			set { mIsDCPReport = value; }
		}

		public DynamicReport()
		{}

		public DynamicReport ExecuteCommand(string pstrCommand)
		{
			const char DELIMITER = ' ';
			try
			{
				DynamicReport objDynamicReport = new DynamicReport();
				string strTableName = string.Empty;
				ViewReportBO boViewReport = new ViewReportBO();
				int intMaxID = boViewReport.GetMaxReportHistoryID();
				string[] arrCommand = pstrCommand.Split(DELIMITER);
				boViewReport.CreateHistoryTable(arrCommand, intMaxID, out strTableName);
				objDynamicReport.mTableName = strTableName;
				DataSet dstReportData = new DataSet();
				dstReportData = boViewReport.ExecuteReportCommand(pstrCommand, strTableName);
				objDynamicReport.mReportData = dstReportData.Tables[strTableName];
				return objDynamicReport;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public static string GetConnectionString()
		{
			try
			{
				return Utils.Instance.ConnectionString;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
//using PCSAssemblyLoader;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using PCSUtils;
using Utils = PCSComUtils.DataAccess.Utils;
using PCSUtils.Utils;
using C1.Win.C1Preview;

namespace WorkCenterListReport
{
	/// <summary>	
	/// </summary>
	[Serializable]
	public class WorkCenterListReport : MarshalByRefObject, IDynamicReport
	{
		public WorkCenterListReport()
		{}


		#region IDynamicReport Implementation
		private string mConnectionString;
		private ReportBuilder mReportBuilder;
		private C1PrintPreviewControl mReportViewer;
		private bool mUseReportViewerRenderEngine = true;	
		private object mResult;
		private string mstrReportDefinitionFolder = string.Empty;

		/// <summary>
		/// ConnectionString, provide for the Dynamic Report
		/// ALlow Dynamic Report to access the DataBase of PCS
		/// </summary>
		public string PCSConnectionString
		{
			get { return mConnectionString; }
			set { mConnectionString = value; }
		}

		/// <summary>
		/// Report Builder Utility Object
		/// Dynamic Report can use this object to render, modify, layout the report
		/// </summary>
		public ReportBuilder PCSReportBuilder
		{
			get { return mReportBuilder; }
			set { mReportBuilder = value; }
		}

		/// <summary>
		/// ReportViewer Object, provide for the DynamicReport, 
		/// allow Dynamic Report to manipulate with the REportViewer, 
		/// modify the report after rendered if needed
		/// </summary>
		public C1PrintPreviewControl PCSReportViewer
		{
			get { return mReportViewer; }
			set { mReportViewer = value; }
		}

		/// <summary>
		/// Store other result if any. Ussually we store return DataTable here to display on the ReportViewer Form's Grid
		/// </summary>
		public object Result
		{
			get { return mResult; }
			set { mResult = value; }
		}


		/// <summary>
		/// Notify PCS whether the rendering report process is run by 
		/// this IDynamicReport
		/// or the ReportViewer Engine (in the ReportViewer form)
		/// </summary>
		public bool UseReportViewerRenderEngine
		{
			get
			{
				return mUseReportViewerRenderEngine;
			}
			set
			{
				mUseReportViewerRenderEngine = value;
			}
		}

		
		/// <summary>
		/// Inform External Process where to find out the ReportLayout	 ( the PCS' ReportDefinition Folder Path )
		/// </summary>				
		public string ReportDefinitionFolder
		{
			get 
			{
				return mstrReportDefinitionFolder;
			}
			set
			{
				mstrReportDefinitionFolder = value;
			}
		}


		private string mstrReportLayoutFile = string.Empty;		
		/// <summary>
		/// Inform External Process about the Layout file
		/// in which PCS instruct to use
		/// (PCS will assign this property while ReportViewer Form execute,
		/// ReportVIewer form will use the layout file in the report config entry to put in this property)
		/// </summary>		
		public string ReportLayoutFile
		{
			get 
			{
				return mstrReportLayoutFile;
			}
			set
			{
				mstrReportLayoutFile = value;
			}
		}


		public object Invoke(string pstrMethod, object[] pobjParameters)
		{
			return this.GetType().InvokeMember(pstrMethod, BindingFlags.InvokeMethod, null, this, pobjParameters);
		}


		#endregion

		/// <summary>
		/// Main function, generate the result data Table for the REPORT VIEWER
		/// </summary>
		/// <param name="pstrCCNID"></param>
		/// <returns></returns>
		public DataTable ExecuteReport(string pnCCNID, string pstrDepartmentID_LIST, string pstrProductionLineID_LIST)
		{
			const string METHOD_NAME = ".GetWorkCenterListData()";
			const string TABLE_NAME = "WorkCenterListReport";

			const string CODE = "Code";
			const string NAME = "Name";
			const string DESCRIPTION = "Description";
			const string BEGIN_DATE = "Begin Date";
			const string END_DATE = "End Date";
			const string FACTOR = "Factor";
			const string TYPE = "Type";
			const string CREW_SIZE = "Crew Size";
			const string NO_OF_MACH = "No. of Mach";
			const string CAPACITY = "Capacity";
			const string SHIFT = "Shift";

			/// key field of table containing selected fields
			/// We use 2 fields to select exactly a row from the dtbSourceData
			const string WORKCENTERID = "WorkCenterID";
			const string WCCAPACITYID = "WCCapacityID";
			
			System.Data.DataTable dtbRet;
			System.Data.DataTable dtbSourceData;			
			System.Data.DataTable dtbWorkCenterListDistinct;

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = string.Empty;
			
			#region BUILD THE DATA TABLE
			
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS = null;
				ocmdPCS = null;
			
				strSql = 
					" select  " + 
					" MST_WorkCenter.WorkCenterID as [WorkCenterID], " +
					" MST_WorkCenter.Code as [Code], " + 
					" MST_WorkCenter.Name as [Name], " + 
					" MST_WorkCenter.Description as [Description], " + 
					" PRO_WCCapacity.WCCapacityID as [WCCapacityID], " +
					" PRO_WCCapacity.BeginDate as [Begin Date], " + 
					" PRO_WCCapacity.EndDate as [End Date], " + 
					" PRO_WCCapacity.Factor as [Factor], " + 
					" CASE " + 
					" WHEN PRO_WCCapacity.WCType = 0 THEN 'Labor' " +
					" WHEN PRO_WCCapacity.WCType = 1 THEN 'Machine' " +
					" END " + 
					"as [Type], " + 
					" PRO_WCCapacity.CrewSize as [Crew Size], " + 
					" PRO_WCCapacity.MachineNo as [No. of Mach], " + 
					" PRO_WCCapacity.Capacity as [Capacity], " + 
					" PRO_Shift.ShiftDesc as [Shift] " + 
					"  " + 
					" from  " + 
					" MST_WorkCenter " + 
					" join PRO_WCCapacity " + 
					" on MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID " + 
					" join PRO_ShiftCapacity " + 
					" on PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID " + 
					" join PRO_Shift " + 
					" on PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID " + 
					
"  " + 
					" join PRO_ProductionLine " + 
					" on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID " + 
					" and MST_WorkCenter.CCNID =  "  + pnCCNID  + 	

					((pstrDepartmentID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and PRO_ProductionLine.DepartmentID in (" +pstrDepartmentID_LIST+ ") /*Department LIST*/ ")) + 
					((pstrProductionLineID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and PRO_ProductionLine.ProductionLineID in (" +pstrProductionLineID_LIST+ ") /*ProductionLine LIST*/	 ")) + 
"  " + 

					" Order by [Code], [Name], [BeginDate], [EndDate] " + 
					"  " ;


				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbSourceData = dstPCS.Tables[TABLE_NAME].Copy();
				}
				else
				{
					dtbSourceData = new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new Exception(strSql,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}			
			#endregion			 
			
			#region BUILD THE DISTINCT WORK CENTER LIST WITHOUT SHIFT COLUMN
			try 
			{
				DataSet dstPCS = new DataSet();
				oconPCS =null;
				ocmdPCS = null;
			
				strSql = 
					" select DISTINCT " + 
					" MST_WorkCenter.WorkCenterID as [WorkCenterID], " +
					" MST_WorkCenter.Code as [Code], " + 
					" MST_WorkCenter.Name as [Name], " + 
					" MST_WorkCenter.Description as [Description], " + 
					" PRO_WCCapacity.WCCapacityID as [WCCapacityID], " +
					" PRO_WCCapacity.BeginDate as [Begin Date], " + 
					" PRO_WCCapacity.EndDate as [End Date], " + 
					" PRO_WCCapacity.Factor as [Factor], " + 
					" CASE " + 
					" WHEN PRO_WCCapacity.WCType = 0 THEN 'Labor' " +
					" WHEN PRO_WCCapacity.WCType = 1 THEN 'Machine' " +
					" END " + 
					"as [Type], " + 
					" PRO_WCCapacity.CrewSize as [Crew Size], " + 
					" PRO_WCCapacity.MachineNo as [No. of Mach], " + 
					" PRO_WCCapacity.Capacity as [Capacity] " + 					
					"  " + 
					" from  " + 
					" MST_WorkCenter " + 
					" join PRO_WCCapacity " + 
					" on MST_WorkCenter.WorkCenterID = PRO_WCCapacity.WorkCenterID " + 
					" join PRO_ShiftCapacity " + 
					" on PRO_WCCapacity.WCCapacityID = PRO_ShiftCapacity.WCCapacityID " + 
					" join PRO_Shift " + 
					" on PRO_ShiftCapacity.ShiftID = PRO_Shift.ShiftID " + 
					
"  " + 
					" join PRO_ProductionLine " + 
					" on MST_WorkCenter.ProductionLineID = PRO_ProductionLine.ProductionLineID " + 
					" and MST_WorkCenter.CCNID =  "  + pnCCNID  + 	

					((pstrDepartmentID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and PRO_ProductionLine.DepartmentID in (" +pstrDepartmentID_LIST+ ") /*Department LIST*/ ")) + 
					((pstrProductionLineID_LIST.Trim() == string.Empty )?(string.Empty) : ( " 	and PRO_ProductionLine.ProductionLineID in (" +pstrProductionLineID_LIST+ ") /*ProductionLine LIST*/	 ")) + 
"  " + 

					" Order by [Code], [Name], [BeginDate], [EndDate] " + 
					"  " ;




				
				oconPCS = new OleDbConnection(mConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				
				ocmdPCS.Connection.Open();				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, TABLE_NAME);

				if(dstPCS.Tables.Count > 0)
				{
					dtbWorkCenterListDistinct = dstPCS.Tables[TABLE_NAME].Copy();
				}
				else
				{
					dtbWorkCenterListDistinct = new DataTable();
				}
			}
			catch(OleDbException ex)
			{
				throw new Exception(strSql,ex);
			}			
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
			
			#endregion		

			#region TRANSFORM ORIGINAL TABLE FOR REPORT

			#region BUILD RESULT TABLE SCHEMA
			/// Shallow copy, only get the schema of the Source Data table.
			dtbRet = dtbSourceData.Clone();
			#endregion

			try
			{				
				foreach(DataRow drowWCItem in dtbWorkCenterListDistinct.Rows)
				{
					string strFilter = string.Format("[{0}]='{1}' AND [{2}]='{3}' ",
						WORKCENTERID,drowWCItem[WORKCENTERID],
						WCCAPACITYID,drowWCItem[WCCAPACITYID]
						);

					/// Create DUMMYROW FIRST
					DataRow dtrNew = dtbRet.NewRow();

					string strSumShift = string.Empty;	// we will assign this string to the SHIFT Field of dtbRet
					// builde the string SumShift here, loop by each row in dtbSourceData, extract the SHift, += to the strSumShift	
					DataRow[] dtrows = dtbSourceData.Select(strFilter,SHIFT + " ASC ");
					foreach(DataRow dtr in dtrows)
					{
						/// fill data to the dummy row
						/// these column is persistance, we always set to the first rows
						dtrNew[CODE] = dtrows[0][CODE];
						dtrNew[NAME] = dtrows[0][NAME];
						dtrNew[DESCRIPTION] = dtrows[0][DESCRIPTION];
						dtrNew[BEGIN_DATE] = dtrows[0][BEGIN_DATE];
						dtrNew[END_DATE] = dtrows[0][END_DATE];
						dtrNew[FACTOR] = dtrows[0][FACTOR];
						dtrNew[TYPE] = dtrows[0][TYPE];
						dtrNew[CREW_SIZE] = dtrows[0][CREW_SIZE];
						dtrNew[NO_OF_MACH] = dtrows[0][NO_OF_MACH];
						dtrNew[CAPACITY] = dtrows[0][CAPACITY];

						strSumShift += dtr[SHIFT] + ",";
					}
					
					dtrNew[SHIFT] = strSumShift.Trim(',');
					// add to the transform data table
					dtbRet.Rows.Add(dtrNew);										
				}
			}
			catch(Exception ex)
			{				
				throw ex;
			}

			dtbRet.Columns.Remove(WCCAPACITYID);			
			#endregion

			UseReportViewerRenderEngine = true;
			Result = dtbRet;
			return dtbRet;
		}
	}
}

using System.Collections;
using System.Data;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class ViewReportBO 
	{
		public DataSet GetDataFromTable(string pstrField, string pstrTableName, string[] pstrFilterFields)
		{
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();
			return dsReportPara.GetDataFromTable(pstrField, pstrTableName, pstrFilterFields);
		}
		public DataSet ExecuteSqlClause(string pstrSqlClause, string pstrWhereClause)
		{
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();
			return dsReportPara.ExecuteSqlClause(pstrSqlClause, pstrWhereClause);
		}
		public int GetMaxReportHistoryID()
		{
			sys_ReportHistoryDS dsHistoryPara = new sys_ReportHistoryDS();
			return dsHistoryPara.GetMaxReportHistoryID();
		}

		public void DeleteHistoryPara(object pobjHistory)
		{
			sys_ReportHistoryVO voReportHistory = (sys_ReportHistoryVO)pobjHistory;
			sys_ReportHistoryParaDS dsReportHistoryPara = new sys_ReportHistoryParaDS();
			dsReportHistoryPara.Delete(voReportHistory.HistoryID);
		}
		public void DeleteHistory(object pobjHistory)
		{
			sys_ReportHistoryVO voReportHistory = (sys_ReportHistoryVO)pobjHistory;
			sys_ReportHistoryDS dsReportHistory = new sys_ReportHistoryDS();
			sys_ReportHistoryParaDS dsReportHistoryPara = new sys_ReportHistoryParaDS();
				
			// delete history param first
			dsReportHistoryPara.Delete(voReportHistory.HistoryID);
			// delete history object
			dsReportHistory.Delete(voReportHistory.HistoryID);
			// drop history table from databas
				
			try
			{				
				dsReportHistory.DropHistoryTables(voReportHistory.HistoryID);
			}
			catch
			{				
				dsReportHistory.DropHistoryTables(voReportHistory.TableName);
			}	
		}
		public void DropHistoryTables(int pintHistoryID)
		{
			sys_ReportHistoryDS dsReportHistory = new sys_ReportHistoryDS();
			dsReportHistory.DropHistoryTables(pintHistoryID);
		}
		public DataSet CreateHistoryTable(string[] parrCommand, int pintMaxID, out string ostrTableName)
		{
			sys_ReportHistoryDS dsHistory = new sys_ReportHistoryDS();
			DataSet dstResult = dsHistory.CreateHistoryTable(parrCommand, pintMaxID, out ostrTableName);
			return dstResult;
		}
		public void AddHistory(object pobjHistoryVO)
		{
			sys_ReportHistoryDS  dsHistory = new sys_ReportHistoryDS();
			dsHistory.Add(pobjHistoryVO);
		}
		public void AddHistoryPara(object pobjHistoryParaVO)
		{
			sys_ReportHistoryParaDS  dsHistoryPara = new sys_ReportHistoryParaDS();
			dsHistoryPara.Add(pobjHistoryParaVO);
		}
		public DataSet ExecuteReportCommand(string pstrReportCommand)
		{
			DataSet dstResult = new DataSet();
			sys_ReportDS dsReport = new sys_ReportDS();
			dstResult = dsReport.ExecuteReportCommand(pstrReportCommand);
			return dstResult;
		}
		public DataSet ExecuteReportCommand(string pstrReportCommand, string pstrTableName)
		{
			DataSet dstResult = new DataSet();
			sys_ReportDS dsReport = new sys_ReportDS();
			dstResult = dsReport.ExecuteReportCommand(pstrReportCommand, pstrTableName);
			return dstResult;
		}
		public int SumFieldsWidth(string pstrReportID)
		{
			sys_ReportFieldsDS dsField = new sys_ReportFieldsDS();
			return dsField.SumFieldsWidth(pstrReportID);
		}
		public DataTable GetDataFromHistoryTable(string pstrHistoryTableName)
		{
			sys_ReportHistoryDS dsHistory = new sys_ReportHistoryDS();
			return dsHistory.GetDataFromHistoryTable(pstrHistoryTableName);
		}
		public ArrayList GetHistoryReportByUser(string pstrUsername)
		{
			sys_ReportHistoryDS dsHistoryPara = new sys_ReportHistoryDS();
			return dsHistoryPara.GetHistoryReportByUser(pstrUsername);
		}
		public bool PushDataTableIntoNewDatabaseTable(DataTable pdtb, string pstrDBNewTableName)
		{
			sys_ReportHistoryDS dsHistoryPara = new sys_ReportHistoryDS();
			return dsHistoryPara.PushDataTableIntoNewDatabaseTable(pdtb, pstrDBNewTableName);
		}
		public ArrayList GetAllFieldGroups(string pstrReportID)
		{
			Sys_FieldGroupDetailDS dsFieldGroup = new Sys_FieldGroupDetailDS();
			return dsFieldGroup.List(pstrReportID);
		}
		public ArrayList GetAllGroups(string pstrReportID)
		{
			Sys_FieldGroupDS dsFieldGroup = new Sys_FieldGroupDS();
			return dsFieldGroup.GetAllGroups(pstrReportID);
		}
		public object GetReportByReportID(string pstrReportID)
		{
			return (new sys_ReportDS()).GetObjectVO(pstrReportID);
		}
	}
}

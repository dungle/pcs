using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComProduction.DCP.BO
{
	/// <summary>
	/// Biz object for import planning data from excel file
	/// </summary>
	
	public class ImportPlanDataBO
	{
		public void ImportData(DataTable pdtbData, int cycleId, int workCenterId, int shiftId, DateTime month)
		{
			ImportPlanDataDS dsImport = new ImportPlanDataDS();
			// delete A1 table first
			dsImport.Delete();
			// put data into A1 table
			DataSet pdstData = new DataSet();
			pdstData.Tables.Add(pdtbData);
			dsImport.UpdateDataSet(pdstData);
			// shift pattern
			PRO_ShiftPatternDS dsPattern = new PRO_ShiftPatternDS();
			PRO_ShiftPatternVO voPattern = (PRO_ShiftPatternVO)dsPattern.GetObjectVOByShiftID(shiftId);
			// build the sql string
			StringBuilder sbSql = new StringBuilder();
			for (int i = 1; i <= DateTime.DaysInMonth(month.Year, month.Month); i++)
			{
				string strFromDate = string.Format("'{0}-{1} {2}'", month.ToString("yyyy-MM"), i.ToString("00"), voPattern.WorkTimeFrom.ToString("HH:mm:ss"));
				string strToDate = string.Format("'{0}-{1} {2}'", month.ToString("yyyy-MM"), i.ToString("00"), voPattern.WorkTimeTo.ToString("HH:mm:ss"));
				DateTime dtmWorkingDate = new DateTime(month.Year, month.Month, i);
				sbSql.AppendFormat("SELECT {0} AS StartDate,", strFromDate);
				sbSql.AppendFormat("{0} AS DueDate,", strToDate);
				sbSql.AppendFormat("{0} AS WorkCenterID,", workCenterId);
				sbSql.Append("99999999.0002 AS TotalSecond,");
				sbSql.Append("99999999 AS DCPResultMasterID,");
				sbSql.AppendFormat("{0} AS ShiftID,", shiftId);
				sbSql.Append("1 AS IDNo,");
				sbSql.AppendFormat("{0} AS DCOptionMasterID,", cycleId);
				sbSql.AppendFormat("'{0}' AS WorkingDate,", dtmWorkingDate.ToString("yyyy-MM-dd HH:mm:ss"));
				sbSql.AppendFormat("ProductID, WOGeneratedID, ISNULL(F{0},0) AS Qty FROM A1", i);
				sbSql.Append("\n");
			    if (i < DateTime.DaysInMonth(month.Year, month.Month))
			    {
			        sbSql.Append("UNION ALL").Append("\n");
			    }
			}
			// put data into A2 from A1
			string strSql = "IF EXISTS(select id from dbo.sysobjects where id = object_id(N'A2') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\n"
				+ "BEGIN\n"
				+ " Drop Table A2\n"
				+ "End\n"
				+ "IF EXISTS(select id from dbo.sysobjects where id = object_id(N'A3') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\n"
				+ "Begin\n"
				+ " Drop Table A3\n"
				+ "End\n"
				+ "SELECT * INTO A2 FROM\n"
				+ "(" + sbSql + ") AS B1";
			Debug.WriteLine(strSql);
			dsImport.ExecuteCommand(strSql);
			// put data into A3 table
			strSql = "SELECT StartDate, DueDate, WorkCenterID, TotalSecond, DCPResultMasterID,"
				+ " ShiftID, DCOptionMasterID, WorkingDate, ProductID, WOGeneratedID, Qty, IDENTITY(int,1,1) as IDNo"
                + " INTO A3 FROM A2"
				+ " WHERE Qty > 0";
			dsImport.ExecuteCommand(strSql);
			// delete DCP result detail
			PRO_DCPResultDetailDS dsDCPResultDetail = new PRO_DCPResultDetailDS();
			dsDCPResultDetail.Delete(cycleId, workCenterId, shiftId, month);
			// put data to dcp result table
			strSql = "Update A3 \n"
				+ " Set TotalSecond=Round(ISNULL(Qty*("
				+ " select LTVariableTime from ITM_Product where productid=A3.productid),0),0)\n"
				+ " Insert Into PRO_DCPResultMaster (DCOptionMasterID,MasterShiftID,WorkCenterID,StartDateTime,DueDateTime,Quantity,ProductID,DeliveryQuantity)\n"
				+ " (Select DCOptionMasterID,ShiftID,WorkCenterID,\n"
				+ " StartDate,DueDate,Qty,ProductID,IDNo\n"
				+ "  from A3 where Qty>0)\n"
				+ " Update A3\n"
				+ " Set DCPResultMasterID=(select DCPResultMasterID from PRO_DCPResultMaster where DCOptionMasterID=A3.DCOptionMasterID\n"
				+ " and WorkCenterID=A3.WorkCenterID and ProductID=A3.ProductID AND DeliveryQuantity=A3.IDNo)\n"
				+ " Insert Into PRO_DCPResultDetail (WOGeneratedID, StartTime,EndTime,TotalSecond,Quantity,DCPResultMasterID,WorkingDate,ShiftID,Percentage)\n"
                + " (select WOGeneratedID, StartDate,DueDate,TotalSecond,qty,DCPResultMasterID,WorkingDate,ShiftID,'100' as Percentage FROM A3"
                + " Where Qty>0)\n"
				+ " Update PRO_DCPResultMaster\n"
				+ " set DeliveryQuantity=0";
			dsImport.ExecuteCommand(strSql);
		}
		public int GetMainWorkCenter(int productionLineId)
		{
			PRO_ProductionLineDS dsProLine = new PRO_ProductionLineDS();
			DataTable dtbWorkCenter = dsProLine.GetWorkCenterByProductionLine(productionLineId);
			DataRow[] drowMain = dtbWorkCenter.Select(MST_WorkCenterTable.ISMAIN_FLD + "=1");
			return drowMain.Length > 0 ? Convert.ToInt32(drowMain[0][MST_WorkCenterTable.WORKCENTERID_FLD]) : 0;
		}

	    /// <summary>
	    /// Get generated work order id based on cycle option and production line
	    /// </summary>
	    /// <param name="cycleOptionId"></param>
	    /// <param name="productionLineId"></param>
	    /// <returns></returns>
	    public int GetGeneratedWorkOrder(int cycleOptionId, int productionLineId)
	    {
	        var importDs = new ImportPlanDataDS();
	        return importDs.GetGeneratedWorkOrder(cycleOptionId, productionLineId);
	    }
	}
}

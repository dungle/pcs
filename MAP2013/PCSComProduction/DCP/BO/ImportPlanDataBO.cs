using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;



namespace PCSComProduction.DCP.BO
{
	public interface IImportPlanDataBO
	{
		
	}
	/// <summary>
	/// Biz object for import planning data from excel file
	/// </summary>
	
	public class ImportPlanDataBO : IImportPlanDataBO
	{
		public ImportPlanDataBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region IObjectBO Members

		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			// TODO:  Add ImportPlanDataBO.UpdateDataSet implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add ImportPlanDataBO.Update implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add ImportPlanDataBO.Delete implementation
		}

		public void Add(object pObjectDetail)
		{
			// TODO:  Add ImportPlanDataBO.Add implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add ImportPlanDataBO.GetObjectVO implementation
			return null;
		}

		#endregion

	
		public void ImportData(DataTable pdtbData, int pintCycleID, int pintWorkCenterID, int pintShiftID, DateTime pdtmMonth)
		{
			ImportPlanDataDS dsImport = new ImportPlanDataDS();
			/// delete A1 table first
			dsImport.Delete();
			/// put data into A1 table
			DataSet pdstData = new DataSet();
			pdstData.Tables.Add(pdtbData);
			dsImport.UpdateDataSet(pdstData);
			/// shift pattern
			PRO_ShiftPatternDS dsPattern = new PRO_ShiftPatternDS();
			PRO_ShiftPatternVO voPattern = (PRO_ShiftPatternVO)dsPattern.GetObjectVOByShiftID(pintShiftID);
			/// build the sql string
			StringBuilder sbSQL = new StringBuilder();
			for (int i = 1; i <= DateTime.DaysInMonth(pdtmMonth.Year, pdtmMonth.Month); i++)
			{
				string strFromDate = "'" + pdtmMonth.ToString("yyyy-MM") + "-" + i.ToString("00") + " " + voPattern.WorkTimeFrom.ToString("HH:mm:ss") + "'";
				string strToDate = "'" + pdtmMonth.ToString("yyyy-MM") + "-" + i.ToString("00") + " " + voPattern.WorkTimeTo.ToString("HH:mm:ss") + "'";
				DateTime dtmWorkingDate = new DateTime(pdtmMonth.Year, pdtmMonth.Month, i);
				sbSQL.Append("SELECT " + strFromDate + " AS StartDate,");
				sbSQL.Append(strToDate + " AS DueDate,");
				sbSQL.Append(pintWorkCenterID + " AS WorkCenterID,");
				sbSQL.Append("99999999.0002 AS TotalSecond,");
				sbSQL.Append("99999999 AS DCPResultMasterID,");
				sbSQL.Append(pintShiftID + " AS ShiftID,");
				sbSQL.Append("1 AS IDNo,");
				sbSQL.Append(pintCycleID + " AS DCOptionMasterID,");
				sbSQL.Append("'" + dtmWorkingDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AS WorkingDate,");
				sbSQL.Append("ProductID, ISNULL(F" + i + ",0) AS Qty FROM A1");
				sbSQL.Append("\n");
				if (i < DateTime.DaysInMonth(pdtmMonth.Year, pdtmMonth.Month))
					sbSQL.Append("UNION ALL").Append("\n");
			}
			/// put data into A2 from A1
			string strSql = "IF EXISTS(select id from dbo.sysobjects where id = object_id(N'A2') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\n"
				+ "BEGIN\n"
				+ " Drop Table A2\n"
				+ "End\n"
				+ "IF EXISTS(select id from dbo.sysobjects where id = object_id(N'A3') and OBJECTPROPERTY(id, N'IsUserTable') = 1)\n"
				+ "Begin\n"
				+ " Drop Table A3\n"
				+ "End\n"
				+ "SELECT * INTO A2 FROM\n"
				+ "(" + sbSQL.ToString() + ") AS B1";
			Debug.WriteLine(strSql);
			dsImport.ExecuteCommand(strSql);
			/// put data into A3 table
			strSql = "SELECT StartDate, DueDate, WorkCenterID, TotalSecond, DCPResultMasterID,"
				+ " ShiftID, DCOptionMasterID, WorkingDate, ProductID, Qty, IDENTITY(int,1,1) as IDNo"
				+ " INTO A3 FROM A2"
				+ " WHERE Qty > 0";
			dsImport.ExecuteCommand(strSql);
			/// delete DCP result detail
			PRO_DCPResultDetailDS dsDCPResultDetail = new PRO_DCPResultDetailDS();
			dsDCPResultDetail.Delete(pintCycleID, pintWorkCenterID, pintShiftID, pdtmMonth);
			/// put data to dcp result table
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
				+ " Insert Into PRO_DCPResultDetail (StartTime,EndTime,TotalSecond,Quantity,DCPResultMasterID,WorkingDate,ShiftID,Percentage)\n"
				+ " (select StartDate,DueDate,TotalSecond,qty,DCPResultMasterID,WorkingDate,ShiftID,'100' as Percentage FROM A3"
				+ " Where Qty>0)\n"
				+ " Update PRO_DCPResultMaster\n"
				+ " set DeliveryQuantity=0";
			dsImport.ExecuteCommand(strSql);
		}
	
		public int GetMainWorkCenter(int pintProductionLineID)
		{
			PRO_ProductionLineDS dsProLine = new PRO_ProductionLineDS();
			DataTable dtbWorkCenter = dsProLine.GetWorkCenterByProductionLine(pintProductionLineID);
			DataRow[] drowMain = dtbWorkCenter.Select(MST_WorkCenterTable.ISMAIN_FLD + "=1");
			if (drowMain.Length > 0)
				return Convert.ToInt32(drowMain[0][MST_WorkCenterTable.WORKCENTERID_FLD]);
			else
				return 0;
		}
	}
}

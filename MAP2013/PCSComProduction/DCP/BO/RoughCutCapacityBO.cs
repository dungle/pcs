using System;
using System.Collections;
using System.Data;

using System.Text;
using PCSComProduction.DCP.DS;
using PCSComUtils.Common;


namespace PCSComProduction.DCP.BO
{
	public interface IRoughCutCapacityBO
	{
		DataTable GetStandardCapacity(int pintCCNID, int pintProductionLineID);
		DataTable GetTotalRequiredCapacity(int pintProductionLineID, int pintCycleID, DateTime pdtmFromDate, DateTime pdtmToDate);
	}
	/// <summary>
	/// Business class for Rough Cut Capacity
	/// </summary>
	
	public class RoughCutCapacityBO : IRoughCutCapacityBO
	{
	
		public DataTable GetStandardCapacity(int pintCCNID, int pintProductionLineID)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetStandardCapacity(pintCCNID, pintProductionLineID);
		}
	
		public DataTable GetTotalRequiredCapacity(int pintProductionLineID, int pintCycleID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetTotalRequiredCapacity(pintProductionLineID, pintCycleID, pdtmFromDate, pdtmToDate);
		}
		public DataTable GetTotalRequiredCapacityByShift(int pintProductionLineID, int pintCycleID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetTotalRequiredCapacityByShift(pintProductionLineID, pintCycleID, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetPlanningOffset(int pintCCNID)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetPlanningOffset(pintCCNID.ToString());
		}
	
		public DataTable GetOverData(int pintProductionLineID, int pintCycleID)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetOverItems(pintProductionLineID, pintCycleID);
		}
	
		public DataTable GetWorkingDateFromWCCapacity(int pintProductionLineID)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetWorkingDateFromWCCapacity(pintProductionLineID);
		}

	
		public DataTable ListProduct(int pintProductionLineID, string pstrProductID)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.ListProduct(pintProductionLineID, pstrProductID);
		}

	
		public DataTable GetBeginStock(int pintCycleID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetBeginStock(pintCycleID);
		}

	
		public DataTable GetDeliveryForSO(string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetDeliveryForSO(strItems, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetProduce(string pstrOptionID, int pintProductionLineID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetProduce(pstrOptionID, pintProductionLineID, strItems, pdtmFromDate, pdtmToDate);
		}
	
		public DataTable GetDeliveryForParent(string pstrOptionID, string strItems, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			DCPReportDS dsDCP = new DCPReportDS();
			return dsDCP.GetDeliveryForParent(pstrOptionID, strItems, pdtmFromDate, pdtmToDate);
		}

	
		public DataTable GetShiftPattern(int pintProductionLineID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetShiftPattern(pintProductionLineID);
		}
	
		public DataTable GetProductionGroup(int pintProductionLineID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetProductionGroup(pintProductionLineID);
		}

	
		public DataTable GetDCPResultSchema()
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetDCPResultSchema();
		}

	
		public object GetCycleInfo(int pintCycleID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetCycleInfo(pintCycleID);
		}
		#region IObjectBO Members

	
		public void UpdateDataSet(DataSet dstData)
		{
			
		}
	
		public void UpdateOver(DataTable pdtbData, DataTable pdtbOverData, ArrayList parrResultMasterID, StringBuilder psbResultMasterID, object pobjCycle)
		{
			PRO_DCPResultDetailDS dsDCPDetail = new PRO_DCPResultDetailDS();
			PRO_DCOptionMasterVO voCycle = (PRO_DCOptionMasterVO)pobjCycle;
			string strResultMasterIDs = "0";

			#region check result master id in order to delete master data

			// get the list of master id which will not be deleted
			DataTable dtbNotDeleteID = dsDCPDetail.GetNotDeleteMasterID(psbResultMasterID.ToString(), voCycle.DCOptionMasterID);
			foreach (DataRow drowData in dtbNotDeleteID.Rows)
				parrResultMasterID.Remove(drowData[PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD].ToString());

			foreach (string strMasterID in parrResultMasterID)
				if (pdtbData.Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + strMasterID).Length == 0
					&& pdtbOverData.Select(PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD + "=" + strMasterID).Length == 0)
					strResultMasterIDs += "," + strMasterID;
			
			#endregion

			StringBuilder sbOverID = new StringBuilder();
			foreach (DataRow drowOver in pdtbOverData.Rows)
				sbOverID.Append(drowOver[PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD].ToString()).Append(",");
			sbOverID.Append("0"); // avoid exception
			// delete over data first
			dsDCPDetail.DeleteOverData(psbResultMasterID.ToString(), strResultMasterIDs, sbOverID.ToString());
			// update quantity of over item
			dsDCPDetail.UpdateOver(pdtbOverData);
			// update new data to dcp table
			DataSet dstData = new DataSet();
			dstData.Tables.Add(pdtbData);
			dsDCPDetail.UpdateDataSet(dstData);
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add RoughCutCapacityBO.Update implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add RoughCutCapacityBO.Delete implementation
		}

		public void Add(object pObjectDetail)
		{
			// TODO:  Add RoughCutCapacityBO.Add implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add RoughCutCapacityBO.GetObjectVO implementation
			return null;
		}

		#endregion

	
		public DataTable GetOrderProduce(int pintCycleID)
		{
			DCPReportDS dsReport = new DCPReportDS();
			return dsReport.GetOrderProduce(pintCycleID);
		}

	
		public DataTable GetChangeCategoryTime()
		{
			PRO_ChangeCategoryMatrixDS dsChangeCategory = new PRO_ChangeCategoryMatrixDS();
			return dsChangeCategory.List().Tables[0];
		}
	}
}

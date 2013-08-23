using System;
using System.Collections;
using System.Data;
using PCSComProduction.DCP.DS;
using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.BO
{
	public class ProductionLineBO
	{
		#region ProductionLineBO's members
		
		/// <summary>
		/// Get list of Work center which belong to specific Production Line
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns></returns>
	
		public DataTable GetWorkCenterByProductionLine(int pintProductionLineID)
		{
			try
			{
				return (new PRO_ProductionLineDS()).GetWorkCenterByProductionLine(pintProductionLineID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
		/// <summary>
		/// Set Production Line value to Work Order
		/// </summary>
		/// <param name="phtbWorkCenterIDList"></param>
		/// <param name="pintProductionLineID"></param>
	
		public bool SetProductionLine4WorkOrder(Hashtable phtbWorkCenterIDList, int pintProductionLineID)
		{
			try
			{
				return (new PRO_ProductionLineDS()).SetProductionLine4WorkOrder(phtbWorkCenterIDList, pintProductionLineID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// Get production line code by ID
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Mar 22 2006</date>
	
		public DataTable GetProductionLineCode(int pintProductionLineID)
		{
			DataTable dtbReturn = new DataTable();
			PRO_ProductionLineDS dsProductionLine = new PRO_ProductionLineDS();
			dtbReturn = dsProductionLine.GetProductionLineByID(pintProductionLineID);
			return dtbReturn;
		}

	
		public DataSet List()
		{
			PRO_ProductionLineDS dsPro = new PRO_ProductionLineDS();
			return dsPro.List();
		}
		#endregion ProductionLineBO's members
	}
}

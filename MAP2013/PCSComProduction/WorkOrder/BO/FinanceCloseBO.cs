using System;
using System.Collections;
using System.Data;

using PCSComUtils.PCSExc;

using PCSComUtils.Common;
using PCSComProduction.WorkOrder.DS;

namespace PCSComProduction.WorkOrder.BO
{
	/// <summary>
	/// Summary description for FinanceCloseBO.
	/// </summary>
	
	public class FinanceCloseBO
	{
		#region IObjectBO Members
		/// <summary>
		/// Update status to ManufacturingClose and MfgCloseDate of each WorkOrderLine in ArrayList
		/// </summary>
		/// <param name="pdtmCloseDate">Close Date</param>
		/// <param name="parrSelectedLines">All selected work order line to be closed</param>
	
		public void CloseWorkOrderLines(DateTime pdtmCloseDate, ArrayList parrSelectedLines)
		{
			const string COLON = ",";
			try
			{
				string strListOfIds = string.Empty;
				for (int i = 0; i < parrSelectedLines.Count; i++)
				{
					strListOfIds += parrSelectedLines[i] + COLON;
				}
				// remove the last "," in string
				strListOfIds = strListOfIds.Remove(strListOfIds.Length - 1, 1);
				dsPRO_WorkOrderDetail = new PRO_WorkOrderDetailDS();
				dsPRO_WorkOrderDetail.CloseWorkOrderLines(WOLineStatus.FinClose, pdtmCloseDate, strListOfIds);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// - Search all work order line have status MfgClose and have Due Date between FromDueDate and ToDueDate
		/// - after search work order line succeed, we need to update OpenQuantity column for each work order line
		/// Open Quantity = WorkOrderLine.OrderQuantity - Completed Quantity - Scrap Quantity
		/// 	+ Completed Quantity = PRO_WorkOrderCompletionDS.GetCompletedQuantity(WorkOrderMasterID, WorkOrderDetailID)
		/// 	+ Scrap Quantity = PRO_AssemblyScrapMasterDS.GetScrapQuantity(WorkOrderMasterID, WorkOrderDetailID) + PRO_OperationScrapMasterDS.GetScrapQuantity(WorkOrderMasterID, WorkOrderDetailID)
		/// </summary>
		/// <param name="pdtmFromDueDate">From due date of work order line</param>
		/// <param name="pdtmToDueDate">To due date of work order line</param>
		/// <returns>All work order line have status is Released</returns>
	
		public DataSet SearchMfgCloseWOLines(int pintCCNID, int pintMasterLocationID, DateTime pdtmFromDueDate, DateTime pdtmToDueDate)
		{
			try
			{
				dsPRO_WorkOrderDetail = new PRO_WorkOrderDetailDS();
				return dsPRO_WorkOrderDetail.SearchWOForClose(WOLineStatus.MfgClose, pintCCNID, pintMasterLocationID, pdtmFromDueDate, pdtmToDueDate);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	
		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			// TODO:  Add FinanceCloseBO.UpdateDataSet implementation
		}

	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add FinanceCloseBO.Update implementation
		}

	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add FinanceCloseBO.Delete implementation
		}

	
		public void Add(object pObjectDetail)
		{
			// TODO:  Add FinanceCloseBO.Add implementation
		}

	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add FinanceCloseBO.GetObjectVO implementation
			return null;
		}

		#endregion
		private DS.PRO_WorkOrderDetailDS dsPRO_WorkOrderDetail;
	}
}

using System;
using System.Collections;
using System.Data;

using PCSComUtils.Common.BO;
using PCSComUtils.PCSExc;

using PCSComUtils.Common;
using PCSComProduction.WorkOrder.DS;


namespace PCSComProduction.WorkOrder.BO
{
	/// <summary>
	/// Summary description for ManufacturingCloseBO.
	/// </summary>
	public class ManufacturingCloseBO
	{
		#region IObjectBO Members
		
        /// <summary>
		/// Update status to ManufacturingClose and MfgCloseDate of each WorkOrderLine in ArrayList
		/// </summary>
		/// <param name="pdtmCloseDate">Close Date</param>
		/// <param name="parrSelectedLines">All selected work order line to be closed</param>
		/// <author>Trada</author>
		/// <date>Friday, June </date>
		public void CloseWorkOrderLines(DateTime pdtmCloseDate, ArrayList parrSelectedLines)
		{
            dsPRO_WorkOrderDetail = new PRO_WorkOrderDetailDS();
            ArrayList arrListOfIds = new ArrayList();
            arrListOfIds = UtilsBO.GetSplitList(parrSelectedLines, 200);
            if (arrListOfIds.Count > 0)
            {
                foreach (object t in arrListOfIds)
                {
                    dsPRO_WorkOrderDetail.CloseWorkOrderLines(WOLineStatus.MfgClose, pdtmCloseDate, t.ToString());
                }
            }
		}
		
		public DataSet SearchReleasedWO(int pintCCNID, int pintMasterLocationID, DateTime pdtmFromDueDate, DateTime pdtmToDueDate)
		{
            dsPRO_WorkOrderDetail = new PRO_WorkOrderDetailDS();
            return dsPRO_WorkOrderDetail.SearchWOForClose(WOLineStatus.Released, pintCCNID, pintMasterLocationID, pdtmFromDueDate, pdtmToDueDate);
		}
	
		#endregion
		private DS.PRO_WorkOrderDetailDS dsPRO_WorkOrderDetail;
	}
}

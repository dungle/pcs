using System;
using System.Data;
using PCSComUtils.Common;
using PCSComProduction.WorkOrder.DS;
namespace PCSComProduction.WorkOrder.BO
{
	public class ReleaseWorkOrderBO
	{
		/// <summary>
		/// Search unrelease WorkOrders
		/// </summary>
		/// <author>Trada</author>
		/// <date>Friday, June 3 2005</date>
		/// <param name="pintCCNID"></param>
		/// <param name="pintMasLocID"></param>
		/// <param name="pstrWONo"></param>
		/// <param name="pdtmFromStartDate"></param>
		/// <param name="pdtmToStartDate"></param>
	
		public DataSet SearchUnReleaseWO(int pintCCNID, int pintMasLocID, string pstrWONo, int pintProLineID, DateTime pdtmFromStartDate, DateTime pdtmToStartDate, int pintStatus)
		{
			PRO_WorkOrderDetailDS dsWODetail = new PRO_WorkOrderDetailDS();
				return dsWODetail.SearchUnReleaseWO(pintCCNID , pintMasLocID , pstrWONo , pintProLineID, pdtmFromStartDate , pdtmToStartDate, pintStatus);	
		}
		/// <summary>
		/// UnReleaseWO
		/// </summary>
		/// <param name="pdtbData"></param>
		/// <author>Trada</author>
		/// <date>Wednesday, May 24 2006</date>
	
		public void UnReleaseWO(DataSet pdtbData)
		{
			const string SELECT = "Selected";
			const string TRUE = "True";
			PRO_WorkOrderDetailDS dsWODetail = new PRO_WorkOrderDetailDS();
			foreach(DataRow drow in pdtbData.Tables[0].Rows)
			{
				if (drow[SELECT].ToString() == TRUE)
				{
					drow[PRO_WorkOrderDetailTable.STATUS_FLD] = (int)WOLineStatus.Unreleased;
				}
			}
			dsWODetail.ReleaseWO(pdtbData);
		}
		/// <summary>
		/// <author>Trada</author>
		/// <date>Monday, June 6 2005</date>
		/// </summary>
		/// <param name="pdtbData"></param>
	
		public void ReleaseWO(DataSet pdtbData)
		{
			 const string SELECT = "Selected";
			 const string TRUE = "True";
             PRO_WorkOrderDetailDS dsWODetail = new PRO_WorkOrderDetailDS();
             foreach (DataRow drow in pdtbData.Tables[0].Rows)
             {
                 if (drow[SELECT].ToString() == TRUE)
                 {
                     //Release Work Order
                     drow[PRO_WorkOrderDetailTable.STATUS_FLD] = (int)WOLineStatus.Released;
                 }
             }
             dsWODetail.ReleaseWO(pdtbData);
		}
		
	}
}

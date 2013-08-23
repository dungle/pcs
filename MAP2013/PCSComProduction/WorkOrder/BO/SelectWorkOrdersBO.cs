using System;
using System.Collections;
using System.Data;
using System.Text;
using PCSComUtils.Common;
using PCSComUtils.Common.DS;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComProduction.WorkOrder.BO
{
	public class SelectWorkOrdersBO
	{
		#region ISelectWorkOrdersBO Members

		public DataTable SearchWorkOrderToIssueMaterial(Hashtable phashCondition,string pstrRecordCondition)
		{
			PRO_WorkOrderDetailDS objPRO_WorkOrderDetailDS = new PRO_WorkOrderDetailDS();
			return objPRO_WorkOrderDetailDS.SearchWorkOrderToIssueMaterial(phashCondition,pstrRecordCondition);
		}

		public DataSet SearchWorkOrderToIssueMaterial(string pstrRecordPermission, int pintMasterLocationID, int pintLocationID,
			int pintWorkOrderMasterID, DateTime pdtmFromStartDate, DateTime pdtmToStartDate)
		{
		    PRO_WorkOrderDetailDS objPRO_WorkOrderDetailDS = new PRO_WorkOrderDetailDS();
			return objPRO_WorkOrderDetailDS.SearchWorkOrderToIssueMaterial(pstrRecordPermission, pintMasterLocationID, pintLocationID,
				pintWorkOrderMasterID, pdtmFromStartDate, pdtmToStartDate);
		}

		/// <summary>
		/// Get selected records from temp table
		/// </summary>
		/// <param name="pstrTableName">Temp table name</param>
		/// <returns></returns>
		public DataSet GetSelectedRecords(string pstrTableName, DataSet pdstResultData)
		{
			UtilsDS dsUtils = new UtilsDS();
			return dsUtils.GetSelectedRecords(pstrTableName, pdstResultData);
		}
		#endregion
	}
}

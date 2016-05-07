using System;
using System.Collections;
using System.Data;
using PCSComMaterials.Plan.DS;
using PCSComProduction.DCP.DS;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;

namespace PCSComProduction.WorkOrder.BO
{
	public class WorkOrderBO
	{
		public int AddAndReturnID(object pobjObjectMaster, DataSet pdstData)
		{
			//add master and get id
			PRO_WorkOrderMasterVO voWOMaster = (PRO_WorkOrderMasterVO) pobjObjectMaster;
			dsWorkOrderMaster = new  PRO_WorkOrderMasterDS();
			voWOMaster.WorkOrderMasterID = dsWorkOrderMaster.AddAndReturnID(pobjObjectMaster);
			foreach (DataRow drow in pdstData.Tables[0].Rows)
			{
				// 19-04-2006 dungla: fix bug for NgaHT: refine start date-time and due date-time
				DateTime dtmStartTime = (DateTime)drow[PRO_WorkOrderDetailTable.STARTDATE_FLD];
				dtmStartTime = new DateTime(dtmStartTime.Year, dtmStartTime.Month, dtmStartTime.Day, dtmStartTime.Hour, dtmStartTime.Minute, 0);
				DateTime dtmToTime = (DateTime)drow[PRO_WorkOrderDetailTable.DUEDATE_FLD];
				dtmToTime = new DateTime(dtmToTime.Year, dtmToTime.Month, dtmToTime.Day, dtmToTime.Hour, dtmToTime.Minute, 0);
				// update new date time
				drow[PRO_WorkOrderDetailTable.STARTDATE_FLD] = dtmStartTime;
				drow[PRO_WorkOrderDetailTable.DUEDATE_FLD] = dtmToTime;
				// 19-04-2006 dungla: fix bug for NgaHT: refine start date-time and due date-time
				drow[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD] = voWOMaster.WorkOrderMasterID;
			}
				
			//update detail
			dsWorkOrderDetail = new PRO_WorkOrderDetailDS();
			dsWorkOrderDetail.UpdateDataSet(pdstData);

			//return ID
			return voWOMaster.WorkOrderMasterID;
		}
		public int AddNewWO(object pobjObjectDetail, DataSet pdstData, DataSet pdstCPOs)
		{
            int intWOMasterID = 0;
            //add Master
            intWOMasterID = AddAndReturnID(pobjObjectDetail, pdstData);

            //assign GenerateID
            return intWOMasterID;
        }
		public int AddNewWOImmediately(object pobjObjectDetail, DataSet pdstData, ArrayList parlCPOIDs)
		{
            int intWOMasterID = 0;
            //add Master
            intWOMasterID = AddAndReturnID(pobjObjectDetail, pdstData);

            //assign GenerateID
            new MTR_CPODS().SetWOMasterID(parlCPOIDs, intWOMasterID);
            return intWOMasterID;
        }
		public int AddNewWOImmediately(object pobjObjectDetail, DataSet pdstData, ArrayList parlCPOIDs, string pType)
		{
            int intWOMasterID = 0;
            //add Master
            intWOMasterID = AddAndReturnID(pobjObjectDetail, pdstData);

            //assign GenerateID
            if (pType == PlanTypeEnum.MPS.ToString())
                new MTR_CPODS().SetWOMasterID(parlCPOIDs, intWOMasterID);
            else
                new MTR_CPODS().SetWOMasterIDForDCP(parlCPOIDs, intWOMasterID);
            return intWOMasterID;
        }
		public object GetObjectWOMasterVO(int pintWOMasterID)
		{
            dsWorkOrderMaster = new PRO_WorkOrderMasterDS();
            return dsWorkOrderMaster.GetObjectVO(pintWOMasterID);
        }
		public DataSet GetWODetailByMaster(int pintWOMasterID)
		{
            return new PRO_WorkOrderDetailDS().GetWODetailByMaster(pintWOMasterID);
        }
		public void UpdateWOAndWOLines(object pObjectDetail, DataSet pdstData, ArrayList parlWOLineDeleted)
		{
			//update Master
			dsWorkOrderMaster = new PRO_WorkOrderMasterDS();
			dsWorkOrderDetail = new PRO_WorkOrderDetailDS();
			dsWorkOrderMaster.Update(pObjectDetail);
			PRO_WorkOrderMasterVO voWOMaster = (PRO_WorkOrderMasterVO) pObjectDetail;

			//update Detail
			foreach (DataRow drow in pdstData.Tables[0].Rows)
			{
				//set WOMasterID for all added row
				if (drow.RowState == DataRowState.Added)
				{
					drow[PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD] = voWOMaster.WorkOrderMasterID;
				}
			}
				
			DataSet dstTemp = new DataSet();
			dstTemp.Tables.Add(pdstData.Tables[0].Copy());
			dstTemp.Tables[0].Clear();
			foreach (DataRow drow in pdstData.Tables[0].Rows)
			{
				if (drow.RowState != DataRowState.Deleted)
				{
					dstTemp.Tables[0].ImportRow(drow);
				}
			}
			dsWorkOrderDetail.UpdateDataSet(pdstData);
				
				
			//create dataset for all WO which was added
			DataSet dsNewDataSetForBOM = GetWODetailByMaster( ((PRO_WorkOrderMasterVO) pObjectDetail).WorkOrderMasterID);
			DataSet dsNewDataSetForNewWOLine = dsNewDataSetForBOM.Copy();
			dsNewDataSetForNewWOLine.Tables[0].Rows.Clear();
			for(int i =0; i < dstTemp.Tables[0].Rows.Count; i++)
			{
				if (dstTemp.Tables[0].Rows[i][PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString() == string.Empty)
				{
					dstTemp.Tables[0].Rows[i][PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD] = dsNewDataSetForBOM.Tables[0].Rows[i][PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
					dsNewDataSetForNewWOLine.Tables[0].ImportRow(dstTemp.Tables[0].Rows[i]);
				}
			}
		}
		public void DeleteWOAndWOLines(object pObjectVO, DataSet pdstData,ArrayList parlWOLineDeleted)
		{
            PRO_WorkOrderMasterVO voWOMaster = (PRO_WorkOrderMasterVO)pObjectVO;

            dsWorkOrderDetail = new PRO_WorkOrderDetailDS();
            //delete detail
            dsWorkOrderDetail.DeleteByWOMasterID(voWOMaster.WorkOrderMasterID);
        }
		
		/// <summary>
		/// Get UMCode, CostMethod, AGCCode, EstCode
		/// </summary>
		/// <author>
		/// Do Manh Tuan
		/// 09-06-2005
		/// </author>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
	
		public string GetProductInforByID(int pintProductID)
		{
            return new PRO_WorkOrderDetailDS().GetProductInforByID(pintProductID);
        }
		/// <summary>
		/// Get Production Line by ID
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>wednesday, January 4 2006</date>
	
		public string GetProductionLineByID(int pintProductionLineID)
		{
			string strProductionLine = string.Empty;
			PRO_ProductionLineDS dsPRO_ProductionLine = new PRO_ProductionLineDS();
			PRO_ProductionLineVO voPRO_ProductionLine = (PRO_ProductionLineVO)dsPRO_ProductionLine.GetObjectVO(pintProductionLineID);
			if (voPRO_ProductionLine.Code != string.Empty)
			{
				strProductionLine = voPRO_ProductionLine.Code;
			}
			return strProductionLine;
		}

		public void UpdateExistedWOImmediately(DataSet pdstData, object pobjObjectDetail, ArrayList parlWOLineDeleted, ArrayList parlCPOIDs, string pType)    
		{
            //update WO
            UpdateWOAndWOLines(pobjObjectDetail, pdstData, parlWOLineDeleted);

            //assign GenerateID
            if (pType == PlanTypeEnum.MPS.ToString())
                new MTR_CPODS().SetWOMasterID(parlCPOIDs, ((PRO_WorkOrderMasterVO)pobjObjectDetail).WorkOrderMasterID);
            else
                new MTR_CPODS().SetWOMasterIDForDCP(parlCPOIDs, ((PRO_WorkOrderMasterVO)pobjObjectDetail).WorkOrderMasterID);
        }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pintMasterLocID"></param>
		/// <returns></returns>
	
		public MST_MasterLocationVO GetMasterLocByID(int pintMasterLocID)
		{
            return (MST_MasterLocationVO)new MST_MasterLocationDS().GetObjectVO(pintMasterLocID);
        }

		/// <summary>
		/// GetWorkOrderNo
		/// </summary>
		/// <param name="pstrQuery"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Mar 24 2006</date>
	
		public DataSet GetWorkOrderNo(string pstrQuery)
		{
			DataSet dstReturn = new DataSet();
			PRO_WorkOrderMasterDS dsWorkOrderMaster = new PRO_WorkOrderMasterDS();
			dstReturn = dsWorkOrderMaster.GetWorkOrderNo(pstrQuery);
			return dstReturn;
		}
		private DS.PRO_WorkOrderDetailDS dsWorkOrderDetail;
		private DS.PRO_WorkOrderMasterDS dsWorkOrderMaster;
	}
}

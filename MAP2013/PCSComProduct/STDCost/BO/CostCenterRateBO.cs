using System;
using System.Data;
using PCSComProduct.STDCost.DS;
using PCSComUtils.Common;

namespace PCSComProduct.STDCost.BO
{
	public class CostCenterRateBO
	{
		public int AddAndReturnID(object pobjMaster, DataSet pdstDetail)
		{
			STD_CostCenterRateMasterDS dsMaster = new STD_CostCenterRateMasterDS();
			// save master object first and get new id
			int intNewID = dsMaster.AddAndReturnID(pobjMaster);
			// now assign new id to detail data
			foreach (DataRow drowData in pdstDetail.Tables[0].Rows)
			{
				if (drowData.RowState != DataRowState.Deleted)
					drowData[STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD] = intNewID;
			}
			// update detail data
			STD_CostCenterRateDetailDS dsDetail = new STD_CostCenterRateDetailDS();
			dsDetail.UpdateDataSet(pdstDetail);

			// return master id to client
			return intNewID;
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			STD_CostCenterRateMasterVO voMaster = (STD_CostCenterRateMasterVO)pObjectVO;
			// delete detail first
			STD_CostCenterRateDetailDS dsDetail = new STD_CostCenterRateDetailDS();
			dsDetail.Delete(voMaster.CostCenterRateMasterID);
			// delete master
			STD_CostCenterRateMasterDS dsMaster = new STD_CostCenterRateMasterDS();
			dsMaster.Delete(voMaster.CostCenterRateMasterID);
		}

		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			STD_CostCenterRateMasterDS dsMaster = new STD_CostCenterRateMasterDS();
			return dsMaster.GetObjectVO(pintID);
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Update Master object and update detail dataset
		/// </summary>
	
		public void Update(object pobjMaster, DataSet pdstDetails)
		{
			STD_CostCenterRateMasterDS dsMaster = new STD_CostCenterRateMasterDS();
			// update master
			dsMaster.Update(pobjMaster);
			
			// update detail
			STD_CostCenterRateDetailDS dsDetail = new STD_CostCenterRateDetailDS();
			dsDetail.UpdateDataSet(pdstDetails);
		}
		/// <summary>
		/// List all detail cost for master cost center rate
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
	
		public DataSet GetDetailCost(int pintMasterID, EnumAction pMode)
		{
			STD_CostCenterRateDetailDS dsDetail = new STD_CostCenterRateDetailDS();
			if (pintMasterID > 0)
			{
				DataSet dstData = dsDetail.List(pintMasterID);
				DataTable dtbMissElement = dsDetail.GetMissElement(pintMasterID);
				DataTable dtbCopy = dstData.Tables[0].Copy();
				if (pMode == EnumAction.Edit)
				{
					if (dtbMissElement.Rows.Count > 0)
					{
						// clear original data first
						dstData.Tables[0].Clear();
						// now make new data with miss element is Added row
						for (int i = 0; i < dtbCopy.Rows.Count; i++)
						{
							// source row
							DataRow drowData = dtbCopy.Rows[i];
							// new row
							DataRow drowNew = dstData.Tables[0].NewRow();
							// copy value
							foreach (DataColumn dcolName in dtbCopy.Columns)
							{
								drowNew[dcolName.ColumnName] = drowData[dcolName.ColumnName];
							}
							if (drowData[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].Equals(DBNull.Value))
							{
								drowNew[STD_CostCenterRateDetailTable.COSTCENTERRATEMASTERID_FLD] = pintMasterID;
								drowNew[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD] = pintMasterID;
								dstData.Tables[0].Rows.Add(drowNew);
							}
                            else
							{
								dstData.Tables[0].Rows.Add(drowNew);
								dstData.Tables[0].Rows[i].AcceptChanges();
							}
						}
					}
				}
				else
				{
					if (dtbMissElement.Rows.Count > 0)
					{
						// remove miss element from detail
						foreach (DataRow drowData in dstData.Tables[0].Rows)
						{
							if (drowData[STD_CostCenterRateDetailTable.COSTCENTERRATEDETAILID_FLD].Equals(DBNull.Value))
								drowData.Delete();
						}
						dstData.AcceptChanges();
					}
				}
				return dstData;
			}
			else
				return dsDetail.List();
		}
	}
}

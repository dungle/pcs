using System;
using System.Data;


using PCSComUtils.Common;
using PCSComMaterials.ActualCost.DS;


namespace PCSComMaterials.ActualCost.BO
{
	public interface IFreightBO
	{
		int AddAndReturnID(cst_FreightMasterVO pobjMasterVO, DataSet pdstDetail);
		DataSet GetPOReceiveDetail(int pintPOReceiveMasterID);
		DataSet GetFreightDetailByMasterID(int pintFreightMasterID);
		void UpdateMasterAndDetail(cst_FreightMasterVO pobjMasterVO, DataSet pdstDetail);
		void DeleteMasterAndDetail(int pintMasterID, DataSet pdstDetail);
		DataSet GetReturnToVendorDetail(int pintReturnToVendorMasterID);
	}
	/// <summary>
	/// Summary description for ActualCostRollUpBO.
	/// </summary>
	
	public class FreightBO : IFreightBO
	{
		public FreightBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
		public void Add(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
		public void Delete(object pObjectVO)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get the object information by ID of VO class
		/// </summary>
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// GetObjectVO
		/// </summary>
		/// <param name="pintFreightMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
	
		public DataTable GetFreightMaster(int pintFreightMasterID)
		{
			DataTable dtbReturn = new DataTable();
			cst_FreightMasterDS dsFreightMaster = new cst_FreightMasterDS();
			dtbReturn = dsFreightMaster.ListMasterByID(pintFreightMasterID);
			return dtbReturn;
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
		/// GetPOReceiveDetail
		/// </summary>
		/// <param name="pintPOReceiveMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 24 2006</date>
	
		public DataSet GetPOReceiveDetail(int pintPOReceiveMasterID)
		{	
			DataSet dstPOReceiveDetail = new DataSet();
			cst_FreightMasterDS dsFreightMaster = new cst_FreightMasterDS();
			dstPOReceiveDetail = dsFreightMaster.GetPOReceive(pintPOReceiveMasterID);
			foreach (DataRow drow in dstPOReceiveDetail.Tables[0].Rows)
			{
				drow[IV_AdjustmentTable.ADJUSTMENTID_FLD] = DBNull.Value;
			}
			return dstPOReceiveDetail;
		}
		/// <summary>
		/// GetReturnToVendorDetail
		/// </summary>
		/// <param name="pintReturnToVendorMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, July 6 2006</date>
	
		public DataSet GetReturnToVendorDetail(int pintReturnToVendorMasterID)
		{	
			DataSet dstReturnToVendor = new DataSet();
			cst_FreightDetailDS dsFreightDetail = new cst_FreightDetailDS();
			dstReturnToVendor = dsFreightDetail.GetReturnToVendorByMasterID(pintReturnToVendorMasterID);
			foreach (DataRow drow in dstReturnToVendor.Tables[0].Rows)
			{
				drow[IV_AdjustmentTable.ADJUSTMENTID_FLD] = DBNull.Value;
			}
			return dstReturnToVendor;
		}
		/// <summary>
		/// GetInvoice_PONumber
		/// </summary>
		/// <param name="pintReturnToVendorMasterID"></param>
		/// <returns></returns>
		/// <author>TRada</author>
		/// <date>Thursday, July 6 2006</date>
	
		public DataSet GetInvoice_PONumber(int pintReturnToVendorMasterID)
		{
			cst_FreightMasterDS dsFreightMaster = new cst_FreightMasterDS();
			return dsFreightMaster.GetInvoice_PONumber(pintReturnToVendorMasterID);
		}
		/// <summary>
		/// GetFreightDetailByMasterID
		/// </summary>
		/// <param name="pintFreightMasterID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
	
		public DataSet GetFreightDetailByMasterID(int pintFreightMasterID)
		{
			DataSet dstFreightDetail = new DataSet();
			cst_FreightDetailDS dscst_FreightDetail = new cst_FreightDetailDS();
			dstFreightDetail = dscst_FreightDetail.GetFreightDetailByMasterID(pintFreightMasterID);
			return dstFreightDetail;
		}
		/// <summary>
		/// AddAndReturnID
		/// </summary>
		/// <param name="pobjMasterVO"></param>
		/// <param name="pdstDetail"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, Feb 27 2006</date>
	
		public int AddAndReturnID(cst_FreightMasterVO pobjMasterVO, DataSet pdstDetail)
		{	
			int intMasterID = 0;
			cst_FreightMasterDS dscst_FreightMaster = new cst_FreightMasterDS();
			intMasterID = dscst_FreightMaster.AddAndReturnID(pobjMasterVO);
			//save detail
			foreach (DataRow drow in pdstDetail.Tables[0].Rows)
			{
				if (drow.RowState == DataRowState.Deleted) continue;
				drow[cst_FreightDetailTable.FREIGHTMASTERID_FLD] = intMasterID;
			}
			cst_FreightDetailDS dscst_FreightDetail = new cst_FreightDetailDS();
			//Update dataset
			dscst_FreightDetail.UpdateDataSet(pdstDetail);
			return intMasterID;
		}
		/// <summary>
		/// UpdateMasterAndDetail
		/// </summary>
		/// <param name="pobjMasterVO"></param>
		/// <param name="pdstDetail"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
	
		public void UpdateMasterAndDetail(cst_FreightMasterVO pobjMasterVO, DataSet pdstDetail)
		{
			cst_FreightMasterDS dsFreightMaster = new cst_FreightMasterDS();
			cst_FreightDetailDS dsFreightDetail = new cst_FreightDetailDS();
			if (pobjMasterVO.FreightMasterID > 0)
			{
				foreach (DataRow drow in pdstDetail.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Deleted) continue;
					if (int.Parse(drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD].ToString()) == 0)
					{
						drow[cst_FreightMasterTable.FREIGHTMASTERID_FLD] = pobjMasterVO.FreightMasterID;
					}
				}
			}
			//Update master
			dsFreightMaster.Update(pobjMasterVO);
			//Update Detail
			dsFreightDetail.UpdateDataSet(pdstDetail);
		}
		/// <summary>
		/// DeleteMasterAndDetail
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <author>Trada</author>
		/// <date>Tuesday, Feb 28 2006</date>
	
		public void DeleteMasterAndDetail(int pintMasterID, DataSet pdstDetail)
		{
			foreach (DataRow drow in pdstDetail.Tables[0].Rows)
			{
				if (drow.RowState != DataRowState.Deleted)
				{
					drow.Delete();
				}
			}
			cst_FreightDetailDS dsFreightDetail = new cst_FreightDetailDS();
			dsFreightDetail.UpdateDataSet(pdstDetail);
			cst_FreightMasterDS dsFreightMaster = new cst_FreightMasterDS();
			dsFreightMaster.Delete(pintMasterID);
			
		}
	}
}

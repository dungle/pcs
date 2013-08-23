using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComProduction.DCP.DS;


namespace PCSComProduction.DCP.BO
{
	public interface IDCPAdjustmentBO
	{
		DataTable GetShiftByWCCapacity(int pintWCCapacityID);
	}
	/// <summary>
	/// Summary description for DCPAdjustmentBO.
	/// </summary>
	
	
	public class DCPAdjustmentBO : IDCPAdjustmentBO
	{
		public DCPAdjustmentBO()
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
		/// Get shift by WCCapacity
		/// </summary>
		/// <param name="pintWCCapacityID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, January 12 2006</date>
	
		public DataTable GetShiftByWCCapacity(int pintWCCapacityID)
		{
			DataTable dtbReturn = new DataTable();
			PRO_ShiftCapacityDS dsPRO_ShiftCapacity = new PRO_ShiftCapacityDS();
			dtbReturn = dsPRO_ShiftCapacity.GetShiftByWCCapacityID(pintWCCapacityID);	
			return dtbReturn;
		}

	}
}

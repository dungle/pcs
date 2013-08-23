using System;
using System.Data;

using PCSComProduction.DCP.DS;

using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.BO
{
	public interface ICheckPointBO
	{
		DataSet ListByCCNID(int pintCCNID);
		DataSet ListByWorkCenterID(int pintCCNID, int pintWorkCenterID);
	}
	/// <summary>
	/// Summary description for CheckPointBO.
	/// </summary>
	public class CheckPointBO : ICheckPointBO
	{
		public CheckPointBO()
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
	
		public void UpdateDataSet(DataSet pdstData)
		{
			try
			{
				new PRO_CheckPointDS().UpdateDataSet(pdstData);
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
		/// Update into Database
		/// </summary>
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Get all CheckPoint by CCNID
		/// </summary>
		/// <param name="pintCCNID"></param>
		/// <returns></returns>
	
		public DataSet ListByCCNID(int pintCCNID)
		{
			try
			{
				return new PRO_CheckPointDS().ListByCCNID(pintCCNID);
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
		/// Get all CheckPoint by CCNID, WorkCenterID
		/// </summary>
		/// <param name="pintWorkCenterID"></param>
		/// <returns></returns>
	
		public DataSet ListByWorkCenterID(int pintCCNID, int pintWorkCenterID)
		{
			try
			{
				return new PRO_CheckPointDS().ListByWorkCenterID(pintCCNID, pintWorkCenterID);
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
	}
}

using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComProduction.DCP.DS;


namespace PCSComProduction.DCP.BO
{
	public interface IShiftPatternBO
	{
		object GetShiftParttern(int pintShiftID, int pintCCNID);	
		int SaveShiftParttern(object pobjShift);
		void Update(object pObjectDetail);
	}
	/// <summary>
	/// Summary description for ShiftPatternBO.
	/// </summary>
	
	
	public class ShiftPatternBO : IShiftPatternBO
	{
		public ShiftPatternBO()
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
		/// Delete
		/// </summary>
		/// <param name="pintID"></param>
		/// <author>Trada</author></author>
		/// <date>Monday, August 15 2005</date>
	
		public void Delete(int pintID)
		{
			try
			{
				PRO_ShiftPatternDS dsPRO_ShiftPattern = new PRO_ShiftPatternDS();
				dsPRO_ShiftPattern.Delete(pintID);
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
		/// Get the object information by ID of VO class
		/// </summary>
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// GetShiftParttern
		/// </summary>
		/// <param name="pintShiftID"></param>
		/// <param name="pintCCNID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
	
		public object GetShiftParttern(int pintShiftID, int pintCCNID)
		{
			try
			{
				PRO_ShiftPatternDS dsPRO_ShiftPattern = new PRO_ShiftPatternDS();
				return dsPRO_ShiftPattern.GetShiftPartternByShiftCode(pintShiftID, pintCCNID);
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
		/// SaveShiftParttern
		/// </summary>
		/// <param name="pobjShift"></param>
		/// <author>Trada</author>
		/// <date>Friday, August 12 2005</date>
	
		public int SaveShiftParttern(object pobjShift)
		{
			try
			{
				PRO_ShiftPatternDS dsPRO_ShiftPattern = new PRO_ShiftPatternDS();
				PRO_ShiftPatternVO voPRO_ShiftPattern = (PRO_ShiftPatternVO) pobjShift;
				//Add
				return dsPRO_ShiftPattern.AddAndReturnID(voPRO_ShiftPattern);
				
				
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
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
		public void UpdateDataSet(DataSet dstData)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update into Database
		/// </summary>
		/// <author>Trada</author>
		/// <date>Monday, August 15 2005</date>
	
		public void Update(object pObjectDetail)
		{
			try
			{
				PRO_ShiftPatternDS dsPRO_ShiftPattern = new PRO_ShiftPatternDS();
				PRO_ShiftPatternVO voPRO_ShiftPattern = (PRO_ShiftPatternVO) pObjectDetail;
				//Update
				dsPRO_ShiftPattern.Update(voPRO_ShiftPattern);
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
	}
}

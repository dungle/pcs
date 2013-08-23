using System;
using System.Data;
using PCSComUtils.Common;


using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComMaterials.Plan.BO
{
	public interface IWorkDayCalendarBO
	{
		int AddWDCalendar(object pobjMaster, DataSet pdstData);
		object GetWDCalendarMaster(int pintYear, int pintCCNID);
		DataSet GetDetailByMasterID(int pintMasterID);
		void UpdateWDCalendar(object pobjMaster, DataSet pdstData);
	}
	/// <summary>
	/// Summary description for WorkDayCalendarBO.
	/// </summary>
	
	public class WorkDayCalendarBO : IWorkDayCalendarBO
	{
		public WorkDayCalendarBO()
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
		/// Add Master & Detail
		/// </summary>
		/// <param name="pobjMaster"></param>
		/// <param name="pdstData"></param>
	
		public int AddWDCalendar(object pobjMaster, DataSet pdstData)
		{
			try
			{
				//add Master
				int intMasterID = new MST_WorkingDayMasterDS().AddAndReturnID(pobjMaster);

				//add Detail
				foreach (DataRow drow in pdstData.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Added)
					{
						drow[MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD] = intMasterID;
					}
				}
				new MST_WorkingDayDetailDS().UpdateDataSet(pdstData);

				return intMasterID;
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
		/// Update Master & Detail
		/// </summary>
		/// <param name="pobjMaster"></param>
		/// <param name="pdstData"></param>
	
		public void UpdateWDCalendar(object pobjMaster, DataSet pdstData)
		{
			try
			{
				//add Master
				new MST_WorkingDayMasterDS().Update(pobjMaster);

				//add Detail
				foreach (DataRow drow in pdstData.Tables[0].Rows)
				{
					if (drow.RowState == DataRowState.Added)
					{
						drow[MST_WorkingDayDetailTable.WORKINGDAYMASTERID_FLD] = ((MST_WorkingDayMasterVO) pobjMaster).WorkingDayMasterID;
					}
				}
				new MST_WorkingDayDetailDS().UpdateDataSet(pdstData);
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
		/// Delete record by condition
		/// </summary>
	
		public void Delete(object pObjectVO)
		{
			try
			{
				//delete Detail
				DataSet dstDataDetail = GetDetailByMasterID(((MST_WorkingDayMasterVO) pObjectVO).WorkingDayMasterID);
				foreach (DataRow drow in dstDataDetail.Tables[0].Rows)
				{
					drow.Delete();
				}
				new MST_WorkingDayDetailDS().UpdateDataSet(dstDataDetail);

				//delete Master
				new MST_WorkingDayMasterDS().Delete(((MST_WorkingDayMasterVO) pObjectVO).WorkingDayMasterID);
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
		/// Get WorkingDayDetail by MasterID
		/// </summary>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
	
		public DataSet GetDetailByMasterID(int pintMasterID)
		{
			const string DAYOFWEEK = "Day";
			try
			{
				DataSet dstReturn = new MST_WorkingDayDetailDS().GetDetailByMasterID(pintMasterID);
				foreach (DataRow drow in dstReturn.Tables[0].Rows)
				{
					drow[DAYOFWEEK] = DateTime.Parse(drow[MST_WorkingDayDetailTable.OFFDAY_FLD].ToString().Trim()).DayOfWeek;
				}
				dstReturn.AcceptChanges();
				return dstReturn;
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
		/// Get the object information by ID of VO class
		/// </summary>
		public object GetObjectVO(int pintID, string VOclass)
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Get WorkingDayMaster by Year and CCN
		/// </summary>
		/// <param name="pintYear"></param>
		/// <param name="pintCCNID"></param>
		/// <returns></returns>
	
		public object GetWDCalendarMaster(int pintYear, int pintCCNID)
		{
			try
			{
				return new MST_WorkingDayMasterDS().GetWDCalendarMaster(pintYear, pintCCNID);	
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
	}
}

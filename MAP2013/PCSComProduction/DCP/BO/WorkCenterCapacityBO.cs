using System;
using System.Data;



//

using PCSComUtils.Common;
using PCSComUtils.MasterSetup.DS;
using PCSComProduction.DCP.DS;
using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.BO
{
	public interface IWorkCenterCapacityBO
	{
		void UpdateShiftCapacityDataSet(DataSet dstData);
		void DeleteByWorkCenter(int pintCCNID, int pintWorkCenterId);
		int AddAndReturnId(object pobjDetail);
		DataSet GetShiftCapacityByWorkCenter(int pintCCNID, int pintWorkCenterId);
		DataSet GetWCCapacityByWorkCenter(int pintCCNID, int pintWorkCenterId);
		DataTable GetShiftWithShiftPattern();
		DataSet GetShiftAndShiftPattern();
		object GetWorkCenterVO(int pintWorkCenterID);
		DataSet GetWorkingDayCalendar();
		bool CheckIfWorkingday(DateTime pdtmDate, int pintYear);
		bool CheckIfDateIsOutOfPeriod(DateTime pdtmDateToCheck, int pintYear);
	}

	/// <summary>
	/// Summary description for WorkCenterCapacityBO.
	/// </summary>
	
	
	public class WorkCenterCapacityBO : IWorkCenterCapacityBO
	{
		public WorkCenterCapacityBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		public object GetWorkCenterVO(int pintWorkCenterID)
		{
			MST_WorkCenterDS dsWorkCenter = new MST_WorkCenterDS();

			return dsWorkCenter.GetObjectVO(pintWorkCenterID);
		}

		/// <summary>
		/// Insert a new record into database
		/// </summary>
	
		public void Add(object pobjDetail)
		{
			try
			{
				PRO_WCCapacityDS dsWCCapacity = new PRO_WCCapacityDS();
				dsWCCapacity.Add(pobjDetail);
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
		/// Insert a new record into database
		/// </summary>
	
		public int AddAndReturnId(object pobjDetail)
		{
			try
			{
				return (new PRO_WCCapacityDS()).AddAndReturnId(pobjDetail);
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
		/// Get total working hours of collection of shifts
		/// </summary>
		/// <param name="pstrShiftIDs"></param>
		/// <returns></returns>
	
		public int GetTotalWorkingTime(string pstrShiftIDs)
		{
			try
			{
				return (new PRO_WCCapacityDS()).GetTotalWorkingTime(pstrShiftIDs);
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
		/// Get actual working time 
		/// </summary>
		/// <param name="pstrShiftIDs"></param>
		/// <returns></returns>
	
		public int GetTotalActualWorkingTime(string pstrShiftIDs)
		{
			try
			{
				return (new PRO_WCCapacityDS()).GetTotalActualWorkingTime(pstrShiftIDs);
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
	
		public void Delete(object pobjDetail)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Delete record by condition
		/// </summary>
	
		public void Delete(int pintWCCapacityID)
		{
			try
			{
				PRO_WCCapacityDS dsWCCapacity = new PRO_WCCapacityDS();
				dsWCCapacity.Delete(pintWCCapacityID);
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
		/// Delete record by work center
		/// </summary>
	
		public void DeleteByWorkCenter(int pintCCNID, int pintWorkCenterId)
		{
			try
			{
				PRO_ShiftCapacityDS dsShiftCapacity = new PRO_ShiftCapacityDS();
				PRO_WCCapacityDS dsWCCapacity = new PRO_WCCapacityDS();
				
				dsShiftCapacity.DeleteByWorkCenter(pintCCNID, pintWorkCenterId);
				dsWCCapacity.DeleteByWorkCenter(pintCCNID, pintWorkCenterId);
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
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			try
			{
				PRO_WCCapacityDS dsWCCapacity = new PRO_WCCapacityDS();
				dsWCCapacity.UpdateDataSet(dstData);
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
	
		public void UpdateShiftCapacityDataSet(DataSet dstData)
		{
			try
			{
				PRO_ShiftCapacityDS dsShiftCapacity = new PRO_ShiftCapacityDS();
				dsShiftCapacity.UpdateDataSet(dstData);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			try
			{
				PRO_WCCapacityDS dsWCCapacity = new PRO_WCCapacityDS();
				dsWCCapacity.Update(pObjectDetail);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		
	
		public DataTable GetShiftWithShiftPattern()
		{
			try
			{
				return (new PRO_WCCapacityDS()).GetShiftWithShiftPattern();
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
		/// GetShiftAndShiftPattern
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, January 9 2006</date>
	
		public DataSet GetShiftAndShiftPattern()
		{
			try
			{
				return (new PRO_WCCapacityDS()).GetShiftAndShiftPattern();
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

	
		public DataSet GetShiftCapacityByWorkCenter(int pintCCNID, int pintWorkCenterId)
		{
			try
			{
				return (new PRO_ShiftCapacityDS()).ListByWorkCenter(pintCCNID, pintWorkCenterId);
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
		/// Check if the user has entered is working day
		/// </summary>
		/// <param name="pdtmDate"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Feb 16 2006</date>
	
		public bool CheckIfWorkingday(DateTime pdtmDate, int pintYear)
		{
			MST_WorkingDayMasterDS dsMST_WorkingDayMaster = new	MST_WorkingDayMasterDS();
			MST_WorkingDayMasterVO voWorkingDayMaster = (MST_WorkingDayMasterVO) dsMST_WorkingDayMaster.GetNoWorkingDay(pintYear);
			if (pdtmDate.DayOfWeek == DayOfWeek.Sunday)
			{
				return voWorkingDayMaster.Sun;
			}
			if (pdtmDate.DayOfWeek == DayOfWeek.Monday)
			{
				return voWorkingDayMaster.Mon;
			}
			if (pdtmDate.DayOfWeek == DayOfWeek.Tuesday)
			{
				return voWorkingDayMaster.Tue;
			}
			if (pdtmDate.DayOfWeek == DayOfWeek.Wednesday)
			{
				return voWorkingDayMaster.Wed;
			}
			if (pdtmDate.DayOfWeek == DayOfWeek.Thursday)
			{
				return voWorkingDayMaster.Thu;
			}
			if (pdtmDate.DayOfWeek == DayOfWeek.Friday)
			{
				return voWorkingDayMaster.Fri;
			}
			if (pdtmDate.DayOfWeek == DayOfWeek.Saturday)
			{
				return voWorkingDayMaster.Sat;
			}
			return false;
		}
	
		public DataSet GetWCCapacityByWorkCenter(int pintCCNID, int pintWorkCenterId)
		{
			try
			{
				return (new PRO_WCCapacityDS()).ListByWorkCenter(pintCCNID, pintWorkCenterId);
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
		/// GetWorkingDayCalendar
		/// </summary>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, Feb 10 2006</date>
	
		public DataSet GetWorkingDayCalendar()
		{
			DataSet dstWorkingDayCalendar = new DataSet();
			MST_WorkingDayDetailDS dsMST_WorkingDayDetail = new	MST_WorkingDayDetailDS();
			dstWorkingDayCalendar = dsMST_WorkingDayDetail.List();
			return dstWorkingDayCalendar;
		}
		/// <summary>
		/// CheckIfDateIsOutOfPeriod
		/// </summary>
		/// <param name="pintYear"></param>
		/// <param name="pdtmDateToCheck"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, Mar 7 2006</date>
	
		public bool CheckIfDateIsOutOfPeriod(DateTime pdtmDateToCheck, int pintYear)
		{
			DataSet dstWorkingDayMaster = new DataSet();
			MST_WorkingDayMasterDS dsWorkingDayMaster = new MST_WorkingDayMasterDS();
			dstWorkingDayMaster = dsWorkingDayMaster.GetCollectionOffDays(pintYear);
			if (dstWorkingDayMaster.Tables[0].Rows.Count == 0)
			{
				//Not config
				return true;
			}
//			else
//			{
//				//Find the last off day
//				DateTime dtmTheLastOffDay = new DateTime();
//				
//				if ((bool)dstWorkingDayMaster.Tables[0].Rows[0][MST_WorkingDayMasterTable.SUN_FLD] == false)
//				{
//						
//				}
//			}
			return false;
		}
	}
}
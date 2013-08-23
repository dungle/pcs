using System;
using System.Data;
using System.Collections;


using PCSComUtils.Common;
using PCSComMaterials.Plan.DS;
//using PCSComUtils.PCSExc;

namespace PCSComMaterials.Plan.BO
{
	public interface ICPODataViewerBO
	{
		DataSet Search(Hashtable pobjCriteria);
		DataSet SearchForDCP(Hashtable pobjCriteria);
		//void UpdateDataSet(DataSet dstData);
		DataTable GetVendorDeliveryPolicyByParty(int pintPartyID);
		DataSet GetWorkDayCalendar();
		void UpdateDataSetForDCP(DataSet dstData);	
	}
	/// <summary>
	/// Summary description for CPODataViewerBO.
	/// </summary>
	
	public class CPODataViewerBO : ICPODataViewerBO
	{
		public CPODataViewerBO()
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
		public void ConvertToNewWO(object pobjCPO)
		{
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
		public void PrepareDataForPO()
		{
		}
		
	
		public DataSet Search(Hashtable phtbCriteria)
		{
			return (new MTR_CPODS()).Search(phtbCriteria);
		}

	
		public DataSet SearchForDCP(Hashtable phtbCriteria)
		{
			return (new MTR_CPODS()).SearchForDCP(phtbCriteria);
		}

		/// <summary>
		/// Return the DataSet (list of record) by inputing the FieldList and Condition
		/// </summary>
	
		public void UpdateDataSet(DataSet dstData)
		{
			MTR_CPODS dsCPO = new MTR_CPODS();
			dsCPO.UpdateDataSet(dstData);
		}
		/// <summary>
		/// Delete MRP result by list of CPO
		/// </summary>
	
		public void DeleteMRP(string pstrCPOIDs)
		{
			MTR_CPODS dsCPO = new MTR_CPODS();
			dsCPO.Delete(pstrCPOIDs);
		}
		/// <summary>
		/// UpdateDataSetForDCP
		/// </summary>
		/// <param name="dstData"></param>
		/// <author>Trada</author>
		/// <date>Monday, April 24 2006</date>
	
		public void UpdateDataSetForDCP(DataSet dstData)
		{
			MTR_CPODS dsCPO = new MTR_CPODS();
			dsCPO.UpdateDataSetForDCP(dstData);
		}

		/// <summary>
		/// Update into Database
		/// </summary>
	
		public void Update(object pObjectDetail)
		{
			throw new NotImplementedException();
		}

	
		public DataTable GetVendorDeliveryPolicyByParty(int pintPartyID)
		{
			return new MTR_CPODS().ListVendorDeliveryPolicy(pintPartyID);
		}

	
		public DataSet GetWorkDayCalendar()
		{
			return new MTR_CPODS().GetWorkDayCalendar();
		}

	
		public DateTime GetAsOfDate(int pintCycleID, bool pblnIsMPS)
		{
			MTR_MRPCycleOptionMasterDS dsMaster = new MTR_MRPCycleOptionMasterDS();
			return dsMaster.GetAsOfDate(pintCycleID, pblnIsMPS);
		}
	}
}
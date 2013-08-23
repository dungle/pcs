using System;
using System.Data;


using PCSComProcurement.Purchase.DS;

namespace PCSComProcurement.Purchase.BO
{
	public interface IPODeliverySlipBO
	{
		DataTable ExecuteReport(int pintPurchaseOrderMasterID, DateTime pdtmFromDate, DateTime pdtmToDate);
	}
	/// <summary>
	/// Summary description for PODeliverySlipBO.
	/// </summary>
	
	public class PODeliverySlipBO : IPODeliverySlipBO
	{
		public PODeliverySlipBO()
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
		/// Get all delivery for a PO from FromDate to ToDate
		/// </summary>
		/// <param name="pintPurchaseOrderMasterID">PO Master</param>
		/// <param name="pdtmFromDate">From Date</param>
		/// <param name="pdtmToDate">To Date</param>
		/// <returns>List of Delivery Slip</returns>
	
		public DataTable ExecuteReport(int pintPurchaseOrderMasterID, DateTime pdtmFromDate, DateTime pdtmToDate)
		{
			PODeliverySlipDS dsPOSlip = new PODeliverySlipDS();
			return dsPOSlip.GetReportData(pintPurchaseOrderMasterID, pdtmFromDate , pdtmToDate);
		}
	}
}

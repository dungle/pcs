using System;

using PCSComProcurement.Purchase.DS;

using System.Data;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;

namespace PCSComProcurement.Purchase.BO
{
	public interface IDeliveryApprovalBO
	{}
	/// <summary>
	/// Summary description for DeliveryApprovalBO.
	/// </summary>
	
	public class DeliveryApprovalBO : IDeliveryApprovalBO
	{
		#region IObjectBO Members

	
		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			dsSchedule.UpdateCancelDelivery(dstData);
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add DeliveryApprovalBO.Update implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add DeliveryApprovalBO.Delete implementation
		}

		public void Add(object pObjectDetail)
		{
			// TODO:  Add DeliveryApprovalBO.Add implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add DeliveryApprovalBO.GetObjectVO implementation
			return null;
		}

		#endregion

	
		public DataSet SearchDeliverySchedule(DateTime pdtmFromDate, DateTime pdtmToDate, int pintPOMasterID,
			int pintCategoryID, string pstrProductID)
		{
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			return dsSchedule.SearchDeliverySchedule(pdtmFromDate, pdtmToDate, pintPOMasterID, pintCategoryID, pstrProductID);;
		}
	
		public void UpdateDelivery(string pstrCancelList, string pstrApprovedList)
		{
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			dsSchedule.UpdateDelivery(pstrCancelList, pstrApprovedList);
		}
	}
}

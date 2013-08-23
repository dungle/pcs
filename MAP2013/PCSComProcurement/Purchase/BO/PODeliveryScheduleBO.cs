using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComProcurement.Purchase.DS;


namespace PCSComProcurement.Purchase.BO
{
	public interface IPODeliveryScheduleBO 
	{
		DataSet GetDeliverySchedule(int pintSaleOrderLineID);
		void DeleteDeliveryDetail(int pintSaleOrderLineID);
		void UpdateDeliveryDataSet(DataSet pdstDeliverySchedule, int pintPurchaseOrderDetailID);
		bool GetPurchaseOrderApprovalStatus(int pintPurchaseOrderDetailID);
		decimal GetTotalDeliveryQuantityOfLine(int pintPODetailID);
	}
	/// <summary>
	/// Summary description for .
	/// </summary>
	
	
	public class PODeliveryScheduleBO :IPODeliveryScheduleBO
	{
		private const string THIS = "PCSComProcurement.Purchase.BO.PODeliveryScheduleBO";
		public PODeliveryScheduleBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add(object pObjectDetail)
		{
			// TODO:  

		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  

		}
	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  

		}
	
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  

		}
		//**************************************************************************              
		///    <Description>
		///       Get the list of Delivery Schedule 
		///       Set the the Line field to automatically increased
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       March, 9 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet GetDeliverySchedule(int pintPurchaseOrderLineID)
		{
			PO_DeliveryScheduleDS objPO_DeliveryScheduleDS = new PO_DeliveryScheduleDS();
			DataSet dsData = objPO_DeliveryScheduleDS.GetDeliverySchedule(pintPurchaseOrderLineID);
			return dsData;
		}

		//**************************************************************************              
		///    <Description>
		///       Delete all delivery schedule
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       March, 9 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void DeleteDeliveryDetail(int pintPurchaseOrderLineID)
		{
			PO_DeliveryScheduleDS objPO_DeliveryScheduleDS = new PO_DeliveryScheduleDS();
			objPO_DeliveryScheduleDS.DeleteDeliveryDetail(pintPurchaseOrderLineID);
		}

		//**************************************************************************              
		///    <Description>
		///       Update Delivery Schedule - DataSet
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       March, 9 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void UpdateDeliveryDataSet(DataSet pdstDeliverySchedule, int pintPurchaseOrderDetailID)
		{
			PO_DeliveryScheduleDS objPO_DeliveryScheduleDS = new PO_DeliveryScheduleDS();
			objPO_DeliveryScheduleDS.UpdateDataSet(pdstDeliverySchedule);
		}
		//**************************************************************************              
		///    <Description>
		///       Update Delivery Schedule - DataSet
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       THIENHD
		///    </Authors>
		///    <History>
		///       March, 9 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool GetPurchaseOrderApprovalStatus(int pintPurchaseOrderDetailID)
		{
			PO_PurchaseOrderDetailDS objPO_PurchaseOrderDetailDS = new PO_PurchaseOrderDetailDS();
			//return false;
			return objPO_PurchaseOrderDetailDS.GetPurchaseOrderApprovalStatus(pintPurchaseOrderDetailID);
		}

		/// <summary>
		/// Gets total schedule delivery quantity of a po line
		/// </summary>
		/// <param name="pintPODetailID">PurchaseOrderDetail ID</param>
		/// <returns>Total Schedule Delivery Quantity</returns>
	
		public decimal GetTotalDeliveryQuantityOfLine(int pintPODetailID)
		{
			PO_DeliveryScheduleDS dsSchedule = new PO_DeliveryScheduleDS();
			return dsSchedule.GetTotalDeliveryQuantityOfLine(pintPODetailID);
		}

	}
}

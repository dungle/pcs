using System;
using System.Data;
using PCSComUtils.PCSExc;


using PCSComUtils.Common;
using PCSComSale.Order.DS;
using PCSComUtils.MasterSetup.DS;


namespace PCSComSale.Order.BO
{
	/// <summary>
	/// Summary description for SODeliveryScheduleBO.
	/// </summary>
	/// 

	public interface ISODeliveryScheduleBO 
	{
		DataSet GetDeliverySchedule(int pintSaleOrderDetailID);
		void UpdateDeliveryDataSet(DataSet dstData, int pintSaleOrderDetailID);
		int GetMaxDeliveryScheduleLine(int pintSaleOrderDetailID);
		void DeleteDeliveryDetail (int pintSaleOrderDetailID);
	}

	
	
	public class SODeliveryScheduleBO : ISODeliveryScheduleBO
	{
		public SODeliveryScheduleBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IObjectBO Members

		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add SODeliveryScheduleBO.UpdateDataSet implementation
			try
			{
				SO_DeliveryScheduleDS objSO_DeliveryScheduleDS = new SO_DeliveryScheduleDS();
				objSO_DeliveryScheduleDS.UpdateDataSet(dstData);

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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void UpdateDeliveryDataSet(DataSet dstData, int pintSaleOrderDetailID)
		{
			// TODO:  Add SODeliveryScheduleBO.UpdateDataSet implementation
			try
			{
				SO_DeliveryScheduleDS objSO_DeliveryScheduleDS = new SO_DeliveryScheduleDS();
				objSO_DeliveryScheduleDS.UpdateDeliveryDataSet(dstData,pintSaleOrderDetailID);

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

	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add SODeliveryScheduleBO.Update implementation
		}

	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add SODeliveryScheduleBO.Delete implementation
		}


	
		public void Add(object pObjectDetail)
		{
			// TODO:  Add SODeliveryScheduleBO.Add implementation
		}

	
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add SODeliveryScheduleBO.GetObjectVO implementation
			return null;
		}

		#endregion

		#region ISODeliveryScheduleBO Members

		//**************************************************************************              
		///    <Description>
		///       Get the Delivery Detail dataset for a specific Sale Order Detail ID
		///    </Description>
		///    <Inputs>
		///        Sale Order Detail ID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet GetDeliverySchedule(int pintSaleOrderDetailID)
		{
			// TODO:  Add SODeliveryScheduleBO.GetDeliverySchedule implementation
			try 
			{
				SO_DeliveryScheduleDS objSO_DeliveryScheduleDS = new SO_DeliveryScheduleDS();
				DataSet dsData = objSO_DeliveryScheduleDS.GetDeliverySchedule(pintSaleOrderDetailID);
//				dsData.Tables[0].Columns[SO_DeliveryScheduleTable.LINE_FLD].AutoIncrement = true;
//				dsData.Tables[0].Columns[SO_DeliveryScheduleTable.LINE_FLD].AutoIncrementSeed = objSO_DeliveryScheduleDS.GetMaxDeliveryScheduleLine(pintSaleOrderDetailID) + 1;
				return dsData;
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
		//**************************************************************************              
		///    <Description>
		///       Get max sale order line 
		///       This max number will be used to add next value for the Line
		///    </Description>
		///    <Inputs>
		///        Sale Order Detail ID
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, February 01, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public int GetMaxDeliveryScheduleLine(int pintSaleOrderDetailID)
		{
			try 
			{
				SO_DeliveryScheduleDS objSO_DeliveryScheduleDS = new SO_DeliveryScheduleDS();
				return objSO_DeliveryScheduleDS.GetMaxDeliveryScheduleLine(pintSaleOrderDetailID);
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

	
		public void DeleteDeliveryDetail (int pintSaleOrderDetailID)
		{
			try
			{
				SO_DeliveryScheduleDS objSO_DeliveryScheduleDS = new SO_DeliveryScheduleDS();
				objSO_DeliveryScheduleDS.DeleteDeliveryDetail(pintSaleOrderDetailID);

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
		#endregion
	}
}

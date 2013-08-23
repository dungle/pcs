using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common.DS;

namespace PCSComUtils.Common.BO
{
	public interface IManagePeriodBO 
	{
		
		DataSet ListPeriod();
		object GetPeriod();
		void Delete(int pintPeriodID);
		/*
		DataSet ListPODetailByPOMasterID(int pintPOMasterID);
		int GetPurchaseOrderMasterID(string pstrCode);
		bool CheckLevelApproval(int pintApproverID, int pintMasterID);
		void UpdateAllAfterApprove(DataTable pdtbDataApprove, DateTime pdtmApprovalDate, int pintApproveID);
		*/
	}
	/// <summary>
	/// Summary description for UpdatePeriodBO.
	/// </summary>
	
	public class ManagePeriodBO :IManagePeriodBO
	{
		public ManagePeriodBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	
		public void Add(object pObjectDetail)
		{
			// TODO:  
			try
			{
				Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
				dsPeriod.Add(pObjectDetail);
			}
				catch (PCSException ex)
			{
				throw ex;
			}
				catch (Exception ex)
			{
				throw ex;
			}
		}
	
	
		public int AddAndReturnID(object pObjectDetail)
		{
			Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
			Sys_PeriodVO voPeriod = (Sys_PeriodVO)pObjectDetail;
			if (voPeriod.Activate)
				dsPeriod.CloseAllPeriod(voPeriod);
			return dsPeriod.AddAndReturnID(pObjectDetail);
		}
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_Period
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
		///       Trada
		///    </Authors>
		///    <History>
		///       May, 5 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(int pintPeriodID)
		{
			// TODO:  
			try 
			{
				Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
				dsPeriod.Delete(pintPeriodID);
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
		///       This method uses to update all data from sys_Period
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
		///       Trada
		///    </Authors>
		///    <History>
		///       May, 5 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void Delete(object pObjectDetail)
		{
			// TODO:  
			

		}
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  
			return null;
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to update all data from sys_Period
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
		///       Trada
		///    </Authors>
		///    <History>
		///       May, 5 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void ChangeStatusOfActivate(object pObjectDetail)
		{
			// TODO:
			try 
			{
				Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
				dsPeriod.ChangeStatusOfActivate(pObjectDetail);
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
		///       This method uses to update all data from sys_Period
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///      void
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       Trada
		///    </Authors>
		///    <History>
		///       May, 5 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pObjectDetail)
		{
			Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
			Sys_PeriodVO voPeriod = (Sys_PeriodVO)pObjectDetail;
			if (voPeriod.Activate)
				dsPeriod.CloseAllPeriod(voPeriod);
			dsPeriod.Update(pObjectDetail);
		}

		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  

		}
		
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_Period
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
		///       Trada
		///    </Authors>
		///    <History>
		///       May, 5 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet ListPeriod()
		{
			try
			{
				Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
				return dsPeriod.List();
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
		///       This method uses to get all data from sys_Period
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
		///       Trada
		///    </Authors>
		///    <History>
		///       May, 5 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetPeriod()
		{
			Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
			return dsPeriod.GetObjectVO();
		}

	
		public bool IsPeriodOverlap(object objPeriod)
		{
			Sys_PeriodDS dsPeriod = new Sys_PeriodDS();
			return dsPeriod.IsPeriodOverlap(objPeriod);
		}

	}
}

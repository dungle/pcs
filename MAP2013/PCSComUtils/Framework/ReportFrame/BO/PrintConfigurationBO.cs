using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class PrintConfigurationBO
	{
        private const string THIS = ".PrintConfigurationBO";		
		public PrintConfigurationBO()
		{
		}
		
		///    <Description>
		///       This method checks business rule and call Add() method of DS class 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///     13-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Add(object pobjObjectVO)
		{
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				templateDS.Add(pobjObjectVO);				
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
	
		
		//**************************************************************************              
		///    <Description>
		///       This method not implements yet
		///    </Description>
		///    <Inputs>
		///        
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
		///    13-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
       	
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}
	
	
		public DataTable GetPrintConfigurationByFormName(string pstrFormName)
		{
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				return templateDS.GetPrintConfigurationByFormName(pstrFormName);
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

		//**************************************************************************              
		///    <Description>
		///       This method not implements yet
		///    </Description>
		///    <Inputs>
		///        
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
		///    13-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	

		//**************************************************************************              
		///    <Description>
		///       This method checks business rule and call Delete() method of DS class 
		///    </Description>
		///    <Inputs>
		///        pintID      
		///    </Inputs>
		///    <Outputs>
		///     Delete a record  from Database
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///    13-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(int pintID)
		{
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				templateDS.Delete(pintID);				
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data 
		///    </Description>
		///    <Inputs>
		///       pintID    
		///    </Inputs>
		///    <Outputs>
		///      Value object
		///    </Outputs>
		///    <Returns>
		///       object
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///     13-Dec-2004  
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(int pintID)
		{	
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				return templateDS.GetObjectVO(pintID);				
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to update data
		///    </Description>
		///    <Inputs>
		///       pobjObjecVO
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa 
		///    </Authors>
		///    <History>
		///    13-Dec-2004   
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pobjObjecVO)
		{
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				templateDS.Update(pobjObjecVO);				
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

		
		//**************************************************************************              
		///    <Description>
		///      This method uses to get all data
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///    
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       HungLa 
		///    </Authors>
		///    <History>
		///      
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet List()
		{
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				return templateDS.List();
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
		
		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
		///    </Inputs>
		///    <Outputs>
		///      N/A 
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       13-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void UpdateDataSet(DataSet pData)
		{
			try
			{
				Sys_PrintConfigurationDS  templateDS = new Sys_PrintConfigurationDS();
				templateDS.UpdateDataSet(pData);				
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
	}
}



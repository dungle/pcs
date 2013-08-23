using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class EditReportBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.EditReportBO";
		
		//**************************************************************************
		///    <Description>
		///       Default constructor
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///     3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public EditReportBO()
		{
		}
		
		//**************************************************************************
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
		///       DungLA
		///    </Authors>
		///    <History>
		///     3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Add(object pobjObjectVO)
		{
			sys_ReportDS  dsReport = new sys_ReportDS();
			dsReport.Add(pobjObjectVO);
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    3-Jan-2005 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    3-Jan-2005 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


	
		public void Delete(object pObjectVO)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
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
		///       DungLA
		///    </Authors>
		///    <History>
		///    3-Jan-2005 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(string pstrID)
		{
			sys_ReportDS  dsReport = new sys_ReportDS();
			dsReport.Delete(pstrID);
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
		///       DungLA
		///    </Authors>
		///    <History>
		///     3-Jan-2005  
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(string pstrID)
		{	
			sys_ReportDS  dsReport = new sys_ReportDS();
			return dsReport.GetObjectVO(pstrID);
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
		///       DungLA 
		///    </Authors>
		///    <History>
		///    3-Jan-2005   
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pobjObjecVO)
		{
			sys_ReportDS  dsReport = new sys_ReportDS();
			dsReport.Update(pobjObjecVO);
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
		///       DungLA 
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet List()
		{
			sys_ReportDS  dsReport = new sys_ReportDS();
			return dsReport.List();
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
		///       DungLA
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void UpdateDataSet(DataSet pData)
		{
			sys_ReportDS  dsReport = new sys_ReportDS();
			dsReport.UpdateDataSet(pData);
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to add a report to specified group
		///    </Description>
		///    <Inputs>
		///        ReportID, GroupID
		///    </Inputs>
		///    <Outputs>
		///      N/A 
		///    </Outputs>
		///    <Returns>
		///       New report order
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int AddReportToGroup(string pstrReportID, string pstrGroupID)
		{
			sys_ReportAndGroupDS dsReportAndGroup = new sys_ReportAndGroupDS();
			sys_ReportAndGroupVO voReportAndGroup = new sys_ReportAndGroupVO();
				
			voReportAndGroup.GroupID = pstrGroupID;
			voReportAndGroup.ReportID = pstrReportID;
			// increase report order by one
			voReportAndGroup.ReportOrder = dsReportAndGroup.GetMaxReportOrder(pstrGroupID) + 1;
			// insert report and group to sys_ReportAndGroup table
			dsReportAndGroup.Add(voReportAndGroup);
			return voReportAndGroup.ReportOrder;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to fill form controls with data by specified conditions.
		///    </Description>
		///    <Inputs>
		///        condition
		///    </Inputs>
		///    <Outputs>
		///      N/A 
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       3-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void FillData(string pstrCondition)
		{
			const string METHOD_NAME = THIS + ".FillData()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME, new Exception());
		}
	}
}


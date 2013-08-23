using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.Framework.TableFrame.DS;

namespace PCSComUtils.Framework.ReportFrame.BO
{
	public class ReportParameterBO
	{
		private const string THIS = "PCSComUtils.Framework.ReportFrame.BO.ReportParameterBO";		
		public ReportParameterBO()
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
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			templateDS.Add(pobjObjectVO);
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
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			templateDS.Delete(((sys_ReportParaVO)pObjectVO).ReportID, ((sys_ReportParaVO)pObjectVO).ParaName);
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
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			templateDS.Delete(pintID);
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
	
		public object GetObjectVO(string pstrReportID, string pstrParaName)
		{	
			sys_ReportParaDS  dsReportPara = new sys_ReportParaDS();
			return dsReportPara.GetObjectVO(pstrReportID, pstrParaName);
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
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			templateDS.Update(pobjObjecVO);
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data
		///    </Description>
		///    <Inputs>
		///       pobjObjecVO, Old para name
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
		///		09-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pobjObjecVO, string pstrParaName)
		{
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			templateDS.Update(pobjObjecVO, pstrParaName);
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
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			return templateDS.List();
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
			sys_ReportParaDS  templateDS = new sys_ReportParaDS();
			templateDS.UpdateDataSet(pData);
		}

		//**************************************************************************              
		///    <Description>
		///      This method uses to get all para of specified report
		///    </Description>
		///    <Inputs>
		///      ReportID
		///    </Inputs>
		///    <Outputs>
		///		List of parameter
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       DungLA 
		///    </Authors>
		///    <History>
		///      05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList ListByReport(string pstrReportID)
		{
			sys_ReportParaDS  dsReportPara = new sys_ReportParaDS();
			return dsReportPara.GetObjectVOs(pstrReportID);
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to change up order of a row in sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        ReportID, ParaName
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Created: 05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool MoveUp(string pstrReportID, string pstrParaName)
		{
			bool blnResult = false;
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();
			sys_ReportParaVO voPreviousPara = new sys_ReportParaVO();
			// get current para order
			sys_ReportParaVO voCurrentPara = (sys_ReportParaVO)dsReportPara.GetObjectVO(pstrReportID, pstrParaName);
			// if current parameter reached the top of order - 1, then cannot move
			if (voCurrentPara.ParaOrder <= 1)
			{
				blnResult = false;
			}
			else
			{
				// get next para order
				int intNextOrder = dsReportPara.GetNextOrder(pstrReportID, voCurrentPara.ParaOrder, MoveDirection.Up);
				// change order
				voPreviousPara.ParaOrder = voCurrentPara.ParaOrder;
				voCurrentPara.ParaOrder = intNextOrder;

				// update two rows in database
				dsReportPara.Update(voPreviousPara);
				dsReportPara.Update(voCurrentPara);
				// return value
				blnResult = true;
			}
			return blnResult;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to change down order of a row in sys_ReportPara
		///    </Description>
		///    <Inputs>
		///        ReportID, ParaName
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Created: 05-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public bool MoveDown(string pstrReportID, string pstrParaName)
		{
			bool blnResult = false;
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();
			sys_ReportParaVO voNextPara = new sys_ReportParaVO();
			// get current para order
			sys_ReportParaVO voCurrentPara = (sys_ReportParaVO)dsReportPara.GetObjectVO(pstrReportID, pstrParaName);
			// get max order
			int intMaxOrder = dsReportPara.GetMaxOrder(pstrReportID);
			// if current parameter reached the bottom, then cannot move down
			if (voCurrentPara.ParaOrder == intMaxOrder)
			{
				blnResult = false;
			}
			else
			{
				// get next para order
				int intNextOrder = dsReportPara.GetNextOrder(pstrReportID, voCurrentPara.ParaOrder, MoveDirection.Down);
				// change order
				voNextPara.ParaOrder = voCurrentPara.ParaOrder;
				voCurrentPara.ParaOrder = intNextOrder;

				// update two rows in database
				dsReportPara.Update(voNextPara);
				dsReportPara.Update(voCurrentPara);
				// return value
				blnResult = true;
			}
			return blnResult;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get max para order of specified report
		///    </Description>
		///    <Inputs>
		///        ReportID
		///    </Inputs>
		///    <Outputs>
		///      Max order
		///    </Outputs>
		///    <Returns>
		///       int
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Created: 09-Jan-2005
		///    </History>
		///    <Notes>
		///		  Change output from ArrayList to DataSet
		///    </Notes>
		//**************************************************************************
	
		public int GetMaxParaOrder(string pstrReportID)
		{
			sys_ReportParaDS dsReportPara = new sys_ReportParaDS();
			return dsReportPara.GetMaxOrder(pstrReportID);
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all table
		///    </Description>
		///    <Inputs>
		///        
		///    </Inputs>
		///    <Outputs>
		///      DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       Created: 05-Jan-2005
		///       Modified: 08-Jan-2005
		///    </History>
		///    <Notes>
		///		  Change output from ArrayList to DataSet
		///    </Notes>
		//**************************************************************************
	
		public DataSet GetAllTables()
		{
			sys_TableDS dsTable = new sys_TableDS();
			return dsTable.GetAllTables();
		}

		//**************************************************************************              
		///    <Description>
		///       Return the list of Fields from database
		///    </Description>
		///    <Inputs>
		///       Null 
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       07-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

	
		public DataSet getFieldList(int pintTableID) 
		{
			sys_TableFieldDS objTableFieldDS = new sys_TableFieldDS();
			return objTableFieldDS.List(pintTableID);
		}

		/// <summary>
		/// Switch two params order
		/// </summary>
		/// <param name="pobjSourceParameter">Source Parameter</param>
		/// <param name="pobjDestParameter">Destination Parameter</param>
	
		public void SwitchParameters(object pobjSourceParameter, object pobjDestParameter)
		{
			sys_ReportParaDS dsReportParameter = new sys_ReportParaDS();
			dsReportParameter.Update(pobjSourceParameter);
			dsReportParameter.Update(pobjDestParameter);
		}
	}
}
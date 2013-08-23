using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.TableFrame.DS;

namespace PCSComUtils.Framework.TableFrame.BO
{
	public class TableConfigBO
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.BO.ITableConfigBO";		
		public TableConfigBO()
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
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void AddTable(object pobjObjectVO,int pintGroupID)
		{
			try
			{
				sys_TableVO voSysTable = (sys_TableVO)pobjObjectVO;
				sys_TableAndGroupVO voTableAndGroup = new sys_TableAndGroupVO();
				sys_TableDS  dsSysTable = new sys_TableDS();
				
				voTableAndGroup.TableGroupID = pintGroupID;
				voTableAndGroup.TableID = voSysTable.TableID;
				voTableAndGroup.TableOrder = dsSysTable.MaxTableOrder(pintGroupID) + 1;
				
				dsSysTable.AddTable(pobjObjectVO,pintGroupID);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch (Exception ex)
			{
				
				throw ex;
			}
		}	
	

		///    <Description>
		///       This method checks business rule and call AddAndReturnMaxID() method of DS class 
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
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int AddTableAndReturnMaxID(object pobjObjectVO,int pintGroupID)
		{
//			try
//			{
				sys_TableVO voSysTable = (sys_TableVO)pobjObjectVO;
				sys_TableAndGroupVO voTableAndGroup = new sys_TableAndGroupVO();
				sys_TableDS  dsSysTable = new sys_TableDS();
				
				voTableAndGroup.TableGroupID = pintGroupID;
				voTableAndGroup.TableID = voSysTable.TableID;
				voTableAndGroup.TableOrder = dsSysTable.MaxTableOrder(pintGroupID) + 1;
				
				return dsSysTable.AddTableAndReturnMaxID(pobjObjectVO,pintGroupID);
//			}
//			catch(PCSDBException ex)
//			{
//				throw ex;
//			}
//			catch (Exception ex)
//			{
//				throw ex;
//			}
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
		///    27-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		
		public object GetObjectVO(int pintID,string VOclass)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			throw new PCSException(ErrorCode.NOT_IMPLEMENT, METHOD_NAME,new Exception());
		}	
	
		/// <summary>
		/// Get all columns name of a table
		/// </summary>
		/// <param name="pstrTableOrViewName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, Nov 30 2005</date>
		public DataSet GetAllColumnNameOfTable(string pstrTableOrViewName)
		{
			DataSet dstData = new DataSet();	
			sys_TableDS dssys_Table = new sys_TableDS();
			dstData = dssys_Table.GetAllColumnNameOfTable(pstrTableOrViewName);
			return dstData;
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
		///    27-Dec-2004 
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
		///    27-Dec-2004 
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(int pintID)
		{
			try
			{
				sys_TableDS  dsSysTable = new sys_TableDS();
				dsSysTable.Delete(pintID);
			}
			catch(PCSDBException ex)
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
		///     27-Dec-2004  
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(int pintID)
		{	
			try
			{
				sys_TableDS  dsSysTable = new sys_TableDS();
				return dsSysTable.GetObjectVO(pintID);
				
			}
			catch(PCSDBException ex)
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
		///    27-Dec-2004   
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************		
	
		public void UpdateTable(object pobjObjecVO)
		{
			try
			{
				sys_TableDS  dsSysTable = new sys_TableDS();
				dsSysTable.Update(pobjObjecVO);
			}
			catch(PCSDBException ex)
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
				sys_TableDS  dsSysTable = new sys_TableDS();
				return dsSysTable.List();
			}
			catch(PCSDBException ex)
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
		///      N/A 
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void UpdateDataSet(DataSet pData)
		{
			try
			{
				sys_TableDS  dsSysTable = new sys_TableDS();
				dsSysTable.UpdateDataSet(pData);
			}
			catch(PCSDBException ex)
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
		///       This method uses to get all row in sys_table 
		///    </Description>
		///    <Inputs>
		///        N/A       
		///    </Inputs>
		///    <Outputs>
		///      dataset sys_TableVO
		///    </Outputs>
		///    <Returns>
		///       dataset
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       28-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet ListTableOrView()
		{
			try
			{
				sys_TableDS dsSysTable = new sys_TableDS();
				return dsSysTable.ListTableOrView();
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add TableConfigBO.Add implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add TableConfigBO.Update implementation
		}

		#endregion
	}
}

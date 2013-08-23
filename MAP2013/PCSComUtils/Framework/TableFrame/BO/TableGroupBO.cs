using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.TableFrame.DS;

namespace PCSComUtils.Framework.TableFrame.BO
{
	public class TableGroupBO
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.BO.ITableGroupBO";		
		public TableGroupBO()
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
		public void AddGroup(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddGroup()";
			try
			{
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				dsTableGroup.Add(pobjObjectVO);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{
				
				throw new PCSBOException(ErrorCode.MESSAGE_COM_TRANSACTION,METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				
				throw ex;
			}

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
	
		public int AddGroupAndReturnMaxID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".AddGroup()";
			try
			{
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				return dsTableGroup.AddAndReturnMaxID(pobjObjectVO);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{
				throw new PCSBOException(ErrorCode.MESSAGE_COM_TRANSACTION,METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw ex;
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
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				dsTableGroup.Delete(pintID);
				
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
		///     13-Dec-2004  
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(int pintID)
		{	
			try
			{
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				return dsTableGroup.GetObjectVO(pintID);
				
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
		///    13-Dec-2004   
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateGroup(object pobjObjecVO)
		{

			const string METHOD_NAME = THIS + ".UpdateGroup()";
			try
			{
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				dsTableGroup.Update(pobjObjecVO);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			catch (System.Runtime.InteropServices.COMException ex) 
			{
				
				throw new PCSBOException(ErrorCode.MESSAGE_COM_TRANSACTION,METHOD_NAME,ex);
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
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				return dsTableGroup.List();
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
		///       13-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSet(DataSet pData)
		{
			try
			{
				sys_TableGroupDS  dsTableGroup = new sys_TableGroupDS();
				dsTableGroup.UpdateDataSet(pData);
				
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
		///       This method uses to get a row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        GroupID      
		///    </Inputs>
		///    <Outputs>
		///      object sys_TableGroupVO
		///    </Outputs>
		///    <Returns>
		///       object
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
	
		public object ReturnTableGroup(int pintGroupID)
		{
			try
			{
				sys_TableGroupDS dsTableGroup = new sys_TableGroupDS();
				return dsTableGroup.GetObjectVO(pintGroupID);
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
		///       This method uses to get the order of row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        N/A      
		///    </Inputs>
		///    <Outputs>
		///      the order of group
		///    </Outputs>
		///    <Returns>
		///       int
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
	
		public int ReturnGroupOrder()
		{
			try
			{
				sys_TableGroupDS dsTableGroup = new sys_TableGroupDS();
				return dsTableGroup.MaxGroupOrder();
			}
			catch (PCSDBException ex)
			{
				throw (ex);
			}			
			catch (Exception ex)
			{
				throw ex;
			}

		}
		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add TableGroupBO.Add implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add TableGroupBO.Update implementation
		}

		#endregion
	}
}

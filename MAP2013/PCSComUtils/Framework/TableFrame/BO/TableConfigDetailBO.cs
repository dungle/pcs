using System.Data;
using System.Collections;
using PCSComUtils.Framework.TableFrame.DS;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.TableFrame.BO
{
	public class TableConfigDetailBO
	{
		public TableConfigDetailBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to add a record into sys_TableField
		///    </Description>
		///    <Inputs>
		///        sysTableFieldVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       TableID
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Add(object pobjObjectVO)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				ds.Add(pobjObjectVO);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}		
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to sys_TableField
		///    </Description>
		///    <Inputs>
		///        sysTableFieldVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Update(object pobjObjectVO)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				ds.Update(pobjObjectVO);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}		
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from sys_TableField
		///    </Description>
		///    <Inputs>
		///        TableID       
		///    </Inputs>
		///    <Outputs>
		///       sysTableFieldVO
		///    </Outputs>
		///    <Returns>
		///       sysTableFieldVO
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public object GetObjectVO(int pintTableFieldID)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				return ds.GetObjectVO(pintTableFieldID);
			}
			catch(PCSDBException ex)
			{				
				throw ex;
			}		
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from sys_TableField
		///    </Description>
		///    <Inputs>
		///        pintTableID
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void Delete(int pintTableFieldID)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				ds.Delete(pintTableFieldID);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}		
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_TableGroup
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
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet List(int pintTableID)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				return ds.List(pintTableID);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}		
		}
	
		public DataSet List(string pstrTableName)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				return ds.List(pstrTableName);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}		
		}
		//**************************************************************************              
		///    <Description>
		///       List field name
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
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataSet ListFieldName(string pstrTableName)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				return ds.ListFieldName(pstrTableName);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}	
		}

		/// <summary>
		/// GetInformationSchema
		/// </summary>
		/// <param name="pstrTableName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Friday, April 14 2006</date>
	
		public DataSet GetInformationSchema(string pstrTableName)
		{
			DataSet dstReturn = new DataSet();
			sys_TableFieldDS dsTableField = new sys_TableFieldDS();
			dstReturn = dsTableField.GetInformationSchema(pstrTableName);
			return dstReturn;
		}
		//**************************************************************************              
		///    <Description>
		///       List field name
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
		///       Hoang Trung Son - iSphere software
		///    </Authors>
		///    <History>
		///       14-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetColumnProperty(string pstrTableName,string pstrFieldName)
		{
			try
			{
				sys_TableFieldDS ds = new sys_TableFieldDS();
				return ds.GetColumnProperty(pstrTableName,pstrFieldName);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}	
		}	
		
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add TableConfigDetailBO.UpdateDataSet implementation

		}
		/// <summary>
		/// UpdateDataSetByTableID
		/// </summary>
		/// <param name="dstData"></param>
		/// <param name="pintTableID"></param>
		/// <author>Trada</author>
		/// <date>Thursday, Dec 1 2005</date>
	
		public void UpdateDataSetByTableID(DataSet dstData, int pintTableID)
		{
			sys_TableFieldDS dsSys_TableField = new sys_TableFieldDS();
			dsSys_TableField.UpdateDataSetByTableID(dstData, pintTableID);
		}
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add TableConfigDetailBO.GetObjectVO implementation
			return null;
		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add TableConfigDetailBO.Delete implementation

		}
	}
}

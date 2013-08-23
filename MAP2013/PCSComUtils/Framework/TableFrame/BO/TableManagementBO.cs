using System;
using System.Collections;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.TableFrame.DS;

namespace PCSComUtils.Framework.TableFrame.BO
{
	public class TableManagementBO
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.BO.ITableManagementBO";		
		const string COPY_OF = "copy of ";

		public TableManagementBO()
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
				sys_TableAndGroupDS  templateDS = new sys_TableAndGroupDS();
				templateDS.Add(pobjObjectVO);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
		}	
	
	
		public object GetTableInfo(int pintTableID)
		{
			try
			{
				sys_TableDS dssys_TableDS = new sys_TableDS();
				return dssys_TableDS.GetObjectVO(pintTableID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		

	
		public object GetGroupInfo(int pintGroupID)
		{
			try
			{
				sys_TableGroupDS dssys_TableGroupDS = new sys_TableGroupDS();
				return dssys_TableGroupDS.GetObjectVO(pintGroupID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
			catch (Exception ex)
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
				sys_TableAndGroupDS  templateDS = new sys_TableAndGroupDS();
				templateDS.Delete(pintID);
				
			}
			catch(PCSDBException ex)
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
				sys_TableAndGroupDS  templateDS = new sys_TableAndGroupDS();
				return templateDS.GetObjectVO(pintID);
				
			}
			catch(PCSDBException ex)
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
		public void Update(object pobjObjecVO)
		{
			try
			{
				sys_TableAndGroupDS  templateDS = new sys_TableAndGroupDS();
				templateDS.Update(pobjObjecVO);
				
			}
			catch(PCSDBException ex)
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
				sys_TableAndGroupDS  templateDS = new sys_TableAndGroupDS();
				return templateDS.List();
			}
			catch(PCSDBException ex)
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
				sys_TableAndGroupDS  templateDS = new sys_TableAndGroupDS();
				templateDS.UpdateDataSet(pData);
				
			}
			catch(PCSDBException ex)
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
		///      object sys_TableVO
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetAllTable()
		{
			const string METHOD_NAME = "GetAllTable()";
			sys_TableDS dsSysTable = new sys_TableDS();
			sys_TableVO objTableVO;
			ArrayList arr = new ArrayList();
			DataSet dset = new DataSet();
			try
			{
				dset = dsSysTable.List();
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}

			try
			{
				
				if (dset.Tables[0] != null)
				{
					foreach (DataRow row in dset.Tables[0].Rows)
					{
						objTableVO = new sys_TableVO();
						objTableVO.TableID = int.Parse(row[sys_TableTable.TABLEID_FLD].ToString());
						objTableVO.Code = row[sys_TableTable.CODE_FLD].ToString().Trim();
						objTableVO.TableName = row[sys_TableTable.TABLENAME_FLD].ToString().Trim();
						objTableVO.TableOrView = row[sys_TableTable.TABLEORVIEW_FLD].ToString().Trim();
						objTableVO.Height = int.Parse(row[sys_TableTable.HEIGHT_FLD].ToString());
						objTableVO.IsViewOnly = bool.Parse(row[sys_TableTable.ISVIEWONLY_FLD].ToString());
						
						//insert into array
						arr.Add(objTableVO);
					}
				}
				else
					arr = null;
				return arr;
			}
			catch (Exception ex)
			{
				throw new PCSBOException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all row in sys_tableandgroup 
		///    </Description>
		///    <Inputs>
		///        N/A       
		///    </Inputs>
		///    <Outputs>
		///      object sys_TableandGroupVO
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetAllTableAndGroup()
		{
			const string METHOD_NAME = "GetAllTableAndGroup()";
			sys_TableAndGroupDS objAnd = new sys_TableAndGroupDS();
			sys_TableAndGroupVO objAndVO;
			ArrayList arr = new ArrayList();
			try
			{
				DataSet dset = new DataSet();
				dset = objAnd.List();
				foreach (DataRow row in dset.Tables[0].Rows)
				{
					objAndVO = new sys_TableAndGroupVO();
					objAndVO.TableAndGroupID = int.Parse(row["TableAndGroupId"].ToString());
					objAndVO.TableGroupID = int.Parse(row["TableGroupID"].ToString());
					objAndVO.TableID = int.Parse(row["TableID"].ToString());
					objAndVO.TableOrder = int.Parse(row["TableOrder"].ToString());
					
					//add into array
					arr.Add(objAndVO);
				}
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			return arr;
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get all row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        N/A       
		///    </Inputs>
		///    <Outputs>
		///      object sys_TableGroupVO
		///    </Outputs>
		///    <Returns>
		///       ArrayList
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public ArrayList GetAllGroup()
		{
			const string METHOD_NAME = "GetAllGroup()";
			sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();
			sys_TableGroupVO voSysTableGroup;
			ArrayList arr = new ArrayList();
			try
			{
				DataSet dset = new DataSet();
				dset = dsSysTableGroup.List();
				foreach (DataRow row in dset.Tables[0].Rows)
				{
					voSysTableGroup = new sys_TableGroupVO();
					voSysTableGroup.TableGroupID = int.Parse(row[sys_TableGroupTable.TABLEGROUPID_FLD].ToString());
					voSysTableGroup.Code = row[sys_TableGroupTable.CODE_FLD].ToString().Trim();
					voSysTableGroup.TableGroupName = row[sys_TableGroupTable.TABLEGROUPNAME_FLD].ToString().Trim();
					voSysTableGroup.GroupOrder = int.Parse(row[sys_TableGroupTable.GROUPORDER_FLD].ToString());
					
					// add object sys_TableGroupVO into array
					arr.Add(voSysTableGroup);
				}
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			return arr;
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to delete a row in sys_table 
		///    </Description>
		///    <Inputs>
		///        TableID     
		///    </Inputs>
		///    <Outputs>
		///      void
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
	
		public void DeleteTable(int pintTableID)
		{
			try
			{
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				dsTableAndGroup.Delete(pintTableID);
				dsSysTable.Delete(pintTableID);
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete a row in sys_table 
		///    </Description>
		///    <Inputs>
		///        TableID     
		///    </Inputs>
		///    <Outputs>
		///      void
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
	
		public void DeleteTable(int pintTableID,int pintTableGroupID)
		{
			try
			{
				sys_TableAndGroupDS objsys_TableAndGroupDS = new sys_TableAndGroupDS();
				sys_TableFieldDS objsys_TableFieldDS = new sys_TableFieldDS();
				sys_TableDS dsSysTable = new sys_TableDS();

				//first check to see if this TableID is in another group
				int intNoOfTableInGroup = objsys_TableAndGroupDS.CountTableInGroup(pintTableID);

				//First delete the Sys_TableAndGroup 
				
				if (intNoOfTableInGroup > 1)
				{
					objsys_TableAndGroupDS.DeleteTableAndGroup(pintTableID,pintTableGroupID);
				}
				else
				{
					objsys_TableAndGroupDS.Delete(pintTableID);
					//objsys_TableAndGroupDS.DeleteTableAndGroup(pintTableID,pintTableGroupID);

					//Second delete the Table Field
				
					objsys_TableFieldDS.DeleteTable(pintTableID);

					//the last is used to delete the sys_TableDS
				
					//sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
					

					dsSysTable.Delete(pintTableID);
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to copy total tables inside the specified tablegroup 
		///    </Description>
		///    <Inputs>
		///        GroupID     
		///    </Inputs>
		///    <Outputs>
		///      sys_tablegroup and sys_table
		///    </Outputs>
		///    <Returns>
		///       void
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
		public void CopyGroup(int pintGroupID)
		{
			try
			{ //copy old object in tablegroup and add it into database
				sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();

				//create object to contain the current TableGroup row
				sys_TableGroupVO voOldTableGroup = new sys_TableGroupVO();
				voOldTableGroup = (sys_TableGroupVO)dsSysTableGroup.GetObjectVO(pintGroupID);

				//create new object which will be copiedobject
				sys_TableGroupVO voNewTableGroup = new sys_TableGroupVO();
				voNewTableGroup.Code = COPY_OF + voOldTableGroup.Code;
				voNewTableGroup.TableGroupName = COPY_OF + voOldTableGroup.TableGroupName;
				voNewTableGroup.GroupOrder = dsSysTableGroup.MaxGroupOrder() + 1;
			
				int intNewGroupID = dsSysTableGroup.AddGroupAndReturnID(voNewTableGroup);

				//Copy from all tables from old group to a new group
				//initialize the sys_TableAndGroupDs
				sys_TableAndGroupDS objSys_TableAndGroupDS = new sys_TableAndGroupDS();
				objSys_TableAndGroupDS.CopyTwoGroup(pintGroupID,intNewGroupID);


				//dsSysTableGroup.Add(voNewTableGroup);
				//return groupid for new tablegroup row
				/*
				int intGroupID = dsSysTableGroup.MaxGroupID();
				
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				DataSet sysAndDSET = new DataSet();
				sysAndDSET = dsTableAndGroup.List();
				int nTableOrder = dsSysTable.MaxTableOrder(intGroupID);

				foreach (DataRow r in sysAndDSET.Tables[0].Rows)
				{
					if (r["TableGroupID"].Equals(pintGroupID))
					{
						//get back tableid of copied table
						int intTableID = int.Parse(r["TableID"].ToString());
						//get back old row in sysTable
						sys_TableVO oldTableVO = new sys_TableVO();
						oldTableVO = (sys_TableVO)dsSysTable.GetObjectVO(intTableID);

						//create new row in sysTable which will be a copied row
						sys_TableVO voSysTable = new sys_TableVO();
						voSysTable.Code = COPY_OF + oldTableVO.Code;
						voSysTable.TableName = COPY_OF + oldTableVO.TableName;
						voSysTable.TableOrView = oldTableVO.TableOrView;
						voSysTable.Height = oldTableVO.Height;
						//insert new row into sysTable
						dsSysTable.Add(voSysTable);
						int intNewTableID = dsSysTable.MaxTableID();

						//create new row in sysTableAndGroup which will be a copied row
						sys_TableAndGroupVO voTableAndGroup = new sys_TableAndGroupVO();
						voTableAndGroup.TableGroupID = intGroupID;
						voTableAndGroup.TableID = intNewTableID;
						voTableAndGroup.TableOrder = ++nTableOrder;
						//insert new row into sysTableAndGroup
						dsTableAndGroup.Add(voTableAndGroup);
					}
				}
				*/

				
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
		///       This method uses to copy total tables inside the specified tablegroup 
		///    </Description>
		///    <Inputs>
		///        GroupID     
		///    </Inputs>
		///    <Outputs>
		///      sys_tablegroup and sys_table
		///    </Outputs>
		///    <Returns>
		///       void
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
	
		public int CopyGroupAndReturnMaxID(int pintGroupID)
		{
			try
			{ //copy old object in tablegroup and add it into database
				sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();

				//create object to contain the current TableGroup row
				sys_TableGroupVO voOldTableGroup = new sys_TableGroupVO();
				voOldTableGroup = (sys_TableGroupVO)dsSysTableGroup.GetObjectVO(pintGroupID);

				//create new object which will be copiedobject
				sys_TableGroupVO voNewTableGroup = new sys_TableGroupVO();
				voNewTableGroup.Code = COPY_OF + voOldTableGroup.Code;
				voNewTableGroup.TableGroupName = COPY_OF + voOldTableGroup.TableGroupName;
				voNewTableGroup.GroupOrder = dsSysTableGroup.MaxGroupOrder() + 1;
			
				int intNewGroupID = dsSysTableGroup.AddGroupAndReturnID(voNewTableGroup);

				//Copy from all tables from old group to a new group
				//initialize the sys_TableAndGroupDs
				sys_TableAndGroupDS objSys_TableAndGroupDS = new sys_TableAndGroupDS();
				objSys_TableAndGroupDS.CopyTwoGroup(pintGroupID,intNewGroupID);
				
				return intNewGroupID;

				//dsSysTableGroup.Add(voNewTableGroup);
				//return groupid for new tablegroup row
				/*
				int intGroupID = dsSysTableGroup.MaxGroupID();
				
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				DataSet sysAndDSET = new DataSet();
				sysAndDSET = dsTableAndGroup.List();
				int nTableOrder = dsSysTable.MaxTableOrder(intGroupID);

				foreach (DataRow r in sysAndDSET.Tables[0].Rows)
				{
					if (r["TableGroupID"].Equals(pintGroupID))
					{
						//get back tableid of copied table
						int intTableID = int.Parse(r["TableID"].ToString());
						//get back old row in sysTable
						sys_TableVO oldTableVO = new sys_TableVO();
						oldTableVO = (sys_TableVO)dsSysTable.GetObjectVO(intTableID);

						//create new row in sysTable which will be a copied row
						sys_TableVO voSysTable = new sys_TableVO();
						voSysTable.Code = COPY_OF + oldTableVO.Code;
						voSysTable.TableName = COPY_OF + oldTableVO.TableName;
						voSysTable.TableOrView = oldTableVO.TableOrView;
						voSysTable.Height = oldTableVO.Height;
						//insert new row into sysTable
						dsSysTable.Add(voSysTable);
						int intNewTableID = dsSysTable.MaxTableID();

						//create new row in sysTableAndGroup which will be a copied row
						sys_TableAndGroupVO voTableAndGroup = new sys_TableAndGroupVO();
						voTableAndGroup.TableGroupID = intGroupID;
						voTableAndGroup.TableID = intNewTableID;
						voTableAndGroup.TableOrder = ++nTableOrder;
						//insert new row into sysTableAndGroup
						dsTableAndGroup.Add(voTableAndGroup);
					}
				}
				*/

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
		///       This method uses to delete a row in sys_tablegroup 
		///    </Description>
		///    <Inputs>
		///        GroupID      
		///    </Inputs>
		///    <Outputs>
		///      N/A
		///    </Outputs>
		///    <Returns>
		///       void
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
	
		public void DeleteGroup(int pintGroupID)
		{
			try
			{
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				sys_TableDS dsSysTable = new sys_TableDS();
				DataSet dset = new DataSet();
				dset = dsTableAndGroup.List();
				DataTable dtable = dset.Tables[0];
				foreach (DataRow row in dtable.Rows)
				{
					if (row[sys_TableAndGroupTable.TABLEGROUPID_FLD].Equals(pintGroupID))
					{
						int intTableID = int.Parse(row[sys_TableAndGroupTable.TABLEID_FLD].ToString());
						dsTableAndGroup.Delete(intTableID);
						dsSysTable.Delete(intTableID);
					}
				}
				sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();
				dsSysTableGroup.Delete(pintGroupID);
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to change up order of a row in sys_table 
		///    </Description>
		///    <Inputs>
		///        TableID     
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int MoveUpTable(int pintTableID)
		{
			int intTableOrder = 0;
			try
			{
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupVO objAndVOPre = new sys_TableAndGroupVO();
				sys_TableAndGroupVO objAndVOCur = new sys_TableAndGroupVO();
				ArrayList arrobjAnd = new ArrayList();

				int intGroupID  = dsTableAndGroup.GetGroupIDByTableID(pintTableID);
				arrobjAnd = dsTableAndGroup.GetObjectVOs(intGroupID);
				for (int i = 0; i < arrobjAnd.Count; i++)
				{
					sys_TableAndGroupVO obj = (sys_TableAndGroupVO)arrobjAnd[i];
					if (obj.TableID == pintTableID)
					{
						int mCurTableOrder = obj.TableOrder;
						if (mCurTableOrder > 1)
						{
							objAndVOPre = (sys_TableAndGroupVO)arrobjAnd[i-1];
							objAndVOCur = (sys_TableAndGroupVO)arrobjAnd[i];
							objAndVOCur.TableOrder = objAndVOPre.TableOrder;
							objAndVOPre.TableOrder = mCurTableOrder;
							intTableOrder = objAndVOPre.TableOrder;
							//update two rows into database
							dsTableAndGroup.Update(objAndVOPre);
							dsTableAndGroup.Update(objAndVOCur);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			return intTableOrder;
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to change up order of a row in sys_table 
		///    </Description>
		///    <Inputs>
		///        TableID     
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int MoveUpTable(int pintTableID, int pintTableGroupID)
		{
			int intTableOrder = 0;
			try
			{
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupVO objAndVOPre = new sys_TableAndGroupVO();
				sys_TableAndGroupVO objAndVOCur = new sys_TableAndGroupVO();
				ArrayList arrobjAnd = new ArrayList();

				int intGroupID  = pintTableGroupID ; // dsTableAndGroup.GetGroupIDByTableID(pintTableID);
				arrobjAnd = dsTableAndGroup.GetObjectVOs(intGroupID);
				for (int i = 0; i < arrobjAnd.Count; i++)
				{
					sys_TableAndGroupVO obj = (sys_TableAndGroupVO)arrobjAnd[i];
					if (obj.TableID == pintTableID)
					{
						int mCurTableOrder = obj.TableOrder;
						if (mCurTableOrder > 1)
						{
							objAndVOPre = (sys_TableAndGroupVO)arrobjAnd[i-1];
							objAndVOCur = (sys_TableAndGroupVO)arrobjAnd[i];
							objAndVOCur.TableOrder = objAndVOPre.TableOrder;
							objAndVOPre.TableOrder = mCurTableOrder;
							intTableOrder = objAndVOPre.TableOrder;
							//update two rows into database
							dsTableAndGroup.Update(objAndVOPre);
							dsTableAndGroup.Update(objAndVOCur);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			return intTableOrder;
		}




		//**************************************************************************              
		///    <Description>
		///       This method uses to change down of a row in sys_table 
		///    </Description>
		///    <Inputs>
		///        TableID     
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int MoveDownTable(int pintTableID)
		{
			int intTableOrder = 0;
			try
			{
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupVO objAndVONex = new sys_TableAndGroupVO();
				sys_TableAndGroupVO objAndVOCur = new sys_TableAndGroupVO();
				ArrayList arrobjAnd = new ArrayList();

				int intGroupID  = dsTableAndGroup.GetGroupIDByTableID(pintTableID);
				arrobjAnd = dsTableAndGroup.GetObjectVOs(intGroupID);
				int intNum = arrobjAnd.Count;
				for (int i = 0; i < intNum; i++)
				{
					sys_TableAndGroupVO obj = (sys_TableAndGroupVO)arrobjAnd[i];
					if (obj.TableID == pintTableID)
					{
						int mCurTableOrder = obj.TableOrder;
						if (mCurTableOrder < intNum)
						{
							objAndVONex = (sys_TableAndGroupVO)arrobjAnd[i+1];
							objAndVOCur = (sys_TableAndGroupVO)arrobjAnd[i];
							objAndVOCur.TableOrder = objAndVONex.TableOrder;
							objAndVONex.TableOrder = mCurTableOrder;
							intTableOrder = objAndVONex.TableOrder;
							//update two rows into database
							dsTableAndGroup.Update(objAndVONex);
							dsTableAndGroup.Update(objAndVOCur);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			return intTableOrder;
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to change down of a row in sys_table 
		///    </Description>
		///    <Inputs>
		///        TableID     
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int MoveDownTable(int pintTableID, int pintTableGroupID)
		{
			int intTableOrder = 0;
			try
			{
				sys_TableAndGroupDS dsTableAndGroup = new sys_TableAndGroupDS();
				sys_TableDS dsSysTable = new sys_TableDS();
				sys_TableAndGroupVO objAndVONex = new sys_TableAndGroupVO();
				sys_TableAndGroupVO objAndVOCur = new sys_TableAndGroupVO();
				ArrayList arrobjAnd = new ArrayList();

				int intGroupID  = pintTableGroupID ; //dsTableAndGroup.GetGroupIDByTableID(pintTableID);
				arrobjAnd = dsTableAndGroup.GetObjectVOs(intGroupID);
				int intNum = arrobjAnd.Count;
				for (int i = 0; i < intNum; i++)
				{
					sys_TableAndGroupVO obj = (sys_TableAndGroupVO)arrobjAnd[i];
					if (obj.TableID == pintTableID)
					{
						int mCurTableOrder = obj.TableOrder;
						if (mCurTableOrder < intNum)
						{
							objAndVONex = (sys_TableAndGroupVO)arrobjAnd[i+1];
							objAndVOCur = (sys_TableAndGroupVO)arrobjAnd[i];
							objAndVOCur.TableOrder = objAndVONex.TableOrder;
							objAndVONex.TableOrder = mCurTableOrder;
							intTableOrder = objAndVONex.TableOrder;
							//update two rows into database
							dsTableAndGroup.Update(objAndVONex);
							dsTableAndGroup.Update(objAndVOCur);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			return intTableOrder;
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to change up order of TableGroup
		///    </Description>
		///    <Inputs>
		///        GroupID     
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int MoveUpGroup(int pintGroupID)
		{
			int intGroupOrder = 0;
			try
			{
				sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();
				sys_TableGroupVO voSysTableGroupPre = new sys_TableGroupVO();
				sys_TableGroupVO voSysTableGroupCur = new sys_TableGroupVO();
				ArrayList arrobjGroup = new ArrayList();

				arrobjGroup = dsSysTableGroup.GetObjectVOs();
				for (int i = 0; i < arrobjGroup.Count; i++)
				{
					sys_TableGroupVO voSysTableGroup = (sys_TableGroupVO)arrobjGroup[i];
					int intGroupID = voSysTableGroup.TableGroupID;
					if (intGroupID.Equals(pintGroupID))
					{
						int intCurGroupOrder = voSysTableGroup.GroupOrder;
						if (intCurGroupOrder > 1)
						{
							voSysTableGroupPre = (sys_TableGroupVO)arrobjGroup[i-1];
							voSysTableGroupCur = (sys_TableGroupVO)arrobjGroup[i];
							voSysTableGroupCur.GroupOrder = voSysTableGroupPre.GroupOrder;
							voSysTableGroupPre.GroupOrder = intCurGroupOrder;
							intGroupOrder = voSysTableGroupPre.GroupOrder;
							//update two rows into database
							dsSysTableGroup.Update(voSysTableGroupPre);
							dsSysTableGroup.Update(voSysTableGroupCur);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			return intGroupOrder;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to change down order of TableGroup
		///    </Description>
		///    <Inputs>
		///        GroupID     
		///    </Inputs>
		///    <Outputs>
		///      changes
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       29-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int MoveDownGroup(int pintGroupID)
		{
			int intGroupOrder = 0;
			try
			{
				sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();
				sys_TableGroupVO voSysTableGroupNex = new sys_TableGroupVO();
				sys_TableGroupVO voSysTableGroupCur = new sys_TableGroupVO();
				ArrayList arrobjGroup = new ArrayList();

				arrobjGroup = dsSysTableGroup.GetObjectVOs();
				int intNum = arrobjGroup.Count; 
				for (int i = 0; i < intNum; i++)
				{
					sys_TableGroupVO voSysTableGroup = (sys_TableGroupVO)arrobjGroup[i];
					int intGroupID = voSysTableGroup.TableGroupID;
					if (intGroupID.Equals(pintGroupID))
					{
						int intCurGroupOrder = voSysTableGroup.GroupOrder;
						if (intCurGroupOrder < intNum)
						{
							voSysTableGroupNex = (sys_TableGroupVO)arrobjGroup[i+1];
							voSysTableGroupCur = (sys_TableGroupVO)arrobjGroup[i];
							voSysTableGroupCur.GroupOrder = voSysTableGroupNex.GroupOrder;
							voSysTableGroupNex.GroupOrder = intCurGroupOrder;
							intGroupOrder = voSysTableGroupNex.GroupOrder;
							//update two rows into database
							dsSysTableGroup.Update(voSysTableGroupNex);
							dsSysTableGroup.Update(voSysTableGroupCur);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw (ex);
			}
			return intGroupOrder;
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to check order of Table
		///    </Description>
		///    <Inputs>
		///        GroupID,TableID    
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void TablePosition(int pintGroupID,int pintTableOrder,ref bool blnMin,ref bool blnMax)
		{
			sys_TableDS dsSysTable = new sys_TableDS();
			int intMaxTableOrder = dsSysTable.MaxTableOrder(pintGroupID);
			int intMinTableOrder = dsSysTable.MinTableOrder(pintGroupID);
			if ((pintTableOrder < intMaxTableOrder)&&(pintTableOrder > intMinTableOrder))
			{
				blnMin = false;
				blnMax = false;
			}
			else if (pintTableOrder.Equals(intMinTableOrder))
				blnMin = true;
			else if (pintTableOrder.Equals(intMaxTableOrder))
				blnMax = true;
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to check order of TableGroup
		///    </Description>
		///    <Inputs>
		///        GroupID     
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public void GroupPosition(int pintGroupOrder,ref bool blnMin,ref bool blnMax)
		{
			sys_TableGroupDS dsSysTableGroup = new sys_TableGroupDS();
			int intMaxGroupOrder = dsSysTableGroup.MaxGroupOrder();
			int intMinGroupOrder = dsSysTableGroup.MinGroupOrder();
			if ((pintGroupOrder < intMaxGroupOrder)&&(pintGroupOrder > intMinGroupOrder))
			{
				blnMin = false;
				blnMax = false;
			}
			else if (pintGroupOrder.Equals(intMinGroupOrder))
				blnMin = true;
			else if (pintGroupOrder.Equals(intMaxGroupOrder))
				blnMax = true;
		}
		//**************************************************************************              
		///    <Description>
		///       This method uses to get the position of a table in a specific group
		///    </Description>
		///    <Inputs>
		///        GroupID,TableID    
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public int GetTablePositionInGroup(int pintGroupID,int pintTableId)
		{
			try 
			{
				sys_TableAndGroupDS objSysTableAndGroupDS = new sys_TableAndGroupDS();
				return objSysTableAndGroupDS.GetTablePositionInGroup(pintGroupID,pintTableId);
			}
			catch (PCSDBException ex) 
			{
				throw ex;
			}
		}
		#region ITableManagementBO Members
		//**************************************************************************              
		///    <Description>
		///       This method uses to copy a table into a specific group
		///    </Description>
		///    <Inputs>
		///        GroupID,TableID    
		///    </Inputs>
		///    <Outputs>
		///      
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///       30-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void CopyTable(int pintTableId, int pintGroupID)
		{
			// TODO:  Add TableManagementBO.CopyTable implementation
			const string METHOD_NAME = THIS + ".CopyTable()";
			try
			{
				//Check to see if this table is already existed in this group
				//Initialize the DS class
				sys_TableAndGroupDS objSys_TableAndGroupDS = new sys_TableAndGroupDS();
				if (objSys_TableAndGroupDS.CheckIfTableExisted(pintTableId,pintGroupID))
				{
					throw new PCSBOException(ErrorCode.MESSAGE_TABLEMANAGEMENT_DUPLICATE_TABLE,METHOD_NAME,null);
				}
				

				//Initialize the VO class
				sys_TableAndGroupVO objSysTableAndGroupVO = new sys_TableAndGroupVO();

				objSysTableAndGroupVO.TableGroupID = pintGroupID;
				objSysTableAndGroupVO.TableID = pintTableId;
				objSysTableAndGroupVO.TableOrder = objSys_TableAndGroupDS.GetMaxTableOrder(pintGroupID);
				
				//Save this record into database
				objSys_TableAndGroupDS.Add(objSysTableAndGroupVO);

				
			}catch (PCSBOException ex)
			{
				
				throw ex;
			}
			catch (PCSDBException ex)
			{
				
				throw ex.CauseException;
			}
			catch (Exception ex)
			{
				
				throw ex;
			}
		}

		#endregion
	}
}

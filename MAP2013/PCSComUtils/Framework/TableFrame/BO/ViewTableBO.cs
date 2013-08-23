using System;
using System.Data;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.TableFrame.BO
{
	public class ViewTableBO
	{
		
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.sysTableGroupDS";
		public int GetMaxValue (string pstrTableName, string pstrColumnName) 
		{
			try 
			{
				sysGenerateTableFormDS objSysGenerateTableFormDS = new sysGenerateTableFormDS();
				return objSysGenerateTableFormDS.GetMaxValue(pstrTableName,pstrColumnName) + 1;
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
		public string BuildSQLSelect(DataSet pdstFieldList,string pstrTableName, bool pblnForEdit) 
		{
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				string strSqlSelectCommand = objGenerateTableFormDS.BuildSQLSelect(pdstFieldList,pstrTableName,pblnForEdit);
				return strSqlSelectCommand;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		}
		public DataRow GetFieldLength(DataSet pdstFieldList, string pstrTableName)
		{
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				return objGenerateTableFormDS.GetFieldLength(pdstFieldList,pstrTableName);
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
		public DataSet getFieldList(int  pintTableID) 
		{
			
			sys_TableFieldDS objTableFieldDS = new sys_TableFieldDS();
			try 
			{
				DataSet dstFieldList = objTableFieldDS.List(pintTableID);
				return dstFieldList;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		} 
		public DataSet getFieldList(string  pstrTableOrViewName) 
		{
			
			sys_TableFieldDS objTableFieldDS = new sys_TableFieldDS();
			try 
			{
				DataSet dstFieldList = objTableFieldDS.List(pstrTableOrViewName);
				return dstFieldList;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		} 
		public DataSet getDataListForUpdate(string pstrSql, string pstrTableName) 
		{
			
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				DataSet dstData = objGenerateTableFormDS.List(pstrSql,pstrTableName);
				return dstData;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		} 
		public DataSet getDataList(string pstrSql, string pstrTableName) 
		{
			
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				DataSet dstData = objGenerateTableFormDS.List(pstrSql,pstrTableName);
				return dstData;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		} 
		public DataSet getDataListForComboBox (string strSQL, string strTableName) 
		{
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				DataSet dstData = objGenerateTableFormDS.List(strSQL,strTableName);
				return dstData;
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		}
		public string BuildSqlForTrueDBDropDown (string strFromTable, string strFromField, string strFilterField1, string strFilterField2) 
		{
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				return objGenerateTableFormDS.BuildSqlForTrueDBDropDown(strFromTable, strFromField, strFilterField1, strFilterField2);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		}
		public void UpdateDataSet(DataSet dstData)
		{
			/*
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				objGenerateTableFormDS.UpdateDataSet(dstData,strSqlSelectCommand);
				
			}
			catch(PCSDBException ex)
			{
				
				throw ex;
			}
			*/

		}

		public void UpdateDataSetViewTable(DataSet pdstData,string pstrSqlSelect, bool pblnForEdit,string pstrSqlSelectUpdate)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSetViewTable()";
			try 
			{
				sysGenerateTableFormDS objGenerateTableFormDS = new sysGenerateTableFormDS();
				objGenerateTableFormDS.UpdateDataSet(pdstData,pstrSqlSelect,pblnForEdit,pstrSqlSelectUpdate);
				//
			}
			catch(PCSDBException ex)
			{
				try 
				{
					
				}
				catch (System.Runtime.InteropServices.COMException exCom)
				{
					throw new PCSBOException(ErrorCode.MESSAGE_COM_TRANSACTION,METHOD_NAME,exCom);
				}
				throw ex;
			}
			catch (System.Runtime.InteropServices.COMException ex) 
			{
				
				throw new PCSBOException(ErrorCode.MESSAGE_COM_TRANSACTION,METHOD_NAME,ex);
			}
		}

		public bool CheckDataSet(DataSet dstData) 
		{
			return true;
		}
		public int GetTableID(string pstrTableName) 
		{
			try 
			{
				sys_TableDS objsysTableDS = new sys_TableDS();
				return objsysTableDS.GetTableID(pstrTableName);
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		}

		public int GetTotalColumnWidth (string pstrTableName)
		{
			try 
			{
				sys_TableDS objsysTableDS = new sys_TableDS();
				int intTableID = objsysTableDS.GetTableID(pstrTableName);
				if (intTableID > 0)
				{
					sys_TableFieldDS objsys_TableFieldDS = new sys_TableFieldDS();
					return objsys_TableFieldDS.GetTotalColumnWidth(intTableID);
				}
				else
				{
					return intTableID;
				}
			}
			catch(PCSDBException ex)
			{
				throw ex;
			}
		}
	
		public void Add(object pObjectDetail)
		{
		}
	
		public void Delete(object pObjectVO)
		{
			// TODO:  Add ViewTableBO.Delete implementation

		}
	
		public object GetObjectVO(int pintID,string VOclass)
		{
			try
			{
				sys_TableDS dsSysTable = new sys_TableDS();
				return dsSysTable.GetObjectVO(pintID);
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
	
		public DataSet List(string strCondition, string strFieldList)
		{
			// TODO:  Add ViewTableBO.List implementation
			return null;
		}
	
		public void Update(object pObjectDetail)
		{
			// TODO:  Add ViewTableBO.Update implementation

		}
		public DataTable SearchValueForButtonColumn(string[] pstrColumnInfor, string pstrValue)
		{
			//strArrayValue[0] - From TableName
			//strArrayValue[1] - Value Field
			//strArrayValue[2] - Display Field 1
			//strArrayValue[3] - Display Field 2
			//strArrayValue[4] - Ogininal Column Name

			try
			{
				sysGenerateTableFormDS objsysGenerateTableFormDS = new sysGenerateTableFormDS();
				return objsysGenerateTableFormDS.SearchValueForButtonColumn(pstrColumnInfor,pstrValue);
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
	}
}

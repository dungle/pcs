using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;

namespace PCSComUtils.Framework.TableFrame.DS
{
	/// <summary>
	/// Summary description for sysGenerateTableForm.
	/// </summary>
	public class sysGenerateTableFormDS  
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.DS.sysTableGroupDS";
		public void UpdateDataSet(DataSet pData, string strSql, bool blnForEdit, string strSqlEdit)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				DataAccess.Utils utils =	new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				oconPCS.Open();
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				
				//string strXX1 = odcbPCS.GetDeleteCommand().CommandText;
				//string strXX2 = odcbPCS.GetUpdateCommand().CommandText;
				//string strXX3 = odcbPCS.GetInsertCommand().CommandText;

				pData.EnforceConstraints = false;
				
				odadPCS.Update(pData,pData.Tables[0].TableName);

				pData.Clear();
				if (blnForEdit)
				{
					odadPCS.SelectCommand.CommandText = strSqlEdit;
					odadPCS.SelectCommand.CommandType = CommandType.Text;
				}
				odadPCS.Fill(pData,pData.Tables[0].TableName);
				pData.AcceptChanges();
			}
			catch(OleDbException ex)
			{
                if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					||ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE )
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{

					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLDBNULL_VIALATION_KEYCODE)
				{
					//515 - Null data vialation
					throw new PCSDBException(ErrorCode.DBNULL_VIALATION, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
			}			

			catch(InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}

		}

		public string BuildSQLSelect(DataSet dstFieldList, string strTableName, bool blnForEdit) 
		{
			if (dstFieldList==null) 
			{
				return String.Empty;
			}
			//build the select command
			string strSql ="SELECT ";
			string strSqlFrom = " FROM " + strTableName.Trim();
			bool blnFirstTime = true;
			string strTableAlias = String.Empty;
			int intFromTable = 0;
            foreach (DataRow drTableField in dstFieldList.Tables[0].Rows)
            {
                string strFieldName = drTableField[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim();

                #region Create Select fields

                if (blnFirstTime)
                {
                    strSql += strTableName + "." + strFieldName;
                    blnFirstTime = false;
                }
                else
                {
                    strSql += "," + strTableName + "." + strFieldName;
                }

                #endregion

                #region //In case of Edit , and this field value will get from another table
                //We have to join two table and get another field name for this
                if (blnForEdit && drTableField[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() != String.Empty)
                {
                    intFromTable++;
                    strTableAlias = drTableField[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() + intFromTable.ToString();
                    // Add field 1 select
                    strSql += "," + strTableAlias.Trim() + "." + drTableField[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim() + " as "
                        + strFieldName + "_" + drTableField[sys_TableFieldTable.FILTERFIELD1_FLD].ToString().Trim();
                    // Add field 2 select
                    if (drTableField[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() != String.Empty)
                    {
                        strSql += "," + strTableAlias + "." + drTableField[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim() + " as "
                            + strFieldName + "_" + drTableField[sys_TableFieldTable.FILTERFIELD2_FLD].ToString().Trim();
                    }
                    // Add field 3 select
                    if (drTableField[sys_TableFieldTable.FILTERFIELD3_FLD].ToString().Trim() != String.Empty)
                    {
                        strSql += "," + strTableAlias + "." + drTableField[sys_TableFieldTable.FILTERFIELD3_FLD].ToString().Trim() + " as "
                            + strFieldName + "_" + drTableField[sys_TableFieldTable.FILTERFIELD3_FLD].ToString().Trim();
                    }

                    //Set the JOINS command between two tables
                    strSqlFrom += " left join "
                        + drTableField[sys_TableFieldTable.FROMTABLE_FLD].ToString().Trim() + " " + strTableAlias
                        + " on " + strTableName + "." + strFieldName + "=" + strTableAlias + "." + drTableField[sys_TableFieldTable.FROMFIELD_FLD].ToString().Trim();
                }

                #endregion
            }
            strSql += strSqlFrom;
            return strSql;
		}
		public DataTable SearchValueForButtonColumn(string[] pstrColumnInfor, string pstrValue)
		{
			//strArrayValue[0] - From TableName
			//strArrayValue[1] - Value Field
			//strArrayValue[2] - Display Field 1
			//strArrayValue[3] - Display Field 2
			//strArrayValue[4] - Ogininal Column Name
			//strArrayValue[5] - Original filter column

			const string METHOD_NAME = THIS + ".SearchValueForButtonColumn()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSqlStatement = String.Empty;
				string strTableName = pstrColumnInfor[0];

				//build the SELECT Statement
				strSqlStatement = "SELECT " + pstrColumnInfor[1] + "," + pstrColumnInfor[2];
				if (pstrColumnInfor[3] != String.Empty)
				{
					strSqlStatement += "," + pstrColumnInfor[3];
				}
				strSqlStatement += " FROM " + pstrColumnInfor[0];
				strSqlStatement += " WHERE " + pstrColumnInfor[5]  + "='" +  pstrValue.Replace("'","''") + "'";

				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSqlStatement, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,strTableName);
				return dstPCS.Tables[0];

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}

		}

		public DataSet List (string strSqlStatement, string strTableName) 
		{
			const string METHOD_NAME = THIS + ".List()";
			const string BASE_TABLE = "BASE TABLE";
			DataSet dstPCS = new DataSet();
			


			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				strSqlStatement += "; SELECT TABLE_TYPE from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + strTableName + "'";
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSqlStatement, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,strTableName);
				if(dstPCS.Tables[1].Rows[0]["TABLE_TYPE"].ToString().ToUpper().Trim() == BASE_TABLE)
				{
					odadPCS.FillSchema(dstPCS.Tables[strTableName], SchemaType.Mapped);
				}
				return dstPCS;

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}

		}
		public int GetMaxValue (string pstrTableName, string pstrColumnName) 
		{
			const string METHOD_NAME = THIS + ".GetMaxValue()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = String.Empty ;
			strSql = " SELECT isnull(max( " + pstrColumnName  + "),0) " +
				     " FROM " + pstrTableName;
			try 
			{
				DataAccess.Utils utils = new DataAccess.Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				string strMaxValue = ocmdPCS.ExecuteScalar().ToString();

				try 
				{
					return int.Parse(strMaxValue);
				}
				catch
				{
					return 0;
				}
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}

		}

		public string BuildSqlForTrueDBDropDown (string strFromTable, string strFromField, string strFilterField1, string strFilterField2)
		{
			const string METHOD_NAME = THIS + ".BuildSqlForTrueDBDropDown()";
			string strSqL;
			bool blnHasField=false;
			strSqL = "SELECT " ;

			try 
			{
				if (strFilterField1!=String.Empty && !strFromField.Equals(strFilterField1) ) 
				{
					strSqL += strFilterField1;
					blnHasField = true;
				}
				if (strFilterField2!=null && strFilterField2 !=String.Empty) 
				{
					if (blnHasField) 
					{
						strSqL += "," + strFilterField2;
					}
					else 
					{
						strSqL += strFilterField2;
					}
					blnHasField = true;
				}
				if (blnHasField) 
				{
					strSqL += "," + strFromField + " From " + strFromTable;
				}
				else 
				{
					strSqL += strFromField + " From " + strFromTable;

				}
			}
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			return strSqL;
		}
		public DataRow GetFieldLength(DataSet dstFieldList, string strTableName) 
		{
			const string METHOD_NAME = THIS + ".GetFieldLength()";
			if (dstFieldList==null) 
			{
				return null;
			}
			//build the select command
			string strSql ="SELECT ";
			bool blnFirstTime = true;

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;

			DataSet dstPCS = new DataSet();
			try 
			{
				foreach (DataRow drTableField in dstFieldList.Tables[0].Rows) 
				{
					string strFieldName = drTableField[sys_TableFieldTable.FIELDNAME_FLD].ToString().Trim();
					if (blnFirstTime ) 
					{
						strSql += " COLUMNPROPERTY( OBJECT_ID('" + strTableName + "'),'" + strFieldName + "','" + Constants.GET_FIELD_LENGTH +"')  as " + strFieldName;
						blnFirstTime = false;
					}
					else 
					{
						strSql += ", COLUMNPROPERTY( OBJECT_ID('" + strTableName + "'),'" + strFieldName + "','" + Constants.GET_FIELD_LENGTH +"')  as " + strFieldName;
					}
				}
				Utils utils = new Utils();

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,strTableName);

				return dstPCS.Tables[0].Rows[0];

			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch (Exception ex) 
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally 
			{
				if (oconPCS!=null) 
				{
					if (oconPCS.State != ConnectionState.Closed) 
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// if pstrTableName existed in sys_RecordSecurityParam then return where condition
		/// else return string.empty
		/// </summary>
		/// <param name="pstrTableName"></param>
		/// <returns></returns>
		/// <author>SonHT 2005-11-30</author>
		public string GetConditionByRecord(string pstrUserName, string pstrTableName)
		{
			const string COLUMN_NAME = "COLUMN_NAME";
			if (pstrTableName == string.Empty)
			{
				return string.Empty;
			}
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			DataSet dstPCS = new DataSet();
			try
			{
				// Get security table
				string strSql = "Select TableOrView,SourceTableName, SecurityTableName from (  \n"
					+ " Select SourceTableName TableOrView,SourceTableName, SecurityTableName from sys_RecordSecurityParam  \n"
					+ " union  \n"
					+ " select TableOrView,SourceTableName, SecurityTableName from sys_RecordSecurityParam rs  \n"
					+ " inner join sys_RelatedView rv on rs.RecordSecurityParamID=rv.RecordSecurityParamID  \n"
					+ ") X Where TableOrView = '" + pstrTableName + "'  \n";
				// Get primary key
				strSql += "; DECLARE @SourceTableName varchar(50)  \n"
					+ " SET @SourceTableName = (Select Distinct SourceTableName  \n"
					+ " from(  \n"
					+ " 	select SourceTableName TableOrView,SourceTableName, SecurityTableName from sys_RecordSecurityParam  \n"
					+ " 	union  \n"
					+ " 	select TableOrView,SourceTableName, SecurityTableName from sys_RecordSecurityParam rs  \n"
					+ " 	inner join sys_RelatedView rv on rs.RecordSecurityParamID=rv.RecordSecurityParamID  \n"
					+ " ) X  \n"
					+ " where TableOrView='" + pstrTableName + "')  \n"
					+ "; EXEC sp_pkeys @table_name = @SourceTableName;  \n";

				// Get MapField
				strSql += " SELECT MapField FROM sys_RelatedView WHERE TableOrView = '" + pstrTableName + "' \n";

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				if (dstPCS.Tables[1].Rows.Count == 0)
				{
					return string.Empty;
				}
				else if (dstPCS.Tables[0].Rows.Count == 0)
				{
					return string.Empty;
				}
				else
				{
					string strSecurityTableName = dstPCS.Tables[0].Rows[0][sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD].ToString();

					string strPrimaryKey = dstPCS.Tables[1].Rows[0][COLUMN_NAME].ToString();
					string strMapField = strPrimaryKey;
					if(dstPCS.Tables[2].Rows.Count > 0)
					{
						if(dstPCS.Tables[2].Rows[0]["MapField"] != DBNull.Value)
						{
							strMapField = dstPCS.Tables[2].Rows[0]["MapField"].ToString();
						}
					}

					string strRightByRecord = // Where
						" AND " + pstrTableName + "." + strMapField + "  Not In("
							+ "	Select " + strPrimaryKey
							+ "	From " + strSecurityTableName
							+ "	Where RoleID IN("
							+ "		Select RoleID from sys_UserToRole UR"
							+ "		Inner Join Sys_User U on UR.UserID=U.UserID And UserName='" + pstrUserName + "'"
							+ "	)"
							+ ")";
					return strRightByRecord;
				}

			}
			catch
			{
				return string.Empty;
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public void Add(Object pobjObjectVO)
		{
		}

		public void Delete(int pintID)
		{
			// TODO:  Add sysGenerateTableFormDS.Delete implementation

		}

		public Object GetObjectVO(int pintID)
		{
			// TODO:  Add sysGenerateTableFormDS.GetObjectVO implementation
			return null;
		}

		public DataSet List()
		{
			// TODO:  Add sysGenerateTableFormDS.List implementation
			return null;
		}

		public void UpdateDataSet(DataSet pData)
		{
		}

		public void Update(Object pobjObjecVO)
		{
			// TODO:  Add sysGenerateTableFormDS.Update implementation

		}
	}
}

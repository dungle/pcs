using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Admin.DS
{
	public class sys_RolePartyDS 
	{

		public sys_RolePartyDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.sys_RolePartyDS";
		string strPKColumnName, strFKColumnName;
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_RoleParty
		///    </Description>
		///    <Inputs>
		///        sys_RolePartyVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, November 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************


		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				sys_RolePartyVO objObject = (sys_RolePartyVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO sys_RoleParty("
				+ sys_RolePartyTable.ROLEID_FLD + ","
				+ sys_RolePartyTable.PARTYID_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RolePartyTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_RolePartyTable.ROLEID_FLD].Value = objObject.RoleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RolePartyTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_RolePartyTable.PARTYID_FLD].Value = objObject.PartyID;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
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
	
		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from sys_RoleParty
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + sys_RolePartyTable.TABLE_NAME + " WHERE  " + "RolePartyID" + "=" + pintID.ToString();
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
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
		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from sys_RoleParty
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_RolePartyVO
		///    </Outputs>
		///    <Returns>
		///       sys_RolePartyVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, November 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
				+ sys_RolePartyTable.ROLEPARTYID_FLD + ","
				+ sys_RolePartyTable.ROLEID_FLD + ","
				+ sys_RolePartyTable.PARTYID_FLD
				+ " FROM " + sys_RolePartyTable.TABLE_NAME
				+" WHERE " + sys_RolePartyTable.ROLEPARTYID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_RolePartyVO objObject = new sys_RolePartyVO();

				while (odrPCS.Read())
				{ 
				objObject.RolePartyID = int.Parse(odrPCS[sys_RolePartyTable.ROLEPARTYID_FLD].ToString().Trim());
				objObject.RoleID = int.Parse(odrPCS[sys_RolePartyTable.ROLEID_FLD].ToString().Trim());
				objObject.PartyID = int.Parse(odrPCS[sys_RolePartyTable.PARTYID_FLD].ToString().Trim());

				}		
				return objObject;					
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to sys_RoleParty
		///    </Description>
		///    <Inputs>
		///       sys_RolePartyVO       
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
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
	
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			sys_RolePartyVO objObject = (sys_RolePartyVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE sys_RoleParty SET "
				+ sys_RolePartyTable.ROLEID_FLD + "=   ?" + ","
				+ sys_RolePartyTable.PARTYID_FLD + "=  ?"
				+" WHERE " + sys_RolePartyTable.ROLEPARTYID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RolePartyTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_RolePartyTable.ROLEID_FLD].Value = objObject.RoleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RolePartyTable.PARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_RolePartyTable.PARTYID_FLD].Value = objObject.PartyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RolePartyTable.ROLEPARTYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_RolePartyTable.ROLEPARTYID_FLD].Value = objObject.RolePartyID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
																   
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from sys_RoleParty
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
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, November 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
				+ sys_RolePartyTable.ROLEPARTYID_FLD + ","
				+ sys_RolePartyTable.ROLEID_FLD + ","
				+ sys_RolePartyTable.PARTYID_FLD
					+ " FROM " + sys_RolePartyTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_RolePartyTable.TABLE_NAME);

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
		/// <summary>
		/// GetDataByRoleID
		/// </summary>
		/// <param name="pintRoleID"></param>
		/// <param name="pstrSecurityTableName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 17 2005</date>
		public DataSet GetDataByRoleID(int pintRoleID, string pstrSecurityTableName, string pstrSourceTableName)
		{
			const string METHOD_NAME = THIS + ".GetDataByRoleID()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				CommonBO boCommon = new CommonBO();
				strPKColumnName = boCommon.GetPKColumnName(pstrSecurityTableName);
				strFKColumnName = boCommon.GetFKColumnName(pstrSourceTableName, pstrSecurityTableName);
				strSql=	"SELECT "
					+ strPKColumnName + ","
					+ sys_RolePartyTable.ROLEID_FLD + ","
					+ strFKColumnName
					+ " FROM " + pstrSecurityTableName
					+ " WHERE " + sys_RolePartyTable.ROLEID_FLD + " = " + pintRoleID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, pstrSecurityTableName);

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
		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
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
		///       Wednesday, November 16, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
				+ sys_RolePartyTable.ROLEPARTYID_FLD + ","
				+ sys_RolePartyTable.ROLEID_FLD + ","
				+ sys_RolePartyTable.PARTYID_FLD 
		+ "  FROM " + sys_RolePartyTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,sys_RolePartyTable.TABLE_NAME);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
		/// <summary>
		/// UpdateSecurityTable
		/// </summary>
		/// <param name="pdstSecurityTable"></param>
		/// <param name="pstrSecurityTableName"></param>
		/// <author>Trada</author>
		/// <date>Friday, Nov 18 2005</date>
		public void UpdateSecurityTable(DataSet pdstSecurityTable, string pstrSecurityTableName, string pstrSourceTableName)
		{
			const string METHOD_NAME = THIS + ".UpdateSecurityTable()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				CommonBO boCommon = new CommonBO();
				//strPKColumnName = boCommon.GetPKColumnName(pstrSecurityTableName);
				//strFKColumnName = boCommon.GetFKColumnName(pstrSourceTableName, pstrSecurityTableName);
				const string FKTABLE_NAME = "FKTABLE_NAME";
				const string FKCOLUMN_NAME = "FKCOLUMN_NAME";
				const string PKCOLUMN_NAME = "COLUMN_NAME";
				// HACK: Trada 12-12-2005 Get FK Column Name
				DataTable dtbFKTable = GetFKColumnName(pstrSourceTableName);
				foreach (DataRow drow in dtbFKTable.Rows)
				{
					if (drow[FKTABLE_NAME].ToString() == pstrSecurityTableName)
					{
						strFKColumnName = drow[FKCOLUMN_NAME].ToString();
					}
				} 
				// END: Trada 12-12-2005
				dtbFKTable = GetPKColumnName(pstrSecurityTableName);
				if (dtbFKTable.Rows.Count == 0)
				{
					strPKColumnName = string.Empty;
				}
				else
				{
					strPKColumnName = dtbFKTable.Rows[0][PKCOLUMN_NAME].ToString();
				}
					strSql=	"SELECT "
					+ strPKColumnName + ","
					+ sys_RolePartyTable.ROLEID_FLD + ","
					+ strFKColumnName
					+ "  FROM " + pstrSecurityTableName;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstSecurityTable.EnforceConstraints = false;
				odadPCS.Update(pdstSecurityTable, pstrSecurityTableName);

			}
			catch(OleDbException ex)
			{
				if(ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
					}
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
					{
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
					}
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
		/// <summary>
		/// GetFKColumnName
		/// </summary>
		/// <param name="pstrPKTable_Name"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 17 2005</date>
		public DataTable GetFKColumnName(string pstrPKTable_Name)
		{
			const string METHOD_NAME = THIS + ".GetFKColumnName()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"EXEC sp_fkeys @pktable_name = N'" + pstrPKTable_Name + "'"; 
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_RolePartyTable.TABLE_NAME);

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
		/// <summary>
		/// GetPKColumnName
		/// </summary>
		/// <param name="pstrPKTable_Name"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, Nov 17 2005</date>
		public DataTable GetPKColumnName(string pstrPKTable_Name)
		{
			const string METHOD_NAME = THIS + ".GetPKColumnName()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"EXEC sp_pkeys @table_name = N'" + pstrPKTable_Name + "'"; 
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_RolePartyTable.TABLE_NAME);

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
	}
}

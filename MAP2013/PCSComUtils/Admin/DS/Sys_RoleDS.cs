using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class Sys_RoleDS 
	{
		public Sys_RoleDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.DS.Sys_RoleDS";
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_Role
		///    </Description>
		///    <Inputs>
		///        Sys_RoleVO       
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
		///       Thursday, January 06, 2005
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
				Sys_RoleVO objObject = (Sys_RoleVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_Role("
				+ Sys_RoleTable.NAME_FLD + ","
				+ Sys_RoleTable.DESCRIPTION_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RoleTable.NAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_RoleTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RoleTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_RoleTable.DESCRIPTION_FLD].Value = objObject.Description;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
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
		///       This method uses to delete data from Sys_Role
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
			strSql=	"DELETE " + Sys_RoleTable.TABLE_NAME + " WHERE  " + "RoleID" + "=" + pintID.ToString();
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
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
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
		///       This method uses to get data from Sys_Role
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_RoleVO
		///    </Outputs>
		///    <Returns>
		///       Sys_RoleVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Thursday, January 06, 2005
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
				+ Sys_RoleTable.ROLEID_FLD + ","
				+ Sys_RoleTable.NAME_FLD + ","
				+ Sys_RoleTable.DESCRIPTION_FLD
				+ " FROM " + Sys_RoleTable.TABLE_NAME
				+" WHERE " + Sys_RoleTable.ROLEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_RoleVO objObject = new Sys_RoleVO();

				while (odrPCS.Read())
				{ 
				objObject.RoleID = int.Parse(odrPCS[Sys_RoleTable.ROLEID_FLD].ToString());
				objObject.Name = odrPCS[Sys_RoleTable.NAME_FLD].ToString();
				objObject.Description = odrPCS[Sys_RoleTable.DESCRIPTION_FLD].ToString();

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
		///       This method uses to update data to Sys_Role
		///    </Description>
		///    <Inputs>
		///       Sys_RoleVO       
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

			Sys_RoleVO objObject = (Sys_RoleVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_Role SET "
				+ Sys_RoleTable.NAME_FLD + "=   ?" + ","
				+ Sys_RoleTable.DESCRIPTION_FLD + "=  ?"
				+" WHERE " + Sys_RoleTable.ROLEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RoleTable.NAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_RoleTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RoleTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_RoleTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_RoleTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_RoleTable.ROLEID_FLD].Value = objObject.RoleID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
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
		///       This method uses to get all data from Sys_Role
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
		///       Thursday, January 06, 2005
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
				+ Sys_RoleTable.ROLEID_FLD + ","
				+ Sys_RoleTable.NAME_FLD + ","
				+ Sys_RoleTable.DESCRIPTION_FLD
				+ " FROM " + Sys_RoleTable.TABLE_NAME
				+ " WHERE Name<>'" + Constants.ALL_ROLE + "'"
				+ " ORDER BY " + Sys_RoleTable.NAME_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RoleTable.TABLE_NAME);

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
		///       This method uses to get all data from Sys_Role
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
		///       Thursday, January 06, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet ListAll()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
		
	

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
			
				strSql=	"SELECT "
					+ Sys_RoleTable.ROLEID_FLD + ","
					+ Sys_RoleTable.NAME_FLD + ","
					+ Sys_RoleTable.DESCRIPTION_FLD
					+ " FROM " + Sys_RoleTable.TABLE_NAME
					+ " ORDER BY " + Sys_RoleTable.NAME_FLD;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RoleTable.TABLE_NAME);

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
		///       Return the length of the columns in table
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
		///       Thursday, January 06, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataRow GetFieldLength()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT COLUMNPROPERTY( OBJECT_ID('" + Sys_RoleTable.TABLE_NAME + "'),'" + Sys_RoleTable.ROLEID_FLD + "','" + Constants.GET_FIELD_LENGTH +"')  as " + Sys_RoleTable.ROLEID_FLD
					+ ", COLUMNPROPERTY( OBJECT_ID('" + Sys_RoleTable.TABLE_NAME + "'),'" + Sys_RoleTable.NAME_FLD + "','" + Constants.GET_FIELD_LENGTH + "')  as " + Sys_RoleTable.NAME_FLD
					+ ", COLUMNPROPERTY( OBJECT_ID('" + Sys_RoleTable.TABLE_NAME + "'),'" + Sys_RoleTable.DESCRIPTION_FLD + "','" + Constants.GET_FIELD_LENGTH +"')  as " + Sys_RoleTable.DESCRIPTION_FLD;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RoleTable.TABLE_NAME);

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
		///       Thursday, January 06, 2005
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
			DataSet dstChanged = pData.GetChanges();
			try
			{
				strSql=	"SELECT "
				+ Sys_RoleTable.ROLEID_FLD + ","
				+ Sys_RoleTable.NAME_FLD + ","
				+ Sys_RoleTable.DESCRIPTION_FLD 
				+ "  FROM " + Sys_RoleTable.TABLE_NAME
				+ " WHERE Name<>'" + Constants.ALL_ROLE + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				oconPCS.Open();
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;

				//odadPCS.Update(pData,Sys_RoleTable.TABLE_NAME);
				odadPCS.Update(dstChanged,Sys_RoleTable.TABLE_NAME);
				
				pData.Clear();
				odadPCS.Fill(pData,pData.Tables[0].TableName);
				pData.AcceptChanges();
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{																   
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);				
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);				
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
		///       This method uses to get all role but not grant to any user
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
		///       Thursday, January 06, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet ListRoleNotGrantToUser(int intUserId)
		{
			const string METHOD_NAME = THIS + ".ListRoleNotGrantToUser()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_RoleTable.ROLEID_FLD + ","
					+ Sys_RoleTable.NAME_FLD + ","
					+ Sys_RoleTable.DESCRIPTION_FLD
					+ " FROM " + Sys_RoleTable.TABLE_NAME
					+ " WHERE " + Sys_RoleTable.ROLEID_FLD + " NOT IN (" 
					+ " SELECT " + Sys_UserToRoleTable.ROLEID_FLD 
					+ " FROM  " + Sys_UserToRoleTable.TABLE_NAME 
					+ " WHERE " + Sys_UserToRoleTable.USERID_FLD + " = " + intUserId + ") AND Name<>'" + Constants.ALL_ROLE + "'"; 
					
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RoleTable.TABLE_NAME);

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
		/// ListRoleAndSysParam
		/// </summary>
		/// <param name="pintUserID"></param>
		/// <param name="pstrUserName"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Wednesday, December 14 2005</date>
		public DataSet ListRoleAndSysParam(int pintUserID, string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".ListRoleAndSysParam()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_RoleTable.ROLEID_FLD + ","
					+ Sys_RoleTable.NAME_FLD + ","
					+ Sys_RoleTable.DESCRIPTION_FLD
					+ " FROM " + Sys_RoleTable.TABLE_NAME
					+ " WHERE " + Sys_RoleTable.ROLEID_FLD + " IN (" 
					+ " SELECT " + Sys_UserToRoleTable.ROLEID_FLD 
					+ " FROM  " + Sys_UserToRoleTable.TABLE_NAME 
					+ " WHERE " + Sys_UserToRoleTable.USERID_FLD + " = " + pintUserID + ")"; 
				strSql += "; SELECT "
					+ " CCN." + MST_CCNTable.CCNID_FLD + ","
					+ " CCN." + MST_CCNTable.CODE_FLD + ","
					+ " CCN." + MST_CCNTable.DESCRIPTION_FLD + ","
					+ " CCN." + MST_CCNTable.NAME_FLD + ","
					+ " CCN." + MST_CCNTable.STATE_FLD + ","
					+ " CCN." + MST_CCNTable.ZIPCODE_FLD + ","
					+ " CCN." + MST_CCNTable.PHONE_FLD + ","
					+ " CCN." + MST_CCNTable.FAX_FLD + ","
					+ " CCN." + MST_CCNTable.WEBSITE_FLD + ","
					+ " CCN." + MST_CCNTable.EMAIL_FLD + ","
					+ " CCN." + MST_CCNTable.VAT_FLD + ","
					+ " CCN." + MST_CCNTable.COUNTRYID_FLD + ","
					+ " CCN." + MST_CCNTable.CITYID_FLD + ","
					+ " CCN." + MST_CCNTable.HOMECURRENCYID_FLD + ","
					+ " Cur." + MST_CurrencyTable.CODE_FLD + " HomeCurrency,"
					+ " CCN." + MST_CCNTable.EXCHANGERATE_FLD + ","
					+ " CCN." + MST_CCNTable.DEFAULTCURRENCYID_FLD + ","
					+ " EM." + MST_EmployeeTable.EMPLOYEEID_FLD + ","
					+ " EM." + MST_EmployeeTable.CODE_FLD + " EmployeeName,"
					+ " US." + Sys_UserTable.MASTERLOCATIONID_FLD + ","
					+ " M." + MST_MasterLocationTable.CODE_FLD + " MasterLocationCode,"
					+ " M." + MST_MasterLocationTable.NAME_FLD + " MasterLocationName,"
					+ " CCN." + MST_CCNTable.EXCHANGERATEOPERATOR_FLD
					+ " FROM " + MST_CCNTable.TABLE_NAME 
					+ " CCN LEFT JOIN " + MST_CurrencyTable.TABLE_NAME + " Cur on Cur." 
					+ MST_CurrencyTable.CURRENCYID_FLD + " = CCN." + MST_CCNTable.HOMECURRENCYID_FLD
					+ " INNER JOIN " + Sys_UserTable.TABLE_NAME + " US ON CCN."
					+ MST_CCNTable.CCNID_FLD + " = " + "US." + Sys_UserTable.CCNID_FLD
					+ " INNER JOIN " + MST_EmployeeTable.TABLE_NAME + " EM ON EM." + MST_EmployeeTable.EMPLOYEEID_FLD 
                    + " = US." + MST_EmployeeTable.EMPLOYEEID_FLD	
					+ " INNER JOIN " + MST_MasterLocationTable.TABLE_NAME + " M ON M." + MST_MasterLocationTable.MASTERLOCATIONID_FLD 
					+ " = US." + Sys_UserTable.MASTERLOCATIONID_FLD	
					+ " WHERE " + " US." + Sys_UserTable.USERNAME_FLD + " ='" + pstrUserName.Replace("'","''") + "'";
				strSql += "; SELECT "
					+ Sys_ParamTable.PARAMID_FLD + ","
					+ Sys_ParamTable.NAME_FLD + ","
					+ Sys_ParamTable.VALUE_FLD
					+ " FROM " + Sys_ParamTable.TABLE_NAME;
					
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RoleTable.TABLE_NAME);

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
		///       This method uses to get all role but grant to any user
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
		///       Thursday, January 06, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet ListRoleGrantToUser(int intUserId)
		{
			const string METHOD_NAME = THIS + ".ListRoleNotGrantToUser()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_RoleTable.ROLEID_FLD + ","
					+ Sys_RoleTable.NAME_FLD + ","
					+ Sys_RoleTable.DESCRIPTION_FLD
					+ " FROM " + Sys_RoleTable.TABLE_NAME
					+ " WHERE " + Sys_RoleTable.ROLEID_FLD + " IN (" 
					+ " SELECT " + Sys_UserToRoleTable.ROLEID_FLD 
					+ " FROM  " + Sys_UserToRoleTable.TABLE_NAME 
					+ " WHERE " + Sys_UserToRoleTable.USERID_FLD + " = " + intUserId + ")"; 
					
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_RoleTable.TABLE_NAME);

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
		///			This method uses to get all role but grant to any user
		///    </Description>
		///    <Inputs>
		///			pstrUserName - username of user
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Friday, January 14, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public DataSet ListRoleGrantToUser(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".ListRoleNotGrantToUser()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_UserToRoleTable.ROLEID_FLD + ","
					+ " FROM " + Sys_UserToRoleTable.TABLE_NAME
					+ " WHERE " + Sys_UserToRoleTable.USERID_FLD + " IN (" 
					+ " SELECT " + Sys_UserTable.USERID_FLD
					+ " FROM  " + Sys_UserTable.TABLE_NAME 
					+ " WHERE " + Sys_UserTable.USERNAME_FLD + " = '" + pstrUserName + "' )"; 
					
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, Sys_UserToRoleTable.TABLE_NAME);

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
		/// 
		/// </summary>
		/// <param name="pstrUserName"></param>
		/// <returns></returns>
		public bool UserNameBelongToAdministratorRole(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".UserNameBelongToAdministratorRole()";

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"Select count(*) from sys_role r"
						+ " inner join sys_usertoRole ur on r.roleid=ur.roleid"
						+ " inner join sys_user u on ur.Userid=u.UserID"
						+ " where u.UserName = ' + pstrUserName + '"
						+ " and r.Name = '" + Constants.ADMINISTRATORS + "'";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null)
				{
					if (int.Parse(objResult.ToString()) > 0)
						return true;
					else
						return false;
				}
				else
				{
					return false;
				}
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch(PCSException ex)
			{
				throw ex;
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
		/// GetRightToModify
		/// </summary>
		/// <param name="pstrUserName"></param>
		/// <param name="pstrTableName"></param>
		/// <param name="pstrPrimaryKeyField"></param>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		public DataSet GetRightToModify(string pstrUserName, string pstrTableName, string pstrPrimaryKeyField, int pintMasterID)
		{
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = String.Empty;
				// Get right belong to admin role to modify
				strSql=	"Select ISNULL(count(*),0) from sys_role r"
					+ " inner join sys_usertoRole ur on r.roleid=ur.roleid"
					+ " inner join sys_user u on ur.Userid=u.UserID"
					+ " where u.UserName = '" + pstrUserName + "'"
					+ " and r.Name = '" + Constants.ADMINISTRATORS + "'";
				// Get created user (UserName)
				strSql += "; Select ISNULL(UserName,'') UserName From " + pstrTableName 
					+ " Where " + pstrPrimaryKeyField + " = " + pintMasterID;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);
				

				return dstPCS;

			}
			catch
			{
				return null;
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
		/// 
		/// </summary>
		/// <param name="pstrTableName"></param>
		/// <param name="pstrPrimaryKeyField"></param>
		/// <param name="pintMasterID"></param>
		/// <returns></returns>
		public string GetCreatedUser(string pstrTableName, string pstrPrimaryKeyField, int pintMasterID)
		{
			const string METHOD_NAME = THIS + ".UserNameBelongToAdministratorRole()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"Select UserName From " + pstrTableName + " TranTable"
					+ " Where TranTable." + pstrPrimaryKeyField + " = " + pintMasterID;
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				return objResult.ToString();
			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch(PCSException ex)
			{
				throw ex;
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
		/// Get Security Information
		/// </summary>
		/// <param name="pstrUserName"></param>
		/// <param name="pstrFormName"></param>
		/// <returns></returns>
		public DataSet GetSecurityInfo(string pstrUserName,string pstrFormName)
		{
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				// Get Role by user
				strSql=	"SELECT "
					+ Sys_UserToRoleTable.ROLEID_FLD
					+ " FROM " + Sys_UserToRoleTable.TABLE_NAME
					+ " WHERE " + Sys_UserToRoleTable.USERID_FLD
					+ " IN ( SELECT " + Sys_UserTable.USERID_FLD + " FROM " + Sys_UserTable.TABLE_NAME 
					+ " WHERE " +  Sys_UserTable.USERNAME_FLD + " = '" + pstrUserName + "' ); " ;
				// Get Permistion base on FormName, and list of role
				strSql += "Select IsNull(Sum(Permission),0) Permission "
						+ " From Sys_Right "
						+ " Where Menu_EntryID IN (Select Menu_EntryID From Sys_Menu_Entry Where FormLoad='" + pstrFormName + "') "
						+ " AND RoleID IN (Select RoleID From Sys_UserToRole "
						+ " WHERE UserID = (Select UserID From Sys_User Where UserName='" + pstrUserName + "')); ";
				// Get Existed record menu
				strSql += " Select IsNull(Count(*),0) ExistedForm From Sys_menu_entry Where FormLoad = '" + pstrFormName + "'; ";
				// GetVisibleControl
				strSql += " SELECT VG.FormName,VI.Name ControlName, ISNULL(VI.Type,0) Type" 
					+ " FROM sys_VisibilityGroup_Role VGR"
					+ " INNER JOIN sys_VisibilityGroup VG ON VGR.GroupID=VG.VisibilityGroupID"
					+ " INNER JOIN sys_VisibilityItem VI ON VG.VisibilityGroupID=VI.GroupID"
					+ " WHERE VG.FormName = '" + pstrFormName + "'"
					+ " AND VGR.RoleID IN (Select RoleID From Sys_UserToRole "
						+ " WHERE UserID = (Select UserID From Sys_User Where UserName='" + pstrUserName + "') )";


				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);
				dstPCS.Tables[0].TableName = Sys_UserToRoleTable.TABLE_NAME;
				dstPCS.Tables[1].TableName = Sys_RightTable.TABLE_NAME;
				dstPCS.Tables[2].TableName = Sys_Menu_EntryTable.TABLE_NAME;
				dstPCS.Tables[3].TableName = Sys_VisibilityGroup_RoleTable.TABLE_NAME;

				return dstPCS;
			}
			catch
			{
				return null;
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
		/// Update UserName Modify Transaction
		/// </summary>
		/// <param name="pstrUserName"></param>
		/// <param name="pstrTableName"></param>
		/// <param name="pstrPrimaryKeyField"></param>
		/// <param name="pintMasterID"></param>
		public void UpdateUserNameModifyTransaction(string pstrUserName,string pstrTableName, string pstrPrimaryKeyField, int pintMasterID)
		{

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"Update " + pstrTableName + " SET UserName = '" + pstrUserName + "', LastChange = GetDate()"
					+ " Where " + pstrPrimaryKeyField + " = " + pintMasterID;
				
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteScalar();
			}
			catch
			{
				// do nothing
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
		/// 
		/// </summary>
		/// <returns></returns>
		public DataSet GetDefaultInfomation()
		{
			const string METHOD_NAME = THIS + ".UserNameBelongToAdministratorRole()";
			DataSet dstPCS = new DataSet();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			try 
			{
				string strSql = String.Empty;
				// Get Sys_Error_MsgTable
				strSql=	"SELECT "
					+ Sys_Error_MsgTable.ERROR_MSGID_FLD + ","
					+ Sys_Error_MsgTable.CODE_FLD + ","
					+ Sys_Error_MsgTable.MSGDEFAULT_FLD + ","
					+ Sys_Error_MsgTable.MSGVN_FLD + ","
					+ Sys_Error_MsgTable.MSGEN_FLD + ","
					+ Sys_Error_MsgTable.MSGJP_FLD + ","
					+ Sys_Error_MsgTable.DESCRIPTION_FLD
					+ " FROM " + Sys_Error_MsgTable.TABLE_NAME;				
				// Get Sys_menu_entry
				strSql += "; Select Parent_Shortcut, Menu_EntryID, Shortcut, Button_Caption, Text_CaptionDefault, Text_Caption_VI_VN, "
						+ " Text_Caption_EN_US, Text_Caption_JA_JP, Text_Caption_Language_Default, Parent_Child, FormLoad, Description, Type, "
						+ " CollapsedImage, ExpandedImage, Prefix, TransFormat, IsTransaction, IsUserCreated, TableName, TransNoFieldName, ReportID"
						+ " FROM " + Sys_Menu_EntryTable.TABLE_NAME;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);
				dstPCS.Tables[0].TableName = Sys_Error_MsgTable.TABLE_NAME;
				dstPCS.Tables[1].TableName = Sys_Menu_EntryTable.TABLE_NAME;

				return dstPCS;

			}
			catch(OleDbException ex)
			{			
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
			}			
			catch(PCSException ex)
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

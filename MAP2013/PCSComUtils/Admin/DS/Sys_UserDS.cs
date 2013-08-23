using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class Sys_UserDS 
	{
		public Sys_UserDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.DS.Sys_UserDS";


		//**************************************************************************              
		///    <Description>
		///       Get the System date from database
		///    </Description>
		///    <Inputs>
		///        Sys_UserVO       
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

		public string GetSystemDate() 
		{
			const string METHOD_NAME = THIS + ".GetSystemDate()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				strSql=	" SELECT  CONVERT(VARCHAR(10),getdate(),101)  ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return ocmdPCS.ExecuteScalar().ToString();

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
	
		public DateTime GetDatabaseDate() 
		{
			const string METHOD_NAME = THIS + ".GetSystemDate()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = String.Empty;
				strSql=	" SELECT  getdate() ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				return (DateTime)ocmdPCS.ExecuteScalar();

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
		///       This method uses to add data to Sys_User
		///    </Description>
		///    <Inputs>
		///        Sys_UserVO       
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
				Sys_UserVO objObject = (Sys_UserVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_User("
				+ Sys_UserTable.USERNAME_FLD + ","
				+ Sys_UserTable.PWD_FLD + ","
				+ Sys_UserTable.NAME_FLD + ","
				+ Sys_UserTable.CREATEDDATE_FLD + ","
				+ Sys_UserTable.DESCRIPTION_FLD + ","
				+ Sys_UserTable.CCNID_FLD + ","
				+ Sys_UserTable.EMPLOYEEID_FLD + ","
				+ Sys_UserTable.MASTERLOCATIONID_FLD + ","
				+ Sys_UserTable.ACTIVATE_FLD + ","
				+ Sys_UserTable.EXPIREDDATE_FLD + ")"
				+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERNAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_UserTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.PWD_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_UserTable.PWD_FLD].Value = objObject.Pwd;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_UserTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CREATEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.CREATEDDATE_FLD].Value = objObject.CreatedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.DESCRIPTION_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_UserTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = objObject.CCNID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.ACTIVATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EXPIREDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = objObject.ExpiredDate;


				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to add data to Sys_User
		///    </Description>
		///    <Inputs>
		///        Sys_UserVO       
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


		public int AddNewUserAndReturnNewID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_UserVO objObject = (Sys_UserVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_User("
					+ Sys_UserTable.USERNAME_FLD + ","
					+ Sys_UserTable.PWD_FLD + ","
					+ Sys_UserTable.NAME_FLD + ","
					+ Sys_UserTable.CREATEDDATE_FLD + ","
					+ Sys_UserTable.DESCRIPTION_FLD + ","
					+ Sys_UserTable.CCNID_FLD + ","
					+ Sys_UserTable.EMPLOYEEID_FLD + ","
					+ Sys_UserTable.MASTERLOCATIONID_FLD + ","
					+ Sys_UserTable.ACTIVATE_FLD + ","
					+ Sys_UserTable.EXPIREDDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				strSql += " ; SELECT @@IDENTITY ";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.PWD_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.PWD_FLD].Value = objObject.Pwd;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_UserTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CREATEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.CREATEDDATE_FLD].Value = objObject.CreatedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_UserTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CCNID_FLD, OleDbType.Integer));

				if (objObject.CCNID <= 0) 
				{
					ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = objObject.CCNID;
				}
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.ACTIVATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EXPIREDDATE_FLD, OleDbType.Date));

				if (objObject.ExpiredDate == DateTime.MinValue) 
				{
					ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = objObject.ExpiredDate;
				}
				
				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				//ocmdPCS.ExecuteNonQuery();	
				return int.Parse(ocmdPCS.ExecuteScalar().ToString());

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to add data to Sys_User
		///    </Description>
		///    <Inputs>
		///        Sys_UserVO       
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


		public void AddNewUser(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			 
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_UserVO objObject = (Sys_UserVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_User("
					+ Sys_UserTable.USERNAME_FLD + ","
					+ Sys_UserTable.PWD_FLD + ","
					+ Sys_UserTable.NAME_FLD + ","
					+ Sys_UserTable.CREATEDDATE_FLD + ","
					+ Sys_UserTable.DESCRIPTION_FLD + ","
					+ Sys_UserTable.CCNID_FLD + ","
					+ Sys_UserTable.EMPLOYEEID_FLD + ","
					+ Sys_UserTable.MASTERLOCATIONID_FLD + ","
					+ Sys_UserTable.ACTIVATE_FLD + ","
					+ Sys_UserTable.EXPIREDDATE_FLD + ")"
					+ "VALUES(?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERNAME_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_UserTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.PWD_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_UserTable.PWD_FLD].Value = objObject.Pwd;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_UserTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CREATEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.CREATEDDATE_FLD].Value = objObject.CreatedDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.DESCRIPTION_FLD, OleDbType.Char));
				ocmdPCS.Parameters[Sys_UserTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CCNID_FLD, OleDbType.Integer));

				if (objObject.CCNID <= 0) 
				{
					ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = objObject.CCNID;
				}
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.ACTIVATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EXPIREDDATE_FLD, OleDbType.Date));
				if (objObject.ExpiredDate == DateTime.MinValue)
				{
					ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = objObject.ExpiredDate;
				}


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to delete data from Sys_User
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
			strSql=	"DELETE " + Sys_UserTable.TABLE_NAME + " WHERE  " + Sys_UserTable.USERID_FLD + "=" + pintID.ToString();
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
		///       This method uses to get data from Sys_User
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_UserVO
		///    </Outputs>
		///    <Returns>
		///       Sys_UserVO
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
	
		public int GetUserIDByUserName(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".GetUserIDByUserName()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ Sys_UserTable.USERID_FLD 
					+ " FROM " + Sys_UserTable.TABLE_NAME
					+" WHERE " + Sys_UserTable.USERNAME_FLD + "='" + pstrUserName + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if (objReturnValue == null)
				{
					return -1;
				}
				else
				{
					return int.Parse(objReturnValue.ToString());
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

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from Sys_User
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_UserVO
		///    </Outputs>
		///    <Returns>
		///       Sys_UserVO
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
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
				+ Sys_UserTable.USERID_FLD + ","
				+ Sys_UserTable.USERNAME_FLD + ","
				+ Sys_UserTable.PWD_FLD + ","
				+ Sys_UserTable.NAME_FLD + ","
				+ Sys_UserTable.CREATEDDATE_FLD + ","
				+ Sys_UserTable.DESCRIPTION_FLD + ","
				+ Sys_UserTable.CCNID_FLD + ","
				+ Sys_UserTable.EMPLOYEEID_FLD + ","
				+ Sys_UserTable.MASTERLOCATIONID_FLD + ","
				+ Sys_UserTable.ACTIVATE_FLD + ","
				+ Sys_UserTable.EXPIREDDATE_FLD
				+ " FROM " + Sys_UserTable.TABLE_NAME
				+" WHERE " + Sys_UserTable.USERID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_UserVO objObject = new Sys_UserVO();

				while (odrPCS.Read())
				{ 
					objObject.UserID = int.Parse(odrPCS[Sys_UserTable.USERID_FLD].ToString());
					objObject.UserName = odrPCS[Sys_UserTable.USERNAME_FLD].ToString().Trim();
					objObject.Pwd = odrPCS[Sys_UserTable.PWD_FLD].ToString().Trim();
					objObject.Name = odrPCS[Sys_UserTable.NAME_FLD].ToString().Trim();
					if(odrPCS[Sys_UserTable.CREATEDDATE_FLD].ToString().Length > 0)
					{
						objObject.CreatedDate = DateTime.Parse(odrPCS[Sys_UserTable.CREATEDDATE_FLD].ToString());
					}
					objObject.Description = odrPCS[Sys_UserTable.DESCRIPTION_FLD].ToString();
					if(odrPCS[Sys_UserTable.CCNID_FLD].ToString().Length > 0)
					{
						objObject.CCNID = int.Parse(odrPCS[Sys_UserTable.CCNID_FLD].ToString());
					}
					objObject.EmployeeID = int.Parse(odrPCS[Sys_UserTable.EMPLOYEEID_FLD].ToString());
					objObject.MasterLocationID = int.Parse(odrPCS[Sys_UserTable.MASTERLOCATIONID_FLD].ToString());
					objObject.Activate = bool.Parse(odrPCS[Sys_UserTable.ACTIVATE_FLD].ToString());					
					if(odrPCS[Sys_UserTable.EXPIREDDATE_FLD].ToString().Length > 0)
					{
						objObject.ExpiredDate = DateTime.Parse(odrPCS[Sys_UserTable.EXPIREDDATE_FLD].ToString());
					}

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
		///       This method uses to get data from Sys_User
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_UserVO
		///    </Outputs>
		///    <Returns>
		///       Sys_UserVO
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

		public object GetObjectVO(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				strSql=	"SELECT "
					+ Sys_UserTable.USERID_FLD + ","
					+ Sys_UserTable.USERNAME_FLD + ","
					+ Sys_UserTable.PWD_FLD + ","
					+ Sys_UserTable.NAME_FLD + ","
					+ Sys_UserTable.CREATEDDATE_FLD + ","
					+ Sys_UserTable.DESCRIPTION_FLD + ","
					+ Sys_UserTable.CCNID_FLD + ","
					+ Sys_UserTable.EMPLOYEEID_FLD + ","
					+ Sys_UserTable.MASTERLOCATIONID_FLD + ","	
					+ Sys_UserTable.ACTIVATE_FLD + ","
					+ Sys_UserTable.EXPIREDDATE_FLD
					+ " FROM " + Sys_UserTable.TABLE_NAME
					+ " WHERE " + Sys_UserTable.USERNAME_FLD + "= '" + pstrUserName + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_UserVO objObject = new Sys_UserVO();

				while (odrPCS.Read())
				{ 
					objObject.UserID = int.Parse(odrPCS[Sys_UserTable.USERID_FLD].ToString());
					objObject.UserName = odrPCS[Sys_UserTable.USERNAME_FLD].ToString().Trim();
					objObject.Pwd = odrPCS[Sys_UserTable.PWD_FLD].ToString().Trim();
					objObject.Name = odrPCS[Sys_UserTable.NAME_FLD].ToString().Trim();
					if(odrPCS[Sys_UserTable.CREATEDDATE_FLD].ToString().Length > 0)
					{
						objObject.CreatedDate = DateTime.Parse(odrPCS[Sys_UserTable.CREATEDDATE_FLD].ToString());
					}
					objObject.Description = odrPCS[Sys_UserTable.DESCRIPTION_FLD].ToString();
					if(odrPCS[Sys_UserTable.CCNID_FLD].ToString().Length > 0)
					{
						objObject.CCNID = int.Parse(odrPCS[Sys_UserTable.CCNID_FLD].ToString());
					}
					objObject.EmployeeID = int.Parse(odrPCS[Sys_UserTable.EMPLOYEEID_FLD].ToString());
					objObject.MasterLocationID = int.Parse(odrPCS[Sys_UserTable.MASTERLOCATIONID_FLD].ToString());
					if (odrPCS[Sys_UserTable.ACTIVATE_FLD].ToString().Length > 0)
					{
						objObject.Activate = bool.Parse(odrPCS[Sys_UserTable.ACTIVATE_FLD].ToString());
					}
					if(odrPCS[Sys_UserTable.EXPIREDDATE_FLD].ToString().Length > 0)
					{
						objObject.ExpiredDate = DateTime.Parse(odrPCS[Sys_UserTable.EXPIREDDATE_FLD].ToString());
					}

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
		///       This method uses to update data to Sys_User
		///    </Description>
		///    <Inputs>
		///       Sys_UserVO       
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

			Sys_UserVO objObject = (Sys_UserVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_User SET "
				+ Sys_UserTable.USERNAME_FLD + "=   ?" + ","
				+ Sys_UserTable.PWD_FLD + "=   ?" + ","
				+ Sys_UserTable.NAME_FLD + "=   ?" + ","
				//+ Sys_UserTable.CREATEDDATE_FLD + "=   ?" + ","
				+ Sys_UserTable.DESCRIPTION_FLD + "=   ?" + ","
				+ Sys_UserTable.CCNID_FLD + "=   ?" + ","
				+ Sys_UserTable.EMPLOYEEID_FLD + "=   ?" + ","
				+ Sys_UserTable.MASTERLOCATIONID_FLD + "=   ?" + ","
				+ Sys_UserTable.ACTIVATE_FLD + "=   ?" + ","
				+ Sys_UserTable.EXPIREDDATE_FLD + "=  ?"
				+" WHERE " + Sys_UserTable.USERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.PWD_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.PWD_FLD].Value = objObject.Pwd;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.NAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.NAME_FLD].Value = objObject.Name;

				/*
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CREATEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.CREATEDDATE_FLD].Value = objObject.CreatedDate;
				*/

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.DESCRIPTION_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = objObject.CCNID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.ACTIVATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EXPIREDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = objObject.ExpiredDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.USERID_FLD].Value = objObject.UserID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to update data to Sys_User
		///    </Description>
		///    <Inputs>
		///       Sys_UserVO       
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
		
	
		public void UpdateUserInfo(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			Sys_UserVO objObject = (Sys_UserVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_User SET "
					+ Sys_UserTable.USERNAME_FLD + "=   ?" + ","
					+ Sys_UserTable.PWD_FLD + "=   ?" + ","
					+ Sys_UserTable.NAME_FLD + "=   ?" + ","
					//+ Sys_UserTable.CREATEDDATE_FLD + "=   ?" + ","
					+ Sys_UserTable.DESCRIPTION_FLD + "=   ?" + ","
					+ Sys_UserTable.CCNID_FLD + "=   ?" + ","
					+ Sys_UserTable.EMPLOYEEID_FLD + "=   ?" + ","
					+ Sys_UserTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ Sys_UserTable.ACTIVATE_FLD + "=   ?" + ","
					+ Sys_UserTable.EXPIREDDATE_FLD + "=  ?"
					+" WHERE " + Sys_UserTable.USERID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.USERNAME_FLD].Value = objObject.UserName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.PWD_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.PWD_FLD].Value = objObject.Pwd;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.NAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.NAME_FLD].Value = objObject.Name;

				/*
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CREATEDDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[Sys_UserTable.CREATEDDATE_FLD].Value = objObject.CreatedDate;
				*/

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.DESCRIPTION_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID <= 0) 
				{
					ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = DBNull.Value;
				}
				else 
				{
					ocmdPCS.Parameters[Sys_UserTable.CCNID_FLD].Value = objObject.CCNID;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;
				
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.ACTIVATE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.ACTIVATE_FLD].Value = objObject.Activate;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.EXPIREDDATE_FLD, OleDbType.Date));
				if (objObject.ExpiredDate == DateTime.MinValue) 
				{
					ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[Sys_UserTable.EXPIREDDATE_FLD].Value = objObject.ExpiredDate;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_UserTable.USERID_FLD].Value = objObject.UserID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
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
		///       This method uses to update data to Sys_User
		///    </Description>
		///    <Inputs>
		///       Sys_UserVO       
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
		
	
		public void UpdatePassword(string pstrUserName, string pstrNewPassword)
		{
			const string METHOD_NAME = THIS + ".Update()";


			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_User SET "
					+ Sys_UserTable.PWD_FLD + "=   ?"
					+" WHERE " + Sys_UserTable.USERNAME_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.PWD_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.PWD_FLD].Value = pstrNewPassword;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_UserTable.USERNAME_FLD, OleDbType.VarChar));
				ocmdPCS.Parameters[Sys_UserTable.USERNAME_FLD].Value = pstrUserName;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
			}
			catch(OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
		///       This method uses to get all data from Sys_User
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
				+ Sys_UserTable.USERID_FLD + ","
				+ Sys_UserTable.USERNAME_FLD + ","
				+ Sys_UserTable.PWD_FLD + ","
				//+ Sys_UserTable.NAME_FLD + ","
				+ "E.Name,"
				+ Sys_UserTable.CREATEDDATE_FLD + ","
				+ Sys_UserTable.DESCRIPTION_FLD + ","
				+ "U." + Sys_UserTable.CCNID_FLD + ","
				+ "U." + Sys_UserTable.EMPLOYEEID_FLD + ","
				+ "U." + Sys_UserTable.MASTERLOCATIONID_FLD + ","
				+ "M." + MST_MasterLocationTable.CODE_FLD + " MasterLocationCode,"
				+ Sys_UserTable.ACTIVATE_FLD + ","
				+ Sys_UserTable.EXPIREDDATE_FLD

				+ " FROM " + Sys_UserTable.TABLE_NAME + " U"
				+ " INNER JOIN MST_Employee E ON U.EmployeeID=E.EmployeeID"
				+ " INNER JOIN MST_MasterLocation M ON U.MasterLocationID=M.MasterLocationID"
				+ " AND U.UserName <> '" + Constants.SUPER_ADMIN_USER + "'";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_UserTable.TABLE_NAME);

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
		///       This method uses to get all data from Sys_User
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

		public DataTable ListAllUsers()
		{
			const string METHOD_NAME = THIS + ".ListAllUsers()";
			DataSet dstPCS = new DataSet();
			
		

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_UserTable.USERID_FLD + ","
					+ Sys_UserTable.USERNAME_FLD
					+ " FROM " + Sys_UserTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_UserTable.TABLE_NAME);

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

			try
			{
				strSql=	"SELECT "
				+ Sys_UserTable.USERID_FLD + ","
				+ Sys_UserTable.USERNAME_FLD + ","
				+ Sys_UserTable.PWD_FLD + ","
				+ Sys_UserTable.NAME_FLD + ","
				+ Sys_UserTable.CREATEDDATE_FLD + ","
				+ Sys_UserTable.DESCRIPTION_FLD + ","
				+ Sys_UserTable.CCNID_FLD + ","
				+ Sys_UserTable.EMPLOYEEID_FLD + ","
				+ Sys_UserTable.MASTERLOCATIONID_FLD + ","
				+ Sys_UserTable.ACTIVATE_FLD + ","
				+ Sys_UserTable.EXPIREDDATE_FLD 
		+ "  FROM " + Sys_UserTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_UserTable.TABLE_NAME);

				pData.Clear();
				odadPCS.Fill(pData,pData.Tables[0].TableName);
				pData.AcceptChanges();

			}
			catch(OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE )
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
		///       This method uses to check authenticate for a user
		///    </Description>
		///    <Inputs>
		///        pobjObjecVO contains UserName and Password of user        
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       UserID : If user authenticates
		///       -1: If user doesn't authenticate
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, January 10, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public Sys_UserVO CheckAuthenticate(Sys_UserVO pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".CheckAuthenticate()";

			Sys_UserVO objOldObjec = pobjObjecVO;
			Sys_UserVO objObjec = (Sys_UserVO) GetObjectVO(pobjObjecVO.UserName);

			PCSComUtils.Common.DS.UtilsDS dsUtils = new PCSComUtils.Common.DS.UtilsDS();

			try 
			{
				// Check null user
				if(objObjec.UserName == null)
				{
					PCSException ex = new PCSException();
					ex.mCode = ErrorCode.USERNAME_PASSWORD_INVALID;
					throw ex;
				}
				else if ((objOldObjec.UserName.ToLower() != objObjec.UserName.ToLower())||(objOldObjec.Pwd != objObjec.Pwd))
				{
					PCSException ex = new PCSException();
					ex.mCode = ErrorCode.USERNAME_PASSWORD_INVALID;
					throw ex;
				}
				else
				{
					if (!objObjec.Activate)
					{
						PCSException ex = new PCSException();
						ex.mCode = ErrorCode.MESSAGE_ACOUNT_NOTACTIVE;
						throw ex;
					}

					//HACK by Tuan TQ. 11 Jan, 2006
					//if(objObjec.ExpiredDate < DateTime.Now) //Rem by Tuan TQ. 11 Jan, 2005
					if ((objObjec.ExpiredDate < dsUtils.GetDBDate()) 
						&& (objObjec.ExpiredDate != DateTime.MinValue))
					{
						PCSException ex = new PCSException();
						ex.mCode = ErrorCode.MESSAGE_ACOUNT_EXPIRE;
						throw ex;
					}
					//End hack
				}

				return objObjec;
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
			}
		}

		//**************************************************************************              
		///    <Description>
		///       Get all role for a user defined by username
		///    </Description>
		///    <Inputs>
		///        pstrUserName : UserName of user
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, January 10, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet ListRoleForUser(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".ListRoleForUser()";
			DataSet dstPCS = new DataSet();
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT "
					+ Sys_UserToRoleTable.ROLEID_FLD
					+ " FROM " + Sys_UserToRoleTable.TABLE_NAME
					+ " WHERE " + Sys_UserToRoleTable.USERID_FLD
					+ " IN ( SELECT " + Sys_UserTable.USERID_FLD + " FROM " + Sys_UserTable.TABLE_NAME 
					+ " WHERE " +  Sys_UserTable.USERNAME_FLD + " = '" + pstrUserName + "' )" ;
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_UserToRoleTable.TABLE_NAME);

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
		///       This method uses to check authenticate for a user
		///    </Description>
		///    <Inputs>
		///        pobjObjecVO contains UserName and Password of user        
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       UserID : If user authenticates
		///       -1: If user doesn't authenticate
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Tuesday, January 10, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool IsSystemAdmin(string pstrUserName)
		{
			const string METHOD_NAME = THIS + ".IsSystemAdmin()";

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				
				strSql=	"SELECT ISNULL(COUNT(*), 0) FROM " + Sys_UserToRoleTable.TABLE_NAME
					+ " WHERE " + Sys_UserToRoleTable.ROLEID_FLD + "="
					+ " (SELECT " + Sys_RoleTable.ROLEID_FLD + " FROM " + Sys_RoleTable.TABLE_NAME
					+ " WHERE " + Sys_RoleTable.NAME_FLD + "='" + Constants.ADMINISTRATORS + "')";
				
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
					return false;
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
			}
		}

	}
}

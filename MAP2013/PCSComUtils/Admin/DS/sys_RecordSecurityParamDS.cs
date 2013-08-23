using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class sys_RecordSecurityParamDS 
	{
		public sys_RecordSecurityParamDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.sys_RecordSecurityParamDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to sys_RecordSecurityParam
		///    </Description>
		///    <Inputs>
		///        sys_RecordSecurityParamVO       
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
				sys_RecordSecurityParamVO objObject = (sys_RecordSecurityParamVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO sys_RecordSecurityParam("
				+ sys_RecordSecurityParamTable.SOURCETABLENAME_FLD + ","
				+ sys_RecordSecurityParamTable.MENUNAME_FLD + ","
				+ sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.SOURCETABLENAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.SOURCETABLENAME_FLD].Value = objObject.SourceTableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.MENUNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.MENUNAME_FLD].Value = objObject.MenuName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD].Value = objObject.SecurityTableName;


				
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
		///       This method uses to delete data from sys_RecordSecurityParam
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
			strSql=	"DELETE " + sys_RecordSecurityParamTable.TABLE_NAME + " WHERE  " + "RecordSecurityParamID" + "=" + pintID.ToString();
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
		///       This method uses to get data from sys_RecordSecurityParam
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       sys_RecordSecurityParamVO
		///    </Outputs>
		///    <Returns>
		///       sys_RecordSecurityParamVO
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
				+ sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD + ","
				+ sys_RecordSecurityParamTable.SOURCETABLENAME_FLD + ","
				+ sys_RecordSecurityParamTable.MENUNAME_FLD + ","
				+ sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD
				+ " FROM " + sys_RecordSecurityParamTable.TABLE_NAME
				+" WHERE " + sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				sys_RecordSecurityParamVO objObject = new sys_RecordSecurityParamVO();

				while (odrPCS.Read())
				{ 
				objObject.RecordSecurityParamID = int.Parse(odrPCS[sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD].ToString().Trim());
				objObject.SourceTableName = odrPCS[sys_RecordSecurityParamTable.SOURCETABLENAME_FLD].ToString().Trim();
				objObject.MenuName = odrPCS[sys_RecordSecurityParamTable.MENUNAME_FLD].ToString().Trim();
				objObject.SecurityTableName = odrPCS[sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD].ToString().Trim();

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
		///       This method uses to update data to sys_RecordSecurityParam
		///    </Description>
		///    <Inputs>
		///       sys_RecordSecurityParamVO       
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

			sys_RecordSecurityParamVO objObject = (sys_RecordSecurityParamVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE sys_RecordSecurityParam SET "
				+ sys_RecordSecurityParamTable.SOURCETABLENAME_FLD + "=   ?" + ","
				+ sys_RecordSecurityParamTable.MENUNAME_FLD + "=   ?" + ","
				+ sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD + "=  ?"
				+" WHERE " + sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.SOURCETABLENAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.SOURCETABLENAME_FLD].Value = objObject.SourceTableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.MENUNAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.MENUNAME_FLD].Value = objObject.MenuName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD].Value = objObject.SecurityTableName;

				ocmdPCS.Parameters.Add(new OleDbParameter(sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD].Value = objObject.RecordSecurityParamID;


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
		///       This method uses to get all data from sys_RecordSecurityParam
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
				+ sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD + ","
				+ sys_RecordSecurityParamTable.SOURCETABLENAME_FLD + ","
				+ sys_RecordSecurityParamTable.MENUNAME_FLD + ","
				+ sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD
					+ " FROM " + sys_RecordSecurityParamTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,sys_RecordSecurityParamTable.TABLE_NAME);

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
				+ sys_RecordSecurityParamTable.RECORDSECURITYPARAMID_FLD + ","
				+ sys_RecordSecurityParamTable.SOURCETABLENAME_FLD + ","
				+ sys_RecordSecurityParamTable.MENUNAME_FLD + ","
				+ sys_RecordSecurityParamTable.SECURITYTABLENAME_FLD 
		+ "  FROM " + sys_RecordSecurityParamTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,sys_RecordSecurityParamTable.TABLE_NAME);

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
	}
}

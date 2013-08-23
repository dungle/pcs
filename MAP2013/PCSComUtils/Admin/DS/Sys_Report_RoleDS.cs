using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Admin.DS
{
	public class Sys_Report_RoleDS 
	{
		public Sys_Report_RoleDS()
		{
		}
		private const string THIS = "PCSComUtils.Admin.DS.Sys_Report_RoleDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to Sys_Report_Role
		///    </Description>
		///    <Inputs>
		///        Sys_Report_RoleVO       
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
		///       Wednesday, April 06, 2005
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
				Sys_Report_RoleVO objObject = (Sys_Report_RoleVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO Sys_Report_Role("
				+ Sys_Report_RoleTable.REPORTID_FLD + ","
				+ Sys_Report_RoleTable.ROLEID_FLD + ")"
				+ "VALUES(?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Report_RoleTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_Report_RoleTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Report_RoleTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_Report_RoleTable.ROLEID_FLD].Value = objObject.RoleID;


				
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
		///       This method uses to delete data from Sys_Report_Role
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
			strSql=	"DELETE " + Sys_Report_RoleTable.TABLE_NAME + " WHERE  " + "Report_RoleID" + "=" + pintID.ToString();
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
		///       This method uses to check if user has right to view report or not
		///    </Description>
		///    <Inputs>
		///        ReportID, List of Role of User
		///    </Inputs>
		///    <Outputs>
		///       bool
		///    </Outputs>
		///    <Returns>
		///       bool
		///    </Returns>
		///    <Authors>
		///       DungLa
		///    </Authors>
		///    <History>
		///       06-Apr-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************

		public bool HasRight(string pstrReportID, ArrayList parrRoles)
		{
			const string METHOD_NAME = THIS + ".HasRight()";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				string strSql = string.Empty;
				strSql = "SELECT ISNULL(COUNT(*),0) FROM " + Sys_Report_RoleTable.TABLE_NAME;
				for (int i = 0; i < parrRoles.Count; i++)
				{
					if (i == 0)
						strSql += " WHERE " + Sys_Report_RoleTable.ROLEID_FLD + "=" + parrRoles[i].ToString();
					else
						strSql += " OR " + Sys_Report_RoleTable.ROLEID_FLD + "=" + parrRoles[i].ToString();
				}
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objResult = ocmdPCS.ExecuteScalar();
				ocmdPCS = null;
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
				if (ex.Errors.Count > 0)
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
				else
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
		///       This method uses to get data from Sys_Report_Role
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       Sys_Report_RoleVO
		///    </Outputs>
		///    <Returns>
		///       Sys_Report_RoleVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Wednesday, April 06, 2005
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
				+ Sys_Report_RoleTable.REPORT_ROLEID_FLD + ","
				+ Sys_Report_RoleTable.REPORTID_FLD + ","
				+ Sys_Report_RoleTable.ROLEID_FLD
				+ " FROM " + Sys_Report_RoleTable.TABLE_NAME
				+" WHERE " + Sys_Report_RoleTable.REPORT_ROLEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_Report_RoleVO objObject = new Sys_Report_RoleVO();

				while (odrPCS.Read())
				{ 
				objObject.Report_RoleID = int.Parse(odrPCS[Sys_Report_RoleTable.REPORT_ROLEID_FLD].ToString().Trim());
				objObject.ReportID = odrPCS[Sys_Report_RoleTable.REPORTID_FLD].ToString().Trim();
				objObject.RoleID = int.Parse(odrPCS[Sys_Report_RoleTable.ROLEID_FLD].ToString().Trim());

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
		///       This method uses to update data to Sys_Report_Role
		///    </Description>
		///    <Inputs>
		///       Sys_Report_RoleVO       
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

			Sys_Report_RoleVO objObject = (Sys_Report_RoleVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE Sys_Report_Role SET "
				+ Sys_Report_RoleTable.REPORTID_FLD + "=   ?" + ","
				+ Sys_Report_RoleTable.ROLEID_FLD + "=  ?"
				+" WHERE " + Sys_Report_RoleTable.REPORT_ROLEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Report_RoleTable.REPORTID_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_Report_RoleTable.REPORTID_FLD].Value = objObject.ReportID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Report_RoleTable.ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_Report_RoleTable.ROLEID_FLD].Value = objObject.RoleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_Report_RoleTable.REPORT_ROLEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_Report_RoleTable.REPORT_ROLEID_FLD].Value = objObject.Report_RoleID;


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
		///       This method uses to get all data from Sys_Report_Role
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
		///       Wednesday, April 06, 2005
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
				+ Sys_Report_RoleTable.REPORT_ROLEID_FLD + ","
				+ Sys_Report_RoleTable.REPORTID_FLD + ","
				+ Sys_Report_RoleTable.ROLEID_FLD
					+ " FROM " + Sys_Report_RoleTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_Report_RoleTable.TABLE_NAME);

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
		///       Wednesday, April 06, 2005
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
				+ Sys_Report_RoleTable.REPORT_ROLEID_FLD + ","
				+ Sys_Report_RoleTable.REPORTID_FLD + ","
				+ Sys_Report_RoleTable.ROLEID_FLD 
		+ "  FROM " + Sys_Report_RoleTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,Sys_Report_RoleTable.TABLE_NAME);

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
	}
}

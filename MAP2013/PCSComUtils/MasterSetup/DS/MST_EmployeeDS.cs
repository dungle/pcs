using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_EmployeeDS 
	{
		public MST_EmployeeDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_EmployeeDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_Employee
		///    </Description>
		///    <Inputs>
		///        MST_EmployeeVO       
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
		///       Tuesday, January 25, 2005
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
				MST_EmployeeVO objObject = (MST_EmployeeVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_Employee("
				+ MST_EmployeeTable.CODE_FLD + ","
				+ MST_EmployeeTable.NAME_FLD + ","
				+ MST_EmployeeTable.DEPARTMENTID_FLD + ","
				+ MST_EmployeeTable.SHIFT_FLD + ")"
				+ "VALUES(?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_EmployeeTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_EmployeeTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.SHIFT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeTable.SHIFT_FLD].Value = objObject.Shift;


				
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
		///       This method uses to delete data from MST_Employee
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
			strSql=	"DELETE " + MST_EmployeeTable.TABLE_NAME + " WHERE  " + "EmployeeID" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_Employee
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_EmployeeVO
		///    </Outputs>
		///    <Returns>
		///       MST_EmployeeVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
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
				+ MST_EmployeeTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeTable.CODE_FLD + ","
				+ MST_EmployeeTable.NAME_FLD + ","
				+ MST_EmployeeTable.DEPARTMENTID_FLD + ","
				+ MST_EmployeeTable.SHIFT_FLD
				+ " FROM " + MST_EmployeeTable.TABLE_NAME
				+" WHERE " + MST_EmployeeTable.EMPLOYEEID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_EmployeeVO objObject = new MST_EmployeeVO();

				while (odrPCS.Read())
				{ 
				objObject.EmployeeID = int.Parse(odrPCS[MST_EmployeeTable.EMPLOYEEID_FLD].ToString().Trim());
				objObject.Code = odrPCS[MST_EmployeeTable.CODE_FLD].ToString().Trim();
				objObject.Name = odrPCS[MST_EmployeeTable.NAME_FLD].ToString().Trim();
				objObject.DepartmentID = int.Parse(odrPCS[MST_EmployeeTable.DEPARTMENTID_FLD].ToString().Trim());
				objObject.Shift = int.Parse(odrPCS[MST_EmployeeTable.SHIFT_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_Employee
		///    </Description>
		///    <Inputs>
		///       MST_EmployeeVO       
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

			MST_EmployeeVO objObject = (MST_EmployeeVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_Employee SET "
				+ MST_EmployeeTable.CODE_FLD + "=   ?" + ","
				+ MST_EmployeeTable.NAME_FLD + "=   ?" + ","
				+ MST_EmployeeTable.DEPARTMENTID_FLD + "=   ?" + ","
				+ MST_EmployeeTable.SHIFT_FLD + "=  ?"
				+" WHERE " + MST_EmployeeTable.EMPLOYEEID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.CODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_EmployeeTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.NAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_EmployeeTable.NAME_FLD].Value = objObject.Name;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.DEPARTMENTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeTable.DEPARTMENTID_FLD].Value = objObject.DepartmentID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.SHIFT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeTable.SHIFT_FLD].Value = objObject.Shift;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;


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
		///       This method uses to get all data from MST_Employee
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
		///       Tuesday, January 25, 2005
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
				+ MST_EmployeeTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeTable.CODE_FLD + ","
				+ MST_EmployeeTable.NAME_FLD + ","
				+ MST_EmployeeTable.DEPARTMENTID_FLD + ","
				+ MST_EmployeeTable.SHIFT_FLD
					+ " FROM " + MST_EmployeeTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_EmployeeTable.TABLE_NAME);

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
		///       Tuesday, January 25, 2005
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
				+ MST_EmployeeTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeTable.CODE_FLD + ","
				+ MST_EmployeeTable.NAME_FLD + ","
				+ MST_EmployeeTable.DEPARTMENTID_FLD + ","
				+ MST_EmployeeTable.SHIFT_FLD 
		+ "  FROM " + MST_EmployeeTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_EmployeeTable.TABLE_NAME);

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

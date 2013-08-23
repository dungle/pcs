using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.MasterSetup.DS
{
	public class MST_EmployeeApprovalLevelDS 
	{
		public MST_EmployeeApprovalLevelDS()
		{
		}
		private const string THIS = "PCSComUtils.MasterSetup.DS.MST_EmployeeApprovalLevelDS";

	
		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to MST_EmployeeApprovalLevel
		///    </Description>
		///    <Inputs>
		///        MST_EmployeeApprovalLevelVO       
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
				MST_EmployeeApprovalLevelVO objObject = (MST_EmployeeApprovalLevelVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
				
				strSql=	"INSERT INTO MST_EmployeeApprovalLevel("
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD + ")"
				+ "VALUES(?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD].Value = objObject.EmployeeApprovalLevelID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD].Value = objObject.ApprovalLevelID;


				
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
		///       This method uses to delete data from MST_EmployeeApprovalLevel
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
			strSql=	"DELETE " + MST_EmployeeApprovalLevelTable.TABLE_NAME + " WHERE  " + "Description" + "=" + pintID.ToString();
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
		///       This method uses to get data from MST_EmployeeApprovalLevel
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       MST_EmployeeApprovalLevelVO
		///    </Outputs>
		///    <Returns>
		///       MST_EmployeeApprovalLevelVO
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
				+ MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD
				+ " FROM " + MST_EmployeeApprovalLevelTable.TABLE_NAME
				+" WHERE " + MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				MST_EmployeeApprovalLevelVO objObject = new MST_EmployeeApprovalLevelVO();

				while (odrPCS.Read())
				{ 
				objObject.Description = odrPCS[MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD].ToString().Trim();
				objObject.EmployeeApprovalLevelID = int.Parse(odrPCS[MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD].ToString().Trim());
				objObject.EmployeeID = int.Parse(odrPCS[MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD].ToString().Trim());
				objObject.ApprovalLevelID = int.Parse(odrPCS[MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD].ToString().Trim());

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
		///       This method uses to update data to MST_EmployeeApprovalLevel
		///    </Description>
		///    <Inputs>
		///       MST_EmployeeApprovalLevelVO       
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

			MST_EmployeeApprovalLevelVO objObject = (MST_EmployeeApprovalLevelVO) pobjObjecVO;
			

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql=	"UPDATE MST_EmployeeApprovalLevel SET "
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD + "=   ?" + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD + "=   ?" + ","
				+ MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD + "=  ?"
				+" WHERE " + MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD].Value = objObject.EmployeeApprovalLevelID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD].Value = objObject.EmployeeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD].Value = objObject.ApprovalLevelID;

				ocmdPCS.Parameters.Add(new OleDbParameter(MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD].Value = objObject.Description;


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
		///       This method uses to get all data from MST_EmployeeApprovalLevel
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
				+ MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD
					+ " FROM " + MST_EmployeeApprovalLevelTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
				
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,MST_EmployeeApprovalLevelTable.TABLE_NAME);

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
				+ MST_EmployeeApprovalLevelTable.DESCRIPTION_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEAPPROVALLEVELID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.EMPLOYEEID_FLD + ","
				+ MST_EmployeeApprovalLevelTable.APPROVALLEVELID_FLD 
		+ "  FROM " + MST_EmployeeApprovalLevelTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData,MST_EmployeeApprovalLevelTable.TABLE_NAME);

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

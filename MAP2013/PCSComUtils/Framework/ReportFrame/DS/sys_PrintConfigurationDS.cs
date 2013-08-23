using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.Framework.ReportFrame.DS
{
	public class Sys_PrintConfigurationDS 
	{
		public Sys_PrintConfigurationDS()
		{

		}
		private const string THIS = ".Sys_PrintConfigurationDS";
	

		///    <summary>
		///       This method uses to add data to Sys_PrintConfiguration
		///    </summary>
		///    <Inputs>
		///        Sys_PrintConfigurationVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, September 16, 2005
		///    </History>
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";
			
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Sys_PrintConfigurationVO objObject = (Sys_PrintConfigurationVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);
			
				strSql=	"INSERT INTO Sys_PrintConfiguration("
					+ Sys_PrintConfigurationTable.FORMNAME_FLD + ","
					+ Sys_PrintConfigurationTable.FILENAME_FLD + ","
					+ Sys_PrintConfigurationTable.COPIES_FLD + ","
					+ Sys_PrintConfigurationTable.DESCRIPTION_FLD + ","					
					+ Sys_PrintConfigurationTable.PRINTABLE_FLD  + ","
					+ Sys_PrintConfigurationTable.FUNCTIONNAME_FLD + ","
					+ Sys_PrintConfigurationTable.REPORTNAME_FLD 
					+ ")"
					+ "VALUES(?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FILENAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FILENAME_FLD].Value = objObject.FileName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.COPIES_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.COPIES_FLD].Value = objObject.Copies;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.PRINTABLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.PRINTABLE_FLD].Value = objObject.Printable;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FUNCTIONNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FUNCTIONNAME_FLD].Value = objObject.FunctionName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.REPORTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.REPORTNAME_FLD].Value = objObject.ReportName;

			
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

		


		///    <summary>
		///       This method uses to add data to Sys_PrintConfiguration
		///    </summary>
		///    <Inputs>
		///        Sys_PrintConfigurationVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, September 16, 2005
		///    </History>

		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql=	"DELETE " + Sys_PrintConfigurationTable.TABLE_NAME + " WHERE  " + "PrintConfigurationID" + "=" + pintID.ToString();
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




		///    <summary>
		///       This method uses to add data to Sys_PrintConfiguration
		///    </summary>
		///    <Inputs>
		///        Sys_PrintConfigurationVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, September 16, 2005
		///    </History>

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
					+ Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD + ","
					+ Sys_PrintConfigurationTable.FORMNAME_FLD + ","
					+ Sys_PrintConfigurationTable.FILENAME_FLD + ","
					+ Sys_PrintConfigurationTable.COPIES_FLD + ","
					+ Sys_PrintConfigurationTable.DESCRIPTION_FLD + ","
					+ Sys_PrintConfigurationTable.PRINTABLE_FLD + ","
					+ Sys_PrintConfigurationTable.FUNCTIONNAME_FLD + ","
					+ Sys_PrintConfigurationTable.REPORTNAME_FLD 
					+ " FROM " + Sys_PrintConfigurationTable.TABLE_NAME
					+" WHERE " + Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				Sys_PrintConfigurationVO objObject = new Sys_PrintConfigurationVO();

				while (odrPCS.Read())
				{ 
					objObject.PrintConfigurationID = int.Parse(odrPCS[Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD].ToString().Trim());
					objObject.FormName = odrPCS[Sys_PrintConfigurationTable.FORMNAME_FLD].ToString().Trim();
					objObject.FileName = odrPCS[Sys_PrintConfigurationTable.FILENAME_FLD].ToString().Trim();
					objObject.Copies = int.Parse(odrPCS[Sys_PrintConfigurationTable.COPIES_FLD].ToString().Trim());
					objObject.Description = odrPCS[Sys_PrintConfigurationTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.Printable = bool.Parse(odrPCS[Sys_PrintConfigurationTable.PRINTABLE_FLD].ToString().Trim());
					objObject.FunctionName = odrPCS[Sys_PrintConfigurationTable.FUNCTIONNAME_FLD].ToString().Trim();
					objObject.ReportName = odrPCS[Sys_PrintConfigurationTable.REPORTNAME_FLD].ToString().Trim();

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
		    
	
		
		/// <summary>
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pobjObjecVO"></param>
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			Sys_PrintConfigurationVO objObject = (Sys_PrintConfigurationVO) pobjObjecVO;		

			//prepare value for parameters
			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql=	"UPDATE Sys_PrintConfiguration SET "
					+ Sys_PrintConfigurationTable.FORMNAME_FLD + "=   ?" + ","
					+ Sys_PrintConfigurationTable.FILENAME_FLD + "=   ?" + ","
					+ Sys_PrintConfigurationTable.COPIES_FLD + "=   ?" + ","
					+ Sys_PrintConfigurationTable.DESCRIPTION_FLD + "=   ?" + ","
					+ Sys_PrintConfigurationTable.PRINTABLE_FLD + "=  ?"  + ","
					+ Sys_PrintConfigurationTable.FUNCTIONNAME_FLD + "=   ?" + ","
					+ Sys_PrintConfigurationTable.REPORTNAME_FLD + "=   ?"
					+" WHERE " + Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD + "= ?";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

				ocmdPCS = new OleDbCommand(strSql, oconPCS);			

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FORMNAME_FLD].Value = objObject.FormName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FILENAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FILENAME_FLD].Value = objObject.FileName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.COPIES_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.COPIES_FLD].Value = objObject.Copies;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.DESCRIPTION_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.PRINTABLE_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.PRINTABLE_FLD].Value = objObject.Printable;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FUNCTIONNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FUNCTIONNAME_FLD].Value = objObject.FunctionName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.REPORTNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.REPORTNAME_FLD].Value = objObject.ReportName;

				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD].Value = objObject.PrintConfigurationID;
				
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


		///    <summary>
		///       This method uses to add data to Sys_PrintConfiguration
		///       
		///    </summary>
		///    <Inputs>
		///        Sys_PrintConfigurationVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, September 16, 2005
		///       12/Oct/2005 Thachnn: fix bug injection
		///    </History>
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
					+ Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD + ","
					+ Sys_PrintConfigurationTable.FORMNAME_FLD + ","
					+ Sys_PrintConfigurationTable.FILENAME_FLD + ","
					+ Sys_PrintConfigurationTable.COPIES_FLD + ","
					+ Sys_PrintConfigurationTable.DESCRIPTION_FLD + ","
					+ Sys_PrintConfigurationTable.PRINTABLE_FLD  + ","
					+ Sys_PrintConfigurationTable.FUNCTIONNAME_FLD + ","
					+ Sys_PrintConfigurationTable.REPORTNAME_FLD
					+ " FROM " + Sys_PrintConfigurationTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();
			
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS,Sys_PrintConfigurationTable.TABLE_NAME);

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


		///    <summary>
		///       This method uses to add data to Sys_PrintConfiguration
		///    </summary>
		///    <Inputs>
		///        Sys_PrintConfigurationVO       
		///    </Inputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <History>
		///       Friday, September 16, 2005
		///    </History>
	
		public void UpdateDataSet(DataSet pdstData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT "
					+ Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD + ","
					+ Sys_PrintConfigurationTable.FORMNAME_FLD + ","
					+ Sys_PrintConfigurationTable.FILENAME_FLD + ","
					+ Sys_PrintConfigurationTable.COPIES_FLD + ","
					+ Sys_PrintConfigurationTable.DESCRIPTION_FLD + ","
					+ Sys_PrintConfigurationTable.PRINTABLE_FLD  + ","
					+ Sys_PrintConfigurationTable.FUNCTIONNAME_FLD + ","
					+ Sys_PrintConfigurationTable.REPORTNAME_FLD
					+ "  FROM " + Sys_PrintConfigurationTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pdstData.EnforceConstraints = false;
				odadPCS.Update(pdstData,Sys_PrintConfigurationTable.TABLE_NAME);

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


		/// <summary>
		/// 12/Oct/2005 Thachnn: fix bug injection
		/// </summary>
		/// <param name="pstrFormName"></param>
		/// <returns></returns>
		public DataTable GetPrintConfigurationByFormName(string pstrFormName)
		{
			const string METHOD_NAME = THIS + ".GetPrintConfigurationByFormName()";
			DataSet dstPCS = new DataSet();	

			OleDbConnection oconPCS =null;
			OleDbCommand ocmdPCS = null;
			try 
			{
				string strSql = "SELECT "
					+ Sys_PrintConfigurationTable.PRINTABLE_FLD  + ", "
					+ Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD + ", "
					+ Sys_PrintConfigurationTable.FORMNAME_FLD + ", "
					+ Sys_PrintConfigurationTable.FILENAME_FLD + ", "
					+ Sys_PrintConfigurationTable.COPIES_FLD + ", "
					+ Sys_PrintConfigurationTable.DESCRIPTION_FLD	+ ", "				
					+ Sys_PrintConfigurationTable.FUNCTIONNAME_FLD	+ ", "			
					+ Sys_PrintConfigurationTable.REPORTNAME_FLD					
					+ " FROM " + Sys_PrintConfigurationTable.TABLE_NAME
					+ " WHERE " + Sys_PrintConfigurationTable.FORMNAME_FLD + " = ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);				
				
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(Sys_PrintConfigurationTable.FORMNAME_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[Sys_PrintConfigurationTable.FORMNAME_FLD].Value = pstrFormName;
				ocmdPCS.Connection.Open();			

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, Sys_PrintConfigurationTable.TABLE_NAME);
				if(dstPCS.Tables.Count != 0)
				{
					return dstPCS.Tables[Sys_PrintConfigurationTable.TABLE_NAME];
				}
				else
				{
					return null;
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


	}
}


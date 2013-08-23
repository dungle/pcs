using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Text;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduction.DCP.DS
{
	/// <summary>
	/// Summary description for ImportPlanDataDS.
	/// </summary>
	public class ImportPlanDataDS 
	{
		private const string THIS = "PCSComProduction.DCP.DS.ImportPlanDataDS";
		public ImportPlanDataDS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		/// <summary>
		///  Clear A1 data
		/// </summary>
		public void Delete()
		{
			const string METHOD_NAME = THIS + ".Delete()";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("DELETE A1", oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;

			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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

		/// <summary>
		/// Update A1 Table
		/// </summary>
		/// <param name="pData"></param>
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS =null;
			OleDbCommandBuilder odcbPCS ;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql=	"SELECT ProductID, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11,"
					+ " F12, F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24,"
					+ " F25, F26, F27, F28, F29, F30, F31 FROM A1";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				int intCon = odadPCS.Update(pData, "A1");
				int a = intCon;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
						throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
					else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
				}
				else
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

		public DataSet List()
		{
			// TODO:  Add ImportPlanDataDS.List implementation
			return null;
		}

		public void Update(object pobjObjecVO)
		{
			// TODO:  Add ImportPlanDataDS.Update implementation
		}

		public void Delete(int pintID)
		{
			// TODO:  Add ImportPlanDataDS.Delete implementation
		}

		public void Add(object pobjObjectVO)
		{
			// TODO:  Add ImportPlanDataDS.Add implementation
		}

		public object GetObjectVO(int pintID)
		{
			// TODO:  Add ImportPlanDataDS.GetObjectVO implementation
			return null;
		}

		public void ExecuteCommand(string pstrSql)
		{
			const string METHOD_NAME = THIS + ".ExecuteCommand()";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(pstrSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
				ocmdPCS = null;
			}
			catch(OleDbException ex)
			{
				if (ex.Errors.Count > 1)
				{
					if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
						throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
					else
						throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME,ex);
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
	}
}

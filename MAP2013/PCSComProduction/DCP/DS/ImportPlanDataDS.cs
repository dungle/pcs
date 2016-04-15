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
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("DELETE A1", oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
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
		    OleDbConnection oconPCS =null;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				var strSql = "SELECT ProductID, WOGeneratedID, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11,"
                                + " F12, F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24,"
				                + " F25, F26, F27, F28, F29, F30, F31 FROM A1";

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, "A1");
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

		public void ExecuteCommand(string pstrSql)
		{
			const string METHOD_NAME = THIS + ".ExecuteCommand()";
			OleDbConnection oconPCS=null;
			OleDbCommand ocmdPCS =null;
			try
			{
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(pstrSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();	
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

	    public int GetGeneratedWorkOrder(int cycleOptionId, int productionLineId)
	    {
            const string METHOD_NAME = THIS + ".GetGeneratedWorkOrder()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var sqlCmd = new StringBuilder();
                sqlCmd.AppendLine("SELECT DISTINCT TOP 1 M.WorkOrderMasterID");
                sqlCmd.AppendLine("FROM PRO_WorkOrderMaster M JOIN PRO_WorkOrderDetail D");
                sqlCmd.AppendLine("ON M.WorkOrderMasterID = D.WorkOrderMasterID");
                sqlCmd.AppendLine("WHERE D.[Status] = 1");
                sqlCmd.AppendLine(string.Format("AND M.DCOptionMasterID = {0}", cycleOptionId));
                sqlCmd.AppendLine(string.Format("AND M.ProductionLineID = {0}", productionLineId));
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(sqlCmd.ToString(), oconPCS);

                ocmdPCS.Connection.Open();
                var result = ocmdPCS.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return -1;
                }
                return Convert.ToInt32(result);
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    else
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                else
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }

            catch (Exception ex)
            {
                throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
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
    }
}

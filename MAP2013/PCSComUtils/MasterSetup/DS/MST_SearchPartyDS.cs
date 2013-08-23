using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;
using System.Text;
namespace PCSComUtils.MasterSetup.DS
{
    public class MST_SearchPartyDS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public int GetRowCount(string strTable, string strFilterCondition)
        {
            const string METHOD_NAME = "" + ".List()";
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT Count(*) As Counts From " + strTable;
                if (strFilterCondition != null && strFilterCondition.Length > 0)
                    strSql += " WHERE " + strFilterCondition;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();
                ocmdPCS.CommandTimeout = 1000;
                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

                return Convert.ToInt32(dstPCS.Tables[0].Rows[0]["Counts"].ToString());
            }
            catch (OleDbException ex)
            {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strClause"></param>
        /// <returns></returns>
        public DataSet GetList(string strTableName, string strKeyWord, bool check, string strFilterConditon, int iCurrentPage, int iCount)
        {
            const string METHOD_NAME = ".GetList()";
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                string strCheck = string.Empty;
                if (check)
                    strCheck = "CAST(0 AS bit) As Checks,";
                strSql.Append("Select " + strCheck + " * From( SELECT ROW_NUMBER() OVER(ORDER BY " + strKeyWord + "," + strKeyWord + ") AS STT ,* FROM ");
                strSql.Append(strTableName);
                if (strFilterConditon.Length > 0) strSql.Append(" WHERE " + strFilterConditon);
                strSql.Append(") As " + strTableName);
                strSql.Append(" Where STT BETWEEN " + ((iCurrentPage - 1) * iCount + 1) + " AND " + iCurrentPage * iCount);

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

                return dstPCS;
            }
            catch (OleDbException ex)
            {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public DataSet GetListField(string strTable)
        {
            const string METHOD_NAME = "" + ".List()";
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = ""
                    + " SELECT     COLUMN_NAME AS Field "
                    + " FROM         INFORMATION_SCHEMA.COLUMNS "
                    + " WHERE     (TABLE_NAME = '" + strTable + "')";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MST_PartyTable.TABLE_NAME);

                return dstPCS;
            }
            catch (OleDbException ex)
            {
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

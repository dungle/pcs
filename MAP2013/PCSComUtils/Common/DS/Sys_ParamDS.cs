using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Common.DS
{
    public class Sys_ParamDS
    {
        private const string THIS = "PCSComUtils.Common.DS.Sys_ParamDS";

        #region IObjectDS Members

        public void Add(object pobjObjectVO)
        {
            const string METHOD_NAME = THIS + ".Add()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var objObject = (Sys_ParamVO)pobjObjectVO;
                string strSql = String.Empty;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO Sys_Param("
                         + Sys_ParamTable.NAME_FLD + ","
                         + Sys_ParamTable.VALUE_FLD + ")"
                         + "VALUES(?,?)";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_ParamTable.NAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_ParamTable.NAME_FLD].Value = objObject.Name;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_ParamTable.VALUE_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_ParamTable.VALUE_FLD].Value = objObject.Value;


                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
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

        public void Delete(int pintID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            string strSql = String.Empty;
            strSql = "DELETE " + Sys_ParamTable.TABLE_NAME + " WHERE  " + "ParamID" + "=" + pintID;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
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

        public object GetObjectVO(int pintID)
        {
            const string METHOD_NAME = THIS + ".GetObjectVO()";
            var dstPCS = new DataSet();

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                         + Sys_ParamTable.PARAMID_FLD + ","
                         + Sys_ParamTable.NAME_FLD + ","
                         + Sys_ParamTable.VALUE_FLD
                         + " FROM " + Sys_ParamTable.TABLE_NAME
                         + " WHERE " + Sys_ParamTable.PARAMID_FLD + "=" + pintID;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                var objObject = new Sys_ParamVO();

                while (odrPCS.Read())
                {
                    objObject.ParamID = int.Parse(odrPCS[Sys_ParamTable.PARAMID_FLD].ToString().Trim());
                    objObject.Name = odrPCS[Sys_ParamTable.NAME_FLD].ToString().Trim();
                    objObject.Value = odrPCS[Sys_ParamTable.VALUE_FLD].ToString().Trim();
                }
                return objObject;
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

        public void Update(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".Update()";

            var objObject = (Sys_ParamVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE Sys_Param SET "
                         + Sys_ParamTable.NAME_FLD + "=   ?" + ","
                         + Sys_ParamTable.VALUE_FLD + "=  ?"
                         + " WHERE " + Sys_ParamTable.PARAMID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_ParamTable.NAME_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_ParamTable.NAME_FLD].Value = objObject.Name;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_ParamTable.VALUE_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[Sys_ParamTable.VALUE_FLD].Value = objObject.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(Sys_ParamTable.PARAMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[Sys_ParamTable.PARAMID_FLD].Value = objObject.ParamID;


                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                {
                    throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                }
                else
                {
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
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

        public DataSet List()
        {
            const string METHOD_NAME = THIS + ".List()";
            var dstPCS = new DataSet();


            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                         + Sys_ParamTable.PARAMID_FLD + ","
                         + Sys_ParamTable.NAME_FLD + ","
                         + Sys_ParamTable.VALUE_FLD
                         + " FROM " + Sys_ParamTable.TABLE_NAME;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, Sys_ParamTable.TABLE_NAME);

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

        public void UpdateDataSet(DataSet pData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSet()";
            string strSql;
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            var odadPCS = new OleDbDataAdapter();

            try
            {
                strSql = "SELECT "
                         + Sys_ParamTable.PARAMID_FLD + ","
                         + Sys_ParamTable.NAME_FLD + ","
                         + Sys_ParamTable.VALUE_FLD
                         + "  FROM " + Sys_ParamTable.TABLE_NAME;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pData.EnforceConstraints = false;
                odadPCS.Update(pData, Sys_ParamTable.TABLE_NAME);
            }
            catch (OleDbException ex)
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
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
            }

            catch (InvalidOperationException ex)
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

        #endregion

        public string GetNameValue(string pstrName)
        {
            const string METHOD_NAME = THIS + ".GetNameValue()";
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                         + Sys_ParamTable.VALUE_FLD
                         + " FROM " + Sys_ParamTable.TABLE_NAME
                         + " WHERE " + Sys_ParamTable.NAME_FLD + "='" + pstrName + "'";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                object objResult = ocmdPCS.ExecuteScalar();
                if (objResult == null)
                {
                    return String.Empty;
                }
                else
                {
                    return objResult.ToString();
                }
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

        public DataSet ListParameter(int pintCCNID)
        {
            const string METHOD_NAME = THIS + ".ListParameter()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS;
            try
            {
                string strSql = "SELECT * FROM Sys_APParameters WHERE CCNID = " + pintCCNID + ";"
                                + "SELECT * FROM Sys_FinanceParamters WHERE CCNID = " + pintCCNID + ";"
                                + "SELECT * FROM Sys_PCParam WHERE CCNID = " + pintCCNID;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                var dstPCS = new DataSet();
                odadPCS.Fill(dstPCS);
                dstPCS.Tables[0].TableName = "Sys_APParameters";
                dstPCS.Tables[1].TableName = "Sys_FinanceParamters";

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
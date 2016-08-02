using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.Plan.DS
{
    public class MTR_MRPCycleOptionMasterDS
    {
        private const string THIS = "PCSComMaterials.Plan.DS.MTR_MRPCycleOptionMasterDS";
        public void Add(object pobjObjectVO)
        {
            const string METHOD_NAME = THIS + ".Add()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                MTR_MRPCycleOptionMasterVO objObject = (MTR_MRPCycleOptionMasterVO)pobjObjectVO;
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO MTR_MRPCycleOptionMaster("
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ")"
                    + "VALUES(?,?,?,?,?,?,?)";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].Value = objObject.MPSGenDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;



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
        ///    <summary>
        ///       This method uses to add data to MTR_MRPCycleOptionMaster
        ///    </summary>
        ///    <Inputs>
        ///        MTR_MRPCycleOptionMasterVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>
        public void Delete(int pintID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            string strSql = String.Empty;
            strSql = "DELETE " + MTR_MRPCycleOptionMasterTable.TABLE_NAME + " WHERE  " + "MRPCycleOptionMasterID" + "=" + pintID.ToString();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                Utils utils = new Utils();
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
        ///    <summary>
        ///       This method uses to add data to MTR_MRPCycleOptionMaster
        ///    </summary>
        ///    <Inputs>
        ///        MTR_MRPCycleOptionMasterVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
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
                strSql = "SELECT "
                    + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD
                    + " FROM " + MTR_MRPCycleOptionMasterTable.TABLE_NAME
                    + " WHERE " + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + "=" + pintID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                MTR_MRPCycleOptionMasterVO objObject = new MTR_MRPCycleOptionMasterVO();

                while (odrPCS.Read())
                {
                    objObject.MRPCycleOptionMasterID = int.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD].ToString().Trim());
                    objObject.Cycle = odrPCS[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].ToString().Trim();
                    objObject.AsOfDate = DateTime.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD].ToString().Trim());
                    if (odrPCS[MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].ToString().Trim() != string.Empty)
                        objObject.MPSGenDate = DateTime.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].ToString().Trim());
                    if (odrPCS[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].ToString().Trim() != string.Empty)
                        objObject.PlanHorizon = int.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].ToString().Trim());
                    objObject.CCNID = int.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.CCNID_FLD].ToString().Trim());
                    objObject.Description = odrPCS[MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD].ToString().Trim();
                    if (odrPCS[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim() != string.Empty)
                        objObject.MPSCycleOptionMasterID = int.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
                    if (odrPCS[MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD] != DBNull.Value)
                        objObject.IncludedRemainPO = bool.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD].ToString().Trim());
                    if (odrPCS[MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD] != DBNull.Value)
                        objObject.DaysBeforeAsOfDate = int.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD].ToString().Trim());
                    else
                        objObject.DaysBeforeAsOfDate = 0;
                    if (odrPCS[MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD] != DBNull.Value)
                        objObject.IncludedReturnToVendor = bool.Parse(odrPCS[MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD].ToString().Trim());

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
        ///    <summary>
        ///       This method uses to add data to MTR_MRPCycleOptionMaster
        ///    </summary>
        ///    <Inputs>
        ///        MTR_MRPCycleOptionMasterVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>
        public void Update(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".Update()";

            MTR_MRPCycleOptionMasterVO objObject = (MTR_MRPCycleOptionMasterVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE MTR_MRPCycleOptionMaster SET "
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD + "=   ?" + ","
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + "=  ?"
                    + " WHERE " + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].Value = objObject.MPSGenDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD].Value = objObject.IncludedRemainPO;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD].Value = objObject.IncludedReturnToVendor;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD, OleDbType.Integer));
                if (objObject.DaysBeforeAsOfDate != 0)
                {
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD].Value = objObject.DaysBeforeAsOfDate;
                }
                else
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD].Value = DBNull.Value;
                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MPSCycleOptionMasterID != 0)
                {
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
                }
                else
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;


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

        public void UpdateGenDate(int pintMrpCycleMasterID)
        {
            const string METHOD_NAME = THIS + ".UpdateGenDate()";

            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.CommandTimeout = 10000;
                strSql = "UPDATE MTR_MRPCycleOptionMaster SET "
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + "= GETDATE()"
                    + " WHERE " + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD].Value = pintMrpCycleMasterID;

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

        ///    <summary>
        ///       This method uses to add data to MTR_MRPCycleOptionMaster
        ///    </summary>
        ///    <Inputs>
        ///        MTR_MRPCycleOptionMasterVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>
        public DataSet List()
        {
            const string METHOD_NAME = THIS + ".List()";
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                    + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD
                    + " FROM " + MTR_MRPCycleOptionMasterTable.TABLE_NAME;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MTR_MRPCycleOptionMasterTable.TABLE_NAME);

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

        ///    <summary>
        ///       This method uses to add data to MTR_MRPCycleOptionMaster
        ///    </summary>
        ///    <Inputs>
        ///        MTR_MRPCycleOptionMasterVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>
        public void UpdateDataSet(DataSet pdstData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSet()";
            string strSql;
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            OleDbDataAdapter odadPCS = new OleDbDataAdapter();

            try
            {
                strSql = "SELECT "
                    + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD
                    + "  FROM " + MTR_MRPCycleOptionMasterTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pdstData.EnforceConstraints = false;
                odadPCS.Update(pdstData, MTR_MRPCycleOptionMasterTable.TABLE_NAME);

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
        public object GetObjectVO(int pintCCN, int pintCyc)
        {
            return null;
        }
        public DataSet getMPSCycle(int pintMPSCycleOptionMasterID)
        {
            return null;
        }
        /// <summary>
        /// GetMRPCycleOptionMaster
        /// </summary>
        /// <param name="pintMRPCycleOptionMasterID"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Friday, August 12 2005</date>
        public DataTable GetMRPCycleOptionMaster(int pintMRPCycleOptionMasterID)
        {
            const string METHOD_NAME = THIS + ".GetMRPCycleOptionMaster()";
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD + ","
                    + " (SELECT " + MTR_MPSCycleOptionMasterTable.CYCLE_FLD
                    + " FROM " + MTR_MPSCycleOptionMasterTable.TABLE_NAME + " MPS"
                    + " WHERE MPS." + MTR_MPSCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + " = "
                    + "MRP." + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + " )"
                    + " MPSCycleOption" + ", "
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD
                    + " FROM " + MTR_MRPCycleOptionMasterTable.TABLE_NAME + " MRP"
                    + " WHERE " + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + " = " + pintMRPCycleOptionMasterID.ToString();
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MTR_MRPCycleOptionMasterTable.TABLE_NAME);

                return dstPCS.Tables[0];
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
        /// AddAndReturnID
        /// </summary>
        /// <param name="pobjMasterData"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Thursday, August 11 2005</date>
        public int AddAndReturnID(object pobjMasterData)
        {
            DateTime dtmSpecialDate = new DateTime(2005, 1, 1);
            const string METHOD_NAME = THIS + ".Add()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                MTR_MRPCycleOptionMasterVO objObject = (MTR_MRPCycleOptionMasterVO)pobjMasterData;
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO MTR_MRPCycleOptionMaster("
                    + MTR_MRPCycleOptionMasterTable.CYCLE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.CCNID_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD + ","
                    + MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD + ")"
                    + "VALUES(?,?,?,?,?,?,?,?,?,?)"
                    + " SELECT @@IDENTITY";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.CYCLE_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.CYCLE_FLD].Value = objObject.Cycle;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.ASOFDATE_FLD].Value = objObject.AsOfDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD, OleDbType.Date));
                if (objObject.MPSGenDate != dtmSpecialDate)
                {
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].Value = objObject.MPSGenDate;
                }
                else
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSGENDATE_FLD].Value = DBNull.Value;
                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD, OleDbType.Integer));
                if (objObject.PlanHorizon != 0)
                {
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].Value = objObject.PlanHorizon;
                }
                else
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.PLANHORIZON_FLD].Value = DBNull.Value;
                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DESCRIPTION_FLD].Value = objObject.Description;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.INCLUDEDREMAINPO_FLD].Value = objObject.IncludedRemainPO;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.INCLUDEDRETURNTOVENDOR_FLD].Value = objObject.IncludedReturnToVendor;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD, OleDbType.Integer));
                if (objObject.DaysBeforeAsOfDate != 0)
                {
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD].Value = objObject.DaysBeforeAsOfDate;
                }
                else
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.DAYBEFOREASOFDATE_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MPSCycleOptionMasterID != 0)
                {
                    ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
                }
                else ocmdPCS.Parameters[MTR_MRPCycleOptionMasterTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;

                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                object objReturn = ocmdPCS.ExecuteScalar();
                if (objReturn != null)
                {
                    return int.Parse(objReturn.ToString());
                }
                else
                {
                    return 0;
                }


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
        public DateTime GetAsOfDate(int pintID, bool pblnIsMPS)
        {
            const string METHOD_NAME = THIS + ".GetAsOfDate()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;

            try
            {
                string strSql = "SELECT AsOfDate FROM ";
                if (pblnIsMPS)
                    strSql += MTR_MRPCycleOptionMasterTable.TABLE_NAME + " WHERE " + MTR_MRPCycleOptionMasterTable.MRPCYCLEOPTIONMASTERID_FLD + "=" + pintID;
                else
                    strSql += PRO_DCOptionMasterTable.TABLE_NAME + " WHERE " + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + pintID;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                return Convert.ToDateTime(ocmdPCS.ExecuteScalar());
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

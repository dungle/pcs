using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Diagnostics;

using System.Text;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComMaterials.Plan.DS
{
    public class MTR_CPODS
    {
        private const string THIS = "PCSComMaterials.Plan.DS.MTR_CPODS";

        public const string SELECT_COLUMN = "Select_Col";
        public const string LINE_NUMBER_COLUMN = "LineNumber";

        public MTR_CPODS()
        {
        }

        public void Add(object pobjObjectVO)
        {
            const string METHOD_NAME = THIS + ".Add()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                MTR_CPOVO objObject = (MTR_CPOVO)pobjObjectVO;
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO " + MTR_CPOTable.TABLE_NAME + " ("
                    + MTR_CPOTable.QUANTITY_FLD + ","
                    + MTR_CPOTable.STARTDATE_FLD + ","
                    + MTR_CPOTable.DUEDATE_FLD + ","
                    + MTR_CPOTable.REFMASTERID_FLD + ","
                    + MTR_CPOTable.REFDETAILID_FLD + ","
                    + MTR_CPOTable.REFTYPE_FLD + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ","
                    + MTR_CPOTable.CCNID_FLD + ","
                    + MTR_CPOTable.PRODUCTID_FLD + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + ","
                    + MTR_CPOTable.STOCKUMID_FLD + ","
                    + MTR_CPOTable.ISMPS_FLD + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + ","
                    + MTR_CPOTable.WOGENERATEDID_FLD + ","
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.PARENTCPOID_FLD + ","
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + ","
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + ","
                    + MTR_CPOTable.CONVERTED_FLD + ")"
                    + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.QUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.QUANTITY_FLD].Value = objObject.Quantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = objObject.StartDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = objObject.DueDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFMASTERID_FLD].Value = objObject.RefMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFDETAILID_FLD].Value = objObject.RefDetailID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFTYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFTYPE_FLD].Value = objObject.RefType;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].Value = objObject.NetAvailableQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.ISMPS_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.ISMPS_FLD].Value = objObject.IsMPS;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.POGENERATEDID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.POGENERATEDID_FLD].Value = objObject.POGeneratedID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.WOGENERATEDID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.WOGENERATEDID_FLD].Value = objObject.WOGeneratedID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MRPCycleOptionMasterID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MPSCycleOptionMasterID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PARENTCPOID_FLD, OleDbType.BigInt));
                if (objObject.ParentCPOID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = objObject.ParentCPOID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DEMANDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.DEMANDQUANTITY_FLD].Value = objObject.DemandQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CONVERTED_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.CONVERTED_FLD].Value = objObject.Converted;

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
            strSql = "DELETE " + MTR_CPOTable.TABLE_NAME + " WHERE  " + MTR_CPOTable.CPOID_FLD + " = " + pintID.ToString();
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

        public void Delete(object pobjCPOVO)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                MTR_CPOVO voCPO = (MTR_CPOVO)pobjCPOVO;
                string strSql = String.Empty;
                strSql = "DELETE " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.CCNID_FLD + "= ?"
                    + " AND " + MTR_CPOTable.DUEDATE_FLD + "= ?"
                    + " AND " + MTR_CPOTable.STARTDATE_FLD + "= ?"
                    + " AND " + MTR_CPOTable.ISMPS_FLD + "= ?"
                    + " AND " + MTR_CPOTable.MASTERLOCATIONID_FLD + "= ?"
                    + " AND " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "= ?";
                if (voCPO.ParentCPOID > 0)
                    strSql += " AND " + MTR_CPOTable.PARENTCPOID_FLD + "= ?";
                strSql += " AND " + MTR_CPOTable.PRODUCTID_FLD + "= ?"
                    + " AND " + MTR_CPOTable.QUANTITY_FLD + "= ?"
                    + " AND " + MTR_CPOTable.STOCKUMID_FLD + "= ?";
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.CCNID_FLD].Value = voCPO.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = voCPO.DueDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = voCPO.StartDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.ISMPS_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.ISMPS_FLD].Value = voCPO.IsMPS;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.MASTERLOCATIONID_FLD].Value = voCPO.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (voCPO.MPSCycleOptionMasterID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = voCPO.MPSCycleOptionMasterID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;

                if (voCPO.ParentCPOID > 0)
                {
                    ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PARENTCPOID_FLD, OleDbType.BigInt));
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = voCPO.ParentCPOID;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.PRODUCTID_FLD].Value = voCPO.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.QUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.QUANTITY_FLD].Value = voCPO.Quantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.STOCKUMID_FLD].Value = voCPO.StockUMID;

                ocmdPCS.Connection.Open();
                int intEffectRow = ocmdPCS.ExecuteNonQuery();
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

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                    + MTR_CPOTable.CPOID_FLD + ","
                    + MTR_CPOTable.QUANTITY_FLD + ","
                    + MTR_CPOTable.STARTDATE_FLD + ","
                    + MTR_CPOTable.DUEDATE_FLD + ","
                    + MTR_CPOTable.REFMASTERID_FLD + ","
                    + MTR_CPOTable.REFDETAILID_FLD + ","
                    + MTR_CPOTable.REFTYPE_FLD + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ","
                    + MTR_CPOTable.CCNID_FLD + ","
                    + MTR_CPOTable.PRODUCTID_FLD + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + ","
                    + MTR_CPOTable.STOCKUMID_FLD + ","
                    + MTR_CPOTable.ISMPS_FLD + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + ","
                    + MTR_CPOTable.WOGENERATEDID_FLD + ","
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.PARENTCPOID_FLD + ","
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + ","
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + ","
                    + MTR_CPOTable.CONVERTED_FLD
                    + " FROM " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.CPOID_FLD + "=" + pintID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                MTR_CPOVO objObject = new MTR_CPOVO();

                while (odrPCS.Read())
                {
                    objObject.CPOID = long.Parse(odrPCS[MTR_CPOTable.CPOID_FLD].ToString().Trim());
                    objObject.Quantity = Decimal.Parse(odrPCS[MTR_CPOTable.QUANTITY_FLD].ToString().Trim());
                    objObject.StartDate = DateTime.Parse(odrPCS[MTR_CPOTable.STARTDATE_FLD].ToString().Trim());
                    objObject.DueDate = DateTime.Parse(odrPCS[MTR_CPOTable.DUEDATE_FLD].ToString().Trim());
                    try
                    {
                        objObject.RefMasterID = int.Parse(odrPCS[MTR_CPOTable.REFMASTERID_FLD].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        objObject.RefDetailID = int.Parse(odrPCS[MTR_CPOTable.REFDETAILID_FLD].ToString().Trim());
                    }
                    catch { }
                    try
                    {
                        objObject.RefType = int.Parse(odrPCS[MTR_CPOTable.REFTYPE_FLD].ToString().Trim());
                    }
                    catch { }

                    objObject.NetAvailableQuantity = Decimal.Parse(odrPCS[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].ToString().Trim());
                    objObject.CCNID = int.Parse(odrPCS[MTR_CPOTable.CCNID_FLD].ToString().Trim());
                    objObject.ProductID = int.Parse(odrPCS[MTR_CPOTable.PRODUCTID_FLD].ToString().Trim());
                    objObject.MasterLocationID = int.Parse(odrPCS[MTR_CPOTable.MASTERLOCATIONID_FLD].ToString().Trim());
                    objObject.StockUMID = int.Parse(odrPCS[MTR_CPOTable.STOCKUMID_FLD].ToString().Trim());
                    objObject.IsMPS = bool.Parse(odrPCS[MTR_CPOTable.ISMPS_FLD].ToString().Trim());

                    if (!odrPCS[MTR_CPOTable.POGENERATEDID_FLD].Equals(DBNull.Value))
                    {
                        objObject.POGeneratedID = int.Parse(odrPCS[MTR_CPOTable.POGENERATEDID_FLD].ToString().Trim());
                    }
                    else
                    {
                        objObject.POGeneratedID = 0;
                    }

                    if (!odrPCS[MTR_CPOTable.WOGENERATEDID_FLD].Equals(DBNull.Value))
                    {
                        objObject.WOGeneratedID = int.Parse(odrPCS[MTR_CPOTable.WOGENERATEDID_FLD].ToString().Trim());
                    }
                    else
                    {
                        objObject.WOGeneratedID = 0;
                    }

                    if (!odrPCS[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
                    {
                        objObject.MRPCycleOptionMasterID = int.Parse(odrPCS[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].ToString().Trim());
                    }
                    else
                    {
                        objObject.MRPCycleOptionMasterID = 0;
                    }

                    if (!odrPCS[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Equals(DBNull.Value))
                    {
                        objObject.MPSCycleOptionMasterID = int.Parse(odrPCS[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].ToString().Trim());
                    }
                    else
                    {
                        objObject.MPSCycleOptionMasterID = 0;
                    }

                    if (!odrPCS[MTR_CPOTable.PARENTCPOID_FLD].Equals(DBNull.Value))
                    {
                        objObject.ParentCPOID = long.Parse(odrPCS[MTR_CPOTable.PARENTCPOID_FLD].ToString().Trim());
                    }
                    else
                    {
                        objObject.ParentCPOID = 0;
                    }

                    objObject.DemandQuantity = decimal.Parse(odrPCS[MTR_CPOTable.DEMANDQUANTITY_FLD].ToString().Trim());
                    objObject.SupplyQuantity = decimal.Parse(odrPCS[MTR_CPOTable.SUPPLYQUANTITY_FLD].ToString().Trim());
                    objObject.Converted = bool.Parse(odrPCS[MTR_CPOTable.CONVERTED_FLD].ToString().Trim());

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

            MTR_CPOVO objObject = (MTR_CPOVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE MTR_CPO SET "
                    + MTR_CPOTable.QUANTITY_FLD + "=   ?" + ","
                    + MTR_CPOTable.STARTDATE_FLD + "=   ?" + ","
                    + MTR_CPOTable.DUEDATE_FLD + "=   ?" + ","
                    + MTR_CPOTable.REFMASTERID_FLD + "=   ?" + ","
                    + MTR_CPOTable.REFDETAILID_FLD + "=   ?" + ","
                    + MTR_CPOTable.REFTYPE_FLD + "=   ?" + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + "=   ?" + ","
                    + MTR_CPOTable.CCNID_FLD + "=   ?" + ","
                    + MTR_CPOTable.PRODUCTID_FLD + "=   ?" + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + "=   ?" + ","
                    + MTR_CPOTable.STOCKUMID_FLD + "=   ?" + ","
                    + MTR_CPOTable.ISMPS_FLD + "=   ?" + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + "=?, "
                    + MTR_CPOTable.WOGENERATEDID_FLD + "= ?, "
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + "=?, "
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=?, "
                    + MTR_CPOTable.PARENTCPOID_FLD + "=?, "
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + "=?, "
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + "=?, "
                    + MTR_CPOTable.CONVERTED_FLD + "=  ?"
                    + " WHERE " + MTR_CPOTable.CPOID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.QUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.QUANTITY_FLD].Value = objObject.Quantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = objObject.StartDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = objObject.DueDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFMASTERID_FLD].Value = objObject.RefMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFDETAILID_FLD].Value = objObject.RefDetailID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFTYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFTYPE_FLD].Value = objObject.RefType;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].Value = objObject.NetAvailableQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.ISMPS_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.ISMPS_FLD].Value = objObject.IsMPS;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.POGENERATEDID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.POGENERATEDID_FLD].Value = objObject.POGeneratedID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.WOGENERATEDID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.WOGENERATEDID_FLD].Value = objObject.WOGeneratedID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MRPCycleOptionMasterID != 0)
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;
                }
                else
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MPSCycleOptionMasterID != 0)
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
                }
                else
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PARENTCPOID_FLD, OleDbType.BigInt));
                if (objObject.ParentCPOID != 0)
                {
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = objObject.ParentCPOID;
                }
                else
                {
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DEMANDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.DEMANDQUANTITY_FLD].Value = objObject.DemandQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CONVERTED_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.CONVERTED_FLD].Value = objObject.Converted;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CPOID_FLD, OleDbType.BigInt));
                ocmdPCS.Parameters[MTR_CPOTable.CPOID_FLD].Value = objObject.CPOID;

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
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                    + MTR_CPOTable.CPOID_FLD + ","
                    + MTR_CPOTable.QUANTITY_FLD + ","
                    + MTR_CPOTable.STARTDATE_FLD + ","
                    + MTR_CPOTable.DUEDATE_FLD + ","
                    + MTR_CPOTable.REFMASTERID_FLD + ","
                    + MTR_CPOTable.REFDETAILID_FLD + ","
                    + MTR_CPOTable.REFTYPE_FLD + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ","
                    + MTR_CPOTable.CCNID_FLD + ","
                    + MTR_CPOTable.PRODUCTID_FLD + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + ","
                    + MTR_CPOTable.STOCKUMID_FLD + ","
                    + MTR_CPOTable.ISMPS_FLD + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + ","
                    + MTR_CPOTable.WOGENERATEDID_FLD + ","
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.PARENTCPOID_FLD + ","
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + ","
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + ","
                    + MTR_CPOTable.CONVERTED_FLD
                    + " FROM " + MTR_CPOTable.TABLE_NAME;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MTR_CPOTable.TABLE_NAME);

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
                    + MTR_CPOTable.CPOID_FLD + ","
                    + MTR_CPOTable.QUANTITY_FLD + ","
                    + MTR_CPOTable.STARTDATE_FLD + ","
                    + MTR_CPOTable.DUEDATE_FLD + ","
                    + MTR_CPOTable.REFMASTERID_FLD + ","
                    + MTR_CPOTable.REFDETAILID_FLD + ","
                    + MTR_CPOTable.REFTYPE_FLD + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ","
                    + MTR_CPOTable.CCNID_FLD + ","
                    + MTR_CPOTable.PRODUCTID_FLD + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + ","
                    + MTR_CPOTable.STOCKUMID_FLD + ","
                    + MTR_CPOTable.ISMPS_FLD + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + ","
                    + MTR_CPOTable.WOGENERATEDID_FLD + ","
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.PARENTCPOID_FLD + ","
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + ","
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + ","
                    + MTR_CPOTable.CONVERTED_FLD
                    + "  FROM " + MTR_CPOTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odadPCS.SelectCommand.CommandTimeout = 10000;
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pdstData.EnforceConstraints = false;
                odadPCS.Update(pdstData, MTR_CPOTable.TABLE_NAME);

            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    else
                        throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                else
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
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

        /// <param name="phtbCriteria">
        /// SELECT 	CPO.Converted,
        /// CPO.CPOID,Category.Code Category,product.Code PartNumber,product.Description PartName, 
        /// product.Revision Model,um.Code UM,CPO.Quantity,CPO.StartDate,CPO.DueDate,
        /// wo.WorkOrderNo WONo,po.Code PONo,po.PurchaseOrderMasterID,wo.WorkOrderMasterID,
        /// product.ProductID,category.CategoryID
        /// FROM MTR_CPO CPO
        /// INNER JOIN ITM_Product product ON CPO.ProductID = product.ProductID
        ///	LEFT JOIN ITM_Category Category ON product.CategoryID = Category.CategoryID 
        /// LEFT JOIN MST_UnitOfMeasure um ON CPO.StockUMID = um.UnitOfMeasureID 
        /// LEFT JOIN PRO_WorkOrderMaster wo ON CPO.WOGeneratedID = wo.WorkOrderMasterID
        /// LEFT join PO_PurchaseOrderMaster po ON CPO.POGeneratedID = po.PurchaseOrderMasterID
        /// /// AND pobjCriteria....
        /// </param>
        public DataSet Search(Hashtable phtbCriteria)
        {
            const string METHOD_NAME = THIS + ".Search()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;

            try
            {
                string strSql = "SELECT DISTINCT 0 as " + LINE_NUMBER_COLUMN
                    + ", Convert(bit, 0) as " + SELECT_COLUMN
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.CONVERTED_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.CPOID_FLD
                    + ", " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
                    + ", " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " as " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.PRODUCTID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD
                    + ", " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " as " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD
                    + ", " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.CODE_FLD + " as " + MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD
                    + ", WC." + MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.QUANTITY_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.STARTDATE_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.DUEDATE_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.PARENTCPOID_FLD
                    + ", " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + " as " + PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD
                    + ", " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD
                    + ", " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + " as " + PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD
                    + ", " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.CCNID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MASTERLOCATIONID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.ISMPS_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.DEMANDQUANTITY_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.SUPPLYQUANTITY_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.REFMASTERID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.REFDETAILID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.REFTYPE_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.STOCKUMID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.POGENERATEDID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.WOGENERATEDID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD
                    + ", " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VAT_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.SPECIALTAX_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.IMPORTTAX_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.BUYINGUMID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VENDORLOCATIONID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.LISTPRICE_FLD
                + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERQUANTITY_FLD
                + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD
                    + ", ISNULL(ITM_Product.LTFixedTime,0) FixLT"
                    + ", WC." + MST_WorkCenterTable.PRODUCTIONLINEID_FLD
                    + ", '' as " + PRO_ShiftTable.SHIFTDESC_FLD
                    + ", '' as " + PRO_ShiftTable.SHIFTID_FLD
                    + ", '' as " + PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT
                    + ", '' as " + PRO_DCPResultDetailTable.ISMANUAL_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VENDORCURRENCYID_FLD
                    + ", " + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.CODE_FLD + " as " + MST_MasterLocationTable.TABLE_NAME + MST_MasterLocationTable.CODE_FLD
                    + ", (SELECT " + MST_ExchangeRateTable.TABLE_NAME + "." + MST_ExchangeRateTable.RATE_FLD + " FROM " + MST_ExchangeRateTable.TABLE_NAME + " WHERE " + MST_ExchangeRateTable.CURRENCYID_FLD + " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VENDORCURRENCYID_FLD + ") as " + MST_ExchangeRateTable.RATE_FLD

                    + " FROM " + MTR_CPOTable.TABLE_NAME

                    + " INNER JOIN " + ITM_ProductTable.TABLE_NAME
                    + " ON " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.PRODUCTID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD

                    + " LEFT JOIN " + MST_MasterLocationTable.TABLE_NAME
                    + " ON " + MST_MasterLocationTable.TABLE_NAME + "." + MST_MasterLocationTable.MASTERLOCATIONID_FLD + " = " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.MASTERLOCATIONID_FLD

                    + " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME
                    + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD

                    + " LEFT JOIN " + MST_PartyTable.TABLE_NAME
                    + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + "=" + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.PARTYID_FLD

                    + " LEFT JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
                    + " ON " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.STOCKUMID_FLD + "=" + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD

                    + " LEFT JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME
                    + " ON " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.WOGENERATEDID_FLD + "=" + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD

                    + " LEFT JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME
                    + " ON " + MTR_CPOTable.TABLE_NAME + "." + MTR_CPOTable.POGENERATEDID_FLD + "=" + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD

                    + " LEFT JOIN ( "
                    + "	Select Distinct ITM_Routing.ProductID, MST_WorkCenter.ProductionLineID, MST_WorkCenter.Code as MST_WorkCenterCode "
                    + "	From ITM_Routing inner join ITM_Product on ITM_Routing.ProductID = ITM_Product.ProductID "
                    + "	Inner join MST_WorkCenter on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID "
                    + "	Inner join PRO_ProductionLine on PRO_ProductionLine.ProductionLineID = MST_WorkCenter.ProductionLineID "
                    + "	Where MST_WorkCenter.IsMain = 1 "
                    + "	) WC on WC.ProductID = ITM_Product.ProductID ";

                //build the where clause
                string strProductionLineID = string.Empty;
                string strWhereClause = " WHERE 1=1 ";

                if (phtbCriteria != null)
                {
                    IDictionaryEnumerator myEnumerator = phtbCriteria.GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        if (!myEnumerator.Key.ToString().Equals(PRO_ProductionLineTable.TABLE_NAME + "." + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD))
                        {
                            if (!myEnumerator.Key.ToString().Equals(ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD))
                            {
                                strWhereClause += " AND (" + myEnumerator.Key.ToString().Trim() + " = '" + myEnumerator.Value + "')";
                                if (myEnumerator.Key.ToString() == PlanTypeEnum.MPS.ToString())
                                    strWhereClause += " AND (" + MTR_CPOTable.DCPUPDATED_FLD + " <> 1 )";
                            }
                            else
                                strWhereClause += " AND (" + myEnumerator.Key.ToString().Trim() + " IN (" + myEnumerator.Value + "))";
                        }
                        else
                            strProductionLineID = myEnumerator.Value.ToString();
                    }
                }

                if (strProductionLineID != string.Empty)
                    if (int.Parse(strProductionLineID) > 0)
                        strWhereClause += " AND WC.ProductionLineID = " + strProductionLineID;

                strSql += strWhereClause;
                if (phtbCriteria == null)
                    strSql += " and 1 = 0";
                strSql += " Order by " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD
                    + ", " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, MTR_CPOTable.TABLE_NAME);

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

        /// <param name="phtbCriteria">
        /// SELECT 	CPO.Converted,
        /// CPO.CPOID,Category.Code Category,product.Code PartNumber,product.Description PartName, 
        /// product.Revision Model,um.Code UM,CPO.Quantity,CPO.StartDate,CPO.DueDate,
        /// wo.WorkOrderNo WONo,po.Code PONo,po.PurchaseOrderMasterID,wo.WorkOrderMasterID,
        /// product.ProductID,category.CategoryID
        /// FROM MTR_CPO CPO
        /// INNER JOIN ITM_Product product ON CPO.ProductID = product.ProductID
        ///	LEFT JOIN ITM_Category Category ON product.CategoryID = Category.CategoryID 
        /// LEFT JOIN MST_UnitOfMeasure um ON CPO.StockUMID = um.UnitOfMeasureID 
        /// LEFT JOIN PRO_WorkOrderMaster wo ON CPO.WOGeneratedID = wo.WorkOrderMasterID
        /// LEFT join PO_PurchaseOrderMaster po ON CPO.POGeneratedID = po.PurchaseOrderMasterID
        /// /// AND pobjCriteria....
        /// </param>
        public DataSet SearchForDCP(Hashtable phtbCriteria)
        {
            const string METHOD_NAME = THIS + ".Search()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;

            try
            {
                string strSql = "SELECT DISTINCT 0 as " + LINE_NUMBER_COLUMN
                    + ", Convert(bit, 0) as " + SELECT_COLUMN
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.WOCONVERTED_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.PERCENTAGE_FLD
                    + ", " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
                    + ", " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " as " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD
                    + ", " + PRO_DCPResultMasterTable.TABLE_NAME + "." + PRO_DCPResultMasterTable.PRODUCTID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.DESCRIPTION_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.DESCRIPTION_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD + " as " + ITM_ProductTable.TABLE_NAME + ITM_ProductTable.REVISION_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VAT_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.SPECIALTAX_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.IMPORTTAX_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.BUYINGUMID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VENDORLOCATIONID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VENDORCURRENCYID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.LISTPRICE_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.LTVARIABLETIME_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERQUANTITY_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD
                    + ", " + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.CODE_FLD + " as " + MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD
                    + ", WC." + MST_WorkCenterTable.TABLE_NAME + MST_WorkCenterTable.CODE_FLD
                    + ", WC." + MST_WorkCenterTable.WORKCENTERID_FLD
                    + ", WC." + ITM_RoutingTable.ROUTINGID_FLD
                    + ", " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + " as " + PRO_WorkOrderMasterTable.TABLE_NAME + PRO_WorkOrderMasterTable.WORKORDERNO_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.QUANTITY_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.STARTTIME_FLD + " as " + MTR_CPOTable.STARTDATE_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.ENDTIME_FLD + " as " + MTR_CPOTable.DUEDATE_FLD
                    + " ,PRO_DCPResultDetail.ShiftID, PRO_Shift.ShiftDesc "
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.TOTALSECOND_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.STARTTIME_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.ENDTIME_FLD
                    + ", " + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD
                    + ", " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.CODE_FLD + " as " + PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD
                    + ", " + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD
                    + ",null as " + MTR_CPOTable.CPOID_FLD
                    + ",null as " + MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD
                    + ", " + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.CCNID_FLD
                    + ", " + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.STOCKUMID_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.WOGENERATEDID_FLD
                    + ", " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.WORKINGDATE_FLD
                    + " ,PRO_DCPResultDetail.SafetyStockAmount,"
                    + " PRO_DCPResultDetail.IsManual"

                    + ", ISNULL(ITM_Product.LTFixedTime,0) FixLT"

                    + ",null as " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD
                    + ", (SELECT " + MST_ExchangeRateTable.TABLE_NAME + "." + MST_ExchangeRateTable.RATE_FLD + " FROM " + MST_ExchangeRateTable.TABLE_NAME + " WHERE " + MST_ExchangeRateTable.CURRENCYID_FLD + " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.VENDORCURRENCYID_FLD + ") as " + MST_ExchangeRateTable.RATE_FLD
                    + ", WC." + MST_WorkCenterTable.PRODUCTIONLINEID_FLD

                    + " FROM " + PRO_DCPResultMasterTable.TABLE_NAME
                    + " INNER JOIN " + PRO_DCPResultDetailTable.TABLE_NAME
                    + " ON " + PRO_DCPResultMasterTable.TABLE_NAME + "." + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + "=" + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.DCPRESULTMASTERID_FLD

                    + " LEFT JOIN PRO_Shift ON PRO_Shift.ShiftID = PRO_DCPResultDetail.ShiftID "

                    + " INNER JOIN " + ITM_ProductTable.TABLE_NAME
                    + " ON " + PRO_DCPResultMasterTable.TABLE_NAME + "." + PRO_DCPResultMasterTable.PRODUCTID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD

                    + " LEFT JOIN " + ITM_CategoryTable.TABLE_NAME
                    + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD + "=" + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD

                    + " LEFT JOIN " + MST_UnitOfMeasureTable.TABLE_NAME
                    + " ON " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.STOCKUMID_FLD + "=" + MST_UnitOfMeasureTable.TABLE_NAME + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD

                    + " LEFT JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME
                    + " ON " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.WOGENERATEDID_FLD + "=" + PRO_WorkOrderMasterTable.TABLE_NAME + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD

                    + " LEFT JOIN " + PRO_DCOptionMasterTable.TABLE_NAME
                    + " ON " + PRO_DCOptionMasterTable.TABLE_NAME + "." + PRO_DCOptionMasterTable.DCOPTIONMASTERID_FLD + "=" + PRO_DCPResultMasterTable.TABLE_NAME + "." + PRO_DCPResultMasterTable.DCOPTIONMASTERID_FLD

                    + " LEFT JOIN " + PO_PurchaseOrderMasterTable.TABLE_NAME
                    + " ON " + PRO_DCPResultDetailTable.TABLE_NAME + "." + PRO_DCPResultDetailTable.POGENERATEDID_FLD + "=" + PO_PurchaseOrderMasterTable.TABLE_NAME + "." + PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD

                    + " LEFT JOIN ( "
                    + "	Select Distinct ITM_Routing.ProductID, MST_WorkCenter.ProductionLineID, MST_WorkCenter.Code as MST_WorkCenterCode, "
                    + " itm_routing.RoutingID, MST_WorkCenter.WorkCenterID"
                    + "	From ITM_Routing inner join ITM_Product on ITM_Routing.ProductID = ITM_Product.ProductID "
                    + "	Inner join MST_WorkCenter on MST_WorkCenter.WorkCenterID = ITM_Routing.WorkCenterID "
                    + "	Inner join PRO_ProductionLine on PRO_ProductionLine.ProductionLineID = MST_WorkCenter.ProductionLineID "
                    + "	Where MST_WorkCenter.IsMain = 1 "
                    + "	) WC on WC.ProductID = ITM_Product.ProductID";

                //build the where clause
                string strProductionLineID = string.Empty;
                string strWhereClause = " WHERE 1=1 ";
                if (phtbCriteria != null)
                {
                    IDictionaryEnumerator myEnumerator = phtbCriteria.GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        if (!myEnumerator.Key.ToString().Equals(PRO_ProductionLineTable.TABLE_NAME + "." + PRO_ProductionLineTable.PRODUCTIONLINEID_FLD))
                        {
                            if (!myEnumerator.Key.ToString().Equals(ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD))
                                strWhereClause += " AND " + myEnumerator.Key.ToString().Trim() + " = '" + myEnumerator.Value + "'";
                            else
                                strWhereClause += " AND " + myEnumerator.Key.ToString().Trim() + " IN (" + myEnumerator.Value + ")";
                        }
                        else
                            strProductionLineID = myEnumerator.Value.ToString();
                    }
                }

                if (strProductionLineID != string.Empty)
                    if (int.Parse(strProductionLineID) > 0)
                        strWhereClause += " AND WC.ProductionLineID = " + strProductionLineID;

                strSql += strWhereClause + " and " + PRO_DCPResultDetailTable.TYPE_FLD + "= 0 ";
                if (phtbCriteria == null)
                    strSql += " and 1 = 0";

                strSql += " Order by " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD
                    + ", " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.REVISION_FLD
                    + ", " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CODE_FLD;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_DCPResultDetailTable.TABLE_NAME);

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

        public void MarkCPOConverted(int pintCPOID)
        {
        }
        /// <summary>
        /// Delete all CPOs of previous regeneration process by CCN, Cycle Option MasterID, MasterLocation and Product
        /// </summary>
        public void Delete(int pintCCNID, int pintCycleOptionMasterID, bool pblnIsMPS)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "DELETE " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.CCNID_FLD + "=" + pintCCNID;
                if (pblnIsMPS)
                    strSql += " AND " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;
                else
                    strSql += " AND " + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;
            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
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

        /// <summary>
        /// Delete all CPOs of previous regeneration process by CCN, Cycle Option MasterID, MasterLocation and Product
        /// </summary>
        public void Delete(int pintCCNID, int pintCycleOptionMasterID, int pintProductID, bool pblnIsMPS)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "DELETE " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.CCNID_FLD + "=" + pintCCNID
                    + " AND " + MTR_CPOTable.PRODUCTID_FLD + "=" + pintProductID;
                if (pblnIsMPS)
                    strSql += " AND " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;
                else
                    strSql += " AND " + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;
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

        /// <summary>
        /// Delete all CPOs of previous regeneration process by CCN, Cycle Option MasterID, MasterLocation and Product
        /// </summary>
        public void Delete(int pintCCNID, int pintCycleOptionMasterID, int pintProductID, bool pblnIsMPS, long pintParentCPOID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "DELETE " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.CCNID_FLD + "=" + pintCCNID
                    + " AND " + MTR_CPOTable.PRODUCTID_FLD + "=" + pintProductID
                    + " AND " + MTR_CPOTable.PARENTCPOID_FLD + "=" + pintParentCPOID;
                if (pblnIsMPS)
                    strSql += " AND " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;
                else
                    strSql += " AND " + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + "=" + pintCycleOptionMasterID;
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
        /// <summary>
        /// DeleteByMPSCycleOptionMasterID
        /// </summary>
        /// <param name="pintMPSCycleOptionMasterID"></param>
        /// <author>Trada</author>
        /// <date>Thursday, October 20 2005</date>
        public void DeleteByMPSCycleOptionMasterID(int pintMPSCycleOptionMasterID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            string strSql = "DELETE " + MTR_CPOTable.TABLE_NAME + " WHERE  "
                + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + pintMPSCycleOptionMasterID.ToString();
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
        ///       This method uses to add data to MTR_CPO
        ///    </summary>
        ///    <Inputs>
        ///        MTR_CPOVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>

        public void Delete(int pintCCNID, int pintMRPCycleOptionMasterID, string pstrVendorID, string pstrCategoryID,
            string pstrModel, string pstrProductID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "DELETE MTR_CPO WHERE CCNID = " + pintCCNID
                    + " AND MRPCycleOptionMasterID = " + pintMRPCycleOptionMasterID
                    + " AND ProductID IN 	(SELECT ProductID FROM ITM_Product "
                    + " WHERE 1=1";
                if (pstrVendorID != null && pstrVendorID != string.Empty)
                    strSql += " AND PrimaryVendorID IN (" + pstrVendorID + ")";
                if (pstrCategoryID != null && pstrCategoryID != string.Empty)
                    strSql += " AND CategoryID IN (" + pstrCategoryID + ")";
                if (pstrModel != null && pstrModel != string.Empty)
                    strSql += " AND Revision IN (" + pstrModel + ")";
                if (pstrProductID != null && pstrProductID != string.Empty)
                    strSql += " AND ProductID IN (" + pstrProductID + ")";
                strSql += ")";
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;

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

        public void Delete(int pintCCNID, int pintMRPCycleOptionMasterID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "DELETE MTR_CPO WHERE CCNID = " + pintCCNID
                    + " AND MRPCycleOptionMasterID = " + pintMRPCycleOptionMasterID;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.Connection.Open();
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;

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

        /// <summary>
        /// Add new object and return ID
        /// </summary>
        public long AddAndReturnID(object pobjObjectVO)
        {
            const string METHOD_NAME = THIS + ".AddAndReturnID()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                MTR_CPOVO objObject = (MTR_CPOVO)pobjObjectVO;
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO " + MTR_CPOTable.TABLE_NAME + " ("
                    + MTR_CPOTable.QUANTITY_FLD + ","
                    + MTR_CPOTable.STARTDATE_FLD + ","
                    + MTR_CPOTable.DUEDATE_FLD + ","
                    + MTR_CPOTable.REFMASTERID_FLD + ","
                    + MTR_CPOTable.REFDETAILID_FLD + ","
                    + MTR_CPOTable.REFTYPE_FLD + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + ","
                    + MTR_CPOTable.CCNID_FLD + ","
                    + MTR_CPOTable.PRODUCTID_FLD + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + ","
                    + MTR_CPOTable.STOCKUMID_FLD + ","
                    + MTR_CPOTable.ISMPS_FLD + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + ","
                    + MTR_CPOTable.WOGENERATEDID_FLD + ","
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + ","
                    + MTR_CPOTable.PARENTCPOID_FLD + ","
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + ","
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + ","
                    + MTR_CPOTable.ISSAFETYSTOCK_FLD + ","
                    + MTR_CPOTable.CONVERTED_FLD + ")"
                    + "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                strSql += " ; SELECT @@IDENTITY AS NEWID";

                //Debug.WriteLine("====== NEW CPO ======");
                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.QUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.QUANTITY_FLD].Value = objObject.Quantity;
                //Debug.WriteLine("Quantity: " + objObject.Quantity.ToString());

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = objObject.StartDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = objObject.DueDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFMASTERID_FLD, OleDbType.Integer));
                if (objObject.RefMasterID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.REFMASTERID_FLD].Value = objObject.RefMasterID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.REFMASTERID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFDETAILID_FLD, OleDbType.Integer));
                if (objObject.RefDetailID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.REFDETAILID_FLD].Value = objObject.RefDetailID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.REFDETAILID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFTYPE_FLD, OleDbType.Integer));
                if (objObject.RefType > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.REFTYPE_FLD].Value = objObject.RefType;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.REFTYPE_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].Value = objObject.NetAvailableQuantity;
                //Debug.WriteLine("Net Quantity: " + objObject.NetAvailableQuantity.ToString());

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.ISMPS_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.ISMPS_FLD].Value = objObject.IsMPS;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.POGENERATEDID_FLD, OleDbType.Integer));
                if (objObject.POGeneratedID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.POGENERATEDID_FLD].Value = objObject.POGeneratedID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.POGENERATEDID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.WOGENERATEDID_FLD, OleDbType.Integer));
                if (objObject.WOGeneratedID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.WOGENERATEDID_FLD].Value = objObject.WOGeneratedID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.WOGENERATEDID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MRPCycleOptionMasterID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MPSCycleOptionMasterID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PARENTCPOID_FLD, OleDbType.BigInt));
                if (objObject.ParentCPOID > 0)
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = objObject.ParentCPOID;
                else
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DEMANDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.DEMANDQUANTITY_FLD].Value = objObject.DemandQuantity;
                //Debug.WriteLine("Demand Quantity: " + objObject.DemandQuantity.ToString());

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;
                //Debug.WriteLine("Supply Quantity: " + objObject.SupplyQuantity.ToString());

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.ISSAFETYSTOCK_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.ISSAFETYSTOCK_FLD].Value = objObject.IsSafetyStock;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CONVERTED_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.CONVERTED_FLD].Value = objObject.Converted;

                ocmdPCS.CommandText = strSql;
                ocmdPCS.Connection.Open();
                object objReturn = ocmdPCS.ExecuteScalar();
                //Debug.WriteLine("====== END CPO ======");
                if (objReturn != null)
                {
                    return long.Parse(objReturn.ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (OleDbException ex)
            {
                Debug.WriteLine(ex.Message);
                if (ex.Errors.Count > 1)
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

        /// <summary>
        /// CheckDCPResult
        /// </summary>
        /// <param name="pintMpsCycleOptionMasterID"></param>
        /// <param name="pintIsMPS"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Thursday, October 20 2005</date>
        public int CheckDCPResult(int pintMpsCycleOptionMasterID, int pintIsMPS)
        {
            const string METHOD_NAME = THIS + ".CheckDCPResult()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT COUNT(*)"
                    + " FROM " + PRO_DCPResultMasterTable.TABLE_NAME
                    + " WHERE " + PRO_DCPResultMasterTable.CPOID_FLD + " IN (SELECT "
                    + MTR_CPOTable.CPOID_FLD
                    + " FROM " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.ISMPS_FLD + " = " + pintIsMPS.ToString()
                    + " AND " + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + " = " + pintMpsCycleOptionMasterID.ToString()
                    + ")";

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                object objReturn = ocmdPCS.ExecuteScalar();
                return int.Parse(objReturn.ToString());

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

        public void SetWOMasterID(ArrayList parlCPOIDs, int pintWOMasterID)
        {
            const string METHOD_NAME = THIS + ".SetWOMasterID()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                StringBuilder strDCPDetails = new StringBuilder();
                strDCPDetails.Append(" in (0");
                for (int i = 0; i < parlCPOIDs.Count; i++)
                {
                    strDCPDetails.Append("," + parlCPOIDs[i].ToString());
                }
                strDCPDetails.Append(")");
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();
                strSql = " UPDATE MTR_CPO SET "
                    + MTR_CPOTable.WOGENERATEDID_FLD + "=" + pintWOMasterID + ","
                    + MTR_CPOTable.CONVERTED_FLD + "=  1"
                    + " WHERE " + MTR_CPOTable.CPOID_FLD + strDCPDetails;

                ocmdPCS.CommandText = strSql;
                ocmdPCS.ExecuteNonQuery();
            }
            catch (OleDbException ex)
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

        public void SetWOMasterIDForDCP(ArrayList parlDCPDetalIDs, int pintWOMasterID)
        {
            const string METHOD_NAME = THIS + ".SetWOMasterID()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                StringBuilder strDCPDetails = new StringBuilder();
                strDCPDetails.Append(" in (0");
                for (int i = 0; i < parlDCPDetalIDs.Count; i++)
                {
                    strDCPDetails.Append("," + parlDCPDetalIDs[i].ToString());
                }
                strDCPDetails.Append(")");
                string strSql = String.Empty;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();
                strSql = " UPDATE PRO_DCPResultDetail SET "
                + PRO_DCPResultDetailTable.WOGENERATEDID_FLD + "=" + pintWOMasterID + ","
                + PRO_DCPResultDetailTable.WOCONVERTED_FLD + "=  1"
                + " WHERE " + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + strDCPDetails;

                ocmdPCS.CommandText = strSql;
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

        public void SetPOMasterID(ArrayList parlCPOIDs, int pintPOMasterID)
        {
            const string METHOD_NAME = THIS + ".SetPOMasterID()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();
                for (int i = 0; i < parlCPOIDs.Count; i++)
                {
                    strSql = " UPDATE MTR_CPO SET "
                        + MTR_CPOTable.POGENERATEDID_FLD + "=" + pintPOMasterID + ","
                        + MTR_CPOTable.CONVERTED_FLD + "=  1"
                        + " WHERE " + MTR_CPOTable.CPOID_FLD + " = " + parlCPOIDs[i];

                    ocmdPCS.CommandText = strSql;
                    ocmdPCS.ExecuteNonQuery();
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

        public void SetPOMasterIDForDCPDetail(ArrayList parlCPOIDs, int pintPOMasterID)
        {
            const string METHOD_NAME = THIS + ".SetPOMasterID()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();
                for (int i = 0; i < parlCPOIDs.Count; i++)
                {
                    strSql = " UPDATE " + PRO_DCPResultDetailTable.TABLE_NAME + " SET "
                        + PRO_DCPResultDetailTable.POGENERATEDID_FLD + "=" + pintPOMasterID + ","
                        + PRO_DCPResultDetailTable.WOCONVERTED_FLD + "=  1"
                        + " WHERE " + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + " = " + parlCPOIDs[i];

                    ocmdPCS.CommandText = strSql;
                    ocmdPCS.ExecuteNonQuery();
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

        ///    <summary>
        ///       This method uses to add data to MTR_CPO
        ///    </summary>
        ///    <Inputs>
        ///        MTR_CPOVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>

        public void UpdateDataSetConverted(DataSet pdstData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSetConverted()";
            string strSql;
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            OleDbDataAdapter odadPCS = new OleDbDataAdapter();
            try
            {
                strSql = "SELECT "
                    + MTR_CPOTable.CPOID_FLD + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + ","
                    + MTR_CPOTable.WOGENERATEDID_FLD + ","
                    + MTR_CPOTable.CONVERTED_FLD
                    + "  FROM " + MTR_CPOTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pdstData.EnforceConstraints = false;
                odadPCS.Update(pdstData, MTR_CPOTable.TABLE_NAME);

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

        ///    <summary>
        ///       This method uses to add data to MTR_CPO
        ///    </summary>
        ///    <Inputs>
        ///        MTR_CPOVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>


        public void UpdateForDCP(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".Update()";

            MTR_CPOVO objObject = (MTR_CPOVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE MTR_CPO SET "
                    + MTR_CPOTable.QUANTITY_FLD + "=   ?" + ","
                    + MTR_CPOTable.STARTDATE_FLD + "=   ?" + ","
                    + MTR_CPOTable.DUEDATE_FLD + "=   ?" + ","
                    + MTR_CPOTable.REFMASTERID_FLD + "=   ?" + ","
                    + MTR_CPOTable.REFDETAILID_FLD + "=   ?" + ","
                    + MTR_CPOTable.REFTYPE_FLD + "=   ?" + ","
                    + MTR_CPOTable.NETAVAILABLEQUANTITY_FLD + "=   ?" + ","
                    + MTR_CPOTable.CCNID_FLD + "=   ?" + ","
                    + MTR_CPOTable.PRODUCTID_FLD + "=   ?" + ","
                    + MTR_CPOTable.MASTERLOCATIONID_FLD + "=   ?" + ","
                    + MTR_CPOTable.STOCKUMID_FLD + "=   ?" + ","
                    + MTR_CPOTable.ISMPS_FLD + "=   ?" + ","
                    + MTR_CPOTable.POGENERATEDID_FLD + "=?, "
                    + MTR_CPOTable.WOGENERATEDID_FLD + "= ?, "
                    + MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD + "=?, "
                    + MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD + "=?, "
                    + MTR_CPOTable.PARENTCPOID_FLD + "=?, "
                    + MTR_CPOTable.SUPPLYQUANTITY_FLD + "=?, "
                    + MTR_CPOTable.DEMANDQUANTITY_FLD + "=?, "
                    + MTR_CPOTable.CONVERTED_FLD + "=  ?,"
                    + MTR_CPOTable.DCPUPDATED_FLD + "= ?"
                    + " WHERE " + MTR_CPOTable.CPOID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.QUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.QUANTITY_FLD].Value = objObject.Quantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STARTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.STARTDATE_FLD].Value = objObject.StartDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DUEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[MTR_CPOTable.DUEDATE_FLD].Value = objObject.DueDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFMASTERID_FLD].Value = objObject.RefMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFDETAILID_FLD].Value = objObject.RefDetailID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.REFTYPE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.REFTYPE_FLD].Value = objObject.RefType;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.NETAVAILABLEQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.NETAVAILABLEQUANTITY_FLD].Value = objObject.NetAvailableQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.ISMPS_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.ISMPS_FLD].Value = objObject.IsMPS;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.POGENERATEDID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.POGENERATEDID_FLD].Value = objObject.POGeneratedID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.WOGENERATEDID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[MTR_CPOTable.WOGENERATEDID_FLD].Value = objObject.WOGeneratedID;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MRPCycleOptionMasterID != 0)
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = objObject.MRPCycleOptionMasterID;
                }
                else
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MRPCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD, OleDbType.Integer));
                if (objObject.MPSCycleOptionMasterID != 0)
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = objObject.MPSCycleOptionMasterID;
                }
                else
                {
                    ocmdPCS.Parameters[MTR_CPOTable.MPSCYCLEOPTIONMASTERID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.PARENTCPOID_FLD, OleDbType.BigInt));
                if (objObject.ParentCPOID != 0)
                {
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = objObject.ParentCPOID;
                }
                else
                {
                    ocmdPCS.Parameters[MTR_CPOTable.PARENTCPOID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DEMANDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.DEMANDQUANTITY_FLD].Value = objObject.DemandQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.SUPPLYQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[MTR_CPOTable.SUPPLYQUANTITY_FLD].Value = objObject.SupplyQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CONVERTED_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.CONVERTED_FLD].Value = objObject.Converted;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.DCPUPDATED_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[MTR_CPOTable.DCPUPDATED_FLD].Value = 1;

                ocmdPCS.Parameters.Add(new OleDbParameter(MTR_CPOTable.CPOID_FLD, OleDbType.BigInt));
                ocmdPCS.Parameters[MTR_CPOTable.CPOID_FLD].Value = objObject.CPOID;

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
        ///       This method uses to add data to PRO_DCPResultDetail
        ///    </summary>
        ///    <Inputs>
        ///        PRO_DCPResultDetailVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Monday, September 05, 2005
        ///    </History>

        public void UpdateDataSetForDCP(DataSet pdstData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSet()";
            string strSql;
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            OleDbDataAdapter odadPCS = new OleDbDataAdapter();

            try
            {
                for (int i = 0; i < pdstData.Tables[0].Rows.Count; i++)
                {
                    if (pdstData.Tables[0].Rows[i].RowState == DataRowState.Modified)
                    {
                        pdstData.Tables[0].Rows[i][PRO_DCPResultDetailTable.STARTTIME_FLD] = pdstData.Tables[0].Rows[i][MTR_CPOTable.STARTDATE_FLD];
                        pdstData.Tables[0].Rows[i][PRO_DCPResultDetailTable.ENDTIME_FLD] = pdstData.Tables[0].Rows[i][MTR_CPOTable.DUEDATE_FLD];
                    }
                }
                strSql = "SELECT "
                    + PRO_DCPResultDetailTable.DCPRESULTDETAILID_FLD + ","
                    + PRO_DCPResultDetailTable.STARTTIME_FLD + ","
                    + PRO_DCPResultDetailTable.ENDTIME_FLD + ","
                    + PRO_DCPResultDetailTable.SHIFTID_FLD + ","
                    + PRO_DCPResultDetailTable.SAFETYSTOCKAMOUNT + ","
                    + PRO_DCPResultDetailTable.TOTALSECOND_FLD + ","
                    + PRO_DCPResultDetailTable.QUANTITY_FLD
                    + "  FROM " + PRO_DCPResultDetailTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pdstData.EnforceConstraints = false;
                odadPCS.Update(pdstData, PRO_DCPResultDetailTable.TABLE_NAME);
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

        ///    <summary>
        ///       This method uses to add data to MTR_CPO
        ///    </summary>
        ///    <Inputs>
        ///        MTR_CPOVO       
        ///    </Inputs>
        ///    <Returns>
        ///       void
        ///    </Returns>
        ///    <History>
        ///       Thursday, July 21, 2005
        ///    </History>

        public DataTable ListVendorDeliveryPolicy(int pintPartyID)
        {
            const string METHOD_NAME = THIS + ".ListVendorDeliveryPolicy()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " SELECT PO_VendorDeliverySchedule.*, P.ORDERQUANTITY, P.ORDERQUANTITYMULTIPLE "
                            + " FROM " + PO_VendorDeliveryScheduleTable.TABLE_NAME
                            + " INNER JOIN ITM_Product P ON PO_VendorDeliverySchedule.ProductID = P.ProductID"
                            + " WHERE " + PO_VendorDeliveryScheduleTable.PARTYID_FLD + "=" + pintPartyID;


                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PO_VendorDeliveryScheduleTable.TABLE_NAME);
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

        public DataSet GetWorkDayCalendar()
        {
            const string METHOD_NAME = THIS + ".ListVendorDeliveryPolicy()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " SELECT WorkingDayMasterID, Sun, CCNID, Year, Mon, Tue, Wed, Thu, Fri, Sat FROM MST_WorkingDayMaster;"
                            + " SELECT WorkingDayDetailID, OffDay, Comment, WorkingDayMasterID FROM MST_WorkingDayDetail";


                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS);
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
        /// GetDCPResultMasterByProductID
        /// </summary>
        /// <param name="pintProductID"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Monday, April 24 2006</date>
        public DataSet GetDCPResultMasterByProductID(int pintProductID)
        {
            const string METHOD_NAME = THIS + ".GetDCPResultMasterByProductID()";
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                    + PRO_DCPResultMasterTable.DCPRESULTMASTERID_FLD + ","
                    + " FROM " + PRO_DCPResultMasterTable.TABLE_NAME
                    + " WHERE " + PRO_DCPResultMasterTable.PRODUCTID_FLD + " = " + pintProductID.ToString();
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_DCPResultMasterTable.TABLE_NAME);

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
        /// Delete Data by list of CPO ID
        /// </summary>
        /// <param name="pstrCPOIDs">List of CPO ID to be deleted</param>
        public void Delete(string pstrCPOIDs)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "DELETE " + MTR_CPOTable.TABLE_NAME
                    + " WHERE " + MTR_CPOTable.CPOID_FLD + " IN (" + pstrCPOIDs + ")";
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                ocmdPCS.CommandTimeout = 1000;
                ocmdPCS.ExecuteNonQuery();
                ocmdPCS = null;

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
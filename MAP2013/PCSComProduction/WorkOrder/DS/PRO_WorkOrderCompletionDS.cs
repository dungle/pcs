using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.DataAccess;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using System.Text;
using PCSComUtils.DataContext;

namespace PCSComProduction.WorkOrder.DS
{
    public class PRO_WorkOrderCompletionDS
    {
        const string PRO = " Product";
        const string UM = "UnitOfMeasure";
        const string CAPTION_UM = "UM";
        const string WOD = " WODetail";
        const string CQ = " Compqty";
        const string SQ = " Scrapqty";
        const string OPENQUANTITY = "OpenQuantity";
        const string COMPQTY = "CompletedQuantity";
        const string SCRAPQUANTITY = "ScrapQuantity";
        const string WOC = " WOCompletion";
        const string WOM = " WOMaster";
        const string LOC = " Location";
        const string BIN = " Bin";

        private const string THIS = "PCSComProduction.WorkOrder.DS.PRO_WorkOrderCompletionDS";
        public DataTable GetAvailableQtyByPostDate(DateTime pdtmPostDate)
        {
            const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm";
            const string BINAVAILABLE_FLD = "BinAvailable";

            DataTable dtbResultTable = new DataTable();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;

            if (pdtmPostDate == DateTime.MinValue)
                pdtmPostDate = (new UtilsBO()).GetDBDate();

            string strSql = "SELECT DISTINCT";
            strSql += " child.ProductID,";

            strSql += " (ISNULL(SUM(ISNULL(bc.OHQuantity, 0) - ISNULL(bc.CommitQuantity, 0)), 0)";

            strSql += " + ISNULL((SELECT SUM(CASE T.Type";
            strSql += "		WHEN 1 THEN -Quantity";
            strSql += "		WHEN 0 THEN Quantity";
            strSql += "		WHEN 2 THEN -Quantity";
            strSql += " END) Quantity";
            strSql += " FROM MST_TransactionHistory H JOIN MST_TranType T ON H.TranTypeID = T.TranTypeID";
            strSql += " WHERE T.Type IN (0,1,2)";
            strSql += " AND PostDate > '" + pdtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
            strSql += " AND LocationID = bc.LocationID";
            strSql += " AND BinID = bc.BinID";
            strSql += " AND ProductID = bc.ProductID),0)";

            strSql += " ) as " + BINAVAILABLE_FLD;
            strSql += " , bc.ProductID";
            strSql += " , bc.BinID";
            strSql += " , bc.LocationID";
            strSql += " ,PRO_ProductionLine.ProductionLineID";

            strSql += " FROM PRO_WorkOrderDetail woi";
            strSql += " INNER JOIN ITM_BOM bom ON woi.ProductID = bom.ProductID";
            strSql += " INNER JOIN ITM_Product child ON bom.ComponentID = child.ProductID";
            strSql += " INNER JOIN IV_BinCache bc ON bc.ProductID = child.ProductID";
            strSql += " INNER JOIN MST_BIN ON MST_BIN.BinID = bc.BinID";
            strSql += " INNER JOIN PRO_ProductionLine ON PRO_ProductionLine.LocationID = bc.LocationID";

            strSql += " WHERE MST_BIN.BinTypeID = " + (int)BinTypeEnum.IN;
            //strSql += " AND PRO_ProductionLine.ProductionLineID = " + pintProductionLineID;
            //strSql += " AND woi.WorkOrderDetailID = " + pintWODetailId;

            strSql += " GROUP BY bc.ProductID,  bc.BinID,  bc.LocationID,";
            strSql += " bc.CCNID,  bc.MasterLocationID, child.ProductID,PRO_ProductionLine.ProductionLineID";

            oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
            ocmdPCS = new OleDbCommand(strSql, oconPCS);
            ocmdPCS.CommandTimeout = 10000;

            ocmdPCS.Connection.Open();
            OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
            odadPCS.Fill(dtbResultTable);

            return dtbResultTable;
        }
        public DataTable GetAvailableQuantityByProductionLinePostDate(DateTime pdtmPostDate, int pintProductionLineID, int pintWODetailId)
        {
            const string SQL_DATETIME_FORMAT = "yyyy-MM-dd HH:mm";
            const string BINAVAILABLE_FLD = "BinAvailable";

            DataTable dtbResultTable = new DataTable();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;

            if (pdtmPostDate == DateTime.MinValue)
                pdtmPostDate = (new UtilsBO()).GetDBDate();

            string strSql = "SELECT DISTINCT";
            strSql += " child.ProductID,";

            strSql += " (ISNULL(SUM(ISNULL(bc.OHQuantity, 0) - ISNULL(bc.CommitQuantity, 0)), 0)";

            strSql += " + ISNULL((SELECT SUM(CASE T.Type";
            strSql += "		WHEN 1 THEN -Quantity";
            strSql += "		WHEN 0 THEN Quantity";
            strSql += "		WHEN 2 THEN -Quantity";
            strSql += " END) Quantity";
            strSql += " FROM MST_TransactionHistory H JOIN MST_TranType T ON H.TranTypeID = T.TranTypeID";
            strSql += " WHERE T.Type IN (0,1,2)";
            strSql += " AND PostDate > '" + pdtmPostDate.ToString(SQL_DATETIME_FORMAT) + "'";
            strSql += " AND LocationID = bc.LocationID";
            strSql += " AND BinID = bc.BinID";
            strSql += " AND ProductID = bc.ProductID),0)";

            strSql += " ) as " + BINAVAILABLE_FLD;
            strSql += " , bc.ProductID";
            strSql += " , bc.BinID";
            strSql += " , bc.LocationID";

            strSql += " FROM PRO_WorkOrderDetail woi";
            strSql += " INNER JOIN ITM_BOM bom ON woi.ProductID = bom.ProductID";
            strSql += " INNER JOIN ITM_Product child ON bom.ComponentID = child.ProductID";
            strSql += " INNER JOIN IV_BinCache bc ON bc.ProductID = child.ProductID";
            strSql += " INNER JOIN MST_BIN ON MST_BIN.BinID = bc.BinID";
            strSql += " INNER JOIN PRO_ProductionLine ON PRO_ProductionLine.LocationID = bc.LocationID";

            strSql += " WHERE MST_BIN.BinTypeID = " + (int)BinTypeEnum.IN;
            strSql += " AND PRO_ProductionLine.ProductionLineID = " + pintProductionLineID;
            strSql += " AND woi.WorkOrderDetailID = " + pintWODetailId;

            strSql += " GROUP BY bc.ProductID,  bc.BinID,  bc.LocationID,";
            strSql += " bc.CCNID,  bc.MasterLocationID, child.ProductID";

            Utils utils = new Utils();
            oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
            ocmdPCS = new OleDbCommand(strSql, oconPCS);
            ocmdPCS.CommandTimeout = 10000;

            ocmdPCS.Connection.Open();
            OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
            odadPCS.Fill(dtbResultTable);

            return dtbResultTable;
        }

        public void Add(PRO_WorkOrderCompletion objObject)
        {
            const string METHOD_NAME = THIS + ".Add()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand("", oconPCS);

                strSql = "INSERT INTO PRO_WorkOrderCompletion("
                    + PRO_WorkOrderCompletionTable.POSTDATE_FLD + ","
                    + PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + ","
                    + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOT_FLD + ","
                    + PRO_WorkOrderCompletionTable.SERIAL_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.BINID_FLD + ","
                    + PRO_WorkOrderCompletionTable.CCNID_FLD + ","
                    + PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderCompletionTable.SHIFTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + ","
                    + PRO_WorkOrderCompletionTable.REMARK_FLD + ","
                    + PRO_WorkOrderCompletionTable.QASTATUS_FLD + ")"
                    + "VALUES(?, ?, ?, ?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.POSTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.POSTDATE_FLD].Value = objObject.PostDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].Value = objObject.WOCompletionNo;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].Value = objObject.CompletedQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.LOT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOT_FLD].Value = objObject.Lot;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.SERIAL_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SERIAL_FLD].Value = objObject.Serial;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.LOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOCATIONID_FLD].Value = objObject.LocationID;


                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.BINID_FLD, OleDbType.Integer));
                if (objObject.BinID != null)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.BINID_FLD].Value = objObject.BinID;
                }
                else
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.BINID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

                //HACKED: Added by Tuan TQ. 09 Dec, 2005: Add more properties
                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.SHIFTID_FLD, OleDbType.Integer));
                if (objObject.ShiftID != null & objObject.ShiftID > 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Value = objObject.ShiftID;
                }
                else
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
                if (objObject.IssuePurposeID != null && objObject.IssuePurposeID > 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;
                }
                else
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.REMARK_FLD, OleDbType.WChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.REMARK_FLD].Value = objObject.Remark;

                //End Hacked
                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.QASTATUS_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.QASTATUS_FLD].Value = objObject.QAStatus;



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
        /// <summary>
        /// AddAndReturnID
        /// </summary>
        /// <param name="pobjObjectVO"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Friday, August 19 2005</date>
        public int AddAndReturnID(PRO_WorkOrderCompletion objObject)
        {
            const string METHOD_NAME = THIS + ".AddAndReturnID()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(string.Empty, oconPCS);
                ocmdPCS.CommandTimeout = 10000;

                StringBuilder strSql = new StringBuilder("INSERT INTO PRO_WorkOrderCompletion(");
                strSql.Append(PRO_WorkOrderCompletionTable.POSTDATE_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.WOCOMPLETIONDAT_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.LOT_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.SERIAL_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.LOCATIONID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.BINID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.CCNID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.PRODUCTID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.STOCKUMID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.SHIFTID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.REMARK_FLD + ",");
                //strSql.Append("TransNo" + ",");
                strSql.Append(PRO_WorkOrderCompletionTable.QASTATUS_FLD + ")");
                strSql.Append("VALUES(?, ?, ?, ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?) SELECT @@IDENTITY");

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.POSTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.POSTDATE_FLD].Value = objObject.PostDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WOCOMPLETIONDAT_FLD, OleDbType.Date));
                if (objObject.CompletedDate != null && objObject.CompletedDate != DateTime.MinValue)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WOCOMPLETIONDAT_FLD].Value = objObject.CompletedDate;
                }
                else
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WOCOMPLETIONDAT_FLD].Value = DBNull.Value;
                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].Value = objObject.WOCompletionNo;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].Value = objObject.CompletedQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.LOT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOT_FLD].Value = objObject.Lot;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.SERIAL_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SERIAL_FLD].Value = objObject.Serial;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.LOCATIONID_FLD, OleDbType.Integer));
                if (objObject.LocationID != null && objObject.LocationID > 0)
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOCATIONID_FLD].Value = objObject.LocationID;
                else
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOCATIONID_FLD].Value = DBNull.Value;
                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.BINID_FLD, OleDbType.Integer));
                if (objObject.BinID != 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.BINID_FLD].Value = objObject.BinID;
                }
                else
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.BINID_FLD].Value = DBNull.Value;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

                //HACKED: Added by Tuan TQ. 09 Dec, 2005: Add more properties
                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.SHIFTID_FLD, OleDbType.Integer));
                if (objObject.ShiftID > 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Value = objObject.ShiftID;
                }
                else
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
                if (objObject.IssuePurposeID > 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;
                }
                else
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.REMARK_FLD, OleDbType.WChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.REMARK_FLD].Value = objObject.Remark;

                //End Hacked

                //ocmdPCS.Parameters.Add(new OleDbParameter("TransNo", OleDbType.WChar));
                //ocmdPCS.Parameters["TransNo"].Value = objObject.TransNo;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.QASTATUS_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.QASTATUS_FLD].Value = objObject.QAStatus;

                ocmdPCS.CommandText = strSql.ToString();
                ocmdPCS.Connection.Open();
                object objResult = ocmdPCS.ExecuteScalar();
                if ((objResult != null) && (objResult != DBNull.Value))
                {
                    return int.Parse(objResult.ToString());
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
            return 0;
        }
        public void Delete(int pintID)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            string strSql = String.Empty;
            strSql = "DELETE " + PRO_WorkOrderCompletionTable.TABLE_NAME + " WHERE  " + "WorkOrderCompletionID" + "=" + pintID.ToString();
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

        public PRO_WorkOrderCompletion GetObjectVO(int pintID)
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
                    + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.POSTDATE_FLD + ","
                    + PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + ","
                    + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOT_FLD + ","
                    + PRO_WorkOrderCompletionTable.SERIAL_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.BINID_FLD + ","
                    + PRO_WorkOrderCompletionTable.CCNID_FLD + ","
                    + PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderCompletionTable.SHIFTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + ","
                    + PRO_WorkOrderCompletionTable.REMARK_FLD + ","
                    + PRO_WorkOrderCompletionTable.QASTATUS_FLD
                    + " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME
                    + " WHERE " + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + "=" + pintID;

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                PRO_WorkOrderCompletion objObject = new PRO_WorkOrderCompletion();

                while (odrPCS.Read())
                {
                    objObject.WorkOrderCompletionID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD].ToString().Trim());
                    objObject.PostDate = DateTime.Parse(odrPCS[PRO_WorkOrderCompletionTable.POSTDATE_FLD].ToString().Trim());
                    objObject.WOCompletionNo = odrPCS[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].ToString().Trim();
                    objObject.CompletedQuantity = Decimal.Parse(odrPCS[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].ToString().Trim());
                    objObject.Lot = odrPCS[PRO_WorkOrderCompletionTable.LOT_FLD].ToString().Trim();
                    objObject.Serial = odrPCS[PRO_WorkOrderCompletionTable.SERIAL_FLD].ToString().Trim();
                    objObject.LocationID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.LOCATIONID_FLD].ToString().Trim());
                    objObject.CCNID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.CCNID_FLD].ToString().Trim());
                    objObject.MasterLocationID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD].ToString().Trim());
                    objObject.ProductID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.PRODUCTID_FLD].ToString().Trim());
                    objObject.StockUMID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.STOCKUMID_FLD].ToString().Trim());
                    objObject.WorkOrderMasterID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD].ToString().Trim());
                    objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].ToString().Trim());

                    //HACKED: edit by Tuan TQ, 09 Dec, 2005: Valid data before parse
                    if (!odrPCS[PRO_WorkOrderCompletionTable.BINID_FLD].Equals(DBNull.Value))
                    {
                        objObject.BinID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.BINID_FLD].ToString().Trim());
                    }
                    if (!odrPCS[PRO_WorkOrderCompletionTable.QASTATUS_FLD].Equals(DBNull.Value))
                    {
                        objObject.QAStatus = byte.Parse(odrPCS[PRO_WorkOrderCompletionTable.QASTATUS_FLD].ToString().Trim());
                    }
                    //HACKED: Added by Tuan TQ. 09 Dec, 2005. Add more properties
                    if (!odrPCS[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Equals(DBNull.Value))
                    {
                        objObject.ShiftID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.SHIFTID_FLD].ToString().Trim());
                    }

                    if (!odrPCS[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Equals(DBNull.Value))
                    {
                        objObject.IssuePurposeID = int.Parse(odrPCS[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].ToString().Trim());
                    }

                    objObject.Remark = odrPCS[PRO_WorkOrderCompletionTable.REMARK_FLD].ToString().Trim();

                    //End hacked

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

            PRO_WorkOrderCompletionVO objObject = (PRO_WorkOrderCompletionVO)pobjObjecVO;


            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE PRO_WorkOrderCompletion SET "
                    + PRO_WorkOrderCompletionTable.POSTDATE_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.LOT_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.SERIAL_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.LOCATIONID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.BINID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.CCNID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.PRODUCTID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.STOCKUMID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + "=   ?" + ","
                    + PRO_WorkOrderCompletionTable.SHIFTID_FLD + " = ?, "
                    + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + " =?, "
                    + PRO_WorkOrderCompletionTable.REMARK_FLD + " = ?, "
                    + PRO_WorkOrderCompletionTable.QASTATUS_FLD + "=  ?"
                    + " WHERE " + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.POSTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.POSTDATE_FLD].Value = objObject.PostDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].Value = objObject.WOCompletionNo;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].Value = objObject.CompletedQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.LOT_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOT_FLD].Value = objObject.Lot;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.SERIAL_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SERIAL_FLD].Value = objObject.Serial;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.LOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.LOCATIONID_FLD].Value = objObject.LocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.BINID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.BINID_FLD].Value = objObject.BinID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.CCNID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.CCNID_FLD].Value = objObject.CCNID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

                //HACKED: Added by Tuan TQ. 09 Dec, 2005: Add more properties
                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.SHIFTID_FLD, OleDbType.Integer));
                if (objObject.ShiftID > 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Value = objObject.ShiftID;
                }
                else
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.SHIFTID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD, OleDbType.Integer));
                if (objObject.IssuePurposeID > 0)
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Value = objObject.IssuePurposeID;
                }
                else
                {
                    ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD].Value = DBNull.Value;
                }

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.REMARK_FLD, OleDbType.WChar));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.REMARK_FLD].Value = objObject.Remark;

                //End Hacked

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.QASTATUS_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.QASTATUS_FLD].Value = objObject.QAStatus;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD].Value = objObject.WorkOrderCompletionID;


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
                    + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.POSTDATE_FLD + ","
                    + PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + ","
                    + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOT_FLD + ","
                    + PRO_WorkOrderCompletionTable.SERIAL_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.BINID_FLD + ","
                    + PRO_WorkOrderCompletionTable.CCNID_FLD + ","
                    + PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderCompletionTable.SHIFTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + ","
                    + PRO_WorkOrderCompletionTable.REMARK_FLD + ","
                    + PRO_WorkOrderCompletionTable.QASTATUS_FLD
                    + " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderCompletionTable.TABLE_NAME);

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
            OleDbDataAdapter odadPCS = new OleDbDataAdapter();

            try
            {
                strSql = "SELECT "
                    + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.POSTDATE_FLD + ","
                    + PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + ","
                    + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOT_FLD + ","
                    + PRO_WorkOrderCompletionTable.SERIAL_FLD + ","
                    + PRO_WorkOrderCompletionTable.LOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.BINID_FLD + ","
                    + PRO_WorkOrderCompletionTable.CCNID_FLD + ","
                    + PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + ","
                    + PRO_WorkOrderCompletionTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderCompletionTable.SHIFTID_FLD + ","
                    + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + ","
                    + PRO_WorkOrderCompletionTable.REMARK_FLD + ","
                    + PRO_WorkOrderCompletionTable.QASTATUS_FLD
                    + "  FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pData.EnforceConstraints = false;
                odadPCS.Update(pData, PRO_WorkOrderCompletionTable.TABLE_NAME);

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
        /// <summary>
        /// get all completed quantity of a WorkOrderDetail = 
        /// SELECT ISNULL(SUM(CompletedQuantity),0) 
        /// FROM PRO_WorkOrderCompletion 
        /// WHERE WorkOrderMasterID = pintWorkOrderMasterID 
        /// 	AND WorkOrderDetailID = pintWorkOrderDetailID
        /// </summary>
        /// <returns>CompletedQuantity</returns>
        public decimal GetCompletedQuantity(int pintWorkOrderMasterID, int pintWorkOrderDetailID)
        {
            const string METHOD_NAME = THIS + ".GetCompletedQuantity()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);

                string strSql = "SELECT ISNULL(SUM(" + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + "), 0)";
                strSql += " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME;
                strSql += " WHERE " + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + "=" + pintWorkOrderMasterID;
                strSql += " AND " + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + "=" + pintWorkOrderDetailID;

                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                object objResult = ocmdPCS.ExecuteScalar();

                if ((objResult != null) && (objResult != DBNull.Value))
                {
                    return decimal.Parse(objResult.ToString());
                }

                return 0;
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
        /// <summary>
        /// Get WO completion detail to load on form 
        /// </summary>
        /// <param name="pintWOCompletionID"></param>
        /// <returns></returns>
        public DataRow GetWOCompletion(int pintWOCompletionID)
        {
            const string LOCATION_CODE = "LocationCode";
            const string BIN_CODE = "BinCode";
            const string METHOD_NAME = THIS + ".GetWOCompletion()";
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.POSTDATE_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.WOCOMPLETIONDAT_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.LOT_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.SERIAL_FLD + ","
                    + LOC + Constants.DOT + MST_LocationTable.CODE_FLD + Constants.WHITE_SPACE + LOCATION_CODE + ","
                    + BIN + Constants.DOT + MST_BINTable.CODE_FLD + Constants.WHITE_SPACE + BIN_CODE + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.CCNID_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.PRODUCTID_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.STOCKUMID_FLD + ","
                    + WOM + Constants.DOT + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ","
                    + WOD + Constants.DOT + PRO_WorkOrderDetailTable.LINE_FLD + ","

                    //Added by Tuan TQ. 29 Dec, 2005. Get WO Detail ID for printing BOM shortage
                    + WOD + Constants.DOT + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    //End added

                    //Added by Tuan TQ. 09 Dec, 2005. Add properties
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.SHIFTID_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD + ","
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.REMARK_FLD + ","
                    + PRO_ShiftTable.TABLE_NAME + Constants.DOT + PRO_ShiftTable.SHIFTDESC_FLD + " as " + PRO_ShiftTable.TABLE_NAME + PRO_ShiftTable.SHIFTDESC_FLD + ","
                    + PRO_IssuePurposeTable.TABLE_NAME + Constants.DOT + PRO_IssuePurposeTable.DESCRIPTION_FLD + " as " + PRO_IssuePurposeTable.TABLE_NAME + PRO_IssuePurposeTable.DESCRIPTION_FLD + ","
                    //End added

                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.QASTATUS_FLD
                    + " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME + WOC
                    + " INNER JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME + WOM + " ON "
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.WORKORDERMASTERID_FLD + " = " + WOM + Constants.DOT + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD
                    + " INNER JOIN " + PRO_WorkOrderDetailTable.TABLE_NAME + WOD + " ON "
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + " = " + WOD + Constants.DOT + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD
                    + " INNER JOIN " + MST_LocationTable.TABLE_NAME + LOC + " ON "
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.LOCATIONID_FLD + " = " + LOC + Constants.DOT + MST_LocationTable.LOCATIONID_FLD
                    + " LEFT JOIN " + MST_BINTable.TABLE_NAME + BIN + " ON "
                    + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.BINID_FLD + " = " + BIN + Constants.DOT + MST_BINTable.BINID_FLD

                    //Added by Tuan TQ. 09 Dec, 2005
                    + " LEFT JOIN " + PRO_ShiftTable.TABLE_NAME
                    + " ON " + PRO_ShiftTable.TABLE_NAME + Constants.DOT + PRO_ShiftTable.SHIFTID_FLD + "=" + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.SHIFTID_FLD

                    + " LEFT JOIN " + PRO_IssuePurposeTable.TABLE_NAME
                    + " ON " + PRO_IssuePurposeTable.TABLE_NAME + Constants.DOT + PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD + "=" + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.ISSUEPURPOSEID_FLD
                    //End added

                    + " WHERE " + WOC + Constants.DOT + PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD + " = " + pintWOCompletionID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderCompletionTable.TABLE_NAME);

                return dstPCS.Tables[0].Rows[0];
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
        /// GetWorkingTimeByProductionLineAndYearMonth
        /// </summary>
        /// <param name="pintProductionLineID"></param>
        /// <param name="pintYear"></param>
        /// <param name="pintMonth"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Wednesday, July 19 2006</date>
        public DataSet GetWorkingTimeByProductionLineAndYearMonth(int pintProductionLineID, int pintYear, int pintMonth)
        {
            const string METHOD_NAME = THIS + ".GetWorkingTimeByProductionLineAndYearMonth()";
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                    + PCSComUtils.Common.PRO_WorkingTime.WORKINGTIMEID_FLD + ", "
                    + PCSComUtils.Common.PRO_WorkingTime.ENDTIME_FLD + ", "
                    + PCSComUtils.Common.PRO_WorkingTime.STARTTIME_FLD + ", "
                    + PCSComUtils.Common.PRO_WorkingTime.YEARSETUP_FLD + ", "
                    + PCSComUtils.Common.PRO_WorkingTime.MONTHSETUP_FLD + ", "
                    + PCSComUtils.Common.PRO_WorkingTime.PRODUCTIONLINEID_FLD + ", "
                    + PCSComUtils.Common.PRO_WorkingTime.WORKINGHOURS_FLD
                    + " FROM " + PCSComUtils.Common.PRO_WorkingTime.TABLE_NAME
                    + " WHERE " + PCSComUtils.Common.PRO_WorkingTime.PRODUCTIONLINEID_FLD + " = " + pintProductionLineID.ToString()
                    + " AND " + PCSComUtils.Common.PRO_WorkingTime.YEARSETUP_FLD + " = " + pintYear.ToString()
                    + " AND " + PCSComUtils.Common.PRO_WorkingTime.MONTHSETUP_FLD + " = " + pintMonth.ToString();

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PCSComUtils.Common.PRO_WorkingTime.TABLE_NAME);

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
        /// LoadDataForWOLine
        /// </summary>
        /// <param name="pintWODetailID"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        public DataSet LoadDataForWOLine(int pintWODetailID)
        {
            const string METHOD_NAME = THIS + ".LoadDataForWOLine()";
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = "SELECT "
                            + PRO + "." + ITM_ProductTable.PRODUCTID_FLD + ", "
                            + PRO + "." + ITM_ProductTable.CODE_FLD + ", "
                            + UM + "." + MST_UnitOfMeasureTable.CODE_FLD + Constants.WHITE_SPACE + CAPTION_UM + ", "
                            + PRO + "." + ITM_ProductTable.DESCRIPTION_FLD + ", "
                            + PRO + "." + ITM_ProductTable.REVISION_FLD + ", "
                            + PRO + "." + ITM_ProductTable.QASTATUS_FLD + ", "
                            + PRO + "." + ITM_ProductTable.LOTCONTROL_FLD + ", "
                            + PRO + "." + ITM_ProductTable.LOTSIZE_FLD + ", "
                            + WOD + "." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ", "
                            + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ", "
                            + " ISNULL(( " + WOD + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + " - "
                            + CQ + "." + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + " - "
                            + SQ + "." + PRO_AOScrapDetailTable.SCRAPQUANTITY_FLD + "),0) " + OPENQUANTITY
                            + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME + Constants.WHITE_SPACE + WOD
                            + " INNER JOIN " + ITM_ProductTable.TABLE_NAME + Constants.WHITE_SPACE + PRO
                            + " ON " + WOD + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + " = " + PRO + "." + ITM_ProductTable.PRODUCTID_FLD
                            + " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + Constants.WHITE_SPACE + UM
                            + " ON " + WOD + "." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + " = " + UM + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
                            + " LEFT JOIN ("
                            + " SELECT " + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD + ", "
                            + " SUM(" + PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD + ") " + COMPQTY
                            + " FROM " + PRO_WorkOrderCompletionTable.TABLE_NAME
                            + " GROUP BY " + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD
                            + " ) " + CQ + " ON " + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = "
                            + CQ + "." + PRO_WorkOrderCompletionTable.WORKORDERDETAILID_FLD
                            + " LEFT JOIN ( "
                            + " SELECT " + PRO_AOScrapDetailTable.WORKORDERDETAILID_FLD + ", "
                            + " SUM(" + PRO_AOScrapDetailTable.SCRAPQUANTITY_FLD + ") " + SCRAPQUANTITY
                            + " FROM " + PRO_AOScrapDetailTable.TABLE_NAME
                            + " GROUP BY " + PRO_AOScrapDetailTable.WORKORDERDETAILID_FLD
                            + " ) " + SQ + " ON " + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = " + SQ + "." + PRO_AOScrapDetailTable.WORKORDERDETAILID_FLD
                            + " WHERE " + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = " + pintWODetailID;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderCompletionTable.TABLE_NAME);

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
        /// Searches the work order line.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="masterLocationId">The master location id.</param>
        /// <param name="workOrderMasterId">The work order master id.</param>
        /// <param name="productCondition"></param>
        /// <returns></returns>
        public DataTable SearchWorkOrderLine(DateTime fromDate, DateTime toDate, int masterLocationId, int workOrderMasterId, string productCondition)
        {
            const string METHOD_NAME = THIS + ".SearchWorkOrderLine()";
            var dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS;
            try
            {
                var commandText = new StringBuilder();
                commandText.AppendLine(" SELECT TOP 20 0 'No', WOD.Line,");
                commandText.AppendLine(" ITM_Category.Code AS ITM_CategoryCode, ITM_Product.Code, ITM_Product.Description, ITM_Product.Revision, UM.Code MST_UnitOfMeasureCode,");
                commandText.AppendLine(" WOD.StartDate, WOD.DueDate, WOD.OrderQuantity - SUM(ISNULL(WOC.CompletedQuantity,0)) CompletedQuantity,");
                commandText.AppendLine(" WOD.OrderQuantity, L.Code MST_LocationCode, B.Code MST_BINCode, '' WOCompletionNo, WOM.WorkOrderNo, '' Remark,");
                commandText.AppendLine(" WOD.WorkOrderDetailID, WOD.WorkOrderMasterID, WOD.ProductID, WOD.StockUMID,");
                commandText.AppendLine(" ITM_Product.MasterLocationID, PL.LocationID, B.BinID, ITM_Product.LotSize, ITM_Product.LotControl,");
                commandText.AppendLine(" L.Bin, B.BinTypeID, ITM_Product.CategoryID, CAST(NULL AS DATETIME) CompletedDate, WOM.ProductionLineID");
                commandText.AppendLine(" FROM PRO_WorkOrderDetail WOD JOIN PRO_WorkOrderMaster WOM ON WOD.WorkOrderMasterID = WOM.WorkOrderMasterID");
                commandText.AppendLine(" INNER JOIN ITM_Product ON WOD.ProductID = ITM_Product.ProductID ");
                commandText.AppendLine(" INNER JOIN MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = WOD.StockUMID ");
                commandText.AppendLine(" LEFT JOIN ITM_Category ON ITM_Product.CategoryID = ITM_Category.CategoryID");
                commandText.AppendLine(" LEFT JOIN PRO_ProductionLine PL ON WOM.ProductionLineID = PL.ProductionLineID");
                commandText.AppendLine(" LEFT JOIN MST_Location L ON L.LocationID = PL.LocationID ");
                commandText.AppendLine(" LEFT JOIN MST_BIN B ON B.LocationID = L.LocationID");
                commandText.AppendLine(" AND B.BinTypeID = " + (int)BinTypeEnum.OK);
                commandText.AppendLine(" LEFT JOIN PRO_WorkOrderCompletion WOC ON WOD.WorkOrderDetailID = WOC.WorkOrderDetailID");
                commandText.AppendLine(" WHERE   WOD.Status = 2");
                commandText.AppendLine(" AND DueDate >= ?");
                commandText.AppendLine(" AND DueDate <= ?");
                commandText.AppendLine(" AND WOM.MasterLocationID = " + masterLocationId);
                commandText.AppendLine(" AND WOD.WorkOrderMasterID = " + workOrderMasterId);
                if (!string.IsNullOrEmpty(productCondition))
                {
                    commandText.AppendLine(productCondition);
                }
                commandText.AppendLine(" GROUP BY WOD.WorkOrderMasterID, WOM.WorkOrderNo, WOD.WorkOrderDetailID, WOD.StartDate, WOD.DueDate,");
                commandText.AppendLine(" WOD.ProductID, WOD.OrderQuantity, WOD.Line, WOD.Status, ITM_Category.Code,");
                commandText.AppendLine(" WOD.StockUMID, UM.Code, L.Code, B.Code, ITM_Product.Code, ITM_Product.Description, WOM.ProductionLineID,");
                commandText.AppendLine(" ITM_Product.Revision, ITM_Product.QAStatus, ITM_Product.MasterLocationID, PL.LocationID, B.BinID, ITM_Product.LotSize, ITM_Product.LotControl,");
                commandText.AppendLine(" L.Bin, B.BinTypeID, ITM_Product.CategoryID");
                commandText.AppendLine(" HAVING SUM(ISNULL(WOC.CompletedQuantity,0)) < WOD.OrderQuantity");
                commandText.AppendLine(" ORDER BY WOD.DueDate");
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(commandText.ToString(), oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("@FromDate", OleDbType.Date)).Value = fromDate;
                ocmdPCS.Parameters.Add(new OleDbParameter("@ToDate", OleDbType.Date)).Value = toDate;
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderCompletionTable.TABLE_NAME);

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
        /// Searches the work order line.
        /// </summary>
        /// <param name="multiTransNo">The multi trans no.</param>
        /// <returns></returns>
        public DataTable SearchWorkOrderLine(string multiTransNo)
        {
            const string METHOD_NAME = THIS + ".SearchWorkOrderLine()";
            var dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS;
            try
            {
                var commandText = new StringBuilder();
                commandText.AppendLine(" SELECT TOP 20 0 AS 'No', WOD.Line,");
                commandText.AppendLine(" ITM_Category.Code AS ITM_CategoryCode, P.Code, P.Description, P.Revision, UM.Code MST_UnitOfMeasureCode,");
                commandText.AppendLine(" WOD.StartDate, WOD.DueDate, WOC.CompletedQuantity, WOD.OrderQuantity, L.Code MST_LocationCode, B.Code MST_BINCode,");
                commandText.AppendLine(" WOC.WOCompletionNo, WOM.WorkOrderNo, WOC.Remark,");
                commandText.AppendLine(" WOD.WorkOrderDetailID, WOD.WorkOrderMasterID, WOD.ProductID, WOD.StockUMID,");
                commandText.AppendLine(" P.MasterLocationID, P.LocationID, P.BinID, P.LotSize, P.LotControl,");
                commandText.AppendLine(" L.Bin, B.BinTypeID, P.CategoryID, CompletedDate, WOM.ProductionLineID");
                commandText.AppendLine(" FROM PRO_WorkOrderDetail WOD JOIN PRO_WorkOrderMaster WOM ON WOD.WorkOrderMasterID = WOM.WorkOrderMasterID");
                commandText.AppendLine(" JOIN ITM_Product P ON WOD.ProductID = P.ProductID ");
                commandText.AppendLine(" JOIN PRO_WorkOrderCompletion WOC ON WOD.WorkOrderDetailID = WOC.WorkOrderDetailID ");
                commandText.AppendLine(" JOIN MST_UnitOfMeasure UM ON UM.UnitOfMeasureID = WOD.StockUMID ");
                commandText.AppendLine(" LEFT JOIN ITM_Category ON P.CategoryID = ITM_Category.CategoryID");
                commandText.AppendLine(" LEFT JOIN MST_Location L ON L.LocationID = P.LocationID ");
                commandText.AppendLine(" LEFT JOIN MST_BIN B ON B.BinID = P.BinID");
                commandText.AppendLine(" WHERE   WOC.MultiCompletionNo = ?");
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(commandText.ToString(), oconPCS);
                ocmdPCS.Parameters.Add(new OleDbParameter("@TransNo", OleDbType.VarWChar)).Value = multiTransNo;
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderCompletionTable.TABLE_NAME);

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
        /// List all component of a product in work order detail
        /// </summary>
        /// <param name="pintWODetailID"></param>
        /// <returns></returns>
        public DataTable ListComponentByWODetail(int pintWODetailID)
        {
            const string METHOD_NAME = THIS + ".ListComponentByWODetail()";
            const string UM_CODE = "UMCode";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                // 07-08-2006 dungla: join ITM_BOM instead of work order bom
                // 28-02-2007 CanhNv: add ,ITM_BOM.BomID,
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT  ITM_BOM.ComponentID, ITM_BOM.BomID,ITM_BOM.Quantity AS RequiredQuantity,");
                strSql.Append(" ITM_Product.StockUMID, ITM_Product.Code, ITM_Product.Description, ITM_Product.Revision,");
                strSql.Append(" MST_UnitOfMeasure.Code AS UMCode, ISNULL(ITM_Product.AllowNegativeQty,0) AllowNegativeQty,");
                strSql.Append(" WOD.OrderQuantity * ITM_BOM.Quantity +  WOD.OrderQuantity * ITM_BOM.Quantity* ISNULL(ITM_BOM.Shrink, 0)/100 ");
                strSql.Append(" -  ISNULL((SELECT SUM(ISNULL(PRO_ComponentScrapDetail.ScrapQuantity, 0)) ");
                strSql.Append(" FROM PRO_ComponentScrapMaster Inner join PRO_ComponentScrapDetail ");
                strSql.Append(" on PRO_ComponentScrapMaster.ComponentScrapMasterID  = PRO_ComponentScrapDetail.ComponentScrapMasterID ");
                strSql.Append(" Where PRO_ComponentScrapDetail.WorkOrderDetailID = WOD.WorkOrderDetailID ");
                strSql.Append(" and ITM_BOM.ComponentID = PRO_ComponentScrapDetail.ProductID), 0)");
                strSql.Append(" -ISNULL((SELECT SUM(ISNULL(PRO_IssueMaterialDetail.CommitQuantity, 0)) ");
                strSql.Append(" FROM PRO_IssueMaterialMaster Inner join PRO_IssueMaterialDetail ");
                strSql.Append(" on PRO_IssueMaterialMaster.IssueMaterialMasterID  = PRO_IssueMaterialDetail.IssueMaterialMasterID ");
                strSql.Append(" Where PRO_IssueMaterialDetail.WorkOrderDetailID = WOD.WorkOrderDetailID ");
                strSql.Append(" and ITM_BOM.ComponentID = PRO_IssueMaterialDetail.ProductID), 0) ");
                strSql.Append(" -ISNULL((SELECT SUM(ISNULL(PRO_WorkOrderCompletion.CompletedQuantity, 0)) ");
                strSql.Append(" FROM PRO_WorkOrderCompletion ");
                strSql.Append(" Where PRO_WorkOrderCompletion.WorkOrderDetailID = WOD.WorkOrderDetailID), 0)*ITM_BOM.Quantity as ShortageQuantity  ");
                strSql.Append(" FROM PRO_WorkOrderDetail WOD inner join ITM_BOM");
                strSql.Append(" on WOD.ProductID = ITM_BOM.ProductID ");
                strSql.Append(" INNER JOIN ITM_Product on ITM_BOM.ComponentID= ITM_Product.ProductID");
                strSql.Append(" INNER JOIN MST_UnitOfMeasure ON ITM_Product.StockUMID = MST_UnitOfMeasure.UnitOfMeasureID");
                strSql.Append(" WHERE WOD.WorkOrderDetailID = " + pintWODetailID);

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS);
                ocmdPCS.Connection.Open();
                ocmdPCS.CommandTimeout = 10000;

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WOScheduleDetailTable.TABLE_NAME);

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
    }
}

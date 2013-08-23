using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using log4net;
using log4net.Core;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;
using System.Text;
using ErrorCode = PCSComUtils.Common.ErrorCode;

namespace PCSComProduction.WorkOrder.DS
{
    public class PRO_WorkOrderDetailDS 
    {
        private const string THIS = "PCSComProduction.WorkOrder.DS.PRO_WorkOrderDetailDS";
        private const string WOD = " WorkOrderDetail";
        private const string WOM = " WorkOrderMaster";
        private const string PRO = " Product";
        private const string UM = " UnitOfMeasure ";
        private const string PARTNUMBER = " PartNumber ";
        private const string PARTNAME = " PartName ";
        private const string MODEL = " Model ";
        private const string SELECTED = " Selected";

        private ILog _logger = LogManager.GetLogger(typeof (PRO_WorkOrderDetailDS));

        public void Update(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".Update()";
            PRO_WorkOrderDetailVO objObject = (PRO_WorkOrderDetailVO)pobjObjecVO;
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE PRO_WorkOrderDetail SET "
                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.LINE_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.DUEDATE_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.STARTDATE_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.PRIORITY_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.AGC_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.ESTCST_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.STOCKUMID_FLD + "=   ?" + ","
                    + PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD + "=  ?,"
                    + PRO_WorkOrderDetailTable.STATUS_FLD + "=  ?"
                    + " WHERE " + PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.LINE_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.LINE_FLD].Value = objObject.Line;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD].Value = objObject.MfgCloseDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.DUEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.DUEDATE_FLD].Value = objObject.DueDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.STARTDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.STARTDATE_FLD].Value = objObject.StartDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.PRODUCTID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.PRODUCTID_FLD].Value = objObject.ProductID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.PRIORITY_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.PRIORITY_FLD].Value = objObject.Priority;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD].Value = objObject.SaleOrderDetailID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD].Value = objObject.WorkOrderMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.AGC_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.AGC_FLD].Value = objObject.AGC;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.ESTCST_FLD, OleDbType.Decimal));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.ESTCST_FLD].Value = objObject.EstCst;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.STOCKUMID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.STOCKUMID_FLD].Value = objObject.StockUMID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD].Value = objObject.FinCloseDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD].Value = objObject.SaleOrderMasterID;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.STATUS_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.STATUS_FLD].Value = objObject.Status;


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
        /// This method is used to search for all released work order line
        /// for selecting to issue materials
        /// </summary>
        /// <param name="phashCondition"></param>
        /// <returns></returns>
        public DataTable SearchWorkOrderToIssueMaterial(Hashtable phashCondition, string pstrRecordPermission)
        {
            const string METHOD_NAME = THIS + ".SearchWorkOrderToIssueMaterial()";
            const string REMAIN_COMPONENT_FOR_ISSUE_VIEW = "v_RemainComponentForWOIssueWithParentInfo";

            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT Convert(bit, 0) as SELECTED, " + REMAIN_COMPONENT_FOR_ISSUE_VIEW + ".* "
                    + " FROM " + REMAIN_COMPONENT_FOR_ISSUE_VIEW
                    + " WHERE (1 = 1) \n" + pstrRecordPermission;

                if (phashCondition != null)
                {
                    IDictionaryEnumerator myEnumerator = phashCondition.GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        strSql += " AND (" + myEnumerator.Key + "'" + myEnumerator.Value + "')";
                    }
                }

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, REMAIN_COMPONENT_FOR_ISSUE_VIEW);

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
        /// This method is used to search for all released work order line
        /// for selecting to issue materials
        /// </summary>
        /// <returns></returns>
        public DataSet SearchWorkOrderToIssueMaterial(string pstrRecordPermission, int pintMasterLocationID, int pintLocationID,
            int pintWorkOrderMasterID, DateTime pdtmFromStartDate, DateTime pdtmToStartDate)
        {
            const string METHOD_NAME = THIS + ".SearchWorkOrderToIssueMaterial()";
            const string REMAIN_COMPONENT_FOR_ISSUE_VIEW = "v_RemainComponentForWOIssueWithParentInfo";

            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                var columns = new StringBuilder();
                columns.AppendLine("Convert(bit, 0) as SELECTED, WorkOrderMasterID, PRO_WorkOrderMasterWorkOrderNo, WorkOrderDetailID, PRO_WorkOrderDetailLine, ");
                columns.AppendLine("ITM_ProductCode, ITM_ProductDescription, ITM_ProductRevision, AllowNegativeQty, ITM_CategoryCode, ");
                columns.AppendLine("StartDate, DueDate, OrderQuantity, MasterLocationID, ProductionLineLocationID, LocationID, ");
                columns.AppendLine("MST_LocationCode, BinID, MST_BinCode, ProductID, ParentCode, ParentName, ParentRevision, LotSize, ");
                columns.AppendLine("StockUMID, LotControl, MST_UnitOfMeasureCode, Shrink, RequiredQuantity, CommitQuantity, CommitedQuantity, ");
                columns.AppendLine("BomQuantity, AvailableQuantity, ComponentID, MST_PartyCode, MST_PartyName");
                string strTableName = PRO_IssueMaterialDetailTable.TABLE_NAME + SystemProperty.UserName
                    + DateTime.Now.ToString("ddMMyyyyHHmmss");
                var strSql = new StringBuilder();
                var strCondition = new StringBuilder();
                strSql.AppendLine(string.Format("SELECT {0}", columns));
                strSql.AppendLine(string.Format(" INTO {0} FROM {1} WHERE 1=1 ", strTableName, REMAIN_COMPONENT_FOR_ISSUE_VIEW));

                if (pintMasterLocationID > 0)
                {
                    strCondition.AppendLine(" AND MasterLocationID = " + pintMasterLocationID);
                }
                // 28-04-2006 dungla: search by location
                if (pintLocationID > 0)
                {
                    strCondition.AppendLine(" AND ProductionLineLocationID = " + pintLocationID);
                }
                // 28-04-2006 dungla: search by location
                if (pintWorkOrderMasterID > 0)
                {
                    strCondition.AppendLine(" AND WorkOrderMasterID = " + pintWorkOrderMasterID);
                }
                if (pdtmFromStartDate > DateTime.MinValue)
                {
                    strCondition.AppendLine(" AND StartDate >= ? ");
                }
                if (pdtmToStartDate > DateTime.MinValue)
                {
                    strCondition.AppendLine(" AND StartDate <= ? ");
                }
                if (pstrRecordPermission.Length > 0)
                {
                    strCondition.AppendLine(pstrRecordPermission);
                }

                if (strCondition.Length > 0)
                {
                    strSql.AppendLine(strCondition.ToString());
                }

                strSql.AppendLine(string.Format("; SELECT {0} FROM {1}", columns, strTableName));
                

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql.ToString(), oconPCS) {CommandTimeout = 10000};
                if (pdtmFromStartDate > DateTime.MinValue)
                {
                    ocmdPCS.Parameters.Add(new OleDbParameter("FromDate", OleDbType.Date)).Value = pdtmFromStartDate;
                }
                if (pdtmToStartDate > DateTime.MinValue)
                {
                    ocmdPCS.Parameters.Add(new OleDbParameter("ToDate", OleDbType.Date)).Value = pdtmToStartDate;
                }
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, strTableName);

                // store temp table name for cleanup
                SystemProperty.TempTables.Add(strTableName);

                return dstPCS;
            }
            catch (OleDbException ex)
            {
                _logger.Error(ex.Message, ex);
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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
                    + PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.LINE_FLD + ","
                    + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ","
                    + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderDetailTable.PRIORITY_FLD + ","
                    + PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.AGC_FLD + ","
                    + PRO_WorkOrderDetailTable.ESTCST_FLD + ","
                    + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderDetailTable.STATUS_FLD + ","
                    + PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD
                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderDetailTable.TABLE_NAME);

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
                    + PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.LINE_FLD + ","
                    + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ","
                    + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderDetailTable.PRIORITY_FLD + ","
                    + PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.AGC_FLD + ","
                    + PRO_WorkOrderDetailTable.ESTCST_FLD + ","
                    + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.STATUS_FLD
                    + "  FROM " + PRO_WorkOrderDetailTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pData.EnforceConstraints = false;
                odadPCS.Update(pData, PRO_WorkOrderDetailTable.TABLE_NAME);

            }
            catch (OleDbException ex)
            {
                if (ex.Errors.Count > 1)
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
                else
                {
                    throw ex;
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
        /// - get all work order line by work order master id and return DataSet
        /// </summary>
        /// <param name="pintWOMasterID">WorkOrderMasterID</param>
        /// <returns>All Work Order Lines of a WorkOrderMaster</returns>
        public DataSet List(int pintWOMasterID)
        {
            return null;
        }
        /// <summary>
        /// Delete all work order line of a work order master
        /// </summary>
        /// <param name="pintWOMasterID">WorkOrderMasterID</param>
        /// <author>Trada</author>
        /// <date>Thursday, September 21 2006</date>
        public void DeleteByWOMasterID(int pintWOMasterID)
        {
            const string METHOD_NAME = THIS + ".DeleteByWOMasterID()";
            string strSql = String.Empty;
            strSql = "DELETE " + PRO_WorkOrderDetailTable.TABLE_NAME
                + " WHERE  " + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + "=" + pintWOMasterID.ToString();
            strSql += "; DELETE " + PRO_WorkOrderMasterTable.TABLE_NAME + " WHERE  " + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + "=" + pintWOMasterID.ToString()
                + "; UPDATE " + MTR_CPOTable.TABLE_NAME + " SET " + MTR_CPOTable.WOGENERATEDID_FLD + "= null WHERE " + MTR_CPOTable.WOGENERATEDID_FLD + "=" + pintWOMasterID;

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
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
        /// - Search all work order line have status Released and have Due Date between FromDueDate and ToDueDate
        /// 
        /// SELECT	Master.WorkOrderNo, Detail.Line, (Detail.OrderQuantity - 
        /// 	(SELECT ISNULL(SUM(CompletedQuantity),0) FROM PRO_WorkOrderCompletion
        /// 	WHERE PRO_WorkOrderCompletion.WorkOrderMasterID = Detail.WorkOrderMasterID
        /// 	AND PRO_WorkOrderCompletion.WorkOrderDetailID = Detail.WorkOrderDetailID)
        /// 	- (SELECT ISNULL(SUM(ScrapQuantity),0) FROM PRO_AOScrapDetail
        /// 	WHERE PRO_AOScrapDetail.WorkOrderMasterID = Detail.WorkOrderMasterID
        /// 	AND PRO_AOScrapDetail.WorkOrderDetailID = Detail.WorkOrderDetailID))
        /// 	AS OpenQuantity, Product.Code, Unit.Code, Detail.StartDate, Detail.DueDate,
        /// 	Product.Description, Product.Revision
        /// FROM	PRO_WorkOrderMaster AS Master, PRO_WorkOrderDetail AS Detail,
        /// 	ITM_Product AS Product, MST_UnitOfMeasure AS Unit
        /// WHERE	Master.WorkOrderMasterID = Detail.WorkOrderMasterID
        /// 	AND Detail.ProductID = Product.ProductID
        /// 	AND Product.StockUMID = Unit.UnitOfMeasureID
        /// 	AND Master.CCNID = pintCCNID
        /// 	AND Master.MasterLocationID = pintMasterLocationID
        /// 	AND Detail.Status = (int)penumStatus
        /// if FromDueDate >= ToDueDate
        /// 	AND Detail.DuetDate > ? (pdtmFromDueDate)
        /// else
        /// 	AND Detail.DueDate > ? (pdtmFromDueDate) AND Detail.DueDate < ? (pdtmToDueDate)
        /// </summary>
        /// <param name="pintCCNID">CCN ID</param>
        /// <param name="pintMasterLocationID">Master Location ID</param>
        /// <param name="pdtmFromDueDate">From due date of work order line</param>
        /// <param name="pdtmToDueDate">To due date of work order line</param>
        /// <returns>All work order line have status is Released</returns>
        /// <author>Trada</author>
        /// <date>Thursday, June 16 2005</date>
        public DataSet SearchWOForClose(WOLineStatus penumStatus, int pintCCNID, int pintMasterLocationID, DateTime pdtmFromDueDate, DateTime pdtmToDueDate)
        {

            const string METHOD_NAME = THIS + ".SearchUnReleaseWO()";
            const string YYYYMMDD = "yyyyMMdd";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS;
            try
            {
                var sql = new StringBuilder();
                sql.AppendLine("SELECT '' AS  Selected, WorkOrderDetail.WorkOrderDetailID, WorkOrderMaster.WorkOrderNo, ");
                sql.AppendLine(" WorkOrderDetail.Line, Product.Code AS  PartNumber ,  Product.Description, ");
                sql.AppendLine(" Product.Revision, CA.Code ITM_CategoryCode, ");
                sql.AppendLine(" ( WorkOrderDetail.OrderQuantity - (SELECT ISNULL(SUM(CompletedQuantity),0) FROM PRO_WorkOrderCompletion ");
                sql.AppendLine(" WHERE PRO_WorkOrderCompletion.WorkOrderMasterID =  WorkOrderDetail.WorkOrderMasterID ");
                sql.AppendLine(" AND PRO_WorkOrderCompletion.WorkOrderDetailID =  WorkOrderDetail.WorkOrderDetailID)) AS  OpenQuantity , ");
                sql.AppendLine(" UnitOfMeasure .Code AS  UM ,  WorkOrderDetail.StartDate, WorkOrderDetail.DueDate ");
                sql.AppendLine(" FROM PRO_WorkOrderMaster AS  WorkOrderMaster  ");
                sql.AppendLine(" INNER JOIN PRO_WorkOrderDetail AS  WorkOrderDetail ON  WorkOrderMaster.WorkOrderMasterID = WorkOrderDetail.WorkOrderMasterID  ");
                sql.AppendLine(" INNER JOIN ITM_Product AS  Product ON Product.ProductID = WorkOrderDetail.ProductID  ");
                sql.AppendLine(" LEFT JOIN MST_UnitOfMeasure AS  UnitOfMeasure ON UnitOfMeasure.UnitOfMeasureID = Product.StockUMID  ");
                sql.AppendLine(" LEFT JOIN ITM_Category CA ON CA.CategoryID = Product.CategoryID  ");
                sql.AppendLine(string.Format(" WHERE  WorkOrderMaster.CCNID = {0}", pintCCNID));
                sql.AppendLine(string.Format(" AND  WorkOrderMaster.MasterLocationID = {0}", pintMasterLocationID));
                sql.AppendLine(string.Format(" AND  WorkOrderDetail.Status = {0}", (int)penumStatus));

                if ((pdtmFromDueDate > DateTime.MinValue) && (pdtmToDueDate > DateTime.MinValue))
                {
                    if (pdtmFromDueDate > pdtmToDueDate)
                    {
                        sql.AppendLine(string.Format(" AND DATEDIFF(dayofyear, {0}.{1},'{2}') <= 0 ", WOD,
                                                     PRO_WorkOrderDetailTable.DUEDATE_FLD,
                                                     pdtmFromDueDate.ToString(YYYYMMDD)));
                    }
                    else
                    {
                        sql.AppendLine(string.Format(" AND DATEDIFF(dayofyear, {0}.{1},'{2}') <= 0  AND DATEDIFF(dayofyear, {3}.{4},'{5}') >= 0 ",
                                WOD, PRO_WorkOrderDetailTable.DUEDATE_FLD, pdtmFromDueDate.ToString(YYYYMMDD), WOD,
                                PRO_WorkOrderDetailTable.DUEDATE_FLD, pdtmToDueDate.ToString(YYYYMMDD)));
                    }
                }
                // FromDueDate is null and ToDueDate is not null
                if ((pdtmFromDueDate == DateTime.MinValue) && (pdtmToDueDate > DateTime.MinValue))
                {
                    sql.AppendLine(string.Format(" AND DATEDIFF(dayofyear, {0}.{1},'{2}') >= 0 ", WOD,
                                                 PRO_WorkOrderDetailTable.DUEDATE_FLD, pdtmToDueDate.ToString(YYYYMMDD)));
                }
                // FromDueDate is not null and ToDueDate is null
                if ((pdtmFromDueDate > DateTime.MinValue) && (pdtmToDueDate == DateTime.MinValue))
                {
                    sql.AppendLine(string.Format(" AND DATEDIFF(dayofyear, {0}.{1},'{2}') <= 0 ", WOD,
                                                 PRO_WorkOrderDetailTable.DUEDATE_FLD,
                                                 pdtmFromDueDate.ToString(YYYYMMDD)));
                }

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(sql.ToString(), oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderDetailTable.TABLE_NAME);

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
        /// Update WorkOrderLine: 
        /// 	Status = (int)penumStatus
        /// 	MfgCloseDate = pdtmCloseDate
        /// where WorkOrderDetailID IN (pstrListOfIDs)
        /// </summary>
        /// <param name="pdtmCloseDate">Close Date</param>
        /// <param name="pstrListOfIDs">WorkOrderDetailID to be Manufacturing Close</param>
        /// <old-param name="pintworkorderdetailid">WorkOrderDetailID to be Manufacturing Close</old-param>
        /// <author>Trada</author>
        /// <date>Thursday, June 16 2005</date>
        public void CloseWorkOrderLines(WOLineStatus penumStatus, DateTime pdtmCloseDate, string pstrListOfIDs)
        {
            const string METHOD_NAME = THIS + ".CloseWorkOrderLines()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = string.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE PRO_WorkOrderDetail SET "
                    + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + " = ?" + ","
                    + PRO_WorkOrderDetailTable.STATUS_FLD + " = ?"
                    + " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " IN " + pstrListOfIDs;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD, OleDbType.Date));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD].Value = pdtmCloseDate;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.STATUS_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.STATUS_FLD].Value = (int)penumStatus;


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
        /// UpdateWOCompletion
        /// </summary>
        /// <param name="penumStatus"></param>
        /// <param name="pintWODetailID"></param>
        /// <author>Trada</author>
        /// <date>Monday, September 12 2005</date>
        public void UpdateWOCompletion(WOLineStatus penumStatus, int pintWODetailID)
        {
            const string METHOD_NAME = THIS + ".UpdateWOCompletion()";
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = string.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE PRO_WorkOrderDetail SET "
                    + PRO_WorkOrderDetailTable.STATUS_FLD + " = ?"
                    + " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = " + pintWODetailID.ToString();

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.STATUS_FLD, OleDbType.VarWChar));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.STATUS_FLD].Value = (int)penumStatus;


                ocmdPCS.CommandText = strSql;
                ocmdPCS.CommandTimeout = 10000;
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
        /// Search UnRelease WOs
        /// </summary>
        /// <author>Trada</author>
        /// <date>Friday, June 3 2005</date>
        /// <param name="pintCCNID"></param>
        /// <param name="pintMasLocID"></param>
        /// <param name="pstrWONo"></param>
        /// <param name="pdtmFromStartDate"></param>
        /// <param name="pdtmToStartDate"></param>
        /// <returns></returns>
        public DataSet SearchUnReleaseWO(int pintCCNID, int pintMasLocID, string pstrWONo, int pintProLineID, DateTime pdtmFromStartDate, DateTime pdtmToStartDate, int pintStatus)
        {
            const string METHOD_NAME = THIS + ".SearchUnReleaseWO()";
            const string YYYYMMDD = "yyyyMMdd";
            //const string ONE = "1";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = string.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                strSql = "SELECT DISTINCT "
                    + " '' AS " + SELECTED + ", "
                    + "PRO_ProductionLine.Code ProductionLine, "
                    + WOM + "." + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + ", "
                    + WOM + "." + PRO_WorkOrderMasterTable.CCNID_FLD + ", "
                    + WOM + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD + ", "
                    + WOM + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + ", "
                    + WOD + "." + PRO_WorkOrderDetailTable.LINE_FLD + ","
                    + WOD + "." + PRO_WorkOrderDetailTable.STATUS_FLD + ","
                    + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO + "." + ITM_ProductTable.CODE_FLD + PARTNUMBER + ", "
                    + PRO + "." + ITM_ProductTable.PRODUCTID_FLD + ", "
                    + PRO + "." + ITM_ProductTable.DESCRIPTION_FLD + PARTNAME + ", "
                    + PRO + "." + ITM_ProductTable.REVISION_FLD + MODEL + ", "
                    //					+ " '' AS " + SELECTED + ", "
                    + UM + "." + MST_UnitOfMeasureTable.CODE_FLD + " UM " + ", "
                    + WOD + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ", "
                    + WOD + "." + PRO_WorkOrderDetailTable.ESTCST_FLD + ", "
                    + WOD + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ", "
                    + WOD + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD
                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME + " " + WOD
                    + " INNER JOIN " + PRO_WorkOrderMasterTable.TABLE_NAME + " " + WOM
                    + " ON " + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + " = "
                    + WOM + "." + PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD
                    + " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " " + PRO
                    + " ON " + WOD + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + " = "
                    + PRO + "." + ITM_ProductTable.PRODUCTID_FLD
                    + " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + " " + UM
                    + " ON " + WOD + "." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + " = "
                    + UM + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
                    //					+ " Left Join PRO_WORouting on PRO_WORouting.WorkOrderDetailID = WorkOrderDetail.WorkOrderDetailID "
                    //					+ " Left Join MST_WorkCenter on MST_WorkCenter.WorkCenterID = PRO_WORouting.WorkCenterID "
                    + " Left Join PRO_ProductionLine on PRO_ProductionLine.ProductionLineID = " + WOM + ".ProductionLineID "
                    + " WHERE " + WOM + "." + PRO_WorkOrderMasterTable.CCNID_FLD + " = " + pintCCNID
                    + " AND " + WOM + "." + PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD + " = " + pintMasLocID.ToString();
                if (pintStatus == (int)WOLineStatus.Unreleased)
                {
                    strSql += " AND " + WOD + "." + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + pintStatus.ToString();
                }
                else
                {
                    strSql += " AND " + WOD + "." + PRO_WorkOrderDetailTable.STATUS_FLD + " = " + pintStatus.ToString();
                    strSql += " AND " + WOD + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " NOT IN (Select WorkOrderDetailID from PRO_WorkOrderCompletion) ";
                }

                // WorkOrderNO
                if (pstrWONo != string.Empty)
                {
                    strSql += " AND " + WOM + "." + PRO_WorkOrderMasterTable.WORKORDERNO_FLD + " = '" + pstrWONo + "'";
                }
                // FromStartDate
                if (pdtmFromStartDate > DateTime.MinValue)
                {
                    //strSql += " AND DATEDIFF(dayofyear, " + WOD + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ",'" + pdtmFromStartDate.ToString(YYYYMMDD) + "') <= 0 ";
                    strSql += " AND " + WOD + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + " >= ?";
                    ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.STARTDATE_FLD, OleDbType.Date));
                    ocmdPCS.Parameters[PRO_WorkOrderDetailTable.STARTDATE_FLD].Value = pdtmFromStartDate;
                }
                // ToStartDate
                if (pdtmToStartDate > DateTime.MinValue)
                {
                    //					strSql += " AND DATEDIFF(dayofyear, " + WOD + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ",'" + pdtmToStartDate.ToString(YYYYMMDD) + "') >= 0 ";
                    strSql += " AND " + WOD + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + " <= ?";
                    ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.DUEDATE_FLD, OleDbType.Date));
                    ocmdPCS.Parameters[PRO_WorkOrderDetailTable.DUEDATE_FLD].Value = pdtmToStartDate;
                }
                //ProLineID
                if (pintProLineID > 0)
                {
                    strSql += " AND PRO_ProductionLine.ProductionLineID = " + pintProLineID;
                }
                //Order by
                strSql += " ORDER BY Productionline, WorkOrderNo, Line, StartDate, DueDate";
                ocmdPCS.Connection.Open();
                ocmdPCS.CommandText = strSql;
                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderDetailTable.TABLE_NAME);

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
        /// Get List of WODetails by WOMaster
        /// </summary>
        /// <param name="pintWOMasterID">Work Order Master ID</param>
        /// <author>
        /// Do Manh Tuan
        /// 07 - 06 - 2005
        /// </author>
        /// <returns>DataSet</returns>
        public DataSet GetWODetailByMaster(int pintWOMasterID)
        {
            const string METHOD_NAME = THIS + ".List()", UNRELEASE = "UnRelease", RELEASED = "Released", MFGCLOSE = "MfgClose", FINCLOSE = "FinClose", EXTEND = "String", UM = "UM",
                      SALEORDERCODE = "SaleOrderCode", CODE = "Code", ACT = "ACT", STD = "STD", AVG = "AVG";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT "
                    + PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.LINE_FLD + ","
                    + "( SELECT " + ITM_ProductTable.CODE_FLD + " FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.CODE_FLD + ","
                    + "( SELECT " + ITM_ProductTable.DESCRIPTION_FLD + " FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.DESCRIPTION_FLD + ","
                    + "( SELECT " + ITM_ProductTable.REVISION_FLD + " FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.REVISION_FLD + ","

                    //HACK: added by Tuan TQ. 23 May, 2006. Add more category column
                    + "( SELECT " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CODE_FLD + " FROM " + ITM_CategoryTable.TABLE_NAME
                    + " INNER JOIN " + ITM_ProductTable.TABLE_NAME + " ON " + ITM_CategoryTable.TABLE_NAME + "." + ITM_CategoryTable.CATEGORYID_FLD
                    + " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.CATEGORYID_FLD
                    + " WHERE " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD
                    + ") as " + ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD + ","
                    //End hack

                    + "( SELECT " + MST_UnitOfMeasureTable.CODE_FLD + " FROM " + MST_UnitOfMeasureTable.TABLE_NAME + " WHERE " + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ") as " + UM + ","
                    + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ","
                    + PRO_WorkOrderDetailTable.STATUS_FLD + ","
                    + " CASE " + PRO_WorkOrderDetailTable.STATUS_FLD + " WHEN " + (int)WOLineStatus.Unreleased + " THEN '" + UNRELEASE + "' WHEN " + (int)WOLineStatus.Released + " THEN '" + RELEASED + "' WHEN " + (int)WOLineStatus.MfgClose + " THEN '" + MFGCLOSE + "' WHEN " + (int)WOLineStatus.FinClose + " THEN '" + FINCLOSE + "' END" + " as " + PRO_WorkOrderDetailTable.STATUS_FLD + EXTEND + ","
                    + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderDetailTable.PRIORITY_FLD + ","
                    + PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + ","
                    + "( SELECT " + SO_SaleOrderMasterTable.CODE_FLD + " FROM " + SO_SaleOrderMasterTable.TABLE_NAME + " WHERE " + SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ") as " + SALEORDERCODE + ","
                    + "( SELECT " + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + " FROM " + SO_SaleOrderDetailTable.TABLE_NAME + " WHERE " + SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ") as " + SO_SaleOrderDetailTable.SALEORDERLINE_FLD + ","
                    + "( SELECT " + ITM_ProductTable.COSTMETHOD_FLD + " = CASE " + ITM_ProductTable.COSTMETHOD_FLD + " WHEN " + (int)CostMethodEnum.ACT + " THEN '" + ACT + "' WHEN " + (int)CostMethodEnum.STD + " THEN '" + STD + "' WHEN " + (int)CostMethodEnum.AVG + " THEN '" + AVG + "' END FROM " + ITM_ProductTable.TABLE_NAME + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ") as " + ITM_ProductTable.COSTMETHOD_FLD + ","
                    + PRO_WorkOrderDetailTable.AGC_FLD + ","
                    + PRO_WorkOrderDetailTable.ESTCST_FLD + ","
                    + "( SELECT " + MST_AGCTable.CODE_FLD + " FROM " + MST_AGCTable.TABLE_NAME + " WHERE " + MST_AGCTable.AGCID_FLD + " = " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.AGC_FLD + ") as " + PRO_WorkOrderDetailTable.AGC_FLD + CODE + ","
                    + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD
                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME

                    + " WHERE " + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + " = " + pintWOMasterID
                    + " ORDER BY Line";
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderDetailTable.TABLE_NAME);

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
        /// Get StockUMCode, CostMethod, AGCCode
        /// </summary>
        /// <param name="pintProductID"></param>
        /// <returns></returns>
        public string GetProductInforByID(int pintProductID)
        {
            const string METHOD_NAME = THIS + ".GetProductInforByID()", ACT = "ACT", STD = "STD", AVG = "AVG";

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                    + "( SELECT " + MST_UnitOfMeasureTable.CODE_FLD + " FROM " + MST_UnitOfMeasureTable.TABLE_NAME + " WHERE "
                    + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD + " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.STOCKUMID_FLD + "),"
                    + "(SELECT " + ITM_ProductTable.COSTMETHOD_FLD + " = " + " CASE " + ITM_ProductTable.COSTMETHOD_FLD +
                    " WHEN " + (int)CostMethodEnum.ACT + " THEN '" + ACT + "' WHEN " + (int)CostMethodEnum.STD + " THEN '" + STD + "' WHEN " + (int)CostMethodEnum.AVG + " THEN '" + AVG + "' END),"
                    + "(SELECT " + MST_AGCTable.CODE_FLD + " FROM " + MST_AGCTable.TABLE_NAME + " WHERE " + MST_AGCTable.AGCID_FLD + " = "
                    + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.AGCID_FLD + "),"
                    + "(SELECT " + ITM_CostTable.ITEMCOSTTOTALAMOUNT21_FLD + " FROM " + ITM_CostTable.TABLE_NAME + " WHERE " + ITM_CostTable.PRODUCTID_FLD + " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRODUCTID_FLD + ")"
                    + " FROM " + ITM_ProductTable.TABLE_NAME
                    + " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                while (odrPCS.Read())
                {
                    return odrPCS[0].ToString() + ";" + odrPCS[1].ToString() + ";" + odrPCS[2].ToString() + ";" + odrPCS[3].ToString();
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
            return string.Empty;
        }

        /// <summary>
        /// Release WO
        /// </summary>
        /// <autho>Trada</autho>
        /// <date>Monday, June 6 2005</date>
        /// <param name="pData"></param>
        public void ReleaseWO(DataSet pData)
        {
            const string METHOD_NAME = THIS + ".ReleaseWO()";
            string strSql;
            OleDbConnection oconPCS = null;
            OleDbCommandBuilder odcbPCS;
            OleDbDataAdapter odadPCS = new OleDbDataAdapter();

            try
            {
                strSql = "SELECT "

                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.STATUS_FLD
                    + "  FROM " + PRO_WorkOrderDetailTable.TABLE_NAME;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
                odcbPCS = new OleDbCommandBuilder(odadPCS);
                pData.EnforceConstraints = false;
                odadPCS.Update(pData, PRO_WorkOrderDetailTable.TABLE_NAME);

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
        /// SELECT OrderQuantity, WODetailID FROM PRO_WorkOrderDetail WHERE WODetailID = pintWODetailID
        /// </summary>
        public decimal GetOrderQuantityByWODetail(int pintWODetailID)
        {
            const string METHOD_NAME = THIS + ".GetScrapQuantity()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {

                string strSql = "SELECT ISNULL(" + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ", 0) "
                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME + " WHERE "
                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + " = " + pintWODetailID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                return decimal.Parse(ocmdPCS.ExecuteScalar().ToString());
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
        /// This method is used to get some information from WO Line ID
        /// </summary>
        /// <param name="pintID"></param>
        /// <returns></returns>
        public object GetWorkOrderDetailInfo(int pintID)
        {
            const string METHOD_NAME = THIS + ".GetWorkOrderDetailInfo()";

            OleDbDataReader odrPCS = null;
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                    + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.LINE_FLD + ","
                    + PRO_WorkOrderDetailTable.INCREMENT_FLD + ","
                    + PRO_WorkOrderDetailTable.DESCRIPTION_FLD + ","
                    + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderDetailTable.STATUS_FLD
                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME
                    + " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + pintID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                odrPCS = ocmdPCS.ExecuteReader();

                PRO_WorkOrderDetailVO objObject = new PRO_WorkOrderDetailVO();

                while (odrPCS.Read())
                {
                    objObject.WorkOrderDetailID = int.Parse(odrPCS[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].ToString().Trim());
                    objObject.Line = int.Parse(odrPCS[PRO_WorkOrderDetailTable.LINE_FLD].ToString().Trim());
                    objObject.Status = odrPCS[PRO_WorkOrderDetailTable.STATUS_FLD].ToString().Trim();
                    if (odrPCS[PRO_WorkOrderDetailTable.INCREMENT_FLD] != DBNull.Value)
                    {
                        objObject.Increment = int.Parse(odrPCS[PRO_WorkOrderDetailTable.INCREMENT_FLD].ToString().Trim());
                    }
                    else
                        objObject.Increment = -1;
                    objObject.Description = odrPCS[PRO_WorkOrderDetailTable.DESCRIPTION_FLD].ToString().Trim();
                    objObject.ProductID = int.Parse(odrPCS[PRO_WorkOrderDetailTable.PRODUCTID_FLD].ToString());
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

        public void UpdateWORouting(object pobjObjecVO)
        {
            const string METHOD_NAME = THIS + ".UpdateWORouting()";
            PRO_WorkOrderDetailVO objObject = (PRO_WorkOrderDetailVO)pobjObjecVO;
            //prepare value for parameters
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                strSql = "UPDATE PRO_WorkOrderDetail SET "
                    + PRO_WorkOrderDetailTable.INCREMENT_FLD + "=  ?,"
                    + PRO_WorkOrderDetailTable.DESCRIPTION_FLD + "=  ?"
                    + " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "= ?";

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.INCREMENT_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.INCREMENT_FLD].Value = objObject.Increment;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.DESCRIPTION_FLD, OleDbType.VarChar));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.DESCRIPTION_FLD].Value = objObject.Description;

                ocmdPCS.Parameters.Add(new OleDbParameter(PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD, OleDbType.Integer));
                ocmdPCS.Parameters[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Value = objObject.WorkOrderDetailID;


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
        /// Get Status of WOLine
        /// </summary>
        /// <param name="pintID"></param>
        /// <returns></returns>
        /// <author>
        /// Do Manh Tuan
        /// 04-07-2005
        /// </author>
        public int GetStatusForWOLine(int pintID)
        {
            const string METHOD_NAME = THIS + ".GetWorkOrderDetailInfo()";

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;
                strSql = "SELECT "
                    + PRO_WorkOrderDetailTable.STATUS_FLD
                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME
                    + " WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + pintID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);

                ocmdPCS.Connection.Open();
                object objResult = ocmdPCS.ExecuteScalar();
                if (objResult != null)
                {
                    return int.Parse(objResult.ToString());
                }
                else
                {
                    return 0;
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

        public void AddAndReturnID(object pobjObjectVO)
        {
        }

        public DataTable GetBinAvailableByProductionLine(int pintProductionLineID)
        {
            const string METHOD_NAME = THIS + ".ListByWorkCenter()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " select V_PRODUCT_AVAILABLE_IN_BIN_INCOMING.*"
                            + " from dbo.V_PRODUCT_AVAILABLE_IN_BIN_INCOMING"
                            + " Inner join PRO_ProductionLine on V_PRODUCT_AVAILABLE_IN_BIN_INCOMING.LocationID = PRO_ProductionLIne.LocationID "
                            + " where PRO_ProductionLine.ProductionLineID = " + pintProductionLineID;

                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderDetailTable.TABLE_NAME);

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

        public DataSet ListByWorkCenter(int pintWorkCenterID)
        {
            const string METHOD_NAME = THIS + ".ListByWorkCenter()";
            DataSet dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = String.Empty;

                strSql = "SELECT DISTINCT "
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.SALEORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.LINE_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.MFGCLOSEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.DUEDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STARTDATE_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRIORITY_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.SALEORDERDETAILID_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.AGC_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.ESTCST_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STOCKUMID_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.STATUS_FLD + ","
                    + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.FINCLOSEDATE_FLD + ","
                    + MST_WorkCenterTable.TABLE_NAME + "." + MST_WorkCenterTable.CODE_FLD + " WorkCenterCode,"

                    + " CASE "
                    + " WHEN " + PRO_WORoutingTable.TABLE_NAME + "." + PRO_WORoutingTable.PACER_FLD + "='M' THEN "
                    + PRO_WORoutingTable.MACHINERUNTIME_FLD + " + " + PRO_WORoutingTable.MACHINESETUPTIME_FLD + " + " + PRO_WORoutingTable.MOVETIME_FLD
                    + " WHEN " + PRO_WORoutingTable.TABLE_NAME + "." + PRO_WORoutingTable.PACER_FLD + "='L' THEN "
                    + PRO_WORoutingTable.LABORRUNTIME_FLD + " + " + PRO_WORoutingTable.LABORSETUPTIME_FLD + " + " + PRO_WORoutingTable.MOVETIME_FLD
                    + " WHEN " + PRO_WORoutingTable.TABLE_NAME + "." + PRO_WORoutingTable.PACER_FLD + "='B' THEN "
                    + PRO_WORoutingTable.MACHINERUNTIME_FLD + " + " + PRO_WORoutingTable.MACHINESETUPTIME_FLD + " + " + PRO_WORoutingTable.LABORRUNTIME_FLD + " + " + PRO_WORoutingTable.LABORSETUPTIME_FLD + " + " + PRO_WORoutingTable.MOVETIME_FLD
                    + " END LeadTime "

                    + " FROM " + PRO_WorkOrderDetailTable.TABLE_NAME
                    + " INNER JOIN " + PRO_WORoutingTable.TABLE_NAME
                    + " ON " + PRO_WorkOrderDetailTable.TABLE_NAME + "." + PRO_WorkOrderDetailTable.PRODUCTID_FLD + "=" + PRO_WORoutingTable.TABLE_NAME + "." + PRO_WORoutingTable.PRODUCTID_FLD
                    + " INNER JOIN " + MST_WorkCenterTable.TABLE_NAME
                    + " ON " + PRO_WORoutingTable.TABLE_NAME + "." + PRO_WORoutingTable.WORKCENTERID_FLD + "=" + MST_WorkCenterTable.TABLE_NAME + "." + MST_WorkCenterTable.WORKCENTERID_FLD + " AND " + MST_WorkCenterTable.ISMAIN_FLD + " = 1 "
                    + " WHERE " + MST_WorkCenterTable.TABLE_NAME + "." + MST_WorkCenterTable.WORKCENTERID_FLD + " = " + pintWorkCenterID.ToString();
                Utils utils = new Utils();
                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, PRO_WorkOrderDetailTable.TABLE_NAME);

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

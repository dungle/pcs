using System;
using System.Data;
using System.Linq;
using PCSComProduct.Items.DS;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;

namespace PCSComSale.Order.BO
{
    public class SaleOrderBO
    {
        private const string This = "PCSComSale.Order.BO.SaleOrderBO";

        public DataSet ListCCN()
        {
            var dsCCN = new MST_CCNDS();
            return dsCCN.ListCCNForCombo();
        }

        public DataSet ListCurrency()
        {
            var dsCurrency = new MST_CurrencyDS();
            return dsCurrency.List();
        }

        public DataSet ListLocation()
        {
            var dsLocation = new MST_LocationDS();
            return dsLocation.List();
        }

        public DataSet ListPartyLocation()
        {
            var dsPartyLocation = new MST_PartyLocationDS();
            return dsPartyLocation.ListForCombo();
        }

        public DataSet ListPartyContact()
        {
            var dsContact = new MST_PartyContactDS();
            return dsContact.List();
        }

        public DataSet ListSaleType()
        {
            var dsSaleType = new SO_SaleTypeDS();
            return dsSaleType.List();
        }

        public DataSet ListGate()
        {
            var dsGate = new SO_GateDS();
            return dsGate.List();
        }

        public DataSet ListDiscountTerm()
        {
            var dsDiscoutTerm = new MST_DiscountTermDS();
            return dsDiscoutTerm.List();
        }

        public DataSet ListDeliveryTerm()
        {
            var dsDeliveryTerm = new MST_DeliveryTermDS();
            return dsDeliveryTerm.List();
        }

        public DataSet ListPaymentTerm()
        {
            var dsPaymentTerm = new MST_PaymentTermDS();
            return dsPaymentTerm.List();
        }

        public DataSet ListCarrier()
        {
            var dsCarrier = new MST_CarrierDS();
            return dsCarrier.List();
        }

        public DataSet ListPause()
        {
            var dsPause = new MST_PauseDS();
            return dsPause.List();
        }

        public DataSet ListSalePresentative()
        {
            var dsSalePresentative = new MST_EmployeeDS();
            return dsSalePresentative.List();
        }

        public DataSet ListParty()
        {
            var dsParty = new MST_PartyDS();
            return dsParty.List();
        }

        public DataSet ListSaleOrderMaster()
        {
            var dsSaleOrderMaster = new SO_SaleOrderMasterDS();
            return dsSaleOrderMaster.List();
        }

        public DataSet ListSaleOrderDetail()
        {
            var dsSaleOrderDetail = new SO_SaleOrderDetailDS();
            return dsSaleOrderDetail.List();
        }

        public DataSet ListSaleOrderDetail(int pintMasterId)
        {
            var dsSaleOrderDetail = new SO_SaleOrderDetailDS();
            return dsSaleOrderDetail.List(pintMasterId);
        }

        public DataSet ListExchangeRate()
        {
            var dsExchangeRate = new MST_ExchangeRateDS();
            return dsExchangeRate.List();
        }

        public DataSet ListUnitOfMeasure()
        {
            var dsUnitOfMeasure = new MST_UnitOfMeasureDS();
            return dsUnitOfMeasure.List();
        }

        public object GetUnitOfMeasure(int pintId)
        {
            var dsUnitOfMeasure = new MST_UnitOfMeasureDS();
            return dsUnitOfMeasure.GetObjectVO(pintId);
        }

        public DataSet ListCancelReason()
        {
            var dsReason = new SO_CancelReasonDS();
            return dsReason.List();
        }

        public object GetProduct(int pintId)
        {
            var dsProduct = new ITM_ProductDS();
            return dsProduct.GetObjectVO(pintId);
        }

        public DataTable ListProduct()
        {
            var dsProduct = new ITM_ProductDS();
            return dsProduct.ListForCombo();
        }

        public void UpdateSaleOrder(object pvoSOMaster, DataSet pdstSODetail)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            dsMaster.Update(pvoSOMaster);
            if (pdstSODetail != null)
                if (pdstSODetail.Tables.Count > 0)
                {
                    foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
                    {
                        if (objRow.RowState == DataRowState.Deleted) continue;
                        objRow[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] =
                            ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID;
                    }
                    var dsDetail = new SO_SaleOrderDetailDS();
                    dsDetail.UpdateDataSet(pdstSODetail);
                }
        }

        public int AddNewSaleOrder(object pvoSOMaster, DataSet pdstSODetail)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            int intSOMasterId = dsMaster.AddAndReturnID(pvoSOMaster);
            if (pdstSODetail.Tables.Count > 0)
            {
                foreach (DataRow objRow in
                    pdstSODetail.Tables[0].Rows.Cast<DataRow>().Where(objRow => objRow.RowState != DataRowState.Deleted))
                {
                    objRow[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] = intSOMasterId;
                }
                var dsDetail = new SO_SaleOrderDetailDS();
                dsDetail.UpdateDataSet(pdstSODetail);
            }
            return intSOMasterId;
        }

        /// <summary>
        /// Check if SO can be edit.
        /// </summary>
        /// <param name="pintSOMasterId"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Friday, July 15 2005</date>
        public bool CheckEditableSO(int pintSOMasterId)
        {
            const string methodName = This + ".CheckEditableSO()";

            var dstDetail = new DataSet();
            //Check if SaleOrder is released
            var dsSOCommitInventoryMaster = new SO_CommitInventoryMasterDS();
            //dstDetail.Tables[0]
            dstDetail.Tables.Add(
                dsSOCommitInventoryMaster.ListCommittedInventoryMasterBySaleOrderMasterID(pintSOMasterId).Tables[0].
                    Copy());
            if (dstDetail.Tables[0].Rows.Count > 0)
            {
                //SaleOrder is released
                throw new PCSBOException(ErrorCode.MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_RELEASED, methodName,
                                         null);
            }
            var dsDetail = new SO_SaleOrderDetailDS();
            //dstDetail.Tables[2]
            dstDetail.Tables.Add(dsDetail.List(pintSOMasterId).Tables[0].Copy());
            DataTable dtbDeliverySchedule;
            foreach (DataRow drow in dstDetail.Tables[2].Rows)
            {
                //Check if SaleOrder is delivery schedule
                var dsSODeliverySchedule = new SO_DeliveryScheduleDS();
                dtbDeliverySchedule =
                    dsSODeliverySchedule.GetDeliverySchedule(
                        int.Parse(drow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString())).Tables[0];
                if (dtbDeliverySchedule.Rows.Count > 0)
                {
                    //SaleOrder is delivery schedule
                    throw new PCSBOException(ErrorCode.MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_DELIVERY_SCHEDULE,
                                             methodName, null);
                }
            }
            return true;
        }

        public void DeleteSaleOrder(int pintSOMasterId)
        {
            var dstDetail = new DataSet();
            //Check if SaleOrder is released
            var dsSOCommitInventoryMaster = new SO_CommitInventoryMasterDS();
            //dstDetail.Tables[0]
            dstDetail.Tables.Add(
                dsSOCommitInventoryMaster.ListCommittedInventoryMasterBySaleOrderMasterID(pintSOMasterId).Tables[0].
                    Copy());
            if (dstDetail.Tables[0].Rows.Count > 0)
            {
                //SaleOrder is released
                throw new PCSBOException(ErrorCode.MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_RELEASED,
                                         ".DeleteSaleOrder()", null);
            }

            var dsDetail = new SO_SaleOrderDetailDS();
            //dstDetail.Tables[2]
            dstDetail.Tables.Add(dsDetail.List(pintSOMasterId).Tables[0].Copy());
            DataTable dtbDeliverySchedule;
            foreach (DataRow drow in dstDetail.Tables[2].Rows)
            {
                //Check if SaleOrder is delivery schedule
                var dsSODeliverySchedule = new SO_DeliveryScheduleDS();
                dtbDeliverySchedule =
                    dsSODeliverySchedule.GetDeliverySchedule(
                        int.Parse(drow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString())).Tables[0];
                if (dtbDeliverySchedule.Rows.Count > 0)
                {
                    //SaleOrder is delivery schedule
                    throw new PCSBOException(ErrorCode.MESSAGE_CANNOT_DELETE_SALE_ORDER_LINE_BECAUSE_DELIVERY_SCHEDULE,
                                             "DeleteSaleOrder", null);
                }
                dsDetail.Delete(int.Parse(drow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString()));
            }
            var dsMaster = new SO_SaleOrderMasterDS();
            dsMaster.Delete(pintSOMasterId);
        }

        public DataSet ListDetailByMaster(int pintId)
        {
            var dsDetail = new SO_SaleOrderDetailDS();
            return dsDetail.List(pintId);
        }

        public object GetMasterVO(int pintSOMasterId)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            return dsMaster.GetObjectVO(pintSOMasterId);
        }

        public object GetSaleOrderByCode(string pstrCode)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            return dsMaster.GetObjectVO(pstrCode);
        }

        public decimal GetUMRate(int pintStockUmid, int pintSellingUmid)
        {
            var dsSale = new SO_SaleOrderDetailDS();
            return dsSale.GetUMRate(pintStockUmid, pintSellingUmid);
        }

        /// <summary>
        /// GetSODetailDeliverySchedule
        /// </summary>
        /// <param name="pintMasterId"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <Date>Friday, April 28 2006</Date>
        public DataSet GetSODetailDeliverySchedule(int pintMasterId)
        {
            var dsSaleOrderDetail = new SO_SaleOrderDetailDS();
            return dsSaleOrderDetail.GetSODetailDeliverySchedule(pintMasterId);
        }

        public object GetExchangeRate(int pintCurrencyId, DateTime pdtmOrderDate)
        {
            var dsMst = new MST_ExchangeRateDS();
            return dsMst.GetExchangeRate(pintCurrencyId, pdtmOrderDate);
        }

        public int IsValidateData(string pstrValue, string pstrTable, string pstrField, string pstrCodition)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            return dsMaster.IsValidateData(pstrValue, pstrTable, pstrField, pstrCodition);
        }

        public DataRow GetDataRow(string pstrKeyField, string pstrValue, string pstrTable, string pstrField,
                                  string pstrCodition)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            return dsMaster.GetDataRow(pstrKeyField, pstrValue, pstrTable, pstrField, pstrCodition);
        }

        public DataRow LoadObjectVO(int pintMasterId)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            return dsMaster.LoadObjectVO(pintMasterId);
        }

        public object GetProductVO(int pintId)
        {
            var dsProduct = new ITM_ProductDS();
            return dsProduct.GetObjectVO(pintId);
        }

        public int ImportNewSaleOrder(object pvoSOMaster, DataSet pdstSODetail, DataTable dtImportData,
                                      DataSet pdstGateData)
        {
            const string tempQtyColName = "TempQty";
            var dsMaster = new SO_SaleOrderMasterDS();
            int intSOMasterId = dsMaster.AddAndReturnID(pvoSOMaster);
            int intMonth = 0, intYear = 0;
            string strMmyyyy = dtImportData.Rows[0][0].ToString();
            const string dateSlash = "/";
            try
            {
                intMonth = int.Parse(strMmyyyy.Substring(0, strMmyyyy.IndexOf(dateSlash)));
                intYear = int.Parse(strMmyyyy.Substring(strMmyyyy.IndexOf(dateSlash) + dateSlash.Length));
            }
            catch (InvalidCastException)
            {
            }

            if (pdstSODetail.Tables.Count > 0)
            {
                foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
                {
                    if (objRow.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    objRow[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] = intSOMasterId;
                }
                var dsDetail = new SO_SaleOrderDetailDS();
                dsDetail.UpdateDataSetForImport(pdstSODetail, intSOMasterId);

                //get delivery schedules schema
                var dsSODeli = new SO_DeliveryScheduleDS();
                int intMasterId = Convert.ToInt32(pdstSODetail.Tables[SO_SaleOrderDetailTable.TABLE_NAME].Rows[0][SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD]);
                DataSet dstDeli = dsSODeli.ListForImport(intMasterId);

                //Add temp column for new quantity
                var objCol = new DataColumn(tempQtyColName) {DataType = typeof (decimal), DefaultValue = 0};
                dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Add(objCol);

                //insert delivery schedules
                int intEndDataCol = dtImportData.Columns.Count - 1;
                int intEndDataRow = dtImportData.Rows.Count;
                foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
                {
                    int intLine = 0;
                    for (int i = IntBeginDataCol; i < intEndDataCol; i++)
                    {
                        for (int j = IntBeginDataRow; j < intEndDataRow; j++)
                        {
                            if (
                                dtImportData.Rows[j][IntPartsNoCol].ToString().Equals(
                                    objRow[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].ToString()))
                            {
                                int intDay = int.Parse(dtImportData.Rows[IntHeaderRow][i].ToString());
                                int intHour = int.Parse(dtImportData.Rows[j][IntTimeCol].ToString());
                                var dateDeli = new DateTime(intYear, intMonth, intDay, intHour, 0, 0);
                                decimal dcmQty;
                                if (dtImportData.Rows[j][i] == DBNull.Value)
                                {
                                    continue;
                                }
                                intLine++;
                                try
                                {
                                    dcmQty = decimal.Parse(dtImportData.Rows[j][i].ToString());
                                }
                                catch
                                {
                                    continue;
                                }

                                //findout gate code
                                string strGateCode = dtImportData.Rows[j][IntCodeCol].ToString().Trim();
                                int intGateId = 0;
                                if (strGateCode != string.Empty)
                                {
                                    //lookup gate id
                                    DataRow[] arrGate =
                                        pdstGateData.Tables[0].Select(SO_GateTable.CODE_FLD + "='" + strGateCode + "'");
                                    if (arrGate.Length == 0)
                                    {
                                        //never occurs
                                    }
                                    else
                                    {
                                        intGateId = Convert.ToInt32(arrGate[0][SO_GateTable.GATEID_FLD]);
                                    }
                                }

                                //Check if this schedule exist, only update quantity
                                string strFilter = SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" +
                                                   objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]
                                                   + " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" +
                                                   dateDeli + "'";
                                if (intGateId != 0)
                                {
                                    strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + "=" + intGateId;
                                }
                                else
                                {
                                    strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + " IS NULL";
                                }

                                DataRow[] arrSchRows =
                                    dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Select(strFilter);
                                if (arrSchRows.Length > 0)
                                {
                                    arrSchRows[0][tempQtyColName] =
                                        decimal.Parse(arrSchRows[0][tempQtyColName].ToString()) + dcmQty;
                                    continue;
                                }

                                DataRow drDeli = dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].NewRow();
                                drDeli[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD] =
                                    objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD];
                                if (dtImportData.Rows[j][i] == DBNull.Value)
                                {
                                    continue;
                                }
                                drDeli[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmQty;
                                drDeli[tempQtyColName] = dcmQty;
                                drDeli[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dateDeli;
                                drDeli[SO_DeliveryScheduleTable.LINE_FLD] = intLine;
                                if (intGateId > 0)
                                {
                                    drDeli[SO_DeliveryScheduleTable.GATEID_FLD] = intGateId;
                                }
                                dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Rows.Add(drDeli);
                            }
                        }
                    }
                }

                //refine DelSch data
                int intNewLine = 0;
                DataRow[] arrRows = dstDeli.Tables[0].Select(string.Empty, SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD);
                int intLastDetailId = -1;
                foreach (DataRow t in arrRows)
                {
                    int intDetailId = int.Parse(t[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());

                    if (intDetailId != intLastDetailId)
                    {
                        //reset line
                        intNewLine = 0;
                        intLastDetailId = intDetailId;
                    }
                    if (decimal.Parse(t[tempQtyColName].ToString()) == 0)
                    {
                        t.Delete();
                    }
                    else
                    {
                        //update line
                        t[SO_DeliveryScheduleTable.LINE_FLD] = ++intNewLine;
                        //only update qty
                        decimal dcmNewQTy = decimal.Parse(t[tempQtyColName].ToString());
                        t[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmNewQTy;
                    }
                }

                dsSODeli.UpdateDataSet(dstDeli);
                dstDeli.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Remove(objCol);
            }
            return intSOMasterId;
        }

        public DataSet GetAllDeliveryLine(int pintSaleOrderMasterId)
        {
            var dsDelSch = new SO_DeliveryScheduleDS();
            return dsDelSch.GetDeliveryScheduleBySOMaster(pintSaleOrderMasterId);
        }

        public void ImportUpdateSaleOrder(object pvoSOMaster, DataSet pdstSODetail, DataTable dtImportData,
                                          DataSet pdstDelSchData, ref int pintErrorLine, DataSet pdstGateData)
        {
            const string tempQtyColName = "TempQty";
            const string methodName = This + ".ImportUpdateSaleOrder()";
            const string sumcommitquantityFld = "SUMCommitQuantity";
            int intMonth = 0, intYear = 0;
            string strMmyyyy = dtImportData.Rows[0][0].ToString();
            const string dateSlash = "/";
            try
            {
                intMonth = int.Parse(strMmyyyy.Substring(0, strMmyyyy.IndexOf(dateSlash)));
                intYear = int.Parse(strMmyyyy.Substring(strMmyyyy.IndexOf(dateSlash) + dateSlash.Length));
            }
            catch (InvalidCastException)
            {
            }

            if (pdstSODetail.Tables.Count > 0)
            {
                int intRowIdx = 0;

                foreach (DataRow drowSODetail in pdstSODetail.Tables[0].Rows)
                {
                    if (drowSODetail.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    if (int.Parse(drowSODetail[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString()) == -1)
                    {
                        //delete all related 
                        int intDetailId = int.Parse(drowSODetail[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
                        try
                        {
                            //offline
                            DataRow[] arrDeliverySchedule =
                                pdstDelSchData.Tables[0].Select(SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" +
                                                                intDetailId);
                            foreach (DataRow drowDeliverySchedule in arrDeliverySchedule)
                            {
                                if (Convert.ToDecimal(drowDeliverySchedule[sumcommitquantityFld]) == 0)
                                {
                                    drowDeliverySchedule.Delete();
                                }
                                else
                                {
                                    throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED, methodName, null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //cannot delete commited schedule
                            pintErrorLine = intRowIdx + IntBeginDataRow;
                            throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED, methodName, ex);
                        }
                        drowSODetail.Delete();
                        continue;
                    }
                    drowSODetail[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] =
                        ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID;
                    intRowIdx++;
                }
                var dsDetail = new SO_SaleOrderDetailDS();

                //update deleted delivery schedule in dataset
                var dsDelSch = new SO_DeliveryScheduleDS();
                dsDelSch.UpdateDeletedRowInDataSet(pdstDelSchData,
                                                   ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID);

                //update sale order detail dataset
                dsDetail.UpdateDataSetForImport(pdstSODetail, ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID);

                //get delivery schedules schema
                var dsSODeli = new SO_DeliveryScheduleDS();
                //Add temp column for new quantity
                var objCol = new DataColumn(tempQtyColName) {DataType = typeof (decimal), DefaultValue = 0};
                pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Add(objCol);

                //insert delivery schedules
                int intEndDataCol = dtImportData.Columns.Count - 1;
                int intEndDataRow = dtImportData.Rows.Count;
                intRowIdx = -1;
                foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
                {
                    intRowIdx++;
                    int intLine = 0;
                    for (int i = IntBeginDataCol; i < intEndDataCol; i++)
                    {
                        for (int j = IntBeginDataRow; j < intEndDataRow; j++)
                        {
                            if (
                                dtImportData.Rows[j][IntPartsNoCol].ToString().Equals(
                                    objRow[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD].ToString()))
                            {
                                int intDay = int.Parse(dtImportData.Rows[IntHeaderRow][i].ToString());
                                int intHour = int.Parse(dtImportData.Rows[j][IntTimeCol].ToString());
                                var dateDeli = new DateTime(intYear, intMonth, intDay, intHour, 0, 0);
                                decimal dcmQty;
                                if (dtImportData.Rows[j][i] == DBNull.Value)
                                {
                                    continue;
                                }
                                intLine++;
                                try
                                {
                                    dcmQty = decimal.Parse(dtImportData.Rows[j][i].ToString());
                                    if (dcmQty <= 0)
                                    {
                                        continue;
                                    }
                                }
                                catch
                                {
                                    continue;
                                }

                                //findout gate code
                                string strGateCode = dtImportData.Rows[j][IntCodeCol].ToString().Trim();
                                int intGateId = 0;
                                if (strGateCode != string.Empty)
                                {
                                    //lookup gate id
                                    DataRow[] arrGate =
                                        pdstGateData.Tables[0].Select(SO_GateTable.CODE_FLD + "='" + strGateCode + "'");
                                    if (arrGate.Length == 0)
                                    {
                                        //never occurs
                                    }
                                    else
                                    {
                                        intGateId = Convert.ToInt32(arrGate[0][SO_GateTable.GATEID_FLD]);
                                    }
                                }

                                //Check if this schedule exist, only update quantity
                                string strFilter = SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" +
                                                   objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD]
                                                   + " AND " + SO_DeliveryScheduleTable.SCHEDULEDATE_FLD + "='" +
                                                   dateDeli + "'";

                                if (intGateId > 0)
                                {
                                    strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + "=" + intGateId;
                                }
                                else
                                {
                                    strFilter += " AND " + SO_DeliveryScheduleTable.GATEID_FLD + " IS NULL";
                                }

                                DataRow[] arrSchRows =
                                    pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Select(strFilter);
                                if (arrSchRows.Length > 0)
                                {
                                    arrSchRows[0][tempQtyColName] =
                                        decimal.Parse(arrSchRows[0][tempQtyColName].ToString()) + dcmQty;
                                    if (intGateId > 0)
                                    {
                                        arrSchRows[0][SO_DeliveryScheduleTable.GATEID_FLD] = intGateId;
                                    }
                                    else
                                    {
                                        arrSchRows[0][SO_DeliveryScheduleTable.GATEID_FLD] = DBNull.Value;
                                    }
                                    continue;
                                }

                                DataRow drDeli = pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].NewRow();
                                drDeli[tempQtyColName] = dcmQty;
                                drDeli[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmQty;
                                drDeli[SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD] =
                                    objRow[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD];
                                drDeli[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD] = dateDeli;
                                drDeli[SO_DeliveryScheduleTable.LINE_FLD] = intLine;
                                if (intGateId > 0)
                                {
                                    drDeli[SO_DeliveryScheduleTable.GATEID_FLD] = intGateId;
                                }
                                else
                                {
                                    drDeli[SO_DeliveryScheduleTable.GATEID_FLD] = DBNull.Value;
                                }
                                pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Rows.Add(drDeli);
                            }
                        }
                    }
                }

                //refine DelSch data
                int intNewLine = 0;
                DataRow[] arrRows = pdstDelSchData.Tables[0].Select(string.Empty,
                                                                    SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD);
                int intLastDetailId = -1;
                int intCurrentProductId = -1;
                var dsCommit = new SO_CommitInventoryDetailDS();

                intRowIdx = -1;
                foreach (DataRow t in arrRows)
                {
                    int intDetailId = int.Parse(t[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());

                    if (intDetailId != intLastDetailId)
                    {
                        //reset line
                        intNewLine = 0;
                        intLastDetailId = intDetailId;
                        intRowIdx++;

                        //get new product id
                        DataRow[] arrDetails =
                            pdstSODetail.Tables[SO_SaleOrderDetailTable.TABLE_NAME].Select(
                                SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD + "=" + intLastDetailId);
                        //check if not found? not needed
                        intCurrentProductId = int.Parse(arrDetails[0][SO_SaleOrderDetailTable.PRODUCTID_FLD].ToString());
                    }
                    if (decimal.Parse(t[tempQtyColName].ToString()) == 0)
                    {
                        //Not exist in excel file
                        int intMasterSoid = ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID;
                        int intDeliId = int.Parse(t[SO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString());
                        decimal dcmRemainQty = dsCommit.GetRemainQuantity(intMasterSoid, intDeliId, intCurrentProductId);
                        decimal dcmOldQty =
                            decimal.Parse(t[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
                        if (dcmRemainQty != dcmOldQty)
                        {
                            pintErrorLine = intRowIdx + IntBeginDataRow;
                            throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED, methodName, null);
                        }
                        t.Delete();
                    }
                    else
                    {
                        if (t.RowState != DataRowState.Added)
                        {
                            //update line
                            t[SO_DeliveryScheduleTable.LINE_FLD] = ++intNewLine;
                            decimal dcmCommittedQty = Convert.ToDecimal(t[sumcommitquantityFld]);
                            decimal dcmNewQTy = decimal.Parse(t[tempQtyColName].ToString());
                            if (dcmCommittedQty > dcmNewQTy)
                            {
                                //Cannot update quantity less than commited
                                pintErrorLine = intRowIdx + IntBeginDataRow;
                                throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED, methodName, null);
                            }
                            t[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = dcmNewQTy;
                        }
                        else
                        {
                            //update line
                            t[SO_DeliveryScheduleTable.LINE_FLD] = ++intNewLine;
                            t[SO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD] = t[tempQtyColName];
                        }
                    }
                }

                pdstDelSchData.Tables[SO_DeliveryScheduleTable.TABLE_NAME].Columns.Remove(objCol);
                dsSODeli.UpdateInsertedRowInDataSet(pdstDelSchData,
                                                    ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID);
            }
            pintErrorLine = -1;
        }

        public void ImportUpdateSaleOrder(object pvoSOMaster, DataSet pdstSODetail, DataTable dtImportData,
                                          DataSet pdstDelSchData, ref int pintErrorLine)
        {
            const string methodName = This + ".ImportUpdateSaleOrder()";
            const string sumcommitquantityFld = "SUMCommitQuantity";

            if (pdstSODetail.Tables.Count > 0)
            {
                int intRowIdx = 0;

                foreach (DataRow drowSODetail in pdstSODetail.Tables[0].Rows)
                {
                    if (drowSODetail.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    if (int.Parse(drowSODetail[SO_SaleOrderDetailTable.SALEORDERLINE_FLD].ToString()) == -1)
                    {
                        //delete all related 
                        int intDetailId =
                            int.Parse(drowSODetail[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
                        try
                        {
                            //offline
                            DataRow[] arrDeliverySchedule =
                                pdstDelSchData.Tables[0].Select(SO_DeliveryScheduleTable.SALEORDERDETAILID_FLD + "=" +
                                                                intDetailId);
                            foreach (DataRow drowDeliverySchedule in arrDeliverySchedule)
                            {
                                if (Convert.ToDecimal(drowDeliverySchedule[sumcommitquantityFld]) == 0)
                                {
                                    drowDeliverySchedule.Delete();
                                }
                                else
                                {
                                    throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED,
                                                             methodName, null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //cannot delete commited schedule
                            pintErrorLine = intRowIdx + IntBeginDataRow;
                            throw new PCSDBException(ErrorCode.MESSAGE_IMPORT_ERROR_UPDATE_COMMITTED, methodName, ex);
                        }
                        drowSODetail.Delete();
                        continue;
                    }
                    drowSODetail[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] =
                        ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID;
                    intRowIdx++;
                }
                var dsDetail = new SO_SaleOrderDetailDS();

                //update deleted delivery schedule in dataset
                var dsDelSch = new SO_DeliveryScheduleDS();
                dsDelSch.UpdateDeletedRowInDataSet(pdstDelSchData,
                                                   ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID);

                //update sale order detail dataset
                dsDetail.UpdateDataSetForImport(pdstSODetail, ((SO_SaleOrderMasterVO) pvoSOMaster).SaleOrderMasterID);
            }
            pintErrorLine = -1;
        }

        /// <summary>
        /// Get customer item reference by partyid and productid
        /// </summary>
        /// <param name="pintProductId"></param>
        /// <param name="pintCustomerId"></param>
        /// <returns></returns>
        public object GetItemCustomerRef(int pintProductId, int pintCustomerId)
        {
            return new SO_CustomerItemRefDetailDS().GetObjectVO(pintProductId, pintCustomerId);
        }

        /// <summary>
        /// CheckSORelease
        /// </summary>
        /// <param name="pintSaleOrderMasterId"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Friday,April 7 2006</date>
        public bool CheckSOReleased(int pintSaleOrderMasterId)
        {
            var dsCommitInventoryMaster = new SO_CommitInventoryMasterDS();
            DataSet dstSOCommitted =
                dsCommitInventoryMaster.ListCommittedInventoryMasterBySaleOrderMasterID(pintSaleOrderMasterId);
            if (dstSOCommitted.Tables[0].Rows.Count > 0) return true;
            return false;
        }

        public decimal GetUnitPriceDefualt(int pProductId, int pCustomerId)
        {
            return (new SO_SaleOrderDetailDS()).GetUnitPriceDefualt(pProductId, pCustomerId);
        }

        public int ImportNewSaleOrder(object pvoSOMaster, DataSet pdstSODetail, DataTable dtImportData)
        {
            var dsMaster = new SO_SaleOrderMasterDS();
            int intSOMasterId = dsMaster.AddAndReturnID(pvoSOMaster);

            if (pdstSODetail.Tables.Count > 0)
            {
                foreach (DataRow objRow in pdstSODetail.Tables[0].Rows)
                {
                    if (objRow.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    objRow[SO_SaleOrderDetailTable.SALEORDERMASTERID_FLD] = intSOMasterId;
                }
                var dsDetail = new SO_SaleOrderDetailDS();
                dsDetail.UpdateDataSetForImport(pdstSODetail, intSOMasterId);
            }
            return intSOMasterId;
        }

        public DataSet ListScheduleForImport(int pintMasterId)
        {
            var dsSODeli = new SO_DeliveryScheduleDS();
            return dsSODeli.ListForImport(pintMasterId);
        }

        public void UpdateScheduleForImport(DataSet pdstData)
        {
            var dsSchedule = new SO_DeliveryScheduleDS();
            dsSchedule.UpdateDataSet(pdstData);
        }

        public void UpdateInsertedRowInDataSet(DataSet pdstDelSchData, int pintMasterId)
        {
            var dsSchedule = new SO_DeliveryScheduleDS();
            dsSchedule.UpdateInsertedRowInDataSet(pdstDelSchData, pintMasterId);
        }

        public DataTable GetRemainQuantity(int pintMasterId)
        {
            var dsCommit = new SO_CommitInventoryDetailDS();
            return dsCommit.GetRemainQuantity(pintMasterId);
        }

        public DataSet GetListDetail(int printMasterId, int printCustomerRefMasterId)
        {
            var objSODetailDS = new SO_SaleOrderDetailDS();
            return objSODetailDS.GetListDetail(printMasterId, printCustomerRefMasterId);
        }

        public void UpdateUnitPrice(DataSet pdstSODetail)
        {
            if (pdstSODetail != null && pdstSODetail.Tables.Count > 0)
            {
                var dsDetail = new SO_SaleOrderDetailDS();
                dsDetail.UpdateDataSet(pdstSODetail);
            }
        }

        #region // HACK: DuongNA 2005-10-20

        private const int IntHeaderRow = 1;
        private const int IntBeginDataRow = 3;
        private const int IntPartsNoCol = 1;
        private const int IntTimeCol = 4;
        private const int IntCodeCol = 5;
        private const int IntBeginDataCol = 6;


        public int ImportNewMappingData(DataTable dtImpData, int intPartyId, int intCcnid, int intMaxLine,
                                        DataSet dstMappingData, DataSet dstGateData)
        {
            //add new row for gate in detail table
            if (!dstMappingData.Tables[0].Columns.Contains(SO_DeliveryScheduleTable.GATEID_FLD))
            {
                var dcolGateId = new DataColumn(SO_DeliveryScheduleTable.GATEID_FLD, typeof (int));
                dstMappingData.Tables[0].Columns.Add(dcolGateId);
            }
            int intResult = 0;
            int intMaxId;
            dstMappingData.Tables[0].DefaultView.Sort = SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD;
            try
            {
                intMaxId =
                    int.Parse(
                        dstMappingData.Tables[0].Rows[dstMappingData.Tables[0].Rows.Count - 1][
                            SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
            }
            catch
            {
                intMaxId = 0;
            }
            dstMappingData.Tables[0].DefaultView.Sort = string.Empty;

            //walk through data	
            var dsMasterRef = new SO_CustomerItemRefMasterDS();
            var voMasterRef = (SO_CustomerItemRefMasterVO) dsMasterRef.GetObjectVO(intPartyId, intCcnid);

            var dsDetailRef = new SO_CustomerItemRefDetailDS();
            var dsProduct = new ITM_ProductDS();

            for (int i = IntBeginDataRow; i < dtImpData.Rows.Count; i++)
            {
                //findout gate code
                string strGateCode = dtImpData.Rows[i][IntCodeCol].ToString().Trim();
                int intGateId = 0;
                if (strGateCode != string.Empty)
                {
                    //lookup gate id
                    DataRow[] arrGate = dstGateData.Tables[0].Select(SO_GateTable.CODE_FLD + "='" + strGateCode + "'");
                    if (arrGate.Length == 0)
                    {
                        intResult = -i;
                        //negative mean that error is caused by gate code not found
                        //positive mean that error is caused by item mapping not exist
                        break;
                    }
                    intGateId = Convert.ToInt32(arrGate[0][SO_GateTable.GATEID_FLD]);
                }

                //findout Customer Code
                string strCustomerCode = dtImpData.Rows[i][IntPartsNoCol].ToString();

                //find out total quantity at last column
                decimal dcmOrderQty = decimal.Parse(dtImpData.Rows[i][dtImpData.Columns.Count - 1].ToString());

                //check if this item existed, update quantity only
                DataRow[] arrRows =
                    dstMappingData.Tables[0].Select(SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + "='" +
                                                    strCustomerCode + "'");
                if (arrRows.Length > 0)
                {
                    arrRows[0][SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] =
                        decimal.Parse(arrRows[0][SO_SaleOrderDetailTable.ORDERQUANTITY_FLD].ToString()) + dcmOrderQty;
                    continue;
                }

                if (dcmOrderQty <= 0)
                {
                    continue;
                }

                //findout master_ref_id
                int intMasterRefId = voMasterRef.CustomerItemRefMasterID;
                if (intMasterRefId == 0)
                {
                    intResult = i;
                    break;
                }

                //find out product_id
                var voDetailRef = (SO_CustomerItemRefDetailVO) dsDetailRef.GetObjectVO(intMasterRefId, strCustomerCode);

                int intProductId = voDetailRef.ProductID;
                if (intProductId == 0)
                {
                    intResult = i;
                    break;
                }
                var voProduct = (ITM_ProductVO) dsProduct.GetObjectVO(intProductId);

                //New row
                DataRow dr = dstMappingData.Tables[0].NewRow();

                var boUtils = new UtilsBO();
                //fill row
                dr[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = dcmOrderQty;
                dr[SO_SaleOrderDetailTable.PRODUCTID_FLD] = intProductId;
                dr[SO_SaleOrderDetailTable.SELLINGUMID_FLD] = voDetailRef.UnitOfMeasureID;
                dr[SO_SaleOrderDetailTable.UNITPRICE_FLD] = voDetailRef.UnitPrice;
                dr[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = voDetailRef.UnitPrice*dcmOrderQty;
                dr[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = dtImpData.Rows[i][IntPartsNoCol];
                dr[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = voDetailRef.CustomerItemModel;
                dr[SO_SaleOrderDetailTable.UMRATE_FLD] = boUtils.GetUMRate(voProduct.StockUMID,
                                                                           voDetailRef.UnitOfMeasureID);
                dr[SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = ++intMaxLine;
                dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] = ++intMaxId;
                dr[SO_SaleOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
                if (intGateId > 0)
                {
                    dr[SO_DeliveryScheduleTable.GATEID_FLD] = intGateId; //lookup for gateid
                }
                dstMappingData.Tables[0].Rows.Add(dr);
            }

            return intResult;
        }


        public int ImportUpdateMappingData(DataTable dtImpData, int intPartyId, int intCcnid, int intMaxLine,
                                           DataSet dstMappingData, DataSet dstGateData)
        {
            int intResult = 0;
            const string tempQtyColName = "TempQty";

            //Add new column for temp qty
            var objCol = new DataColumn(tempQtyColName) {DataType = typeof (Decimal), DefaultValue = 0};
            dstMappingData.Tables[0].Columns.Add(objCol);
            //add new row for gate in detail table
            if (!dstMappingData.Tables[0].Columns.Contains(SO_DeliveryScheduleTable.GATEID_FLD))
            {
                var dcolGateId = new DataColumn(SO_DeliveryScheduleTable.GATEID_FLD, typeof (int));
                dstMappingData.Tables[0].Columns.Add(dcolGateId);
            }

            int intMaxId;
            dstMappingData.Tables[0].DefaultView.Sort = SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD;
            try
            {
                intMaxId =
                    int.Parse(
                        dstMappingData.Tables[0].Rows[dstMappingData.Tables[0].Rows.Count - 1][
                            SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD].ToString());
            }
            catch
            {
                intMaxId = 0;
            }
            dstMappingData.Tables[0].DefaultView.Sort = string.Empty;
            var dsMasterRef = new SO_CustomerItemRefMasterDS();
            var voMasterRef = (SO_CustomerItemRefMasterVO) dsMasterRef.GetObjectVO(intPartyId, intCcnid);
            var dsProduct = new ITM_ProductDS();

            for (int i = IntBeginDataRow; i < dtImpData.Rows.Count; i++)
            {
                //findout gate code
                string strGateCode = dtImpData.Rows[i][IntCodeCol].ToString().Trim();
                int intGateId = 0;
                if (strGateCode != string.Empty)
                {
                    //lookup gate id
                    DataRow[] arrGate = dstGateData.Tables[0].Select(SO_GateTable.CODE_FLD + "='" + strGateCode + "'");
                    if (arrGate.Length == 0)
                    {
                        intResult = -i;
                        break;
                    }
                    intGateId = Convert.ToInt32(arrGate[0][SO_GateTable.GATEID_FLD]);
                }
                //findout Customer Code
                string strCustomerCode = dtImpData.Rows[i][IntPartsNoCol].ToString();
                //find out total quantity at last column
                decimal dcmOrderQty = int.Parse(dtImpData.Rows[i][dtImpData.Columns.Count - 1].ToString());

                //check if this item existed, update quantity only
                DataRow[] arrRows =
                    dstMappingData.Tables[0].Select(SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD + "='" +
                                                    strCustomerCode + "'");
                if (arrRows.Length > 0)
                {
                    arrRows[0][tempQtyColName] = decimal.Parse(arrRows[0][tempQtyColName].ToString()) +
                                                    dcmOrderQty;
                    if (intGateId > 0)
                    {
                        arrRows[0][SO_DeliveryScheduleTable.GATEID_FLD] = intGateId;
                    }
                    else
                    {
                        arrRows[0][SO_DeliveryScheduleTable.GATEID_FLD] = DBNull.Value;
                    }
                    continue;
                }
                if (dcmOrderQty <= 0)
                {
                    continue;
                }

                //else

                //findout master_ref_id
                int intMasterRefId = voMasterRef.CustomerItemRefMasterID;
                if (intMasterRefId == 0)
                {
                    intResult = i;
                    break;
                }

                //find out product_id
                var dsDetailRef = new SO_CustomerItemRefDetailDS();
                var voDetailRef = (SO_CustomerItemRefDetailVO) dsDetailRef.GetObjectVO(intMasterRefId, strCustomerCode);
                int intProductId = voDetailRef.ProductID;
                if (intProductId == 0)
                {
                    intResult = i;
                    break;
                }
                var voProduct = (ITM_ProductVO) dsProduct.GetObjectVO(intProductId);

                //New row
                DataRow dr = dstMappingData.Tables[0].NewRow();

                var boUtils = new UtilsBO();
                //fill row
                dr[tempQtyColName] = dcmOrderQty;
                dr[SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] = dcmOrderQty;
                dr[SO_SaleOrderDetailTable.PRODUCTID_FLD] = intProductId;
                dr[SO_SaleOrderDetailTable.SELLINGUMID_FLD] = voDetailRef.UnitOfMeasureID;
                dr[SO_SaleOrderDetailTable.UNITPRICE_FLD] = voDetailRef.UnitPrice;
                dr[SO_SaleOrderDetailTable.TOTALAMOUNT_FLD] = voDetailRef.UnitPrice*dcmOrderQty;
                dr[SO_SaleOrderDetailTable.ITEMCUSTOMERCODE_FLD] = dtImpData.Rows[i][IntPartsNoCol];
                dr[SO_SaleOrderDetailTable.ITEMCUSTOMERREVISION_FLD] = voDetailRef.CustomerItemModel;
                    //dtImpData.Rows[i][INT_PARTS_NAME_COL];
                dr[SO_SaleOrderDetailTable.UMRATE_FLD] = boUtils.GetUMRate(voProduct.StockUMID,
                                                                           voDetailRef.UnitOfMeasureID);
                dr[SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = ++intMaxLine;
                dr[SO_SaleOrderDetailTable.SALEORDERDETAILID_FLD] = ++intMaxId;
                dr[SO_SaleOrderDetailTable.STOCKUMID_FLD] = voProduct.StockUMID;
                if (intGateId > 0)
                {
                    dr[SO_DeliveryScheduleTable.GATEID_FLD] = intGateId;
                }
                dr[tempQtyColName] = dcmOrderQty;
                dstMappingData.Tables[0].Rows.Add(dr);
            }
            if (intResult != 0)
            {
                dstMappingData.Tables[0].Columns.Remove(objCol);
                return intResult;
            }
            //refine data, with correct line
            int intLine = 1;
            for (int i = 0; i < dstMappingData.Tables[0].Rows.Count; i++)
            {
                if (int.Parse(dstMappingData.Tables[0].Rows[i][tempQtyColName].ToString()) == 0)
                {
                    dstMappingData.Tables[0].Rows[i][SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = -1;
                }
                else
                {
                    //Update Line
                    dstMappingData.Tables[0].Rows[i][SO_SaleOrderDetailTable.SALEORDERLINE_FLD] = intLine;
                    //Update quantity
                    dstMappingData.Tables[0].Rows[i][SO_SaleOrderDetailTable.ORDERQUANTITY_FLD] =
                        dstMappingData.Tables[0].Rows[i][tempQtyColName];
                    intLine++;
                }
            }

            dstMappingData.Tables[0].Columns.Remove(objCol);
            return intResult;
        }

        #endregion // END: DuongNA 2005-10-20
    }
}
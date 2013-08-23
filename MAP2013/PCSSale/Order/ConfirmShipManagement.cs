using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using C1.C1Report;
using C1.Win.C1Input;
using C1.Win.C1List;
using PCSComSale.Order.BO;
using PCSComSale.Order.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataAccess;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;

namespace PCSSale.Order
{
    /// <summary>
    /// Summary description for ConfirmShipManagerment.
    /// </summary>
    public partial class ConfirmShipManagement : Form
    {
        private const string This = "PCSSale.Order.ConfirmShipManagement";
        private const string ViewForInvoice = "v_SaleInvoice";
        private const string CommittedqtyCol = "CommittedQuantity";
        private const string OldInvoiceqtyCol = "OldInvoiceQty";
        private readonly SOConfirmShipManagementBO _boCsManagement = new SOConfirmShipManagementBO();

        private readonly SO_InvoiceMasterVO _voInvoiceMaster = new SO_InvoiceMasterVO();
        private readonly MST_MasterLocationVO _voMasLoc = new MST_MasterLocationVO();
        private readonly SO_ConfirmShipMasterVO _voMaster = new SO_ConfirmShipMasterVO();
        private bool _hasError;
        private bool _validatingError;
        private UtilsBO _boUtils;
        private int _soMasterId;
        private int _saleType = 1; // default is Domestic
        private EnumAction _formAction = EnumAction.Default;
        private string _gateIds = string.Empty;
        private SO_SaleOrderMasterVO _voSOMaster = new SO_SaleOrderMasterVO();
        private List<int> _removedId = new List<int>();

        #region Private Methods

        private void FillSaleOrderData(DataRow pdrowData)
        {
            const string MASLOC_FLD = "MasLoc";
            const string SALEORDER_FLD = "SaleOrder";
            const string CUSTOMERCODE_FLD = "CustomerCode";
            const string CUSTOMERNAME_FLD = "CustomerName";
            // get all sale order details of selected sale order master id
            if (_formAction == EnumAction.Add)
            {
                //pdrowData contains SO row
                _voSOMaster.SaleOrderMasterID = int.Parse(pdrowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
                _voSOMaster = (SO_SaleOrderMasterVO) _boCsManagement.GetSaleOrderMasterVO(_voSOMaster.SaleOrderMasterID);
                txtSalesOrder.Text = _voSOMaster.Code;
                txtSalesOrder.Tag = _voSOMaster;
                txtMasLoc.Text = pdrowData[MASLOC_FLD].ToString();
                txtCustomerCode.Text = pdrowData[CUSTOMERCODE_FLD].ToString();
                txtCustomerName.Text = pdrowData[CUSTOMERNAME_FLD].ToString();
                txtPaymentTerm.Text = pdrowData[MST_PaymentTermTable.TABLE_NAME + MST_PaymentTermTable.CODE_FLD].ToString();
            }
            else // modify
            {
                int intCCNID = int.Parse(pdrowData[MST_CCNTable.CCNID_FLD].ToString());
                int intMasterLocationID = int.Parse(pdrowData[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                //pdrowData contains Confirm shipment row
                if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                {
                    _voMaster.ConfirmShipMasterID =
                        Convert.ToInt32(pdrowData[SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD]);
                    _voMaster.CCNID = intCCNID;
                    _voMaster.MasterLocationID = intMasterLocationID;
                    //get detail data
                    dstData = _boCsManagement.GetExistedForView(_voMaster.ConfirmShipMasterID, false);
                }
                else
                {
                    _voInvoiceMaster.InvoiceMasterID =
                        Convert.ToInt32(pdrowData[SO_InvoiceMasterTable.INVOICEMASTERID_FLD]);
                    _voInvoiceMaster.CCNID = intCCNID;
                    _voInvoiceMaster.MasterLocationID = intMasterLocationID;
                    //get detail data
                    dstData = _boCsManagement.GetExistedForView(_voInvoiceMaster.InvoiceMasterID, true);
                }
                if (dstData != null)
                {
                    if (dstData.Tables.Count > 0)
                    {
                        if (dstData.Tables[0].Rows[0]["BCode"] != DBNull.Value)
                        {
                            txtBin.Text = dstData.Tables[0].Rows[0]["BCode"].ToString();
                            txtBin.Tag = dstData.Tables[0].Rows[0]["BinID"];
                        }
                        if (dstData.Tables[0].Rows[0]["LCode"] != DBNull.Value)
                        {
                            txtLocation.Text = dstData.Tables[0].Rows[0]["LCode"].ToString();
                            txtLocation.Tag = dstData.Tables[0].Rows[0]["LocationID"];
                        }
                    }
                }

                _voSOMaster.SaleOrderMasterID =
                    int.Parse(pdrowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
                _voSOMaster.Code = pdrowData[SALEORDER_FLD].ToString();
                txtSalesOrder.Text = _voSOMaster.Code;
                txtConfirmShipNo.Text = pdrowData[SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD].ToString();
                dtmShipmentDate.Value = pdrowData[SO_ConfirmShipMasterTable.SHIPPEDDATE_FLD];
                txtSalesOrder.Tag = _voSOMaster;
                txtCurrency.Text = pdrowData[MST_CurrencyTable.CODE_FLD].ToString();
                txtCurrency.Tag = pdrowData[MST_CurrencyTable.CURRENCYID_FLD];
                txtExchRate.Value = pdrowData[SO_ConfirmShipMasterTable.EXCHANGERATE_FLD];
                txtSaleType.Text = pdrowData[SO_TypeTable.TABLE_NAME + SO_TypeTable.CODE_FLD].ToString();
                txtSaleType.Tag = pdrowData[SO_TypeTable.TYPEID_FLD];
                txtPaymentTerm.Text =
                    pdrowData[MST_PaymentTermTable.TABLE_NAME + MST_PaymentTermTable.CODE_FLD].ToString();
                txtMasLoc.Text = pdrowData[MASLOC_FLD].ToString();
                txtMasLoc.Tag = pdrowData[MST_MasterLocationTable.MASTERLOCATIONID_FLD];
                txtCustomerCode.Text = pdrowData[CUSTOMERCODE_FLD].ToString();
                txtCustomerName.Text = pdrowData[CUSTOMERNAME_FLD].ToString();
                txtShippingCode.Text = pdrowData[SO_ConfirmShipMasterTable.SHIPCODE_FLD].ToString();
                txtFromPort.Text = pdrowData[SO_ConfirmShipMasterTable.FROMPORT_FLD].ToString();
                txtCNo.Text = pdrowData[SO_ConfirmShipMasterTable.CNO_FLD].ToString();
                if (pdrowData[SO_ConfirmShipMasterTable.MEASUREMENT_FLD] != DBNull.Value)
                    txtMeasurement.Value = Convert.ToDecimal(pdrowData[SO_ConfirmShipMasterTable.MEASUREMENT_FLD]);
                if (pdrowData[SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD] != DBNull.Value)
                    txtGrossWeight.Value = Convert.ToDecimal(pdrowData[SO_ConfirmShipMasterTable.GROSSWEIGHT_FLD]);
                if (pdrowData[SO_ConfirmShipMasterTable.NETWEIGHT_FLD] != DBNull.Value)
                    txtNetWeight.Value = Convert.ToDecimal(pdrowData[SO_ConfirmShipMasterTable.NETWEIGHT_FLD]);
                txtIssuingBank.Text = pdrowData[SO_ConfirmShipMasterTable.ISSUINGBANK_FLD].ToString();
                txtLCNo.Text = pdrowData[SO_ConfirmShipMasterTable.LCNO_FLD].ToString();
                txtVessel.Text = pdrowData[SO_ConfirmShipMasterTable.VESSELNAME_FLD].ToString();
                txtComment.Text = pdrowData[SO_ConfirmShipMasterTable.COMMENT_FLD].ToString();
                txtReferenceNo.Text = pdrowData[SO_ConfirmShipMasterTable.REFERENCENO_FLD].ToString();
                txtInvoiceNo.Text = pdrowData[SO_ConfirmShipMasterTable.INVOICENO_FLD].ToString();
                // invoice date
                dtmInvoiceDate.Value = (DateTime) pdrowData[SO_ConfirmShipMasterTable.INVOICEDATE_FLD];
                if (pdrowData[SO_ConfirmShipMasterTable.LCDATE_FLD] != DBNull.Value)
                    dtmLCDate.Value = (DateTime) pdrowData[SO_ConfirmShipMasterTable.LCDATE_FLD];
                if (pdrowData[SO_ConfirmShipMasterTable.ONBOARDDATE_FLD] != DBNull.Value)
                    dtmOnBoardDate.Value = (DateTime) pdrowData[SO_ConfirmShipMasterTable.ONBOARDDATE_FLD];
                txtGate.Enabled = false;
                btnGate.Enabled = false;

                _saleType = Convert.ToInt32(pdrowData[SO_TypeTable.TYPEID_FLD]) == (int) SOType.Export ? 2 : 1;
            }
        }

        private void SwitchFormMode()
        {
            if (_formAction == EnumAction.Add)
            {
                btnShipNo.Enabled = false;
                btnSearchMasLoc.Enabled = true;
                dtmShipmentDate.Enabled = true;
                txtConfirmShipNo.Enabled = true;
                txtMasLoc.Enabled = true;
                txtSalesOrder.Enabled = true;
                btnSO.Enabled = true;
                cboCCN.Enabled = true;
                dgrdData.Enabled = true;
                chkHaveGate.Enabled = true;
                btnSearch.Enabled = true;
                txtShippingCode.Enabled = true;
                txtFromPort.Enabled = true;
                txtCNo.Enabled = true;
                txtSaleType.Enabled = true;
                btnSaleType.Enabled = true;
                txtMeasurement.Enabled = true;
                txtGrossWeight.Enabled = true;
                txtNetWeight.Enabled = true;
                txtIssuingBank.Enabled = true;
                dtmLCDate.Enabled = true;
                txtLCNo.Enabled = true;
                txtVessel.Enabled = true;
                txtComment.Enabled = true;
                txtReferenceNo.Enabled = true;
                txtInvoiceNo.Enabled = true;
                dgrdData.AllowUpdate = false;
                dgrdData.AllowDelete = true;
                txtCurrency.Enabled = true;
                txtExchRate.Enabled = true;
                txtGate.Enabled = false;
                btnGate.Enabled = false;
                dtmOnBoardDate.Enabled = true;
                btnCurrency.Enabled = true;
                dgrdData.AllowAddNew = false;
                dgrdData.AllowUpdate = true;
                cboCCN.Enabled = true;
                btnAdd.Enabled = false;
                btnConfirmShippment.Enabled = true;
                btnPrint.Enabled = false;
                btnAttachedSheet.Enabled = false;
                dtmInvoiceDate.Enabled = true;
                txtLocation.ReadOnly = false;
                btnSearchLocation.Enabled = true;
                txtBin.ReadOnly = false;
                btnSearchBin.Enabled = true;
                dtmFromDate.Enabled = true;
                dtmToDate.Enabled = true;
                cboPurpose.Enabled = false;
            }
            else if (_formAction == EnumAction.Default)
            {
                btnSearchMasLoc.Enabled = false;
                dtmShipmentDate.Enabled = false;
                btnSO.Enabled = false;
                txtSalesOrder.Enabled = false;
                txtMasLoc.Enabled = false;
                dtmOnBoardDate.Enabled = false;
                btnSearch.Enabled = false;
                txtShippingCode.Enabled = false;
                txtGate.Enabled = false;
                btnGate.Enabled = false;
                btnModify.Enabled = false;
                txtFromPort.Enabled = false;
                chkHaveGate.Enabled = false;
                txtCNo.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtMeasurement.Enabled = false;
                txtGrossWeight.Enabled = false;
                txtNetWeight.Enabled = false;
                txtSaleType.Enabled = false;
                btnSaleType.Enabled = false;
                txtIssuingBank.Enabled = false;
                dtmLCDate.Enabled = false;
                txtLCNo.Enabled = false;
                txtVessel.Enabled = false;
                txtComment.Enabled = false;
                txtReferenceNo.Enabled = false;
                foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                {
                    dcolCol.Locked = true;
                }
                dgrdData.AllowUpdate = false;
                dgrdData.AllowDelete = false;
                dgrdData.AllowAddNew = false;
                txtCurrency.Enabled = false;
                txtExchRate.Enabled = false;
                btnCurrency.Enabled = false;
                btnConfirmShippment.Enabled = false;
                btnAttachedSheet.Enabled = true;
                dtmInvoiceDate.Enabled = false;
                dtmFromDate.Enabled = false;
                dtmToDate.Enabled = false;
                txtLocation.ReadOnly = true;
                btnSearchLocation.Enabled = false;
                txtBin.ReadOnly = true;
                btnSearchBin.Enabled = false;
                cboPurpose.Enabled = true;
                if (cboPurpose.SelectedIndex >= 0)
                {
                    btnShipNo.Enabled = true;
                    cboCCN.Enabled = true;
                    btnAdd.Enabled = true;
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnShipNo.Enabled = false;
                    cboCCN.Enabled = false;
                    btnAdd.Enabled = false;
                    btnPrint.Enabled = false;
                }
            }
        }

        #endregion

        #region Event Handlers

        private void btnConfirmShippment_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnConfirmShippment_Click()";

            //Add master record and detail records
            try
            {
                if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_DATA, MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (Security.IsDifferencePrefix(this, lblShipNo, txtConfirmShipNo))
                    {
                        return;
                    }

                    #region Validating Data

                    //Check madatory
                    if (FormControlComponents.CheckMandatory(txtConfirmShipNo))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtConfirmShipNo.Focus();
                        _hasError = true;
                        return;
                    }

                    if (FormControlComponents.CheckMandatory(txtMasLoc))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtMasLoc.Focus();
                        _hasError = true;
                        return;
                    }

                    if (FormControlComponents.CheckMandatory(txtSalesOrder))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtSalesOrder.Focus();
                        _hasError = true;
                        return;
                    }

                    if (FormControlComponents.CheckMandatory(dtmShipmentDate))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        dtmShipmentDate.Focus();
                        _hasError = true;
                        return;
                    }
                    if (FormControlComponents.CheckMandatory(dtmInvoiceDate))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        dtmInvoiceDate.Focus();
                        _hasError = true;
                        return;
                    }
                    // HACK: Trada 12-04-2006
                    if (FormControlComponents.CheckMandatory(txtCurrency))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtCurrency.Focus();
                        _hasError = true;
                        return;
                    }
                    if (chkHaveGate.Checked)
                    {
                        if (FormControlComponents.CheckMandatory(txtGate))
                        {
                            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                            txtGate.Focus();
                            _hasError = true;
                            return;
                        }
                    }
                    if (FormControlComponents.CheckMandatory(txtLocation))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtLocation.Focus();
                        _hasError = true;
                        return;
                    }
                    if (FormControlComponents.CheckMandatory(txtBin))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtBin.Focus();
                        _hasError = true;
                        return;
                    }
                    //Check postdate in configuration
                    if (_formAction == EnumAction.Add)
                    {
                        if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                        {
                            var shipDate = (DateTime) dtmShipmentDate.Value;
                            if (!shipDate.IsValidPostDate())
                            {
                                var period = Utilities.Instance.GetWorkingPeriod();
                                var date = period.ToDate.AddDays(1).AddMilliseconds(-1);
                                var param = new[] {date.ToString(Constants.DATETIME_FORMAT_HOUR)};
                                PCSMessageBox.Show(ErrorCode.MESSAGE_CAN_NOT_EDIT_POSTDATE, MessageBoxIcon.Warning, param);
                                return;
                            }
                        }
                        if (FormControlComponents.CheckMandatory(txtExchRate))
                        {
                            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                            txtExchRate.Focus();
                            _hasError = true;
                            return;
                        }
                    }
                    if (_formAction == EnumAction.Edit)
                    {
                        //Check data in the grid
                        for (int i = 0; i < dgrdData.RowCount; i++)
                        {
                            if (dgrdData[i, SO_ConfirmShipDetailTable.INVOICEQTY_FLD].ToString() == string.Empty)
                            {
                                var strParam = new string[2];
                                strParam[0] = "quantity";
                                strParam[1] = "Invoice Quantity columns";
                                PCSMessageBox.Show(ErrorCode.MESSAGE_RELATION_REQUIRE, MessageBoxIcon.Exclamation,
                                                   strParam);
                                dgrdData.Row = i;
                                dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD]);
                                dgrdData.Focus();
                                _hasError = true;
                                return;
                            }
                        }
                    }
                    
                    //End hack

                    if (dgrdData.RowCount <= 0)
                    {
                        // You have to input at least a record in grid sale order detail
                        PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_AT_LEAST_RECORD_IN_GRID, MessageBoxIcon.Exclamation);
                        dgrdData.Focus();
                        return;
                    }
                    for (int i = 0; i < dgrdData.RowCount; i++)
                    {
                        if (dgrdData[i, SO_ConfirmShipDetailTable.PRICE_FLD].ToString() == String.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                            dgrdData.Row = i;
                            dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.PRICE_FLD]);
                            dgrdData.Focus();
                            _hasError = true;
                            return;
                        }
                        if (decimal.Parse(dgrdData[i, SO_ConfirmShipDetailTable.PRICE_FLD].ToString()) <= 0)
                        {
                            var strParam = new string[2];
                            strParam[0] = SO_ConfirmShipDetailTable.PRICE_FLD;
                            strParam[1] = "0";
                            PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
                            dgrdData.Row = i;
                            dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.PRICE_FLD]);
                            dgrdData.Focus();
                            _hasError = true;
                            return;
                        }
                        if (dgrdData[i, SO_ConfirmShipDetailTable.NETAMOUNT_FLD].ToString() == String.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                            dgrdData.Row = i;
                            dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                            dgrdData.Focus();
                            _hasError = true;
                            return;
                        }
                        if (decimal.Parse(dgrdData[i, SO_ConfirmShipDetailTable.NETAMOUNT_FLD].ToString()) <= 0)
                        {
                            var strParam = new string[2];
                            strParam[0] = SO_ConfirmShipDetailTable.NETAMOUNT_FLD;
                            strParam[1] = "0";
                            PCSMessageBox.Show(ErrorCode.MESSAGE_GREATER_THAN, MessageBoxIcon.Warning, strParam);
                            dgrdData.Row = i;
                            dgrdData.Col = dgrdData.Splits[0].DisplayColumns.IndexOf(dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD]);
                            dgrdData.Focus();
                            _hasError = true;
                            return;
                        }
                    }
                    if (Security.IsDifferencePrefix(this, lblShipNo, txtConfirmShipNo))
                    {
                        return;
                    }
                    // if purpose is Shipping, then check available quantity
                    if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping && !CheckAvailableQuantity())
                    {
                        return;
                    }

                    #endregion

                    if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                    {
                        #region assign data & save to database

                        _voMaster.ConfirmShipNo = txtConfirmShipNo.Text.Trim();
                        _voMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
                        _voMaster.MasterLocationID = _voMasLoc.MasterLocationID = int.Parse(txtMasLoc.Tag.ToString());
                        _voMaster.SaleOrderMasterID = _voSOMaster.SaleOrderMasterID;
                        _voMaster.ShippedDate = new DateTime(((DateTime) dtmShipmentDate.Value).Year,
                                                            ((DateTime) dtmShipmentDate.Value).Month,
                                                            ((DateTime) dtmShipmentDate.Value).Day,
                                                            ((DateTime) dtmShipmentDate.Value).Hour,
                                                            ((DateTime) dtmShipmentDate.Value).Minute, 0);
                        _voMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());
                        _voMaster.ExchangeRate = (Decimal) txtExchRate.Value;
                        _voMaster.ShipCode = txtShippingCode.Text;
                        _voMaster.FromPort = txtFromPort.Text;
                        _voMaster.CNo = txtCNo.Text;

                        _voMaster.Measurement = txtMeasurement.Text != string.Empty
                                                   ? Convert.ToDecimal(txtMeasurement.Value)
                                                   : 0;
                        _voMaster.GrossWeight = txtGrossWeight.Text != string.Empty
                                                   ? Convert.ToDecimal(txtGrossWeight.Value)
                                                   : 0;
                        _voMaster.NetWeight = txtNetWeight.Text != string.Empty
                                                 ? Convert.ToDecimal(txtNetWeight.Value)
                                                 : 0;
                        _voMaster.IssuingBank = txtIssuingBank.Text;
                        _voMaster.LCNo = txtLCNo.Text;
                        _voMaster.VesselName = txtVessel.Text;
                        _voMaster.Comment = txtComment.Text;
                        _voMaster.ReferenceNo = txtReferenceNo.Text;
                        _voMaster.InvoiceNo = txtInvoiceNo.Text;
                        _voMaster.InvoiceDate = (DateTime) dtmInvoiceDate.Value;
                        if (dtmLCDate.Value != DBNull.Value && dtmLCDate.Value != null)
                        {
                            _voMaster.LCDate = Convert.ToDateTime(dtmLCDate.Value);
                        }
                        else
                        {
                            _voMaster.LCDate = DateTime.MinValue;
                        }
                        if (dtmOnBoardDate.Value != DBNull.Value && dtmOnBoardDate.Value != null)
                        {
                            _voMaster.OnBoardDate = Convert.ToDateTime(dtmOnBoardDate.Value);
                        }
                        else
                        {
                            _voMaster.OnBoardDate = DateTime.MinValue;
                        }
                        _voMaster.LocationId = (int) txtLocation.Tag;
                        _voMaster.BinId = (int) txtBin.Tag;
                        if (_formAction == EnumAction.Add)
                        {
                            _voMaster.ConfirmShipMasterID = _boCsManagement.AddShipData(_voMaster, dstData);
                            txtConfirmShipNo.Tag = _voMaster.ConfirmShipMasterID;
                        }
                        else if (_formAction == EnumAction.Edit)
                        {
                            _boCsManagement.ModifyShip(_voMaster, dstData, _removedId);
                        }

                        #endregion
                    }
                    else
                    {
                        #region assign data & save to database

                        _voInvoiceMaster.ConfirmShipNo = txtConfirmShipNo.Text.Trim();
                        _voInvoiceMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
                        _voInvoiceMaster.MasterLocationID =
                            _voMasLoc.MasterLocationID = int.Parse(txtMasLoc.Tag.ToString());
                        _voInvoiceMaster.SaleOrderMasterID = _voSOMaster.SaleOrderMasterID;
                        _voInvoiceMaster.ShippedDate = new DateTime(((DateTime) dtmShipmentDate.Value).Year,
                                                                   ((DateTime) dtmShipmentDate.Value).Month,
                                                                   ((DateTime) dtmShipmentDate.Value).Day,
                                                                   ((DateTime) dtmShipmentDate.Value).Hour,
                                                                   ((DateTime) dtmShipmentDate.Value).Minute, 0);
                        _voInvoiceMaster.CurrencyID = int.Parse(txtCurrency.Tag.ToString());
                        _voInvoiceMaster.ExchangeRate = (Decimal) txtExchRate.Value;
                        _voInvoiceMaster.ShipCode = txtShippingCode.Text;
                        _voInvoiceMaster.FromPort = txtFromPort.Text;
                        _voInvoiceMaster.CNo = txtCNo.Text;

                        _voInvoiceMaster.Measurement = txtMeasurement.Text != string.Empty
                                                          ? Convert.ToDecimal(txtMeasurement.Value)
                                                          : 0;
                        _voInvoiceMaster.GrossWeight = txtGrossWeight.Text != string.Empty
                                                          ? Convert.ToDecimal(txtGrossWeight.Value)
                                                          : 0;
                        _voInvoiceMaster.NetWeight = txtNetWeight.Text != string.Empty
                                                        ? Convert.ToDecimal(txtNetWeight.Value)
                                                        : 0;
                        _voInvoiceMaster.IssuingBank = txtIssuingBank.Text;
                        _voInvoiceMaster.LCNo = txtLCNo.Text;
                        _voInvoiceMaster.VesselName = txtVessel.Text;
                        _voInvoiceMaster.Comment = txtComment.Text;
                        _voInvoiceMaster.ReferenceNo = txtReferenceNo.Text;
                        _voInvoiceMaster.InvoiceNo = txtInvoiceNo.Text;
                        _voInvoiceMaster.InvoiceDate = (DateTime) dtmInvoiceDate.Value;
                        if (dtmLCDate.Value != DBNull.Value && dtmLCDate.Value != null)
                            _voInvoiceMaster.LCDate = Convert.ToDateTime(dtmLCDate.Value);
                        else
                            _voInvoiceMaster.LCDate = DateTime.MinValue;
                        if (dtmOnBoardDate.Value != DBNull.Value && dtmOnBoardDate.Value != null)
                            _voInvoiceMaster.OnBoardDate = Convert.ToDateTime(dtmOnBoardDate.Value);
                        else
                            _voInvoiceMaster.OnBoardDate = DateTime.MinValue;
                        _voInvoiceMaster.BinID = Convert.ToInt32(txtBin.Tag);
                        _voInvoiceMaster.LocationID = Convert.ToInt32(txtLocation.Tag);

                        if (_formAction == EnumAction.Add)
                        {
                            _voInvoiceMaster.InvoiceMasterID = _boCsManagement.AddSOInvoiceMaster(_voInvoiceMaster, dstData);
                            txtConfirmShipNo.Tag = _voInvoiceMaster.InvoiceMasterID;
                        }
                        else if (_formAction == EnumAction.Edit)
                        {
                            _boCsManagement.ModifyInvoice(_voInvoiceMaster, dstData, _removedId);
                        }

                        #endregion
                    }
                    //Get data to edit
                    var isInvoice = cboPurpose.SelectedIndex != (int) ShipViewType.Shipping;
                    dstData = _boCsManagement.GetExistedForView(_voMaster.ConfirmShipMasterID, isInvoice);
                    dgrdData.DataSource = dstData.Tables[0];

                    // restore grid layout
                    FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                    foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                    {
                        dcolCol.Locked = true;
                    }
                    //Format columns
                    dgrdData.Columns[CommittedqtyCol].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.VATPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[IV_AdjustmentTable.AVAILABLEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.PRICE_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    
                    _formAction = EnumAction.Default;
                    SwitchFormMode();
                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                    _hasError = false;
                    txtConfirmShipNo.Enabled = true;
                    btnModify.Enabled = true;
                    txtCurrency.Enabled = false;
                    txtExchRate.Enabled = false;
                    btnCurrency.Enabled = false;
                    _removedId.Clear();
                }
            }
            catch (PCSException ex)
            {
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                {
                    var strParam = new string[1];
                    strParam[0] = ex.mMethod;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_ENOUGH_QUANTITY_OF_COMPONENT_TO_COMPLETE,
                                       MessageBoxIcon.Warning, strParam);
                }
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtConfirmShipNo.Focus();
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfirmShipManagerment_Load(object sender, EventArgs e)
        {
            const string methodName = This + ".ConfirmShipManagerment_Load()";
            try
            {
                #region Apply security

                //Set authorization for user
                var objSecurity = new Security();
                Name = This;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    Close();
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                //Enable CCN combobox
                cboCCN.Enabled = true;
                dtmShipmentDate.FormatType = FormatTypeEnum.CustomFormat;
                dtmShipmentDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
                dtmLCDate.FormatType = FormatTypeEnum.CustomFormat;
                dtmLCDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
                dtmOnBoardDate.FormatType = FormatTypeEnum.CustomFormat;
                dtmOnBoardDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
                txtMeasurement.FormatType = FormatTypeEnum.CustomFormat;
                txtMeasurement.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
                txtGrossWeight.FormatType = FormatTypeEnum.CustomFormat;
                txtGrossWeight.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
                txtNetWeight.FormatType = FormatTypeEnum.CustomFormat;
                txtNetWeight.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
                dtbGridDesign = FormControlComponents.StoreGridLayout(dgrdData);

                _boUtils = new UtilsBO();
                // set default form purpose to print invoice
                cboPurpose.SelectedIndex = (int) ShipViewType.PrintInvoice;
                // switch form mode
                SwitchFormMode();
                // fill CCN combo box
                FormControlComponents.PutDataIntoC1ComboBox(cboCCN, _boUtils.ListCCN().Tables[0], MST_CCNTable.CODE_FLD,
                                                            MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
                cboCCN.SelectedValue = SystemProperty.CCNID;
                txtExchRate.Value = null;
                dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.AVAILABLEQTY_FLD].Visible = false;

                #region Print Configuration

                btnPrintConfiguration.Click += FormControlComponents.ShowMenuReportListHandler;
                btnPrint.Click += FormControlComponents.RunDefaultReportEntriesHandler;
                btnPrint.EnabledChanged += btnPrint_EnabledChanged;

                #endregion
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnAdd_Click()";
            try
            {
                // turn ADD mode on
                _formAction = EnumAction.Add;
                // switch form to add mode
                SwitchFormMode();
                ClearForm(this);
                cboCCN.SelectedValue = SystemProperty.CCNID;
                dtmShipmentDate.Value = Utilities.Instance.GetServerDate();
                var tableName = (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                                    ? SO_ConfirmShipMasterTable.TABLE_NAME
                                    : SO_InvoiceMasterTable.TABLE_NAME;
                txtConfirmShipNo.Text = FormControlComponents.GetNoByMask("", tableName, "ConfirmShipNo", "", "yy.MM.###");
                txtConfirmShipNo.Tag = null;
                //Fill Default Master Location 
                FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
                _voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
                FillDefaultLocation();
                // set focus to entry date field
                txtConfirmShipNo.Focus();
                txtConfirmShipNo.Select();
                var workingPeriod = Utilities.Instance.GetWorkingPeriod();
                dtmFromDate.Value = workingPeriod.FromDate;
                dtmToDate.Value = Utilities.Instance.GetServerDate();

                if (dstData != null && dstData.Tables.Count > 0)
                {
                    dstData.Tables[0].Clear();
                    dgrdData.DataSource = dstData.Tables[0];
                }
                btnModify.Enabled = false;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSO_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSO_Click()";
            try
            {
                var htData = new Hashtable();
                if (!txtMasLoc.Text.Trim().Equals(string.Empty))
                    htData.Add(SO_SaleOrderMasterTable.SHIPFROMLOCID_FLD, _voMasLoc.MasterLocationID);
                else
                {
                    if (txtMasLoc.Text.Trim() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Warning);
                        txtMasLoc.Focus();
                        return;
                    }
                }
                if (txtSaleType.Text == string.Empty)
                {
                    var strParam = new string[2];
                    strParam[0] = lblSaleType.Text;
                    strParam[1] = lblSO.Text;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
                    txtSaleType.Focus();
                    return;
                }
                htData.Add(SO_SaleOrderMasterTable.TYPEID_FLD, txtSaleType.Tag);
                // open search form allows user to select commitment
                DataRowView drowData = FormControlComponents.OpenSearchForm(ViewForInvoice, SO_SaleOrderMasterTable.CODE_FLD, txtSalesOrder.Text.Trim(), htData, true);
                // fill data to form
                if (drowData != null)
                {
                    if (_soMasterId != int.Parse(drowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString()))
                    {
                        //Clear grid
                        dstData = _boCsManagement.GetCommitedDelSchLines(0, string.Empty, DateTime.MinValue, DateTime.MinValue, 0, 0, cboPurpose.SelectedIndex);
                        dgrdData.DataSource = dstData.Tables[0];
                        // restore grid layout
                        FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                    }
                    _soMasterId = int.Parse(drowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
                    FillSaleOrderData(drowData.Row);
                    dgrdData.Focus();
                }
                else
                {
                    txtSalesOrder.Focus();
                    txtSalesOrder.SelectAll();
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearchMasLoc_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSearchMasLoc_Click()";
            try
            {
                DataRowView drwResult = null;
                var htbCondition = new Hashtable();

                if (cboCCN.SelectedValue != null)
                {
                    htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                 MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text,
                                                                 htbCondition, true);
                if (drwResult != null)
                {
                    txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
                    _voMasLoc.MasterLocationID =
                        int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                    txtSalesOrder.Text = string.Empty;
                    if (dstData != null)
                    {
                        dstData.Tables[0].Clear();
                        txtCustomerCode.Text = string.Empty;
                        txtCustomerName.Text = string.Empty;
                    }
                    txtSalesOrder.Focus();
                }
                else
                {
                    txtMasLoc.Focus();
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
                }
            }
        }

        private void btnShipNo_Click(object sender, EventArgs e)
        {
            const string VIEW_NAME = "v_SOConfirmShipMaster";
            const string SO_INVOICE_VIEW = "v_SOInvoiceMaster";
            // find confirm ship
            const string methodName = This + ".btnOrderNo_Click()";
            try
            {
                DataRowView drwResult = null;
                if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                    drwResult = FormControlComponents.OpenSearchForm(VIEW_NAME,
                                                                     SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD,
                                                                     txtConfirmShipNo.Text.Trim(), null, true);
                else
                    drwResult = FormControlComponents.OpenSearchForm(SO_INVOICE_VIEW,
                                                                     SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD,
                                                                     txtConfirmShipNo.Text.Trim(), null, true);
                if (drwResult != null)
                {
                    if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                    {
                        _voMaster.ConfirmShipMasterID = Convert.ToInt32(drwResult[SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD]);
                        txtConfirmShipNo.Tag = _voMaster.ConfirmShipMasterID;
                    }
                    else
                    {
                        _voInvoiceMaster.InvoiceMasterID = Convert.ToInt32(drwResult[SO_InvoiceMasterTable.INVOICEMASTERID_FLD]);
                        txtConfirmShipNo.Tag = _voInvoiceMaster.InvoiceMasterID;
                    }
                    FillSaleOrderData(drwResult.Row);
                    //Bind data to grid
                    dgrdData.DataSource = dstData.Tables[0];

                    // restore grid layout
                    FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                    foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                        dcolCol.Locked = true;
                    //unlock Invoice quantity, price and VAT amount column
                    dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].Locked = false;
                    dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.PRICE_FLD].Locked = false;
                    dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].Locked = false;
                    dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.AVAILABLEQTY_FLD].Visible = false;
                    //Format columns
                    dgrdData.Columns[CommittedqtyCol].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.VATPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[IV_AdjustmentTable.AVAILABLEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.PRICE_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
                    dgrdData.Columns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                    dgrdData.Columns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
                    btnModify.Enabled = true;
                }
                else
                    txtConfirmShipNo.Focus();
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtConfirmShipNo_Leave(object sender, EventArgs e)
        {
            const string methodName = This + ".txtConfirmShipNo_Leave()";
            try
            {
                if (txtConfirmShipNo.Text.Trim().Equals(string.Empty))
                {
                    if (_formAction != EnumAction.Add && _formAction != EnumAction.Edit)
                    {
                        //Clear form
                        FormControlComponents.ClearForm(this);
                        cboCCN.SelectedValue = SystemProperty.CCNID;
                        if (dstData != null)
                        {
                            dstData.Tables[0].Clear();
                        }
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtMasLoc_Leave(object sender, EventArgs e)
        {
            const string methodName = This + ".txtMasLoc_Leave()";
            try
            {
                if (txtMasLoc.Text.Trim().Equals(string.Empty))
                {
                    txtCustomerCode.Text = string.Empty;
                    txtCustomerName.Text = string.Empty;
                    txtSalesOrder.Text = string.Empty;
                    if (dstData != null)
                    {
                        dstData.Tables[0].Clear();
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtSalesOrder_Leave(object sender, EventArgs e)
        {
            const string methodName = This + ".txtSalesOrder_Leave()";
            try
            {
                if (txtSalesOrder.Text.Trim().Equals(string.Empty))
                {
                    txtCustomerCode.Text = string.Empty;
                    txtCustomerName.Text = string.Empty;
                    if (dstData != null)
                    {
                        dstData.Tables[0].Clear();
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfirmShipManagement_Closing(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".ConfirmShipManagement_Closing()";
            try
            {
                if (_validatingError)
                {
                    _validatingError = false;
                    e.Cancel = true;
                    return;
                }

                if (_formAction != EnumAction.Default)
                {
                    DialogResult enumDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,
                                                                 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (enumDialog == DialogResult.Yes)
                    {
                        // store database
                        btnConfirmShippment_Click(sender, e);
                        if (_hasError)
                        {
                            e.Cancel = true;
                        }
                        //e.Cancel = false;
                    }
                    else if (enumDialog == DialogResult.No) // click No button
                    {
                        e.Cancel = false;
                    }
                    else if (enumDialog == DialogResult.Cancel) // click Cancel button
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtConfirmShipNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                if (btnShipNo.Enabled)
                    btnShipNo_Click(sender, e);
        }

        private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                if (btnSearchMasLoc.Enabled)
                    btnSearchMasLoc_Click(sender, e);
        }

        private void txtSalesOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                if (btnSO.Enabled)
                    btnSO_Click(sender, e);
        }

        private void txtConfirmShipNo_Validating(object sender, CancelEventArgs e)
        {
            const string VIEW_NAME = "v_SOConfirmShipMaster";
            const string SO_INVOICE_VIEW = "v_SOInvoiceMaster";
            const string methodName = This + ".txtConfirmShipNo_Validating()";

            if (!txtConfirmShipNo.Modified)
            {
                return;
            }
            if (txtConfirmShipNo.Text == string.Empty)
            {
                return;
            }
            try
            {
                if (_formAction == EnumAction.Add)
                {
                    return;
                }
                if (btnConfirmShippment.Enabled) return;
                DataRowView drwResult = null;
                if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                    drwResult = FormControlComponents.OpenSearchForm(VIEW_NAME,
                                                                     SO_ConfirmShipMasterTable.CONFIRMSHIPNO_FLD,
                                                                     txtConfirmShipNo.Text.Trim(), null, false);
                else
                    drwResult = FormControlComponents.OpenSearchForm(SO_INVOICE_VIEW,
                                                                     SO_InvoiceMasterTable.CONFIRMSHIPNO_FLD,
                                                                     txtConfirmShipNo.Text.Trim(), null, false);
                if (drwResult != null)
                {
                    if (_formAction == EnumAction.Default)
                    {
                        if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                        {
                            _voMaster.ConfirmShipMasterID =
                                Convert.ToInt32(drwResult[SO_ConfirmShipMasterTable.CONFIRMSHIPMASTERID_FLD]);
                            txtConfirmShipNo.Tag = _voMaster.ConfirmShipMasterID;
                        }
                        else
                        {
                            _voInvoiceMaster.InvoiceMasterID =
                                Convert.ToInt32(drwResult[SO_InvoiceMasterTable.INVOICEMASTERID_FLD]);
                            txtConfirmShipNo.Tag = _voInvoiceMaster.InvoiceMasterID;
                        }
                        FillSaleOrderData(drwResult.Row);
                        dgrdData.DataSource = dstData.Tables[0];

                        // restore grid layout
                        FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                        foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                            dcolCol.Locked = true;
                        //unlock Invoice quantity, price and VAT amount column
                        dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].Locked = false;
                        dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.PRICE_FLD].Locked = false;
                        dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].Locked = false;
                        //Format columns
                        dgrdData.Columns[CommittedqtyCol].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                        dgrdData.Columns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                        dgrdData.Columns[SO_ConfirmShipDetailTable.VATPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                        dgrdData.Columns[IV_AdjustmentTable.AVAILABLEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                        dgrdData.Columns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
                        dgrdData.Columns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].NumberFormat = "##############,0.0000";
                        dgrdData.Columns[SO_ConfirmShipDetailTable.PRICE_FLD].NumberFormat = "##############,0.0000";
                        btnModify.Enabled = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                    txtConfirmShipNo.Focus();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtMasLoc_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtMasLoc_Validating()";

            if (!txtMasLoc.Modified)
            {
                return;
            }
            if (txtMasLoc.Text == string.Empty)
            {
                return;
            }
            try
            {
                DataRowView drwResult = null;
                var htbCondition = new Hashtable();

                if (cboCCN.SelectedValue != null)
                {
                    htbCondition.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                 MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text,
                                                                 htbCondition, false);
                if (drwResult != null)
                {
                    txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();
                    _voMasLoc.MasterLocationID =
                        int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                    txtSalesOrder.Text = string.Empty;
                    if (dstData != null)
                    {
                        dstData.Tables[0].Clear();
                        txtCustomerCode.Text = string.Empty;
                        txtCustomerName.Text = string.Empty;
                    }
                    _validatingError = false;
                    txtSalesOrder.Focus();
                }
                else
                {
                    e.Cancel = true;
                    _validatingError = true;
                    txtMasLoc.Focus();
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION);
                }
            }
        }

        private void txtSalesOrder_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtSalesOrder_Validating()";

            if (!txtSalesOrder.Modified)
            {
                return;
            }
            if (txtSalesOrder.Text == string.Empty)
            {
                return;
            }
            try
            {
                DataRowView drowData = null;
                var htData = new Hashtable();
                htData.Add(SO_SaleOrderMasterTable.SHIPCOMPLETED_FLD, 0);
                if (txtSaleType.Text == string.Empty)
                {
                    var strParam = new string[2];
                    strParam[0] = lblSaleType.Text;
                    strParam[1] = lblSO.Text;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, strParam);
                    txtSaleType.Focus();
                    return;
                }
                else
                {
                    htData.Add(SO_SaleOrderMasterTable.TYPEID_FLD, txtSaleType.Tag);
                }
                // open search form allows user to select commitment
                //drowData = FormControlComponents.OpenSearchForm(VIEW_NAME, SO_SaleOrderMasterTable.CODE_FLD, txtSalesOrder.Text.Trim(), htData, false);
                drowData = FormControlComponents.OpenSearchForm(ViewForInvoice, SO_SaleOrderMasterTable.CODE_FLD,
                                                                txtSalesOrder.Text.Trim(), htData, false);
                // fill data to form
                if (drowData != null)
                {
                    _validatingError = false;
                    if (_soMasterId != int.Parse(drowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString()))
                    {
                        //Clear grid
                        dstData = _boCsManagement.GetCommitedDelSchLines(0, string.Empty, DateTime.MinValue, DateTime.MinValue, 0, 0, cboPurpose.SelectedIndex);
                        dgrdData.DataSource = dstData.Tables[0];
                        // restore grid layout
                        FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                    }
                    _soMasterId = int.Parse(drowData[SO_SaleOrderMasterTable.SALEORDERMASTERID_FLD].ToString());
                    FillSaleOrderData(drowData.Row);
                    dgrdData.Focus();
                }
                else
                {
                    e.Cancel = true;
                    _validatingError = true;
                    txtSalesOrder.Focus();
                    txtSalesOrder.SelectAll();
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Print Processing (HACKED by TUAN TQ, 28 Oct, 2005)

        private const string APPLICATION_PATH = @"PCSMain\bin\Debug";
        private const string SO_INVOICE_STANDARD_REPORT = "Invoice4SaleOrder.xml";
        private const string SO_INVOICE_APPENDIX_REPORT = "Invoice4SaleOrder_Appendix.xml";
        private const string REPORT_NAME = "Sale Order Invoice";
        private const string REPORTFLD_COMPANY = "fldCompany";
        private const string REPORTFLD_ADDRESS = "fldAddress";
        private const string REPORTFLD_TEL = "fldTel";
        private const string REPORTFLD_FAX = "fldFax";
        private const string REPORTFLD_AMOUNT_IN_WORD = "fldAmountInWord";
        private const string REPORTFLD_AMOUNT_IN_WORD1 = "fldAmountInWord1";
        private const string REPORTFLD_TOTAL_AMOUNT = "fldSumTotalNetAmount";

        private readonly C1PrintPreviewDialog printPreview = new C1PrintPreviewDialog();
        //variable indicates that appendix report is used
        private bool blnIsAppendix;
        private bool blnIsBuilt;
        private DataTable dtbResult;

        private int CountDistinctProduct(DataTable pdtbTable)
        {
            var arlItem = new ArrayList();
            foreach (DataRow drow in pdtbTable.Rows)
                if (!arlItem.Contains(drow[ITM_ProductTable.PRODUCTID_FLD]))
                    arlItem.Add(drow[ITM_ProductTable.PRODUCTID_FLD]);
            return arlItem.Count;
        }

        private void BuildAndPrintReportWithoutLayout()
        {
            const string methodName = This + ".BuildAndPrintReportWithoutLayout()";

            try
            {
                //return if no ConfirmShip was selected
                if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping && _voMaster.ConfirmShipMasterID <= 0)
                    return;
                if (cboPurpose.SelectedIndex == (int) ShipViewType.PrintInvoice && _voInvoiceMaster.InvoiceMasterID <= 0)
                    return;

                // return if no datasource
                if (dtbResult == null)
                {
                    return;
                }

                //Print report without rebuilt
                if (blnIsBuilt)
                {
                    printPreview.ReportViewer.PreviewPane.Print();
                    return;
                }

                //Built report and print
                Cursor = Cursors.WaitCursor;

                var reportBuilderWithoutLayout = new ReportBuilder();

                //Get actual application path
                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
                if (intIndex > -1)
                {
                    strReportPath = strReportPath.Substring(0, intIndex);
                }

                if (strReportPath.Substring(strReportPath.Length - 1) == @"\")
                {
                    strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
                }
                else
                {
                    strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
                }

                //Set datasource and lay-out path for reports
                reportBuilderWithoutLayout.SourceDataTable = dtbResult;
                reportBuilderWithoutLayout.ReportDefinitionFolder = strReportPath;

                //Check rows to select valid report lay-out
                reportBuilderWithoutLayout.ReportLayoutFile = blnIsAppendix ? SO_INVOICE_APPENDIX_REPORT : SO_INVOICE_STANDARD_REPORT;

                //check if layout is valid
                if (reportBuilderWithoutLayout.AnalyseLayoutFile())
                {
                    reportBuilderWithoutLayout.UseLayoutFile = true;
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilderWithoutLayout.MakeDataTableForRender();

                // and show it in preview dialog
                printPreview.FormTitle = REPORT_NAME;
                reportBuilderWithoutLayout.ReportViewer = printPreview.ReportViewer;
                reportBuilderWithoutLayout.RenderReport();

                //Header information get from system params				
                reportBuilderWithoutLayout.DrawPredefinedField(REPORTFLD_COMPANY,
                                                               SystemProperty.SytemParams.Get(
                                                                   SystemParam.COMPANY_FULL_NAME));
                reportBuilderWithoutLayout.DrawPredefinedField(REPORTFLD_ADDRESS,
                                                               SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
                reportBuilderWithoutLayout.DrawPredefinedField(REPORTFLD_TEL,
                                                               SystemProperty.SytemParams.Get(SystemParam.TEL));
                reportBuilderWithoutLayout.DrawPredefinedField(REPORTFLD_FAX,
                                                               SystemProperty.SytemParams.Get(SystemParam.FAX));

                string strTotalAmount =
                    reportBuilderWithoutLayout.Report.Fields[REPORTFLD_TOTAL_AMOUNT].Value.ToString();
                if (strTotalAmount != string.Empty)
                {
                    decimal decValue = decimal.Round(decimal.Parse(strTotalAmount), 0);

                    string strTotalAmountInWord = ConvertNumberToWord.ChuyenSoThanhChu(decValue);
                    reportBuilderWithoutLayout.DrawPredefinedField(REPORTFLD_AMOUNT_IN_WORD, strTotalAmountInWord);
                    reportBuilderWithoutLayout.DrawPredefinedField(REPORTFLD_AMOUNT_IN_WORD1, strTotalAmountInWord);
                }

                //Hide layout of non-layout report
                for (int i = 0; i < reportBuilderWithoutLayout.Report.Fields.Count; i++)
                {
                    reportBuilderWithoutLayout.Report.Fields[i].Visible = (reportBuilderWithoutLayout.Report.Fields[i].Tag != null);
                }

                reportBuilderWithoutLayout.RefreshReport();
                //Turn on built status
                blnIsBuilt = true;

                //Print report
                printPreview.ReportViewer.PreviewPane.Print();
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ShowDomesticInvoiceReport()
        {
            const string methodName = This + ".ShowDomesticInvoiceReport()";

            try
            {
                //return if no ConfirmShip was selected
                if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping && _voMaster.ConfirmShipMasterID <= 0)
                    return;
                if (cboPurpose.SelectedIndex == (int) ShipViewType.PrintInvoice && _voInvoiceMaster.InvoiceMasterID <= 0)
                    return;

                //Turn of built status then change cursor
                blnIsBuilt = false;
                Cursor = Cursors.WaitCursor;

                var rptReport = new C1Report();

                #region Get actual application path

                string reportFolder = Application.StartupPath;

                int intIndex = reportFolder.IndexOf(APPLICATION_PATH);
                if (intIndex > -1)
                {
                    reportFolder = reportFolder.Substring(0, intIndex);
                }

                if (reportFolder.Substring(reportFolder.Length - 1) == @"\")
                {
                    reportFolder += Constants.REPORT_DEFINITION_STORE_LOCATION;
                }
                else
                {
                    reportFolder += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
                }

                #endregion

                var boDataReport = new C1PrintPreviewDialogBO();
                dtbResult = cboPurpose.SelectedIndex == (int) ShipViewType.Shipping
                                ? boDataReport.GetSaleOrderCommitData(_voMaster.ConfirmShipMasterID)
                                : boDataReport.GetSaleOrderInvoiceData(_voInvoiceMaster.InvoiceMasterID);
                // we can't preview while we don't have any data
                if (dtbResult == null)
                {
                    return;
                }

                #region Check VAT: VAT must be unique 

                var arlVat = new ArrayList();
                decimal totalAmount = 0;
                foreach (DataRow row in dtbResult.Rows)
                {
                    if (!arlVat.Contains(row[SO_SaleOrderDetailTable.VATPERCENT_FLD]))
                    {
                        arlVat.Add(row[SO_SaleOrderDetailTable.VATPERCENT_FLD]);
                    }
                    if (arlVat.Count > 1)
                    {
                        var arrMessage = new[] { lblVATInInvoice.Text };
                        PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_BE_UNIQUE, MessageBoxIcon.Exclamation, arrMessage);
                        return;
                    }
                    decimal netAmount = Convert.ToDecimal(row["NetAmount"]);
                    decimal vatPercent = Convert.ToDecimal(row[SO_SaleOrderDetailTable.VATPERCENT_FLD]);
                    totalAmount += netAmount + netAmount*vatPercent/100;
                }

                #endregion

                //Check rows to select valid report lay-out
                string reportFilePath;
                if (CountDistinctProduct(dtbResult) >= Utils.Instance.MaxRowSaleOrderInvoiceReport)
                {
                    blnIsAppendix = true;
                    reportFilePath = Path.Combine(reportFolder, SO_INVOICE_APPENDIX_REPORT);
                }
                else
                {
                    blnIsAppendix = false;
                    reportFilePath = Path.Combine(reportFolder, SO_INVOICE_STANDARD_REPORT);
                }

                //check if layout is valid
                try
                {
                    rptReport.Load(reportFilePath, rptReport.GetReportInfo(reportFilePath)[0]);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                #region report parameter

                try
                {
                    string totalAmountInWord = ConvertNumberToWord.ChuyenSoThanhChu(decimal.Round(totalAmount, 0));
                    rptReport.Fields[REPORTFLD_AMOUNT_IN_WORD].Text = totalAmountInWord;
                    rptReport.Fields[REPORTFLD_AMOUNT_IN_WORD1].Text = totalAmountInWord;
                }
                catch
                {
                }
                try
                {
                    var companyFullname = SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
                    rptReport.Fields[REPORTFLD_COMPANY].Text = companyFullname;
                }
                catch
                {
                }
                try
                {
                    var address = SystemProperty.SytemParams.Get(SystemParam.ADDRESS);
                    rptReport.Fields[REPORTFLD_ADDRESS].Text = address;
                }
                catch
                {
                }
                try
                {
                    var telephone = SystemProperty.SytemParams.Get(SystemParam.TEL);
                    rptReport.Fields[REPORTFLD_TEL].Text = telephone;
                }
                catch
                {
                }
                try
                {
                    var fax = SystemProperty.SytemParams.Get(SystemParam.FAX);
                    rptReport.Fields[REPORTFLD_FAX].Text = fax;
                }
                catch
                {
                }

                #endregion

                // set datasource object that provides data to report.
                rptReport.DataSource.Recordset = dtbResult;
                // render report
                rptReport.Render();

                // the report have more than one page
                if (blnIsAppendix)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_ATTACHMENT_SOINVOICE_REPORT, MessageBoxIcon.Information);
                }
                // render the report into the PrintPreviewControl
                var ppvViewer = new C1PrintPreviewDialog {FormTitle = REPORT_NAME, Report = rptReport, HandlePrintEvent = true};
                ppvViewer.ReportViewer.PreviewNavigationPanel.Visible = false;
                ppvViewer.ReportViewer.Document = rptReport.C1Document;

                ppvViewer.Show();
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void ReportViewer_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".ReportViewer_KeyDown";
            try
            {
                
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void Print_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = This + ".Print_Click";

            try
            {
                //Get sender object
                var printButton = sender as ToolStripButton;
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion Print Processing		

        #region Print Processing - Export Invoice (HACKED by TUAN TQ, 02 May, 2006) 

        private const int EXPORT_INVOICE_MAX_ROW = 10;

        private void ShowPackingListReport(object sender, EventArgs e)
        {
            //return if no record was selected
            if (_voMaster.ConfirmShipMasterID <= 0)
            {
                return;
            }

            if (txtSaleType.Text.Trim() == string.Empty || txtSaleType.Tag == null)
            {
                return;
            }

            if (int.Parse(txtSaleType.Tag.ToString()) == (int) SOType.Export)
            {
                ShowExportPackingListReport();
            }
            else
            {
                ShowDomesticPackingListReport();
            }
        }

        private void ShowSaleContractReport(object sender, EventArgs e)
        {
            //return if no record was selected
            if (_voMaster.ConfirmShipMasterID <= 0)
            {
                return;
            }

            if (txtSaleType.Text.Trim() == string.Empty || txtSaleType.Tag == null)
            {
                return;
            }

            if (int.Parse(txtSaleType.Tag.ToString()) == (int) SOType.Export)
            {
                ShowExportSaleContractReport();
            }
            else
            {
                ShowDomesticSaleContractReport();
            }
        }

        protected void ShowInvoiceReport(object sender, EventArgs e)
        {
            //return if no ConfirmShip was selected
            if (txtConfirmShipNo.Tag == null)
                return;

            if (txtSaleType.Text.Trim() == string.Empty || txtSaleType.Tag == null)
                return;

            if (int.Parse(txtSaleType.Tag.ToString()) == (int) SOType.Export)
            {
                ShowExportInvoiceReport();
            }
            else
            {
                ShowDomesticInvoiceReport();
            }
        }

        private void ShowDomesticPackingListReport()
        {
        }

        private void ShowExportPackingListReport()
        {
            const string methodName = This + ".ShowExportPackingListReport()";
            try
            {
                const string TEMPLATE_FILE_NORMAL = "SOInvoice_PackingList.xml";
                const string TEMPLATE_FILE_ATTACHEMENT = "SOInvoiceAttachement_PackingList.xml";

                //Change cursor to wait status
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                DataTable dtbResult;

                dtbResult = boDataReport.GetSOShippingDetailData4ImportInvoice(_voMaster.ConfirmShipMasterID);

                var reportBuilder = new ReportBuilder();

                //Get actual application path

                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
                if (intIndex > -1)
                {
                    strReportPath = strReportPath.Substring(0, intIndex);
                }

                if (strReportPath.Substring(strReportPath.Length - 1) == @"\")
                {
                    strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
                }
                else
                {
                    strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
                }

                //Set datasource and lay-out path for reports
                reportBuilder.SourceDataTable = dtbResult;

                reportBuilder.ReportDefinitionFolder = strReportPath;
                if (CountDistinctProduct(dtbResult) > EXPORT_INVOICE_MAX_ROW)
                {
                    reportBuilder.ReportLayoutFile = TEMPLATE_FILE_ATTACHEMENT;
                }
                else
                {
                    reportBuilder.ReportLayoutFile = TEMPLATE_FILE_NORMAL;
                }

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())

                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    //Reset cursor
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog 
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params 
                reportBuilder.DrawPredefinedField(REPORTFLD_COMPANY,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
                reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
                reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
                reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));

                reportBuilder.RefreshReport();
                printPreview.Show();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);

                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                //reset cursor
                Cursor = Cursors.Default;
            }
        }


        private void ShowDomesticSaleContractReport()
        {
        }

        private void ShowExportSaleContractReport()
        {
            const string methodName = This + ".ShowSaleContractReport()";

            try
            {
                const string TEMPLATE_FILE_NORMAL = "SOInvoice_SaleContract.xml";
                const string TEMPLATE_FILE_ATTACHEMENT = "SOInvoiceAttachement_SaleContract.xml";

                //Change cursor to wait status
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                DataTable dtbResult;
                dtbResult = boDataReport.GetSOShippingDetailData4ImportInvoice(_voMaster.ConfirmShipMasterID);

                var reportBuilder = new ReportBuilder();

                //Get actual application path

                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(APPLICATION_PATH);

                if (intIndex > -1)
                {
                    strReportPath = strReportPath.Substring(0, intIndex);
                }

                if (strReportPath.Substring(strReportPath.Length - 1) == @"\")
                {
                    strReportPath += Constants.REPORT_DEFINITION_STORE_LOCATION;
                }
                else
                {
                    strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
                }

                //Set datasource and lay-out path for reports

                reportBuilder.SourceDataTable = dtbResult;
                reportBuilder.ReportDefinitionFolder = strReportPath;

                if (CountDistinctProduct(dtbResult) > EXPORT_INVOICE_MAX_ROW)
                {
                    reportBuilder.ReportLayoutFile = TEMPLATE_FILE_ATTACHEMENT;
                }
                else
                {
                    reportBuilder.ReportLayoutFile = TEMPLATE_FILE_NORMAL;
                }

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    //Reset cursor
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }
                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog 
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params 
                reportBuilder.DrawPredefinedField(REPORTFLD_COMPANY,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
                reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
                reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
                reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));

                //Bank information
                //reportBuilder.DrawPredefinedField(REPORTFLD_BANKACCOUNT, SystemProperty.SytemParams.Get(SystemParam.ACCOUNT));					
                //reportBuilder.DrawPredefinedField(REPORTFLD_BANKNAME, SystemProperty.SytemParams.Get(SystemParam.BANK_NAME));
                //reportBuilder.DrawPredefinedField(RPT_BANK_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.BANK_ADDR));

                reportBuilder.RefreshReport();
                printPreview.Show();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);

                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch

                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                //reset cursor
                Cursor = Cursors.Default;
            }
        }

        private void ShowExportInvoiceReport()
        {
            const string methodName = This + ".ShowInvoiceReport()";
            try
            {
                const string TEMPLATE_FILE_NORMAL = "SOInvoice_Invoice.xml";
                const string TEMPLATE_FILE_ATTACHEMENT = "SOInvoiceAttachement_Invoice.xml";

                //Change cursor to wait status
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                DataTable dtbResult;
                dtbResult = cboPurpose.SelectedIndex == (int) ShipViewType.Shipping
                                ? boDataReport.GetSOShippingDetailData4ImportInvoice(_voMaster.ConfirmShipMasterID)
                                : boDataReport.GetSOInvoiceDetailData4ImportInvoice(_voInvoiceMaster.InvoiceMasterID);
                var reportBuilder = new ReportBuilder();

                //Get actual application path
                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(APPLICATION_PATH);
                if (intIndex > -1)
                {
                    strReportPath = strReportPath.Substring(0, intIndex);
                }

                strReportPath += strReportPath.Substring(strReportPath.Length - 1) == @"\"
                                     ? Constants.REPORT_DEFINITION_STORE_LOCATION
                                     : "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
                //Set datasource and lay-out path for reports
                reportBuilder.SourceDataTable = dtbResult;
                reportBuilder.ReportDefinitionFolder = strReportPath;

                reportBuilder.ReportLayoutFile = CountDistinctProduct(dtbResult) > EXPORT_INVOICE_MAX_ROW
                                                     ? TEMPLATE_FILE_ATTACHEMENT
                                                     : TEMPLATE_FILE_NORMAL;

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    //Reset cursor
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params
                reportBuilder.DrawPredefinedField(REPORTFLD_COMPANY,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
                reportBuilder.DrawPredefinedField(REPORTFLD_ADDRESS, SystemProperty.SytemParams.Get(SystemParam.ADDRESS));
                reportBuilder.DrawPredefinedField(REPORTFLD_TEL, SystemProperty.SytemParams.Get(SystemParam.TEL));
                reportBuilder.DrawPredefinedField(REPORTFLD_FAX, SystemProperty.SytemParams.Get(SystemParam.FAX));

                reportBuilder.RefreshReport();
                printPreview.Show();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);

                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.

                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                //reset cursor
                Cursor = Cursors.Default;
            }
        }

        #endregion Print Processing 

        public ConfirmShipManagement()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void dgrdData_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".dgrdData_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.Delete && dgrdData.SelectedRows.Count > 0)
                {
                    if (_formAction == EnumAction.Edit)
                    {
                        foreach (int rowIndex in dgrdData.SelectedRows)
                        {
                            var fieldName = cboPurpose.SelectedIndex != (int)ShipViewType.Shipping
                                                ? SO_InvoiceDetailTable.INVOICEDETAILID_FLD
                                                : SO_ConfirmShipDetailTable.CONFIRMSHIPDETAILID_FLD;
                            int detailId = Convert.ToInt32(dgrdData[rowIndex, fieldName]);
                            if (!_removedId.Contains(detailId))
                            {
                                _removedId.Add(detailId);
                            }
                        }
                    }

                    dgrdData.DeleteMultiRows();
                }
            }
            catch (Exception ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// FillExchangeRate
        /// </summary>
        /// <param name="pintCurrencyID"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Wednesday, April 12 2006</date>
        private int FillExchangeRate(int pintCurrencyID)
        {
            // Fill Exch. Rate if the system configured the exchange rate get form Exchange Rate Table
            // based on currency and transaction date (begin date<= transaction date <= end date and approved)
            const decimal DEFAULT_RATE = 1;
            const string methodName = This + ".FillExchangeRate()";
            int intExchangeRateID = 0;
            if (pintCurrencyID == 0) return intExchangeRateID;
            //•	If the currency is same as base(Home - CuongNT fixed) currency then the system automatically fill the number 1 to exchange rate field
            if (pintCurrencyID == SystemProperty.HomeCurrencyID)
            {
                txtExchRate.Value = DEFAULT_RATE;
                return intExchangeRateID;
            }
            try
            {
                if (dtmShipmentDate.Value == DBNull.Value)
                {
                    // Input Transaction date before execute this function
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INPUT_TRANSACTION_BEFORE, MessageBoxIcon.Exclamation);
                    txtExchRate.Value = DEFAULT_RATE;
                    dtmShipmentDate.Focus();
                    return intExchangeRateID;
                }
                var dtmOrderDate = (DateTime) dtmShipmentDate.Value;
                var boOrder = new SaleOrderBO();
                var voExchange = (MST_ExchangeRateVO) boOrder.GetExchangeRate(pintCurrencyID, dtmOrderDate);
                if (voExchange.ExchangeRateID == 0)
                {
                    // Do not found any Exchange Rate records which (begin effective date <= the current date <= end of effective date)
                    PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_FOUND_EXCHANGE_RATE, MessageBoxIcon.Exclamation);
                    return intExchangeRateID;
                }
                // fill value and return
                intExchangeRateID = voExchange.ExchangeRateID;
                txtExchRate.Value = voExchange.Rate;
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            return intExchangeRateID;
        }

        /// <summary>
        /// btnCurrency_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, April 12 2006</date>
        private void btnCurrency_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnCurrency_Click()";
            try
            {
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME,
                                                                             MST_CurrencyTable.CODE_FLD,
                                                                             txtCurrency.Text.Trim(), null, true);
                if (drwResult != null)
                {
                    txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];
                    txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
                    txtExchRate.Tag = FillExchangeRate(int.Parse(drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
                    if (drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString() !=
                        SystemProperty.HomeCurrencyID.ToString())
                    {
                        txtExchRate.Enabled = true;
                    }
                    else
                        txtExchRate.Enabled = false;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtCurrency_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, April 12 2006</date>
        private void txtCurrency_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtCurrency_Validating()";
            try
            {
                if (!txtCurrency.Modified) return;
                if (txtCurrency.Text.Trim() == string.Empty)
                {
                    txtCurrency.Tag = null;
                    return;
                }
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_CurrencyTable.TABLE_NAME,
                                                                             MST_CurrencyTable.CODE_FLD,
                                                                             txtCurrency.Text.Trim(), null, false);
                if (drwResult != null)
                {
                    txtCurrency.Tag = drwResult[MST_CurrencyTable.CURRENCYID_FLD];
                    txtCurrency.Text = drwResult[MST_CurrencyTable.CODE_FLD].ToString();
                    txtExchRate.Tag = FillExchangeRate(int.Parse(drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString()));
                    if (drwResult[MST_CurrencyTable.CURRENCYID_FLD].ToString() !=
                        SystemProperty.HomeCurrencyID.ToString())
                    {
                        txtExchRate.Enabled = true;
                    }
                    else
                        txtExchRate.Enabled = false;
                }
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtCurrency_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, April 12 2006</date>
        private void txtCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btnCurrency_Click(sender, e);
            }
        }

        /// <summary>
        /// btnGate_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void btnGate_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnGate_Click()";
            try
            {
                DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(SO_GateTable.TABLE_NAME,
                                                                                              SO_GateTable.CODE_FLD,
                                                                                              txtGate.Text.Trim(),
                                                                                              string.Empty, true);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    string strGateNameArray = string.Empty;
                    string strGateIDArray = string.Empty;
                    foreach (DataRow drow in dtbResult.Rows)
                    {
                        strGateNameArray += drow[SO_GateTable.CODE_FLD] + ", ";
                        strGateIDArray += drow[SO_GateTable.GATEID_FLD] + ", ";
                    }
                    //Remove colon in the last position
                    strGateNameArray = strGateNameArray.Substring(0, strGateNameArray.Length - 2);
                    strGateIDArray = strGateIDArray.Substring(0, strGateIDArray.Length - 2);
                    txtGate.Text = strGateNameArray;
                    _gateIds = strGateIDArray;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnSaleType_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void btnSaleType_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSaleType_Click()";
            try
            {
                DataRowView drwResult = FormControlComponents.OpenSearchForm(SO_TypeTable.TABLE_NAME,
                                                                             SO_TypeTable.DESCRIPTION_FLD,
                                                                             txtSaleType.Text.Trim(), null, true);
                if (drwResult != null)
                {
                    _saleType = Convert.ToInt32(drwResult[SO_TypeTable.TYPEID_FLD]) == (int) SOType.Export ? 2 : 1;
                    if (txtSaleType.Tag != null &&
                        txtSaleType.Tag.ToString() != drwResult[SO_TypeTable.TYPEID_FLD].ToString())
                    {
                        //Clear relate information 
                        txtSalesOrder.Text = string.Empty;
                        txtSalesOrder.Tag = null;
                        dstData = _boCsManagement.GetCommitedDelSchLines(0, string.Empty, DateTime.MinValue, DateTime.MinValue, 0, 0, cboPurpose.SelectedIndex);
                        dgrdData.DataSource = dstData.Tables[0];
                        // restore grid layout
                        FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                        FillDefaultLocation();
                    }
                    txtSaleType.Tag = drwResult[SO_TypeTable.TYPEID_FLD];
                    txtSaleType.Text = drwResult[SO_TypeTable.DESCRIPTION_FLD].ToString();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtSaleType_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void txtSaleType_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtSaleType_Validating()";
            try
            {
                if (!txtSaleType.Modified) return;
                if (txtSaleType.Text == string.Empty)
                {
                    txtSaleType.Tag = null;
                    return;
                }
                DataRowView drwResult = FormControlComponents.OpenSearchForm(SO_TypeTable.TABLE_NAME,
                                                                             SO_TypeTable.DESCRIPTION_FLD,
                                                                             txtSaleType.Text.Trim(), null, false);
                if (drwResult != null)
                {
                    _saleType = Convert.ToInt32(drwResult[SO_TypeTable.TYPEID_FLD]) == (int) SOType.Export ? 2 : 1;
                    if (txtSaleType.Tag != null &&
                        txtSaleType.Tag.ToString() != drwResult[SO_TypeTable.TYPEID_FLD].ToString())
                    {
                        //Clear relate information 
                        txtSalesOrder.Text = string.Empty;
                        txtSalesOrder.Tag = null;
                        dstData = _boCsManagement.GetCommitedDelSchLines(0, string.Empty, DateTime.MinValue, DateTime.MinValue, 0, 0, cboPurpose.SelectedIndex);
                        dgrdData.DataSource = dstData.Tables[0];
                        // restore grid layout
                        FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                        FillDefaultLocation();
                    }
                    txtSaleType.Tag = drwResult[SO_TypeTable.TYPEID_FLD];
                    txtSaleType.Text = drwResult[SO_TypeTable.DESCRIPTION_FLD].ToString();
                }
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtSaleType_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void txtSaleType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && btnSaleType.Enabled)
            {
                btnSaleType_Click(null, null);
            }
        }

        /// <summary>
        /// chkHaveGate_CheckedChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void chkHaveGate_CheckedChanged(object sender, EventArgs e)
        {
            const string methodName = This + ".chkHaveGate_CheckedChanged()";
            try
            {
                txtGate.Text = string.Empty;
                _gateIds = string.Empty;
                txtGate.Enabled = chkHaveGate.Checked;
                btnGate.Enabled = chkHaveGate.Checked;
                if (chkHaveGate.Checked)
                {
                    lblGate.ForeColor = Color.Maroon;
                }
                else
                {
                    lblGate.ForeColor = Color.Black;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtGate_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void txtGate_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtGate_Validating()";
            try
            {
                if (!txtGate.Modified) return;
                if (txtGate.Text == string.Empty)
                {
                    _gateIds = string.Empty;
                    return;
                }
                DataTable dtbResult = FormControlComponents.OpenSearchFormForMultiSelectedRow(SO_GateTable.TABLE_NAME,
                                                                                              SO_GateTable.CODE_FLD,
                                                                                              txtGate.Text.Trim(),
                                                                                              string.Empty, false);
                if (dtbResult != null && dtbResult.Rows.Count > 0)
                {
                    string strGateNameArray = string.Empty;
                    string strGateIDArray = string.Empty;
                    foreach (DataRow drow in dtbResult.Rows)
                    {
                        strGateNameArray += drow[SO_GateTable.CODE_FLD] + ", ";
                        strGateIDArray += drow[SO_GateTable.GATEID_FLD] + ", ";
                    }
                    //Remove colon in the last position
                    strGateNameArray = strGateNameArray.Substring(0, strGateNameArray.Length - 2);
                    strGateIDArray = strGateIDArray.Substring(0, strGateIDArray.Length - 2);
                    txtGate.Text = strGateNameArray;
                    _gateIds = strGateIDArray;
                }
                else
                    e.Cancel = true;
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtGate_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void txtGate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && btnGate.Enabled)
            {
                btnGate_Click(null, null);
            }
        }

        /// <summary>
        /// btnSearch_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, May 31 2006</date>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSearch_Click()";
            try
            {
                if (txtSalesOrder.Text == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                    txtSalesOrder.Focus();
                    return;
                }
                if (chkHaveGate.Checked)
                {
                    if (txtGate.Text == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Warning);
                        txtGate.Focus();
                        return;
                    }
                }
                if (cboPurpose.SelectedIndex == (int) ShipViewType.PrintInvoice)
                {
                    if (FormControlComponents.CheckMandatory(dtmFromDate))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        dtmFromDate.Focus();
                        return;
                    }
                    if (FormControlComponents.CheckMandatory(dtmToDate))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        dtmToDate.Focus();
                        return;
                    }
                    if (FormControlComponents.CheckMandatory(txtLocation))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtLocation.Focus();
                        return;
                    }
                    if (FormControlComponents.CheckMandatory(txtBin))
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                        txtBin.Focus();
                        return;
                    }
                    if (!IsValidateToDate())
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_MP_PERIODDATE, MessageBoxIcon.Error);
                        dtmFromDate.Focus();
                        return;
                    }
                }
                var locationId = (int)txtLocation.Tag;
                var binId = (int)txtBin.Tag;
                var dtmFromDateParam = (DateTime)dtmFromDate.Value;
                var dtmToDateParam = (DateTime)dtmToDate.Value;
                dstData = _boCsManagement.GetCommitedDelSchLines(_voSOMaster.SaleOrderMasterID, _gateIds, dtmFromDateParam, dtmToDateParam, locationId, binId, cboPurpose.SelectedIndex);
                
                //Bind data to grid
                dgrdData.DataSource = dstData.Tables[0];

                // restore grid layout
                FormControlComponents.RestoreGridLayout(dgrdData, dtbGridDesign);
                foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                    dcolCol.Locked = true;
                dgrdData.Columns[CommittedqtyCol].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Columns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Columns[SO_ConfirmShipDetailTable.VATPERCENT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Columns[IV_AdjustmentTable.AVAILABLEQTY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.AVAILABLEQTY_FLD].Visible = true;
                //Allow edit Price column
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.PRICE_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.VATPERCENT_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD].Locked = false;

                dgrdData.Columns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
                dgrdData.Columns[SO_ConfirmShipDetailTable.PRICE_FLD].NumberFormat = Constants.DECIMAL_LONG_FORMAT;
                dgrdData.Columns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Columns[SO_DeliveryScheduleTable.SCHEDULEDATE_FLD].NumberFormat = Constants.DATETIME_FORMAT_HOUR;
                dgrdData.Splits[0].DisplayColumns["InvoiceQty"].Locked = false;
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnModify_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Thursday, June 1 2006</date>
        private void btnModify_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnModify_Click()";
            try
            {
                _formAction = EnumAction.Edit;
                btnConfirmShippment.Text = "&Save";
                //allow edit some controls
                txtShippingCode.Enabled = true;
                txtFromPort.Enabled = true;
                txtCNo.Enabled = true;
                txtMeasurement.Enabled = true;
                txtGrossWeight.Enabled = true;
                txtNetWeight.Enabled = true;
                txtIssuingBank.Enabled = true;
                dtmLCDate.Enabled = true;
                txtLCNo.Enabled = true;
                txtVessel.Enabled = true;
                txtComment.Enabled = true;
                txtReferenceNo.Enabled = true;
                txtInvoiceNo.Enabled = true;
                dtmOnBoardDate.Enabled = true;
                dtmInvoiceDate.Enabled = true;

                btnModify.Enabled = false;
                //allow edit price column
                dgrdData.AllowUpdate = true;
                //lock all column except Price column
                foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                {
                    dcolCol.Locked = true;
                }
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.PRICE_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.INVOICEQTY_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.VATAMOUNT_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.VATPERCENT_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[SO_ConfirmShipDetailTable.NETAMOUNT_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[IV_AdjustmentTable.AVAILABLEQTY_FLD].Visible = true;
                //enable Save button
                btnConfirmShippment.Enabled = true;
                btnAdd.Enabled = false;
                btnShipNo.Enabled = false;
                txtConfirmShipNo.Enabled = true;
                txtCurrency.Enabled = true;
                txtExchRate.Enabled = true;
                btnCurrency.Enabled = true;

                //HACK: added by Tuan TQ
                btnAttachedSheet.Enabled = false;
                btnPrint.Enabled = false;
                //End hack
                if (cboPurpose.SelectedIndex == (int) ShipViewType.PrintInvoice)
                {
                    dgrdData.AllowDelete = true;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAttachedSheet_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnAttachedSheet_Click()";
            const string ATTACHED_SHEET_REPORT = "20060605100737827";
            try
            {
                //return if no record was selected
                if (_voMaster.ConfirmShipMasterID <= 0)
                {
                    return;
                }

                if (txtSaleType.Text.Trim() == string.Empty || txtSaleType.Tag == null)
                {
                    return;
                }

                if (int.Parse(txtSaleType.Tag.ToString()) != (int) SOType.Export)
                {
                    return;
                }

                var boDataReport = new C1PrintPreviewDialogBO();
                DataTable dtbResult;
                dtbResult = boDataReport.GetSOShippingDetailData4ImportInvoice(_voMaster.ConfirmShipMasterID);

                if (dtbResult != null)
                {
                    if (CountDistinctProduct(dtbResult) > EXPORT_INVOICE_MAX_ROW)
                    {
                        var boViewReportBO = new ViewReportBO();
                        var voReportVO = (sys_ReportVO) boViewReportBO.GetReportByReportID(ATTACHED_SHEET_REPORT);

                        if (voReportVO == null)
                        {
                            return;
                        }
                        Cursor = Cursors.WaitCursor;

                        var frmViewReport = new ViewReport();
                        frmViewReport.VoReport = voReportVO;
                        frmViewReport.ViewMode = ViewReportMode.Normal;
                        frmViewReport.Show();
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// dgrdData_BeforeColUpdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Monday, June 5 2006</date>
        private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            const string methodName = This + ".dgrdData_BeforeColUpdate()";
            try
            {
                if (e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.PRICE_FLD
                    || e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.INVOICEQTY_FLD
                    || e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.NETAMOUNT_FLD
                    || e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.VATAMOUNT_FLD
                    || e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.VATPERCENT_FLD)
                {
                    if (e.Column.DataColumn.Text == string.Empty)
                    {
                        return;
                    }
                    try
                    {
                        decimal.Parse(e.Column.DataColumn.Text);
                    }
                    catch
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.PRICE_FLD)
                    {
                        if (decimal.Parse(e.Column.DataColumn.Text) < 0)
                        {
                            e.Cancel = true;
                        }
                    }

                    if (e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.NETAMOUNT_FLD)
                    {
                        if (decimal.Parse(e.Column.DataColumn.Text) < 0)
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        if (decimal.Parse(e.Column.DataColumn.Text) < 0)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                //check available quantity before updating Invoice qty column
                if (cboPurpose.SelectedIndex == (int) ShipViewType.Shipping)
                {
                    if (e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.INVOICEQTY_FLD)
                    {
                        if (dgrdData.Columns[OldInvoiceqtyCol].Value.ToString() != string.Empty)
                        {
                            if (decimal.Parse(e.Column.DataColumn.Text) >
                                decimal.Parse(dgrdData.Columns[OldInvoiceqtyCol].Value.ToString()))
                            {
                                decimal decDeltaQty = 0;
                                decDeltaQty = decimal.Parse(e.Column.DataColumn.Text) -
                                              decimal.Parse(dgrdData.Columns[OldInvoiceqtyCol].Value.ToString());
                                if (dgrdData.Columns[IV_AdjustmentTable.AVAILABLEQTY_FLD].Value != DBNull.Value)
                                {
                                    if (decDeltaQty >
                                        decimal.Parse(
                                            dgrdData.Columns[IV_AdjustmentTable.AVAILABLEQTY_FLD].Value.ToString()))
                                    {
                                        PCSMessageBox.Show(
                                            ErrorCode.MESSAGE_IV_ADJUSTMENT_ADJUSTQTY_MUST_BE_SMALLER_THAN_AVAILABLEQTY,
                                            MessageBoxIcon.Warning);
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// dgrdData_AfterColUpdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Monday, June 5 2006</date>
        private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
        {
            const string methodName = This + ".dgrdData_AfterColUpdate()";
            try
            {
                if (e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.PRICE_FLD
                    || e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.INVOICEQTY_FLD
                    || e.Column.DataColumn.DataField == SO_ConfirmShipDetailTable.VATPERCENT_FLD)
                {
                    if (dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.PRICE_FLD].ToString() != string.Empty
                        && dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.INVOICEQTY_FLD].ToString() != string.Empty)
                    {
                        if (dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.VATPERCENT_FLD].ToString() != string.Empty)
                        {
                            dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.VATAMOUNT_FLD] = decimal.Parse(
                                dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.INVOICEQTY_FLD].ToString())
                                                                                              *
                                                                                              decimal.Parse(
                                                                                                  dgrdData[
                                                                                                      dgrdData.Row,
                                                                                                      SO_ConfirmShipDetailTable
                                                                                                          .PRICE_FLD].
                                                                                                      ToString())*
                                                                                              decimal.Parse(
                                                                                                  dgrdData[
                                                                                                      dgrdData.Row,
                                                                                                      SO_ConfirmShipDetailTable
                                                                                                          .
                                                                                                          VATPERCENT_FLD
                                                                                                      ].ToString())/100;
                        }

                        dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.NETAMOUNT_FLD] = decimal.Parse(
                            dgrdData[dgrdData.Row, SO_ConfirmShipDetailTable.INVOICEQTY_FLD].ToString())
                                                                                          *
                                                                                          decimal.Parse(
                                                                                              dgrdData[
                                                                                                  dgrdData.Row,
                                                                                                  SO_ConfirmShipDetailTable
                                                                                                      .PRICE_FLD].
                                                                                                  ToString());
                    }
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPrint_EnabledChanged(object sender, EventArgs e)
        {
            btnPrintConfiguration.Enabled = ((Control) sender).Enabled;
        }

        private bool IsValidateToDate()
        {
            //if ToDate < FromDate then return false else return true
            if ((dtmFromDate.Value.ToString() != string.Empty) && (dtmToDate.Value.ToString() != string.Empty))
            {
                if (((DateTime) (dtmFromDate.Value)) > ((DateTime) (dtmToDate.Value)))
                    return false;
            }
            return true;
        }

        private void cboPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            const string methodName = This + "cboPurpose_SelectedIndexChanged";
            try
            {
                ClearForm(this);
                if (dstData != null && dstData.Tables.Count > 0)
                    dstData.Tables[0].Clear();
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Displays the error message if throwed from system.
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm(Control pobjControl)
        {
            foreach (Control objControl in pobjControl.Controls)
            {
                if (objControl.HasChildren)
                    ClearForm(objControl);

                // if it's a ListBox, clear all item in it
                if (objControl.GetType().Equals(typeof (ListBox)))
                {
                    // clear datasource first if any
                    ((ListBox) objControl).DataSource = null;
                    // clear all items
                    ((ListBox) objControl).Items.Clear();
                }
                // If it's TextBox or C1TextBox
                if ((objControl.GetType().Equals(typeof (TextBox)))
                    || (objControl.GetType().Equals(typeof (C1TextBox))))
                {
                    var txtBox = (TextBox) objControl;
                    txtBox.Text = string.Empty;
                    txtBox.Tag = null;
                }
                else if (objControl.GetType().Equals(typeof (C1DateEdit)))
                {
                    var dtmC1Date = (C1DateEdit) objControl;
                    dtmC1Date.Value = DBNull.Value;
                }
                else if (objControl.GetType().Equals(typeof (C1NumericEdit))) // if it is C1NumericEdit
                {
                    var dtmC1Num = (C1NumericEdit) objControl;
                    dtmC1Num.Value = DBNull.Value;
                }
                else if (objControl.GetType().Equals(typeof (C1Combo))) // if it is C1Combo
                {
                    if (!objControl.Equals(cboCCN))
                    {
                        var cboC1 = (C1Combo) objControl;
                        cboC1.SelectedIndex = -1;
                    }
                }
                else if (objControl.GetType().Equals(typeof (CheckBox)))
                {
                    var chkBox = (CheckBox) objControl;
                    chkBox.Checked = false;
                }
            }
        }

        private bool SelectLocation(bool pblnAlwaysShowDialog)
        {
            //User has entered master location
            if (txtMasLoc.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTMASLOC, MessageBoxIcon.Exclamation);
                txtMasLoc.Focus();
                return false;
            }

            var htbCriteria = new Hashtable();
            DataRowView drwResult = null;

            htbCriteria.Add(MST_LocationTable.MASTERLOCATIONID_FLD, txtMasLoc.Tag);

            drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME, MST_LocationTable.CODE_FLD,
                                                             txtLocation.Text.Trim(), htbCriteria, pblnAlwaysShowDialog);
            // If has BIN matched searching condition, fill values to form's controls
            if (drwResult != null)
            {
                FillLocationData(drwResult);
            }
            else if (!pblnAlwaysShowDialog)
            {
                txtLocation.Focus();
                return false;
            }

            return true;
        }

        private void FillLocationData(DataRowView drwResult)
        {
            if (txtLocation.Tag == null)
            {
                txtBin.Text = string.Empty;
                txtBin.Tag = null;
            }
            else if (!txtLocation.Tag.Equals(drwResult[MST_LocationTable.LOCATIONID_FLD]))
            {
                txtBin.Text = string.Empty;
                txtBin.Tag = null;
            }

            txtLocation.Text = drwResult == null ? string.Empty : drwResult[MST_LocationTable.CODE_FLD].ToString();
            txtLocation.Tag = drwResult == null ? null : drwResult[MST_LocationTable.LOCATIONID_FLD];

            // reset available quantity
            for (int i = 0; i < dgrdData.RowCount; i++)
                dgrdData[i, IV_AdjustmentTable.AVAILABLEQTY_FLD] = null;

            //reset Modified status
            txtLocation.Modified = false;
        }

        private void btnSearchLocation_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSearchLocation_Click()";
            try
            {
                SelectLocation(true);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtLocation_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtLocation_Validating()";

            try
            {
                //exit if not in add action or empty


                if (txtLocation.Text.Trim().Length == 0)
                {
                    txtLocation.Tag = null;

                    txtBin.Text = string.Empty;
                    txtBin.Tag = null;
                    return;
                }
                else if (!txtLocation.Modified)
                {
                    return;
                }

                e.Cancel = !SelectLocation(false);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private bool SelectBin(bool pblnAlwaysShowDialog)
        {
            //User has entered Location
            if (txtLocation.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Exclamation);
                txtLocation.Focus();
                return false;
            }

            string strCondition = MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + " = " + txtLocation.Tag;
            strCondition += " AND MST_BIN." + MST_BINTable.BINTYPEID_FLD + " != " + ((int) BinTypeEnum.LS);
            strCondition += " AND MST_BIN." + MST_BINTable.BINTYPEID_FLD + " != " + ((int) BinTypeEnum.IN);
            DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD,
                                                                         txtBin.Text.Trim(), strCondition);
            // If has BIN matched searching condition, fill values to form's controls
            if (drwResult != null)
            {
                txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
                txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];

                // get bin cache data of to selected bin
                DataTable binCacheData = _boCsManagement.GetBinCacheData((int) txtBin.Tag);

                // reset available quantity
                for (int i = 0; i < dgrdData.RowCount; i++)
                {
                    try
                    {
                        var productId = (int) dgrdData[i, SO_ConfirmShipDetailTable.PRODUCTID_FLD];
                        DataRow row =
                            binCacheData.Rows.Cast<DataRow>().FirstOrDefault(
                                r => (int) r[IV_BinCacheTable.PRODUCTID_FLD] == productId);
                        if (row != null)
                            dgrdData[i, IV_AdjustmentTable.AVAILABLEQTY_FLD] = row[IV_BinCacheTable.OHQUANTITY_FLD];
                    }
                    catch
                    {
                    }
                }

                //Reset modify status
                txtBin.Modified = false;
            }
            else if (!pblnAlwaysShowDialog)
            {
                txtBin.Tag = null;
                txtBin.Focus();
                return false;
            }

            return true;
        }

        private void btnSearchBin_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSearchBin_Click()";
            try
            {
                SelectBin(true);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBin_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtBin_Validating()";

            try
            {
                if (txtBin.Text.Trim().Length == 0)
                {
                    txtBin.Tag = null;
                    // reset available quantity
                    for (int i = 0; i < dgrdData.RowCount; i++)
                        dgrdData[i, IV_AdjustmentTable.AVAILABLEQTY_FLD] = null;
                    return;
                }
                if (!txtBin.Modified)
                {
                    return;
                }

                e.Cancel = !SelectBin(false);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Exclamation);
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Exclamation);
                try
                {
                    Logger.LogMessage(ex, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckAvailableQuantity()
        {
            var binId = (int) txtBin.Tag;
            DataTable binCacheData = _boCsManagement.GetBinCacheData(binId);
            var isEdit = _formAction == EnumAction.Edit;

            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                bool alowNegative = false;
                try
                {
                    alowNegative = (bool) dgrdData[i, ITM_ProductTable.ALLOWNEGATIVEQTY_FLD];
                }
                catch
                {
                }
                if (alowNegative)
                {
                    continue;
                }
                try
                {
                    var productId = (int) dgrdData[i, SO_ConfirmShipDetailTable.PRODUCTID_FLD];
                    DataRow row = binCacheData.Rows.Cast<DataRow>().FirstOrDefault(r => (int) r[IV_BinCacheTable.PRODUCTID_FLD] == productId);
                    decimal cacheQuantity = decimal.Zero;
                    if (row != null)
                    {
                        cacheQuantity = (decimal) row[IV_BinCacheTable.OHQUANTITY_FLD];
                    }
                    var invoiceQuantity = (decimal) dgrdData[i, SO_ConfirmShipDetailTable.INVOICEQTY_FLD];
                    // if user modify the transaction, we need to subtract from old invoice quantiy
                    if (isEdit)
                    {
                        decimal oldQuantity;
                        decimal.TryParse(dgrdData[i, OldInvoiceqtyCol].ToString(), out oldQuantity);
                        invoiceQuantity = invoiceQuantity - oldQuantity;
                    }
                    if (invoiceQuantity > cacheQuantity)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_COMMITQUANTITY_GREATER_THAN_AVAILABLEQUANTITY, MessageBoxIcon.Warning);
                        dgrdData.Row = i;
                        return false;
                    }
                }
                catch
                {
                }
            }
            return true;
        }

        private void FillDefaultLocation()
        {
            string locationCode = _saleType == 1 ? "PC-DE-FG" : "PC-SF-EX";
            string binCode = _saleType == 1 ? "OK PC-DE-FG" : "OK PC-SF-EX";
            // fill default location to WH-FinishGoods
            DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                         MST_LocationTable.CODE_FLD, locationCode, null,
                                                                         false);
            if (drwResult != null)
                FillLocationData(drwResult);
            // fill default bin to OK bin
            drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD, binCode,
                                                             null, false);
            if (drwResult != null)
            {
                txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
                txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
            }
        }

        private void dtmShipmentDate_ValueChanged(object sender, EventArgs e)
        {
            // when user enter shipped date, update invoice date, onboard date time
            if (!dtmShipmentDate.ValueIsDbNull)
            {
                var shipmentDate = (DateTime) dtmShipmentDate.Value;
                dtmInvoiceDate.Value = shipmentDate;
                dtmOnBoardDate.Value = shipmentDate;
            }
        }

        private void dgrdData_AfterDelete(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.C1Report;
using C1.Win.C1TrueDBGrid;
using PCSComProcurement.Purchase.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataContext;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using v_PurchaseOrderOfItem = PCSComUtils.Common.v_PurchaseOrderOfItem;
using v_ReceiptBySchedule = PCSComUtils.Common.v_ReceiptBySchedule;

namespace PCSProcurement.Purchase
{
    public partial class PurchaseOrderReceipts : Form
    {
        #region constants

        private const string This = "PCSProcurement.Purchase.PurchaseOrderReceipts";
        private const string ReceiptlineCol = "ReceiptLine";
        private const string Seperator = "-";
        private const string SlipEndFormat = "yyyyMMddhh";
        private const string YearFilter = "[Year]";
        private const string MonthFilter = "[Month]";
        private const string DayFilter = "[Day]";
        private const string HourFilter = "[Hour]";

        #endregion

        #region fields

        private EnumAction _formMode = EnumAction.Default;
        private RadioStatus _enmOldRadioStatus = RadioStatus.BySlip;
        private RadioStatus _enmStatus = RadioStatus.BySlip;
        private DataTable _gridLayOut = new DataTable();
        private DataSet _dataSource;
        private DataSet _dataCopy;
        private DateTime _currentDate;
        private DateTime _slipDate;
        private int _ccnId;
        private int _masterLocationId;
        private int _partyId;
        private string _pONumber = string.Empty;
        private PO_PurchaseOrderReceiptMaster _receiptMaster;
        private PO_InvoiceMaster _invoiceMaster = new PO_InvoiceMaster();
        private MST_MasterLocation _masterLocation = new MST_MasterLocation();
        private PO_PurchaseOrderDetail _poDetail = new PO_PurchaseOrderDetail();
        private PO_PurchaseOrderMaster _poMaster = new PO_PurchaseOrderMaster();

        #endregion
      
        public PurchaseOrderReceipts()
        {
            InitializeComponent();
        }

        private void PurchaseOrderReceipts_Load(object sender, EventArgs e)
        {
            const string methodName = This + ".PurchaseOrderReceipts_Load()";
            try
            {
                //Set authorization for user
                var objSecurity = new Security();
                Name = This;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    Close();
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    return;
                }
                // fill data to CCN combo
                FormControlComponents.PutDataIntoC1ComboBox(CCNCombo, Utilities.Instance.ListCCN(), MST_CCNTable.CODE_FLD,
                                                            MST_CCNTable.CCNID_FLD, MST_CCNTable.TABLE_NAME);
                // set selected value to default CCN
                CCNCombo.SelectedValue = SystemProperty.CCNID;
                // set value to PostDate = null
                PostDatePicker.Value = null;
                radBySlip.Checked = true;
                _currentDate = Utilities.Instance.GetServerDate().AddDays(1);
                // switch form mode
                SwitchFormMode();
                _gridLayOut = FormControlComponents.StoreGridLayout(dgrdData);
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

        private void ReceiveNoText_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".ReceiveNoText_Validating()";
            try
            {
                if (!SearchReceiveButton.Enabled)
                    return;
                if (ReceiveNoText.Modified)
                {
                    if (ReceiveNoText.Text.Trim().Length == 0)
                    {
                        ResetForm();
                        return;
                    }
                    // display open search form
                    var htCon = new Hashtable { { PO_PurchaseOrderReceiptMasterTable.CCNID_FLD, SystemProperty.CCNID } };
                    var drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderReceiptMasterTable.TABLE_NAME, PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD,
                                                                         ReceiveNoText.Text.Trim(), htCon, false);
                    if (drvResult != null)
                        FillMasterData((int)drvResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD]);
                    else
                        e.Cancel = true;
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

        private void ReceiveNoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (SearchReceiveButton.Enabled && e.KeyCode == Keys.F4)
                SearchReceiveButton_Click(null, null);
        }

        private void SearchReceiveButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".SearchReceiveButton_Click()";
            try
            {
                // display open search form
                var htCon = new Hashtable { { PO_PurchaseOrderReceiptMasterTable.CCNID_FLD, SystemProperty.CCNID } };
                var drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseOrderReceiptMasterTable.TABLE_NAME, PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD,
                                                                     ReceiveNoText.Text.Trim(), htCon, true);
                if (drvResult != null)
                    FillMasterData((int)drvResult[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD]);
                else
                    ReceiveNoText.Focus();
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

        private void txtMasLoc_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtMasLoc_Validating()";
            try
            {
                if (!btnSearchMasLoc.Enabled)
                    return;
                if (txtMasLoc.Modified)
                {
                    if (txtMasLoc.Text.Trim().Length == 0)
                    {
                        FillMasterLocationData(0);
                        return;
                    }
                    // display open search form
                    var htCon = new Hashtable { { MST_MasterLocationTable.CCNID_FLD, SystemProperty.CCNID } };
                    var drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD,
                                                                         txtMasLoc.Text.Trim(), htCon, false);
                    if (drvResult != null)
                        FillMasterData((int)drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD]);
                    else
                        e.Cancel = true;
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

        private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnSearchMasLoc.Enabled && e.KeyCode == Keys.F4)
                btnSearchMasLoc_Click(null, null);
        }

        private void btnSearchMasLoc_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSearchMasLoc_Click()";
            try
            {
                // display open search form
                    var htCon = new Hashtable { { MST_MasterLocationTable.CCNID_FLD, SystemProperty.CCNID } };
                    var drvResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME, MST_MasterLocationTable.CODE_FLD,
                                                                         txtMasLoc.Text.Trim(), htCon, true);
                    if (drvResult != null)
                        FillMasterData((int)drvResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD]);
                    else
                        txtMasLoc.Focus();
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

        private void radBySlip_CheckedChanged(object sender, EventArgs e)
        {
            const string methodName = This + ".radBySlip_CheckedChanged()";
            try
            {
                if (!radBySlip.Enabled)
                    return;
                // Avoid recursive
                if (_enmStatus == RadioStatus.NoChange) return;
                _enmStatus = RadioStatus.BySlip;
                if (!ConfirmRadio()) return;

                EnableButtons();
                if (radBySlip.Checked)
                {
                    txtInvoice.Text = string.Empty;
                    txtProductionLine.Text = txtOutside.Text = string.Empty;
                    txtVendorName.Text = string.Empty;
                    txtVendorNo.Text = string.Empty;
                    _invoiceMaster = new PO_InvoiceMaster();
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    // lock grid
                    foreach (C1DisplayColumn dcolC1 in dgrdData.Splits[0].DisplayColumns)
                        dcolC1.Locked = true;
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

        private void btnDeliverySlip_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".ReceiveNoText_Validating()";
            try
            {
                string strSlipNumber = txtDeliverySlip.Text.Trim();
                
                if (strSlipNumber == string.Empty)
                {
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    // lock the grid
                    dgrdData.Splits[0].Locked = true;
                    return;
                }
                // user must select CCN first
                int intCCNID = 0;
                try
                {
                    intCCNID = int.Parse(CCNCombo.SelectedValue.ToString());
                    if (intCCNID <= 0) throw new Exception();
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Error);
                    CCNCombo.Focus();
                    return;
                }
                if (_masterLocationId <= 0)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMasLoc.Focus();
                    return;
                }
                try
                {
                    // now get the po number from slip number
                    _pONumber = strSlipNumber.Remove(
                        strSlipNumber.Length - SlipEndFormat.Length - Seperator.Length,
                        SlipEndFormat.Length + Seperator.Length);
                    // get PO Master object from the code
                    _poMaster = PurchaseOrderReceiptBO.Instance.GetPurchaseOrderMaster(_pONumber);
                    if (_poMaster.PurchaseTypeID != (int)POType.Domestic)
                    {
                        var strMsg = new string[2];
                        strMsg[0] = radBySlip.Text;
                        strMsg[1] = POType.Domestic.ToString();
                        PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_POTYPE, MessageBoxIcon.Error, strMsg);
                        return;
                    }
                    // remove Po number from the string
                    strSlipNumber = strSlipNumber.Substring(_pONumber.Length + 1);
                    // slip number = yyyyMMddHH
                    // get year from first 4 char of slip number
                    int intYear = int.Parse(strSlipNumber.Substring(0, 4));
                    int intMonth = int.Parse(strSlipNumber.Substring(4, 2));
                    int intDay = int.Parse(strSlipNumber.Substring(6, 2));
                    int intHours = int.Parse(strSlipNumber.Substring(8));
                    _slipDate = new DateTime(intYear, intMonth, intDay, intHours, 0, 0);
                }
                catch (Exception ex)
                {
                    var strMsg = new string[2];
                    strMsg[0] = txtDeliverySlip.Text.Trim();
                    strMsg[1] = "xxxxx" + Seperator + SlipEndFormat;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_FORMAT, MessageBoxIcon.Error, strMsg);
                    txtDeliverySlip.Focus();
                    txtDeliverySlip.SelectAll();
                    return;
                }
                _partyId = _poMaster.PartyID;
                FillCustomerInfo(_partyId);
                // retrieve detail data from delivery schedule by PO number
                _dataSource = PurchaseOrderReceiptBO.Instance.ListByPurchaseOrderCode(_pONumber, _slipDate);
                // fill to data grid
                if (_dataSource.Tables.Count > 0 && _dataSource.Tables[0].Rows.Count > 0)
                    BindDataToGrid(false);
                else
                {
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    BindDataToGrid(false);
                    // lock the grid
                    dgrdData.Splits[0].Locked = true;
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

        private void radByInvoice_CheckedChanged(object sender, EventArgs e)
        {
            const string methodName = This + ".radByInvoice_CheckedChanged()";
            try
            {
                if (!radByInvoice.Enabled)
                    return;
                // Avoid recursive
                if (_enmStatus == RadioStatus.NoChange) return;
                _enmStatus = RadioStatus.ByInvoice;
                if (!ConfirmRadio()) return;
                EnableButtons();
                if (radByInvoice.Checked)
                {
                    txtDeliverySlip.Text = string.Empty;
                    txtOutside.Text = txtProductionLine.Text = string.Empty;
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    // lock grid
                    foreach (C1DisplayColumn dcolC1 in dgrdData.Splits[0].DisplayColumns)
                        dcolC1.Locked = true;
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

        private void txtInvoice_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtInvoice_Validating()";
            try
            {
                if (!btnInvoice.Enabled)
                    return;
                if (txtInvoice.Modified)
                {
                    if (txtInvoice.Text.Trim().Length == 0)
                    {
                        FillInvoiceData(null);
                        return;
                    }
                    // display open search form
                    var htCon = new Hashtable { { PO_InvoiceMasterTable.CCNID_FLD, SystemProperty.CCNID } };
                    var drvResult = FormControlComponents.OpenSearchForm(PO_InvoiceMasterTable.TABLE_NAME, PO_InvoiceMasterTable.INVOICENO_FLD,
                                                                         txtInvoice.Text.Trim(), htCon, false);
                    if (drvResult != null)
                        FillInvoiceData(drvResult.Row);
                    else
                        e.Cancel = true;
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

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnInvoice.Enabled && e.KeyCode == Keys.F4)
                btnInvoice_Click(null, null);
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnInvoice_Click()";
            try
            {
                // display open search form
                var htCon = new Hashtable {{PO_InvoiceMasterTable.CCNID_FLD, SystemProperty.CCNID}};
                var drvResult = FormControlComponents.OpenSearchForm(PO_InvoiceMasterTable.TABLE_NAME,
                                                                     PO_InvoiceMasterTable.INVOICENO_FLD,
                                                                     txtInvoice.Text.Trim(), htCon, false);
                if (drvResult != null)
                    FillInvoiceData(drvResult.Row);
                else
                    txtInvoice.Focus();
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

        private void radOutside_CheckedChanged(object sender, EventArgs e)
        {
            const string methodName = This + ".radOutside_CheckedChanged()";
            try
            {
                if (!radOutside.Enabled)
                    return;
                // Avoid recursive
                if (_enmStatus == RadioStatus.NoChange) return;
                _enmStatus = RadioStatus.ByOutside;
                if (!ConfirmRadio()) return;
                EnableButtons();
                if (radOutside.Checked)
                {
                    txtInvoice.Text = string.Empty;
                    txtInvoice.Text = string.Empty;
                    txtDeliverySlip.Text = string.Empty;

                    txtVendorName.Text = string.Empty;
                    txtVendorNo.Text = string.Empty;
                    _invoiceMaster = new PO_InvoiceMaster();
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    // lock grid
                    foreach (C1DisplayColumn dcolC1 in dgrdData.Splits[0].DisplayColumns)
                        dcolC1.Locked = true;
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

        private void btnOutside_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnOutside_Click()";
            try
            {
                string strSlipNumber = txtOutside.Text.Trim();

                // Empty slip number
                if (strSlipNumber == string.Empty)
                {
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    // lock the grid
                    dgrdData.Splits[0].Locked = true;
                    return;
                }
                
                if (_masterLocationId <= 0)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMasLoc.Focus();
                    return;
                }
                try
                {
                    // now get the po number from slip number
                    _pONumber = strSlipNumber.Remove(
                        strSlipNumber.Length - SlipEndFormat.Length - Seperator.Length,
                        SlipEndFormat.Length + Seperator.Length);
                    // get PO Master object from the code
                    _poMaster = PurchaseOrderReceiptBO.Instance.GetPurchaseOrderMaster(_pONumber);
                    if (_poMaster.PurchaseTypeID != (int)POType.Outside)
                    {
                        var strMsg = new string[2];
                        strMsg[0] = radOutside.Text;
                        strMsg[1] = POType.Outside.ToString();
                        PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_POTYPE, MessageBoxIcon.Error, strMsg);
                        return;
                    }
                    // remove Po number from the string
                    strSlipNumber = strSlipNumber.Substring(_pONumber.Length + 1);
                    // slip number = yyyyMMddHH
                    // get year from first 4 char of slip number
                    int intYear = int.Parse(strSlipNumber.Substring(0, 4));
                    int intMonth = int.Parse(strSlipNumber.Substring(4, 2));
                    int intDay = int.Parse(strSlipNumber.Substring(6, 2));
                    int intHours = int.Parse(strSlipNumber.Substring(8));
                    _slipDate = new DateTime(intYear, intMonth, intDay, intHours, 0, 0);
                }
                catch
                {
                    var strMsg = new string[2];
                    strMsg[0] = txtOutside.Text.Trim();
                    strMsg[1] = "xxxxx" + Seperator + SlipEndFormat;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_FORMAT, MessageBoxIcon.Error, strMsg);
                    txtOutside.Focus();
                    txtOutside.SelectAll();
                    return;
                }
                _partyId = _poMaster.PartyID;
                FillCustomerInfo(_partyId);
                // retrieve detail data from delivery schedule by PO number
                _dataSource = PurchaseOrderReceiptBO.Instance.ListByPurchaseOrderCode(_pONumber, _slipDate);
                // fill to data grid
                if (_dataSource.Tables.Count > 0 && _dataSource.Tables[0].Rows.Count > 0)
                    BindDataToGrid(false);
                else
                {
                    if (_dataSource != null && _dataSource.Tables.Count > 0)
                        _dataSource.Tables[0].Clear();
                    BindDataToGrid(false);
                    // lock the grid
                    dgrdData.Splits[0].Locked = true;
                }

                //HACK: added by Tuan TQ: Enable BOM shortage button
                if (radOutside.Checked
                    && _dataSource != null
                    && txtProductionLine.Tag != null
                    && _pONumber != string.Empty
                    && !PostDatePicker.Text.Trim().Equals(string.Empty)
                    && !txtOutside.Text.Trim().Equals(string.Empty)
                    && dgrdData.RowCount > 0)
                {
                    btnBOMShortage.Enabled = true;
                }
                else
                {
                    btnBOMShortage.Enabled = false;
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

        private void txtProductionLine_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtProductionLine_Validating()";
            try
            {
                if (!btnProductionLine.Enabled)
                    return;
                if (txtProductionLine.Modified)
                {
                    if (txtProductionLine.Text.Trim().Length == 0)
                    {
                        ResetForm();
                        return;
                    }
                    var drowData = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD,
                                                                    txtProductionLine.Text.Trim(), null, false);
                    if (drowData != null)
                        FillProductionLine(drowData.Row);
                    else
                        e.Cancel = true;
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

        private void txtProductionLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnProductionLine.Enabled && e.KeyCode == Keys.F4)
                btnProductionLine_Click(null, null);
        }

        private void btnProductionLine_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnProductionLine_Click()";
            try
            {
                var drowData = FormControlComponents.OpenSearchForm(PRO_ProductionLineTable.TABLE_NAME, PRO_ProductionLineTable.CODE_FLD,
                                                                    txtProductionLine.Text.Trim(), null, true);
                if (drowData != null)
                    FillProductionLine(drowData.Row);
                else
                    txtProductionLine.Focus();
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

        private void dgrdData_AfterColUpdate(object sender, ColEventArgs e)
        {
            const string methodName = This + ".dgrdData_AfterColUpdate()";
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD:
                        if (e.Column.DataColumn.Tag != null && dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD].ToString() != string.Empty)
                            FillPOLineData((DataRow)e.Column.DataColumn.Tag);
                        else
                            FillPOLineData(null);
                        break;
                    case MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD:
                        if (e.Column.DataColumn.Tag != null && dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].ToString() != string.Empty)
                            FillLocationData((DataRow)e.Column.DataColumn.Tag);
                        else
                            FillLocationData(null);
                        break;
                    case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
                        if (e.Column.DataColumn.Tag != null && dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].ToString() != string.Empty)
                            FillBinData((DataRow)e.Column.DataColumn.Tag);
                        else
                            FillBinData(null);
                        break;
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

        private void dgrdData_AfterDelete(object sender, EventArgs e)
        {
            const string methodName = This + ".dgrdData_AfterDelete()";
            try
            {
                if (dgrdData.RowCount == 0)
                    return;
                // re-assign line value
                int intCount = 0;
                for (int i = 0; i < dgrdData.RowCount; i++)
                {
                    dgrdData[i, ReceiptlineCol] = ++intCount;
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

        private void dgrdData_BeforeColEdit(object sender, BeforeColEditEventArgs e)
        {
            const string methodName = This + ".dgrdData_BeforeColEdit()";
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD:
                        // if current selected Location is not controlled by Bin then lock Bin cell
                        var locationId = (int)dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD];
                        if (locationId > 0)
                        {
                            var voLocation = Utilities.Instance.GetLocation(locationId);
                            if (voLocation.Bin.GetValueOrDefault(false))
                                e.Column.Locked = false;
                            else
                            {
                                e.Cancel = true;
                                e.Column.Locked = true;
                            }
                        }
                        break;
                    case PO_PurchaseOrderReceiptDetailTable.LOT_FLD:
                        // if current Item is not controlled by Lot then Lock Lot cell
                        var productId = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD];
                        if (productId > 0)
                        {
                            var voCurrentProduct = Utilities.Instance.GetProductInfo(productId);
                            if (!voCurrentProduct.LotControl.GetValueOrDefault(false))
                            {
                                e.Cancel = true;
                                // lock Lot cell
                                e.Column.Locked = true;
                            }
                            else // unlock Lot cell
                                e.Column.Locked = false;
                        }
                        break;
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

        private void dgrdData_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            const string methodName = This + ".dgrdData_BeforeColUpdate()";
            try
            {
                string strColumn = dgrdData.Columns[dgrdData.Col].DataField;
                string strValue = dgrdData.Columns[dgrdData.Col].Text.Trim();
                if (strValue == string.Empty)
                    return;
                var htbConditions = new Hashtable();
                decimal receiveQuantity = 0;
                ITM_Product currentProduct;
                decimal totalQuantityOfLot;
                int poDetailId;
                DataRowView drvData = null;
                switch (strColumn)
                {
                    #region Receive Quantity

                    case PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD:
                        try
                        {
                            poDetailId = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
                            if (poDetailId <= 0)
                                throw new Exception();
                        }
                        catch
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_PRODUCT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
                            dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].Text = string.Empty;
                            dgrdData.Focus();
                            dgrdData.Select();
                            return;
                        }
                        try
                        {
                            receiveQuantity = decimal.Parse(strValue);
                            if (receiveQuantity <= 0)
                            {
                                PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
                                e.Cancel = true;
                            }
                        }
                        catch
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_INVALID_NUMERIC, MessageBoxButtons.OK,MessageBoxIcon.Error);
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
                            e.Cancel = true;
                        }
                        break;

                    #endregion

                    #region Purchase order line

                    // select purchase order line
                    case (PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD):
                        if (radBySlip.Checked)
                        {
                            if (_slipDate != DateTime.MaxValue)
                            {
                                htbConditions.Add(YearFilter, _slipDate.Year);
                                htbConditions.Add(MonthFilter, _slipDate.Month);
                                htbConditions.Add(DayFilter, _slipDate.Day);
                                htbConditions.Add(HourFilter, _slipDate.Hour);
                            }
                        }
                        var purchaseOrderMasterId = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD];
                        htbConditions.Add(PO_PurchaseOrderDetailTable.PURCHASEORDERMASTERID_FLD, purchaseOrderMasterId);
                        if (strValue != string.Empty)
                        {
                            drvData = radBySlip.Checked
                                          ? FormControlComponents.OpenSearchForm(v_ReceiptBySchedule.VIEW_NAME,
                                                                                 PO_PurchaseOrderDetailTable.LINE_FLD,
                                                                                 strValue, htbConditions, true)
                                          : FormControlComponents.OpenSearchForm(v_PurchaseOrderOfItem.VIEW_NAME,
                                                                                 PO_PurchaseOrderDetailTable.LINE_FLD,
                                                                                 strValue, htbConditions, true);
                        }
                        if (drvData == null)
                        {
                            if (strValue != string.Empty)
                                e.Cancel = true;
                        }
                        else
                        {
                            string strExpression = PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
                                                   + Constants.EQUAL +
                                                   drvData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
                            if (radBySlip.Checked)
                                strExpression += Constants.AND + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
                                                 + Constants.EQUAL +
                                                 drvData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString();
                            // get current row purchase order detail and delivery schedule
                            var intRowPODetailId = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
                            var intDeliveryScheduleId = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD];
                            // we need to check for exsiting PO Line and delivery line in the list
                            DataRow[] drowExisted = _dataSource.Tables[0].Select(strExpression);
                            if (drowExisted.Length.Equals(0))
                            {
                                e.Column.DataColumn.Tag = drvData.Row;
                            }
                            else
                            {
                                if (radBySlip.Checked)
                                {
                                    if (drvData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].Equals(intRowPODetailId) &&
                                        drvData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].Equals(intDeliveryScheduleId))
                                        e.Column.DataColumn.Tag = drvData.Row;
                                }
                            }
                        }
                        break;

                    #endregion

                    #region Location

                    // select location
                    case (MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD):
                        // user must select master location first
                        if (_masterLocation.MasterLocationID < 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_MASTERLOCATION, MessageBoxIcon.Error);
                            txtMasLoc.Focus();
                            e.Cancel = true;
                            return;
                        }
                        htbConditions.Add(MST_LocationTable.MASTERLOCATIONID_FLD, _masterLocation.MasterLocationID);
                        if (strValue != string.Empty)
                            drvData = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                           MST_LocationTable.CODE_FLD, strValue,
                                                                           htbConditions, true);
                        if (drvData == null)
                        {
                            if (strValue != string.Empty)
                                e.Cancel = true;
                        }
                        else
                            e.Column.DataColumn.Tag = drvData.Row;
                        break;

                    #endregion

                    #region Bin

                    // select bin
                    case (MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD):
                        int locationId;
                        try
                        {
                            locationId = (int)dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD];
                            // user must select location first
                            if (locationId <= 0)
                            {
                                PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxButtons.OK,MessageBoxIcon.Error);
                                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                                e.Cancel = true;
                                return;
                            }
                        }
                        catch
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxButtons.OK,MessageBoxIcon.Error);
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                            e.Cancel = true;
                            return;
                        }
                        var voLocation = Utilities.Instance.GetLocation(locationId);
                        if (voLocation.Bin.GetValueOrDefault(false))
                            dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = false;
                        else
                        {
                            dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = true;
                            return;
                        }
                        htbConditions.Add(MST_BINTable.LOCATIONID_FLD, locationId);
                        if (radByInvoice.Checked)
                        {
                            htbConditions.Add(MST_BINTable.BINTYPEID_FLD,
                                              (int)BinTypeEnum.OK
                                              + " OR " + MST_BINTable.TABLE_NAME + ".BinTypeID = " +
                                              (int)BinTypeEnum.NG);
                        }
                        if (strValue != string.Empty)
                            drvData = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME,
                                                                           MST_BINTable.CODE_FLD, strValue,
                                                                           htbConditions, true);
                        if (drvData == null)
                        {
                            if (strValue != string.Empty)
                                e.Cancel = true;
                        }
                        else
                            e.Column.DataColumn.Tag = drvData.Row;
                        break;

                    #endregion
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

        private void dgrdData_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (_dataSource != null && _dataSource.Tables.Count > 0)
            {
                _dataSource.Tables[0].Columns[ReceiptlineCol].AutoIncrement = false;
            }
        }

        private void dgrdData_ButtonClick(object sender, ColEventArgs e)
        {
            const string methodName = This + ".dgrdData_ButtonClick()";
            try
            {
                if (!dgrdData.AllowAddNew && !dgrdData.AllowUpdate)
                {
                    return;
                }
                // if current column is locked then return
                if (dgrdData.Splits[0].Locked || dgrdData.Splits[0].DisplayColumns[dgrdData.Col].Locked)
                    return;
                FillDataToGrid();
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

        private void dgrdData_KeyDown(object sender, KeyEventArgs e)
        {
            if (_formMode == EnumAction.Default)
            {
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.F4:
                    dgrdData_ButtonClick(null, null);
                    break;
                case Keys.Delete:
                    if (dgrdData.SelectedRows.Count <= 0)
                    {
                        return;
                    }

                    dgrdData.DeleteMultiRows();

                    // re-assign line value
                    int intCount = 0;
                    for (int i = 0; i < dgrdData.RowCount; i++)
                    {
                        dgrdData[i, ReceiptlineCol] = ++intCount;
                    }
                    break;
            }
        }

        private void dgrdData_OnAddNew(object sender, EventArgs e)
        {
            int intMaxLine = 0;
            // get max line number
            if (dgrdData.Row > 0)
                intMaxLine = (int)dgrdData[dgrdData.Row - 1, ReceiptlineCol];
            dgrdData[dgrdData.Row, ReceiptlineCol] = ++intMaxLine;
        }

        private void dgrdData_RowColChange(object sender, RowColChangeEventArgs e)
        {
            const string methodName = This + ".dgrdData_RowColChange()";
            try
            {
                if (e.LastRow != dgrdData.Row)
                {
                    // if current Item is not controlled by Lot then Lock Lot cell
                    var productId = 0;
                    try
                    {
                        productId = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD];
                        // check if Lot cell of current row have no value then lock serial cell
                        string strLot = dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.LOT_FLD].ToString().Trim();
                        dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Locked = string.IsNullOrEmpty(strLot);
                    }
                    catch{}
                    if (productId > 0)
                    {
                        var voCurrentProduct = Utilities.Instance.GetProductInfo(productId);
                        dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Locked = !voCurrentProduct.LotControl.GetValueOrDefault(false);
                    }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnAdd_Click()";
            try
            {
                // turn form mode to ADD
                _formMode = EnumAction.Add;
                // clear all data
                ResetForm();
                _receiptMaster = new PO_PurchaseOrderReceiptMaster();
                PostDatePicker.Value = Utilities.Instance.GetServerDate();
                // fill in receive number
                ReceiveNoText.Text = FormControlComponents.GetNoByMask(this);
                //Fill Default Master Location 
                FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
                _masterLocationId = SystemProperty.MasterLocationID;
                _masterLocation.MasterLocationID = SystemProperty.MasterLocationID;
                radBySlip.Checked = true;
                _enmStatus = RadioStatus.BySlip;
                // switching form mode
                SwitchFormMode();
                PostDatePicker.Focus();
                PostDatePicker.SelectAll();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSave_Click()";
            try
            {
                DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_DATA,
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button2);
                if (dlgResult == DialogResult.No)
                    return;
                if (Security.IsDifferencePrefix(this, ReceiveNoLabel, ReceiveNoText))
                    return;

                if (dgrdData.EditActive)
                    return;

                if (!CheckMandatoryFields())
                    return;
                // make a copy of source dataset
                _dataCopy = _dataSource.Copy();
                if (SaveData())
                {
                    // turn to DEFAULT mode
                    _formMode = EnumAction.Default;
                    // get new data
                    _dataSource = PurchaseOrderReceiptBO.Instance.ListReceiptDetailByReceiptMaster(_receiptMaster.PurchaseOrderReceiptID);
                    // bind to grid
                    BindDataToGrid(false);
                    // switch form to DEFAULT mode
                    SwitchFormMode();
                    // display successful message
                    _enmOldRadioStatus = RadioStatus.BySlip;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                // restore the source
                _dataSource = new DataSet();
                _dataSource = _dataCopy.Copy();
                BindDataToGrid(false);
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
                // restore the source
                _dataSource = new DataSet();
                _dataSource = _dataCopy.Copy();
                BindDataToGrid(false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnDelete_Click()";
            if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                try
                {
                    var products = _dataSource.Tables[0].Rows.Cast<DataRow>().Select(r => (int)r[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD]).ToList();
                    string strReturnNo = PurchaseOrderReceiptBO.Instance.CheckReturn(_receiptMaster, products, radByInvoice.Checked);
                    if (strReturnNo != string.Empty)
                    {
                        var strMsg = new[] { strReturnNo };
                        PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELETE_RECEIPT, MessageBoxIcon.Information, strMsg);
                        return;
                    }

                    //1. turn form mode to Delete
                    _formMode = EnumAction.Delete;
                    PurchaseOrderReceiptBO.Instance.DeletePOReceipt(_receiptMaster.PurchaseOrderReceiptID);
                    //2. turn form mode to Default
                    _formMode = EnumAction.Default;
                    //3. clear all data
                    ResetForm();
                    //4. Enable and Disable controls
                    SwitchFormMode();
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
                    PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                    // log message.
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
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnDeleteRow_Click()";
            if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                try
                {
                    var products = new List<int>();
                    products.Add(dgrdData.RowCount > 0
                                     ? Convert.ToInt32(dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD])
                                     : Convert.ToInt32(_dataSource.Tables[0].Rows[0][PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD]));
                    string strReturnNo = PurchaseOrderReceiptBO.Instance.CheckReturn(_receiptMaster, products, radByInvoice.Checked);
                    if (strReturnNo != string.Empty)
                    {
                        var strMsg = new[] { strReturnNo };
                        PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_DELETE_RECEIPT, MessageBoxIcon.Information, strMsg);
                        return;
                    }
                    if (dgrdData.RowCount > 0)
                    {
                        #region delete row

                        //1. turn form mode to Delete
                        _formMode = EnumAction.Delete;
                        // If Rows.count = 1 then delete voucher
                        int intPurchaseOrderReceiptId = GetCurrentRow();
                        PurchaseOrderReceiptBO.Instance.DeleteRowPOReceipt(_receiptMaster.PurchaseOrderReceiptID,intPurchaseOrderReceiptId);
                        //2. turn form mode to Default
                        _formMode = EnumAction.Default;
                        //3. Load Receipt no
                        DataRowView drowData = FormControlComponents.OpenSearchForm(PO_PurchaseOrderReceiptMasterTable.TABLE_NAME,
                                                                 PO_PurchaseOrderReceiptMasterTable.RECEIVENO_FLD,
                                                                 ReceiveNoText.Text.Trim(), null, false);
                        if (drowData != null)
                            FillMasterData((int)drowData.Row[PO_PurchaseOrderReceiptMasterTable.PURCHASEORDERRECEIPTID_FLD]);

                        #endregion
                    }
                    else
                    {
                        #region Delete voucher

                        //1. turn form mode to Delete
                        _formMode = EnumAction.Delete;
                        PurchaseOrderReceiptBO.Instance.DeletePOReceipt(_receiptMaster.PurchaseOrderReceiptID);
                        //2. turn form mode to Default
                        _formMode = EnumAction.Default;
                        //3. clear all data
                        ResetForm();
                        //4. Enable and Disable controls
                        SwitchFormMode();

                        #endregion
                    }
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
                    PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
                    // log message.
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
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_receiptMaster == null)
            {
                return;
            }

            if (_receiptMaster.PurchaseOrderReceiptID <= 0)
            {
                return;
            }

            ShowPOSlipReport();
        }

        private void btnBOMShortage_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".ReceiveNoText_Validating()";
            try
            {
                //return if condition is not match
                if (!radOutside.Checked || _dataSource == null
                    || txtProductionLine.Tag == null || _pONumber == string.Empty
                    || PostDatePicker.Text.Trim().Equals(string.Empty)
                    || txtOutside.Text.Trim().Equals(string.Empty)
                    || dgrdData.RowCount <= 0)
                {
                    return;
                }

                if (_dataSource.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                Cursor = Cursors.WaitCursor;
                ShowBOMShortageReport();
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region private methods

        private void ResetForm()
        {
            // clear all data
            _receiptMaster = new PO_PurchaseOrderReceiptMaster();
            if ((_dataSource != null) && (_dataSource.Tables.Count > 0))
            {
                _dataSource.Tables[0].Clear();
            }
            PostDatePicker.Value = DBNull.Value;
            ReceiveNoText.Text = string.Empty;
            txtMasLoc.Text = string.Empty;
            ReceiveNoText.Text = string.Empty;
            txtVendorNo.Text = string.Empty;
            txtVendorName.Text = string.Empty;
            txtDeliverySlip.Text = string.Empty;
            txtInvoice.Text = string.Empty;
            txtOutside.Text = txtProductionLine.Text = string.Empty;
            _invoiceMaster = new PO_InvoiceMaster();
            _receiptMaster = new PO_PurchaseOrderReceiptMaster();
            cboPurpose.SelectedIndex = 0;
        }

        /// <summary>
        /// Enable or disable button based on receipt type
        /// </summary>
        private void EnableButtons()
        {
            bool blnBySlip = (radBySlip.Checked && radBySlip.Enabled);
            bool blnByInvoice = (radByInvoice.Checked && radByInvoice.Enabled);
            bool blnOutside = (radOutside.Checked && radOutside.Enabled);
            txtVendorName.Enabled = blnByInvoice;
            txtVendorNo.Enabled = blnByInvoice;
            txtDeliverySlip.Enabled = btnDeliverySlip.Enabled = blnBySlip;
            txtInvoice.Enabled = btnInvoice.Enabled = blnByInvoice;
            txtOutside.Enabled = btnOutside.Enabled = txtProductionLine.Enabled = btnProductionLine.Enabled = lblProductionLine.Enabled = blnOutside;
        }

        private bool ConfirmRadio()
        {
            if (_enmStatus != _enmOldRadioStatus)
            {
                //Confirm message: Are you sure you want to change type of receipt
                DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_CHANGE_TYPE_OF_RECEIVING,
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button2);
                switch (dlgResult)
                {
                    case DialogResult.Yes:
                        _enmOldRadioStatus = _enmStatus;
                        return true;
                        //break;
                    default:
                        _enmStatus = RadioStatus.NoChange;
                        if (_enmOldRadioStatus == RadioStatus.BySlip)
                        {
                            radBySlip.Checked = true;
                            _enmStatus = RadioStatus.BySlip;
                        }
                        else if (_enmOldRadioStatus == RadioStatus.ByInvoice)
                        {
                            radByInvoice.Checked = true;
                            _enmStatus = RadioStatus.ByInvoice;
                        }
                        else if (_enmOldRadioStatus == RadioStatus.ByOutside)
                        {
                            radOutside.Checked = true;
                            _enmStatus = RadioStatus.ByOutside;
                        }
                        return false;
                        //break;
                }
            }
            return false;
        }

        /// <summary>
        /// This methods uses to assign default value for grid
        /// </summary>
        private void AssignDefaultValue()
        {
            // if receipt by PO then we assign default value for PO MasterID
            if (radBySlip.Checked && _poMaster != null && (_poMaster.PurchaseOrderMasterID > 0))
            {
                _dataSource.Tables[0].Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD].DefaultValue = _poMaster.Code;
                _dataSource.Tables[0].Columns[PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].DefaultValue = _poMaster.PurchaseOrderMasterID;
            }

            for (int i = 0; i < dgrdData.RowCount; i++)
            {
                if (dgrdData.AddNewMode == AddNewModeEnum.NoAddNew)
                {
                    var poDetailId = (int) dgrdData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
                    // get detail information
                    _poDetail = PurchaseOrderReceiptBO.Instance.GetPurchaseOrderDetail(poDetailId);
                    // UMRate
                    if (_poDetail.StockUMID != _poDetail.BuyingUMID)
                    {
                        // get UM Rate
                        decimal decRate = Utilities.Instance.GetRate(_poDetail.BuyingUMID, _poDetail.StockUMID);
                        if (decRate > 0)
                            dgrdData[i, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decRate;
                        else
                            dgrdData[i, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decimal.MinusOne;
                    }
                    else
                        dgrdData[i, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decimal.One;
                }
            }
            _dataSource.Tables[0].Columns[PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].DefaultValue = decimal.MinusOne;
        }

        private void SwitchFormMode()
        {
            if (_formMode == EnumAction.Add)
            {
                dgrdData.AllowAddNew = true;
                dgrdData.AllowUpdate = true;
                dgrdData.AllowDelete = true;

                PostDatePicker.Enabled = true;
                CCNCombo.Enabled = true;
                ReceiveNoText.Enabled = true;
                SearchReceiveButton.Enabled = false;
                txtMasLoc.Enabled = true;
                btnSearchMasLoc.Enabled = true;

                radBySlip.Enabled = true;
                radBySlip.Enabled = true;
                radByInvoice.Enabled = true;
                radOutside.Enabled = true;
                
                txtDeliverySlip.Enabled = btnDeliverySlip.Enabled = radBySlip.Checked;
                txtInvoice.Enabled = btnInvoice.Enabled = radByInvoice.Checked;
                dgrdData.Splits[0].Locked = true;
                dgrdData.AllowDelete = true;
                btnAdd.Enabled = false;
                btnSave.Enabled = true;
                btnPrint.Enabled = false;
                btnDelete.Enabled = false;
                btnDeleteRow.Enabled = false;

                cboPurpose.Enabled = true;
            }
            else if (_formMode == EnumAction.Default)
            {
                dgrdData.AllowAddNew = false;
                dgrdData.AllowUpdate = false;
                dgrdData.AllowDelete = false;

                PostDatePicker.Enabled = false;
                CCNCombo.Enabled = false;
                ReceiveNoText.Enabled = true;
                SearchReceiveButton.Enabled = true;
                txtMasLoc.Enabled = false;
                btnSearchMasLoc.Enabled = false;
                txtOutside.Enabled = btnOutside.Enabled = txtProductionLine.Enabled = btnProductionLine.Enabled = false;
                radBySlip.Enabled = false;
                radBySlip.Enabled = false;
                radByInvoice.Enabled = false;
                radOutside.Enabled = false;
                txtDeliverySlip.Enabled = btnDeliverySlip.Enabled = (radBySlip.Checked && radBySlip.Enabled);
                txtInvoice.Enabled = btnInvoice.Enabled = (radByInvoice.Checked && radByInvoice.Enabled);
                foreach (C1DisplayColumn dcolCol in dgrdData.Splits[0].DisplayColumns)
                {
                    dcolCol.Locked = true;
                }
                btnAdd.Enabled = true;
                btnSave.Enabled = false;

                if (_receiptMaster != null && _receiptMaster.PurchaseOrderReceiptID > 0)
                {
                    btnPrint.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteRow.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDeleteRow.Enabled = false;
                }

                cboPurpose.Enabled = false;
            }

            btnBOMShortage.Enabled = false;
        }

        /// <summary>
        /// Fills the master location data.
        /// </summary>
        /// <param name="masterLocationId">The master location id.</param>
        private void FillMasterLocationData(int masterLocationId)
        {
            if (masterLocationId > 0)
            {
                _masterLocationId = masterLocationId;
                _masterLocation = Utilities.Instance.GetMasterLocation(masterLocationId);
                // fill Master location info to form
                txtMasLoc.Text = _masterLocation.Code;
            }
            else
            {
                _masterLocationId = masterLocationId;
                _masterLocation = new MST_MasterLocation();
                // fill Master location info to form
                txtMasLoc.Text = string.Empty;
            }
        }

        /// <summary>
        /// Fill Customer information to form
        /// </summary>
        /// <param name="partyId">Party ID</param>
        private void FillCustomerInfo(int partyId)
        {
            var customerInfo = Utilities.Instance.GetCustomerInfo(partyId);
            txtVendorName.Text = customerInfo.Name;
            txtVendorNo.Text = customerInfo.Code;
        }

        private void BindDataToGrid(bool pblnAddManual)
        {
            if (_dataSource != null && _dataSource.Tables.Count > 0)
            {
                // bind data
                if (pblnAddManual)
                    _dataSource.Tables[0].Clear();
                // fill data into grid
                dgrdData.DataSource = _dataSource.Tables[0];
                FormControlComponents.RestoreGridLayout(dgrdData, _gridLayOut);

                #region restore column caption and setting display style

                // unlock the grid.
                dgrdData.Splits[0].Locked = false;
                foreach (C1DisplayColumn objCol in dgrdData.Splits[0].DisplayColumns)
                    objCol.Locked = true;
                dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].Locked = false;
                dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Columns[PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Columns[PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
                dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Button = true;
                dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = true;
                dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = false;
                
                dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.QASTATUS_FLD].Locked = false;
                dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Locked = false;
                
                #endregion

                AssignDefaultValue();

                // if receipt by invoice
                if (radByInvoice.Checked)
                {
                    // lock all columns first
                    foreach (C1DisplayColumn dcolData in dgrdData.Splits[0].DisplayColumns)
                    {
                        dcolData.Button = false;
                        dcolData.Locked = true;
                    }
                    // unlock quantity
                    dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].Locked = false;
                    // unlock location
                    dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].
                        Button = true;
                    dgrdData.Splits[0].DisplayColumns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD].
                        Locked = false;
                    // unlock bin
                    dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Button = true;
                    dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked =
                        false;
                    // unlock lot
                    dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Locked = false;
                    // unlock serial
                    dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD].Locked = false;

                    // not allow user to add new line or delete a line
                    dgrdData.AllowAddNew = false;
                    dgrdData.AllowDelete = false;
                }
            }
        }

        /// <summary>
        /// Fills the master data.
        /// </summary>
        /// <param name="masterId">The master id.</param>
        private void FillMasterData(int masterId)
        {
            // get POReceiptMasterVO object
            _receiptMaster = PurchaseOrderReceiptBO.Instance.GetReceiptMaster(masterId);
            // get data source
            _dataSource = PurchaseOrderReceiptBO.Instance.ListReceiptDetailByReceiptMaster(_receiptMaster.PurchaseOrderReceiptID);
            // fill data to form
            ReceiveNoText.Text = _receiptMaster.ReceiveNo;
            PostDatePicker.Value = _receiptMaster.PostDate;
            CCNCombo.SelectedValue = _receiptMaster.CCNID;
            FillMasterLocationData(_receiptMaster.MasterLocationID);
            txtInvoice.Text = txtDeliverySlip.Text = txtOutside.Text = txtProductionLine.Text = string.Empty;
            if (_receiptMaster.Purpose != null)
                cboPurpose.SelectedIndex = (int) _receiptMaster.Purpose;

            switch (_receiptMaster.ReceiptType)
            {
                case (int)POReceiptTypeEnum.ByInvoice:
                    radByInvoice.Checked = true;
                    txtInvoice.Text = _receiptMaster.RefNo;
                    txtDeliverySlip.Text = string.Empty;
                    // get invoice master
                    _invoiceMaster = PurchaseOrderReceiptBO.Instance.GetInvoiceMaster(_receiptMaster.RefNo);
                    // get vendor info
                    _partyId = _invoiceMaster.PartyID;
                    FillCustomerInfo(_partyId);
                    break;
                case (int)POReceiptTypeEnum.ByDeliverySlip:
                    radBySlip.Checked = true;
                    txtInvoice.Text = string.Empty;
                    txtDeliverySlip.Text = _receiptMaster.RefNo;
                    _poMaster = PurchaseOrderReceiptBO.Instance.GetPurchaseOrderMaster(_receiptMaster.PurchaseOrderMasterID.GetValueOrDefault(0));
                    // get vendor info
                    _partyId = _poMaster.PartyID;
                    FillCustomerInfo(_partyId);
                    break;
                case (int)POReceiptTypeEnum.ByOutside:
                    radOutside.Checked = true;
                    txtOutside.Text = _receiptMaster.RefNo;
                    txtProductionLine.Tag = _receiptMaster.ProductionLineID;
                    if (_receiptMaster.ProductionLineID.HasValue)
                    {
                        txtProductionLine.Text = _receiptMaster.PRO_ProductionLine.Code;
                        // get vendor info
                        _partyId = _receiptMaster.PO_PurchaseOrderMaster.PartyID;
                    }
                    FillCustomerInfo(_partyId);
                    break;
            }

            // bind data grid
            BindDataToGrid(false);
            // turn form to Default mode for view only
            _formMode = EnumAction.Default;
            // switch form mode
            SwitchFormMode();
        }

        /// <summary>
        /// Radio status
        /// </summary>
        internal enum RadioStatus
        {
            /// <summary>
            /// By Slip
            /// </summary>
            BySlip = 0,
            /// <summary>
            /// By Invoice
            /// </summary>
            ByInvoice = 1,
            /// <summary>
            /// By Outside
            /// </summary>
            ByOutside = 2,
            /// <summary>
            /// No changes
            /// </summary>
            NoChange = 3
        }

        /// <summary>
        /// This method use to fill master data when select Invoice
        /// </summary>
        /// <param name="pdrowData">Data Row</param>
        private void FillInvoiceData(DataRow pdrowData)
        {
            if (pdrowData == null)
            {
                txtVendorName.Text = string.Empty;
                txtVendorNo.Text = string.Empty;
                if ((_dataSource != null) && (_dataSource.Tables.Count > 0))
                {
                    _dataSource.Tables[0].Clear();
                }
                // lock grid
                foreach (C1DisplayColumn dcolC1 in dgrdData.Splits[0].DisplayColumns)
                    dcolC1.Locked = true;
                return;
            }
            // get invoice master
            _invoiceMaster = PurchaseOrderReceiptBO.Instance.GetInvoiceMaster(pdrowData[PO_InvoiceMasterTable.INVOICENO_FLD].ToString());
            // get data source
            _dataSource = PurchaseOrderReceiptBO.Instance.ListByInvoice(_invoiceMaster.InvoiceMasterID);
            // fill data to form
            txtInvoice.Text = _invoiceMaster.InvoiceNo;
            txtInvoice.Tag = _invoiceMaster.InvoiceMasterID;
            // get vendor info
            _partyId = _invoiceMaster.PartyID;
            FillCustomerInfo(_partyId);

            // fill to data grid
            if (_dataSource.Tables.Count > 0 && _dataSource.Tables[0].Rows.Count > 0)
            {
                BindDataToGrid(false);
            }
            else
            {
                if (_dataSource != null && _dataSource.Tables.Count > 0)
                {
                    _dataSource.Tables[0].Clear();
                }
                BindDataToGrid(false);
                // lock the grid
                dgrdData.Splits[0].Locked = true;
            }
        }

        /// <summary>
        /// Fills the production line.
        /// </summary>
        /// <param name="pdrowData">The pdrow data.</param>
        private void FillProductionLine(DataRow pdrowData)
        {
            if (pdrowData == null)
            {
                txtProductionLine.Text = string.Empty;
                txtProductionLine.Tag = null;
                btnBOMShortage.Enabled = false;
            }
            else
            {
                var productionLineId = (int) pdrowData[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
                txtProductionLine.Tag = productionLineId;
                txtProductionLine.Text = pdrowData[PRO_ProductionLineTable.CODE_FLD].ToString();

                if (radOutside.Checked && _dataSource != null
                    && _pONumber != string.Empty
                    && !PostDatePicker.Text.Trim().Equals(string.Empty)
                    && !txtOutside.Text.Trim().Equals(string.Empty)
                    && dgrdData.RowCount > 0)
                {
                    btnBOMShortage.Enabled = true;
                }
                else
                {
                    btnBOMShortage.Enabled = false;
                }
            }
        }

        private void FillPOLineData(DataRow pdrowData)
        {
            dgrdData.EditActive = true;
            if (pdrowData != null)
            {
                // id value
                dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] = pdrowData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD];
                // display value
                dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD] = pdrowData[PO_PurchaseOrderDetailTable.LINE_FLD];
                // receive quantity
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] = pdrowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD];
                // UM
                dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD] = pdrowData[PO_PurchaseOrderDetailTable.STOCKUMID_FLD];
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD] = pdrowData[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD];
                if (pdrowData[PO_PurchaseOrderDetailTable.STOCKUMID_FLD].ToString() != pdrowData[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD].ToString())
                {
                    // get UM Rate
                    decimal decUMRate = Utilities.Instance.GetRate((int)pdrowData[PO_PurchaseOrderDetailTable.BUYINGUMID_FLD], (int)pdrowData[PO_PurchaseOrderDetailTable.STOCKUMID_FLD]);
                    if (decUMRate > 0)
                        dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decUMRate;
                    else
                        dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decimal.MinusOne;
                }
                else
                    dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decimal.One;
                // if Item is not controlled by Lot, lock Lot cell
                var voCurrentProduct = Utilities.Instance.GetProductInfo(int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString()));
                dgrdData.Splits[0].DisplayColumns[PO_PurchaseOrderReceiptDetailTable.LOT_FLD].Locked = !voCurrentProduct.LotControl.GetValueOrDefault(false);
                // get default location of item
                if (voCurrentProduct.LocationID.GetValueOrDefault(0) > 0)
                {
                    dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = voCurrentProduct.MST_Location.Code;
                    dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = voCurrentProduct.LocationID;
                }
                // get default bin of item
                if (voCurrentProduct.BinID.GetValueOrDefault(0) > 0)
                {
                    dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = voCurrentProduct.MST_BIN.Code;
                    dgrdData[dgrdData.Row, MST_BINTable.LOCATIONID_FLD] = voCurrentProduct.BinID;
                }
                // temporary, we need to assign DeliveryScheduleID value when user open search form
                // if Receipt by schedule
                if (radBySlip.Checked)
                    dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD] = pdrowData[PO_PurchaseOrderReceiptDetailTable.DELIVERYSCHEDULEID_FLD];
                // in case of receipt by PO, we need to fill item detail when select PO detail
                if (radBySlip.Checked)
                {
                    // code value
                    dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = voCurrentProduct.Code;
                    // description value
                    dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = voCurrentProduct.Description;
                    // revision value
                    dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = voCurrentProduct.Revision;
                    // receive quantity value
                    dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] = pdrowData[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD];
                    // id value
                    dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = voCurrentProduct.ProductID;
                }
            }
            else
            {
                // id value
                dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD] = DBNull.Value;
                // display value
                dgrdData[dgrdData.Row, PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD] = string.Empty;
                // receive quantity
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD] = string.Empty;
                // UM
                dgrdData[dgrdData.Row, MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD] = string.Empty;
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD] = DBNull.Value;
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.BUYINGUMID_FLD] = DBNull.Value;
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD] = decimal.Zero;
                // location
                dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
                dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = DBNull.Value;
                // bin
                dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
                dgrdData[dgrdData.Row, MST_BINTable.LOCATIONID_FLD] = DBNull.Value;
                // in case of receipt by PO, we need to fill item detail when select PO detail
                if (radBySlip.Checked)
                {
                    // code value
                    dgrdData[dgrdData.Row, ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD] = string.Empty;
                    // description value
                    dgrdData[dgrdData.Row, ITM_ProductTable.DESCRIPTION_FLD] = string.Empty;
                    // revision value
                    dgrdData[dgrdData.Row, ITM_ProductTable.REVISION_FLD] = string.Empty;
                    // id value
                    dgrdData[dgrdData.Row, ITM_ProductTable.PRODUCTID_FLD] = DBNull.Value;
                }
            }
        }

        private void FillLocationData(DataRow pdrowData)
        {
            dgrdData.EditActive = true;
            if (pdrowData != null)
            {
                var voLocation = Utilities.Instance.GetLocation((int)pdrowData[MST_LocationTable.LOCATIONID_FLD]);
                // display value
                dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = voLocation.Code;
                // id
                dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = voLocation.LocationID;
                // clear bin id
                dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = DBNull.Value;
                // clear bin code
                dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
                // clear Lot
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.LOT_FLD] = string.Empty;
                // clear Serial
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD] = string.Empty;
                // if select Location is not controlled by bin
                // lock Bin cell
                dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = !voLocation.Bin.GetValueOrDefault(false);
            }
            else
            {
                // display value
                dgrdData[dgrdData.Row, MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD] = string.Empty;
                // id
                dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD] = DBNull.Value;
                // clear bin id
                dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = DBNull.Value;
                // clear bin code
                dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
                // clear Lot
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.LOT_FLD] = string.Empty;
                // clear Serial
                dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD] = string.Empty;
            }
        }

        private void FillBinData(DataRow pdrowData)
        {
            dgrdData.EditActive = true;
            if (pdrowData != null)
            {
                // display value
                dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = pdrowData[MST_BINTable.CODE_FLD];
                // id
                dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = pdrowData[MST_BINTable.BINID_FLD];
            }
            else
            {
                // display value
                dgrdData[dgrdData.Row, MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD] = string.Empty;
                // id
                dgrdData[dgrdData.Row, MST_BINTable.BINID_FLD] = DBNull.Value;
            }
            // clear Lot
            dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.LOT_FLD] = string.Empty;
            // clear Serial
            dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.SERIAL_FLD] = string.Empty;
        }

        private void FillDataToGrid()
        {
            string strColumn = dgrdData.Columns[dgrdData.Col].DataField;
            string strValue = dgrdData.Columns[dgrdData.Col].Text.Trim();
            DataRowView drvData = null;
            var htbConditions = new Hashtable();
            if (dgrdData.AllowAddNew)
            switch (strColumn)
            {
                #region // select purchase order line

                case (PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD):
                    if (radBySlip.Checked)
                    {
                        htbConditions.Add(v_PurchaseOrderOfItem.PO_CODE, dgrdData[dgrdData.Row, "PO_PurchaseOrderMasterCode"].ToString());
                        if (_slipDate != DateTime.MaxValue)
                        {
                            htbConditions.Add(YearFilter, _slipDate.Year);
                            htbConditions.Add(MonthFilter, _slipDate.Month);
                            htbConditions.Add(DayFilter, _slipDate.Day);
                            htbConditions.Add(HourFilter, _slipDate.Hour);
                        }
                        drvData = FormControlComponents.OpenSearchForm(v_ReceiptBySchedule.VIEW_NAME,
                                                                       PO_PurchaseOrderDetailTable.LINE_FLD, strValue,
                                                                       htbConditions, true);
                    }
                    if (drvData != null)
                    {
                        string strExpression = PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD
                                               + Constants.EQUAL +
                                               drvData[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString();
                        if (radBySlip.Checked)
                            strExpression += Constants.AND + PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD
                                             + Constants.EQUAL +
                                             drvData[PO_DeliveryScheduleTable.DELIVERYSCHEDULEID_FLD].ToString();
                        // we need to check for exsiting PO Line and delivery line in the list
                        DataRow[] drowExisted = _dataSource.Tables[0].Select(strExpression);
                        if (drowExisted.Length.Equals(0))
                        {
                            FillPOLineData(drvData.Row);
                        }
                    }
                    else
                    {
                        if (strValue != string.Empty)
                        {
                            dgrdData.Row = dgrdData.Row;
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD]);
                            dgrdData.Select();
                            return;
                        }
                        FillPOLineData(null);
                    }
                    break;

                #endregion

                #region // select location

                case (MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD):
                    // user must select master location first
                    if (_masterLocation.MasterLocationID <= 0)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_MASTERLOCATION, MessageBoxIcon.Error);
                        txtMasLoc.Focus();
                        return;
                    }
                    htbConditions.Add(MST_LocationTable.MASTERLOCATIONID_FLD, _masterLocation.MasterLocationID);
                    drvData = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                   MST_LocationTable.CODE_FLD, strValue, htbConditions,
                                                                   true);
                    if (drvData != null)
                    {
                        FillLocationData(drvData.Row);
                    }
                    else
                    {
                        if (strValue != string.Empty)
                        {
                            dgrdData.Row = dgrdData.Row;
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                            dgrdData.Select();
                            return;
                        }
                        FillLocationData(null);
                    }
                    break;

                #endregion

                #region // select bin

                case (MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD):
                    int intLocationID = 0;
                    try
                    {
                        intLocationID = int.Parse(dgrdData[dgrdData.Row, MST_LocationTable.LOCATIONID_FLD].ToString());
                        // user must select location first
                        if (intLocationID <= 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxButtons.OK,MessageBoxIcon.Error);
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                            return;
                        }
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_LOCATION, MessageBoxButtons.OK,MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                        return;
                    }
                    var voLocation = Utilities.Instance.GetLocation(intLocationID);
                    if (voLocation.Bin.GetValueOrDefault(false))
                        dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = false;
                    else
                    {
                        dgrdData.Splits[0].DisplayColumns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD].Locked = true;
                        return;
                    }
                    string strWhereClause = MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + "=" + intLocationID;
                    if (radOutside.Checked)
                    {
                        strWhereClause += " AND (" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "=" +
                                          (int)BinTypeEnum.OK + " OR " + MST_BINTable.TABLE_NAME + ".BinTypeID = " + (int)BinTypeEnum.NG + ")";
                    }
                    drvData = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD,
                                                                   strValue, strWhereClause);
                    if (drvData != null)
                    {
                        FillBinData(drvData.Row);
                    }
                    else
                    {
                        if (strValue != string.Empty)
                        {
                            dgrdData.Row = dgrdData.Row;
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
                            dgrdData.Select();
                            return;
                        }
                        FillBinData(null);
                    }
                    break;

                #endregion
            }
        }

        /// <summary>
        /// This method uses to check all madatory fields in form.
        /// </summary>
        /// <returns>True if succeed. False if failure</returns>
        private bool CheckMandatoryFields()
        {
            // check mandatory field
            if (FormControlComponents.CheckMandatory(CCNCombo))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                CCNCombo.Focus();
                return false;
            }
            if (PostDatePicker.ValueIsDbNull)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                PostDatePicker.Focus();
                return false;
            }

            if (!FormControlComponents.CheckDateInCurrentPeriod((DateTime)PostDatePicker.Value))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PKL_TRANSDATE_PERIOD, MessageBoxButtons.OK, MessageBoxIcon.Error);
                PostDatePicker.Focus();
                return false;
            }

            DateTime dtmDBDate = Utilities.Instance.GetServerDate();
            if (dtmDBDate < (DateTime)PostDatePicker.Value)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                PostDatePicker.Focus();
                return false;
            }

            if (FormControlComponents.CheckMandatory(ReceiveNoText))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReceiveNoText.Focus();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtMasLoc))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMasLoc.Focus();
                return false;
            }
            // Outside
            if (txtOutside.Text != string.Empty)
                if (FormControlComponents.CheckMandatory(txtProductionLine))
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductionLine.Focus();
                    return false;
                }
            //Check postdate in configuration
            if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)PostDatePicker.Value))
            {
                return false;
            }
            _ccnId = (int)CCNCombo.SelectedValue;

            if (dgrdData.RowCount <= 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_PORECEIPT_INPUT_DETAIL, MessageBoxIcon.Error);
                return false;
            }

            if (_formMode != EnumAction.Edit)
            {
                var dtmSelectedDate = (DateTime)(PostDatePicker.Value);
                dtmSelectedDate = new DateTime(dtmSelectedDate.Year, dtmSelectedDate.Month, dtmSelectedDate.Day,
                                               dtmSelectedDate.Hour, dtmSelectedDate.Minute, 0);
                for (int i = 0; i < dgrdData.RowCount; i++)
                {
                    dgrdData.Row = i;
                    decimal decReceiveQuantity = 0;
                    int intLocationID = 0;
                    int intPOLineID = 0;

                    #region Purchase Order Master

                    // purchase order master
                    try
                    {
                        if (int.Parse(dgrdData[i, PO_PurchaseOrderMasterTable.PURCHASEORDERMASTERID_FLD].ToString()) <= 0)
                            throw new Exception();
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region Purchase Order Line

                    // purchase order line
                    try
                    {
                        intPOLineID = int.Parse(dgrdData[i, PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString());
                        if (intPOLineID <= 0)
                            throw new Exception();
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderDetailTable.TABLE_NAME + PO_PurchaseOrderDetailTable.LINE_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region Postdate must greater than Approved Date

                    _poDetail = PurchaseOrderReceiptBO.Instance.GetPurchaseOrderDetail(intPOLineID);
                    if (_poDetail.ApprovalDate != null)
                    {
                        var approvalDate = (DateTime) _poDetail.ApprovalDate;
                        _poDetail.ApprovalDate = new DateTime(approvalDate.Year, approvalDate.Month,
                                                              approvalDate.Day, approvalDate.Hour,
                                                              approvalDate.Minute, 0);
                    }
                    if (dtmSelectedDate < _poDetail.ApprovalDate)
                    {
                        // Cannot receive this line because of Post Date < ApprovalDate
                        PCSMessageBox.Show(ErrorCode.MESSAGE_POST_DATE_LESS_THAN_APPROVAL_DATE, MessageBoxButtons.OK,MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderMasterTable.TABLE_NAME + PO_PurchaseOrderMasterTable.CODE_FLD]);
                        dgrdData.Select();
                        PostDatePicker.Focus();
                        return false;
                    }

                    #endregion

                    #region Product

                    // product
                    try
                    {
                        if (int.Parse(dgrdData[i, ITM_ProductTable.PRODUCTID_FLD].ToString()) <= 0)
                            throw new Exception();
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[ITM_ProductTable.TABLE_NAME + ITM_ProductTable.CODE_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region Stock UM

                    // stock um
                    try
                    {
                        if (int.Parse(dgrdData[i, PO_PurchaseOrderReceiptDetailTable.STOCKUMID_FLD].ToString()) <= 0)
                            throw new Exception();
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region check UM rate

                    try
                    {
                        decimal decUMRate = 0;
                        try
                        {
                            decUMRate = decimal.Parse(dgrdData[i, PO_PurchaseOrderReceiptDetailTable.UMRATE_FLD].ToString());
                        }
                        catch
                        {
                        }
                        if ((decUMRate <= 0) && (_poDetail.BuyingUMID != _poDetail.StockUMID))
                        {
                            decUMRate = Utilities.Instance.GetRate(_poDetail.BuyingUMID, _poDetail.StockUMID);
                            if (decUMRate <= 0)
                            {
                                PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SET_UMRATE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dgrdData.Select();
                                return false;
                            }
                        }
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region Location

                    // location
                    try
                    {
                        intLocationID = int.Parse(dgrdData[i, MST_LocationTable.LOCATIONID_FLD].ToString());
                        if (intLocationID <= 0)
                            throw new Exception();
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region Selected Location must be in selected Master Location

                    var voLocation = Utilities.Instance.GetLocation(intLocationID);
                    if (voLocation.MasterLocationID != _masterLocation.MasterLocationID)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_LOCATION_NOT_MATCH_WITH_MASLOC, MessageBoxButtons.OK,MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_LocationTable.TABLE_NAME + MST_LocationTable.CODE_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion

                    #region Check Bin if Location is Bin controlled or not

                    int intBinID = 0;
                    if (intLocationID > 0)
                    {
                        try
                        {
                            intBinID = int.Parse(dgrdData[i, MST_BINTable.BINID_FLD].ToString());
                        }
                        catch
                        {
                        }
                        if (voLocation.Bin.GetValueOrDefault(false))
                        {
                            // if Location is Bin controlled but user not select Bin yet
                            if (intBinID <= 0)
                            {
                                PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_SELECT_BIN_FOR_LOCATION, MessageBoxButtons.OK,MessageBoxIcon.Error);
                                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
                                dgrdData.Select();
                                return false;
                            }
                        }
                        else
                        {
                            // if Location is not Bin controlled but user already select Bin
                            if (intBinID > 0)
                            {
                                PCSMessageBox.Show(ErrorCode.MESSAGE_MUST_CANNOT_SELECT_BIN_FOR_LOCATION,MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[MST_BINTable.TABLE_NAME + MST_BINTable.CODE_FLD]);
                                dgrdData.Select();
                                return false;
                            }
                        }
                    }

                    #endregion

                    #region Receive Quantity

                    // receive quantity
                    try
                    {
                        decReceiveQuantity = decimal.Parse(dgrdData[i, PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD].ToString());
                        if (decReceiveQuantity <= 0)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_RECEIVEQTYTOZERO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
                            dgrdData.Select();
                            return false;
                        }
                        // get current receive quantity from database
                        decimal decCurrentReceiveQuantity = 0;
                        decimal decOrderQuantity = 0;
                        try
                        {
                            decCurrentReceiveQuantity = decimal.Parse(dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.RECEIVEDQUANTITY_FLD].ToString());
                        }
                        catch
                        {
                        }
                        try
                        {
                            decOrderQuantity = decimal.Parse(dgrdData[dgrdData.Row, PO_DeliveryScheduleTable.DELIVERYQUANTITY_FLD].ToString());
                        }
                        catch
                        {
                        }
                        // if sum of current receive quantity from database and receive quantity input by user
                        // greater than total delivery from po detail then warn user
                        if (decOrderQuantity < (decCurrentReceiveQuantity + decReceiveQuantity)
                            || decOrderQuantity < decReceiveQuantity)
                        {
                            DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_RECEIVE_GREATER_THAN_TOTAL,
                                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                                                        MessageBoxDefaultButton.Button1);
                            if (dlgResult == DialogResult.No)
                            {
                                dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
                                dgrdData.Select();
                                return false;
                            }
                        }
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgrdData.Col = dgrdData.Columns.IndexOf(dgrdData.Columns[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD]);
                        dgrdData.Select();
                        return false;
                    }

                    #endregion
                }
            }
            return true;
        }

        private bool SaveData()
        {
            // prepare data for Master Receipt
            var dtmDateOnly = (DateTime)PostDatePicker.Value;
            dtmDateOnly = new DateTime(dtmDateOnly.Year, dtmDateOnly.Month, dtmDateOnly.Day, dtmDateOnly.Hour,
                                       dtmDateOnly.Minute, 0);
            _receiptMaster.PostDate = dtmDateOnly;
            _receiptMaster.MasterLocationID = _masterLocationId;
            _receiptMaster.CCNID = int.Parse(CCNCombo.SelectedValue.ToString());
            _receiptMaster.UserName = SystemProperty.UserName;
            _receiptMaster.LastChange = Utilities.Instance.GetServerDate();
            _receiptMaster.Purpose = cboPurpose.SelectedIndex;

            // END: Trada 28-12-2005
            if (radByInvoice.Checked)
            {
                _receiptMaster.ReceiptType = (int)POReceiptTypeEnum.ByInvoice;
                _receiptMaster.RefNo = txtInvoice.Text.Trim();
                _receiptMaster.InvoiceMasterID = Convert.ToInt32(txtInvoice.Tag);
            }
            else if (radBySlip.Checked)
            {
                _receiptMaster.ReceiptType = (int)POReceiptTypeEnum.ByDeliverySlip;
                _receiptMaster.RefNo = txtDeliverySlip.Text.Trim();
                _receiptMaster.InvoiceMasterID = null;
            }
            else if (radOutside.Checked)
            {
                _receiptMaster.ReceiptType = (int)POReceiptTypeEnum.ByOutside;
                _receiptMaster.RefNo = txtOutside.Text.Trim();
                _receiptMaster.InvoiceMasterID = null;
                _receiptMaster.ProductionLineID = Convert.ToInt32(txtProductionLine.Tag);
                var locationBin = PurchaseOrderReceiptBO.Instance.GetLocationAndBin(null, (int)_receiptMaster.ProductionLineID);
                if (locationBin.Length == 0)
                {
                    // Show message: You have to issue material to Outside before receipt finished goods.
                    PCSMessageBox.Show(ErrorCode.MESSAGE_ISSUE_MATERIAL_TO_OUTSIDE, MessageBoxIcon.Error);
                    btnBOMShortage.Focus();
                    return false;
                }

                int locationId = locationBin[0];
                int binId = locationBin[1];
                var cacheData = PurchaseOrderReceiptBO.Instance.GetBinCacheData(locationId, binId);
                
                if ((from DataRow drow in _dataSource.Tables[0].Rows
                     where drow.RowState != DataRowState.Deleted
                     let bomList = PurchaseOrderReceiptBO.Instance.GetBOM(null, (int)drow[ITM_ProductTable.PRODUCTID_FLD], true)
                     from drowBom in bomList
                     let availableQuantity = GetAvailableQuantity(cacheData, locationId, binId, drowBom.ComponentID)
                     let decBOMQty = drowBom.Quantity.GetValueOrDefault(0)
                     let decOrderQty = Convert.ToDecimal(drow[PO_PurchaseOrderReceiptDetailTable.RECEIVEQUANTITY_FLD])
                     where availableQuantity < decOrderQty*decBOMQty
                     select availableQuantity).Any())
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_ISSUE_MATERIAL_TO_OUTSIDE, MessageBoxIcon.Exclamation);
                    btnBOMShortage.Focus();
                    return false;
                }
            }
            if (_poMaster != null && _poMaster.PurchaseOrderMasterID > 0)
                _receiptMaster.PurchaseOrderMasterID = _poMaster.PurchaseOrderMasterID;
            if (_receiptMaster.PurchaseOrderMasterID == 0)
                _receiptMaster.PurchaseOrderMasterID = null;
            _receiptMaster.ReceiveNo = ReceiveNoText.Text.Trim();
            // synchronyze data
            FormControlComponents.SynchronyGridData(dgrdData);
            if (_formMode == EnumAction.Add)
                _receiptMaster.PurchaseOrderReceiptID = PurchaseOrderReceiptBO.Instance.AddMasterReceipt(_receiptMaster, _dataSource, _ccnId, _currentDate);

            return true;
        }

        private static decimal GetAvailableQuantity(List<IV_BinCache> cacheData, int locationId, int binId, int componentId)
        {
            return cacheData.Where(b => b.LocationID == locationId && b.BinID == binId && b.ProductID == componentId).
                Sum(b => b.OHQuantity.GetValueOrDefault(0) - b.CommitQuantity.GetValueOrDefault(0));
        }

        private int GetCurrentRow()
        {
            int intPurchaseOrderReceiptID = 0;
            try
            {
                int intProductID = (int)dgrdData[dgrdData.Row, PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD];
                string strCondition = PO_PurchaseOrderDetailTable.PRODUCTID_FLD + "=" + intProductID;
                DataRow[] arrMatchedRows = _dataSource.Tables[0].Select(strCondition);
                if (arrMatchedRows.Length != 0)
                    intPurchaseOrderReceiptID = (int)arrMatchedRows[0][PO_PurchaseOrderReceiptDetailTable.PURCHASEORDERRECEIPTDETAILID_FLD];
            }
            catch
            {
                PCSMessageBox.Show(ErrorCode.MSG_VIEWTABLE_SELECT_ROW, MessageBoxIcon.Information);
            }
            return intPurchaseOrderReceiptID;
        }

        #endregion

        #region PO Slip Report & BOM Shortate Report: Tuan TQ.

        /// <summary>
        /// Build and show PO Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 10 Apr, 2006</Author>
        private void ShowPOSlipReport()
        {
            const string methodName = This + ".ShowPOSlipReport()";
            try
            {
                const string APPLICATION_PATH = @"PCSMain\bin\Debug";

                const string REPORT_LAYOUT = "PurchasingReceiptSlip.xml";
                const string REPORT_COMPANY_FLD = "fldCompany";
                const string RPT_TITLE_FLD = "fldTitle";
                const string RPT_INVOICE_NO_FLD = "fldInvoiceNo";

                Cursor = Cursors.WaitCursor;
                var printPreview = new C1PrintPreviewDialog();
                var boReport = new C1PrintPreviewDialogBO();

                DataTable dtbResult = boReport.GetPOSlipData(_receiptMaster.PurchaseOrderReceiptID);

                // Check data source
                if (dtbResult == null)
                {
                    Cursor = Cursors.Default;
                    return;
                }

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
                    strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
                }

                //Set datasource and lay-out path for reports
                reportBuilder.SourceDataTable = dtbResult;
                reportBuilder.ReportDefinitionFolder = strReportPath;

                reportBuilder.ReportLayoutFile = REPORT_LAYOUT;

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // And show it in preview dialog				
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params
                reportBuilder.DrawPredefinedField(REPORT_COMPANY_FLD,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));

                //Hide invoice caption if no invoice
                if (dtbResult.Rows.Count > 0)
                {
                    reportBuilder.Report.Fields[RPT_INVOICE_NO_FLD].Visible =
                        (dtbResult.Rows[0][PO_InvoiceMasterTable.INVOICENO_FLD].ToString().Trim() != string.Empty);
                }

                reportBuilder.RefreshReport();

                //Print report
                try
                {
                    printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD).Text;
                }
                catch
                {
                }

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
                Cursor = Cursors.Default;
            }
        }

        private DataTable GetBOMShortageData()
        {
            const string RECEIVE_QUANTITY_FLD = "ReceiveQuantity";

            int intProductionLineID = int.Parse(txtProductionLine.Tag.ToString());
            var boDataReport = new C1PrintPreviewDialogBO();

            // build the list of item in the grid
            var sbItemList = new StringBuilder();
            string strLastID = string.Empty;
            foreach (DataRow drowData in _dataSource.Tables[0].Rows)
            {
                string strProductID = drowData[PO_PurchaseOrderReceiptDetailTable.PRODUCTID_FLD].ToString();
                if (strLastID == strProductID)
                    continue;
                strLastID = strProductID;
                sbItemList.Append(strProductID).Append(",");
            }
            sbItemList.Append("0"); // avoid exception
            var dtbResult = boDataReport.GetPOBOMShortageData(_currentDate, intProductionLineID, _pONumber, sbItemList.ToString());

            if (dtbResult == null)
            {
                return null;
            }

            //Loop and fill receive quantity
            foreach (DataRow drow in dtbResult.Rows)
            {
                //Find in data source (data in grid)
                string strCondition = PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD + "=" +
                                      drow[PO_PurchaseOrderDetailTable.PURCHASEORDERDETAILID_FLD].ToString();
                DataRow[] arrMatchedRows = _dataSource.Tables[0].Select(strCondition);
                if (arrMatchedRows.Length != 0)
                {
                    drow[RECEIVE_QUANTITY_FLD] = arrMatchedRows[0][RECEIVE_QUANTITY_FLD];
                }
            }

            return dtbResult;
        }

        /// <summary>
        /// Build and show PO BOM Shortage Report
        /// </summary>
        /// <Author> Tuan TQ, 06 Apr, 2006</Author>
        private void ShowBOMShortageReport()
        {
            const string APPLICATION_PATH = @"PCSMain\bin\Debug";

            const string REPORT_LAYOUT = "POBOMShortageReport.xml";
            const string RPT_PAGE_HEADER = "PageHeader";
            const string REPORT_COMPANY_FLD = "fldCompany";
            const string RPT_TITLE_FLD = "fldTitle";
            const string RPT_PO_NO_FLD = "P0 No.";

            var printPreview = new C1PrintPreviewDialog();

            DataTable dtbResult = GetBOMShortageData();

            // Check data source
            if (dtbResult == null)
            {
                Cursor = Cursors.Default;
                return;
            }

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
                strReportPath += @"\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
            }

            //Set datasource and lay-out path for reports
            reportBuilder.SourceDataTable = dtbResult;
            reportBuilder.ReportDefinitionFolder = strReportPath;

            reportBuilder.ReportLayoutFile = REPORT_LAYOUT;

            //check if layout is valid
            if (reportBuilder.AnalyseLayoutFile())
            {
                reportBuilder.UseLayoutFile = true;
            }
            else
            {
                Cursor = Cursors.Default;
                PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                return;
            }

            reportBuilder.MakeDataTableForRender();

            // And show it in preview dialog				
            reportBuilder.ReportViewer = printPreview.ReportViewer;
            reportBuilder.RenderReport();

            //Header information get from system params
            reportBuilder.DrawPredefinedField(REPORT_COMPANY_FLD,
                                              SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));

            //Draw parameters				
            var arrParamAndValue = new NameValueCollection();
            arrParamAndValue.Add(RPT_PO_NO_FLD, _pONumber);
            arrParamAndValue.Add(lblProductionLine.Text, txtProductionLine.Text);
            arrParamAndValue.Add(lblVenderNo.Text, txtVendorNo.Text + " ( " + txtVendorName.Text + ")");

            //Anchor the Parameter drawing canvas cordinate to the fldTitle
            Field fldTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD);
            double dblStartX = fldTitle.Left;
            double dblStartY = fldTitle.Top + 1.3 * fldTitle.RenderHeight;
            reportBuilder.GetSectionByName(RPT_PAGE_HEADER).CanGrow = true;
            reportBuilder.DrawParameters(reportBuilder.GetSectionByName(RPT_PAGE_HEADER), dblStartX, dblStartY,
                                         arrParamAndValue, reportBuilder.Report.Font.Size);


            reportBuilder.RefreshReport();

            //Print report
            try
            {
                printPreview.FormTitle = reportBuilder.GetFieldByName(RPT_TITLE_FLD).Text;
            }
            catch
            {
            }

            printPreview.Show();
        }

        #endregion
    }
}
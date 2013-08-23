using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1Input;
using PCSComProcurement.Purchase.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProcurement.Purchase
{
    /// <summary>
    /// Summary description for DeletePurchaseOrder.
    /// </summary>
    public class DeletePurchaseOrder : Form
    {
        private const string This = "PCSProcurement.Purchase.DeletePurchaseOrder";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components;

        private Button btnClose;
        private Button btnDelete;
        private Button btnHelp;
        private Button btnPartName;
        private Button btnPartNumber;
        private Button btnSource;
        private Button btnVendorCode;
        private Button btnVendorName;

        private C1DateEdit dtmFromDate;
        private C1DateEdit dtmToDate;
        private Label lblFromStartDate;
        private Label lblModel;
        private Label lblPartName;
        private Label lblPartNumber;
        private Label lblSource;
        private Label lblToStartDate;
        private Label lblVendorCode;
        private Label lblVendorName;
        private TextBox txtModel;
        private TextBox txtPartName;
        private TextBox txtPartNumber;
        private TextBox txtSource;
        private TextBox txtVendorCode;
        private TextBox txtVendorName;

        public DeletePurchaseOrder()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void DeletePurchaseOrder_Load(object sender, EventArgs e)
        {
            const string methodName = This + ".DeletePurchaseOrder_Load()";
            try
            {
                //Set authorization for user

                var objSecurity = new Security();
                Name = This;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    Close();
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    return;
                }
                txtSource.Tag = string.Empty;
                txtVendorCode.Tag = string.Empty;
                txtPartNumber.Tag = string.Empty;
                dtmFromDate.FormatType = FormatTypeEnum.CustomFormat;
                dtmFromDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
                dtmToDate.FormatType = FormatTypeEnum.CustomFormat;
                dtmToDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
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

        private void txtSource_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtSource_Validating()";
            try
            {
                if (!txtSource.Modified) return;
                if (txtSource.Text.Trim() == string.Empty)
                {
                    txtSource.Tag = string.Empty;
                    return;
                }
                DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseTypeTable.TABLE_NAME,
                                                                             PO_PurchaseTypeTable.CODE_FLD,
                                                                             txtSource.Text.Trim(), null, false);
                if (drvResult != null)
                {
                    txtSource.Text = drvResult[PO_PurchaseTypeTable.CODE_FLD].ToString();
                    txtSource.Tag = drvResult[PO_PurchaseTypeTable.PURCHASETYPEID_FLD];
                }
                else
                    e.Cancel = true;
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

        private void txtSource_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".txtSource_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnSource_Click(null, null);
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

        private void btnSource_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnSource_Click()";
            try
            {
                DataRowView drvResult = FormControlComponents.OpenSearchForm(PO_PurchaseTypeTable.TABLE_NAME,
                                                                             PO_PurchaseTypeTable.CODE_FLD,
                                                                             txtSource.Text.Trim(), null, true);
                if (drvResult != null)
                {
                    txtSource.Text = drvResult[PO_PurchaseTypeTable.CODE_FLD].ToString();
                    txtSource.Tag = drvResult[PO_PurchaseTypeTable.PURCHASETYPEID_FLD];
                }
                else
                    txtSource.Focus();
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

        private void txtVendorCode_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtVendorCode_Validating()";
            try
            {
                if (!txtVendorCode.Modified) return;
                if (txtVendorCode.Text.Trim() == string.Empty)
                {
                    txtVendorCode.Tag = string.Empty;
                    txtVendorName.Text = string.Empty;
                    return;
                }
                string strFilter = MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.TYPE_FLD + " <> " +
                                   (int) PartyTypeEnum.CUSTOMER;
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_PartyTable.TABLE_NAME,
                                                                                            MST_PartyTable.CODE_FLD,
                                                                                            txtVendorCode.Text,
                                                                                            strFilter, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[MST_PartyTable.PARTYID_FLD].ToString()).Append(",");
                    txtVendorCode.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.CODE_FLD].ToString();
                    txtVendorCode.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtVendorName.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.NAME_FLD].ToString();
                }
                else
                    e.Cancel = true;
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

        private void txtVendorCode_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".txtPartNumber_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnPartNumber_Click(null, null);
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

        private void btnVendorCode_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnPartNumber_Click()";
            try
            {
                string strFilter = MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.TYPE_FLD + " <> " +
                                   (int) PartyTypeEnum.CUSTOMER;
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_PartyTable.TABLE_NAME,
                                                                                            MST_PartyTable.CODE_FLD,
                                                                                            txtVendorCode.Text,
                                                                                            strFilter, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[MST_PartyTable.PARTYID_FLD].ToString()).Append(",");
                    txtVendorCode.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.CODE_FLD].ToString();
                    txtVendorCode.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtVendorName.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.NAME_FLD].ToString();
                }
                else
                    txtVendorCode.Focus();
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

        private void txtVendorName_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtVendorName_Validating()";
            try
            {
                if (!txtVendorName.Modified) return;
                if (txtVendorName.Text.Trim() == string.Empty)
                {
                    txtVendorCode.Tag = string.Empty;
                    txtVendorName.Text = string.Empty;
                    return;
                }
                string strFilter = MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.TYPE_FLD + " <> " +
                                   (int) PartyTypeEnum.CUSTOMER;
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_PartyTable.TABLE_NAME,
                                                                                            MST_PartyTable.NAME_FLD,
                                                                                            txtVendorName.Text,
                                                                                            strFilter, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[MST_PartyTable.PARTYID_FLD].ToString()).Append(",");
                    txtVendorCode.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.CODE_FLD].ToString();
                    txtVendorCode.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtVendorName.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.NAME_FLD].ToString();
                }
                else
                    e.Cancel = true;
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

        private void txtVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".txtVendorName_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnVendorName_Click(null, null);
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

        private void btnVendorName_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnPartName_Click()";
            try
            {
                string strFilter = MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.TYPE_FLD + " <> " +
                                   (int) PartyTypeEnum.CUSTOMER;
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(MST_PartyTable.TABLE_NAME,
                                                                                            MST_PartyTable.NAME_FLD,
                                                                                            txtVendorName.Text,
                                                                                            strFilter, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[MST_PartyTable.PARTYID_FLD].ToString()).Append(",");
                    txtVendorCode.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.CODE_FLD].ToString();
                    txtVendorCode.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtVendorName.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][MST_PartyTable.NAME_FLD].ToString();
                }
                else
                    txtVendorName.Focus();
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

        private void txtPartNumber_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtPartNumber_Validating()";
            try
            {
                if (!txtPartNumber.Modified) return;
                if (txtPartNumber.Text.Trim() == string.Empty)
                {
                    txtPartNumber.Tag = string.Empty;
                    txtPartName.Text = string.Empty;
                    return;
                }
                string strFilter = string.Empty;

                if (txtVendorCode.Tag != null && txtVendorCode.Tag.ToString() != string.Empty)
                    strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" +
                                txtSource.Tag + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(
                    ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1)
                                           ? "Multi Selection"
                                           : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
                }
                else
                    e.Cancel = true;
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

        private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".txtPartNumber_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnPartNumber_Click(null, null);
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

        private void btnPartNumber_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnPartNumber_Click()";
            try
            {
                string strFilter = string.Empty;

                if (txtVendorCode.Tag != null && txtVendorCode.Tag.ToString() != string.Empty)
                    strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" +
                                txtSource.Tag + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(
                    ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1)
                                           ? "Multi Selection"
                                           : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
                }
                else
                    txtPartNumber.Focus();
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

        private void txtPartName_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".txtPartName_Validating()";
            try
            {
                if (!txtPartName.Modified) return;
                if (txtPartName.Text.Trim() == string.Empty)
                {
                    txtPartNumber.Text = string.Empty;
                    txtPartNumber.Tag = null;
                    txtPartName.Text = string.Empty;
                    return;
                }
                string strFilter = string.Empty;

                if (txtVendorCode.Tag != null && txtVendorCode.Tag.ToString() != string.Empty)
                    strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" +
                                txtSource.Tag + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(
                    ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, false);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1)
                                           ? "Multi Selection"
                                           : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
                }
                else
                    e.Cancel = true;
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

        private void txtPartName_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".txtPartName_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4)
                    btnPartName_Click(null, null);
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

        private void btnPartName_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnPartName_Click()";
            try
            {
                string strFilter = string.Empty;

                if (txtVendorCode.Tag != null && txtVendorCode.Tag.ToString() != string.Empty)
                    strFilter = ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.PRIMARYVENDORID_FLD + " IN (" +
                                txtSource.Tag + ")";
                DataTable dtbData = FormControlComponents.OpenSearchFormForMultiSelectedRow(
                    ITM_ProductTable.TABLE_NAME, ITM_ProductTable.CODE_FLD, txtPartNumber.Text, strFilter, true);
                if (dtbData != null && dtbData.Rows.Count > 0)
                {
                    var sbID = new StringBuilder();
                    foreach (DataRow drowData in dtbData.Rows)
                        sbID.Append(drowData[ITM_ProductTable.PRODUCTID_FLD].ToString()).Append(",");
                    txtPartNumber.Text = (dtbData.Rows.Count > 1)
                                             ? "Multi Selection"
                                             : dtbData.Rows[0][ITM_ProductTable.CODE_FLD].ToString();
                    txtPartNumber.Tag = sbID.ToString(0, sbID.Length - 1);
                    txtPartName.Text = (dtbData.Rows.Count > 1)
                                           ? "Multi Selection"
                                           : dtbData.Rows[0][ITM_ProductTable.DESCRIPTION_FLD].ToString();
                }
                else
                    txtPartName.Focus();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".btnDelete_Click()";
            try
            {
                // confirm action
                DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD,
                                                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button1);
                if (dlgResult == DialogResult.Yes)
                {
                    DateTime dtmStartDate = DateTime.MinValue, dtmEndDate = DateTime.MinValue;
                    try
                    {
                        dtmStartDate = (DateTime) dtmFromDate.Value;
                    }
                    catch
                    {
                    }
                    try
                    {
                        dtmEndDate = (DateTime) dtmToDate.Value;
                    }
                    catch
                    {
                    }
                    int pintPOType = 0;
                    try
                    {
                        pintPOType = Convert.ToInt32(txtSource.Tag);
                    }
                    catch
                    {
                    }
                    string strVendor = txtVendorCode.Tag.ToString();
                    string strItem = txtPartNumber.Tag.ToString();
                    var boPurchaseOrder = new PurchaseOrderBO();
                    boPurchaseOrder.DeleteEstimatePO(dtmStartDate, dtmEndDate, pintPOType, strVendor, strItem);
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(
                    ex.mCode == ErrorCode.CASCADE_DELETE_PREVENT ? ErrorCode.CASCADE_DELETE_PREVENT : ex.mCode,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtmToDate = new C1.Win.C1Input.C1DateEdit();
            this.dtmFromDate = new C1.Win.C1Input.C1DateEdit();
            this.lblToStartDate = new System.Windows.Forms.Label();
            this.lblFromStartDate = new System.Windows.Forms.Label();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.btnPartName = new System.Windows.Forms.Button();
            this.lblPartName = new System.Windows.Forms.Label();
            this.btnPartNumber = new System.Windows.Forms.Button();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnSource = new System.Windows.Forms.Button();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.txtVendorCode = new System.Windows.Forms.TextBox();
            this.btnVendorName = new System.Windows.Forms.Button();
            this.lblVendorName = new System.Windows.Forms.Label();
            this.btnVendorCode = new System.Windows.Forms.Button();
            this.lblVendorCode = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.dtmToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dtmFromDate)).BeginInit();
            this.SuspendLayout();
            // 
            // dtmToDate
            // 
            // 
            // dtmToDate.Calendar
            // 
            this.dtmToDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmToDate.CustomFormat = "dd-MM-yyyy";
            this.dtmToDate.EmptyAsNull = true;
            this.dtmToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmToDate.Location = new System.Drawing.Point(294, 4);
            this.dtmToDate.Name = "dtmToDate";
            this.dtmToDate.Size = new System.Drawing.Size(122, 20);
            this.dtmToDate.TabIndex = 3;
            this.dtmToDate.Tag = null;
            this.dtmToDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // dtmFromDate
            // 
            // 
            // dtmFromDate.Calendar
            // 
            this.dtmFromDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtmFromDate.EmptyAsNull = true;
            this.dtmFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmFromDate.Location = new System.Drawing.Point(86, 4);
            this.dtmFromDate.Name = "dtmFromDate";
            this.dtmFromDate.Size = new System.Drawing.Size(122, 20);
            this.dtmFromDate.TabIndex = 1;
            this.dtmFromDate.Tag = null;
            this.dtmFromDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblToStartDate
            // 
            this.lblToStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblToStartDate.Location = new System.Drawing.Point(210, 4);
            this.lblToStartDate.Name = "lblToStartDate";
            this.lblToStartDate.Size = new System.Drawing.Size(82, 20);
            this.lblToStartDate.TabIndex = 2;
            this.lblToStartDate.Text = "To Date";
            this.lblToStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFromStartDate
            // 
            this.lblFromStartDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFromStartDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFromStartDate.Location = new System.Drawing.Point(2, 4);
            this.lblFromStartDate.Name = "lblFromStartDate";
            this.lblFromStartDate.Size = new System.Drawing.Size(82, 20);
            this.lblFromStartDate.TabIndex = 0;
            this.lblFromStartDate.Text = "From Date";
            this.lblFromStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPartName
            // 
            this.txtPartName.Location = new System.Drawing.Point(86, 114);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(302, 20);
            this.txtPartName.TabIndex = 19;
            this.txtPartName.Text = "";
            this.txtPartName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartName_KeyDown);
            this.txtPartName.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartName_Validating);
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(294, 92);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(122, 20);
            this.txtModel.TabIndex = 17;
            this.txtModel.Text = "";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(86, 92);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(122, 20);
            this.txtPartNumber.TabIndex = 14;
            this.txtPartNumber.Text = "";
            this.txtPartNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNumber_KeyDown);
            this.txtPartNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtPartNumber_Validating);
            // 
            // btnPartName
            // 
            this.btnPartName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPartName.Location = new System.Drawing.Point(390, 114);
            this.btnPartName.Name = "btnPartName";
            this.btnPartName.Size = new System.Drawing.Size(24, 20);
            this.btnPartName.TabIndex = 20;
            this.btnPartName.Text = "...";
            this.btnPartName.Click += new System.EventHandler(this.btnPartName_Click);
            // 
            // lblPartName
            // 
            this.lblPartName.ForeColor = System.Drawing.Color.Black;
            this.lblPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPartName.Location = new System.Drawing.Point(2, 114);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(82, 20);
            this.lblPartName.TabIndex = 18;
            this.lblPartName.Text = "Part Name";
            this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPartNumber
            // 
            this.btnPartNumber.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPartNumber.Location = new System.Drawing.Point(210, 92);
            this.btnPartNumber.Name = "btnPartNumber";
            this.btnPartNumber.Size = new System.Drawing.Size(24, 20);
            this.btnPartNumber.TabIndex = 15;
            this.btnPartNumber.Text = "...";
            this.btnPartNumber.Click += new System.EventHandler(this.btnPartNumber_Click);
            // 
            // lblPartNumber
            // 
            this.lblPartNumber.ForeColor = System.Drawing.Color.Black;
            this.lblPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPartNumber.Location = new System.Drawing.Point(2, 92);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(82, 20);
            this.lblPartNumber.TabIndex = 13;
            this.lblPartNumber.Text = "Part Number";
            this.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModel
            // 
            this.lblModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblModel.Location = new System.Drawing.Point(236, 92);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(56, 20);
            this.lblModel.TabIndex = 16;
            this.lblModel.Text = "Model";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSource
            // 
            this.lblSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSource.Location = new System.Drawing.Point(2, 26);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(82, 20);
            this.lblSource.TabIndex = 4;
            this.lblSource.Text = "Purchase Type";
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(86, 26);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(122, 20);
            this.txtSource.TabIndex = 5;
            this.txtSource.Text = "";
            this.txtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSource_KeyDown);
            this.txtSource.Validating += new System.ComponentModel.CancelEventHandler(this.txtSource_Validating);
            // 
            // btnSource
            // 
            this.btnSource.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSource.Location = new System.Drawing.Point(210, 26);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(24, 20);
            this.btnSource.TabIndex = 6;
            this.btnSource.Text = "...";
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // txtVendorName
            // 
            this.txtVendorName.Location = new System.Drawing.Point(86, 70);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(302, 20);
            this.txtVendorName.TabIndex = 11;
            this.txtVendorName.Text = "";
            this.txtVendorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorName_KeyDown);
            this.txtVendorName.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorName_Validating);
            // 
            // txtVendorCode
            // 
            this.txtVendorCode.Location = new System.Drawing.Point(86, 48);
            this.txtVendorCode.Name = "txtVendorCode";
            this.txtVendorCode.Size = new System.Drawing.Size(122, 20);
            this.txtVendorCode.TabIndex = 8;
            this.txtVendorCode.Text = "";
            this.txtVendorCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendorCode_KeyDown);
            this.txtVendorCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtVendorCode_Validating);
            // 
            // btnVendorName
            // 
            this.btnVendorName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVendorName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVendorName.Location = new System.Drawing.Point(390, 70);
            this.btnVendorName.Name = "btnVendorName";
            this.btnVendorName.Size = new System.Drawing.Size(24, 20);
            this.btnVendorName.TabIndex = 12;
            this.btnVendorName.Text = "...";
            this.btnVendorName.Click += new System.EventHandler(this.btnVendorName_Click);
            // 
            // lblVendorName
            // 
            this.lblVendorName.ForeColor = System.Drawing.Color.Black;
            this.lblVendorName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVendorName.Location = new System.Drawing.Point(2, 70);
            this.lblVendorName.Name = "lblVendorName";
            this.lblVendorName.Size = new System.Drawing.Size(82, 20);
            this.lblVendorName.TabIndex = 10;
            this.lblVendorName.Text = "Vendor Name";
            this.lblVendorName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVendorCode
            // 
            this.btnVendorCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVendorCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnVendorCode.Location = new System.Drawing.Point(210, 48);
            this.btnVendorCode.Name = "btnVendorCode";
            this.btnVendorCode.Size = new System.Drawing.Size(24, 20);
            this.btnVendorCode.TabIndex = 9;
            this.btnVendorCode.Text = "...";
            this.btnVendorCode.Click += new System.EventHandler(this.btnVendorCode_Click);
            // 
            // lblVendorCode
            // 
            this.lblVendorCode.ForeColor = System.Drawing.Color.Black;
            this.lblVendorCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVendorCode.Location = new System.Drawing.Point(2, 48);
            this.lblVendorCode.Name = "lblVendorCode";
            this.lblVendorCode.Size = new System.Drawing.Size(82, 20);
            this.lblVendorCode.TabIndex = 7;
            this.lblVendorCode.Text = "Vendor Code";
            this.lblVendorCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(4, 141);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(286, 142);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(64, 22);
            this.btnHelp.TabIndex = 22;
            this.btnHelp.Text = "&Help";
            // 
            // btnClose
            // 
            this.btnClose.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(350, 142);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 22);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DeletePurchaseOrder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(418, 167);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this.txtVendorCode);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnVendorName);
            this.Controls.Add(this.lblVendorName);
            this.Controls.Add(this.btnVendorCode);
            this.Controls.Add(this.lblVendorCode);
            this.Controls.Add(this.btnPartName);
            this.Controls.Add(this.lblPartName);
            this.Controls.Add(this.btnPartNumber);
            this.Controls.Add(this.lblPartNumber);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.dtmToDate);
            this.Controls.Add(this.dtmFromDate);
            this.Controls.Add(this.lblToStartDate);
            this.Controls.Add(this.lblFromStartDate);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnSource);
            this.MaximizeBox = false;
            this.Name = "DeletePurchaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Estimate Purchase Order";
            this.Load += new System.EventHandler(this.DeletePurchaseOrder_Load);
            ((System.ComponentModel.ISupportInitialize) (this.dtmToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dtmFromDate)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
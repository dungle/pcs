using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using PCSComProcurement.Purchase.BO;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSProcurement.Purchase
{
    public partial class UpdatePurchaseOrder : Form
    {
        public const string This = "PCSProcurement.Purchase.UpdatePurchaseOrder";

        public UpdatePurchaseOrder()
        {
            InitializeComponent();
        }

        private void UpdatePurchaseOrder_Load(object sender, EventArgs e)
        {
            var methodName = string.Format("{0}.UpdatePurchaseOrder_Load()", GetType().FullName);
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

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var methodName = string.Format("{0}.UpdatePurchaseOrder_Load()", GetType().FullName);
            try
            {
                new UpdatePurchaseOrderBO().AdjustPurchaseOrder(Convert.ToDateTime(dtmFromDateTime.Value), Convert.ToDateTime(dtmToDateTime.Value), VendorCodeText.Tag as int?);
                PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void VendorCodeText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string methodName = This + ".VendorCodeText_Validating()";
            try
            {
                if (!VendorCodeText.Modified) return;
                if (VendorCodeText.Text.Trim() == string.Empty)
                {
                    FillVendor(null);
                    return;
                }
                var htbCriteria = new Hashtable { { "Vendor", 1 } };
                var drvResult = FormControlComponents.OpenSearchForm("V_VendorCustomer", MST_PartyTable.CODE_FLD, VendorCodeText.Text.Trim(), htbCriteria, false);
                if (drvResult != null)
                    FillVendor(drvResult);
                else
                    e.Cancel = true;
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

        private void VendorCodeText_KeyDown(object sender, KeyEventArgs e)
        {
            if (VendorCodeButton.Enabled && e.KeyCode == Keys.F4)
                VendorCodeButton_Click(null, null);
        }

        private void VendorCodeButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".VendorCodeButton_Click()";
            try
            {
                var htbCriteria = new Hashtable {{"Vendor", 1}};
                var drvResult = FormControlComponents.OpenSearchForm("V_VendorCustomer", MST_PartyTable.CODE_FLD, VendorCodeText.Text.Trim(), htbCriteria, true);
                if (drvResult != null)
                    FillVendor(drvResult);
                else
                    VendorCodeText.Focus();
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
        private void FillVendor(DataRowView rowView)
        {
            if (rowView== null)
            {
                VendorNameText.Text = string.Empty;
                VendorCodeText.Text = string.Empty;
                VendorCodeText.Tag = null;
            }
            else
            {
                VendorNameText.Text = rowView[MST_PartyTable.NAME_FLD].ToString();
                VendorCodeText.Text = rowView[MST_PartyTable.CODE_FLD].ToString();
                VendorCodeText.Tag = rowView[MST_PartyTable.PARTYID_FLD];
            }
        }
    }
}

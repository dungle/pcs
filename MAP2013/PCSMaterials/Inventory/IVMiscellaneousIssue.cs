using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComMaterials.Inventory.BO;
using PCSComMaterials.Inventory.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using AlignHorzEnum = C1.Win.C1TrueDBGrid.AlignHorzEnum;
using BeforeColUpdateEventArgs = C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using ColEventArgs = C1.Win.C1TrueDBGrid.ColEventArgs;

namespace PCSMaterials.Inventory
{
    /// <summary>
    /// This class allows you to transfer Part number quantities from one or more locations 
    /// to one or more locations associated with the same master location
    /// </summary>
    public class IVMiscellaneousIssue : Form
    {
        #region controls & variables

        private const string This = "PCSMaterials.Inventory.IVMiscellaneousIssue";
        private const string PartNumberFld = "PartNumber";
        private const string PartNameFld = "PartName";
        private const string ModelFld = "Model";
        private const string UnitFld = "UM";
        private const string LotControlFld = "LotControl";
        private const string ViewLocationCache = "V_ProductInLocCache";
        private const string ViewBinCache = "V_ProductInBinCache";

        private const string DateTimeHourFormat = "dd-MM-yyyy HH:mm";
        private readonly IVMiscellaneousIssueBO _boMiscellaneousIssue = new IVMiscellaneousIssueBO();

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components;

        private readonly IV_MiscellaneousIssueMasterVO _voMiscellaneousMaster = new IV_MiscellaneousIssueMasterVO();
        private C1Button AddButton;
        private C1Button CloseButton;

        private C1TextBox CommentText;
        private C1Button DeleteButton;
        private C1Button DestBinButton;
        private C1TextBox DestBinText;
        private C1Button DestLocButton;
        private C1TextBox DestLocText;
        private C1Button DestMasLocButton;
        private C1TextBox DestMasLocText;
        private C1TrueDBGrid DetailGrid;
        private EnumAction FormMode = EnumAction.Default;
        private C1Button HelpButton;
        private C1Button PartyCodeButton;
        private C1TextBox PartyCodeText;
        private C1Button PartyNameButton;
        private C1TextBox PartyNameText;
        private C1DateEdit PostDatePicker;
        private C1Button PrintButton;
        private C1Button PurposeButton;
        private C1TextBox PurposeText;
        private C1Button SaveButton;
        private C1Button SourceBinButton;
        private C1TextBox SourceBinText;
        private C1Button SourceLocButton;
        private C1TextBox SourceLocText;
        private C1Button SourceMasLocButton;
        private C1TextBox SourceMasLocText;
        private C1Button TransNoButton;
        private C1TextBox TransNoText;
        private bool blnHasError;
        private C1Combo cboCCN;
        private DataSet dstData;
        private DataTable dtbGridLayout;
        private DateTime dtmValidDate = DateTime.MinValue;
        private GroupBox grbDestination;
        private GroupBox grbSource;
        private int intTransTypeID;
        private Label lblCCN;
        private Label lblComment;
        private Label lblDestBin;
        private Label lblDestLoc;
        private Label lblDestMasLoc;
        private Label lblParty;
        private Label lblPostDate;
        private Label lblPurpose;
        private Label lblSourceBin;
        private Label lblSourceLoc;
        private Label lblSourceMasLoc;
        private Label lblTransNo;
        private string _lastValidDestBin = string.Empty;
        private string _lastValidDestLoc = string.Empty;
        private string _lastValidDestMasLoc = string.Empty;
        private string _lastValidPartyCode = string.Empty;
        private string _lastValidPartyName = string.Empty;
        private string _lastValidPurpose = string.Empty;
        private string _lastValidSouceMasLoc = string.Empty;
        private string _lastValidSourceBin = string.Empty;
        private string _lastValidSourceLoc = string.Empty;

        #endregion

        public IVMiscellaneousIssue()
        {
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

        private void FormLoad(object sender, EventArgs e)
        {
            const string methodName = This + ".FormLoad()";
            try
            {
                var objSecurity = new Security();
                Name = This;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    Close();
                    return;
                }

                //Load CCN and set default CCN
                var boUtils = new UtilsBO();
                DataSet dstCCN = boUtils.ListCCN();
                cboCCN.DataSource = dstCCN.Tables[MST_CCNTable.TABLE_NAME];
                cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
                cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
                FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN.Tables[MST_CCNTable.TABLE_NAME],
                                                            MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD,
                                                            MST_CCNTable.TABLE_NAME);
                if (SystemProperty.CCNID != 0)
                {
                    cboCCN.SelectedValue = SystemProperty.CCNID;
                }
                //HACK: added by Tuan TQ. Format post date
                PostDatePicker.FormatType = FormatTypeEnum.CustomFormat;
                PostDatePicker.CustomFormat = DateTimeHourFormat;
                //Reset form and save grid's layout
                dtbGridLayout = FormControlComponents.StoreGridLayout(DetailGrid);
                SwitchFormMode();

                intTransTypeID = new UtilsBO().GetTransTypeIDByCode(TransactionTypeEnum.IVMiscellaneousIssue.ToString());
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

        private void MiscIssueFormClosing(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".MiscIssueFormClosing()";
            try
            {
                if (FormMode == EnumAction.Add)
                {
                    DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,
                                                                    MessageBoxButtons.YesNoCancel,
                                                                    MessageBoxIcon.Question);
                    switch (confirmDialog)
                    {
                        case DialogResult.Yes:
                            //Save before exit
                            Save_Click(SaveButton, new EventArgs());
                            if (blnHasError)
                            {
                                e.Cancel = true;
                            }
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
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

        private void Delete_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".Delete_Click()";
            if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                try
                {
                    FormMode = EnumAction.Delete;
                    // Delete MiscellaneousIssue
                    _boMiscellaneousIssue.DeleteTransaction(Convert.ToInt32(TransNoText.Tag));

                    FormMode = EnumAction.Default;

                    BindDataToGrid(0);
                    //Reset Form
                    ResetForm();
                    //Enable button
                    SwitchFormMode();
                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxIcon.Information);
                }
                catch (PCSException ex)
                {
                    if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                    {
                        //Show message
                        PCSMessageBox.Show(string.Format("Not enought quantity to delete item {0}", ex.mMethod));
                        var productId = Convert.ToInt32(ex.Hash[ITM_ProductTable.PRODUCTID_FLD]);
                        // find in the grid and set focus
                        for (int i = 0; i < DetailGrid.RowCount; i++)
                        {
                            var product = Convert.ToInt32(DetailGrid[i, ITM_ProductTable.PRODUCTID_FLD]);
                            if (productId == product)
                            {
                                DetailGrid.Row = i;
                                DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[PRO_IssueMaterialDetailTable.AVAILABLEQUANTITY_FLD]);
                                DetailGrid.Focus();
                            }
                        }
                    }
                    else
                    {
                        // displays the error message.
                        PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                    }
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

        #region Button's clicking event

        private void TransNo_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".TransNo_Click()";
            try
            {
                var htbCriterial = new Hashtable();
                if (cboCCN.SelectedValue != null)
                    htbCriterial.Add(IV_MiscellaneousIssueMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    cboCCN.Focus();
                    return;
                }
                DataRowView drwResult = FormControlComponents.OpenSearchForm(
                    IV_MiscellaneousIssueMasterTable.TABLE_NAME, IV_MiscellaneousIssueMasterTable.TRANSNO_FLD,
                    TransNoText.Text, htbCriterial, true);
                if (drwResult != null)
                {
                    TransNoText.Text = drwResult[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].ToString().Trim();
                    TransNoText.Tag =
                        drwResult[IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD].ToString().Trim();
                    BindDataToGrid(int.Parse(TransNoText.Tag.ToString()));
                }
                else
                    TransNoText.Focus();
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

        private void Purpose_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".Purpose_Click()";
            try
            {
                DataRowView drowView = SearchPurpose(true);
                if (drowView != null)
                    FillPurposeData(drowView);
                else
                    PurposeText.Focus();
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

        private void SourceMasLoc_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".SourceMasLoc_Click()";
            try
            {
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    if (SourceMasLocText.Text != string.Empty)
                        SourceMasLocText.Focus();
                    else
                        cboCCN.Focus();
                    return;
                }
                var htbCondition = new Hashtable {{MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                             MST_MasterLocationTable.CODE_FLD,
                                                                             SourceMasLocText.Text, htbCondition, true);
                if (drwResult != null)
                    FillMasterLocationData(drwResult, true);
                else
                    SourceMasLocText.Focus();
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

        private void SourceLoc_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".SourceLoc_Click()";
            try
            {
                if (SourceMasLocText.Tag == null)
                {
                    string[] msgs = {lblSourceMasLoc.Text, lblSourceLoc.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (SourceLocText.Text != string.Empty)
                        SourceLocText.Focus();
                    else
                        SourceMasLocText.Focus();
                    return;
                }
                var htbCondition = new Hashtable
                                       {{MST_LocationTable.MASTERLOCATIONID_FLD, SourceMasLocText.Tag.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                             MST_LocationTable.CODE_FLD,
                                                                             SourceLocText.Text, htbCondition, true);
                if (drwResult != null)
                    FillLocationData(drwResult, true);
                else
                    SourceLocText.Focus();
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

        private void SourceBin_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".SourceBin_Click()";
            try
            {
                // check purpose for mandatory field
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblSourceBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (DestBinText.Text != string.Empty)
                        DestBinText.Focus();
                    else
                        PurposeText.Focus();
                    return;
                }

                if (SourceLocText.Tag == null)
                {
                    string[] msgs = {lblSourceLoc.Text, lblSourceBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (SourceBinText.Text != string.Empty)
                        SourceBinText.Focus();
                    else
                        SourceLocText.Focus();
                    return;
                }
                DataRowView drwResult = SearchSourceBin(true);
                if (drwResult != null)
                    FillBinData(drwResult, true);
                else
                    SourceBinText.Focus();
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

        private void DestMasLoc_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".DestMasLoc_Click()";
            try
            {
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    if (DestMasLocText.Text != string.Empty)
                        DestMasLocText.Focus();
                    else
                        cboCCN.Focus();
                    return;
                }
                var htbCondition = new Hashtable {{MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                             MST_MasterLocationTable.CODE_FLD,
                                                                             DestMasLocText.Text, htbCondition, true);
                if (drwResult != null)
                    FillMasterLocationData(drwResult, false);
                else
                    DestMasLocText.Focus();
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

        private void DestLoc_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".DestLoc_Click()";
            try
            {
                if (DestMasLocText.Tag == null)
                {
                    string[] msgs = {lblDestMasLoc.Text, lblDestLoc.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (DestLocText.Text != string.Empty)
                        DestLocText.Focus();
                    else
                        DestMasLocText.Focus();
                    return;
                }
                var htbCondition = new Hashtable
                                       {{MST_LocationTable.MASTERLOCATIONID_FLD, DestMasLocText.Tag.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                             MST_LocationTable.CODE_FLD,
                                                                             DestLocText.Text, htbCondition, true);
                if (drwResult != null)
                    FillLocationData(drwResult, false);
                else
                    DestLocText.Focus();
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

        private void DestBin_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".SourceBin_Click()";
            try
            {
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblDestBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (DestBinText.Text != string.Empty)
                        DestBinText.Focus();
                    else
                        PurposeText.Focus();
                    return;
                }

                if (DestLocText.Tag == null)
                {
                    string[] msgs = {lblDestLoc.Text, lblDestBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (DestBinText.Text != string.Empty)
                        DestBinText.Focus();
                    else
                        DestLocText.Focus();
                    return;
                }
                DataRowView drwResult = SearchDestBin(true);
                if (drwResult != null)
                    FillBinData(drwResult, false);
                else
                    DestBinText.Focus();
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

        private void Add_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".Add_Click()";
            try
            {
                ResetForm();
                FormMode = EnumAction.Add;
                PostDatePicker.Value = dtmValidDate = new UtilsBO().GetDBDate();

                TransNoText.Text = FormControlComponents.GetNoByMask(this);
                //Fill Default Master Location 
                FormControlComponents.SetDefaultMasterLocation(SourceMasLocText);
                FormControlComponents.SetDefaultMasterLocation(DestMasLocText);
                _lastValidSouceMasLoc = SourceMasLocText.Text;
                _lastValidDestMasLoc = DestMasLocText.Text;

                CreateDataSet();
                DetailGrid.DataSource = dstData.Tables[0];
                FormControlComponents.RestoreGridLayout(DetailGrid, dtbGridLayout);
                SwitchFormMode();
                DisableDestintion(0);
                ResetVariable();
                PurposeText.Focus();
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

        private void ResetVariable()
        {
            _lastValidSourceBin = string.Empty;
            _lastValidDestBin = string.Empty;
            _lastValidDestLoc = string.Empty;
            _lastValidSourceLoc = string.Empty;
            _lastValidPurpose = string.Empty;
            _lastValidPartyName = string.Empty;
            _lastValidPartyCode = string.Empty;
        }

        /// <summary>
        /// Save event:
        ///		Check data & call method to save into DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".Save_Click()";
            DataSet dstTemp = null;
            blnHasError = true;
            try
            {
                DateTime dtmServerDate = Utilities.Instance.GetServerDate();
                DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_DATA,
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button2);
                if (dlgResult == DialogResult.No)
                    return;
                if (Security.IsDifferencePrefix(this, lblTransNo, TransNoText))
                {
                    return;
                }

                if (!DetailGrid.EditActive && ValidateData())
                {
                    DetailGrid.UpdateData();
                    dstTemp = dstData.Copy();
                    FillDataToVOObject();
                    // synchronyze data
                    FormControlComponents.SynchronyGridData(DetailGrid);
                    _voMiscellaneousMaster.MiscellaneousIssueMasterID = _boMiscellaneousIssue.AddAndReturnId(_voMiscellaneousMaster, dstData, dtmServerDate);

                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                    FormMode = EnumAction.Default;
                    SwitchFormMode();
                    blnHasError = false;
                    TransNoText.Tag = _voMiscellaneousMaster.MiscellaneousIssueMasterID;
                }
            }
            catch (PCSException ex)
            {
                try
                {
                    Logger.LogMessage(ex.CauseException, methodName, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
                if (ex.mCode == ErrorCode.DUPLICATE_KEY)
                {
                    PCSMessageBox.Show(ErrorCode.DUPLICATE_KEY, MessageBoxIcon.Error);
                    TransNoText.Focus();
                }
                else if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                {
                    if (dstTemp != null) dstData = dstTemp.Copy();
                    DetailGrid.Refresh();
                    //Show message
                    PCSMessageBox.Show(string.Format("Not enought quantity to transfer for item {0}", ex.mMethod));
                    var productId = Convert.ToInt32(ex.Hash[ITM_ProductTable.PRODUCTID_FLD]);
                    var ohQuantity = Convert.ToDecimal(ex.Hash[IV_BinCacheTable.OHQUANTITY_FLD]);
                    // find in the grid and set focus
                    for (int i = 0; i < DetailGrid.RowCount; i++)
                    {
                        var product = Convert.ToInt32(DetailGrid[i, ITM_ProductTable.PRODUCTID_FLD]);
                        if (productId == product)
                        {
                            DetailGrid.Row = i;
                            DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD]);
                            DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] = ohQuantity;
                            DetailGrid.Focus();
                        }
                    }
                }
                else
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
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

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Party_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".Party_Click()";
            try
            {
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblParty.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (PartyCodeText.Text != string.Empty)
                        PartyCodeText.Focus();
                    else
                        PurposeText.Focus();
                    return;
                }
                int intPurposeCode = Convert.ToInt32(PurposeText.Tag);
                DataRowView drowView = SearchParty(intPurposeCode, MST_PartyTable.CODE_FLD, PartyCodeText.Text.Trim(),
                                                   true);
                if (drowView != null)
                    FillPartyData(drowView);
                else
                    PartyCodeText.Focus();
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

        private void PartyName_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".PartyName_Click()";
            try
            {
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblParty.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    if (PartyNameText.Text != string.Empty)
                        PartyNameText.Focus();
                    else
                        PurposeText.Focus();
                    return;
                }
                int intPurposeCode = Convert.ToInt32(PurposeText.Tag);
                DataRowView drowView = SearchParty(intPurposeCode, MST_PartyTable.NAME_FLD, PartyNameText.Text.Trim(),
                                                   true);
                if (drowView != null)
                    FillPartyData(drowView);
                else
                    PartyNameText.Focus();
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

        private void Print_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".Print_Click()";
            try
            {
                //exit if no item was selected
                if (TransNoText.Tag == null || PurposeText.Tag == null)
                {
                    return;
                }
                //exit if have no purpose
                if (PurposeText.Tag == DBNull.Value)
                {
                    return;
                }

                int intPurposeId = int.Parse(PurposeText.Tag.ToString());

                switch (intPurposeId)
                {
                    case (int) PurposeEnum.Scrap:
                        ShowDestroySlipReport(sender, e);
                        break;

                    case (int) PurposeEnum.Outside:
                        ShowDelivery2OutsourcingReport(sender, e);
                        break;

                    default:
                        ShowIssueMaterialReport(sender, e);
                        break;
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

        #endregion

        #region Control's leaving event

        private void TransNo_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".TransNo_Validating()";
            try
            {
                if (TransNoText.Text.Trim() == string.Empty)
                {
                    if (FormMode == EnumAction.Default)
                    {
                        ResetForm();
                        CreateDataSet();
                        DetailGrid.DataSource = dstData.Tables[0];
                        FormControlComponents.RestoreGridLayout(DetailGrid, dtbGridLayout);
                        SwitchFormMode();
                    }
                    return;
                }
                if (!TransNoText.Modified || FormMode == EnumAction.Add) return;
                var htbCriterial = new Hashtable();
                if (cboCCN.SelectedValue != null)
                {
                    htbCriterial.Add(IV_MiscellaneousIssueMasterTable.CCNID_FLD, cboCCN.SelectedValue.ToString());
                }
                else
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
                DataRowView drwResult = FormControlComponents.OpenSearchForm(
                    IV_MiscellaneousIssueMasterTable.TABLE_NAME, IV_MiscellaneousIssueMasterTable.TRANSNO_FLD,
                    TransNoText.Text, htbCriterial, false);
                if (drwResult != null)
                {
                    TransNoText.Text = drwResult[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].ToString().Trim();
                    TransNoText.Tag =
                        drwResult[IV_MiscellaneousIssueMasterTable.MISCELLANEOUSISSUEMASTERID_FLD].ToString().Trim();
                    BindDataToGrid(int.Parse(TransNoText.Tag.ToString()));
                }
                else
                    e.Cancel = true;
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

        private void Purpose_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".Purpose_Validating()";
            try
            {
                if (PurposeText.Text.Trim() == string.Empty)
                {
                    PurposeText.Tag = null;
                    DisableDestintion(0);
                    // clear source and destination bin
                    ChangePurpose();
                    _lastValidPurpose = string.Empty;
                    return;
                }
                if (_lastValidPurpose == PurposeText.Text.Trim()) return;
                DataRowView drowView = SearchPurpose(false);
                if (drowView != null)
                    FillPurposeData(drowView);
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

        private void SourceMasLoc_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".SourceMasLoc_Validating()";
            try
            {
                if (SourceMasLocText.Text.Trim() == string.Empty)
                {
                    ClearFormIfSourceMasLocChange();
                    SourceMasLocText.Tag = null;
                    _lastValidSouceMasLoc = string.Empty;
                    return;
                }
                if (_lastValidSouceMasLoc == SourceMasLocText.Text.Trim()) return;
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
                var htbCriteria = new Hashtable {{MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                             MST_MasterLocationTable.CODE_FLD,
                                                                             SourceMasLocText.Text.Trim(), htbCriteria,
                                                                             false);
                if (drwResult != null)
                    FillMasterLocationData(drwResult, true);
                else
                    e.Cancel = true;
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

        private void SourceLoc_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".SourceLoc_Validating()";
            try
            {
                if (SourceLocText.Text.Trim() == string.Empty)
                {
                    SourceLocText.Tag = null;
                    // clear data
                    ClearFormIfSourceLocChange();
                    // assign new valid source location
                    _lastValidSourceLoc = string.Empty;
                    return;
                }
                if (_lastValidSourceLoc == SourceLocText.Text.Trim()) return;
                if (SourceMasLocText.Tag == null)
                {
                    string[] msgs = {lblSourceMasLoc.Text, lblSourceLoc.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }
                var htbCondition = new Hashtable
                                       {{MST_LocationTable.MASTERLOCATIONID_FLD, SourceMasLocText.Tag.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                             MST_LocationTable.CODE_FLD,
                                                                             SourceLocText.Text, htbCondition, false);
                if (drwResult != null)
                    FillLocationData(drwResult, true);
                else
                    e.Cancel = true;
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

        private void SourceBin_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".SourceBin_Validating()";
            try
            {
                if (SourceBinText.Text.Trim() == string.Empty)
                {
                    SourceBinText.Tag = null;
                    // update grid
                    UpdateGridWhenChangeSouceBin(0);
                    // assign new valid source bin
                    _lastValidSourceBin = SourceBinText.Text;
                    DestLocText.Focus();
                    return;
                }
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblSourceBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }

                if (SourceLocText.Tag == null)
                {
                    string[] msgs = {lblSourceLoc.Text, lblSourceBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }
                DataRowView drwResult = SearchSourceBin(false);
                if (drwResult != null)
                {
                    FillBinData(drwResult, true);
                    DestLocText.Focus();
                }
                else
                {
                    SourceBinText.Focus();
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

        private void DestMasLoc_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".DestMasLoc_Validating()";
            try
            {
                if (DestMasLocText.Text.Trim() == string.Empty)
                {
                    DestMasLocText.Tag = null;
                    ClearFormIfDestMasLocChange();
                    _lastValidDestMasLoc = string.Empty;
                    return;
                }
                if (_lastValidDestMasLoc == DestMasLocText.Text.Trim()) return;
                if (cboCCN.SelectedValue == null)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_CCN, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
                var htbCondition = new Hashtable {{MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                             MST_MasterLocationTable.CODE_FLD,
                                                                             DestMasLocText.Text, htbCondition, false);
                if (drwResult != null)
                    FillMasterLocationData(drwResult, false);
                else
                    e.Cancel = true;
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

        private void DestLoc_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".DestLoc_Validating()";
            try
            {
                if (DestLocText.Text.Trim() == string.Empty)
                {
                    DestLocText.Tag = null;
                    ClearFormIfDestLocChange();
                    _lastValidDestLoc = string.Empty;
                    return;
                }
                if (_lastValidDestLoc == DestLocText.Text.Trim()) return;
                if (DestMasLocText.Tag == null)
                {
                    string[] msgs = {lblDestMasLoc.Text, lblDestLoc.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }
                var htbCondition = new Hashtable
                                       {{MST_LocationTable.MASTERLOCATIONID_FLD, DestMasLocText.Tag.ToString()}};
                DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_LocationTable.TABLE_NAME,
                                                                             MST_LocationTable.CODE_FLD,
                                                                             DestLocText.Text, htbCondition, false);
                if (drwResult != null)
                    FillLocationData(drwResult, false);
                else
                    e.Cancel = true;
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

        private void DestBin_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".DestBin_Validating()";
            try
            {
                if (DestBinText.Text.Trim() == string.Empty)
                {
                    DestBinText.Tag = null;
                    _lastValidDestBin = string.Empty;
                    return;
                }
                if (DestBinText.Text.Trim().Equals(_lastValidDestBin)) return;
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblDestBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }

                if (DestLocText.Tag == null)
                {
                    string[] msgs = {lblDestLoc.Text, lblDestBin.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }
                DataRowView drwResult = SearchDestBin(false);
                if (drwResult != null)
                    FillBinData(drwResult, false);
                else
                    e.Cancel = true;
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

        private void Party_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".Party_Validating()";
            try
            {
                if (PartyCodeText.Text.Trim() == string.Empty)
                {
                    PartyCodeText.Tag = null;
                    PartyNameText.Text = string.Empty;
                    _lastValidPartyCode = string.Empty;
                    _lastValidPartyName = string.Empty;
                    return;
                }
                if (_lastValidPartyCode == PartyCodeText.Text.Trim()) return;
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblParty.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }
                int intPurposeCode = Convert.ToInt32(PurposeText.Tag);
                DataRowView drowView = SearchParty(intPurposeCode, MST_PartyTable.CODE_FLD, PartyCodeText.Text.Trim(),
                                                   false);
                if (drowView != null)
                    FillPartyData(drowView);
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

        private void PartyName_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".PartyName_Validating()";
            try
            {
                if (PartyNameText.Text.Trim() == string.Empty)
                {
                    PartyCodeText.Tag = null;
                    PartyCodeText.Text = string.Empty;
                    _lastValidPartyCode = string.Empty;
                    _lastValidPartyName = string.Empty;
                    return;
                }
                if (_lastValidPartyName == PartyNameText.Text.Trim()) return;
                if (PurposeText.Tag == null)
                {
                    string[] msgs = {lblPurpose.Text, lblParty.Text};
                    PCSMessageBox.Show(ErrorCode.MESSAGE_SELECT_ONE_BEFORE_SELECT_ONE, MessageBoxIcon.Warning, msgs);
                    e.Cancel = true;
                    return;
                }
                int intPurposeCode = Convert.ToInt32(PurposeText.Tag);
                DataRowView drowView = SearchParty(intPurposeCode, MST_PartyTable.NAME_FLD, PartyNameText.Text.Trim(),
                                                   false);
                if (drowView != null)
                    FillPartyData(drowView);
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

        private void PostDate_Validating(object sender, CancelEventArgs e)
        {
            const string methodName = This + ".PostDate_Validating()";
            try
            {
                if (cboCCN.SelectedValue == DBNull.Value ||
                    SourceMasLocText.Tag == null ||
                    SourceLocText.Tag == null)
                    return;
                if (dtmValidDate.Equals(Convert.ToDateTime(PostDatePicker.Value)))
                    return;
                dtmValidDate = Convert.ToDateTime(PostDatePicker.Value);
                // update available quantity
                UpdateAvailableQuantityInGrid();
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
                // error from date time control when user select date out of range
                if (ex.Message.Equals(PostDatePicker.PostValidation.ErrorMessage))
                    return;
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

        #endregion

        #region Keydown event

        private void SourceMasLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && SourceMasLocButton.Enabled)
                SourceMasLoc_Click(sender, e);
        }

        private void SourceLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && SourceLocButton.Enabled)
                SourceLoc_Click(sender, e);
        }

        private void SourceBin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && SourceBinButton.Enabled)
                SourceBin_Click(sender, e);
        }

        private void DestMasLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && DestMasLocButton.Enabled)
                DestMasLoc_Click(sender, e);
        }

        private void DestLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && DestLocButton.Enabled)
                DestLoc_Click(sender, e);
        }

        private void DestBin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && DestBinButton.Enabled)
                DestBin_Click(sender, e);
        }

        private void TransNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (TransNoButton.Enabled)
            {
                if (e.KeyCode == Keys.F4)
                {
                    TransNo_Click(sender, new EventArgs());
                }
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".FormKeyDown()";
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    if (!DetailGrid.Splits[0].DisplayColumns[PartNumberFld].Locked)
                    {
                        DetailGrid.Row = DetailGrid.RowCount;
                        DetailGrid.Col = DetailGrid.Columns.IndexOf(DetailGrid.Columns[PartNumberFld]);
                        DetailGrid.Focus();
                        DetailGrid.EditActive = false;
                    }
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

        private void Purpose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && PurposeButton.Enabled)
            {
                Purpose_Click(PurposeButton, new EventArgs());
            }
        }

        private void Party_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".Party_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4 && PartyCodeButton.Enabled)
                {
                    Party_Click(PurposeButton, new EventArgs());
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

        private void PartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && PartyNameButton.Enabled)
            {
                PartyName_Click(PurposeButton, new EventArgs());
            }
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Get master and detail to bind in the grid
        /// </summary>
        /// <param name="pintMasterId"></param>
        private void BindDataToGrid(int pintMasterId)
        {
            //get Master
            DataRow drowInfor = _boMiscellaneousIssue.GetMiscellaneousMasterInfor(pintMasterId);
            if (drowInfor != null)
            {
                PostDatePicker.Value = drowInfor[IV_MiscellaneousIssueMasterTable.POSTDATE_FLD];
                TransNoText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.TRANSNO_FLD].ToString();
                CommentText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.COMMENT_FLD].ToString();

                SourceBinText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.SOURCEBINID_FLD].ToString();
                DestBinText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.DESBINID_FLD].ToString();
                SourceLocText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.SOURCELOCATIONID_FLD].ToString();
                DestLocText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.DESLOCATIONID_FLD].ToString();
                SourceMasLocText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.SOURCEMASLOCATIONID_FLD].ToString();
                DestMasLocText.Text = drowInfor[IV_MiscellaneousIssueMasterTable.DESMASLOCATIONID_FLD].ToString();
                PurposeText.Text =
                    drowInfor[PRO_IssuePurposeTable.TABLE_NAME + PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                PartyCodeText.Text = drowInfor[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD].ToString();
                PartyNameText.Text = drowInfor[MST_PartyTable.TABLE_NAME + MST_PartyTable.NAME_FLD].ToString();
                //HACK: added by Tuan TQ. 13 Mar, 2006
                PurposeText.Tag = drowInfor[IV_MiscellaneousIssueMasterTable.ISSUEPURPOSEID_FLD];
                //End
            }

            dstData = _boMiscellaneousIssue.GetMiscellaneousByMaster(pintMasterId);
            for (int i = 0; i < dstData.Tables[0].Rows.Count; i++)
                dstData.Tables[0].Rows[i][PRO_WorkOrderDetailTable.LINE_FLD] = i + 1;
            DetailGrid.DataSource = dstData.Tables[0];
            FormControlComponents.RestoreGridLayout(DetailGrid, dtbGridLayout);
            FormMode = EnumAction.Default;
            SwitchFormMode();
        }

        private void DetailGrid_ButtonClick(object sender, ColEventArgs e)
        {
            const string methodName = This + ".DetailGrid_ButtonClick()";
            try
            {
                DataRowView drwResult;
                var htbCondition = new Hashtable();
                string strValue = DetailGrid.AddNewMode == AddNewModeEnum.AddNewCurrent
                                      ? DetailGrid[DetailGrid.Row, DetailGrid.Col].ToString()
                                      : DetailGrid.Columns[DetailGrid.Col].Text.Trim();
                if (!SaveButton.Enabled) return;
                if (DetailGrid.Col == DetailGrid.Columns.IndexOf(DetailGrid.Columns[PartNumberFld])
                    || DetailGrid.Col == DetailGrid.Columns.IndexOf(DetailGrid.Columns[PartNameFld]))
                {
                    if (SourceLocText.Text.Trim() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_LOCATION);
                        SourceLocText.Focus();
                        return;
                    }

                    //if location with bin
                    if (lblSourceLoc.Tag.ToString() == true.ToString())
                    {
                        if (SourceBinText.Text.Trim() == string.Empty)
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_SOURCE_BIN);
                            SourceBinText.Focus();
                            return;
                        }
                    }

                    //open the search form to select Product
                    if (SourceLocText.Text.Trim() != string.Empty)
                    {
                        if (SourceBinText.Text.Trim() != string.Empty)
                        {
                            htbCondition.Add(ITM_ProductTable.BINID_FLD, SourceBinText.Tag.ToString());
                            htbCondition.Add(ITM_ProductTable.LOCATIONID_FLD, SourceLocText.Tag.ToString());
                            drwResult = DetailGrid.Col ==
                                        DetailGrid.Columns.IndexOf(DetailGrid.Columns[PartNumberFld])
                                            ? FormControlComponents.OpenSearchForm(ViewBinCache,
                                                                                   ITM_ProductTable.CODE_FLD, strValue,
                                                                                   htbCondition, true)
                                            : FormControlComponents.OpenSearchForm(ViewBinCache,
                                                                                   ITM_ProductTable.DESCRIPTION_FLD,
                                                                                   strValue, htbCondition, true);
                            if (drwResult != null)
                                FillItemDataToGrid(drwResult.Row);
                        }
                        else
                        {
                            htbCondition.Add(ITM_ProductTable.LOCATIONID_FLD, SourceLocText.Tag.ToString());
                            drwResult = DetailGrid.Col ==
                                        DetailGrid.Columns.IndexOf(DetailGrid.Columns[PartNumberFld])
                                            ? FormControlComponents.OpenSearchForm(ViewLocationCache,
                                                                                   ITM_ProductTable.CODE_FLD, strValue,
                                                                                   htbCondition, true)
                                            : FormControlComponents.OpenSearchForm(ViewLocationCache,
                                                                                   ITM_ProductTable.DESCRIPTION_FLD,
                                                                                   strValue, htbCondition, true);
                            if (drwResult != null)
                                FillItemDataToGrid(drwResult.Row);
                        }
                    }
                }
                if (DetailGrid.Col ==
                    DetailGrid.Columns.IndexOf(DetailGrid.Columns[IV_MiscellaneousIssueDetailTable.LOT_FLD]))
                {
                    if (DetailGrid[DetailGrid.Row, PartNumberFld].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_ITEM_BEFORE_SELECT_LOT);
                        return;
                    }
                    if (SourceBinText.Text.Trim() != string.Empty)
                    {
                        htbCondition.Add(IV_BinCacheTable.BINID_FLD, SourceBinText.Tag.ToString());
                        htbCondition.Add(IV_BinCacheTable.PRODUCTID_FLD,
                                         int.Parse(
                                             DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].
                                                 ToString()));
                        if (DetailGrid.AddNewMode != AddNewModeEnum.AddNewPending)
                        {
                            drwResult = FormControlComponents.OpenSearchForm(IV_BinCacheTable.TABLE_NAME,
                                                                             IV_MiscellaneousIssueDetailTable.LOT_FLD,
                                                                             DetailGrid[
                                                                                 DetailGrid.Row,
                                                                                 IV_MiscellaneousIssueDetailTable.
                                                                                     LOT_FLD].ToString(), htbCondition,
                                                                             true);
                        }
                        else
                        {
                            drwResult = FormControlComponents.OpenSearchForm(IV_BinCacheTable.TABLE_NAME,
                                                                             IV_MiscellaneousIssueDetailTable.LOT_FLD,
                                                                             DetailGrid.Columns[
                                                                                 IV_MiscellaneousIssueDetailTable.
                                                                                     LOT_FLD].Text.Trim(), htbCondition,
                                                                             true);
                        }
                        if (drwResult != null)
                        {
                            e.Column.DataColumn.Value = drwResult[IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString();
                            FillLotAndAutoFillSerial(drwResult.Row);
                        }
                    }
                    else
                    {
                        htbCondition.Add(IV_LocationCacheTable.LOCATIONID_FLD, SourceLocText.Tag.ToString());
                        htbCondition.Add(IV_LocationCacheTable.PRODUCTID_FLD,
                                         int.Parse(
                                             DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].
                                                 ToString()));
                        drwResult = DetailGrid.AddNewMode != AddNewModeEnum.AddNewPending
                                        ? FormControlComponents.OpenSearchForm(IV_LocationCacheTable.TABLE_NAME,
                                                                               IV_MiscellaneousIssueDetailTable.LOT_FLD,
                                                                               DetailGrid[
                                                                                   DetailGrid.Row,
                                                                                   IV_MiscellaneousIssueDetailTable.
                                                                                       LOT_FLD].ToString(), htbCondition,
                                                                               true)
                                        : FormControlComponents.OpenSearchForm(IV_LocationCacheTable.TABLE_NAME,
                                                                               IV_MiscellaneousIssueDetailTable.LOT_FLD,
                                                                               DetailGrid.Columns[
                                                                                   IV_MiscellaneousIssueDetailTable.
                                                                                       LOT_FLD].Text.Trim(),
                                                                               htbCondition, true);
                        if (drwResult != null)
                        {
                            e.Column.DataColumn.Value = drwResult[IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString();
                            FillLotAndAutoFillSerial(drwResult.Row);
                        }
                    }
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

        private void DetailGrid_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            const string methodName = This + ".DetailGrid_BeforeColUpdate()";
            try
            {
                var htbCriteria = new Hashtable();
                DataRowView drwResult = null;
                if (e.Column.DataColumn.DataField == PartNumberFld
                    || e.Column.DataColumn.DataField == PartNameFld)
                {
                    # region open Product search form

                    if (DetailGrid.Columns[e.Column.DataColumn.DataField].Text.Trim() != string.Empty)
                    {
                        if (SourceLocText.Text.Trim() != string.Empty)
                        {
                            if (SourceBinText.Text.Trim() != string.Empty)
                                htbCriteria.Add(ITM_ProductTable.BINID_FLD, SourceBinText.Tag.ToString());
                            else
                                htbCriteria.Add(ITM_ProductTable.LOCATIONID_FLD, SourceLocText.Tag.ToString());
                        }
                        else
                        {
                            PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_LOCATION);
                            e.Cancel = true;
                            return;
                        }
                        drwResult = SourceBinText.Text.Trim() != string.Empty
                                        ? FormControlComponents.OpenSearchForm(ViewBinCache,e.Column.DataColumn.DataField == PartNumberFld ? ITM_ProductTable.CODE_FLD : ITM_ProductTable.DESCRIPTION_FLD, DetailGrid.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false)
                                        : FormControlComponents.OpenSearchForm(ViewLocationCache, e.Column.DataColumn.DataField == PartNumberFld ? ITM_ProductTable.CODE_FLD : ITM_ProductTable.DESCRIPTION_FLD, DetailGrid.Columns[e.Column.DataColumn.DataField].Text.Trim(), htbCriteria, false);
                        if (drwResult != null)
                            e.Column.DataColumn.Tag = drwResult.Row;
                        else
                            e.Cancel = true;
                    }

                    #endregion
                }
                if (e.Column.DataColumn.DataField == IV_MiscellaneousIssueDetailTable.LOT_FLD)
                {
                    #region Open to search Lot

                    if (DetailGrid[DetailGrid.Row, PartNumberFld].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_ITEM_BEFORE_SELECT_LOT);
                        return;
                    }
                    if (e.Column.DataColumn.Value.ToString() == string.Empty) return;
                    if (SourceBinText.Text.Trim() != string.Empty)
                    {
                        htbCriteria.Add(IV_BinCacheTable.BINID_FLD, SourceBinText.Tag.ToString());
                        htbCriteria.Add(IV_BinCacheTable.PRODUCTID_FLD,
                                        int.Parse(
                                            DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].
                                                ToString()));
                        drwResult = FormControlComponents.OpenSearchForm(IV_BinCacheTable.TABLE_NAME,
                                                                         IV_MiscellaneousIssueDetailTable.LOT_FLD,
                                                                         e.Column.DataColumn.Value.ToString(),
                                                                         htbCriteria, false);
                        if (drwResult != null)
                        {
                            e.Column.DataColumn.Value = drwResult[IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString();
                            FillLotAndAutoFillSerial(drwResult.Row);
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        htbCriteria.Add(IV_LocationCacheTable.LOCATIONID_FLD, SourceLocText.Tag.ToString());
                        htbCriteria.Add(IV_LocationCacheTable.PRODUCTID_FLD,
                                        int.Parse(
                                            DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].
                                                ToString()));
                        if (DetailGrid.AddNewMode != AddNewModeEnum.AddNewPending)
                            drwResult = FormControlComponents.OpenSearchForm(IV_LocationCacheTable.TABLE_NAME,
                                                                             IV_MiscellaneousIssueDetailTable.LOT_FLD,
                                                                             e.Column.DataColumn.Value.ToString(),
                                                                             htbCriteria, false);
                        if (drwResult != null)
                        {
                            e.Column.DataColumn.Value = drwResult[IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString();
                            FillLotAndAutoFillSerial(drwResult.Row);
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }

                    #endregion
                }

                //check quantity
                if (DetailGrid.Splits[0].DisplayColumns[DetailGrid.Col].DataColumn.DataField ==
                    IV_MiscellaneousIssueDetailTable.QUANTITY_FLD)
                {
                    try
                    {
                        if (DetailGrid.Splits[0].DisplayColumns[DetailGrid.Col].DataColumn.Value.ToString() == string.Empty)
                            return;
                        decimal fQuantity = Convert.ToDecimal(DetailGrid.Splits[0].DisplayColumns[DetailGrid.Col].DataColumn.Value);
                        if (fQuantity <= 0)
                        {
                            PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                    }
                    catch
                    {
                        //cancel update and throw PCSException
                        e.Cancel = true;
                        PCSMessageBox.Show(ErrorCode.POSITIVE_NUMBER, MessageBoxIcon.Error);
                        return;
                    }
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

        private void DetailGrid_AfterColUpdate(object sender, ColEventArgs e)
        {
            const string methodName = This + ".DetailGrid_AfterColUpdate()";
            try
            {
                if (e.Column.DataColumn.DataField == PartNumberFld)
                {
                    if ((e.Column.DataColumn.Tag == null) ||
                        (DetailGrid[DetailGrid.Row, PartNumberFld].ToString() == string.Empty))
                    {
                        DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD] = DBNull.Value;
                        DetailGrid[DetailGrid.Row, PartNumberFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, PartNameFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, ModelFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, UnitFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.LOT_FLD] = string.Empty;
                        DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] = string.Empty;
                        DetailGrid[DetailGrid.Row, LotControlFld] = string.Empty;
                    }
                    else
                    {
                        FillItemDataToGrid((DataRow) e.Column.DataColumn.Tag);
                        return;
                    }
                }
                if (e.Column.DataColumn.DataField == PartNameFld)
                {
                    if ((e.Column.DataColumn.Tag == null) ||
                        (DetailGrid[DetailGrid.Row, PartNameFld].ToString() == string.Empty))
                    {
                        DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD] = DBNull.Value;
                        DetailGrid[DetailGrid.Row, PartNumberFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, PartNameFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, ModelFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, UnitFld] = string.Empty;
                        DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.LOT_FLD] = string.Empty;
                        DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] = string.Empty;
                        DetailGrid[DetailGrid.Row, LotControlFld] = string.Empty;
                    }
                    else
                    {
                        FillItemDataToGrid((DataRow) e.Column.DataColumn.Tag);
                        return;
                    }
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

        private void DetailGrid_KeyDown(object sender, KeyEventArgs e)
        {
            const string methodName = This + ".DetailGrid_KeyDown()";
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F4:
                        if (SaveButton.Enabled)
                        {
                            DetailGrid_ButtonClick(sender, null);
                        }
                        break;
                    case Keys.Delete:
                        FormControlComponents.DeleteMultiRowsOnTrueDBGrid(DetailGrid);
                        for (int i = 0; i < DetailGrid.RowCount; i++)
                        {
                            DetailGrid[i, SO_DeliveryScheduleTable.LINE_FLD] = i + 1;
                        }
                        break;
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

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IVMiscellaneousIssue));
            this.lblCCN = new System.Windows.Forms.Label();
            this.grbSource = new System.Windows.Forms.GroupBox();
            this.SourceBinButton = new C1.Win.C1Input.C1Button();
            this.SourceLocButton = new C1.Win.C1Input.C1Button();
            this.SourceMasLocButton = new C1.Win.C1Input.C1Button();
            this.SourceBinText = new C1.Win.C1Input.C1TextBox();
            this.SourceLocText = new C1.Win.C1Input.C1TextBox();
            this.SourceMasLocText = new C1.Win.C1Input.C1TextBox();
            this.lblSourceLoc = new System.Windows.Forms.Label();
            this.lblSourceMasLoc = new System.Windows.Forms.Label();
            this.lblSourceBin = new System.Windows.Forms.Label();
            this.lblPostDate = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.DetailGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.grbDestination = new System.Windows.Forms.GroupBox();
            this.DestBinButton = new C1.Win.C1Input.C1Button();
            this.DestLocButton = new C1.Win.C1Input.C1Button();
            this.PartyNameButton = new C1.Win.C1Input.C1Button();
            this.PartyCodeButton = new C1.Win.C1Input.C1Button();
            this.DestMasLocButton = new C1.Win.C1Input.C1Button();
            this.DestBinText = new C1.Win.C1Input.C1TextBox();
            this.DestLocText = new C1.Win.C1Input.C1TextBox();
            this.PartyNameText = new C1.Win.C1Input.C1TextBox();
            this.PartyCodeText = new C1.Win.C1Input.C1TextBox();
            this.DestMasLocText = new C1.Win.C1Input.C1TextBox();
            this.lblDestLoc = new System.Windows.Forms.Label();
            this.lblDestMasLoc = new System.Windows.Forms.Label();
            this.lblDestBin = new System.Windows.Forms.Label();
            this.lblParty = new System.Windows.Forms.Label();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.PostDatePicker = new C1.Win.C1Input.C1DateEdit();
            this.TransNoButton = new C1.Win.C1Input.C1Button();
            this.TransNoText = new C1.Win.C1Input.C1TextBox();
            this.PurposeButton = new C1.Win.C1Input.C1Button();
            this.PurposeText = new C1.Win.C1Input.C1TextBox();
            this.CommentText = new C1.Win.C1Input.C1TextBox();
            this.SaveButton = new C1.Win.C1Input.C1Button();
            this.AddButton = new C1.Win.C1Input.C1Button();
            this.DeleteButton = new C1.Win.C1Input.C1Button();
            this.PrintButton = new C1.Win.C1Input.C1Button();
            this.HelpButton = new C1.Win.C1Input.C1Button();
            this.CloseButton = new C1.Win.C1Input.C1Button();
            this.grbSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceBinText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceMasLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).BeginInit();
            this.grbDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DestBinText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyNameText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyCodeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestMasLocText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransNoText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentText)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCCN
            // 
            resources.ApplyResources(this.lblCCN, "lblCCN");
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.Name = "lblCCN";
            // 
            // grbSource
            // 
            resources.ApplyResources(this.grbSource, "grbSource");
            this.grbSource.Controls.Add(this.SourceBinButton);
            this.grbSource.Controls.Add(this.SourceLocButton);
            this.grbSource.Controls.Add(this.SourceMasLocButton);
            this.grbSource.Controls.Add(this.SourceBinText);
            this.grbSource.Controls.Add(this.SourceLocText);
            this.grbSource.Controls.Add(this.SourceMasLocText);
            this.grbSource.Controls.Add(this.lblSourceLoc);
            this.grbSource.Controls.Add(this.lblSourceMasLoc);
            this.grbSource.Controls.Add(this.lblSourceBin);
            this.grbSource.Name = "grbSource";
            this.grbSource.TabStop = false;
            // 
            // SourceBinButton
            // 
            resources.ApplyResources(this.SourceBinButton, "SourceBinButton");
            this.SourceBinButton.Name = "SourceBinButton";
            this.SourceBinButton.UseVisualStyleBackColor = true;
            this.SourceBinButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinButton.Click += new System.EventHandler(this.SourceBin_Click);
            // 
            // SourceLocButton
            // 
            resources.ApplyResources(this.SourceLocButton, "SourceLocButton");
            this.SourceLocButton.Name = "SourceLocButton";
            this.SourceLocButton.UseVisualStyleBackColor = true;
            this.SourceLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocButton.Click += new System.EventHandler(this.SourceLoc_Click);
            // 
            // SourceMasLocButton
            // 
            resources.ApplyResources(this.SourceMasLocButton, "SourceMasLocButton");
            this.SourceMasLocButton.Name = "SourceMasLocButton";
            this.SourceMasLocButton.UseVisualStyleBackColor = true;
            this.SourceMasLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocButton.Click += new System.EventHandler(this.SourceMasLoc_Click);
            // 
            // SourceBinText
            // 
            this.SourceBinText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.SourceBinText, "SourceBinText");
            this.SourceBinText.Name = "SourceBinText";
            this.SourceBinText.TextDetached = true;
            this.SourceBinText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceBinText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceBin_KeyDown);
            this.SourceBinText.Validating += new System.ComponentModel.CancelEventHandler(this.SourceBin_Validating);
            // 
            // SourceLocText
            // 
            this.SourceLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.SourceLocText, "SourceLocText");
            this.SourceLocText.Name = "SourceLocText";
            this.SourceLocText.TextDetached = true;
            this.SourceLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceLoc_KeyDown);
            this.SourceLocText.Validating += new System.ComponentModel.CancelEventHandler(this.SourceLoc_Validating);
            // 
            // SourceMasLocText
            // 
            this.SourceMasLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.SourceMasLocText, "SourceMasLocText");
            this.SourceMasLocText.Name = "SourceMasLocText";
            this.SourceMasLocText.TextDetached = true;
            this.SourceMasLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SourceMasLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceMasLoc_KeyDown);
            this.SourceMasLocText.Validating += new System.ComponentModel.CancelEventHandler(this.SourceMasLoc_Validating);
            // 
            // lblSourceLoc
            // 
            this.lblSourceLoc.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblSourceLoc, "lblSourceLoc");
            this.lblSourceLoc.Name = "lblSourceLoc";
            // 
            // lblSourceMasLoc
            // 
            this.lblSourceMasLoc.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblSourceMasLoc, "lblSourceMasLoc");
            this.lblSourceMasLoc.Name = "lblSourceMasLoc";
            // 
            // lblSourceBin
            // 
            resources.ApplyResources(this.lblSourceBin, "lblSourceBin");
            this.lblSourceBin.Name = "lblSourceBin";
            // 
            // lblPostDate
            // 
            this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPostDate, "lblPostDate");
            this.lblPostDate.Name = "lblPostDate";
            // 
            // lblComment
            // 
            this.lblComment.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblComment, "lblComment");
            this.lblComment.Name = "lblComment";
            // 
            // DetailGrid
            // 
            resources.ApplyResources(this.DetailGrid, "DetailGrid");
            this.DetailGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("DetailGrid.Images"))));
            this.DetailGrid.Name = "DetailGrid";
            this.DetailGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DetailGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DetailGrid.PreviewInfo.ZoomFactor = 75D;
            this.DetailGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DetailGrid.PrintInfo.PageSettings")));
            this.DetailGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.DetailGrid.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DetailGrid_AfterColUpdate);
            this.DetailGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.DetailGrid_BeforeColUpdate);
            this.DetailGrid.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.DetailGrid_ButtonClick);
            this.DetailGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailGrid_KeyDown);
            this.DetailGrid.PropBag = resources.GetString("DetailGrid.PropBag");
            // 
            // lblTransNo
            // 
            this.lblTransNo.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblTransNo, "lblTransNo");
            this.lblTransNo.Name = "lblTransNo";
            // 
            // grbDestination
            // 
            resources.ApplyResources(this.grbDestination, "grbDestination");
            this.grbDestination.Controls.Add(this.DestBinButton);
            this.grbDestination.Controls.Add(this.DestLocButton);
            this.grbDestination.Controls.Add(this.PartyNameButton);
            this.grbDestination.Controls.Add(this.PartyCodeButton);
            this.grbDestination.Controls.Add(this.DestMasLocButton);
            this.grbDestination.Controls.Add(this.DestBinText);
            this.grbDestination.Controls.Add(this.DestLocText);
            this.grbDestination.Controls.Add(this.PartyNameText);
            this.grbDestination.Controls.Add(this.PartyCodeText);
            this.grbDestination.Controls.Add(this.DestMasLocText);
            this.grbDestination.Controls.Add(this.lblDestLoc);
            this.grbDestination.Controls.Add(this.lblDestMasLoc);
            this.grbDestination.Controls.Add(this.lblDestBin);
            this.grbDestination.Controls.Add(this.lblParty);
            this.grbDestination.Name = "grbDestination";
            this.grbDestination.TabStop = false;
            // 
            // DestBinButton
            // 
            resources.ApplyResources(this.DestBinButton, "DestBinButton");
            this.DestBinButton.Name = "DestBinButton";
            this.DestBinButton.UseVisualStyleBackColor = true;
            this.DestBinButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinButton.Click += new System.EventHandler(this.DestBin_Click);
            // 
            // DestLocButton
            // 
            resources.ApplyResources(this.DestLocButton, "DestLocButton");
            this.DestLocButton.Name = "DestLocButton";
            this.DestLocButton.UseVisualStyleBackColor = true;
            this.DestLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocButton.Click += new System.EventHandler(this.DestLoc_Click);
            // 
            // PartyNameButton
            // 
            resources.ApplyResources(this.PartyNameButton, "PartyNameButton");
            this.PartyNameButton.Name = "PartyNameButton";
            this.PartyNameButton.UseVisualStyleBackColor = true;
            this.PartyNameButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameButton.Click += new System.EventHandler(this.PartyName_Click);
            // 
            // PartyCodeButton
            // 
            resources.ApplyResources(this.PartyCodeButton, "PartyCodeButton");
            this.PartyCodeButton.Name = "PartyCodeButton";
            this.PartyCodeButton.UseVisualStyleBackColor = true;
            this.PartyCodeButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeButton.Click += new System.EventHandler(this.Party_Click);
            // 
            // DestMasLocButton
            // 
            resources.ApplyResources(this.DestMasLocButton, "DestMasLocButton");
            this.DestMasLocButton.Name = "DestMasLocButton";
            this.DestMasLocButton.UseVisualStyleBackColor = true;
            this.DestMasLocButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocButton.Click += new System.EventHandler(this.DestMasLoc_Click);
            // 
            // DestBinText
            // 
            this.DestBinText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.DestBinText, "DestBinText");
            this.DestBinText.Name = "DestBinText";
            this.DestBinText.TextDetached = true;
            this.DestBinText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestBinText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DestBin_KeyDown);
            this.DestBinText.Validating += new System.ComponentModel.CancelEventHandler(this.DestBin_Validating);
            // 
            // DestLocText
            // 
            this.DestLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.DestLocText, "DestLocText");
            this.DestLocText.Name = "DestLocText";
            this.DestLocText.TextDetached = true;
            this.DestLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DestLoc_KeyDown);
            this.DestLocText.Validating += new System.ComponentModel.CancelEventHandler(this.DestLoc_Validating);
            // 
            // PartyNameText
            // 
            this.PartyNameText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PartyNameText, "PartyNameText");
            this.PartyNameText.Name = "PartyNameText";
            this.PartyNameText.TextDetached = true;
            this.PartyNameText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyNameText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PartyName_KeyDown);
            this.PartyNameText.Validating += new System.ComponentModel.CancelEventHandler(this.PartyName_Validating);
            // 
            // PartyCodeText
            // 
            this.PartyCodeText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PartyCodeText, "PartyCodeText");
            this.PartyCodeText.Name = "PartyCodeText";
            this.PartyCodeText.TextDetached = true;
            this.PartyCodeText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PartyCodeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Party_KeyDown);
            this.PartyCodeText.Validating += new System.ComponentModel.CancelEventHandler(this.Party_Validating);
            // 
            // DestMasLocText
            // 
            this.DestMasLocText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.DestMasLocText, "DestMasLocText");
            this.DestMasLocText.Name = "DestMasLocText";
            this.DestMasLocText.TextDetached = true;
            this.DestMasLocText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DestMasLocText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DestMasLoc_KeyDown);
            this.DestMasLocText.Validating += new System.ComponentModel.CancelEventHandler(this.DestMasLoc_Validating);
            // 
            // lblDestLoc
            // 
            this.lblDestLoc.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDestLoc, "lblDestLoc");
            this.lblDestLoc.Name = "lblDestLoc";
            // 
            // lblDestMasLoc
            // 
            this.lblDestMasLoc.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblDestMasLoc, "lblDestMasLoc");
            this.lblDestMasLoc.Name = "lblDestMasLoc";
            // 
            // lblDestBin
            // 
            resources.ApplyResources(this.lblDestBin, "lblDestBin");
            this.lblDestBin.Name = "lblDestBin";
            // 
            // lblParty
            // 
            resources.ApplyResources(this.lblParty, "lblParty");
            this.lblParty.Name = "lblParty";
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCCN, "cboCCN");
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCCN.Images"))));
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.MatchEntryTimeout = ((long)(2000));
            this.cboCCN.MaxDropDownItems = ((short)(5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
            this.cboCCN.PropBag = resources.GetString("cboCCN.PropBag");
            // 
            // lblPurpose
            // 
            this.lblPurpose.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Name = "lblPurpose";
            // 
            // PostDatePicker
            // 
            this.PostDatePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PostDatePicker, "PostDatePicker");
            // 
            // 
            // 
            this.PostDatePicker.Calendar.Font = ((System.Drawing.Font)(resources.GetObject("PostDatePicker.Calendar.Font")));
            this.PostDatePicker.Calendar.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("PostDatePicker.Calendar.RightToLeft")));
            this.PostDatePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Name = "PostDatePicker";
            this.PostDatePicker.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PostDatePicker.Validating += new System.ComponentModel.CancelEventHandler(this.PostDate_Validating);
            // 
            // TransNoButton
            // 
            resources.ApplyResources(this.TransNoButton, "TransNoButton");
            this.TransNoButton.Name = "TransNoButton";
            this.TransNoButton.UseVisualStyleBackColor = true;
            this.TransNoButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoButton.Click += new System.EventHandler(this.TransNo_Click);
            // 
            // TransNoText
            // 
            this.TransNoText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.TransNoText, "TransNoText");
            this.TransNoText.Name = "TransNoText";
            this.TransNoText.TextDetached = true;
            this.TransNoText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.TransNoText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransNo_KeyDown);
            this.TransNoText.Validating += new System.ComponentModel.CancelEventHandler(this.TransNo_Validating);
            // 
            // PurposeButton
            // 
            resources.ApplyResources(this.PurposeButton, "PurposeButton");
            this.PurposeButton.Name = "PurposeButton";
            this.PurposeButton.UseVisualStyleBackColor = true;
            this.PurposeButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeButton.Click += new System.EventHandler(this.Purpose_Click);
            // 
            // PurposeText
            // 
            this.PurposeText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.PurposeText, "PurposeText");
            this.PurposeText.Name = "PurposeText";
            this.PurposeText.TextDetached = true;
            this.PurposeText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PurposeText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Purpose_KeyDown);
            this.PurposeText.Validating += new System.ComponentModel.CancelEventHandler(this.Purpose_Validating);
            // 
            // CommentText
            // 
            this.CommentText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            resources.ApplyResources(this.CommentText, "CommentText");
            this.CommentText.Name = "CommentText";
            this.CommentText.TextDetached = true;
            this.CommentText.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CommentText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.SaveButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // AddButton
            // 
            resources.ApplyResources(this.AddButton, "AddButton");
            this.AddButton.Name = "AddButton";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.AddButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.AddButton.Click += new System.EventHandler(this.Add_Click);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DeleteButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DeleteButton.Click += new System.EventHandler(this.Delete_Click);
            // 
            // PrintButton
            // 
            resources.ApplyResources(this.PrintButton, "PrintButton");
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PrintButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.PrintButton.Click += new System.EventHandler(this.Print_Click);
            // 
            // HelpButton
            // 
            resources.ApplyResources(this.HelpButton, "HelpButton");
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.HelpButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.CloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // IVMiscellaneousIssue
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.CloseButton;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CommentText);
            this.Controls.Add(this.PurposeButton);
            this.Controls.Add(this.TransNoButton);
            this.Controls.Add(this.PurposeText);
            this.Controls.Add(this.TransNoText);
            this.Controls.Add(this.PostDatePicker);
            this.Controls.Add(this.DetailGrid);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.grbDestination);
            this.Controls.Add(this.lblTransNo);
            this.Controls.Add(this.grbSource);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblPostDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "IVMiscellaneousIssue";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MiscIssueFormClosing);
            this.Load += new System.EventHandler(this.FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.grbSource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SourceBinText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourceMasLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetailGrid)).EndInit();
            this.grbDestination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DestBinText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyNameText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PartyCodeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DestMasLocText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostDatePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransNoText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PurposeText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region reporting

        /// <summary>
        /// Build and show Issue Material Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 16 Mar, 2006</Author>
        private void ShowIssueMaterialReport(object sender, EventArgs e)
        {
            const string methodName = This + ".ShowIssueMaterialReport()";
            try
            {
                const string receipMaterialCaption = "Receive Material Slip";
                const string reportLayoutFile = "IssueMaterialSlip.xml";

                const string masterReportName = "IssueMaterial";
                const string subReportName = "SubIssueMaterialSlip";

                const string singleIssueMasterialReport = "SingleIssueMaterialSlip";
                const string singleReceiveMaterialReport = "SingleReceiveMaterialSlip";

                const string maxRows = " 100 PERCENT ";
                const int rowsPerPage = 10;

                const string applicationPath = @"PCSMain\bin\Debug";
                const string reportfldTitle = "fldTitle";
                const string reportfldCompany = "fldCompany";
                const string rowIndexFld = "RowIndex";

                //un-used data column name
                const string itemCodeFld = "ITM_ProductCode";
                const string itemModelFld = "ITM_ProductRevision";
                const string itemNameFld = "ITM_ProductDescription";
                const string quantityFld = "Quantity";
                const string stockumFld = "StockUM";
                const string categoryFld = "CategoryCode";

                //return if no record was selected
                if (TransNoText.Tag == null)
                {
                    return;
                }

                int intMasterId = int.Parse(TransNoText.Tag.ToString());
                if (intMasterId <= 0)
                {
                    return;
                }

                //Change cursor to wait status
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                C1PrintPreviewDialog receivePrintPreview = null;

                var boDataReport = new C1PrintPreviewDialogBO();

                DataTable dtbResult = boDataReport.GetDelivery2OutsourcingData(intMasterId, maxRows);

                var reportBuilder = new ReportWithSubReportBuilder();
                ReportWithSubReportBuilder receiveReportBuilder = null;

                #region Get Actual Report Path

                //Get actual application path
                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(applicationPath);
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

                #endregion

                #region Get Data Source & Assign To Report

                // Check data source
                if (dtbResult != null)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        dtbResult.Rows[i][rowIndexFld] = i + 1;
                    }

                    //Prepare data for blank row
                    DataRow drowSourceBlankRow = dtbResult.NewRow();
                    if (dtbResult.Rows.Count > 0)
                    {
                        //Copy data
                        for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                        {
                            drowSourceBlankRow[colIndex] = dtbResult.Rows[0][colIndex];
                        }

                        //Clear value of un-used column
                        drowSourceBlankRow[rowIndexFld] = DBNull.Value;
                        drowSourceBlankRow[itemCodeFld] = DBNull.Value;
                        drowSourceBlankRow[itemModelFld] = DBNull.Value;
                        drowSourceBlankRow[itemNameFld] = DBNull.Value;
                        drowSourceBlankRow[quantityFld] = DBNull.Value;
                        drowSourceBlankRow[categoryFld] = DBNull.Value;
                        drowSourceBlankRow[stockumFld] = DBNull.Value;
                    }

                    //Append blank rows to enough MAX_ROWS
                    if (dtbResult.Rows.Count <= rowsPerPage)
                    {
                        int intTotalLackRows = rowsPerPage - dtbResult.Rows.Count;
                        for (int i = 1; i <= intTotalLackRows; i++)
                        {
                            //Create new blank row
                            DataRow drowBlank = dtbResult.NewRow();

                            //Copy data
                            for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                            {
                                drowBlank[colIndex] = drowSourceBlankRow[colIndex];
                            }

                            dtbResult.Rows.Add(drowBlank);
                        }

                        //Set datasource and lay-out path for reports
                        reportBuilder.ReportName = masterReportName;
                        reportBuilder.SourceDataTable = dtbResult;
                        reportBuilder.SubReportDataSources.Add(subReportName, dtbResult);
                    }
                    else
                    {
                        //Set datasource and lay-out path for reports
                        reportBuilder.ReportName = singleIssueMasterialReport;
                        reportBuilder.SourceDataTable = dtbResult;

                        receiveReportBuilder = new ReportWithSubReportBuilder();
                        receivePrintPreview = new C1PrintPreviewDialog();

                        receiveReportBuilder.ReportName = singleReceiveMaterialReport;
                        receiveReportBuilder.SourceDataTable = dtbResult;

                        receiveReportBuilder.ReportLayoutFile = reportLayoutFile;
                        receiveReportBuilder.ReportDefinitionFolder = strReportPath;
                    }
                }

                #endregion

                reportBuilder.ReportLayoutFile = reportLayoutFile;
                reportBuilder.ReportDefinitionFolder = strReportPath;

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    //Set cursor to default
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params				
                reportBuilder.DrawPredefinedField(reportfldCompany,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));

                if (receiveReportBuilder == null)
                {
                    reportBuilder.Report.Fields[subReportName].Subreport.Fields[reportfldCompany].Text =
                        SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);
                }

                reportBuilder.RefreshReport();

                //Print report
                var titleField = reportBuilder.GetFieldByName(reportfldTitle);
                if (titleField != null)
                {
                    printPreview.FormTitle = titleField.Text;
                }

                #region Show Receive Slip Report

                if (receiveReportBuilder != null)
                {
                    //check if layout is valid
                    if (receiveReportBuilder.AnalyseLayoutFile())
                    {
                        receiveReportBuilder.UseLayoutFile = true;
                    }
                    else
                    {
                        //Set cursor to default
                        Cursor = Cursors.Default;
                        PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                        return;
                    }

                    receiveReportBuilder.MakeDataTableForRender();

                    // and show it in preview dialog
                    receiveReportBuilder.ReportViewer = receivePrintPreview.ReportViewer;
                    receiveReportBuilder.RenderReport();

                    //Header information get from system params				
                    receiveReportBuilder.DrawPredefinedField(reportfldCompany,
                                                             SystemProperty.SytemParams.Get(
                                                                 SystemParam.COMPANY_FULL_NAME));

                    receiveReportBuilder.RefreshReport();
                    receivePrintPreview.FormTitle = receipMaterialCaption;
                }

                printPreview.Show();

                if (receivePrintPreview != null)
                {
                    receivePrintPreview.Show();
                }

                #endregion
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
                //Set cursor to default status
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Build and show Delivery To Outsourcing Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 13 Mar, 2006</Author>
        private void ShowDelivery2OutsourcingReport(object sender, EventArgs e)
        {
            const string methodName = This + ".ShowDelivery2OutsourcingReport()";
            try
            {
                const string masterSubreportName = "Delivery2Outsourcing";
                const string deliveryToOutsourcingSlipReport = "Delivery2OutsourcingReport.xml";
                const string subreportName = "SubDelivery2Outsourcing";
                const string reportfldTitle = "fldTitle";

                const string maxRows = " 100 PERCENT ";
                const int rowsPerPage = 10;
                const string applicationPath = @"PCSMain\bin\Debug";

                const string reportfldCompany = "fldCompany";
                const string rowIndexFld = "RowIndex";

                //un-used data column name
                const string itemCodeFld = "ITM_ProductCode";
                const string itemModelFld = "ITM_ProductRevision";
                const string itemNameFld = "ITM_ProductDescription";
                const string quantityFld = "Quantity";
                const string stockumFld = "StockUM";
                const string categoryFld = "CategoryCode";

                //return if no record was selected
                if (TransNoText.Tag == null)
                {
                    return;
                }

                int intMasterId = int.Parse(TransNoText.Tag.ToString());
                if (intMasterId <= 0)
                {
                    return;
                }

                //Change cursor to wait status
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                DataTable dtbResult = boDataReport.GetDelivery2OutsourcingData(intMasterId, maxRows);

                // Check data source
                if (dtbResult != null)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        dtbResult.Rows[i][rowIndexFld] = i + 1;
                    }

                    //Prepare data for blank row
                    DataRow drowSourceBlankRow = dtbResult.NewRow();
                    if (dtbResult.Rows.Count > 0)
                    {
                        //Copy data
                        for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                        {
                            drowSourceBlankRow[colIndex] = dtbResult.Rows[0][colIndex];
                        }

                        //Clear value of un-used column
                        drowSourceBlankRow[rowIndexFld] = DBNull.Value;
                        drowSourceBlankRow[itemCodeFld] = DBNull.Value;
                        drowSourceBlankRow[itemModelFld] = DBNull.Value;
                        drowSourceBlankRow[itemNameFld] = DBNull.Value;
                        drowSourceBlankRow[quantityFld] = DBNull.Value;
                        drowSourceBlankRow[categoryFld] = DBNull.Value;
                        drowSourceBlankRow[stockumFld] = DBNull.Value;
                    }

                    //Append blank rows to enough MAX_ROWS
                    int intTotalLackRows = rowsPerPage - dtbResult.Rows.Count%rowsPerPage;
                    for (int i = 1; i <= intTotalLackRows; i++)
                    {
                        //Create new blank row
                        DataRow drowBlank = dtbResult.NewRow();

                        //Copy data
                        for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                        {
                            drowBlank[colIndex] = drowSourceBlankRow[colIndex];
                        }

                        dtbResult.Rows.Add(drowBlank);
                    }
                }

                var reportBuilder = new ReportWithSubReportBuilder();

                //Get actual application path
                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(applicationPath);
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
                reportBuilder.ReportName = masterSubreportName;
                reportBuilder.SourceDataTable = dtbResult;
                reportBuilder.SubReportDataSources.Add(subreportName, dtbResult);
                reportBuilder.ReportDefinitionFolder = strReportPath;

                reportBuilder.ReportLayoutFile = deliveryToOutsourcingSlipReport;

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    //Set cursor to default
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params				
                reportBuilder.DrawPredefinedField(reportfldCompany,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
                reportBuilder.Report.Fields[subreportName].Subreport.Fields[reportfldCompany].Text =
                    SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME);

                reportBuilder.RefreshReport();

                //Print report
                var titleField = reportBuilder.GetFieldByName(reportfldTitle);
                if (titleField != null)
                {
                    printPreview.FormTitle = titleField.Text;
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
                //Set cursor to default status
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Build and show Destroy Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 14 Mar, 2006</Author>
        private void ShowDestroySlipReport(object sender, EventArgs e)
        {
            const string methodName = This + ".ShowDestroySlipReportReport()";
            try
            {
                const string reportName = "DestroySlipReport";
                const string reportLayoutFile = "DestroySlipReport.xml";
                const string reportfldTitle = "fldTitle";

                const int rowsPerPage = 30;
                const string maxRows = " 100 PERCENT ";

                const string applicationPath = @"PCSMain\bin\Debug";

                const string reportfldCompany = "fldCompany";
                const string rowIndexFld = "RowIndex";

                //un-used data column name
                const string itemCodeFld = "ITM_ProductCode";
                const string itemModelFld = "ITM_ProductRevision";
                const string itemNameFld = "ITM_ProductDescription";
                const string quantityFld = "Quantity";
                const string stockumFld = "StockUM";
                const string categoryFld = "ITM_CategoryCode";
                const string vendorFld = "MST_PartyCode";


                //return if no record was selected
                if (TransNoText.Tag == null)
                {
                    return;
                }

                int intMasterId = int.Parse(TransNoText.Tag.ToString());
                if (intMasterId <= 0)
                {
                    return;
                }

                //Change cursor to wait status
                Cursor = Cursors.WaitCursor;

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                var dtbResult = boDataReport.GetDestroySlipData(intMasterId, maxRows);

                // Check data source
                if (dtbResult != null)
                {
                    for (int i = 0; i < dtbResult.Rows.Count; i++)
                    {
                        dtbResult.Rows[i][rowIndexFld] = i + 1;
                    }

                    //Prepare data for blank row
                    DataRow drowSourceBlankRow = dtbResult.NewRow();
                    if (dtbResult.Rows.Count > 0)
                    {
                        //Copy data
                        for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                        {
                            drowSourceBlankRow[colIndex] = dtbResult.Rows[0][colIndex];
                        }

                        //Clear value of un-used column
                        drowSourceBlankRow[rowIndexFld] = DBNull.Value;
                        drowSourceBlankRow[itemCodeFld] = DBNull.Value;
                        drowSourceBlankRow[itemModelFld] = DBNull.Value;
                        drowSourceBlankRow[itemNameFld] = DBNull.Value;
                        drowSourceBlankRow[quantityFld] = DBNull.Value;
                        drowSourceBlankRow[categoryFld] = DBNull.Value;
                        drowSourceBlankRow[vendorFld] = DBNull.Value;
                        drowSourceBlankRow[stockumFld] = DBNull.Value;
                    }

                    //Append blank rows to enough MAX_ROWS
                    int intTotalLackRows = rowsPerPage - dtbResult.Rows.Count%rowsPerPage;
                    for (int i = 1; i <= intTotalLackRows; i++)
                    {
                        //Create new blank row
                        DataRow drowBlank = dtbResult.NewRow();

                        //Copy data
                        for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                        {
                            drowBlank[colIndex] = drowSourceBlankRow[colIndex];
                        }

                        dtbResult.Rows.Add(drowBlank);
                    }
                }

                var reportBuilder = new ReportWithSubReportBuilder();

                //Get actual application path
                string strReportPath = Application.StartupPath;
                int intIndex = strReportPath.IndexOf(applicationPath);
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
                reportBuilder.ReportName = reportName;
                reportBuilder.SourceDataTable = dtbResult;
                reportBuilder.ReportDefinitionFolder = strReportPath;

                reportBuilder.ReportLayoutFile = reportLayoutFile;

                //check if layout is valid
                if (reportBuilder.AnalyseLayoutFile())
                {
                    reportBuilder.UseLayoutFile = true;
                }
                else
                {
                    //Set cursor to default
                    Cursor = Cursors.Default;
                    PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                    return;
                }

                reportBuilder.MakeDataTableForRender();

                // and show it in preview dialog
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params				
                reportBuilder.DrawPredefinedField(reportfldCompany,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));

                reportBuilder.RefreshReport();

                //Print report
                var titleField = reportBuilder.GetFieldByName(reportfldTitle);
                if (titleField != null)
                {
                    printPreview.FormTitle = titleField.Text;
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
                //Set cursor to default status
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Clear all cotrols on the form
        /// </summary>
        private void ResetForm()
        {
            PostDatePicker.Value = null;
            TransNoText.Text = string.Empty;
            TransNoText.Tag = null;
            CommentText.Text = string.Empty;
            SourceBinText.Text = string.Empty;
            SourceBinText.Tag = null;
            SourceLocText.Text = string.Empty;
            SourceLocText.Tag = null;
            SourceMasLocText.Text = string.Empty;
            SourceMasLocText.Tag = null;
            DestBinText.Text = string.Empty;
            DestBinText.Tag = null;
            DestLocText.Text = string.Empty;
            DestLocText.Tag = null;
            DestMasLocText.Text = string.Empty;
            DestMasLocText.Tag = null;
            PartyCodeText.Text = string.Empty;
            PartyCodeText.Tag = null;
            PurposeText.Text = string.Empty;
            PurposeText.Tag = null;
            PartyNameText.Text = string.Empty;
            PostDatePicker.Focus();
        }

        private void FillDataToVOObject()
        {
            _voMiscellaneousMaster.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
            var dtmTransDate = (DateTime) PostDatePicker.Value;
            _voMiscellaneousMaster.PostDate = new DateTime(dtmTransDate.Year, dtmTransDate.Month, dtmTransDate.Day,
                                                          dtmTransDate.Hour, dtmTransDate.Minute, 0);

            _voMiscellaneousMaster.Comment = CommentText.Text.Trim();
            _voMiscellaneousMaster.TransNo = TransNoText.Text.Trim();
            _voMiscellaneousMaster.SourceMasLocationID = int.Parse(SourceMasLocText.Tag.ToString());
            _voMiscellaneousMaster.SourceLocationID = int.Parse(SourceLocText.Tag.ToString());
            if (SourceBinText.Text.Trim() != string.Empty && SourceBinText.Enabled)
            {
                _voMiscellaneousMaster.SourceBinID = int.Parse(SourceBinText.Tag.ToString());
            }
            _voMiscellaneousMaster.DesMasLocationID = DestMasLocText.Text.Trim() != string.Empty
                                                         ? int.Parse(DestMasLocText.Tag.ToString())
                                                         : 0;
            _voMiscellaneousMaster.DesLocationID = DestLocText.Text.Trim() != string.Empty
                                                      ? int.Parse(DestLocText.Tag.ToString())
                                                      : 0;

            _voMiscellaneousMaster.DesBinID = DestBinText.Text.Trim() != string.Empty && DestBinText.Enabled
                                                 ? int.Parse(DestBinText.Tag.ToString())
                                                 : 0;
            _voMiscellaneousMaster.IssuePurposeID = PurposeText.Text.Trim() != string.Empty ? (int) PurposeText.Tag : 0;
            _voMiscellaneousMaster.PartyID = PartyCodeText.Text.Trim() != string.Empty ? (int) PartyCodeText.Tag : 0;

            _voMiscellaneousMaster.Comment = CommentText.Text.Trim();
        }

        /// <summary>
        /// Chang the state of control when FormMode is changed
        /// </summary>
        private void SwitchFormMode()
        {
            switch (FormMode)
            {
                case EnumAction.Add:
                    AddButton.Enabled = false;
                    DeleteButton.Enabled = false;
                    SaveButton.Enabled = true;
                    TransNoButton.Enabled = false;
                    PostDatePicker.Enabled = true;

                    CommentText.Enabled = true;
                    SourceMasLocText.Enabled = true;
                    SourceLocText.Enabled = true;
                    SourceBinText.Enabled = false;
                    DestMasLocText.Enabled = true;
                    DestLocText.Enabled = true;
                    DestBinText.Enabled = false;
                    DetailGrid.AllowDelete = true;
                    SourceBinButton.Enabled = false;
                    DestBinButton.Enabled = false;

                    PartyCodeText.Enabled = true;
                    PurposeText.Enabled = true;
                    PartyCodeButton.Enabled = true;
                    PartyNameText.Enabled = true;
                    PartyNameButton.Enabled = true;
                    PurposeButton.Enabled = true;
                    PrintButton.Enabled = false;

                    SourceMasLocButton.Enabled = true;
                    DestMasLocButton.Enabled = true;
                    SourceLocButton.Enabled = true;
                    DestLocButton.Enabled = true;
                    ConfigGrid(false);
                    break;
                case EnumAction.Edit:
                    break;
                default:
                    AddButton.Enabled = true;
                    DeleteButton.Enabled = (TransNoText.Text != string.Empty);
                    SaveButton.Enabled = false;
                    TransNoButton.Enabled = true;
                    TransNoText.Enabled = true;
                    PostDatePicker.Enabled = false;

                    CommentText.Enabled = false;
                    SourceMasLocText.Enabled = false;
                    SourceLocText.Enabled = false;
                    SourceBinText.Enabled = false;
                    DestMasLocText.Enabled = false;
                    DestLocText.Enabled = false;
                    DestBinText.Enabled = false;
                    DetailGrid.AllowDelete = false;
                    PartyCodeText.Enabled = false;
                    PurposeText.Enabled = false;
                    PartyCodeButton.Enabled = false;
                    PurposeButton.Enabled = false;
                    PrintButton.Enabled = true;

                    SourceMasLocButton.Enabled = false;
                    DestMasLocButton.Enabled = false;
                    SourceLocButton.Enabled = false;
                    DestLocButton.Enabled = false;
                    SourceBinButton.Enabled = false;
                    DestBinButton.Enabled = false;
                    PartyNameText.Enabled = false;
                    PartyNameButton.Enabled = false;
                    ConfigGrid(true);
                    break;
            }
        }

        /// <summary>
        /// Create template datasource for add event
        /// </summary>
        private void CreateDataSet()
        {
            dstData = new DataSet();
            dstData.Tables.Add(IV_MiscellaneousIssueDetailTable.TABLE_NAME);

            dstData.Tables[0].Columns.Add(new DataColumn(PRO_WorkOrderDetailTable.LINE_FLD, typeof (int)));
            dstData.Tables[0].Columns.Add(IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEMASTERID_FLD);
            dstData.Tables[0].Columns.Add(IV_MiscellaneousIssueDetailTable.MISCELLANEOUSISSUEDETAILID_FLD);
            dstData.Tables[0].Columns.Add(IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD);
            dstData.Tables[0].Columns.Add(ITM_ProductTable.STOCKUMID_FLD);
            dstData.Tables[0].Columns.Add(LotControlFld);

            dstData.Tables[0].Columns.Add(ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD);
            dstData.Tables[0].Columns.Add(PartNumberFld);
            dstData.Tables[0].Columns.Add(PartNameFld);
            dstData.Tables[0].Columns.Add(ModelFld);
            dstData.Tables[0].Columns.Add(MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD);
            dstData.Tables[0].Columns.Add(UnitFld);
            dstData.Tables[0].Columns.Add(IV_MiscellaneousIssueDetailTable.QUANTITY_FLD, typeof (decimal));
            dstData.Tables[0].Columns.Add(IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD, typeof (decimal));
            dstData.Tables[0].Columns.Add(IV_MiscellaneousIssueDetailTable.LOT_FLD);
        }

        /// <summary>
        /// Lock and config grid
        /// </summary>
        /// <param name="pblnLock"></param>
        private void ConfigGrid(bool pblnLock)
        {
            DetailGrid.Enabled = true;
            for (int i = 0; i < DetailGrid.Splits[0].DisplayColumns.Count; i++)
            {
                DetailGrid.Splits[0].DisplayColumns[i].Locked = true;
            }
            if (!pblnLock)
            {
                DetailGrid.Splits[0].DisplayColumns[PartNumberFld].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[PartNameFld].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.LOT_FLD].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].Locked = false;
                DetailGrid.Splits[0].DisplayColumns[PartNumberFld].Button = true;
                DetailGrid.Splits[0].DisplayColumns[PartNameFld].Button = true;
                DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.LOT_FLD].Button = true;
                DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].Style.
                    HorizontalAlignment = AlignHorzEnum.Far;
            }
            DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD].DataColumn.
                NumberFormat = Constants.DECIMAL_NUMBERFORMAT;
            DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].DataColumn.NumberFormat =
                Constants.DECIMAL_NUMBERFORMAT;
        }

        /// <summary>
        /// validate data before save
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (FormControlComponents.CheckMandatory(PostDatePicker))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                PostDatePicker.Focus();
                PostDatePicker.Select();
                return false;
            }
            if (!FormControlComponents.CheckDateInCurrentPeriod(DateTime.Parse(PostDatePicker.Value.ToString())))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD);
                PostDatePicker.Focus();
                PostDatePicker.Select();
                return false;
            }
            //Check postdate in configuration
            if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)PostDatePicker.Value))
            {
                return false;
            }
            //check the PostDate smaller than the current date
            if ((DateTime) PostDatePicker.Value > new UtilsBO().GetDBDate())
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE, MessageBoxIcon.Warning);
                PostDatePicker.Focus();
                return false;
            }
            if (FormControlComponents.CheckMandatory(TransNoText))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                TransNoText.Focus();
                TransNoText.Select();
                return false;
            }
            if (FormControlComponents.CheckMandatory(PurposeText))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                PurposeText.Focus();
                return false;
            }
            if (FormControlComponents.CheckMandatory(SourceMasLocText))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                SourceMasLocText.Focus();
                SourceMasLocText.Select();
                return false;
            }
            if (FormControlComponents.CheckMandatory(SourceLocText))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                SourceLocText.Focus();
                SourceLocText.Select();
                return false;
            }

            if (PurposeText.Text.Trim() != string.Empty && lblPurpose.Tag.ToString() == 1.ToString())
            {
                if (DestMasLocText.Text.Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                    DestMasLocText.Focus();
                    return false;
                }
            }

            if (DestMasLocText.Text.Trim() != string.Empty && DestLocText.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                DestLocText.Focus();
                DestLocText.Select();
                return false;
            }
            if ((SourceBinText.Text.Trim() != string.Empty) && (DestBinText.Text.Trim() != string.Empty) &&
                (SourceBinText.Text.Trim() == DestBinText.Text.Trim()))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_DIFFERENCE_BIN, MessageBoxIcon.Error);
                DestBinText.Focus();
                return false;
            }
            if ((SourceBinText.Text.Trim() == string.Empty) && (DestBinText.Text.Trim() == string.Empty) &&
                (SourceLocText.Text.Trim() == DestLocText.Text.Trim()))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_DIFFERENCE_LOC, MessageBoxIcon.Error);
                DestLocText.Focus();
                return false;
            }
            if (SourceLocText.Text.Trim() != string.Empty)
            {
                if (SourceBinText.Text.Trim() == string.Empty && lblSourceLoc.Tag.ToString() == true.ToString())
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_SOURCE_BIN, MessageBoxIcon.Error);
                    SourceBinText.Focus();
                    return false;
                }
            }
            if (DestLocText.Text.Trim() != string.Empty)
            {
                if (DestBinText.Text.Trim() == string.Empty && lblDestLoc.Tag.ToString() == true.ToString())
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_DEST_BIN, MessageBoxIcon.Error);
                    DestBinText.Focus();
                    return false;
                }
            }
            if (lblParty.ForeColor == Color.Maroon)
            {
                if (FormControlComponents.CheckMandatory(PartyCodeText))
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                    PartyCodeText.Focus();
                    PartyCodeText.Select();
                    return false;
                }
                if (FormControlComponents.CheckMandatory(PartyNameText))
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID);
                    PartyNameText.Focus();
                    PartyNameText.Select();
                    return false;
                }
            }
            if (DetailGrid.RowCount == 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_HAS_ATLEAST_ONELINE);
                DetailGrid.Focus();
                return false;
            }

            //check madatory field in grid
            for (int i = 0; i < DetailGrid.RowCount; i++)
            {
                if (DetailGrid[i, PartNumberFld].ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Error);
                    DetailGrid.Row = i;
                    DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PartNumberFld]);
                    DetailGrid.Focus();
                    return false;
                }
                if (i > 0 && DetailGrid[i, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].ToString() == DetailGrid[i-1, IV_MiscellaneousIssueDetailTable.PRODUCTID_FLD].ToString())
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_DUPLICATE_ITEM, MessageBoxIcon.Error);
                    DetailGrid.Row = i;
                    DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[PartNumberFld]);
                    DetailGrid.Focus();
                    return false;
                }
                if (DetailGrid[i, LotControlFld].ToString() == 1.ToString())
                {
                    if (DetailGrid[i, IV_MiscellaneousIssueDetailTable.LOT_FLD].ToString() == string.Empty)
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_SELECT_LOT, MessageBoxIcon.Error);
                        DetailGrid.Row = i;
                        DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.LOT_FLD]);
                        DetailGrid.Focus();
                        return false;
                    }
                }
                if (DetailGrid[i, IV_MiscellaneousIssueDetailTable.QUANTITY_FLD].ToString() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_LOCTOLOC_TRANSFER_QUANTITY, MessageBoxIcon.Error);
                    DetailGrid.Row = i;
                    DetailGrid.Col = DetailGrid.Splits[0].DisplayColumns.IndexOf(DetailGrid.Splits[0].DisplayColumns[IV_MiscellaneousIssueDetailTable.QUANTITY_FLD]);
                    DetailGrid.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Fill data for item after selected
        /// </summary>
        /// <param name="pdrowData"></param>
        private void FillItemDataToGrid(DataRow pdrowData)
        {
            DetailGrid.EditActive = true;
            DetailGrid[DetailGrid.Row, PRO_WorkOrderDetailTable.LINE_FLD] = DetailGrid.Row + 1;
            DetailGrid[DetailGrid.Row, ITM_ProductTable.PRODUCTID_FLD] =
                int.Parse(pdrowData[ITM_ProductTable.PRODUCTID_FLD].ToString());
            // category and vendor
            DetailGrid[DetailGrid.Row, ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD] =
                pdrowData[ITM_CategoryTable.TABLE_NAME + ITM_CategoryTable.CODE_FLD];
            DetailGrid[DetailGrid.Row, MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD] =
                pdrowData[MST_PartyTable.TABLE_NAME + MST_PartyTable.CODE_FLD];
            // general information
            DetailGrid[DetailGrid.Row, PartNumberFld] = pdrowData[ITM_ProductTable.CODE_FLD];
            DetailGrid[DetailGrid.Row, PartNameFld] = pdrowData[ITM_ProductTable.DESCRIPTION_FLD];
            DetailGrid[DetailGrid.Row, ModelFld] = pdrowData[ITM_ProductTable.REVISION_FLD];
            DetailGrid[DetailGrid.Row, UnitFld] =
                pdrowData[MST_UnitOfMeasureTable.TABLE_NAME + MST_UnitOfMeasureTable.CODE_FLD];
            DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD] =
                pdrowData[IV_MiscellaneousIssueDetailTable.STOCKUMID_FLD];
            DetailGrid[DetailGrid.Row, ITM_ProductTable.LOTCONTROL_FLD] = pdrowData[ITM_ProductTable.LOTCONTROL_FLD];
            // clear issue quantity
            DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.QUANTITY_FLD] = string.Empty;

            int intLocId = Convert.ToInt32(SourceLocText.Tag);
            int intBinId = SourceBinText.Text != string.Empty ? Convert.ToInt32(SourceBinText.Tag) : 0;
            int intProductId = Convert.ToInt32(pdrowData[ITM_ProductTable.PRODUCTID_FLD]);
            var binCache = InventoryUtilities.Instance.ListBinCache(intLocId, intBinId, intProductId);
            decimal decAvailable = binCache.Sum(b => b.OHQuantity.GetValueOrDefault(0));
            DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] = decAvailable;
        }

        /// <summary>
        /// Get serial and fill data to control
        /// </summary>
        /// <param name="pdrowLot"></param>
        private void FillLotAndAutoFillSerial(DataRow pdrowLot)
        {
            if (pdrowLot[IV_BinCacheTable.COMMITQUANTITY_FLD].ToString() != string.Empty)
            {
                DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] =
                    decimal.Parse(pdrowLot[IV_BinCacheTable.OHQUANTITY_FLD].ToString()) -
                    decimal.Parse(pdrowLot[IV_BinCacheTable.COMMITQUANTITY_FLD].ToString());
            }
            else
                DetailGrid[DetailGrid.Row, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] =
                    decimal.Parse(pdrowLot[IV_BinCacheTable.OHQUANTITY_FLD].ToString());
        }

        /// <summary>
        /// Disable destination when user changed/clear purpose 
        /// </summary>
        /// <param name="pintPurposeCode">Purpose Code. 0 empty purpose</param>
        private void DisableDestintion(int pintPurposeCode)
        {
            if (pintPurposeCode > 0)
            {
                switch (pintPurposeCode)
                {
                    case (int) PurposeEnum.CompensationForCustomer:
                    case (int) PurposeEnum.Misc: // allow enter party, destintion mas loc, loc, bin
                        // party is mandatory in this case
                        lblParty.ForeColor = Color.Black;
                        PartyCodeText.Enabled = true;
                        PartyCodeButton.Enabled = true;
                        PartyNameText.Enabled = true;
                        PartyNameButton.Enabled = true;

                        DestMasLocText.Enabled = true;
                        DestMasLocButton.Enabled = true;
                        lblDestMasLoc.ForeColor = Color.Black;

                        DestLocText.Text = string.Empty;
                        DestLocText.Tag = null;
                        DestLocText.Enabled = true;
                        DestLocButton.Enabled = true;
                        lblDestLoc.Tag = null;
                        lblDestLoc.ForeColor = Color.Black;

                        DestBinText.Text = string.Empty;
                        DestBinText.Tag = null;
                        DestBinText.Enabled = true;
                        DestBinButton.Enabled = true;
                        break;
                    default: // disable party, enable destination
                        PartyCodeText.Text = string.Empty;
                        PartyCodeText.Tag = null;
                        PartyCodeText.Enabled = false;
                        PartyCodeButton.Enabled = false;
                        PartyNameText.Text = string.Empty;
                        PartyNameText.Tag = null;
                        PartyNameText.Enabled = false;
                        PartyNameButton.Enabled = false;

                        DestMasLocText.Enabled = true;
                        DestMasLocButton.Enabled = true;

                        DestLocText.Enabled = true;
                        DestLocButton.Enabled = true;

                        bool blnHasDestination = Convert.ToBoolean(lblPurpose.Tag);
                        lblDestMasLoc.ForeColor = (blnHasDestination) ? Color.Maroon : Color.Black;
                        lblDestLoc.ForeColor = (blnHasDestination) ? Color.Maroon : Color.Black;
                        lblParty.ForeColor = Color.Black;
                        break;
                }
            }
            else
            {
                PartyCodeText.Enabled = false;
                PartyCodeButton.Enabled = false;
                PartyNameText.Enabled = false;
                PartyNameButton.Enabled = false;

                DestMasLocText.Enabled = false;
                DestMasLocButton.Enabled = false;

                DestLocText.Enabled = false;
                DestLocButton.Enabled = false;

                DestBinText.Enabled = false;
                DestBinButton.Enabled = false;

                lblDestMasLoc.ForeColor = Color.Black;
                lblDestLoc.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// When purpose was changed, clear Source and Destination Bin, clear grid
        /// </summary>
        private void ChangePurpose()
        {
            _lastValidSourceBin = SourceBinText.Text = string.Empty;
            SourceBinText.Tag = null;

            _lastValidDestLoc = DestLocText.Text = string.Empty;
            DestLocText.Tag = null;

            _lastValidDestBin = DestBinText.Text = string.Empty;
            DestBinText.Tag = null;

            _lastValidPartyCode = PartyCodeText.Text = string.Empty;
            PartyCodeText.Tag = null;
            _lastValidPartyName = PartyNameText.Text = string.Empty;

            // clear grid information if have any data
            if (dstData != null && dstData.Tables.Count > 0)
                dstData.Tables[0].Clear();
        }

        /// <summary>
        /// update grid detail for new bin
        /// </summary>
        /// <param name="pintBinId">New Bin ID. 0 will be empty bin</param>
        private void UpdateGridWhenChangeSouceBin(int pintBinId)
        {
            if (dstData != null && dstData.Tables.Count > 0)
            {
                if (pintBinId > 0)
                {
                    for (int i = dstData.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow drowData = dstData.Tables[0].Rows[i];
                        if (drowData[ITM_ProductTable.PRODUCTID_FLD] != null &&
                            drowData[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
                        {
                            var binCache = InventoryUtilities.Instance.ListBinCache((int)SourceLocText.Tag, pintBinId, Convert.ToInt32(drowData[ITM_ProductTable.PRODUCTID_FLD]));
                            //get Availbale by PostDate
                            if (binCache.Count > 0)
                                drowData[IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] = binCache.Sum(b => b.OHQuantity);
                            else
                                dstData.Tables[0].Rows.RemoveAt(i);
                        }
                    }
                }
                else
                    dstData.Tables[0].Clear();
            }
        }

        /// <summary>
        /// Update available quantity in grid if has any changes
        /// </summary>
        private void UpdateAvailableQuantityInGrid()
        {
            var binCache = InventoryUtilities.Instance.ListBinCache((int)SourceLocText.Tag, SourceBinText.Text.Trim() != string.Empty ? (int)SourceBinText.Tag : 0, 0);
            for (int i = 0; i < DetailGrid.RowCount; i++)
            {
                if (DetailGrid[i, PartNumberFld].ToString() != string.Empty)
                {
                    int productId = Convert.ToInt32(DetailGrid[i, ITM_ProductTable.PRODUCTID_FLD]);
                    DetailGrid[i, IV_MiscellaneousIssueDetailTable.AVAILABLEQTY_FLD] = binCache.Where(b => b.ProductID == productId).Sum(b => b.OHQuantity);
                }
            }
        }

        private void ClearFormIfSourceMasLocChange()
        {
            SourceLocText.Tag = null;
            _lastValidSourceLoc = SourceLocText.Text = string.Empty;
            SourceBinText.Tag = null;
            _lastValidSourceBin = SourceBinText.Text = string.Empty;
            CreateDataSet();
            DetailGrid.DataSource = dstData.Tables[0];
            FormControlComponents.RestoreGridLayout(DetailGrid, dtbGridLayout);
            ConfigGrid(false);
        }

        private void ClearFormIfSourceLocChange()
        {
            SourceBinText.Tag = null;
            _lastValidSourceBin = SourceBinText.Text = string.Empty;
            CreateDataSet();
            DetailGrid.DataSource = dstData.Tables[0];
            FormControlComponents.RestoreGridLayout(DetailGrid, dtbGridLayout);
            ConfigGrid(false);
        }

        private void ClearFormIfDestLocChange()
        {
            DestBinText.Tag = null;
            _lastValidDestBin = DestBinText.Text = string.Empty;
        }

        private void ClearFormIfDestMasLocChange()
        {
            DestLocText.Tag = null;
            _lastValidDestLoc = DestLocText.Text = string.Empty;
            DestBinText.Tag = null;
            _lastValidDestBin = DestBinText.Text = string.Empty;
            _lastValidPartyCode = PartyCodeText.Text = string.Empty;
            _lastValidPartyName = PartyNameText.Text = string.Empty;
            PartyCodeText.Tag = null;
        }

        /// <summary>
        /// Display search form for select purpose
        /// </summary>
        /// <param name="pblnOpenForm">Open form or not</param>
        /// <returns>DataRowView</returns>
        private DataRowView SearchPurpose(bool pblnOpenForm)
        {
            var htbCondition = new Hashtable {{PRO_IssuePurposeTable.TRANTYPEID_FLD, intTransTypeID}};
            DataRowView drowView = FormControlComponents.OpenSearchForm(PRO_IssuePurposeTable.TABLE_NAME,
                                                                    PRO_IssuePurposeTable.DESCRIPTION_FLD,
                                                                    PurposeText.Text.Trim(),
                                                                    htbCondition, pblnOpenForm);
            return drowView;
        }

        /// <summary>
        /// Display search form for select source bin
        /// </summary>
        /// <param name="pblnOpenForm">Open form or not</param>
        /// <returns>DataRowView</returns>
        private DataRowView SearchSourceBin(bool pblnOpenForm)
        {
            var htbCondition = new Hashtable();
            string strWhere = string.Empty;
            int intPurposeCode = Convert.ToInt32(PurposeText.Tag);
            htbCondition.Add(MST_BINTable.LOCATIONID_FLD, SourceLocText.Tag.ToString());

            strWhere += MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + "=" + SourceLocText.Tag;

            // 25-04-2006 dungla: thuypt required:
            switch (intPurposeCode)
            {
                case (int) PurposeEnum.Transfer:
                    // Purpose= Loc Transfer from NG to OK, SourceBin must be NG.
                    htbCondition.Add(MST_BINTable.BINTYPEID_FLD, (int) BinTypeEnum.NG);
                    break;
                case (int) PurposeEnum.Outside:
                    // Purpose= Outside processing, SourceBin must be OK, BF.
                    strWhere += " AND (" + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "=" +
                                (int) BinTypeEnum.OK;
                    strWhere += " OR " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "=" +
                                (int) BinTypeEnum.IN + ")";
                    break;
            }
            // 25-04-2006 dungla: thuypt required:
            DataRowView drwResult = intPurposeCode == (int) PurposeEnum.Outside
                                        ? FormControlComponents.OpenSearchFormWithWhere(MST_BINTable.TABLE_NAME,
                                                                                        MST_BINTable.CODE_FLD,
                                                                                        SourceBinText.Text, strWhere,
                                                                                        pblnOpenForm)
                                        : FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME,
                                                                               MST_BINTable.CODE_FLD, SourceBinText.Text,
                                                                               htbCondition, pblnOpenForm);
            return drwResult;
        }

        /// <summary>
        /// Display search form for select destination bin
        /// </summary>
        /// <param name="pblnOpenForm">Open form or not</param>
        /// <returns>DataRowView</returns>
        private DataRowView SearchDestBin(bool pblnOpenForm)
        {
            DataRowView drwResult;
            var htbCondition = new Hashtable();
            string strWhere = string.Empty;
            int intPurposeCode = Convert.ToInt32(PurposeText.Tag);
            htbCondition.Add(MST_BINTable.LOCATIONID_FLD, DestLocText.Tag.ToString());
            strWhere += MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + "=" + DestLocText.Tag;
            switch (intPurposeCode)
            {
                case (int) PurposeEnum.Scrap:
                    // Purpose= Xuat Huy then To Bin has Type=Destroy.
                    htbCondition.Add(MST_BINTable.BINTYPEID_FLD, (int) BinTypeEnum.LS);
                    break;
                case (int) PurposeEnum.Transfer:
                    // Purpose= NG to OK then To Bin has Type=OK.
                    htbCondition.Add(MST_BINTable.BINTYPEID_FLD, (int) BinTypeEnum.OK);
                    break;
                case (int) PurposeEnum.Outside:
                    // Purpose= Outside then To Bin has Type=BF.
                    htbCondition.Add(MST_BINTable.BINTYPEID_FLD, (int) BinTypeEnum.IN);
                    break;
                default:
                    // other purpose: To Bin cannot be Destroy
                    strWhere += " AND " + MST_BINTable.TABLE_NAME + "." + MST_BINTable.BINTYPEID_FLD + "<>" +
                                (int) BinTypeEnum.LS;
                    break;
            }
            if (intPurposeCode == (int) PurposeEnum.Scrap ||
                intPurposeCode == (int) PurposeEnum.Transfer ||
                intPurposeCode == (int) PurposeEnum.Outside)
                drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD,
                                                                 DestBinText.Text, htbCondition, pblnOpenForm);
            else
                drwResult = FormControlComponents.OpenSearchFormWithWhere(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD,
                                                                          DestBinText.Text, strWhere, pblnOpenForm);
            return drwResult;
        }

        /// <summary>
        /// Open search form for select party
        /// </summary>
        /// <param name="pintPurposeCode">Purpose</param>
        /// <param name="pstrField">Filter Field</param>
        /// <param name="pstrValue">Filter Value</param>
        /// <param name="pblnOpenForm">Open form or not</param>
        /// <returns>DataRowView</returns>
        private static DataRowView SearchParty(int pintPurposeCode, string pstrField, string pstrValue,
                                               bool pblnOpenForm)
        {
            DataRowView drowView;
            string strWhere = string.Empty;
            // CompensationForCustomer: Party type = Customer & Both
            if (pintPurposeCode == (int) PurposeEnum.CompensationForCustomer)
            {
                strWhere += MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.TYPE_FLD + "=" +
                            (int) PartyTypeEnum.CUSTOMER;
                strWhere += " OR " + MST_PartyTable.TABLE_NAME + "." + MST_PartyTable.TYPE_FLD + "=" +
                            (int) PartyTypeEnum.BOTH;
                drowView = FormControlComponents.OpenSearchFormWithWhere(MST_PartyTable.TABLE_NAME, pstrField, pstrValue,
                                                                         strWhere, pblnOpenForm);
            }
            else // otherwise display all type
                drowView = FormControlComponents.OpenSearchForm(MST_PartyTable.TABLE_NAME, pstrField, pstrValue, null,
                                                                pblnOpenForm);
            return drowView;
        }

        /// <summary>
        /// Fill Purpose data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillPurposeData(DataRowView pdrwResult)
        {
            PurposeText.Text = pdrwResult[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
            PurposeText.Tag = pdrwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];
            lblPurpose.Tag = pdrwResult[PRO_IssuePurposeTable.HASDESTINATION_FLD];
            // user has changed purpose
            if (_lastValidPurpose != PurposeText.Text)
            {
                DisableDestintion(Convert.ToInt32(PurposeText.Tag));
                ChangePurpose();
                // assign new valid purpose
                _lastValidPurpose = PurposeText.Text;
            }
        }

        /// <summary>
        /// Fill master location data based on search result
        /// </summary>
        /// <param name="pdrvData">Search result</param>
        /// <param name="pblnIsSource">true: Source. false: Destination</param>
        private void FillMasterLocationData(DataRowView pdrvData, bool pblnIsSource)
        {
            if (pblnIsSource)
                FillSourceMasterLocationData(pdrvData);
            else
                FillDestMasterLocationData(pdrvData);
        }

        /// <summary>
        /// Fill location data based on search result
        /// </summary>
        /// <param name="pdrvData">Search result</param>
        /// <param name="pblnIsSource">true: Source. false: Destination</param>
        private void FillLocationData(DataRowView pdrvData, bool pblnIsSource)
        {
            if (pblnIsSource)
                FillSourceLocationData(pdrvData);
            else
                FillDestLocationData(pdrvData);
        }

        /// <summary>
        /// Fill bin data based on search result
        /// </summary>
        /// <param name="pdrvData">Search result</param>
        /// <param name="pblnIsSource">true: Source. false: Destination</param>
        private void FillBinData(DataRowView pdrvData, bool pblnIsSource)
        {
            if (pblnIsSource)
                FillSourceBinData(pdrvData);
            else
                FillDestBinData(pdrvData);
        }

        /// <summary>
        /// Fill Source Master Location data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillSourceMasterLocationData(DataRowView pdrwResult)
        {
            SourceMasLocText.Text = pdrwResult[MST_MasterLocationTable.CODE_FLD].ToString();
            SourceMasLocText.Tag = int.Parse(pdrwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
            // user has changed source master location
            if (_lastValidSouceMasLoc != SourceMasLocText.Text)
            {
                // clear data
                ClearFormIfSourceMasLocChange();
                // assign new valid source master location
                _lastValidSouceMasLoc = SourceMasLocText.Text;
            }
        }

        /// <summary>
        /// Fill destination master location data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillDestMasterLocationData(DataRowView pdrwResult)
        {
            DestMasLocText.Text = pdrwResult[MST_MasterLocationTable.CODE_FLD].ToString();
            DestMasLocText.Tag = int.Parse(pdrwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
            lblDestLoc.ForeColor = Color.Maroon;
            // user has changed destination master location
            if (_lastValidDestMasLoc != DestMasLocText.Text)
            {
                // clear data
                ClearFormIfDestMasLocChange();
                // assign new valid dest. master location
                _lastValidDestMasLoc = DestMasLocText.Text;
            }
        }

        /// <summary>
        /// Fill source location data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillSourceLocationData(DataRowView pdrwResult)
        {
            SourceLocText.Text = pdrwResult[MST_LocationTable.CODE_FLD].ToString();
            SourceLocText.Tag = int.Parse(pdrwResult[MST_LocationTable.LOCATIONID_FLD].ToString());
            bool blnBinControl = Convert.ToBoolean(pdrwResult[MST_LocationTable.BIN_FLD]);
            lblSourceLoc.Tag = blnBinControl;
            SourceBinText.Enabled = blnBinControl;
            SourceBinButton.Enabled = blnBinControl;
            // user has changed source location
            if (_lastValidSourceLoc != SourceLocText.Text)
            {
                // clear data
                ClearFormIfSourceLocChange();
                // assign new valid source location
                _lastValidSourceLoc = SourceLocText.Text;
            }
        }

        /// <summary>
        /// Fill destination location data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillDestLocationData(DataRowView pdrwResult)
        {
            DestLocText.Text = pdrwResult[MST_LocationTable.CODE_FLD].ToString();
            DestLocText.Tag = int.Parse(pdrwResult[MST_LocationTable.LOCATIONID_FLD].ToString());
            bool blnBinControl = Convert.ToBoolean(pdrwResult[MST_LocationTable.BIN_FLD]);
            lblDestLoc.Tag = blnBinControl;
            DestBinText.Enabled = blnBinControl;
            DestBinButton.Enabled = blnBinControl;
            // user has changed destination location
            if (_lastValidDestLoc != DestLocText.Text)
            {
                // clear data
                ClearFormIfDestLocChange();
                // assign new valid data
                _lastValidDestLoc = DestLocText.Text;
            }
        }

        /// <summary>
        /// Fill source bin data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>	
        private void FillSourceBinData(DataRowView pdrwResult)
        {
            SourceBinText.Text = pdrwResult[MST_BINTable.CODE_FLD].ToString();
            SourceBinText.Tag = int.Parse(pdrwResult[MST_BINTable.BINID_FLD].ToString());
            if (_lastValidSourceBin != SourceBinText.Text)
            {
                // update grid
                UpdateGridWhenChangeSouceBin(Convert.ToInt32(SourceBinText.Tag));
                // assign new valid source bin
                _lastValidSourceBin = SourceBinText.Text;
            }
        }

        /// <summary>
        /// Fill destination bin data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillDestBinData(DataRowView pdrwResult)
        {
            _lastValidDestBin = DestBinText.Text = pdrwResult[MST_BINTable.CODE_FLD].ToString();
            DestBinText.Tag = int.Parse(pdrwResult[MST_BINTable.BINID_FLD].ToString());
        }

        /// <summary>
        /// Fill Party data based on search result
        /// </summary>
        /// <param name="pdrwResult">Search result</param>
        private void FillPartyData(DataRowView pdrwResult)
        {
            _lastValidPartyCode = PartyCodeText.Text = pdrwResult[MST_PartyTable.CODE_FLD].ToString();
            _lastValidPartyName = PartyNameText.Text = pdrwResult[MST_PartyTable.NAME_FLD].ToString();
            PartyCodeText.Tag = pdrwResult[MST_PartyTable.PARTYID_FLD];
            // clear master location, location, bin if any
            DestMasLocText.Tag = null;
            _lastValidDestMasLoc = DestMasLocText.Text = string.Empty;
            DestLocText.Tag = null;
            _lastValidDestLoc = DestLocText.Text = string.Empty;
            DestBinText.Tag = null;
            _lastValidDestBin = DestBinText.Text = string.Empty;
        }

        #endregion
    }
}
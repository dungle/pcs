using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using PCSComProduction.WorkOrder.BO;
using PCSComProduction.WorkOrder.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataContext;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
using PRO_WorkingTime = PCSComUtils.Common.PRO_WorkingTime;

namespace PCSProduction.WorkOrder
{
    /// <summary>
    /// Summary description for WorkOrderCompletion.
    /// </summary>
    public class WorkOrderCompletion : Form
    {
        private const string VWorkOrderCompletion = "v_WorkOrderCompletion";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components;

        private Button btnAdd;
        private Button btnClose;
        private Button btnHelp;
        private Button btnPrint;
        private Button btnPurpose;
        private Button btnSave;
        private Button btnSearchBin;

        private Button btnSearchCompletionNo;
        private Button btnSearchLocation;
        private Button btnSearchMasLoc;
        private Button btnSearchWO;
        private Button btnSearchWOLine;
        private Button btnShift;
        private C1Combo cboCCN;
        private ComboBox cboQAStatus;
        private C1DateEdit dtmPostDate;
        private GroupBox grpMoveCompleteMaterial;
        private int intProductionLineID;
        private Label lblBin;
        private Label lblCCN;
        private Label lblCompletedQty;
        private Label lblLocation;
        private Label lblLot;
        private Label lblModel;
        private Label lblPartName;
        private Label lblPartNumber;
        private Label lblPostDate;
        private Label lblPurpose;
        private Label lblQAStatus;
        private Label lblRemark;
        private Label lblSerial;
        private Label lblShift;
        private Label lblUM;
        private Label lblWO;
        private Label lblWOLine;
        private TextBox txtBin;
        private C1NumericEdit txtCompletedQty;
        private TextBox txtCompletionNo;
        private TextBox txtItem;
        private TextBox txtLocation;
        private TextBox txtLot;
        private TextBox txtMasLoc;
        private TextBox txtModel;
        private TextBox txtPartName;
        private TextBox txtPurpose;
        private TextBox txtRemark;
        private TextBox txtSerial;
        private TextBox txtShift;
        private TextBox txtUM;
        private TextBox txtWO;
        private TextBox txtWOLine;

        #region My variable

        protected const string THIS = "PCSProduction.WorkOrder.WorkOrderCompletion";

        private const string REPORTFLD_TITLE = "fldTitle";

        private const string CAPTION_UM = "UM";
        private const string LOCATION_CODE = "LocationCode";
        private const string BIN_CODE = "BinCode";
        private const string V_WOCOMPLETION = "v_WOCompletion";
        private const string PARTNUMBER = "PartNumber";
        private const string PARTNAME = "PartName";
        private const string MODEL = "Model";
        private const string V_WORELEASE = "V_WOReleaseForCompletion";

        private readonly MST_MasterLocationVO voMasLoc = new MST_MasterLocationVO();
        private bool blnHasError;
        private bool blnIsLotControl;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnPrintBOMShortage;
        private C1DateEdit dtmWOCompletionDate;
        private EnumAction formMode;
        private int intMasterLocationDefaultID;
        private int intTransTypeID;
        private Label lblCompletionNO;
        private Label lblDate;
        private Label lblLabelPostDate;
        private Label lblMasLoc;
        private Label lblWOCompletionDate;
        private decimal pdecQuantity;
        private int pintLotSize;
        private int pintProductID;

        #endregion My variable		

        public WorkOrderCompletion()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        public WorkOrderCompletion(bool blnClose)
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

        /// <summary>
        /// WorkOrderCompletion_Load
        /// </summary>
        /// <author>Trada</author>
        /// <date>Thursday, June 16 2005</date>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkOrderCompletion_Load(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".WorkOrderCompletion_Load()";
            try
            {
                //Set authorization for user
                var objSecurity = new Security();
                Name = THIS;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    Close();
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    return;
                }
                // Load combo box
                var dstCCN = Utilities.Instance.ListCCN();
                cboCCN.DataSource = dstCCN;
                cboCCN.DisplayMember = MST_CCNTable.CODE_FLD;
                cboCCN.ValueMember = MST_CCNTable.CCNID_FLD;
                FormControlComponents.PutDataIntoC1ComboBox(cboCCN, dstCCN,
                                                            MST_CCNTable.CODE_FLD, MST_CCNTable.CCNID_FLD,
                                                            MST_CCNTable.TABLE_NAME);
                if (SystemProperty.CCNID != 0)
                {
                    cboCCN.SelectedValue = SystemProperty.CCNID;
                }
                txtMasLoc.Focus();
                btnSave.Enabled = false;
                btnPrint.Enabled = false;

                //HACKED added by Tuan TQ. Enable/Disable print BOM shortage
                btnPrintBOMShortage.Enabled = false;
                //End hacked

                //Reset form
                formMode = EnumAction.Default;
                //Set default Master Location
                FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
                voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;

                //Set default for QA Status 
                cboQAStatus.SelectedIndex = (int) QAStatusEnum.QUALITY_ASSURED;

                //set format for datetime
                dtmPostDate.CustomFormat = Constants.DATETIME_FORMAT_HOUR;
                dtmWOCompletionDate.CustomFormat = Constants.DATETIME_FORMAT;
                //HACK: added by TuanTQ 03 Apr, 2006. Fix error no. 3627. 
                txtCompletedQty.FormatType = FormatTypeEnum.CustomFormat;
                txtCompletedQty.CustomFormat = Constants.INTERGER_NUMBERFORMAT;
                //End hack
                LockForm();
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// ResetForm
        /// </summary>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private void ResetForm()
        {
            dtmPostDate.Enabled = false;
            txtWO.Enabled = false;
            txtWOLine.Enabled = false;
            txtPartName.Enabled = false;
            txtModel.Enabled = false;
            txtItem.Enabled = false;
            txtLot.Enabled = false;
            txtSerial.Enabled = false;
            txtCompletedQty.Enabled = false;
            txtBin.Enabled = false;
            cboQAStatus.Enabled = false;
            txtLocation.Enabled = false;
            txtBin.Enabled = false;
            btnSearchBin.Enabled = false;
            btnSearchLocation.Enabled = false;
            btnSearchWO.Enabled = false;
            btnSearchWOLine.Enabled = false;

            //HACKED added by Tuan TQ
            txtShift.Enabled = false;
            btnShift.Enabled = false;
            txtPurpose.Enabled = false;
            btnPurpose.Enabled = false;
            txtRemark.Enabled = false;
            //End hacked
        }

        /// <summary>
        /// ClearForm
        /// </summary>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void ClearForm()
        {
            //txtMasLoc.Text = string.Empty;
            //txtCompletionNo.Text = string.Empty;
            txtWO.Text = string.Empty;
            txtWOLine.Text = string.Empty;
            txtWOLine.Tag = string.Empty;
            txtPartName.Text = string.Empty;
            txtModel.Text = String.Empty;
            txtItem.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtSerial.Text = string.Empty;
            txtCompletedQty.Value = null;
            txtBin.Text = string.Empty;
            //				cboQAStatus.SelectedIndex = 0;
            txtLocation.Text = string.Empty;
            txtBin.Text = string.Empty;
            txtUM.Text = string.Empty;
            if (formMode == EnumAction.Default)
            {
                txtCompletionNo.Text = string.Empty;
            }

            //HACKED by Tuan TQ. Add properties
            txtShift.Text = string.Empty;
            txtPurpose.Text = string.Empty;
            txtRemark.Text = string.Empty;

            txtShift.Tag = null;
            txtPurpose.Tag = null;
            //End hacked
        }

        /// <summary>
        /// InitVariable
        /// </summary>
        /// <author>Trada</author>
        /// <date>Thurday, June 16 2005</date>
        private void InitVariable()
        {
            // Load PostDate
            var voWorkOrderCompletion = new PRO_WorkOrderCompletionVO {PostDate = Utilities.Instance.GetServerDate()};
            if ((DateTime.MinValue < voWorkOrderCompletion.PostDate) &&
                (voWorkOrderCompletion.PostDate < DateTime.MaxValue))
                dtmPostDate.Value = voWorkOrderCompletion.PostDate;
            else
                dtmPostDate.Value = DBNull.Value;
            //Fill Completion Number
            txtCompletionNo.Text = FormControlComponents.GetNoByMask(this);
            //Set focus to PostDate
            dtmPostDate.Enabled = true;
            dtmPostDate.Focus();
        }

        /// <summary>
        /// btnAdd_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnAdd_Click()";
            try
            {
                formMode = EnumAction.Add;
                ClearForm();
                dtmPostDate.Value = (new UtilsBO()).GetDBDate();
                lblDate.Text = dtmPostDate.Text;
                txtMasLoc.Text = string.Empty;

                InitVariable();
                //Fill Default Master Location 
                FormControlComponents.SetDefaultMasterLocation(txtMasLoc);
                voMasLoc.MasterLocationID = SystemProperty.MasterLocationID;
                LockForm();
                dtmWOCompletionDate.Value = null;
                //set default for QA Status 
                cboQAStatus.SelectedIndex = (int) QAStatusEnum.QUALITY_ASSURED;

                //fill Purpose Default
                intTransTypeID =
                    new UtilsBO().GetTransTypeIDByCode(TransactionTypeEnum.PROWorkOrderCompletion.ToString());
                DataTable dtbPurpose = new UtilsBO().GetPurposeByTransTypeID(intTransTypeID);
                if (dtbPurpose.Rows.Count == 1)
                {
                    txtPurpose.Text = dtbPurpose.Rows[0][PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                    txtPurpose.Tag = (int) dtbPurpose.Rows[0][PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnSave_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private void btnSave_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSave_Click()";
            try
            {
                if (PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIRM_BEFORE_SAVE_WOCOMPLETION, MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                                       new[] {dtmPostDate.Text}) == DialogResult.Yes)
                {
                    blnHasError = true;

                    if (Security.IsDifferencePrefix(this, lblCompletionNO, txtCompletionNo))
                    {
                        return;
                    }

                    if (IsValidateData())
                    {
                        var voWOCompletion = new PRO_WorkOrderCompletion();
                        voWOCompletion.ShiftID = int.Parse(txtShift.Tag.ToString());
                        voWOCompletion.IssuePurposeID = int.Parse(txtPurpose.Tag.ToString());
                        voWOCompletion.Remark = txtRemark.Text.Trim();
                        voWOCompletion.PostDate = DateTime.Parse(dtmPostDate.Value.ToString());
                        voWOCompletion.MasterLocationID = voMasLoc.MasterLocationID;
                        voWOCompletion.WOCompletionNo = txtCompletionNo.Text;
                        if (formMode == EnumAction.Edit)
                        {
                            voWOCompletion.WorkOrderCompletionID = int.Parse(lblCompletionNO.Tag.ToString());
                        }
                        voWOCompletion.WorkOrderMasterID = int.Parse(txtWO.Tag.ToString());
                        voWOCompletion.WorkOrderDetailID = int.Parse(txtWOLine.Tag.ToString());
                        voWOCompletion.ProductID = pintProductID;
                        voWOCompletion.LocationID = int.Parse(txtLocation.Tag.ToString());
                        voWOCompletion.CCNID = int.Parse(cboCCN.SelectedValue.ToString());
                        voWOCompletion.CompletedQuantity = Decimal.Parse(txtCompletedQty.Value.ToString());
                        voWOCompletion.StockUMID = int.Parse(txtUM.Tag.ToString());
                        voWOCompletion.QAStatus = Convert.ToByte(cboQAStatus.SelectedIndex);
                        if (blnIsLotControl)
                        {
                            voWOCompletion.Lot = txtLot.Text;
                            voWOCompletion.Serial = txtSerial.Text;
                        }
                        else
                        {
                            voWOCompletion.Lot = string.Empty;
                            voWOCompletion.Serial = string.Empty;
                        }
                        if (txtBin.Text != string.Empty)
                        {
                            voWOCompletion.BinID = (int) txtBin.Tag;
                        }
                        else
                        {
                            voWOCompletion.BinID = null;
                        }
                        if (dtmWOCompletionDate.Value != null && dtmWOCompletionDate.Value != DBNull.Value)
                        {
                            voWOCompletion.CompletedDate = (DateTime)dtmWOCompletionDate.Value;
                        }
                        else
                        {
                            voWOCompletion.CompletedDate = FillWOCompletionDate(voWOCompletion.PostDate, intProductionLineID);
                        }

                        var boWOCompletion = new WorkOrderCompletionBO();
                        if (formMode == EnumAction.Add)
                        {
                            voWOCompletion.WorkOrderCompletionID = boWOCompletion.AddNew(voWOCompletion, intProductionLineID);
                        }
                        if (formMode == EnumAction.Edit)
                        {
                            boWOCompletion.EditWorkOrder(voWOCompletion);
                        }
                        Security.UpdateUserNameModifyTransaction(this, PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD, voWOCompletion.WorkOrderCompletionID);
                        lblCompletionNO.Tag = voWOCompletion.WorkOrderCompletionID;

                        PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                        formMode = EnumAction.Default;
                        ResetForm();
                        LockForm();
                        btnAdd.Focus();
                    }
                }
            }
            catch (PCSException ex)
            {
                //HACKED: Added & Modified by Tuan TQ. 04 Jan, 2006: show bom shortage report
                if (ex.mCode == ErrorCode.MESSAGE_NOT_ENOUGH_COMPONENT_TO_COMPLETE)
                {
                    txtCompletedQty.Focus();
                    txtCompletedQty.SelectAll();
                    if (!dtmPostDate.Enabled)
                    {
                        if (PCSMessageBox.Show(ex.mCode, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
                            DialogResult.Yes)
                        {
                            ShowBOMShortageReport(null, null);
                        }
                    }
                    else
                    {
                        var strParam = new string[1];
                        strParam[0] = ex.mMethod;
                        //Show message
                        PCSMessageBox.Show(ErrorCode.MESSAGE_NOT_ENOUGH_QUANTITY_OF_COMPONENT_TO_COMPLETE,
                                           MessageBoxIcon.Warning, strParam);
                    }
                    //End hacked.
                }
                else
                {
                    // Displays the error message if throwed from PCSException.
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);

                    //Check if WOCompletionNo was duplicated
                    if (ex.mCode == ErrorCode.DUPLICATE_KEY)
                    {
                        txtCompletionNo.Focus();
                        txtCompletionNo.Select();
                    }
                }

                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// btnClose_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// WorkOrderCompletion_Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Tuesday, October 25 2005</date>
        private void WorkOrderCompletion_Closing(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".WorkOrderCompletion_Closing()";
            try
            {
                if (formMode != EnumAction.Default)
                {
                    DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,
                                                                    MessageBoxButtons.YesNoCancel,
                                                                    MessageBoxIcon.Question);
                    switch (confirmDialog)
                    {
                        case DialogResult.Yes:
                            //Save before exit
                            btnSave_Click(btnSave, new EventArgs());
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnSearchCompletionNo_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void btnSearchCompletionNo_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchCompletionNo_Click()";
            try
            {
                if (formMode == EnumAction.Default)
                {
                    var htbCriteria = new Hashtable();
                    DataRowView drwResult = null;
                    //User has enter MasLoc
                    if (txtMasLoc.Text != string.Empty)
                    {
                        htbCriteria.Add(PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD,
                                        voMasLoc.MasterLocationID.ToString());
                    }
                    else //User has not entered MasLoc
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Error);
                        txtMasLoc.Focus();
                        return;
                    }
                    drwResult = FormControlComponents.OpenSearchForm(VWorkOrderCompletion,
                                                                     PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD,
                                                                     txtCompletionNo.Text.Trim(), htbCriteria, true);
                    if (drwResult != null)
                    {
                        txtCompletionNo.Text = drwResult[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].ToString();
                        //Keep value of WOCompletionID and ProductID
                        lblCompletionNO.Tag = drwResult[PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD];
                        txtCompletionNo.Tag = drwResult[PRO_WorkOrderCompletionTable.PRODUCTID_FLD];
                        FillDataToControlsForView(int.Parse(lblCompletionNO.Tag.ToString()));
                        voMasLoc.MasterLocationID =
                            int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                        txtWO.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
                        txtWOLine.Tag = drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
                        txtUM.Tag = drwResult[PRO_WorkOrderDetailTable.STOCKUMID_FLD];
                        txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
                        txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
                        pintProductID = (int) drwResult[ITM_ProductTable.PRODUCTID_FLD];
                        intMasterLocationDefaultID =
                            int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());

                        //reset Modified status
                        txtCompletionNo.Modified = false;
                        LockForm();
                    }
                    else
                    {
                        txtCompletionNo.Focus();
                        return;
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
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnSearchMasLoc_Click
        /// </summary>
        /// <author>Trada</author>
        /// <date>Thursday, June 16 2005</date>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchMasLoc_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchMasLoc_Click()";
            try
            {
                var htbCriteria = new Hashtable();
                DataRowView drwResult = null;
                //User has selected CCN
                if (cboCCN.SelectedIndex != -1)
                {
                    htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedValue);
                }
                else //User has not selected CCN
                {
                    htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, 0);
                }

                drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                 MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text.Trim(),
                                                                 htbCriteria, true);
                if (drwResult != null)
                {
                    if (voMasLoc.MasterLocationID !=
                        int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
                    {
                        voMasLoc.MasterLocationID =
                            int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                        ClearForm();
                    }
                    txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();

                    //reset Modified status
                    txtMasLoc.Modified = false;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// FillWOCompletionDate
        /// </summary>
        /// <param name="pdtmPostDate"></param>
        /// <param name="pintProductionLineID"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, July 19 2006</date>
        private DateTime FillWOCompletionDate(DateTime pdtmPostDate, int pintProductionLineID)
        {
            var boWorkOrderCompletion = new WorkOrderCompletionBO();
            var dstWorkingTime = new DataSet();
            int intYear = pdtmPostDate.Year;
            int intMonth = pdtmPostDate.Month;
            var dtmWOCompDate = new DateTime();
            dstWorkingTime = boWorkOrderCompletion.GetWorkingTimeByProductionLineAndYearMonth(pintProductionLineID,
                                                                                              intYear, intMonth);
            if (dstWorkingTime.Tables[0].Rows.Count == 1)
            {
                var dtmStartTimeFromDB = (DateTime) dstWorkingTime.Tables[0].Rows[0][PRO_WorkingTime.STARTTIME_FLD];
                //Build StartTime and EndTime 
                var dtmStartTime = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day,
                                                dtmStartTimeFromDB.Hour, dtmStartTimeFromDB.Minute, 0);
                //DateTime dtmEndTime = dtmStartTime.AddHours(int.Parse(dstWorkingTime.Tables[0].Rows[0][PRO_WorkingTime.WORKINGHOURS_FLD].ToString()));	
                DateTime dtmEndTime = dtmStartTime.AddHours(24);
                if (pdtmPostDate >= dtmStartTime && pdtmPostDate <= dtmEndTime)
                {
                    dtmWOCompDate = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day, 0, 0, 0);
                }
                else
                {
                    dtmWOCompDate = pdtmPostDate.AddDays(-1);
                    dtmWOCompDate = new DateTime(dtmWOCompDate.Year, dtmWOCompDate.Month, dtmWOCompDate.Day);
                }
                dtmWOCompletionDate.Value = dtmWOCompDate;
                return dtmWOCompDate;
            }
            else
            {
                var dtmStartTimeFromDB = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day, 6, 15, 0);
                var dtmStartTime = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day,
                                                dtmStartTimeFromDB.Hour, dtmStartTimeFromDB.Minute, 0);
                if (pdtmPostDate >= dtmStartTime)
                {
                    dtmWOCompDate = new DateTime(pdtmPostDate.Year, pdtmPostDate.Month, pdtmPostDate.Day, 0, 0, 0);
                }
                else
                {
                    dtmWOCompDate = pdtmPostDate.AddDays(-1);
                    dtmWOCompDate = new DateTime(dtmWOCompDate.Year, dtmWOCompDate.Month, dtmWOCompDate.Day);
                }
                dtmWOCompletionDate.Value = dtmWOCompDate;

                return dtmWOCompDate;
            }
        }

        /// <summary>
        /// btnSearchWO_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Monday, June 20 2005</date>
        private void btnSearchWO_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchWO_Click()";
            try
            {
                var htbCriteria = new Hashtable();
                //User has entered MasLoc
                if (txtMasLoc.Text != string.Empty)
                    htbCriteria.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());
                else //User has not entered MasLoc
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Error);
                    txtMasLoc.Focus();
                    return;
                }

                htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int) WOLineStatus.Released);
                var drwResult = FormControlComponents.OpenSearchForm(V_WORELEASE, PRO_WorkOrderMasterTable.WORKORDERNO_FLD,
                                                                 txtWO.Text.Trim(), htbCriteria, true);
                if (drwResult != null)
                {
                    intProductionLineID = (int) drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];

                    if (!drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Equals(txtWO.Tag))
                    {
                        txtWO.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
                        ClearFormWhenChangeWOLine();
                        dtmWOCompletionDate.Value = null;
                        txtWOLine.Text = string.Empty;
                    }
                    if (dtmPostDate.Value != DBNull.Value && dtmPostDate.Value != null)
                    {
                        //Fill WOCompletionDate
                        FillWOCompletionDate((DateTime)dtmPostDate.Value, intProductionLineID);
                    }
                    txtWO.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();

                    //reset Modified status
                    txtWO.Modified = false;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// ClearFormWhenChangeWOLine
        /// </summary>
        /// <author>Trada</author>
        /// <date>Thursday, June 23 2005</date>
        private void ClearFormWhenChangeWOLine()
        {
            txtPartName.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtUM.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtSerial.Text = string.Empty;
            txtBin.Text = string.Empty;
            txtCompletedQty.Value = null;
        }

        /// <summary>
        /// btnSearchLocation_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private void btnSearchLocation_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchLocation_Click()";
            try
            {
                SelectLocation(true);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnSearchBin_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tuan TQ</author>
        /// <date>Apr 03, 2006</date>
        private void btnSearchBin_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchBin_Click()";
            try
            {
                SelectBin(true);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// btnSearchWOLine_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void btnSearchWOLine_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnSearchWOLine_Click()";
            try
            {
                var htbCriteria = new Hashtable();
                DataRowView drwResult = null;
                //User has entered Work Order Master
                if (txtWO.Text != string.Empty)
                {
                    htbCriteria.Add(PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD, txtWO.Tag.ToString());
                    htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int) WOLineStatus.Released);
                }
                else //User has not entered Work Order Master
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_INPUT_WORKORDER_FIRST, MessageBoxIcon.Error);
                    txtWO.Focus();

                    //HACKED added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = false;
                    //End hacked
                    return;
                }

                drwResult = FormControlComponents.OpenSearchForm(V_WOCOMPLETION, PRO_WorkOrderDetailTable.LINE_FLD,
                                                                 txtWOLine.Text.Trim(), htbCriteria, true);
                if (drwResult != null)
                {
                    if (!drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Equals(txtWOLine.Tag))
                    {
                        txtWOLine.Tag = drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
                        ClearFormWhenChangeWOLine();
                    }

                    txtWOLine.Text = drwResult[PRO_WorkOrderDetailTable.LINE_FLD].ToString();

                    //HACKED added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = true;
                    //End hacked

                    //reset Modified status
                    txtWOLine.Modified = false;

                    pintProductID = int.Parse(drwResult[PRO_WorkOrderDetailTable.PRODUCTID_FLD].ToString());

                    //FillDataToControls
                    txtItem.Text = drwResult[PARTNUMBER].ToString();
                    txtPartName.Text = drwResult[PARTNAME].ToString();
                    txtModel.Text = drwResult[MODEL].ToString();

                    //Fill data to OpenQuantity
                    decimal pdecComplettedQuantity, pdecScrapQuantity;
                    pdecComplettedQuantity = drwResult[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].ToString() !=
                                             string.Empty ? decimal.Parse(drwResult[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].ToString()) : 0;
                    pdecScrapQuantity = drwResult[PRO_AOScrapDetailTable.SCRAPQUANTITY_FLD].ToString() != string.Empty
                                            ? decimal.Parse(drwResult[PRO_AOScrapDetailTable.SCRAPQUANTITY_FLD].ToString()) : 0;
                    pdecQuantity = decimal.Parse(drwResult[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].ToString()) -
                                   pdecComplettedQuantity - pdecScrapQuantity;
                    txtCompletedQty.Value = pdecQuantity;
                    //Fill data to UM label
                    txtUM.Text = drwResult[CAPTION_UM].ToString();
                    //Keep value of StockUMID
                    txtUM.Tag = drwResult[PRO_WorkOrderDetailTable.STOCKUMID_FLD];
                    //txtLOT.Enable = Product.LotControl (true/false)
                    txtLot.Enabled = bool.Parse(drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString());
                    txtSerial.Enabled = bool.Parse(drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString());
                    blnIsLotControl = bool.Parse(drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString());
                    //Set value for pintLotSize
                    pintLotSize = drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString() == string.Empty ? 0 : int.Parse(drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString());
                    //Save MasterLocationID default
                    intMasterLocationDefaultID = drwResult[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString() != string.Empty ? int.Parse(drwResult[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString()) : 0;
                    //Fill default value for Location and Bin
                    if (drwResult[LOCATION_CODE].ToString() != string.Empty)
                    {
                        txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
                        txtLocation.Text = drwResult[LOCATION_CODE].ToString();
                    }
                    else
                    {
                        txtLocation.Tag = null;
                        txtLocation.Text = string.Empty;
                    }
                    if (drwResult[MST_LocationTable.BIN_FLD].ToString() != string.Empty)
                    {
                        if (bool.Parse(drwResult[MST_LocationTable.BIN_FLD].ToString())
                            && drwResult[MST_BINTable.BINTYPEID_FLD].ToString() != string.Empty)
                        {
                            if ((int) drwResult[MST_BINTable.BINTYPEID_FLD] == (int) BinTypeEnum.OK
                                || (int) drwResult[MST_BINTable.BINTYPEID_FLD] == (int) BinTypeEnum.NG)
                            {
                                txtBin.Text = drwResult[BIN_CODE].ToString();
                                txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
                            }
                        }
                        txtBin.Enabled = bool.Parse(drwResult[MST_LocationTable.BIN_FLD].ToString());
                        btnSearchBin.Enabled = bool.Parse(drwResult[MST_LocationTable.BIN_FLD].ToString());
                    }
                }
                else
                {
                    txtWOLine.Focus();
                    //HACKED added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = false;
                    //End hacked
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtMasLoc_Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private void txtMasLoc_Leave(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtMasLoc_Leave()";
            try
            {
                if (!txtMasLoc.Modified) return;
                if (txtMasLoc.Text.Trim() == string.Empty)
                {
                    ClearForm();
                    if (btnAdd.Enabled)
                    {
                        dtmPostDate.Value = null;
                    }
                    return;
                }
                var htbCriteria = new Hashtable();
                //User has selected CCN
                htbCriteria.Add(MST_MasterLocationTable.CCNID_FLD, cboCCN.SelectedIndex != -1 ? cboCCN.SelectedValue : 0);
                var drwResult = FormControlComponents.OpenSearchForm(MST_MasterLocationTable.TABLE_NAME,
                                                                 MST_MasterLocationTable.CODE_FLD, txtMasLoc.Text,
                                                                 htbCriteria, false);
                if (drwResult != null)
                {
                    if (voMasLoc.MasterLocationID !=
                        int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString()))
                    {
                        voMasLoc.MasterLocationID =
                            int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                        ClearForm();
                    }
                    txtMasLoc.Text = drwResult[MST_MasterLocationTable.CODE_FLD].ToString();

                    //reset Modified status
                    txtMasLoc.Modified = false;
                }
                else
                {
                    txtMasLoc.Focus();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Validate data
        /// </summary>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Tuesday, June 21 2005</date>
        private bool IsValidateData()
        {
            if (formMode == EnumAction.Add)
            {
                //Check Mandatory
                if (FormControlComponents.CheckMandatory(dtmPostDate))
                {
                    PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                    dtmPostDate.Focus();
                    dtmPostDate.Select();
                    return false;
                }

                //Check date in period
                if (!FormControlComponents.CheckDateInCurrentPeriod(DateTime.Parse(dtmPostDate.Value.ToString())))
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_RGV_POSTDATE_PERIOD, MessageBoxIcon.Exclamation);
                    dtmPostDate.Focus();
                    dtmPostDate.Select();
                    return false;
                }
                //check the PostDate smaller than the current date
                if ((DateTime)dtmPostDate.Value > new UtilsBO().GetDBDate())
                {
                    //MessageBox.Show("The Post Date you input is not in the current period");
                    PCSMessageBox.Show(ErrorCode.MESSAGE_INV_TRANSACTION_CANNOT_IN_FUTURE, MessageBoxIcon.Warning);
                    dtmPostDate.Focus();
                    return false;
                }
            }
            if (FormControlComponents.CheckMandatory(cboCCN))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                cboCCN.Focus();
                cboCCN.Select();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtMasLoc))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtMasLoc.Focus();
                txtMasLoc.Select();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtCompletionNo))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtCompletionNo.Focus();
                txtCompletionNo.Select();
                return false;
            }


            if (FormControlComponents.CheckMandatory(txtWO))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtWO.Focus();
                txtWO.Select();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtWOLine))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtWOLine.Focus();
                txtWOLine.Select();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtCompletedQty))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtCompletedQty.Focus();
                txtCompletedQty.Select();
                return false;
            }

            if (FormControlComponents.CheckMandatory(cboQAStatus))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                cboQAStatus.Focus();
                cboQAStatus.Select();
                return false;
            }

            //HACK: added by Tuan TQ. 09 Dec, 2005
            if (FormControlComponents.CheckMandatory(txtShift))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtShift.Focus();
                txtShift.Select();
                return false;
            }
            //End hacked

            if (FormControlComponents.CheckMandatory(txtPurpose))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtPurpose.Focus();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtLocation))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtLocation.Focus();
                txtLocation.Select();
                return false;
            }

            if (FormControlComponents.CheckMandatory(txtBin))
            {
                PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxIcon.Exclamation);
                txtBin.Focus();
                return false;
            }

            //If Lot != string.empty then check for Completed Quantity <= LOT Size
            if (txtLot.Text != string.Empty)
            {
                if (Decimal.Parse(txtCompletedQty.Value.ToString()) > pintLotSize)
                {
                    //MessageBox.Show(pintLotSize.ToString());
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_COMPQTY_MUST_SMALLER_LOTSIZE,
                                       MessageBoxIcon.Exclamation);
                    txtCompletedQty.Focus();
                    txtCompletedQty.Select();
                    return false;
                }
            }

            //If Serial != string.empty then check for Completed Quantity == 1
            if (txtSerial.Text != string.Empty)
            {
                if (Decimal.Parse(txtCompletedQty.Value.ToString()) != 1)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_COMPQTY_MUST_BE_ONE,
                                       MessageBoxIcon.Exclamation);
                    txtCompletedQty.Focus();
                    txtCompletedQty.Select();
                    return false;
                }
                //If user inputs Serial then user has to input Lot also.
                if (txtLot.Text.Trim() == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_PLS_INPUT_LOT, MessageBoxIcon.Exclamation);
                    txtLot.Focus();
                    txtLot.Select();
                    return false;
                }
            }

            //Display warning message if 0 < completed quantity 
            if (Decimal.Parse(txtCompletedQty.Value.ToString()) <= 0)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_COMPQTY_MUST_BE_GREATER_ZERO,
                                   MessageBoxIcon.Exclamation);
                txtCompletedQty.Focus();
                txtCompletedQty.Select();
                return false;
            }

            //Displays error message if the not enter lot number for item is required using lot control
            if (blnIsLotControl)
            {
                if (txtLot.Text == string.Empty)
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOCOMPLETION_PLS_INPUT_LOT, MessageBoxIcon.Exclamation);
                    txtLot.Focus();
                    txtLot.Select();
                    return false;
                }
            }
            
            //Check if Location doesn't belong to MasterLocation
            if (intMasterLocationDefaultID != voMasLoc.MasterLocationID)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_LOCATION_NOT_MATCH_WITH_MASLOC, MessageBoxIcon.Exclamation);
                txtLocation.Focus();
                return false;
            }
            //Check postdate in configuration
            if (!FormControlComponents.CheckPostDateInConfiguration((DateTime)dtmPostDate.Value))
                return false;
            return true;
        }

        /// <summary>
        /// txtCompletionNo_Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void txtCompletionNo_Leave(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCompletionNo_Leave()";
            try
            {
                if (!txtCompletionNo.Modified) return;

                if (txtCompletionNo.Text == string.Empty)
                {
                    ClearForm();
                    if (btnAdd.Enabled)
                    {
                        dtmPostDate.Value = null;
                    }

                    return;
                }
                if (formMode == EnumAction.Default)
                {
                    var htbCriteria = new Hashtable();
                    DataRowView drwResult = null;
                    //User has enter MasLoc
                    if (txtMasLoc.Text != string.Empty)
                    {
                        htbCriteria.Add(PRO_WorkOrderCompletionTable.MASTERLOCATIONID_FLD,
                                        voMasLoc.MasterLocationID.ToString());
                    }
                    else //User has not entered MasLoc
                    {
                        PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_MASTERLOCATION, MessageBoxIcon.Error);
                        txtCompletionNo.Text = string.Empty;
                        txtMasLoc.Focus();

                        return;
                    }

                    drwResult = FormControlComponents.OpenSearchForm(VWorkOrderCompletion,
                                                                     PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD,
                                                                     txtCompletionNo.Text.Trim(), htbCriteria, false);
                    if (drwResult != null)
                    {
                        txtCompletionNo.Text = drwResult[PRO_WorkOrderCompletionTable.WOCOMPLETIONNO_FLD].ToString();
                        //Keep valua of WOCompletionID and ProductID
                        lblCompletionNO.Tag = drwResult[PRO_WorkOrderCompletionTable.WORKORDERCOMPLETIONID_FLD];
                        txtCompletionNo.Tag = drwResult[PRO_WorkOrderCompletionTable.PRODUCTID_FLD];
                        FillDataToControlsForView(int.Parse(lblCompletionNO.Tag.ToString()));

                        //reset Modified status
                        txtCompletionNo.Modified = false;

                        //Hacked Canhnv
                        voMasLoc.MasterLocationID =
                            int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                        txtWO.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
                        txtWOLine.Tag = drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
                        txtUM.Tag = drwResult[PRO_WorkOrderDetailTable.STOCKUMID_FLD];
                        txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
                        txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
                        pintProductID = (int) drwResult[ITM_ProductTable.PRODUCTID_FLD];
                        intMasterLocationDefaultID =
                            int.Parse(drwResult[MST_MasterLocationTable.MASTERLOCATIONID_FLD].ToString());
                        //End hacked						
                    }
                    else
                    {
                        txtCompletionNo.Focus();
                    }
                    LockForm();
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// FillDataToControlsForView
        /// </summary>
        /// <param name="pintWOCompletionID"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void FillDataToControlsForView(int pintWOCompletionID)
        {
            var boWOCompletion = new WorkOrderCompletionBO();
            //Load information of WOCompletion
            var drowWOCompletion = boWOCompletion.GetWorkOrderCompletion(pintWOCompletionID);
            //Bind data to all controls
            dtmPostDate.Value = drowWOCompletion.PostDate;
            dtmWOCompletionDate.Value = drowWOCompletion.CompletedDate;
            txtWO.Text = drowWOCompletion.PRO_WorkOrderMaster.WorkOrderNo;
            txtWOLine.Text = drowWOCompletion.PRO_WorkOrderDetail.Line.ToString();
            txtWOLine.Tag = drowWOCompletion.WorkOrderDetailID;
            txtCompletedQty.Value = drowWOCompletion.CompletedQuantity;
            txtLocation.Text = drowWOCompletion.MST_Location.Code;
            txtBin.Text = drowWOCompletion.MST_BIN.Code;
            txtUM.Text = drowWOCompletion.MST_UnitOfMeasure.Code;

            var product = drowWOCompletion.ITM_Product;
            txtPartName.Text = product.Description;
            txtItem.Text = product.Code;
            txtModel.Text = product.Revision;

            txtShift.Text = drowWOCompletion.PRO_Shift.ShiftDesc;
            txtShift.Tag = drowWOCompletion.ShiftID;

            txtPurpose.Text = drowWOCompletion.PRO_IssuePurpose.Description;
            txtPurpose.Tag = drowWOCompletion.IssuePurposeID;
            txtRemark.Text = drowWOCompletion.Remark;
        }

        /// <summary>
        /// txtMasLoc_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void txtMasLoc_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtMasLoc_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4 && btnSearchMasLoc.Enabled)
                {
                    btnSearchMasLoc_Click(sender, e);
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtCompletionNo_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void txtCompletionNo_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtCompletionNo_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4 && btnSearchCompletionNo.Enabled)
                {
                    btnSearchCompletionNo_Click(sender, e);
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtWO_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void txtWO_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtWO_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4 && btnSearchWO.Enabled)
                {
                    btnSearchWO_Click(sender, e);
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtWOLine_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void txtWOLine_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtWOLine_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4 && btnSearchWOLine.Enabled)
                {
                    btnSearchWOLine_Click(sender, e);
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtLocation_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, June 22 2005</date>
        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtLocation_KeyDown()";
            try
            {
                if (e.KeyCode == Keys.F4 && btnSearchLocation.Enabled)
                {
                    SelectLocation(true);
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// txtBin_KeyDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Tuan TQ</author>
        /// <date>03 Apr 2006</date>
        private void txtBin_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtBin_KeyDown()";

            try
            {
                if ((e.KeyCode == Keys.F4) && (btnSearchBin.Enabled))
                {
                    SelectBin(true);
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <date>03 Apr 2006</date>
        private void txtBin_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtBin_Validating()";

            try
            {
                //exit if not in add action or empty
                if (!btnSave.Enabled) return;

                if (txtBin.Text.Trim().Length == 0)
                {
                    txtBin.Tag = null;
                    return;
                }
                else if (!txtBin.Modified)
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
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtLocation_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtLocation_Validating()";

            try
            {
                //exit if not in add action or empty
                if (!btnSave.Enabled) return;

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
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtWO_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtWO_Validating()";
            try
            {
                if (!txtWO.Modified) return;

                if (txtWO.Text == string.Empty)
                {
                    ClearFormWhenChangeWOLine();
                    dtmWOCompletionDate.Value = null;
                    txtWOLine.Text = string.Empty;
                    return;
                }

                DataRowView drwResult = null;
                var htbCriteria = new Hashtable();
                if (txtMasLoc.Text != string.Empty)
                {
                    htbCriteria.Add(PRO_WorkOrderMasterTable.MASTERLOCATIONID_FLD, voMasLoc.MasterLocationID.ToString());
                }
                else //User has not entered MasLoc
                {
                    e.Cancel = true;
                    return;
                }

                htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int) WOLineStatus.Released);
                drwResult = FormControlComponents.OpenSearchForm(V_WORELEASE, PRO_WorkOrderMasterTable.WORKORDERNO_FLD,
                                                                 txtWO.Text.Trim(), htbCriteria, false);
                if (drwResult != null)
                {
                    intProductionLineID = (int) drwResult[PRO_ProductionLineTable.PRODUCTIONLINEID_FLD];
                    if (!drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD].Equals(txtWO.Tag))
                    {
                        txtWO.Tag = drwResult[PRO_WorkOrderMasterTable.WORKORDERMASTERID_FLD];
                        ClearFormWhenChangeWOLine();
                        dtmWOCompletionDate.Value = null;
                        txtWOLine.Text = string.Empty;
                    }
                    if (dtmPostDate.Value != DBNull.Value && dtmPostDate.Value != null)
                    {
                        //Fill WOCompletionDate
                        FillWOCompletionDate((DateTime) dtmPostDate.Value, intProductionLineID);
                    }
                    txtWO.Text = drwResult[PRO_WorkOrderMasterTable.WORKORDERNO_FLD].ToString();

                    //reset Modified status
                    txtWO.Modified = false;
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
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void txtWOLine_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtWOLine_Validating()";
            try
            {
                var htbCriteria = new Hashtable();
                DataRowView drwResult = null;

                if (!txtWOLine.Modified) return;
                if (txtWOLine.Text == string.Empty)
                {
                    ClearFormWhenChangeWOLine();
                    txtWOLine.Text = string.Empty;

                    //HACKED added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = false;
                    //End hacked
                    return;
                }

                //User has entered Work Order Master
                if (txtWO.Text != string.Empty)
                {
                    htbCriteria.Add(PRO_WorkOrderDetailTable.WORKORDERMASTERID_FLD, txtWO.Tag);
                    htbCriteria.Add(PRO_WorkOrderDetailTable.STATUS_FLD, (int) WOLineStatus.Released);
                }
                else //User has not entered Work Order Master
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_WOROUTING_INPUT_WORKORDER_FIRST, MessageBoxIcon.Error);
                    txtWOLine.Text = string.Empty;
                    txtWO.Focus();

                    //HACKED added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = false;
                    //End hacked

                    return;
                }

                drwResult = FormControlComponents.OpenSearchForm(V_WOCOMPLETION, PRO_WorkOrderDetailTable.LINE_FLD,
                                                                 txtWOLine.Text.Trim(), htbCriteria, false);
                if (drwResult != null)
                {
                    if (!drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD].Equals(txtWOLine.Tag))
                    {
                        txtWOLine.Tag = drwResult[PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD];
                        ClearFormWhenChangeWOLine();
                    }

                    //HACK: added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = true;
                    //End hack

                    txtWOLine.Text = drwResult[PRO_WorkOrderDetailTable.LINE_FLD].ToString();
                    //reset Modified status
                    txtWOLine.Modified = false;

                    pintProductID = int.Parse(drwResult[PRO_WorkOrderDetailTable.PRODUCTID_FLD].ToString());
                    //FillDataToControls
                    txtItem.Text = drwResult[PARTNUMBER].ToString();
                    txtPartName.Text = drwResult[PARTNAME].ToString();
                    txtModel.Text = drwResult[MODEL].ToString();

                    //Fill data to OpenQuantity
                    var pdecComplettedQuantity = drwResult[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].ToString() !=
                                             string.Empty ? decimal.Parse(drwResult[PRO_WorkOrderCompletionTable.COMPLETEDQUANTITY_FLD].ToString()) : 0;
                    var pdecScrapQuantity = drwResult[PRO_AOScrapDetailTable.SCRAPQUANTITY_FLD].ToString() != string.Empty ? decimal.Parse(drwResult[PRO_AOScrapDetailTable.SCRAPQUANTITY_FLD].ToString()) : 0;

                    pdecQuantity = decimal.Parse(drwResult[PRO_WorkOrderDetailTable.ORDERQUANTITY_FLD].ToString()) -
                                   pdecComplettedQuantity - pdecScrapQuantity;
                    txtCompletedQty.Value = pdecQuantity;
                    //Fill data to UM label
                    txtUM.Text = drwResult[CAPTION_UM].ToString();
                    //Keep value of StockUMID
                    txtUM.Tag = drwResult[PRO_WorkOrderDetailTable.STOCKUMID_FLD];
                    txtLot.Enabled = bool.Parse(drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString());
                    txtSerial.Enabled = bool.Parse(drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString());
                    blnIsLotControl = bool.Parse(drwResult[ITM_ProductTable.LOTCONTROL_FLD].ToString());
                    //Set value for pintLotSize
                    pintLotSize = drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString() == string.Empty
                                      ? 0
                                      : int.Parse(drwResult[ITM_ProductTable.LOTSIZE_FLD].ToString());

                    //Save MasterLocationID default
                    intMasterLocationDefaultID = drwResult[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString() !=
                                                 string.Empty ? int.Parse(drwResult[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString()) : 0;
                    //Fill default value for Location and Bin
                    if (drwResult[LOCATION_CODE].ToString() != string.Empty)
                    {
                        txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];
                        txtLocation.Text = drwResult[LOCATION_CODE].ToString();
                    }
                    else
                    {
                        txtLocation.Tag = null;
                        txtLocation.Text = string.Empty;
                    }
                    if (drwResult[MST_LocationTable.BIN_FLD].ToString() != string.Empty)
                    {
                        if (bool.Parse(drwResult[MST_LocationTable.BIN_FLD].ToString())
                            && drwResult[MST_BINTable.BINTYPEID_FLD].ToString() != string.Empty)
                        {
                            txtBin.Enabled = true;
                            btnSearchBin.Enabled = true;
                            if ((int) drwResult[MST_BINTable.BINTYPEID_FLD] == (int) BinTypeEnum.OK
                                || (int) drwResult[MST_BINTable.BINTYPEID_FLD] == (int) BinTypeEnum.NG)
                            {
                                txtBin.Text = drwResult[BIN_CODE].ToString();
                                txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];
                            }
                        }
                        txtBin.Enabled = bool.Parse(drwResult[MST_LocationTable.BIN_FLD].ToString());
                        btnSearchBin.Enabled = bool.Parse(drwResult[MST_LocationTable.BIN_FLD].ToString());
                    }
                }
                else
                {
                    txtWOLine.Focus();

                    //HACK: added by Tuan TQ. Enable/Disable print BOM shortage
                    btnPrintBOMShortage.Enabled = false;
                    //End hack
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// dtmPostDate_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, July 19 2006</date>
        private void dtmPostDate_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dtmPostDate_Validating()";
            try
            {
                if (txtWO.Text.Trim() != string.Empty && dtmPostDate.Value != DBNull.Value)
                {
                    //Fill WOCompletionDate
                    FillWOCompletionDate((DateTime) dtmPostDate.Value, intProductionLineID);
                }
                else
                {
                    dtmWOCompletionDate.Value = null;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    // Show message if logger has an error.
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompletionNo.Text.Trim() != string.Empty)
                {
                    formMode = EnumAction.Edit;
                    LockForm();
                }
                else
                {
                    return;
                }
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ErrorCode.MSG_LABORTIME_SELECTWOMASTER, MessageBoxIcon.Information);
                return;
            }
        }

        private void LockForm()
        {
            switch (formMode)
            {
                case EnumAction.Add:
                    txtWO.Enabled = true;
                    txtWOLine.Enabled = true;
                    txtCompletedQty.Enabled = true;
                    txtLocation.Enabled = true;
                    cboQAStatus.Enabled = true;
                    txtShift.Enabled = true;
                    txtPurpose.Enabled = true;
                    txtRemark.Enabled = true;
                    txtBin.Enabled = true;
                    txtCompletionNo.Enabled = true;
                    txtMasLoc.Enabled = true;

                    dtmPostDate.Enabled = true;

                    btnPurpose.Enabled = true;
                    btnSearchLocation.Enabled = true;
                    btnSearchWO.Enabled = true;
                    btnSearchWOLine.Enabled = true;
                    btnSearchCompletionNo.Enabled = false;
                    btnShift.Enabled = true;
                    btnSearchBin.Enabled = true;
                    btnSearchWO.Enabled = true;

                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnPrint.Enabled = false;
                    btnPrintBOMShortage.Enabled = false;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = false;

                    break;
                case EnumAction.Edit:
                    txtCompletedQty.Enabled = true;
                    txtLocation.Enabled = true;
                    txtBin.Enabled = true;
                    txtCompletionNo.Enabled = false;
                    txtWO.Enabled = false;
                    txtWOLine.Enabled = false;
                    cboQAStatus.Enabled = false;
                    txtShift.Enabled = false;
                    txtPurpose.Enabled = false;
                    txtRemark.Enabled = true;
                    txtMasLoc.Enabled = false;

                    dtmPostDate.Enabled = false;

                    btnPurpose.Enabled = false;
                    btnShift.Enabled = false;
                    btnSearchLocation.Enabled = true;
                    btnSearchWO.Enabled = false;
                    btnSearchWOLine.Enabled = false;
                    btnSearchCompletionNo.Enabled = false;
                    btnSearchBin.Enabled = true;
                    btnSearchWO.Enabled = false;

                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnPrint.Enabled = false;
                    btnPrintBOMShortage.Enabled = false;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = false;

                    break;
                case EnumAction.Default:
                    txtWO.Enabled = false;
                    txtWOLine.Enabled = false;
                    txtCompletedQty.Enabled = false;
                    txtLocation.Enabled = false;
                    cboQAStatus.Enabled = false;
                    txtCompletionNo.Enabled = true;
                    txtShift.Enabled = false;
                    txtPurpose.Enabled = false;
                    txtRemark.Enabled = false;
                    txtBin.Enabled = false;

                    dtmPostDate.Enabled = false;

                    btnPurpose.Enabled = false;
                    btnShift.Enabled = false;
                    btnSearchLocation.Enabled = false;
                    btnSearchWO.Enabled = true;
                    btnSearchWOLine.Enabled = false;
                    btnSearchCompletionNo.Enabled = true;
                    btnSearchBin.Enabled = false;
                    btnSearchWO.Enabled = false;

                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    if (lblCompletionNO.Tag != null || txtCompletionNo.Text.Trim() != string.Empty)
                    {
                        btnEdit.Enabled = true;
                        btnPrint.Enabled = true;
                        btnPrintBOMShortage.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        btnPrint.Enabled = false;
                        btnPrintBOMShortage.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    break;
            }
            //End hacked			
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnDelete_Click()";
            if (txtCompletionNo.Text.Trim() == string.Empty)
            {
                return;
            }
            if (PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                try
                {
                    //call the BO class to delete
                    var objWOCompletionBO = new WorkOrderCompletionBO();
                    objWOCompletionBO.DeleteCompletion((int) lblCompletionNO.Tag);


                    //After deleting, clean environment
                    lblCompletionNO.Tag = null; //No product after deleting

                    //Turn to default action
                    formMode = EnumAction.Default;

                    //clear controls
                    ClearForm();

                    //lock form
                    LockForm();

                    //focus
                    txtCompletionNo.Focus();
                }
                catch (PCSException ex)
                {
                    // displays the error message.
                    PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                    // log message.
                    try
                    {
                        Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                        Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                    }
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
            this.btnSearchCompletionNo = new System.Windows.Forms.Button();
            this.grpMoveCompleteMaterial = new System.Windows.Forms.GroupBox();
            this.btnSearchBin = new System.Windows.Forms.Button();
            this.txtBin = new System.Windows.Forms.TextBox();
            this.lblBin = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnSearchLocation = new System.Windows.Forms.Button();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.lblCompletionNO = new System.Windows.Forms.Label();
            this.txtMasLoc = new System.Windows.Forms.TextBox();
            this.lblMasLoc = new System.Windows.Forms.Label();
            this.lblPostDate = new System.Windows.Forms.Label();
            this.lblCCN = new System.Windows.Forms.Label();
            this.lblWO = new System.Windows.Forms.Label();
            this.txtWO = new System.Windows.Forms.TextBox();
            this.lblWOLine = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.lblPartName = new System.Windows.Forms.Label();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblCompletedQty = new System.Windows.Forms.Label();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.lblLot = new System.Windows.Forms.Label();
            this.lblUM = new System.Windows.Forms.Label();
            this.lblQAStatus = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.btnSearchWO = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtCompletionNo = new System.Windows.Forms.TextBox();
            this.btnSearchMasLoc = new System.Windows.Forms.Button();
            this.txtWOLine = new System.Windows.Forms.TextBox();
            this.dtmPostDate = new C1.Win.C1Input.C1DateEdit();
            this.cboCCN = new C1.Win.C1List.C1Combo();
            this.cboQAStatus = new System.Windows.Forms.ComboBox();
            this.btnSearchWOLine = new System.Windows.Forms.Button();
            this.txtCompletedQty = new C1.Win.C1Input.C1NumericEdit();
            this.txtUM = new System.Windows.Forms.TextBox();
            this.btnShift = new System.Windows.Forms.Button();
            this.txtShift = new System.Windows.Forms.TextBox();
            this.lblShift = new System.Windows.Forms.Label();
            this.btnPurpose = new System.Windows.Forms.Button();
            this.txtPurpose = new System.Windows.Forms.TextBox();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.btnPrintBOMShortage = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblLabelPostDate = new System.Windows.Forms.Label();
            this.dtmWOCompletionDate = new C1.Win.C1Input.C1DateEdit();
            this.lblWOCompletionDate = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grpMoveCompleteMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dtmPostDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.cboCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.txtCompletedQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dtmWOCompletionDate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearchCompletionNo
            // 
            this.btnSearchCompletionNo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchCompletionNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchCompletionNo.Location = new System.Drawing.Point(211, 48);
            this.btnSearchCompletionNo.Name = "btnSearchCompletionNo";
            this.btnSearchCompletionNo.Size = new System.Drawing.Size(24, 20);
            this.btnSearchCompletionNo.TabIndex = 5;
            this.btnSearchCompletionNo.Text = "...";
            this.btnSearchCompletionNo.Click += new System.EventHandler(this.btnSearchCompletionNo_Click);
            // 
            // grpMoveCompleteMaterial
            // 
            this.grpMoveCompleteMaterial.Controls.Add(this.btnSearchBin);
            this.grpMoveCompleteMaterial.Controls.Add(this.txtBin);
            this.grpMoveCompleteMaterial.Controls.Add(this.lblBin);
            this.grpMoveCompleteMaterial.Controls.Add(this.lblLocation);
            this.grpMoveCompleteMaterial.Controls.Add(this.txtLocation);
            this.grpMoveCompleteMaterial.Controls.Add(this.btnSearchLocation);
            this.grpMoveCompleteMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grpMoveCompleteMaterial.Location = new System.Drawing.Point(6, 250);
            this.grpMoveCompleteMaterial.Name = "grpMoveCompleteMaterial";
            this.grpMoveCompleteMaterial.Size = new System.Drawing.Size(580, 58);
            this.grpMoveCompleteMaterial.TabIndex = 24;
            this.grpMoveCompleteMaterial.TabStop = false;
            this.grpMoveCompleteMaterial.Text = "Move Completed Material";
            // 
            // btnSearchBin
            // 
            this.btnSearchBin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchBin.Location = new System.Drawing.Point(380, 26);
            this.btnSearchBin.Name = "btnSearchBin";
            this.btnSearchBin.Size = new System.Drawing.Size(24, 20);
            this.btnSearchBin.TabIndex = 4;
            this.btnSearchBin.Text = "...";
            this.btnSearchBin.Click += new System.EventHandler(this.btnSearchBin_Click);
            // 
            // txtBin
            // 
            this.txtBin.Location = new System.Drawing.Point(280, 26);
            this.txtBin.Name = "txtBin";
            this.txtBin.Size = new System.Drawing.Size(98, 20);
            this.txtBin.TabIndex = 3;
            this.txtBin.Text = "";
            this.txtBin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBin_KeyDown);
            this.txtBin.Validating += new System.ComponentModel.CancelEventHandler(this.txtBin_Validating);
            // 
            // lblBin
            // 
            this.lblBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblBin.ForeColor = System.Drawing.Color.Maroon;
            this.lblBin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBin.Location = new System.Drawing.Point(252, 26);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(26, 20);
            this.lblBin.TabIndex = 2;
            this.lblBin.Text = "Bin";
            this.lblBin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocation
            // 
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblLocation.ForeColor = System.Drawing.Color.Maroon;
            this.lblLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLocation.Location = new System.Drawing.Point(22, 26);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(54, 20);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(83, 26);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(120, 20);
            this.txtLocation.TabIndex = 0;
            this.txtLocation.Text = "";
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
            this.txtLocation.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocation_Validating);
            // 
            // btnSearchLocation
            // 
            this.btnSearchLocation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchLocation.Location = new System.Drawing.Point(205, 26);
            this.btnSearchLocation.Name = "btnSearchLocation";
            this.btnSearchLocation.Size = new System.Drawing.Size(24, 20);
            this.btnSearchLocation.TabIndex = 1;
            this.btnSearchLocation.Text = "...";
            this.btnSearchLocation.Click += new System.EventHandler(this.btnSearchLocation_Click);
            // 
            // lblPartNumber
            // 
            this.lblPartNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPartNumber.Location = new System.Drawing.Point(6, 92);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(82, 20);
            this.lblPartNumber.TabIndex = 15;
            this.lblPartNumber.Text = "Part Number";
            this.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCompletionNO
            // 
            this.lblCompletionNO.ForeColor = System.Drawing.Color.Maroon;
            this.lblCompletionNO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCompletionNO.Location = new System.Drawing.Point(6, 48);
            this.lblCompletionNO.Name = "lblCompletionNO";
            this.lblCompletionNO.Size = new System.Drawing.Size(82, 20);
            this.lblCompletionNO.TabIndex = 6;
            this.lblCompletionNO.Text = "Completion No.";
            this.lblCompletionNO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMasLoc
            // 
            this.txtMasLoc.Location = new System.Drawing.Point(90, 26);
            this.txtMasLoc.Name = "txtMasLoc";
            this.txtMasLoc.Size = new System.Drawing.Size(120, 20);
            this.txtMasLoc.TabIndex = 2;
            this.txtMasLoc.Text = "";
            this.txtMasLoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMasLoc_KeyDown);
            this.txtMasLoc.Leave += new System.EventHandler(this.txtMasLoc_Leave);
            // 
            // lblMasLoc
            // 
            this.lblMasLoc.ForeColor = System.Drawing.Color.Maroon;
            this.lblMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMasLoc.Location = new System.Drawing.Point(6, 26);
            this.lblMasLoc.Name = "lblMasLoc";
            this.lblMasLoc.Size = new System.Drawing.Size(82, 20);
            this.lblMasLoc.TabIndex = 3;
            this.lblMasLoc.Text = "Mas. Location";
            this.lblMasLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPostDate
            // 
            this.lblPostDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblPostDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPostDate.Location = new System.Drawing.Point(6, 4);
            this.lblPostDate.Name = "lblPostDate";
            this.lblPostDate.Size = new System.Drawing.Size(82, 20);
            this.lblPostDate.TabIndex = 2;
            this.lblPostDate.Text = "Post Date";
            this.lblPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCCN
            // 
            this.lblCCN.ForeColor = System.Drawing.Color.Maroon;
            this.lblCCN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCCN.Location = new System.Drawing.Point(498, 4);
            this.lblCCN.Name = "lblCCN";
            this.lblCCN.Size = new System.Drawing.Size(29, 20);
            this.lblCCN.TabIndex = 37;
            this.lblCCN.Text = "CCN";
            this.lblCCN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWO
            // 
            this.lblWO.ForeColor = System.Drawing.Color.Maroon;
            this.lblWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblWO.Location = new System.Drawing.Point(6, 70);
            this.lblWO.Name = "lblWO";
            this.lblWO.Size = new System.Drawing.Size(82, 20);
            this.lblWO.TabIndex = 9;
            this.lblWO.Text = "Work Order";
            this.lblWO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWO
            // 
            this.txtWO.Location = new System.Drawing.Point(90, 70);
            this.txtWO.Name = "txtWO";
            this.txtWO.Size = new System.Drawing.Size(120, 20);
            this.txtWO.TabIndex = 6;
            this.txtWO.Text = "";
            this.txtWO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWO_KeyDown);
            this.txtWO.Validating += new System.ComponentModel.CancelEventHandler(this.txtWO_Validating);
            // 
            // lblWOLine
            // 
            this.lblWOLine.ForeColor = System.Drawing.Color.Maroon;
            this.lblWOLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblWOLine.Location = new System.Drawing.Point(255, 70);
            this.lblWOLine.Name = "lblWOLine";
            this.lblWOLine.Size = new System.Drawing.Size(51, 20);
            this.lblWOLine.TabIndex = 12;
            this.lblWOLine.Text = "WO Line";
            this.lblWOLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Location = new System.Drawing.Point(90, 92);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(160, 20);
            this.txtItem.TabIndex = 10;
            this.txtItem.Text = "";
            // 
            // lblPartName
            // 
            this.lblPartName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPartName.Location = new System.Drawing.Point(6, 114);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(82, 20);
            this.lblPartName.TabIndex = 19;
            this.lblPartName.Text = "Part Name";
            this.lblPartName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPartName
            // 
            this.txtPartName.Enabled = false;
            this.txtPartName.Location = new System.Drawing.Point(90, 114);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(294, 20);
            this.txtPartName.TabIndex = 12;
            this.txtPartName.Text = "";
            // 
            // lblModel
            // 
            this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblModel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblModel.Location = new System.Drawing.Point(255, 92);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(51, 20);
            this.lblModel.TabIndex = 17;
            this.lblModel.Text = "Model";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtModel
            // 
            this.txtModel.Enabled = false;
            this.txtModel.Location = new System.Drawing.Point(308, 92);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(76, 20);
            this.txtModel.TabIndex = 11;
            this.txtModel.Text = "";
            // 
            // lblCompletedQty
            // 
            this.lblCompletedQty.ForeColor = System.Drawing.Color.Maroon;
            this.lblCompletedQty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCompletedQty.Location = new System.Drawing.Point(6, 158);
            this.lblCompletedQty.Name = "lblCompletedQty";
            this.lblCompletedQty.Size = new System.Drawing.Size(82, 20);
            this.lblCompletedQty.TabIndex = 25;
            this.lblCompletedQty.Text = "Completed Qty";
            this.lblCompletedQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLot
            // 
            this.txtLot.Location = new System.Drawing.Point(308, 136);
            this.txtLot.Name = "txtLot";
            this.txtLot.Size = new System.Drawing.Size(76, 20);
            this.txtLot.TabIndex = 16;
            this.txtLot.Text = "";
            // 
            // lblLot
            // 
            this.lblLot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLot.Location = new System.Drawing.Point(256, 136);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(50, 20);
            this.lblLot.TabIndex = 27;
            this.lblLot.Text = "Lot";
            this.lblLot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUM
            // 
            this.lblUM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUM.Location = new System.Drawing.Point(390, 114);
            this.lblUM.Name = "lblUM";
            this.lblUM.Size = new System.Drawing.Size(30, 20);
            this.lblUM.TabIndex = 13;
            this.lblUM.Text = "UM";
            this.lblUM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQAStatus
            // 
            this.lblQAStatus.ForeColor = System.Drawing.Color.Maroon;
            this.lblQAStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQAStatus.Location = new System.Drawing.Point(6, 136);
            this.lblQAStatus.Name = "lblQAStatus";
            this.lblQAStatus.Size = new System.Drawing.Size(82, 20);
            this.lblQAStatus.TabIndex = 23;
            this.lblQAStatus.Text = "QA Status";
            this.lblQAStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(308, 158);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(76, 20);
            this.txtSerial.TabIndex = 18;
            this.txtSerial.Text = "";
            this.txtSerial.Visible = false;
            // 
            // lblSerial
            // 
            this.lblSerial.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSerial.Location = new System.Drawing.Point(256, 158);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(50, 20);
            this.lblSerial.TabIndex = 29;
            this.lblSerial.Text = "Serial";
            this.lblSerial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSerial.Visible = false;
            // 
            // btnSearchWO
            // 
            this.btnSearchWO.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchWO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchWO.Location = new System.Drawing.Point(211, 70);
            this.btnSearchWO.Name = "btnSearchWO";
            this.btnSearchWO.Size = new System.Drawing.Size(24, 20);
            this.btnSearchWO.TabIndex = 7;
            this.btnSearchWO.Text = "...";
            this.btnSearchWO.Click += new System.EventHandler(this.btnSearchWO_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(526, 314);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHelp.Location = new System.Drawing.Point(466, 314);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(60, 23);
            this.btnHelp.TabIndex = 28;
            this.btnHelp.Text = "&Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(67, 314);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(6, 314);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 24;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPrint.Location = new System.Drawing.Point(252, 314);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 23);
            this.btnPrint.TabIndex = 26;
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtCompletionNo
            // 
            this.txtCompletionNo.Location = new System.Drawing.Point(90, 48);
            this.txtCompletionNo.MaxLength = 40;
            this.txtCompletionNo.Name = "txtCompletionNo";
            this.txtCompletionNo.Size = new System.Drawing.Size(120, 20);
            this.txtCompletionNo.TabIndex = 4;
            this.txtCompletionNo.Text = "";
            this.txtCompletionNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompletionNo_KeyDown);
            this.txtCompletionNo.Leave += new System.EventHandler(this.txtCompletionNo_Leave);
            // 
            // btnSearchMasLoc
            // 
            this.btnSearchMasLoc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchMasLoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchMasLoc.Location = new System.Drawing.Point(211, 26);
            this.btnSearchMasLoc.Name = "btnSearchMasLoc";
            this.btnSearchMasLoc.Size = new System.Drawing.Size(24, 20);
            this.btnSearchMasLoc.TabIndex = 3;
            this.btnSearchMasLoc.Text = "...";
            this.btnSearchMasLoc.Click += new System.EventHandler(this.btnSearchMasLoc_Click);
            // 
            // txtWOLine
            // 
            this.txtWOLine.Location = new System.Drawing.Point(308, 70);
            this.txtWOLine.Name = "txtWOLine";
            this.txtWOLine.Size = new System.Drawing.Size(76, 20);
            this.txtWOLine.TabIndex = 8;
            this.txtWOLine.Text = "";
            this.txtWOLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWOLine_KeyDown);
            this.txtWOLine.Validating += new System.ComponentModel.CancelEventHandler(this.txtWOLine_Validating);
            // 
            // dtmPostDate
            // 
            // 
            // dtmPostDate.Calendar
            // 
            this.dtmPostDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmPostDate.CustomFormat = "dd-MM-yyyy hh:mm";
            this.dtmPostDate.EmptyAsNull = true;
            this.dtmPostDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmPostDate.Location = new System.Drawing.Point(90, 4);
            this.dtmPostDate.Name = "dtmPostDate";
            this.dtmPostDate.Size = new System.Drawing.Size(144, 20);
            this.dtmPostDate.TabIndex = 1;
            this.dtmPostDate.Tag = null;
            this.dtmPostDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmPostDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.dtmPostDate.Validating += new System.ComponentModel.CancelEventHandler(this.dtmPostDate_Validating);
            // 
            // cboCCN
            // 
            this.cboCCN.AddItemSeparator = ';';
            this.cboCCN.Caption = "";
            this.cboCCN.CaptionHeight = 17;
            this.cboCCN.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCCN.ColumnCaptionHeight = 17;
            this.cboCCN.ColumnFooterHeight = 17;
            this.cboCCN.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCCN.ContentHeight = 15;
            this.cboCCN.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCCN.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCCN.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                             System.Drawing.FontStyle.Regular,
                                                             System.Drawing.GraphicsUnit.Point, ((System.Byte) (0)));
            this.cboCCN.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCCN.EditorHeight = 15;
            this.cboCCN.FlatStyle = C1.Win.C1List.FlatModeEnum.System;
            this.cboCCN.GapHeight = 2;
            this.cboCCN.ItemHeight = 15;
            this.cboCCN.Location = new System.Drawing.Point(528, 4);
            this.cboCCN.MatchEntryTimeout = ((long) (2000));
            this.cboCCN.MaxDropDownItems = ((short) (5));
            this.cboCCN.MaxLength = 32767;
            this.cboCCN.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCCN.Name = "cboCCN";
            this.cboCCN.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.cboCCN.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCCN.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCCN.Size = new System.Drawing.Size(58, 21);
            this.cboCCN.TabIndex = 0;
            this.cboCCN.Text = "CCN";
            this.cboCCN.PropBag =
                "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
                "ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
                "}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
                "lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
                "ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{BackColor:Wind" +
                "ow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}Re" +
                "cordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raise" +
                "d,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}" +
                "Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowC" +
                "olSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
                "rHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0," +
                " 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Hei" +
                "ght>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
                "Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
                "<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
                "le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
                "ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
                "electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
                "ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
                "iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
                "me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
                "\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
                "elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
                "\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
                "ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
                "/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
                "th>16</DefaultRecSelWidth></Blob>";
            // 
            // cboQAStatus
            // 
            this.cboQAStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQAStatus.ItemHeight = 13;
            this.cboQAStatus.Items.AddRange(new object[]
                                                {
                                                    "",
                                                    "Require Inspection",
                                                    "Not Require Inspection",
                                                    "Quality assured"
                                                });
            this.cboQAStatus.Location = new System.Drawing.Point(90, 136);
            this.cboQAStatus.Name = "cboQAStatus";
            this.cboQAStatus.Size = new System.Drawing.Size(92, 21);
            this.cboQAStatus.TabIndex = 15;
            // 
            // btnSearchWOLine
            // 
            this.btnSearchWOLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSearchWOLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearchWOLine.Location = new System.Drawing.Point(385, 70);
            this.btnSearchWOLine.Name = "btnSearchWOLine";
            this.btnSearchWOLine.Size = new System.Drawing.Size(24, 20);
            this.btnSearchWOLine.TabIndex = 9;
            this.btnSearchWOLine.Text = "...";
            this.btnSearchWOLine.Click += new System.EventHandler(this.btnSearchWOLine_Click);
            // 
            // txtCompletedQty
            // 
            this.txtCompletedQty.CustomFormat = "###,###,###,###";
            this.txtCompletedQty.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.txtCompletedQty.Location = new System.Drawing.Point(90, 158);
            this.txtCompletedQty.Name = "txtCompletedQty";
            this.txtCompletedQty.ParseInfo.NumberStyle =
                ((C1.Win.C1Input.NumberStyleFlags)
                 ((C1.Win.C1Input.NumberStyleFlags.Integer | C1.Win.C1Input.NumberStyleFlags.AllowDecimalPoint)));
            this.txtCompletedQty.Size = new System.Drawing.Size(92, 20);
            this.txtCompletedQty.TabIndex = 17;
            this.txtCompletedQty.Tag = null;
            this.txtCompletedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCompletedQty.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            // 
            // txtUM
            // 
            this.txtUM.Enabled = false;
            this.txtUM.Location = new System.Drawing.Point(421, 114);
            this.txtUM.Name = "txtUM";
            this.txtUM.ReadOnly = true;
            this.txtUM.Size = new System.Drawing.Size(58, 20);
            this.txtUM.TabIndex = 14;
            this.txtUM.TabStop = false;
            this.txtUM.Text = "";
            // 
            // btnShift
            // 
            this.btnShift.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShift.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShift.Location = new System.Drawing.Point(184, 180);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(24, 20);
            this.btnShift.TabIndex = 20;
            this.btnShift.Text = "...";
            this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // txtShift
            // 
            this.txtShift.Location = new System.Drawing.Point(90, 180);
            this.txtShift.Name = "txtShift";
            this.txtShift.Size = new System.Drawing.Size(92, 20);
            this.txtShift.TabIndex = 19;
            this.txtShift.Text = "";
            this.txtShift.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtShift_KeyDown);
            this.txtShift.Validating += new System.ComponentModel.CancelEventHandler(this.txtShift_Validating);
            // 
            // lblShift
            // 
            this.lblShift.ForeColor = System.Drawing.Color.Maroon;
            this.lblShift.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblShift.Location = new System.Drawing.Point(6, 180);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(82, 20);
            this.lblShift.TabIndex = 38;
            this.lblShift.Text = "Shift";
            this.lblShift.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPurpose
            // 
            this.btnPurpose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPurpose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPurpose.Location = new System.Drawing.Point(250, 202);
            this.btnPurpose.Name = "btnPurpose";
            this.btnPurpose.Size = new System.Drawing.Size(24, 20);
            this.btnPurpose.TabIndex = 22;
            this.btnPurpose.Text = "...";
            this.btnPurpose.Click += new System.EventHandler(this.btnPurpose_Click);
            // 
            // txtPurpose
            // 
            this.txtPurpose.Location = new System.Drawing.Point(90, 202);
            this.txtPurpose.Name = "txtPurpose";
            this.txtPurpose.Size = new System.Drawing.Size(158, 20);
            this.txtPurpose.TabIndex = 21;
            this.txtPurpose.Text = "";
            this.txtPurpose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPurpose_KeyDown);
            this.txtPurpose.Validating += new System.ComponentModel.CancelEventHandler(this.txtPurpose_Validating);
            // 
            // lblPurpose
            // 
            this.lblPurpose.ForeColor = System.Drawing.Color.Maroon;
            this.lblPurpose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPurpose.Location = new System.Drawing.Point(6, 202);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(82, 20);
            this.lblPurpose.TabIndex = 41;
            this.lblPurpose.Text = "Purpose";
            this.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRemark
            // 
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(90, 226);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(294, 20);
            this.txtRemark.TabIndex = 23;
            this.txtRemark.Text = "";
            // 
            // lblRemark
            // 
            this.lblRemark.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRemark.Location = new System.Drawing.Point(6, 226);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(82, 20);
            this.lblRemark.TabIndex = 44;
            this.lblRemark.Text = "Remark";
            this.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrintBOMShortage
            // 
            this.btnPrintBOMShortage.Location = new System.Drawing.Point(314, 314);
            this.btnPrintBOMShortage.Name = "btnPrintBOMShortage";
            this.btnPrintBOMShortage.Size = new System.Drawing.Size(121, 23);
            this.btnPrintBOMShortage.TabIndex = 27;
            this.btnPrintBOMShortage.Text = "Print &BOM Shortage";
            this.btnPrintBOMShortage.Click += new System.EventHandler(this.btnPrintBOMShortage_Click);
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(90, 4);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(144, 20);
            this.lblDate.TabIndex = 45;
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLabelPostDate
            // 
            this.lblLabelPostDate.ForeColor = System.Drawing.Color.Black;
            this.lblLabelPostDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLabelPostDate.Location = new System.Drawing.Point(6, 4);
            this.lblLabelPostDate.Name = "lblLabelPostDate";
            this.lblLabelPostDate.Size = new System.Drawing.Size(82, 20);
            this.lblLabelPostDate.TabIndex = 46;
            this.lblLabelPostDate.Text = "Post Date";
            this.lblLabelPostDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtmWOCompletionDate
            // 
            // 
            // dtmWOCompletionDate.Calendar
            // 
            this.dtmWOCompletionDate.Calendar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtmWOCompletionDate.CustomFormat = "dd-MM-yyyy hh:mm";
            this.dtmWOCompletionDate.EmptyAsNull = true;
            this.dtmWOCompletionDate.Enabled = false;
            this.dtmWOCompletionDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.dtmWOCompletionDate.Location = new System.Drawing.Point(349, 4);
            this.dtmWOCompletionDate.Name = "dtmWOCompletionDate";
            this.dtmWOCompletionDate.Size = new System.Drawing.Size(130, 20);
            this.dtmWOCompletionDate.TabIndex = 47;
            this.dtmWOCompletionDate.Tag = null;
            this.dtmWOCompletionDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dtmWOCompletionDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // lblWOCompletionDate
            // 
            this.lblWOCompletionDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblWOCompletionDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblWOCompletionDate.Location = new System.Drawing.Point(242, 4);
            this.lblWOCompletionDate.Name = "lblWOCompletionDate";
            this.lblWOCompletionDate.Size = new System.Drawing.Size(114, 20);
            this.lblWOCompletionDate.TabIndex = 48;
            this.lblWOCompletionDate.Text = "WO Completion Date";
            this.lblWOCompletionDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(128, 314);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 23);
            this.btnEdit.TabIndex = 50;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(190, 314);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 23);
            this.btnDelete.TabIndex = 51;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // WorkOrderCompletion
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(590, 343);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.dtmWOCompletionDate);
            this.Controls.Add(this.lblWOCompletionDate);
            this.Controls.Add(this.btnPrintBOMShortage);
            this.Controls.Add(this.grpMoveCompleteMaterial);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.txtPurpose);
            this.Controls.Add(this.txtShift);
            this.Controls.Add(this.txtUM);
            this.Controls.Add(this.txtWOLine);
            this.Controls.Add(this.txtCompletionNo);
            this.Controls.Add(this.txtWO);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.txtMasLoc);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtLot);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.btnPurpose);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.btnShift);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.txtCompletedQty);
            this.Controls.Add(this.btnSearchWOLine);
            this.Controls.Add(this.cboQAStatus);
            this.Controls.Add(this.cboCCN);
            this.Controls.Add(this.dtmPostDate);
            this.Controls.Add(this.btnSearchMasLoc);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblCompletionNO);
            this.Controls.Add(this.lblPartNumber);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnSearchWO);
            this.Controls.Add(this.btnSearchCompletionNo);
            this.Controls.Add(this.lblWO);
            this.Controls.Add(this.lblUM);
            this.Controls.Add(this.lblLot);
            this.Controls.Add(this.lblWOLine);
            this.Controls.Add(this.lblPartName);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.lblCompletedQty);
            this.Controls.Add(this.lblCCN);
            this.Controls.Add(this.lblMasLoc);
            this.Controls.Add(this.lblQAStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblPostDate);
            this.Controls.Add(this.lblLabelPostDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WorkOrderCompletion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work Order Completion";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.WorkOrderCompletion_Closing);
            this.Load += new System.EventHandler(this.WorkOrderCompletion_Load);
            this.grpMoveCompleteMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.dtmPostDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.cboCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.txtCompletedQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dtmWOCompletionDate)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        #region BOM Shortage Report: Tuan TQ

        /// <summary>
        /// Print BOM Shortage Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ., 29 Dec, 2005</author>
        private void btnPrintBOMShortage_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPrint_Click()";
            try
            {
                //Change cursor to busy
                Cursor = Cursors.WaitCursor;

                //Show report
                ShowBOMShortageReport(this, null);
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
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


        private void ShowBOMShortageReport(object sender, EventArgs e)
        {
            const string APPLICATION_PATH = @"PCSMain\bin\Debug";
            const string RPT_COMPANY_FIELD = "fldCompany";
            const string RPT_COMPLETED_QUANTITY_FIELD = "fldCompletedQuantity";

            const string BOM_SHORTAGE_REPORT = "BOMShortageReport.xml";
            try
            {
                //return if no record was selected
                if (txtWOLine.Tag == null)
                {
                    return;
                }

                if (txtWOLine.Tag.ToString().Trim() == string.Empty)
                {
                    return;
                }

                //Change cursor type
                Cursor = Cursors.WaitCursor;

                //get completed quatity
                decimal decCompletedQuantity = decimal.Zero;
                if (!txtCompletedQty.ValueIsDbNull && txtCompletedQty.Text.Trim() != string.Empty)
                {
                    decCompletedQuantity = decimal.Parse(txtCompletedQty.Value.ToString());
                }

                var printPreview = new C1PrintPreviewDialog();
                var boDataReport = new C1PrintPreviewDialogBO();

                var dtbResult = boDataReport.GetBOMShortageData(new List<string> { txtWOLine.Tag.ToString() }, decCompletedQuantity);

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

                reportBuilder.ReportLayoutFile = BOM_SHORTAGE_REPORT;

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

                // and show it in preview dialog								
                reportBuilder.ReportViewer = printPreview.ReportViewer;
                reportBuilder.RenderReport();

                //Header information get from system params				
                reportBuilder.DrawPredefinedField(RPT_COMPANY_FIELD,
                                                  SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));
                reportBuilder.DrawPredefinedField(RPT_COMPLETED_QUANTITY_FIELD,
                                                  decCompletedQuantity.ToString(Constants.DECIMAL_NUMBERFORMAT));

                reportBuilder.RefreshReport();

                //Print report
                try
                {
                    printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
                }
                catch
                {
                }
                printPreview.Show();
            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Print WO Completion: Tuan TQ

        /// <summary>
        /// Build and show WO Completion Slip Report
        /// </summary>
        /// <Author> Tuan TQ, 08 Dec, 2005</Author>
        private void ShowWOCompletionSlipReport(object sender, EventArgs e)
        {
            const int NUMBER_OF_COPIES = 1;
            const string APPLICATION_PATH = @"PCSMain\bin\Debug";
            const string RPT_COMPANY_FIELD = "fldCompany";
            const string WO_COMPLETION_SLIP_REPORT = "WOCompletionSlipReport.xml";

            //return if no record was selected
            if (lblCompletionNO.Tag == null)
            {
                return;
            }

            if (lblCompletionNO.Tag.ToString().Trim() == string.Empty)
            {
                return;
            }

            var printPreview = new C1PrintPreviewDialog();
            var boDataReport = new C1PrintPreviewDialogBO();

            DataTable dtbResult;
            dtbResult = boDataReport.GetWOCompletionData(int.Parse(lblCompletionNO.Tag.ToString()));

            // Check data source
            if (dtbResult != null)
            {
                //Prepare data for blank row				
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i = 1; i < NUMBER_OF_COPIES; i++)
                    {
                        DataRow drowSourceBlankRow = dtbResult.NewRow();
                        //Copy data
                        for (int colIndex = 0; colIndex < dtbResult.Columns.Count; colIndex++)
                        {
                            drowSourceBlankRow[colIndex] = dtbResult.Rows[0][colIndex];
                        }
                        dtbResult.Rows.Add(drowSourceBlankRow);
                    }
                }
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
                strReportPath += "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;
            }

            //Set datasource and lay-out path for reports
            reportBuilder.SourceDataTable = dtbResult;
            reportBuilder.ReportDefinitionFolder = strReportPath;

            reportBuilder.ReportLayoutFile = WO_COMPLETION_SLIP_REPORT;

            //check if layout is valid
            if (reportBuilder.AnalyseLayoutFile())
            {
                reportBuilder.UseLayoutFile = true;
            }
            else
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error);
                return;
            }

            reportBuilder.MakeDataTableForRender();

            // and show it in preview dialog			
            reportBuilder.ReportViewer = printPreview.ReportViewer;
            reportBuilder.RenderReport();

            //Header information get from system params				
            reportBuilder.DrawPredefinedField(RPT_COMPANY_FIELD,
                                              SystemProperty.SytemParams.Get(SystemParam.COMPANY_FULL_NAME));

            reportBuilder.RefreshReport();

            //Print report
            try
            {
                printPreview.FormTitle = reportBuilder.GetFieldByName(REPORTFLD_TITLE).Text;
            }
            catch
            {
            }
            printPreview.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPrint_Click()";
            try
            {
                //Change cursor to busy
                Cursor = Cursors.WaitCursor;

                //Show report
                ShowWOCompletionSlipReport(this, null);
            }
            catch (PCSException ex)
            {
                // Displays the error message if throwed from PCSException.
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    // Log error message into log file.
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
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

        #endregion

        #region HACKED: Add more 3 properties, Tuan TQ

        //HACKED: Added by Tuan TQ. 09 Dec, 2005

        /// <summary>
        /// Fill related data on controls when selecting Shift
        /// </summary>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private bool SelectShift(bool pblnAlwaysShowDialog)
        {
            var htbCriteria = new Hashtable();
            DataRowView drwResult = null;

            //Call OpenSearchForm for selecting Shift
            drwResult = FormControlComponents.OpenSearchForm(PRO_ShiftTable.TABLE_NAME, PRO_ShiftTable.SHIFTDESC_FLD,
                                                             txtShift.Text, htbCriteria, pblnAlwaysShowDialog);

            // If has Shift matched searching condition, fill values to form's controls
            if (drwResult != null)
            {
                txtShift.Text = drwResult[PRO_ShiftTable.SHIFTDESC_FLD].ToString();
                txtShift.Tag = drwResult[PRO_ShiftTable.SHIFTID_FLD];

                //Reset modify status
                txtShift.Modified = false;
            }
            else if (!pblnAlwaysShowDialog)
            {
                txtShift.Tag = null;
                txtShift.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Open Search Form for selecting location
        /// </summary>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <returns></returns>
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

                txtLocation.Text = drwResult[MST_LocationTable.CODE_FLD].ToString();
                txtLocation.Tag = drwResult[MST_LocationTable.LOCATIONID_FLD];

                //reset Modified status
                txtLocation.Modified = false;

                //Reset MasterLocationDefaultID
                intMasterLocationDefaultID = voMasLoc.MasterLocationID;
            }
            else if (!pblnAlwaysShowDialog)
            {
                txtLocation.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Open Search Form for selecting bin
        /// </summary>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <returns></returns>
        private bool SelectBin(bool pblnAlwaysShowDialog)
        {
            //User has entered Location
            if (txtLocation.Text.Trim() == string.Empty)
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_RGA_SELECTLOC, MessageBoxIcon.Exclamation);
                txtLocation.Focus();
                return false;
            }

            DataRowView drwResult = null;
            string strCondition = MST_BINTable.TABLE_NAME + "." + MST_BINTable.LOCATIONID_FLD + " = " + txtLocation.Tag;
            strCondition += " AND MST_BIN." + MST_BINTable.BINTYPEID_FLD + " != " + ((int) BinTypeEnum.LS);
            strCondition += " AND MST_BIN." + MST_BINTable.BINTYPEID_FLD + " != " + ((int) BinTypeEnum.IN);
            drwResult = FormControlComponents.OpenSearchForm(MST_BINTable.TABLE_NAME, MST_BINTable.CODE_FLD,
                                                             txtBin.Text.Trim(), strCondition);
            // If has BIN matched searching condition, fill values to form's controls
            if (drwResult != null)
            {
                txtBin.Text = drwResult[MST_BINTable.CODE_FLD].ToString();
                txtBin.Tag = drwResult[MST_BINTable.BINID_FLD];

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

        /// <summary>
        /// Fill related data on controls when selecting Issue purpose
        /// </summary>
        /// <param name="pblnAlwaysShowDialog"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private bool SelectIssuePurpose(bool pblnAlwaysShowDialog)
        {
            var htbCriteria = new Hashtable();
            DataRowView drwResult = null;

            htbCriteria.Add(PRO_IssuePurposeTable.TRANTYPEID_FLD, intTransTypeID);

            //Call OpenSearchForm for selecting Issue purpose
            drwResult = FormControlComponents.OpenSearchForm(PRO_IssuePurposeTable.TABLE_NAME,
                                                             PRO_IssuePurposeTable.DESCRIPTION_FLD, txtPurpose.Text,
                                                             htbCriteria, pblnAlwaysShowDialog);

            // If has Issue Purpose matched searching condition, fill values to form's controls
            if (drwResult != null)
            {
                txtPurpose.Text = drwResult[PRO_IssuePurposeTable.DESCRIPTION_FLD].ToString();
                txtPurpose.Tag = drwResult[PRO_IssuePurposeTable.ISSUEPURPOSEID_FLD];

                //Reset modify status
                txtPurpose.Modified = false;
            }
            else if (!pblnAlwaysShowDialog)
            {
                txtPurpose.Tag = null;
                txtPurpose.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Processing click event on btnPurpose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private void btnPurpose_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnPurpose_Click()";
            try
            {
                SelectIssuePurpose(true);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Processing Validating event on txtPurpose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private void txtPurpose_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPurpose_Validating()";

            try
            {
                //exit if not in add action or empty
                if (!btnSave.Enabled) return;

                if (txtPurpose.Text.Trim().Length == 0)
                {
                    txtPurpose.Tag = null;
                    return;
                }
                else if (!txtPurpose.Modified)
                {
                    return;
                }

                e.Cancel = !SelectIssuePurpose(false);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Processing Keydown event on txtPurpose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private void txtPurpose_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtPurpose_KeyDown()";

            try
            {
                if ((e.KeyCode == Keys.F4) && (btnPurpose.Enabled))
                {
                    SelectIssuePurpose(true);
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Processing click event on btnShift
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private void btnShift_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".btnShift_Click()";

            try
            {
                SelectShift(true);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Processing KeyDown event on txtShift
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private void txtShift_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtShift_KeyDown()";

            try
            {
                if ((e.KeyCode == Keys.F4) && (btnShift.Enabled))
                {
                    SelectShift(true);
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Processing Validating event on txtShift
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author> Tuan TQ. 09 Dec, 2005</author>
        private void txtShift_Validating(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".txtShift_Validating()";

            try
            {
                //exit if not in add action or empty
                if (!btnSave.Enabled) return;

                if (txtShift.Text.Trim().Length == 0)
                {
                    txtShift.Tag = null;
                    return;
                }
                else if (!txtShift.Modified)
                {
                    return;
                }

                e.Cancel = !SelectShift(false);
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}
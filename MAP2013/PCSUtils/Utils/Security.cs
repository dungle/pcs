using System;
// using System.Collections;
using System.Data;
using System.Drawing;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
// using PCSComUtils.PCSExc;
using System.Windows.Forms;
using C1.Win.C1Command;
using PCSComUtils.Common.BO;
using PCSComUtils.ErrorMsg.BO;
using PCSUtils.Admin;
using AlignHorzEnum = C1.Win.C1TrueDBGrid.AlignHorzEnum;
using AlignVertEnum = C1.Win.C1TrueDBGrid.AlignVertEnum;
using C1DisplayColumn = C1.Win.C1TrueDBGrid.C1DisplayColumn;
using System.Linq;

namespace PCSUtils.Utils
{
    /// <summary>
    /// Summary summary for Security.
    /// </summary>
    public class Security
    {
        const string Chara = "@";
        const string Cboccn = "cboCCN";
        /// <summary>
        /// This message was used for only Super admin account so that no need to add into database
        /// </summary>
        const string MessageCreateDataVisibility = "Do you want create data for visibility?";
        /// <summary>
        /// 31 = 2^5-1
        /// </summary>
        const int FullPermission = 31;

        /// <summary>
        /// Find button by Name
        /// </summary>
        /// <param name="pForm"></param>
        /// <param name="pControlName"></param>
        /// <returns></returns>
        /// <author>TuanDM</author>
        private static Control FindButtonControlByName(Form pForm, string pControlName)
        {
            Control ctl = pForm;
            while (ctl != null)
            {
                ctl = pForm.GetNextControl(ctl, true);
                if (ctl != null && ctl is Button)
                {
                    if (ctl.Name == pControlName)
                    {
                        return ctl;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Enable a control on the form
        /// </summary>
        /// <Authors>
        /// SonHT
        /// </Authors>
        /// <History>
        ///	Friday - January 13 - 2005	
        /// </History>
        private static void EnabledChanged(object sender, EventArgs e)
        {
            if (sender == null) return;
            var cboCCN = (C1Combo)sender;
            if (cboCCN.Enabled == false)
            {
                cboCCN.Enabled = true;
            }
        }

        /// <summary>
        /// Set all right for a user on form
        /// </summary>
        /// <Inputs>
        /// pForm : the form needs to set
        /// pstrUserName : contains UserName
        /// </Inputs>
        /// <Return>
        /// void
        /// </Return>
        /// <Authors>
        /// TuanDm
        /// </Authors>
        /// <History>
        ///	Friday - January 13 - 2005	
        /// </History>
        public int SetRightForUserOnForm(Form pForm, string pstrUserName)
        {
            // SetButtonForRight_View(pForm);
            if (!pForm.Modal)
            {
                bool blnIsFound = false;

                #region Search form which assign security

                foreach (object t in SystemProperty.ArrayForms)
                {
                    try
                    {
                        var objInforForm = (FormInfo)t;
                        if (objInforForm.mForm == pForm)
                        {
                            blnIsFound = true;
                            break;
                        }
                    }
                    catch { }
                }

                #endregion

                if (!blnIsFound)
                {
                    #region // if not found add to Array form to manage
                    var list = (from obj in SystemProperty.TableMenuEntry
                                where (obj.FormLoad == pForm.GetType().FullName && obj.IsUserCreated == 0)
                                orderby obj.Menu_EntryID ascending
                                select obj).ToList();
                    FormInfo infoForm = list.Count > 0 ? new FormInfo(pForm, list[0].Prefix, list[0].TransFormat, list[0].TableName, list[0].TransNoFieldName, SystemProperty.UserName) : new FormInfo(pForm, string.Empty, string.Empty, string.Empty, string.Empty, SystemProperty.UserName);
                    SystemProperty.ArrayForms.Add(infoForm);

                    #endregion
                }
            }

            var boCommon = new CommonBO();
            DataSet dstSecurity = boCommon.GetSecurityInfo(pstrUserName, pForm.GetType().FullName);
            int intFullPermission = int.Parse(dstSecurity.Tables[Sys_RightTable.TABLE_NAME].Rows[0][0].ToString());

            #region // HACK: Added by Tuan TQ update code to manage super admin user

            if (pstrUserName.Equals(Constants.SUPER_ADMIN_USER))
            {
                // HACK: SONHT HardCode
                DialogResult result = PCSMessageBox.Show(MessageCreateDataVisibility, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var frmGroupControl = new GroupControls(pForm);
                    frmGroupControl.Show();
                    if (pForm.Modal)
                        pForm.Show();
                }
                intFullPermission = FullPermission;// (2 exp 5) -1
            }

            #endregion

            #region // if has no permission
            if (intFullPermission == 0)
            {
                int intCountExisted = int.Parse(dstSecurity.Tables[Sys_Menu_EntryTable.TABLE_NAME].Rows[0][0].ToString());
                if (intCountExisted > 0)
                    return intFullPermission;
                intFullPermission = FullPermission;// (2 exp 5) -1
            }

            #endregion

            // Set visible control
            DataTable dtbTable = dstSecurity.Tables[Sys_VisibilityGroup_RoleTable.TABLE_NAME];
            // Set other property for form
            pForm.StartPosition = FormStartPosition.CenterScreen;
            pForm.KeyPreview = true;

            Control objControl = pForm;
            #region // Scan control on form to set default property
            while (true)
            {
                #region // Get next control
                objControl = pForm.GetNextControl(objControl, true);
                if (objControl == null)
                    break;

                #endregion

                #region // Set OnEnter and OnLeave
                if (objControl.GetType().Equals(typeof(TextBox))
                    || objControl.GetType().Equals(typeof(ComboBox))
                    || objControl.GetType().Equals(typeof(C1Combo))
                    || objControl.GetType().Equals(typeof(C1DateEdit))
                    || objControl.GetType().Equals(typeof(C1TextBox))
                    || objControl.GetType().Equals(typeof(C1NumericEdit))
                    || objControl.GetType().Equals(typeof(NumericUpDown)))
                {
                    objControl.Enter += FormControlComponents.OnEnterControl;
                    objControl.Leave += FormControlComponents.OnLeaveControl;
                }

                if (objControl.GetType().Equals(typeof(Button)))
                    if (objControl.Name == "btnHelp" || objControl.Name == "HelpButton")
                        objControl.Click += FormControlComponents.OnHelpClick;

                #endregion

                if (objControl.GetType().Equals(typeof(TextBox)))
                {
                    #region // If it's TextBox
                    var txtBox = (TextBox)objControl;
                    txtBox.TextAlign = HorizontalAlignment.Left;
                    //HACK : TUANDM 18 - 10 - 2005
                    txtBox.Text = txtBox.Text.Trim();
                    if (txtBox.Text != string.Empty)
                    {
                        var btnSearch = (Button)FindButtonControlByName(pForm, txtBox.Text);
                        if (btnSearch != null)
                        {
                            txtBox.Validating += FormControlComponents.ControlValidating;
                            txtBox.Text = string.Empty;
                        }
                    }

                    #endregion

                    //END: TUANDM 18 - 10 - 2005
                }
                else if (objControl.GetType().Equals(typeof(C1TextBox)))
                {
                    #region C1TextBox
                    var txtC1Text = (C1TextBox)objControl;
                    txtC1Text.TextAlign = HorizontalAlignment.Left;
                    //HACK : TUANDM 18 - 10 - 2005
                    txtC1Text.Value = txtC1Text.Value.ToString().Trim();
                    if (txtC1Text.Value.ToString() != string.Empty)
                    {
                        var btnSearch = (Button)FindButtonControlByName(pForm, txtC1Text.Value.ToString());
                        if (btnSearch != null)
                        {
                            txtC1Text.Validating += FormControlComponents.ControlValidating;
                            txtC1Text.Value = string.Empty;
                        }
                    }

                    #endregion
                }
                else if (objControl.GetType().Equals(typeof(C1DateEdit)))
                {
                    var dtmC1Date = (C1DateEdit)objControl;
                    dtmC1Date.Validated += FormControlComponents.C1DateEdit_Validated;
                    dtmC1Date.TextAlign = HorizontalAlignment.Center;
                    dtmC1Date.AcceptsEscape = false;

                    #region // Set default dd-MM-yyyy format
                    if (dtmC1Date.CustomFormat.Trim().Length == 0)
                    {
                        dtmC1Date.CustomFormat = Constants.DATETIME_FORMAT;
                        dtmC1Date.FormatType = FormatTypeEnum.CustomFormat;
                    }
                    string strMsg = ErrorMessageBO.GetErrorMessage(ErrorCode.MESSAGE_C1NUMBER_INPUT_VALUE);
                    strMsg = strMsg.Replace(Chara, string.Empty);
                    string strSecondMsg = ErrorMessageBO.GetErrorMessage(ErrorCode.MESSAGE_C1NUMBER_OR_MSG);
                    // if it has no Intervals
                    if (dtmC1Date.PostValidation.Intervals.Count == 0)
                        continue;
                    strMsg = dtmC1Date.PostValidation.Intervals.Cast<ValueInterval>().Aggregate(strMsg, (current, vlInterval) => current + (Constants.OPEN_SBRACKET + vlInterval.MinValue + Constants.COMMA + Constants.WHITE_SPACE + vlInterval.MaxValue + Constants.CLOSE_SBRACKET + Constants.WHITE_SPACE + strSecondMsg + Constants.WHITE_SPACE));
                    strMsg = strMsg.Substring(0, strMsg.Length - strSecondMsg.Length - 1);
                    dtmC1Date.ErrorInfo.ShowErrorMessage = true;
                    dtmC1Date.ErrorInfo.ErrorMessageCaption = Constants.APPLICATION_NAME;
                    dtmC1Date.ErrorInfo.ErrorMessage = strMsg;
                    dtmC1Date.ErrorInfo.CanLoseFocus = false;

                    #endregion
                }
                else if (objControl.GetType().Equals(typeof(C1NumericEdit)))
                {
                    #region // if it is C1NumericEdit
                    var dtmC1Num = (C1NumericEdit)objControl;
                    dtmC1Num.TextAlign = HorizontalAlignment.Right;
                    dtmC1Num.AcceptsEscape = false;
                    // Set default number format
                    if (dtmC1Num.CustomFormat.Trim().Length == 0)
                    {
                        dtmC1Num.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;
                        dtmC1Num.FormatType = FormatTypeEnum.CustomFormat;
                    }
                    string strMsg = ErrorMessageBO.GetErrorMessage(ErrorCode.MESSAGE_C1NUMBER_INPUT_VALUE);
                    strMsg = strMsg.Replace(Chara, string.Empty);
                    string strSecondMsg = ErrorMessageBO.GetErrorMessage(ErrorCode.MESSAGE_C1NUMBER_OR_MSG);
                    // if it has no Intervals
                    if (dtmC1Num.PostValidation.Intervals.Count == 0)
                        continue;
                    strMsg = dtmC1Num.PostValidation.Intervals.Cast<ValueInterval>().Aggregate(strMsg, (current, vlInterval) => current + (Constants.OPEN_SBRACKET + vlInterval.MinValue + Constants.COMMA + Constants.WHITE_SPACE + vlInterval.MaxValue + Constants.CLOSE_SBRACKET + Constants.WHITE_SPACE + strSecondMsg + Constants.WHITE_SPACE));
                    strMsg = strMsg.Substring(0, strMsg.Length - strSecondMsg.Length - 1);
                    dtmC1Num.ErrorInfo.ShowErrorMessage = true;
                    dtmC1Num.ErrorInfo.ErrorMessageCaption = Constants.APPLICATION_NAME;
                    dtmC1Num.ErrorInfo.ErrorMessage = strMsg;
                    dtmC1Num.ErrorInfo.CanLoseFocus = false;

                    #endregion
                }

                else if (objControl.GetType().Equals(typeof(C1Combo)))
                {
                    #region // if it is C1Combo
                    var cboC1 = (C1Combo)objControl;
                    cboC1.DropDownWidth = Constants.DEFAULT_C1COMBO_DROPDOWNWIDTH;
                    cboC1.KeyDown += FormControlComponents.C1Combo_KeyDown;
                    cboC1.ComboStyle = ComboStyleEnum.DropdownList;
                    // Enable cboCCN control
                    if (cboC1.Name == Cboccn)
                    {
                        cboC1.Enabled = true;
                        cboC1.EnabledChanged += EnabledChanged;
                    }

                    #endregion
                }
                else if (objControl.GetType().Equals(typeof(C1TrueDBGrid)))
                {
                    #region // if is C1TrueDBGrid control
                    var grid = (C1TrueDBGrid)objControl;
                    grid.RowHeight = Constants.DEFAULT_ROW_HEIGHT;
                    grid.MarqueeStyle = MarqueeEnum.HighlightCell;
                    grid.HighLightRowStyle.BackColor = Color.FromArgb(Constants.BACKGROUND_COLOUR_R, Constants.BACKGROUND_COLOUR_G, Constants.BACKGROUND_COLOUR_B);
                    grid.HighLightRowStyle.ForeColor = Color.FromArgb(Constants.FORE_COLOUR_R, Constants.FORE_COLOUR_R, Constants.FORE_COLOUR_R);
                    grid.Style.VerticalAlignment = AlignVertEnum.Center;
                    if (grid.Splits.Count > 0)
                    {
                        // Set default alignment
                        foreach (C1DisplayColumn c1Column in grid.Splits[0].DisplayColumns)
                            c1Column.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
                    }
                    // Not allow user change column order
                    grid.AllowColMove = false;
                    grid.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    // Set Maximize and Resize
                    pForm.MaximizeBox = true;
                    pForm.FormBorderStyle = FormBorderStyle.Sizable;

                    #endregion
                }
            }
            #endregion

            if (!pstrUserName.Equals(Constants.SUPER_ADMIN_USER))
            {
                #region // Scan data from datatable to set visible control
                if (dtbTable != null)
                {
                    foreach (DataRow drowData in dtbTable.Rows)
                    {
                        Control objCon = pForm.GetNextControl(pForm, true);
                        while (objCon != null)
                        {
                            // if is C1TrueDBGrid control
                            if (objCon.GetType().Equals(typeof(C1TrueDBGrid)))
                            {
                                var grid = (C1TrueDBGrid)objCon;
                                foreach (C1DisplayColumn objCol in grid.Splits[0].DisplayColumns)
                                {
                                    // do not process if col is invisible
                                    if (!objCol.Visible)
                                    {
                                        objCol.AllowSizing = objCol.Visible = false;
                                        continue;
                                    }
                                    if ((objCol.DataColumn.DataField == drowData[Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString().Trim())
                                        && (drowData[Sys_VisibilityItemTable.TYPE_FLD].ToString() == VisibilityItemTypeEnum.ColumnTrueDBGrid.GetHashCode().ToString()))
                                    {
                                        objCol.AllowSizing = objCol.Visible = false;
                                    }
                                }
                            }
                            // If control is C1TabControl
                            else if (objCon.GetType().Equals(typeof(C1DockingTab)))
                            {
                                var c1DockingTab = (C1DockingTab)objCon;
                                for (int k = 0; k < c1DockingTab.TabCount; k++)
                                {
                                    if (c1DockingTab.TabPages[k].Name == drowData[Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString().Trim())
                                        c1DockingTab.TabPages[k].TabVisible = false;
                                }
                            }
                            // set visible if it is normal control
                            else if (objCon.Name == drowData[Sys_HiddenControlsTable.CONTROLNAME_FLD].ToString().Trim())
                                objCon.Visible = false;
                            // Get next control
                            objCon = pForm.GetNextControl(objCon, true);
                        }
                    }
                }
                #endregion
            }

            // return full permission
            return intFullPermission;

        }

        /// <summary>
        /// Noes the right to edit transaction.
        /// </summary>
        /// <param name="pForm">The p form.</param>
        /// <param name="pstrPrimaryField">The PSTR primary field.</param>
        /// <param name="pintMasterId">The pint master ID.</param>
        /// <returns></returns>
        public static bool NoRightToEditTransaction(Form pForm, string pstrPrimaryField, int pintMasterId)
        {
            if (SystemProperty.UserName.ToLower().Equals(Constants.SUPER_ADMIN_USER))
            {
                return false;
            }

            FormInfo formInfor;
            string strTableName = string.Empty;
            foreach (object t in SystemProperty.ArrayForms)
            {
                formInfor = (FormInfo)t;
                if (pForm == formInfor.mForm)
                {
                    strTableName = formInfor.mTableName;
                    break;
                }
            }
            // Check role administrator
            var boUtils = new UtilsBO();
            DataSet dstGetRightToModify = boUtils.GetRightToModify(SystemProperty.UserName, strTableName, pstrPrimaryField, pintMasterId);
            if (dstGetRightToModify == null) return false;
            if (int.Parse(dstGetRightToModify.Tables[0].Rows[0][0].ToString()) > 0)
            {
                return false;
            }
            
            string strUserCreated = dstGetRightToModify.Tables[1].Rows[0][0].ToString();
            if (!strUserCreated.ToLower().Equals(SystemProperty.UserName.ToLower()))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_DONT_HAVE_RIGHT_TO_EDIT, MessageBoxIcon.Information, new[] { strUserCreated });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is difference prefix] [the specified p form].
        /// </summary>
        /// <param name="pForm">The p form.</param>
        /// <param name="plblTrans">The PLBL trans.</param>
        /// <param name="ptxtTransNo">The PTXT trans no.</param>
        /// <returns>
        /// 	<c>true</c> if [is difference prefix] [the specified p form]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDifferencePrefix(Form pForm, Label plblTrans, TextBox ptxtTransNo)
        {
            if (SystemProperty.UserName.ToLower().Equals(Constants.SUPER_ADMIN_USER))
            {
                return false;
            }

            FormInfo formInfor;
            string strPrefix = string.Empty;
            foreach (object t in SystemProperty.ArrayForms)
            {
                formInfor = (FormInfo)t;
                if (pForm == formInfor.mForm)
                {
                    strPrefix = formInfor.mPrefix;
                    break;
                }
            }
            if (string.IsNullOrEmpty(strPrefix)) return false;
            if (ptxtTransNo.Text == null) ptxtTransNo.Text = string.Empty;
            ptxtTransNo.Text = ptxtTransNo.Text.ToUpper();
            string strTransNo;
            var strTrans = new string[2];
            if (ptxtTransNo.Text.Length >= strPrefix.Length)
            {
                strTransNo = ptxtTransNo.Text.ToLower().Substring(0, strPrefix.Length);
            }
            else
            {
                strTrans[0] = plblTrans.Text;
                strTrans[1] = strPrefix;
                PCSMessageBox.Show(ErrorCode.MESSAGE_TRANSACTION_HAS_TO_HAVE_PREFIX, MessageBoxIcon.Information, strTrans);
                ptxtTransNo.Focus();
                return true;
            }
            if (strPrefix.ToLower().Equals(strTransNo))
            {
                return false;
            }
            strTrans[0] = plblTrans.Text;
            strTrans[1] = strPrefix;
            PCSMessageBox.Show(ErrorCode.MESSAGE_TRANSACTION_HAS_TO_HAVE_PREFIX, MessageBoxIcon.Information, strTrans);
            ptxtTransNo.Focus();
            return true;
        }

        /// <summary>
        /// Noes the right to delete transaction.
        /// </summary>
        /// <param name="pForm">The p form.</param>
        /// <param name="pstrPrimaryField">The PSTR primary field.</param>
        /// <param name="pintMasterId">The pint master ID.</param>
        /// <returns></returns>
        public static bool NoRightToDeleteTransaction(Form pForm, string pstrPrimaryField, int pintMasterId)
        {
            if (SystemProperty.UserName.ToLower().Equals(Constants.SUPER_ADMIN_USER))
            {
                return false;
            }
            FormInfo formInfor;
            string strTableName = string.Empty;
            foreach (object t in SystemProperty.ArrayForms)
            {
                formInfor = (FormInfo)t;
                if (pForm == formInfor.mForm)
                {
                    strTableName = formInfor.mTableName;
                    break;
                }
            }
            // Check role administrator
            var boUtils = new UtilsBO();
            DataSet dstGetRightToModify = boUtils.GetRightToModify(SystemProperty.UserName, strTableName, pstrPrimaryField, pintMasterId);
            if (dstGetRightToModify == null) return false;
            if (int.Parse(dstGetRightToModify.Tables[0].Rows[0][0].ToString()) > 0)
            {
                return false;
            }
            // 
            string strUserCreated = dstGetRightToModify.Tables[1].Rows[0][0].ToString();
            if (!strUserCreated.ToLower().Equals(SystemProperty.UserName.ToLower()))
            {
                PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_DONT_HAVE_RIGHT_TO_DELETE, MessageBoxIcon.Information, new[] { strUserCreated });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the user name modify transaction.
        /// </summary>
        /// <param name="pForm">The p form.</param>
        /// <param name="pstrPrimaryField">The PSTR primary field.</param>
        /// <param name="pintMasterId">The pint master ID.</param>
        public static void UpdateUserNameModifyTransaction(Form pForm, string pstrPrimaryField, int pintMasterId)
        {
            FormInfo formInfor;
            string strTableName = string.Empty;
            foreach (object t in SystemProperty.ArrayForms)
            {
                formInfor = (FormInfo)t;
                if (pForm == formInfor.mForm)
                {
                    strTableName = formInfor.mTableName;
                    break;
                }
            }
            var boUtils = new UtilsBO();
            boUtils.UpdateUserNameModifyTransaction(SystemProperty.UserName, strTableName, pstrPrimaryField, pintMasterId);
        }
    }
}
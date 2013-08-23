using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1TrueDBGrid;
using Microsoft.CSharp;
using PCSAssemblyLoader;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.Common.DS;
using PCSComUtils.DataContext;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.Framework.TableFrame.BO;
using PCSComUtils.Framework.TableFrame.DS;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.PCSExc;
using PCSUtils.Framework.TableFrame;
using PCSUtils.Log;
namespace PCSUtils.Utils
{
    /// <summary>
    /// FormControlComponents. Contain utility functions to manipulate in PCSUtils
    /// </summary>
    public class FormControlComponents
    {
        const int DEFAULT_WIDTH = 1;
        const int THREE_FISRT_CHAR = 3;
        const string TEXTBOX_TYPE = "txt";
        const string CHECKBOX_TYPE = "chk";
        const string COMBOBOX_TYPE = "cbo";
        const string DATETIMEBOX_TYPE = "dtm";
        const string GROUPBOX_TYPE = "grp";
        const string PANEL_TYPE = "pnl";
        const string RADIOBOX_TYPE = "rad";
        const string TABCONTROL_TYPE = "tab";
        const string BUTTON_ADD = "btnAdd";
        const string BUTTON_SAVE = "btnSave";
        const string BUTTON_EDIT = "btnEdit";
        const string BUTTON_DELETE = "btnDelete";
        const string BUTTON_CANCEL = "btnCancel";
        const string CBOCCN = "cboCCN";
        const int DECIMAL_ROUND = 2;

        /// <summary>
        /// Use in 
        /// </summary>
        const string UTC_STRING_FORMAT = "yyyyMMddHHmmss";


        const string THIS = "FormControlComponents";
        static string strOldText = string.Empty;

        //For Treeview
        #region For TreeView Control
        TreeView treeView;
        ArrayList arrTreeNode;
        //-----------
        public FormControlComponents(TreeView treeView, ArrayList arrTreeNode)
        {
            this.treeView = treeView;
            this.arrTreeNode = arrTreeNode;
        }

        public void FillDataTreeView(TreeNode pobjTreeNode, int pintParentNode)
        {
            for (int i = 0; i < arrTreeNode.Count; i++)
            {
                TableNodes objTableNodes = (TableNodes)arrTreeNode[i];

                if (objTableNodes.Parent == pintParentNode)
                {

                    if (pintParentNode != 0)
                    {
                        sys_TableVO voTable = (sys_TableVO)objTableNodes.OBJ;
                        TreeNode objTreeNode = new TreeNode(voTable.TableName);
                        objTreeNode.Tag = objTableNodes.OBJ;
                        pobjTreeNode.Nodes.Add(objTreeNode);
                        FillDataTreeView(objTreeNode, objTableNodes.Current);
                    }
                    else
                    {
                        sys_TableGroupVO voTableGroup = (sys_TableGroupVO)objTableNodes.OBJ;
                        TreeNode objTreeNode = new TreeNode(voTableGroup.TableGroupName);
                        objTreeNode.Tag = objTableNodes.OBJ;
                        treeView.Nodes.Add(objTreeNode);
                        FillDataTreeView(objTreeNode, objTableNodes.Current);
                    }
                }
            }
        }
        #endregion

        #region For Load Control

        public FormControlComponents()
        {
        }

        public bool LoadValueToControl(System.Windows.Forms.Form pfrmForm)
        {
            // if get form unsuccessful 
            if (pfrmForm == null)
            {
                return false;
            }
            // Set uneditable
            // SetUnEditabeControl(frm);
            // Scan all controls in form
            ArrayList arrControls = new ArrayList();
            arrControls = GetChildrenControl(pfrmForm);
            // Scan all controls in form
            for (int i = 0; i < arrControls.Count; i++)
            {
                Control objControl = (Control)arrControls[i];
                // if control is textbox
                if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == TEXTBOX_TYPE)
                {
                    // Get value for this control base on TableName & FieldName properties
                    // ctr.Text = getControlValue(ctr.TableName,ctr.FieldName);
                    //objControl.Text = "test";
                }
                // if control is checkbox
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == CHECKBOX_TYPE)
                {
                    // ctr.Checked = 
                    //CheckBox chkControl = (CheckBox)objControl;
                    //chkControl.Checked = true;
                }
                // if control is Combo box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == COMBOBOX_TYPE)
                {
                    // ctr.Text = getControlValue(ctr.TableName,ctr.FieldName);
                    //objControl.Text = "test";
                }
                // if control is DateTimePicker box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == DATETIMEBOX_TYPE)
                {
                    // ctr.Text = getControlValue(ctr.TableName,ctr.FieldName);
                    //objControl.Text = DateTime.MinValue.ToString();
                }
            }
            return true;
        }
        public bool LoadValueToControl(ArrayList parrControl)
        {
            // if get form unsuccessful 
            if (parrControl == null)
            {
                return false;
            }
            // Scan all controls in list control
            for (int i = 0; i < parrControl.Count; i++)
            {
                Control objControl = (Control)parrControl[i];
                // if control is textbox
                if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == TEXTBOX_TYPE)
                {
                    // Get value for this control base on TableName & FieldName properties
                    // ctr.Text = getControlValue(ctr.TableName,ctr.FieldName);
                    //objControl.Text = "test";
                }
                // if control is checkbox
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == CHECKBOX_TYPE)
                {
                    // ctr.Checked = 
                    //CheckBox chkControl = (CheckBox)objControl;
                    //chkControl.Checked = true;
                }
                // if control is Combo box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == COMBOBOX_TYPE)
                {
                    // ctr.Text = getControlValue(ctr.TableName,ctr.FieldName);
                    //objControl.Text = "test";
                }
                // if control is DateTimePicker box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == DATETIMEBOX_TYPE)
                {
                    // ctr.Text = getControlValue(ctr.TableName,ctr.FieldName);
                    //objControl.Text = "01/01/01";
                }
            }
            return true;
        }

        public bool ClearValueOnControl(System.Windows.Forms.Form pfrmName)
        {
            // if get form unsuccessful 
            if (pfrmName == null)
            {
                return false;
            }
            // Scan all controls in form
            ArrayList arrControls = new ArrayList();
            arrControls = GetChildrenControl(pfrmName);
            for (int i = 0; i < arrControls.Count; i++)
            {
                Control objControl = (Control)arrControls[i];
                // if control is textbox
                if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == TEXTBOX_TYPE)
                {
                    // Clear value on control
                    objControl.Text = string.Empty;
                }
                // if control is checkbox
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == CHECKBOX_TYPE)
                {
                    System.Windows.Forms.CheckBox chkControl = (System.Windows.Forms.CheckBox)objControl;
                    chkControl.Checked = false;
                }
                // if control is Combo box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == COMBOBOX_TYPE)
                {
                    objControl.Text = string.Empty;
                }
                // if control is DateTimePicker box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == DATETIMEBOX_TYPE)
                {
                    objControl.Text = string.Empty;
                }
            }
            return true;
        }
        public bool ClearValueOnControl(ArrayList parrControls)
        {
            // if get form unsuccessful 
            if (parrControls == null)
            {
                return false;
            }
            // Scan all controls in listcontrol
            for (int i = 0; i < parrControls.Count; i++)
            {
                Control objControl = (Control)parrControls[i];
                // if control is textbox
                if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == TEXTBOX_TYPE)
                {
                    // Clear value on control
                    objControl.Text = string.Empty;
                }
                // if control is checkbox
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == CHECKBOX_TYPE)
                {
                    System.Windows.Forms.CheckBox chkControl = (System.Windows.Forms.CheckBox)objControl;
                    chkControl.Checked = false;
                }
                // if control is Combo box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == COMBOBOX_TYPE)
                {
                    objControl.Text = string.Empty;
                }
                // if control is DateTimePicker box
                else if (objControl.Name.Substring(0, THREE_FISRT_CHAR) == DATETIMEBOX_TYPE)
                {
                    objControl.Text = string.Empty;
                }
            }
            return true;
        }

        private ArrayList GetChildrenControl(Control pobjControl)
        {
            ArrayList arrControls = new ArrayList();
            for (int i = 0; i < pobjControl.Controls.Count; i++)
            {
                // Panel
                if (pobjControl.Controls[i].Name.Substring(0, THREE_FISRT_CHAR) == PANEL_TYPE)
                {
                    // get child of ctr control
                    ArrayList arrChild = new ArrayList();
                    arrChild = GetChildrenControl(pobjControl.Controls[i]);
                    for (int j = 0; j < arrChild.Count; j++)
                    {
                        arrControls.Add(arrChild[j]);
                    }
                    arrChild.Clear();
                }
                // GroupBox
                else if (pobjControl.Controls[i].Name.Substring(0, THREE_FISRT_CHAR) == GROUPBOX_TYPE)
                {
                    // get child of ctr control
                    ArrayList arrChild = new ArrayList();
                    arrChild = GetChildrenControl(pobjControl.Controls[i]);
                    for (int j = 0; j < arrChild.Count; j++)
                    {
                        arrControls.Add(arrChild[j]);
                    }
                    arrChild.Clear();
                }
                // tabControl
                else if (pobjControl.Controls[i].Name.Substring(0, THREE_FISRT_CHAR) == TABCONTROL_TYPE)
                {
                    TabControl tabControl = (TabControl)pobjControl.Controls[i];
                    for (int k = 0; k < tabControl.TabCount; k++)
                    {
                        Control cPage = tabControl.TabPages[k];
                        // get children of ctr control
                        ArrayList arrChild = new ArrayList();
                        arrChild = GetChildrenControl(cPage);
                        for (int j = 0; j < arrChild.Count; j++)
                        {
                            arrControls.Add(arrChild[j]);
                        }
                    }
                }
                // any other control
                else
                {
                    arrControls.Add(pobjControl.Controls[i]);
                }
            }
            return arrControls;
        }

        public void SaveForm(System.Windows.Forms.Form pfrmName)
        {
            // Scan all controls in form
            // HACK: SonHT 2005-10-13
            //			ArrayList arrControls = new ArrayList();
            //			arrControls = GetChildrenControl(pfrmName);
            // END: SonHT 2005-10-13
            GetChildrenControl(pfrmName);
        }

        public void SetUnEditabeControl(System.Windows.Forms.Form pfrmName)
        {
            // Scan all controls in form
            ArrayList arrControls = GetChildrenControl(pfrmName);
            for (int i = 0; i < arrControls.Count; i++)
            {
                Control objControl = (Control)arrControls[i];
                string strType = objControl.Name.Substring(0, THREE_FISRT_CHAR);
                bool blnIsTextBox = (strType == TEXTBOX_TYPE);
                bool blnIsCheckBox = (strType == CHECKBOX_TYPE);
                bool blnIsComboBox = (strType == COMBOBOX_TYPE);
                bool blnIsRadioBox = (strType == RADIOBOX_TYPE);
                if (blnIsTextBox || blnIsCheckBox || blnIsComboBox || blnIsRadioBox)
                {
                    objControl.Enabled = false;
                }
            }
        }

        public void SetUnEditabeControl(System.Windows.Forms.Form pfrmName, ArrayList parrControls)
        {
            // Scan all controls in array list
            for (int i = 0; i < parrControls.Count; i++)
            {
                Control objControl = (Control)parrControls[i];
                string strType = objControl.Name.Substring(0, THREE_FISRT_CHAR);
                bool blnIsTextBox = (strType == TEXTBOX_TYPE);
                bool blnIsCheckBox = (strType == CHECKBOX_TYPE);
                bool blnIsComboBox = (strType == COMBOBOX_TYPE);
                bool blnIsRadioBox = (strType == RADIOBOX_TYPE);
                if (blnIsTextBox || blnIsCheckBox || blnIsComboBox || blnIsRadioBox)
                {
                    objControl.Enabled = false;
                }
            }
        }

        public void SetEditabeControl(System.Windows.Forms.Form pfrmName)
        {
            // Scan all controls in form
            ArrayList arrControls = new ArrayList();
            arrControls = GetChildrenControl(pfrmName);
            for (int i = 0; i < arrControls.Count; i++)
            {
                Control objControl = (Control)arrControls[i];
                string strType = objControl.Name.Substring(0, THREE_FISRT_CHAR);
                bool isTextBox = (strType == TEXTBOX_TYPE);
                bool isCheckBox = (strType == CHECKBOX_TYPE);
                bool isComboBox = (strType == COMBOBOX_TYPE);
                bool isRadioBox = (strType == RADIOBOX_TYPE);
                if (isTextBox || isCheckBox || isComboBox || isRadioBox)
                {
                    objControl.Enabled = true;
                }
            }
        }

        public void SetEditabeControl(ArrayList parrControls)
        {
            // Scan all controls in array list
            for (int i = 0; i < parrControls.Count; i++)
            {
                Control objControl = (Control)parrControls[i];
                string strType = objControl.Name.Substring(0, THREE_FISRT_CHAR);
                bool blnIsTextBox = (strType == TEXTBOX_TYPE);
                bool blnIsCheckBox = (strType == CHECKBOX_TYPE);
                bool blnIsComboBox = (strType == COMBOBOX_TYPE);
                bool blnIsRadioBox = (strType == RADIOBOX_TYPE);
                if (blnIsTextBox || blnIsCheckBox || blnIsComboBox || blnIsRadioBox)
                {
                    objControl.Enabled = true;
                }
            }
        }

        public void SetDisableGroupButton(System.Windows.Forms.Form pfrmName, Control pobjControl)
        {
            Control objParentControl = pobjControl.Parent;
            if (objParentControl == null)
            {
                return;
            }
            // click Add button
            if (pobjControl.Name == BUTTON_ADD)
            {
                // scan all friend button
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl.Name == BUTTON_ADD) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_SAVE) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_EDIT) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_DELETE) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_CANCEL) objControl.Enabled = true;
                }
            }
            // click Save button
            if (pobjControl.Name == BUTTON_SAVE)
            {
                // scan all friend button
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl.Name == BUTTON_ADD) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_SAVE) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_EDIT) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_DELETE) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_CANCEL) objControl.Enabled = false;
                }
            }
            // click Edit button
            if (pobjControl.Name == BUTTON_EDIT)
            {
                // scan all friend button
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl.Name == BUTTON_ADD) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_SAVE) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_EDIT) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_DELETE) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_CANCEL) objControl.Enabled = true;
                }
            }
            // click Delete button
            if (pobjControl.Name == BUTTON_DELETE)
            {
                // scan all friend button
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl.Name == BUTTON_ADD) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_SAVE) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_EDIT) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_DELETE) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_CANCEL) objControl.Enabled = false;
                }
            }
            // click Cancel button
            if (pobjControl.Name == BUTTON_CANCEL)
            {
                // scan all friend button
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl.Name == BUTTON_ADD) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_SAVE) objControl.Enabled = false;
                    if (objControl.Name == BUTTON_EDIT) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_DELETE) objControl.Enabled = true;
                    if (objControl.Name == BUTTON_CANCEL) objControl.Enabled = false;
                }
            }
        }

        #endregion

        public static bool CheckDateInCurrentPeriod(DateTime p_dtm_CheckDate)
        {
            try
            {
                UtilsBO boUtil = new UtilsBO();
                DateTime l_dtmFromDate, l_dtmToDate;

                boUtil.GetDateOfCurrentPeriod(out l_dtmFromDate, out l_dtmToDate);

                if (l_dtmFromDate.Equals(l_dtmToDate)) return true;

                // Declare temp variables to ignore time while comparing
                DateTime tmp_dtmCheckDate = new DateTime(p_dtm_CheckDate.Year, p_dtm_CheckDate.Month, p_dtm_CheckDate.Day);
                DateTime tmp_dtmFromDate = new DateTime(l_dtmFromDate.Year, l_dtmFromDate.Month, l_dtmFromDate.Day);
                DateTime tmp_dtmToDate = new DateTime(l_dtmToDate.Year, l_dtmToDate.Month, l_dtmToDate.Day);

                // compare date to current period's "From Date" & "To Date"
                if (tmp_dtmCheckDate.CompareTo(tmp_dtmFromDate) >= 0
                    && tmp_dtmCheckDate.CompareTo(tmp_dtmToDate) <= 0)
                    return true;

                return false;
            }
            catch (PCSException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Check PostDate in configuration date
        /// </summary>
        /// <param name="pdtmPostDate"></param>
        /// <returns></returns>
        public static bool CheckPostDateInConfiguration(DateTime pdtmPostDate)
        {
            //check if user doesn't config postdate
            var dsPostdateConfiguration = new Sys_PostdateConfigurationDS();
            var dstPostdateConfiguration = dsPostdateConfiguration.List(PostDateConfigPurpose.PostDate);
            if (dstPostdateConfiguration.Tables[0].Rows.Count > 0)
            {
                //get server date 
                DateTime dtmServerDate = (new UtilsBO()).GetDBDate();
                var dtmDateToCheck = new DateTime(dtmServerDate.Year, dtmServerDate.Month, dtmServerDate.Day, 0, 0, 0);
                if (dtmDateToCheck.AddDays(-int.Parse(dstPostdateConfiguration.Tables[0].Rows[0][Sys_PostdateConfigurationTable.DAYBEFORE_FLD].ToString())) <= pdtmPostDate)
                    return true;
                var strParam = new string[1];
                strParam[0] = dtmDateToCheck.AddDays(-int.Parse(dstPostdateConfiguration.Tables[0].Rows[0][Sys_PostdateConfigurationTable.DAYBEFORE_FLD].ToString())).ToString(Constants.DATETIME_FORMAT);
                PCSMessageBox.Show(ErrorCode.MESSAGE_CAN_NOT_EDIT_POSTDATE, MessageBoxIcon.Warning, strParam);
                return false;
            }
            else
            {
                var strParam = new string[1];
                strParam[0] = Sys_PostdateConfigurationTable.TABLE_NAME;
                PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxIcon.Warning, strParam);
                return false;
            }
        }
        public static bool IsNumeric(string pstrNumber)
        {
            try
            {
                double dblResult = 0;
                return Double.TryParse(pstrNumber, System.Globalization.NumberStyles.Any,
                    System.Globalization.NumberFormatInfo.CurrentInfo, out dblResult);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPositiveNumeric(string pstrNumber)
        {
            try
            {
                decimal decResult = 0;
                decResult = decimal.Parse(pstrNumber);
                if (decResult <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate percent number between 0 and 100
        /// </summary>
        /// <param name="pstrNumber">Number of percent to be validated</param>
        /// <returns>boolean</returns>
        public static bool IsValidPercent(string pstrNumber)
        {
            try
            {
                if (IsPositiveNumeric(pstrNumber))
                {
                    double dblNumber = 0;
                    if (Double.TryParse(pstrNumber, System.Globalization.NumberStyles.Any,
                                        System.Globalization.NumberFormatInfo.CurrentInfo, out dblNumber))
                    {
                        if ((dblNumber > 0) && (dblNumber <= 100))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pobjControl"></param>
        /// <returns>if (pobjControl.Text.Trim() == string.Empty) return true else return false</returns>
        public static bool CheckMandatory(Control pobjControl)
        {
            if (pobjControl.Text.Trim() == string.Empty)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Thachnn: 23/12/2005
        /// SetDefaultCCN to the TextBox control
        /// Use in the ReportViewer form
        /// </summary>
        /// <param name="ptxtControl"></param>
        /// <author>Thachnn</author>
        /// <date>Wednesday, December 21 2005</date>
        public static void SetDefaultCCN(TextBox ptxtControl)
        {
            const string METHOD_NAME = THIS + ".SetDefaultCCN()";
            try
            {
                if (SystemProperty.CCNID != 0)
                {
                    ptxtControl.Text = SystemProperty.CCNCode;
                    ptxtControl.Tag = SystemProperty.CCNID;
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
        /// SetDefaultMasterLocation
        /// </summary>
        /// <param name="ptxtControl"></param>
        /// <author>Trada</author>
        /// <date>Wednesday, December 21 2005</date>
        public static void SetDefaultMasterLocation(TextBox ptxtControl)
        {
            const string METHOD_NAME = THIS + ".SetDefaultMasterLocation()";
            try
            {
                if (SystemProperty.MasterLocationID != 0)
                {
                    ptxtControl.Text = SystemProperty.MasterLocationCode;
                    ptxtControl.Tag = SystemProperty.MasterLocationID;
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
        public static bool IsPhoneFaxNumber(string pstrNumber)
        {
            // return true if pstrNumber is valid Phone/Fax number
            return !Regex.IsMatch(pstrNumber, "[a-zA-Z]");
        }
        public static bool IsValidEmail(string pstrEmail)
        {
            // Return true if pstrEmail is in valid e-mail format.
            return Regex.IsMatch(pstrEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static void OnEnterControl(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".OnEnterControl()";
            try
            {
                Control objControl = (Control)sender;
                if (objControl.GetType().Equals(typeof(TextBox))
                    || objControl.GetType().Equals(typeof(ComboBox))
                    || objControl.GetType().Equals(typeof(C1Combo))
                    || objControl.GetType().Equals(typeof(C1DateEdit))
                    || objControl.GetType().Equals(typeof(C1TextBox))
                    || objControl.GetType().Equals(typeof(C1NumericEdit))
                    || objControl.GetType().Equals(typeof(NumericUpDown)))
                {
                    if (objControl is TextBox)
                    {
                        strOldText = objControl.Text;
                        ((TextBox)objControl).SelectAll();
                        if (((TextBox)objControl).ReadOnly) return;
                    }
                    else if (objControl is ComboBox)
                    {
                        ((ComboBox)objControl).SelectAll();
                    }
                    else if (objControl is C1Combo)
                    {
                        ((C1Combo)objControl).SelectAll();
                        if (((C1Combo)objControl).ReadOnly) return;
                    }
                    else if (objControl is NumericUpDown)
                    {
                        if (((NumericUpDown)objControl).ReadOnly) return;
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

        public static void OnLeaveControl(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".OnLeaveControl()";
            try
            {
                Control objControl = (Control)sender;
                if (objControl.GetType().Equals(typeof(TextBox))
                    || objControl.GetType().Equals(typeof(ComboBox))
                    || objControl.GetType().Equals(typeof(C1Combo))
                    || objControl.GetType().Equals(typeof(C1DateEdit))
                    || objControl.GetType().Equals(typeof(C1TextBox))
                    || objControl.GetType().Equals(typeof(C1NumericEdit))
                    || objControl.GetType().Equals(typeof(NumericUpDown)))
                {
                    objControl.ResetForeColor();
                    objControl.ResetBackColor();
                    if (objControl.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox txtBox = (TextBox)objControl;
                        if (txtBox.PasswordChar == 0)
                        {
                            bool blnModified = txtBox.Modified;
                            txtBox.Text = txtBox.Text.Trim();
                            txtBox.Modified = blnModified;
                            // Compare old text
                            if ((strOldText == txtBox.Text) && (txtBox.Tag != null)) txtBox.Modified = false;
                        }
                    }
                    else if (objControl.GetType().Equals(typeof(C1TextBox)))
                    {
                        C1TextBox txtC1Text = (C1TextBox)objControl;
                        if (txtC1Text.PasswordChar == 0)
                        {
                            if (txtC1Text.Value != null)
                            {
                                bool blnModified = txtC1Text.Modified;
                                txtC1Text.Value = txtC1Text.Value.ToString().Trim();
                                txtC1Text.Modified = blnModified;
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

        static public void C1DateEdit_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        static public void C1DateEdit_Validated(object sender, System.EventArgs e)
        {
            try
            {
                if (sender.GetType().Equals(typeof(C1DateEdit)))
                {
                    C1DateEdit dtmDate = (C1DateEdit)sender;
                    if (dtmDate.FormatType == FormatTypeEnum.CustomFormat)
                    {
                        if (dtmDate.CustomFormat == Constants.DATETIME_FORMAT)
                        {
                            dtmDate.Value = ((DateTime)dtmDate.Value).Date;
                        }
                        else if (dtmDate.CustomFormat == Constants.DATETIME_FORMAT_HOUR)
                        {
                            DateTime dtmTemp = new DateTime(((DateTime)dtmDate.Value).Year,
                                ((DateTime)dtmDate.Value).Month,
                                ((DateTime)dtmDate.Value).Day,
                                ((DateTime)dtmDate.Value).Hour,
                                ((DateTime)dtmDate.Value).Minute, 0);
                            dtmDate.Value = dtmTemp;
                        }

                    }
                }
            }
            catch
            {

            }
        }

        public static void OnHelpClick(object sender, System.EventArgs e)
        {
            string sFilePath;
            //sFilePath = SystemProperty.ExecutablePath.ToString()+@"\PCSHelp\" +((Button)sender).Parent.GetType().ToString()+".htm";
            sFilePath = ((Button)sender).Parent.GetType().ToString() + ".htm";
            try
            {
                //Process.Start(sFilePath);
                Help.ShowHelp((Button)sender, "PCSHelp.chm", sFilePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không có file help.");
            }
        }

        static public void C1DateEdit_PostValidating(object sender, PostValidationEventArgs e)
        {

        }

        #region Load Data Into C1ComboBox of the Component One control
        public static void PutDataIntoC1ComboBox(C1.Win.C1List.C1Combo cboComboBox,
                                                DataTable dtData,
                                                string strDisplayMember,
                                                string strValueMember,
                                                string strTableName)
        {
            PutDataIntoC1ComboBox(cboComboBox, dtData, strDisplayMember, strValueMember, strTableName, false);
        }

        public static void PutDataIntoC1ComboBox(C1.Win.C1List.C1Combo cboComboBox,
                                                    DataTable dtData,
                                                    string strDisplayMember,
                                                    string strValueMember,
                                                    string strTableName,
                                                    bool blnAddEmptyRow)
        {
            try
            {
                //const string EMPTY_VALUE = "-1";
                if (blnAddEmptyRow)
                {
                    DataRow drNewRow = dtData.NewRow();
                    //drNewRow[strValueMember] = EMPTY_VALUE;
                    dtData.Rows.InsertAt(drNewRow, 0);
                    dtData.AcceptChanges();
                }
                cboComboBox.DataSource = dtData;
                cboComboBox.DisplayMember = strDisplayMember;
                cboComboBox.ValueMember = strValueMember;
                cboComboBox.SelectedIndex = -1;
                // Set default value for cboCCN
                if (cboComboBox.Name == CBOCCN)
                {
                    cboComboBox.SelectedValue = SystemProperty.CCNID;
                }
                //change the column caption based on the Table Config
                ViewTableBO objViewTableBO = new ViewTableBO();

                //First get the TableID for this TableName
                int intTableID = objViewTableBO.GetTableID(strTableName);

                int intComboWidth = DEFAULT_WIDTH;
                //Get the FieldList and store it into dataset variable
                if (intTableID > 0)
                {
                    DataSet dstFieldList = objViewTableBO.getFieldList(intTableID);
                    if (dstFieldList.Tables[0].Rows.Count > 0)
                    {
                        intComboWidth = 0;
                        //Set the column caption for this Combo Box
                        //loop in the Field List, for each field set its corresponding column in the grid to its properties
                        string strFieldName = String.Empty;
                        string strSoftType = string.Empty;
                        // get filter condition
                        DataRow[] drowFields = dstFieldList.Tables[0].Select(sys_TableFieldTable.SORTTYPE_FLD + " <> " + EnumSortType.None.GetHashCode());
                        foreach (DataRow drow in drowFields)
                        {
                            if (drow[sys_TableFieldTable.SORTTYPE_FLD].ToString() == EnumSortType.Ascending.GetHashCode().ToString())
                            {
                                strSoftType += drow[sys_TableFieldTable.FIELDNAME_FLD] + " ASC,";
                            }
                            else
                            {
                                strSoftType += drow[sys_TableFieldTable.FIELDNAME_FLD] + " DESC,";
                            }
                        }
                        // if found soft type
                        if (strSoftType.Length > 0)
                        {
                            strSoftType = strSoftType.Substring(0, strSoftType.Length - 1);
                            // re assign data
                            DataTable dtbTemp = dtData.Copy();
                            dtData.Rows.Clear();
                            foreach (DataRow drowData in dtbTemp.Select("", strSoftType))
                            {
                                DataRow drowNew = dtData.NewRow();
                                drowNew.ItemArray = drowData.ItemArray;
                                dtData.Rows.Add(drowNew);
                            }
                        }

                        foreach (DataColumn dcol in dtData.Columns)
                        {
                            DataRow[] drows = dstFieldList.Tables[0].Select(sys_TableFieldTable.FIELDNAME_FLD + "='" + dcol.ColumnName + "'");
                            if (drows.Length == 0)
                            {
                                cboComboBox.Splits[0].DisplayColumns[dcol.ColumnName].Visible = false;
                                continue;
                            }
                            //get the Field Name
                            strFieldName = ((string)drows[0][sys_TableFieldTable.FIELDNAME_FLD]).Trim();
                            //Set the combo box column caption 
                            //Select language here 
                            cboComboBox.Columns[strFieldName].Caption = drows[0][sys_TableFieldTable.CAPTION_FLD].ToString();

                            //set Column Width
                            cboComboBox.Splits[0].DisplayColumns[strFieldName].Width = int.Parse(drows[0][sys_TableFieldTable.WIDTH_FLD].ToString());

                            intComboWidth = int.Parse(drows[0][sys_TableFieldTable.WIDTH_FLD].ToString());

                            if (intComboWidth > DEFAULT_WIDTH)
                            {
                                cboComboBox.Splits[0].DisplayColumns[strFieldName].Visible = true;
                            }
                            else
                            {
                                cboComboBox.Splits[0].DisplayColumns[strFieldName].Visible = false;
                            }
                        }
                    }
                }
                //cboComboBox.DropDownWidth = intComboWidth;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        public static void PutDataIntoC1TrueDBDropDown(C1TrueDBDropdown dropDropDown,
                                                        DataTable dtData,
                                                        string strDisplayMember,
                                                        string strValueMember)
        {
            try
            {
                dropDropDown.DataSource = dtData;
                dropDropDown.ValueMember = strDisplayMember;
                dropDropDown.DataMember = strValueMember;

                //change the column caption based on the Table Config
                ViewTableBO objViewTableBO = new ViewTableBO();

                //First get the TableID for this TableName
                int intTableID = objViewTableBO.GetTableID(dtData.TableName);

                int intComboWidth = 100;
                //Get the FieldList and store it into dataset variable
                if (intTableID > 0)
                {
                    DataSet dstFieldList = objViewTableBO.getFieldList(intTableID);
                    if (dstFieldList.Tables[0].Rows.Count > 0)
                    {
                        intComboWidth = 0;
                        //Set the column caption for this Combo Box
                        //loop in the Field List, for each field set its corresponding column in the grid to its properties
                        foreach (DataRow drRow in dstFieldList.Tables[0].Rows)
                        {
                            //get the Field Name
                            string strFieldName = ((string)drRow[sys_TableFieldTable.FIELDNAME_FLD]).Trim();

                            //Set the combo box column caption 
                            //Select language here 
                            dropDropDown.Columns[strFieldName].Caption = drRow[sys_TableFieldTable.CAPTION_FLD].ToString();

                            //set Column Width
                            dropDropDown.DisplayColumns[strFieldName].Width = int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());

                            //Set Invisible or not invisible column
                            dropDropDown.DisplayColumns[strFieldName].Visible = !(bool)drRow[sys_TableFieldTable.INVISIBLE_FLD];

                            intComboWidth += int.Parse(drRow[sys_TableFieldTable.WIDTH_FLD].ToString());
                        }

                    }
                }
                //cboComboBox.DropDownWidth = intComboWidth;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void DisplayComboBoxValue(ComboBox cboCombo, string strFieldValueName, string strValue)
        {
            for (int i = 0; i < cboCombo.Items.Count; i++)
            {
                if (((DataRowView)cboCombo.Items[i])[strFieldValueName].ToString().Trim().ToUpper() == strValue.Trim().ToUpper())
                {
                    cboCombo.SelectedIndex = i;
                    return;
                    //break;
                }
            }
            cboCombo.SelectedIndex = -1;
        }

        /// <summary>
        /// This method used to add 'select' column to the first position of the grid
        /// </summary>
        /// <param name="pdstTempDataSet">Origin DataSet</param>
        /// <returns>The DataSet which has 'select' column at the first column</returns>
        /// <author>Trada</author>
        /// <date>Tuesday, January 3 2006</date>
        public static DataSet AddSelectColumnToTheFirstPositionOfGrid(DataSet pdstTempDataSet)
        {
            const string METHOD_NAME = THIS + ".AddSelectColumnToTheFirstPositionOfGrid()";
            const string SELECT = "Select";
            const string TABLE_NAME = "ReturnTable";
            try
            {

                DataSet dstData = new DataSet();
                dstData.Tables.Add(TABLE_NAME);
                dstData.Tables[0].Columns.Add(SELECT, typeof(bool));
                for (int i = 0; i < pdstTempDataSet.Tables[0].Columns.Count; i++)
                {
                    dstData.Tables[0].Columns.Add(pdstTempDataSet.Tables[0].Columns[i].ColumnName, pdstTempDataSet.Tables[0].Columns[i].DataType);
                }
                foreach (DataRow drow in pdstTempDataSet.Tables[0].Rows)
                {
                    DataRow drowData = dstData.Tables[0].NewRow();
                    foreach (DataColumn dcol in pdstTempDataSet.Tables[0].Columns)
                    {
                        drowData[dcol.ColumnName] = drow[dcol.ColumnName];
                    }
                    dstData.Tables[0].Rows.Add(drowData);
                }
                foreach (DataRow drow in dstData.Tables[0].Rows)
                {
                    drow[SELECT] = false;
                }
                return dstData;
            }
            catch (PCSException ex)
            {
                throw new PCSException(ex.mCode, METHOD_NAME, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static DataRowView OpenSearchForm(string strTableNameOrView,
            string strFilterField, string strFilterFieldValue,
            string pstrWhereClause)
        {
            string strWhereClause = " WHERE 1=1 ";

            #region /// HACKED: Thachnn: fix bug injection
            strFilterFieldValue = KillInjection(strFilterFieldValue);
            pstrWhereClause = KillInjectionInLikeClause(pstrWhereClause);
            #endregion /// ENDHACKED: Thachnn: fix bug injection

            if (pstrWhereClause != null && pstrWhereClause.Trim().Length > 0)
                strWhereClause += " AND " + pstrWhereClause;

            strWhereClause += (new UtilsBO()).GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);

            //call the OpenSearchForm					
            ViewTable objViewTableForm = new ViewTable(strTableNameOrView);
            objViewTableForm.ViewOnly = true; //blnViewOnly;
            objViewTableForm.GetData = true;
            objViewTableForm.WhereClause = strWhereClause;

            objViewTableForm.ReturnField = String.Empty;

            objViewTableForm.FilterField1 = strFilterField;
            objViewTableForm.FilterFieldValue1 = strFilterFieldValue;

            objViewTableForm.FilterField2 = String.Empty;
            objViewTableForm.FilterFieldValue2 = String.Empty;


            DataRowView drvReturn = null;
            if (objViewTableForm.ShowDialog() == DialogResult.OK)
            {
                drvReturn = objViewTableForm.ReturnDataRowView;
            }
            objViewTableForm.Close();
            return drvReturn;
        }

        public static DataRowView OpenSearchFormWithWhere(string strTableNameOrView,
            string strFilterField, string strFilterFieldValue,
            string pstrWhereClause, bool pblnOpenForm)
        {
            string strWhereClause = " WHERE 1=1 ";

            #region /// HACKED: Thachnn: fix bug injection
            strFilterFieldValue = KillInjection(strFilterFieldValue);
            pstrWhereClause = KillInjectionInLikeClause(pstrWhereClause);
            #endregion /// ENDHACKED: Thachnn: fix bug injection

            if (pstrWhereClause != null && pstrWhereClause.Trim().Length > 0)
                strWhereClause += " AND " + pstrWhereClause;

            string strConditionByRecord = (new UtilsBO()).GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);

            DataRowView drvReturn = null;
            if (!pblnOpenForm)
            {
                ViewTableBO objTableBO = new ViewTableBO();
                DataSet dstFieldList = objTableBO.getFieldList(strTableNameOrView);
                string strFilter = string.Empty;
                if ((strFilterField == string.Empty) || (strFilterFieldValue == string.Empty))
                {
                    // do nothing
                }
                else
                {
                    strFilter = " AND " + strTableNameOrView + "." + strFilterField + " LIKE N'" + strFilterFieldValue.Replace("'", "''") + "%'";
                }

                string strSqlSelectUpdateCommand = objTableBO.BuildSQLSelect(dstFieldList, strTableNameOrView, true);
                strSqlSelectUpdateCommand += " " + strWhereClause + strFilter + " " + strConditionByRecord;

                //DataTable dt = objUtilsBO.GetRows(strTableNameOrView,strFilterField,strFilterFieldValue,phashOtherConditions, strConditionByRecord);
                DataTable dt = objTableBO.getDataList(strSqlSelectUpdateCommand, strTableNameOrView).Tables[0];
                if (dt.Rows.Count == 1)	// HACKED: Thachnn: short circuit, no need to filter in the below ELSE section
                    drvReturn = dt.DefaultView[0];
                else
                    drvReturn = OpenSearchForm(strTableNameOrView, strFilterField, strFilterFieldValue, pstrWhereClause);
            }
            else
                drvReturn = OpenSearchForm(strTableNameOrView, strFilterField, strFilterFieldValue, pstrWhereClause);

            return drvReturn;
        }

        // ******************** ROOT OpenSearchForm ***********************
        public static DataRowView OpenSearchForm(string strTableNameOrView,
            string strFilterField, string strFilterFieldValue,
            Hashtable phashOtherConditions, bool pblnAlwayOpenForm)
        {
            string strWhereClause = Constants.WHERE_KEYWORD + Constants.WHITE_SPACE + " 1=1 ";

            //build the where clause
            if (phashOtherConditions != null)
            {
                IDictionaryEnumerator myEnumerator = phashOtherConditions.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    // HACK: dungla 10-20-2005
                    // if value is DBNull.Value then we must compare value with IS NULL in database.
                    if (myEnumerator.Value == DBNull.Value)
                        strWhereClause += " AND (" + strTableNameOrView + "." + myEnumerator.Key.ToString().Trim() + " IS NULL)";
                    else
                        strWhereClause += " AND (" + strTableNameOrView + "." + myEnumerator.Key.ToString().Trim() + "=N'" + myEnumerator.Value + "')";
                    // END: dungla 10-20-2005
                }
            }

            #region /// HACKED: Thachnn: fix bug injection
            strFilterFieldValue = KillInjection(strFilterFieldValue);
            strWhereClause = KillInjectionInLikeClause(strWhereClause);
            #endregion /// ENDHACKED: Thachnn: fix bug injection

            UtilsBO objUtilsBO = new UtilsBO();

            string strConditionByRecord = objUtilsBO.GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);
            strWhereClause += strConditionByRecord;


            ViewTable objViewTableForm;
            //search row from UtilsBo old parameter blnOpenFormOnly

            if (pblnAlwayOpenForm)
            {
                DataTable dt = objUtilsBO.GetRows(strTableNameOrView, strFilterField, strFilterFieldValue, phashOtherConditions,
                    strConditionByRecord);
                if (dt == null)
                {
                    /// HACKED: Thachnn: fix the flash screen when OpenSearchForm with DIDN'T_CONFIG table
                    string[] arrstr = { strTableNameOrView };
                    PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        arrstr);
                    return null;
                    /// ENDHACKED: Thachnn
                }
                //open the search form
                objViewTableForm = new ViewTable(strTableNameOrView);
                objViewTableForm.ViewOnly = true; //blnViewOnly;
                objViewTableForm.GetData = true;
                objViewTableForm.WhereClause = strWhereClause;

                objViewTableForm.ReturnField = String.Empty;
                objViewTableForm.FilterField1 = strFilterField;
                objViewTableForm.FilterFieldValue1 = strFilterFieldValue;

                objViewTableForm.FilterField2 = String.Empty;
                objViewTableForm.FilterFieldValue2 = String.Empty;

                DataRowView drvReturn = null;
                if (objViewTableForm.ShowDialog() == DialogResult.OK)
                {
                    drvReturn = objViewTableForm.ReturnDataRowView;
                }
                objViewTableForm.Close();
                return drvReturn;
            }
            else
            {
                //first search the result and then
                //based on the searched result
                //the program will determine to open or not
                ViewTableBO objTableBO = new ViewTableBO();
                DataSet dstFieldList = objTableBO.getFieldList(strTableNameOrView);
                string strFilter = string.Empty;
                if ((strFilterField != string.Empty) && (strFilterFieldValue != string.Empty))
                    strFilter = " AND " + strTableNameOrView + "." + strFilterField + " LIKE N'" + strFilterFieldValue.Replace("'", "''") + "%'";

                string strSqlSelectUpdateCommand = objTableBO.BuildSQLSelect(dstFieldList, strTableNameOrView, true);
                strSqlSelectUpdateCommand += " " + strWhereClause + strFilter + " " + strConditionByRecord;

                DataTable dt = objTableBO.getDataList(strSqlSelectUpdateCommand, strTableNameOrView).Tables[0];
                if (dt.Rows.Count == 1)	// HACKED: Thachnn: short circuit, no need to filter in the below ELSE section
                    return dt.DefaultView[0];
                else
                {
                    //call the OpenSearchForm
                    objViewTableForm = new ViewTable(strTableNameOrView);
                    objViewTableForm.ViewOnly = true; //blnViewOnly;
                    objViewTableForm.GetData = true;
                    objViewTableForm.WhereClause = strWhereClause;

                    objViewTableForm.ReturnField = String.Empty;
                    objViewTableForm.FilterField1 = strFilterField;
                    objViewTableForm.FilterFieldValue1 = strFilterFieldValue;

                    objViewTableForm.FilterField2 = String.Empty;
                    objViewTableForm.FilterFieldValue2 = String.Empty;

                    DataRowView drvReturn = null;
                    if (objViewTableForm.ShowDialog() == DialogResult.OK)
                        drvReturn = objViewTableForm.ReturnDataRowView;
                    objViewTableForm.Close();
                    return drvReturn;
                }
            }
        }

        /// <summary>
        /// Open search form for select multiple row
        /// </summary>
        /// <param name="strTableNameOrView">Table Name</param>
        /// <param name="strFilterField">Filter Field</param>
        /// <param name="strFilterFieldValue">Filter Value</param>
        /// <param name="pstrWhereClause">Where Clause</param>
        /// <param name="blnOpenFormOnly"></param>
        /// <returns>DataTable</returns>
        /// <author>DungLA</author>
        /// <date>31-03-2006</date>
        public static DataTable OpenSearchFormForMultiSelectedRow(string strTableNameOrView,
            string strFilterField, string strFilterFieldValue, string pstrWhereClause, bool blnOpenFormOnly)
        {
            string strWhereClause = " WHERE 1=1";
            if (pstrWhereClause != null && pstrWhereClause.Trim() != string.Empty)
                strWhereClause += " AND " + pstrWhereClause;

            #region /// HACKED: Thachnn: fix bug injection
            strFilterFieldValue = KillInjection(strFilterFieldValue);
            pstrWhereClause = KillInjectionInLikeClause(pstrWhereClause);
            #endregion /// ENDHACKED: Thachnn: fix bug injection

            UtilsBO objUtilsBO = new UtilsBO();

            string strConditionByRecord = objUtilsBO.GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);
            strWhereClause += strConditionByRecord;


            ViewTable objViewTableForm;
            try
            {
                //search row from UtilsBo
                if (blnOpenFormOnly)
                {
                    // edited: dungla 10-04-2006 fix bug for NganNT & NgaHT
                    DataTable dt = objUtilsBO.GetRowsWithWhere(strTableNameOrView, strFilterField, strFilterFieldValue, strWhereClause);
                    if (dt == null)
                    {
                        /// HACKED: Thachnn: fix the flash screen when OpenSearchForm with DIDN'T_CONFIG table
                        string[] arrstr = { strTableNameOrView };
                        PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            arrstr);
                        return null;
                        /// ENDHACKED: Thachnn
                    }
                    //open the search form
                    objViewTableForm = new ViewTable(strTableNameOrView, true);
                    objViewTableForm.ViewOnly = true; //blnViewOnly;
                    objViewTableForm.GetData = true;
                    objViewTableForm.WhereClause = strWhereClause;

                    objViewTableForm.ReturnField = String.Empty;

                    objViewTableForm.FilterField1 = strFilterField;
                    objViewTableForm.FilterFieldValue1 = strFilterFieldValue;

                    objViewTableForm.FilterField2 = String.Empty;
                    objViewTableForm.FilterFieldValue2 = String.Empty;

                    DataTable dtbReturn = new DataTable();
                    if (objViewTableForm.ShowDialog() == DialogResult.OK)
                        dtbReturn = objViewTableForm.ReturnTable;
                    objViewTableForm.Close();
                    return dtbReturn;
                }
                else
                {
                    //first search the result and then
                    //based on the searched result
                    //the program will determine to open or not
                    DataTable dt = objUtilsBO.GetRowsWithWhere(strTableNameOrView, strFilterField, strFilterFieldValue, strWhereClause);
                    if (dt.Rows.Count == 1)	// HACKED: Thachnn: short circuit, no need to filter in the below ELSE section
                        return dt;
                    else
                    {
                        //call the OpenSearchForm
                        objViewTableForm = new ViewTable(strTableNameOrView, true);
                        objViewTableForm.ViewOnly = true; //blnViewOnly;
                        objViewTableForm.GetData = true;
                        objViewTableForm.WhereClause = strWhereClause;

                        objViewTableForm.ReturnField = String.Empty;
                        objViewTableForm.FilterField1 = strFilterField;
                        objViewTableForm.FilterFieldValue1 = strFilterFieldValue;
                        objViewTableForm.FilterField2 = String.Empty;
                        objViewTableForm.FilterFieldValue2 = String.Empty;

                        DataTable dtbReturn = new DataTable();
                        if (objViewTableForm.ShowDialog() == DialogResult.OK)
                            dtbReturn = objViewTableForm.ReturnTable;
                        objViewTableForm.Close();
                        return dtbReturn;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region	FINISHED: INTERFACE CALL ROOT OpenSearchForm function
        //cuonglv
        public static DataRowView OpenSearch(string strFilterFieldName, string strFilterFieldValue, string strKeyofTable, string strTableNameOrView, Hashtable htbOrtherFilterCondition, bool HasShowDialog, bool isFullScreen)
        {
            DataRowView Returndr = null;
            string strFilterConditionOnlyHashTable = string.Empty;//Condition in HashTable
            StringBuilder strFilterCondition = new StringBuilder();
            strFilterFieldValue = KillInjection(strFilterFieldValue);

            if (htbOrtherFilterCondition != null)
            {
                var myEnumerator = htbOrtherFilterCondition.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    if (myEnumerator.Value == DBNull.Value)
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTableNameOrView + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("IS NULL");
                    }
                    else if (myEnumerator.Value.ToString().ToUpper().Contains("IS NOT NULL"))
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTableNameOrView + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("IS NOT NULL");
                    }
                    else
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTableNameOrView + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("=N'");
                        strFilterCondition.Append(myEnumerator.Value);
                        strFilterCondition.Append("'");
                    }
                }
                strFilterConditionOnlyHashTable = KillInjectionInLikeClause(strFilterCondition.ToString());
            }
            if (!string.IsNullOrEmpty(strFilterFieldName) && strFilterFieldValue != string.Empty)
            {
                if (strFilterCondition.Length > 0)
                    strFilterCondition.Append(" AND ");
                strFilterCondition.Append(strTableNameOrView + "." + strFilterFieldName);
                strFilterCondition.Append(" LIKE N'%");
                strFilterCondition.Append(strFilterFieldValue.Replace("'", "''"));
                strFilterCondition.Append("%'");
            }
            #region /// HACKED: Thachnn: fix bug injection

            StringBuilder sql = new StringBuilder();
            sql.Append(KillInjectionInLikeClause(strFilterCondition.ToString()));

            #endregion /// ENDHACKED: Thachnn: fix bug injection

            //var strConditionByRecord = Utilities.Instance.GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);
            //sql.Append(strConditionByRecord);

            #region Get Data

            StringBuilder sqlComand = new StringBuilder();

            MST_SearchPartyBO objBO = new MST_SearchPartyBO();
            int TotalRecord = objBO.GetRowCount(strTableNameOrView, sql.ToString());
            int TotalPage = TotalRecord / PCSComUtils.Common.Constants.CountPage;
            if (TotalRecord % PCSComUtils.Common.Constants.CountPage != 0)
                TotalPage++;
            if (TotalPage > 1)
            {
                sqlComand.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + strKeyofTable + ") AS STT,*  FROM ");
                sqlComand.Append(strTableNameOrView);
                if (sql.Length > 0)
                {
                    sqlComand.Append(" WHERE ");
                    sqlComand.Append(sql);
                }
                sqlComand.Append(") AS v_Table WHERE  STT >0 AND STT<= " + PCSComUtils.Common.Constants.CountPage.ToString());
            }
            else
            {
                sqlComand.Append(" SELECT ROW_NUMBER() OVER(ORDER BY " + strKeyofTable + ") AS STT,* FROM ");
                sqlComand.Append(strTableNameOrView);
                if (sql.Length > 0)
                {
                    sqlComand.Append(" WHERE ");
                    sqlComand.Append(sql);
                }
            }
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            oconPCS = new OleDbConnection(PCSComUtils.DataAccess.Utils.Instance.OleDbConnectionString);
            ocmdPCS = new OleDbCommand(sqlComand.ToString(), oconPCS);
            ocmdPCS.Connection.Open();
            ocmdPCS.CommandTimeout = 1000;//With row number>6000;
            var odadPCS = new OleDbDataAdapter(ocmdPCS);
            odadPCS.Fill(dstPCS, strTableNameOrView);
            #endregion
            if (HasShowDialog)
            {
                #region Check role of record
                //var dt = Utilities.Instance.GetRows(strTableNameOrView, strFilterField, strFilterFieldValue,
                //                                 htbOrtherFilterCondition, strConditionByRecord);
                //if (dt == null)
                //{
                //    /// HACKED: Thachnn: fix the flash screen when OpenSearchForm with DIDN'T_CONFIG table
                //    string[] arrstr = { strTableNameOrView };
                //    PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxButtons.OK,
                //                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, arrstr);
                //    return null;
                //    /// ENDHACKED: Thachnn
                //}
                #endregion
                PCSUtils.MasterSetup.SystemTable.ViewData frmViewData = new PCSUtils.MasterSetup.SystemTable.ViewData(strFilterFieldName, strFilterFieldValue, strKeyofTable, strTableNameOrView, htbOrtherFilterCondition);
                frmViewData.dset = dstPCS;
                frmViewData.TotalRecord = TotalRecord;
                frmViewData.GetTotalPage = TotalPage;
                frmViewData.strTable = strTableNameOrView;
                frmViewData.strKeyWord = strKeyofTable;
                frmViewData.strCondition = strFilterCondition.ToString();
                frmViewData.strOnlyHashTable = strFilterConditionOnlyHashTable;
                if (isFullScreen)
                    frmViewData.WindowState = FormWindowState.Maximized;
                frmViewData.Text = strTableNameOrView;
                frmViewData.ShowDialog();
                if (frmViewData.ReturnDataRowView != null)
                {
                    Returndr = frmViewData.ReturnDataRowView;
                }
                frmViewData.Close();
                return Returndr;
            }
            else
            {
                if (dstPCS.Tables[0].Rows.Count == 1)
                    return dstPCS.Tables[0].DefaultView[0];
                //var dt = objTableBO.getDataList(strSqlSelectUpdateCommand, strTableNameOrView).Tables[0];

                PCSUtils.MasterSetup.SystemTable.ViewData frmViewData = new PCSUtils.MasterSetup.SystemTable.ViewData();
                frmViewData.dset = dstPCS;
                frmViewData.TotalRecord = TotalRecord;
                frmViewData.GetTotalPage = TotalPage;
                frmViewData.strTable = strTableNameOrView;
                frmViewData.strKeyWord = strKeyofTable;
                string strCond = string.Empty;
                if (strFilterCondition.ToString().Trim() != string.Empty)
                    strCond += " AND " + strFilterCondition.ToString();
                frmViewData.strCondition = strCond;
                frmViewData.Text = strTableNameOrView;
                frmViewData.ShowDialog();
                if (frmViewData.ReturnDataRowView != null)
                    Returndr = frmViewData.ReturnDataRowView;
                frmViewData.Close();
                return Returndr;
            }
        }
        public static DataRow[] OpenSearchMultiSelected(string strFilterFieldName, string strFilterFieldValue, string strKeyofTable, string strTableNameOrView, Hashtable htbOrtherFilterCondition, bool HasShowDialog, bool isFullScreen)
        {
            DataRow[] returnSelectedRow = null;
            string strFilterConditionOnlyHashTable = string.Empty;//Condition in HashTable
            StringBuilder strFilterCondition = new StringBuilder();
            strFilterFieldValue = KillInjection(strFilterFieldValue);

            if (htbOrtherFilterCondition != null)
            {
                var myEnumerator = htbOrtherFilterCondition.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    if (myEnumerator.Value == DBNull.Value)
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTableNameOrView + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("IS NULL");
                    }
                    else if (myEnumerator.Value.ToString().ToUpper().Contains("IS NOT NULL"))
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTableNameOrView + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("IS NOT NULL");
                    }
                    else
                    {
                        if (strFilterCondition.Length > 0)
                            strFilterCondition.Append(" AND ");
                        strFilterCondition.Append(strTableNameOrView + "." + myEnumerator.Key.ToString().Trim());
                        strFilterCondition.Append("=N'");
                        strFilterCondition.Append(myEnumerator.Value);
                        strFilterCondition.Append("'");
                    }
                }
                strFilterConditionOnlyHashTable = KillInjectionInLikeClause(strFilterCondition.ToString());
            }
            if (!string.IsNullOrEmpty(strFilterFieldName) && strFilterFieldValue != string.Empty)
            {
                if (strFilterCondition.Length > 0)
                    strFilterCondition.Append(" AND ");
                strFilterCondition.Append(strTableNameOrView + "." + strFilterFieldName);
                strFilterCondition.Append(" LIKE N'%");
                strFilterCondition.Append(strFilterFieldValue.Replace("'", "''"));
                strFilterCondition.Append("%'");
            }
            #region /// HACKED: Thachnn: fix bug injection

            StringBuilder sql = new StringBuilder();
            sql.Append(KillInjectionInLikeClause(strFilterCondition.ToString()));

            #endregion /// ENDHACKED: Thachnn: fix bug injection

            //var strConditionByRecord = Utilities.Instance.GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);
            //sql.Append(strConditionByRecord);

            #region Get Data

            StringBuilder sqlComand = new StringBuilder();

            MST_SearchPartyBO objBO = new MST_SearchPartyBO();
            int TotalRecord = objBO.GetRowCount(strTableNameOrView, sql.ToString());
            int TotalPage = TotalRecord / PCSComUtils.Common.Constants.CountPage;
            if (TotalRecord % PCSComUtils.Common.Constants.CountPage != 0)
                TotalPage++;
            if (TotalPage > 1)
            {
                sqlComand.Append("SELECT CAST(0 AS bit) As Checks, * FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + strKeyofTable + ") AS STT,*  FROM ");
                sqlComand.Append(strTableNameOrView);
                if (sql.Length > 0)
                {
                    sqlComand.Append(" WHERE ");
                    sqlComand.Append(sql);
                }
                sqlComand.Append(") AS v_Table WHERE  STT >0 AND STT<= " + PCSComUtils.Common.Constants.CountPage.ToString());
            }
            else
            {
                sqlComand.Append(" SELECT CAST(0 AS bit) As Checks, ROW_NUMBER() OVER(ORDER BY " + strKeyofTable + ") AS STT,* FROM ");
                sqlComand.Append(strTableNameOrView);
                if (sql.Length > 0)
                {
                    sqlComand.Append(" WHERE ");
                    sqlComand.Append(sql);
                }
            }
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            oconPCS = new OleDbConnection(PCSComUtils.DataAccess.Utils.Instance.OleDbConnectionString);
            ocmdPCS = new OleDbCommand(sqlComand.ToString(), oconPCS);
            ocmdPCS.Connection.Open();

            var odadPCS = new OleDbDataAdapter(ocmdPCS);
            odadPCS.Fill(dstPCS, strTableNameOrView);
            #endregion
            if (HasShowDialog)
            {
                #region Check role of record
                //var dt = Utilities.Instance.GetRows(strTableNameOrView, strFilterField, strFilterFieldValue,
                //                                 htbOrtherFilterCondition, strConditionByRecord);
                //if (dt == null)
                //{
                //    /// HACKED: Thachnn: fix the flash screen when OpenSearchForm with DIDN'T_CONFIG table
                //    string[] arrstr = { strTableNameOrView };
                //    PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE, MessageBoxButtons.OK,
                //                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, arrstr);
                //    return null;
                //    /// ENDHACKED: Thachnn
                //}
                #endregion
                PCSUtils.MasterSetup.SystemTable.ViewData frmViewData = new PCSUtils.MasterSetup.SystemTable.ViewData(strFilterFieldName, strFilterFieldValue, strKeyofTable, strTableNameOrView, htbOrtherFilterCondition);
                frmViewData.dset = dstPCS;
                frmViewData.TotalRecord = TotalRecord;
                frmViewData.GetTotalPage = TotalPage;
                frmViewData.strTable = strTableNameOrView;
                frmViewData.strKeyWord = strKeyofTable;
                frmViewData.strCondition = strFilterCondition.ToString();
                frmViewData.strOnlyHashTable = strFilterConditionOnlyHashTable;
                frmViewData.SelectMultiRow = true;
                if (isFullScreen)
                    frmViewData.WindowState = FormWindowState.Maximized;
                frmViewData.ShowDialog();
                if (frmViewData.SelectedRows != null)
                {
                    returnSelectedRow = frmViewData.SelectedRows;
                }
                frmViewData.Close();
                return returnSelectedRow;
            }
            else
            {
                if (dstPCS.Tables[0].Rows.Count == 1)
                    returnSelectedRow = dstPCS.Tables[0].Select();
                //var dt = objTableBO.getDataList(strSqlSelectUpdateCommand, strTableNameOrView).Tables[0];

                PCSUtils.MasterSetup.SystemTable.ViewData frmViewData = new PCSUtils.MasterSetup.SystemTable.ViewData();
                frmViewData.dset = dstPCS;
                frmViewData.TotalRecord = TotalRecord;
                frmViewData.GetTotalPage = TotalPage;
                frmViewData.strTable = strTableNameOrView;
                frmViewData.strKeyWord = strKeyofTable;
                string strCond = string.Empty;
                if (strFilterCondition.ToString().Trim() != string.Empty)
                    strCond += " AND " + strFilterCondition.ToString();
                frmViewData.strCondition = strCond;
                frmViewData.ShowDialog();
                if (frmViewData.SelectedRows != null)
                    returnSelectedRow = frmViewData.SelectedRows;
                frmViewData.Close();
                return returnSelectedRow;
            }
        }
        /// <summary>
        /// HACKED: Thachnn: REFACTORING using the ROOT
        /// Just an INTERFACE for call the ROOT OpenSearchForm
        /// </summary>				
        public static DataRowView OpenSearchForm(string strTableNameOrView,
            string strFilterField, string strFilterFieldValue,
            Hashtable phashOtherConditions)
        {
            // Call the ROOT OpenSearchForm
            // Begin Del by SonHT
            //			return     		      OpenSearchForm(		strTableNameOrView,
            //				strFilterField,		  strFilterFieldValue,
            //				phashOtherConditions,		 false) ;
            // End Del by SonHT
            //GetRows(string pstrTableName, string pstrFieldName, string pstrFieldValue, Hashtable phashOtherConditions);
            //strWhereClause sample = "WHERE TABLENAME.FIELDNAME = VALUE"
            string strWhereClause = Constants.WHERE_KEYWORD + Constants.WHITE_SPACE + " 1=1 ";

            //build the where clause
            if (phashOtherConditions != null)
            {
                IDictionaryEnumerator myEnumerator = phashOtherConditions.GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    strWhereClause += " AND (" + strTableNameOrView + "." + myEnumerator.Key.ToString().Trim() + "='" + myEnumerator.Value + "')";
                }
            }

            #region /// HACKED: Thachnn: fix bug injection
            strFilterFieldValue = KillInjection(strFilterFieldValue);
            strWhereClause = KillInjectionInLikeClause(strWhereClause);
            #endregion /// ENDHACKED: Thachnn: fix bug injection

            UtilsBO objUtilsBO = new UtilsBO();

            string strConditionByRecord = objUtilsBO.GetConditionByRecord(SystemProperty.UserName, strTableNameOrView);
            strWhereClause += strConditionByRecord;

            ViewTable objViewTableForm;
            try
            {
                //search row from UtilsBo
                bool blnOpenFormOnly = true; // alway open form
                if (blnOpenFormOnly)
                {
                    DataTable dt = objUtilsBO.GetRows(strTableNameOrView, string.Empty, string.Empty, phashOtherConditions, strConditionByRecord);
                    if (dt == null)
                    {
                        /// HACKED: Thachnn: fix the flash screen when OpenSearchForm with DIDN'T_CONFIG table
                        string[] arrstr = { strTableNameOrView };
                        PCSMessageBox.Show(ErrorCode.MESSAGE_CONFIGURED_TABLE,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            arrstr);
                        return null;
                        /// ENDHACKED: Thachnn
                    }
                    // open the search form
                    objViewTableForm = new ViewTable(strTableNameOrView);
                    objViewTableForm.ViewOnly = true; //blnViewOnly;
                    objViewTableForm.GetData = true;
                    objViewTableForm.WhereClause = strWhereClause;

                    objViewTableForm.ReturnField = String.Empty;

                    objViewTableForm.FilterField1 = strFilterField;
                    objViewTableForm.FilterFieldValue1 = strFilterFieldValue;

                    objViewTableForm.FilterField2 = String.Empty;
                    objViewTableForm.FilterFieldValue2 = String.Empty;

                    DataRowView drvReturn = null;
                    if (objViewTableForm.ShowDialog() == DialogResult.OK)
                    {
                        drvReturn = objViewTableForm.ReturnDataRowView;
                    }
                    objViewTableForm.Close();
                    return drvReturn;
                }
                #region DEL By SonHT

                else
                {
                    //first search the result and then
                    //based on the searched result
                    //the program will determine to open or not
                    DataTable dt = objUtilsBO.GetRows(strTableNameOrView, strFilterField, strFilterFieldValue, phashOtherConditions, strConditionByRecord);
                    //					if (dt.Rows.Count == 1)	// HACKED: Thachnn: short circuit, no need to filter in the below ELSE section
                    //					{
                    //						return dt.DefaultView[0];
                    //					}
                    //					else
                    //					{
                    //call the OpenSearchForm
                    objViewTableForm = new ViewTable(strTableNameOrView);
                    objViewTableForm.ViewOnly = true; //blnViewOnly;
                    objViewTableForm.GetData = true;
                    objViewTableForm.WhereClause = strWhereClause;

                    objViewTableForm.ReturnField = String.Empty;
                    //					if (dt.Rows.Count > 0)
                    //					{
                    objViewTableForm.FilterField1 = strFilterField;
                    objViewTableForm.FilterFieldValue1 = strFilterFieldValue;
                    //					}
                    //					else
                    //					{
                    //						objViewTableForm.FilterField1 = String.Empty;
                    //						objViewTableForm.FilterFieldValue1 = String.Empty;
                    //					}

                    objViewTableForm.FilterField2 = String.Empty;
                    objViewTableForm.FilterFieldValue2 = String.Empty;

                    DataRowView drvReturn = null;
                    if (objViewTableForm.ShowDialog() == DialogResult.OK)
                    {
                        drvReturn = objViewTableForm.ReturnDataRowView;
                    }
                    objViewTableForm.Close();
                    return drvReturn;
                    //					}
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static DataRowView OpenSearchForm(string strTableNameOrView,
            string strWhereClause)
        {
            /// Call the OpenSearchForm(string strTableNameOrView,
            ///  string strFilterField, string strFilterFieldValue,
            /// string  strWhereClause)
            return OpenSearchForm(strTableNameOrView,
                string.Empty, string.Empty,
                strWhereClause);
        }

        #endregion

        /// <summary>
        ///		Store the designed inteface of the grid 
        ///       and later will get this setting back at runtime
        ///       - Store Caption
        ///       - Store Width
        ///       - Store color
        ///       - This function must be called in the form load of every form to store the design
        ///       Note : In the design of the grid we have to setup two fileds
        ///       DataField : Is the same exactly with database table
        ///       Caption : is the caption will be displayed
        ///       
        ///       After calling this function,the output of this function must be kept
        ///       in memory for later restoring it.
        /// </summary>
        /// <param name="dbGrid">TrueDBGrid</param>
        /// <returns>Layout Table</returns>
        public static DataTable StoreGridLayout(C1.Win.C1TrueDBGrid.C1TrueDBGrid dbGrid)
        {
            DataTable dtGridDesign;
            DataColumn dcolMyColumn;
            dtGridDesign = new DataTable(Store_GridLayOutTable.TABLE_NAME);


            DataColumn[] dcolKey = new DataColumn[1];

            dcolMyColumn = new DataColumn();
            dcolMyColumn.DataType = System.Type.GetType("System.String");
            dcolMyColumn.ColumnName = Store_GridLayOutTable.COL_NAME_FLD;
            dtGridDesign.Columns.Add(dcolMyColumn);
            dcolKey[0] = dcolMyColumn; //set this column as the primary key

            dcolMyColumn = new DataColumn();
            dcolMyColumn.DataType = System.Type.GetType("System.String");
            dcolMyColumn.ColumnName = Store_GridLayOutTable.CAPTION_FLD;
            dtGridDesign.Columns.Add(dcolMyColumn);


            dcolMyColumn = new DataColumn();
            dcolMyColumn.DataType = System.Type.GetType("System.String");
            dcolMyColumn.ColumnName = Store_GridLayOutTable.WIDTH_FLD;
            dtGridDesign.Columns.Add(dcolMyColumn);

            dcolMyColumn = new DataColumn();
            dcolMyColumn.DataType = System.Type.GetType("System.Int32");
            dcolMyColumn.ColumnName = Store_GridLayOutTable.COLOR_FLD;
            dtGridDesign.Columns.Add(dcolMyColumn);

            dcolMyColumn = new DataColumn();
            dcolMyColumn.DataType = typeof(bool);
            dcolMyColumn.ColumnName = Store_GridLayOutTable.LOCKED_FLD;
            dtGridDesign.Columns.Add(dcolMyColumn);

            #region Added by duongna to process visibility of columns (21-Sep-2005)
            dcolMyColumn = new DataColumn();
            dcolMyColumn.DataType = System.Type.GetType("System.Boolean");
            dcolMyColumn.ColumnName = "Visible";
            dtGridDesign.Columns.Add(dcolMyColumn);
            #endregion Added by duongna

            DataRow drNewRow;
            for (int i = 0; i < dbGrid.Splits[0].DisplayColumns.Count; i++)
            {
                if (dbGrid.Splits[0].DisplayColumns[i].DataColumn.DataField.Trim() != String.Empty)
                {
                    drNewRow = dtGridDesign.NewRow();
                    drNewRow[Store_GridLayOutTable.COL_NAME_FLD] = dbGrid.Splits[0].DisplayColumns[i].DataColumn.DataField;
                    drNewRow[Store_GridLayOutTable.CAPTION_FLD] = dbGrid.Splits[0].DisplayColumns[i].DataColumn.Caption;
                    drNewRow[Store_GridLayOutTable.LOCKED_FLD] = dbGrid.Splits[0].DisplayColumns[i].Locked;
                    drNewRow[Store_GridLayOutTable.WIDTH_FLD] = dbGrid.Splits[0].DisplayColumns[i].Width;
                    drNewRow[Store_GridLayOutTable.COLOR_FLD] = dbGrid.Splits[0].DisplayColumns[i].HeadingStyle.ForeColor.ToArgb();
                    #region Added by duongna to store visibility (21-Sep-2005)
                    drNewRow[/*Store_GridLayOutTable.COLOR_FLD*/"Visible"] = dbGrid.Splits[0].DisplayColumns[i].Visible;
                    #endregion Added by duongna
                    dtGridDesign.Rows.Add(drNewRow);
                }
            }
            return dtGridDesign;
        }

        /// <summary>
        /// Restore the designed inteface of the grid 
        ///       - Restore Caption
        ///       - Restore Width
        ///       - Restore color
        ///       - Invisible all the other columns 
        ///       - This function must be called only after loading data into this grid, otherwise it doesn't work
        /// </summary>
        /// <param name="dbGrid">TrueDBGrid</param>
        /// <param name="dtGridLayoutTable">Layout Table</param>
        public static void RestoreGridLayout(C1TrueDBGrid dbGrid, DataTable dtGridLayoutTable)
        {
            //first invisible all the columns in the grid
            for (int i = 0; i < dbGrid.Splits[0].DisplayColumns.Count; i++)
            {
                dbGrid.Splits[0].DisplayColumns[i].Visible = false;
                dbGrid.Splits[0].DisplayColumns[i].Locked = true;
                dbGrid.Splits[0].DisplayColumns[i].Button = false;
                dbGrid.Splits[0].DisplayColumns[i].AllowSizing = false;
                dbGrid.Splits[0].DisplayColumns[i].HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;
            }

            //restore the grid layount,
            //its default designed is getting from the table dtGridLayoutTable
            for (int i = 0; i < dtGridLayoutTable.Rows.Count; i++)
            {
                string strColumnName = dtGridLayoutTable.Rows[i][Store_GridLayOutTable.COL_NAME_FLD].ToString();
                try
                {
                    dbGrid.Splits[0].DisplayColumns[strColumnName].DataColumn.Caption = dtGridLayoutTable.Rows[i][Store_GridLayOutTable.CAPTION_FLD].ToString();
                    dbGrid.Splits[0].DisplayColumns[strColumnName].Width = int.Parse(dtGridLayoutTable.Rows[i][Store_GridLayOutTable.WIDTH_FLD].ToString());
                    dbGrid.Splits[0].DisplayColumns[strColumnName].Locked = bool.Parse(dtGridLayoutTable.Rows[i][Store_GridLayOutTable.LOCKED_FLD].ToString());
                    #region Modified by duongna to restore visibility (21-Sep-2005)
                    dbGrid.Splits[0].DisplayColumns[strColumnName].Visible = bool.Parse(dtGridLayoutTable.Rows[i]["Visible"].ToString());
                    if (dbGrid.Splits[0].DisplayColumns[strColumnName].Visible)
                    {
                        dbGrid.Splits[0].DisplayColumns[strColumnName].AllowSizing = true;
                    }
                    #endregion Modified by duongna
                    dbGrid.Splits[0].DisplayColumns[strColumnName].HeadingStyle.ForeColor = Color.FromArgb(int.Parse(dtGridLayoutTable.Rows[i][Store_GridLayOutTable.COLOR_FLD].ToString()));
                }
                catch (ArgumentException)
                {
                    Logger.LogMessage(string.Format("DataField or Caption Not Found: {0}", strColumnName), "RestoreGridLayout", Level.ERROR);
                    throw;
                }
            }
        }


        #region Thachnn Report

        public static StringCollection GetNumberedListForBOMProduct(int[] parriInput,
            int pintRootNumber,
            string pstrDeli)
        {
            const string METHOD_NAME = THIS + ".GetNumberedListForBOMProduct()";
            try
            {
                #region DEFINE Variables
                StringCollection arrRet = new StringCollection();
                int intRecordCount = parriInput.Length;
                StringCollection arrParentString = new StringCollection();
                for (int intCounter = 0; intCounter < intRecordCount + 1; intCounter++)
                {
                    arrParentString.Add("");
                }
                int[] arriLevelHit = new int[intRecordCount + 1];
                #endregion


                int intPrev = pintRootNumber;	// in start phase, iRootNumber is iPrev
                foreach (int i in parriInput)
                {
                    string strOut = "";
                    /// Update level hit count == active running number of last index
                    (arriLevelHit[i])++;	// increase the hit count  of level i
                    arriLevelHit[i + 1] = 0;	// reset hit count of level i+1 to ZERO

                    if (i == pintRootNumber)	// if the level is restart to iRootNumber
                    {
                        // level 0, not exist
                        // Parent string of level iRootNumber, alway = ""
                        // strOut always = "1"
                        arrParentString[i] = "";
                        strOut = "1";
                    }
                    else
                    {
                        strOut = arrParentString[i] + pstrDeli + arriLevelHit[i];
                    }
                    intPrev = i;
                    arrParentString[i + 1] = strOut;
                    arrRet.Add(strOut);
                }
                return arrRet;
            }
            catch (Exception ex)
            {
                //DEBUG:MessageBox.Show(ex.Message);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                throw ex;
            }
        }


        //**************************************************************************              
        ///    <summary>
        ///       Return array of int, contain level columm (named "Level") from DataTable
        ///       Use only by AddNumberedListToDataTable() method
        ///    </summary>
        ///    <Inputs>
        ///       DataTable contain column "Level"
        ///    </Inputs>
        ///    <Outputs>
        ///       Array of int, contain value from "Level" data column
        ///    </Outputs>
        ///    <Returns>
        ///       
        ///    </Returns>
        ///    <Authors>
        ///       ThachNN
        ///    </Authors>
        ///    <History>
        ///       21-Sep-2005
        ///    </History>
        ///    <Notes>
        ///    </Notes>
        //**************************************************************************
        public static int[] ExtractArrayOfLevelFromDataTable(DataTable pdtb)
        {
            const string METHOD_NAME = THIS + ".ExtractArrayOfLevelFromDataTable()";
            try
            {
                int[] arrintRet = new int[pdtb.Rows.Count];
                int intCount = 0;
                foreach (DataRow row in pdtb.Rows)
                {
                    arrintRet[intCount] = int.Parse(row["Level"].ToString());
                    intCount++;
                }
                return arrintRet;
            }
            catch (Exception ex)
            {
                //DEBUG:MessageBox.Show(ex.Message);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                throw ex;
            }
        }


        //**************************************************************************              
        ///    <summary>
        ///       Add a column named "NumberedList" to dataTable		
        ///    </summary>
        ///    <Inputs>
        ///       
        ///    </Inputs>
        ///    <Outputs>
        ///       
        ///    </Outputs>
        ///    <Returns>
        ///       
        ///    </Returns>
        ///    <Authors>
        ///       ThachNN
        ///    </Authors>
        ///    <History>
        ///       21-Sep-2005
        ///    </History>
        ///    <Notes>
        ///    </Notes>
        //**************************************************************************
        public static DataTable AddNumberedListToDataTable(DataTable pdtb)
        {
            const string METHOD_NAME = THIS + ".AddNumberedListToDataTable()";

            try
            {
                DataTable dtbRet = pdtb.Copy();

                DataColumn odcol = new DataColumn("NumberedList");
                odcol.DataType = typeof(string);
                odcol.DefaultValue = "";
                dtbRet.Columns.Add(odcol);

                int[] arriInputLevel = ExtractArrayOfLevelFromDataTable(pdtb);
                StringCollection arrNumberedList = GetNumberedListForBOMProduct(arriInputLevel, 1, ".");

                int intCount = 0;
                foreach (DataRow row in dtbRet.Rows)
                {
                    row["NumberedList"] = arrNumberedList[intCount];
                    intCount++;
                }
                return dtbRet;
            }
            catch (Exception ex)
            {
                //DEBUG:MessageBox.Show(ex.Message);
                // log message.
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                throw ex;
            }
        }


        /// <summary>
        ///  Use only for 2 function CompileCSharpFile()
        /// </summary>
        const string ENTERPRISE_SERVICES = "System.EnterpriseServices.dll";
        string PCSCOMUTILS = typeof(PCSComUtils.DataAccess.Utils).Assembly.Location;
        string PCSUTILS = typeof(FormControlComponents).Assembly.Location;
        string C1PPV = typeof(C1.Win.C1Preview.C1PrintPreviewControl).Assembly.Location;
        string SYSTEM_WINDOWFORMS = typeof(System.Windows.Forms.Form).Assembly.Location;
        string C1CHART = typeof(C1.Win.C1Chart.C1Chart).Assembly.Location;
        string C1REPORT = typeof(C1.C1Report.C1Report).Assembly.Location;
        string C1REPORT_CUSTOMFIELD = typeof(C1.C1Report.CustomFields.Chart).Assembly.Location;
        string SYSTEM_DRAWING = typeof(System.Drawing.Point).Assembly.Location;
        string EXCEL = typeof(Microsoft.Office.Interop.Excel.Application).Assembly.Location;



        /// <summary>
        /// Thachnn 30/Sep/2005
        /// Compile C# source code file to DLL asemblly.
        /// If Compile SUCCESS, this function will return the DLL file fullpath
        /// DLL filename is C# file name (without .cs extension) + .pstrDLL_EXTENSION
        /// throw exception if has error.
        /// THROW: PCSexception
        /// </summary>
        /// <param name="pstrCSharpFilePath">C# file path to  compile. You should provide full path</param>
        /// <param name="pstrDLL_EXTENSION">extension of DLL file name, Ex: .DLL</param>
        /// <param name="pstrSYSTEM_ASSEMBLY">System Assembly name. Ex: System.dll</param>
        /// <param name="pstrSYSTEM_DATA_ASSEMBLY">System.Data Assembly name. Ex: System.Data.dll</param>
        /// <param name="pstrSYSTEM_XML_ASSEMBLY">System.Xml Assembly name. Ex: System.Xml.dll</param>
        /// <param name="pstrCOMMAND_SEPERATOR">seperator sign. Use only in return error message</param>
        /// <returns>Compiled DLL file path (fullpath) of the input C# source file. NULL if error</returns>
        public string CompileCSharpFile(
            string pstrCSharpFilePath,
            string pstrDLL_EXTENSION,
            string pstrSYSTEM_ASSEMBLY,
            string pstrSYSTEM_DATA_ASSEMBLY,
            string pstrSYSTEM_XML_ASSEMBLY,
            string pstrCOMMAND_SEPERATOR
            )
        {
            const string METHOD_NAME = THIS + ".CompileCSharpFile()";

            try
            {
                // include the utc time to avoid share file conflict. Later, delete this file after done			
                string strAssemblyName =
                    System.IO.Path.GetFileNameWithoutExtension(pstrCSharpFilePath) +
                    NowToUTCString() +
                    pstrDLL_EXTENSION;
                string strFolder = System.IO.Path.GetDirectoryName(pstrCSharpFilePath);


                // create c# code provider target to 3.5
                var providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v3.5" } };
                CodeDomProvider codeProvider = new CSharpCodeProvider(providerOptions);
                var objParamters = new CompilerParameters();
                // start by adding any referenced assemblies
                objParamters.ReferencedAssemblies.Add(pstrSYSTEM_ASSEMBLY);
                objParamters.ReferencedAssemblies.Add(pstrSYSTEM_DATA_ASSEMBLY);
                objParamters.ReferencedAssemblies.Add(pstrSYSTEM_XML_ASSEMBLY);
                objParamters.ReferencedAssemblies.Add(typeof(AssemblyLoader).Assembly.Location);
                objParamters.ReferencedAssemblies.Add(ENTERPRISE_SERVICES);
                objParamters.ReferencedAssemblies.Add(PCSCOMUTILS);
                objParamters.ReferencedAssemblies.Add(PCSUTILS);
                objParamters.ReferencedAssemblies.Add(C1PPV);
                objParamters.ReferencedAssemblies.Add(SYSTEM_WINDOWFORMS);
                objParamters.ReferencedAssemblies.Add(C1CHART);
                objParamters.ReferencedAssemblies.Add(C1REPORT);
                objParamters.ReferencedAssemblies.Add(C1REPORT_CUSTOMFIELD);
                objParamters.ReferencedAssemblies.Add(SYSTEM_DRAWING);
                objParamters.ReferencedAssemblies.Add(EXCEL);
                // load the resulting assembly into memory
                objParamters.GenerateInMemory = true;
                // compile the file to the CSharp file's folder
                objParamters.OutputAssembly = strFolder + "\\" + strAssemblyName;

                // now compile the whole thing
                CompilerResults objCompiled = codeProvider.CompileAssemblyFromFile(objParamters, pstrCSharpFilePath);

                // if compile error then display the error message and return
                if (objCompiled.Errors.HasErrors)
                {
                    string strErrorMsg = string.Empty;
                    // create error string
                    strErrorMsg = objCompiled.Errors.Count + " ERRORs";
                    for (int i = 0; i < objCompiled.Errors.Count; i++)
                    {
                        strErrorMsg = strErrorMsg + "\r\nLine:" + objCompiled.Errors[i].Line
                            + pstrCOMMAND_SEPERATOR + objCompiled.Errors[i].ErrorText;
                    }
                    try
                    {
                        Logger.LogMessage(strErrorMsg, METHOD_NAME, Level.DEBUG);
                    }
                    catch { }
                    throw new PCSException(ErrorCode.COMPILE_ERROR, METHOD_NAME, new Exception(strErrorMsg));
                }
                return strFolder + "\\" + strAssemblyName;
            }
            catch (Exception ex)
            {
                throw new PCSException(ErrorCode.COMPILE_ERROR, METHOD_NAME, ex);
            }
        }


        /// <summary>
        /// Thachnn 30/Sep/2005
        /// Compile C# source code file to DLL asemblly.
        /// If Compile SUCCESS, this function will return the DLL file fullpath
        /// DLL filename is C# file name (without .cs extension) + .pstrDLL_EXTENSION
        /// throw exception if has error.
        /// THROW: PCSexception
        /// </summary>
        /// <param name="pstrCSharpFilePath">C# file path to  compile. You should provide full path</param>
        /// <param name="pstrDLL_EXTENSION">extension of DLL file name, Ex: .DLL</param>
        /// <param name="parrUsingAssembly">ArrayList of Assembly name to be using when compiled C# file. Ex: System.dll, PCSComUtils.dll</param>		
        /// <param name="pstrCOMMAND_SEPERATOR">seperator sign. Use only in return error message</param>
        /// <returns>Compiled DLL file path (fullpath) of the input C# source file. NULL if error</returns>
        public string CompileCSharpFile(
            string pstrCSharpFilePath,
            string pstrDLL_EXTENSION,
            ArrayList parrUsingAssembly,
            string pstrCOMMAND_SEPERATOR
            )
        {
            const string METHOD_NAME = THIS + ".CompileCSharpFile()";

            try
            {
                // include the utc time to avoid share file conflict. Later, delete this file after done			
                string strAssemblyName =
                    System.IO.Path.GetFileNameWithoutExtension(pstrCSharpFilePath) +
                    NowToUTCString() +
                    pstrDLL_EXTENSION;
                string strFolder = System.IO.Path.GetDirectoryName(pstrCSharpFilePath);


                // create c# code provider target to 3.5
                var providerOptions = new Dictionary<string, string> {{"CompilerVersion", "v3.5"}};
                CodeDomProvider codeProvider = new CSharpCodeProvider(providerOptions);
                var objParamters = new CompilerParameters();
                // start by adding any referenced assemblies
                foreach (string strAssembly in parrUsingAssembly)
                {
                    objParamters.ReferencedAssemblies.Add(strAssembly);
                }
                objParamters.ReferencedAssemblies.Add(typeof(PCSAssemblyLoader.AssemblyLoader).Assembly.Location);
                objParamters.ReferencedAssemblies.Add(ENTERPRISE_SERVICES);
                objParamters.ReferencedAssemblies.Add(PCSCOMUTILS);
                objParamters.ReferencedAssemblies.Add(PCSUTILS);
                objParamters.ReferencedAssemblies.Add(C1PPV);
                objParamters.ReferencedAssemblies.Add(SYSTEM_WINDOWFORMS);
                objParamters.ReferencedAssemblies.Add(C1CHART);
                objParamters.ReferencedAssemblies.Add(C1REPORT);
                objParamters.ReferencedAssemblies.Add(C1REPORT_CUSTOMFIELD);
                objParamters.ReferencedAssemblies.Add(SYSTEM_DRAWING);
                objParamters.ReferencedAssemblies.Add(EXCEL);

                objParamters.GenerateInMemory = true;	// load the resulting assembly into memory			
                objParamters.OutputAssembly = strFolder + "\\" + strAssemblyName;	// compile the file to the CSharp file's folder
                
                // now compile the whole thing
                CompilerResults objCompiled = codeProvider.CompileAssemblyFromFile(objParamters, pstrCSharpFilePath);

                // if compile error then display the error message and return
                if (objCompiled.Errors.HasErrors)
                {
                    string strErrorMsg = string.Empty;
                    // create error string
                    strErrorMsg = objCompiled.Errors.Count.ToString() + " ERRORs";
                    for (int i = 0; i < objCompiled.Errors.Count; i++)
                    {
                        strErrorMsg = strErrorMsg + "\r\nLine:" + objCompiled.Errors[i].Line.ToString()
                            + pstrCOMMAND_SEPERATOR + objCompiled.Errors[i].ErrorText;
                    }
                    throw new PCSException(ErrorCode.COMPILE_ERROR, METHOD_NAME, new Exception(strErrorMsg));
                }
                return strFolder + "\\" + strAssemblyName;
            }
            catch (Exception ex)
            {
                throw new PCSException(ErrorCode.COMPILE_ERROR, METHOD_NAME, ex);
            }
        }


        /// <summary>
        /// Thachnn: 13/10/2005 copy from DungLA function
        /// This method used to get selected font object to correct format string
        /// </summary>
        /// <param name="pobjFont">Font</param>
        /// <returns>Font in string format</returns>
        public static string GetReportFontString(Font pobjFont)
        {
            try
            {
                StringBuilder strSelectedFont = new StringBuilder();
                strSelectedFont.Append(pobjFont.Name).Append(Constants.REPORT_FONT_SEPARATOR).Append(pobjFont.Size).Append(Constants.REPORT_FONT_SEPARATOR);
                strSelectedFont.Append(pobjFont.Style).Append(Constants.REPORT_FONT_SEPARATOR).Append(pobjFont.GdiCharSet.ToString()).Append(Constants.REPORT_FONT_SEPARATOR);
                strSelectedFont.Append(pobjFont.GdiVerticalFont).Append(Constants.REPORT_FONT_SEPARATOR).Append(pobjFont.Unit);
                return strSelectedFont.ToString();
            }
            catch //(Exception ex)
            {
                //throw ex;
                return string.Empty;
            }
        }


        #endregion

        #region Thachnn File, Time function

        /// <summary>
        /// Thachnn" 20/12/2005
        /// Search in the pstrLocationFolder
        /// Delete all PCS Temp report file (was made by PCS when it generate and execute Report, DynamicReport, Report with Excel File)
        /// This function will analyse ReportDefinition Folder, delete all temp file		
        /// </summary>
        /// <param name="pstrLocationFolder"></param>
        public static void DeletePCSTempReportFile(string pstrLocationFolder)
        {
            try
            {
                string[] arrAllFileNameInFolder = Directory.GetFiles(pstrLocationFolder);

                foreach (string strFile in arrAllFileNameInFolder)
                {
                    if (IsTempFile(strFile))
                    {
                        try
                        {
                            File.SetAttributes(strFile, FileAttributes.Temporary);
                            File.Delete(strFile);
                        }
                        catch
                        {
                            /// if can't delete, my be process reference or Program reference to this file still remain, we will take a look at this file next time
                        }
                    }
                }
            }
            catch
            {
                /// don't make this function progress affect the main flow of PCS
            }
        }

        /// <summary>
        /// Thachnn: 20/12/2005
        /// Determine is input filename is temp file
        /// temp file is something like: Dynamic20051214010203.dll or Excel20051214010203.xls
        /// </summary>
        /// <param name="pstrFile"></param>
        /// <returns></returns>
        private static bool IsTempFile(string pstrFile)
        {
            // this pattern mean:
            // FileName is "xyz some thing", but must have at least one character
            // Token must be 14 digits
            // Dot is . sign
            // Extension is dll or xls
            string strPattern = @"(?<FileNameGR>(\w+))(?<TokenGR>(\d+){14})(?<DotGR>\.)(?<ExtensionGR>(dll)|(xls))";
            return Regex.Match(pstrFile, strPattern, RegexOptions.IgnoreCase).Success;
        }


        /// <summary>
        /// Thachnn : 25/Sep/2005
        /// Create a new tempfile in the Windows-local user temporary folder
        /// </summary>
        /// <returns>string contain filename of the new temporary file</returns>
        public static string GetNewTempFileName()
        {
            return System.IO.Path.GetTempFileName();
        }


        /// <summary>
        /// Thachnn: 25/Sep/2005
        /// Create a new tempfile in the Windows-local user temporary folder
        /// include the UTC time string in the start of temp filename
        /// </summary>
        /// <returns>string contain filename of the new temporary file</returns>
        public static string GetNewTempFileNameIncludeUTC()
        {
            return NowToUTCString() + System.IO.Path.GetTempFileName();
        }

        /// <summary>		
        /// Thachnn: 24/Sep/2005
        /// Get current date time, convert to UTC string. Format = yyyyMMddHHmmss
        /// Ex: 20050916590000
        /// </summary>
        /// <returns>return string in UTC format: yyyyMMddHHmmss</returns>
        public static string NowToUTCString()
        {
            try
            {
                System.DateTime dtmThisMoment = System.DateTime.Now;
                string strRet = dtmThisMoment.ToString(UTC_STRING_FORMAT);
                return strRet;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>		
        /// Thachnn: 24/Sep/2005
        /// Convert input DateTime object to UTC string. Format = yyyyMMddHHmmss
        /// Ex: 20050916590000
        /// </summary>
        /// <param name="pdmtDateTime">DateTime object to convert</param>
        /// <returns>return string in UTC format: yyyyMMddHHmmss</returns>		
        public static string DateTimeToUTCString(DateTime pdmtDateTime)
        {
            try
            {
                string strRet = pdmtDateTime.ToString(UTC_STRING_FORMAT);
                return strRet;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>	
        /// Thachnn: 24/Sep/2005	
        /// Convert input UTC string (Format = yyyyMMddHHmmss) to DateTime object						
        /// </summary>
        /// <param name="strUTC">UTC string in yyyyMMddHHmmss format</param>
        /// <returns>result DateTime object</returns>
        public static System.DateTime UTCStringToDateTime(string strUTC)
        {
            try
            {
                System.DateTime oRet = new System.DateTime(
                    int.Parse(strUTC.Substring(0, 4)),	// year
                    int.Parse(strUTC.Substring(4, 2)),	// month
                    int.Parse(strUTC.Substring(6, 2)),	// day
                    int.Parse(strUTC.Substring(8, 2)),	// hour
                    int.Parse(strUTC.Substring(10, 2)),	// minute
                    int.Parse(strUTC.Substring(12, 2))		// second				
                    );
                return oRet;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }


        #endregion

        #region Thachnn Kill Injection

        /// <summary>
        /// Thachnn: 01/Oct/2005
        /// Example:
        /// Init the mapping:
        /// map.Add("'","''");
        /// map.Add("-","--");
        /// Call function:
        /// inject = KillInjection(map,inject);
        /// pMap can be null or does not have value. In this case, we kill the ' character by default
        /// </summary>
        /// <param name="pnvarrMappingTable">mapping table to replace old value with new value</param>
        /// <param name="strInject">injected string to kill</param>
        /// <returns>free injection string. If error, return string.Empty</returns>
        public static string KillInjection(System.Collections.Specialized.NameValueCollection pnvarrMappingTable, string strInject)
        {
            System.Collections.Specialized.NameValueCollection nvarrMappingTable = pnvarrMappingTable;

            if (nvarrMappingTable == null || nvarrMappingTable.Count <= 0)	// pnvarrMappingTable is not define, then we kill the default ' character
            {
                /// DEFAULT KILL INJECTION
                nvarrMappingTable = new System.Collections.Specialized.NameValueCollection();
                nvarrMappingTable.Add("'", "''");
            }
            if (strInject != null)
            {
                foreach (string strOld in nvarrMappingTable.AllKeys)
                {
                    try
                    {
                        strInject = strInject.Replace(strOld, nvarrMappingTable.Get(strOld));
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
            }
            return strInject;
        }


        /// <summary>
        /// Thachnn: 30/Sep/2005
        /// OVERLOAD INTERFACE
        /// </summary>		
        /// <param name="strInject">injected string to kill</param>
        /// <returns>free injection string. If error, return string.Empty</returns>
        public static string KillInjection(string strInject)
        {
            return KillInjection(null, strInject);
        }


        /// <summary>
        /// Thachnn: 04/Oct/2005
        /// When SQL clause contain one LIKE pair (aa like VALUE)
        /// this function analyze the SQL clause, find the LIKE pair and KillInjection in VALUE
        /// </summary>
        /// <param name="pstrSQL">SQL clause with LIKE pair (aa like Value)</param>
        /// <returns>if SUCCESS: return SQL sentence with clean LIKE Clause, return null if error</returns>
        public static string KillInjectionInLikeClause(string pstrSQL)
        {
            string strRet = null;
            string strLikeClause = null;
            string strLikeValue = null;

            /// ignore case sensitive
            string strRegexp = @"(?<likeGR>(like|LIKE)(\s*)')(?<contentGR>.*)(?<endGR>(\s*)(%(\s*)'))";

            try
            {
                Match oMatch = Regex.Match(pstrSQL, strRegexp, RegexOptions.IgnoreCase);
                if (oMatch.Success)
                {
                    strLikeClause = oMatch.Value;	// get like string from LIKE '   till     %'
                }
                if (strLikeClause != null)
                {
                    strLikeValue = Regex.Replace(strLikeClause, strRegexp, @"${contentGR}", RegexOptions.IgnoreCase);	// extract the Like Value
                }
                if (strLikeValue != null)
                {
                    strLikeValue = KillInjection(null, strLikeValue);	// Kill injection in strLikeValue
                }

                strRet = Regex.Replace(pstrSQL, strRegexp, @"LIKE N'" + strLikeValue + "%'", RegexOptions.IgnoreCase);	// Put the pure LikeValue into the original SQL Like Query
            }
            catch
            {
                strRet = null;
            }
            return strRet;
        }


        #endregion

        #region Thachnn Replace Param with VALUE in string

        /// <summary>
        /// Thachnn: 06 Oct 2005
        /// Find in the SQL clause		
        /// Replace SQL parameter (named pstrPara) with its value (named pstrValue)
        /// if pstr value is string.Empty or null then I remove the whole parameter (remove pattern: a=@a)
        /// if I can't find the pattern a=@a or something like that, I return the original SQL clause
        /// THROW: Exception if error
        /// </summary>
        /// <history>
        /// Thachnn: 20/10/2005: make this function can work with [TableName].[FieldName] syntax style
        /// </history>		
        /// <param name="pstrSQL">input SQL clause</param>
        /// <param name="pstrPara">parameter name (like @something)</param>
        /// <param name="pstrValue">parameter value, can be null or string.Empty</param>
        /// <param name="pblnIsMultiSelection">True: Parameter allows user to select mutil rows</param>
        /// <returns>result SQL clause, if I can't find the pattern a=@a or something like that, I return the original SQL clause</returns>
        public static string FindAndReplaceParameterRegEx(string pstrSQL, string pstrPara, string pstrValue, bool pblnIsMultiSelection)
        {
            string METHOD_NAME = THIS + ".FindAndReplaceParameterRegEx()";
            try
            {
                string strPattern = string.Empty;
                string strReplacePattern = string.Empty;

                pstrSQL += "   ";
                string strRet = StardandizeSQL(pstrSQL);

                // We need to define pattern right upper the Match command to easy to debug.
                strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))"
                    + @"(?<operatorGR>((\s)in(\s))|((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)"
                    + @"(?<valueGR>\(" + pstrPara + @"\)|" + pstrPara + @")(\s+)";
                Match oMatch = Regex.Match(pstrSQL, strPattern, RegexOptions.IgnoreCase);

                if (oMatch.Success)
                {
                    // if value is '' string (single quote, single quote) or null or empty, then we remove parameter from the sql command
                    if ((pstrValue == "''") || (pstrValue == string.Empty) || (pstrValue == null))
                    {
                        /// Kill non-last PARAM
                        //						strPattern = @"(?<fieldGR>(?<open>\[)?\w+(?(open)\])\.)?(?<open2>\[)?\w+(?(open2)\])\s*"
                        //							+ @"(?<operatorGR>like|=|>|<|>=|<=|<>|(?<in>IN))\s*(?(in)\()"
                        //							+ @"(?<valueGR>\s*?" + pstrPara + @"\s*?)(?(in)\))"
                        strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))"
                            + @"(?<operatorGR>((\s)in(\s))|((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)"
                            + @"(?<valueGR>\(" + pstrPara + @"\)|" + pstrPara + @")(\s+)"
                            + @"(?<booloperatorGR>(and)|(or))";
                        strReplacePattern = " ";
                        strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);

                        if (strRet == pstrSQL)	// mean SQL clause is "SELECT * FROM XXX WHERE a = @a   . Kill param pair Not follow by AND,OR (the last PARAM)
                        {
                            //							strPattern = @"(?<fieldGR>(?<open>\[)?\w+(?(open)\])\.)?(?<open2>\[)?\w+(?(open2)\])\s*"
                            //								+ @"(?<operatorGR>like|=|>|<|>=|<=|<>|(?<in>IN))\s*(?(in)\()"
                            //								+ @"(?<valueGR>\s*?" + pstrPara + @"\s*?)(?(in)\))";
                            strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))"
                                + @"(?<operatorGR>((\s)in(\s))|((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)"
                                + @"(?<valueGR>\(" + pstrPara + @"\)|" + pstrPara + @")(\s+)";
                            strReplacePattern = " ";
                            strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
                        }
                    }
                    else // replace parameter by its value
                    {
                        //						strPattern = @"(?<fieldGR>(?<open>\[)?\w+(?(open)\])\.)?(?<open2>\[)?\w+(?(open2)\])\s*"
                        //							+ @"(?<operatorGR>like|=|>|<|>=|<=|<>|(?<in>IN))\s*(?(in)\()"
                        //							+ @"(?<valueGR>\s*?" + pstrPara + @"\s*?)(?(in)\))";
                        strPattern = @"(?<fieldGR>(\s*)((\[|)(\w*)(\]|)(\.|)(\[|)(\w*)(\]|))(\s*))"
                            + @"(?<operatorGR>((\s)in(\s))|((\s)like(\s))|=|>|<|>=|<=|<>)(\s*)"
                            + @"(?<valueGR>\(" + pstrPara + @"\)|" + pstrPara + @")(\s+)";
                        // multi selection will use IN as operator, we need to put ()
                        if (pblnIsMultiSelection)
                            pstrValue = "(" + pstrValue + ")";
                        strReplacePattern = @"${fieldGR}${operatorGR} " + pstrValue + " ";
                        strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
                    }
                }
                strRet = Kill_SQL_BooleanOperator_AtLast(strRet);
                strRet = Kill_SQL_WHERE_AtLast(strRet);
                return strRet;
            }
            catch (Exception ex)
            {
                throw new Exception(METHOD_NAME, ex);
            }
        }


        /// <summary>
        /// Thachnn: 06 Oct 2005
        /// Remove the AND, OR operator at the end of the input SQL clause
        /// </summary>
        /// <param name="pstrSQL">input string</param>
        /// <returns>string with no SQL boolean operator at last</returns>
        public static string Kill_SQL_BooleanOperator_AtLast(string pstrSQL)
        {
            string METHOD_NAME = THIS + ".Kill_SQL_BooleanOperator_AtLast()";
            try
            {
                string strPattern = string.Empty;
                string strReplacePattern = string.Empty;

                pstrSQL += "   ";	//refine the SQL clause, do not allow it end with "\n", fix bug when replace the last parameter
                string strRet = pstrSQL;

                strPattern = @"(\s+)(?<booloperatorGR>(and|or)(\s*)$|(and|or)(?=\s+(group by|order by)))";
                Match oMatch = Regex.Match(pstrSQL, strPattern, RegexOptions.IgnoreCase);

                if (oMatch.Success)
                {
                    strPattern = @"(\s+)(?<booloperatorGR>(and|or)(\s*)$|(and|or)(?=\s+(group by|order by)))";
                    strReplacePattern = " ";
                    strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
                }
                return strRet;
            }
            catch (Exception ex)
            {
                throw new Exception(METHOD_NAME, ex);
            }
        }


        /// <summary>
        /// Thachnn: 06 Oct 2005
        /// Remove the WHERE keyword at the end of the input SQL clause		
        /// </summary>
        /// <param name="pstrSQL">input string</param>
        /// <returns>string with no WHERE keyword at last</returns>
        public static string Kill_SQL_WHERE_AtLast(string pstrSQL)
        {
            string METHOD_NAME = THIS + ".Kill_SQL_WHERE_AtLast()";
            try
            {
                string strPattern = string.Empty;
                string strReplacePattern = string.Empty;

                pstrSQL += "   ";	//refine the SQL clause, do not allow it end with "\n", fix bug when replace the last parameter
                string strRet = pstrSQL;

                strPattern = @"(\s+)(?<booloperatorGR>(where)(\s*)$)";
                Match oMatch = Regex.Match(pstrSQL, strPattern, RegexOptions.IgnoreCase);

                if (oMatch.Success)
                {
                    strPattern = @"(\s+)(?<booloperatorGR>(where)(\s*)$)";
                    strReplacePattern = " ";
                    strRet = Regex.Replace(pstrSQL, strPattern, strReplacePattern, RegexOptions.IgnoreCase);
                }
                return strRet;
            }
            catch (Exception ex)
            {
                throw new Exception(METHOD_NAME, ex);
            }
        }


        /// <summary>
        /// Thachnn: 06 Oct 2005		
        /// replace all \n \t \r in the SQL clause to _space_ character
        /// </summary>
        /// <param name="pstrOld"></param>
        /// <returns>standard string</returns>
        public static string StardandizeSQL(string pstrOld)
        {
            string METHOD_NAME = THIS + ".StardandizeSQL()";
            try
            {
                string strRet = pstrOld;
                strRet = strRet.Replace("\t", " ");
                strRet = strRet.Replace("\n", " ");
                strRet = strRet.Replace("\r", " ");
                strRet = strRet.Replace(@"\t", " ");
                strRet = strRet.Replace(@"\n", " ");
                strRet = strRet.Replace(@"\r", " ");
                return strRet;
            }
            catch (Exception ex)
            {
                throw new Exception(METHOD_NAME, ex);
            }
        }


        #endregion


        #region Thachnn: Find And Replace Pattern with Regular Expression

        /// <summary>
        /// Thachnn: Find and replace all in the provided string
        /// using Regular Expression syntax to find and replace
        /// <author>Thachnn: 12/01/2006</author>		
        /// </summary>		
        /// <example></example>		
        /// <remarks></remarks>				
        /// <value></value>		
        /// <param name="pstrWhereToFind"></param>
        /// <param name="pstrFindWhatPattern"></param>
        /// <param name="pstrReplaceWithPattern"></param>
        /// <param name="pRegExOption"></param>				
        /// <returns></returns>				
        public static string FindAndReplaceAll(string pstrWhereToFind, string pstrFindWhatPattern, string pstrReplaceWithPattern, RegexOptions pRegExOption)
        {
            return Regex.Replace(pstrWhereToFind, pstrFindWhatPattern, pstrReplaceWithPattern, pRegExOption);
        }

        /// <summary>
        /// Thachnn: Find and replace all in the provided string
        /// using Regular Expression syntax to find and replace
        /// Process with no option of Regular Expression Engine
        /// <author>Thachnn: 12/01/2006</author>
        /// </summary>
        /// <param name="pstrWhereToFind"></param>
        /// <param name="pstrFindWhatPattern"></param>
        /// <param name="pstrReplaceWithPattern"></param>
        /// <returns></returns>
        public static string FindAndReplaceAll(string pstrWhereToFind, string pstrFindWhatPattern, string pstrReplaceWithPattern)
        {
            return FindAndReplaceAll(pstrWhereToFind, pstrFindWhatPattern, pstrReplaceWithPattern, RegexOptions.None);
        }


        /// <summary>
        /// Wrapper of FindAndReplaceAll() function - for short function calling
        // Thachnn: Find and replace all in the provided string
        /// using Regular Expression syntax to find and replace
        /// </summary>		
        /// <example></example>		
        /// <remarks></remarks>				
        /// <value></value>		
        /// <author>Thachnn: 12/01/2006</author>
        /// <param name="pstrWhereToFind"></param>
        /// <param name="pstrFindWhatPattern"></param>
        /// <param name="pstrReplaceWithPattern"></param>
        /// <param name="pRegExOption"></param>				
        /// <returns></returns>				
        public static string FARA(string pstrWhereToFind, string pstrFindWhatPattern, string pstrReplaceWithPattern, RegexOptions pRegExOption)
        {
            return FindAndReplaceAll(pstrWhereToFind, pstrFindWhatPattern, pstrReplaceWithPattern, pRegExOption);
        }

        /// <summary>
        /// Wrapper of FindAndReplaceAll() function - for short function calling
        /// Thachnn: Find and replace all in the provided string
        /// using Regular Expression syntax to find and replace
        /// Process with no option of Regular Expression Engine
        /// <author>Thachnn: 12/01/2006</author>		
        /// </summary>
        /// <param name="pstrWhereToFind"></param>
        /// <param name="pstrFindWhatPattern"></param>
        /// <param name="pstrReplaceWithPattern"></param>
        /// <returns></returns> 
        public static string FARA(string pstrWhereToFind, string pstrFindWhatPattern, string pstrReplaceWithPattern)
        {
            return FindAndReplaceAll(pstrWhereToFind, pstrFindWhatPattern, pstrReplaceWithPattern, RegexOptions.None);
        }

        #endregion Thachnn: Find And Replace Pattern	with Regular Expression

        #region Thachnn: Report Print Configuration Helper
        const string MENU_PRINT_REPORT_CONFIG = "Report Printing Configuration ...";

        /// <summary>
        /// Thachnn 22/11/2005
        /// bool ExecuteReportLayout(string pstrLayoutFileName, string pstrReportName): 
        /// run effectively with the easy-render REPORT
        /// Load the report layout with named: pstrLayoutFileName
        /// get the select sql command from the layout file
        /// build the datatable with the sql from the layout file,		
        /// build report.
        /// SHow report on the PCS.C1PrintPreviewDialog		
        /// EXCEPTION if it can't render report with the SQL Command (store in the layout file)
        /// this exception is catch in the ActionWithoutFunctionNameHandler(). So do not put try catch (of whole function body) here		
        /// </summary>
        /// <remarks>this function is Call only by this.ActionWithoutFunctionNameHandler()</remarks>
        /// <exception cref="">Throw EXCEPTION if it can't render report with the SQL Command (store in the layout file)
        /// this exception is catch in the ActionWithoutFunctionNameHandler(). So do not put try catch (of whole function body) here</exception>		
        public static bool ExecuteReportLayout(string pstrLayoutFileName, string pstrReportName, short pnCopies)
        {
            bool blnRet = false;
            string strPCSConnectionString = PCSComUtils.DataAccess.Utils.Instance.OleDbConnectionString;

            /// 1. LOAD REPORT FILE FROM pstrLayoutFileName, pstrReportName				
            string mstrReportDefFolder = Application.StartupPath + "\\" + Constants.REPORT_DEFINITION_STORE_LOCATION;

            #region INIT REPORT BUIDER OBJECT to load layout file
            PCSUtils.Utils.ReportBuilder objRB = new PCSUtils.Utils.ReportBuilder();
            try
            {
                objRB.ReportDefinitionFolder = mstrReportDefFolder;
                objRB.ReportLayoutFile = pstrLayoutFileName;
                if (objRB.AnalyseLayoutFile() == false)
                {
                    MessageBox.Show("ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND, MessageBoxIcon.Error");
                    return false;
                }
                //objRB.UseLayoutFile = objRB.AnalyseLayoutFile();	// use layout file if any , auto drawing if not found layout file
                objRB.UseLayoutFile = true;	// always use layout file
            }
            catch
            {
                objRB.UseLayoutFile = false;
                MessageBox.Show("ErrorCode.MESSAGE_REPORT_TEMPLATE_FILE_NOT_FOUND,MessageBoxIcon.Error");
                return false;
            }
            #endregion

            /// 2. CHECK EXIST THE REPORTNAME
            /// If not, use the first report in the layout file
            /// But in this moment, we use the first report in the layout file only
            /// The report builder do that action automatically


            #region BUILD THE DATA TABLE
            DataTable dtbForReport = new DataTable();
            /// 2. GET THE SQL SELECT COMMAND from the Report Loaded
            string strSql = objRB.Report.DataSource.RecordSource;

            /// EXECUTE THE REPORT SQL SELECT COMMAND
            DataSet dstPCS = new DataSet();
            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            //Utils utils = new Utils();
            oconPCS = new OleDbConnection(strPCSConnectionString);
            ocmdPCS = new OleDbCommand(strSql, oconPCS);
            ocmdPCS.Connection.Open();

            OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
            odadPCS.Fill(dstPCS, Sys_PrintConfigurationTable.TABLE_NAME);
            if (dstPCS.Tables.Count != 0)
            {
                dtbForReport = dstPCS.Tables[Sys_PrintConfigurationTable.TABLE_NAME];
            }
            #endregion


            /// RENDER REPORT FROM THE REPORT BUILDER				
            try
            {
                objRB.ReportName = pstrReportName;
                objRB.SourceDataTable = dtbForReport;
            }
            catch//(Exception ex)
            {
                /// we can't preview while we don't have any data
                //MessageBox.Show(ex.Message);
                return false;
            }

            objRB.MakeDataTableForRender();
            //grid.DataSource = objRB.RenderDataTable;

            // and show it in preview dialog				
            PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog printPreview = new PCSUtils.Framework.ReportFrame.C1PrintPreviewDialog();

            objRB.ReportViewer = printPreview.ReportViewer;
            objRB.RenderReport();
            printPreview.Show();

            return blnRet;
        }

        /// <summary>
        /// /// Thachnn 22/11/2005		
        /// Build a new ContextMenu for a form
        /// ContextMenu Name = cmnuReportList (FIXED)
        /// Build base on the sys_PrintCOnfiguration database table
        /// User can use this menu to select which report to print on the print preview		
        /// if database entry in the sys_PrintConfiguration do not define the FunctionName, we call the ExecuteReportLayout() function for that MenuItem
        /// if database entry in the sys_PrintConfiguration do not define the FunctionName, we assign function (with name = FunctionName) in the target form to the MenuItem
        /// EXCEPTION: any exception of this function  will be throw to the caller function
        /// SO WE DON'T NEED TO TRY CATCH THE ABSTRACT EXCEPTION HERE
        /// </summary>
        /// <param name="pfrmToAdd">Target form to add the context Menu to the btnPrint and btnPrintConfiguration button</param>
        /// <returns>just-building ContextMenu</returns>
        public static ContextMenu BuildReportMenu(Form pfrmToAdd)
        {
            //const string MENU_CONTEXT_NAME = "cmnuReportList";			
            string strFormName = pfrmToAdd.Name;

            /// 1. FETCH THE DATABASE TO GET ENTRIES (sys_PrintConfiguration,)
            DataTable dtbPrintEntries = (new PCSComUtils.Framework.ReportFrame.BO.PrintConfigurationBO()).GetPrintConfigurationByFormName(strFormName);

            ArrayList arrMenuItems = new ArrayList();
            /// 2. BUILD THE MENU ITEMS (base on the got entries, add )
            int i = 0;
            foreach (DataRow drow in dtbPrintEntries.Rows)
            {
                string strDescription = drow[Sys_PrintConfigurationTable.DESCRIPTION_FLD].ToString();
                //string strCopies = ((int)drow["Copies"]).ToString();
                int nCopies = (int)drow[Sys_PrintConfigurationTable.COPIES_FLD];
                string strCopies = string.Empty;
                if (nCopies > 1)
                {
                    strCopies = "(x" + nCopies + ")";
                }
                string strFunctionName = drow[Sys_PrintConfigurationTable.FUNCTIONNAME_FLD].ToString();
                bool blnDefault = (bool)drow[Sys_PrintConfigurationTable.PRINTABLE_FLD];

                //string strMenuText = string.Format("{2,3}{1}{0}" ,strDescription,  " x " , nCopies);
                string strMenuText = string.Format("{0}   {1}", strDescription, strCopies);
                //string strMenuText = string.Format("{0}" ,strDescription);
                ReportMenuItem objMenuEntry = new ReportMenuItem();
                objMenuEntry.Text = strMenuText;
                objMenuEntry.Checked = blnDefault;
                objMenuEntry.Index = i++;
                objMenuEntry.ContainerForm = pfrmToAdd;

                Sys_PrintConfigurationVO objVO = new Sys_PrintConfigurationVO();
                objVO.PrintConfigurationID = int.Parse(drow[Sys_PrintConfigurationTable.PRINTCONFIGURATIONID_FLD].ToString().Trim());
                objVO.FormName = drow[Sys_PrintConfigurationTable.FORMNAME_FLD].ToString().Trim();
                objVO.FileName = drow[Sys_PrintConfigurationTable.FILENAME_FLD].ToString().Trim();
                objVO.Copies = int.Parse(drow[Sys_PrintConfigurationTable.COPIES_FLD].ToString().Trim());
                objVO.Description = drow[Sys_PrintConfigurationTable.DESCRIPTION_FLD].ToString().Trim();
                objVO.Printable = bool.Parse(drow[Sys_PrintConfigurationTable.PRINTABLE_FLD].ToString().Trim());
                objVO.FunctionName = drow[Sys_PrintConfigurationTable.FUNCTIONNAME_FLD].ToString().Trim();
                objVO.ReportName = drow[Sys_PrintConfigurationTable.REPORTNAME_FLD].ToString().Trim();
                objMenuEntry.PrintConfigurationVO = objVO;

                /// Get the Delegate from name
                /// if(strFunctionName.Trim() == string.Empty)
                /// {g?i ExecuteReportLayout()  neu khong co function name trong database}
                try
                {
                    objMenuEntry.Click += (EventHandler)System.Delegate.CreateDelegate(typeof(EventHandler), pfrmToAdd, strFunctionName, true);
                }
                catch//(ArgumentException ex)	//We use the default ExecuteReportLayout(LayoutFileName,ReportFileName) here
                {
                    objMenuEntry.Click += new EventHandler(ActionWithoutFunctionNameHandler);
                }
                arrMenuItems.Add(objMenuEntry);
            }

            /// 3. NEW CONTEXT MENU OBJECT 
            ContextMenu cmnuReportList = new ContextMenu();

            // (if arrayItem has any), add to the cmnuReportList
            if (arrMenuItems.Count > 0)
            {
                foreach (ReportMenuItem objMenuEntry in arrMenuItems)
                {
                    cmnuReportList.MenuItems.Add(objMenuEntry);
                }
            }

            /// 4. ADD the ReportConfig Menu Item
            ReportMenuItem mnuSeperator = new ReportMenuItem();
            mnuSeperator.Text = "-";
            mnuSeperator.ContainerForm = pfrmToAdd;
            cmnuReportList.MenuItems.Add(mnuSeperator);
            ReportMenuItem mnuReportConfig = new ReportMenuItem();
            mnuReportConfig.Text = MENU_PRINT_REPORT_CONFIG;
            mnuReportConfig.Click += new EventHandler(OpenPrintConfigurationFormHandler);
            mnuReportConfig.Index = int.MaxValue;	// always is the end item in the context menu list			
            mnuReportConfig.ContainerForm = pfrmToAdd;
            cmnuReportList.MenuItems.Add(mnuReportConfig);

            /// REVIEW: this line, we add the context menu to the Form.ContextMenu
            //pfrmToAdd.ContextMenu = cmnuReportList;
            return cmnuReportList;
        }

        /// <summary>
        /// Thachnn: /// Thachnn 22/11/2005
        /// Use this Delegate when there is no FunctionName associate with this PrintConfiguration Item.
        /// This Delegate will call function to Display the Report with SQL store in the layout file. (easy-render report case)
        /// Can assign this delegate to the BUTTON CLICK or MENU CLICK
        /// Exception: Catch all. NOT THROW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>this function Calls this.ExecuteReportLayout()</remarks>
        public static void ActionWithoutFunctionNameHandler(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".ActionWithoutFunctionNameHandler()";
            ReportMenuItem mnuItem = new ReportMenuItem();
            try
            {
                mnuItem = (ReportMenuItem)sender;
            }
            catch
            {
            }

            if (mnuItem != null && mnuItem.PrintConfigurationVO != null)
            {
                try
                {
                    ExecuteReportLayout(mnuItem.PrintConfigurationVO.FileName, mnuItem.PrintConfigurationVO.ReportName, (short)mnuItem.PrintConfigurationVO.Copies);
                }
                catch (Exception ex)
                {
                    PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                    try
                    {
                        Logger.LogMessage(ex, METHOD_NAME + "@" + mnuItem.Text + "@" + mnuItem.PrintConfigurationVO.ReportName, Level.ERROR);
                    }
                    catch
                    {
                        PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Thachnn 22/11/2005
        /// This Delegate Function will display the Print Configuration Form
        /// ALlow User to re-config the Print behavious
        /// Can assign this delegate to the BUTTON CLICK or MENU CLICK		
        /// EXCEPTION: not throw
        /// </summary>
        /// <exception cref="">EXCEPTION: Do not throw</exception>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OpenPrintConfigurationFormHandler(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".OpenPrintConfigurationFormHandler()";
            try
            {
                Form frmInAction = ((ReportMenuItem)sender).ContainerForm;
                (new PrintConfiguration(frmInAction.Name)).Show();
            }
            catch (Exception ex)
            {
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME + "@" + ((ReportMenuItem)sender).ContainerForm.Name, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Delegate: this event handler is reponsible to show the cmnuReportList Context Menu
        /// We will show the context menu next to the btnPrintConfiguration button
        /// Can assign this delegate to the BUTTON CLICK or MENU CLICK
        /// EXCEPTION: not throw
        /// </summary>
        /// <exception cref="">EXCEPTION: Do not throw</exception>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>this function Calls this.BuildReportMenu(Form pfrmToAdd)()</remarks>
        public static void ShowMenuReportListHandler(object sender, System.EventArgs e)
        {
            const string METHOD_NAME = THIS + ".ShowMenuReportListHandler()";
            try
            {
                Form frmInAction = ((Control)sender).FindForm();
                BuildReportMenu(frmInAction).Show((Control)sender, new Point(((Control)sender).Width, 0));
            }
            catch (Exception ex)
            {
                PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                try
                {
                    Form frmInAction = ((Control)sender).FindForm();
                    Logger.LogMessage(ex, METHOD_NAME + "@" + frmInAction.Text, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Delegate: this event handler is reponsible to RUN the Default setting Print COnfig of this form
        /// use this delegate to handle the btnPrint_Click Event
        /// Can assign this delegate to the BUTTON CLICK or MENU CLICK
        /// EXCEPTION: NOT throw any Exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>this function Calls this.BuildReportMenu(Form pfrmToAdd)()</remarks>
        public static void RunDefaultReportEntriesHandler(object sender, System.EventArgs e)
        {
            try
            {
                /// simulate the action: Open Report List, run all the checked (default) entries
                /// this is btnPrint_Click Action
                Form frmInAction = ((Control)sender).FindForm();
                ContextMenu cmnuReportList = BuildReportMenu(frmInAction);
                foreach (ReportMenuItem mnuItem in cmnuReportList.MenuItems)
                {
                    // is CHECKED and not seperator, not Report config
                    if (mnuItem.Checked && mnuItem.Text != MENU_PRINT_REPORT_CONFIG && mnuItem.Text != "-")
                    {
                        /// Simulate the click event
                        try
                        {
                            mnuItem.PerformClick();
                        }
                        catch { }
                    }
                }
            }
            catch
            {
                /// we don't want wrong or incorrect configs in the database show error to user.
                /// Show we don't show anything here.
            }
        }

        #endregion


        /// <summary>
        /// Clear all data on form
        /// if Control is TextBox then Text = string.Empty, Tag = null
        /// if Control is C1DateEdit or C1NumericEdit then Value = DBNull.Value
        /// if Control is C1Combo or ComboBox then SelectedIndex = -1
        /// if Control is CheckBox then Checked = false
        /// </summary>
        /// <param name="pfrmForm"></param>
        /// <author>SonHT 2005-10-16</author>
        public static void ClearForm(Control pobjControl)
        {
            // HACKED: DungLA, updat entire function, using Control instead of Form
            Control objControl = pobjControl;

            #region // Scan control on form to clean data

            while (objControl != null)
            {
                // Get next control
                objControl = pobjControl.GetNextControl(objControl, true);
                if (objControl == null)
                {
                    break;
                }

                // if it's a ListBox, clear all item in it
                if (objControl.GetType().Equals(typeof(ListBox)))
                {
                    // clear datasource first if any
                    ((ListBox)objControl).DataSource = null;
                    // clear all items
                    ((ListBox)objControl).Items.Clear();
                }

                // If it's TextBox or C1TextBox
                if ((objControl.GetType().Equals(typeof(TextBox)))
                    || (objControl.GetType().Equals(typeof(C1TextBox))))
                {
                    TextBox txtBox = (TextBox)objControl;
                    txtBox.Text = string.Empty;
                    // HACK: dungla 10-19-2005
                    txtBox.Tag = null;
                    // END: dungla 10-19-2005
                }
                else if (objControl.GetType().Equals(typeof(C1DateEdit)))
                {
                    C1DateEdit dtmC1Date = (C1DateEdit)objControl;
                    dtmC1Date.Value = DBNull.Value;
                }
                // if it is C1NumericEdit
                else if (objControl.GetType().Equals(typeof(C1NumericEdit)))
                {
                    // TODO: SonHT Set property for C1Num
                    C1NumericEdit dtmC1Num = (C1NumericEdit)objControl;
                    dtmC1Num.Value = DBNull.Value;

                }
                // if it is C1Combo
                else if (objControl.GetType().Equals(typeof(C1Combo)))
                {
                    C1Combo cboC1 = (C1Combo)objControl;
                    cboC1.SelectedIndex = -1;

                    #region // HACK: DEL SonHT 2005-10-19
                    //					if(cboC1.Name == CBOCCN)
                    //					{
                    //						if(cboC1.DataSource)
                    //						cboC1.SelectedValue = SystemProperty.CCNID;
                    //					}
                    #endregion // END: DEL SonHT 2005-10-19

                }
                // if it is ComboBox
                else if (objControl.GetType().Equals(typeof(ComboBox)))
                {
                    ComboBox cboBox = (ComboBox)objControl;
                    cboBox.SelectedIndex = -1;
                }
                else if (objControl.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox chkBox = (CheckBox)objControl;
                    chkBox.Checked = false;
                }
                // if is C1TrueDBGrid control
                //				else if(objControl.GetType().Equals(typeof(C1TrueDBGrid)))
                //				{
                //					C1TrueDBGrid grid = (C1TrueDBGrid) objControl;
                //				}
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C1Combo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            const string METHOD_NAME = THIS + ".C1Combo_KeyDown()";
            try
            {
                Control objControl = (Control)sender;
                if (e.KeyCode == Keys.Escape)
                {
                    if (objControl.GetType().Equals(typeof(C1Combo)))
                    {
                        Form objForm = objControl.FindForm();
                        if (objForm != null)
                        {
                            objForm.Close();
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
        /// Validate in textbox which has the button search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>TuanDM 10 - 17 - 2005</author>
        public static void ControlValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".ControlValidating()";
            try
            {
                if (((Control)sender).GetType().Equals(typeof(TextBox)))
                {
                    if (((TextBox)sender).Text != string.Empty && ((TextBox)sender).Modified)
                    {
                        e.Cancel = true;
                    }
                }
                else if (((Control)sender).GetType().Equals(typeof(C1TextBox)))
                {
                    if (((C1TextBox)sender).Value.ToString() != string.Empty && ((C1TextBox)sender).Modified)
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

        public static int ConvertIncheToTwips(decimal pdecNumber)
        {
            try
            {
                return decimal.ToInt32(decimal.Floor(pdecNumber * Constants.INCHE_TWIPS_RATE));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ConvertTwipsToInches(int pintNumber)
        {
            try
            {
                decimal decResult = decimal.Round((decimal)pintNumber / (decimal)Constants.INCHE_TWIPS_RATE, 2);
                return decResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Init Month, Year Combobox

        /// <summary>
        /// Init year combobox
        /// </summary>
        /// <param name="pcboYear"></param>
        /// <param name="pintFromYear"></param>
        /// <param name="pintToYear"></param>
        /// <author> Tuan TQ, 16 Nov, 2005</author>
        public static void InitYearComboBox(System.Windows.Forms.ComboBox pcboYear, int pintFromYear, int pintToYear)
        {
            //Clear combox box items
            pcboYear.Items.Clear();

            //Init combox item
            for (int i = pintFromYear; i < pintToYear; i++)
            {
                pcboYear.Items.Add(i.ToString());
            }
        }

        /// <summary>
        /// Init year combobox
        /// </summary>
        /// <param name="pcboYear"></param>
        /// <param name="pintFromYear"></param>
        /// <param name="pintToYear"></param>
        /// <param name="pblnInsertBlankItem">True: Insert blank item at position 0</param>
        /// <author> Tuan TQ, 16 Nov, 2005</author>
        public static void InitYearComboBox(System.Windows.Forms.ComboBox pcboYear, int pintFromYear, int pintToYear, bool pblnInsertBlankItem)
        {
            InitYearComboBox(pcboYear, pintFromYear, pintToYear);
            if (pblnInsertBlankItem)
            {
                pcboYear.Items.Insert(0, string.Empty);
            }
        }

        /// <summary>
        /// Init month combobox
        /// </summary>
        /// <param name="pcboMonth"></param>
        /// <author> Tuan TQ, 16 Nov, 2005</author>
        public static void InitMonthComboBox(System.Windows.Forms.ComboBox pcboMonth)
        {
            const string PAD_CHAR = "0";

            //Clear combox box items
            pcboMonth.Items.Clear();

            //Init combox item
            for (int i = 1; i < 13; i++)
            {
                pcboMonth.Items.Add((i < 10) ? PAD_CHAR + i.ToString() : i.ToString());
            }
        }

        /// <summary>
        /// Init day of month combobox
        /// </summary>
        /// <param name="pcboDayOfMonth"></param>
        /// <author> SonHT, 16 Nov, 2005</author>
        public static void InitDayOfMonthComboBox(System.Windows.Forms.ComboBox pcboDayOfMonth)
        {
            const string PAD_CHAR = "0";
            const int MAX_DAY = 31;

            //Clear combox box items
            pcboDayOfMonth.Items.Clear();

            //Init combox item
            for (int i = 1; i <= MAX_DAY; i++)
            {
                pcboDayOfMonth.Items.Add(i);
                //pcboDayOfMonth.Items.Add((i<10)? PAD_CHAR + i.ToString():i.ToString());
            }
        }

        /// <summary>
        /// Init month combobox
        /// </summary>
        /// <param name="pcboMonth"></param>
        /// <param name="pblnInsertBlankItem">True: Insert blank item at position 0</param>
        /// <author> Tuan TQ, 16 Nov, 2005</author>
        public static void InitMonthComboBox(System.Windows.Forms.ComboBox pcboMonth, bool pblnInsertBlankItem)
        {
            InitMonthComboBox(pcboMonth);

            if (pblnInsertBlankItem)
            {
                pcboMonth.Items.Insert(0, string.Empty);
            }
        }

        #endregion Init Month, Year Combobox


        public static FormInfo GetFormInfo(Form pForm, out string strNo)
        {
            UtilsBO boUtils = new UtilsBO();
            strNo = string.Empty;
            string strTableName = string.Empty, strTransNoFieldName = string.Empty;
            string strPrefix = string.Empty, strFormat = string.Empty;
            boUtils.GetMenuInfo(pForm.GetType().ToString(), out strTableName, out strTransNoFieldName, out strPrefix, out strFormat);
            FormInfo formInfor = new FormInfo(pForm, strPrefix, strFormat, strTableName, strTransNoFieldName, SystemProperty.UserName);
            if (formInfor != null)
                strNo = (new UtilsBO()).GetNoByMask(formInfor.mTableName, formInfor.mTransNoFieldName, strPrefix, strFormat);
            return formInfor;
        }
        public static string GetNoByMask(Form pForm)
        {
            FormInfo formInfor = null;
            string strFormat = string.Empty;
            string strPrefix = string.Empty;
            foreach (object t in SystemProperty.ArrayForms)
            {
                formInfor = (FormInfo)t;
                if (pForm == formInfor.mForm)
                {
                    strFormat = formInfor.mTransFormat;
                    strPrefix = formInfor.mPrefix;
                    break;
                }
            }

            return formInfor != null ? GetNoByMask(formInfor.mTableName, formInfor.mTransNoFieldName, strPrefix, strFormat) : string.Empty;
        }
        public static string GetNoByMask(string tableName, string transNoField, string prefix, string format)
        {
            return (new UtilsBO()).GetNoByMask(tableName, transNoField, prefix, format);
        }
        public static string GetNoByMask(string tableName, string transNoField, string prefix, string format, DateTime date)
        {
            int revision;
            return (new UtilsBO()).GetNoByMask(tableName, transNoField, prefix, format, date, out revision);
        }
        public static string GetNoByMask(string pstrUsername, string strTableName, string strFieldName, string strPrefix, string strFormat)
        {
            return (new UtilsBO()).GetNoByMask(pstrUsername, strTableName, strFieldName, strPrefix, strFormat);
        }
        public static string GetNoByMask(Form pForm, string pstrUsername)
        {
            FormInfo formInfor = null;
            foreach (var t in SystemProperty.ArrayForms)
            {
                formInfor = t as FormInfo;
                if (formInfor != null && pForm == formInfor.mForm)
                {
                    break;
                }
            }

            return GetNoByMask(formInfor, pstrUsername);
        }

        public static string GetNoByMask(FormInfo formInfo, string userName)
        {
            return formInfo != null ? (new UtilsBO()).GetNoByMask(userName, formInfo.mTableName, formInfo.mTransNoFieldName, formInfo.mPrefix, formInfo.mTransFormat) : string.Empty;
        }

        /// <summary>
        ///     Get transaction number by menu entry
        /// </summary>
        /// <param name="menuEntry"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GetNoByMask(Sys_Menu_Entry menuEntry, string userName)
        {
            return menuEntry != null ? (new UtilsBO()).GetNoByMask(userName, menuEntry.TableName, menuEntry.TransNoFieldName, menuEntry.Prefix, menuEntry.TransFormat) : string.Empty;
        }

        /// <summary>
        /// Gets the form info.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static FormInfo GetFormInfo(Form form)
        {
            return (from object t in SystemProperty.ArrayForms select t as FormInfo).FirstOrDefault(formInfor => formInfor != null && form == formInfor.mForm);
        }

        /// <summary>
        /// Delete multiply Rows on the truedbgird
        /// </summary>
        /// <param name="pdgrdData"></param>
        public static void DeleteMultiRowsOnTrueDBGrid(C1TrueDBGrid pdgrdData)
        {
            //store the index of selectrows
            int intSelectRows = pdgrdData.SelectedRows.Count;
            var intIndexOfSelectedRows = new ArrayList();
            for (int i = 0; i < intSelectRows; i++)
            {
                intIndexOfSelectedRows.Add(int.Parse(pdgrdData.SelectedRows[i].ToString()));
            }
            intIndexOfSelectedRows.Sort();

            //delete Rows
            for (int i = intSelectRows - 1; i >= 0; i--)
            {
                pdgrdData.Row = (int)intIndexOfSelectedRows[i];
                pdgrdData.Delete();
            }
        }

        /// <summary>
        /// Synchrony grid data 
        /// </summary>
        /// <param name="pdgrdData"></param>
        /// <author>SonHT</author>
        public static void SynchronyGridData(C1TrueDBGrid pdgrdData)
        {
            if (pdgrdData.DataSource == null) return;
            DataSet dstData = null;
            DataTable dtbData = null;

            #region Get data table from Grid

            if (pdgrdData.DataSource.GetType().Equals(typeof(DataSet)))
            {
                dstData = (DataSet)pdgrdData.DataSource;
            }
            else if (pdgrdData.DataSource.GetType().Equals(typeof(DataTable)))
            {
                dtbData = (DataTable)pdgrdData.DataSource;
            }
            if (dtbData == null) dtbData = dstData.Tables[0];

            #endregion

            foreach (C1.Win.C1TrueDBGrid.C1DataColumn colData in pdgrdData.Columns)
            {
                if (colData.NumberFormat == Constants.DECIMAL_NUMBERFORMAT)
                {
                    #region DECIMAL_NUMBERFORMAT

                    if (dtbData.Columns[colData.DataField].DataType.Equals(typeof(decimal)))
                    {
                        // Update data
                        foreach (DataRow drow in dtbData.Rows)
                        {
                            if (drow.RowState != DataRowState.Deleted)
                            {
                                if (drow[colData.DataField] != DBNull.Value)
                                {
                                    drow[colData.DataField] = decimal.Round(Convert.ToDecimal(drow[colData.DataField]), DECIMAL_ROUND);
                                }
                            }
                        }
                    }

                    #endregion
                }
                else if (colData.NumberFormat == Constants.DATETIME_FORMAT)
                {
                    #region DATETIME_FORMAT
                    if (dtbData.Columns[colData.DataField].DataType.Equals(typeof(DateTime)))
                    {
                        if (dtbData.Columns[colData.DataField].DataType.Equals(typeof(DateTime)))
                        {
                            // Update data
                            foreach (DataRow drow in dtbData.Rows)
                            {
                                if (drow.RowState != DataRowState.Deleted)
                                {
                                    if (drow[colData.DataField] != DBNull.Value)
                                    {
                                        DateTime dtmDate = Convert.ToDateTime(drow[colData.DataField]);
                                        drow[colData.DataField] = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day);
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (colData.NumberFormat == Constants.DATETIME_FORMAT_HOUR)
                {
                    #region DATETIME_FORMAT_HOUR

                    if (dtbData.Columns[colData.DataField].DataType.Equals(typeof(DateTime)))
                    {
                        // Update data
                        foreach (DataRow drow in dtbData.Rows)
                        {
                            if (drow.RowState != DataRowState.Deleted)
                            {
                                if (drow[colData.DataField] != DBNull.Value)
                                {
                                    DateTime dtmDate = Convert.ToDateTime(drow[colData.DataField]);
                                    drow[colData.DataField] = new DateTime(dtmDate.Year, dtmDate.Month, dtmDate.Day, dtmDate.Hour, dtmDate.Minute, 0);
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
        }
        public static decimal GetUMRate(int pintInID, int pintOutID)
        {
            UtilsBO boUtils = new UtilsBO();
            return boUtils.GetUMRate(pintInID, pintOutID);
        }
    }

}

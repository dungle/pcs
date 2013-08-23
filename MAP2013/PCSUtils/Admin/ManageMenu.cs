using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using PCSComUtils.Admin.BO;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.Admin
{
    /// <summary>
    /// This class uses to manage all system menu.
    /// </summary>
    public class ManageMenu : Form
    {
        #region Controls

        private Button btnClose;
        private Button btnCopy;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnHelp;
        private Button btnSave;
        private ContextMenu cMenu;
        private C1FlexGrid dgrdMenu;
        private GroupBox grbDetails;
        private GroupBox grbFunctions;
        private Label lblCaptionDefault;
        private Label lblCaptionJP;
        private Label lblCaptionUS;
        private Label lblCaptionVN;
        private Label lblCopyFrom;
        private Label lblFormat;
        private Label lblMenuName;
        private Label lblPrefix;
        private Label lblShortcut;
        private MenuItem miCopy;
        private MenuItem miDelete;
        private MenuItem miEdit;
        private TextBox txtCaptionEN;
        private TextBox txtCaptionJP;
        private TextBox txtCaptionVN;
        private TextBox txtFormat;
        private TextBox txtMenuName;
        private TextBox txtPrefix;
        private TextBox txtShortcut;

        #endregion

        private const string THIS = "PCSUtils.Admin.ManageMenu";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        #region Members

        private const string FORMAT_COL = "colFormat";
        private const int INT_POS_TREE = 1;
        private const string IS_TRANSACTION_COL = "colIsTransaction";
        private const string IS_USER_CREATED_COL = "colIsUserCreated";
        private const string MAIN = "MAIN";
        private const int MAX_LENGTH_CAPTION = 1000;
        private const int MAX_LENGTH_DEFAULT = 1000;
        private const string MENU_ENTRY_COL = "colMenuID";
        private const string OPEN_QUOTE = " (";
        private const string PREFIX_COL = "colPrefix";
        private List<Sys_Menu_Entry> list = new List<Sys_Menu_Entry>();
        private bool blnIsChanged;
        private int intCurrentRow;
        private EnumAction mFormMode = EnumAction.Default;

        private Sys_Menu_Entry _sys_Menu_Entry;

        public EnumAction FormMode
        {
            get { return mFormMode; }
            set { mFormMode = value; }
        }
        public Sys_Menu_Entry Sys_Menu_Entry
        {
            get { return _sys_Menu_Entry; }
            set { _sys_Menu_Entry = value; }
        }

        #endregion

        public ManageMenu()
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
        /// Load data from a sys_menu_entry to form
        /// </summary>
        /// <param name="pvoMenuEntry">Sys_Menu_EntryVO object</param>
        /// private void LoadDataToForm(Sys_Menu_EntryVO pvoMenuEntry)
        private void LoadDataToForm(Sys_Menu_Entry objMenuEntry)
        {
            if (objMenuEntry != null)
            {
                txtMenuName.Text = objMenuEntry.Text_CaptionDefault;
                txtCaptionEN.Text = objMenuEntry.Text_Caption_EN_US;
                txtCaptionVN.Text = objMenuEntry.Text_Caption_VI_VN;
                txtCaptionJP.Text = objMenuEntry.Text_Caption_JA_JP;
                txtShortcut.Text = objMenuEntry.Shortcut;
                txtPrefix.Text = objMenuEntry.Prefix;
                txtFormat.Text = objMenuEntry.TransFormat;
                btnCopy.Enabled = true;
            }
        }

        /// <summary>
        /// Enable/Disable button based on selected menu
        /// </summary>
        private void EnableControls(bool pblnEnable)
        {
            btnSave.Enabled = txtFormat.Enabled = txtPrefix.Enabled = pblnEnable;
            txtMenuName.Enabled = txtCaptionEN.Enabled = txtCaptionVN.Enabled = pblnEnable;
        }

        /// <summary>
        /// Set width column, column number, set styte for c1FlexGridProtect
        ///	this function will be executed only one time when the form is loaded. 
        /// </summary>
        private void SetProtectFormat()
        {
            // Set Tree column
            dgrdMenu.Tree.Column = INT_POS_TREE;

            //dgrdMenu.Cols[0].Width = COLUMN_WIDTH_BASE;
            //dgrdMenu.Cols[INT_POS_TREE].Width = COLUMN_WIDTH_BASE * 10;
        }

        /// <summary>
        /// Load data from arrList into TreeProtect, This function 
        ///	will build the tree, get data from collection arrStorePermission.
        ///	RoleProtectionForm_Load and c1FlexGridRole_AfterRowColChange will call this function.
        /// </summary>
        /// <param name="c1FlexGridProtect"></param>
        /// <param name="pstrShortcut"></param>
        /// <param name="pintLevel"></param>
        /// <Author> Son HT, Dec 30, 2004</Author>
        private void LoadTreeProtect(ref C1FlexGrid c1FlexGridProtect, string pstrShortcut, int pintLevel)
        {
            // Scan all rows in Sys_Menu_Entry table to build tree 
            //arrSysMenuEntry.Sort(this);

            foreach (Sys_Menu_Entry voMenu in list)
            {
                // if this item is child node of current
                // as well as if it is a function with no form loaded or the formloaded is not right depended
                if ((voMenu.Parent_Shortcut != null && voMenu.Parent_Shortcut.Equals(pstrShortcut)))
                // && (voMenu.Parent_Child != (int)MenuParentChildEnum.SpecialLeafMenu))
                {
                    // Root Node
                    intCurrentRow++;
                    Node objNode = c1FlexGridProtect.Rows.InsertNode(intCurrentRow, pintLevel);
                    string strLangName = CultureInfo.CurrentUICulture.Name;
                    string strText = voMenu.Text_CaptionDefault;
                    switch (strLangName)
                    {
                        case Constants.CULTURE_VN:
                            strText = voMenu.Text_Caption_VI_VN;
                            break;
                        case Constants.CULTURE_JP:
                            strText = voMenu.Text_Caption_JA_JP;
                            break;
                        default:
                            strText = voMenu.Text_CaptionDefault;
                            break;
                    }
                    // Set Text for tree node
                    c1FlexGridProtect[intCurrentRow, INT_POS_TREE] = strText;
                    c1FlexGridProtect[intCurrentRow, PREFIX_COL] = voMenu.Prefix;
                    c1FlexGridProtect[intCurrentRow, FORMAT_COL] = voMenu.TransFormat;
                    c1FlexGridProtect[intCurrentRow, MENU_ENTRY_COL] = voMenu.Menu_EntryID;
                    c1FlexGridProtect[intCurrentRow, IS_TRANSACTION_COL] = voMenu.IsTransaction;
                    c1FlexGridProtect[intCurrentRow, IS_USER_CREATED_COL] = voMenu.IsUserCreated;
                    // Recursion build child node
                    LoadTreeProtect(ref c1FlexGridProtect, voMenu.Shortcut, pintLevel + 1);
                    if (objNode.Level == 0)
                    {
                        objNode.Collapsed = true;
                    }
                    //objNode.Data = voMenuEntry;
                }
            }
        }

        /// <summary>
        /// Validate data before save
        /// </summary>
        /// <returns>True if succeed, False if failure</returns>
        private bool ValidateData()
        {
            if (lblPrefix.ForeColor == Color.Maroon && txtPrefix.Text.Trim() == string.Empty)
            {
                string[] strMsg = { lblPrefix.Text };
                PCSMessageBox.Show(ErrorCode.MESSAGE_MANDATORY_FIELD_REQUIRED, MessageBoxIcon.Error, strMsg);
                txtPrefix.Focus();
                return false;
            }
            if (dgrdMenu.Rows[dgrdMenu.Row].Node == null)
                return false;
            return true;
        }

        /// <summary>
        /// Save data to database
        /// </summary>
        /// <returns>True if succeed. False if failure</returns>
        private bool SaveData()
        {
            Node objSelectedNode = null;
            try
            {
                objSelectedNode = dgrdMenu.Rows[intCurrentRow].Node;
            }
            catch
            {
                return false;
            }
            if (objSelectedNode == null)
            {
                return false;
            }
            var boManageMenu = new ManageMenuBO();
            Sys_Menu_Entry menu = new Sys_Menu_Entry();
            string strPrefix = txtPrefix.Text.Trim();
            string strFormat = txtFormat.Text.Trim();
            if (strPrefix != string.Empty)
            {
                switch (mFormMode)
                {
                    case EnumAction.Add:
                        menu.Text_CaptionDefault = string.Format("{0} ({1})", txtMenuName.Text.Trim(), strPrefix);
                        menu.Text_Caption_EN_US = string.Format("{0} ({1})", txtCaptionEN.Text.Trim(), strPrefix);
                        menu.Text_Caption_JA_JP = string.Format("{0} ({1})", txtCaptionEN.Text.Trim(), strPrefix);
                        menu.Text_Caption_VI_VN = string.Format("{0} ({1})", txtCaptionVN.Text.Trim(), strPrefix);
                        menu.Shortcut = string.Format("{0} ({1})", txtShortcut.Text, strPrefix);
                        break;
                    case EnumAction.Edit:
                        
                        #region Text_CaptionDefault

                        int intPos = txtMenuName.Text.Trim().IndexOf(OPEN_QUOTE);
                        Sys_Menu_Entry.Text_CaptionDefault = intPos > 0 ? txtMenuName.Text.Trim().Remove(intPos, txtMenuName.Text.Trim().Length - intPos).Trim() : txtMenuName.Text.Trim();
                        Sys_Menu_Entry.Text_CaptionDefault = string.Format("{0} ({1})", Sys_Menu_Entry.Text_CaptionDefault, strPrefix);
                        menu.Text_CaptionDefault = Sys_Menu_Entry.Text_CaptionDefault;

                        #endregion

                        #region Text_Caption_EN_US

                        intPos = txtCaptionEN.Text.Trim().IndexOf(OPEN_QUOTE);
                        Sys_Menu_Entry.Text_Caption_EN_US = intPos > 0 ? txtCaptionEN.Text.Trim().Remove(intPos, txtCaptionEN.Text.Trim().Length - intPos).Trim() : txtCaptionEN.Text.Trim();
                        Sys_Menu_Entry.Text_Caption_EN_US = string.Format("{0} ({1})", Sys_Menu_Entry.Text_Caption_EN_US, strPrefix);
                        menu.Text_Caption_EN_US = Sys_Menu_Entry.Text_Caption_EN_US;

                        #endregion

                        #region Text_Caption_JA_JP
                        intPos = Sys_Menu_Entry.Text_Caption_JA_JP.IndexOf(OPEN_QUOTE);
                        Sys_Menu_Entry.Text_Caption_JA_JP = intPos > 0 ? Sys_Menu_Entry.Text_Caption_JA_JP.Remove(intPos, Sys_Menu_Entry.Text_Caption_JA_JP.Length - intPos).Trim() : Sys_Menu_Entry.Text_Caption_JA_JP;
                        Sys_Menu_Entry.Text_Caption_EN_US = string.Format("{0} ({1})", Sys_Menu_Entry.Text_Caption_EN_US, strPrefix);
                        menu.Text_Caption_JA_JP = Sys_Menu_Entry.Text_Caption_JA_JP;

                        #endregion

                        #region Text_Caption_Vi_VN

                        intPos = txtCaptionVN.Text.Trim().IndexOf(OPEN_QUOTE);
                        Sys_Menu_Entry.Text_Caption_VI_VN = intPos > 0 ? txtCaptionVN.Text.Trim().Remove(intPos, txtCaptionVN.Text.Trim().Length - intPos).Trim() : txtCaptionVN.Text.Trim();
                        Sys_Menu_Entry.Text_Caption_VI_VN = string.Format("{0} ({1})", Sys_Menu_Entry.Text_Caption_VI_VN, strPrefix);
                        menu.Text_Caption_VI_VN = Sys_Menu_Entry.Text_Caption_VI_VN;

                        #endregion

                        #region Shortcut

                        intPos = Sys_Menu_Entry.Shortcut.IndexOf(OPEN_QUOTE);
                        if (intPos > 0)
                        {
                            Sys_Menu_Entry.Shortcut = Sys_Menu_Entry.Shortcut.Remove(intPos, Sys_Menu_Entry.Shortcut.Length - intPos).Trim();
                        }

                        Sys_Menu_Entry.Shortcut = string.Format("{0} ({1})", Sys_Menu_Entry.Shortcut, strPrefix);
                        menu.Shortcut = Sys_Menu_Entry.Shortcut;

                        #endregion

                        menu.Menu_EntryID = Sys_Menu_Entry.Menu_EntryID;
                        break;
                }
            }
            if (Sys_Menu_Entry.Text_CaptionDefault.Length > MAX_LENGTH_CAPTION)
            {
                string[] strMsg = { lblCaptionDefault.Text };
                PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_TOO_LONG, MessageBoxIcon.Exclamation, strMsg);
                return false;
            }
            if (Sys_Menu_Entry.Text_Caption_EN_US.Length > MAX_LENGTH_CAPTION)
            {
                string[] strMsg = { lblCaptionUS.Text };
                PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_TOO_LONG, MessageBoxIcon.Exclamation, strMsg);
                return false;
            }
            if (Sys_Menu_Entry.Text_Caption_JA_JP.Length > MAX_LENGTH_DEFAULT)
            {
                string[] strMsg = { lblCaptionJP.Text };
                PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_TOO_LONG, MessageBoxIcon.Exclamation, strMsg);
                return false;
            }
            if (Sys_Menu_Entry.Text_Caption_VI_VN.Length > MAX_LENGTH_CAPTION)
            {
                string[] strMsg = { lblCaptionVN.Text };
                PCSMessageBox.Show(ErrorCode.MESSAGE_VALUE_TOO_LONG, MessageBoxIcon.Exclamation, strMsg);
                return false;
            }
            Sys_Menu_Entry.Prefix = strPrefix;
            menu.Prefix = strPrefix;
            Sys_Menu_Entry.TransFormat = strFormat;
            menu.TransFormat = strFormat;
            menu.Parent_Shortcut = Sys_Menu_Entry.Parent_Shortcut;
            menu.IsTransaction = Sys_Menu_Entry.IsTransaction;
            menu.IsUserCreated = Sys_Menu_Entry.IsUserCreated;
            menu.Type = Sys_Menu_Entry.Type;
            menu.FormLoad = Sys_Menu_Entry.FormLoad;
            menu.Text_Caption_Language_Default = Sys_Menu_Entry.Text_Caption_Language_Default;
            menu.Button_Caption = Sys_Menu_Entry.Button_Caption;
            switch (mFormMode)
            {
                case EnumAction.Add:
                    Sys_Menu_Entry.IsUserCreated = 1;
                    menu.IsUserCreated = 1;
                    Sys_Menu_Entry.Menu_EntryID = boManageMenu.AddAndReturnID(menu, SystemProperty.RoleID);
                    intCurrentRow += 1;
                    // insert new node to grid
                    dgrdMenu.Rows.InsertNode(intCurrentRow, objSelectedNode.Level);
                    // sets Text for tree node
                    dgrdMenu[intCurrentRow, INT_POS_TREE] = menu.Text_CaptionDefault;
                    dgrdMenu[intCurrentRow, PREFIX_COL] = menu.Prefix;
                    dgrdMenu[intCurrentRow, FORMAT_COL] = menu.TransFormat;
                    dgrdMenu[intCurrentRow, MENU_ENTRY_COL] = menu.Menu_EntryID;
                    dgrdMenu[intCurrentRow, IS_TRANSACTION_COL] = menu.IsTransaction;
                    dgrdMenu[intCurrentRow, IS_USER_CREATED_COL] = menu.IsUserCreated;
                    break;
                case EnumAction.Edit:
                    menu.Menu_EntryID = Sys_Menu_Entry.Menu_EntryID;
                    boManageMenu.UpdateTrans(menu, SystemProperty.RoleID);
                    // sets Text for tree node
                    dgrdMenu[intCurrentRow, PREFIX_COL] = menu.Prefix;
                    dgrdMenu[intCurrentRow, FORMAT_COL] = menu.TransFormat;
                    dgrdMenu[intCurrentRow, INT_POS_TREE] = menu.Text_CaptionDefault;
                    break;
            }
            return true;
        }

        /// <summary>
        /// Check securtiy and load all menu to the tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageMenu_Load(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".ManageMenu_Load()";
            try
            {
                #region Security

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

                #endregion
                ManageMenuBO bo = new ManageMenuBO();
                list = bo.GetMenuAll();
                // set table Protect format
                SetProtectFormat();
                // Current row that system create row when Load tree data
                intCurrentRow = 0;
                LoadTreeProtect(ref dgrdMenu, MAIN, intCurrentRow);
                // invisible unneeded column
                dgrdMenu.Cols[MENU_ENTRY_COL].Visible = false;
                dgrdMenu.Cols[IS_USER_CREATED_COL].Visible = false;
                EnableControls(false);
                txtPrefix.CharacterCasing = CharacterCasing.Upper;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Make a copy of selected menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCopy_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            btnCopy_Click(null, null);

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Edit selected menu information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miEdit_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            btnEdit_Click(null, null);

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Delete selected menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelete_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            btnDelete_Click(null, null);

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Make a copy of selected menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            const string METHOD_NAME = THIS + ".btnCopy_Click()";
            try
            {
                int intSelectedID = 0;
                try
                {
                    intSelectedID = int.Parse(dgrdMenu[intCurrentRow, MENU_ENTRY_COL].ToString());
                }
                catch
                {
                }
                if (intSelectedID == 0 || Sys_Menu_Entry == null)
                {
                    // Code Inserted Automatically

                    #region Code Inserted Automatically

                    Cursor = Cursors.Default;

                    #endregion Code Inserted Automatically

                    return;
                }
                mFormMode = EnumAction.Add;
                // load data to form
                LoadDataToForm(Sys_Menu_Entry);
                // enable controls
                EnableControls(true);
                btnDelete.Enabled = btnCopy.Enabled = btnEdit.Enabled = false;
                lblCopyFrom.Visible = true;
                lblMenuName.Visible = false;
                // focus on Prefix TextBox
                txtPrefix.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Edit selected menu information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            const string METHOD_NAME = THIS + ".btnEdit_Click()";
            try
            {
                int intSelectedID = 0;
                try
                {
                    intSelectedID = int.Parse(dgrdMenu[dgrdMenu.Row, MENU_ENTRY_COL].ToString());
                }
                catch
                {
                }
                if (intSelectedID == 0 || Sys_Menu_Entry == null)
                {
                    // Code Inserted Automatically

                    #region Code Inserted Automatically

                    Cursor = Cursors.Default;

                    #endregion Code Inserted Automatically

                    return;
                }
                mFormMode = EnumAction.Edit;
                // load data to form
                LoadDataToForm(Sys_Menu_Entry);
                // enable controls
                EnableControls(true);
                txtMenuName.ReadOnly = txtCaptionEN.ReadOnly = txtCaptionVN.ReadOnly = (Sys_Menu_Entry.IsUserCreated <= 0);
                lblCopyFrom.Visible = false;
                lblMenuName.Visible = true;
                btnDelete.Enabled = btnCopy.Enabled = btnEdit.Enabled = false;
                // focus on Prefix TextBox
                txtPrefix.Focus();
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Delete selected menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            const string METHOD_NAME = THIS + ".btnDelete_Click()";
            try
            {
                int intSelectedID = 0;
                try
                {
                    intSelectedID = int.Parse(dgrdMenu[dgrdMenu.Row, MENU_ENTRY_COL].ToString());
                }
                catch
                {
                }
                if (intSelectedID == 0 )
                {
                    // Code Inserted Automatically

                    #region Code Inserted Automatically

                    Cursor = Cursors.Default;

                    #endregion Code Inserted Automatically

                    return;
                }
                // ask user to confirm
                DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Question);
                if (dlgResult == DialogResult.OK)
                {
                    var boManageMenu = new ManageMenuBO();
                    boManageMenu.Delete(intSelectedID);
                    FormControlComponents.ClearForm(this);
                    // reload the form
                    int intIndex = dgrdMenu.Row;
                    dgrdMenu.Rows.Remove(intIndex);
                    dgrdMenu.Row = intIndex;
                    dgrdMenu_RowColChange(null, null);
                    blnIsChanged = true;
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            Close();

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// Validate data and save changes to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.WaitCursor;

            #endregion Code Inserted Automatically

            const string METHOD_NAME = THIS + ".btnSave_Click()";
            try
            {
                if (!ValidateData())
                {
                    // Code Inserted Automatically

                    #region Code Inserted Automatically

                    Cursor = Cursors.Default;

                    #endregion Code Inserted Automatically

                    return;
                }
                if (SaveData())
                {
                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                    mFormMode = EnumAction.Default;
                    // disable control
                    EnableControls(false);
                    dgrdMenu.Row = intCurrentRow;
                    btnEdit.Enabled = true;
                    btnCopy.Enabled = (Sys_Menu_Entry.IsTransaction > 0 && Sys_Menu_Entry.IsUserCreated == 0);
                    btnDelete.Enabled = (Sys_Menu_Entry.IsUserCreated > 0);
                    dgrdMenu_RowColChange(null, null);
                    blnIsChanged = true;
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Code Inserted Automatically

            #region Code Inserted Automatically

            Cursor = Cursors.Default;

            #endregion Code Inserted Automatically
        }

        /// <summary>
        /// When user change the selection, get the selecetd object
        /// in order to enable/disable related buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdMenu_RowColChange(object sender, EventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdMenu_RowColChange()";
            try
            {
                int intIsTransaction = 0;
                int intIsUserCreated = 0;
                try
                {
                    if (dgrdMenu[dgrdMenu.Row, IS_USER_CREATED_COL] != DBNull.Value)
                    {
                        intIsUserCreated = int.Parse(dgrdMenu[dgrdMenu.Row, IS_USER_CREATED_COL].ToString());
                    }
                }
                catch 
                {
                    
                }
                try
                {
                    intIsTransaction =
                        Convert.ToInt32(Convert.ToBoolean(dgrdMenu[dgrdMenu.Row, IS_TRANSACTION_COL].ToString()));
                }
                catch
                {
                }
                if (intIsUserCreated > 0)
                {
                    // allows user to delete copied menu
                    btnDelete.Enabled = miDelete.Enabled = true;
                    // prefix is mandatory
                    lblPrefix.ForeColor = Color.Maroon;
                }
                else
                {
                    // not allows user to delete original menu
                    btnDelete.Enabled = miDelete.Enabled = false;
                    // prefix is not mandatory
                    lblPrefix.ForeColor = Color.Black;
                }
                // selected menu is the transaction menu
                // allows user to copy/edit this menu
                btnEdit.Enabled = miEdit.Enabled = (intIsTransaction > 0);
                btnCopy.Enabled = miCopy.Enabled = (intIsTransaction > 0 && intIsUserCreated == 0);
                // get the selected menu
                int intSelectedID = 0;
                try
                {
                    intSelectedID = int.Parse(dgrdMenu[dgrdMenu.Row, MENU_ENTRY_COL].ToString());
                }
                catch 
                {
                    
                }
                if (intSelectedID == 0)
                    return;
                var boManageMenu = new ManageMenuBO();
                Sys_Menu_Entry = boManageMenu.GetMenu(intSelectedID);
                LoadDataToForm(Sys_Menu_Entry);
                lblCopyFrom.Visible = false;
                lblMenuName.Visible = true;
                EnableControls(false);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                intCurrentRow = dgrdMenu.Row;
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// When the form is closing, if user made any changes, ask them to confirm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageMenu_Closing(object sender, CancelEventArgs e)
        {
            const string METHOD_NAME = THIS + ".ManageMenu_Closing()";
            try
            {
                if (mFormMode != EnumAction.Default)
                {
                    DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,
                                                                    MessageBoxButtons.YesNoCancel,
                                                                    MessageBoxIcon.Question);
                    switch (confirmDialog)
                    {
                        case DialogResult.Yes:
                            try
                            {
                                if (!ValidateData())
                                {
                                    txtPrefix.Focus();
                                    e.Cancel = true;
                                }
                                else if (!SaveData())
                                {
                                    txtPrefix.Focus();
                                    e.Cancel = true;
                                }
                                else
                                {
                                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                                    mFormMode = EnumAction.Default;
                                    // disable control
                                    EnableControls(false);
                                    btnEdit.Enabled = true;
                                    btnCopy.Enabled = (Sys_Menu_Entry.IsTransaction > 0 && Sys_Menu_Entry.IsUserCreated == 0);
                                    btnDelete.Enabled = (Sys_Menu_Entry.IsUserCreated > 0);
                                    blnIsChanged = true;
                                }
                            }
                            catch
                            {
                                txtPrefix.Focus();
                                e.Cancel = true;
                            }
                            break;
                        case DialogResult.Cancel:
                            txtPrefix.Focus();
                            e.Cancel = true;
                            break;
                    }
                }
                // check if user made any changes to the menu
                // ask them to re-login to get new menu
                if (blnIsChanged && !e.Cancel)
                {
                    // display alert message
                    PCSMessageBox.Show(ErrorCode.MESSAGE_APPLY_NEW_MENU, MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
                }
            }
            catch (PCSException ex)
            {
                // displays the error message.
                PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log message.
                try
                {
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Sets the selected row and col for grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdMenu_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    HitTestInfo hiClicked = dgrdMenu.HitTest(e.X, e.Y);
                    intCurrentRow = dgrdMenu.Row = hiClicked.Row;
                    dgrdMenu.Col = hiClicked.Column;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Before move to another row, check the form mode to ask user want to save the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdMenu_BeforeRowColChange(object sender, RangeEventArgs e)
        {
            const string METHOD_NAME = THIS + ".dgrdMenu_BeforeRowColChange()";
            try
            {
                if (mFormMode != EnumAction.Default)
                {
                    DialogResult confirmDialog = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE,
                                                                    MessageBoxButtons.YesNoCancel,
                                                                    MessageBoxIcon.Question);
                    switch (confirmDialog)
                    {
                        case DialogResult.Yes:
                            try
                            {
                                if (!ValidateData())
                                {
                                    txtPrefix.Focus();
                                    e.Cancel = true;
                                }
                                else if (!SaveData())
                                {
                                    txtPrefix.Focus();
                                    e.Cancel = true;
                                }
                                else
                                {
                                    PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA);
                                    mFormMode = EnumAction.Default;
                                    // disable control
                                    EnableControls(false);
                                    btnEdit.Enabled = true;
                                    btnCopy.Enabled = (Sys_Menu_Entry.IsTransaction > 0 && Sys_Menu_Entry.IsUserCreated == 0);
                                    btnDelete.Enabled = (Sys_Menu_Entry.IsUserCreated > 0);
                                    blnIsChanged = true;
                                }
                            }
                            catch
                            {
                                txtPrefix.Focus();
                                e.Cancel = true;
                            }
                            break;
                        case DialogResult.No:
                            // clear the form
                            FormControlComponents.ClearForm(this);
                            mFormMode = EnumAction.Default;
                            break;
                        case DialogResult.Cancel:
                            txtPrefix.Focus();
                            e.Cancel = true;
                            break;
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
                    Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
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
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageMenu));
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grbFunctions = new System.Windows.Forms.GroupBox();
            this.dgrdMenu = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.cMenu = new System.Windows.Forms.ContextMenu();
            this.miCopy = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miDelete = new System.Windows.Forms.MenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbDetails = new System.Windows.Forms.GroupBox();
            this.lblCaptionDefault = new System.Windows.Forms.Label();
            this.txtMenuName = new System.Windows.Forms.TextBox();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtCaptionEN = new System.Windows.Forms.TextBox();
            this.txtCaptionJP = new System.Windows.Forms.TextBox();
            this.txtShortcut = new System.Windows.Forms.TextBox();
            this.txtCaptionVN = new System.Windows.Forms.TextBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblCaptionUS = new System.Windows.Forms.Label();
            this.lblCaptionVN = new System.Windows.Forms.Label();
            this.lblMenuName = new System.Windows.Forms.Label();
            this.lblCopyFrom = new System.Windows.Forms.Label();
            this.lblCaptionJP = new System.Windows.Forms.Label();
            this.lblShortcut = new System.Windows.Forms.Label();
            this.grbFunctions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdMenu)).BeginInit();
            this.grbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grbFunctions
            // 
            resources.ApplyResources(this.grbFunctions, "grbFunctions");
            this.grbFunctions.Controls.Add(this.dgrdMenu);
            this.grbFunctions.Name = "grbFunctions";
            this.grbFunctions.TabStop = false;
            // 
            // dgrdMenu
            // 
            this.dgrdMenu.AllowEditing = false;
            this.dgrdMenu.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            resources.ApplyResources(this.dgrdMenu, "dgrdMenu");
            this.dgrdMenu.ContextMenu = this.cMenu;
            this.dgrdMenu.Name = "dgrdMenu";
            this.dgrdMenu.Rows.Count = 1;
            this.dgrdMenu.Rows.DefaultSize = 17;
            this.dgrdMenu.StyleInfo = resources.GetString("dgrdMenu.StyleInfo");
            this.dgrdMenu.Tree.Column = 1;
            this.dgrdMenu.Tree.Indent = 10;
            this.dgrdMenu.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.dgrdMenu_BeforeRowColChange);
            this.dgrdMenu.RowColChange += new System.EventHandler(this.dgrdMenu_RowColChange);
            this.dgrdMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgrdMenu_MouseDown);
            // 
            // cMenu
            // 
            this.cMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miCopy,
            this.miEdit,
            this.miDelete});
            // 
            // miCopy
            // 
            this.miCopy.Index = 0;
            resources.ApplyResources(this.miCopy, "miCopy");
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // miEdit
            // 
            this.miEdit.Index = 1;
            resources.ApplyResources(this.miEdit, "miEdit");
            this.miEdit.Click += new System.EventHandler(this.miEdit_Click);
            // 
            // miDelete
            // 
            this.miDelete.Index = 2;
            resources.ApplyResources(this.miDelete, "miDelete");
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbDetails
            // 
            resources.ApplyResources(this.grbDetails, "grbDetails");
            this.grbDetails.Controls.Add(this.lblCaptionDefault);
            this.grbDetails.Controls.Add(this.txtMenuName);
            this.grbDetails.Controls.Add(this.txtFormat);
            this.grbDetails.Controls.Add(this.txtPrefix);
            this.grbDetails.Controls.Add(this.txtCaptionEN);
            this.grbDetails.Controls.Add(this.txtCaptionJP);
            this.grbDetails.Controls.Add(this.txtShortcut);
            this.grbDetails.Controls.Add(this.txtCaptionVN);
            this.grbDetails.Controls.Add(this.lblFormat);
            this.grbDetails.Controls.Add(this.lblPrefix);
            this.grbDetails.Controls.Add(this.lblCaptionUS);
            this.grbDetails.Controls.Add(this.lblCaptionVN);
            this.grbDetails.Controls.Add(this.lblMenuName);
            this.grbDetails.Controls.Add(this.lblCopyFrom);
            this.grbDetails.Controls.Add(this.lblCaptionJP);
            this.grbDetails.Name = "grbDetails";
            this.grbDetails.TabStop = false;
            // 
            // lblCaptionDefault
            // 
            resources.ApplyResources(this.lblCaptionDefault, "lblCaptionDefault");
            this.lblCaptionDefault.Name = "lblCaptionDefault";
            // 
            // txtMenuName
            // 
            resources.ApplyResources(this.txtMenuName, "txtMenuName");
            this.txtMenuName.Name = "txtMenuName";
            // 
            // txtFormat
            // 
            resources.ApplyResources(this.txtFormat, "txtFormat");
            this.txtFormat.Name = "txtFormat";
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // txtCaptionEN
            // 
            resources.ApplyResources(this.txtCaptionEN, "txtCaptionEN");
            this.txtCaptionEN.Name = "txtCaptionEN";
            // 
            // txtCaptionJP
            // 
            resources.ApplyResources(this.txtCaptionJP, "txtCaptionJP");
            this.txtCaptionJP.Name = "txtCaptionJP";
            this.txtCaptionJP.ReadOnly = true;
            // 
            // txtShortcut
            // 
            resources.ApplyResources(this.txtShortcut, "txtShortcut");
            this.txtShortcut.Name = "txtShortcut";
            this.txtShortcut.ReadOnly = true;
            // 
            // txtCaptionVN
            // 
            resources.ApplyResources(this.txtCaptionVN, "txtCaptionVN");
            this.txtCaptionVN.Name = "txtCaptionVN";
            // 
            // lblFormat
            // 
            resources.ApplyResources(this.lblFormat, "lblFormat");
            this.lblFormat.Name = "lblFormat";
            // 
            // lblPrefix
            // 
            this.lblPrefix.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Name = "lblPrefix";
            // 
            // lblCaptionUS
            // 
            resources.ApplyResources(this.lblCaptionUS, "lblCaptionUS");
            this.lblCaptionUS.Name = "lblCaptionUS";
            // 
            // lblCaptionVN
            // 
            resources.ApplyResources(this.lblCaptionVN, "lblCaptionVN");
            this.lblCaptionVN.Name = "lblCaptionVN";
            // 
            // lblMenuName
            // 
            resources.ApplyResources(this.lblMenuName, "lblMenuName");
            this.lblMenuName.Name = "lblMenuName";
            // 
            // lblCopyFrom
            // 
            resources.ApplyResources(this.lblCopyFrom, "lblCopyFrom");
            this.lblCopyFrom.Name = "lblCopyFrom";
            // 
            // lblCaptionJP
            // 
            resources.ApplyResources(this.lblCaptionJP, "lblCaptionJP");
            this.lblCaptionJP.Name = "lblCaptionJP";
            // 
            // lblShortcut
            // 
            resources.ApplyResources(this.lblShortcut, "lblShortcut");
            this.lblShortcut.Name = "lblShortcut";
            // 
            // ManageMenu
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.grbDetails);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grbFunctions);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblShortcut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "ManageMenu";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ManageMenu_Closing);
            this.Load += new System.EventHandler(this.ManageMenu_Load);
            this.grbFunctions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdMenu)).EndInit();
            this.grbDetails.ResumeLayout(false);
            this.grbDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
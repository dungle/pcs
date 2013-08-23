using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Command;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using PCSComUtils.Framework.ReportFrame.BO;
using PCSComUtils.Framework.ReportFrame.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Admin;
using PCSUtils.Framework.ReportFrame;
using PCSUtils.Log;
using PCSUtils.Utils;
//using CopyDirectory;

namespace PCSMain
{
    /// <summary>
    ///     Summary description for MainForm.
    /// </summary>
    public class MainForm : Form
    {
        private const string HelpItem = "HELP";
        private const int IdxImageExpandedFolder = 0;
        private const int IdxImageFolder = 1;
        private const int IdxImageForm = 2;
        private const string MainParent = "MAIN";
        private const string Nothing = "nothing";
        private const string This = "PCSMain.MainForm";
        private readonly ArrayList _mOpenedForms = new ArrayList();
        private readonly Hashtable _menuItemHash = new Hashtable();
        private CultureInfo _englishCulture;
        private CultureInfo _japaneseCulture;
        private CultureInfo _vietnameseCulture;
        private IContainer components;

        private ImageList imglIcon;
        private ImageList imglTreeNode;
        private Label lblCommand;
        private Label lblMain;
        private Label lblSelection;
        private List<Sys_Menu_Entry> listmenu = new List<Sys_Menu_Entry>();
        private MenuItem mnuChangeIcon;
        private ContextMenu mnuContext;
        private StatusBar sbarCmd;
        private StatusBarPanel spnlCCN;
        private StatusBarPanel spnlCommand;
        private StatusBarPanel spnlDBName;
        private StatusBarPanel spnlDBServer;
        private StatusBarPanel spnlOther;
        private StatusBarPanel spnlUsername;
        private StatusBarPanel spnlVersion;
        private C1DockingTab tabMenus;
        private TextBox txtcmd;

        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        ///     Clean up any resources being used.
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

        protected override void OnClosing(CancelEventArgs e)
        {
            Utilities.Instance.CleanTempTable();
            base.OnClosing(e);
        }

        /// <summary>
        ///     This method uses to log-out from PCS
        /// </summary>
        private void Logout()
        {
            Application.Restart();
        }

        #region events

        private void MainForm_Load(object sender, EventArgs e)
        {
            const string methodName = This + ".MainForm_Load()";
            _englishCulture = new CultureInfo(Constants.CULTURE_EN);
            _japaneseCulture = new CultureInfo(Constants.CULTURE_JP);
            _vietnameseCulture = new CultureInfo(Constants.CULTURE_VN);
            try
            {
                if (SystemProperty.ArrayForms == null)
                    SystemProperty.ArrayForms = new ArrayList();
                if (SystemProperty.TempTables == null)
                    SystemProperty.TempTables = new List<string>();

                if (!string.IsNullOrEmpty(SystemProperty.ApplicationName))
                    Text = SystemProperty.ApplicationName;
                Icon = SystemProperty.FormIcon;
                //get all menus
                var boCommon = new CommonBO();
                var obj = new ManageMenuBO();
                listmenu = !SystemProperty.UserName.ToLower().Equals(Constants.SUPER_ADMIN_USER) ? boCommon.GetAllMenusWithImageFields(CultureInfo.CurrentUICulture, SystemProperty.UserName) : obj.GetMenuAllWithImageFields();

                SystemProperty.TableMenuEntry = listmenu;
                //buidl menu for the form
                _menuItemHash.Clear();
                var mainMenu = new MainMenu();
                Menu m = mainMenu;
                LoadMenuTree(ref m, MainParent);
                Menu = mainMenu;
                BuildMenuTree();
                ShowStatusBarInfo();
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }

            //added by DuongNA 19-Sep-2005
            if (tabMenus.SelectedTab != null)
            {
                tabMenus.SelectedTab.Focus();
            }
        }

        private void MainForm_Closed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        ///     Click event of menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = This + ".menuItem_Click()";
            try
            {
                var m = (MenuItem) sender;
                // if user click on report or help item then display message that this module not implement yet
                if (m.Text.ToUpper().Equals(HelpItem))
                {
                    PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                    return;
                }

                Sys_Menu_Entry voMenu = FindMenuVO(m.Text, CultureInfo.CurrentUICulture.Name);

                //check to found class to init
                if ((voMenu.FormLoad != string.Empty) && (voMenu.FormLoad.ToLower() != Nothing))
                {
                    //load selected form
                    LoadSelectedForm(voMenu);
                    return;
                }
                if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.ENG)))
                {
                    Thread.CurrentThread.CurrentUICulture = _englishCulture;
                    ChangeLanguage();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.VIE)))
                {
                    Thread.CurrentThread.CurrentUICulture = _vietnameseCulture;
                    ChangeLanguage();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.JAP)))
                {
                    Thread.CurrentThread.CurrentUICulture = _japaneseCulture;
                    ChangeLanguage();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.EXT))
                         || voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.EXIT)))
                {
                    DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_DO_YOU_REALLY_WANT_TO_EXIT,
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                             MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.OUT)))
                    Logout();
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        ///     when select or mouse over menu item, display command in status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem_Select(object sender, EventArgs e)
        {
            var miCurrent = (MenuItem) sender;
            sbarCmd.Panels[0].Text = miCurrent.Text;
        }

        /// <summary>
        ///     when user double click on tree node
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 17-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeNode_DblClick(object sender, EventArgs e)
        {
            const string METHOD_NAME = This + ".treeNode_DblClick()";

            try
            {
                var tvwSubMenu = (TreeView) sender;

                Point ptMouse = Cursor.Position;
                ptMouse = tvwSubMenu.PointToClient(ptMouse);

                TreeNode node = tvwSubMenu.GetNodeAt(ptMouse.X, ptMouse.Y);
                if (node == null)
                {
                    return;
                }
                string strMenu = tvwSubMenu.SelectedNode.Text;

                // if user click on report or help item then display message that this module not implement yet
                if (strMenu.ToUpper().Equals(HelpItem))
                {
                    PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                    return;
                }

                Sys_Menu_Entry voMenu = FindMenuVO(strMenu, CultureInfo.CurrentUICulture.Name);
                ExecMenuEntry(voMenu);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        ///     when select or mouse over tree node, display command in status bar
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 17-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeNode_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //sbarCmd.Text = lblCommand.Text + e.Node.Text;
            sbarCmd.Panels[0].Text = e.Node.Text;
        }

        /// <summary>
        ///     when key down on menu tree
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 18-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeNode_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = This + ".treeNode_KeyDown()";
            try
            {
                var tvwObj = (TreeView) sender;
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        {
                            if ((tvwObj.SelectedNode != null) && (tvwObj.SelectedNode.Parent != null))
                                tvwObj.SelectedNode = tvwObj.SelectedNode.Parent;
                            else
                            {
                                //focus to tab
                                if (tvwObj.Parent.Parent != null)
                                {
                                    tvwObj.Parent.Parent.Focus();
                                    sbarCmd.Panels[0].Text = tabMenus.SelectedTab.Text;
                                }
                            }
                            e.Handled = true;
                            break;
                        }
                    case Keys.Enter:
                        {
                            string strMenu = tvwObj.SelectedNode.Text;
                            // if user click on report or help item then display message that this module not implement yet
                            if (strMenu.ToUpper().Equals(HelpItem))
                                PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                            else
                            {
                                var voMenu = (Sys_Menu_Entry) tvwObj.SelectedNode.Tag;
                                ExecMenuEntry(voMenu);
                            }
                            if (tvwObj.SelectedNode.Nodes.Count > 0)
                            {
                                if (tvwObj.SelectedNode.IsExpanded)
                                    tvwObj.SelectedNode.Collapse();
                                else
                                    tvwObj.SelectedNode.Expand();
                            }
                            e.Handled = true;
                            break;
                        }
                    default:
                        {
                            e.Handled = false;
                            break;
                        }
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        ///     when key down on menu tree
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 18-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMenus_KeyDown(object sender, KeyEventArgs e)
        {
            const string METHOD_NAME = This + ".tabMenus_KeyDown()";
            try
            {
                var tabSubMenu = (C1DockingTabPage) sender;
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        {
                            if (tabSubMenu.Controls.Count > 0)
                            {
                                try
                                {
                                    var pnlObj = (Panel) tabSubMenu.Controls[0];
                                    var tvwObj = (TreeView) pnlObj.Controls[0];
                                    if (tvwObj.Nodes.Count > 0)
                                    {
                                        pnlObj.Controls[0].Focus();
                                        sbarCmd.Panels[0].Text = tvwObj.SelectedNode.Text;
                                    }
                                }
                                catch
                                {
                                }
                            }
                            break;
                        }
                    case Keys.Enter:
                        {
                            string strMenu = tabSubMenu.Text;
                            // if user click on report or help item then display message that this module not implement yet
                            if (strMenu.ToUpper().Equals(HelpItem))
                                PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                            else
                            {
                                var voMenu = (Sys_Menu_Entry) tabSubMenu.Tag;
                                ExecMenuEntry(voMenu);
                            }
                            e.Handled = true;
                            break;
                        }
                    default:
                        {
                            e.Handled = false;
                            break;
                        }
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        ///     when mouse click on screen menus tab
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 19-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMenus_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = This + ".tabMenus_Click()";

            const int TAB_INDENT = 4;
            const int TAB_BORDER = 3;

            try
            {
                if (tabMenus.SelectedTab == null)
                    return;

                string strMenu = tabMenus.SelectedTab.Text;

                //sbarCmd.Panels[0].Text = strMenu;
                Point ptMouse = Cursor.Position;
                ptMouse = tabMenus.PointToClient(ptMouse);
                int nMax = TAB_INDENT +
                           tabMenus.TabPages.Count*(tabMenus.Padding.Y*2 + (int) tabMenus.Font.GetHeight() + TAB_BORDER);
                if ((ptMouse.Y > nMax) || (ptMouse.Y < TAB_INDENT))
                    return;

                // if user click on report or help item then display message that this module not implement yet
                if (strMenu.ToUpper().Equals(HelpItem))
                {
                    PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                    return;
                }
                var voMenu = (Sys_Menu_Entry) tabMenus.SelectedTab.Tag;
                ExecMenuEntry(voMenu);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        ///     before treenode expanded
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 21-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvwSubMenus_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node == null)
                return;
            var voMenu = (Sys_Menu_Entry) e.Node.Tag;
            if (e.Node.Nodes.Count > 0)
            {
                int expan = voMenu.ExpandedImage ?? 0;
                e.Node.ImageIndex = e.Node.SelectedImageIndex = expan - 1;
                if (e.Node.ImageIndex < 0)
                {
                    e.Node.ImageIndex = e.Node.SelectedImageIndex = IdxImageExpandedFolder;
                }
            }
            e.Cancel = false;
        }

        /// <summary>
        ///     before treenode collapsed
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 21-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvwSubMenus_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            var voMenu = (Sys_Menu_Entry) e.Node.Tag;
            if (e.Node.Nodes.Count > 0)
            {
                int col = voMenu.CollapsedImage ?? 0;
                e.Node.ImageIndex = e.Node.SelectedImageIndex = col - 1;
                if (e.Node.ImageIndex < 0)
                {
                    e.Node.ImageIndex = e.Node.SelectedImageIndex = IdxImageFolder;
                }
            }
            e.Cancel = false;
        }

        /// <summary>
        ///     popup menu click
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 23-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangeIcon_Click(object sender, EventArgs e)
        {
            const string METHOD_NAME = This + ".mnuChangeIcon_Click";
            try
            {
                var mnuItem = (MenuItem) sender;
                var tvwObj = (TreeView) (((ContextMenu) mnuItem.Parent).SourceControl);
                if (tvwObj.SelectedNode == null)
                {
                    return;
                }
                var frmSelectIcon = new SelectIcon();
                var voMenu = tvwObj.SelectedNode.Tag as Sys_Menu_Entry;
                if (voMenu != null)
                {
                    frmSelectIcon.MenuEntry = voMenu;
                    frmSelectIcon.IsGroup = (tvwObj.SelectedNode.Nodes.Count > 0);
                    if (frmSelectIcon.ShowDialog(this) == DialogResult.OK)
                    {
                        voMenu.CollapsedImage = frmSelectIcon.MenuEntry.CollapsedImage;
                        voMenu.ExpandedImage = frmSelectIcon.MenuEntry.ExpandedImage;
                        var boCommon = new CommonBO();
                        boCommon.UpdateMenuWithImageFields(voMenu.Menu_EntryID, voMenu.CollapsedImage,
                                                           voMenu.ExpandedImage);
                        if (tvwObj.SelectedNode.IsExpanded)
                        {
                            int expan = frmSelectIcon.MenuEntry.ExpandedImage ?? 0;
                            tvwObj.SelectedNode.ImageIndex =
                                tvwObj.SelectedNode.SelectedImageIndex = expan - 1;
                        }
                        else
                        {
                            int col = frmSelectIcon.MenuEntry.CollapsedImage ?? 0;
                            tvwObj.SelectedNode.ImageIndex =
                                tvwObj.SelectedNode.SelectedImageIndex = col - 1;
                        }
                        tvwObj.Invalidate(true);
                    }
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        /// <summary>
        ///     right click in treeview
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 24-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvwSubMenus_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var tvwObj = (TreeView) sender;
                if (e.Button == MouseButtons.Right)
                    tvwObj.SelectedNode = tvwObj.GetNodeAt(e.X, e.Y);
            }
            catch
            {
            }
        }

        /// <summary>
        ///     when selected tab menu change
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 28-Sep-2005
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMenus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sbarCmd.Panels[0].Text = tabMenus.SelectedTab.Text;
            }
            catch
            {
            }
        }

        #endregion

        #region private methods

        /// <summary>
        ///     Find the Sys_Menu_EntryVO object by MenuText
        /// </summary>
        /// <param name="pstrMenuText">MenuText</param>
        /// <param name="pstrLang">Language</param>
        /// <returns>Sys_Menu_EntryVO</returns>
        private Sys_Menu_Entry FindMenuVO(string pstrMenuText, string pstrLang)
        {
            Sys_Menu_Entry voResult = null;
            foreach (Sys_Menu_Entry voMenu in listmenu)
            {
                string strMenuText = voMenu.Text_CaptionDefault;
                switch (pstrLang)
                {
                    case Constants.CULTURE_VN:
                        strMenuText = voMenu.Text_Caption_VI_VN;
                        break;
                    case Constants.CULTURE_JP:
                        strMenuText = voMenu.Text_Caption_JA_JP;
                        break;
                }

                if (strMenuText.Equals(pstrMenuText))
                {
                    voResult = voMenu;
                    break;
                }
            }
            return voResult;
        }

        /// <summary>
        ///     Load menu tree
        /// </summary>
        /// <param name="mnuParent"></param>
        /// <param name="pstrShortcut"></param>
        private void LoadMenuTree(ref Menu mnuParent, string pstrShortcut)
        {
            foreach (Sys_Menu_Entry voMenu in listmenu)
            {
                if (voMenu.Parent_Shortcut == null || !voMenu.Parent_Shortcut.Equals(pstrShortcut)) continue;
                //else return;
                string strShortcut = voMenu.Shortcut;
                string strMenuText = voMenu.Text_CaptionDefault;
                CultureInfo lang = CultureInfo.CurrentUICulture;
                switch (lang.Name)
                {
                    case Constants.CULTURE_VN:
                        strMenuText = voMenu.Text_Caption_VI_VN;
                        break;
                    case Constants.CULTURE_JP:
                        strMenuText = voMenu.Text_Caption_JA_JP;
                        break;
                    default:
                        strMenuText = voMenu.Text_CaptionDefault;
                        break;
                }

                var menuItem = new MenuItem();
                mnuParent.MenuItems.Add(menuItem);
                menuItem.Text = strMenuText;
                Menu m = menuItem;
                if (mnuParent.MenuItems.Count > 1)
                {
                    if (mnuParent.MenuItems[mnuParent.MenuItems.Count - 2].Text.Equals("-"))
                    {
                        if (strMenuText.Equals("-"))
                        {
                            menuItem.Visible = false;
                        }
                        else
                        {
                            mnuParent.MenuItems[mnuParent.MenuItems.Count - 2].Visible = true;
                        }
                    }
                }

                LoadMenuTree(ref m, strShortcut);
                if ((menuItem.GetType() == typeof (MenuItem)) && (!strMenuText.Equals("-")))
                {
                    _menuItemHash.Add(strShortcut, menuItem);
                    menuItem.Click += menuItem_Click;
                    menuItem.Select += menuItem_Select;
                }
                else if (mnuParent.MenuItems[mnuParent.MenuItems.Count - 1].Text.Equals("-"))
                {
                    mnuParent.MenuItems[mnuParent.MenuItems.Count - 1].Visible = false;
                }
                foreach (MenuItem mItem in mnuParent.MenuItems)
                {
                    if (!mItem.Visible) continue;
                    if (mItem.Text.Equals("-"))
                        mItem.Visible = false;
                    break;
                }
            }
        }

        /// <summary>
        ///     Display the selected form
        /// </summary>
        /// <param name="pvoMenu">Form name</param>
        private void LoadSelectedForm(Sys_Menu_Entry pvoMenu)
        {
            const string METHOD_NAME = This + ".LoadSelectedForm()";
            const string DOT = ".";
            const string DLL_EXTENSION = ".dll";
            const string SLASH = "\\";
            string strFormName = pvoMenu.FormLoad;
            Cursor = Cursors.WaitCursor;
            if (strFormName.ToLower().Equals(Nothing)) return;
            try
            {
                //create the application instance
                //find the dll name
                int nDotPos = 0;
                nDotPos = strFormName.IndexOf(DOT);
                // if form name not including dot character it means current not implement yet
                if (nDotPos < 0)
                {
                    PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                    Cursor = Cursors.Default;
                    return;
                }
                string strAssemblyName = strFormName.Substring(0, nDotPos) + DLL_EXTENSION;

                Assembly assem = Assembly.LoadFrom(Application.StartupPath + SLASH + strAssemblyName);
                Type objType = assem.GetType(strFormName);
                if (objType == null)
                {
                    PCSMessageBox.Show(ErrorCode.NOT_IMPLEMENT);
                    Cursor = Cursors.Default;
                    return;
                }
                var frmToLoad = (Form) Activator.CreateInstance(objType);

                var formInfor = new FormInfo(frmToLoad, pvoMenu.Prefix, pvoMenu.TransFormat, pvoMenu.TableName,
                                             pvoMenu.TransNoFieldName, SystemProperty.UserName);
                SystemProperty.ArrayForms.Add(formInfor);
                string strLangName = CultureInfo.CurrentUICulture.Name;
                string strText = pvoMenu.Text_CaptionDefault;
                switch (strLangName)
                {
                    case Constants.CULTURE_JP:
                        strText = pvoMenu.Text_Caption_JA_JP;
                        break;
                    case Constants.CULTURE_VN:
                        strText = pvoMenu.Text_Caption_VI_VN;
                        break;
                    default:
                        strText = pvoMenu.Text_CaptionDefault;
                        break;
                }
                frmToLoad.Text = strText;
                frmToLoad.Icon = SystemProperty.FormIcon;
                if (!string.IsNullOrEmpty(pvoMenu.ReportID))
                {
                    var frmViewReport = (ViewReport) frmToLoad;
                    frmViewReport.VoReport = (sys_ReportVO) (new ViewReportBO()).GetReportByReportID(pvoMenu.ReportID);
                    frmViewReport.ViewMode = ViewReportMode.Normal;
                }
                _mOpenedForms.Add(frmToLoad);
                frmToLoad.Show();
            }
            catch (FileNotFoundException ex)
            {
                PCSMessageBox.Show(ErrorCode.TYPELOADEXCEPTION);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
            catch (TypeLoadException ex)
            {
                PCSMessageBox.Show(ErrorCode.TYPELOADEXCEPTION);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
            Cursor = Cursors.Default;
        }

        private void BuildMenuTree()
        {
            const string METHOD_NAME = This + ".BuildMenuTree()";

            const int LBL_Y_SIZE = 60;
            const int DOCK_PADDING_SIZE = 10;
            const int DOCK_PADDING_HALF_SIZE = 5;
            const int ITEM_HEIGHT = 18;

            Color objLblBackColor = Color.FromArgb(84, 84, 84);
            Color objLblForeColor = Color.White;
            var objLblFont = new Font("Times New Roman", 24);
            TreeView tvwSubMenus;

            try
            {
                //build level 1 menus
                tabMenus.TabPages.Clear();
                foreach (Sys_Menu_Entry voMenu in listmenu)
                {
                    if ((voMenu.Text_CaptionDefault != null && voMenu.Text_CaptionDefault.Equals("-")) ||
                        (voMenu.Parent_Shortcut != null && !voMenu.Parent_Shortcut.Equals(MainParent)))
                        continue;
                    //else return;
                    //create tab page
                    var tabMenuItem = new C1DockingTabPage();
                    string strMenuText = voMenu.Text_CaptionDefault;
                    CultureInfo lang = CultureInfo.CurrentUICulture;
                    switch (lang.Name)
                    {
                        case Constants.CULTURE_VN:
                            strMenuText = voMenu.Text_Caption_VI_VN;
                            break;
                        case Constants.CULTURE_JP:
                            strMenuText = voMenu.Text_Caption_JA_JP;
                            break;
                        default:
                            strMenuText = voMenu.Text_CaptionDefault;
                            break;
                    }
                    tabMenuItem.Text = strMenuText;
                    tabMenuItem.KeyDown += tabMenus_KeyDown;
                    tabMenuItem.Tag = voMenu;

                    //add tabpage
                    tabMenus.TabPages.Add(tabMenuItem);

                    //create and format the label
                    var lblMenuName = new Label
                        {
                            Text = strMenuText,
                            Dock = DockStyle.Fill,
                            BackColor = objLblBackColor,
                            ForeColor = objLblForeColor,
                            Font = objLblFont,
                            TextAlign = ContentAlignment.MiddleLeft
                        };

                    //create and format tree
                    tvwSubMenus = new TreeView {Dock = DockStyle.Fill, ImageList = imglTreeNode};
                    tvwSubMenus.AfterSelect += treeNode_AfterSelect;
                    tvwSubMenus.DoubleClick += treeNode_DblClick;
                    tvwSubMenus.KeyDown += treeNode_KeyDown;
                    tvwSubMenus.BeforeExpand += tvwSubMenus_BeforeExpand;
                    tvwSubMenus.BeforeCollapse += tvwSubMenus_BeforeCollapse;
                    tvwSubMenus.MouseDown += tvwSubMenus_MouseDown;
                    tvwSubMenus.ContextMenu = mnuContext;
                    tvwSubMenus.Nodes.Clear();
                    tvwSubMenus.ItemHeight = ITEM_HEIGHT;
                    TreeNodeCollection colNodes = tvwSubMenus.Nodes;
                    BuildMenuTreeNode(ref colNodes, voMenu.Shortcut, lang.Name);
                    tvwSubMenus.ExpandAll();

                    //add label and tree
                    var pnlTree = new Panel {Dock = DockStyle.Fill};
                    pnlTree.DockPadding.All = DOCK_PADDING_SIZE;
                    pnlTree.DockPadding.Top = DOCK_PADDING_HALF_SIZE;
                    pnlTree.Controls.Add(tvwSubMenus);

                    var pnlLabel = new Panel {Dock = DockStyle.Top};
                    pnlLabel.DockPadding.All = DOCK_PADDING_SIZE;
                    pnlLabel.DockPadding.Bottom = DOCK_PADDING_HALF_SIZE;
                    pnlLabel.Height = LBL_Y_SIZE;
                    pnlLabel.Controls.Add(lblMenuName);

                    tabMenuItem.Controls.Add(pnlTree);
                    tabMenuItem.Controls.Add(pnlLabel);
                }
            }
            catch (PCSException ex)
            {
                PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
                try
                {
                    Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
                }
                catch
                {
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
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
                    PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void BuildMenuTreeNode(ref TreeNodeCollection objNodes, string pstrShortcut, string pstrLang)
        {
            const string MENU_SEPARATOR = "-";
            foreach (Sys_Menu_Entry voMenu in listmenu)
            {
                if ((voMenu.Text_CaptionDefault.Equals(MENU_SEPARATOR)) ||
                    (voMenu.Parent_Shortcut == null || !voMenu.Parent_Shortcut.Equals(pstrShortcut))) continue;
                string strShortcut = voMenu.Shortcut;
                string strMenuText = voMenu.Text_CaptionDefault;
                switch (pstrLang)
                {
                    case Constants.CULTURE_VN:
                        strMenuText = voMenu.Text_Caption_VI_VN;
                        break;
                    case Constants.CULTURE_JP:
                        strMenuText = voMenu.Text_Caption_JA_JP;
                        break;
                    default:
                        strMenuText = voMenu.Text_CaptionDefault;
                        break;
                }

                var objSubNode = new TreeNode();
                objNodes.Add(objSubNode);
                objSubNode.Text = strMenuText;
                objSubNode.Tag = voMenu;
                TreeNodeCollection colNodes = objSubNode.Nodes;
                BuildMenuTreeNode(ref colNodes, strShortcut, pstrLang);
                if (objSubNode.Nodes.Count > 0)
                {
                    int expan = voMenu.ExpandedImage ?? 0;
                    objSubNode.ImageIndex = expan - 1;
                    objSubNode.SelectedImageIndex = expan - 1;
                    if (objSubNode.ImageIndex < 0)
                    {
                        objSubNode.ImageIndex = IdxImageExpandedFolder;
                        objSubNode.SelectedImageIndex = IdxImageExpandedFolder;
                    }
                }
                else
                {
                    int expan = voMenu.ExpandedImage ?? 0;
                    objSubNode.ImageIndex = expan - 1;
                    objSubNode.SelectedImageIndex = expan - 1;
                    if (objSubNode.ImageIndex < 0)
                    {
                        objSubNode.ImageIndex = IdxImageForm;
                        objSubNode.SelectedImageIndex = IdxImageForm;
                    }
                }
            }
        }

        /// <summary>
        ///     exec menu entry
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 18-Sep-2005
        /// </history>
        private void ExecMenuEntry(Sys_Menu_Entry voMenu)
        {
            //check to found class to init
            if ((voMenu.FormLoad != string.Empty) && (voMenu.FormLoad.ToLower() != Nothing))
            {
                try
                {
                    LoadSelectedForm(voMenu);
                }
                catch
                {
                }
            }
            else
            {
                if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.ENG)))
                {
                    Thread.CurrentThread.CurrentUICulture = _englishCulture;
                    ChangeLanguage();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.VIE)))
                {
                    Thread.CurrentThread.CurrentUICulture = _vietnameseCulture;
                    ChangeLanguage();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.JAP)))
                {
                    Thread.CurrentThread.CurrentUICulture = _japaneseCulture;
                    ChangeLanguage();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.EXT))
                         || voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.EXIT)))
                {
                    DialogResult result = PCSMessageBox.Show(ErrorCode.MESSAGE_DO_YOU_REALLY_WANT_TO_EXIT,
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                             MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                        Application.Exit();
                }
                else if (voMenu.Shortcut.Equals(Convert.ToString(ShortcutEnum.OUT)))
                    Logout();
            }
        }

        /// <summary>
        ///     show info on statusbar
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 28-Sep-2005
        /// </history>
        private void ShowStatusBarInfo()
        {
            const string PNL_USERNAME = "spnlUsername";
            const string PNL_DBSERVER = "spnlDBServer";
            const string PNL_DBNAME = "spnlDBName";
            const string PNL_VERSION = "spnlVersion";
            const string STR_DATASOURCE = "Data Source";
            const string STR_INITIAL_CATALOG = "Initial Catalog";
            const string SERVER_DOT = ".";
            const string SERVER_LOCAL = "localhost";
            const string PCSMAIN = "PCSMain.Exe";

            string strConn = Utils.Instance.OleDbConnectionString;

            string strUsername = SystemProperty.UserName;
            string strDBServer = ParseConnectionString(strConn, STR_DATASOURCE);
            if (strDBServer.Equals(SERVER_DOT))
            {
                strDBServer = SERVER_LOCAL;
            }

            string strDBName = ParseConnectionString(strConn, STR_INITIAL_CATALOG);

            string strVersion = Utilities.Instance.GetDBVersion();

            DateTime dtmSourceCreate = File.GetLastWriteTime(SystemProperty.ExecutablePath + "\\" + PCSMAIN);

            sbarCmd.Panels[PNL_USERNAME].Text = strUsername;
            sbarCmd.Panels[PNL_DBSERVER].Text = strDBServer;
            sbarCmd.Panels[PNL_DBNAME].Text = strDBName;
            sbarCmd.Panels[PNL_VERSION].Text = string.Format("{0} (Build:{1})", strVersion,
                                                             dtmSourceCreate.ToString(Constants.DATETIME_FORMAT_HOUR));
        }

        /// <summary>
        ///     parse connection string to retrieve information
        /// </summary>
        /// <author>
        ///     DuongNA
        /// </author>
        /// <history>
        ///     Created : 28-Sep-2005
        /// </history>
        private static string ParseConnectionString(string strConn, string strAttName)
        {
            const string STR_NOTHING = "";
            const string STR_SEPARATOR = ";";
            const string STR_EQUAL = "=";

            int nBeginIndex = strConn.IndexOf(strAttName);
            if (nBeginIndex < 0)
            {
                return STR_NOTHING;
            }
            int nEndIndex = strConn.IndexOf(STR_SEPARATOR, nBeginIndex);
            if (nEndIndex < 0)
            {
                nEndIndex = strConn.Length;
            }
            string strPair = strConn.Substring(nBeginIndex, nEndIndex - nBeginIndex);
            int nEqualIndex = strPair.IndexOf(STR_EQUAL);
            if ((nEqualIndex < 0) || (nEqualIndex == strPair.Length - 1))
            {
                return STR_NOTHING;
            }
            return strPair.Substring(nEqualIndex + 1).Trim();
        }

        private void ChangeLanguage()
        {
            for (int i = 0; i < SystemProperty.ArrayForms.Count; i++)
            {
                var frmInfor = (FormInfo) SystemProperty.ArrayForms[i];
                if (frmInfor.mForm != null)
                    frmInfor.mForm.Close();
            }
            // reload main form to get new menu
            MainForm_Load(null, null);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof (MainForm));
            this.imglIcon = new System.Windows.Forms.ImageList(this.components);
            this.sbarCmd = new System.Windows.Forms.StatusBar();
            this.spnlCommand = new System.Windows.Forms.StatusBarPanel();
            this.spnlOther = new System.Windows.Forms.StatusBarPanel();
            this.spnlCCN = new System.Windows.Forms.StatusBarPanel();
            this.spnlUsername = new System.Windows.Forms.StatusBarPanel();
            this.spnlDBServer = new System.Windows.Forms.StatusBarPanel();
            this.spnlDBName = new System.Windows.Forms.StatusBarPanel();
            this.spnlVersion = new System.Windows.Forms.StatusBarPanel();
            this.lblCommand = new System.Windows.Forms.Label();
            this.lblMain = new System.Windows.Forms.Label();
            this.lblSelection = new System.Windows.Forms.Label();
            this.txtcmd = new System.Windows.Forms.TextBox();
            this.tabMenus = new C1.Win.C1Command.C1DockingTab();
            this.mnuContext = new System.Windows.Forms.ContextMenu();
            this.mnuChangeIcon = new System.Windows.Forms.MenuItem();
            this.imglTreeNode = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize) (this.spnlCommand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlOther)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlCCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlDBServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlDBName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tabMenus)).BeginInit();
            this.SuspendLayout();
            // 
            // imglIcon
            // 
            this.imglIcon.ImageStream =
                ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imglIcon.ImageStream")));
            this.imglIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imglIcon.Images.SetKeyName(0, "");
            // 
            // sbarCmd
            // 
            resources.ApplyResources(this.sbarCmd, "sbarCmd");
            this.sbarCmd.Name = "sbarCmd";
            this.sbarCmd.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[]
                {
                    this.spnlCommand,
                    this.spnlOther,
                    this.spnlCCN,
                    this.spnlUsername,
                    this.spnlDBServer,
                    this.spnlDBName,
                    this.spnlVersion
                });
            this.sbarCmd.ShowPanels = true;
            // 
            // spnlCommand
            // 
            resources.ApplyResources(this.spnlCommand, "spnlCommand");
            // 
            // spnlOther
            // 
            this.spnlOther.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            resources.ApplyResources(this.spnlOther, "spnlOther");
            // 
            // spnlCCN
            // 
            this.spnlCCN.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.spnlCCN, "spnlCCN");
            // 
            // spnlUsername
            // 
            this.spnlUsername.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.spnlUsername, "spnlUsername");
            // 
            // spnlDBServer
            // 
            this.spnlDBServer.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.spnlDBServer, "spnlDBServer");
            // 
            // spnlDBName
            // 
            this.spnlDBName.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.spnlDBName, "spnlDBName");
            // 
            // spnlVersion
            // 
            this.spnlVersion.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            resources.ApplyResources(this.spnlVersion, "spnlVersion");
            // 
            // lblCommand
            // 
            resources.ApplyResources(this.lblCommand, "lblCommand");
            this.lblCommand.Name = "lblCommand";
            // 
            // lblMain
            // 
            resources.ApplyResources(this.lblMain, "lblMain");
            this.lblMain.Name = "lblMain";
            // 
            // lblSelection
            // 
            resources.ApplyResources(this.lblSelection, "lblSelection");
            this.lblSelection.Name = "lblSelection";
            // 
            // txtcmd
            // 
            this.txtcmd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtcmd, "txtcmd");
            this.txtcmd.Name = "txtcmd";
            // 
            // tabMenus
            // 
            this.tabMenus.Alignment = System.Windows.Forms.TabAlignment.Left;
            resources.ApplyResources(this.tabMenus, "tabMenus");
            this.tabMenus.Name = "tabMenus";
            this.tabMenus.Padding = new System.Drawing.Point(20, 3);
            this.tabMenus.SplitterWidth = 20;
            this.tabMenus.TabsSpacing = 0;
            this.tabMenus.TextDirection = C1.Win.C1Command.TabTextDirectionEnum.Horizontal;
            this.tabMenus.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            this.tabMenus.SelectedIndexChanged += new System.EventHandler(this.tabMenus_SelectedIndexChanged);
            this.tabMenus.Click += new System.EventHandler(this.tabMenus_Click);
            // 
            // mnuContext
            // 
            this.mnuContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
                {
                    this.mnuChangeIcon
                });
            // 
            // mnuChangeIcon
            // 
            this.mnuChangeIcon.Index = 0;
            resources.ApplyResources(this.mnuChangeIcon, "mnuChangeIcon");
            this.mnuChangeIcon.Click += new System.EventHandler(this.mnuChangeIcon_Click);
            // 
            // imglTreeNode
            // 
            this.imglTreeNode.ImageStream =
                ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imglTreeNode.ImageStream")));
            this.imglTreeNode.TransparentColor = System.Drawing.Color.Transparent;
            this.imglTreeNode.Images.SetKeyName(0, "");
            this.imglTreeNode.Images.SetKeyName(1, "");
            this.imglTreeNode.Images.SetKeyName(2, "");
            this.imglTreeNode.Images.SetKeyName(3, "");
            this.imglTreeNode.Images.SetKeyName(4, "");
            this.imglTreeNode.Images.SetKeyName(5, "");
            this.imglTreeNode.Images.SetKeyName(6, "");
            this.imglTreeNode.Images.SetKeyName(7, "");
            this.imglTreeNode.Images.SetKeyName(8, "");
            this.imglTreeNode.Images.SetKeyName(9, "");
            this.imglTreeNode.Images.SetKeyName(10, "");
            this.imglTreeNode.Images.SetKeyName(11, "");
            this.imglTreeNode.Images.SetKeyName(12, "");
            this.imglTreeNode.Images.SetKeyName(13, "");
            this.imglTreeNode.Images.SetKeyName(14, "");
            this.imglTreeNode.Images.SetKeyName(15, "");
            this.imglTreeNode.Images.SetKeyName(16, "");
            this.imglTreeNode.Images.SetKeyName(17, "");
            this.imglTreeNode.Images.SetKeyName(18, "");
            this.imglTreeNode.Images.SetKeyName(19, "");
            this.imglTreeNode.Images.SetKeyName(20, "");
            this.imglTreeNode.Images.SetKeyName(21, "");
            this.imglTreeNode.Images.SetKeyName(22, "");
            this.imglTreeNode.Images.SetKeyName(23, "");
            this.imglTreeNode.Images.SetKeyName(24, "");
            this.imglTreeNode.Images.SetKeyName(25, "");
            this.imglTreeNode.Images.SetKeyName(26, "");
            this.imglTreeNode.Images.SetKeyName(27, "");
            this.imglTreeNode.Images.SetKeyName(28, "");
            this.imglTreeNode.Images.SetKeyName(29, "");
            this.imglTreeNode.Images.SetKeyName(30, "");
            this.imglTreeNode.Images.SetKeyName(31, "");
            this.imglTreeNode.Images.SetKeyName(32, "");
            this.imglTreeNode.Images.SetKeyName(33, "");
            this.imglTreeNode.Images.SetKeyName(34, "");
            this.imglTreeNode.Images.SetKeyName(35, "");
            this.imglTreeNode.Images.SetKeyName(36, "");
            this.imglTreeNode.Images.SetKeyName(37, "");
            this.imglTreeNode.Images.SetKeyName(38, "");
            this.imglTreeNode.Images.SetKeyName(39, "");
            this.imglTreeNode.Images.SetKeyName(40, "");
            this.imglTreeNode.Images.SetKeyName(41, "");
            this.imglTreeNode.Images.SetKeyName(42, "");
            this.imglTreeNode.Images.SetKeyName(43, "");
            this.imglTreeNode.Images.SetKeyName(44, "");
            this.imglTreeNode.Images.SetKeyName(45, "");
            this.imglTreeNode.Images.SetKeyName(46, "");
            this.imglTreeNode.Images.SetKeyName(47, "");
            this.imglTreeNode.Images.SetKeyName(48, "");
            this.imglTreeNode.Images.SetKeyName(49, "");
            this.imglTreeNode.Images.SetKeyName(50, "");
            this.imglTreeNode.Images.SetKeyName(51, "");
            this.imglTreeNode.Images.SetKeyName(52, "");
            this.imglTreeNode.Images.SetKeyName(53, "");
            this.imglTreeNode.Images.SetKeyName(54, "");
            this.imglTreeNode.Images.SetKeyName(55, "");
            this.imglTreeNode.Images.SetKeyName(56, "");
            this.imglTreeNode.Images.SetKeyName(57, "");
            this.imglTreeNode.Images.SetKeyName(58, "");
            this.imglTreeNode.Images.SetKeyName(59, "");
            this.imglTreeNode.Images.SetKeyName(60, "");
            this.imglTreeNode.Images.SetKeyName(61, "");
            this.imglTreeNode.Images.SetKeyName(62, "");
            this.imglTreeNode.Images.SetKeyName(63, "");
            this.imglTreeNode.Images.SetKeyName(64, "");
            this.imglTreeNode.Images.SetKeyName(65, "");
            this.imglTreeNode.Images.SetKeyName(66, "");
            this.imglTreeNode.Images.SetKeyName(67, "");
            this.imglTreeNode.Images.SetKeyName(68, "");
            this.imglTreeNode.Images.SetKeyName(69, "");
            this.imglTreeNode.Images.SetKeyName(70, "");
            this.imglTreeNode.Images.SetKeyName(71, "");
            this.imglTreeNode.Images.SetKeyName(72, "");
            this.imglTreeNode.Images.SetKeyName(73, "");
            this.imglTreeNode.Images.SetKeyName(74, "");
            this.imglTreeNode.Images.SetKeyName(75, "");
            this.imglTreeNode.Images.SetKeyName(76, "");
            this.imglTreeNode.Images.SetKeyName(77, "");
            this.imglTreeNode.Images.SetKeyName(78, "");
            this.imglTreeNode.Images.SetKeyName(79, "");
            this.imglTreeNode.Images.SetKeyName(80, "");
            this.imglTreeNode.Images.SetKeyName(81, "");
            this.imglTreeNode.Images.SetKeyName(82, "");
            this.imglTreeNode.Images.SetKeyName(83, "");
            this.imglTreeNode.Images.SetKeyName(84, "");
            this.imglTreeNode.Images.SetKeyName(85, "");
            this.imglTreeNode.Images.SetKeyName(86, "");
            this.imglTreeNode.Images.SetKeyName(87, "");
            this.imglTreeNode.Images.SetKeyName(88, "");
            this.imglTreeNode.Images.SetKeyName(89, "");
            this.imglTreeNode.Images.SetKeyName(90, "");
            this.imglTreeNode.Images.SetKeyName(91, "");
            this.imglTreeNode.Images.SetKeyName(92, "");
            this.imglTreeNode.Images.SetKeyName(93, "");
            this.imglTreeNode.Images.SetKeyName(94, "");
            this.imglTreeNode.Images.SetKeyName(95, "");
            this.imglTreeNode.Images.SetKeyName(96, "");
            this.imglTreeNode.Images.SetKeyName(97, "");
            this.imglTreeNode.Images.SetKeyName(98, "");
            this.imglTreeNode.Images.SetKeyName(99, "");
            this.imglTreeNode.Images.SetKeyName(100, "");
            this.imglTreeNode.Images.SetKeyName(101, "");
            this.imglTreeNode.Images.SetKeyName(102, "");
            this.imglTreeNode.Images.SetKeyName(103, "");
            this.imglTreeNode.Images.SetKeyName(104, "");
            this.imglTreeNode.Images.SetKeyName(105, "");
            this.imglTreeNode.Images.SetKeyName(106, "");
            this.imglTreeNode.Images.SetKeyName(107, "");
            this.imglTreeNode.Images.SetKeyName(108, "");
            this.imglTreeNode.Images.SetKeyName(109, "");
            this.imglTreeNode.Images.SetKeyName(110, "");
            this.imglTreeNode.Images.SetKeyName(111, "");
            this.imglTreeNode.Images.SetKeyName(112, "");
            this.imglTreeNode.Images.SetKeyName(113, "");
            this.imglTreeNode.Images.SetKeyName(114, "");
            this.imglTreeNode.Images.SetKeyName(115, "");
            this.imglTreeNode.Images.SetKeyName(116, "");
            this.imglTreeNode.Images.SetKeyName(117, "");
            this.imglTreeNode.Images.SetKeyName(118, "");
            this.imglTreeNode.Images.SetKeyName(119, "");
            this.imglTreeNode.Images.SetKeyName(120, "");
            this.imglTreeNode.Images.SetKeyName(121, "");
            this.imglTreeNode.Images.SetKeyName(122, "");
            this.imglTreeNode.Images.SetKeyName(123, "");
            this.imglTreeNode.Images.SetKeyName(124, "");
            this.imglTreeNode.Images.SetKeyName(125, "");
            this.imglTreeNode.Images.SetKeyName(126, "");
            this.imglTreeNode.Images.SetKeyName(127, "");
            this.imglTreeNode.Images.SetKeyName(128, "");
            this.imglTreeNode.Images.SetKeyName(129, "");
            this.imglTreeNode.Images.SetKeyName(130, "");
            this.imglTreeNode.Images.SetKeyName(131, "");
            this.imglTreeNode.Images.SetKeyName(132, "");
            this.imglTreeNode.Images.SetKeyName(133, "");
            this.imglTreeNode.Images.SetKeyName(134, "");
            this.imglTreeNode.Images.SetKeyName(135, "");
            this.imglTreeNode.Images.SetKeyName(136, "");
            this.imglTreeNode.Images.SetKeyName(137, "");
            this.imglTreeNode.Images.SetKeyName(138, "");
            this.imglTreeNode.Images.SetKeyName(139, "");
            this.imglTreeNode.Images.SetKeyName(140, "");
            this.imglTreeNode.Images.SetKeyName(141, "");
            this.imglTreeNode.Images.SetKeyName(142, "");
            this.imglTreeNode.Images.SetKeyName(143, "");
            this.imglTreeNode.Images.SetKeyName(144, "");
            this.imglTreeNode.Images.SetKeyName(145, "");
            this.imglTreeNode.Images.SetKeyName(146, "");
            this.imglTreeNode.Images.SetKeyName(147, "");
            this.imglTreeNode.Images.SetKeyName(148, "");
            this.imglTreeNode.Images.SetKeyName(149, "");
            this.imglTreeNode.Images.SetKeyName(150, "");
            this.imglTreeNode.Images.SetKeyName(151, "");
            this.imglTreeNode.Images.SetKeyName(152, "");
            this.imglTreeNode.Images.SetKeyName(153, "");
            this.imglTreeNode.Images.SetKeyName(154, "");
            this.imglTreeNode.Images.SetKeyName(155, "");
            this.imglTreeNode.Images.SetKeyName(156, "");
            this.imglTreeNode.Images.SetKeyName(157, "");
            this.imglTreeNode.Images.SetKeyName(158, "");
            this.imglTreeNode.Images.SetKeyName(159, "");
            this.imglTreeNode.Images.SetKeyName(160, "");
            this.imglTreeNode.Images.SetKeyName(161, "");
            this.imglTreeNode.Images.SetKeyName(162, "");
            this.imglTreeNode.Images.SetKeyName(163, "1284266860_industry.ico");
            this.imglTreeNode.Images.SetKeyName(164, "1284266908_deliverables.ico");
            this.imglTreeNode.Images.SetKeyName(165, "1284266860_industry.ico");
            this.imglTreeNode.Images.SetKeyName(166, "1284266882_industry.ico");
            this.imglTreeNode.Images.SetKeyName(167, "1284266899_desktop_computer.ico");
            this.imglTreeNode.Images.SetKeyName(168, "1284266908_deliverables.ico");
            this.imglTreeNode.Images.SetKeyName(169, "1284266917_shopping_cart.ico");
            this.imglTreeNode.Images.SetKeyName(170, "1284267060_dedicated_server.ico");
            this.imglTreeNode.Images.SetKeyName(171, "1284267069_receptionist.ico");
            this.imglTreeNode.Images.SetKeyName(172, "1284267094_rigid_dump_truck.ico");
            this.imglTreeNode.Images.SetKeyName(173, "1284267104_software_development.ico");
            this.imglTreeNode.Images.SetKeyName(174, "1284267112_local_network.ico");
            this.imglTreeNode.Images.SetKeyName(175, "1284267119_electricity.ico");
            this.imglTreeNode.Images.SetKeyName(176, "1284267134_industry.ico");
            this.imglTreeNode.Images.SetKeyName(177, "1284267179_schedule_scan.ico");
            this.imglTreeNode.Images.SetKeyName(178, "1284267202_calendar_year.ico");
            this.imglTreeNode.Images.SetKeyName(179, "1284267209_briefcase.ico");
            this.imglTreeNode.Images.SetKeyName(180, "1284267224_data_transport.ico");
            this.imglTreeNode.Images.SetKeyName(181, "1284267251_air_compressor.ico");
            this.imglTreeNode.Images.SetKeyName(182, "1284267258_aerial_platform.ico");
            this.imglTreeNode.Images.SetKeyName(183, "1284267299_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(184, "1284267306_truck.ico");
            this.imglTreeNode.Images.SetKeyName(185, "1284267313_hijack.ico");
            this.imglTreeNode.Images.SetKeyName(186, "1284267319_demographic.ico");
            this.imglTreeNode.Images.SetKeyName(187, "1284267328_objects.ico");
            this.imglTreeNode.Images.SetKeyName(188, "1284267359_insource.ico");
            this.imglTreeNode.Images.SetKeyName(189, "1284267369_primitives.ico");
            this.imglTreeNode.Images.SetKeyName(190, "1284267377_autoresponse.ico");
            this.imglTreeNode.Images.SetKeyName(191, "1284267505_truck.ico");
            this.imglTreeNode.Images.SetKeyName(192, "1284267614_money_bag.ico");
            this.imglTreeNode.Images.SetKeyName(193, "1284267621_money.ico");
            this.imglTreeNode.Images.SetKeyName(194, "1284267635_company.ico");
            this.imglTreeNode.Images.SetKeyName(195, "1284267801_vault.ico");
            this.imglTreeNode.Images.SetKeyName(196, "1284267809_interact.ico");
            this.imglTreeNode.Images.SetKeyName(197, "1284267817_people.ico");
            this.imglTreeNode.Images.SetKeyName(198, "1284267829_school.ico");
            this.imglTreeNode.Images.SetKeyName(199, "1284267850_05_phonebook.ico");
            this.imglTreeNode.Images.SetKeyName(200, "1284267858_02_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(201, "1284267864_06_calculator.ico");
            this.imglTreeNode.Images.SetKeyName(202, "1284267873_Gear.ico");
            this.imglTreeNode.Images.SetKeyName(203, "1284267987_Delivery.ico");
            this.imglTreeNode.Images.SetKeyName(204, "1284267994_Home.ico");
            this.imglTreeNode.Images.SetKeyName(205, "1284268028_Money_Bag.ico");
            this.imglTreeNode.Images.SetKeyName(206, "1284268040_Calendar.ico");
            this.imglTreeNode.Images.SetKeyName(207, "1284268257_destroyer.ico");
            this.imglTreeNode.Images.SetKeyName(208, "1284268327_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(209, "1284268347_Run.ico");
            this.imglTreeNode.Images.SetKeyName(210, "1284268356_Search.ico");
            this.imglTreeNode.Images.SetKeyName(211, "1284268398_Settings.ico");
            this.imglTreeNode.Images.SetKeyName(212, "1284268429_packing.ico");
            this.imglTreeNode.Images.SetKeyName(213, "1284268436_Add-Male-User.ico");
            this.imglTreeNode.Images.SetKeyName(214, "1284268446_Remove-Male-User.ico");
            this.imglTreeNode.Images.SetKeyName(215, "1284268452_Accept-Male-User.ico");
            this.imglTreeNode.Images.SetKeyName(216, "1284268498_Process-Accept.ico");
            this.imglTreeNode.Images.SetKeyName(217, "1284268527_binary-tree.ico");
            this.imglTreeNode.Images.SetKeyName(218, "1284268630_earning-statements.ico");
            this.imglTreeNode.Images.SetKeyName(219, "1284268638_event.ico");
            this.imglTreeNode.Images.SetKeyName(220, "1284268645_autoship.ico");
            this.imglTreeNode.Images.SetKeyName(221, "1284268664_contact.ico");
            this.imglTreeNode.Images.SetKeyName(222, "1284268743_pie_chart.ico");
            this.imglTreeNode.Images.SetKeyName(223, "1284268754_data_transport.ico");
            this.imglTreeNode.Images.SetKeyName(224, "1284268833_taxes.ico");
            this.imglTreeNode.Images.SetKeyName(225, "1284268842_our_process_2.ico");
            this.imglTreeNode.Images.SetKeyName(226, "1284268855_pie_chart.ico");
            this.imglTreeNode.Images.SetKeyName(227, "1284268886_support_belt.ico");
            this.imglTreeNode.Images.SetKeyName(228, "1284269035_button_blue_repeat.ico");
            this.imglTreeNode.Images.SetKeyName(229, "1284269234_gnome-mime-application-vnd.ms-powerpoint.ico");
            this.imglTreeNode.Images.SetKeyName(230, "1284269274_emblem-system.ico");
            this.imglTreeNode.Images.SetKeyName(231, "1284269281_ark.ico");
            this.imglTreeNode.Images.SetKeyName(232, "1284269471_United-States-Flag.ico");
            this.imglTreeNode.Images.SetKeyName(233, "1284269693_BuildingManagement.ico");
            this.imglTreeNode.Images.SetKeyName(234, "1284269755_exit.ico");
            this.imglTreeNode.Images.SetKeyName(235, "1284269789_Japan.ico");
            this.imglTreeNode.Images.SetKeyName(236, "1284269810_klipper.ico");
            this.imglTreeNode.Images.SetKeyName(237, "1284269910_cabinet.ico");
            this.imglTreeNode.Images.SetKeyName(238, "1284269919_cashbox.ico");
            this.imglTreeNode.Images.SetKeyName(239, "1284269982_gear.ico");
            this.imglTreeNode.Images.SetKeyName(240, "1284270042_content-reorder.ico");
            this.imglTreeNode.Images.SetKeyName(241, "1284270075_paste.ico");
            this.imglTreeNode.Images.SetKeyName(242, "1284270081_catalog.ico");
            this.imglTreeNode.Images.SetKeyName(243, "1284270091_options.ico");
            this.imglTreeNode.Images.SetKeyName(244, "1284270122_organization.ico");
            this.imglTreeNode.Images.SetKeyName(245, "1284270198_Delivery.ico");
            this.imglTreeNode.Images.SetKeyName(246, "1284270208_Business.ico");
            this.imglTreeNode.Images.SetKeyName(247, "1284270216_Time.ico");
            this.imglTreeNode.Images.SetKeyName(248, "1284270325_gear_wheel.ico");
            this.imglTreeNode.Images.SetKeyName(249, "1284270332_magnifying_glass.ico");
            this.imglTreeNode.Images.SetKeyName(250, "1284270338_lock.ico");
            this.imglTreeNode.Images.SetKeyName(251, "1284270361_library.ico");
            this.imglTreeNode.Images.SetKeyName(252, "1284270441_distributor-report.ico");
            this.imglTreeNode.Images.SetKeyName(253, "1284270455_Package-Download.ico");
            this.imglTreeNode.Images.SetKeyName(254, "1284270462_reports.ico");
            this.imglTreeNode.Images.SetKeyName(255, "1284270489_delivery.ico");
            this.imglTreeNode.Images.SetKeyName(256, "1284270589_industry.ico");
            this.imglTreeNode.Images.SetKeyName(257, "1284270598_local_network.ico");
            this.imglTreeNode.Images.SetKeyName(258, "1284270628_cabinet.ico");
            this.imglTreeNode.Images.SetKeyName(259, "1284270652_objects.ico");
            this.imglTreeNode.Images.SetKeyName(260, "1284270666_agency.ico");
            this.imglTreeNode.Images.SetKeyName(261, "1284270673_data_management.ico");
            this.imglTreeNode.Images.SetKeyName(262, "1284270681_open_safety_box.ico");
            this.imglTreeNode.Images.SetKeyName(263, "1284270688_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(264, "1284270759_truck.ico");
            this.imglTreeNode.Images.SetKeyName(265, "1284270778_merge_cells.ico");
            this.imglTreeNode.Images.SetKeyName(266, "1284270831_tablet.ico");
            this.imglTreeNode.Images.SetKeyName(267, "1284271010_line_chart.ico");
            this.imglTreeNode.Images.SetKeyName(268, "1284271020_pie_chart.ico");
            this.imglTreeNode.Images.SetKeyName(269, "1284271035_stock_task.ico");
            this.imglTreeNode.Images.SetKeyName(270, "1284271100_kontact_date.ico");
            this.imglTreeNode.Images.SetKeyName(271, "1284271142_preferences-contact-list.ico");
            this.imglTreeNode.Images.SetKeyName(272, "1284271242_file-roller.ico");
            this.imglTreeNode.Images.SetKeyName(273, "1284271290_stock_task-assigned-to.ico");
            this.imglTreeNode.Images.SetKeyName(274, "1284271368_gnome-settings-default-applications.ico");
            this.imglTreeNode.Images.SetKeyName(275, "1284271546_Todo.ico");
            this.imglTreeNode.Images.SetKeyName(276, "1284272015_config-date.ico");
            this.imglTreeNode.Images.SetKeyName(277, "1284272042_gnome-mime-application-vnd.sun.xml.calc.template.ico");
            this.imglTreeNode.Images.SetKeyName(278, "1284272081_gnome-help.ico");
            this.imglTreeNode.Images.SetKeyName(279, "1284272158_system-switch-user.ico");
            this.imglTreeNode.Images.SetKeyName(280, "1284272175_system-config-boot.ico");
            this.imglTreeNode.Images.SetKeyName(281, "1284272209_start-here-kubuntu.ico");
            this.imglTreeNode.Images.SetKeyName(282, "1284272217_gtk-dialog-warning.ico");
            this.imglTreeNode.Images.SetKeyName(283, "1284272359_gtk-save-as.ico");
            this.imglTreeNode.Images.SetKeyName(284, "1284272400_stock_task-recurring.ico");
            this.imglTreeNode.Images.SetKeyName(285, "1284272455_evolution-tasks.ico");
            this.imglTreeNode.Images.SetKeyName(286, "1284272485_preferences-certificates.ico");
            this.imglTreeNode.Images.SetKeyName(287, "1284272568_gtk-edit.ico");
            this.imglTreeNode.Images.SetKeyName(288, "1284272607_crack-attack.ico");
            this.imglTreeNode.Images.SetKeyName(289, "1284272624_gtk-execute.ico");
            this.imglTreeNode.Images.SetKeyName(290, "1284272845_kde-folder.ico");
            this.imglTreeNode.Images.SetKeyName(291, "1284272875_system-shutdown.ico");
            this.imglTreeNode.Images.SetKeyName(292, "1284272976_emblem-system.ico");
            this.imglTreeNode.Images.SetKeyName(293, "1284273003_filesave.ico");
            this.imglTreeNode.Images.SetKeyName(294, "1284273052_xfce-edit.ico");
            this.imglTreeNode.Images.SetKeyName(295, "1284273491_error.ico");
            this.imglTreeNode.Images.SetKeyName(296, "Check16.png");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabMenus);
            this.Controls.Add(this.txtcmd);
            this.Controls.Add(this.sbarCmd);
            this.Controls.Add(this.lblSelection);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.lblCommand);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize) (this.spnlCommand)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlOther)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlCCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlDBServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlDBName)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.spnlVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tabMenus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        #region Nested type: ShortcutEnum

        /// <summary>
        ///     Short cut menu menu
        /// </summary>
        private enum ShortcutEnum
        {
            /// <summary>
            ///     Exit application
            /// </summary>
            EXT,

            /// <summary>
            ///     Exit application
            /// </summary>
            EXIT,

            /// <summary>
            ///     Log out
            /// </summary>
            OUT,

            /// <summary>
            ///     English language
            /// </summary>
            ENG,

            /// <summary>
            ///     Vietnamese language
            /// </summary>
            VIE,

            /// <summary>
            ///     Japanese language
            /// </summary>
            JAP
        }

        #endregion
    }
}
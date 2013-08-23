using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using PCSComUtils.Admin.BO;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.DataContext;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using Syncfusion.Windows.Forms.Tools;

namespace PCSUtils.Admin
{
    public partial class RightAssignment : Form
    {
        private const string This = "PCSUtils.Admin.RightAssignment";

        private List<Sys_Role> _roleList;
        private List<Sys_Right> _rightList;
        private List<Sys_Menu_Entry> _menuList;
        private int _selectedRoleId;

        public RightAssignment()
        {
            InitializeComponent();
        }

        #region private methods

        private void UpdateRight(int roleId, List<Sys_Right> roleRights, TreeNodeAdv parentNode)
        {
            var children = parentNode == null ? FunctionListTree.Nodes : parentNode.Nodes;
            foreach (TreeNodeAdv node in children)
            {
                var menuEntry = (Sys_Menu_Entry)node.Tag;
                var right = roleRights.FirstOrDefault(r => r.Menu_EntryID == menuEntry.Menu_EntryID && r.RoleID == roleId);
                // current menu entry did not exist in the right records and the node is not check, continue
                if (right == null && !node.Checked)
                {
                    continue;
                }

                int permission;
                switch (node.CheckState)
                {
                    case CheckState.Checked:
                        permission = (int) MenuPermission.All;
                        break;
                    case CheckState.Unchecked:
                        permission = (int)MenuPermission.None;
                        break;
                    default:
                        permission = (int)MenuPermission.View;
                        break;
                }
                if (right != null)
                {
                    right.Permission = permission;
                }
                else
                {
                    right = new Sys_Right { Menu_EntryID = menuEntry.Menu_EntryID, Permission = permission, RoleID = roleId };
                    roleRights.Add(right);
                }
                if (node.GetNodeCount(true) > 0)
                {
                    UpdateRight(roleId, roleRights, node);
                }
            }
        }

        /// <summary>
        ///     Bind the role list to the flex grid
        /// </summary>
        /// <param name="roleList"></param>
        private void BindRoleList(List<Sys_Role> roleList)
        {
            RoleList.DataSource = roleList;
            foreach (Column col in RoleList.Cols)
            {
                // only show the role name and description field
                if (col.Name == Sys_RoleTable.NAME_FLD || col.Name == Sys_RoleTable.DESCRIPTION_FLD)
                {
                    continue;
                }
                col.Visible = false;
            }
        }

        /// <summary>
        ///     Bind the menu list to the tree view
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="shortcut"></param>
        /// <param name="parentNode"></param>
        private void BindMenuList(IEnumerable<Sys_Menu_Entry> menuList, string shortcut, TreeNodeAdv parentNode)
        {
            var children = menuList.Where(m => m.Parent_Shortcut.Equals(shortcut, StringComparison.OrdinalIgnoreCase));
            foreach (var menuEntry in children)
            {
                var node = new TreeNodeAdv(menuEntry.Text_CaptionDefault) { Tag = menuEntry };
                if (parentNode != null)
                {
                    parentNode.Nodes.Add(node);
                }
                else
                {
                    FunctionListTree.Nodes.Add(node);
                }
                if (menuList.Any(m => m.Parent_Shortcut.Equals(menuEntry.Shortcut, StringComparison.OrdinalIgnoreCase)))
                {
                    node.InteractiveCheckBox = true;
                }
                else
                {
                    // hide plus-minus button for child
                    node.ShowPlusMinus = false;
                    node.Optioned = true;
                }
            }
        }

        /// <summary>
        ///     Mark the function list checked with the right
        /// </summary>
        /// <param name="rightList"></param>
        /// <param name="parentNode"></param>
        private void BindRightList(IEnumerable<Sys_Right> rightList, TreeNodeAdv parentNode)
        {
            var children = parentNode == null ? FunctionListTree.Nodes : parentNode.Nodes;
            foreach (TreeNodeAdv child in children)
            {
                // if current node is in the right list, check the permission to check or not
                var menuEntry = (Sys_Menu_Entry)child.Tag;
                var right = rightList.FirstOrDefault(r => r.Menu_EntryID == menuEntry.Menu_EntryID);
                if (right != null)
                {
                    int permission = right.Permission.GetValueOrDefault(0);
                    switch (permission)
                    {
                        case (int)MenuPermission.None:
                            child.CheckState = CheckState.Unchecked;
                            break;
                        case (int)MenuPermission.All:
                            child.CheckState = CheckState.Checked;
                            break;
                        case (int)MenuPermission.View:
                            child.CheckState = CheckState.Indeterminate;
                            break;
                    }
                }
            }
        }

        #endregion

        private void RightAssignment_Load(object sender, EventArgs e)
        {
            const string methodName = This + ".RightAssignment_Load()";
            try
            {
                #region Security

                //Set authorization for user
                var objSecurity = new Security();
                Name = This;
                if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
                {
                    Close();
                    // You don't have the right to view this item
                    PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
                    // Code Inserted Automatically

                    #region Code Inserted Automatically

                    Cursor = Cursors.Default;

                    #endregion Code Inserted Automatically

                    return;
                }

                #endregion

                _roleList = Utilities.Instance.GetRoles();
                if (_roleList.Count == 0)
                {
                    return;
                }

                _rightList = Utilities.Instance.GetRights();
                _menuList = Utilities.Instance.GetMenus();
                BindMenuList(_menuList, "MAIN", null);
                FunctionListTree.CollapseAll();

                // bind role list to the grid
                BindRoleList(_roleList);
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

        private void RoleList_AfterRowColChange(object sender, RangeEventArgs e)
        {
            const string methodName = This + ".RoleList_AfterRowColChange()";
            try
            {
                if (RoleList.Cols.IndexOf(Sys_RoleTable.ROLEID_FLD) < 0)
                {
                    return;
                }

                // Get RoleID
                int intRowSel = RoleList.RowSel;
                // if user change selected row
                if (intRowSel <= 0 || e.OldRange.r1 == e.NewRange.r1)
                {
                    return;
                }
                // Get data display on column 1 of Role
                int roleId;
                if (int.TryParse(RoleList.GetDataDisplay(RoleList.RowSel, Sys_RoleTable.ROLEID_FLD), out roleId))
                {
                    _selectedRoleId = roleId;
                    BindRightList(_rightList.Where(r => r.RoleID == roleId), null);
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

        private void SaveButton_Click(object sender, EventArgs e)
        {
            const string methodName = This + ".SaveButton_Click()";
            try
            {
                int roleId;
                if (!int.TryParse(RoleList.GetDataDisplay(RoleList.RowSel, Sys_RoleTable.ROLEID_FLD), out roleId))
                {
                    return;
                }
                var roleRights = _rightList.Where(r => r.RoleID == roleId).ToList();
                // update the right from the function list
                UpdateRight(roleId, roleRights, null);

                // save changes to database
                RightAssignmentBO.Instance.UpdateRight(roleRights);                
                PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK);
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RightAssignment_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string methodName = This + ".RightAssignment_FormClosing()";
            try
            {
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

        private void FunctionListTree_BeforeExpand(object sender, TreeViewAdvCancelableNodeEventArgs e)
        {
            const string methodName = This + ".FunctionListTree_BeforeExpand()";
            try
            {
                // load children before expand
                var menu = (Sys_Menu_Entry)e.Node.Tag;
                // bind menu for current node
                BindMenuList(_menuList, menu.Shortcut, e.Node);
                // bind the right
                BindRightList(_rightList.Where(r => r.RoleID == _selectedRoleId), e.Node);
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
}

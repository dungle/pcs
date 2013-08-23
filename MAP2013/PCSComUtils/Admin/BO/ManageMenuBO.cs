using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

using PCSComUtils.PCSExc;

namespace PCSComUtils.Admin.BO
{
    /// <summary>
    /// Biz Object of ManageMenu form
    /// </summary>
    public class ManageMenuBO
    {
        private const string THIS = "PCSComUtils.Admin.BO.ManageMenuBO";

        #region ManageMenuBO Members

        /// <summary>
        /// Insert a new record into database
        /// </summary>
        public void Add(object pObjectDetail)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// We need to check this menu is already have transaction or not.
        /// If have transaction, unable to delete.
        /// If have no transaction, delete it
        /// </summary>
        public void Delete(int menuId)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);

                    var menu = dcPCS.Sys_Menu_Entries.SingleOrDefault(m => m.Menu_EntryID == menuId);
                    var objRights = dcPCS.Sys_Rights.SingleOrDefault(m => m.Menu_EntryID == menuId);

                    var dsMenuEntry = new Sys_Menu_EntryDS();
                    // we need to check this menu is already have transaction or not
                    int intNumOfTrans = 0;
                    if (menu.TableName != null)
                    {
                        if (menu.TableName.Trim() != string.Empty && menu.TransNoFieldName.Trim() != string.Empty)
                            intNumOfTrans = dsMenuEntry.GetAllTransactions(menu);
                    }
                    // if have transaction, unable to delete
                    if (intNumOfTrans > 0)
                        throw new PCSBOException(ErrorCode.MESSAGE_COULD_NOT_DELETE_MENU, METHOD_NAME, new Exception());
                    // update button caption
                    var subMenus = dcPCS.Sys_Menu_Entries.Where(
                        m => m.Parent_Shortcut == menu.Shortcut && m.Button_Caption > menu.Button_Caption).OrderBy(
                        m => m.Button_Caption).ToList();
                    subMenus.ForEach(m => m.Button_Caption = m.Button_Caption - 1);

                    // if have no transction, delete it
                    if (objRights != null)
                    {
                        dcPCS.Sys_Rights.DeleteOnSubmit(objRights);
                    }
                    dcPCS.Sys_Menu_Entries.DeleteOnSubmit(menu);

                    // submit changes
                    dcPCS.SubmitChanges();
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// Get the object information by ID of VO class
        /// </summary>
        public Sys_Menu_Entry GetMenu(int menuId)
        {
            PCSDataContext dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.Sys_Menu_Entries.SingleOrDefault(m => m.Menu_EntryID == menuId);
        }

        /// <summary>
        /// Get all right
        /// </summary>
        /// <returns></returns>
        public List<Sys_Right> GetRightAll()
        {
            List<Sys_Right> list = new List<Sys_Right>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var l = from obj in db.Sys_Rights
                            orderby obj.RoleID
                            select obj;
                    if (l != null) list = l.ToList<Sys_Right>();
                }
            }
            return list;
        }

        /// <summary>
        /// Get all Role
        /// </summary>
        /// <returns></returns>
        public List<Sys_Role> GetRoleAll()
        {
            List<Sys_Role> list = new List<Sys_Role>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var l = from obj in db.Sys_Roles
                            where obj.Name != Constants.ALL_ROLE
                            orderby obj.RoleID
                            select obj;
                    if (l != null) list = l.ToList<Sys_Role>();
                }
            }
            return list;
        }
        /// <summary>
        /// Get All menu
        /// </summary>
        /// <returns></returns>
        public List<Sys_Menu_Entry> GetMenuAll()
        {
            List<Sys_Menu_Entry> list = new List<Sys_Menu_Entry>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var l = from obj in db.Sys_Menu_Entries
                            where (obj.Text_CaptionDefault != "-" && (obj.Type == (int)MenuTypeEnum.VisibleBoth || obj.Type == (int)MenuTypeEnum.InvisibleMenu))
                            orderby obj.Text_CaptionDefault
                            select obj;
                    if (l != null) list = l.ToList<Sys_Menu_Entry>();
                }
            }
            return list;
        }
        //cuonglv
        public List<Sys_Menu_Entry> GetMenuAllWithImageFields()
        {
            List<Sys_Menu_Entry> list = new List<Sys_Menu_Entry>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    var l = from obj in db.Sys_Menu_Entries
                            orderby obj.Parent_Shortcut, obj.Button_Caption
                            select obj;
                    if (l != null) list = l.ToList<Sys_Menu_Entry>();
                }
            }
            return list;
        }
        /// <summary>
        ///     Return the DataSet (list of record) by inputing the FieldList and Condition
        /// </summary>
        public void UpdateDataSet(DataSet dstData)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dsMenuEntry = new Sys_Menu_EntryDS();
                    dsMenuEntry.UpdateDataSet(dstData);
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// Update data table
        /// </summary>
        public void UpdateDataTable(DataTable pdtbData)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dsMenuEntry = new Sys_Menu_EntryDS();
                    dsMenuEntry.UpdateDataTable(pdtbData);
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// Update into Database
        /// </summary>
        public void Update(Sys_Menu_Entry menu, int roleId)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    PCSDataContext dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
                    var current = dataContext.Sys_Menu_Entries.SingleOrDefault(m => m.Menu_EntryID == menu.Menu_EntryID);
                    if (current != null)
                    {
                        current.Button_Caption = menu.Button_Caption;
                        current.CollapsedImage = menu.CollapsedImage;
                        current.Description = menu.Description;
                        current.ExpandedImage = menu.ExpandedImage;
                        current.FormLoad = menu.FormLoad;
                        current.IsTransaction = menu.IsTransaction;
                        current.IsUserCreated = menu.IsUserCreated;
                        current.Parent_Child = menu.Parent_Child;
                        current.Parent_Shortcut = menu.Parent_Shortcut;
                        current.Prefix = menu.Prefix;
                        current.ReportID = menu.ReportID;
                        current.Shortcut = menu.Shortcut;
                        current.TableName = menu.TableName;
                        current.Text_Caption_EN_US = menu.Text_Caption_EN_US;
                        current.Text_Caption_JA_JP = menu.Text_Caption_JA_JP;
                        current.Text_Caption_VI_VN = menu.Text_Caption_VI_VN;
                        current.Text_Caption_Language_Default = menu.Text_Caption_Language_Default;
                        current.Text_CaptionDefault = menu.Text_CaptionDefault;
                        current.TransFormat = menu.TransFormat;
                        current.TransNoFieldName = menu.TransNoFieldName;
                        current.Type = menu.Type;
                    }
                    else
                    {
                        dataContext.Sys_Menu_Entries.InsertOnSubmit(menu);
                        // create right for new menu
                        var right = new Sys_Right
                        {
                            Menu_EntryID = menu.Menu_EntryID,
                            Permission = 1,
                            RoleID = roleId
                        };
                        dataContext.Sys_Rights.InsertOnSubmit(right);
                    }

                    dataContext.SubmitChanges();
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// Add new menu to database and return id
        /// </summary>
        /// <param name="menu">SysMenuEntry object</param>
        /// <param name="roleId">Role ID</param>
        /// <returns>New ID</returns>
        public int AddAndReturnID(Sys_Menu_Entry menu, int roleId)
        {
            const string METHOD_NAME = THIS + ".AddAndReturnID()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    PCSDataContext dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
                    dataContext.Sys_Menu_Entries.InsertOnSubmit(menu);

                    // update button caption
                    var subMenus = dataContext.Sys_Menu_Entries.Where(
                        m => m.Parent_Shortcut == menu.Parent_Shortcut && m.Button_Caption > menu.Button_Caption).OrderBy(
                        m => m.Button_Caption).ToList();
                    subMenus.ForEach(m => m.Button_Caption = m.Button_Caption + 1);

                    // save new menu
                    menu.Button_Caption += 1;
                    // submit changes
                    dataContext.SubmitChanges();

                    // create right for new menu
                    var right = new Sys_Right
                    {
                        Menu_EntryID = menu.Menu_EntryID,
                        Permission = 1,
                        RoleID = roleId
                    };
                    dataContext.Sys_Rights.InsertOnSubmit(right);

                    dataContext.SubmitChanges();
                    trans.Complete();
                    return menu.Menu_EntryID;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// Get menu by its short cut
        /// </summary>
        /// <param name="pstrShortCut">Shortcut of menu</param>
        /// <returns>SysMenu</returns>
        public object GetMenuByShortCut(string pstrShortCut)
        {
            var dsMenuEntry = new Sys_Menu_EntryDS();
            return dsMenuEntry.GetMenuByShortCut(pstrShortCut);
        }
        public void UpdateTrans(Sys_Menu_Entry menu, int roleId)
        {
            const string METHOD_NAME = THIS + ".Delete()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    PCSDataContext dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
                    var current = dataContext.Sys_Menu_Entries.SingleOrDefault(m => m.Menu_EntryID == menu.Menu_EntryID);
                    if (current != null)
                    {
                       
                        current.Text_Caption_EN_US = menu.Text_Caption_EN_US;
                        current.Text_Caption_JA_JP = menu.Text_Caption_JA_JP;
                        current.Text_Caption_VI_VN = menu.Text_Caption_VI_VN;
                        current.Text_CaptionDefault = menu.Text_CaptionDefault;
                        current.Prefix = menu.Prefix;
                        current.TransFormat = menu.TransFormat;
                      
                    }
                    else
                    {
                        dataContext.Sys_Menu_Entries.InsertOnSubmit(menu);
                        // create right for new menu
                        var right = new Sys_Right
                        {
                            Menu_EntryID = menu.Menu_EntryID,
                            Permission = 1,
                            RoleID = roleId
                        };
                        dataContext.Sys_Rights.InsertOnSubmit(right);
                    }

                    dataContext.SubmitChanges();
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }
        #endregion
    }
}
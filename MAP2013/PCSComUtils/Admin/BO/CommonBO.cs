using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Transactions;
using PCSComUtils.Admin.DS;
using PCSComUtils.Common;
using PCSComUtils.Common.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using System.Collections.Generic;

namespace PCSComUtils.Admin.BO
{
    /// <summary>
    /// CommonBO object: An interface for many common methods.
    /// </summary>
    public class CommonBO
    {
        private const string THIS = "PCSComUtils.Admin.BO.CommonBO";

        /// <summary>
        /// This method checks business rule and call Add() method of DS class 
        /// </summary>
        public void AddSysRight(Sys_Right right)
        {
            const string METHOD_NAME = THIS + ".Add()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    dcPCS.Sys_Rights.InsertOnSubmit(right);

                    // submit changes
                    dcPCS.SubmitChanges();
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// This method checks business rule and call Delete() method of DS class 
        /// </summary>
        public void DeleteRole(int roleId)
        {
            const string METHOD_NAME = THIS + ".Detele()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    dcPCS.Sys_Roles.DeleteOnSubmit(dcPCS.Sys_Roles.SingleOrDefault(e => e.RoleID == roleId));
                    // submit changes
                    dcPCS.SubmitChanges();
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.CASCADE_DELETE_PREVENT)
                        throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// This method uses to get SysRoleVO object by ID
        /// </summary>
        public Sys_Role GetRole(int roleId)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.Sys_Roles.SingleOrDefault(r => r.RoleID == roleId);
        }

        /// <summary>
        /// This method uses to get CCNVO object by UserName
        /// </summary>
        public MST_CCN GetCCN(string userName)
        {
            using (var trans = new TransactionScope())
            {
                using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    IQueryable<MST_CCN> query = from ccn in dataContext.MST_CCNs
                                                join user in dataContext.Sys_Users on ccn.CCNID equals user.CCNID
                                                where user.UserName == userName
                                                select ccn;
                    trans.Complete();
                    return query.SingleOrDefault();
                }
            }
        }

        /// <summary>
        /// This method uses to update data in SysRole table
        /// </summary>
        public void Update(Sys_Role role)
        {
            const string METHOD_NAME = THIS + ".Update()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    Sys_Role current = dcPCS.Sys_Roles.SingleOrDefault(c => c.RoleID == role.RoleID);

                    if (current != null)
                    {
                        current.Name = role.Name;
                        current.Description = role.Description;
                    }

                    // submit changes
                    dcPCS.SubmitChanges();
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// This method uses to get all data in SysRole table
        /// </summary>
        /// <returns></returns>
        public DataSet List()
        {
            var dsSysRole = new Sys_RoleDS();
            return dsSysRole.List();
        }

        /// <summary>
        /// This method uses to update a DataSet of SysRole
        /// </summary>
        /// <param name="pData">SysRole Data</param>
        public void UpdateDataSet(DataSet pData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSet()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dsSysRole = new Sys_RoleDS();
                    dsSysRole.UpdateDataSet(pData);
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// This method uses to update a DataSet of SysRight
        /// </summary>
        /// <param name="pdstData"></param>
        public void UpdateRight(DataSet pdstData)
        {
            const string METHOD_NAME = THIS + ".UpdateDataSet()";
            try
            {
                var dsSysRight = new Sys_RightDS();
                dsSysRight.UpdateDataSet(pdstData);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// Get all protection of menu entry
        /// </summary>
        /// <returns>Menu entry dataset</returns>
        public DataSet GetAllProtection()
        {
            var dsSysMenuEntry = new Sys_Menu_EntryDS();
            DataSet dstData = dsSysMenuEntry.GetAllProtection();
            return dstData;
        }

        /// <summary>
        /// This method uses to update data in SysRight table
        /// </summary>
        public void UpdateSysRight(Sys_Right right)
        {
            const string METHOD_NAME = THIS + ".UpdateSysRight()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    Sys_Right current = dcPCS.Sys_Rights.SingleOrDefault(r => r.RightID == right.RightID);

                    if (current != null)
                    {
                        current.Menu_EntryID = right.Menu_EntryID;
                        current.Permission = right.Permission;
                        current.RoleID = right.RoleID;
                        // submit changes
                        dcPCS.SubmitChanges();
                    }
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }


        /// <summary>
        /// Update password of a specified user
        /// </summary>
        /// <param name="userName">Name of user who will be changed password</param>
        /// <param name="password">New password</param>
        public void UpdateNewPassword(string userName, string password)
        {
            const string METHOD_NAME = THIS + ".UpdateNewPassword()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    Sys_User current = dcPCS.Sys_Users.SingleOrDefault(u => u.UserName == userName);

                    if (current != null)
                    {
                        current.Pwd = CryptoUtil.EncryptPassword(password);
                        // submit changes
                        dcPCS.SubmitChanges();
                    }
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        /// <summary>
        /// GetFKColumnName
        /// </summary>
        /// <param name="primaryTable"></param>
        /// <param name="foreignTable"></param>
        /// <returns>FK Column Name else return string.Emty</returns>
        public string GetFKColumnName(string primaryTable, string foreignTable)
        {
            string strColumnName = string.Empty;
            const string FKTABLE_NAME = "FKTABLE_NAME";
            const string FKCOLUMN_NAME = "FKCOLUMN_NAME";
            var dsRoleParty = new sys_RolePartyDS();
            DataTable dtbFKTable = dsRoleParty.GetFKColumnName(primaryTable);
            foreach (DataRow drow in dtbFKTable.Rows)
            {
                if (drow[FKTABLE_NAME].ToString() == foreignTable)
                {
                    strColumnName = drow[FKCOLUMN_NAME].ToString();
                }
            }
            return strColumnName;
        }

        /// <summary>
        /// GetPKColumnName
        /// </summary>
        /// <param name="primaryTable"></param>
        /// <returns>Primary Key column or string.Emty if table has not configed</returns>
        public string GetPKColumnName(string primaryTable)
        {
            string strColumnName = string.Empty;
            const string PKCOLUMN_NAME = "COLUMN_NAME";
            var dsRoleParty = new sys_RolePartyDS();
            DataTable dtbFKTable = dsRoleParty.GetPKColumnName(primaryTable);
            if (dtbFKTable.Rows.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                strColumnName = dtbFKTable.Rows[0][PKCOLUMN_NAME].ToString();
                return strColumnName;
            }
        }

        /// <summary>
        /// Return all roles for the user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataSet ListRoleForUser(string userName)
        {
            var objSysUserDS = new Sys_UserDS();
            return objSysUserDS.ListRoleForUser(userName);
        }

        /// <summary>
        /// Get Role for a specify user and some system properties for this. 
        /// And get all system params
        /// </summary>
        /// <param name="pintUserID"></param>
        /// <param name="pstrUserName"></param>
        /// <param name="pintCCNID"></param>
        /// <returns></returns>
        /// <author>Trada</author>
        /// <date>Wednesday, December 14 2005</date>
        public DataSet ListRoleAndSysParam(int pintUserID, string pstrUserName)
        {
            var objSysRoleDs = new Sys_RoleDS();
            return objSysRoleDs.ListRoleAndSysParam(pintUserID, pstrUserName);
        }


        public DataSet ListParameter(int pintCCNID)
        {
            var objParamDS = new Sys_ParamDS();
            return objParamDS.ListParameter(pintCCNID);
        }

        /// <summary>
        /// Get all menu items of current culture.
        /// </summary>
        /// <param name="pobjCulture">Current culture info </param>
        /// <returns></returns>
        /// <author> Tuan DM - Jan 14, 2005</author>
        public ArrayList GetAllMenus(CultureInfo pobjCulture)
        {
            var dsSys_Menu_Entry = new Sys_Menu_EntryDS();
            return dsSys_Menu_Entry.GetAllMenus(pobjCulture);
        }

        /// <summary>
        /// Get all menu items in current culture of a specified user
        /// </summary>
        /// <param name="pobjCulture">Current culture</param>
        /// <param name="pstrUserName">Current user</param>
        /// <returns></returns>
        /// <author> Tuan DM - Jan 14, 2005</author>
        public ArrayList GetAllMenus(CultureInfo pobjCulture, string pstrUserName)
        {
            var dsSys_Menu_Entry = new Sys_Menu_EntryDS();
            return dsSys_Menu_Entry.GetAllMenus(pobjCulture, pstrUserName);
        }

        /// <summary>
        /// Get hidden control of form
        /// </summary>
        /// <param name="pstrFormName">Form name</param>
        /// <param name="pstrControlName">Control name</param>
        /// <param name="pstrSubControl">Sub-control name</param>
        /// <param name="pstrRoleIDs">ID of role</param>
        /// <returns>True/False</returns>
        /// <author> Tuan DM - Jan 14, 2005</author>
        public bool GetHiddenControl(string pstrFormName, string pstrControlName, string pstrSubControl,
                                     string[] pstrRoleIDs)
        {
            var dsHidden = new Sys_HiddenControlsDS();
            return dsHidden.GetHiddenControl(pstrFormName, pstrControlName, pstrSubControl, pstrRoleIDs);
        }

        /// <summary>
        /// Get visible control
        /// </summary>
        /// <param name="pstrFormName">Form name</param>
        /// <param name="pstrRoleIDs">Array of checking roles</param>
        /// <returns>DataTable</returns>
        /// <author> Son HT - Jan 14, 2005</author>
        public DataTable GetVisibleControl(string pstrFormName, string[] pstrRoleIDs)
        {
            var dsHidden = new Sys_VisibleControlDS();
            return dsHidden.GetVisibleControl(pstrFormName, string.Empty, string.Empty, pstrRoleIDs);
        }

        /// <summary>
        /// Get home currency code
        /// </summary>
        /// <param name="pintID">ID of home currency</param>
        /// <returns>Code of currency</returns>
        /// <author> Tuan DM - Jan 14, 2005</author>
        public string GetHomeCurrency(int pintID)
        {
            var dsCur = new MST_CurrencyDS();
            return ((MST_CurrencyVO)dsCur.GetObjectVO(pintID)).Code;
        }


        public DataSet GetSecurityInfo(string pstrUserName, string pstrFormName)
        {
            var dsRole = new Sys_RoleDS();
            return dsRole.GetSecurityInfo(pstrUserName, pstrFormName);
        }

        #region To add icon for menu entries
        public Sys_UserVO CheckAuthenticate(object pobjObjecVO)
        {
            Sys_UserDS dsSysUser = new Sys_UserDS();
            Sys_UserVO objObjec = (Sys_UserVO)pobjObjecVO;
            objObjec.Pwd = CryptoUtil.EncryptPassword(objObjec.Pwd);
            return dsSysUser.CheckAuthenticate(objObjec);
        }
        /// <summary>
        /// Get all menu items with image fields by specific user
        /// </summary>
        /// <param name="pobjCulture">Current culture</param>
        /// <param name="pstrUserName">User name</param>
        /// <returns>ArrayList object</returns>
        /// <author> Duong NA - Sep 23, 2005</author>
        public List<Sys_Menu_Entry> GetAllMenusWithImageFields(CultureInfo pobjCulture, string pstrUserName)
        {
            var dsSys_Menu_Entry = new Sys_Menu_EntryDS();
            return dsSys_Menu_Entry.GetAllMenusWithImageFields(pobjCulture, pstrUserName);
        }

        /// <summary>
        /// Get all menu items with image fields
        /// </summary>
        /// <param name="pobjCulture">Current culture</param>		
        /// <returns>ArrayList object</returns>
        /// <author> Duong NA - Sep 23, 2005</author>
        public ArrayList GetAllMenusWithImageFields(CultureInfo pobjCulture)
        {
            var dsSys_Menu_Entry = new Sys_Menu_EntryDS();
            return dsSys_Menu_Entry.GetAllMenusWithImageFields(pobjCulture);
        }

        /// <summary>
        /// Get all menu items with image fields
        /// </summary>
        /// <param name="pobjObjectVO">Sys_Menu_EntryVO object</param>		
        /// <returns>ArrayList object</returns>
        /// <author> Duong NA - Sep 23, 2005</author>
        public void UpdateMenuWithImageFields(int menuId, int? collapsedImage, int? expandedImage)
        {
            const string METHOD_NAME = THIS + ".UpdateMenuWithImageFields()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    var sql = string.Format("UPDATE Sys_Menu_Entry SET CollapsedImage={0}, ExpandedImage={1} WHERE Menu_EntryID={2}", collapsedImage, expandedImage, menuId);
                    dcPCS.ExecuteCommand(sql);
                    trans.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 1)
                {
                    if (ex.Number == ErrorCode.SQLDUPLICATE_KEYCODE)
                        throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
                    throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                }
                throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
            }
        }

        #endregion
    }
}
//PCS namespaces
using System;
using System.Data;
using System.Data.Linq;
using System.Transactions;
using PCSComUtils.Admin.DS;

using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;
using System.Linq;
using PCSComUtils.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PCSComUtils.Admin.BO
{
    /// <summary>
    /// Summary description for ManageUserBO.
    /// </summary>
    public class ManageUserBO
    {
        /// <summary>
        /// Add a new user into database
        /// </summary>
        /// <param name="pObjectDetail"></param>
        /// <Author> Thien HD, Jan-07-2005</Author>
        public void Add(object pObjectDetail)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                var objSys_UserDS = new Sys_UserDS();
                objSys_UserDS.Add(pObjectDetail);
                trans.Complete();
            }
        }

        /// <summary>
        /// Update the whole data set into database
        /// </summary>
        /// <param name="dstData">Dataset of users</param>
        /// <Author> Thien HD, Jan-07-2005</Author>
        public void UpdateDataSet(DataSet dstData)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                //Init the DS object
                var objSysUserDs = new Sys_UserDS();
                objSysUserDs.UpdateDataSet(dstData);
            }
        }

        /// <summary>
        /// Update a record into database
        /// </summary>
        /// <param name="pObjectDetail"></param>
        /// <Author> Thien HD, Jan-07-2005</Author>
        /*public void Update(object pObjectDetail)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                var objSys_UserDS = new Sys_UserDS();
                objSys_UserDS.UpdateUserInfo(pObjectDetail);
                trans.Complete();
            }
        }*/
        public bool UserExist(string strUserName, string Id)
        {
            bool ret = false;
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    try
                    {
                        var listuser = new List<Sys_User>();
                        if (!string.IsNullOrEmpty(Id))
                        {
                            int iId = int.Parse(Id);
                            var list = from obj in db.Sys_Users
                                       where obj.UserName == strUserName && obj.UserID != iId
                                       select obj;
                            if (list == null)
                                ret = false;
                            else
                            {
                                listuser = list.ToList<Sys_User>();
                                if (listuser.Count > 0)
                                    ret = true;
                                else ret = false;
                            }
                        }
                        else
                        {
                            var list = from obj in db.Sys_Users
                                       where obj.UserName == strUserName
                                       select obj;
                            if (list == null)
                                ret = false;
                            else
                            {
                                listuser = list.ToList<Sys_User>();
                                if (listuser.Count > 0)
                                    ret = true;
                                else ret = false;
                            }
                        }
                    }
                    catch
                    {
                        trans.Complete();
                        ret = true;
                    }
                }
                trans.Complete();
            } return ret;

        }
        //Khanh Edit Update
        public void Update(object pObjectDetail)
        {
            const string METHOD_NAME = "" + ".Update()";
            var objMaster = (Sys_UserVO)pObjectDetail;
            try
            {
                using (var trans = new TransactionScope())
                {
                    var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString);
                    // update master object first
                    var objMatchedMaster =
                        dcPCS.Sys_Users.SingleOrDefault(
                            e => e.UserID == objMaster.UserID);
                    if (objMatchedMaster != null)
                    {

                        objMatchedMaster.UserName = objMaster.UserName;
                        objMatchedMaster.Pwd = objMaster.Pwd;
                        objMatchedMaster.Name = objMaster.Name;
                        objMatchedMaster.Description = objMaster.Description;
                        objMatchedMaster.EmployeeID = objMaster.EmployeeID;
                        objMatchedMaster.MasterLocationID = objMaster.MasterLocationID;
                        objMatchedMaster.Activate = objMaster.Activate;
                        objMatchedMaster.ExpiredDate = objMaster.ExpiredDate;
                    }

                    dcPCS.SubmitChanges();


                    trans.Complete();
                    //  trans.Complete();
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
        /// Return all data from User data. This method will call the List method of DS class
        /// </summary>
        /// <returns></returns>
        /// <Author> Thien HD, Jan-07-2005</Author>
        public DataSet List()
        {
            var objSysUserDS = new Sys_UserDS();
            return objSysUserDS.List();
        }
        public List<Sys_User> GetAll()
        {
            List<Sys_User> ret = new List<Sys_User>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    ret = db.GetTable<Sys_User>().ToList<Sys_User>();
                }
                trans.Complete();
            }
            return ret;
        }

        public List<MST_Employee> GetAllEmploy()
        {
            List<MST_Employee> ret = new List<MST_Employee>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    ret = db.GetTable<MST_Employee>().ToList<MST_Employee>();
                }
                trans.Complete();
            }
            return ret;
        }
        public List<MST_MasterLocation> GetAllMasterLocation()
        {
            List<MST_MasterLocation> ret = new List<MST_MasterLocation>();
            using (var trans = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    ret = db.GetTable<MST_MasterLocation>().ToList<MST_MasterLocation>();
                }
                trans.Complete();
            }
            return ret;
        }
        //public IQueryable GetAll()
        //{
        //    IQueryable ret = null;
        //    using (var trans = new TransactionScope())
        //    {
        //        using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
        //        {
        //            var result = from user in db.Sys_Users
        //                         join employ in db.MST_Employees on user.EmployeeID equals employ.EmployeeID
        //                         join masterlocation in db.MST_MasterLocations on user.MasterLocationID equals masterlocation.MasterLocationID
        //                        // where user.UserName != Constants.SUPER_ADMIN_USER
        //                         select new
        //                         {
        //                             user.UserID,
        //                             user.UserName,
        //                             user.Pwd,
        //                             employ.Name,
        //                             user.CreatedDate,
        //                             user.Description,
        //                             user.EmployeeID,
        //                             user.MasterLocationID,
        //                             masterlocation.Code,
        //                             user.Activate,
        //                             user.ExpiredDate
        //                         };
        //            ret = result;
        //        }
        //        trans.Complete();
        //    }
        //    return ret;

        //}

        //public List<Sys_User> GetAll()
        //{
        //    List<Sys_User> ret = new List<Sys_User>();
        //    using (var trans = new TransactionScope())
        //    {
        //        using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
        //        {
        //            ret = db.GetTable<Sys_User>().ToList<Sys_User>();
        //        }
        //        trans.Complete();
        //    }
        //    return ret;
        //}
        /// <summary>
        /// Add a new user into database and return its new ID
        /// </summary>
        /// <param name="pObjectDetail"></param>
        /// <returns></returns>
        /// <Author> Thien HD, Jan-07-2005</Author>
        /*public int AddNewUserAndReturnNewID(object pObjectDetail)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                var objSys_UserDS = new Sys_UserDS();
                int newId = objSys_UserDS.AddNewUserAndReturnNewID(pObjectDetail);
                trans.Complete();
                return newId;
            }
        }*/
        public int AddNewUserAndReturnNewID(Sys_User objUser)
        {

            const string METHOD_NAME = "" + ".Add()";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        if (objUser.UserID > 0)
                        {
                            var objUse = dcPCS.Sys_Users.SingleOrDefault<Sys_User>(e => e.UserID == objUser.UserID);
                            if (objUse != null)
                            {
                                objUse.UserID = objUser.UserID;
                                objUse.UserName = objUser.UserName;
                                //objUse.Super = objUser.Super;
                                objUse.Pwd = objUser.Pwd;
                                objUse.Name = objUser.Name;
                                objUse.MasterLocationID = objUser.MasterLocationID;
                                //objUse.LastLogoutTime = objUser.LastLogoutTime;
                                //objUse.LastLoginTime = objUser.LastLoginTime;
                                objUse.ExpiredDate = objUser.ExpiredDate;
                                objUse.EmployeeID = objUser.EmployeeID;
                                objUse.Description = objUser.Description;
                                objUse.CreatedDate = objUser.CreatedDate;
                                objUse.Activate = objUser.Activate;
                            }
                        }
                        else
                        {
                            // insert master object first
                            dcPCS.Sys_Users.InsertOnSubmit(objUser);
                        }
                        dcPCS.SubmitChanges();
                        trans.Complete();
                    }
                    return objUser.UserID;
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

        public void DeleteUser(int pintID)
        {
            const string METHOD_NAME = "" + ".Delete()";

            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var dcPCS = new PCSDataContext(Utils.Instance.ConnectionString))
                    {
                        try
                        {
                            dcPCS.Sys_Users.DeleteOnSubmit(dcPCS.Sys_Users.SingleOrDefault(e => e.UserID == pintID));
                            // submit changes
                            dcPCS.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
                        }
                    }
                    // complete transaction
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
        /// Get list of CCN
        /// </summary>
        /// <returns></returns>
        /// <Author> Thien HD, Jan-07-2005</Author>
        public DataSet ListCCN()
        {
            var objCCNDS = new MST_CCNDS();
            return objCCNDS.List();
        }
        public List<MST_CCN> GetList()
        {
            List<MST_CCN> list = new List<MST_CCN>();
            using (var tran = new TransactionScope())
            {
                using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    list = db.GetTable<MST_CCN>().ToList<MST_CCN>();
                }
                tran.Complete();
            }
            return list;
        }

        /// <summary>
        /// Get the system date from Database Server
        /// </summary>
        /// <returns></returns>
        /// <Author> Thien HD, Jan-07-2005</Author>
        public string GetSystemDate()
        {
            var objSysUserDS = new Sys_UserDS();
            return objSysUserDS.GetSystemDate();
        }

        /// <summary>
        /// Get current date from Database server
        /// </summary>
        /// <returns>Database server date</returns>
        /// <Author> Thien HD, Jan-07-2005</Author>
        public DateTime GetDatabaseDate()
        {
            var objSysUserDS = new Sys_UserDS();
            return objSysUserDS.GetDatabaseDate();
        }
    }
}
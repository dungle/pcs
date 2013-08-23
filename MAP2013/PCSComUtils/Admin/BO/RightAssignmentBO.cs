using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCSComUtils.Common.BO;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComUtils.Admin.BO
{
    public class RightAssignmentBO
    {
        private static readonly object SyncRoot = new object();
        private static RightAssignmentBO _instance;

        private RightAssignmentBO()
        {
        }

        /// <summary>
        /// Gets the instance of RightAssignmentBO
        /// </summary>
        /// <value>The instance.</value>
        public static RightAssignmentBO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new RightAssignmentBO();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Updates the right.
        /// </summary>
        /// <param name="rightList">The right list.</param>
        public void UpdateRight(List<Sys_Right> rightList)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                foreach (var sysRight in rightList)
                {
                    var roleId = sysRight.RoleID;
                    var menuEntryId = sysRight.Menu_EntryID;
                    var right = dataContext.Sys_Rights.FirstOrDefault(r => r.RoleID == roleId && r.Menu_EntryID == menuEntryId);
                    if (right != null)
                    {
                        right.Permission = sysRight.Permission;
                    }
                    else
                    {
                        right = new Sys_Right
                                    {
                                        Menu_EntryID = sysRight.Menu_EntryID,
                                        Permission = sysRight.Permission,
                                        RoleID = sysRight.RoleID
                                    };
                        dataContext.Sys_Rights.InsertOnSubmit(right);
                    }
                }

                dataContext.SubmitChanges();
            }
        }
    }
}

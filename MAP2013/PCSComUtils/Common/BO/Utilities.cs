using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using log4net;
using PCSComUtils.Common.DS;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComUtils.Common.BO
{
    public class Utilities
    {
        private static readonly object SyncRoot = new object();
        private static Utilities _instance;
        private ILog _logger = LogManager.GetLogger(typeof (Utilities));

        /// <summary>
        /// Generic setter delegate
        /// </summary>
        public delegate void GenericSetter(object target, object value);
        /// <summary>
        /// Generic getter delegate
        /// </summary>
        public delegate object GenericGetter(object target);

        private Utilities()
        {
        }

        /// <summary>
        /// Gets the instance of Utilities
        /// </summary>
        /// <value>The instance.</value>
        public static Utilities Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Utilities();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates a dynamic setter for the property
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns></returns>
        public GenericSetter CreateSetMethod(PropertyInfo propertyInfo)
        {
            // If there's no setter return null
            MethodInfo setMethod = propertyInfo.GetSetMethod();
            if (setMethod == null)
                return null;

            // Create the dynamic method
            var arguments = new Type[2];
            arguments[0] = arguments[1] = typeof(object);

            var setter = new DynamicMethod(
                String.Concat("_Set", propertyInfo.Name, "_"),
                typeof(void), arguments, propertyInfo.DeclaringType);
            ILGenerator generator = setter.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            generator.Emit(OpCodes.Ldarg_1);

            generator.Emit(propertyInfo.PropertyType.IsClass ? OpCodes.Castclass : OpCodes.Unbox_Any,
                           propertyInfo.PropertyType);

            generator.EmitCall(OpCodes.Callvirt, setMethod, null);
            generator.Emit(OpCodes.Ret);

            // Create the delegate and return it
            return (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
        }

        /// <summary>
        /// Creates a dynamic getter for the property
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns></returns>
        public GenericGetter CreateGetMethod(PropertyInfo propertyInfo)
        {
            // If there's no getter return null
            MethodInfo getMethod = propertyInfo.GetGetMethod();
            if (getMethod == null)
                return null;

            // Create the dynamic method
            var arguments = new Type[1];
            arguments[0] = typeof(object);

            var getter = new DynamicMethod(
                String.Concat("_Get", propertyInfo.Name, "_"),
                typeof(object), arguments, propertyInfo.DeclaringType);
            ILGenerator generator = getter.GetILGenerator();
            generator.DeclareLocal(typeof(object));
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            generator.EmitCall(OpCodes.Callvirt, getMethod, null);

            if (!propertyInfo.PropertyType.IsClass)
                generator.Emit(OpCodes.Box, propertyInfo.PropertyType);

            generator.Emit(OpCodes.Ret);

            // Create the delegate and return it
            return (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
        }

        /// <summary>
        /// Lists the CCN.
        /// </summary>
        /// <returns></returns>
        public DataTable ListCCN()
        {
            using(var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return CollectionHelper.ConvertTo(dataContext.MST_CCNs.ToList());
            }
        }

        /// <summary>
        /// Gets the customer info.
        /// </summary>
        /// <param name="pintPartyId">The pint party id.</param>
        /// <returns></returns>
        public MST_Party GetCustomerInfo(int pintPartyId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.MST_Parties.SingleOrDefault(p => p.PartyID == pintPartyId);
            }
        }

        /// <summary>
        /// Gets the master location.
        /// </summary>
        /// <param name="masterLocationId">The master location id.</param>
        /// <returns></returns>
        public MST_MasterLocation GetMasterLocation(int masterLocationId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.MST_MasterLocations.SingleOrDefault(m => m.MasterLocationID == masterLocationId);
            }
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns></returns>
        public MST_Location GetLocation(int locationId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.MST_Locations.SingleOrDefault(m => m.LocationID == locationId);
            }
        }

        /// <summary>
        /// Gets the rate.
        /// </summary>
        /// <param name="inId">The in id.</param>
        /// <param name="outId">The out id.</param>
        /// <returns></returns>
        public decimal GetRate(int inId, int outId)
        {
            if (inId == outId)
                return 1;

            using (var dataContext =new PCSDataContext(Utils.Instance.ConnectionString))
            {
                var rate = dataContext.MST_UMRates.SingleOrDefault(u => u.UnitOfMeasureInID == inId && u.UnitOfMeasureOutID == outId);
                if (rate == null)
                {
                    rate = dataContext.MST_UMRates.SingleOrDefault(u => u.UnitOfMeasureInID == outId && u.UnitOfMeasureOutID == inId);
                    return rate.Scale == null ? 0 : 1 / rate.Scale.GetValueOrDefault(1);
                }
                return rate.Scale.GetValueOrDefault(0);
            }
        }

        /// <summary>
        /// Gets the product info.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public ITM_Product GetProductInfo(int productId)
        {
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return dataContext.ITM_Products.SingleOrDefault(p => p.ProductID == productId);
            }
        }

        /// <summary>
        /// Gets the Server date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerDate()
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return db.GetServerDate();
            }
        }

        /// <summary>
        /// Cleans the temp table.
        /// </summary>
        public void CleanTempTable()
        {
            try
            {
                using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
                {
                    // get list of table need to be clean up by current user
                    var strSql = new StringBuilder();
                    var listCmd = string.Format(
                            "SELECT name FROM dbo.sysobjects WHERE xtype = 'U' AND name LIKE 'PRO_IssueMaterialDetail{0}%' AND crdate >= '2010-01-01'",
                            SystemProperty.UserName);
                    var tableNames = dataContext.ExecuteQuery<string>(listCmd);
                    foreach (var tableName in tableNames)
                    {
                        strSql.AppendLine(string.Format("DROP TABLE {0};", tableName));
                    }
                    if (strSql.Length > 0)
                    {
                        dataContext.ExecuteCommand(strSql.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Info(string.Format("Could not delete temp table: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Gets the DB version.
        /// </summary>
        /// <returns></returns>
        public string GetDBVersion()
        {
            var dsSysParam = new Sys_ParamDS();
            return dsSysParam.GetNameValue(SystemParam.DB_VERSION);
        }

        /// <summary>
        /// Gets the multi work order number.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns></returns>
        public string GetMultiWorkOrderNumber(DateTime postDate)
        {
            const string format = "###";
            using (var dataContext = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                string transNo = string.Format("{0}-MWC-{1}", SystemProperty.UserName, postDate.ToString("yyMMdd"));
                var maxValue = dataContext.PRO_WorkOrderCompletions.Where(w => w.PostDate.Date.Equals(postDate.Date) && w.UserName == SystemProperty.UserName).Max(w => w.MultiCompletionNo);
                if (string.IsNullOrEmpty(maxValue))
                {
                    maxValue = "1".PadLeft(format.Length, '0');
                    transNo = string.Format("{0}{1}", transNo, maxValue);
                    return transNo;
                }
                int max;
                try
                {
                    max = Convert.ToInt32(maxValue.Substring(transNo.Length)) + 1;
                }
                catch (Exception)
                {
                    max = 1;
                }
                transNo = string.Format("{0}{1}", transNo, max.ToString().PadLeft(format.Length, '0'));
                return transNo;
            }
        }

        /// <summary>
        ///     Gets curent active working period
        /// </summary>
        /// <returns></returns>
        public Sys_Period GetWorkingPeriod()
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                return db.Sys_Periods.FirstOrDefault(s => s.Activate);
            }
        }

        /// <summary>
        ///     Gets system paramter value by name
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public string GetParamValue(string paramName)
        {
            using (var db = new PCSDataContext(Utils.Instance.ConnectionString))
            {
                var param = db.Sys_Params.FirstOrDefault(p => p.Name == paramName);
                return param == null ? string.Empty : param.Value;
            }
        }

        /// <summary>
        ///     Gets list of menu which visible both or invisible menu
        /// </summary>
        /// <returns></returns>
        public List<Sys_Menu_Entry> GetMenus()
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
            var list = db.Sys_Menu_Entries.Where(m => m.Text_CaptionDefault != "-"
                                                      && (m.Type == (int) MenuTypeEnum.VisibleBoth ||
                                                       m.Type == (int) MenuTypeEnum.InvisibleMenu)).OrderBy(
                                                           m => m.Text_CaptionDefault);
            return list.ToList();
        }

        /// <summary>
        ///     Lists all rights of system
        /// </summary>
        /// <returns></returns>
        public List<Sys_Right> GetRights()
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
            var list = db.Sys_Rights.OrderBy(r => r.RoleID);
            return list.ToList();
        }

        /// <summary>
        ///     Lists all roles of system
        /// </summary>
        /// <returns></returns>
        public List<Sys_Role> GetRoles()
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
            var list = db.Sys_Roles.Where(r => r.Name != Constants.ALL_ROLE).OrderBy(r => r.Name);
            return list.ToList();
        }

        public MST_UnitOfMeasure GetUnitOfMeasure(int unitId)
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
            return db.MST_UnitOfMeasures.FirstOrDefault(u => u.UnitOfMeasureID == unitId);
        }
    }
}
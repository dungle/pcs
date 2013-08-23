using System;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace PCSComUtils.DataContext
{
    public partial class PCSDataContext: System.Data.Linq.DataContext
    {
        /// <summary>
        /// Called when [created].
        /// </summary>
        partial void OnCreated()
        {
            CommandTimeout = 3600;
        }

        [Function(Name = "GetDate", IsComposable = true)]
        public DateTime GetServerDate()
        {
            MethodInfo mi = MethodBase.GetCurrentMethod() as MethodInfo;
            return (DateTime)this.ExecuteMethodCall(this, mi, new object[] { }).ReturnValue;
        }
    }
}

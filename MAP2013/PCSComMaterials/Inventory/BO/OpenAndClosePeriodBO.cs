using System;
using System.Linq;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComMaterials.Inventory.BO
{
	public class OpenAndClosePeriodBO
	{
        /// <summary>
        ///     Close the period by calling store procedure.
        ///     Deactivate selected period and active period of next month
        /// </summary>
        /// <param name="effectDate"></param>
        /// <param name="periodId"></param>
		public void ClosePeriod(DateTime effectDate, int periodId)
        {
            var db = new PCSDataContext(Utils.Instance.ConnectionString);
		    DateTime nextMonth = effectDate.AddMonths(1);
		    DateTime previousMonth = effectDate.AddMonths(-1);
		    db.spClosePeriod(effectDate, nextMonth, previousMonth);
		    var period = db.Sys_Periods.FirstOrDefault(p => p.PeriodID == periodId);
            if (period != null)
            {
                period.Activate = false;
            }
		    // find the next period and active it
		    var nextPeriod = db.Sys_Periods.FirstOrDefault(p => p.FromDate == nextMonth);
            if (nextPeriod != null)
            {
                nextPeriod.Activate = true;
            }
            if (db.GetChangeSet().Updates.Count > 0)
            {
                db.SubmitChanges();
            }
        }
	}
}

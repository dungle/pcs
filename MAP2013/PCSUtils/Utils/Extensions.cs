using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C1.Win.C1TrueDBGrid;
using PCSComUtils.Common.BO;

namespace PCSUtils.Utils
{
    public static class Extensions
    {
        #region DateTime extensions

        /// <summary>
        /// Determines whether the date is valid post date
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid post date] [the specified date]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidPostDate(this DateTime date)
        {
            var currentPeriod = Utilities.Instance.GetWorkingPeriod();
            if (currentPeriod == null)
            {
                return false;
            }

            return date <= currentPeriod.ToDate.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Truncate part of date time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            return timeSpan == TimeSpan.Zero ? dateTime : dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        #endregion

        #region C1TrueDBGrid extensions

        /// <summary>
        /// Deletes the multi rows on grid and return list of remove item
        /// </summary>
        /// <param name="grid">The grid.</param>
        public static void DeleteMultiRows(this C1TrueDBGrid grid)
        {
            //store the index of selectrows
            int intSelectRows = grid.SelectedRows.Count;
            var intIndexOfSelectedRows = new ArrayList();
            for (int i = 0; i < intSelectRows; i++)
            {
                intIndexOfSelectedRows.Add(int.Parse(grid.SelectedRows[i].ToString()));
            }
            intIndexOfSelectedRows.Sort();

            //delete Rows
            for (int i = intSelectRows - 1; i >= 0; i--)
            {
                grid.Row = (int)intIndexOfSelectedRows[i];
                grid.Delete();
            }
        }

        #endregion
    }
}

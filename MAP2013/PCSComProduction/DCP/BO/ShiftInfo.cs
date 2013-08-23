using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Shift information for running DCP Estimate
    /// </summary>
    internal class ShiftInfo
    {
        /// <summary>
        /// Gets or sets the production line id.
        /// </summary>
        /// <value>The production line id.</value>
        public int ProductionLineId { get; set; }
        /// <summary>
        /// Gets or sets the shift id.
        /// </summary>
        /// <value>The shift id.</value>
        public int ShiftId { get; set; }
        /// <summary>
        /// Gets or sets the work time from.
        /// </summary>
        /// <value>The work time from.</value>
        public DateTime? WorkTimeFrom { get; set; }
        /// <summary>
        /// Gets or sets the work time to.
        /// </summary>
        /// <value>The work time to.</value>
        public DateTime? WorkTimeTo { get; set; }
        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        /// <value>The begin date.</value>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; set; }
    }
}

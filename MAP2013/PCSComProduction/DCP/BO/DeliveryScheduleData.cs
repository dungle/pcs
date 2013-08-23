using System;

namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Represents class of Delivery schedule data for running DCP
    /// </summary>
    public class DeliveryScheduleData : BaseInfo
    {
        /// <summary>
        /// Gets or sets the schedule date.
        /// </summary>
        /// <value>The schedule date.</value>
        public DateTime ScheduleDate { get; set; }
        /// <summary>
        /// Gets or sets the safety stock.
        /// </summary>
        /// <value>The safety stock.</value>
        public decimal SafetyStock { get; set; }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime { get; set; }
    }
}
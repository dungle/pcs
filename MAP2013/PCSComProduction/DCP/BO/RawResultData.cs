using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComProduction.DCP.BO
{
    internal class RawResultData : BaseInfo
    {
        /// <summary>
        /// Gets or sets the produce quantity.
        /// </summary>
        /// <value>The produce quantity.</value>
        public decimal ProduceQuantity { get; set; }
        /// <summary>
        /// Gets or sets the produce order.
        /// </summary>
        /// <value>The produce order.</value>
        public decimal ProduceOrder { get; set; }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the safety stock amount.
        /// </summary>
        /// <value>The safety stock amount.</value>
        public decimal SafetyStockAmount { get; set; }
    }
}

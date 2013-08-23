using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Represents base class for all information object when running DCP
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// Gets or sets the checkpoint per item.
        /// </summary>
        /// <value>The checkpoint per item.</value>
        public decimal CheckpointPerItem { get; set; }
        /// <summary>
        /// Gets or sets the capacity bottle id.
        /// </summary>
        /// <value>The capacity bottle id.</value>
        public int CapacityBottleId { get; set; }
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>The product id.</value>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the work center id.
        /// </summary>
        /// <value>The work center id.</value>
        public int WorkCenterId { get; set; }
        /// <summary>
        /// Gets or sets the work center code.
        /// </summary>
        /// <value>The work center code.</value>
        public string WorkCenterCode { get; set; }
        /// <summary>
        /// Gets or sets the production line id.
        /// </summary>
        /// <value>The production line id.</value>
        public int ProductionLineId { get; set; }
        /// <summary>
        /// Gets or sets the product revision.
        /// </summary>
        /// <value>The revision.</value>
        public string Revision { get; set; }
        /// <summary>
        /// Gets or sets the order quantity multiple.
        /// </summary>
        /// <value>The order quantity multiple.</value>
        public decimal OrderQuantityMultiple { get; set; }
        /// <summary>
        /// Gets or sets the min produce.
        /// </summary>
        /// <value>The min produce.</value>
        public decimal MinProduce { get; set; }
        /// <summary>
        /// Gets or sets the max produce.
        /// </summary>
        /// <value>The max produce.</value>
        public decimal MaxProduce { get; set; }
        /// <summary>
        /// Gets or sets the scrap percent.
        /// </summary>
        /// <value>The scrap percent.</value>
        public double ScrapPercent { get; set; }
        /// <summary>
        /// Gets or sets the max round up to min.
        /// </summary>
        /// <value>The max round up to min.</value>
        public decimal MaxRoundUpToMin { get; set; }
        /// <summary>
        /// Gets or sets the max round up to multiple.
        /// </summary>
        /// <value>The max round up to multiple.</value>
        public decimal MaxRoundUpToMultiple { get; set; }
        /// <summary>
        /// Gets or sets the lead time.
        /// </summary>
        /// <value>The lead time.</value>
        public decimal? LeadTime { get; set; }
        /// <summary>
        /// Gets or sets the fix lead time.
        /// </summary>
        /// <value>The fix lead time.</value>
        public decimal FixLeadTime { get; set; }
        /// <summary>
        /// Gets or sets the routing id.
        /// </summary>
        /// <value>The routing id.</value>
        public int RoutingId { get; set; }
        /// <summary>
        /// Gets or sets the sample pattern.
        /// </summary>
        /// <value>The sample pattern.</value>
        public byte SamplePattern { get; set; }
        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        /// <value>The sample rate.</value>
        public decimal SampleRate { get; set; }
        /// <summary>
        /// Gets or sets the delay time.
        /// </summary>
        /// <value>The delay time.</value>
        public decimal DelayTime { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public decimal Quantity { get; set; }
    }
}

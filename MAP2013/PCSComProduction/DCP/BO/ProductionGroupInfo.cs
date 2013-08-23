using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Represent class for Product group information when running DCP
    /// </summary>
    internal class ProductionGroupInfo
    {
        /// <summary>
        /// Gets or sets the production group id.
        /// </summary>
        /// <value>The production group id.</value>
        public int ProductionGroupId { get; set; }
        /// <summary>
        /// Gets or sets the group production max.
        /// </summary>
        /// <value>The group production max.</value>
        public decimal? GroupProductionMax { get; set; }
        /// <summary>
        /// Gets or sets the production line id.
        /// </summary>
        /// <value>The production line id.</value>
        public int ProductionLineId { get; set; }
        /// <summary>
        /// Gets or sets the work center id.
        /// </summary>
        /// <value>The work center id.</value>
        public int WorkCenterId { get; set; }
        /// <summary>
        /// Gets or sets the capacity of group.
        /// </summary>
        /// <value>The capacity of group.</value>
        public decimal? CapacityOfGroup { get; set; }
        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>The product id.</value>
        public int ProductId { get; set; }
    }
}

using System;

namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Order information for running DCP estimate
    /// </summary>
    internal class OrderInfo
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>The product id.</value>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Gets or sets the ship from loc id.
        /// </summary>
        /// <value>The ship from loc id.</value>
        public int MasterLocationId { get; set; }
        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>The due date.</value>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Gets or sets the work center id.
        /// </summary>
        /// <value>The work center id.</value>
        public int WorkCenterId { get; set; }
    }
}

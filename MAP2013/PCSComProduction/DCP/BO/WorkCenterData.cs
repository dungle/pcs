using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComProduction.DCP.BO
{
    /// <summary>
    /// Represents class of work center data for running DCP
    /// </summary>
    internal class WorkCenterData : BaseInfo
    {
        /// <summary>
        /// Gets or sets the department code.
        /// </summary>
        /// <value>The department code.</value>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [balance planning].
        /// </summary>
        /// <value><c>true</c> if [balance planning]; otherwise, <c>false</c>.</value>
        public bool BalancePlanning { get; set; }
        /// <summary>
        /// Gets or sets the round up days exception.
        /// </summary>
        /// <value>The round up days exception.</value>
        public int RoundUpDaysException { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [set min produce].
        /// </summary>
        /// <value><c>true</c> if [set min produce]; otherwise, <c>false</c>.</value>
        public bool SetMinProduce { get; set; }
        /// <summary>
        /// Gets or sets the planning offset id.
        /// </summary>
        /// <value>The planning offset id.</value>
        public int PlanningOffsetId { get; set; }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public int Offset { get; set; }
        /// <summary>
        /// Gets or sets the planning start date.
        /// </summary>
        /// <value>The planning start date.</value>
        public DateTime? PlanningStartDate { get; set; }
        /// <summary>
        /// Gets or sets the work center level.
        /// </summary>
        /// <value>The work center level.</value>
        public int WorkCenterLevel { get; set; }
        /// <summary>
        /// Gets or sets the work center ancessors.
        /// </summary>
        /// <value>The work center ancessors.</value>
        public string WorkCenterAncessors { get; set; }
    }
}

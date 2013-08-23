using System;

namespace PCSComProduction.DCP.BO
{
    internal class PlanningOffsetInfo
    {
        public int PlanningOffsetId { get; set; }
        public int Offset { get; set; }
        public DateTime? PlanningStartDate { get; set; }
        public int WorkCenterId { get; set; }
    }
}

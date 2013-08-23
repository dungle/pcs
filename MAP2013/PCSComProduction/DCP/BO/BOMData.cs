using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComProduction.DCP.BO
{
    internal class BOMData : BaseInfo
    {
        public int ComponentId { get; set; }
        public decimal LeadTimeOffset { get; set; }
        public decimal Shrink { get; set; }
        public int ParentWorkCenterId { get; set; }
        public string ParentWorkCenterCode { get; set; }
    }
}

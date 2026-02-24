using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class DeltaViewModel
    {
        public Guid EmployeeId { get; set; }
        public PerformanceConfigViewModel performanceConfig { get; set; }
        public List<Delta> lstDeltas { get; set; }
        public List<Pluses> lstPluses { get; set; }       
    }
}

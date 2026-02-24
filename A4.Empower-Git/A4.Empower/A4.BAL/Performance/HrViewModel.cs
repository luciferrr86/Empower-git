using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class HrViewModel
    {
        public bool IsPerformanceStart { get; set; }
        public bool IsConfigurationSet { get; set; }
        public bool IsMidYearEnabled { get; set; }
        public bool IsMidYearReviewCompleted { get; set; }
        public List<HrViewList> lstManager { get; set; }
        public List<HrViewList> lstEmployee { get; set; }
        public int totalCount { get; set; }
    }

    public class HrViewList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Group { get; set; }
        public string Status { get; set; }       
    }
}

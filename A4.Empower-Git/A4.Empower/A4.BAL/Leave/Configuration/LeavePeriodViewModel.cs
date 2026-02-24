using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeavePeriodViewModel
    {
        public LeavePeriodViewModel()
        {
            LeavePeriodModel = new List<LeavePeriodModel>();
        }
        public List<LeavePeriodModel> LeavePeriodModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class LeavePeriodModel
    {

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime PeriodStart { get; set; }
        [Required]
        public DateTime PeriodEnd { get; set; }
        public bool IsLeavePeriodCompleted { get; set; }
        public bool IsEdit { get; set; }
    }
}

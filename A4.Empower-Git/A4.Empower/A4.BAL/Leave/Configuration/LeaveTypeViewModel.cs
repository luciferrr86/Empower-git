using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveTypeViewModel
    {
        public LeaveTypeViewModel()
        {
            LeaveTypeModel = new List<LeaveTypeModel>();
        }
        public List<LeaveTypeModel> LeaveTypeModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class LeaveTypeModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
      //  public string ColorCode { get; set; }
        public string LeavePeriodId { get; set; }
        public bool InUsed { get; set; }
    }
}

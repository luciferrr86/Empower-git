using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveWorkingDayViewModel
    {
        public LeaveWorkingDayViewModel()
        {
            LeaveWorkingDayModel = new List<LeaveWorkingDayModel>();
        }
        public List<LeaveWorkingDayModel> LeaveWorkingDayModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class LeaveWorkingDayModel
    {
        public string Id { get; set; }
        [Required]
        public string WorkingDay { get; set; }
        [Required]
        public string WorkingDayValue { get; set; }
        [Required]
        public string LeavePeriodId { get; set; }
    }
}

using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveRulesViewModel
    {
        public LeaveRulesViewModel()
        {
            LeaveRulesModel = new List<LeaveRulesModel>();
        }
        public List<LeaveRulesModel> LeaveRulesModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class LeaveRulesModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LeavesPerYear { get; set; }
       
        public string LeavePeriodId { get; set; }
        [Required]
        public string LeaveTypeId { get; set; }
        [Required]
        public string BandId { get; set; }

        public List<DropDownList> BandList { get; set; }
        public List<DropDownList> LeaveTypeList { get; set; }
    }
}

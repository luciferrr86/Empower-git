using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveHolidayListViewModel
    {
        public LeaveHolidayListViewModel()
        {
            LeaveHolidayListModel = new List<LeaveHolidayListModel>();
        }
        public List<LeaveHolidayListModel> LeaveHolidayListModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class LeaveHolidayListModel
    {
       
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime HolidayDate { get; set; }
       
        public string LeavePeriodId { get; set; }
    }
}

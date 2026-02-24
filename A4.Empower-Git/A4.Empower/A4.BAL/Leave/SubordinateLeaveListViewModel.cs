using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class SubordinateLeaveListViewModel
    {
        public SubordinateLeaveListViewModel()
        {
            SubordinateLeaveListModel = new List<SubordinateLeaveListModel>();
        }
        public List<SubordinateLeaveListModel> SubordinateLeaveListModel { get; set; }
        public int TotalCount { get; set; }
    }

    public class SubordinateLeaveListModel
    {
        public string LeaveDeatilId { get; set; }

      
        public string EmployeeName { get; set; }
      
        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string NoOfDays { get; set; }
    }


    public class SubordinateLeaveDetailModel
    {
        [Required]
        public string Id { get; set; }
       
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
      
        public string ReasonForApply { get; set; }

        public string Approvedby { get; set; }
      
        public string ManagerId { get; set; }

        public string ManagerName { get; set; }

        [Required]
        public string ManagerComment { get; set; }

        public string LeaveType { get; set; }

        public string NoOfDays { get; set; }

        public string Status { get; set; }

        [Required]
        public string ButtonType { get; set; }
    }

}

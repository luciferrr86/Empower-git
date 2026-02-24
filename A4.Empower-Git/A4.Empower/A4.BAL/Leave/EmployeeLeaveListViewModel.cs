using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class EmployeeLeaveListViewModel
    {

        public EmployeeLeaveListViewModel()
        {
            EmployeeLeaveListModel = new List<EmployeeLeaveListModel>();
        }
        public List<EmployeeLeaveListModel> EmployeeLeaveListModel { get; set; }
        public int TotalCount { get; set; }

    }

    public class EmployeeLeaveListModel
    {
        public string LeaveDeatilId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string LeaveType { get; set; }

        public string Status { get; set; }

        public bool IscancelPeriodEnd { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsSave { get; set; }
    }

    public class EmployeeLeaveDetailModel
    {
        public string Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ReasonForApply { get; set; }

        public string ManagerName { get; set; }

        public string ManagerComment { get; set; }

        public string LeaveType { get; set; }

        public string noOfDays { get; set; }
    }
   
}

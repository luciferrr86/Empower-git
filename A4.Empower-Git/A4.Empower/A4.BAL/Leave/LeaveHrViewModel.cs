using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveHrViewModel
    {
        public LeaveHrViewModel()
        {
            LeaveEmployeeList = new List<LeaveHrModel>();
        }
        public bool IsConfigSet { get; set; }
        public List<LeaveHrModel> LeaveEmployeeList { get; set; }
        public int TotalCount { get; set; }
    }

    public class LeaveHrModel
    {


        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Department { get; set; }

        public int RemainingLeave { get; set; }

        public string EmployeeLeaveDetailsId { get; set; }
    }

    public class EmployeeLeaveDetails
    {
        public string LeaveType { get; set; }

        public int AllottedLeaves { get; set; }

        public int TakenLeaves { get; set; }

        public int RemainingLeave { get; set; }
    }
}

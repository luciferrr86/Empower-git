using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class LeaveEntitlementModel
    {
        public LeaveEntitlementModel()
        {
            ListLeaveInfo = new List<LeaveInfoModel>();
        }

        public int AllLeave { get; set; }

        public int Approved { get; set; }

        public int Rejected { get; set; }

        public int Remaining { get; set; }

        public  List<LeaveInfoModel> ListLeaveInfo { get; set; }

    }

    public class LeaveInfoModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Approved { get; set; }

        public string Pending { get; set; }

        public string Rejected { get; set; }

        public string Remaining { get; set; }

        public string EmployeeLeavesId { get; set; }

        public string LeaveRulesId { get; set; }

        public string LeaveType { get; set; }

        public string NoOfDays { get; set; }

        public bool IsCancellationPeriodEnd { get; set; }
    }
}

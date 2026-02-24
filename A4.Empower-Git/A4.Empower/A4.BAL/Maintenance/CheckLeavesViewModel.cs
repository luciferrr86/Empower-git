using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
  public class CheckLeavesViewModel
    {
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string UserId { get; set; }
        public string EmployeeName { get; set; }
        public decimal WorkedDays { get; set; }
        public decimal OnDuty { get; set; }
        public decimal WeeklyOff { get; set; }
        public decimal Holidays { get; set; }
        public decimal UnPaidLeaves { get; set; }

        public decimal Absent { get; set; }
        public decimal CompOffAvail { get; set; }
        public decimal CompOffEarned { get; set; }
        public decimal ExtraWorking { get; set; }
        public decimal CasualLeaves { get; set; }
        public decimal EarnedLeaves { get; set; }
        public decimal SickLeaves { get; set; }
        public decimal MaternityLeaves { get; set; }
        public decimal RestrictedLeaves { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class AttendenceSummaryViewModel
    {

        public int Id { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal DaysWorked { get; set; }
        public int LeaveTaken { get; set; }
        public int OnDuty { get; set; }
        public int WeeklyOff { get; set; }
        public int Holidays { get; set; }
        public int Unpaid { get; set; }
        public int PaidDays { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }

    }


}

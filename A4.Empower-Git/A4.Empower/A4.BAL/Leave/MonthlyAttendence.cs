using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Leave
{
    public class MonthlyAttendence
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int StartDay { get; set; }
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }

        public List<string> ApprovedLeavedates { get; set; }
        public IEnumerable<EmployeeAttendenceViewModel> EmployeeAttendenceVM { get; set; }
        public List<MonthDate> MonthDates { get; set; }
        
    }
    public class MonthDate
    {
        public string MDate { get; set; }
        public string MDay { get; set; }

        public int MDayStatus { get; set; }

    }
    }

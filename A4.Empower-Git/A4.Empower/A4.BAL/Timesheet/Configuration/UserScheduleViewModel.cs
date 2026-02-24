using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Timesheet.Configuration
{
    public class TimeshettUserScheduleViewModel
    {
        public TimeshettUserScheduleViewModel()
        {
            EmpTimesheet = new EmployeeTimesheet();
        }
        public Guid TimesheetTemplateId { get; set; }
        public EmployeeTimesheet EmpTimesheet { get; set; }
    }

    public class EmployeeTimesheet {

        public Guid Id { get; set; }
        public Guid FullName { get; set; }

    }
}

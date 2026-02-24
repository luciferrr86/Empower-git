using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
  public class ApplicationModuleModal
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Module Module { get; set; }
    } 

    public enum Module
    {
        SalesMarketing = 1,
        Recruitment =2,
        ExpenseBooking =3,
        Timesheet = 4,
        Performance = 5,
        Leave =6
    }
}

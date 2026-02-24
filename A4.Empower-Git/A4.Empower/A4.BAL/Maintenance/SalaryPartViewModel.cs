using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
    public class SalaryPartViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeSalaryId { get; set; }
        public int CtcOtherComponentId { get; set; }
       

    }
}

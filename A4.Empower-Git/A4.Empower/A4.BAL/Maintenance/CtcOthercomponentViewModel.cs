using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
    public class CtcOthercomponentViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeCtcId { get; set; }
        public int SalaryComponentId { get; set; }

        public List<SalaryPartViewModel> SalaryPart { get; set; }
    }
}

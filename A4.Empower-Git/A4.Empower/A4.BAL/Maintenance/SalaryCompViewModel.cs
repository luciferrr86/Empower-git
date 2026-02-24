using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL.Maintenance
{
    public class SalaryCompViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEarnings { get; set; }
        public bool IsMonthly { get; set; }
        public bool IsAllYearly { get; set; }
        public bool IsActive { get; set; }


        public List<CtcOthercomponentViewModel> CtcOthercomponentViewModel { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobWorkExperienceViewModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string ProfileDesc { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOR { get; set; }

        public string ProfileId { get; set; }
    }
}

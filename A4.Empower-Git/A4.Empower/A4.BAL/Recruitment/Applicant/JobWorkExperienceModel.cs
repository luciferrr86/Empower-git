using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobWorkExperienceModel
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string ProfileDesc { get; set; }
        public string Doj { get; set; }
        public string Dor { get; set; }

        public string ProfileId { get; set; }
    }

    public class PostJobWorkExperienceModel
    {
        public List<JobWorkExperienceModel> professional { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobApplicationQuestionsModel
    {
        public string Id { get; set; }

        public string Answer { get; set; }

        public string Weightage { get; set; }

        public string JobApplicationId { get; set; }

        public string JobQuestionsId { get; set; }

        public string KpiType { get; set; }

        public string ObtainedWeightage { get; set; }

        public bool AnsOption1 { get; set; }

        public bool AnsOption2 { get; set; }

        public bool AnsOption3 { get; set; }

        public bool AnsOption4 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class HRKpiModel
    {
        public string Id { get; set; }
        public string JobApplicationId { get; set; }
        public string JobHRQuestionId { get; set; }
        public string Weightage { get; set; }
        public string Question { get; set; }
        public string ObtainedWeightage { get; set; }
    }
}

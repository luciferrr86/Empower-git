using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobApplicationQuestionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Answer { get; set; }
        public string Weightage { get; set; }
        public string JobApplicationId { get; set; }
        public string JobQuestionsId { get; set; }
    }
}

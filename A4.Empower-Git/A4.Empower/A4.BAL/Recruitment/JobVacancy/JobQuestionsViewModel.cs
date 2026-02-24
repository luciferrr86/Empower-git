using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobQuestionsViewModel
    {
        public JobQuestionsViewModel()
        {
            JobQuestionsModel = new List<JobQuestionsModel>();
        }

        public List<JobQuestionsModel> JobQuestionsModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class JobQuestionsModel 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Questions { get; set; }
        public string SType { get; set; }
        [Required]
        public string KPIType { get; set; }
        public string Answer { get; set; }
        [Required]
        public string Weightage { get; set; }
        public bool bIsRequired { get; set; }
        [Required]
        public string JobVacancyId { get; set; }


        public List<QuestionOptionModel> OptionList { get; set; }
    }
}

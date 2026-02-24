using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobVacancyViewModel
    {
        public JobVacancyViewModel()
        {
            JobVacancyModel = new List<JobVacancyModel>();
        }

        public List<JobVacancyModel> JobVacancyModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class JobVacancyModel
    {
        public JobVacancyModel()
        {
            ScreeningQuestionList = new List<JobScreeningQuestions>();
            SkillKpiList = new List<SkillKpi>();
            JobHRKpiList = new List<JobHRKpi>();
            JobVacancyLevel = new List<InterviewLevelOption>();
            NoOfvacancies = 1;
        }
        public string Id { get; set; }

        public int NoOfvacancies { get; set; }

        public string JobTitle { get; set; }

        public string JobLocation { get; set; }

        public string Experience { get; set; }

        public string SalaryRange { get; set; }

        public string Currency { get; set; }

        public string JobDescription { get; set; }

        public string JobRequirements { get; set; }

        public string JobTypeId { get; set; }


        public List<string> ManagerIdL1 { get; set; }
        public List<string> ManagerIdL2 { get; set; }
        public List<string> ManagerIdL3 { get; set; }



        public bool bIsClosed { get; set; }
        public bool bIsPublished { get; set; }
        public string PublishedDate { get; set; }

        public string JdAvailable
        {
            get
            {
                var haveData = !string.IsNullOrEmpty(this.JobDescription);
                if (haveData)
                    return  "Yes";
                else return "No";
            }
            
        }

        public List<DropDownList> ManagerList { get; set; }
        public List<DropDownList> JobTypeList { get; set; }

        public List<JobScreeningQuestions> ScreeningQuestionList { get; set; }
        public List<SkillKpi> SkillKpiList { get; set; }
        public List<JobHRKpi> JobHRKpiList { get; set; }
        public List<InterviewLevelOption> JobVacancyLevel { get; set; }
    }

    public class InterviewLevelOption
    {
        public InterviewLevelOption()
        {
            ManagerList = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> ManagerList { get; set; }
    }
}

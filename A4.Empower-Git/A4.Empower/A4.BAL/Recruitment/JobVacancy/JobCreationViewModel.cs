using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobCreationViewModel
    {
        public JobCreationViewModel()
        {
            JobCreationModel = new List<JobCreationModel>();
        }

        public List<JobCreationModel> JobCreationModel { get; set; }
        public int TotalCount { get; set; }
    }
//    public class JobCreationModel
//    {
//        public JobCreationModel()
//        {
//            ScreeningQuestionList = new List<JobScreeningQuestion>();
//            SkillKpiList = new List<SkillKpi>();
//            JobHRKpiList = new List<JobHRKpi>();
//        }
//        public string Id { get; set; }
//        public string Name { get; set; }
//        [Required]
//        public int NoOfvacancies { get; set; }
//        [Required]
//        public string JobTitle { get; set; }
//        [Required]
//        public string JobLocation { get; set; }
//        [Required]
//        public string Experience { get; set; }
//        [Required]
//        public string SalaryRange { get; set; }
//        [Required]
//        public string Currency { get; set; }
//        [Required]
//        public string JobDescription { get; set; }
//        [Required]
//        public string JobRequirements { get; set; }
//        [Required]
//        public string JobTypeId { get; set; }
//        [Required]
//        public List<string> ManagerId { get; set; }

//        public bool bIsClosed { get; set; }
//        public bool bIsPublished { get; set; }
//        public string PublishedDate { get; set; }
//        public List<DropDownList> ManagerList { get; set; }

//        public List<JobScreeningQuestion> ScreeningQuestionList { get; set; }
//        public List<SkillKpi> SkillKpiList { get; set; }
//        public List<JobHRKpi> JobHRKpiList { get; set; }
//    }
//    public class JobScreeningQuestion
//    {
//        public string Id { get; set; }
//        public string Question { get; set; }
//        public int Weightage { get; set; }
//        public Boolean Mandatory { get; set; }
//        public string ControlType { get; set; }
//        public string Option1 { get; set; }
//        public string Option2 { get; set; }
//        public string Option3 { get; set; }
//        public string Option4 { get; set; }
//        public Boolean OptChk1 { get; set; }
//        public Boolean OptChk2 { get; set; }
//        public Boolean OptChk3 { get; set; }
//        public Boolean OptChk4 { get; set; }

//    }
//    public class JobHRKpi
//    {
//        public string Id { get; set; }
//        public string Question { get; set; }
//        public int Weightage { get; set; }

//    }
//    public class SkillKpi
//    {
//        public string Id { get; set; }
//        public string Question { get; set; }
//        public int Weightage { get; set; }

//    }
}

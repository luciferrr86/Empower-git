using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{

    public class JobCreationModel
    {
        public JobCreationModel()
        {
            //ScreeningQuestionList = new List<JobScreeningQuestionModel>();
            //SkillKpiList = new List<SkillKpi>();
            //JobHRKpiList = new List<JobHRKpi>();
            JobVacancyLevel = new List<JobVaccancyLevelManager>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public int NoOfvacancies { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobLocation { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string SalaryRange { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public string JobRequirements { get; set; }
        [Required]
        public string JobTypeId { get; set; }

        public bool bIsClosed { get; set; }
        public bool bIsPublished { get; set; }
        public string PublishedDate { get; set; }
        public List<DropDownList> ManagerList { get; set; }

        public List<JobVaccancyLevelManager> JobVacancyLevel { get; set; }
    }
    //public class JobScreeningQuestionModel
    //{
    //    public JobScreeningQuestionModel()
    //    {
    //        JobScreeningQuestion = new List<JobScreeningQuestions>();
    //    }
        
    //    public List<JobScreeningQuestions> JobScreeningQuestion { get; set; }

    //}

    public class JobScreeningQuestions
    {
        public string Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public int Weightage { get; set; }
        public Boolean Mandatory { get; set; }
        [Required]
        public string ControlType { get; set; }
        [Required]
        public string Option1 { get; set; }
        [Required]
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public Boolean OptChk1 { get; set; }
        public Boolean OptChk2 { get; set; }
        public Boolean OptChk3 { get; set; }
        public Boolean OptChk4 { get; set; }
    }
    public class JobHRKpi
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public int Weightage { get; set; }

    }
    public class JobHRQuestionModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public int Weightage { get; set; }

    }
    public class JobSkillQuestionModel
    {
        public JobSkillQuestionModel()
        {
            JobSkillQuestion = new List<JobSkillQuestions>();
            LevelList = new List<DropDownList>();
        }
        public List<JobSkillQuestions> JobSkillQuestion { get; set; }
        public List<DropDownList> LevelList { get; set; }

    }
    public class JobSkillQuestions
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public int Weightage { get; set; }
        public List<string> LevelIdList { get; set; }

    }
    public class SkillKpi
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public int Weightage { get; set; }

    }
    public class JobVaccancyLevelManager
    {
        public JobVaccancyLevelManager()
        {
            ManagerList = new List<string>();
        }
        //public string JobVaccancyLevelId { get; set; }
        public string Name { get; set; }
        //public int Level { get; set; }
        public List<string> ManagerList { get; set; }
    }
    public class JobVaccancyManager {
        public string JobVaccancyManagerId { get; set; }
        public string EmployeeId { get; set; }
    }
}

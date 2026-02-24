using A4.BAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace A4.BAL
{
    public class JobApplicationViewModel
    {
        public JobApplicationViewModel()
        {
            GetApplicationModel = new List<GetApplicationModel>();
        }

        public List<GetApplicationModel> GetApplicationModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class JobApplicationModel
    {
        public string Id { get; set; }

        //[Required]
        public string ApplicationType { get; set; }

        public string HRComment { get; set; }

        public string Score { get; set; }

        //[Required]
        public string CandidateId { get; set; }

        //[Required]
        public string JobStatusId { get; set; }

        //[Required]
        public string JobVacancyId { get; set; }

        public List<JobApplicationQuestionsModel> jobApplicationQuestionsModel { get; set; }



    }


    public class GetApplicationModel
    {
        public string ApplicationId { get; set; }

        public string CandidateId { get; set; }

        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string JobVacancyId { get; set; }

        public string VacancyName { get; set; }

        public string ApplicationType { get; set; }

        public string Score { get; set; }

        public string StatusName { get; set; }

        public string JobStatusId { get; set; }

        public List<DropDownList> JobStatusList { get; set; }

        public IFormFile Resume { get; set; }
    }
    public class ApplicationQuestionModel
    {
        public ApplicationQuestionModel()
        {
            questions = new List<ApplicationQuestion>();
        }
        public string Id { get; set; }
        public string CandidateId { get; set; }
        public string JobVacancyId { get; set; }

        public List<ApplicationQuestion> questions { get; set; }
    }
    public class ApplicationQuestion {
        public string Id { get; set; }
        public List<string> Option { get; set; }
    }
}

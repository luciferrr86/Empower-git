using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class CandidateInterViewViewModel
    {
        public string JobTitle { get; set; }

        public string ApplicationType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string DateofApplication { get; set; }

        public string ContactNumber { get; set; }

        public List<JooInterViewScheduleListModel> interViewSchedule { get;set;}

        public List<JobQuestionAnswersModel> QuestionAnswers { get; set; }
    }
    public class JooInterViewScheduleListModel
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public string ManagerName { get; set; }

        public string Status { get; set; }

    }

    public class JobQuestionAnswersModel
    {
        public string Question { get; set; }

        public string CandidateAnswer { get; set; }

        public string ActualAnswer { get; set; }
    }

    public class JobApplyModel
    {
        public Guid jobId { get; set;  }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public IFormFileCollection attachment { get; set; }
    }
}

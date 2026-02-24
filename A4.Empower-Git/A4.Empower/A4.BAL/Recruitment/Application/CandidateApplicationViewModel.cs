using A4.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class CandidateApplicationViewModel
    {
        public CandidateApplicationViewModel()
        {
            ListInterviewScheduleModel = new List<InterviewScheduleModel>();
            QuestionAnswerModel = new QuestionAnswerModel();
            JobInformationModel = new JobInformationModel();
            ManagerList = new List<DropDownList>();
            InterviewTypeList = new List<DropDownList>();
        }
        public JobInformationModel JobInformationModel { get; set; }
        public List<InterviewScheduleModel> ListInterviewScheduleModel { get; set; }
        public QuestionAnswerModel QuestionAnswerModel { get; set; }

        public List<DropDownList> InterviewTypeList { get; set; }

        public List<DropDownList> ManagerList { get; set; }

        public CandidateInterViewModel CandidateInterView { get; set; }

        public class CandidateInterViewModel
        {
            public string ResumeUrl { get; set; } = string.Empty;
            public string CandidateName { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string AppliedFor { get; set; }
            public string Score { get; set; }

            public string Comment { get; set; }
            public string JobApplicationId { get; set; }
            public string Id { get; set; }
            public CandidateInterViewModel()
            {
                QuestionAnswerList = new List<QuestionAnswer>();
            }
            public List<QuestionAnswer> QuestionAnswerList { get; set; }


        }
    }
}

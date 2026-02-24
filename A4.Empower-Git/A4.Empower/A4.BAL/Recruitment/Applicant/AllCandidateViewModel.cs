using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class AllCandidateViewModel
    {
        public AllCandidateViewModel()
        {
            CandidateModel = new List<AllCandidateModel>();
            InterviewLevelModel = new List<InterviewLevelModel>();
        }

        public List<AllCandidateModel> CandidateModel { get; set; }
        public List<InterviewLevelModel> InterviewLevelModel { get; set; }
        public string Position { get; set; }
        public int TotalCount { get; set; }
    }
    public class Basic
    {
        public string ApplicationId { get; set; }

        public string VacancyName { get; set; }
    }
    public class AllCandidateModel : Basic
    {
        public string CandidateId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string JobVacancyId { get; set; }

        public string Resume { get; set; }

        public string ApplicationType { get; set; }

        public string OverAllScore { get; set; }
        public string ScreeningScore { get; set; }
        public string HrScore { get; set; }
        public string SkillScore { get; set; }

        public string JobStatus { get; set; }
        public DateTime InterviewDate { get; set; }
        public string InterviewTime { get; set; }
        public string InterviewId{ get; set; }
        public string LevelId { get; set; }

   }

    public class InterviewLevelModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class CandidateViewModel
    {
        public CandidateViewModel()
        {
            JobCandidateProfileModel = new JobCandidateProfileModel();
            JobQualificationModel = new JobQualificationModel();
            JobWorkExperienceModel = new List<JobWorkExperienceModel>();
        }
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsLockedOut { get; set; }

        public string[] Roles { get; set; }

        public string ApplicationType { get; set; }
        // new private bool IsLockedOut { get; }
        public JobCandidateProfileModel JobCandidateProfileModel { get; set; }

        public JobQualificationModel JobQualificationModel { get; set; }

        public List<JobWorkExperienceModel> JobWorkExperienceModel { get; set; }




    }
}

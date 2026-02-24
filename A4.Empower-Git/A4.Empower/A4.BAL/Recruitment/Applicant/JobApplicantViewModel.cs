using A4.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace A4.BAL
{
    public class JobApplicantViewModel
    {
        public JobApplicantViewModel()
        {
            JobApplicantModel = new List<JobApplicantModel>();
        }

        public List<JobApplicantModel> JobApplicantModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class JobApplicantModel
    {
        
        public string Id { get; set; }
        [Required]
        public string ApplicationType { get; set; }

        public string Name { get; set; }
        [Required]
        public string CandidateId { get; set; }
        [Required]
        public string JobStatusId { get; set; }
        [Required]
        public string JobVacancyId { get; set; }
        [Required]
        public string Score { get; set; }
        [Required]
        public string CoverLetter { get; set; }
        //[Required]
        public string Resume { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }


        public string VacancyName { get; set; }
        //public string StatusName { get; set; }

        public List<DropDownList> JobStatusList { get; set; }
        //public List<DropDownList> ReferenceEmpNameList { get; set; }



    }
}

using A4.BAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobCandidateViewModel
    {
        public JobCandidateViewModel()
        {
            CandidateModel = new List<CandidateModel>();
        }

        public List<CandidateModel> CandidateModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class CandidateModel
    {
         public string ApplicationId { get; set; }

        public string CandidateId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string JobVacancyId { get; set; }

        public string VacancyName { get; set; }

        public string ApplicationType { get; set; }

        public string Score { get; set; }

        public string JobStatusId { get; set; }

        public List<DropDownList> JobStatusList { get; set; }

        public IFormFile Resume { get; set; }
   }
}

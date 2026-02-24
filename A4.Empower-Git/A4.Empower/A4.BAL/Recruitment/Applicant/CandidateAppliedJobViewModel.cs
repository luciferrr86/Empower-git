using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class CandidateAppliedJobViewModel
    {
        public CandidateAppliedJobViewModel()
        {
            CandidateAppliedJobModel = new List<CandidateAppliedJobModel>();
        }

        public List<CandidateAppliedJobModel> CandidateAppliedJobModel { get; set; }
        public int TotalCount { get; set; }
    }

    public class CandidateAppliedJobModel : Basic
    {
        public DateTime AppliedDate { get; set; }

        public string JobType { get; set; }

        public string JobStatus { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class CandidateInterviewScheduleModel
    {
        public string JobApplicationId { get; set; }
        public string Id { get; set; }
        public string LevelId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string JobInterviewTypeId { get; set; }
        public List<string> ManagerIdList { get; set; }

        public string jobCandidateURL { get; set; } = string.Empty;
    }
}

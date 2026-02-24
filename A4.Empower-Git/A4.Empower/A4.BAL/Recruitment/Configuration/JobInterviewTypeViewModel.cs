using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobInterviewTypeViewModel
    {
        public JobInterviewTypeViewModel()
        {
            JobInterviewTypeModel = new List<JobInterviewTypeModel>();
        }

        public List<JobInterviewTypeModel> JobInterviewTypeModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class JobInterviewTypeModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

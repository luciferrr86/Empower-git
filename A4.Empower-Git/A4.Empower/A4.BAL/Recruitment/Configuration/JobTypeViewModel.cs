using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobTypeViewModel
    {
        public JobTypeViewModel()
        {
            JobTypeModel = new List<JobTypeModel>();
        }

        public List<JobTypeModel> JobTypeModel { get; set; }
        public int TotalCount { get; set; }
    }
    public class JobTypeModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

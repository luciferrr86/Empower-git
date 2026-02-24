using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A4.BAL
{
    public class JobInterViewScheduleViewModel
    {
        public string Id { get; set; }

        public string Comment { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string ManagerId { get; set; }

        public string HrComment { get; set; }

        public string InterViewComment { get; set; }

        public string InterViewMode { get; set; }

        public string JobApplicationId { get; set; }

        public string JobInterViewTypeId { get; set; }

        public string JobStatusId { get; set; }

        public string Name { get; set; }

        public bool bIsInterViewCompleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class ReviewGoalViewModel
    {
        public ReviewGoalViewModel()
        {
            EmployeeDetailList = new List<EmployeeDetail>();
        }
        public List<EmployeeDetail> EmployeeDetailList { get; set; }
        public int TotalCount { get; set; }

        public DeltaViewModel deltaViewModel { get; set; }
        public TrainingClassViewModel trainingClassViewModel { get; set; }
        public GoalViewModel goalCurrentYearViewModel { get; set; }
    }
}

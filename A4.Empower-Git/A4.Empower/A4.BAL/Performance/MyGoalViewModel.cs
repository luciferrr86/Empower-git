using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.BAL
{
   //public class MyGoalViewModel
   // {
   //     public string EmployeeId { get; set; }
   //     public DeltaViewModel deltaViewModel { get; set; }
   //     public TrainingClassViewModel trainingClassViewModel { get; set; }
   //     public GoalViewModel goalCurrentYearViewModel { get; set; }
   //     //public GoalViewModel goalNextYearViewModel { get; set; }
   //     public CareerDevViewModel careerDevViewModel { get; set; }        
   //     public EmployeeDetail reviewedDetail { get; set; }

   //     [Required(ErrorMessage = "Please select year")]
   //     public string HistoryId { get; set; }
   //     public List<DropDownList> RoleAccessList { get; set; }
   //     public List<DropDownList> empPerformanceHistory { get; set; }
   //     public string GoalText { get; set; }

   // }

    public class PreCheck
    {
        public bool IsPerformanceStarted { get; set; }
        public bool IsManagerReleased { get; set; }
    }
}

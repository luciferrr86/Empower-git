using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class GoalViewModel
    {
        public string GoalInstructionText { get; set; }
        public string EmpGoalId { get; set; }
        //public bool IsManager { get; set; }
        public List<GoalMeasure> MidYearGoalMeasureList { get; set; }
        public List<GoalMeasure> EndYearGoalMeasureList { get; set; }
        //public IEnumerable<GoalMeasure> ViewGoalMeasureList { get; set; }
        //public string CompletedByConfig { get; set; }        
        //public PerformanceConfigViewModel performanceConfig { get; set; }
        //public List<DropDownList> empPerformanceHistory { get; set; }
        public CheckSaveSubmit CheckSaveSubmit { get; set; }
       
       // public List<SetGoal> lstNextYearGoal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.BAL
{
   public class GoalMeasure
    {
        public string GoalId { get; set; }
        public string GoalName { get; set; }
        public string Measure { get; set; }
        //[Required(ErrorMessage = "Please enter Accomplishment Properly")]
        public string Accomplishment { get; set; }
        //[Required(ErrorMessage = "Please enter Comments Properly")]
        public string ManagerComments { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

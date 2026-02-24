using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class TrainingClassViewModel
    {
        //public int EmployeeId { get; set; }
        public string InstructionText { get; set; }
        public List<TrainingClasses> lstTrainingClasses { get; set; }
        public CheckSaveSubmit CheckSaveSubmit { get; set; }
    }
}

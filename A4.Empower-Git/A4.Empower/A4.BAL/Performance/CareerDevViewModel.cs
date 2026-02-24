using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class CareerDevViewModel
    {
        public string InstructionText { get; set; }
        public List<CareerDevelopment> EmployeeCareerDevList { get; set; }
        public List<CareerDevelopment> ManagerCareerDevList { get; set; }
        //public List<CareerDevDocument> CareerDevDocList { get; set; } 
        public CheckSaveSubmit CheckSaveSubmit { get; set; }

    }
    public class CareerDevDocument
    {
        public string CareerDevDocId { get; set; }
        public string FileName { get; set; }
    }
}

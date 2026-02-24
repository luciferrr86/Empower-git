using System;
using System.Collections.Generic;
using System.Text;

namespace A4.BAL
{
   public class EmployeeGoalDetail
    {
        public string EmpGoalId { get; set; }
        public string ManagerId { get; set; }
        public string EmployeeId { get; set; }
        public string ManagerSignature { get; set; }
        public string ManagerYearEndSignature { get; set; }
        public string ManagerComment { get; set; }
        public string ManagerYearEndComment { get; set; }
        public string EmployeeSignature { get; set; }
        public string EmployeeComment { get; set; }
        public string EmployeeYearEndComment { get; set; }
        public string PresidentSignature { get; set; }
        public string PresidentComment { get; set; }
        public string PresidentYearEndComment { get; set; }
        public string InitialRating { get; set; }
        public string MidYearRating { get; set; }
        public string FinalRating { get; set; }
        public string PerYear { get; set; }
        public bool IsMgrRatingSubmit { get; set; }
    }
}

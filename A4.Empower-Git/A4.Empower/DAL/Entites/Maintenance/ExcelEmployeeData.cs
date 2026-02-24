using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class ExcelEmployeeData
    {
        #region Constructor
        public ExcelEmployeeData()
        {
            Id = new Guid();
            //IsActive = true;
        }
        #endregion


        #region Properties
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string WorkEmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string FunctionalDepartment { get; set; }
        public string FunctionalGroup { get; set; }
        public string Designation { get; set; }
        public string PersonalEmailID { get; set; }
        public string Title { get; set; }
        public string ReportingHeadEmailId { get; set; }
        public string RollAccess { get; set; }
        public string Location { get; set; }
        public string DateofJoining { get; set; }
        public string ReportingHeadName { get; set; }
        public string Band { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }       
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class Personal : AuditableEntity
    {
        public Personal()
        {
            Id = new Guid();
        }

        #region Properties
        public Guid Id { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DOB { get; set; }
        public string IdProofDetail { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string OfficialContactNo { get; set; }
        public string EmailId { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string PanNumber { get; set; }
        public string AadhaarNumber { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentState { get; set; }
        public string CurrentCountry { get; set; }
        public string CurrentZipCode { get; set; }
        #endregion

        #region ForeignKeyRelation

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        #endregion
    }
}

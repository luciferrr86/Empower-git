
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace A4.DAL.Entites.Maintenance
{
   public class EmployeeSalary: AuditableEntity
    {
       
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDaysOfMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AllowedLeave { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LeaveTaken { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal WorkedDays { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DA { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal HRA { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Conveyance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MedicalExpenses { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Special { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonus { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TA { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ContributionToPf { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfessionTax { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TDS { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryAdvance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryTotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPayable { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MedicalBillAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnpaidDays { get; set; }



        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        public int? EmployeeCtcId { get; set; }
        [JsonIgnore]
        public EmployeeCtc EmployeeCtc { get; set; }
       
        #endregion

        #region Navigation Property
        public List<SalaryPart> SalaryPart { get; set; }
        #endregion
    }
}

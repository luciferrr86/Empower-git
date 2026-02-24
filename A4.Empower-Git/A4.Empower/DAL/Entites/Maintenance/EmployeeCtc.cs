
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace A4.DAL.Entites.Maintenance
{
   public class EmployeeCtc: AuditableEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CTC { get; set; }
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
        public decimal ContributionToPf { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProfessionTax { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        #region ForeignKeyRelation

        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        #endregion

        #region Navigation Property
        public List<CtcOtherComponent> CtcOtherComponent { get; set; }
        public List<EmployeeSalary> EmployeeSalary { get; set; }
        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace A4.DAL.Entites.Maintenance
{
   public class SalaryPart : AuditableEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        #region ForeignKeyRelation
        public int EmployeeSalaryId { get; set; }
        [JsonIgnore]
        public EmployeeSalary EmployeeSalary { get; set; }
        public int CtcOtherComponentId { get; set; }
        [JsonIgnore]
        public CtcOtherComponent CtcOtherComponent { get; set; }

        #endregion
    }
}

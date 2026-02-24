
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace A4.DAL.Entites.Maintenance
{
   public class CtcOtherComponent : AuditableEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        #region ForeignKeyRelation
        public int EmployeeCtcId { get; set; }
        [JsonIgnore]
        public EmployeeCtc EmployeeCtc { get; set; }
        public int SalaryComponentId { get; set; }
        [JsonIgnore]
        public SalaryComponent SalaryComponent { get; set; }
        #endregion

        #region Navigation Property
        public List<SalaryPart> SalaryPart { get; set; }
        #endregion
    }
}

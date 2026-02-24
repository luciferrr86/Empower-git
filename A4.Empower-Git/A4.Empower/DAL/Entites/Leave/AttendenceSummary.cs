using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace A4.DAL.Entites.Leave
{
   public class AttendenceSummary : AuditableEntity
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DaysWorked { get; set; }
        public int LeaveTaken { get; set; }
        public int OnDuty { get; set; }
        public int WeeklyOff { get; set; }
        public int Holidays { get; set; }
        public int Unpaid { get; set; }
        public int PaidDays { get; set; }

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        #endregion
    }
}

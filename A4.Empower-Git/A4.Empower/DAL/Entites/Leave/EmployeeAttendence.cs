using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace A4.DAL.Entites.Leave
{
   public class EmployeeAttendence : AuditableEntity
    {
        public int Id { get; set; }
        public string PunchIn { get; set; }
        public string PunchOut { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public bool IsApproved { get; set; }
        public int? LeaveType { get; set; }

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        #endregion
    }
}

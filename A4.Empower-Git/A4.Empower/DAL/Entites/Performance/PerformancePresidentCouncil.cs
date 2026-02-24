using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{ 
   public class PerformancePresidentCouncil : AuditableEntity
    {
        #region Constructor
        public PerformancePresidentCouncil()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        public Guid PresidentId { get; set; }
        public Employee Employee { get; set; }


        public Guid PerformanceYearId { get; set; }
        public PerformanceYear PerformanceYear { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public bool IsPresidentCouncil { get; set; }
        #endregion
    }
}

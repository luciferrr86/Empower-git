using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceGoalMeasureIndiv : AuditableEntity
    {
        #region Constructor
        public PerformanceGoalMeasureIndiv()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid PerformanceGoalMeasureId { get; set; }
        public PerformanceGoalMeasure PerformanceGoalMeasure { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }        
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpGoalNextYear : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpGoalNextYear()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceEmpGoalId { get; set; }
        public PerformanceEmpGoal PerformanceEmpGoal { get; set; }

        public Guid PerformanceGoalMeasureId { get; set; }
        public PerformanceGoalMeasure PerformanceGoalMeasure { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        #endregion
    }
}

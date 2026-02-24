using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceGoalMeasureFunc : AuditableEntity
    {
        #region Constructor
        public PerformanceGoalMeasureFunc()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid FunctionalGroupId { get; set; }
        public FunctionalGroup FunctionalGroup { get; set; }

        public Guid PerformanceGoalMeasureId { get; set; }
        public PerformanceGoalMeasure PerformanceGoalMeasure { get; set; }
       
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public int Level { get; set; }       
        #endregion
    }
}

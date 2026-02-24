using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceConfig:AuditableEntity
    {
        #region Constructor
        public PerformanceConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation

        #endregion

        #region Properties
        public Guid Id { get; set; }

        public string MyGoalInstructionText { get; set; }
        public string PlusesInstructionText { get; set; }
        public string DeltaInstructionText { get; set; }
        public string TrainingClassesInstructionText { get; set; }
        public string CurrentYearInstructionText { get; set; }
        public string NextYearInstructionText { get; set; }
        public string CareerDevInstructionText { get; set; }
        public string RatingInstructionText { get; set; }
        public bool IsPerformanceStart { get; set; }
        #endregion
    }
}

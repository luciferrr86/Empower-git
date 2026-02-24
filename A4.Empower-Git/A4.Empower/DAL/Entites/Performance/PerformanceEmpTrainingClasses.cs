using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{ 
   public class PerformanceEmpTrainingClasses : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpTrainingClasses()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceEmpGoalId { get; set; }
        public PerformanceEmpGoal PerformanceEmpGoal { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string TrainingClasses { get; set; }       
        #endregion
    }
}

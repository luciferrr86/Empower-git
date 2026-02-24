using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpMidYearGoalDetail : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpMidYearGoalDetail()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceGoalMeasureId { get; set; }
        public PerformanceGoalMeasure PerformanceGoalMeasure { get; set; }

        public Guid PerformanceEmpMidYearGoalId { get; set; }
        public PerformanceEmpMidYearGoal PerformanceEmpMidYearGoal { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string EmployeeComment { get; set; }
        public string ManagerComment { get; set; }        
        #endregion
    }
}

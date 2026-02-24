using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class PerformanceGoal : AuditableEntity
    {
        #region Constructor
        public PerformanceGoal()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceGoalMeasure> _performanceGoalMeasure = new List<PerformanceGoalMeasure>();

        public ICollection<PerformanceGoalMeasure> PerformanceGoalMeasure
        {
            get => _performanceGoalMeasure;
            set => _performanceGoalMeasure = (value ?? new List<PerformanceGoalMeasure>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid PerformanceYearId { get; set; }
        public PerformanceYear PerformanceYear { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string GoalName { get; set; }
        #endregion
    }
}

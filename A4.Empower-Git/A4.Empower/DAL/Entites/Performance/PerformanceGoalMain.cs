using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class PerformanceGoalMain : AuditableEntity
    {
        #region Constructor
        public PerformanceGoalMain()
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
        public Guid ManagerId { get; set; }
        public Employee Employee { get; set; }

        public Guid PerformanceStatusId { get; set; }
        public PerformanceStatus PerformanceStatus { get; set; }

        public Guid PerformanceYearId { get; set; }
        public PerformanceYear PerformanceYear { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public bool IsManagerReleased { get; set; }
        public bool IsManagerActive { get; set; }
        #endregion
    }
}

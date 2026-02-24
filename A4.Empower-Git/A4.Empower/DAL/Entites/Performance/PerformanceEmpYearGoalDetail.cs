using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpYearGoalDetail : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpYearGoalDetail()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceEmpMidYearGoalDetail> _performanceEmpMidYearGoalDetail = new List<PerformanceEmpMidYearGoalDetail>();

        public ICollection<PerformanceEmpMidYearGoalDetail> PerformanceEmpMidYearGoalDetail
        {
            get => _performanceEmpMidYearGoalDetail;
            set => _performanceEmpMidYearGoalDetail = (value ?? new List<PerformanceEmpMidYearGoalDetail>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation

        public Guid PerformanceGoalMeasureId { get; set; }
        public PerformanceGoalMeasure PerformanceGoalMeasure { get; set; }

        public Guid PerformanceEmpYearGoalId { get; set; }
        public PerformanceEmpYearGoal PerformanceEmpYearGoal { get; set; }
       
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string EmployeeComment { get; set; }
        public string ManagerComment { get; set; }
        #endregion
    }
}

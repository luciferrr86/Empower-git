using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceStatus : AuditableEntity
    {
        #region Constructor
        public PerformanceStatus()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceGoalMain> _performanceGoalMain = new List<PerformanceGoalMain>();

        public ICollection<PerformanceGoalMain> PerformanceGoalMain
        {
            get => _performanceGoalMain;
            set => _performanceGoalMain = (value ?? new List<PerformanceGoalMain>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpYearGoal> _performanceEmpYearGoal = new List<PerformanceEmpYearGoal>();

        public ICollection<PerformanceEmpYearGoal> PerformanceEmpYearGoal
        {
            get => _performanceEmpYearGoal;
            set => _performanceEmpYearGoal = (value ?? new List<PerformanceEmpYearGoal>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpMidYearGoal> _performanceEmpMidYearGoal = new List<PerformanceEmpMidYearGoal>();

        public ICollection<PerformanceEmpMidYearGoal> PerformanceEmpMidYearGoal
        {
            get => _performanceEmpMidYearGoal;
            set => _performanceEmpMidYearGoal = (value ?? new List<PerformanceEmpMidYearGoal>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpGoal> _performanceEmpGoal = new List<PerformanceEmpGoal>();

        public ICollection<PerformanceEmpGoal> PerformanceEmpGoal
        {
            get => _performanceEmpGoal;
            set => _performanceEmpGoal = (value ?? new List<PerformanceEmpGoal>()).Where(p => p != null).ToList();
        }

        #endregion

        #region ForeignKeyRelation

        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string StatusText { get; set; }
        public string Type { get; set; }
        public string ColorCode { get; set; }
        #endregion
    }
}

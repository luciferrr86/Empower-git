using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceYear : AuditableEntity
    {
        #region Constructor
        public PerformanceYear()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceGoal> _performanceGoal = new List<PerformanceGoal>();

        public ICollection<PerformanceGoal> PerformanceGoal
        {
            get => _performanceGoal;
            set => _performanceGoal = (value ?? new List<PerformanceGoal>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceGoalMain> _performanceGoalMain = new List<PerformanceGoalMain>();

        public ICollection<PerformanceGoalMain> PerformanceGoalMain
        {
            get => _performanceGoalMain;
            set => _performanceGoalMain = (value ?? new List<PerformanceGoalMain>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceInitailRating> _performanceInitailRating = new List<PerformanceInitailRating>();

        public ICollection<PerformanceInitailRating> PerformanceInitailRating
        {
            get => _performanceInitailRating;
            set => _performanceInitailRating = (value ?? new List<PerformanceInitailRating>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpGoal> _performanceEmpGoal = new List<PerformanceEmpGoal>();

        public ICollection<PerformanceEmpGoal> PerformanceEmpGoal
        {
            get => _performanceEmpGoal;
            set => _performanceEmpGoal = (value ?? new List<PerformanceEmpGoal>()).Where(p => p != null).ToList();
        }      

        private ICollection<PerformancePresidentCouncil> _performancePresidentCouncil = new List<PerformancePresidentCouncil>();

        public ICollection<PerformancePresidentCouncil> PerformancePresidentCouncil
        {
            get => _performancePresidentCouncil;
            set => _performancePresidentCouncil = (value ?? new List<PerformancePresidentCouncil>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpInitialRating> _performanceEmpInitialRating = new List<PerformanceEmpInitialRating>();

        public ICollection<PerformanceEmpInitialRating> PerformanceEmpInitialRating
        {
            get => _performanceEmpInitialRating;
            set => _performanceEmpInitialRating = (value ?? new List<PerformanceEmpInitialRating>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation

        #endregion

        #region Properties
        public Guid Id { get; set; }
        public int NoOfEmployee { get; set; }
        public Guid NextYearId { get; set; }
        public string Year { get; set; }
        public bool IsYearActive { get; set; }
        public bool IsCompleted { get; set; }
        #endregion
    }
}

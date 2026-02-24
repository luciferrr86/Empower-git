using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpGoal : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpGoal()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        

      

        private PerformanceEmpYearGoal _performanceEmpYearGoal = new PerformanceEmpYearGoal();

        public PerformanceEmpYearGoal PerformanceEmpYearGoal
        {
            get => _performanceEmpYearGoal;
            set => _performanceEmpYearGoal = (value ?? new PerformanceEmpYearGoal());
        }

        private ICollection<PerformanceEmpDeltas> _performanceEmpDeltas = new List<PerformanceEmpDeltas>();

        public ICollection<PerformanceEmpDeltas> PerformanceEmpDeltas
        {
            get => _performanceEmpDeltas;
            set => _performanceEmpDeltas = (value ?? new List<PerformanceEmpDeltas>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpDevGoal> _performanceEmpDevGoal = new List<PerformanceEmpDevGoal>();

        public ICollection<PerformanceEmpDevGoal> PerformanceEmpDevGoal
        {
            get => _performanceEmpDevGoal;
            set => _performanceEmpDevGoal = (value ?? new List<PerformanceEmpDevGoal>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpDevGoalDoc> _performanceEmpDevGoalDoc = new List<PerformanceEmpDevGoalDoc>();

        public ICollection<PerformanceEmpDevGoalDoc> PerformanceEmpDevGoalDoc
        {
            get => _performanceEmpDevGoalDoc;
            set => _performanceEmpDevGoalDoc = (value ?? new List<PerformanceEmpDevGoalDoc>()).Where(p => p != null).ToList();
        }


        private ICollection<PerformanceEmpFeedback> _performanceEmpFeedback = new List<PerformanceEmpFeedback>();

        public ICollection<PerformanceEmpFeedback> PerformanceEmpFeedback
        {
            get => _performanceEmpFeedback;
            set => _performanceEmpFeedback = (value ?? new List<PerformanceEmpFeedback>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpPluses> _performanceEmpPluses = new List<PerformanceEmpPluses>();

        public ICollection<PerformanceEmpPluses> PerformanceEmpPluses
        {
            get => _performanceEmpPluses;
            set => _performanceEmpPluses = (value ?? new List<PerformanceEmpPluses>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceEmpTrainingClasses> _performanceEmpTrainingClasses = new List<PerformanceEmpTrainingClasses>();

        public ICollection<PerformanceEmpTrainingClasses> PerformanceEmpTrainingClasses
        {
            get => _performanceEmpTrainingClasses;
            set => _performanceEmpTrainingClasses = (value ?? new List<PerformanceEmpTrainingClasses>()).Where(p => p != null).ToList();
        }

        private PerformanceEmpGoalPresident _performanceEmpGoalPresident = new PerformanceEmpGoalPresident();

        public PerformanceEmpGoalPresident PerformanceEmpGoalPresident
        {
            get => _performanceEmpGoalPresident;
            set => _performanceEmpGoalPresident = (value ?? new PerformanceEmpGoalPresident());
        }       

        private ICollection<PerformanceEmpGoalNextYear> _performanceEmpGoalNextYear = new List<PerformanceEmpGoalNextYear>();

        public ICollection<PerformanceEmpGoalNextYear> PerformanceEmpGoalNextYear
        {
            get => _performanceEmpGoalNextYear;
            set => _performanceEmpGoalNextYear = (value ?? new List<PerformanceEmpGoalNextYear>()).Where(p => p != null).ToList();
        }


        #endregion

        #region ForeignKeyRelation
        public Guid EmployeeId { get; set; }
        public Guid ManagerId { get; set; }
        public Employee Employee { get; set; }

        public Guid PerformanceYearId { get; set; }
        public PerformanceYear PerformanceYear { get; set; }

        public Guid PerformanceStatusId { get; set; }
        public PerformanceStatus PerformanceStatus { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string ManagerSignature { get; set; }
        #endregion
    }
}

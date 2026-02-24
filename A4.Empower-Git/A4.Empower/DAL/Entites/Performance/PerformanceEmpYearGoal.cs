using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpYearGoal : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpYearGoal()
        {
            Id =  Guid.NewGuid();
        }
        #endregion
        #region Child
        private ICollection<PerformanceEmpYearGoalDetail> _performanceEmpYearGoalDetail = new List<PerformanceEmpYearGoalDetail>();

        public ICollection<PerformanceEmpYearGoalDetail> PerformanceEmpYearGoalDetail
        {
            get => _performanceEmpYearGoalDetail;
            set => _performanceEmpYearGoalDetail = (value ?? new List<PerformanceEmpYearGoalDetail>()).Where(p => p != null).ToList();
        }
        #endregion
        #region ForeignKeyRelation       

        public Guid PerformanceStatusId { get; set; }
        public PerformanceStatus PerformanceStatus { get; set; }

        public Guid PerformanceEmpGoalId { get; set; }
        public PerformanceEmpGoal PerformanceEmpGoal { get; set; }

        #endregion

        #region Properties        
        public Guid Id { get; set; }
        public string EmployeeAccComment { get; set; }
        public string ManagerAccComment { get; set; }
        public string EmployeeSignature { get; set; }        
        public string FinalRating { get; set; }
        public bool IsEmployeeGoalSaved { get; set; }
        public bool IsEmployeeGoalSubmitted { get; set; }
        public bool IsManagerGoalSaved { get; set; }
        public bool IsManagerGoalSubmitted { get; set; }
        public bool IsEmployeeRatingSaved { get; set; }
        public bool IsManagerRatingSaved { get; set; }
        public bool IsEmployeeRatingSubmitted { get; set; }
        public bool IsManagerRatingSubmitted { get; set; }
        public bool IsReviewCompleted { get; set; }
        public bool IsEmployeeActive { get; set; }
        public bool IsEmployeeDeltaPlusSaved { get; set; }
        public bool IsEmployeeDeltaPlusSubmitted { get; set; }
        public bool IsManagerDeltaPlusSaved { get; set; }
        public bool IsManagerDeltaPlusSubmitted { get; set; }
        public bool IsEmployeeTrainingSaved { get; set; }
        public bool IsEmployeeTrainingSubmitted { get; set; }
        public bool IsManagerTrainingSaved { get; set; }
        public bool IsManagerTrainingSubmitted { get; set; }
        public bool IsEmployeeDevGoalSaved { get; set; }
        public bool IsEmployeeDevGoalSubmitted { get; set; }
        public bool IsManagerDevGoalSaved { get; set; }
        public bool IsManagerDevGoalSubmitted { get; set; }
        #endregion
    }
}

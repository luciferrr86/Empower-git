using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpMidYearGoal : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpMidYearGoal()
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
        public bool IsMidYearReviewCompleted { get; set; }
        #endregion
    }
}

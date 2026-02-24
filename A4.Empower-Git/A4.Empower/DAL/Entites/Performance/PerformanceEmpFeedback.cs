using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpFeedback : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpFeedback()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child
        private ICollection<PerformanceEmpFeedbackDetail> _performanceEmpFeedbackDetail = new List<PerformanceEmpFeedbackDetail>();

        public ICollection<PerformanceEmpFeedbackDetail> PerformanceEmpFeedbackDetail
        {
            get => _performanceEmpFeedbackDetail;
            set => _performanceEmpFeedbackDetail = (value ?? new List<PerformanceEmpFeedbackDetail>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceEmpGoalId { get; set; }
        public PerformanceEmpGoal PerformanceEmpGoal { get; set; }

        public Guid FeedBackEmpId { get; set; }
        public Employee Employee { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string FeedbackuserName { get; set; }
        public string EmailId { get; set; }
        public bool IsMailSent { get; set; }
        public bool IsSubmitted { get; set; }
        #endregion
    }
}

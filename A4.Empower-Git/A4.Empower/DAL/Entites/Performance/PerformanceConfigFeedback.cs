using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
  public class PerformanceConfigFeedback : AuditableEntity
    {
        #region Constructor
        public PerformanceConfigFeedback()
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

        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string LabelText { get; set; }
        public string Description { get; set; }
        #endregion
    }
}

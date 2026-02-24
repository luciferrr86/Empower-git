using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class PerformanceEmpFeedbackDetail : AuditableEntity
    {
        #region Constructor
        public PerformanceEmpFeedbackDetail()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Child

        #endregion

        #region ForeignKeyRelation
        public Guid PerformanceEmpFeedbackId { get; set; }
        public PerformanceEmpFeedback PerformanceEmpFeedback { get; set; }

        public Guid PerformanceConfigFeedbackId { get; set; }
        public PerformanceConfigFeedback PerformanceConfigFeedback { get; set; }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string Comment { get; set; }
        #endregion
    }
}

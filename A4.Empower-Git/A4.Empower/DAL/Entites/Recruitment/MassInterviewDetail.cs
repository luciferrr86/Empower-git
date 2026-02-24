using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
  public class MassInterviewDetail:AuditableEntity
    {
        #region Constructor

        public MassInterviewDetail()
        {
            Id = Guid.NewGuid();
        }

        #endregion
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #region ForeignKeyRelation
        public Guid MassInterviewId { get; set; }
        public MassInterview MassInterview { get; set; }
        #endregion

        public ICollection<MassInterviewPanel> MassInterviewPanel { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class MassInterviewScheduleCandidate
    {
        #region Constructor
        public MassInterviewScheduleCandidate()
        {
            Id = Guid.NewGuid();
        }
        #endregion


        #region ForeignKeyRelation

        public Guid CandidateId { get; set; }

        public JobCandidateProfile JobCandidateProfile { get; set; }

        public Guid RoomId { get; set; }

        public MassInterviewRoom MassInterviewRoom { get; set;}

        public Guid PanelId { get; set; }

        public MassInterviewPanel MassInterviewPanel { get; set; }

        #endregion


        #region Properties
        public DateTime InterviewDate { get; set; }

        public DateTime InterviewTime { get; set; }

        public Guid Id { get; set; }
        #endregion

    }
}

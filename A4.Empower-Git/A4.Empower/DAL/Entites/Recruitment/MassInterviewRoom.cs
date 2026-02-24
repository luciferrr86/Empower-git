using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class MassInterviewRoom:AuditableEntity
    {
        #region Constructor

        public MassInterviewRoom()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Property
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        #endregion



        #region Children

        public ICollection<MassInterviewScheduleCandidate> MassInterviewScheduleCandidate { get; set; }

        #endregion
    }
}

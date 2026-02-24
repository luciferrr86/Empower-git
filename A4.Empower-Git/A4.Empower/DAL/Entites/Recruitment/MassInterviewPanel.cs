using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class MassInterviewPanel
    {
        #region Constructor
        public MassInterviewPanel()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid VacancyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancy JobVacancy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid InterviewDateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MassInterviewDetail MassInterviewDetail { get; set; }

        #endregion

        #region Property

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string BreakStartTime { get; set; }
        public string BreakEndTime { get; set; }
        #endregion

        #region Children
        public ICollection<MassInterviewScheduleCandidate> MassInterviewScheduleCandidate { get; set; }
        public ICollection<MassInterviewPanelVacancy> MassInterviewPanelVaccany { get; set; }
        #endregion
    }
}

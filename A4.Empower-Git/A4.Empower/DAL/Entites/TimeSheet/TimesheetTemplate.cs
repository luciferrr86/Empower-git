using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetTemplate : AuditableEntity
    {
        #region Constructor

        public TimesheetTemplate()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKey

        /// <summary>
        /// 
        /// </summary>
        public Guid TimesheetFrequencyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetConfiguration TimesheetConfiguration { get; set; }

        #endregion


        #region Children

        private ICollection<TimesheetUserSchedule> _timesheetUserSchedule = new List<TimesheetUserSchedule>();

        public ICollection<TimesheetUserSchedule>  TimesheetUserSchedule
        {
            get => _timesheetUserSchedule;
            set=>_timesheetUserSchedule=(value ?? new List<TimesheetUserSchedule>()).Where(p => p != null).ToList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TempalteName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ScheduleType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Monday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Tuesday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Wednesday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Thursday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Friday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Saturday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Sunday { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetWorkingDays : AuditableEntity
    {
        #region Constructor

        public TimesheetWorkingDays()
        {
            Id = Guid.NewGuid();
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
        public string WorkingDay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WorkingDayValue { get; set; }

        #endregion
    }
}

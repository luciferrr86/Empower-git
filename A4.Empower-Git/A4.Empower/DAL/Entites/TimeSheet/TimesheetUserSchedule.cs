using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetUserSchedule : AuditableEntity
    {
        #region Constructor

        public TimesheetUserSchedule()
        {
            Id = Guid.NewGuid(); 
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid TimesheetTemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetTemplate TimesheetTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }
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
        public string ScheduleType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

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

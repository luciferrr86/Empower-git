using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetUserDetailProjectHour : AuditableEntity
    {

        #region Constructor

        public TimesheetUserDetailProjectHour()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetProject TimesheetProject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TimesheetUserDetailId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetUserDetail TimesheetUserDetail { get; set; }

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
        public string Hour { get; set; }

        #endregion

    }
}

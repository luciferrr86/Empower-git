using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetEmployeeProject : AuditableEntity
    {
        #region Constructor

        public TimesheetEmployeeProject()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid TimeSheetProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetProject TimesheetProject { get; set; }

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

        #endregion
    }
}

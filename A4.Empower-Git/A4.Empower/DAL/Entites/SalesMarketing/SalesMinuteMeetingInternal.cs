using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesMinuteMeetingInternal : AuditableEntity
    {
        #region Constructor
        public SalesMinuteMeetingInternal()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid SalesMinuteMeetingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesMinuteMeeting SalesMinuteMeeting { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        #endregion
    }
}

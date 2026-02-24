using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesScheduleMeetingInternal : AuditableEntity
    {
        #region Constructor
        public SalesScheduleMeetingInternal()
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
        public Guid SalesScheduleMeetingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesScheduleMeeting SalesScheduleMeeting { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        #endregion
    }
}

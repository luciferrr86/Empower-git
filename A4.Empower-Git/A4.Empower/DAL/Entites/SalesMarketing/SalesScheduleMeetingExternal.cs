using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesScheduleMeetingExternal : AuditableEntity
    {
        #region Constructor
        public SalesScheduleMeetingExternal()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid? SalesCompanyContactId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesCompanyContact SalesCompanyContact { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid SalesScheduleMeetingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesScheduleMeeting SalesScheduleMeeting {get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        #endregion
    }
}

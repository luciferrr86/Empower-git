using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesDocument : AuditableEntity
    {
        #region Constructor
        public SalesDocument()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation
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
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

     
        #endregion
    }
}

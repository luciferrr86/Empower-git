using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesMinuteMeeting : AuditableEntity
    {
        #region Constructor
        public SalesMinuteMeeting()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<SalesMinuteMeetingInternal> _salesMinuteMeetingInternal = new List<SalesMinuteMeetingInternal>();
        public ICollection<SalesMinuteMeetingInternal> SalesMinuteMeetingInternal
        {
            get => _salesMinuteMeetingInternal;
            set => _salesMinuteMeetingInternal = (value ?? new List<SalesMinuteMeetingInternal>()).Where(p => p != null).ToList();
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
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ActionDescription { get; set; }
        #endregion
    }
}

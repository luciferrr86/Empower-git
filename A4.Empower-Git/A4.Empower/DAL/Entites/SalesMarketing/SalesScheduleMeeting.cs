using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public  class SalesScheduleMeeting : AuditableEntity
    {
        #region Constructor
        public SalesScheduleMeeting()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<SalesDocument> _salesDocument = new List<SalesDocument>();
        public ICollection<SalesDocument> SalesDocument
        {
            get => _salesDocument;
            set => _salesDocument = (value ?? new List<SalesDocument>()).Where(p => p != null).ToList();
        }

        private ICollection<SalesScheduleMeetingExternal> _salesScheduleMeetingExternal = new List<SalesScheduleMeetingExternal>();
        public ICollection<SalesScheduleMeetingExternal> SalesScheduleMeetingExternal
        {
            get => _salesScheduleMeetingExternal;
            set => _salesScheduleMeetingExternal = (value ?? new List<SalesScheduleMeetingExternal>()).Where(p => p != null).ToList();
        }

        private ICollection<SalesScheduleMeetingInternal> _salesScheduleMeetingInternal = new List<SalesScheduleMeetingInternal>();
        public ICollection<SalesScheduleMeetingInternal> SalesScheduleMeetingInternal
        {
            get => _salesScheduleMeetingInternal;
            set => _salesScheduleMeetingInternal = (value ?? new List<SalesScheduleMeetingInternal>()).Where(p => p != null).ToList();
        }


        private ICollection<SalesMinuteMeeting> _salesMinuteMeeting = new List<SalesMinuteMeeting>();
        public ICollection<SalesMinuteMeeting> SalesMinuteMeeting
        {
            get => _salesMinuteMeeting;
            set => _salesMinuteMeeting = (value ?? new List<SalesMinuteMeeting>()).Where(p => p != null).ToList();
        }

        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid SalesCompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SalesCompany SalesCompany { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Venue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Writer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Agenda { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ScheduleDate { get; set; }

        public int Document { get; set; }

        #endregion
    }
}

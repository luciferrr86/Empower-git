using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetUserDetail :AuditableEntity
    {
        #region Constructor

        public TimesheetUserDetail()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid ApproverlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TimesheetUserSpanId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimesheetUserSpan TimesheetUserSpan { get; set; }

        #endregion

        #region Children

        private ICollection<TimesheetUserDetailProjectHour> _timesheetUserDetailProjectHour = new List<TimesheetUserDetailProjectHour>();

        public ICollection<TimesheetUserDetailProjectHour> TimesheetUserDetailProjectHour
        {
            get => _timesheetUserDetailProjectHour;
            set => _timesheetUserDetailProjectHour = (value ?? new List<TimesheetUserDetailProjectHour>()).Where(p => p != null).ToList();
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
        public string TotalHour { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TimeSheetDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bManagerApproved { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bIsUserSaved { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bIsUserSubmit { get; set; }

        #endregion
    }
}

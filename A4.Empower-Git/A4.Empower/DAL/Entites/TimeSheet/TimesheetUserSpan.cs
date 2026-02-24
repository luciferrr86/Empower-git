using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class TimesheetUserSpan : AuditableEntity
    {
        #region Constructor

        public TimesheetUserSpan()
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

        #endregion

        #region Children

        private ICollection<TimesheetUserDetail> _timesheetUserDetail = new List<TimesheetUserDetail>();

        public ICollection<TimesheetUserDetail> TimesheetUserDetail
        {
            get => _timesheetUserDetail;
            set => _timesheetUserDetail = (value ?? new List<TimesheetUserDetail>()).Where(p => p != null).ToList();
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
        public DateTime WeekStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime WeekEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalHour { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bIsUserSubmit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bIsManagerApprove { get; set; }

        #endregion
    }
}

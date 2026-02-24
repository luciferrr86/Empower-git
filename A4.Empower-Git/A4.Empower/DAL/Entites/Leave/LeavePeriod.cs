using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class LeavePeriod : AuditableEntity
    {

        #region Constructor
        public LeavePeriod()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<LeaveHolidayList> _leaveholidayList = new List<LeaveHolidayList>();

        public ICollection<LeaveHolidayList> LeaveHolidayList
        {
            get => _leaveholidayList;
            set => _leaveholidayList = (value ?? new List<LeaveHolidayList>()).Where(p => p != null).ToList();
        }

        private ICollection<LeaveWorkingDay> _leaveWorkingDay = new List<LeaveWorkingDay>();

        public ICollection<LeaveWorkingDay> LeaveWorkingDay
        {
            get => _leaveWorkingDay;
            set => _leaveWorkingDay = (value ?? new List<LeaveWorkingDay>()).Where(p => p != null).ToList();
        }

        private ICollection<LeaveType> _leaveType = new List<LeaveType>();
        public ICollection<LeaveType> LeaveType
        {
            get => _leaveType;
            set => _leaveType = (value ?? new List<LeaveType>()).Where(p => p != null).ToList();
        }

        private ICollection<LeaveRules> _leaveRules = new List<LeaveRules>();
        public ICollection<LeaveRules> LeaveRules
        {
            get => _leaveRules;
            set => _leaveRules = (value ?? new List<LeaveRules>()).Where(p => p != null).ToList();
        }



        #endregion

        #region Navigation Property
        public virtual ICollection<EmployeeLeaves> EmployeeLeaves { get; set; }
        public virtual ICollection<EmployeeLeaveDetail> EmployeeLeaveDetail { get; set; }
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
        public DateTime PeriodStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime PeriodEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsLeavePeriodCompleted { get; set; }
        #endregion
    }
}

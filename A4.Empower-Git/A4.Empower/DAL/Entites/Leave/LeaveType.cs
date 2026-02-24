using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public  class LeaveType : AuditableEntity
    {
        #region Constructor
        public LeaveType()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children
        private ICollection<LeaveRules> _leaveRules = new List<LeaveRules>();
        public ICollection<LeaveRules> LeaveRules
        {
            get => _leaveRules;
            set => _leaveRules = (value ?? new List<LeaveRules>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid LeavePeriodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LeavePeriod LeavePeriod { get; set; }
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
        /// 
        public string ColorCode { get; set; }     
        #endregion
    }
}

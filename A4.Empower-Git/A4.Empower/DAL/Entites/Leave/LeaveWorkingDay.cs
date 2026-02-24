using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class LeaveWorkingDay : AuditableEntity
    {
        #region Constructor
        public LeaveWorkingDay()
        {
            Id = Guid.NewGuid();
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
        /// 
        public string WorkingDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WorkingDayValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LocalOrder { get; set; }
        #endregion
    }
}

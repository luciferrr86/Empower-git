using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class LeaveHolidayList : AuditableEntity
    {
        #region Constructor
        public LeaveHolidayList()
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
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Holidaydate { get; set; }
        #endregion
    }
}

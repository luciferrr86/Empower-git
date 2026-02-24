using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class EmployeeLeaveDetail : AuditableEntity
    {
        #region Constructor
        public EmployeeLeaveDetail()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid LeaveStatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LeaveStatus LeaveStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid LeavePeriodId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LeavePeriod LeavePeriod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public Employee Manager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid LeavesEntitlementId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmployeeLeavesEntitlement EmployeeLeavesEntitlement { get; set; }
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
        public DateTime LeaveStartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LeaveEndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ReasonForApply { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid Approvedby { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSubmitted { get; set; }        
        /// <summary>
        /// 
        /// </summary>
        public string ManagerComment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSave { get; set; }
        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
  public  class EmployeeLeavesEntitlement : AuditableEntity
    {
        #region Constructor
        public EmployeeLeavesEntitlement()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children 
        private ICollection<EmployeeLeaveDetail> _employeeLeaveDetail = new List<EmployeeLeaveDetail>();

        public ICollection<EmployeeLeaveDetail> EmployeeLeaveDetail
        {
            get => _employeeLeaveDetail;
            set => _employeeLeaveDetail = (value ?? new List<EmployeeLeaveDetail>()).Where(p => p != null).ToList();
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeLeavesId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmployeeLeaves EmployeeLeaves { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid LeaveRulesId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LeaveRules LeaveRules { get; set; }
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
        public string Approved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Pending { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Rejected { get; set; }
        #endregion
    }
}

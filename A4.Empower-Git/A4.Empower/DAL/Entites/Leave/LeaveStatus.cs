using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class LeaveStatus : AuditableEntity
    {
        #region Constructor
        public LeaveStatus()
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

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        #endregion
    }
}

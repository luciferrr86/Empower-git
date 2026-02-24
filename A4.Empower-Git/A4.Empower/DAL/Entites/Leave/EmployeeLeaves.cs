using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class EmployeeLeaves : AuditableEntity
    {
        #region Constructor
        public EmployeeLeaves()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children 
        private ICollection<EmployeeLeavesEntitlement> _employeeLeavesEntitlement = new List<EmployeeLeavesEntitlement>();

        public ICollection<EmployeeLeavesEntitlement> EmployeeLeavesEntitlement
        {
            get => _employeeLeavesEntitlement;
            set => _employeeLeavesEntitlement = (value ?? new List<EmployeeLeavesEntitlement>()).Where(p => p != null).ToList();
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
        /// <summary>
        /// 
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }
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

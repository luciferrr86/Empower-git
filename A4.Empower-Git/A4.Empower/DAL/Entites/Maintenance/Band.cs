using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class Band : AuditableEntity
    {
        public Band()
        {
            Id = Guid.NewGuid();
        }
        #region Children     

        private ICollection<Employee> _employee = new List<Employee>();

        public ICollection<Employee> Employee
        {
            get => _employee;
            set => _employee = (value ?? new List<Employee>()).Where(p => p != null).ToList();
        }

        private ICollection<LeaveRules> _leaveRules = new List<LeaveRules>();
        public ICollection<LeaveRules> LeaveRules
        {
            get => _leaveRules;
            set => _leaveRules = (value ?? new List<LeaveRules>()).Where(p => p != null).ToList();
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
        public string YearsOfExperience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LocalOrder { get; set; }
        #endregion
    }
}

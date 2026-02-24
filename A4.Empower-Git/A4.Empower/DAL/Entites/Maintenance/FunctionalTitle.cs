using System;
using System.Collections.Generic;
using System.Linq;

namespace A4.DAL.Entites
{
  public class FunctionalTitle : AuditableEntity
    {
        #region Constructor
        public FunctionalTitle()
        {
            Id = Guid.NewGuid();
            //IsActive = true;
        }
        #endregion

        #region Children

        private ICollection<Employee> _employee = new List<Employee>();

        public ICollection<Employee> Employee
        {
            get => _employee;
            set => _employee = (value ?? new List<Employee>()).Where(p => p != null).ToList();
        }

        public ExpenseBookingTitleAmount ExpenseBookingTitleAmount { get; set; }

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

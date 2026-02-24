using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace A4.DAL.Entites
{
   public class FunctionalDesignation: AuditableEntity
    {
        #region Constructor
        public FunctionalDesignation()
        {
            Id = new Guid();
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
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Designation Required")]
        public string Name { get; set; }
        #endregion
    }
}

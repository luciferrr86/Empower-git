using System;
using System.Collections.Generic;
using System.Linq;

namespace A4.DAL.Entites
{
  public class FunctionalGroup : AuditableEntity
    {

        #region Constructor
        public FunctionalGroup()
        {
            Id = new Guid();
           
            //IsActive = true;
        }
        #endregion

        #region ForeignKeyRelation
        /// <summary>
        /// 
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FunctionalDepartment FunctionalDepartment { get; set; }
        #endregion

        #region Children

        private ICollection<Employee> _employee = new List<Employee>();

        public ICollection<Employee> Employee
        {
            get => _employee;
            set => _employee = (value ?? new List<Employee>()).Where(p => p != null).ToList();
        }

        private ICollection<PerformanceGoalMeasureFunc> _performanceGoalMeasureFunc = new List<PerformanceGoalMeasureFunc>();

        public ICollection<PerformanceGoalMeasureFunc> PerformanceGoalMeasureFunc
        {
            get => _performanceGoalMeasureFunc;
            set => _performanceGoalMeasureFunc = (value ?? new List<PerformanceGoalMeasureFunc>()).Where(p => p != null).ToList();
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

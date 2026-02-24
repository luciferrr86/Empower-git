using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobVacancyLevelManager : AuditableEntity
    {
        #region Constructor

        public JobVacancyLevelManager()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelaton

        /// <summary>
        /// 
        /// </summary>
        public Guid JobVacancyLevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancyLevel JobVacancyLevel { get; set; }

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

        #endregion

        public ICollection<JobCandidateInterview> JobCandidateInterview { get; set; }
    }
}

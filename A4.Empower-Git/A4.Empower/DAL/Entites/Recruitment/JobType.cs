using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
   public class JobType : AuditableEntity
    {
        #region Constructor
        public JobType()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Children

  

        private ICollection<JobVacancy> _jobVacancy = new List<JobVacancy>();

        public ICollection<JobVacancy> JobVacancy
        {
            get => _jobVacancy;
            set => _jobVacancy = (value ?? new List<JobVacancy>()).Where(p => p != null).ToList();
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

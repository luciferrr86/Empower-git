using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobVacancyLevel : AuditableEntity
    {
        #region Constructor

        public JobVacancyLevel()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelaton

        /// <summary>
        /// 
        /// </summary>
        public Guid JobVacancyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancy JobVacancy { get; set; }


        public ICollection<JobVacancyLevelManager> JobVacancyLevelManagers { get; set; }


        public ICollection<JobVacancyLevelSkillQuestion> JobVacancyLevelSkillQuestion { get; set; }


        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        #endregion

    }
}

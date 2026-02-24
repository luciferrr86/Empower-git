using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobSkillQuestion : AuditableEntity
    {
        #region Constructor

        public JobSkillQuestion()
        {
            Id = Guid.NewGuid();
        }

        #endregion
        public Guid JobVacancyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancy JobVacancy { get; set; }

        #region Properties

        public Guid Id { get; set; }

        public string Question { get; set; }

        public int Weightage { get; set; }

        public IList<JobVacancyLevelSkillQuestion> JobVacancyLevelSkillQuestions { get; set; }
        #endregion
    }
}

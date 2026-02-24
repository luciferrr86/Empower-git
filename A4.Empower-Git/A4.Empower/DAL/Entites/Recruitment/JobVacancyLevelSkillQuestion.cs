using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobVacancyLevelSkillQuestion:AuditableEntity
    {

        public JobVacancyLevelSkillQuestion()
        {
            Id = Guid.NewGuid();
        }
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
        public Guid JobSkillQuestionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobSkillQuestion JobSkillQuestion { get; set; }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        #endregion

        public ICollection<JobApplicationSkillQuestion> JobApplicationSkillQuestions { get; set; }
    }
}

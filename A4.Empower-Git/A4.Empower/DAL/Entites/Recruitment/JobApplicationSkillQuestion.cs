using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobApplicationSkillQuestion:AuditableEntity
    {

        #region Constructor

        public JobApplicationSkillQuestion()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelaton

        /// <summary>
        /// 
        /// </summary>
        public Guid JobCandidateInterviewId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobCandidateInterview JobCandidateInterviews { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Guid JobVacancyLevelSkillQuestionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancyLevelSkillQuestion JobVacancyLevelSkillQuestions { get; set; }


        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }


        public string ObtainedWeightage { get; set; }

        public string Weightage { get; set; }

        [NotMapped]
        public string Question { get; set; }
        #endregion
    }
}

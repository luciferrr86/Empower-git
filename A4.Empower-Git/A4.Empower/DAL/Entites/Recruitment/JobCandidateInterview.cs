using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobCandidateInterview:AuditableEntity
    {
        #region Constructor

        public JobCandidateInterview()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid JobApplicationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobApplication JobApplication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid JobInterviewTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobInterviewType JobInterviewType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid JobVacancyLevelManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancyLevelManager JobVacancyLevelManagers { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InterviewerComment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInterviewCompleted { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool IsLevelCompleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCandidateSelected { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string InterviewMode { get; set; }

        public ICollection<JobApplicationSkillQuestion> JobApplicationSkillQuestions { get; set; }

        #endregion
    }
}

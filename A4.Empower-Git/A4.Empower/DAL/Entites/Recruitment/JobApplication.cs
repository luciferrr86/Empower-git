using A4.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobApplication: AuditableEntity
    {
        #region Constructor

        public JobApplication()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation
         
        /// <summary>
        /// 
        /// </summary>
        public Guid JobCandidateProfileId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobCandidateProfile JobCandidateProfiles { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public int JobStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid JobVacancyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobVacancy JobVacancy { get; set; }
        #endregion

        #region Children

        public ICollection<JobCandidateInterview> JobCandidateInterviews { get; set; }

        public ICollection<JobApplicationScreeningQuestion> JobApplicationScreeningQuestions { get; set; }

        public ICollection<JobApplicationHRQuestions> JobApplicationHRQuestions { get; set; }

        
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HRComment { get; set; }

        public string ScreeningScore { get; set; }

        public string HRScore { get; set; }

        public string SkillScore { get; set; }

        public string Feedback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string OverallScore { get; set; }


        public Guid LevelId { get; set; }

        #endregion
    }
}

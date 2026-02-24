using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using A4.DAL.Entites;

namespace A4.DAL.Entites
{
    public class JobVacancy : AuditableEntity
    {
        #region Constructor
        public JobVacancy()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region ForeignKeyRelation

        /// <summary>
        /// 
        /// </summary>
        public Guid JobTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobType JobType { get; set; }

        #endregion

        #region Children


        public ICollection<JobVacancyLevel> JobVacancyLevel { get; set; }

        public ICollection<JobHRQuestion> JobHRQuestions { get; set; }

        public ICollection<JobScreeningQuestion> JobScreeningQuestions { get; set; }

        public ICollection<JobSkillQuestion> JobSkillQuestions { get; set; }

        public ICollection<JobApplication> JobApplication { get; set; }

        public ICollection<MassInterviewPanel> MassInterviewPanel { get; set; }

      

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NoOfvacancies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PublishedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JobTitle { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string JobLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Experience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SalaryRange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JobRequirements { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool bIsClosed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bIsPublished { get; set; }

        public string JDReson { get; set; }

        #endregion
    }

}

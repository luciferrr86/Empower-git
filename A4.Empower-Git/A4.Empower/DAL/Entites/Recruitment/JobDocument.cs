using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
   public class JobDocument : AuditableEntity
    {
        #region Constructor

        public JobDocument()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Children 

        private JobCandidateProfile _jobCandidateProfile = new JobCandidateProfile();

        public JobCandidateProfile JobCandidateProfile
        {
            get => _jobCandidateProfile;
            set => _jobCandidateProfile = (value ?? new JobCandidateProfile());
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

        /// <summary>
        /// 
        /// </summary>
        public string Document { get; set; }

        #endregion
    }
}

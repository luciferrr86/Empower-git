using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobCandidateWorkExperience : AuditableEntity
    {
        public JobCandidateWorkExperience()
        {
            Id = new Guid();
        }
        #region Properties
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string ProfileDesc { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOR { get; set; }
        #endregion

        #region ForeignKeyRelation

        public Guid JobCandidateProfilesId { get; set; }
        public JobCandidateProfile JobCandidateProfiles { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace A4.DAL.Entites
{
    public class JobCandidateProfile : AuditableEntity
    {
        public JobCandidateProfile()
        {
            Id = new Guid();
        }

        #region Properties
        public Guid Id { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DOB { get; set; }
        public string IdProofDetail { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Nationality { get; set; }
        public string OfficialContactNo { get; set; }
        public string SkillSet { get; set; }
        public string CoverLetter { get; set; }
        public bool IsCompleted { get; set; }
        #endregion

        #region Children

        public JobCandidateQualification JobCandidateQualification { get; set; }

        public ICollection<JobCandidateWorkExperience> JobCandidateWorkExperience { get; set; }

        public ICollection<JobApplication> JobApplication { get; set; }

        #endregion

        #region ForeignKeyRelation
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Picture")]
        public int? ResumeId { get; set; }

        public Picture Picture { get; set; }
        #endregion

        #region NotMapped
        [NotMapped]
        public Guid ApplicationId { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string ScreeningScore { get; set; }

        [NotMapped]
        public string HRScore { get; set; }

        [NotMapped]
        public string SkillScore { get; set; }

        [NotMapped]
        public string Email { get; set; }

        [NotMapped]
        public string PhoneNo { get; set; }

        [NotMapped]
        public Guid JobVacancyId { get; set; }

        [NotMapped]
        public string VacancyName { get; set; }

        [NotMapped]
        public string ApplicationType { get; set; }

        [NotMapped]
        public string OverAllScore { get; set; }

        [NotMapped]
        public Guid JobStatusId { get; set; }

        [NotMapped]
        public DateTime AppliedDate { get; set; }

        [NotMapped]
        public string JobType { get; set; }

        [NotMapped]
        public Guid LevelId { get; set; }

        [NotMapped]
        public string JobStatus { get; set; }


        [NotMapped]
        public Guid InterviewId { get; set; }

        [NotMapped]
        public string InterviewTime { get; set; }

        [NotMapped]
        public DateTime InterviewDate { get; set; }
        #endregion
    }
}

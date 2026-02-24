using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobCandidateWorkExperienceMap : IEntityTypeConfiguration<JobCandidateWorkExperience>
    {
        public void Configure(EntityTypeBuilder<JobCandidateWorkExperience> builder)
        {
            builder.HasOne(m => m.JobCandidateProfiles).WithMany(m => m.JobCandidateWorkExperience).HasForeignKey(m => m.JobCandidateProfilesId).HasConstraintName("ForeignKey_WorkExperience_JobCandidateProfile").OnDelete(DeleteBehavior.Cascade);
            throw new NotImplementedException();
        }
    }
}

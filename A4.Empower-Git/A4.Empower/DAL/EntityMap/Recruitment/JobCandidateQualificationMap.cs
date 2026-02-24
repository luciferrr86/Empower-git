using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobCandidateQualificationMap : IEntityTypeConfiguration<JobCandidateQualification>
    {
        public void Configure(EntityTypeBuilder<JobCandidateQualification> builder)
        {
            builder.HasOne(m => m.JobCandidateProfile).WithOne(m=>m.JobCandidateQualification).HasForeignKey<JobCandidateQualification>(m => m.JobCandidateProfilesId).HasConstraintName("ForeignKey_JobQualification_JobCandidateProfile").OnDelete(DeleteBehavior.Cascade);
        }
    }
}

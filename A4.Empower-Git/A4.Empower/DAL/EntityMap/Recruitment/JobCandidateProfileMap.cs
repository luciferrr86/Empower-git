using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobCandidateProfileMap : IEntityTypeConfiguration<JobCandidateProfile>
    {
        public void Configure(EntityTypeBuilder<JobCandidateProfile> builder)
        {
            // throw new NotImplementedException();
           builder.HasOne(m => m.ApplicationUser).WithOne().HasForeignKey<JobCandidateProfile>(m => m.UserId).HasConstraintName("ForeignKey_ApplicationUser_CandidateProfile").OnDelete(DeleteBehavior.Cascade);
           //builder.HasOne(m=>m.Picture).WithOne().HasForeignKey<JobCandidateProfile>(m=>m.ResumeId).HasConstraintName("ForeignKey_JobCandidateProfile_Resume").OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobCandidateInterviewMap : IEntityTypeConfiguration<JobCandidateInterview>
    {
        public void Configure(EntityTypeBuilder<JobCandidateInterview> builder)
        {
            builder.HasOne(m => m.JobApplication).WithMany(m => m.JobCandidateInterviews).HasForeignKey(m => m.JobApplicationId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.JobVacancyLevelManagers).WithMany(m => m.JobCandidateInterview).HasForeignKey(m => m.JobVacancyLevelManagerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

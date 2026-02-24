using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class JobApplicationMap : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            
            //builder.HasOne(m => m.Employee).WithMany(u => u.JobApplication).HasForeignKey(m => m.EmployeeId);
            //builder.HasOne(m => m.JobStatus).WithMany(u => u.JobApplication).HasForeignKey(m => m.JobStatusId);
            //builder.HasOne(m => m.JobVacancy).WithMany(u => u.JobApplication).HasForeignKey(m => m.JobVacancyId);
           
            builder.HasOne(m => m.JobCandidateProfiles).WithMany(u => u.JobApplication).HasForeignKey(m => m.JobCandidateProfileId).HasConstraintName("Foreign_JobApplication_ApplicationUser").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.JobVacancy).WithMany(u => u.JobApplication).HasForeignKey(m => m.JobVacancyId).HasConstraintName("Foreign_JobApplication_JobVacancy").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("JobApplication");
        }
    }
}

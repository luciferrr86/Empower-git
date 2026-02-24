using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class JobVacancyMap : IEntityTypeConfiguration<JobVacancy>
    {
        public void Configure(EntityTypeBuilder<JobVacancy> builder)
        {
            builder.HasOne(m => m.JobType).WithMany(u => u.JobVacancy).HasForeignKey(m => m.JobTypeId).HasConstraintName("ForeignKey_JobVacancy_JobType");
            builder.ToTable("JobVacancy");
        }
    }
}

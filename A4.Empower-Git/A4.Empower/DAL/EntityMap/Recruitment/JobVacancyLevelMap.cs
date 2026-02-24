using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobVacancyLevelMap : IEntityTypeConfiguration<JobVacancyLevel>
    {
        public void Configure(EntityTypeBuilder<JobVacancyLevel> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(450);
            builder.HasIndex(u => u.Name).IsUnique();
            builder.ToTable("JobVacancyLevel");
        }
    }
}

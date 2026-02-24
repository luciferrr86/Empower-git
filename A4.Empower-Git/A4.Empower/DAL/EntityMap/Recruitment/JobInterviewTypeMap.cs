using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class JobInterviewTypeMap : IEntityTypeConfiguration<JobInterviewType>
    {
        public void Configure(EntityTypeBuilder<JobInterviewType> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.ToTable("JobInterviewType");
        }
    }
}

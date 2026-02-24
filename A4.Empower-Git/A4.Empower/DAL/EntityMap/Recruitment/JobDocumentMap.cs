using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class JobDocumentMap : IEntityTypeConfiguration<JobDocument>
    {
        public void Configure(EntityTypeBuilder<JobDocument> builder)
        {
            builder.ToTable("JobDocument");
        }
    }
}

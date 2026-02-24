using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class ApplicationModuleDetailMap : IEntityTypeConfiguration<ApplicationModuleDetail>
    {
        public void Configure(EntityTypeBuilder<ApplicationModuleDetail> builder)
        {
            builder.HasOne(m => m.ApplicationModule).WithMany(m => m.ApplicationModuleDetail).HasForeignKey(m => m.ApplicationModuleId).HasConstraintName("ForeignKey_ApplicationModuleDetail_ApplicationModule").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("ApplicationModuleDetail");
        }
    }
}

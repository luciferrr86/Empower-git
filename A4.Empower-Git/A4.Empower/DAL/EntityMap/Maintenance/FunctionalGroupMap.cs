using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class FunctionalGroupMap : IEntityTypeConfiguration<FunctionalGroup>
    {
        public void Configure(EntityTypeBuilder<FunctionalGroup> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.HasOne(m => m.FunctionalDepartment).WithMany(m => m.Group).HasForeignKey(m => m.DepartmentId).HasConstraintName("ForeignKey_Group_Department");
            builder.ToTable("FunctionalGroup");
        }
    }
}

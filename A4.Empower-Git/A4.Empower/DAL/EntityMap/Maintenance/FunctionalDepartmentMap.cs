using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class FunctionalDepartmentMap : IEntityTypeConfiguration<FunctionalDepartment>
    {
        public void Configure(EntityTypeBuilder<FunctionalDepartment> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.ToTable("FunctionalDepartment");
        }
    }
}

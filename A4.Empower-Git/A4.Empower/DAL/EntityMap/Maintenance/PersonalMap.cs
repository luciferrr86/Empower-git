using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
  public class PersonalMap : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            builder.HasOne(m => m.Employee).WithOne(m => m.Personal).HasForeignKey<Personal>(m=>m.EmployeeId).HasConstraintName("ForeignKey_Personal_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("EmployeePersonalDetail");
        }
    }
}

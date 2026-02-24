using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    class QualificationMap : IEntityTypeConfiguration<Qualification>
    {
        public void Configure(EntityTypeBuilder<Qualification> builder)
        {
            builder.HasOne(m => m.Employee).WithOne(m => m.Qualification).HasForeignKey<Qualification>(m => m.EmployeeId).HasConstraintName("ForeignKey_Qualification_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("EmployeeQualificationDetail");
        }
    }
}

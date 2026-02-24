using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformancePresidentCouncilMap : IEntityTypeConfiguration<PerformancePresidentCouncil>
    {
        public void Configure(EntityTypeBuilder<PerformancePresidentCouncil> builder)
        {
            //builder.HasOne(m => m.Employee).WithMany(u => u.PerformancePresidentCouncil).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_PerformancePresidentCouncil_Employee").OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(m => m.Employee).WithMany(u => u.PerformancePresidentCouncil).HasForeignKey(m => m.PresidentId).HasConstraintName("ForeignKey_PerformancePresidentCouncil_Employee1").OnDelete(DeleteBehavior.Cascade);            
            builder.HasOne(m => m.PerformanceYear).WithMany(u => u.PerformancePresidentCouncil).HasForeignKey(m => m.PerformanceYearId).HasConstraintName("ForeignKey_PerformancePresidentCouncil_PerformanceYear").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformancePresidentCouncil");
        }
    }
}

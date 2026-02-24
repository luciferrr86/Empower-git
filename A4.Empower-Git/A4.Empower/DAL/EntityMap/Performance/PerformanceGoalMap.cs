using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceGoalMap : IEntityTypeConfiguration<PerformanceGoal>
    {
        public void Configure(EntityTypeBuilder<PerformanceGoal> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceGoal).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_PerformanceGoal_Employee").OnDelete(DeleteBehavior.Cascade);           
            builder.HasOne(m => m.PerformanceYear).WithMany(u => u.PerformanceGoal).HasForeignKey(m => m.PerformanceYearId).HasConstraintName("ForeignKey_PerformanceGoal_PerformanceYear").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformanceGoal");
        }
    }
}

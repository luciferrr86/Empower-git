using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceGoalMeasureIndivMap : IEntityTypeConfiguration<PerformanceGoalMeasureIndiv>
    {
        public void Configure(EntityTypeBuilder<PerformanceGoalMeasureIndiv> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceGoalMeasureIndiv).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_PerformanceGoalMeasureIndiv_Employee").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.PerformanceGoalMeasure).WithMany(u => u.PerformanceGoalMeasureIndiv).HasForeignKey(m => m.PerformanceGoalMeasureId).HasConstraintName("ForeignKey_PerformanceGoalMeasureIndiv_PerformanceGoalMeasure").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("PerformanceGoalMeasureIndiv");
        }
    }
}

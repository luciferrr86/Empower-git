using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceGoalMainMap : IEntityTypeConfiguration<PerformanceGoalMain>
    {
        public void Configure(EntityTypeBuilder<PerformanceGoalMain> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceGoalMain).HasForeignKey(m => m.ManagerId).HasConstraintName("ForeignKey_PerformanceGoalMain_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.PerformanceStatus).WithMany(u => u.PerformanceGoalMain).HasForeignKey(m => m.PerformanceStatusId).HasConstraintName("ForeignKey_PerformanceGoalMain_PerformanceStatus").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.PerformanceYear).WithMany(u => u.PerformanceGoalMain).HasForeignKey(m => m.PerformanceYearId).HasConstraintName("ForeignKey_PerformanceGoalMain_PerformanceYear").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformanceGoalMain");
        }
    }
}

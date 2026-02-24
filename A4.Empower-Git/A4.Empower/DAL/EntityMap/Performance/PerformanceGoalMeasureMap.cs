using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class PerformanceGoalMeasureMap : IEntityTypeConfiguration<PerformanceGoalMeasure>
    {
        public void Configure(EntityTypeBuilder<PerformanceGoalMeasure> builder)
        {
            builder.HasOne(m => m.PerformanceGoal).WithMany(u => u.PerformanceGoalMeasure).HasForeignKey(m => m.PerformanceGoalId).HasConstraintName("ForeignKey_PerformanceGoalMeasure_PerformanceGoal").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.PerformanceGoalMain).WithMany(u => u.PerformanceGoalMeasure).HasForeignKey(m => m.PerformanceGoalMainId).HasConstraintName("ForeignKey_PerformanceGoalMeasure_PerformanceGoalMain").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("PerformanceGoalMeasure");
        }
    }
}

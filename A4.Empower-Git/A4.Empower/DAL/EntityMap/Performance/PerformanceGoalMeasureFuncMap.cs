using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceGoalMeasureFuncMap : IEntityTypeConfiguration<PerformanceGoalMeasureFunc>
    {
        public void Configure(EntityTypeBuilder<PerformanceGoalMeasureFunc> builder)
        {
            builder.HasOne(m => m.FunctionalGroup).WithMany(u => u.PerformanceGoalMeasureFunc).HasForeignKey(m => m.FunctionalGroupId).HasConstraintName("ForeignKey_PerformanceGoalMeasureFunc_FunctionalGroup").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.PerformanceGoalMeasure).WithMany(u => u.PerformanceGoalMeasureFunc).HasForeignKey(m => m.PerformanceGoalMeasureId).HasConstraintName("ForeignKey_PerformanceGoalMeasureFunc_PerformanceGoalMeasure").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("PerformanceGoalMeasureFunc");
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpGoalNextYearMap : IEntityTypeConfiguration<PerformanceEmpGoalNextYear>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpGoalNextYear> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpGoalNextYear).HasForeignKey(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpGoalNextYear_PerformanceEmpGoal").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.PerformanceGoalMeasure).WithMany(u => u.PerformanceEmpGoalNextYear).HasForeignKey(m => m.PerformanceGoalMeasureId).HasConstraintName("ForeignKey_PerformanceEmpGoalNextYear_PerformanceGoalMeasure").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformanceEmpGoalNextYear");
        }
    }
}

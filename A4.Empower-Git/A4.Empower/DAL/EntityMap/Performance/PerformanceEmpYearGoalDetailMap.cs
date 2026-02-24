using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpYearGoalDetailMap : IEntityTypeConfiguration<PerformanceEmpYearGoalDetail>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpYearGoalDetail> builder)
        {
            builder.HasOne(m => m.PerformanceEmpYearGoal).WithMany(u => u.PerformanceEmpYearGoalDetail).HasForeignKey(m => m.PerformanceEmpYearGoalId).HasConstraintName("ForeignKey_PerformanceEmpYearGoalDetail_PerformanceEmpYearGoal").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.PerformanceGoalMeasure).WithMany(u => u.PerformanceEmpYearGoalDetail).HasForeignKey(m => m.PerformanceGoalMeasureId).HasConstraintName("ForeignKey_PerformanceEmpYearGoalDetail_PerformanceGoalMeasure").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformanceEmpYearGoalDetail");
        }
    }
}

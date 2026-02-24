using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpMidYearGoalDetailMap : IEntityTypeConfiguration<PerformanceEmpMidYearGoalDetail>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpMidYearGoalDetail> builder)
        {
            builder.HasOne(m => m.PerformanceGoalMeasure).WithMany(u => u.PerformanceEmpMidYearGoalDetail).HasForeignKey(m => m.PerformanceGoalMeasureId).HasConstraintName("ForeignKey_PerformanceEmpMidYearGoalDetail_PerformanceGoalMeasure").OnDelete(DeleteBehavior.Cascade); ;
            builder.HasOne(m => m.PerformanceEmpMidYearGoal).WithMany(u => u.PerformanceEmpMidYearGoalDetail).HasForeignKey(m => m.PerformanceEmpMidYearGoalId).HasConstraintName("ForeignKey_PerformanceEmpMidYearGoalDetail_PerformanceEmpMidYearGoal").OnDelete(DeleteBehavior.Restrict); ;
            builder.ToTable("PerformanceEmpMidYearGoalDetail");
        }
    }
}

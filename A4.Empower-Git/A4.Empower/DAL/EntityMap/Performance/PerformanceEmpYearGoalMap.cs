using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpYearGoalMap : IEntityTypeConfiguration<PerformanceEmpYearGoal>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpYearGoal> builder)
        {
            builder.HasOne(m => m.PerformanceStatus).WithMany(u => u.PerformanceEmpYearGoal).HasForeignKey(m => m.PerformanceStatusId).HasConstraintName("ForeignKey_PerformanceEmpYearGoal_PerformanceStatus").OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(m => m.PerformanceEmpGoal).WithOne(u => u.PerformanceEmpYearGoal).HasForeignKey<PerformanceEmpYearGoal>(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpYearGoal_PerformanceEmpGoal").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("PerformanceEmpYearGoal");
        }
    }
}

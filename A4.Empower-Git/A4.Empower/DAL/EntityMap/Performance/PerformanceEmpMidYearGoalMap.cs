using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class PerformanceEmpMidYearGoalMap : IEntityTypeConfiguration<PerformanceEmpMidYearGoal>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpMidYearGoal> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithOne().HasForeignKey<PerformanceEmpMidYearGoal>(m => m.PerformanceEmpGoalId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.PerformanceStatus).WithMany(u => u.PerformanceEmpMidYearGoal).HasForeignKey(m => m.PerformanceStatusId).HasConstraintName("ForeignKey_PerformanceEmpMidYearGoal_PerformanceStatus").OnDelete(DeleteBehavior.Restrict);           
            builder.ToTable("PerformanceEmpMidYearGoal");
        }
    }
}

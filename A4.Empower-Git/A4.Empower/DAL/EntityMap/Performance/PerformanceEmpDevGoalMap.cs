using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpDevGoalMap : IEntityTypeConfiguration<PerformanceEmpDevGoal>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpDevGoal> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpDevGoal).HasForeignKey(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpDevGoal_PerformanceEmpGoal");
            builder.ToTable("PerformanceEmpDevGoal");
        }
    }
}

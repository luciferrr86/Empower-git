using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
  public class PerformanceEmpDeltasMap : IEntityTypeConfiguration<PerformanceEmpDeltas>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpDeltas> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpDeltas).HasForeignKey(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpDeltas_PerformanceEmpGoal");
            builder.ToTable("PerformanceEmpDeltas");
        }
    }
}

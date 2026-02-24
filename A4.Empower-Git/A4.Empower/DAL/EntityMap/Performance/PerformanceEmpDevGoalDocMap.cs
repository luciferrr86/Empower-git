using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpDevGoalDocMap : IEntityTypeConfiguration<PerformanceEmpDevGoalDoc>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpDevGoalDoc> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpDevGoalDoc).HasForeignKey(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpDevGoalDoc_PerformanceEmpGoal");
            builder.ToTable("PerformanceEmpDevGoalDoc");
        }
    }
}

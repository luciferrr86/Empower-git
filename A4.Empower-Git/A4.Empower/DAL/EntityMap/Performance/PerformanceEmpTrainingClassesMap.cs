using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpTrainingClassesMap : IEntityTypeConfiguration<PerformanceEmpTrainingClasses>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpTrainingClasses> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpTrainingClasses).HasForeignKey(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpTrainingClasses_PerformanceEmpGoal");
            builder.ToTable("PerformanceEmpTrainingClasses");
        }
    }
}

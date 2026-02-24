using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpGoalPresidentMap : IEntityTypeConfiguration<PerformanceEmpGoalPresident>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpGoalPresident> builder)
        {           
            builder.HasOne(m => m.PerformanceEmpGoal).WithOne(u => u.PerformanceEmpGoalPresident).HasForeignKey<PerformanceEmpGoalPresident>(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpGoalPresident_PerformanceEmpGoal");           
            builder.ToTable("PerformanceEmpGoalPresident");
        }
    }
}

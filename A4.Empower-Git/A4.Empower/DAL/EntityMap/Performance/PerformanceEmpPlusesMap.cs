using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpPlusesMap : IEntityTypeConfiguration<PerformanceEmpPluses>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpPluses> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpPluses).HasForeignKey(m => m.PerformanceEmpGoalId).HasConstraintName("ForeignKey_PerformanceEmpPluses_PerformanceEmpGoal");          
            builder.ToTable("PerformanceEmpPluses");
        }
    }
}

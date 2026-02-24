using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpGoalMap : IEntityTypeConfiguration<PerformanceEmpGoal>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpGoal> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceEmpGoal).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_PerformanceEmpGoal_Employee").OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceEmpGoal).HasForeignKey(m => m.ManagerId).HasConstraintName("ForeignKey_PerformanceEmpGoal_EmployeeMgr").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.PerformanceYear).WithMany(u => u.PerformanceEmpGoal).HasForeignKey(m => m.PerformanceYearId).HasConstraintName("ForeignKey_PerformanceEmpGoal_PerformanceYear").OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(m=>m.PerformanceStatus).WithMany(u=>u.PerformanceEmpGoal).HasForeignKey(m => m.PerformanceStatusId).HasConstraintName("ForeignKey_PerformanceEmpGoal_PerformanceStatus").OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(m => m.PerformanceEmpYearGoal).WithOne(u => u.PerformanceEmpGoal).HasForeignKey<PerformanceEmpYearGoal>(m => m.PerformanceEmpGoalId).OnDelete(DeleteBehavior.Cascade);
          

            builder.ToTable("PerformanceEmpGoal");
        }
    }
}

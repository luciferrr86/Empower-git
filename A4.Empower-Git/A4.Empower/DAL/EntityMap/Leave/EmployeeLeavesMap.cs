using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public  class EmployeeLeavesMap : IEntityTypeConfiguration<EmployeeLeaves>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaves> builder)
        {
          
            builder.HasOne(m => m.LeavePeriod).WithMany(m => m.EmployeeLeaves).HasForeignKey(m => m.LeavePeriodId).HasConstraintName("ForeignKey_EmployeeLeaves_LeavePeriod");
            //builder.HasOne(m => m.Employee).WithMany(m => m.EmployeeLeaves).HasForeignKey<EmployeeLeaves>(m => m.EmployeeId).HasConstraintName("ForeignKey_EmployeeLeaves_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Employee).WithMany(m => m.EmployeeLeaves).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_EmployeeLeaves_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("EmployeeLeaves");
            
        }
    }
}

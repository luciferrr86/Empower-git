using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
  public  class EmployeeLeaveDetailMap : IEntityTypeConfiguration<EmployeeLeaveDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeaveDetail> builder)
        {

            builder.HasOne(m => m.LeaveStatus).WithMany(m => m.EmployeeLeaveDetail).HasForeignKey(m => m.LeaveStatusId).HasConstraintName("ForeignKey_EmployeeLeaveDetail_LeaveStatus");
            builder.HasOne(m => m.LeavePeriod).WithMany(m => m.EmployeeLeaveDetail).HasForeignKey(m => m.LeavePeriodId).HasConstraintName("ForeignKey_EmployeeLeaveDetail_LeavePeriod").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Employee).WithMany(m => m.EmployeeLeaveDetailEmployee).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_EmployeeLeaveDetail_Employee").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Manager).WithMany(m => m.EmployeeLeaveDetailManager).HasForeignKey(m => m.ManagerId).HasConstraintName("ForeignKey_EmployeeLeaveDetail_Manager").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.EmployeeLeavesEntitlement).WithMany(m => m.EmployeeLeaveDetail).HasForeignKey(m => m.LeavesEntitlementId).HasConstraintName("ForeignKey_EmployeeLeaveDetail_EmployeeLeavesEntitlement").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("EmployeeLeaveDetail");
        }
    }
}

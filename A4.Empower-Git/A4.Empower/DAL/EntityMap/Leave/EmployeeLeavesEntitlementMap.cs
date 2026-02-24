using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class EmployeeLeavesEntitlementMap : IEntityTypeConfiguration<EmployeeLeavesEntitlement>
    {
        public void Configure(EntityTypeBuilder<EmployeeLeavesEntitlement> builder)
        {
            builder.HasOne(m => m.EmployeeLeaves).WithMany(m => m.EmployeeLeavesEntitlement).HasForeignKey(m => m.EmployeeLeavesId).HasConstraintName("ForeignKey_EmployeeLeavesEntitlement_EmployeeLeaves").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.LeaveRules).WithMany(m => m.EmployeeLeavesEntitlement).HasForeignKey(m => m.LeaveRulesId).HasConstraintName("ForeignKey_EmployeeLeavesEntitlement_LeaveRules").OnDelete(DeleteBehavior.Restrict);          
            builder.ToTable("EmployeeLeavesEntitlement");
        }
    }
}

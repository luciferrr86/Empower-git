using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class LeaveRulesMap : IEntityTypeConfiguration<LeaveRules>
    {
        public void Configure(EntityTypeBuilder<LeaveRules> builder)
        {
            //builder.HasIndex(u => u.Name).IsUnique();
            builder.HasOne(m => m.LeavePeriod).WithMany(m => m.LeaveRules).HasForeignKey(m => m.LeavePeriodId).HasConstraintName("ForeignKey_LeaveRules_LeavePeriod").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.LeaveType).WithMany(m => m.LeaveRules).HasForeignKey(m => m.LeaveTypeId).HasConstraintName("ForeignKey_LeaveRules_LeaveType").OnDelete(DeleteBehavior.Restrict);
           builder.HasOne(m => m.Band).WithMany(m => m.LeaveRules).HasForeignKey(m => m.BandId).HasConstraintName("ForeignKey_LeaveRules_Band");
            builder.ToTable("LeaveRules");
        }
    }
}

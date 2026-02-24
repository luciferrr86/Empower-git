using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{ 
   public class LeaveTypeMap :  IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
           // builder.HasIndex(u => u.Name).IsUnique();
            builder.HasOne(m => m.LeavePeriod).WithMany(m => m.LeaveType).HasForeignKey(m => m.LeavePeriodId).HasConstraintName("ForeignKey_LeaveType_LeavePeriod");
            builder.ToTable("LeaveType");
        }
    }
}

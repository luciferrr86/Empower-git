using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class LeaveWorkingDayMap : IEntityTypeConfiguration<LeaveWorkingDay>
    {
        public void Configure(EntityTypeBuilder<LeaveWorkingDay> builder)
        {
         
            builder.HasOne(m => m.LeavePeriod).WithMany(m => m.LeaveWorkingDay).HasForeignKey(m => m.LeavePeriodId).HasConstraintName("ForeignKey_WorkingDay_LeavePeriod");
            builder.ToTable("LeaveWorkingDay");
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetUserDetailMap : IEntityTypeConfiguration<TimesheetUserDetail>
    {
        public void Configure(EntityTypeBuilder<TimesheetUserDetail> builder)
        {
            builder.HasOne(m => m.TimesheetUserSpan).WithMany(u => u.TimesheetUserDetail).HasForeignKey(m => m.TimesheetUserSpanId).HasConstraintName("ForeignKey_TimesheetUserDetail_TimesheetUserSpan").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Employee).WithMany(u => u.TimesheetUserDetail).HasForeignKey(m => m.ApproverlId).HasConstraintName("ForeignKey_TimesheetUserDetail_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("TimesheetUserDetail");
        }
    }
}

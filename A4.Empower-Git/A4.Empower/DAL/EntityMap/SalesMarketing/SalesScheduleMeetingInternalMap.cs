using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesScheduleMeetingInternalMap : IEntityTypeConfiguration<SalesScheduleMeetingInternal>
    {
        public void Configure(EntityTypeBuilder<SalesScheduleMeetingInternal> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(m => m.SalesScheduleMeetingInternal).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_SalesScheduleMeetingInternal_Employee");
            builder.HasOne(m => m.SalesScheduleMeeting).WithMany(m => m.SalesScheduleMeetingInternal).HasForeignKey(m => m.SalesScheduleMeetingId).HasConstraintName("ForeignKey_SalesMinuteMeetingInternal_SalesScheduleMeeting").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("SalesScheduleMeetingInternal");
        }
    }
}

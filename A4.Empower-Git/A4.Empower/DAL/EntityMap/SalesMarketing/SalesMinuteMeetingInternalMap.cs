using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesMinuteMeetingInternalMap : IEntityTypeConfiguration<SalesMinuteMeetingInternal>
    {
        public void Configure(EntityTypeBuilder<SalesMinuteMeetingInternal> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(m => m.SalesMinuteMeetingInternal).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_SalesMinuteMeetingInternal_Employee");
            builder.HasOne(m => m.SalesMinuteMeeting).WithMany(m => m.SalesMinuteMeetingInternal).HasForeignKey(m => m.SalesMinuteMeetingId).HasConstraintName("ForeignKey_SalesMinuteMeetingInternal_SalesMinuteMeeting");
            builder.ToTable("SalesMinuteMeetingInternal");
        }
    }
}

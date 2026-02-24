using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesScheduleMeetingExternalMap : IEntityTypeConfiguration<SalesScheduleMeetingExternal>
    {
        public void Configure(EntityTypeBuilder<SalesScheduleMeetingExternal> builder)
        {
           // builder.HasOne(m => m.SalesCompanyContact).WithMany(m => m.SalesScheduleMeetingExternal).HasForeignKey(m => m.SalesCompanyContactId).HasConstraintName("ForeignKey_SalesMinuteMeetingInternal_SalesCompanyContact");
            builder.HasOne(m => m.SalesScheduleMeeting).WithMany(m => m.SalesScheduleMeetingExternal).HasForeignKey(m => m.SalesScheduleMeetingId).HasConstraintName("ForeignKey_SalesScheduleMeetingExternal_SalesScheduleMeeting").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("SalesScheduleMeetingExternal");
        }
    }
}

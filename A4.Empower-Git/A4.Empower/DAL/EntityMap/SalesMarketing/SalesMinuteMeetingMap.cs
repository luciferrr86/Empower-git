using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesMinuteMeetingMap : IEntityTypeConfiguration<SalesMinuteMeeting>
    {
        public void Configure(EntityTypeBuilder<SalesMinuteMeeting> builder)
        {
            builder.HasOne(m => m.SalesScheduleMeeting).WithMany(m => m.SalesMinuteMeeting).HasForeignKey(m => m.SalesScheduleMeetingId).HasConstraintName("ForeignKey_SalesMinuteMeeting_SalesScheduleMeeting").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("SalesMinuteMeeting");
        }
    }
}

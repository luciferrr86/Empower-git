using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetUserDetailProjectHourMap : IEntityTypeConfiguration<TimesheetUserDetailProjectHour>
    {
        public void Configure(EntityTypeBuilder<TimesheetUserDetailProjectHour> builder)
        {
            builder.HasOne(m => m.TimesheetProject).WithMany(u => u.TimesheetUserDetailProjectHour).HasForeignKey(m => m.ProjectId).HasConstraintName("ForeignKey_TimesheetUserDetailProjectHour_Project").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.TimesheetUserDetail).WithMany(u => u.TimesheetUserDetailProjectHour).HasForeignKey(m => m.TimesheetUserDetailId).HasConstraintName("ForeignKey_TimesheetUserDetailProjectHour_TimesheetUserDetail").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("TimesheetUserDetailProjectHour");
        }
    }
}

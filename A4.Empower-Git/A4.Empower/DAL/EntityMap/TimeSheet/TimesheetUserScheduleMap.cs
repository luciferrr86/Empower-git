using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetUserScheduleMap : IEntityTypeConfiguration<TimesheetUserSchedule>
    {
        public void Configure(EntityTypeBuilder<TimesheetUserSchedule> builder)
        {
            builder.HasOne(m => m.TimesheetTemplate).WithMany(u => u.TimesheetUserSchedule).HasForeignKey(m => m.TimesheetTemplateId).HasConstraintName("ForeignKey_TimesheetUserSchedule_TimesheetTemplate").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Employee).WithMany(u => u.TimesheetUserSchedule).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_TimesheetUserSchedule_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("TimesheetUserSchedule");
        }
    }
}

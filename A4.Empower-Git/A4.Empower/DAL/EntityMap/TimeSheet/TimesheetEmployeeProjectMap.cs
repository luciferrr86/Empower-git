using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetEmployeeProjectMap : IEntityTypeConfiguration<TimesheetEmployeeProject>
    {
        public void Configure(EntityTypeBuilder<TimesheetEmployeeProject> builder)
        {
            builder.HasOne(m=>m.TimesheetProject).WithMany(u=>u.TimesheetEmployeeProject).HasForeignKey(m=>m.TimeSheetProjectId).HasConstraintName("ForeignKey_TimesheetEmployeeProject_TimeSheetProject").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Employee).WithMany(u => u.TimesheetEmployeeProject).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_TimesheetEmployeeProject_Employee").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("TimesheetEmployeeProject");
        }
    }
}

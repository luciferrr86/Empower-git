using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetProjectMap : IEntityTypeConfiguration<TimesheetProject>
    {
        public void Configure(EntityTypeBuilder<TimesheetProject> builder)
        {
            builder.HasIndex(m => m.ProjectName).IsUnique();
            builder.HasOne(m=>m.Employee).WithMany(u=>u.TimesheetProject).HasForeignKey(m=>m.ManagerId).HasConstraintName("ForeignKey_TimesheetProject_Employee").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.TimesheetClient).WithMany(u => u.TimesheetProject).HasForeignKey(m => m.ClientId).HasConstraintName("ForeignKey_TimesheetProject_TimesheetClient").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("TimesheetProject");
        }
    }
}

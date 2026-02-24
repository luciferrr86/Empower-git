using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetUserSpanMap : IEntityTypeConfiguration<TimesheetUserSpan>
    {
        public void Configure(EntityTypeBuilder<TimesheetUserSpan> builder)
        {
            builder.HasOne(m => m.Employee).WithMany(u => u.TimesheetUserSpan).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_TimesheetUserSpan_Employee").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("TimesheetUserSpan");
        }
    }
}

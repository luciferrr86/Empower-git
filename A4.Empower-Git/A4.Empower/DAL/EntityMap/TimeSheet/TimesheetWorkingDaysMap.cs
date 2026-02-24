using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetWorkingDaysMap : IEntityTypeConfiguration<TimesheetWorkingDays>
    {
        public void Configure(EntityTypeBuilder<TimesheetWorkingDays> builder)
        {
            builder.ToTable("TimesheetWorkingDays");
        }
    }
}

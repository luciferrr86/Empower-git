using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetClientMap : IEntityTypeConfiguration<TimesheetClient>
    {
        public void Configure(EntityTypeBuilder<TimesheetClient> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.HasIndex(u => u.Contact).IsUnique();
            builder.HasIndex(u => u.EmailId).IsUnique();
            builder.ToTable("TimesheetClient");
        }
    }
}

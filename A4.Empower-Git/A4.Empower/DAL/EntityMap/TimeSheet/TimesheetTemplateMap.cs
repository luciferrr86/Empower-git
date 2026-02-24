using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class TimesheetTemplateMap : IEntityTypeConfiguration<TimesheetTemplate>
    {
        public void Configure(EntityTypeBuilder<TimesheetTemplate> builder)
        {
            builder.HasIndex(m => m.TempalteName).IsUnique();
            builder.HasOne(m => m.TimesheetConfiguration).WithMany(u => u.TimesheetTemplate).HasForeignKey(m => m.TimesheetFrequencyId).HasConstraintName("ForeignKey_TimesheetTemplate_TimesheetConfiguration").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("TimesheetTemplate");
        }
    }
}

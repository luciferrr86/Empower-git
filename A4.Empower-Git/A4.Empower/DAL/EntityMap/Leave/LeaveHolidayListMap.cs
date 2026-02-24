using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class LeaveHolidayListMap : IEntityTypeConfiguration<LeaveHolidayList>
    {
        public void Configure(EntityTypeBuilder<LeaveHolidayList> builder)
        {
            //builder.HasIndex(u => u.Name).IsUnique();
         //   builder.HasIndex(u =>u.Holidaydate).IsUnique();
            builder.HasOne(m => m.LeavePeriod).WithMany(m => m.LeaveHolidayList).HasForeignKey(m => m.LeavePeriodId).HasConstraintName("ForeignKey_HolidayList_LeavePeriod");
            builder.ToTable("LeaveHolidayList");
        }
    }
}

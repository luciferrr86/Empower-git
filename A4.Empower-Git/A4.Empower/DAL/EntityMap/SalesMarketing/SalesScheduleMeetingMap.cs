using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesScheduleMeetingMap : IEntityTypeConfiguration<SalesScheduleMeeting>
    {
        public void Configure(EntityTypeBuilder<SalesScheduleMeeting> builder)
        {
          
            builder.HasOne(m => m.SalesCompany).WithMany(m => m.SalesScheduleMeeting).HasForeignKey(m => m.SalesCompanyId).HasConstraintName("ForeignKey_SalesScheduleMeeting_SalesCompany");
            builder.ToTable("SalesScheduleMeeting");
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesDocumentMap : IEntityTypeConfiguration<SalesDocument>
    {
        public void Configure(EntityTypeBuilder<SalesDocument> builder)
        {
            builder.HasOne(m => m.SalesScheduleMeeting).WithMany(m => m.SalesDocument).HasForeignKey(m => m.SalesScheduleMeetingId).HasConstraintName("ForeignKey_SalesDocument_SalesCompany").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("SalesDocument");
        }
    }
}

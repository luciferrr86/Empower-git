using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class ExpenseDocumentMap : IEntityTypeConfiguration<ExpenseDocument>
    {
        public void Configure(EntityTypeBuilder<ExpenseDocument> builder)
        {
            builder.ToTable("ExpenseDocument");
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Picture)
                .WithMany()
                .HasForeignKey(u => u.PictureId);


            builder.HasOne(u => u.ExpenseBookingRequest)
                .WithMany(u => u.ExpenseDocument)
                .HasForeignKey(u => u.ExpenseBookingId);
        }
    }
}

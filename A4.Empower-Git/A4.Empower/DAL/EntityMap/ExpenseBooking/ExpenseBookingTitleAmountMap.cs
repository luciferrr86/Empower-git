using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class ExpenseBookingTitleAmountMap : IEntityTypeConfiguration<ExpenseBookingTitleAmount>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingTitleAmount> builder)
        {
            builder.HasIndex(u => u.TitleID).IsUnique();
            builder.ToTable("ExpenseBookingTitleAmount");
        }
    }
}

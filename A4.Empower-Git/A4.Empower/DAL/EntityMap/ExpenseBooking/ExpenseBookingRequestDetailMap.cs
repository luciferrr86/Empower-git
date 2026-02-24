using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    internal class ExpenseBookingRequestDetailMap : IEntityTypeConfiguration<ExpenseBookingRequestDetail>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingRequestDetail> builder)
        {
            builder.ToTable("ExpenseBookingRequestDetails");
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.ExpenseBookingApprovers).WithMany(m => m.ExpenseBookingRequestDetails).HasForeignKey(m => m.ExpenseBookingApproverId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

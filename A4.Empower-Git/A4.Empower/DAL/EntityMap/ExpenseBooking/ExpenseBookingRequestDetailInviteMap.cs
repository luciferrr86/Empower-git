using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    internal class ExpenseBookingRequestDetailInviteMap : IEntityTypeConfiguration<ExpenseBookingRequestDetailInvite>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingRequestDetailInvite> builder)
        {
            builder.ToTable("ExpenseBookingRequestDetailInvite");
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.ExpenseBookingInviteApprovers).WithMany(m => m.ExpenseBookingRequestDetailInvites).HasForeignKey(m => m.ExpenseBookingInviteApproverId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

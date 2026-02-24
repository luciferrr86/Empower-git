using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    internal class ExpenseBookingApproverMap : IEntityTypeConfiguration<ExpenseBookingApprover>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingApprover> builder)
        {
            builder.ToTable("ExpenseBookingApprover");
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Manager).WithMany(m => m.ExpenseBookingRequestManager).HasForeignKey(m => m.ManagerId).HasConstraintName("ForeignKey_BookingRequest_Manager").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.ExpenseBookingRequests).WithMany(m => m.ExpenseBookingApprovers).HasForeignKey(m => m.ExpenseBookingRequestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

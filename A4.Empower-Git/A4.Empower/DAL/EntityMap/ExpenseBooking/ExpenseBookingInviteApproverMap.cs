using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    internal class ExpenseBookingInviteApproverMap : IEntityTypeConfiguration<ExpenseBookingInviteApprover>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingInviteApprover> builder)
        {
            builder.ToTable("ExpenseBookingInviteApprover");
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Employee).WithMany(m => m.ExpenseBookingInviteManager).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_BookingRequest_Invite_Manager").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.ExpenseBookingApprovers).WithMany(m => m.ExpenseBookingInviteApprovers).HasForeignKey(m => m.ExpenseBookingApproverId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class ExpenseBookingRequestMap : IEntityTypeConfiguration<ExpenseBookingRequest>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingRequest> builder)
        {
            builder.HasOne(m => m.FunctionalDepartment).WithMany(m => m.ExpenseBookingRequest).HasForeignKey(m => m.DepartmentID).HasConstraintName("ForeignKey_BookingRequest_Department");
            builder.HasOne(m => m.ExpenseBookingSubCategoryItem).WithMany(m => m.ExpenseBookingRequest).HasForeignKey(m => m.ExpenseBookingSubCategoryItemId).HasConstraintName("ForeignKey_BookingRequest_SubCategoryItem").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Employee).WithMany(m => m.ExpenseBookingRequestEmployee).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_BookingRequest_Employee").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("ExpenseBookingRequest");
        }
    }
}

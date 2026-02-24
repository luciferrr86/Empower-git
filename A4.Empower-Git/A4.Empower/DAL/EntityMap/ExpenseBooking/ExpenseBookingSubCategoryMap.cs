using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class ExpenseBookingSubCategoryMap : IEntityTypeConfiguration<ExpenseBookingSubCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingSubCategory> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.HasOne(m => m.ExpenseBookingCategory).WithMany(m => m.ExpenseBookingSubCategories).HasForeignKey(m => m.ExpenseBookingCategoryId).HasConstraintName("ForeignKey_SubCategory_Category");
            builder.ToTable("ExpenseBookingSubCategory");
        }
    }
}

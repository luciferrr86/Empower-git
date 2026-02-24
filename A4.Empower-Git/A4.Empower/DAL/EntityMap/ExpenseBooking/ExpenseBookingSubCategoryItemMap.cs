using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class ExpenseBookingSubCategoryItemMap : IEntityTypeConfiguration<ExpenseBookingSubCategoryItem>
    {
        public void Configure(EntityTypeBuilder<ExpenseBookingSubCategoryItem> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.HasOne(m => m.ExpenseBookingSubCategory).WithMany(m => m.ExpenseBookingSubCategoryItems).HasForeignKey(m => m.ExpenseBookingSubCategoryId).HasConstraintName("ForeignKey_SubCategoryItem_SubCategory");
            builder.ToTable("ExpenseBookingSubCategoryItem");
        }
    }
}

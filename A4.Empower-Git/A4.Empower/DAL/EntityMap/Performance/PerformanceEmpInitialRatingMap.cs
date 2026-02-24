using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    class PerformanceEmpInitialRatingMap : IEntityTypeConfiguration<PerformanceEmpInitialRating>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpInitialRating> builder)
        {
            builder.HasOne(m => m.PerformanceYear).WithMany(u => u.PerformanceEmpInitialRating).HasForeignKey(m => m.PerformanceYearId).HasConstraintName("ForeignKey_PerformanceEmpInitialRating_PerformanceYear");
            builder.ToTable("PerformanceEmpInitialRating");
        }
    }
}

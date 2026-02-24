using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceInitailRatingMap : IEntityTypeConfiguration<PerformanceInitailRating>
    {
        public void Configure(EntityTypeBuilder<PerformanceInitailRating> builder)
        {
            //builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceInitailRating).HasForeignKey(m => m.EmployeeId).HasConstraintName("ForeignKey_PerformanceInitailRating_Employee").OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceInitailRating).HasForeignKey(m => m.ManagerId).HasConstraintName("ForeignKey_PerformanceInitailRating_Employee1").OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(m => m.PerformanceConfigRating).WithMany(u => u.PerformanceInitailRating).HasForeignKey(m => m.PerformanceConfigRatingId).HasConstraintName("ForeignKey_PerformanceInitailRating_PerformanceConfigRating").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.PerformanceYear).WithMany(u => u.PerformanceInitailRating).HasForeignKey(m => m.PerformanceYearId).HasConstraintName("ForeignKey_PerformanceInitailRating_PerformanceYear").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformanceInitailRating");
        }
    }
}

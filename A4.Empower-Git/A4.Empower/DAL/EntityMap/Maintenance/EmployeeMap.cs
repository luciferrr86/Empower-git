using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(m => m.Band).WithMany(m => m.Employee).HasForeignKey(m => m.BandId).HasConstraintName("ForeignKey_Employee_Band").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.FunctionalDesignation).WithMany(m => m.Employee).HasForeignKey(m => m.DesignationId).HasConstraintName("ForeignKey_Employee_Designation").OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.FunctionalGroup).WithMany(m => m.Employee).HasForeignKey(m => m.GroupId).HasConstraintName("ForeignKey_Employee_Group").OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(m => m.FunctionalTitle).WithMany(m => m.Employee).HasForeignKey(m => m.TitleId).HasConstraintName("ForeignKey_Employee_Title").OnDelete(DeleteBehavior.Restrict); 
            //builder.HasOne(m => m.ApplicationUser).WithOne().HasForeignKey<Employee>(m => m.UserId).HasConstraintName("ForeignKey_ApplicationUser_Employee");
            //builder.HasOne(m => m.ApplicationUser).WithOne().HasConstraintName("ForeignKey_ApplicationUser_Employee");
            builder.HasOne(m => m.ApplicationUser).WithOne().HasForeignKey<Employee>(m => m.UserId).HasConstraintName("ForeignKey_ApplicationUser_Employee").OnDelete(DeleteBehavior.Restrict); ;
            builder.ToTable("Employee");
        }
    }
}

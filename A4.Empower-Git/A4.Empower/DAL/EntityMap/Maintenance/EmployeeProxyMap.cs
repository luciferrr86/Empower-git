using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class EmployeeProxyMap : IEntityTypeConfiguration<EmployeeProxy>
    {
        public void Configure(EntityTypeBuilder<EmployeeProxy> builder)
        {
      
            builder.HasOne(m => m.ProxyFor)
                              .WithMany(m => m.EmpProxyFor)
                             .HasForeignKey(m => m.ProxyForId)
                             .HasConstraintName("ForeignKey_Employee_EmployeeProxyFor").OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Employee)
                        .WithMany(m=>m.EmpProxy)
                        .HasForeignKey(m => m.EmployeeId)
                        .HasConstraintName("ForeignKey_Employee_EmployeeProxy")
                        .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("EmployeeProxy");
        }
    }
}

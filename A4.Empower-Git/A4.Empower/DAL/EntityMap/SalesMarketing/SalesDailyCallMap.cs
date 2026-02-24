using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class SalesDailyCallMap : IEntityTypeConfiguration<SalesDailyCall>
    {
        public void Configure(EntityTypeBuilder<SalesDailyCall> builder)
        {
            builder.HasOne(m => m.SalesStatus).WithMany(m => m.SalesDailyCall).HasForeignKey(m => m.SalesCompanyId).HasConstraintName("ForeignKey_SalesStatus_SalesCompany");
            builder.HasOne(m => m.SalesCompany).WithMany(m => m.SalesDailyCall).HasForeignKey(m => m.SalesCompanyId).HasConstraintName("ForeignKey_SalesDailyCall_SalesCompany").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("SalesDailyCall");
        }
    }
}

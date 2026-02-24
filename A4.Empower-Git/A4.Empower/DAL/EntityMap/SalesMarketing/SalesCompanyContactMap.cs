using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{ 
   public class SalesCompanyContactMap :  IEntityTypeConfiguration<SalesCompanyContact>
    {
        public void Configure(EntityTypeBuilder<SalesCompanyContact> builder)
        {
           
            builder.HasOne(m => m.SalesCompany).WithMany(m => m.SalesCompanyContact).HasForeignKey(m => m.SalesCompanyId).HasConstraintName("ForeignKey_SalesCompanyContact_SalesCompany").OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("SalesCompanyContact");
        }
    }
}

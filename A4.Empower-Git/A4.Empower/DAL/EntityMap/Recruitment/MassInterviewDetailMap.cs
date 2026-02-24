using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class MassInterviewDetailMap : IEntityTypeConfiguration<MassInterviewDetail>
    {
        public void Configure(EntityTypeBuilder<MassInterviewDetail> builder)
        {
            builder.HasOne(a => a.MassInterview).WithMany(x => x.MassInterviewDetail).HasForeignKey(z => z.MassInterviewId).HasConstraintName("ForeignKey_MassInterviewDetail_MassInterview");
         
            builder.ToTable("MassInterviewDetail");
        }
    }
}

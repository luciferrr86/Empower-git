using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpFeedbackDetailMap : IEntityTypeConfiguration<PerformanceEmpFeedbackDetail>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpFeedbackDetail> builder)
        {
            builder.HasOne(m => m.PerformanceEmpFeedback).WithMany(u => u.PerformanceEmpFeedbackDetail).HasForeignKey(m => m.PerformanceEmpFeedbackId).HasConstraintName("ForeignKey_PerformanceEmpFeedbackDetail_PerformanceEmpFeedback");
            builder.HasOne(m => m.PerformanceConfigFeedback).WithMany(u => u.PerformanceEmpFeedbackDetail).HasForeignKey(m => m.PerformanceConfigFeedbackId).HasConstraintName("ForeignKey_PerformanceEmpFeedbackDetail_PerformanceConfigFeedback");
            builder.ToTable("PerformanceEmpFeedbackDetail");
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class PerformanceEmpFeedbackMap : IEntityTypeConfiguration<PerformanceEmpFeedback>
    {
        public void Configure(EntityTypeBuilder<PerformanceEmpFeedback> builder)
        {
            builder.HasOne(m => m.PerformanceEmpGoal).WithMany(u => u.PerformanceEmpFeedback).HasForeignKey(m => m.PerformanceEmpGoalId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("ForeignKey_PerformanceEmpFeedback_PerformanceEmpGoal").OnDelete(DeleteBehavior.Cascade); 
            //builder.HasOne(m => m.Employee).WithMany(u => u.PerformanceEmpFeedback).HasForeignKey(m => m.FeedBackEmpId).HasConstraintName("ForeignKey_PerformanceEmpFeedback_Employee").OnDelete(DeleteBehavior.Restrict);
            builder.ToTable("PerformanceEmpFeedback");
        }
    }
}

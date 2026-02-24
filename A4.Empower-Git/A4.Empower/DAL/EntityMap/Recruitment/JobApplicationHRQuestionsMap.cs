using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobApplicationHRQuestionsMap : IEntityTypeConfiguration<JobApplicationHRQuestions>
    {
        public void Configure(EntityTypeBuilder<JobApplicationHRQuestions> builder)
        {
            builder.HasOne(m => m.JobApplication).WithMany(m => m.JobApplicationHRQuestions).HasForeignKey(m => m.JobApplicationId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.JobHRQuestions).WithMany(m => m.JobApplicationHRQuestions).HasForeignKey(m => m.JobHRQuestionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

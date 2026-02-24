using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobApplicationScreeningQuestionMap : IEntityTypeConfiguration<JobApplicationScreeningQuestion>
    {
        public void Configure(EntityTypeBuilder<JobApplicationScreeningQuestion> builder)
        {
            builder.HasOne(m => m.JobScreeningQuestions).WithMany(m => m.JobApplicationScreeningQuestions).HasForeignKey(m => m.JobScreeningQuestionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.JobApplication).WithMany(m => m.JobApplicationScreeningQuestions).HasForeignKey(m => m.JobApplicationId).OnDelete(DeleteBehavior.Cascade);
           // builder.HasOne(m => m.JobHRQuestions).WithMany(m => m.JobApplicationHRQuestions).HasForeignKey(m => m.JobHRQuestionId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

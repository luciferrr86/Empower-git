using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobApplicationSkillQuestionMap : IEntityTypeConfiguration<JobApplicationSkillQuestion>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<JobApplicationSkillQuestion> builder)
        {
            builder.HasOne(m => m.JobCandidateInterviews).WithMany(m => m.JobApplicationSkillQuestions).HasForeignKey(m => m.JobCandidateInterviewId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.JobVacancyLevelSkillQuestions).WithMany(m => m.JobApplicationSkillQuestions).HasForeignKey(m => m.JobVacancyLevelSkillQuestionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

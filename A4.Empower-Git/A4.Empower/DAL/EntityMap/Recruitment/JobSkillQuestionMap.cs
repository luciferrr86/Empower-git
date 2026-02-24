using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobSkillQuestionMap : IEntityTypeConfiguration<JobSkillQuestion>
    {
        public void Configure(EntityTypeBuilder<JobSkillQuestion> builder)
        {
            builder.HasOne(m => m.JobVacancy).WithMany(m => m.JobSkillQuestions).HasForeignKey(m => m.JobVacancyId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobVacancyLevelSkillQuestionMap : IEntityTypeConfiguration<JobVacancyLevelSkillQuestion>
    {
        public void Configure(EntityTypeBuilder<JobVacancyLevelSkillQuestion> builder)
        {
            builder.HasOne(m => m.JobVacancyLevel).WithMany(m => m.JobVacancyLevelSkillQuestion).HasForeignKey(m => m.JobVacancyLevelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.JobSkillQuestion).WithMany(m => m.JobVacancyLevelSkillQuestions).HasForeignKey(m => m.JobSkillQuestionId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(m => m.Band).WithMany(m => m.Employee).HasForeignKey(m => m.BandId).HasConstraintName("ForeignKey_Employee_Band").OnDelete(DeleteBehavior.Cascade);
        }
    }
}

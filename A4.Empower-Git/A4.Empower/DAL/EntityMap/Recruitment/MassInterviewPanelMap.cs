using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class MassInterviewPanelMap : IEntityTypeConfiguration<MassInterviewPanel>
    {
        public void Configure(EntityTypeBuilder<MassInterviewPanel> builder)
        {
            builder.HasOne(m => m.JobVacancy).WithMany(u => u.MassInterviewPanel).HasForeignKey(m => m.VacancyId).HasConstraintName("ForeignKey_MassInterviewPanel_JobVacancy");
            builder.HasOne(m => m.MassInterviewDetail).WithMany(u => u.MassInterviewPanel).HasForeignKey(m => m.InterviewDateId).HasConstraintName("ForeignKey_MassInterviewPanel_InterviewDetail");
            builder.ToTable("MassInterviewPanel");
        }
    }
}

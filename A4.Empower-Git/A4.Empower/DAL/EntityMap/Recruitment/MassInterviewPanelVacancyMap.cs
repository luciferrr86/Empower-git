using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public class MassInterviewPanelVacancyMap : IEntityTypeConfiguration<MassInterviewPanelVacancy>
    {
        public void Configure(EntityTypeBuilder<MassInterviewPanelVacancy> builder)
        {
         
            //builder.HasOne(m => m.Employee).WithMany(u => u.MassInterviewPanelVacancy).HasForeignKey(m => m.MangerId).HasConstraintName("ForeignKey_MassInterviewPanelVaccany_MangerId");
            builder.HasOne(m => m.MassInterviewPanel).WithMany(u => u.MassInterviewPanelVaccany).HasForeignKey(m => m.PanelId).HasConstraintName("ForeignKey_MassInterviewPanelVaccany_MassInterviewPanel");
            builder.ToTable("MassInterviewPanelVacancy");
        }
    }
}

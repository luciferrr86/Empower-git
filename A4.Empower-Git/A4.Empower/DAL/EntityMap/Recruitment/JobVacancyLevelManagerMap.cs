using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobVacancyLevelManagerMap : IEntityTypeConfiguration<JobVacancyLevelManager>
    {
        public void Configure(EntityTypeBuilder<JobVacancyLevelManager> builder)
        {

            builder.ToTable("JobVacancyManager");
        }
    }
}

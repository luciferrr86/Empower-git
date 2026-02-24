using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class ApplicationModuleMap : IEntityTypeConfiguration<ApplicationModule>
    {
        public void Configure(EntityTypeBuilder<ApplicationModule> builder)
        {
            builder.ToTable("ApplicationModule");
        }
    }
}

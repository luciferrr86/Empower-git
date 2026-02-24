using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
   public  class LeaveStatusMap : IEntityTypeConfiguration<LeaveStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveStatus> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.ToTable("LeaveStatus");
        }
    }
}

using A4.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.EntityMap
{
    public class JobHRQuestionMap : IEntityTypeConfiguration<JobHRQuestion>
    {
        public void Configure(EntityTypeBuilder<JobHRQuestion> builder)
        {
            throw new NotImplementedException();
        }
    }
}

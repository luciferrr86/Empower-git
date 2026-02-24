using A4.DAL.Entites;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public class JobApplicationHRQuestionsRepository : Repository<JobApplicationHRQuestions>, IJobApplicationHRQuestionsRepository

    {
        public JobApplicationHRQuestionsRepository(DbContext context) : base(context)
        {
        }
    }
}

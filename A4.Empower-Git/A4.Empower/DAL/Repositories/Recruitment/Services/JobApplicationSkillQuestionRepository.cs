using A4.DAL.Entites;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public class JobApplicationSkillQuestionRepository : Repository<JobApplicationSkillQuestion>, IJobApplicationSkillQuestionRepository
    {
        public JobApplicationSkillQuestionRepository(DbContext context) : base(context)
        {
        }
    }
}

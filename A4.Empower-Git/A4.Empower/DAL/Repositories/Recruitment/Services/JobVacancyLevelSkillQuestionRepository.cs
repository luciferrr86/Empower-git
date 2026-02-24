using A4.DAL.Entites;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public class JobVacancyLevelSkillQuestionRepository : Repository<JobVacancyLevelSkillQuestion>, IJobVacancyLevelSkillQuestionRepository
    {
        public JobVacancyLevelSkillQuestionRepository(DbContext context) : base(context)
        {
        }
    }
}

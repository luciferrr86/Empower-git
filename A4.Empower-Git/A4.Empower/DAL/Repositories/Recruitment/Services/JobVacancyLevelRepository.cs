using A4.DAL.Entites;
using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public class JobVacancyLevelRepository : Repository<JobVacancyLevel>, IJobVacancyLevelRepository
    {
        public JobVacancyLevelRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public void DeleteJobVacancyLevel(JobVacancyLevel jobVacancyLevel)
        {
            _appContext.JobVacancyLevel.Remove(jobVacancyLevel);
            var jobVacancyLevelMgr = _appContext.JobVacancyLevelManager.Where(m => m.JobVacancyLevelId == jobVacancyLevel.Id).ToList();
            if (jobVacancyLevelMgr.Count > 0)
            {
                _appContext.JobVacancyLevelManager.RemoveRange(jobVacancyLevelMgr);
            }

            _appContext.SaveChanges();
        }
    }
}

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
    public class JobRepository : Repository<JobVacancy>, IJobRepository
    {
        public JobRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;


        public List<JobVacancy> GetAllJobPublishedList()
        {
            var publishList = _appContext.JobVacancy.Where(c => c.bIsPublished == true && c.IsActive == true).Select(x => new JobVacancy { JobTitle = x.JobTitle, JobLocation = x.JobLocation, PublishedDate = x.PublishedDate, bIsPublished = x.bIsPublished, Id = x.Id, JobTypeId = x.JobTypeId }).ToList();
            var publishJob = from vaccancy in _appContext.JobVacancy
                             join jobtype in _appContext.JobType on vaccancy.JobTypeId equals jobtype.Id
                             where (vaccancy.bIsPublished == true && vaccancy.IsActive == true)
                             select (new JobVacancy { Id = vaccancy.Id, JobLocation = vaccancy.JobLocation, JobTitle = vaccancy.JobTitle, PublishedDate = vaccancy.PublishedDate, JobType = new JobType { Name = jobtype.Name } });

            return publishList.ToList();
        }


    }
}

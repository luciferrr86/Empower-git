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
  public  class JobTypeRepository : Repository<JobType>, IJobTypeRepository
    {
        public JobTypeRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobType Get(Guid id)
        {
            var jobType = _appContext.JobType.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobType;
        }

        public override void Add(JobType entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(JobType entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobType entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public PagedList<JobType> GetAllJobType(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listJobType = _appContext.JobType.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listJobType = listJobType.Where(c => c.Name.Contains(name));
            listJobType = listJobType.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<JobType>(listJobType, pageIndex, pageSize);
        }
    }
}

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
  public   class JobInterviewTypeRepository : Repository<JobInterviewType>, IJobInterviewTypeRepository
    {
        public JobInterviewTypeRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobInterviewType Get(Guid id)
        {
            var jobType = _appContext.JobInterviewType.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobType;
        }

        public override void Add(JobInterviewType entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(JobInterviewType entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobInterviewType entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }
        public PagedList<JobInterviewType> GetAllJobInterviewType(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listInterviewType = _appContext.JobInterviewType.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listInterviewType = listInterviewType.Where(c => c.Name.Contains(name));
            listInterviewType = listInterviewType.OrderBy(c => c.Name).ThenBy(c => c.Id);
            return new PagedList<JobInterviewType>(listInterviewType, pageIndex, pageSize);
        }
    }
}

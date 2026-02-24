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
   public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
    {

        public JobApplicationRepository(DbContext context) : base(context)
        {

        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobApplication Get(Guid id)
        {
            var jobApplicant = _appContext.JobApplication.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobApplicant;
        }
        public override void Add(JobApplication entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }
        public override void Update(JobApplication entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }
        public override void Remove(JobApplication entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public void CreateQuestions(List<JobApplicationQuestions> entity)
        {
            _appContext.SaveChanges();
        }

    }
}

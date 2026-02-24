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
    public class JobHRQuestionRepository : Repository<JobHRQuestion>, IJobHRQuestionRepository
    {
        public JobHRQuestionRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobHRQuestion Get(Guid id)
        {
            var jobType = _appContext.JobHRQuestion.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobType;
        }

        public override void Update(JobHRQuestion entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobHRQuestion entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public void CreateQuestions(JobHRQuestion entity)
        {
            _appContext.JobHRQuestion.Add(entity);
            _appContext.SaveChanges();
        }

        public void UpdateQuestion(JobHRQuestion entity)
        {
            _appContext.JobHRQuestion.Update(entity);
            _appContext.SaveChanges();
        }

        public List<JobHRQuestion> GetJobQuestions(Guid jobVacancyId)
        {
            var jobQuestios = _appContext.JobHRQuestion.Where(e => e.JobVacancyId == jobVacancyId && e.IsActive == true).ToList();
            return jobQuestios;
        }

        public void DeleteQuestion(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using A4.BAL;
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
    public class JobScreeningQuestionRepository : Repository<JobScreeningQuestion>, IJobScreeningQuestionRepository
    {
        public JobScreeningQuestionRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobScreeningQuestion Get(Guid id)
        {
            var jobType = _appContext.JobScreeningQuestion.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobType;
        }

        public override void Update(JobScreeningQuestion entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobScreeningQuestion entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public void CreateQuestions(JobScreeningQuestion entity)
        {
            _appContext.JobScreeningQuestion.Add(entity);
            _appContext.SaveChanges();
        }

        public void UpdateQuestion(JobScreeningQuestion entity)
        {
            _appContext.JobScreeningQuestion.Update(entity);
            _appContext.SaveChanges();
        }

        public List<JobScreeningQuestion> GetJobQuestions(Guid jobVacancyId)
        {
            var jobQuestios = _appContext.JobScreeningQuestion.Where(e => e.JobVacancyId == jobVacancyId && e.IsActive == true).ToList();
            return jobQuestios;
        }

        public void DeleteQuestion(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

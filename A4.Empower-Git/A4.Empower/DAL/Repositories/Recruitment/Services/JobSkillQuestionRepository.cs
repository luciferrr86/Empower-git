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
    public class JobSkillQuestionRepository : Repository<JobSkillQuestion>, IJobSkillQuestionRepository
    {
        public JobSkillQuestionRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobSkillQuestion Get(Guid id)
        {
            var jobType = _appContext.JobSkillQuestion.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobType;
        }

        public override void Update(JobSkillQuestion entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobSkillQuestion entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public void CreateQuestions(JobSkillQuestion entity)
        {
            _appContext.JobSkillQuestion.Add(entity);
            _appContext.SaveChanges();
        }

        public void UpdateQuestion(JobSkillQuestion entity)
        {
            _appContext.JobSkillQuestion.Update(entity);
            _appContext.SaveChanges();
        }



        public List<JobSkillQuestion> GetJobQuestions(Guid jobVacancyId)
        {
            var jobQuestios = _appContext.JobSkillQuestion.Where(e => e.IsActive == true).ToList();
            return jobQuestios;
        }

        public void DeleteQuestion(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteJobSkillQuestion(JobSkillQuestion jobSkillQuestion)
        {
            _appContext.JobSkillQuestion.Remove(jobSkillQuestion);
            var vacancyLevelSkillQuestions = _appContext.JobVacancyLevelSkillQuestion.Where(m => m.JobSkillQuestionId == jobSkillQuestion.Id).ToList();
            if (vacancyLevelSkillQuestions.Count > 0)
            {
                _appContext.JobVacancyLevelSkillQuestion.RemoveRange(vacancyLevelSkillQuestions);
            }
            _appContext.SaveChanges();

        }
    }
}

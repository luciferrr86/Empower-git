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
    public class JobCandidateInterviewRepository : Repository<JobCandidateInterview>, IJobCandidateInterviewRepository
    {
        public JobCandidateInterviewRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;


        public override JobCandidateInterview Get(Guid id)
        {
            var jobInterviewSchedule = _appContext.JobCandidateInterview.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobInterviewSchedule;
        }

        public override void Add(JobCandidateInterview entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public override void Update(JobCandidateInterview entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobCandidateInterview entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public List<HRKpiModel> GetHRKpi(Guid JobVacancyId, Guid appId)
        {
            var hrKpiQuestion = new List<HRKpiModel>();
            var query = from questionKpi in _appContext.JobHRQuestion.Where(o => o.JobVacancyId == JobVacancyId && o.IsActive == true)
                        join jobapplicationQuestions in _appContext.JobApplicationHRQuestions.Where(o => o.JobApplicationId == appId)
                        on questionKpi.Id equals jobapplicationQuestions.JobHRQuestionId into details
                        from applicationQuestions in details.DefaultIfEmpty()
                        select new HRKpiModel { Question = questionKpi.Question, JobHRQuestionId = questionKpi.Id.ToString(), Weightage = questionKpi.Weightage.ToString(), ObtainedWeightage = applicationQuestions != null ? applicationQuestions.ObtainedWeightage : "", JobApplicationId = appId.ToString() };

            hrKpiQuestion = query.ToList();
            return hrKpiQuestion;
        }

        public List<QuestionAnswerSkillModel> GetSkillKpi(Guid JobVacancyId, Guid appId)
        {
            var listSkillQuestions = new List<QuestionAnswerSkillModel>();
            var levelList = _appContext.JobVacancyLevel.Where(o => o.JobVacancyId == JobVacancyId).ToList();
            if (levelList.Count > 0)
            {
                foreach (var item in levelList)
                {
                    listSkillQuestions.Add(new QuestionAnswerSkillModel { Id = item.Id.ToString(), Name = item.Name, QuestionAnswerSkillList = GetQuestionAnswerSkillList(item.Id) });
                }

            }
            return listSkillQuestions;
        }

        public List<QuestionAnswerSkillModel> GetSkillKpiData(Guid JobVacancyId, Guid appId)
        {
            var listSkillQuestions = new List<QuestionAnswerSkillModel>();
            var levelList = _appContext.JobVacancyLevel.Where(o => o.JobVacancyId == JobVacancyId).ToList();
            if (levelList.Count > 0)
            {
                foreach (var item in levelList)
                {
                    listSkillQuestions.Add(new QuestionAnswerSkillModel { Id = item.Id.ToString(), Name = item.Name, QuestionAnswerSkillList = GetQuestionAnswerSkillList(item.Id) });
                }

            }
            return listSkillQuestions;
        }

        public List<JobApplicationScreeningQuestion> GetAllScreening(Guid JobVacancyId, Guid appId)
        {
            var Listquestions = new List<JobApplicationScreeningQuestion>();
            var query = from jobQuestion in _appContext.JobScreeningQuestion.Where(o => o.JobVacancyId == JobVacancyId && o.IsActive == true)
                        join jobapplicationQuestions in _appContext.JobApplicationScreeningQuestion.Where(o => o.JobApplicationId == appId)
                        on jobQuestion.Id equals jobapplicationQuestions.JobScreeningQuestionId into details
                        from applicationQuestions in details.DefaultIfEmpty()
                        select new JobApplicationScreeningQuestion { Question = jobQuestion.Questions, JobScreeningQuestionId = jobQuestion.Id, Weightage = jobQuestion.Weightage.ToString(), ObtainedWeightage = applicationQuestions.ObtainedWeightage, JobApplicationId = applicationQuestions.JobApplicationId == null ? Guid.Empty : applicationQuestions.JobApplicationId };

            Listquestions = query.ToList();
            return Listquestions;
        }

        private bool CheckNullOrEmpty(Guid? guid)
        {
            if (guid == null) return true;
            return false;

        }

        public JobApplication GetJobInformation(Guid JobApplicationId)
        {
            var jobApplication = new JobApplication();
            var query = (from application in _appContext.JobApplication
                         join vacancy in _appContext.JobVacancy on application.JobVacancyId equals vacancy.Id
                         where (application.Id == JobApplicationId)
                         select new JobApplication { JobVacancy = new JobVacancy { JobTitle = vacancy.JobTitle }, CreatedDate = application.CreatedDate, ApplicationType = application.ApplicationType, JobStatus = application.JobStatus }).SingleOrDefault();
            return jobApplication = query;
        }

        public ApplicationUser GetCandidate(Guid jobApplicationId)
        {
            var candidate = new ApplicationUser();
            var query = (from appliction in _appContext.JobApplication
                         join candidateProfile in _appContext.JobCandidateProfile on appliction.JobCandidateProfileId equals candidateProfile.Id
                         join applicationUser in _appContext.Users on candidateProfile.UserId equals applicationUser.Id
                         join vacancy in _appContext.JobVacancy on appliction.JobVacancyId equals vacancy.Id
                         where (appliction.Id == jobApplicationId)
                         select new ApplicationUser { FullName = applicationUser.FullName, Email = applicationUser.Email }).SingleOrDefault();
            return candidate = query;
        }

        public ApplicationUser GetManager(Guid managerId)
        {
            var manager = new ApplicationUser();
            var query = (from employee in _appContext.Employee
                         join applicationUser in _appContext.Users on employee.UserId equals applicationUser.Id
                         where (employee.Id == managerId)
                         select new ApplicationUser { FullName = applicationUser.FullName, Email = applicationUser.Email }).SingleOrDefault();
            return manager = query;
        }

        public List<InterviewScheduleModel> GetInterviewScheduleList(Guid JobApplicationId, Guid levelId)
        {
            var listSchedule = new List<InterviewScheduleModel>();
            var query = from schedule in _appContext.JobCandidateInterview
                        join vacancyLevel in _appContext.JobVacancyLevelManager on schedule.JobVacancyLevelManagerId equals vacancyLevel.Id
                        join employee in _appContext.Employee on vacancyLevel.EmployeeId equals employee.Id
                        join app in _appContext.Users on employee.UserId equals app.Id
                        where (schedule.JobApplicationId == JobApplicationId && vacancyLevel.JobVacancyLevelId == levelId)
                        select new InterviewScheduleModel {
                            InterviewId = schedule.Id.ToString(), 
                            ManagerId = vacancyLevel.EmployeeId.ToString(), InterviewTypeId = schedule.JobInterviewTypeId.ToString(), 
                            Date = schedule.Date, Time = schedule.Time, ManagerName = app.FullName, Comment = schedule.InterviewerComment, 
                            InterviewStatus = schedule.IsInterviewCompleted,IsLevelCompleted=schedule.IsLevelCompleted,
                            IsCandidateSelected= schedule.IsCandidateSelected };
            listSchedule = query.ToList();
            return listSchedule;
        }

        public List<QuestionAnswer> GetSkillKpiManager(Guid interViewId)
        {
            var query = (from interview in _appContext.JobCandidateInterview
                         join jobVaccancyLevelMgr in _appContext.JobVacancyLevelManager on interview.JobVacancyLevelManagerId equals jobVaccancyLevelMgr.Id
                         join jobVacLevelSkillQuestion in _appContext.JobVacancyLevelSkillQuestion on jobVaccancyLevelMgr.JobVacancyLevelId equals jobVacLevelSkillQuestion.JobVacancyLevelId
                         join jobSkillQuestion in _appContext.JobSkillQuestion on jobVacLevelSkillQuestion.JobSkillQuestionId equals jobSkillQuestion.Id
                         join jobAppSkillQuestion in _appContext.JobApplicationSkillQuestion.Where(m=>m.JobCandidateInterviewId== interViewId) on jobVacLevelSkillQuestion.Id equals jobAppSkillQuestion.JobVacancyLevelSkillQuestionId into jobAppQuestionList
                         from updatedList in jobAppQuestionList.DefaultIfEmpty()
                         where (interview.Id == interViewId)
                         select new QuestionAnswer { LevelSkillQuestionId = jobVacLevelSkillQuestion.Id.ToString(), JobQuestionId = jobSkillQuestion.Id.ToString(), Question = jobSkillQuestion.Question, Weightage = jobSkillQuestion.Weightage.ToString(), ObtainedWeightage = updatedList != null ? updatedList.ObtainedWeightage.ToString() : "" });

            return query.ToList();
        }

        private List<QuestionAnswerSkill> GetQuestionAnswerSkillList(Guid levelId)
        {
            var list = new List<QuestionAnswerSkill>();

            var query = from vacancyLevel in _appContext.JobVacancyLevelManager.Where(m => m.JobVacancyLevelId == levelId)
                        join levelquestion in _appContext.JobVacancyLevelSkillQuestion.Where(m => m.JobVacancyLevelId == levelId) on vacancyLevel.JobVacancyLevelId equals levelquestion.JobVacancyLevelId
                        join skillQuestion in _appContext.JobSkillQuestion on levelquestion.JobSkillQuestionId equals skillQuestion.Id
                        join employee in _appContext.Employee on vacancyLevel.EmployeeId equals employee.Id
                        join user in _appContext.Users on employee.UserId equals user.Id
                        join jobapplicationSkillQuestions in _appContext.JobApplicationSkillQuestion on levelquestion.Id equals jobapplicationSkillQuestions.JobVacancyLevelSkillQuestionId into details
                        from applicationSkillQuestions in details.DefaultIfEmpty()
                        select new QuestionAnswerSkill { JobQuestionId = skillQuestion.Id.ToString(), ManagerName = user.FullName, Question = skillQuestion.Question, Weightage = skillQuestion.Weightage.ToString(), ObtainedWeightage = applicationSkillQuestions.ObtainedWeightage };
            return query.ToList();
        }

    }
}

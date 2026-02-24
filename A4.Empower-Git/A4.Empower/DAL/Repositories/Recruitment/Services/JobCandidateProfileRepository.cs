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
   public class JobCandidateProfileRepository : Repository<JobCandidateProfile>, IJobCandidateProfileRepository
    {
        public JobCandidateProfileRepository(DbContext context) : base(context)
        {
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        #region CandidateProfile

        public JobCandidateProfile GetCandidateProfile(Guid id)
        {
            var jobCandidateProfile = _appContext.JobCandidateProfile.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobCandidateProfile;

        }
        
        public PagedList<JobCandidateProfile> GetCandidateAppliedJob(Guid id,string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
          
            var lstCandidateAppliedJob = from application in _appContext.JobApplication
                                       join vacancy in _appContext.JobVacancy on application.JobVacancyId equals vacancy.Id
                                       join jobType in _appContext.JobType on vacancy.JobTypeId equals jobType.Id
                        
                                       where application.IsActive == true && application.JobCandidateProfileId == id
                                       select new JobCandidateProfile { ApplicationId = application.Id, AppliedDate = application.CreatedDate, JobStatus = application.JobStatus.ToString(),  VacancyName = vacancy.JobTitle , JobType = jobType.Name};
           
            return new PagedList<JobCandidateProfile>(lstCandidateAppliedJob, pageIndex, pageSize);
        }
        public JobCandidateProfile GetJobCandidateProfile(string userId)
        {
            var jobCandidateProfile = new JobCandidateProfile();
            var user = _appContext.Users.Where(e => e.Id == userId && e.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                jobCandidateProfile = _appContext.JobCandidateProfile.Where(e => e.UserId == user.Id  && e.IsActive == true).Include(a=>a.ApplicationUser).FirstOrDefault();
            }
            return jobCandidateProfile;
        }

        public void CreateCandidateProfile(JobCandidateProfile entity)
        {
            _appContext.JobCandidateProfile.Add(entity);
            _appContext.SaveChanges();
        }

        public void UpdateCandidateProfile(JobCandidateProfile entity)
        {
            _appContext.JobCandidateProfile.Update(entity);
            _appContext.SaveChanges();
        }

        public  void RemoveCandidateProfile(JobCandidateProfile entity)
        {
            _appContext.JobCandidateProfile.Remove(entity);
            _appContext.SaveChanges();
        }


        #endregion

        #region WorkExperience

        public JobCandidateWorkExperience GetWorkExperience(Guid id)
        {
            var jobWorkExperience = _appContext.JobCandidateWorkExperience.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobWorkExperience;
        }

        public List<JobCandidateWorkExperience> GetJobWorkExperience(Guid ProfileId)
        {
            var jobWorkExperience = new List<JobCandidateWorkExperience>();
            var candidateProfile = _appContext.JobCandidateProfile.Where(e => e.Id == ProfileId && e.IsActive == true).FirstOrDefault();
            if (candidateProfile != null)
            {
               jobWorkExperience = _appContext.JobCandidateWorkExperience.Where(e => e.JobCandidateProfilesId == candidateProfile.Id && e.IsActive == true).ToList();
            }
            return jobWorkExperience;
        }

        public void CreateWorkExperience(JobCandidateWorkExperience entity)
        {
            _appContext.JobCandidateWorkExperience.Add(entity);
            _appContext.SaveChanges();
        }

        public void UpdateWorkExperience(JobCandidateWorkExperience entity)
        {
            _appContext.JobCandidateWorkExperience.Update(entity);
            _appContext.SaveChanges();
        }

        public void RemoveWorkExperience(JobCandidateWorkExperience entity)
        {
            _appContext.JobCandidateWorkExperience.Remove(entity);
            _appContext.SaveChanges();
        }

        #endregion

        #region Qualification

        public JobCandidateQualification GetQualification(Guid id)
        {
            var jobQualification = _appContext.JobCandidateQualification.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobQualification;
        }

        public JobCandidateQualification GetJobQualification(Guid ProfileId)
        {
            var jobQualification = _appContext.JobCandidateQualification.Where(e => e.JobCandidateProfilesId == ProfileId && e.IsActive == true).FirstOrDefault();
            return jobQualification;
        }

        public void CreateQualification(JobCandidateQualification entity)
        {
            _appContext.JobCandidateQualification.Add(entity);
            _appContext.SaveChanges();
        }

        public void UpdateQualification(JobCandidateQualification entity)
        {
            _appContext.JobCandidateQualification.Update(entity);
            _appContext.SaveChanges();
        }

        public void RemoveQualification(JobCandidateQualification entity)
        {
            _appContext.JobCandidateQualification.Remove(entity);
            _appContext.SaveChanges();
        }

        #endregion

        
        public PagedList<JobCandidateProfile> GetAllCandidateProfile(string levelId,string id = "", string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            

         var listCandidateProfile = from candidateProfile in _appContext.JobCandidateProfile
                        join appuser in _appContext.Users on candidateProfile.UserId equals appuser.Id
                        join application in _appContext.JobApplication on candidateProfile.Id equals application.JobCandidateProfileId
                        join vacancy in _appContext.JobVacancy on application.JobVacancyId equals vacancy.Id
                        where appuser.IsActive == true
                        select new JobCandidateProfile { LevelId=application.LevelId,ResumeId= candidateProfile.ResumeId, ApplicationId = application.Id,JobStatus=application.JobStatus.ToString(), Id = candidateProfile.Id,ApplicationType = application.ApplicationType,JobVacancyId =application.JobVacancyId, VacancyName = vacancy.JobTitle , Name  = appuser.FullName, Email  = appuser.Email, PhoneNo = appuser.PhoneNumber,OverAllScore = application.OverallScore,HRScore=application.HRScore,SkillScore=application.SkillScore,ScreeningScore=application.ScreeningScore };
            if (id != "")
            {
                listCandidateProfile = listCandidateProfile.Where(m => m.JobVacancyId == Guid.Parse(id));
            }
            if (levelId != "all")
            {
                listCandidateProfile = listCandidateProfile.Where(m => m.LevelId == Guid.Parse(levelId));
            }
            return new PagedList<JobCandidateProfile>(listCandidateProfile, pageIndex, pageSize);

        }

        public PagedList<JobCandidateProfile> GetManagerandidate(Guid managerId ,string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {


            var listCandidateProfile = from candidateProfile in _appContext.JobCandidateProfile
                                       join appuser in _appContext.Users on candidateProfile.UserId equals appuser.Id
                                       join application in _appContext.JobApplication on candidateProfile.Id equals application.JobCandidateProfileId
                                       join interview in _appContext.JobCandidateInterview on application.Id equals interview.JobApplicationId
                                       join vacacylevel in _appContext.JobVacancyLevelManager on interview.JobVacancyLevelManagerId equals vacacylevel.Id
                                       join vacancy in _appContext.JobVacancy on application.JobVacancyId equals vacancy.Id
                                       where (appuser.IsActive == true && vacacylevel.EmployeeId== managerId && interview.IsInterviewCompleted==false)
                                       select new JobCandidateProfile { ApplicationId = application.Id, ResumeId = candidateProfile.ResumeId, InterviewId =interview.Id, InterviewDate = interview.Date,InterviewTime=interview.Time, JobStatus = application.JobStatus.ToString(), Id = candidateProfile.Id, ApplicationType = application.ApplicationType, JobVacancyId = application.JobVacancyId, VacancyName = vacancy.JobTitle, Name = appuser.FullName, Email = appuser.Email, PhoneNo = appuser.PhoneNumber, OverAllScore = application.OverallScore, HRScore = application.HRScore, SkillScore = application.SkillScore, ScreeningScore = application.ScreeningScore };
            return new PagedList<JobCandidateProfile>(listCandidateProfile, pageIndex, pageSize);

        }

        public JobCandidateProfile GetCandidateJobInfo(Guid interViewId)
        {


            var listCandidateProfile = (from candidateProfile in _appContext.JobCandidateProfile
                                       join appuser in _appContext.Users on candidateProfile.UserId equals appuser.Id
                                       join application in _appContext.JobApplication on candidateProfile.Id equals application.JobCandidateProfileId
                                        join interview in _appContext.JobCandidateInterview on application.Id equals interview.JobApplicationId
                                       join vacancy in _appContext.JobVacancy on application.JobVacancyId equals vacancy.Id
                                       where (appuser.IsActive == true && interview.Id == interViewId)
                                       select new JobCandidateProfile { ApplicationId = application.Id, InterviewId = interview.Id, InterviewDate = interview.Date, InterviewTime = interview.Time, JobStatus = application.JobStatus.ToString(), Id = candidateProfile.Id, ApplicationType = application.ApplicationType, JobVacancyId = application.JobVacancyId, VacancyName = vacancy.JobTitle, Name = appuser.FullName, Email = appuser.Email, PhoneNo = appuser.PhoneNumber, OverAllScore = application.OverallScore, HRScore = application.HRScore, SkillScore = application.SkillScore, ScreeningScore = application.ScreeningScore }).FirstOrDefault();

            return listCandidateProfile;
        }

        public IQueryable<JobCandidateProfile> GetExcelCandidatesProfile(string id = "", string applicationType = "")
        {
            var listCandidateProfile = from candidateProfile in _appContext.JobCandidateProfile
                                       join appuser in _appContext.Users on candidateProfile.UserId equals appuser.Id
                                       join application in _appContext.JobApplication on candidateProfile.Id equals application.JobCandidateProfileId
                                       join vacancy in _appContext.JobVacancy on application.JobVacancyId equals vacancy.Id
                                       where appuser.IsActive == true
                                       select new JobCandidateProfile { LevelId = application.LevelId, 
                                           ResumeId = candidateProfile.ResumeId, ApplicationId = application.Id,
                                           JobStatus = application.JobStatus.ToString(), Id = candidateProfile.Id,
                                           ApplicationType = application.ApplicationType,
                                           JobVacancyId = application.JobVacancyId, VacancyName = vacancy.JobTitle,
                                           Name = appuser.FullName,
                                           Email = appuser.Email,
                                           PhoneNo = appuser.PhoneNumber,
                                           OverAllScore = application.OverallScore,
                                           HRScore = application.HRScore,
                                           SkillScore = application.SkillScore,
                                           ScreeningScore = application.ScreeningScore
                                       };

            if (id != "")
            {
                listCandidateProfile = listCandidateProfile.Where(m => m.JobVacancyId == Guid.Parse(id));
            }
            if (!string.IsNullOrWhiteSpace(applicationType))
            {
                listCandidateProfile = listCandidateProfile.Where(m => m.JobStatus == applicationType);
            }
            return listCandidateProfile;
        }
    }
}

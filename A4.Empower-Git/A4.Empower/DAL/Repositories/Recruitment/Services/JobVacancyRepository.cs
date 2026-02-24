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
    public class JobVacancyRepository : Repository<JobVacancy>, IJobVacancyRepository
    {
        public JobVacancyRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override JobVacancy Get(Guid id)
        {
            var jobVacancy = _appContext.JobVacancy.Where(e => e.Id == id && e.IsActive == true).FirstOrDefault();
            return jobVacancy;
        }
        public override void Update(JobVacancy entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public override void Remove(JobVacancy entity)
        {
            base.Remove(entity);
            _context.SaveChanges();
        }

        public PagedList<JobVacancy> GetAllJobVacancy(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listJobVacancy = from jobVaccancy in _appContext.JobVacancy
                                 join jobType in _appContext.JobType on jobVaccancy.JobTypeId equals jobType.Id
                                 select new JobVacancy { JobTitle = jobVaccancy.JobTitle, JobLocation = jobVaccancy.JobLocation, PublishedDate = jobVaccancy.PublishedDate, Experience = jobVaccancy.Experience, bIsPublished = jobVaccancy.bIsPublished, Id = jobVaccancy.Id, JobDescription=jobVaccancy.JobDescription };

            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listJobVacancy = listJobVacancy.Where(c => c.JobTitle.Contains(name));
            listJobVacancy = listJobVacancy.OrderBy(c => c.JobTitle).ThenBy(c => c.Id);
            return new PagedList<JobVacancy>(listJobVacancy, pageIndex, pageSize);
        }
        public IEnumerable<JobVacancy> GetAllJobVacancies(string name = "")
        {
            var listJobVacancy = from jobVaccancy in _appContext.JobVacancy
                                 join jobType in _appContext.JobType on jobVaccancy.JobTypeId equals jobType.Id
                                 select new JobVacancy { JobTitle = jobVaccancy.JobTitle,JobType = jobType, JobLocation = jobVaccancy.JobLocation, JobDescription = jobVaccancy.JobDescription, JobRequirements = jobVaccancy.JobRequirements, SalaryRange = jobVaccancy.SalaryRange, PublishedDate = jobVaccancy.PublishedDate, Experience = jobVaccancy.Experience, bIsPublished = jobVaccancy.bIsPublished, Id = jobVaccancy.Id };

            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listJobVacancy = listJobVacancy.Where(c => c.JobTitle.Contains(name));
          //  listJobVacancy = listJobVacancy.OrderBy(c => c.JobTitle).ThenBy(c => c.Id);
            listJobVacancy = listJobVacancy.Where(k => k.bIsPublished).OrderBy(c => c.JobTitle).ThenBy(c => c.Id);
            return listJobVacancy;

        }
        public JobVacancy GetSingleJobVacanncy(string jobTitle,string name)
        {
            var listJobVacancy = from jobVaccancy in _appContext.JobVacancy
                                 join jobType in _appContext.JobType on jobVaccancy.JobTypeId equals jobType.Id
                                 where jobVaccancy.JobTitle.Equals(jobTitle) && jobType.Name.Equals(name)
                                 select new JobVacancy { JobTitle = jobVaccancy.JobTitle, JobType = jobType, JobLocation = jobVaccancy.JobLocation, JobDescription = jobVaccancy.JobDescription, JobRequirements = jobVaccancy.JobRequirements, SalaryRange = jobVaccancy.SalaryRange, PublishedDate = jobVaccancy.PublishedDate, Experience = jobVaccancy.Experience, bIsPublished = jobVaccancy.bIsPublished, Id = jobVaccancy.Id };

            //if (!string.IsNullOrWhiteSpace(jobTitle) && jobTitle != "undefined")
            //    listJobVacancy = listJobVacancy.Where(c => c.JobTitle == jobTitle) ;
            var data= listJobVacancy.ToList().OrderBy(c => c.JobTitle).ThenBy(c => c.Id).FirstOrDefault();
            //listJobVacancy = listJobVacancy.Where(k => k.bIsPublished).OrderBy(c => c.JobTitle).ThenBy(c => c.Id);
            return data;

        }
        public List<string> GetVacancyManagerList(Guid vacancyId)
        {
            List<string> vacancyManagerList = new List<string>();
            var vacancyManager = _appContext.JobVacancyLevelManager.Where(e => e.JobVacancyLevelId == vacancyId && e.IsActive == true).ToList();
            if (vacancyManager.Count() > 0)
            {
                foreach (var item in vacancyManager)
                {

                }
            }
            return vacancyManagerList;
        }

        public void CreateJob(JobVacancy entity, List<string> mangerId)
        {
            _appContext.JobVacancy.Add(entity);
            foreach (var item in mangerId)
            {
                var model = new JobVacancyLevelManager();
                model.JobVacancyLevelId = entity.Id;
                _appContext.JobVacancyLevelManager.Add(model);
            }
            _appContext.SaveChanges();
        }

        public void UpdateVacancyManger(Guid id, List<string> mangerId)
        {
            var VacancyManager = _appContext.JobVacancyLevelManager.Where(e => e.JobVacancyLevelId == id && e.IsActive == true);
            if (VacancyManager.Count() > 0)
            {
                foreach (var item in VacancyManager)
                {
                    _appContext.JobVacancyLevelManager.Remove(item);
                }
                if (mangerId.Count() > 0)
                {
                    foreach (var item in mangerId)
                    {
                        var model = new JobVacancyLevelManager();
                        model.JobVacancyLevelId = id;
                        _appContext.JobVacancyLevelManager.Add(model);
                    }
                }
            }
            _appContext.SaveChanges();
        }
        public List<JobVacancy> GetPublishedVacancyList()
        {
            var vacancy = _appContext.JobVacancy.Where(e => e.bIsPublished == true && e.IsActive == true).Select(x => new JobVacancy { JobTitle = x.JobTitle, Id = x.Id }).ToList();
            return vacancy;
        }

        public List<InterviewLevelOption> GetInterviewLevels(Guid vacancyId)
        {
            var lstInterviewLevel = new List<InterviewLevelOption>();
            var lstLevel = GetLevels(vacancyId);
            if (lstLevel.Count > 0)
            {
                foreach (var item in lstLevel)
                {
                    var listManager = new List<string>();
                    var lstManager = GetLevelManagerList(Guid.Parse(item.Id));
                    if (lstManager.Count > 0)
                    {
                        foreach (var itemMgr in lstManager)
                        {
                            listManager.Add(itemMgr);
                        }
                    }
                    lstInterviewLevel.Add(new InterviewLevelOption { Id = item.Id, Name = item.Name, ManagerList = listManager });
                }
            }
            return lstInterviewLevel;
        }

        private List<InterviewLevelOption> GetLevels(Guid vacancyId)
        {
            var lstLevel = new List<InterviewLevelOption>();
            lstLevel = (from jobVacancy in _appContext.JobVacancy
                        join vacancyLevel in _appContext.JobVacancyLevel on jobVacancy.Id equals vacancyLevel.JobVacancyId
                        where jobVacancy.Id == vacancyId orderby vacancyLevel.Level
                        select new InterviewLevelOption { Id = vacancyLevel.Id.ToString(), Name = vacancyLevel.Name }).ToList();

            return lstLevel;
        }

        private List<string> GetLevelManagerList(Guid levelId)
        {
            var lstMgr = new List<string>();
            lstMgr = (from vacancyLevel in _appContext.JobVacancyLevel
                      join
levelManager in _appContext.JobVacancyLevelManager on vacancyLevel.Id equals levelManager.JobVacancyLevelId
                      where vacancyLevel.Id == levelId
                      select levelManager.EmployeeId.ToString()).ToList();
            return lstMgr;
        }

        public JobVacancy GetJobDetail(Guid jobId)
        {
            var jobDetail = _appContext.JobVacancy.Where(e => e.Id == jobId && e.IsActive == true).ToList().FirstOrDefault();
            return jobDetail;
            
        }
    }
}

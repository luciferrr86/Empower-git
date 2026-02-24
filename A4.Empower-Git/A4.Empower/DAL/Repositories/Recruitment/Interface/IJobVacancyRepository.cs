using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
   public interface IJobVacancyRepository : IRepository<JobVacancy>
    {
        void CreateJob(JobVacancy model, List<string> mangerId);
        void UpdateVacancyManger(Guid id, List<string> mangerId);
        PagedList<JobVacancy> GetAllJobVacancy(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        IEnumerable<JobVacancy> GetAllJobVacancies(string name = "");
        List<string> GetVacancyManagerList(Guid vacancyId);
        List<JobVacancy> GetPublishedVacancyList();
        List<InterviewLevelOption> GetInterviewLevels(Guid vacancyId);
        JobVacancy GetJobDetail(Guid  jobId);
        JobVacancy GetSingleJobVacanncy(string jobTitle, string name);
    }
}

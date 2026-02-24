using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IJobCandidateProfileRepository : IRepository<JobCandidateProfile>
    {
        #region CandidateProfile
        PagedList<JobCandidateProfile> GetCandidateAppliedJob(Guid id, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        PagedList<JobCandidateProfile> GetAllCandidateProfile(string levelId,string id = "", string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        PagedList<JobCandidateProfile> GetManagerandidate(Guid id, string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        IQueryable<JobCandidateProfile> GetExcelCandidatesProfile(string id = "", string applicationType = "");
        JobCandidateProfile GetCandidateJobInfo(Guid interViewId);
        JobCandidateProfile GetCandidateProfile(Guid id);
        JobCandidateProfile GetJobCandidateProfile(string userId);
        void CreateCandidateProfile(JobCandidateProfile entity);
        void UpdateCandidateProfile(JobCandidateProfile entity);
        void RemoveCandidateProfile(JobCandidateProfile entity);


        #endregion

        #region WorkExperience
        JobCandidateWorkExperience GetWorkExperience(Guid id);
        List<JobCandidateWorkExperience> GetJobWorkExperience(Guid ProfileId);
        void CreateWorkExperience(JobCandidateWorkExperience entity);
        void UpdateWorkExperience(JobCandidateWorkExperience entity);
        void RemoveWorkExperience(JobCandidateWorkExperience entity);

        #endregion

        #region Qualification
        JobCandidateQualification GetQualification(Guid id);
        JobCandidateQualification GetJobQualification(Guid ProfileId);
        void CreateQualification(JobCandidateQualification entity);
        void UpdateQualification(JobCandidateQualification entity);
        void RemoveQualification(JobCandidateQualification entity);
        #endregion
    }
}

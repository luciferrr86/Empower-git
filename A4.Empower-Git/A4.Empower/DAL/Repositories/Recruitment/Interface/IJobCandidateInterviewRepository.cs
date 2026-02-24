using A4.BAL;
using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IJobCandidateInterviewRepository : IRepository<JobCandidateInterview>
    {
        List<QuestionAnswer> GetSkillKpiManager(Guid nterviewId);
        JobApplication GetJobInformation(Guid JobApplicationId);
        List<InterviewScheduleModel> GetInterviewScheduleList(Guid JobApplicationId,Guid levelId);
        List<JobApplicationScreeningQuestion> GetAllScreening(Guid JobVacancyId, Guid appId);
        List<HRKpiModel> GetHRKpi(Guid JobVacancyId, Guid appId);
        ApplicationUser GetCandidate(Guid jobApplicationId);
        ApplicationUser GetManager(Guid managerId);
    }
}

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
    public class MassSchedulingRepository : Repository<MassInterview>, IMassSchedulingRepository

    {
        public MassSchedulingRepository(DbContext context) : base(context)
        {

        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public override void Add(MassInterview entity)
        {
            base.Add(entity);
            _context.SaveChanges();
        }

        public void CreateInterviewDetail(MassInterviewDetail entity)
        {
            _appContext.MassInterviewDetail.Add(entity);
            _appContext.SaveChanges();
        }

        public void CreateRooms(MassInterviewRoom entity)
        {
            _appContext.MassInterviewRoom.Add(entity);
            _appContext.SaveChanges();
        }

        public void CreateInterviewPanel(MassInterviewPanel entity)
        {
            _appContext.MassInterviewPanel.Add(entity);
            _appContext.SaveChanges();
        }

        public void CreateInterviewPanelVacancy(MassInterviewPanelVacancy entity)
        {
            _appContext.MassInterviewPanelVacany.Add(entity);
            _appContext.SaveChanges();
        }

        public List<JobVacancy> GetAllJob()
        {
            var listJobVacancy = new List<JobVacancy>();
            listJobVacancy = _appContext.JobVacancy.Where(c => c.IsActive && c.bIsPublished == true && c.bIsClosed == false).Select(x => new JobVacancy { JobTitle = x.JobTitle, Id = x.Id }).ToList();

            return listJobVacancy;
        }

        public List<Employee> GetSelectedManagerList()
        {
            var ListManager = new List<Employee>();
            var query = from vacancyManager in _appContext.JobVacancyLevelManager
                        join employee in _appContext.Employee on vacancyManager.EmployeeId equals employee.Id
                        join users in _appContext.Users on employee.UserId equals users.Id
                        where employee.IsActive == true
                        select new Employee { Id = employee.Id, ApplicationUser = new ApplicationUser { FullName = users.FullName } };
            ListManager = query.ToList();
            return ListManager;
        }

        public List<JobCandidateProfile> GetListcandidate()
        {
            var Listcandidate = new List<JobCandidateProfile>();
            return Listcandidate;
        }

        public List<MassInterviewRoom> GetRooms()
        {
            var roomlist = _appContext.MassInterviewRoom.Where(m => m.IsActive == true).ToList();
            return roomlist;
        }

        public List<MassInterviewDetail> GetInterviewDate(Guid massId)
        {
            var massInterviewDate = _appContext.MassInterviewDetail.Where(m => m.IsActive == true && m.MassInterviewId == massId).ToList();
            return massInterviewDate;
        }

        public List<MassInterviewPanel> GetPanelList(Guid massId)
        {
            var result = from panel in _appContext.MassInterviewPanel
                         join vacnacy in _appContext.JobVacancy on panel.VacancyId equals vacnacy.Id
                         join interview in _appContext.MassInterviewDetail on panel.InterviewDateId equals interview.Id
                         where interview.MassInterviewId == massId
                         select new MassInterviewPanel { Name = panel.Name, BreakEndTime = panel.BreakEndTime, BreakStartTime = panel.BreakStartTime, VacancyId = panel.VacancyId, StartTime = panel.StartTime, EndTime = panel.EndTime, Id = panel.Id, JobVacancy = new JobVacancy { JobTitle = vacnacy.JobTitle }, MassInterviewDetail = new MassInterviewDetail { Date = interview.Date, Id = interview.Id } };

            return result.ToList();

        }

        public List<MassInterviewPanel> ScheduleInterView(Guid massId)
        {

            var candidateList = from vacancy in _appContext.JobVacancy
                                join application in _appContext.JobApplication on vacancy.Id equals application.JobVacancyId
                                join candidate in _appContext.JobCandidateProfile on application.JobCandidateProfileId equals candidate.Id
                                join user in _appContext.Users on candidate.UserId equals user.Id
                                join panel in _appContext.MassInterviewPanel on vacancy.Id equals panel.VacancyId
                                join interviewdate in _appContext.MassInterviewDetail on panel.InterviewDateId equals interviewdate.Id
                                where interviewdate.MassInterviewId == massId
                                select new { CandidateId = candidate.Id, PanelId = panel.Id };



            var result = from panel in _appContext.MassInterviewPanel
                         join vacnacy in _appContext.JobVacancy on panel.VacancyId equals vacnacy.Id
                         join interview in _appContext.MassInterviewDetail on panel.InterviewDateId equals interview.Id
                         where interview.MassInterviewId == massId
                         select new MassInterviewPanel { Name = panel.Name, BreakEndTime = panel.BreakEndTime, BreakStartTime = panel.BreakStartTime, StartTime = panel.StartTime, EndTime = panel.EndTime, Id = panel.Id, JobVacancy = new JobVacancy { JobTitle = vacnacy.JobTitle }, MassInterviewDetail = new MassInterviewDetail { Date = interview.Date } };

            return result.ToList();

        }

        public MassInterviewDetail GetInterviewDetal(Guid detailId)
        {
            return _appContext.MassInterviewDetail.Where(m => m.Id == detailId).FirstOrDefault();

        }

        public List<JobApplication> GetCandidateList()
        {
            var result = from jobApp in _appContext.JobApplication
                         join panel in _appContext.MassInterviewPanel on jobApp.JobVacancyId equals panel.VacancyId
                         select new JobApplication { JobCandidateProfileId = jobApp.JobCandidateProfileId, JobVacancyId = panel.VacancyId };

            return result.OrderBy(m => m.OverallScore).ToList();

        }

        public PagedList<MassInterview> GetAllMassinterview(string name = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var listMassinterview = _appContext.MassInterview.AsQueryable().Where(c => c.IsActive);
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                listMassinterview = listMassinterview.Where(c => c.Venue.Contains(name));
            listMassinterview = listMassinterview.OrderBy(c => c.Venue).ThenBy(c => c.Id);
            return new PagedList<MassInterview>(listMassinterview, pageIndex, pageSize);
        }

        public MassInterview GetSheduleList(Guid id)
        {
            var sheduleId = _appContext.MassInterview.Where(m => m.Id == id && m.IsActive == true).FirstOrDefault();
            return sheduleId;
        }
    }

}


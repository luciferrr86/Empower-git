using A4.DAL.Entites;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace A4.DAL.Repositories
{
    public interface IMassSchedulingRepository : IRepository<MassInterview>
    {
        List<JobVacancy> GetAllJob();
      
        List<JobCandidateProfile> GetListcandidate();
        void CreateInterviewDetail(MassInterviewDetail entity);

        void CreateRooms(MassInterviewRoom entity);

        List<MassInterviewRoom> GetRooms();

        List<MassInterviewDetail> GetInterviewDate(Guid massId);


        MassInterviewDetail GetInterviewDetal(Guid detailId);


        List<JobApplication> GetCandidateList();

        List<MassInterviewPanel> GetPanelList(Guid massId);

        void CreateInterviewPanel(MassInterviewPanel entity);
        void CreateInterviewPanelVacancy(MassInterviewPanelVacancy entity);
        List<Employee> GetSelectedManagerList();
        PagedList<MassInterview> GetAllMassinterview(string name = "", int pageIndex = 0, int pageSize = int.MaxValue);
        MassInterview GetSheduleList(Guid id);


    }
}
